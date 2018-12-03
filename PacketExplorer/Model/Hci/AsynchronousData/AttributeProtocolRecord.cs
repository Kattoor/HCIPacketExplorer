using System;

namespace PacketExplorer.Model.Hci.AsynchronousData
{
    public class AttributeProtocolRecord
    {
        public byte OpCode { private get; set; }
        public byte[] Handle { private get; set; }
        public byte[] Value { private get; set; }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                "\t\t\t----AttributeProtocolRecord----",
                $"\t\t\tOpCode: {OpCode}",
                $"\t\t\tHandle: {BitConverter.ToString(Handle).Replace("-", ":")}",
                $"\t\t\tValue: {BitConverter.ToString(Value).Replace("-", ":")}");
        }
    }
}