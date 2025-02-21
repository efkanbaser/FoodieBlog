using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Front
{
    public class CreateRecipeVm
    {
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Tags { get; set; }
    }
}
