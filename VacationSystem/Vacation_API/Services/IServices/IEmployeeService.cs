using Vacation_API.Data.Repository.IRepository;
using Vacation_API.Models;

namespace Vacation_API.Services.IServices
{
    public interface IEmployeeService : IRepository<Employee>
    {
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task<Employee> AddAsync(Employee employee);
    }
}
