using FoodieBlog.Model.Entity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Front
{
    public class PostIndexVm
    {
        public int Id { get; set; }
        public string Title { get; set; } // Done

        public string Contents { get; set; } // Done

        public string MainImage { get; set; } // Done
        public int? ReadingTime { get; set; } // Done
        public string SecondaryImage { get; set; } // Done
        public int? PrepTime { get; set; } // Done
        public int? CookTime { get; set; } // Done
        public string ServingSize { get; set; } // Done
        public List<string> Ingredients { get; set; } // Done
        public List<string> Directions { get; set; } // Done
        public string UserName { get; set; } // Done
        public int UserId { get; set; }

        public string PublicationMonth { get; set; } // Done
        public int PublicationDay { get; set; } // Done

        public string MiddleText { get; set; } // Done
        public string DescriptionFirst { get; set; } // Done
        public string DescriptionHeader { get; set; } // Done
        public string DescriptionLast { get; set; } // Done
        public string MoreDetails { get; set; } // Done
        public string LastText { get; set; } // Done

        public string Quote { get; set; } // Done


        #region previous post
        [BindNever]
        public string PreviousPostUrl { get; set; }
        [BindNever]
        public string PrevTitle { get; set; } // Done
        [BindNever]
        public string PrevPublicationMonth { get; set; } // Done
        [BindNever]
        public int PrevPublicationDay { get; set; } // Done
        #endregion

        #region next post
        [BindNever]
        public string NextPostUrl { get; set; }
        [BindNever]
        public string NextTitle { get; set; } // Done
        [BindNever]
        public string NextPublicationMonth { get; set; } // Done
        [BindNever]
        public int NextPublicationDay { get; set; } // Done
        #endregion





        [BindNever]
        public string UserPic { get; set; } // Done
        [BindNever]
        public string UserBio { get; set; } // Done

        [BindNever]
        public virtual ICollection<Comment> Comments { get; set; }
        [BindNever]
        public virtual List<string> PostCategories { get; set; }
        [BindNever]
        public virtual List<string> PostTags { get; set; }
        [BindNever]
        public virtual User User { get; set; } // Done

    }
}
