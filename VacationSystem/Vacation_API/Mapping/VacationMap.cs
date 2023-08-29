using Vacation_API.Messaging;
using Vacation_API.Models;
using Vacation_API.Models.Dto;
using System;
using Utility;

namespace Vacation_API.Mapping
{
    public static class VacationMap
    {
        public static HistVacation DtoToVacation(this HistVacationCreateDTO vacation)
        {
            var vacationHist = new HistVacation()
            {
                Id = 0,
                startVacationDate = vacation.startVacationDate,
                finishVacationDate = vacation.startVacationDate.AddDays(vacation.Days),
                employeeId = vacation.employeeId,
                Occupancy = vacation.Occupancy,
                Days = vacation.Days,
                Observacoes = vacation.Observacoes,
                Status = SD.defaultStatus
                
            };

            return vacationHist;
            
        }
        public static HistVacationMessage VacationToMessage(this HistVacation vacation)
        {
            var vacationHist = new HistVacationMessage()
            {
                Email = vacation.Employee.Email,
                Name = vacation.Employee.Name,
                Status = vacation.Status
            };

            return vacationHist;

        }

    }
}
