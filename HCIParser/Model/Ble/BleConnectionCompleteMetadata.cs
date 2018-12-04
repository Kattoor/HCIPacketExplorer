using System;

namespace HCIParser.Model.Ble
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
            
            var connectionHandle = BitConverter.ToString(ConnectionHandle).Split("-");
            var peerAddress = BitConverter.ToString(PeerAddress).Split("-");
            var connectionInterval = BitConverter.ToString(ConnectionInterval).Split("-");
            var connectionLatency = BitConverter.ToString(ConnectionLatency).Split("-");
            var connectionTimeout = BitConverter.ToString(ConnectionTimeout).Split("-");
            
            return "{" +
                   $"\"bleEventCode\": \"{BleEventCode}\"," +
                   $"\"status\": \"{Status}\"," +
                   $"\"connectionHandle\": [{(connectionHandle.Length > 0 && connectionHandle[0] != "" ? "\"" + string.Join("\",\"", connectionHandle) + "\"" : "")}]," +
                   $"\"role\": \"{Role}\"," +
                   $"\"peerAddressType\": \"{PeerAddressType}\"," +
                   $"\"peerAddress\": [{(peerAddress.Length > 0 && peerAddress[0] != "" ? "\"" + string.Join("\",\"", peerAddress) + "\"" : "")}]," +
                   $"\"connectionInterval\": [{(connectionInterval.Length > 0 && connectionInterval[0] != "" ? "\"" + string.Join("\",\"", connectionInterval) + "\"" : "")}]," +
                   $"\"connectionLatency\": [{(connectionLatency.Length > 0 && connectionLatency[0] != "" ? "\"" + string.Join("\",\"", connectionLatency) + "\"" : "")}]," +
                   $"\"connectionTimeout\": [{(connectionTimeout.Length > 0 && connectionTimeout[0] != "" ? "\"" + string.Join("\",\"", connectionTimeout) + "\"" : "")}]," +
                   $"\"clockAccuracy\": \"{ClockAccuracy}\"" +
                   "}";
        }
    }
}