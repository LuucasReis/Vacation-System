using Vacation_MVC.Models;
using Vacation_MVC.Models.Dto;

namespace Vacation_MVC.Services.IServices
{
    public interface IAuthService
    {
        Task<APIResponse> Login(LoginRequestDTO loginRequest);
    }
}
