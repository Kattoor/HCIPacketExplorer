using System;

namespace HCIParser.Model.AsynchronousData
{
    public class AttributeProtocolRecord
    {
        public byte OpCode { private get; set; }
        public byte[] Handle { private get; set; }
        public byte[] Value { private get; set; }

        public override string ToString()
        {
            var handle = BitConverter.ToString(Handle).Split("-");
            var value = BitConverter.ToString(Value).Split("-");
            
            return "{" +
                   $"\"opCode\": \"{OpCode}\"," +
                   $"\"handle\": [{(handle.Length > 0 && handle[0] != "" ? "\"" + string.Join("\",\"", handle) + "\"" : "")}]," +
                   $"\"value\": [{(value.Length > 0 && value[0] != "" ? "\"" + string.Join("\",\"", value) + "\"" : "")}]" +
                   "}";
        }
    }
}