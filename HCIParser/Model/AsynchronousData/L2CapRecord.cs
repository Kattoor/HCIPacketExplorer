using System;

namespace HCIParser.Model.AsynchronousData
{
    public class L2CapRecord
    {
        public int Length { private get; set; }
        public int Cid { get; set; }
        public byte[] Payload { get; set; }
        public AttributeProtocolRecord AttributeProtocolRecord { private get; set; }
        
        public override string ToString()
        {
            var payload = BitConverter.ToString(Payload).Split("-");
            
            return "{" +
                   $"\"length\": \"{Length}\"," +
                   $"\"cid\": \"{Cid}\"," +
                   $"\"payload\": [{(payload.Length > 0 && payload[0] != "" ? "\"" + string.Join("\",\"", payload) + "\"" : "")}]," +
                   $"\"attributeProtocolRecord\": {AttributeProtocolRecord?.ToString() ?? "{}"}" +
                   "}";
        }
    }
}