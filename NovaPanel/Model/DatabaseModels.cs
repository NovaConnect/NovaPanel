using Newtonsoft.Json;
using NovaPanel.Shared;
using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace NovaPanel.Model
{
    #region 声明
    //通用返回内容
    public class ReturnT
    {
        public int code { get; set; }
        public object? data { get; set; }
       

    }

    // 用户表
    public class User
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Tag { get; set; }
        public DateTime RegDate { get; set; }
        public int Permissions { get; set; } = 3;
    }

    // 插件表
    public class Plugin
    {
        public string Name { get; set; }
        public DateTime AddDate { get; set; }
        public string Introduce { get; set; }
        public string Version { get; set; }
    }

    // 定时任务表
    public class ScheduleTask
    {
        public string Name { get; set; }
        public bool Enable { get; set; } = false;
        public string Type { get; set; } = "CS";// PS, CMD, CS, PY, JS
        public string Code { get; set; }
        public double Minute { get; set; }
        public string Tag { get; set; }
    }

    // 白名单表
    public class WhiteList
    {
        public string IP { get; set; }
        public DateTime AddDate { get; set; }
    }

    // 黑名单表
    public class BlackList
    {
        public string IP { get; set; }
        public DateTime AddDate { get; set; }
    }
    #endregion

    public class DatabaseModels
    {
        private static readonly string _connectionString = "Data Source=Config.npc;Version=3;";

        public static bool _disposed = true;
        public static void CheckAndCreateTables()
        {
            if (!System.IO.File.Exists("Config.npc"))
            {
                _disposed = false;
                Console.WriteLine("Initialize the database.....");
                SQLiteConnection.CreateFile("Config.npc");

                File.WriteAllText("config.json", Program.ReadFile("config.json"));
                Console.WriteLine("Import the configuration config.json..");
            }

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                #region 创建表
                // 用户
                string createUsersTable = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        UserName TEXT,
                        PassWord TEXT,
                        Tag TEXT,
                        RegDate TEXT,
                        Permissions INTEGER
                    );
                ";

                // 插件
                string createPluginsTable = @"
                    CREATE TABLE IF NOT EXISTS Plugins (
                        Name TEXT,
                        AddDate TEXT,
                        Introduce TEXT,
                        Version TEXT
                    );
                ";

                // 定时任务
                string createScheduleTasksTable = @"
                    CREATE TABLE IF NOT EXISTS ScheduleTasks (
                        Name TEXT,
                        Enable BOOLEAN,
                        Type TEXT,
                        Code TEXT,
                        Minute INTEGER,
                        Tag TEXT
                    );
                ";

                // 白名单
                string createWhiteListTable = @"
                    CREATE TABLE IF NOT EXISTS WhiteList (
                        IP TEXT,
                        AddDate TEXT
                    );
                ";

                // 黑名单
                string createBlackListTable = @"
                    CREATE TABLE IF NOT EXISTS BlackList (
                        IP TEXT,
                        AddDate TEXT
                    );
                ";

                using (var cmd = new SQLiteCommand(createUsersTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SQLiteCommand(createPluginsTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SQLiteCommand(createScheduleTasksTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SQLiteCommand(createWhiteListTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SQLiteCommand(createBlackListTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }
                #endregion

                if (!_disposed)
                {
                    var usw = Guid.NewGuid().ToString("N").Substring(0, new Random().Next(6, 15));
                    var psw = Guid.NewGuid().ToString("N").Substring(0, new Random().Next(15, 20));
                    var res = AddUser(usw,
                        psw,
                        "初始用户，可更改，不可删除",
                        DateTime.Now,
                        0);
                    if (res)
                    {
                        Console.WriteLine($"默认账户: \n用户名：{usw}\n密码：{psw}");
                    }
                }
            }

        }

        public static bool AddUser(string userName, string passWord, string tag, DateTime regDate, int permissions)
        {
            passWord = SHA1Hash(passWord);
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string checkUserExistsQuery = "SELECT COUNT(*) FROM Users WHERE UserName = @UserName";
                using (var cmd = new SQLiteCommand(checkUserExistsQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                    if (userCount > 0)
                    {
                        return false;
                    }
                }
                string insertUserQuery = @"
            INSERT INTO Users (UserName, PassWord, Tag, RegDate, Permissions)
            VALUES (@UserName, @PassWord, @Tag, @RegDate, @Permissions)";

                using (var cmd = new SQLiteCommand(insertUserQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@PassWord", passWord);
                    cmd.Parameters.AddWithValue("@Tag", tag);
                    cmd.Parameters.AddWithValue("@RegDate", regDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Permissions", permissions);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
        }

        public static ReturnT GetUserInfo(string username)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    // 查询用户信息
                    string query = "SELECT * FROM Users WHERE UserName = @UserName";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", username);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ReturnT returnT = new ReturnT
                                {
                                    code = 200,
                                    data = new User
                                    {
                                        UserName = reader["UserName"].ToString(),
                                        PassWord = reader["PassWord"].ToString(),
                                        Tag = reader["Tag"].ToString(),
                                        RegDate = DateTime.Parse(reader["RegDate"].ToString()),
                                        Permissions = Convert.ToInt32(reader["Permissions"])
                                    }
                                };

                                return returnT;
                            }
                            else
                            {
                                return new ReturnT()
                                {
                                    code = 404,
                                    data = "not found"
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ReturnT()
                {
                    code = 501,
                    data = ex.Message
                };
            }
        }

        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users";
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                UserName = reader["UserName"].ToString(),
                                PassWord = reader["PassWord"].ToString(),
                                Tag = reader["Tag"].ToString(),
                                RegDate = DateTime.Parse(reader["RegDate"].ToString()),
                                Permissions = int.Parse(reader["Permissions"].ToString()),
                            });
                        }
                    }
                }
            }
            return users;
        }

        public static bool DeleteUser(string name)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string deleteTaskQuery = @"
                    DELETE FROM Users
                    WHERE UserName = @UserName";

                using (var cmd = new SQLiteCommand(deleteTaskQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@UserName", name);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
        }

        public static List<ScheduleTask> GetAllScheduleTasks()
        {
            List<ScheduleTask> tasks = new List<ScheduleTask>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ScheduleTasks";
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new ScheduleTask
                            {
                                Name = reader["Name"].ToString(),
                                Enable = bool.Parse(reader["Enable"].ToString()),
                                Type = reader["Type"].ToString(),
                                Code = reader["Code"].ToString(),
                                Minute = Convert.ToInt32(reader["Minute"]),
                                Tag = reader["Tag"].ToString(),
                            });
                        }
                    }
                }
            }
            return tasks;
        }

        public static bool AddScheduleTask(string name, string type, string code,double minute,string tag)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string insertTaskQuery = @"
                    INSERT INTO ScheduleTasks (Name, Enable, Type, Code, Minute, Tag)
                    VALUES (@Name, @Enable, @Type, @Code, @Minute, @Tag)";

                using (var cmd = new SQLiteCommand(insertTaskQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Enable", true);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@Minute", minute);
                    cmd.Parameters.AddWithValue("@Tag", tag);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
        }

        public static bool UpdateScheduleTask(string name, string type, string code, int minute, string tag)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string updateTaskQuery = @"
                    UPDATE ScheduleTasks
                    SET Type = @Type, Code = @Code, Minute = @Minute, Tag = @Tag
                    WHERE Name = @Name";

                using (var cmd = new SQLiteCommand(updateTaskQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@Minute", minute);
                    cmd.Parameters.AddWithValue("@Tag", tag);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
        }
        
        public static bool DeleteScheduleTask(string name)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string deleteTaskQuery = @"
                    DELETE FROM ScheduleTasks
                    WHERE Name = @Name";

                using (var cmd = new SQLiteCommand(deleteTaskQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
        }

        public static string SHA1Hash(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // 转换为16进制字符串
                }
                return sb.ToString();
            }
        }
    }
}
