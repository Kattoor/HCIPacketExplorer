using System;

namespace PacketExplorer.Model.Hci
{
    public class HciCommandRecord : HciRecord
    {
        public string OpCode { private get; set; }
        public int ParameterTotalLength { private get; set; }
        public byte[] Parameters { private get; set; }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                "\t----HciCommandRecord-----",
                $"\tPacket Type: {HciPacketType}",
                $"\tOpCode: {OpCode}",
                $"\tParameter Total Length: {ParameterTotalLength}",
                $"\tParameters: {BitConverter.ToString(Parameters).Replace("-", ":")}");
        }
    }
}