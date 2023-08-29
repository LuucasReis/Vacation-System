using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vacation_MVC.Models.Dto
{
    public class HistVacationCreateDTO
    {
        [DisplayName("Data Inicio")]
        public DateTime startVacationDate { get; set; }
        public int employeeId { get; set; }
        [DisplayName("Regime")]
        public string Occupancy { get; set; }
        [DisplayName("Dias")]
        public int Days { get; set; }
        public string Observacoes { get; set; }
    }
}
