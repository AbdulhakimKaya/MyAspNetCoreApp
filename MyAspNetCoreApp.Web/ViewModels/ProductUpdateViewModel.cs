using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Web.ViewModels
{
    public class ProductUpdateViewModel
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50")]
        [Required]
        public string Name { get; set; }
        //[RegularExpression(@"^[0-9]+(\.[0-9]{1,2})", ErrorMessage = "there should be no more than two digits after the dot in the price field")]
        [Required]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Stock field can not be empty.")]
        [Range(1,200, ErrorMessage = "stock field must be a value between 1-200.")]
        public int Stock { get; set; }
        [Required]
        public string? Color { get; set; }
        public bool IsPublish { get; set; }
        [Required]
        public DateTime? PublishDate { get; set; }
        [Required]
        public int Expire { get; set; }
        [StringLength(500, MinimumLength = 50,ErrorMessage = "Max length is 500, Min length is 50")]
        [Required]
        public string Description { get; set; }
        //[EmailAddress(ErrorMessage = "Email format is not suitable")]
        //public string EmailAddress { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; }
        [ValidateNever]
        public string ImagePath { get; set; }
    }
}
