using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GurruPCL.Models
{
    public class BaseResult
    {
        public HttpStatusCode Status { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
