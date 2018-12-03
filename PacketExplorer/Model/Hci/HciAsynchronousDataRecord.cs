using System;

namespace PacketExplorer.Model.Hci
{
    public class HciAsynchronousDataRecord : HciRecord
    {
        public int ConnectionHandle { private get; set; }
        public int PbFlag { private get; set; }
        public int BcFlag { private get; set; }
        public int DataTotalLength { private get; set; }
        public byte[] Data { private get; set; }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                "\t----HciAsynchronousDataRecord----",
                $"\tPacket Type: {HciPacketType}",
                $"\tConnection Handle: {ConnectionHandle}",
                $"\tPbFlag: {PbFlag}",
                $"\tBcFlag: {BcFlag}",
                $"\tDataTotalLength: {DataTotalLength}",
                $"\tData: {BitConverter.ToString(Data).Replace("-", ":")}");
        }
    }
}