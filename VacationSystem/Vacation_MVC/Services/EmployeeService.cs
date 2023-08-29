using Vacation_MVC.Models;
using Vacation_MVC.Services.IServices;
using Utility;
using Vacation_MVC.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Vacation_MVC.Services
{
    public class EmployeeService : BaseService<APIResponse>, IEmployeeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string apiUrl;
        private string controllerUrl;
        public EmployeeService(IHttpClientFactory clientFactory , IConfiguration configuration) : base(clientFactory)
        {
            _httpClientFactory = clientFactory;
            apiUrl = configuration.GetValue<string>("ServicesUrls:InternsAPI")!;
            controllerUrl = configuration.GetValue<string>("ServicesUrls:EmployeeControllerURl")!;
        }

        public async Task<APIResponse> CreateEmployeeAsync(EmployeeCreateDTO employee)
        {
            return await SendAsync(new APIRequest()
            {
                apiType = SD.APIType.POST,
                Data = employee,
                Url = apiUrl + controllerUrl
            });
        }

        public async Task<APIResponse> GetAllAsync()
        {
            return await SendAsync(new APIRequest ()
            {
                apiType = SD.APIType.GET,
                Url = apiUrl + controllerUrl
            });
        }

        public async Task<APIResponse> GetByIdAsync(int id)
        {
            return await SendAsync(new APIRequest()
            {
                apiType = SD.APIType.GET,
                Url = apiUrl + controllerUrl + "/" + id
            });
        }
    }
}
