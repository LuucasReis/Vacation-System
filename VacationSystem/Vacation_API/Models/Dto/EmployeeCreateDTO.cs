using static Utility.SD;

namespace Vacation_API.Models.Dto
{
    public class EmployeeCreateDTO
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public double Salary { get; set; }
        public DateTime entryDate { get; set; }
        public required string Occupancy { get; set; }
        public Regime Regime { get; set; }
        public DateTime leaveDate { get; set; }
        public int vacationDays { get; set; }
    }
}
