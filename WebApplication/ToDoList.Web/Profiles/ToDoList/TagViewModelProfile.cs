using System;
using System.Collections.Generic;
using AutoMapper;
using ToDoList.Business.Models.ToDoList;

namespace ToDoList.Web.ViewModel.ToDoList
{
    public class TagViewModelProfile:Profile
    {
        public TagViewModelProfile()
        {
            CreateMap<TagDao, TagViewModel>();
        }
    }
}
