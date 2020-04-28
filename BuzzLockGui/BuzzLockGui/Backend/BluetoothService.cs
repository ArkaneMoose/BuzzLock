using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using BuzzLockGui.Backend.Native;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Concurrent;
using System.Data.SQLite;
using NDesk.DBus;
using org.bluez;

namespace BuzzLockGui.Backend
{
    /// <summary>
    /// The Bluetooth discovery and monitoring service.
    /// </summary>
    public static class BluetoothService
    {
        private const short RSSI_THRESHOLD_BT_CLASSIC = 0;
        private const short RSSI_THRESHOLD_BT_LOW_ENERGY = -60;

        private const string busName = "org.bluez";
        private const ushort l2capPsm = 31;
        private static readonly Connection system = Bus.System;
        private static ObjectManager objectManager
            = system.GetObject<ObjectManager>(busName, new ObjectPath("/"));
        private static HashSet<IAdapter1> adaptersDiscovering = new HashSet<IAdapter1>();
        private static ConcurrentDictionary<BluetoothAddress, BluetoothConnection> connections
            = new ConcurrentDictionary<BluetoothAddress, BluetoothConnection>();
        private static Mode mode = Mode.UNINITIALIZED;
        private static Task modeSwitchTask = Task.CompletedTask;

        /// <summary>
        /// The state of the Bluetooth discovery and monitoring service.
        /// </summary>
        public enum Mode
        {
            UNINITIALIZED,
            SCANNING,
            MONITORING
        }

        /// <summary>
        /// Immediately gets the current state of the Bluetooth discovery and
        /// monitoring service, or <c>null</c> if currently switching states.
        /// </summary>
        /// <returns>
        /// A <see cref="Mode"/> if no mode switch is pending, or <c>null</c>
        /// otherwise.
        /// </returns>
        public static Mode? GetMode()
        {
            return modeSwitchTask.IsCompleted ? (Mode?)mode : null;
        }

        /// <summary>
        /// Asynchronously gets the state of the Bluetooth discovery and
        /// monitoring service after any pending mode switches are completed.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that results in a <see cref="Mode"/>.
        /// </returns>
        public static async Task<Mode> GetModeAsync()
        {
            await modeSwitchTask;
            return mode;
        }

        /// <summary>
        /// Asynchronously changes the state of the Bluetooth discovery and
        /// monitoring service to the given mode.
        /// </summary>
        /// <param name="desiredMode">
        /// The desired <see cref="Mode"/> to switch to.
        /// </param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that resolves to a <see cref="bool"/>
        /// that is <c>true</c> if the mode switch was successful or <c>false</c>
        /// otherwise (e.g. if the mode switch was interrupted by a request to
        /// change to a different mode).
        /// </returns>
        /// <remarks>
        /// <para>
        /// See the remarks for <see cref="GetAvailableBluetoothDevices"/> for
        /// a description of how the different modes behave.
        /// </para>
        /// <para>
        /// If the desired mode is already set, this method acts as a harmless
        /// no-op.
        /// </para>
        /// </remarks>
        public static async Task<bool> SetModeAsync(Mode desiredMode)
        {
            Mode prevMode = mode;
            mode = desiredMode;
            if (modeSwitchTask.IsCompleted && prevMode != desiredMode)
            {
                modeSwitchTask = ChangeMode(prevMode);
            }
            await modeSwitchTask;
            return mode == desiredMode;
        }

        /// <summary>
        /// Gets all available Bluetooth devices in range. What counts as
        /// available depends on the mode as reported by <see cref="GetMode"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> of the available
        /// <see cref="BluetoothDevice"/>s.
        /// </returns>
        /// <remarks>
        /// <para>
        /// When the mode is <see cref="Mode.SCANNING"/>, all discoverable
        /// Bluetooth devices are returned. Discoverable Bluetooth devices are
        /// the ones with visible names.
        /// </para>
        /// <para>
        /// When the mode is <see cref="Mode.MONITORING"/>, the behavior
        /// differs for classic Bluetooth devices and BLE devices. The service
        /// tries to connect to all known classic Bluetooth devices constantly.
        /// The RSSI can be fetched for any connected classic Bluetooth device,
        /// so classic Bluetooth devices are returned if they are connected and
        /// their RSSI is greater than <see cref="RSSI_THRESHOLD_BT_CLASSIC"/>.
        /// In contrast, BLE devices are scanned using the same discovery
        /// procedure as for <see cref="Mode.SCANNING"/>, but only devices with
        /// addresses in the database are considered, and the service does not
        /// require them to have their names discoverable at time of
        /// monitoring.
        /// </para>
        /// <para>
        /// Any other mode, in particular <see cref="Mode.UNINITIALIZED"/>,
        /// causes an <see cref="InvalidOperationException"/> to be thrown. The
        /// same is also thrown if a mode switch is in progress. To wait until
        /// all mode switches are complete before completing, use
        /// <see cref="GetAvailableBluetoothDevicesAsync"/>.
        /// </para>
        /// </remarks>
        public static IEnumerable<BluetoothDevice> GetAvailableBluetoothDevices()
        {
            Mode? mode = GetMode();
            Console.Write($"Mode is null: {mode is null}");
            Console.Write($"Mode switch task is null: {modeSwitchTask is null}");
            Console.Write($"Mode switch task: {modeSwitchTask}");
            switch (mode)
            {
                case Mode.SCANNING:
                    return GetAvailableBluetoothDevicesFromScan();
                case Mode.MONITORING:
                    return GetAvailableBluetoothDevicesFromMonitoring();
                default:
                    throw new InvalidOperationException(
                        "Must set mode for Bluetooth service or wait for mode "
                        + "change to complete");
            }
        }

        /// <summary>
        /// Asynchronously gets all available Bluetooth devices in range. What
        /// counts as available depends on the mode as reported by
        /// <see cref="GetMode"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that resolves to an
        /// <see cref="IEnumerable{T}"/> of the available
        /// <see cref="BluetoothDevice"/>s.
        /// </returns>
        /// <remarks>
        /// <para>
        /// When the mode is <see cref="Mode.SCANNING"/>, all discoverable
        /// Bluetooth devices are returned. Discoverable Bluetooth devices are
        /// the ones with visible names.
        /// </para>
        /// <para>
        /// When the mode is <see cref="Mode.MONITORING"/>, the behavior
        /// differs for classic Bluetooth devices and BLE devices. The service
        /// tries to connect to all known classic Bluetooth devices constantly.
        /// The RSSI can be fetched for any connected classic Bluetooth device,
        /// so classic Bluetooth devices are returned if they are connected and
        /// their RSSI is greater than <see cref="RSSI_THRESHOLD_BT_CLASSIC"/>.
        /// In contrast, BLE devices are scanned using the same discovery
        /// procedure as for <see cref="Mode.SCANNING"/>, but only devices with
        /// addresses in the database are considered, and the service does not
        /// require them to have their names discoverable at time of
        /// monitoring.
        /// </para>
        /// <para>
        /// Any other mode, in particular <see cref="Mode.UNINITIALIZED"/>,
        /// causes an <see cref="InvalidOperationException"/> to be thrown.
        /// </para>
        /// </remarks>
        public static async Task<IEnumerable<BluetoothDevice>>
            GetAvailableBluetoothDevicesAsync()
        {
            Mode mode = await GetModeAsync();
            switch (mode)
            {
                case Mode.SCANNING:
                    return GetAvailableBluetoothDevicesFromScan();
                case Mode.MONITORING:
                    return GetAvailableBluetoothDevicesFromMonitoring();
                default:
                    throw new InvalidOperationException(
                        "Must set mode for Bluetooth service");
            }
        }

        private static IEnumerable<BluetoothDevice>
            GetAvailableBluetoothDevicesFromScan()
        {
            string iface = GetDBusInterfaceName<IDevice1>();
            return objectManager.GetManagedObjects()
                .Where(pair => pair.Value.ContainsKey(iface))
                .Select(pair => pair.Value[iface])
                .Where(device =>
                    device.ContainsKey("Name")
                    && !string.IsNullOrEmpty((string)device["Name"])
                    && device.ContainsKey("RSSI"))
                .Select(device =>
                    new BluetoothDevice(
                        (string)device["Address"], (string)device["Name"]));
        }

        private static IEnumerable<BluetoothDevice>
            GetAvailableBluetoothDevicesFromMonitoring()
        {
            Console.WriteLine("GetAvailableBluetoothDevicesFromMonitoring"); // !!DEBUG!!
            var known = new HashSet<BluetoothAddress>(
                Backend.GetAllBluetoothAddresses());
            var hcis = new Dictionary
                <int, List<(BluetoothAddress address, ushort handle)>>();

            Console.WriteLine("Get...FromMonitoring: calling hci_for_each_dev"); // !!DEBUG!!
            Libbluetooth.hci_for_each_dev(Libbluetooth.HCI_UP, (sockfd, hci, arg) =>
            {
                Console.WriteLine("Get...FromMonitoring: in hci_for_each_dev"); // !!DEBUG!!
                var request = new Libbluetooth.hci_conn_list_req
                {
                    dev_id = (ushort)hci,
                    conn_num = 32,
                    conn_info = new Libbluetooth.hci_conn_info[32]
                };
                Console.WriteLine($"Get...FromMonitoring: making ioctl HCIGETCONNLIST for hci {hci}"); // !!DEBUG!!
                if (Libc.ioctl(sockfd, Libbluetooth.HCIGETCONNLIST, ref request) < 0)
                {
                    Libc.perror("Can't get connection list");
                    return 0;
                }
                Console.WriteLine($"Get...FromMonitoring: got {request.conn_num} results"); // !!DEBUG!!
                for (int i = 0; i < request.conn_num; i++)
                {
                    var connInfo = request.conn_info[i];
                    var address = new BluetoothAddress(connInfo.bdaddr);
                    Console.WriteLine($"Get...FromMonitoring: got addr {address}"); // !!DEBUG!!
                    if (known.Remove(address))
                    {
                        Console.WriteLine($"Get...FromMonitoring: this addr is known"); // !!DEBUG!!
                        if (!hcis.ContainsKey(hci))
                        {
                            Console.WriteLine($"Get...FromMonitoring: this addr is first for hci"); // !!DEBUG!!
                            hcis[hci] = new List
                                <(BluetoothAddress address, ushort handle)>();
                        }
                        Console.WriteLine($"Get...FromMonitoring: adding addr {address} and handle {connInfo.handle}"); // !!DEBUG!!
                        hcis[hci].Add((address, connInfo.handle));
                        if (known.Count == 0)
                        {
                            Console.WriteLine($"Get...FromMonitoring: this was last known device, exiting hci_for_each_dev"); // !!DEBUG!!
                            return 1;
                        }
                    }
                }
                return 0;
            }, 0);

            Console.WriteLine($"Get...FromMonitoring: classicInRange"); // !!DEBUG!!
            IEnumerable<BluetoothDevice> classicInRange = hcis.SelectMany(pair =>
            {
                Console.WriteLine($"Get...FromMonitoring: hci_open_dev {pair.Key}"); // !!DEBUG!!
                int hciFd = Libbluetooth.hci_open_dev(pair.Key);
                if (hciFd < 0)
                {
                    Libc.perror("Can't open HCI device");
                    return new BluetoothDevice[0];
                }
                try
                {
                    return (IEnumerable<BluetoothDevice>)pair.Value
                        .Where(addressHandlePair =>
                        {
                            ushort handle = addressHandlePair.handle;
                            int ok = Libbluetooth.hci_read_rssi(
                                hciFd, Libbluetooth.htobs(handle),
                                out sbyte rssi, 1000);
                            if (ok < 0)
                            {
                                Libc.perror("Can't read RSSI");
                                return false;
                            }
                            return rssi >= RSSI_THRESHOLD_BT_CLASSIC;
                        })
                        .Select(addressHandlePair =>
                            new BluetoothDevice(addressHandlePair.address))
                        .ToList();
                }
                finally
                {
                    if (Libbluetooth.hci_close_dev(hciFd) < 0)
                    {
                        Libc.perror("Can't close HCI device");
                    }
                }
            }).ToList();

            Console.WriteLine($"Get...FromMonitoring: get iface"); // !!DEBUG!!
            string iface = GetDBusInterfaceName<IDevice1>();
            Console.WriteLine($"Get...FromMonitoring: iface is {iface}. GetManagedObjectsAsync"); // !!DEBUG!!
            try
            { // !!DEBUG!!
            var objects = objectManager.GetManagedObjects();
            Console.WriteLine($"Get...FromMonitoring: got {objects.Count} objects"); // !!DEBUG!!
            IEnumerable<BluetoothDevice> bleInRange = objects
                .Where(pair => pair.Value.ContainsKey(iface))
                .Select(pair => pair.Value[iface])
                .Where(device =>
                    device.ContainsKey("RSSI")
                    && (short)device["RSSI"] >= RSSI_THRESHOLD_BT_LOW_ENERGY)
                .Select(device => new BluetoothAddress((string)device["Address"]))
                .Where(address => known.Remove(address))
                .Select(address => new BluetoothDevice(address));

            Console.WriteLine($"Get...FromMonitoring: return"); // !!DEBUG!!
            return new IEnumerable<BluetoothDevice>[]
            {
                classicInRange,
                bleInRange
            }.SelectMany(devices => devices);
            }
            catch (Exception e) { Console.WriteLine(e); } // !!DEBUG!!
            return new BluetoothDevice[0];
        }

        private static async Task ChangeMode(Mode fromMode)
        {
            await ExitMode(fromMode);
            Mode toMode = mode;
            EnterMode(toMode);
        }

        private static void EnterMode(Mode mode)
        {
            switch (mode)
            {
                case Mode.SCANNING:
                    Console.WriteLine("EnterMode: entering SCANNING mode"); // !!DEBUG!!
                    StartDiscovery(new Dictionary<string, object>());
                    Console.WriteLine("EnterMode: started discovery"); // !!DEBUG!!
                    break;
                case Mode.MONITORING:
                    Console.WriteLine("EnterMode: entering MONITORING mode"); // !!DEBUG!!
                    OpenConnections();
                    Backend.Update += OnDatabaseUpdate;
                    Console.WriteLine("EnterMode: start discovery"); // !!DEBUG!!
                    StartDiscovery(new Dictionary<string, object>
                    {
                        ["Transport"] = "le"
                    });
                    Console.WriteLine("EnterMode: started LE discovery"); // !!DEBUG!!
                    break;
            }
        }

        private static async Task ExitMode(Mode mode)
        {
            switch (mode)
            {
                case Mode.SCANNING:
                    StopDiscovery();
                    break;
                case Mode.MONITORING:
                    Backend.Update -= OnDatabaseUpdate;
                    StopDiscovery();
                    await CloseConnections();
                    break;
            }
        }

        private static IEnumerable<ObjectPath>
            GetObjectPathsWithInterface(string iface)
        {
            var objects = objectManager.GetManagedObjects();
            return objects
                .Where(pair => pair.Value.ContainsKey(iface))
                .Select(pair => pair.Key);
        }

        private static IEnumerable<T> GetObjectsWithInterface<T>()
        {
            return CreateProxies<T>(
                GetObjectPathsWithInterface(GetDBusInterfaceName<T>()));
        }

        private static string GetDBusInterfaceName<T>()
        {
            InterfaceAttribute attribute = (InterfaceAttribute)
                Attribute.GetCustomAttribute(typeof(T), typeof(InterfaceAttribute));
            return attribute.Name;
        }

        private static IEnumerable<T>
            CreateProxies<T>(IEnumerable<ObjectPath> objectPaths)
        {
            return objectPaths.Select(
                path => system.GetObject<T>(busName, path));
        }

        private static void StartDiscovery(IDictionary<string, object> filter)
        {
            StartDiscovery(GetObjectsWithInterface<IAdapter1>(), filter);
        }

        private static void StartDiscovery(
            IEnumerable<IAdapter1> adapters, IDictionary<string, object> filter)
        {
            foreach (IAdapter1 adapter in adapters)
            {
                StartDiscovery(adapter, filter);
            }
        }

        private static void StartDiscovery(
            IAdapter1 adapter, IDictionary<string, object> filter)
        {
            adapter.SetDiscoveryFilter(filter);
            adapter.StartDiscovery();
            adaptersDiscovering.Add(adapter);
        }

        private static void StopDiscovery()
        {
            StopDiscovery(adaptersDiscovering);
        }

        private static void StopDiscovery(IEnumerable<IAdapter1> adapters)
        {
            foreach (IAdapter1 adapter in adapters)
            {
                StopDiscovery(adapter);
            }
        }

        private static void StopDiscovery(IAdapter1 adapter)
        {
            adapter.StopDiscovery();
            adaptersDiscovering.Remove(adapter);
        }

        private static Task CloseConnections()
        {
            return Task.WhenAll(connections.Select(connection =>
                CloseConnection(connection.Value)));
        }

        private static Task CloseConnection(BluetoothAddress address)
        {
            BluetoothConnection connection;
            if (connections.TryGetValue(address, out connection))
            {
                return CloseConnection(connection);
            }
            return Task.CompletedTask;
        }

        private static Task CloseConnection(BluetoothConnection connection)
        {
            return connection.Stop();
        }

        private static IEnumerable<BluetoothConnection> OpenConnections()
        {
            return Backend.GetAllBluetoothAddresses()
                .Select(OpenConnection)
                .ToList();
        }

        private static BluetoothConnection OpenConnection(BluetoothAddress address)
        {
            Console.WriteLine($"OpenConnection({address})"); // !!DEBUG!!
            return BluetoothConnection.Start(address);
        }

        private static void OnDatabaseUpdate(object sender, UpdateEventArgs e)
        {
            IEnumerable<BluetoothAddress> after = Backend.GetAllBluetoothAddresses();
            IEnumerable<BluetoothAddress> before = connections.Keys;
            IEnumerable<BluetoothAddress> removed = before.Except(after);
            IEnumerable<BluetoothAddress> added = after.Except(before);
            foreach (BluetoothAddress address in removed)
            {
                CloseConnection(address);
            }
            foreach (BluetoothAddress address in added)
            {
                OpenConnection(address);
            }
        }

        private class BluetoothConnection
        {
            private int sockfd = -1;
            private BluetoothAddress address;
            private CancellationTokenSource cancellationTokenSource
                = new CancellationTokenSource();
            private CancellationToken cancellationToken;
            private Task task;
            internal bool StopRequested =>
                cancellationTokenSource.IsCancellationRequested;

            private BluetoothConnection(BluetoothAddress address)
            {
                Console.WriteLine($"new BluetoothConnection({address})"); // !!DEBUG!!
                this.address = address;
                cancellationToken = cancellationTokenSource.Token;
                task = Task.Factory.StartNew(
                    TaskTarget,
                    cancellationToken,
                    TaskCreationOptions.LongRunning,
                    TaskScheduler.Default);
                connections[address] = this;
            }

            internal static BluetoothConnection Start(BluetoothAddress address)
            {
                if (connections.ContainsKey(address))
                {
                    return null;
                }
                return new BluetoothConnection(address);
            }

            internal async Task Stop()
            {
                if (!StopRequested)
                {
                    cancellationTokenSource.Cancel();
                    if (sockfd >= 0)
                    {
                        Libc.shutdown(sockfd, Libc.SHUT_RDWR);
                    }
                }
                try
                {
                    await task;
                }
                catch (OperationCanceledException) { }
            }

            private void TaskTarget()
            {
                Console.WriteLine($"TaskTarget: started for {address}"); // !!DEBUG!!
                try
                {
                    Libbluetooth.sockaddr_l2 addr = new Libbluetooth.sockaddr_l2
                    {
                        l2_family = Libc.AF_BLUETOOTH,
                        l2_psm = Libbluetooth.htobs(l2capPsm),
                        l2_bdaddr = address
                    };
                    while (true)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        sockfd = Libc.socket(
                            Libc.AF_BLUETOOTH,
                            Libc.SOCK_SEQPACKET,
                            Libc.BTPROTO_L2CAP);
                        if (sockfd < 0)
                        {
                            Libc.perror("Couldn't create Bluetooth socket");
                            continue;
                        }
                        try
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            int ok = Libc.connect(
                                sockfd, ref addr, (uint)Marshal.SizeOf(addr));
                            if (ok < 0)
                            {
                                Libc.perror("Couldn't connect");
                                continue;
                            }

                            byte[] buffer = new byte[16];
                            int received;
                            do
                            {
                                cancellationToken.ThrowIfCancellationRequested();
                                received = Libc.recv(
                                    sockfd, buffer, (uint)buffer.Length, 0);
                            }
                            while (received > 0);
                            if (received < 0)
                            {
                                Libc.perror("Receive error");
                                continue;
                            }
                        }
                        finally
                        {
                            if (Libc.close(sockfd) < 0)
                            {
                                Libc.perror("Couldn't close socket");
                            }
                            sockfd = -1;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    Console.WriteLine($"TaskTarget: exited for {address}"); // !!DEBUG!!
                    connections.TryRemove(address, out _);
                }
            }
        }
    }
}
