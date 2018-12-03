using System;
using PacketExplorer.Model.Hci.AsynchronousData;

namespace PacketExplorer.Model.Hci
{
    public class HciAsynchronousDataRecord : HciRecord
    {
        public byte[] ConnectionHandle { private get; set; }
        public int PbFlag { private get; set; }
        public int BcFlag { private get; set; }
        public int DataTotalLength { private get; set; }
        public byte[] Data { private get; set; }
        public L2CapRecord L2CapRecord { private get; set; }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                "\t----HciAsynchronousDataRecord----",
                $"\tPacket Type: {HciPacketType}",
                $"\tConnection Handle: {BitConverter.ToString(ConnectionHandle).Replace("-", ":")}",
                $"\tPbFlag: {PbFlag}",
                $"\tBcFlag: {BcFlag}",
                $"\tDataTotalLength: {DataTotalLength}",
                $"\tData: {BitConverter.ToString(Data).Replace("-", ":")}",
                L2CapRecord.ToString());
        }
    }
}