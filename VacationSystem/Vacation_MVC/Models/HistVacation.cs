using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vacation_MVC.Models
{
    public class HistVacation
    {
        
        public int Id { get; set; }
        
        public DateTime startVacationDate { get; set; }
        public DateTime finishVacationDate { get; set; }
        public int employeeId { get; set; }
        public Employee Employee { get; set; }
        public string Occupancy { get; set; }
        public int Days { get; set; }
        public string Observacoes { get; set; }
        public string? Status { get; set; }
    }
}
