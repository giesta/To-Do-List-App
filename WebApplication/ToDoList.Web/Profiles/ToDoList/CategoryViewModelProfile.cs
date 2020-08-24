using AutoMapper;
using ToDoList.Business.Models;
using ToDoList.Data.Models.ToDoList;
using ToDoList.Web.ViewModel.ToDoList;

namespace ToDoList.Web.Profiles.ToDoList
{
    public class CategoryViewModelProfile:Profile
    {
        public CategoryViewModelProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            //CreateMap<CategoryDao, Category>().ReverseMap();
        }
    }
}
