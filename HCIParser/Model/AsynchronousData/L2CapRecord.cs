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
            return "{" +
                   $"\"length\": \"{Length}\"," +
                   $"\"cid\": \"{Cid}\"," +
                   $"\"payload\": \"{Payload}\"," +
                   $"\"attributeProtocolRecord\": {AttributeProtocolRecord?.ToString() ?? "{}"}" +
                   "}";
        }
    }
}