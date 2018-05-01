using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Dashing.Wellcom.Dam.Models
{
    public class WellcomProductsService
    {
        private readonly string _account ;
        private readonly string _baseUrl;
        private readonly HMACUtils _hmacUtils;

        public WellcomProductsService(HMACUtils hmacUtils)
        {
            _account = hmacUtils.Account;
            _baseUrl = hmacUtils.BaseUrl;
            _hmacUtils = hmacUtils;
        }
        public IEnumerable<WellcomProduct> SearchProdcucts(string desc,string code,string gtin,int batch,int batchSize)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            var timestamp = CreateTimestamp();
            var signature = System.Uri.EscapeDataString(_hmacUtils.CreateMessageSignature(timestamp));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var qualVal = System.Uri.EscapeDataString($"descr={desc},code={code},gtin={gtin}");
            string qualVal = "";
            

            //if (!string.IsNullOrEmpty(desc))
            //    qualVal += "descr=" + desc + ",";
            if (!string.IsNullOrEmpty(code))
                qualVal += "code=" + code + ",";
            if (!string.IsNullOrEmpty(gtin))
                qualVal += "gtin=" + gtin;
            if (qualVal.EndsWith(","))
                qualVal = qualVal.Remove(qualVal.Length - 1);


            qualVal = System.Uri.EscapeDataString(qualVal);
            string requestString =
                $"Product.json?signature={signature}&account={_account}&timestamp={timestamp}&qualifier={qualVal}&batch={batch}&batchSize={batchSize}";
            if (!string.IsNullOrEmpty(desc))
            {
                requestString += "&searchVal=" + System.Uri.EscapeDataString(desc);
            }

            HttpResponseMessage response = client.GetAsync(requestString).Result;  // Blocking call!
            var searchResult = response.Content.ReadAsStringAsync().Result;
            IEnumerable<WellcomProduct> products = new List<WellcomProduct>();
            if (response.IsSuccessStatusCode)
            {
                products = response.Content.ReadAsAsync<IEnumerable<WellcomProduct>>().Result;
            }

            return products;
        }

        public WellcomProduct GetProduct(string uuid)
        {
            var timestamp = CreateTimestamp();
            var signature = System.Uri.EscapeDataString(_hmacUtils.CreateMessageSignature(timestamp));
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"Product/{uuid}.json?signature={signature}&account={_account}&timestamp={timestamp}").Result;  // Blocking call!
            var responseResult = response.Content.ReadAsStringAsync().Result;
            WellcomProduct resultProduct = new WellcomProduct();
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                resultProduct = response.Content.ReadAsAsync<WellcomProduct>().Result;
            }

            return resultProduct;

        }
        private static string CreateTimestamp()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fff") + "Z";
            return timestamp;
        }
    }
}