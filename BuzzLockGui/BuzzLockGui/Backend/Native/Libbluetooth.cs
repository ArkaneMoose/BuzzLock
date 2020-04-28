using System;
using System.Runtime.InteropServices;

namespace BuzzLockGui.Backend.Native
{
    class Libbluetooth
    {
        internal const int HCI_UP = 0;

        [DllImport("libbluetooth.so", SetLastError = true, ExactSpelling = true)]
        internal static extern int hci_for_each_dev(int flag, HciForEachDevCallback func, long arg);
        [DllImport("libbluetooth.so", SetLastError = true, ExactSpelling = true)]
        internal static extern int hci_open_dev(int dev_id);
        [DllImport("libbluetooth.so", SetLastError = true, ExactSpelling = true)]
        internal static extern int hci_close_dev(int dd);
        [DllImport("libbluetooth.so", SetLastError = true, ExactSpelling = true)]
        internal static extern int hci_read_rssi(int dd, ushort handle, out sbyte rssi, int to);
        internal static ushort htobs(ushort x)
            => BitConverter.IsLittleEndian ? x : (ushort)(((x >> 8) & 0xFF) | ((x & 0xFF) << 8));

        [StructLayout(LayoutKind.Sequential)]
        internal struct sockaddr_l2
        {
            internal ushort l2_family;
            internal ushort l2_psm;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            internal byte[] l2_bdaddr;
            internal ushort l2_cid;
            internal byte l2_bdaddr_type;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct hci_conn_list_req
        {
            internal ushort dev_id;
            /// <summary>
            /// <c>conn_num &lt; 32</c> not supported. Must be at most 32!
            /// </summary>
            internal ushort conn_num;
            /// <summary>
            /// Variable array length not supported. Must have length 32!
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            internal hci_conn_info[] conn_info;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct hci_conn_info
        {
            internal ushort handle;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            internal byte[] bdaddr;
            internal byte type;
            internal byte @out;
            internal ushort state;
            internal uint link_mode;
        }

        internal delegate int HciForEachDevCallback(int dd, int dev_id, long arg);
    }
}
