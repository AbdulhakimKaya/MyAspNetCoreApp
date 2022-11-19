using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyAspNetCoreApp.Web.TagHelpers
{
    [HtmlTargetElement("thumbnail")] // kullanılarak component ismi değiştirilebilir
    public class ImageThumbnailTagHelper:TagHelper
    {
        public string ImageSrc { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // <img src=""/>
            output.TagName = "img";

            string fileName = ImageSrc.Split(".")[0];
            string fileExtensions = Path.GetExtension(ImageSrc); // .jpg - .png

            output.Attributes.SetAttribute("src", $"{fileName}-100x100{fileExtensions}");
        }
    }
}
