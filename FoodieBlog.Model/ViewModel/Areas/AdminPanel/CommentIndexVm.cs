using FoodieBlog.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Areas.AdminPanel
{
    public class CommentIndexVm
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public int? PostId { get; set; }

        public int? UserId { get; set; }

        public string Contents { get; set; }

        public string PostTitle { get; set; }
        public string UserName { get; set; }
        public DateTime? PublicationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Post Post { get; set; }

        public virtual User User { get; set; }
    }
}
