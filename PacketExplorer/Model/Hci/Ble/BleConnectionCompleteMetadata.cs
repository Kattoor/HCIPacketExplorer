using System;
using System.Linq;

namespace PacketExplorer.Model.Hci.Ble
{
    public class BleConnectionCompleteMetadata
    {
        public string BleEventCode { private get; set; }
        public int Status { private get; set; }
        public byte[] ConnectionHandle { private get; set; }
        public int Role { private get; set; }
        public int PeerAddressType { private get; set; }
        public byte[] PeerAddress { private get; set; }
        public byte[] ConnectionInterval { private get; set; }
        public byte[] ConnectionLatency { private get; set; }
        public byte[] ConnectionTimeout { private get; set; }
        public int ClockAccuracy { private get; set; }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                "\t\t----BleConnectionCompleteMetadata----",
                $"\t\tBleEventCode: {BleEventCode}",
                $"\t\tStatus: {Status}",
                $"\t\tConnectionHandle: {BitConverter.ToString(ConnectionHandle).Replace("-", ":")}",
                $"\t\tRole: {Role}",
                $"\t\tPeerAddressType: {PeerAddressType}",
                $"\t\tPeerAddress: {BitConverter.ToString(PeerAddress).Replace("-", ":")}",
                $"\t\tConnectionInterval: {BitConverter.ToString(ConnectionInterval).Replace("-", ":")}",
                $"\t\tConnectionLatency: {BitConverter.ToString(ConnectionLatency).Replace("-", ":")}",
                $"\t\tConnectionTimeout: {BitConverter.ToString(ConnectionTimeout).Replace("-", ":")}",
                $"\t\tClockAccuracy: {ClockAccuracy}");
        }
    }
}