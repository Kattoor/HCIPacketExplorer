using System;
using PacketExplorer.Model.Hci.Ble;

namespace PacketExplorer.Model.Hci
{
    public class HciEventRecord : HciRecord
    {
        public string EventCode { private get; set; }
        public int ParameterTotalLength { private get; set; }
        public byte[] Parameters { private get; set; }

        public BleConnectionCompleteMetadata BleConnectionCompleteMetadata { private get; set; }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                "\t----HciEventRecord----",
                $"\tPacket Type: {HciPacketType}",
                $"\tEventCode: {EventCode}",
                $"\tParameter Total Length: {ParameterTotalLength}",
                $"\tParameters: {BitConverter.ToString(Parameters).Replace("-", ":")}",
                BleConnectionCompleteMetadata?.ToString());
        }
    }
}