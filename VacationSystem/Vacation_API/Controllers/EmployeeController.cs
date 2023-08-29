using Vacation_API.Models;
using Vacation_API.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vacation_API.Models.Dto;
using Vacation_API.Mapping;

namespace Vacation_API.Controllers
{
    [ApiController]
    [Route("api/Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDetailsVacationService _detailsService;
        protected APIResponse _response;
        public EmployeeController(IEmployeeService employee, IDetailsVacationService detailsService)
        {
            _employeeService = employee;
            _response = new APIResponse();
            _detailsService = detailsService;

        }

        /// <summary>
        ///     Dados de todos os estagiarios da Framework
        /// </summary>

        [HttpGet]
        //[Authorize(Roles ="PM")]
        [ProducesResponseType(typeof(APIResponse), 200)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAllEmployees()
        {
            try
            {
                var result = await _employeeService.GetAllAsync();
                if (result == null)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.errorMessages.Add("the employee's table is empty!");
                    return NotFound(_response);
                }

                _response.statusCode = HttpStatusCode.OK;
                _response.result = result;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.errorMessages.Add(ex.Message);

                return _response;
            }

        }

        

        /// <summary>
        ///     Retorna dados de um estagiario da Framework.
        /// </summary>

        [ProducesResponseType(typeof(APIResponse), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id:int}",Name ="Obter-estagiario-by-id")]
        [Authorize]
        public async Task<ActionResult<APIResponse>> GetEmployebyId(int id)
        {
            try
            {
                if(id == 0)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.errorMessages.Add("The field id is invalid");
                    return BadRequest(_response);
                }

                var stage = await _employeeService.GetByIdAsync(x => x.Id == id);
                if(stage == null)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.errorMessages.Add("user not found");
                    return NotFound(_response);
                }

                _response.statusCode = HttpStatusCode.OK;
                _response.result = stage;
                return Ok(_response);


            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.errorMessages.Add(ex.Message);

                return _response;
            }
        }

        /// <summary>
        ///     Cria um novo estagiario na Framework
        /// </summary>

        [HttpPost]
        //[Authorize(Roles = "PM")]
        [ProducesResponseType(typeof(APIResponse), 201)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> CreateEmployee([FromBody] EmployeeCreateDTO employeeDTO)
        {
            try
            {
                var employee = employeeDTO.EmployeeCreateDTOtoEmployee();
                var employeeFromDb = await _employeeService.AddAsync(employee);
                if(employeeFromDb == null)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.errorMessages.Add("Something went wrong with your create employee.");
                    return BadRequest(_response);
                }

                var details = await _detailsService.CreateDetailsAsync(employeeFromDb);
                if(details == null)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.errorMessages.Add("Something went wrong with your details create.");
                    return BadRequest(_response);
                }
                _response.statusCode=HttpStatusCode.Created;
                _response.result = employee;
                return CreatedAtRoute("Obter-estagiario-by-id", new {id = employee.Id}, _response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.errorMessages.Add(ex.Message);

                return _response;
            }

        }
    }
}
