using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Utility.SD;

namespace Vacation_MVC.Models.Dto
{
    public class EmployeeCreateDTO
    {
        [DisplayName("Nome")]
        public required string Name { get; set; }
        public required string Email { get; set; }
        [DisplayName("Salario")]
        public double Salary { get; set; }
        [DisplayName("Data de Entrada")]
        public DateTime entryDate { get; set; }
        [DisplayName("Cargo")]
        public required string Occupancy { get; set; }
        public Regime Regime { get; set; }
        [DisplayName("Data final contrato")]
        public DateTime leaveDate { get; set; }
        [DisplayName("Férias combinadas")]
        public int vacationDays { get; set; }
    }
}
