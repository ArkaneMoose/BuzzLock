﻿using System;
using System.Linq;

namespace BuzzLockGui.Backend
{
    /// <summary>
    /// Represents the 6-byte address of a Bluetooth device.
    /// </summary>
    public class BluetoothAddress : IEquatable<BluetoothAddress>
    {
        private readonly long address;

        /// <summary>
        /// Creates a <see cref="BluetoothAddress"/> from a <c>string</c>. Parses any
        /// Bluetooth address in a standard format: either 12 hexadecimal digits, or 6
        /// groups of 2 hexadecimal digits with non-alphanumeric separators in between.
        /// </summary>
        public BluetoothAddress(string address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }

            string[] octets;
            switch (address.Length)
            {
                case 12:
                    octets = (string[])Enumerable.Range(0, 6)
                                                 .Select(i => address.Substring(i * 2, 2));
                    break;

                case 17:
                    char sep = address[2];
                    octets = address.Split(sep);
                    if (char.IsLetterOrDigit(sep)
                        || octets.Length != 6
                        || !octets.All(octet => octet.Length == 2))
                    {
                        throw new ArgumentException("Invalid separator");
                    }
                    break;

                default:
                    throw new ArgumentException("Invalid length");
            }

            this.address = 0L;
            foreach (string octet in octets)
            {
                this.address <<= 8;
                this.address |= Convert.ToByte(octet, 16);
            }
        }

        /// <summary>
        /// Creates a <see cref="BluetoothAddress"/> from an array of 6 bytes.
        /// </summary>
        public BluetoothAddress(byte[] address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }
            if (address.Length != 6)
            {
                throw new ArgumentException("Invalid length");
            }

            this.address = 0L;
            foreach (byte octet in address.Reverse())
            {
                this.address <<= 8;
                this.address |= octet;
            }
        }

        /// <summary>
        /// Creates a <see cref="BluetoothAddress"/> from a 64-bit integer. The lowest 48
        /// bits are used.
        /// </summary>
        public BluetoothAddress(long address)
        {
            this.address = address & 0xFFFFFFFFFFFFL;
        }

        public string ToString(bool uppercase = true, string separator = ":")
        {
            return string.Join(
                separator,
                ToBytes().Reverse().Select(octet => octet.ToString(uppercase ? "X2" : "x2")));
        }

        public override string ToString()
        {
            return ToString();
        }

        public byte[] ToBytes()
        {
            byte[] buffer = new byte[6];
            long addressTemp = address;
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)(addressTemp & 0xFF);
                addressTemp >>= 8;
            }
            return buffer;
        }

        public long ToInt64()
        {
            return address;
        }

        public bool Equals(BluetoothAddress other)
        {
            return other is object && address == other.address;
        }

        public override bool Equals(object obj)
        {
            return obj is BluetoothAddress && Equals((BluetoothAddress)obj);
        }

        public override int GetHashCode()
        {
            return address.GetHashCode();
        }

        public static implicit operator string(BluetoothAddress address)
            => address is object ? address.ToString() : null;
        public static implicit operator byte[](BluetoothAddress address)
            => address is object ? address.ToBytes() : null;
        public static implicit operator long?(BluetoothAddress address)
            => address is object ? (long?)address.ToInt64() : null;
        public static implicit operator long(BluetoothAddress address)
            => address.ToInt64();
        public static implicit operator BluetoothAddress(string address)
            => address is object ? new BluetoothAddress(address) : null;
        public static implicit operator BluetoothAddress(byte[] address)
            => address is object ? new BluetoothAddress(address) : null;
        public static implicit operator BluetoothAddress(long? address)
            => address is object ? new BluetoothAddress((long)address) : null;
        public static implicit operator BluetoothAddress(long address)
            => new BluetoothAddress(address);
        public static bool operator ==(BluetoothAddress a, BluetoothAddress b)
            => a is null ? b is null : a.Equals(b);
        public static bool operator !=(BluetoothAddress a, BluetoothAddress b)
            => !(a == b);
    }
}
