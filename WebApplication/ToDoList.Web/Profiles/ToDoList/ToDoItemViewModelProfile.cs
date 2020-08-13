using AutoMapper;
using ToDoList.Business.Models;
using ToDoList.Data.Models.ToDoList;
using ToDoList.Web.ViewModel.ToDoList;

namespace ToDoList.Web.Profiles.ToDoList
{
    public class ToDoItemViewModelProfile:Profile
    {
        public ToDoItemViewModelProfile()
        {
            CreateMap<ToDoItemDao, ToDoItem>().ReverseMap();
            CreateMap<ToDoItem, ToDoItemViewModel>().ReverseMap();
        }
    }
}
