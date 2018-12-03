using System;
using System.Collections.Generic;
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
                    .ToArray());

        private byte[] ReadReverse(int amount) =>
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
            BitConverter.ToUInt32(ReadReverse(4));

        public ulong ReadUInt64() =>
            BitConverter.ToUInt64(ReadReverse(8));

        public byte[] ReadVariableLength(int size) =>
            Read(size);

        public bool Finished =>
            _offset == Bytes.Count;
    }
}