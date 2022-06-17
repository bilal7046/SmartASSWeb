using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace SmartASSWeb.Bll.Tests
{
    [TestClass]
    public class PaymentGatewayTests
    {
        [TestMethod]
        public void Test_PaymentGateway()
        {
            var client = new RestClient("https://secure.paygate.co.za/payweb3/initiate.trans") {Timeout = -1};
            var request = new RestRequest(Method.POST) {AlwaysMultipartFormData = true};
            request.AddParameter("PAYGATE_ID", "10011072130");
            request.AddParameter("REFERENCE", "xDwyrHua98dga6Cqg4USzhrkTnO2");
            request.AddParameter("AMOUNT", 600);
            request.AddParameter("CURRENCY", "ZAR");
            request.AddParameter("RETURN_URL", "https://my.return.url/page");
            request.AddParameter("TRANSACTION_DATE", "2018-01-01 12:00:00");
            request.AddParameter("LOCALE", "en-za");
            request.AddParameter("COUNTRY", "ZAF");
            request.AddParameter("EMAIL", "customer@paygate.co.za");
            //request.AddParameter("CHECKSUM", "59229d9c6cb336ae4bd287c87e6f0220");
            request.AddParameter("CHECKSUM", GetMd5Hash(request.Parameters, "secret"));
            IRestResponse response = client.Execute(request);
            Debug.WriteLine(response.Content);
        }

        [TestMethod]
        public void Test_PaymentGateway2()
        {
            var client = new RestClient("https://secure.paygate.co.za/payweb3/initiate.trans") {Timeout = -1};
            var request = new RestRequest(Method.POST) {AlwaysMultipartFormData = true};
            request.AddParameter("PAYGATE_ID", "10011072130");
            request.AddParameter("REFERENCE", "pgtest_123456789");
            request.AddParameter("AMOUNT", "3299");
            request.AddParameter("CURRENCY", "ZAR");
            request.AddParameter("RETURN_URL", "https://my.return.url/page");
            request.AddParameter("TRANSACTION_DATE", DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss"));
            request.AddParameter("LOCALE", "en-za");
            request.AddParameter("COUNTRY", "ZAF");
            request.AddParameter("EMAIL", "demo@smartass.com");
            //request.AddParameter("CHECKSUM", "59229d9c6cb336ae4bd287c87e6f0220");
            request.AddParameter("CHECKSUM", GetMd5Hash(request.Parameters, "secret"));
            IRestResponse response = client.Execute(request);
            Debug.WriteLine(response.Content);
        }

        #region FactoryMethods

        // Adapted from
        // https://msdn.microsoft.com/en-us/library/system.security.cryptography.md5(v=vs.110).aspx

        public string GetMd5Hash(List<Parameter> parameters, string encryptionKey)
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

        #endregion
    }
}
