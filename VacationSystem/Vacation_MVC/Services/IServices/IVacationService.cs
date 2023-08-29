using Vacation_MVC.Models.Dto;
using Vacation_MVC.Models;
using System.Linq.Expressions;

namespace Vacation_MVC.Services.IServices
{
    public interface IVacationService 
    {
        Task<APIResponse> GetAllAsync();
        Task<APIResponse> GetAllbyIdAsync(int id);
        Task<APIResponse> GetByIdAsync(int id);
        Task<APIResponse> ApproveVacationAsync(int id);
        Task<APIResponse> ReproveVacationAsync(int id);
        Task<APIResponse> GetMyDetailsAsync(string email);
        Task<APIResponse> GetMyDetailsByIdAsync(string email);
        Task<APIResponse> RequestVacationAsync(HistVacationCreateDTO Create);
    }
}
