using BtSnoop.Parser;
using HCIParser.Model;

namespace HCIParser.Parser
{
    public interface IHciRecordParser
    {
        HciRecord Parse(PacketRecord packetRecord);
    }
}