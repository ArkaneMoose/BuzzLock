using System.Runtime.InteropServices;

namespace BuzzLockGui.Backend.Native
{
    class Libbluetoothext
    {
        [DllImport("libbluetoothext.so", SetLastError = true, ExactSpelling = true)]
        internal static extern int hci_get_conn_list(int fd, ref Libbluetooth.hci_conn_list_req req);
    }
}
