using Vacation_API.Data;
using Vacation_API.Data.Repository;
using Vacation_API.Models;
using Vacation_API.Models.Dto;
using Vacation_API.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace Vacation_API.Services
{
    public class VacationService : Repository<HistVacation>, IVacationService
    {
        private readonly AppDbContext _context;
        public VacationService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<HistVacation> ApproveVacationAsync(int id)
        {
            var vacation = await _context.HistVacations.Include("Employee").FirstOrDefaultAsync(x => x.Id == id);
            if (vacation == null)
            {
                return null;
            }

            vacation.Status = SD.statusApprove ;
            _context.HistVacations.Update(vacation);
            await _context.SaveChangesAsync();
            return vacation;
        }

        public async Task<HistVacation> ReproveVacationAsync(int id)
        {
            var vacation = await _context.HistVacations.Include("Employee").FirstOrDefaultAsync(x => x.Id == id);
            if (vacation == null)
            {
                return null;
            }

            vacation.Status = SD.statusReject;
            _context.HistVacations.Update(vacation);
            await _context.SaveChangesAsync();
            return vacation;
        }

        public async Task<HistVacation> RequestVacationAsync(HistVacation vacation)
        {
            
            await _context.AddAsync(vacation);
            await _context.SaveChangesAsync();
            var vacationFromDb = await _context.HistVacations.Include("Employee").FirstOrDefaultAsync(x=> x.Id == vacation.Id);
            return vacationFromDb!;

        }
    }
}
