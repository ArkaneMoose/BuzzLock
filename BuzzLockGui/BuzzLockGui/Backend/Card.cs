using System;

namespace BuzzLockGui.Backend
{
    /// <summary>
    /// Allows a user to authenticate using a magstripe card.
    /// </summary>
    public class Card : IAuthenticationMethod, IEquatable<Card>
    {
        /// <summary>
        /// The identification string on the magnetic stripe of a card.
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// Creates a magstripe card.
        /// </summary>
        /// <param name="id">
        /// The identification string on the magnetic stripe of this card.
        /// </param>
        public Card(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id cannot be null");
            }
            Id = id;
        }

        public bool Equals(Card other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is Card && Equals((Card)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Card: {Id}";
        }
    }
}
