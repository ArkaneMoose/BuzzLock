using System;

namespace BuzzLockGui.Backend
{
    /// <summary>
    /// Allows a user to authenticate using a PIN.
    /// </summary>
    public class Pin : AuthenticationMethod, IEquatable<Pin>
    {
        /// <summary>
        /// The PIN itself.
        /// </summary>
        public readonly string PinValue;

        /// <summary>
        /// Creates a PIN.
        /// </summary>
        public Pin(string pin)
        {
            if (pin == null)
            {
                throw new ArgumentNullException("pin cannot be null");
            }
            PinValue = pin;
        }

        public bool Equals(Pin other)
        {
            return other is object && PinValue == other.PinValue;
        }

        public override bool Equals(object obj)
        {
            return obj is Pin && Equals((Pin)obj);
        }

        public override int GetHashCode()
        {
            return PinValue.GetHashCode();
        }

        public override string ToString()
        {
            return $"PIN: {PinValue}";
        }
    }
}
