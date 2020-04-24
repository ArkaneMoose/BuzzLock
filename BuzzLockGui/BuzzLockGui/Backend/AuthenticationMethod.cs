namespace BuzzLockGui.Backend
{
    /// <summary>
    /// A device that a <see cref="User"/> can use to authenticate.
    /// </summary>
    public abstract class AuthenticationMethod
    {
        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();

        public static bool operator ==(AuthenticationMethod a, AuthenticationMethod b)
            => a is null ? b is null : a.Equals(b);
        public static bool operator !=(AuthenticationMethod a, AuthenticationMethod b)
            => !(a == b);
    }
}
