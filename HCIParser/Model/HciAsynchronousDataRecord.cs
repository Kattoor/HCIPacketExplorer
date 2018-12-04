using System;
using HCIParser.Model.AsynchronousData;

namespace HCIParser.Model
{
    public class HciAsynchronousDataRecord : HciRecord
    {
        public byte[] ConnectionHandle { private get; set; }
        public int PbFlag { private get; set; }
        public int BcFlag { private get; set; }
        public int DataTotalLength { private get; set; }
        public byte[] Data { private get; set; }

        public L2CapRecord L2CapRecord { private get; set; }

        public override string ToString()
        {
            var connectionHandle = BitConverter.ToString(ConnectionHandle).Split("-");
            var data = BitConverter.ToString(Data).Split("-");
            
            return "{" +
                   $"\"packetType\": \"{HciPacketType}\"," +
                   $"\"connectionHandle\": [{(connectionHandle.Length > 0 && connectionHandle[0] != "" ? "\"" + string.Join("\",\"", connectionHandle) + "\"" : "")}]," +
                   $"\"pbFlag\": \"{PbFlag}\"," +
                   $"\"bcFlag\": \"{BcFlag}\"," +
                   $"\"dataTotalLength\": \"{DataTotalLength}\"," +
                   $"\"data\": [{(data.Length > 0 && data[0] != "" ? "\"" + string.Join("\",\"", data) + "\"" : "")}]," +
                   $"\"l2CapRecord\": {L2CapRecord?.ToString() ?? "{}"}" +
                   "}";
        }
    }
}