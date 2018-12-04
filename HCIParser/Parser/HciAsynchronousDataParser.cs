using System.Linq;
using BtSnoop.Parser;
using HCIParser.Model;
using HCIParser.Model.AsynchronousData;

namespace HCIParser.Parser
{
    public class HciAsynchronousDataParser : IHciRecordParser
    {
        public HciRecord Parse(PacketRecord packetRecord)
        {
            var connectionHandle = 
                MetadataReader.Hci.AsynchronousData.getHandle(new[]
                    {packetRecord.PacketData[1], packetRecord.PacketData[2]});
            var pbFlag = MetadataReader.Hci.AsynchronousData.getPbFlag(packetRecord.PacketData[2]);
            var bcFlag = MetadataReader.Hci.AsynchronousData.getBcFlag(packetRecord.PacketData[2]);
            var dataTotalLength =
                MetadataReader.Hci.AsynchronousData.getDataTotalLength(new[]
                    {packetRecord.PacketData[3], packetRecord.PacketData[4]});
            var data = packetRecord.PacketData.Skip(5).Take(dataTotalLength).ToArray();

            var l2Cap = new L2CapRecord
            {
                Length = (data[1] << 8) | data[0],
                Cid = (data[3] << 8) | data[2],
                Payload = data.Skip(4).ToArray()
            };

            if (l2Cap.Cid == 0x0004)
            {
                l2Cap.AttributeProtocolRecord = new AttributeProtocolRecord
                {
                    OpCode = l2Cap.Payload[0],
                    Handle = l2Cap.Payload.Skip(1).Take(2).Reverse()
                        .ToArray(), //(l2Cap.Payload[2] << 8) | l2Cap.Payload[1],
                    Value = l2Cap.Payload.Skip(3).ToArray()
                };
            }

            return new HciAsynchronousDataRecord
            {
                HciPacketType = "HCI Asynchronous Data Packet",
                ConnectionHandle = connectionHandle,
                PbFlag = pbFlag,
                BcFlag = bcFlag,
                DataTotalLength = dataTotalLength,
                Data = data,
                L2CapRecord = l2Cap
            };
        }
    }
}