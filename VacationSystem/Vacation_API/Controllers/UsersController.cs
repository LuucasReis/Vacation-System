using Vacation_API.Models;
using Vacation_API.Models.Dto;
using Vacation_API.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Vacation_API.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        protected APIResponse _response;
        public UsersController(IUserService userService)
        {
            _userService = userService;
            _response = new();
        }

        /// <summary>
        ///     Faz o Login de um user da Framework
        /// </summary>

        [HttpPost("login")]
        [ProducesResponseType(typeof(APIResponse), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _userService.Login(loginRequestDTO);
            if(loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.isSuccess = false;
                _response.errorMessages.Add("Username or password is invalid");
                return BadRequest(_response);
            }
            _response.statusCode = HttpStatusCode.OK;
            _response.result = loginResponse;
            return Ok(_response);
        }
    }
}
