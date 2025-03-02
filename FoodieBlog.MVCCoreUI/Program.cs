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
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Collections.ObjectModel;
using System.Data;
using FoodieBlog.MVCCoreUI.Middlewares;
using static System.Net.Mime.MediaTypeNames;

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
            builder.Services.AddScoped<IValidator<AddPostVm>, AddPostValidator>();

            #region Logging
            // Logging
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            // this MinimumLevel needs to overlap with the one in the middleware
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.MSSqlServer(
                connectionString: "server=EFKO\\SQLEXPRESS;database=FoodBlogLogsDB;trusted_connection=true;TrustServerCertificate=True",
                sinkOptions: new MSSqlServerSinkOptions {
                    TableName = "WebsiteLogs",
                    AutoCreateSqlTable = true,
                    BatchPostingLimit = 1000,
                    BatchPeriod = TimeSpan.FromSeconds(5)
                },
                columnOptions: new ColumnOptions
                {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn("RequestPath", SqlDbType.NVarChar, true, 512),
                    new SqlColumn("RequestBody", SqlDbType.NVarChar, true, -1),
                    new SqlColumn("ResponseBody", SqlDbType.NVarChar, true, -1),
                    new SqlColumn("StatusCode", SqlDbType.Int),
                    new SqlColumn("RequestMethod", SqlDbType.NVarChar, true, 10)
                },
                Store = new Collection<StandardColumn>
                {
                    StandardColumn.Message,
                    StandardColumn.Level,
                    StandardColumn.TimeStamp
                }
            })
            .CreateLogger();

            // Serilog'u kullan
            builder.Host.UseSerilog();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseSession();

            app.UseRequestResponseLogging();

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

            app.MapFallback(async context =>
            {
                // Redirect to a custom error page or homepage
                context.Response.Redirect("/Home/Error");
            });





            app.Run();
        }
    }
}
