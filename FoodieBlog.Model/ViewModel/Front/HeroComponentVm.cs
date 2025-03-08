using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Front
{
    public class HeroComponentVm
    {
        public string MainImage { get; set; }

        public string SecondaryImage { get; set; }
        public int PublicationDay { get; set; }
        public string PublicationMonth { get; set; }

        public List<string> Categories { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public int? ReadingTime { get; set; }
        public int? Comments { get; set; }
    }
}
