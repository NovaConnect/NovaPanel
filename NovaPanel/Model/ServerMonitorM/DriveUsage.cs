using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace NovaPanel.Model.ServerMonitorM
{
    public class DiskInfo
    {
        public string DriveLetter { get; set; }
        public string Name { get; set; }
        public ulong TotalSize { get; set; }
        public ulong AvailableSize { get; set; }
        public ulong UsedSize { get; set; }
        public double UsagePercentage { get; set; }
    }

    public class DriveUsage
    {
        public static List<DiskInfo> GetAllDiskInfo()
        {
            List<DiskInfo> diskInfos = new List<DiskInfo>();

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    DiskInfo diskInfo = new DiskInfo
                    {
                        DriveLetter = drive.Name,
                        Name = drive.VolumeLabel,
                        TotalSize = (ulong)drive.TotalSize,
                        AvailableSize = (ulong)drive.AvailableFreeSpace,
                        UsedSize = (ulong)(drive.TotalSize - drive.AvailableFreeSpace),
                        UsagePercentage = (double)(drive.TotalSize - drive.AvailableFreeSpace) / drive.TotalSize * 100
                    };

                    diskInfos.Add(diskInfo);
                }
            }

            return diskInfos;
        }
    }
}
