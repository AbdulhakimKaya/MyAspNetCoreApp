using Microsoft.EntityFrameworkCore;
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

//builder.Services.AddSingleton<IHelper, Helper>();

//builder.Services.AddScoped<IHelper, Helper>();

//builder.Services.AddScoped<Helper>(); // interface kullan�lmak istenmedi�i durumlar i�in

//builder.Services.AddTransient<IHelper, Helper>(sp =>
//{
//    return new Helper(true);
//}); // Helper da bool de�er alan ctor i�in yap�ld�

builder.Services.AddTransient<IHelper, Helper>();


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();