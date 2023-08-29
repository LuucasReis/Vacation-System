using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Utility.SD;

namespace Vacation_API.Models
{
    public class MyDetails
    {
        public int Id { get; set; }
        public string Period { get; set; }
        public string Occupancy { get; set; }
        public int EligibleDays { get; set; }
        public double restantDays { get; set; }
        public int combinedDays { get; set; }
        public string Status { get; set; }
        [ForeignKey("Employee")]
        public int employeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime? updatedDate { get; set; }
    }
}
