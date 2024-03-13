using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWPF.Services
{
    public static class TokenManager
    {
        private static readonly string file = "data.dat";
        private static byte[] token;

        public static byte[] Token
        {
            get => GetToken();
            set => SetToken(value);
        }

        static TokenManager()
        {
            if (!File.Exists(file))
            {
                File.WriteAllBytes(file, new byte[0]);
            }
            token = File.ReadAllBytes(file);
        }

        private static void SetToken(byte[] newToken)
        {
            if (newToken == null || newToken.Length == 0)
            {
                throw new InvalidOperationException("Token is not set or is empty.");
            }

            try
            {
                File.WriteAllBytes(file, newToken);
                token = newToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while saving token to file: {ex.Message}");
            }
        }

        private static byte[] GetToken()
        {
            return GetCopy(token);
        }

        private static byte[] GetCopy(byte[] source)
        {
            var result = new byte[source.Length];
            Array.Copy(source, result, source.Length);
            return result;
        }
    }
}
