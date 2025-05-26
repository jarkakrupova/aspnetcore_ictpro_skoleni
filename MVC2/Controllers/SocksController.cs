using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers {
    [Route("ponozky")]
    [Route("[controller]")]
    public class SocksController : Controller {
        public IActionResult Index() {
            return View(SockDataset.Socks);
        }
        [Route("[action]/{id}")]
        public IActionResult Details(int id) {
            var data = SockDataset.Socks;
            var sock = data.FirstOrDefault(sock => sock.Id == id);
            return View(sock);
        }
        //[Route("[action]/min/{minPrice:int}/max/{maxPrice:int}")]
        [Route("[action]")]
        public IActionResult SearchByPrice(int minPrice, int maxPrice) {
            var data = SockDataset.Socks;
            var socks = data.Where(sock => sock.Price >= minPrice && sock.Price <= maxPrice);
            //return View(socks);
            return View("Index", socks); //technicky muzu zrecykovat i jine view, akorat to bude s filtrem
        }
    }
}
