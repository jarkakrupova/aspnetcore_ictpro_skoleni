using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SocksApiController : ControllerBase {
        [HttpGet]
        public IEnumerable<Socks> Index() {
            return SockDataset.GetSocks();
        }
    }
}
