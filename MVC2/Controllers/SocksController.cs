using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers {
    public class SocksController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
