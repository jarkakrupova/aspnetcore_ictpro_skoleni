using Microsoft.AspNetCore.Mvc;
using MVC.Data;

namespace MVC.Controllers {
    public class SocksController : Controller {
        public IActionResult Index() {
            return View(SockDataset.GetSocks());
        }
    }
}
