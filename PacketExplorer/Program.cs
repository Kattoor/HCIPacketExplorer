using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using MetadataReader;

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

                ParseBytesToHci(byteArray, ++count);
                Console.WriteLine("Done");
            }
        }

        private static void ParseBytesToHci(IEnumerable<byte> byteArray, int offset)
        {
            var buffer = new Buffer {Bytes = byteArray.ToList()};

            var packetRecords = new List<PacketRecord>();

            while (!buffer.Finished)
            {
                var originalLength = buffer.ReadUInt32();
                var includedLength = buffer.ReadUInt32();
                var record = new PacketRecord
                {
                    OriginalLength = originalLength,
                    IncludedLength = includedLength,
                    PacketFlags = buffer.ReadUInt32(),
                    CumulativeDrops = buffer.ReadUInt32(),
                    Timestamp = buffer.ReadUInt64(),
                    PacketData = buffer.ReadVariableLength((int) includedLength)
                };

                var hciPacketType = Hci.getPacketType(record.PacketData[0]);
                PacketMetadata metadata = null;
                if (hciPacketType == "HCI Command Packet")
                {
                    var ocf = Hci.Command.getOpcodeCommandFieldKey(new[] {record.PacketData[1], record.PacketData[2]});
                    var ogf = Hci.Command.getOpcodeGroupFieldKey(record.PacketData[2]);
                    var parameterTotalLength = record.PacketData[3];
                    var parameters = record.PacketData.Skip(4).Take(parameterTotalLength).ToArray();

                    metadata = new PacketMetadataCommand
                    {
                        HciPacketType = hciPacketType,
                        OpCode = Hci.Command.getOpcodeCommandField(ogf, ocf),
                        ParameterTotalLength = parameterTotalLength,
                        Parameters = parameters
                    };
                }
                else if (hciPacketType == "HCI Event Packet")
                {
                    var eventCode = Hci.Event.getEventType(record.PacketData[1]);
                    var parameterTotalLength = record.PacketData[2];
                    var parameters = record.PacketData.Skip(3).Take(parameterTotalLength).ToArray();

                    metadata = new PacketMetadataEvent
                    {
                        HciPacketType = hciPacketType,
                        EventCode = eventCode,
                        ParameterTotalLength = parameterTotalLength,
                        Parameters = parameters
                    };

                    if (eventCode == "LE Meta")
                    {
                        var subEvent = Hci.Event.getLowEnergySubEventType(parameters[0]);
                        if (subEvent == "LE Connection Complete")
                        {
                            ((PacketMetadataEvent) metadata).LowEnergyConnectionCompleteEventMetadata =
                                new LowEnergyConnectionCompleteEventMetadata
                                {
                                    SubEvent = subEvent,
                                    Status = parameters[1],
                                    ConnectionHandle = parameters.Skip(2).Take(2).ToArray(),
                                    Role = parameters[4],
                                    PeerAddressType = parameters[5],
                                    BdAddress = parameters.Skip(6).Take(6).ToArray()
                                };
                        }
                    }
                }
                else if (hciPacketType == "HCI Asynchronous Data Packet")
                {
                    var connectionHandle =
                        Hci.AsynchronousData.getHandle(new[] {record.PacketData[1], record.PacketData[2]});
                    var pbFlag = Hci.AsynchronousData.getPbFlag(record.PacketData[2]);
                    var bcFlag = Hci.AsynchronousData.getBcFlag(record.PacketData[2]);
                    var dataTotalLength =
                        Hci.AsynchronousData.getDataTotalLength(new[] {record.PacketData[3], record.PacketData[4]});
                    var data = record.PacketData.Skip(5).Take(dataTotalLength).ToArray();

                    metadata = new PacketMetadataAsynchronousData
                    {
                        HciPacketType = hciPacketType,
                        ConnectionHandle = connectionHandle,
                        PbFlag = pbFlag,
                        BcFlag = bcFlag,
                        DataTotalLength = dataTotalLength,
                        Data = data
                    };
                }

                record.Metadata = metadata;

                packetRecords.Add(record);
            }

            File.WriteAllText($"output-{offset}.log", string.Join(
                Environment.NewLine + Environment.NewLine,
                packetRecords.Select(record => string.Join(Environment.NewLine, record.ToString()))));
        }
    }
}