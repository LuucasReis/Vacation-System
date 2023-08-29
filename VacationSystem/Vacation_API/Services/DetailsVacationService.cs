using Vacation_API.Data;
using Vacation_API.Data.Repository;
using Vacation_API.Services.IServices;
using Vacation_API.Models;
using Vacation_API.Models.Dto;
using Utility;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace Vacation_API.Services
{
    public class DetailsVacationService : Repository<MyDetails>, IDetailsVacationService
    {
        private readonly AppDbContext _context;
        public DetailsVacationService(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<MyDetails> CreateDetailsAsync(Employee employee)
        {

            if (employee == null)
            {
                return null;
            }

            string period = employee.entryDate.ToString("d") + " - " + employee.leaveDate.ToString("d");
            int eligibledays = 0;
            var daysRestant = 0.0;
            var status = "Sem Férias";


            MyDetails myDetails = new()
            {
                Period = period,
                EligibleDays = eligibledays,
                restantDays = daysRestant,
                Occupancy = employee.Occupancy,
                combinedDays = employee.vacationDays,
                Status = status,
                employeeId = employee.Id
            };

            await _context.myDetails.AddAsync(myDetails);
            await _context.SaveChangesAsync();

            return myDetails;
        }


        public async Task<MyDetails> UpdateDetailsApproveAsync(HistVacation histVacation)
        {
            var details = await _context.myDetails.FirstOrDefaultAsync(x => x.employeeId == histVacation.employeeId);

            details!.EligibleDays -= histVacation!.Days;
            details.restantDays = histVacation!.Days;

            _context.myDetails.Update(details);
            await _context.SaveChangesAsync();

            return details;
        }

        public async Task<MyDetails> UpdateDetailsAsync(string email)
        {
            var details = await _context.myDetails.Include("Employee").FirstOrDefaultAsync(x => x.Employee.Email == email);
            var histVacation = await _context.HistVacations.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.employeeId == details!.employeeId);


            if (histVacation == null)
            {
                var eligibleDays = CalcWorkDays(details!.Employee.entryDate);
                if(eligibleDays > 0)
                {
                    details.Status = "A gozar";
                }
                details.EligibleDays = eligibleDays;
                return details;
            }

            else if (histVacation.IsCompleted && details!.updatedDate == null)
            {
                var eligibleDays = CalcWorkDays(histVacation.finishVacationDate);
                var newValue = details.EligibleDays + eligibleDays;
                if(newValue != details.EligibleDays)
                {
                    details.EligibleDays = newValue;
                    details.Status = "A gozar";
                    details.updatedDate = DateTime.Now;
                }

                return details;
            }
            else if (histVacation.Status == SD.statusReject || histVacation.Status == SD.defaultStatus)
            {
                return details!;
            }

            else if (details!.updatedDate != null)
            {
                if (details.restantDays != 0)
                {
                    var newRestDate = histVacation.finishVacationDate.Subtract(DateTime.Now).TotalDays;
                    details.restantDays = Math.Ceiling(newRestDate);
                    return details;
                }
                else
                {
                    histVacation.IsCompleted = true;
                    details.updatedDate = null;
                    if(details.EligibleDays == 0)
                    {
                        details.Status = "Sem Férias";
                    }
                    return details;
                }
            }

            else
            {
                if (DateTime.Now < histVacation.startVacationDate)
                {
                    return details!;
                }
                else
                {
                    var diasConsumidos = DateTime.Now.Subtract(histVacation.startVacationDate).Days;
                    details!.restantDays -= diasConsumidos;
                    details.updatedDate = DateTime.Now;
                    return details;
                }
            }


        }

        public async Task UpdateDetails(MyDetails details)
        {
            _context.myDetails.Update(details);
            await _context.SaveChangesAsync();
        }
        private static int CalcWorkDays(DateTime startDate)
        {
            var workdays = DateTime.Now.Subtract(startDate).Days;
            if (workdays >= 180)
            {
                return 15;
            }
            else if (workdays >= 360)
            {
                return 30;
            }
            else
                return 0;
        }

    }
}