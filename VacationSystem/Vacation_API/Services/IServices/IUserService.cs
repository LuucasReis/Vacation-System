using Vacation_API.Models.Dto;

namespace Vacation_API.Services.IServices
{
    public interface IUserService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    }
}
