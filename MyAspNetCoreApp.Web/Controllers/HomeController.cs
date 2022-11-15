using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using System.Diagnostics;
using MyAspNetCoreApp.Web.Helpers;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //Helper _helper; // interface kullanılmadığı durumlar için anca pek kullanılan bir yöntem değildir

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //_helper = helper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //var text = "Asp.net";
            //var upperText = _helper.Upper(text);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}