using System.Collections.Generic;
using System.Linq;
using Buffer = BtSnoop.Parser.Buffer;

namespace BtSnoop.Parser
{
    public class BtSnoopParser
    {
        private List<PacketRecord> PacketRecords { get; set; }

        private Buffer Buffer { get; }

        public BtSnoopParser(IEnumerable<byte> headlessFile)
        {
            Buffer = new Buffer {Bytes = headlessFile.ToList()};
        }

        public List<PacketRecord> Parse()
        {
            PacketRecords = new List<PacketRecord>();

            while (!Buffer.Finished)
            {
                var originalLength = Buffer.ReadUInt32();
                var includedLength = Buffer.ReadUInt32();

                PacketRecords.Add(new PacketRecord
                {
                    OriginalLength = originalLength,
                    IncludedLength = includedLength,
                    PacketFlags = Buffer.ReadUInt32(),
                    CumulativeDrops = Buffer.ReadUInt32(),
                    Timestamp = Buffer.ReadUInt64(),
                    PacketData = Buffer.ReadVariableLength((int) includedLength)
                });
            }

            return PacketRecords;
        }
    }
}