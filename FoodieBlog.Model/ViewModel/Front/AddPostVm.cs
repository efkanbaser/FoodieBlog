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
        public List<string> Directions { get; set; } = new List<string>();

        public List<string> Ingredients { get; set; } = new List<string>();

        public List<string> TagChosen { get; set; } = new List<string>(); 
        public List<string> CategorieChosen { get; set; } = new List<string>(); 
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Tags { get; set; }


    }
}
