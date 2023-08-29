using Vacation_API.Data;
using Vacation_API.Data.Repository;
using Vacation_API.Services.IServices;
using Vacation_API.Models;
using Vacation_API.Models.Dto;
using Utility;

namespace Vacation_API.Services
{
    public class EmployeeService : Repository<Employee>, IEmployeeService
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeService(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            var employeeFromDb = await _appDbContext.Employees.AddAsync(employee);
            
            var localUser = new LocalUser()
            { 
                Id = 0,
                Email = employee.Email ,
                Name = employee.Name ,
                userName = employee.Email,
                Password = SD.defaultPassword,
                Role = employee.Occupancy
            };

            await _appDbContext.Users.AddAsync(localUser);
            await _appDbContext.SaveChangesAsync();
            return employee;

        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            _appDbContext.Employees.Update(employee);
            await _appDbContext.SaveChangesAsync();
            return employee;
        }
    }
}
