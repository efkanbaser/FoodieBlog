using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Front
{
    public class PostCommentVm
    {
        public string UserName { get; set; }
        public DateTime? Date { get; set; }
        public string Contents { get; set; }
        public string ProfilePic { get; set; }
    }
}
