using Hydron.Models.Definitions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Hydron.Helpers
{

    public interface IHttp
    {
       
        void get();

        Task<PayRegResponseModel> postPayRegisterAsync(PayRegRequestModel mod);

        Task<PayFinalResponseModel> postPayFinalAsync(PayFinalRequestModel mod);

        Task<Boolean> postFBMessageAsync(FBMessagingModel mod);
    }

    public class HttpHelper: IHttp
    {
        private string BASE_URL = "https://demo-ipg.ctdev.comtrust.ae:2443";

        public void get()
        {
            throw new NotImplementedException();
        }

      
        public async Task<PayRegResponseModel> postPayRegisterAsync(PayRegRequestModel mod)
        {
            HttpClient client = new HttpClient();
            var body = JsonConvert.SerializeObject(mod);

            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BASE_URL){
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            PayRegResponseModel model = null;
            try
            {
                HttpResponseMessage response = await client.SendAsync(request);               
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<PayRegResponseModel>(responseBody);

            } catch(Exception e)
            {
               //something went wrong....
            }

            return model;
             
        }

        public async Task<PayFinalResponseModel> postPayFinalAsync(PayFinalRequestModel mod)
        {
            HttpClient client = new HttpClient();
            var body = JsonConvert.SerializeObject(mod);

            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BASE_URL)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            PayFinalResponseModel model = null;
            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<PayFinalResponseModel>(responseBody);

            }
            catch (Exception e)
            {
                //something went wrong....
            }

            return model;

        }

        public async Task<Boolean> postFBMessageAsync(FBMessagingModel mod)
        {
            String BASE_URL = "https://fcm.googleapis.com/v1/projects/robot-238711/messages:send";

            HttpClient client = new HttpClient();
            var body = JsonConvert.SerializeObject(mod);
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer ya29.c.Kp0B7Qd3i-X8zAqHnkKXNyLDZBtrTHwdL-2fHQBm5fo9Cyk9---NDdfmGwVmyUT3l8bLSjo3o_HdBGRV5hTVysDMk7nCA4VCCeghmkmV0faTERcBdFcu7r_70JHaf_fg7aZcl9u_Xp1EqZ6mNihllMTs48a662IBM8dXO_grCl4fjGzFg1-4kTfQYlBSCq9VXFfXnGXX7K_usk0nk66fGA");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "ya29.c.Kp0B7Qd3i-X8zAqHnkKXNyLDZBtrTHwdL-2fHQBm5fo9Cyk9---NDdfmGwVmyUT3l8bLSjo3o_HdBGRV5hTVysDMk7nCA4VCCeghmkmV0faTERcBdFcu7r_70JHaf_fg7aZcl9u_Xp1EqZ6mNihllMTs48a662IBM8dXO_grCl4fjGzFg1-4kTfQYlBSCq9VXFfXnGXX7K_usk0nk66fGA");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BASE_URL)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return true;

            }
            catch(Exception ex)
            {
                return false;
            }

        }
    }
}