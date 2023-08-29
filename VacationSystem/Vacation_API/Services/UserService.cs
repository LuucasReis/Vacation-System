using Vacation_API.Data;
using Vacation_API.Models.Dto;
using Vacation_API.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Vacation_API.Services 
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private string secretKey;
        public UserService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret") !;
        }
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=> x.userName.ToLower() == loginRequestDTO.UserName.ToLower() && x.Password == loginRequestDTO.Password);
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDTO loginResponseDTO = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };

            return loginResponseDTO;

        }
    }

}
