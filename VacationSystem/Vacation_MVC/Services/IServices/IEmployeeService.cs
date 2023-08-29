using Vacation_MVC.Models;
using System.Linq.Expressions;
using Vacation_MVC.Models.Dto;

namespace Vacation_MVC.Services.IServices
{
    public interface IEmployeeService 
    {
        Task<APIResponse> GetAllAsync();
        Task<APIResponse> GetByIdAsync(int id);
        Task<APIResponse> CreateEmployeeAsync(EmployeeCreateDTO employee);
    }
}
