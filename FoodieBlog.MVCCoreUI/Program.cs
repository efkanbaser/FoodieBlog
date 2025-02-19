using FluentValidation;
using FluentValidation.AspNetCore;
using FoodieBlog.Business;
using FoodieBlog.Business.MappingRules;
using FoodieBlog.Business.ValidationRules.Front;
using FoodieBlog.Data;
using FoodieBlog.Data.Concrete.EntityFramework.Context;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Areas.AdminPanel;
using FoodieBlog.Model.ViewModel.Front;
using FoodieBlog.MVCCoreUI.Filters;
using Infrastructure.CrossCuttingConcern.Comunication;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FoodieBlog.MVCCoreUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // Context
            builder.Services.AddDbContext<FoodBlogDbContext>(); 

            // Repository pattern methods
            builder.Services.AddBusinessService();
            builder.Services.AddDataService();

            // Session
            builder.Services.AddSession();
            builder.Services.AddScoped<ISessionManager, SessionManager>();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            // HttpContextAccessor
            builder.Services.AddHttpContextAccessor();

            // Sending mail
            builder.Services.AddSingleton<MailIslemleri>();

            // Validation rules
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddScoped<IValidator<SignUpVm>, SignUpValidator>();
            builder.Services.AddScoped<IValidator<SignInVm>, SignInValidator>();

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

            // Main Website
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Admin Panel
            app.MapAreaControllerRoute(
                name: "area",
                areaName: "AdminPanel",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");





            app.Run();
        }
    }
}
