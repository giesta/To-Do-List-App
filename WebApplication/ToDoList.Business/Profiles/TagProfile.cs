using AutoMapper;
using ToDoList.Business.Models;
using ToDoList.Data.Models.ToDoList;
using ToDoList.Business.Profiles;

namespace ToDoList.Business.Profiles
{
    public class TagProfile:Profile
    {
        public TagProfile()
        {
            CreateMap<TagDao, Tag>().ReverseMap();
        }
    }
}
