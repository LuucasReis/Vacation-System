using Vacation_API.Mapping;
using Vacation_API.Email;
using Vacation_API.Messaging;
using Vacation_API.Models;
using Vacation_API.Models.Dto;
using Vacation_API.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility;

namespace Vacation_API.Controllers
{
    [ApiController]
    [Route("api/Vacation")]
    public class VacationController : ControllerBase
    {
        private readonly IVacationService _vacationService;
        private readonly IDetailsVacationService _detailsService;
        protected APIResponse _response;
        private readonly ISqsMessenger _sqsMessenger;
       
        
        public VacationController(IVacationService vacationService, ISqsMessenger sqsMessenger,  IDetailsVacationService detailsService)
        {
            _vacationService = vacationService;
            _response = new();
            _sqsMessenger = sqsMessenger;
            _detailsService = detailsService;

        }

        /// <summary>
        ///     Histórico das férias solicitadas por todos os estagiarios da Framework
        /// </summary>

        [HttpGet]
        //[Authorize(Roles ="PM")]
        [ProducesResponseType(typeof(APIResponse), 200)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAllVacations()
        {
            try
            {
                var result = await _vacationService.GetAllAsync(includeProperties: "Employee");
                if (result == null)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.errorMessages.Add("the vacation's table is empty!");
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
        ///     Histórico de todas as férias solicitadas por um dos estagiarios da Framework
        /// </summary>

        [HttpGet("GetAllBasedOnId")]
        //[Authorize(Roles ="PM")]
        [ProducesResponseType(typeof(APIResponse), 200)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAllVacationsBasedOnId(int id)
        {
            try
            {
                var result = await _vacationService.GetAllAsync(x => x.employeeId == id, includeProperties: "Employee");
                if (result == null)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.errorMessages.Add("the vacation's table is empty!");
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
        ///     Retorna um histórico de férias baseado na id dele.
        /// </summary>

        [ProducesResponseType(typeof(APIResponse), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id:int}", Name = "Obter-ferias-by-id")]
        //[Authorize]
        public async Task<ActionResult<APIResponse>> GetVacationId(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.errorMessages.Add("The field id is invalid");
                    return BadRequest(_response);
                }

                var vacation = await _vacationService.GetByIdAsync(x => x.Id == id, includeProperties: "Employee");

                _response.statusCode = HttpStatusCode.OK;
                _response.result = vacation;
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
        ///     Retorna detalhes de um estagiário da Framework
        /// </summary>

        [ProducesResponseType(typeof(APIResponse), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("Mydetails")]
        //[Authorize]
        public async Task<ActionResult<APIResponse>> GetMyDetails(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.errorMessages.Add("The field id is invalid");
                    return BadRequest(_response);
                }

                
                var myDetails = await _detailsService.UpdateDetailsAsync(email);
                await _detailsService.UpdateDetails(myDetails);

                _response.statusCode = HttpStatusCode.OK;
                _response.result = myDetails;
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
        ///     Retorna detalhes de um estagiário da Framework
        /// </summary>

        [ProducesResponseType(typeof(APIResponse), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("MydetailsById")]
        //[Authorize]
        public async Task<ActionResult<APIResponse>> GetMyDetailsById(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.errorMessages.Add("The field id is invalid");
                    return BadRequest(_response);
                }


                var myDetails = await _detailsService.GetByIdAsync(x=> x.Employee.Email == email, includeProperties:"Employee");

                _response.statusCode = HttpStatusCode.OK;
                _response.result = myDetails;
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
        ///     Cria um novo registro de férias de um estagiario da Framework
        /// </summary>

        [HttpPost]
        //[Authorize(Roles = "PM")]
        [ProducesResponseType(typeof(APIResponse), 201)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> CreateVacation([FromBody] HistVacationCreateDTO createObjDTO, CancellationToken token)
        {
            try
            {
                var histVacation = createObjDTO.DtoToVacation();
                var objFromDb = await _vacationService.RequestVacationAsync(histVacation);
                if (objFromDb != null)
                {
                    await _sqsMessenger.SendMessageAsync(objFromDb.VacationToMessage(), PM:false);

                    
                    _response.statusCode = HttpStatusCode.Created;
                    _response.result = histVacation;
                    return CreatedAtRoute("Obter-ferias-by-id", new { id = histVacation.Id }, _response);
                }

                return BadRequest(createObjDTO);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.errorMessages.Add(ex.Message);

                return _response;
            }

        }
        /// <summary>
        ///     Atualiza um Registro de Férias para status aprovado
        /// </summary>

        [HttpPut("Approve")]
        //[Authorize(Roles = "PM")]
        [ProducesResponseType(typeof(APIResponse), 200)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> ApproveVacation(int id, CancellationToken token)
        {
            try
            {
                var vacation = await _vacationService.ApproveVacationAsync(id);
                if (vacation == null)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.errorMessages.Add("Vacation Register not found");
                    return NotFound(_response);
                }

                await _detailsService.UpdateDetailsApproveAsync(vacation);
               
                await _sqsMessenger.SendMessageAsync(vacation.VacationToMessage(), PM: true);
                
                _response.statusCode = HttpStatusCode.NoContent;
                _response.result = vacation;
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
        ///     Atualiza um Registro de Férias para status reprovado
        /// </summary>

        [HttpPut("Reject")]
        //[Authorize(Roles = "PM")]
        [ProducesResponseType(typeof(APIResponse), 200)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> RejectVacation(int id, CancellationToken token)
        {
            try
            {
                var vacation = await _vacationService.ReproveVacationAsync(id);
                if (vacation == null)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.errorMessages.Add("Vacation Register not found");
                    return NotFound(_response);
                }

                await _sqsMessenger.SendMessageAsync(vacation.VacationToMessage(), PM: true);


                
                _response.statusCode = HttpStatusCode.NoContent;
                _response.result = vacation;
                return Ok(_response);

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
