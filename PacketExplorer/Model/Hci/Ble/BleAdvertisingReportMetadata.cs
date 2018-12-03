using System;

namespace PacketExplorer.Model.Hci.Ble
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
            return string.Join(Environment.NewLine,
                "\t\t----BleAdvertisingReportMetadata----",
                $"\t\tBleEventCode: {BleEventCode}",
                $"\t\tNumberOfDevices: {NumberOfDevices}",
                $"\t\tDeviceInfoEventType: {DeviceInfoEventType}",
                $"\t\tDeviceInfoAddressType: {DeviceInfoAddressType}",
                $"\t\tDeviceInfoAddress: {BitConverter.ToString(DeviceInfoAddress).Replace("-", ":")}",
                $"\t\tDeviceInfoDataLength: {DeviceInfoDataLength}",
                $"\t\tDeviceInfoAdvertisingData: {BitConverter.ToString(DeviceInfoAdvertisingData).Replace("-", ":")}",
                $"\t\tDeviceInfoRssi: {DeviceInfoRssi}");
        }
    }
}