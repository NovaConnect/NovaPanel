using System.Collections.Generic;
using System.Linq;

namespace NovaPanel.Model
{
    public enum InfoLevel
    {
        Normal,
        Warning,
        Dangerous
    }

    public class LogEntry
    {
        public string Time { get; set; }
        public InfoLevel Level { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// 获取指定日期的日志
        /// </summary>
        /// <param name="date">日志日期</param>
        /// <returns>日志列表</returns>
    }

    public class LoggerModels
    {
        private static string logDirectory;
        private static string logFilePath;

        public static void Initialize()
        {
            logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            logFilePath = Path.Combine(logDirectory, $"{DateTime.Now:yyyy-MM-dd}.log");
        }

        /// <summary>
        /// 添加日志到今日日志
        /// </summary>
        /// <param name="level">Normal, Warning, Dangerous</param>
        public static void AddToLog(string message, InfoLevel level)
        {
            string logMessage = $"{DateTime.Now:HH:mm:ss}|{level}|{message}";
            string logcMessage = $"[{DateTime.Now:HH:mm:ss}][{level}]: {message}";
            switch (level)
            {
                case InfoLevel.Normal:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case InfoLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case InfoLevel.Dangerous:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.WriteLine(logcMessage);
            Console.ResetColor();

            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }

        /// <summary>
        /// 获取所有日志文件
        /// </summary>
        /// <returns>日志文件列表</returns>
        public static List<string> GetAllLogFiles()
        {
            if (Directory.Exists(logDirectory))
            {
                return Directory.GetFiles(logDirectory, "*.log").ToList();
            }
            return new List<string>();
        }

        /// <summary>
        /// 获取指定文件的日志
        /// </summary>
        /// <param name="date">日志日期</param>
        /// <returns>日志列表</returns>
        public static List<LogEntry> GetLogEntriesByDate(DateTime date)
        {
            string filePath = Path.Combine(logDirectory, $"{date:yyyy-MM-dd}.log");
            return GetLogEntries(filePath);
        }

        /// <summary>
        /// 获取指定文件的日志
        /// </summary>
        /// <param name="filePath">日志文件路径</param>
        /// <returns>日志列表</returns>
        public static List<LogEntry> GetLogEntries(string filePath)
        {
            var logEntries = new List<LogEntry>();

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split("|");
                    if (parts.Length == 3)
                    {
                        logEntries.Add(new LogEntry
                        {
                            Time = parts[0],
                            Level = Enum.Parse<InfoLevel>(parts[1]),
                            Message = parts[2]
                        });
                    }
                }
            }

            return logEntries;
        }
    }
}
