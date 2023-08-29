using Vacation_MVC.Mapping;
using Vacation_MVC.Models;
using Vacation_MVC.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vacation_MVC.Models.Dto;

namespace Vacation_MVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<IActionResult> Index()
        {

            var response = await _employeeService.GetAllAsync();
            if (response == null)
            {
                return BadRequest();
            }
            var result = JsonConvert.DeserializeObject<IEnumerable<Employee>>(Convert.ToString(response.result)!);
            var listEmployeesDTO = result!.ToEmployeeDTOCollection();
            
            return View(listEmployeesDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateDTO create)
        {

            var response = await _employeeService.CreateEmployeeAsync(create);
            if (response == null)
            {
                return BadRequest();
            }

            TempData["success"] = "Funcionário criado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        #region APICALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _employeeService.GetAllAsync();
            if (response == null)
            {
                return BadRequest();
            }
            var result = JsonConvert.DeserializeObject<IEnumerable<Employee>>(Convert.ToString(response.result)!);
            var listEmployeesDTO = result!.ToEmployeeDTOCollection();
            return Json(new { data = listEmployeesDTO });
        }
        #endregion

    }
}
