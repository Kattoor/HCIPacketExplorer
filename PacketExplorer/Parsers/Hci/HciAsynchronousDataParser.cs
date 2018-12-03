using System.Linq;
using PacketExplorer.Model.BtSnoop;
using PacketExplorer.Model.Hci;

namespace PacketExplorer.Parsers.Hci
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

            return new HciAsynchronousDataRecord
            {
                HciPacketType = "HCI Asynchronous Data Packet",
                ConnectionHandle = connectionHandle,
                PbFlag = pbFlag,
                BcFlag = bcFlag,
                DataTotalLength = dataTotalLength,
                Data = data
            };
        }
    }
}