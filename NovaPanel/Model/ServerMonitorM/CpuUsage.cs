using System;
using Microsoft.Management.Infrastructure;

namespace NovaPanel.Model.ServerMonitorM
{
    public class CpuUsage
    {
        /// <summary>
        /// 获取CPU当前占用率
        /// </summary>
        public static double GetCpuUsage()
        {
            try
            {
                using (var session = CimSession.Create(null))
                {
                    var instances = session.QueryInstances(@"root\cimv2", "WQL", "SELECT LoadPercentage FROM Win32_Processor WHERE Name LIKE '%Intel%' OR Name LIKE '%AMD%'");
                    foreach (var instance in instances)
                    {
                        return Convert.ToDouble(instance.CimInstanceProperties["LoadPercentage"].Value);
                    }
                }
                return 0.0;
            }
            catch
            {
                return 0.0;
            }
        }

    }
}