using System.Linq;
using BtSnoop.Parser;
using HCIParser.Model;

namespace HCIParser.Parser
{
    public class HciCommandParser : IHciRecordParser
    {
        public HciRecord Parse(PacketRecord packetRecord)
        {
            var ocf = MetadataReader.Hci.Command.getOpcodeCommandFieldKey(new[]
                {packetRecord.PacketData[1], packetRecord.PacketData[2]});
            var ogf = MetadataReader.Hci.Command.getOpcodeGroupFieldKey(packetRecord.PacketData[2]);
            var parameterTotalLength = packetRecord.PacketData[3];
            var parameters = packetRecord.PacketData.Skip(4).Take(parameterTotalLength).ToArray();

            return new HciCommandRecord
            {
                HciPacketType = "HCI Command Packet",
                OpCode = MetadataReader.Hci.Command.getOpcodeCommandField(ogf, ocf),
                ParameterTotalLength = parameterTotalLength,
                Parameters = parameters
            };
        }
    }
}