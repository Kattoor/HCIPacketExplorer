using System;

namespace HCIParser.Model
{
    public class HciCommandRecord : HciRecord
    {
        public string OpCode { private get; set; }
        public int ParameterTotalLength { private get; set; }
        public byte[] Parameters { private get; set; }

        public override string ToString()
        {
            var parameters = BitConverter.ToString(Parameters).Split("-");
            
            return "{" +
                   $"\"packetType\": \"{HciPacketType}\"," +
                   $"\"opCode\": \"{OpCode}\"," +
                   $"\"parameterTotalLength\": \"{ParameterTotalLength}\"," +
                   $"\"parameters\": [{(parameters.Length > 0 && parameters[0] != "" ? "\"" + string.Join("\",\"", parameters) + "\"" : "")}]" +
                   "}";
        }
    }
}