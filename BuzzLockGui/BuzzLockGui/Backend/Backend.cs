using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace BuzzLockGui.Backend
{
    /// <summary>
    /// A class to interact with the database. Generally only used from within the
    /// <see cref="BuzzLockGui.Backend"/> namespace. API consumers should use the
    /// higher-level <see cref="AuthenticationSequence"/> and <see cref="User"/>
    /// classes instead.
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
                InitializeDatabase(conn);
                return conn;
            });
        private static SQLiteConnection conn => _conn.Value;
        internal static event SQLiteUpdateEventHandler Update
        {
            add => conn.Update += value;
            remove => conn.Update -= value;
        }

        private static void InitializeDatabase(SQLiteConnection conn)
        {
            conn.Open();
            SQLiteCommand cmd;

            cmd = new SQLiteCommand(
                @"CREATE TABLE IF NOT EXISTS users (
                    id INTEGER PRIMARY KEY,
                    name TEXT NOT NULL,
                    permissionLevel INTEGER NOT NULL,
                    phone TEXT NOT NULL,
                    photo BLOB,
                    cardId TEXT UNIQUE,
                    bluetoothId INTEGER UNIQUE,
                    bluetoothName TEXT,
                    pin TEXT,
                    CHECK (
                        (cardId IS NOT NULL AND bluetoothId IS NOT NULL AND pin IS NULL)
                        OR (cardId IS NOT NULL AND bluetoothId IS NULL AND pin IS NOT NULL)
                        OR (cardId IS NULL AND bluetoothId IS NOT NULL AND pin IS NOT NULL)
                    ),
                    CHECK ((bluetoothId IS NULL) == (bluetoothName IS NULL))
                )", conn);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand(
                @"CREATE TABLE IF NOT EXISTS auditLog (
                    datetime INTEGER NOT NULL,
                    message TEXT NOT NULL
                )", conn);
            cmd.ExecuteNonQuery();
        }

        internal static long CreateUser(
            string name,
            User.PermissionLevels permissionLevel,
            string phoneNumber,
            byte[] photo,
            AuthenticationMethods authenticationMethods)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (phoneNumber == null)
            {
                throw new ArgumentNullException("phoneNumber");
            }
            if (authenticationMethods == null)
            {
                throw new ArgumentNullException("authenticationMethods");
            }
            if (authenticationMethods.BluetoothDevice != null
                && authenticationMethods.BluetoothDevice.Name == null)
            {
                throw new ArgumentException("You must explicitly set a Name "
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
            cmd.Parameters.AddWithValue("@cardId", authenticationMethods.Card?.Id);
            cmd.Parameters.AddWithValue("@bluetoothId",
                (long?)authenticationMethods.BluetoothDevice?.Address);
            cmd.Parameters.AddWithValue("@bluetoothName",
                authenticationMethods.BluetoothDevice?.Name);
            cmd.Parameters.AddWithValue("@pin", authenticationMethods.Pin?.PinValue);
            if (cmd.ExecuteNonQuery() == 0)
            {
                throw new InvalidOperationException("User not found");
            }
            return conn.LastInsertRowId;
        }

        internal static void DeleteUser(long userId)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "DELETE FROM users WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            if (cmd.ExecuteNonQuery() == 0)
            {
                throw new InvalidOperationException("User not found");
            }
        }

        internal static List<long> GetAllUserIds()
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT id FROM users", conn);
            List<long> userIds = new List<long>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    userIds.Add(reader.GetInt64(0));
                }
            }
            return userIds;
        }

        internal static List<BluetoothAddress> GetAllBluetoothAddresses()
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT bluetoothId FROM users WHERE bluetoothId IS NOT NULL", conn);
            List<BluetoothAddress> bluetoothAddresses = new List<BluetoothAddress>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    bluetoothAddresses.Add(reader.GetInt64(0));
                }
            }
            Console.WriteLine($"GetAllBluetoothAddresses: Found {bluetoothAddresses.Count}"); // !!DEBUG!!
            return bluetoothAddresses;
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
                throw new ArgumentNullException("address");
            }
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT id FROM users WHERE bluetoothId = @address", conn);
            cmd.Parameters.AddWithValue("@address", (long)address);
            return (long?)cmd.ExecuteScalar();
        }

        internal static long? GetUserIdForAuthenticationMethod(
            AuthenticationMethod authenticationMethod)
        {
            switch (authenticationMethod
                ?? throw new ArgumentNullException("authenticationMethod"))
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
                throw new ArgumentNullException("address");
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
            object result = cmd.ExecuteScalar();
            if (result == null)
            {
                throw new InvalidOperationException("User not found");
            }
            return (string)result;
        }

        internal static User.PermissionLevels GetUserPermissionLevel(long userId)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT permissionLevel FROM users WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            object result = cmd.ExecuteScalar();
            if (result == null)
            {
                throw new InvalidOperationException("User not found");
            }
            return (User.PermissionLevels)(long)cmd.ExecuteScalar();
        }

        internal static string GetUserPhoneNumber(long userId)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT phone FROM users WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            object result = cmd.ExecuteScalar();
            if (result == null)
            {
                throw new InvalidOperationException("User not found");
            }
            return (string)cmd.ExecuteScalar();
        }

        internal static byte[] GetUserPhoto(long userId)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT photo FROM users WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            object result = cmd.ExecuteScalar();
            if (result == null)
            {
                throw new InvalidOperationException("User not found");
            }
            if (Convert.IsDBNull(result))
            {
                return null;
            }
            return (byte[])cmd.ExecuteScalar();
        }

        internal static AuthenticationMethods GetUserAuthenticationMethods(
            long userId)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "SELECT cardId, bluetoothId, pin FROM users WHERE id = @userId",
                conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            HashSet<AuthenticationMethod> authenticationMethods
                = new HashSet<AuthenticationMethod>();
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
            return new AuthenticationMethods(authenticationMethods);
        }

        internal static void SetUserName(long userId, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE users SET name = @name WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@name", name);
            if (cmd.ExecuteNonQuery() == 0)
            {
                throw new InvalidOperationException("User not found");
            }
        }

        internal static void SetUserPermissionLevel(long userId,
            User.PermissionLevels permissionLevel)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                @"UPDATE users SET permissionLevel = @permissionLevel
                  WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@permissionLevel", (long)permissionLevel);
            if (cmd.ExecuteNonQuery() == 0)
            {
                throw new InvalidOperationException("User not found");
            }
        }

        internal static void SetUserPhoneNumber(long userId, string phoneNumber)
        {
            if (phoneNumber == null)
            {
                throw new ArgumentNullException("phoneNumber");
            }

            SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE users SET phone = @phoneNumber WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
            if (cmd.ExecuteNonQuery() == 0)
            {
                throw new InvalidOperationException("User not found");
            }
        }

        internal static void SetUserPhoto(long userId, byte[] photo)
        {
            SQLiteCommand cmd = new SQLiteCommand(
                "UPDATE users SET photo = @photo WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@photo", photo);
            if (cmd.ExecuteNonQuery() == 0)
            {
                throw new InvalidOperationException("User not found");
            }
        }

        internal static void SetUserAuthenticationMethods(
            long userId, AuthenticationMethods authenticationMethods)
        {
            if (authenticationMethods == null)
            {
                throw new ArgumentNullException("authenticationMethods");
            }
            if (authenticationMethods.BluetoothDevice != null
                && authenticationMethods.BluetoothDevice.Name == null)
            {
                throw new ArgumentException("You must explicitly set a Name "
                    + "for this BluetoothDevice");
            }
            SQLiteCommand cmd = new SQLiteCommand(
                @"UPDATE users SET cardId = @cardId, bluetoothId = @bluetoothId,
                  bluetoothName = @bluetoothName, pin = @pin WHERE id = @userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@cardId", authenticationMethods.Card?.Id);
            cmd.Parameters.AddWithValue("@bluetoothId",
                (long?)authenticationMethods.BluetoothDevice?.Address);
            cmd.Parameters.AddWithValue("@bluetoothName",
                authenticationMethods.BluetoothDevice?.Name);
            cmd.Parameters.AddWithValue("@pin", authenticationMethods.Pin?.PinValue);
            if (cmd.ExecuteNonQuery() == 0)
            {
                throw new InvalidOperationException("User not found");
            }
        }
    }
}
