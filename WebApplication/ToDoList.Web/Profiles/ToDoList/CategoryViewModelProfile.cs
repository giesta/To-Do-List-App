using System;
using AutoMapper;
using ToDoList.Business.Models.ToDoList;

namespace ToDoList.Web.ViewModel.ToDoList
{
    public class CategoryViewModelProfile:Profile
    {
        public CategoryViewModelProfile()
        {
            CreateMap<CategoryDao, CategoryViewModel>();
        }
    }
}
