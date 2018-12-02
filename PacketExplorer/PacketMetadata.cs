using System;
using System.Linq;

namespace PacketExplorer
{
    public class PacketMetadata
    {
        public string HciPacketType { protected get; set; }
    }

    public class PacketMetadataCommand : PacketMetadata
    {
        public string OpCode { private get; set; }
        public int ParameterTotalLength { private get; set; }
        public byte[] Parameters { private get; set; }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                "PacketMetadataCommand:",
                $"\tPacket Type: {HciPacketType}",
                $"\tOpCode: {OpCode}",
                $"\tParameter Total Length: {ParameterTotalLength}",
                $"\tParameters: {BitConverter.ToString(Parameters).Replace("-", ":")}");
        }
    }

    public class LowEnergyConnectionCompleteEventMetadata
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
                "\tLowEnergyConnectionCompleteEventMetadata:",
                $"\t\tSub Event: {SubEvent}",
                $"\t\tStatus: {Status}",
                $"\t\tConnectionHandle: {BitConverter.ToString(ConnectionHandle).Replace("-", ":")}",
                $"\t\tRole: {Role}",
                $"\t\tPeerAddressType: {PeerAddressType}",
                $"\t\tBdAddress: {BitConverter.ToString(BdAddress.Reverse().ToArray()).Replace("-", ":")}");
        }
    }

    public class PacketMetadataEvent : PacketMetadata
    {
        public string EventCode { private get; set; }
        public int ParameterTotalLength { private get; set; }
        public byte[] Parameters { private get; set; }
        public LowEnergyConnectionCompleteEventMetadata LowEnergyConnectionCompleteEventMetadata { private get; set; }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                "PacketMetadataEvent:",
                $"\tPacket Type: {HciPacketType}",
                $"\tEventCode: {EventCode}",
                $"\tParameter Total Length: {ParameterTotalLength}",
                $"\tParameters: {BitConverter.ToString(Parameters).Replace("-", ":")}",
                LowEnergyConnectionCompleteEventMetadata?.ToString());
        }
    }

    public class PacketMetadataAsynchronousData : PacketMetadata
    {
        public int ConnectionHandle { private get; set; }
        public int PbFlag { private get; set; }
        public int BcFlag { private get; set; }
        public int DataTotalLength { private get; set; }
        public byte[] Data { private get; set; }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                "PacketMetadataAsynchronousData:",
                $"\tPacket Type: {HciPacketType}",
                $"\tConnection Handle: {ConnectionHandle}",
                $"\tPbFlag: {PbFlag}",
                $"\tBcFlag: {BcFlag}",
                $"\tDataTotalLength: {DataTotalLength}",
                $"\tData: {BitConverter.ToString(Data).Replace("-", ":")}");
        }
    }
}