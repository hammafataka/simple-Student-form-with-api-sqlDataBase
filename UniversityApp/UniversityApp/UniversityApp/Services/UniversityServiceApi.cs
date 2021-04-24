using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


using Newtonsoft.Json;
using Xamarin.Essentials;


namespace UniveristyApp.Services
{
    public class UniversityServiceApi
    {
        public const string ApiUrl = "http://192.168.42.1:5014";
        public static async Task<List<T>> GetItems<T>(string Controller) where T : new()
        {
            List<T> list = new List<T>();
            if (Connectivity.NetworkAccess==NetworkAccess.Internet)
            {
                string FullUrl = $"{ApiUrl}/{Controller}";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage message = await client.GetAsync(FullUrl);
                if (message.IsSuccessStatusCode)
                {
                    string json =await message.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<T>>(json);
                }
            }
            return list;
        }
        public static async Task<T>GetItem<T>(string controller)where T : new()
        {
            T item = new T();
            if (Connectivity.NetworkAccess==NetworkAccess.Internet)
            {
                string FullUrl = $"{ApiUrl}/{controller}";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage message = await client.GetAsync(FullUrl);
                if (message.IsSuccessStatusCode)
                {
                    string json = await message.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<T>(json);
                }

            }
            return item;

        }
        public static async Task<bool>AddItem<T>(string controller,T item)where T : new()
        {
            bool success = false;
            if (Connectivity.NetworkAccess==NetworkAccess.Internet)
                {
                string fullUrl = $"{ApiUrl}/{controller}";
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                HttpResponseMessage messagec = await client.PostAsync(fullUrl,content);
                success = messagec.IsSuccessStatusCode;


     


            }
            return success;
        }
        public static async Task<bool>UpdateItem<T>(string controller,T item,int id)where T: new()
        {
            bool success = false;
            if (Connectivity.NetworkAccess==NetworkAccess.Internet)
            {
                string fullUrl = $"{ApiUrl}/{controller}/{id}";
                string json = JsonConvert.SerializeObject(item);
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(json,Encoding.UTF8,"application/json");
                HttpResponseMessage message = await client.PutAsync(fullUrl, content);
                success = message.IsSuccessStatusCode;
            }
            return success;
        }
        public static async Task<bool> DeleteItem<T>(string Controller,int id)
        {
            bool success = false;
            if (Connectivity.NetworkAccess==NetworkAccess.Internet)
            {   
                string fullUrl = $"{ApiUrl}/{Controller}/{id}";
                HttpClient client = new HttpClient();
                HttpResponseMessage message = await client.DeleteAsync(fullUrl);
                success = message.IsSuccessStatusCode; 
            }
            return success;
        }

    }
}
