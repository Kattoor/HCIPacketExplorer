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
                var bleEventCode = MetadataReader.Hci.Event.getBleEventCode(parameters[0]);

                if (bleEventCode == "LE Connection Complete")
                {
                    hciEventRecord.BleConnectionCompleteMetadata =
                        new BleConnectionCompleteMetadata
                        {
                            BleEventCode = bleEventCode,
                            Status = parameters[1],
                            ConnectionHandle = parameters.Skip(2).Take(2).Reverse().ToArray(),
                            Role = parameters[4],
                            PeerAddressType = parameters[5],
                            PeerAddress = parameters.Skip(6).Take(6).Reverse().ToArray(),
                            ConnectionInterval = parameters.Skip(12).Take(2).Reverse().ToArray(),
                            ConnectionLatency = parameters.Skip(14).Take(2).Reverse().ToArray(),
                            ConnectionTimeout = parameters.Skip(16).Take(2).Reverse().ToArray(),
                            ClockAccuracy = parameters[18]
                        };
                }

                if (bleEventCode == "LE Advertising Report")
                {
                    var deviceInfoDataLength = parameters[10];
                    hciEventRecord.BleAdvertisingReportMetadata =
                        new BleAdvertisingReportMetadata
                        {
                            BleEventCode = bleEventCode,
                            NumberOfDevices = parameters[1],
                            DeviceInfoEventType = parameters[2],
                            DeviceInfoAddressType = parameters[3],
                            DeviceInfoAddress = parameters.Skip(4).Take(6).Reverse().ToArray(),
                            DeviceInfoDataLength = deviceInfoDataLength,
                            DeviceInfoAdvertisingData = parameters.Skip(11).Take(deviceInfoDataLength).Reverse().ToArray(),
                            DeviceInfoRssi = parameters[11 + deviceInfoDataLength]
                        };
                }
            }

            return hciEventRecord;
        }
    }
}