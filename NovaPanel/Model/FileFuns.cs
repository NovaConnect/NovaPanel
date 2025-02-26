using System.Text;

namespace NovaPanel.Model
{
    public enum FileItemType
    {
        File,
        Directory
    }

    class FileItem
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public FileItemType FileType { get; set; }
        public ulong Size { get; set; }
        public string CreateTime { get; set; }
        public string LastWriteTime { get; set; }
        public string Permissions { get; set; }
    }
    public class FileFuns
    {    // 获取文件权限
        public static string GetFilePermissions(string file)
        {
            FileSystemInfo item = new FileInfo(file);
            var attributes = item.Attributes;
            return attributes.HasFlag(FileAttributes.ReadOnly) ? "只读" : "可写";
        }

        // 格式化文件大小
        public static string FormatSize(ulong size)
        {
            if (size == 2077576874243179872)
                return "NULL";
            if (size < 1024)
                return $"{size} B";
            else if (size < 1024 * 1024)
                return $"{(size / 1024.0):0.00} KB";
            else if (size < 1024 * 1024 * 1024)
                return $"{(size / (1024.0 * 1024)):0.00} MB";
            else
                return $"{(size / (1024.0 * 1024 * 1024)):0.00} GB";
        }

        public static long GetFileLength(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                return fileInfo.Length;
            }
            else
            {
                return -1;
            }
        }

        public static long GetDirectoryLength(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                return 0;
            long len = 0;
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                len += fi.Length;
            }
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                for (int i = 0; i < dis.Length; i++)
                {
                    len += GetDirectoryLength(dis[i].FullName);
                }
            }
            return len;
        }
        /// <summary>
        /// 自动识别文件编码
        /// </summary>
        public static Encoding DetectFileEncoding(string filePath)
        {
            // 读取文件的前 1024 字节
            byte[] buffer = new byte[1024];
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int bytesRead = fs.Read(buffer, 0, buffer.Length);
                Array.Resize(ref buffer, bytesRead);
            }

            // 使用 Encoding 类检测编码
            Encoding encoding = Encoding.Default;
            if (buffer.Length >= 2)
            {
                if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                {
                    encoding = Encoding.Unicode; // UTF-16 LE
                }
                else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                {
                    encoding = Encoding.BigEndianUnicode; // UTF-16 BE
                }
                else if (buffer.Length >= 3 && buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
                {
                    encoding = Encoding.UTF8; // UTF-8 with BOM
                }
                else
                {
                    // 尝试使用 UTF-8 无 BOM 编码
                    try
                    {
                        string testString = Encoding.UTF8.GetString(buffer);
                        if (IsValidUtf8(buffer))
                        {
                            encoding = Encoding.UTF8;
                        }
                    }
                    catch
                    {
                        // 如果无法解码，则使用默认编码
                    }
                }
            }

            return encoding;
        }

        /// <summary>
        /// 检查字节数组是否为有效的 UTF-8 编码
        /// </summary>
        public static bool IsValidUtf8(byte[] buffer)
        {
            try
            {
                Encoding.UTF8.GetString(buffer);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsTextFile(string filePath)
        {
            try
            {
                byte[] buffer = new byte[1024];
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    int bytesRead = fs.Read(buffer, 0, buffer.Length);
                    Array.Resize(ref buffer, bytesRead);
                }

                // 检查是否包含非文本字符
                int nonTextCount = 0;
                foreach (byte b in buffer)
                {
                    if (b < 32 && b != 9 && b != 10 && b != 13) // 9=Tab, 10=LF, 13=CR
                    {
                        nonTextCount++;
                    }
                }

                // 如果非文本字符超过 10%，则认为是二进制文件
                return nonTextCount < buffer.Length * 0.1;
            }
            catch
            {
                return false;
            }
        }
    }
}
