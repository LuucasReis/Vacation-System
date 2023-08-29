using Vacation_API.Data.Repository.IRepository;
using Vacation_API.Models;

namespace Vacation_API.Services.IServices
{
    public interface IDetailsVacationService : IRepository<MyDetails>
    {
        Task<MyDetails> CreateDetailsAsync(Employee employee);
        Task<MyDetails> UpdateDetailsAsync(string email);
        Task<MyDetails> UpdateDetailsApproveAsync(HistVacation vacation);

        Task UpdateDetails(MyDetails details);
    }
}
