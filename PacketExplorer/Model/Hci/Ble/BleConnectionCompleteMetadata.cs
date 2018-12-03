using System;
using System.Linq;

namespace PacketExplorer.Model.Hci.Ble
{
    public class BleConnectionCompleteMetadata
    {
        public string SubEvent { private get; set; }
        public int Status { private get; set; }
        public byte[] ConnectionHandle { private get; set; }
        public int Role { private get; set; }
        public int PeerAddressType { private get; set; }
        public byte[] BdAddress { private get; set; }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                "\t\t----BleConnectionCompleteMetadata----",
                $"\t\tSub Event: {SubEvent}",
                $"\t\tStatus: {Status}",
                $"\t\tConnectionHandle: {BitConverter.ToString(ConnectionHandle).Replace("-", ":")}",
                $"\t\tRole: {Role}",
                $"\t\tPeerAddressType: {PeerAddressType}",
                $"\t\tBdAddress: {BitConverter.ToString(BdAddress.Reverse().ToArray()).Replace("-", ":")}");
        }
    }
}