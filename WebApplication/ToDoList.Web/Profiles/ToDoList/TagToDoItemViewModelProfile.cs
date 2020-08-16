using AutoMapper;
using ToDoList.Business.Models;
using ToDoList.Data.Models.ToDoList;
using ToDoList.Web.ViewModel.ToDoList;

namespace ToDoList.Web.Profiles.ToDoList
{
    public class TagToDoItemViewModelProfile:Profile
    {
        public TagToDoItemViewModelProfile()
        {
            CreateMap<TagToDoItem, TagToDoItemViewModel>().ReverseMap();
            CreateMap<TagToDoItemDao, TagToDoItem>().ReverseMap();
        }
    }
}
