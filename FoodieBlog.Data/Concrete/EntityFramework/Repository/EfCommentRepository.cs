using FoodieBlog.Data.Abstract;
using FoodieBlog.Model.Entity;
using Infrastructure.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Data.Concrete.EntityFramework.Repository
{
    public class EfCommentRepository:EfRepositoryBase<Comment,FoodBlogDbContext>,ICommentRepository
    {
    }
}
