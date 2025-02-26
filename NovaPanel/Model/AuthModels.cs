using System;
using System.Security.Cryptography;
using System.Text;

namespace NovaPanel.Model
{
    public class AuthModels
    {
        /// <summary>
        /// 生成为期7天的TOKEN，格式为USERNAME|DATETIME
        /// </summary>
        public static string Generate(string username)
        {
            var userInfo = DatabaseModels.GetUserInfo(username);
            if (userInfo.code == 200 && userInfo.data != null)
            {
                string tokenContent = $"{username}|{DateTime.UtcNow.AddDays(7):yyyy-MM-ddTHH:mm:ssZ}";
                string encryptedToken = Encrypt(tokenContent, ((User)userInfo.data).PassWord);
                string usernameLength = username.Length.ToString("D2");
                string finalToken = $"{usernameLength}{username}{encryptedToken}";
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(finalToken)).Replace("=", "[D]");
            }
            return "null";
        }

        /// <summary>
        /// 解密Token并返回 USERNAME|DATETIME
        /// </summary>
        public static string Decrypt(string cipherText, string password)
        {
            string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(cipherText));
            int usernameLength = int.Parse(decodedToken.Substring(0, 2));
            string username = decodedToken.Substring(2, usernameLength);
            string encryptedToken = decodedToken.Substring(2 + usernameLength);
            return DecryptToken(encryptedToken, password);
        }

        /// <summary>
        /// 使用AES加密字符串
        /// </summary>
        private static string Encrypt(string plainText, string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                byte[] key = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
                aesAlg.Key = key;
                aesAlg.IV = new byte[16];

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        /// <summary>
        /// 使用AES解密字符串
        /// </summary>
        private static string DecryptToken(string cipherText, string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                byte[] key = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
                aesAlg.Key = key;
                aesAlg.IV = new byte[16];

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 验证TOKEN
        /// </summary>
        public static bool Auth(string cipherText)
        {
            try
            {
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(cipherText.Replace("[D]", "=")));
                int usernameLength = int.Parse(decodedToken.Substring(0, 2));
                string tokenUsername = decodedToken.Substring(2, usernameLength);

                var userInfo = DatabaseModels.GetUserInfo(tokenUsername);
                if (userInfo.code == 200 && userInfo.data != null)
                {
                    string encryptedToken = decodedToken.Substring(2 + usernameLength);
                    string decryptedToken = DecryptToken(encryptedToken, ((User)userInfo.data).PassWord);
                    string[] parts = decryptedToken.Split('|');
                    if (parts.Length != 2)
                        return false;
                    if (parts[0] != tokenUsername)
                        return false;

                    DateTime tokenDateTime = DateTime.Parse(parts[1]).ToUniversalTime();
                    DateTime currentDateTime = DateTime.UtcNow;

                    if (currentDateTime > tokenDateTime)
                        return false;

                    return true;
                }
                else return false;

            }
            catch
            {
                return false;
            }
        }

    }
}