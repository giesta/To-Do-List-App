using AutoMapper;
using ToDoList.Business.Models;
using ToDoList.Data.Models.ToDoList;
using ToDoList.Web.ViewModel.ToDoList;

namespace ToDoList.Web.Profiles.ToDoList
{
    public class TagViewModelProfile:Profile
    {
        public TagViewModelProfile()
        {
            CreateMap<Tag, TagViewModel>().ReverseMap();
            CreateMap<TagDao, Tag>().ReverseMap();
        }
    }
}
