using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyAspNetCoreApp.Web.Filters;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "SqlCon"));
});

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

//builder.Services.AddSingleton<IHelper, Helper>();

//builder.Services.AddScoped<IHelper, Helper>();

//builder.Services.AddScoped<Helper>(); // interface kullanýlmak istenmediði durumlar için

//builder.Services.AddTransient<IHelper, Helper>(sp =>
//{
//    return new Helper(true);
//}); // Helper da bool deðer alan ctor için yapýldý

builder.Services.AddTransient<IHelper, Helper>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// filter lar request gelince devreye giriyorlar. Scoped, request response a dönüþene kadar hep ayný nesne örneðini verdiði için kullanýlýr.
builder.Services.AddScoped<NotFoundFilter>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// aþaðýdaki Conventional Routing yapýsý artýk pek kullanýlan bir yöntem deðil

// blog/abc -> blogs controller -> article action method çalýþsýn
//app.MapControllerRoute(
//    name: "blogs",
//    pattern: "blog/{*article}",
//    defaults: new {controller="Blogs", action="Article"});

//app.MapControllerRoute(
//    name: "article",
//    pattern: "{controller=Blogs}/{action=Article}/{name}/{id}");

//app.MapControllerRoute(
//    name: "pages",
//    pattern: "{controller}/{action}/{page}/{pagesize}");

//app.MapControllerRoute(
//    name: "getbyid",
//    pattern: "{controller}/{action}/{productid}");




//tamamiyle attribute routing yapmak için kapatýldý

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
