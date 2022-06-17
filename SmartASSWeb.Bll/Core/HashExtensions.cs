using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace SmartASSWeb.Bll.Core
{
    public class HashExtensions
    {
        // Adapted from
        // https://msdn.microsoft.com/en-us/library/system.security.cryptography.md5(v=vs.110).aspx

        public static string GetMd5Hash(List<Parameter> parameters, string encryptionKey)
        {
            using (var md5Hash = MD5.Create())
            {
                var input = new StringBuilder();
                foreach (var parm in parameters)
                {
                    input.Append(parm.Value);
                }
                input.Append(encryptionKey);

                var hash = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input.ToString()));
                var sBuilder = new StringBuilder();

                foreach (var i in hash)
                {
                    sBuilder.Append(i.ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
        public string GetMd5Hash(Dictionary<string, string> data, string encryptionKey)
        {
            using (var md5Hash = MD5.Create())
            {
                var input = new StringBuilder();
                foreach (var value in data.Values)
                {
                    input.Append(value);
                }
                input.Append(encryptionKey);

                var hash = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input.ToString()));
                var sBuilder = new StringBuilder();

                foreach (var i in hash)
                {
                    sBuilder.Append(i.ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        public bool VerifyMd5Hash(Dictionary<string, string> data, string encryptionKey, string hash)
        {
            var hashDict = data.Keys.Where(key => key != "CHECKSUM").ToDictionary(key => key, key => data[key]);

            var hashOfInput = GetMd5Hash(hashDict, encryptionKey);
            var comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(hashOfInput, hash);
        }

    }
}
