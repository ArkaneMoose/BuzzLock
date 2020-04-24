﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace BuzzLockGui.Backend
{
    /// <summary>
    /// A class to interact with the database. Generally only used from within the
    /// <see cref="BuzzLockGui.Backend"/> namespace. API consumers should use the
    /// higher-level <see cref="AuthenticationSequence"/> and <see cref="User"/> classes
    /// instead.
    /// </summary>
    static class Backend
    {
        private static readonly string CONNECTION_STRING
            = new SQLiteConnectionStringBuilder()
            {
                DataSource = "buzzlock.db",
                Version = 3
            }.ToString();

        private static readonly Lazy<SQLiteConnection> _conn
            = new Lazy<SQLiteConnection>(() =>
            {
                var conn = new SQLiteConnection(CONNECTION_STRING);
                conn.Open();
                return conn;
            });
        private static SQLiteConnection conn => _conn.Value;

        internal static long CreateUser(
            string name,
            User.PermissionLevel permissionLevel,
            string phoneNumber,
            byte[] photo,
            HashSet<IAuthenticationMethod> authenticationMethods)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name cannot be null");
            }
            if (phoneNumber == null)
            {
                throw new ArgumentNullException("phoneNumber cannot be null");
            }
            (Card card, BluetoothDevice bluetoothDevice, Pin pin)
                = DestructureAuthenticationMethods(authenticationMethods);
            if (bluetoothDevice is BluetoothDevice bluetoothDeviceNonNull
                && bluetoothDeviceNonNull.Name == null)
            {
                throw new ArgumentNullException("You must explicitly set a Name "
                    + "for the BluetoothDevice when creating a user");
            }

            SQLiteCommand cmd = new SQLiteCommand(
                @"INSERT INTO users (name, permissionLevel, phone, photo,
                  cardId, bluetoothId, bluetoothName, pin) VALUES (@name,
                  @permissionLevel, @phoneNumber, @photo, @cardId, @bluetoothId,
                  @bluetoothName, @pin)", conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@permissionLevel", permissionLevel);
            cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
            cmd.Parameters.AddWithValue("@photo", photo);
            cmd.Parameters.AddWithValue("@cardId", card?.Id);
            cmd.Parameters.AddWithValue("@bluetoothId", (long)bluetoothDevice?.Address);
            cmd.Parameters.AddWithValue("@bluetoothName", bluetoothDevice?.Name);
            cmd.Parameters.AddWithValue("@pin", pin?.PinValue);
            cmd.ExecuteNonQueryOrThrow();
            return conn.LastInsertRowId;
        }

        internal static void DeleteUser(long userId)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "DELETE FROM users WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.ExecuteNonQueryOrThrow();
        }

        internal static long? GetUserIdForCard(string cardId)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT id FROM users WHERE cardId = @cardId", conn);
            cmd.Parameters.AddWithValue("@cardId", cardId);
            return (long?)cmd.ExecuteScalar();
        }

        internal static long? GetUserIdForBluetoothAddress(BluetoothAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address cannot be null");
            }
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT id FROM users WHERE bluetoothId = @address", conn);
            cmd.Parameters.AddWithValue("@address", (long)address);
            return (long?)cmd.ExecuteScalar();
        }

        internal static long? GetUserIdForAuthenticationMethod(
            IAuthenticationMethod authenticationMethod)
        {
            switch (authenticationMethod
                ?? throw new ArgumentNullException("authenticationMethod cannot be null"))
            {
                case Card card:
                    return GetUserIdForCard(card.Id);
                case BluetoothDevice bluetoothDevice:
                    return GetUserIdForBluetoothAddress(bluetoothDevice.Address);
                default:
                    throw new ArgumentException("Cannot query user by this type of "
                        + "authentication method");
            }
        }

        internal static string GetBluetoothDeviceName(BluetoothAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address cannot be null");
            }
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT bluetoothName FROM users WHERE bluetoothId = @address", conn);
            cmd.Parameters.AddWithValue("@address", (long)address);
            return (string)cmd.ExecuteScalar();
        }

        internal static string GetUserName(long userId)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT name FROM users WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            return (string)cmd.ExecuteScalarOrThrow();
        }

        internal static User.PermissionLevel GetUserPermissionLevel(long userId)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT permissionLevel FROM users WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            return (User.PermissionLevel)cmd.ExecuteScalarOrThrow();
        }

        internal static string GetUserPhoneNumber(long userId)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT phone FROM users WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            return (string)cmd.ExecuteScalarOrThrow();
        }

        internal static byte[] GetUserPhoto(long userId)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT photo FROM users WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            return (byte[])cmd.ExecuteScalarOrThrow();
        }

        internal static HashSet<IAuthenticationMethod> GetUserAuthenticationMethods(
            long userId)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT cardId, bluetoothId, pin FROM users WHERE id = @userId",
                conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            HashSet<IAuthenticationMethod> authenticationMethods
                = new HashSet<IAuthenticationMethod>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.Read())
                {
                    throw new InvalidOperationException("User not found");
                }
                if (!reader.IsDBNull(0))
                {
                    authenticationMethods.Add(new Card(reader.GetString(0)));
                }
                if (!reader.IsDBNull(1))
                {
                    authenticationMethods.Add(new BluetoothDevice(reader.GetInt64(1)));
                }
                if (!reader.IsDBNull(2))
                {
                    authenticationMethods.Add(new Pin(reader.GetString(2)));
                }
            }
            return authenticationMethods;
        }

        internal static void SetUserName(long userId, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name must not be null");
            }

            SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE users SET name = @name WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQueryOrThrow();
        }

        internal static void SetUserPermissionLevel(long userId,
            User.PermissionLevel permissionLevel)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                @"UPDATE users SET permissionLevel = @permissionLevel
                  WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@permissionLevel", (int)permissionLevel);
            cmd.ExecuteNonQueryOrThrow();
        }

        internal static void SetUserPhoneNumber(long userId, string phoneNumber)
        {
            if (phoneNumber == null)
            {
                throw new ArgumentNullException("phoneNumber must not be null");
            }

            SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE users SET phone = @phoneNumber WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
            cmd.ExecuteNonQueryOrThrow();
        }

        internal static void SetUserPhoto(long userId, byte[] photo)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE users SET photo = @photo WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@photo", photo);
            cmd.ExecuteNonQueryOrThrow();
        }

        internal static void SetUserAuthenticationMethods(
            long userId,
            HashSet<IAuthenticationMethod> authenticationMethods)
        {
            (Card card, BluetoothDevice bluetoothDevice, Pin pin)
                = DestructureAuthenticationMethods(authenticationMethods);
            SQLiteCommand cmd = new SQLiteCommand(
                @"UPDATE users SET cardId = @cardId, bluetoothId = @bluetoothId,
                  bluetoothName = @bluetoothName, pin = @pin WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@cardId", card?.Id);
            cmd.Parameters.AddWithValue("@bluetoothId", (long)bluetoothDevice?.Address);
            cmd.Parameters.AddWithValue("@bluetoothName", bluetoothDevice?.Name);
            cmd.Parameters.AddWithValue("@pin", pin?.PinValue);
            cmd.ExecuteNonQueryOrThrow();
        }

        private static (
            Card card,
            BluetoothDevice bluetoothDevice,
            Pin pin
        ) DestructureAuthenticationMethods(
            HashSet<IAuthenticationMethod> authenticationMethods)
        {
            if (authenticationMethods == null)
            {
                throw new ArgumentNullException("authenticationMethods must not be null");
            }
            if (authenticationMethods.Count != 2)
            {
                throw new ArgumentException("Exactly two authentication methods required");
            }

            Card card = null;
            BluetoothDevice bluetoothDevice = null;
            Pin pin = null;

            foreach (IAuthenticationMethod authenticationMethod in authenticationMethods)
            {
                switch (authenticationMethod
                    ?? throw new ArgumentNullException("None of the authentication "
                        + "methods can be null"))
                {
                    case Card c:
                        if (card != null)
                        {
                            throw new ArgumentException("Cannot have more than one Card "
                                + "authentication method");
                        }
                        card = c;
                        break;
                    case BluetoothDevice b:
                        if (bluetoothDevice != null)
                        {
                            throw new ArgumentException("Cannot have more than one "
                                + "BluetoothDevice authentication method");
                        }
                        bluetoothDevice = b;
                        break;
                    case Pin p:
                        if (pin != null)
                        {
                            throw new ArgumentException("Cannot have more than one Pin "
                                + "authentication method");
                        }
                        pin = p;
                        break;
                    default:
                        throw new ArgumentException("Unrecognized authentication method");
                }
            }

            return (card, bluetoothDevice, pin);
        }
    }

    internal static class SQLiteExtensions
    {
        /**
         * <summary>
         * Like <see cref="IDbCommand.ExecuteScalar(CommandBehavior)"/>,
         * but throws if there are no rows returned.
         * </summary>
         * <exception cref="InvalidOperationException">
         * If there are no rows returned from the query.
         * </exception>
         */
        internal static object ExecuteScalarOrThrow(this IDbCommand command,
            CommandBehavior behavior)
        {
            using (IDataReader reader = command.ExecuteReader(behavior))
            {
                if (!reader.Read())
                {
                    throw new InvalidOperationException("Command did not return a result");
                }
                return reader[0];
            }
        }

        /**
         * <summary>
         * Like <see cref="IDbCommand.ExecuteScalar"/>,
         * but throws if there are no rows returned.
         * </summary>
         * <exception cref="InvalidOperationException">
         * If there are no rows returned from the query.
         * </exception>
         */
        internal static object ExecuteScalarOrThrow(this IDbCommand command)
        {
            return command.ExecuteScalarOrThrow(CommandBehavior.Default);
        }

        /**
         * <summary>
         * Like <see cref="IDbCommand.ExecuteNonQuery"/>,
         * but throws if there are no rows affected.
         * </summary>
         * <exception cref="InvalidOperationException">
         * If there are no rows affected by the query.
         * </exception>
         */
        internal static int ExecuteNonQueryOrThrow(this IDbCommand command)
        {
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Command affected no rows");
            }
            return rowsAffected;
        }
    }
}
