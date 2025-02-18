using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodieBlog.Data.Abstract;
using FoodieBlog.Data.Concrete;
using FoodieBlog.Data.Concrete.EntityFramework.Repository;
using FoodieBlog.Model.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace FoodieBlog.Data
{
    public static class DataService
    {
        public static IServiceCollection AddDataService(this IServiceCollection services)
        {
            services.AddScoped<ICommentRepository, EfCommentRepository>();
            services.AddScoped<IInteractionRepository, EfInteractionRepository>();
            services.AddScoped<IPostRepository, EfPostRepository>();
            services.AddScoped<IPostTagRepository, EfPostTagRepository>();
            services.AddScoped<ITagRepository, EfTagRepository>();
            services.AddScoped<IUserRepository, EfUserRepository>();
            services.AddScoped<IAdminMenuRepository, EfAdminMenuRepository>();
            services.AddScoped<IMenuAuthorizationRepository, EfMenuAuthorizationRepository>();
            services.AddScoped<IRoleRepository, EfRoleRepository>();
            services.AddScoped<IUserRoleRepository, EfUserRoleRepository>();
            services.AddScoped<IPostCategoryRepository, EfPostCategoryRepository>();
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();
            services.AddScoped<IPostDirectionRepository, EfPostDirectionRepository>();
            services.AddScoped<IPostIngredientRepository, EfPostIngredientRepositorry>();

            return services;
        }
    }
}
