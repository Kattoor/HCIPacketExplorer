using PacketExplorer.Model.BtSnoop;
using PacketExplorer.Model.Hci;

namespace PacketExplorer.Parsers.Hci
{
    public interface IHciRecordParser
    {
        HciRecord Parse(PacketRecord packetRecord);
    }
}