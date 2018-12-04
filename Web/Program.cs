using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using BtSnoop.Interface;
using BtSnoop.Parser;
using HCIParser.Parser;

namespace Web
{
    internal static class Program
    {
        public static void Main()
        {
            var btSnoopInterface = new BtSnoopInterface();

            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();
            while (true)
            {
                var context = listener.GetContext();
                var outputStream = context.Response.OutputStream;

                byte[] byteArray;

                var url = context.Request.RawUrl;
                
                switch (url)
                {
                    case "/":
                    {
                        var fileStream = File.Open("./static/index.html", FileMode.Open, FileAccess.Read,
                            FileShare.Read);
                        var memoryStream = new MemoryStream();

                        fileStream.CopyTo(memoryStream);

                        byteArray = memoryStream.ToArray();
                        break;
                    }
                    case "/delta":
                    {
                        var btSnoopDeltaBytes = btSnoopInterface.RetrieveDelta();
                        var btSnoopPacketRecords = new BtSnoopParser(btSnoopDeltaBytes).Parse();
                        var hciRecords = new HciParser(btSnoopPacketRecords).Parse();

                        /*var zippedRecords = btSnoopPacketRecords.Zip(hciRecords,
                            (packetRecord, hciRecord) =>
                                packetRecord.ToString() + "," + hciRecord.ToString());*/

                        var output = "[" + string.Join(",", hciRecords) + "]";

                        byteArray = Encoding.ASCII.GetBytes(output);
                        break;
                    }
                    default:
                        byteArray = Encoding.ASCII.GetBytes("404: " + url);
                        break;
                }

                outputStream.Write(byteArray, 0, byteArray.Length);
                outputStream.Close();
            }
        }
    }
}