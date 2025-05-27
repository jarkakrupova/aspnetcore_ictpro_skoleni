using Microsoft.Build.Execution;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MVC.Models {
    public class Request {
        public int Id { get; set; }
        public DateTime RequestDateTime { get; set; }
        [MaxLength(500)]
        public string RequestUrl { get; set; }
        [MaxLength(50)]
        public string IPAddress { get; set; }
    }
}
