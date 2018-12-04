using System.Diagnostics;

namespace BtSnoop.Interface
{
    public static class Adb
    {
        private static string CmdCommand(string command)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo("cmd.exe", $"/c {command}")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            p.Start();
            var result = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return result;
        }

        private static bool FileExists => bool.Parse(CmdCommand(
            $"adb shell \"[ -f /sdcard/btsnoop_hci.log ] && echo {bool.TrueString} || echo {bool.FalseString}\""));

        private static void CreateFile() => CmdCommand("adb shell touch /sdcard/btsnoop_hci.log");
        private static void RemoveFile() => CmdCommand("adb shell rm /sdcard/btsnoop_hci.log");

        public static void ResetFile()
        {
            if (FileExists)
            {
                RemoveFile();
            }

            CreateFile();
        }

        public static string FileContent(int offset) =>
            CmdCommand($"adb shell od -An -t x1 -v -j {16 + offset} /sdcard/btsnoop_hci.log").Replace("\r\r", "")
                .Replace("\n", "");
    }
}