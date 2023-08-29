using Vacation_MVC.Models;
using Vacation_MVC.Models.Dto;
using System;

namespace Vacation_MVC.Mapping
{
    public static class VacationMap
    {
        public static HistVacationDTO ToVacationTO(this HistVacation vacation)
        {
            var vacationDTO = new HistVacationDTO()
            {
                Id = vacation.Id,
                startVacationDate = vacation.startVacationDate.ToString("d"),
                finishVacationDate = vacation.finishVacationDate.ToString("d"),
                employeeId = vacation.employeeId,
                Employee = vacation.Employee,
                Occupancy = vacation.Occupancy,
                Days = vacation.Days,
                Observacoes = vacation.Observacoes,
                Status = vacation.Status,
                Period = vacation.Employee.entryDate.ToString("d") + " - " + vacation.Employee.entryDate.AddYears(1).ToString("d"),
            };

            return vacationDTO;
        }

        public static List<HistVacationDTO> ToVacationDTOCollection(this IEnumerable<HistVacation> vacations)
        {
            List<HistVacationDTO> vacationDTOs = new List<HistVacationDTO>();
            foreach (var vacation in vacations)
            {
               vacationDTOs.Add(new HistVacationDTO()
               {
                   Id = vacation.Id,
                   startVacationDate = vacation.startVacationDate.ToString("d"),
                   finishVacationDate = vacation.finishVacationDate.ToString("d"),
                   employeeId = vacation.employeeId,
                   Employee = vacation.Employee,
                   Occupancy = vacation.Occupancy,
                   Days = vacation.Days,
                   Observacoes = vacation.Observacoes,
                   Status = vacation.Status,
                   Period = vacation.Employee.entryDate.ToString("d") + " - " + vacation.Employee.entryDate.AddYears(1).ToString("d"),
               });
            }
            return vacationDTOs;
        }
    }
}
