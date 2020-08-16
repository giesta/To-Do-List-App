using System.Collections.Generic;
using AutoMapper;
using ToDoList.Business.Models;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Business.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            //CreateMap<CategoryDao, Category>().ReverseMap();
        }
    }
}
