using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using Unity.WebApi;

namespace Dashing.Wellcom.Dam.Models
{
    public class HMACUtils
    {
        public string Account { get; set; }
        public string Key { get; set; }
        public string BaseUrl { get; set; }
        

        public HMACUtils()
        {
            var appSettings = ConfigurationManager.AppSettings;
            Account = appSettings["WellcomAccount"];
            Key = appSettings["WellcomKey"];
            BaseUrl= appSettings["WellcomBaseUrl"];
        }

       public string CreateMessageSignature(string timestamp)
        {
            string messageToHMAC = Account + timestamp;
            string sig = CreateHMACForMessageAndKey(messageToHMAC, Key);
            return sig;
        }

        public string CreateHMACForMessageAndKey(string message, string key)
        {
            HMACSHA256 hmacsha1 = new HMACSHA256();
            string secretKey = key;
            string content = message;
            byte[] secretKeyBArr = Encoding.UTF8.GetBytes(secretKey);
            byte[] contentBArr = Encoding.UTF8.GetBytes(content);
            hmacsha1.Key = secretKeyBArr;
            byte[] final = hmacsha1.ComputeHash(contentBArr);
            var result = Convert.ToBase64String(final);
            return result;
        }

    }

}