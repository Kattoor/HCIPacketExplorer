using System.Collections.Generic;
using System.Linq;
using PacketExplorer.Model.BtSnoop;
using PacketExplorer.Model.Hci;

namespace PacketExplorer.Parsers.Hci
{
    public class HciParser
    {
        private readonly Dictionary<string, IHciRecordParser> _parsers = new Dictionary<string, IHciRecordParser>
        {
            {"HCI Command Packet", new HciCommandParser()},
            {"HCI Event Packet", new HciEventParser()},
            {"HCI Asynchronous Data Packet", new HciAsynchronousDataParser()}
        };

        private List<PacketRecord> PacketRecords { get; }

        public HciParser(List<PacketRecord> packetRecords)
        {
            PacketRecords = packetRecords;
        }

        public IEnumerable<HciRecord> Parse()
        {
            return PacketRecords.Select(GetHciRecord).ToList();
        }

        private HciRecord GetHciRecord(PacketRecord packetRecord)
        {
            var hciPacketType = MetadataReader.Hci.getPacketType(packetRecord.PacketData[0]);

            var hciRecord = _parsers[hciPacketType].Parse(packetRecord);

            return hciRecord;
        }
    }
}