using Microsoft.AspNetCore.Mvc;
using MVC.Data;

namespace MVC.Controllers {
    public class SocksController : Controller {
        public IActionResult Index() {
            return View(SockDataset.GetSocks());
        }

        public IActionResult Details(int id) {
            var data = SockDataset.GetSocks();
            var sock = data.FirstOrDefault(sock => sock.Id == id);
            return View(sock);
        }
    }
}
