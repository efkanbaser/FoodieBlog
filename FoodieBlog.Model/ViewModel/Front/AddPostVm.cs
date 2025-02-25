using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Front
{
    public class AddPostVm
    {

        public string MainImage { get; set; }

        public string Title { get; set; }

        public int? ReadingTime { get; set; }

        public string Contents { get; set; }

        public string SecondaryImage { get; set; }

        public string ServingSize { get; set; }

        public int? PrepTime { get; set; }

        public int? CookTime { get; set; }

        public string MiddleText { get; set; }

        public string DescriptionFirst { get; set; }

        public string MoreDetails { get; set; }

        public string LastText { get; set; }

        public string DescriptionHeader { get; set; }

        public string DescriptionLast { get; set; }

        public string Quote { get; set; }
        public string Directions { get; set; } 

        public string Ingredients { get; set; }

        public string TagChosen { get; set; } 
        public string CategorieChosen { get; set; } 
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Tags { get; set; }


    }
}
