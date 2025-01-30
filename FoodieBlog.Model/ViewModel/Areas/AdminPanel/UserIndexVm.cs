using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Areas.AdminPanel
{
    public class UserIndexVm
    {
        // TODO: Maybe you can show more from user
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string ProfilePic { get; set; }
        public bool? Active { get; set; }
        public DateTime? PublicationDate { get; set; } // Name this as registeration date
    }
}
