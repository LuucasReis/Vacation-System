using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vacation_API.Messaging
{
    public class HistVacationMessage
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
