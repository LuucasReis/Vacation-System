using System.ComponentModel;
using static Utility.SD;

namespace Vacation_MVC.Models.Dto
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        public string Email { get; set; }
        public  string Period { get; set; }
        public string Occupancy { get; set; }
        public double Salary { get; set; }
        public Regime Regime { get; set; }

    }
}
