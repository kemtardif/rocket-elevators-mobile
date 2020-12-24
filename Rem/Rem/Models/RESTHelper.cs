using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Rem.Models
{
    class RESTHelper
    {

        private readonly HttpClient _client;


        public RESTHelper()
        {
            _client = new HttpClient();

        }

        public async Task<String> get(string url)
        {
            var response = string.Empty;

            using (_client)
            {
                HttpResponseMessage result = await _client.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();

                }
            }

            return response;
        }

        public async Task<HttpResponseMessage> put(string url)
        {
            HttpResponseMessage response = await _client.PutAsync(url, null);
            return response;
        }

    }
}

