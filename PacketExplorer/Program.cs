using System;
using System.IO;
using System.Linq;
using System.Threading;
using PacketExplorer.Parsers.BtSnoop;
using PacketExplorer.Parsers.Hci;

namespace PacketExplorer
{
    internal static class Program
    {
        private static void Main()
        {
            Adb.ResetFile();
            var offset = 0;
            var count = 0;

            while (true)
            {
                Thread.Sleep(1000);

                Console.WriteLine("Round");

                var deltaContent = Adb.FileContent(offset);
                deltaContent = deltaContent.Length == 0 ? deltaContent : deltaContent.Substring(1);

                var hexValueArray = deltaContent.Split(" ");
                if (hexValueArray.Length == 0 || hexValueArray[0] == "") continue;

                var byteArray = hexValueArray.Select(hex => Convert.ToByte(hex, 16)).ToArray();
                offset += byteArray.Length;

                var packetRecords = new BtSnoopParser(byteArray).Parse();
                var hciRecords = new HciParser(packetRecords).Parse().ToArray();

                var zippedRecords = packetRecords.Zip(hciRecords,
                    (packetRecord, hciRecord) => packetRecord.ToString() + Environment.NewLine + hciRecord.ToString());

                File.WriteAllText($"output-{++count}.log", string.Join(
                    Environment.NewLine + Environment.NewLine,
                    zippedRecords));

                Console.WriteLine("Done");
            }
        }
    }
}