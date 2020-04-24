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
    public class AuthenticationMethods : IReadOnlyList<IAuthenticationMethod>
    {
        /// <summary>
        /// A <see cref="Card"/> if the user uses a magstripe card to authenticate,
        /// or <c>null</c> otherwise.
        /// </summary>
        public readonly Card Card = null;
        /// <summary>
        /// A <see cref="BluetoothDevice"/> if the user uses a Bluetooth device to
        /// authenticate, or <c>null</c> otherwise.
        /// </summary>
        public readonly BluetoothDevice BluetoothDevice = null;
        /// <summary>
        /// A <see cref="Pin"/> if the user uses a PIN to authenticate, or
        /// <c>null</c> otherwise.
        /// </summary>
        public readonly Pin Pin = null;

        /// <summary>
        /// The primary authentication method. Either a <see cref="Card"/> or a
        /// <see cref="BluetoothDevice"/>.
        /// </summary>
        public IAuthenticationMethod Primary
            => (IAuthenticationMethod)Card ?? BluetoothDevice;
        /// <summary>
        /// The secondary authentication method. Either a
        /// <see cref="BluetoothDevice"/> or a <see cref="Pin"/>.
        /// </summary>
        public IAuthenticationMethod Secondary
            => (IAuthenticationMethod)Pin ?? BluetoothDevice;

        /// <summary>
        /// Create a new set of authentication methods from the two
        /// <see cref="IAuthenticationMethod"/>s passed in.
        /// </summary>
        public AuthenticationMethods(
            params IAuthenticationMethod[] authenticationMethods)
            : this(authenticationMethods, authenticationMethods.Length) { }

        /// <summary>
        /// Create a new set of authentication methods from a
        /// <see cref="IReadOnlyCollection{T}"/>, such as a <see cref="List{T}"/>
        /// or a <see cref="HashSet{T}"/>.
        /// </summary>
        /// <param name="authenticationMethods">
        /// A read-only or read-write collection containing two
        /// <see cref="IAuthenticationMethod"/>s.
        /// </param>
        public AuthenticationMethods(
            IReadOnlyCollection<IAuthenticationMethod> authenticationMethods)
            : this(authenticationMethods, authenticationMethods.Count) { }

        private AuthenticationMethods(
            IEnumerable<IAuthenticationMethod> authenticationMethods, int Count)
        {
            if (Count != 2)
            {
                throw new ArgumentException(
                    "Exactly two authentication methods required");
            }

            foreach (
                IAuthenticationMethod authenticationMethod in authenticationMethods)
            {
                switch (authenticationMethod
                    ?? throw new ArgumentNullException("None of the authentication "
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
        /// Given one <see cref="IAuthenticationMethod"/> in this
        /// <see cref="AuthenticationMethods"/> instance, return the other.
        /// </summary>
        /// <param name="authenticationMethod">
        /// One of the two <see cref="IAuthenticationMethod"/>s in this
        /// <see cref="AuthenticationMethods"/> instance.
        /// </param>
        /// <returns>
        /// The other of the two <see cref="IAuthenticationMethod"/>s in this
        /// <see cref="AuthenticationMethods"/> instance.
        /// </returns>
        public IAuthenticationMethod GetOtherAuthenticationMethod(
            IAuthenticationMethod authenticationMethod)
        {
            if (authenticationMethod == null)
            {
                throw new ArgumentNullException(
                    "authenticationMethod cannot be null");
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
        /// Access the <see cref="IAuthenticationMethod"/>s in this
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
        public IAuthenticationMethod this[int i]
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
        /// The number of <see cref="IAuthenticationMethod"/>s in this
        /// <see cref="AuthenticationMethods"/>. Always 2.
        /// </summary>
        public int Count => 2;

        public IEnumerator<IAuthenticationMethod> GetEnumerator()
            => new Enumerator(this);
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private class Enumerator : IEnumerator<IAuthenticationMethod>
        {
            private AuthenticationMethods authenticationMethods;
            private int position = -1;

            internal Enumerator(
                AuthenticationMethods authenticationMethods)
            {
                this.authenticationMethods = authenticationMethods;
            }

            public IAuthenticationMethod Current
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
