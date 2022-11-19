using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Views.Shared.ViewComponents
{
    public class ProductListViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public ProductListViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int type)
        {
            var viewModels = _context.Products.Select(x => new 
                ProductListComponentViewModel()
                {
                    Name = x.Name,
                    Description = x.Description,
                }).ToList();

                if (type == 1)
                {
                    return View("Default", viewModels);
                }
                else
                {
                    return View("Type2", viewModels);
                }
        }
    }
}
