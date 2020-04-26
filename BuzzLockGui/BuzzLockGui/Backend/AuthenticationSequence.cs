using System;
using System.Collections.Generic;
using System.Linq;

namespace BuzzLockGui.Backend
{
    /// <summary>
    /// Represents the process of authenticating a user, usually with more than one
    /// authentication method.
    /// </summary>
    public class AuthenticationSequence
    {
        private readonly HashSet<AuthenticationMethod> remaining;

        /// <summary>
        /// The user being authenticated.
        /// </summary>
        public readonly User User;
        /// <summary>
        /// The next <see cref="AuthenticationMethod"/> the user must present, or
        /// <c>null</c> if authentication has been completed successfully.
        /// </summary>
        public AuthenticationMethod NextAuthenticationMethod { get; private set; }

        private AuthenticationSequence(User user, AuthenticationMethod completed)
        {
            User = user;
            remaining = new HashSet<AuthenticationMethod>(
                user.AuthenticationMethods);
            if (!remaining.Remove(completed))
            {
                throw new ArgumentException("Cannot start authentication sequence "
                    + "with an authentication method the user doesn't have");
            }
            NextAuthenticationMethod = remaining.First();
        }

        /// <summary>
        /// Continue the authentication sequence with this authentication method.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the authentication method was accepted, or <c>false</c>
        /// otherwise.
        /// </returns>
        /// <remarks>
        /// If this method returns <c>true</c>, do not assume that the user has been
        /// successfully authenticated. Instead, check
        /// <see cref="AuthenticationSequence.NextAuthenticationMethod"/>, which is
        /// <c>null</c> if authentication is successful or otherwise indicates the
        /// next authentication method the user must present.
        /// </remarks>
        public bool Continue(
            AuthenticationMethod authenticationMethod)
        {
            if (!remaining.Remove(authenticationMethod
                ?? throw new ArgumentNullException("authenticationMethod")))
            {
                return false;
            }
            if (remaining.Count == 0)
            {
                NextAuthenticationMethod = null;
                return true;
            }
            NextAuthenticationMethod = remaining.First();
            return true;
        }

        /// <summary>
        /// Begin an authentication sequence using the given authentication method.
        /// </summary>
        /// <param name="authenticationMethod">
        /// The authentication method presented. Can be a <see cref="Card"/> or a
        /// <see cref="BluetoothDevice"/>.
        /// </param>
        /// <returns>
        /// An <see cref="AuthenticationSequence"/> if there exists a user with the
        /// given authentication method, or <c>null</c> otherwise.
        /// </returns>
        public static AuthenticationSequence Start(
            AuthenticationMethod authenticationMethod)
        {
            long? nullableUserId = Backend.GetUserIdForAuthenticationMethod(
                authenticationMethod ?? throw new ArgumentNullException(
                    "authenticationMethod"));
            if (nullableUserId is long userId)
            {
                return new AuthenticationSequence(
                    new User(userId), authenticationMethod);
            }
            return null;
        }
    }
}
