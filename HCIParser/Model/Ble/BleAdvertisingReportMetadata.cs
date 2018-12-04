using System;

namespace HCIParser.Model.Ble
{
    public class BleAdvertisingReportMetadata
    {
        public string BleEventCode { private get; set; }
        public int NumberOfDevices { private get; set; }
        public int DeviceInfoEventType { private get; set; }
        public int DeviceInfoAddressType { private get; set; }
        public byte[] DeviceInfoAddress { private get; set; }
        public int DeviceInfoDataLength { private get; set; }
        public byte[] DeviceInfoAdvertisingData { private get; set; }
        public int DeviceInfoRssi { private get; set; }

        public override string ToString()
        {
            var deviceInfoAddress = BitConverter.ToString(DeviceInfoAddress).Split("-");
            var deviceInfoAdvertisingData = BitConverter.ToString(DeviceInfoAdvertisingData).Split("-");
            
            return "{" +
                   $"\"bleEventCode\": \"{BleEventCode}\"," +
                   $"\"numberOfDevices\": \"{NumberOfDevices}\"," +
                   $"\"deviceInfoEventType\": \"{DeviceInfoEventType}\"," +
                   $"\"deviceInfoAddressType\": \"{DeviceInfoAddressType}\"," +
                   $"\"deviceInfoAddress\": [{(deviceInfoAddress.Length > 0 && deviceInfoAddress[0] != "" ? "\"" + string.Join("\",\"", deviceInfoAddress) + "\"" : "")}]," +
                   $"\"deviceInfoDataLength\": \"{DeviceInfoDataLength}\"," +
                   $"\"deviceInfoAdvertisingData\": [{(deviceInfoAdvertisingData.Length > 0 && deviceInfoAdvertisingData[0] != "" ? "\"" + string.Join("\",\"", deviceInfoAdvertisingData) + "\"" : "")}]," +
                   $"\"deviceInfoRssi\": \"{DeviceInfoRssi}\"" +
                   "}";
        }
    }
}