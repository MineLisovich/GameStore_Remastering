using System.Security.Cryptography;
using System.Text;

namespace GameStore.BLL.Infrastrcture.Singletons
{
    /// <summary>
    /// Кприптография ключей от игр
    /// </summary>
    public sealed class GameKeyCryptography
    {
        private static GameKeyCryptography _instance;
        private static readonly object _instanceLock = new object();

        //Так делать нельзя, но так как у меня pet-project, который никогда не выйдет на рынок, то мне можно))
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("1234567890123456"); 
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456"); 

        private GameKeyCryptography() { }

        public static GameKeyCryptography GetIstance()
        {
            // Проверяем, существует ли уже экземпляр
            if (_instance is null)
            {
                // Блокируем доступ к коду ниже, чтобы предотвратить создание
                // нескольких экземпляров в многопоточной среде
                lock (_instanceLock)
                {
                    // Проверяем снова, чтобы убедиться, что экземпляр все еще null
                    if (_instance is null)
                    {
                        _instance = new GameKeyCryptography();
                    }
                }
            }
            return _instance;
        }

        public string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

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
}
