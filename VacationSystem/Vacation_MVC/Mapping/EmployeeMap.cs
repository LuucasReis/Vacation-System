using Vacation_MVC.Models;
using Vacation_MVC.Models.Dto;

namespace Vacation_MVC.Mapping
{
    public static class EmployeeMap
    {
        public static EmployeeDTO ToEmployeeDTO(this Employee employee)
        {
            var employeeDTO = new EmployeeDTO()
            {
                Name = employee.Name,
                Email = employee.Email,
                Salary = employee.Salary,
                Occupancy = employee.Occupancy,
                Period = employee.entryDate.ToString("d") + " - " + employee.entryDate.AddYears(1).ToString("d"),
            };

            return employeeDTO;
        }

        public static List<EmployeeDTO> ToEmployeeDTOCollection(this IEnumerable<Employee> employees)
        {
            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            foreach (var employee in employees)
            {
               employeeDTOs.Add(new EmployeeDTO()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Occupancy = employee.Occupancy,
                    Period = employee.entryDate.ToString("d") + " - " + employee.entryDate.AddYears(1).ToString("d"),
                });
            }
            return employeeDTOs;
        }
    }
}
