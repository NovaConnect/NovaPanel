using Microsoft.Management.Infrastructure;

namespace NovaPanel.Model.ServerMonitorM
{
    public class WMIModels
    {
        public static string GetCpuInfo()
            => GetHardwareInfo("Win32_Processor", "Name");

        public static string GetGpuInfo()
            => GetHardwareInfo("Win32_VideoController", "Name");

        public static string GetHardwareInfo(string className, string propertyName)
        {
            using (CimSession session = CimSession.Create(null))
            {
                var instances = session.QueryInstances(@"root\cimv2", "WQL", $"SELECT * FROM {className}");

                foreach (var instance in instances)
                {
                    if (instance.CimInstanceProperties[propertyName] != null)
                    {
                        var obj = instance.CimInstanceProperties[propertyName].Value;
                        if(obj.ToString() != null)
                        {
                            return obj.ToString();
                        }
                    }
                }
            }
            return "null";
        }
    }
}
