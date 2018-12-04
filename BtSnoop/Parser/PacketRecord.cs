using System;
using System.Globalization;

namespace BtSnoop.Parser
{
    public class PacketRecord
    {
        public uint OriginalLength { private get; set; }
        public uint IncludedLength { private get; set; }
        public uint PacketFlags { private get; set; }
        public uint CumulativeDrops { private get; set; }
        public ulong Timestamp { private get; set; }
        public byte[] PacketData { get; set; }

        public override string ToString()
        {
            var packetData = BitConverter.ToString(PacketData).Split("-"); 
            
            return "{" +
                   $"\"originalLength\": \"{OriginalLength}\"," +
                   $"\"includedLength\": \"{IncludedLength}\"," +
                   $"\"packetFlags\": \"{PacketFlags}\"," +
                   $"\"cumulativeDrops\": \"{CumulativeDrops}\"," +
                   $"\"timestamp\": \"{TimeZoneInfo.ConvertTimeFromUtc(DateTime.UnixEpoch.AddMilliseconds((long) ((Timestamp - 0x00dcddb30f2f8000) / 1000)), TimeZoneInfo.Local).ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}\"," +
                   $"\"packetData\": [{(packetData.Length > 0 && packetData[0] != "" ? "\"" + string.Join("\",\"", packetData) + "\"" : "")}]" +
                   "}";
        }
    }
}