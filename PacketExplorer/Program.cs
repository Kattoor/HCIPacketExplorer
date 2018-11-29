using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace PacketExplorer
{
    internal static class Program
    {
        private static void Main()
        {
            Adb.ResetFile();
            var offset = 0;
            var count = 0;

            while (true)
            {
                Thread.Sleep(1000);
                var content = Adb.FileContent(offset);

                content = content.Length == 0 ? content : content.Substring(1);

                var split = content.Split(" ");

                if (split.Length <= 0 || split[0] == "") continue;
                
                var byteArray = split.Select(hex => Convert.ToByte(hex, 16)).ToArray();
                offset += byteArray.Length;
                ParseBytesToHci(byteArray, ++count);
            }
        }

        private static void ParseBytesToHci(IEnumerable<byte> byteArray, int offset)
        {
            var buffer = new Buffer {Bytes = byteArray.ToList()};

            var packetRecords = new List<PacketRecord>();

            while (!buffer.Finished)
            {
                var originalLength = buffer.ReadUInt32();
                var includedLength = buffer.ReadUInt32();
                packetRecords.Add(new PacketRecord
                {
                    OriginalLength = originalLength,
                    IncludedLength = includedLength,
                    PacketFlags = buffer.ReadUInt32(),
                    CumulativeDrops = buffer.ReadUInt32(),
                    TimeStamp = buffer.ReadUInt64(),
                    PacketData = buffer.ReadVariableLength((int) includedLength)
                });
            }

            File.WriteAllText($"output-{offset}.log", string.Join(
                Environment.NewLine + Environment.NewLine,
                packetRecords.Select(record =>
                    string.Join(Environment.NewLine,
                        "----PackedRecord----",
                        $"Original Length: {record.OriginalLength}",
                        $"Included Length: {record.IncludedLength}",
                        $"Packet Flags: {record.PacketFlags}",
                        $"Cumulative Drops: {record.CumulativeDrops}",
                        $"TimeStamp: {record.TimeStamp}",
                        $"Packet Data: {string.Join(" ", record.PacketData)}"))));
        }
    }
}