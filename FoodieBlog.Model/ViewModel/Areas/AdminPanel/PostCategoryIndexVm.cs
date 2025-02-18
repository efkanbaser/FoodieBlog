using FoodieBlog.Model.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Areas.AdminPanel
{
    public class PostCategoryIndexVm
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public int? CategoryId { get; set; }

        public int? PostId { get; set; }

        public string? CategoryName { get; set; }
        public string? PostName { get; set; }

        public List<string> CategoryNames { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }

        public virtual Category Category { get; set; }

        public virtual Post Post { get; set; }
    }
}
