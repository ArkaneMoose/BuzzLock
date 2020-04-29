using System.Collections.Generic;
using NDesk.DBus;

namespace org.bluez
{
    [Interface("org.bluez.AgentManager1")]
    interface IAgentManager1
    {
        void RegisterAgent(ObjectPath Agent, string Capability);
        void UnregisterAgent(ObjectPath Agent);
        void RequestDefaultAgent(ObjectPath Agent);
    }

    [Interface("org.bluez.ProfileManager1")]
    interface IProfileManager1
    {
        void RegisterProfile(ObjectPath Profile, string UUID, IDictionary<string, object> Options);
        void UnregisterProfile(ObjectPath Profile);
    }

    [Interface("org.bluez.Adapter1")]
    interface IAdapter1
    {
        void StartDiscovery();
        void SetDiscoveryFilter(IDictionary<string, object> Properties);
        void StopDiscovery();
        void RemoveDevice(ObjectPath Device);
        string[] GetDiscoveryFilters();
    }

    [Interface("org.bluez.GattManager1")]
    interface IGattManager1
    {
        void RegisterApplication(ObjectPath Application, IDictionary<string, object> Options);
        void UnregisterApplication(ObjectPath Application);
    }

    [Interface("org.bluez.LEAdvertisingManager1")]
    interface ILEAdvertisingManager1
    {
        void RegisterAdvertisement(ObjectPath Advertisement, IDictionary<string, object> Options);
        void UnregisterAdvertisement(ObjectPath Service);
    }

    [Interface("org.bluez.Media1")]
    interface IMedia1
    {
        void RegisterEndpoint(ObjectPath Endpoint, IDictionary<string, object> Properties);
        void UnregisterEndpoint(ObjectPath Endpoint);
        void RegisterPlayer(ObjectPath Player, IDictionary<string, object> Properties);
        void UnregisterPlayer(ObjectPath Player);
    }

    [Interface("org.bluez.NetworkServer1")]
    interface INetworkServer1
    {
        void Register(string Uuid, string Bridge);
        void Unregister(string Uuid);
    }

    [Interface("org.bluez.Device1")]
    interface IDevice1
    {
        void Disconnect();
        void Connect();
        void ConnectProfile(string UUID);
        void DisconnectProfile(string UUID);
        void Pair();
        void CancelPairing();
    }

    [Interface("org.bluez.Network1")]
    interface INetwork1
    {
        string Connect(string Uuid);
        void Disconnect();
    }
}