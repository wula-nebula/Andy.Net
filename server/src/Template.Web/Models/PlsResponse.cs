using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Web.Models
{
    public class PlsResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }

    public class Responses
    {
        public List<printInfoResponses> printInfoResponses { get; set; } = new List<printInfoResponses>();
    }

    public class printInfoResponses
    {
        public string Shipper_hawbcode { get; set; }
        public string FileUrl { get; set; }
        public string ErrorMessage { get; set; }
    }
}
