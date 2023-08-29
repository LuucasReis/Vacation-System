using Vacation_API.Models;
using Vacation_API.Models.Dto;

namespace Vacation_API.Mapping
{
    public static class EmployeeMap
    {
        public static Employee EmployeeCreateDTOtoEmployee(this EmployeeCreateDTO employeeDTO) 
        {
            var employee = new Employee
            {
                Id = 0,
                Name = employeeDTO.Name,
                Email = employeeDTO.Email,
                Salary = employeeDTO.Salary,
                entryDate = employeeDTO.entryDate,
                Occupancy = employeeDTO.Occupancy,
                leaveDate = employeeDTO.leaveDate,
                Regime = employeeDTO.Regime,
                vacationDays = employeeDTO.vacationDays
            };
            return employee;
        }
    }
}
