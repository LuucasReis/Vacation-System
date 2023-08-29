using Vacation_API.Data.Repository.IRepository;
using Vacation_API.Models;
using Vacation_API.Models.Dto;

namespace Vacation_API.Services.IServices
{
    public interface IVacationService : IRepository<HistVacation>
    {
        Task<HistVacation> RequestVacationAsync(HistVacation vacation);
        Task<HistVacation> ApproveVacationAsync(int id);
        Task<HistVacation> ReproveVacationAsync(int id);
    }
}
