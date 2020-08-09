using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Web.ViewModel.ToDoList
{
    public class ToDoItemViewModelProfile:Profile
    {
        public ToDoItemViewModelProfile()
        {
            CreateMap<ToDoItemDao, ToDoItemViewModel>();
        }
    }
}
