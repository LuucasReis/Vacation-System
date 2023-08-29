using Vacation_MVC.Models.Dto;
using Vacation_MVC.Mapping;
using Vacation_MVC.Models;
using Vacation_MVC.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Utility;
using System.Security.Claims;

namespace Vacation_MVC.Controllers
{
    public class VacationController : Controller
    {
        private readonly IVacationService _vacationService;
        public VacationController(IVacationService vacationService)
        {
            _vacationService = vacationService;
        }
        [HttpGet]
        [Authorize(Roles ="PM")]
        public async Task<IActionResult> Index()
        {

            var response = await _vacationService.GetAllAsync();
            if (response == null)
            {
                return BadRequest();
            }
            var result = JsonConvert.DeserializeObject<IEnumerable<HistVacation>>(Convert.ToString(response.result)!);
            var listVacations = result!.ToVacationDTOCollection();

            return View(listVacations);
        }
        [HttpGet]
        [Authorize(Roles = "PM")]
        public async Task<IActionResult> Details(int id)
        {

            var response = await _vacationService.GetByIdAsync(id);
            if (response == null)
            {
                return BadRequest();
            }
            var result = JsonConvert.DeserializeObject<HistVacation>(Convert.ToString(response.result)!);
            var vacationHist = result!.ToVacationTO();

            return View(vacationHist);
        }
        [HttpPost("vacation/approve")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PM")]
        public async Task<IActionResult> Approve(int id)
        
        {

            var response = await _vacationService.ApproveVacationAsync(id);
            response.result = JsonConvert.DeserializeObject<HistVacation>(Convert.ToString(response.result)!);
            if (response.result == null)
            {
                return BadRequest();
            }
            TempData["success"] = "Férias aprovadas com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost("vacation/reject")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PM")]
        public async Task<IActionResult> Reject(int id)
        {

            var response = await _vacationService.ReproveVacationAsync(id);
            response.result = JsonConvert.DeserializeObject<HistVacation>(Convert.ToString(response.result)!);
            if (response.result == null)
            {
                return BadRequest();
            }
            TempData["success"] = "Férias rejeitadas com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;
            var response = await _vacationService.GetMyDetailsAsync(email);
            if (response == null)
            {
                return BadRequest();
            }
            var myDetailsUpdated = JsonConvert.DeserializeObject<MyDetailsDTO>(Convert.ToString(response.result)!);

            
            return View(myDetailsUpdated);
        }

        [HttpGet]
        public async Task<IActionResult> MyDetails(string email)
        {

            var response = await _vacationService.GetMyDetailsByIdAsync(email);
            if (response == null)
            {
                return BadRequest();
            }
            var myDetails = JsonConvert.DeserializeObject<MyDetailsDTO>(Convert.ToString(response.result)!);


            return View(myDetails);
        }


        [HttpGet]
        public async Task<IActionResult> MyVacations(int id)
        {
            var response = await _vacationService.GetAllbyIdAsync(id);
            if (response == null)
            {
                return BadRequest();
            }

            var vacationHist = JsonConvert.DeserializeObject<IEnumerable<HistVacation>>(Convert.ToString(response.result)!);
            var vacationDto = VacationMap.ToVacationDTOCollection(vacationHist!);

            return View(vacationDto);
        }

        

        [HttpGet]
        public async Task<IActionResult> RequestVacation(int id)
        {
            
            return View(new HistVacationCreateDTO() { employeeId = id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestVacation(HistVacationCreateDTO create)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;
            var response = await _vacationService.RequestVacationAsync(create);
            if (response == null)
            {
                return BadRequest();
            }

            TempData["success"] = "Férias solicitadas com sucesso!";
            return RedirectToAction(nameof(MyDetails), new {email});
        }

        #region APICALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _vacationService.GetAllAsync();
            if (response == null)
            {
                return BadRequest();
            }
            var listVacations = VacationMap.ToVacationDTOCollection(JsonConvert.DeserializeObject<IEnumerable<HistVacation>>(Convert.ToString(response.result)));
            return Json(new { data = listVacations });
        }
        #endregion
    }
}
