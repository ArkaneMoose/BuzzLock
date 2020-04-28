using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuzzLockGui.Backend
{
    /// <summary>
    /// Allows a user to authenticate using a Bluetooth device.
    /// </summary>
    public class BluetoothDevice : AuthenticationMethod, IEquatable<BluetoothDevice>
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
                ?? throw new ArgumentNullException("address");
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
            return other is object && Address == other.Address;
        }

        public override bool Equals(object obj)
        {
            return obj is BluetoothDevice && Equals((BluetoothDevice)obj);
        }

        public override int GetHashCode()
        {
            return Address.GetHashCode();
        }

        /// <summary>
        /// Gets all available Bluetooth devices in range. This is an alias for
        /// <see cref="BluetoothService.GetAvailableBluetoothDevices"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> of the available
        /// <see cref="BluetoothDevice"/>s.
        /// </returns>
        /// <remarks>
        /// See the documentation for
        /// <see cref="BluetoothService.GetAvailableBluetoothDevices"/> for
        /// details.
        /// </remarks>
        public static IEnumerable<BluetoothDevice> GetAllAvailable()
        {
            return BluetoothService.GetAvailableBluetoothDevices();
        }

        /// <summary>
        /// Asynchronously gets all available Bluetooth devices in range. This
        /// is an alias for
        /// <see cref="BluetoothService.GetAvailableBluetoothDevicesAsync"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that resolves to an
        /// <see cref="IEnumerable{T}"/> of the available
        /// <see cref="BluetoothDevice"/>s.
        /// </returns>
        /// <remarks>
        /// See the documentation for
        /// <see cref="BluetoothService.GetAvailableBluetoothDevicesAsync"/> for
        /// details.
        /// </remarks>
        public static Task<IEnumerable<BluetoothDevice>> GetAllAvailableAsync()
        {
            return BluetoothService.GetAvailableBluetoothDevicesAsync();
        }
    }
}
