using System.Net;

namespace Vacation_API.Models
{
    public class APIResponse
    {
        public bool isSuccess { get; set; } = true;
        public HttpStatusCode statusCode { get; set; }
        public List<string> errorMessages { get; set; } = new List<string>();
        public object? result { get; set; }
    }
}
