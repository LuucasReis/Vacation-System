using Vacation_MVC.Models;

namespace Vacation_MVC.Services.IServices
{
    public interface IBaseService<T> where T : class 
    {
        Task<T> SendAsync(APIRequest apiRequest);
    }
}
