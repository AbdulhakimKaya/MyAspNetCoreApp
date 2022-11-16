using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp.Web.Helpers;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _context;
        private IHelper _helper;
        private readonly ProductRepository _productRepository;

        public ProductsController(AppDbContext context, IHelper helper) // constructor injection
        {
            _productRepository = new ProductRepository();
            _context = context;
            _helper = helper;
        }
        public IActionResult Index([FromServices]IHelper helper2) // method injection 
        {
            //var products = _productRepository.GetAll();

            var text = "Asp.Net";
            var upperText = _helper.Upper(text);

            var status = _helper.Equals(helper2);

            var products = _context.Products.ToList();
            var product = _context.Products.First();
            return View(products);
        }

        public IActionResult Remove(int id)
        {
            //_productRepository.Remove(id);

            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Add()
        {

            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 month", 1},
                {"3 month", 3},
                {"6 month", 6},
                {"12 month", 12},
            };


            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data = "Blue", Value = "Blue"},
                new(){Data = "Green", Value = "Green"},
                new(){Data = "Purple", Value = "Purple"},
            }, "Value", "Data");


            return View();
        }
        [HttpPost]  // get işlemi yapılırken HttpGet yazıldı
        public IActionResult Add(Product newProduct)  // get işlemi yapılırken SaveProduct yapılıp yeni view sayfası oluşturuldu
        {
            //// 1.yontem - yukarıda parametre vermeden çalıştırılır - Add()
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock =int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var color = HttpContext.Request.Form["Color"].ToString();

            //Product newProduct = new Product() { Name = name, Price = price, Stock = stock, Color = color };

            //// 2.yontem - Request Header-Body - Add(string Name, decimal Price, int Stock, string Color)
            //Product newProduct = new Product() { Name = Name, Price = Price, Stock = Stock, Color = Color };

            _context.Products.Add(newProduct);
            _context.SaveChanges();
            TempData["status"] = "The product has been successfully added";

            return RedirectToAction("Index");

            //return View(); // get işlemi yapılırken kullanıldı
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);

            ViewBag.ExpireValue = product.Expire;
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 month", 1},
                {"3 month", 3},
                {"6 month", 6},
                {"12 month", 12},
            };


            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data = "Blue", Value = "Blue"},
                new(){Data = "Green", Value = "Green"},
                new(){Data = "Purple", Value = "Purple"},
            }, "Value", "Data", product.Color);

            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product updateProduct, int productId)
        {
            // hybrid model yapmak için productId parametre olarak verildi ve Update.cshtml de değişiklikler yapıldı
            updateProduct.Id = productId;

            _context.Products.Update(updateProduct);
            _context.SaveChanges();

            TempData["status"] = "The product has been successfully updated";

            return RedirectToAction("Index");
        }
    }
}
