using System.ComponentModel.DataAnnotations.Schema;

namespace Vacation_API.Models.Dto
{
    public class HistVacationCreateDTO
    {
        public DateTime startVacationDate { get; set; }
        public int employeeId { get; set; }
        public string Occupancy { get; set; }
        public int Days { get; set; }
        public string Observacoes { get; set; }
    }
}
