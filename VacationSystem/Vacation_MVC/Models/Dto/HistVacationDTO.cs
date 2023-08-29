using System.ComponentModel;

namespace Vacation_MVC.Models.Dto
{
    public class HistVacationDTO
    {
        public int Id { get; set; }

        [DisplayName("Primeiro Dia")]
        public string startVacationDate { get; set; }
        [DisplayName("Ultimo Dia")]
        public string finishVacationDate { get; set; }
        public int employeeId { get; set; }
        public Employee Employee { get; set; }
        public string Occupancy { get; set; }
        [DisplayName("Dias")]
        public int Days { get; set; }
        public string Observacoes { get; set; }
        public string Status { get; set; }
        [DisplayName("Periodo Eletivo")]
        public string Period { get; set; }
    }
}
