using AutoMapper;
using ToDoList.Business.Models;
using ToDoList.Data.Models.ToDoList;
using ToDoList.Business.Profiles;

namespace ToDoList.Business.Profiles
{
    public class ToDoItemProfile:Profile
    {
        public ToDoItemProfile()
        {
            CreateMap<ToDoItemDao, ToDoItem>().ReverseMap();
        }
    }
}
