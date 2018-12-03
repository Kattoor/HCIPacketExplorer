using System.Linq;
using PacketExplorer.Model.BtSnoop;
using PacketExplorer.Model.Hci;
using PacketExplorer.Model.Hci.Ble;

namespace PacketExplorer.Parsers.Hci
{
    public class HciEventParser : IHciRecordParser
    {
        public HciRecord Parse(PacketRecord packetRecord)
        {
            var eventCode = MetadataReader.Hci.Event.getEventType(packetRecord.PacketData[1]);
            var parameterTotalLength = packetRecord.PacketData[2];
            var parameters = packetRecord.PacketData.Skip(3).Take(parameterTotalLength).ToArray();

            var hciEventRecord = new HciEventRecord
            {
                HciPacketType = "HCI Event Packet",
                EventCode = eventCode,
                ParameterTotalLength = parameterTotalLength,
                Parameters = parameters
            };

            if (eventCode == "LE Meta")
            {
                var subEvent = MetadataReader.Hci.Event.getLowEnergySubEventType(parameters[0]);
                if (subEvent == "LE Connection Complete")
                {
                    hciEventRecord.BleConnectionCompleteMetadata =
                        new BleConnectionCompleteMetadata
                        {
                            SubEvent = subEvent,
                            Status = parameters[1],
                            ConnectionHandle = parameters.Skip(2).Take(2).ToArray(),
                            Role = parameters[4],
                            PeerAddressType = parameters[5],
                            BdAddress = parameters.Skip(6).Take(6).ToArray()
                        };
                }
            }

            return hciEventRecord;
        }
    }
}