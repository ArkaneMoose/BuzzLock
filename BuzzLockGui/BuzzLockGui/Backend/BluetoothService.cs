using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using BuzzLockGui.Backend.Native;
using System;
using System.Data.SQLite;
using NDesk.DBus;
using org.bluez;
using System.Diagnostics;

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
        private static readonly Connection system = FormBuzzLock.IS_LINUX
            ? Bus.System : null;
        private static ObjectManager objectManager = FormBuzzLock.IS_LINUX
            ? system.GetObject<ObjectManager>(busName, new ObjectPath("/")) : null;
        private static HashSet<IAdapter1> adaptersDiscovering = new HashSet<IAdapter1>();
        private static Dictionary<BluetoothAddress, BluetoothConnection> connections
            = new Dictionary<BluetoothAddress, BluetoothConnection>();
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
            if (!FormBuzzLock.IS_LINUX)
            {
                return Enumerable.Range(1, 16)
                    .Select(i => new BluetoothDevice(
                        0xAABBCCDDEE00 + i, $"Dummy Device {i}"));
            }

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
            if (!FormBuzzLock.IS_LINUX)
            {
                return Backend.GetAllBluetoothAddresses()
                    .Select(address => new BluetoothDevice(address));
            }

            var known = new HashSet<BluetoothAddress>(
                Backend.GetAllBluetoothAddresses());
            var hcis = new Dictionary
                <int, List<(BluetoothAddress address, ushort handle)>>();

            Libbluetooth.hci_for_each_dev(Libbluetooth.HCI_UP, (sockfd, hci, arg) =>
            {
                var request = new Libbluetooth.hci_conn_list_req
                {
                    dev_id = (ushort)hci,
                    conn_num = 32,
                    conn_info = new Libbluetooth.hci_conn_info[32]
                };
                if (Libbluetoothext.hci_get_conn_list(sockfd, ref request) < 0)
                {
                    Libc.perror("Can't get connection list");
                    return 0;
                }
                for (int i = 0; i < request.conn_num; i++)
                {
                    var connInfo = request.conn_info[i];
                    var address = new BluetoothAddress(connInfo.bdaddr);
                    if (known.Remove(address))
                    {
                        if (!hcis.ContainsKey(hci))
                        {
                            hcis[hci] = new List
                                <(BluetoothAddress address, ushort handle)>();
                        }
                        hcis[hci].Add((address, connInfo.handle));
                        if (known.Count == 0)
                        {
                            return 1;
                        }
                    }
                }
                return 0;
            }, 0);

            IEnumerable<BluetoothDevice> classicInRange = hcis.SelectMany(pair =>
            {
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
                            Console.WriteLine("Found RSSI for Bluetooth "
                                + "classic device "
                                + $"{addressHandlePair.address}: {rssi}");
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

            string iface = GetDBusInterfaceName<IDevice1>();
            var objects = objectManager.GetManagedObjects();
            IEnumerable<BluetoothDevice> bleInRange = objects
                .Where(pair => pair.Value.ContainsKey(iface))
                .Select(pair => pair.Value[iface])
                .Where(device =>
                    device.ContainsKey("RSSI")
                    && (short)device["RSSI"] >= RSSI_THRESHOLD_BT_LOW_ENERGY)
                .Select(device => new BluetoothAddress((string)device["Address"]))
                .Where(address => known.Remove(address))
                .Select(address => new BluetoothDevice(address));

            return new IEnumerable<BluetoothDevice>[]
            {
                classicInRange,
                bleInRange
            }.SelectMany(devices => devices);
        }

        private static async Task ChangeMode(Mode fromMode)
        {
            Console.WriteLine($"Exiting..."); // !!DEBUG!!
            await ExitMode(fromMode);
            Mode toMode = mode;
            Console.WriteLine($"Entering..."); // !!DEBUG!!
            EnterMode(toMode);
            Console.WriteLine($"Done"); // !!DEBUG!!
        }

        private static void EnterMode(Mode mode)
        {
            Console.WriteLine($"Changing Bluetooth mode to {mode}");
            if (!FormBuzzLock.IS_LINUX)
            {
                return;
            }
            switch (mode)
            {
                case Mode.SCANNING:
                    StartDiscovery(new Dictionary<string, object>());
                    break;
                case Mode.MONITORING:
                    OpenConnections();
                    Backend.Update += OnDatabaseUpdate;
                    StartDiscovery(new Dictionary<string, object>
                    {
                        ["Transport"] = "le"
                    });
                    break;
            }
        }

        private static async Task ExitMode(Mode mode)
        {
            if (!FormBuzzLock.IS_LINUX)
            {
                return;
            }
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
            try
            {
                adapter.SetDiscoveryFilter(filter);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error setting Bluetooth discovery filter:");
                Console.WriteLine(e);
            }
            try
            {
                adapter.StartDiscovery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error starting Bluetooth discovery:");
                Console.WriteLine(e);
            }
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
            try
            {
                adapter.StopDiscovery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error stopping Bluetooth discovery:");
                Console.WriteLine(e);
            }
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
                Console.WriteLine($"Bluetooth device {address} removed from database");
                CloseConnection(address);
            }
            foreach (BluetoothAddress address in added)
            {
                Console.WriteLine($"Bluetooth device {address} added to database");
                OpenConnection(address);
            }
        }

        private class BluetoothConnection
        {
            private BluetoothAddress address;
            private ProcessStartInfo startInfo;
            private Process process;
            private Task task;
            internal bool StopRequested { get; private set; }

            private BluetoothConnection(BluetoothAddress address)
            {
                startInfo = new ProcessStartInfo
                {
                    FileName = "sudo",
                    Arguments = $"l2ping {(string)address}",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                };
                this.address = address;
                _ = StartProcessLoop();
            }

            internal static BluetoothConnection Start(BluetoothAddress address)
            {
                if (connections.ContainsKey(address))
                {
                    return null;
                }
                return new BluetoothConnection(address);
            }

            private async Task StartProcessLoop()
            {
                connections[address] = this;
                Console.WriteLine($"Starting Bluetooth ping process for {address}");
                while (!StopRequested)
                {
                    StartProcessOnce();
                    await task;
                }
                connections.Remove(address);
                Console.WriteLine($"Stopping Bluetooth ping process for {address}");
            }

            private void StartProcessOnce()
            {
                process = new Process
                {
                    StartInfo = startInfo,
                };
                TaskCompletionSource<int> taskSource
                    = new TaskCompletionSource<int>();
                process.Exited += (object sender, EventArgs e) =>
                {
                    taskSource.SetResult(process.ExitCode);
                };
                process.EnableRaisingEvents = true;
                task = taskSource.Task;
                process.Start();
            }

            internal Task Stop()
            {
                if (!StopRequested)
                {
                    StopRequested = true;
                    Process kill = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "sudo",
                            Arguments = $"kill {process.Id}",
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                        }
                    };
                    kill.Start();
                }
                return task;
            }
        }
    }
}
