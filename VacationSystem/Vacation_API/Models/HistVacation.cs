using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility;

namespace Vacation_API.Models
{
    public class HistVacation
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime startVacationDate { get; set; }
        public DateTime finishVacationDate { get; set; }
        [ForeignKey("Employee")]
        public int employeeId { get; set; }
        public Employee Employee { get; set; }
        public string Occupancy { get; set; }
        public int Days { get; set; }
        public string Observacoes { get; set; }
        public string Status { get; set; } = SD.defaultStatus;
        public bool IsCompleted { get; set; } = false;
    }
}
