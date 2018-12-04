using System;
using HCIParser.Model.Ble;

namespace HCIParser.Model
{
    public class HciEventRecord : HciRecord
    {
        public string EventCode { private get; set; }
        public int ParameterTotalLength { private get; set; }
        public byte[] Parameters { private get; set; }

        public BleConnectionCompleteMetadata BleConnectionCompleteMetadata { private get; set; }
        public BleAdvertisingReportMetadata BleAdvertisingReportMetadata { get; set; }

        public override string ToString()
        {
            var parameters = BitConverter.ToString(Parameters).Split("-");
            
            return "{" +
                   $"\"packetType\": \"{HciPacketType}\"," +
                   $"\"eventCode\": \"{EventCode}\"," +
                   $"\"parameterTotalLength\": \"{ParameterTotalLength}\"," +
                   $"\"parameters\": [{(parameters.Length > 0 && parameters[0] != "" ? "\"" + string.Join("\",\"", parameters) + "\"" : "")}]," +
                   $"\"bleConnectionCompleteMetadata\": {BleConnectionCompleteMetadata?.ToString() ?? "{}"}" +
                   "}";
        }
    }
}