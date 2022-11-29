using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using System.Drawing;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private AppDbContext _context;
        private IHelper _helper;
        private readonly ProductRepository _productRepository;

        public ProductsController(AppDbContext context, IHelper helper, IMapper mapper) // constructor injection
        {
            _productRepository = new ProductRepository();
            _context = context;
            _helper = helper;
            _mapper = mapper;
        }
        public IActionResult Index([FromServices]IHelper helper2) // method injection 
        {
            //var products = _productRepository.GetAll();

            var text = "Asp.Net";
            var upperText = _helper.Upper(text);

            var status = _helper.Equals(helper2);
            
            var product = _context.Products.First();

            var products = _context.Products.ToList();

            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        [Route("[controller]/[action]/{page}/{pageSize}", Name = "productPage")]
        public IActionResult Pages(int page, int pageSize)
        {
            var products = _context.Products.Skip((page-1)*pageSize).Take(pageSize).ToList();

            ViewBag.page = page;
            ViewBag.pageSize = pageSize;

            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        //[Route("[controller]/[action]/{productId}")]
        //[Route("[action]/{productId}")]
        //[Route("product/{productId}")]
        //[Route("/product/{productId}")]
        [Route("products/product/{productId}", Name = "product")]
        public IActionResult GetById(int productId)
        {
            var product = _context.Products.Find(productId);
            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpGet("{id}")]
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
        public IActionResult Add(ProductViewModel newProduct)  // get işlemi yapılırken SaveProduct yapılıp yeni view sayfası oluşturuldu
        {
            //// 1.yontem - yukarıda parametre vermeden çalıştırılır - Add()
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock =int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var color = HttpContext.Request.Form["Color"].ToString();

            //Product newProduct = new Product() { Name = name, Price = price, Stock = stock, Color = color };

            //// 2.yontem - Request Header-Body - Add(string Name, decimal Price, int Stock, string Color)
            //Product newProduct = new Product() { Name = Name, Price = Price, Stock = Stock, Color = Color };


            if (!string.IsNullOrEmpty(newProduct.Name) && newProduct.Name.StartsWith("A"))
            {
                ModelState.AddModelError(String.Empty, "Name can not start with letter A");
            }

            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 month", 1},
                {"3 month", 3},
                {"6 month", 6},
                {"12 month", 12},
            };


            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new() { Data = "Blue", Value = "Blue" },
                new() { Data = "Green", Value = "Green" },
                new() { Data = "Purple", Value = "Purple" },
            }, "Value", "Data");


            if (ModelState.IsValid)
            {
                try
                {
                    //throw new Exception("db error");
                    _context.Products.Add(_mapper.Map<Product>(newProduct));
                    _context.SaveChanges();
                    TempData["status"] = "The product has been successfully added";

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "An error occurred while adding the product");
                    return View();
                }
            }
            else
            {
                return View();
            }
            
            

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

            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel updateProduct)
        {
            // hybrid model yapmak için productId parametre olarak verildi ve Update.cshtml de değişiklikler yapıldı

            if (!ModelState.IsValid)
            {
                ViewBag.ExpireValue = updateProduct.Expire;
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
                }, "Value", "Data", updateProduct.Color);

                return View();
            }

            _context.Products.Update(_mapper.Map<Product>(updateProduct));
            _context.SaveChanges();

            TempData["status"] = "The product has been successfully updated";

            return RedirectToAction("Index");
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult HasProductName(string Name)
        {
            var anyProduct = _context.Products.Any(x => x.Name.ToLower() == Name.ToLower());
            if (anyProduct)
            {
                return Json("The name of this product is used");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
