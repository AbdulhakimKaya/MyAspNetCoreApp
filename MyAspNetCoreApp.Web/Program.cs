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

//builder.Services.AddScoped<Helper>(); // interface kullan�lmak istenmedi�i durumlar i�in

//builder.Services.AddTransient<IHelper, Helper>(sp =>
//{
//    return new Helper(true);
//}); // Helper da bool de�er alan ctor i�in yap�ld�

builder.Services.AddTransient<IHelper, Helper>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// filter lar request gelince devreye giriyorlar. Scoped, request response a d�n��ene kadar hep ayn� nesne �rne�ini verdi�i i�in kullan�l�r.
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

// a�a��daki Conventional Routing yap�s� art�k pek kullan�lan bir y�ntem de�il

// blog/abc -> blogs controller -> article action method �al��s�n
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




//tamamiyle attribute routing yapmak i�in kapat�ld�

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
