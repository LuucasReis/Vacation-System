using Vacation_MVC.Models;
using Vacation_MVC.Models.Dto;
using Vacation_MVC.Services.IServices;
using Utility;

namespace Vacation_MVC.Services
{
    public class VacationService : BaseService<APIResponse>, IVacationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string apiUrl;
        private string controllerUrl;
        public VacationService(IHttpClientFactory clientFactory , IConfiguration configuration) : base(clientFactory)
        {
            _httpClientFactory = clientFactory;
            apiUrl = configuration.GetValue<string>("ServicesUrls:InternsAPI")!;
            controllerUrl = configuration.GetValue<string>("ServicesUrls:VacationControllerURl")!;
        }

        public async Task<APIResponse> ApproveVacationAsync(int id)
        {
            return await SendAsync(new APIRequest()
            {
                apiType = SD.APIType.PUT,
                Url = apiUrl + controllerUrl + "/Approve?id=" + id 
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

        public async Task<APIResponse> GetAllbyIdAsync(int id)
        {
            return await SendAsync(new APIRequest()
            {
                apiType = SD.APIType.GET,
                Url = apiUrl + controllerUrl + "/GetAllBasedOnId?id=" + id
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

        public async Task<APIResponse> GetMyDetailsAsync(string email)
        {
            return await SendAsync(new APIRequest()
            {
                apiType = SD.APIType.GET,
                Url = apiUrl + controllerUrl + "/Mydetails?email=" + email
            });
        }

        public async Task<APIResponse> GetMyDetailsByIdAsync(string email)
        {
            return await SendAsync(new APIRequest()
            {
                apiType = SD.APIType.GET,
                Url = apiUrl + controllerUrl + "/MydetailsById?email=" + email
            });
        }

        public async Task<APIResponse> ReproveVacationAsync(int id)
        {
            return await SendAsync(new APIRequest()
            {
                apiType = SD.APIType.PUT,
                Url = apiUrl + controllerUrl + "/Reject?id=" + id
            });
        }

        public async Task<APIResponse> RequestVacationAsync(HistVacationCreateDTO create)
        {
            return await SendAsync(new APIRequest()
            {
                apiType = SD.APIType.POST,
                Data = create,
                Url = apiUrl + controllerUrl 
            });
        }
    }
}
