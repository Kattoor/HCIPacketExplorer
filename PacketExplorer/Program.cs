using System;
using System.IO;
using System.Linq;
using System.Threading;
using BtSnoop.Interface;
using BtSnoop.Parser;
using HCIParser.Parser;

namespace PacketExplorer
{
    internal static class Program
    {
        private static void Main()
        {
            var count = 0;
            var btSnoopInterface = new BtSnoopInterface();
            while (true)
            {
                Console.WriteLine("Round");

                Thread.Sleep(1000);
                var btSnoopDeltaBytes = btSnoopInterface.RetrieveDelta();
                var btSnoopPacketRecords = new BtSnoopParser(btSnoopDeltaBytes).Parse();
                var hciRecords = new HciParser(btSnoopPacketRecords).Parse();

                var zippedRecords = btSnoopPacketRecords.Zip(hciRecords,
                    (packetRecord, hciRecord) => packetRecord.ToString() + Environment.NewLine + hciRecord.ToString());

                File.WriteAllText($"output-{++count}.log", string.Join(
                    Environment.NewLine + Environment.NewLine,
                    zippedRecords));

                Console.WriteLine("Done");
            }
        }
    }
}