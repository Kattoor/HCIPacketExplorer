using System;

namespace PacketExplorer.Model.Hci.AsynchronousData
{
    public class L2CapRecord
    {
        public int Length { private get; set; }
        public int Cid { get; set; }
        public byte[] Payload { get; set; }
        public AttributeProtocolRecord AttributeProtocolRecord { private get; set; }
        
        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                "\t\t----L2CapRecord----",
                $"\t\tLength: {Length}",
                $"\t\tCid: {Cid}",
                $"\t\tPayload: {BitConverter.ToString(Payload).Replace("-", ":")}",
                AttributeProtocolRecord?.ToString());
        }
    }
}