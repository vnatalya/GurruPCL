using System.Net;

namespace GurruPCL.Models
{
    public class BaseResult
    {
        public BaseResult(HttpStatusCode status = HttpStatusCode.OK, string title = "Error", string message = "An unexpected error has occured")
        {
            Status = status;
            Message = message;
            Title = title;
        }
        public HttpStatusCode Status { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
