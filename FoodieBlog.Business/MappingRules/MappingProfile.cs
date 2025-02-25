using AutoMapper;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Front;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Business.MappingRules
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SignUpVm, User>().ReverseMap();
            CreateMap<AddPostVm, Post>().ReverseMap();
        }
    }
}
