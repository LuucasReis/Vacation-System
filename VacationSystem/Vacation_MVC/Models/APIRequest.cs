using System.Net;
using static Utility.SD;

namespace Vacation_MVC.Models
{
    public class APIRequest
    {
        public APIType apiType { get; set; } 
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
