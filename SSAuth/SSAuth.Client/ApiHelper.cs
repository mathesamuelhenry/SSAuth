using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SSAuth.Client
{
    public static partial class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient(string uri)
        {
            if (ApiClient == null)
            {
                ApiClient = new HttpClient();
                ApiClient.DefaultRequestHeaders.Accept.Clear();
                ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                ApiClient.BaseAddress = new Uri(uri);
            }
        }

        public static T CallGetWebApi<T>(string url)
        {
            using (HttpResponseMessage response = ApiHelper.ApiClient.GetAsync(url).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(data);
                }
                else
                {
                    var message = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(message);
                }
            }
        }

        public static string CallGetWebApiResultContent(string url)
        {
            using (HttpResponseMessage response = ApiHelper.ApiClient.GetAsync(url).Result)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public static T CallDeleteWebApi<T>(string url)
        {
            using (HttpResponseMessage response = ApiHelper.ApiClient.DeleteAsync(url).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(data);
                }
                else
                {
                    var message = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(message);
                }
            }
        }

        public static T CallPostWebApi<T>(string url, T value)
        {
            using (HttpResponseMessage response = ApiClient.PostAsJsonAsync(url, value).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(data);
                }
                else
                {
                    var message = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(message);
                }
            }
        }

        public static TOut CallPostWebApi<TIn, TOut>(string url, TIn value)
        {
            using (HttpResponseMessage response = ApiClient.PostAsJsonAsync(url, value).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<TOut>(data);
                }
                else
                {
                    var message = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(message);
                }
            }
        }

        public static void GenerateException(int statusCode, string message)
        {
        }
    }
}
