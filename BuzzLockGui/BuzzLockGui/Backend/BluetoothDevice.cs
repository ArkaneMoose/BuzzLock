using System;

namespace BuzzLockGui.Backend
{
    /// <summary>
    /// Allows a user to authenticate using a Bluetooth device.
    /// </summary>
    public class BluetoothDevice : IAuthenticationMethod, IEquatable<BluetoothDevice>
    {
        /// <summary>
        /// The address of this Bluetooth device.
        /// </summary>
        public readonly BluetoothAddress Address;
        private string name;
        /// <summary>
        /// The name of this Bluetooth device.
        /// </summary>
        /// <remarks>
        /// Unless set explicitly, this name is accessed from the table of users in the
        /// database. When adding a new Bluetooth device as an authentication method,
        /// this name must be set explicitly, as the device does not exist in the users
        /// table of the database.
        /// </remarks>
        public string Name
        {
            get => name == null ? Backend.GetBluetoothDeviceName(Address) : name;
            set => name = value;
        }

        /// <summary>
        /// Creates a new Bluetooth device with the given address and name.
        /// </summary>
        public BluetoothDevice(BluetoothAddress address, string name)
        {
            Address = address
                ?? throw new ArgumentNullException("address cannot be null");
            this.name = name;
        }

        /// <summary>
        /// Creates a new Bluetooth device with the given address.
        /// </summary>
        public BluetoothDevice(BluetoothAddress address) : this(address, null) { }

        public override string ToString()
        {
            string name = Name;
            if (name != null)
            {
                return $"{name} ({Address})";
            }
            return Address.ToString();
        }

        public bool Equals(BluetoothDevice other)
        {
            return Address == other.Address;
        }

        public override bool Equals(object obj)
        {
            return obj is BluetoothDevice && Equals((BluetoothDevice)obj);
        }

        public override int GetHashCode()
        {
            return Address.GetHashCode();
        }
    }
}
