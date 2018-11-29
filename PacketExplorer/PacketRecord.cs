namespace PacketExplorer
{
    public class PacketRecord
    {
        public uint OriginalLength { get; set; }
        public uint IncludedLength { get; set; }
        public uint PacketFlags { get; set; }
        public uint CumulativeDrops { get; set; }
        public ulong TimeStamp { get; set; }
        public byte[] PacketData { get; set; }
    }
}