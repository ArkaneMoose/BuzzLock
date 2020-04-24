using System.Collections.Generic;

namespace BuzzLockGui.Backend
{
    /// <summary>
    /// A model of a user as it exists in the database.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The possible levels of access for any user.
        /// </summary>
        public enum PermissionLevel
        {
            /// <summary>
            /// Cannot unlock the door lock.
            /// </summary>
            NONE = 0,
            /// <summary>
            /// Can unlock the door lock, but cannot manage other users.
            /// </summary>
            LIMITED = 1,
            /// <summary>
            /// Can unlock the door lock and manage other users.
            /// </summary>
            FULL = 2
        }

        /// <summary>
        /// The unique ID of this user in the database.
        /// </summary>
        public readonly long Id;

        /// <summary>
        /// The name of the user. Cannot be <c>null</c>.
        /// </summary>
        public string Name
        {
            get => Backend.GetUserName(Id);
            set => Backend.SetUserName(Id, value);
        }

        /// <summary>
        /// The level of access of this user.
        /// </summary>
        public PermissionLevel permissionLevel
        {
            get => Backend.GetUserPermissionLevel(Id);
            set => Backend.SetUserPermissionLevel(Id, value);
        }

        /// <summary>
        /// The phone number of the user. Cannot be <c>null</c>.
        /// </summary>
        public string PhoneNumber
        {
            get => Backend.GetUserPhoneNumber(Id);
            set => Backend.SetUserPhoneNumber(Id, value);
        }

        /// <summary>
        /// The user's photo as a binary blob in an implementation-defined format.
        /// </summary>
        /// <remarks>
        /// Each access to this field returns a new <c>byte[]</c> with the latest
        /// information from the database. Mutating this <c>byte[]</c> has no impact on
        /// the user's photo in the database; the photo is only updated on an assignment
        /// to this field.
        /// </remarks>
        public byte[] Photo
        {
            get => Backend.GetUserPhoto(Id);
            set => Backend.SetUserPhoto(Id, value);
        }

        /// <summary>
        /// The devices or passcodes this user uses to authenticate. Contains exactly two
        /// <see cref="IAuthenticationMethod"/>s of different types. Cannot be
        /// <c>null</c>.
        /// </summary>
        /// <remarks>
        /// Each access to this field returns a new <see cref="HashSet{T}"/> with the
        /// latest information from the database. Mutating this <see cref="HashSet{T}"/>
        /// has no impact on the user's authentication methods in the database; the
        /// user's authentication methods are only updated on an assignment to this
        /// field.
        /// </remarks>
        public HashSet<IAuthenticationMethod> AuthenticationMethods
        {
            get => Backend.GetUserAuthenticationMethods(Id);
            set => Backend.SetUserAuthenticationMethods(Id, value);
        }

        /// <summary>
        /// Creates a model for a user with the given ID in the database.
        /// </summary>
        /// <param name="id">The database ID for this user.</param>
        public User(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Deletes the user from the database. Continuing to use this <see cref="User"/>
        /// object after a call to this method will result in undefined behavior.
        /// </summary>
        public void Delete()
        {
            Backend.DeleteUser(Id);
        }

        /// <summary>
        /// Creates a new user in the database with the given details.
        /// </summary>
        /// <param name="name">
        /// The name of the user. Cannot be <c>null</c>.
        /// </param>
        /// <param name="permissionLevel">
        /// The level of access of this user.
        /// </param>
        /// <param name="phoneNumber">
        /// The phone number of the user. Cannot be <c>null</c>.
        /// </param>
        /// <param name="photo">
        /// The user's photo as a binary blob in an implementation-defined format.
        /// </param>
        /// <param name="authenticationMethods">
        /// The devices or passcodes this user uses to authenticate. Must contain exactly
        /// two <see cref="IAuthenticationMethod"/>s of different types. Cannot be
        /// <c>null</c>.
        /// </param>
        /// <returns>The newly created user.</returns>
        public static User Create(
            string name,
            PermissionLevel permissionLevel,
            string phoneNumber,
            byte[] photo,
            HashSet<IAuthenticationMethod> authenticationMethods)
        {
            long id = Backend.CreateUser(name, permissionLevel, phoneNumber, photo,
                authenticationMethods);
            return new User(id);
        }
    }
}
