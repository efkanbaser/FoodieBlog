using FoodieBlog.Business;
using FoodieBlog.Data;
using FoodieBlog.Data.Concrete.EntityFramework.Context;
using FoodieBlog.Model.Entity;
using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.EntityFrameworkCore;

namespace FoodieBlog.MVCCoreUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Context
            builder.Services.AddDbContext<FoodBlogDbContext>(); 

            // Repository pattern methods
            builder.Services.AddBusinessService();
            builder.Services.AddDataService();

            // Session
            builder.Services.AddSession();
            builder.Services.AddScoped<ISessionManager, SessionManager>();

            // HttpContextAccessor
            builder.Services.AddHttpContextAccessor();





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Admin Panel
            app.MapAreaControllerRoute(
                name: "area",
                areaName: "AdminPanel",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            // Main Website
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
