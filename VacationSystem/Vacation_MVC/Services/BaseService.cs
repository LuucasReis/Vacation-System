using Vacation_MVC.Models;
using Vacation_MVC.Services.IServices;
using Newtonsoft.Json;
using System.Text;
using Utility;

namespace Vacation_MVC.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("API_SQS");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if(apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                switch (apiRequest.apiType)
                {
                    case SD.APIType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case SD.APIType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.APIType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                }

                
                HttpResponseMessage apiResponse = await client.SendAsync(message);

                var apiResult = await apiResponse.Content.ReadAsStringAsync();
                var apiObject = JsonConvert.DeserializeObject<T>(apiResult);
                return apiObject;
            }
            catch (Exception ex)
            {
                APIResponse _response = new()
                {
                    errorMessages = new List<string>() { ex.Message },
                    isSuccess = false
                };

                var response = JsonConvert.SerializeObject(_response);
                var apiResponse = JsonConvert.DeserializeObject<T>(response);
                return apiResponse;

            }
        }
    }
}
