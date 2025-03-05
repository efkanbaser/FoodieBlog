using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FoodieBlog.Business.Abstract;
using FoodieBlog.Business.Concrete.Base;
using FoodieBlog.Business.Common;



namespace FoodieBlog.Business
{
    public static class BusinessService
    {
        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<ICommentBs, CommentBs>();
            services.AddScoped<IInteractionBs, InteractionBs>();
            services.AddScoped<IPostBs, PostBs>();
            services.AddScoped<IPostTagBs, PostTagBs>();
            services.AddScoped<ITagBs, TagBs>();
            services.AddScoped<IUserBs, UserBs>();
            services.AddScoped<IAdminMenuBs, AdminMenuBs>();
            services.AddScoped<IMenuAuthorizationBs, MenuAuthorizationBs>();
            services.AddScoped<IRoleBs, RoleBs>();
            services.AddScoped<IUserRoleBs, UserRoleBs>();
            services.AddScoped<ICategoryBs, CategoryBs>();
            services.AddScoped<IPostCategoryBs, PostCategoryBs>();
            services.AddScoped<IPostIngredientBs, PostIngredientBs>();
            services.AddScoped<IPostDirectionBs, PostDirectionBs>();

            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
