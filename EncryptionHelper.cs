using System.Security.Cryptography;
using System.Text;
using System.IO;

public static class EncryptionHelper
{
    private static string GetEncryptionKey()
    {
        const string envFile = ".env";
        if (!File.Exists(envFile))
        {
            throw new FileNotFoundException("Finner ikke .env-filen. Sørg for at den finnes med ENCRYPTION_KEY.");
        }

        var lines = File.ReadAllLines(envFile);
        foreach (var line in lines)
        {
            if (line.StartsWith("ENCRYPTION_KEY="))
            {
                return line.Substring("ENCRYPTION_KEY=".Length);
            }
        }

        throw new Exception("ENCRYPTION_KEY ikke funnet i .env-filen.");
    }

    public static string Encrypt(string plainText)
    {
        string key = GetEncryptionKey();
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32)); // 256-bit nøkkel
            aes.IV = new byte[16]; // Null IV for enkelhet

            ICryptoTransform encryptor = aes.CreateEncryptor();

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public static string Decrypt(string cipherText)
    {
        string key = GetEncryptionKey();
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
            aes.IV = new byte[16];

            ICryptoTransform decryptor = aes.CreateDecryptor();

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}