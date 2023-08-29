using Vacation_MVC.Models;
using Vacation_MVC.Models.Dto;
using Vacation_MVC.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Utility;

namespace Vacation_MVC.Services
{
    public class AuthService : BaseService<APIResponse>, IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string apiUrl;
        private string controllerUrl;
        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _httpClientFactory = clientFactory;
            apiUrl = configuration.GetValue<string>("ServicesUrls:InternsAPI")!;
            controllerUrl = configuration.GetValue<string>("ServicesUrls:AuthController")!;
        }
        public async Task<APIResponse> Login(LoginRequestDTO loginRequest)
        {
            return await SendAsync(new APIRequest()
            {
                apiType = SD.APIType.POST,
                Data = loginRequest,
                Url = apiUrl + controllerUrl + "/login"
            });
        }
    }
}
