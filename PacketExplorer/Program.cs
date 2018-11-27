using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PacketExplorer
{
    public class Buffer
    {
        private int _offset;

        public List<byte> Bytes { private get; set; }

        private byte[] Read(int amount) =>
            PostIncrement(
                Bytes
                    .Skip(_offset)
                    .Take(amount)
                    .Reverse()
                    .ToArray());

        private byte[] PostIncrement(byte[] bytes)
        {
            _offset += bytes.Length;
            return bytes;
        }

        public uint ReadUInt32() =>
            BitConverter.ToUInt32(Read(4));

        public ulong ReadUInt64() =>
            BitConverter.ToUInt64(Read(8));

        public byte[] ReadVariableLength(int size) =>
            Read(size);

        public bool Finished =>
            _offset == Bytes.Count;
    }

    internal static class Program
    {
        private static void Main()
        {
            var buffer = new Buffer
            {
                Bytes = File.ReadAllBytes("btsnoop_hci.log").ToList().Skip(16).ToList()
            };

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

            File.WriteAllText("output.log", string.Join(
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

    internal class PacketRecord
    {
        public uint OriginalLength { get; set; }
        public uint IncludedLength { get; set; }
        public uint PacketFlags { get; set; }
        public uint CumulativeDrops { get; set; }
        public ulong TimeStamp { get; set; }
        public byte[] PacketData { get; set; }
    }
}