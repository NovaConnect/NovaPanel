using Newtonsoft.Json;
using NovaPanel.Model.ServerMonitorM;

namespace NovaPanel.Model
{
    public static class ApplicationRuntimes
    {
        public static Themes.Theme Theme { get; set; } = Themes.Theme.Default;
        public static VersionItem ?VersionInfo { get; set; } = new();
        public static ConfigModel configModel { get; set; } = new();
        public static NavItem[]? NavItems { get; set; } = Array.Empty<NavItem>();

        public static class HardInfo
        {
            public static string GPU = WMIModels.GetGpuInfo();
            public static string CPU = WMIModels.GetCpuInfo();
        }

        public static class State
        {
            public static double RamTotalA = RamUsage.GetTotalPhys();
            public static double RamUsageA = 0;
            public static double CpuUsageA = 0;
            private static bool _isRunning = false;

            public static async Task StartAsync()
            {
                _isRunning = true;
                while (_isRunning)
                {
                    RamUsageA = RamUsage.GetUsedPhys();
                    CpuUsageA = CpuUsage.GetCpuUsage();
                    await Task.Delay(1000);
                }
            }

            public static void Stop() => _isRunning = false;

        }

        public static void LoadConfig()
        {
            ApplicationRuntimes.NavItems = JsonConvert.DeserializeObject<NavItem[]>(Program.ReadFile("navs.json"));
            ApplicationRuntimes.VersionInfo = JsonConvert.DeserializeObject<VersionItem>(Program.ReadFile("ver.json"));
            ApplicationRuntimes.configModel = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText("config.json"));
            ApplicationRuntimes.Theme = Themes.Parse(ApplicationRuntimes.configModel.Theme);
        }
    }
}
