using System;
using System.Globalization;

namespace PacketExplorer
{
    public class PacketRecord
    {
        public uint OriginalLength { private get; set; }
        public uint IncludedLength { private get; set; }
        public uint PacketFlags { private get; set; }
        public uint CumulativeDrops { private get; set; }
        public ulong Timestamp { private get; set; }
        public byte[] PacketData { get; set; }

        public PacketMetadata Metadata { private get; set; }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                "----PacketRecord----",
                $"Length (Original, Included): ({OriginalLength}, {IncludedLength})",
                $"Packet Flags: {PacketFlags}",
                $"Cumulative Drops: {CumulativeDrops}",
                $"Timestamp: {TimeZoneInfo.ConvertTimeFromUtc(DateTime.UnixEpoch.AddMilliseconds((long) ((Timestamp - 0x00dcddb30f2f8000) / 1000)), TimeZoneInfo.Local).ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}",
                $"Packet Data: {BitConverter.ToString(PacketData).Replace("-",":")}",
                Metadata.ToString());
        }
    }
}