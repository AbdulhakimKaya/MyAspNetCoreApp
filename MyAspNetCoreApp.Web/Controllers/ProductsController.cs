using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using System.Drawing;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _context;
        private readonly ProductRepository _productRepository;

        public ProductsController(AppDbContext context)
        {
            _productRepository = new ProductRepository();
            _context = context;
        }
        public IActionResult Index()
        {
            //var products = _productRepository.GetAll();

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
