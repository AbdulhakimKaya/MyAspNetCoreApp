using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Helpers
{
    public class Helper:IHelper
    {
        //private bool _isConfiguration;
        //public Helper(bool isConfiguration)
        //{
        //    _isConfiguration = isConfiguration;
        //}

        private readonly AppDbContext _context;

        public Helper(AppDbContext context)
        {
            _context = context;
        }
        public string Upper(string text)
        {
            _context.Products.ToList();

            return text.ToUpper();
        }
    }
}
