using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class BlogsController : Controller
    {
        public IActionResult Article(string name, int id)
        {
            //var routes = Request.RouteValues["article"];

            return View();
        }
    }
}
