using System;
using System.Collections;
using System.Collections.Generic;

namespace BuzzLockGui.Backend
{
    /// <summary>
    /// The devices or passcodes a user uses to authenticate.
    /// </summary>
    /// <remarks>
    /// This class implements the <see cref="IReadOnlyList{T}"/> interface, allowing
    /// API consumers to use objects of this type in <c>foreach</c> loops and with
    /// LINQ extension methods, among other uses.
    /// </remarks>
    public class AuthenticationMethods : IReadOnlyList<AuthenticationMethod>
    {
        /// <summary>
        /// A <see cref="BuzzLockGui.Backend.Card"/> if the user uses a
        /// magstripe card to authenticate, or <c>null</c> otherwise.
        /// </summary>
        public readonly Card Card = null;
        /// <summary>
        /// A <see cref="BuzzLockGui.Backend.BluetoothDevice"/> if the user
        /// uses a Bluetooth device to authenticate, or <c>null</c> otherwise.
        /// </summary>
        public readonly BluetoothDevice BluetoothDevice = null;
        /// <summary>
        /// A <see cref="BuzzLockGui.Backend.Pin"/> if the user uses a PIN to
        /// authenticate, or <c>null</c> otherwise.
        /// </summary>
        public readonly Pin Pin = null;

        /// <summary>
        /// The primary authentication method. Either a
        /// <see cref="BuzzLockGui.Backend.Card"/> or a
        /// <see cref="BuzzLockGui.Backend.BluetoothDevice"/>.
        /// </summary>
        public AuthenticationMethod Primary
            => (AuthenticationMethod)Card ?? BluetoothDevice;
        /// <summary>
        /// The secondary authentication method. Either a
        /// <see cref="BuzzLockGui.Backend.BluetoothDevice"/> or a
        /// <see cref="BuzzLockGui.Backend.Pin"/>.
        /// </summary>
        public AuthenticationMethod Secondary
            => (AuthenticationMethod)Pin ?? BluetoothDevice;

        /// <summary>
        /// Create a new set of authentication methods from the two
        /// <see cref="AuthenticationMethod"/>s passed in.
        /// </summary>
        /// <param name="first">
        /// One of the two <see cref="AuthenticationMethod"/>s. Not necessarily
        /// the <see cref="Primary"/> <see cref="AuthenticationMethod"/>.
        /// </param>
        /// <param name="second">
        /// One of the two <see cref="AuthenticationMethod"/>s. Not necessarily
        /// the <see cref="Secondary"/> <see cref="AuthenticationMethod"/>.
        /// </param>
        public AuthenticationMethods(
            AuthenticationMethod first, AuthenticationMethod second)
            : this(new AuthenticationMethod[] { first, second }) { }

        /// <summary>
        /// Create a new set of authentication methods from a
        /// <see cref="IReadOnlyCollection{T}"/>, such as a <see cref="List{T}"/>
        /// or a <see cref="HashSet{T}"/>.
        /// </summary>
        /// <param name="authenticationMethods">
        /// A read-only or read-write collection containing two
        /// <see cref="AuthenticationMethod"/>s.
        /// </param>
        public AuthenticationMethods(
            IReadOnlyCollection<AuthenticationMethod> authenticationMethods)
        {
            if (authenticationMethods.Count != 2)
            {
                throw new ArgumentException(
                    "Exactly two authentication methods required");
            }

            foreach (
                AuthenticationMethod authenticationMethod in authenticationMethods)
            {
                switch (authenticationMethod
                    ?? throw new ArgumentException("None of the authentication "
                        + "methods can be null"))
                {
                    case Card card:
                        if (Card != null)
                        {
                            throw new ArgumentException("Cannot have more than one "
                                + "Card authentication method");
                        }
                        Card = card;
                        break;
                    case BluetoothDevice bluetoothDevice:
                        if (BluetoothDevice != null)
                        {
                            throw new ArgumentException("Cannot have more than one "
                                + "BluetoothDevice authentication method");
                        }
                        BluetoothDevice = bluetoothDevice;
                        break;
                    case Pin pin:
                        if (Pin != null)
                        {
                            throw new ArgumentException("Cannot have more than one "
                                + "Pin authentication method");
                        }
                        Pin = pin;
                        break;
                    default:
                        throw new ArgumentException(
                            "Unrecognized authentication method");
                }
            }
        }

        /// <summary>
        /// Given one <see cref="AuthenticationMethod"/> in this
        /// <see cref="AuthenticationMethods"/> instance, return the other.
        /// </summary>
        /// <param name="authenticationMethod">
        /// One of the two <see cref="AuthenticationMethod"/>s in this
        /// <see cref="AuthenticationMethods"/> instance.
        /// </param>
        /// <returns>
        /// The other of the two <see cref="AuthenticationMethod"/>s in this
        /// <see cref="AuthenticationMethods"/> instance.
        /// </returns>
        public AuthenticationMethod GetOtherAuthenticationMethod(
            AuthenticationMethod authenticationMethod)
        {
            if (authenticationMethod == null)
            {
                throw new ArgumentNullException("authenticationMethod");
            }
            if (authenticationMethod == Primary)
            {
                return Secondary;
            }
            if (authenticationMethod == Secondary)
            {
                return Primary;
            }
            throw new ArgumentException(
                "This AuthenticationMethods instance does not contain the provided "
                + "authenticationMethod");
        }

        /// <summary>
        /// Access the <see cref="AuthenticationMethod"/>s in this
        /// <see cref="AuthenticationMethods"/>.
        /// </summary>
        /// <param name="i">An index, 0 or 1.</param>
        /// <returns>
        /// <see cref="Primary"/> if <c>i</c> is 0, or
        /// <see cref="Secondary"/> if <c>i</c> is 1.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// If <c>i</c> is neither 0 nor 1.
        /// </exception>
        public AuthenticationMethod this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return Primary;
                    case 1:
                        return Secondary;
                    default:
                        throw new IndexOutOfRangeException(
                            "Index must be 0 or 1");
                }
            }
        }

        /// <summary>
        /// The number of <see cref="AuthenticationMethod"/>s in this
        /// <see cref="AuthenticationMethods"/>. Always 2.
        /// </summary>
        public int Count => 2;

        public IEnumerator<AuthenticationMethod> GetEnumerator()
            => new Enumerator(this);
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private class Enumerator : IEnumerator<AuthenticationMethod>
        {
            private AuthenticationMethods authenticationMethods;
            private int position = -1;

            internal Enumerator(
                AuthenticationMethods authenticationMethods)
            {
                this.authenticationMethods = authenticationMethods;
            }

            public AuthenticationMethod Current
            {
                get
                {
                    try
                    {
                        return authenticationMethods[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public bool MoveNext()
            {
                position++;
                return position < authenticationMethods.Count;
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current => Current;
            public void Dispose() { }
        }
    }
}
