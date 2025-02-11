using FoodieBlog.Model.Entity;
using Infrastructure.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Data.Abstract
{
    public interface IPostCategoryRepository : IRepository<PostCategory>
    {

    }
}
