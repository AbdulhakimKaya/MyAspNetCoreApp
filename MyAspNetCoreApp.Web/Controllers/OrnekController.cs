using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class Product2
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class OrnekController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.name = "Asp.Net Core";

            ViewData["age"] = 30;

            ViewData["names"] = new List<string>() {"ahmet", "mehmet", "hasan"};

            ViewBag.person = new { Id=1, Name= "ahmet", Age= 23};

            TempData["surname"] = "KAYA"; // Bir sayfadan diğer bir sayfaya data taşımak için kullanılır

            var productList = new List<Product2>()
            {
                new() { Id = 1, Name = "Kalem" },
                new() { Id = 2, Name = "Defter" },
                new() { Id = 3, Name = "Silgi" },
            };

            return View(productList);
        }

        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Index3()
        {
            return RedirectToAction("Index", "Home");
            //return View();
        }

        // sadece string bir veri döndürürüz sayfa döndürülmez
        public IActionResult ContentResult()
        {
            return Content("Content Result");
        }

        // sadece json formatında bir veri döndürürüz sayfa döndürülmez
        public IActionResult JsonResult()
        {
            return Json(new { id = 1, name = "kalem", price = 120 });
        }

        // hiç bir şey döndürülmez
        public IActionResult EmptyResult()
        {
            return new EmptyResult();
        }

        // action metotların default aldıkları parametreler. Bir action metottan diğer bir action metoda parametre taşınması
        public IActionResult ParametreView(int id)
        {
            return RedirectToAction("JsonResultParametre", "Ornek", new { id = id });
        }

        public IActionResult JsonResultParametre(int id)
        {
            return Json(new { id = id });
        }
    }
}
