using Domain.EntitiyModels.ActionModels;
using Domain.EntitiyModels.RequestModels;

namespace WebApp.Models
{
    public class RequestListViewModel
    {
        public IEnumerable<Request> Requests { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
