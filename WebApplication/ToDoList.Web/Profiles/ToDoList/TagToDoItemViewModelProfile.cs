using AutoMapper;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Web.ViewModel.ToDoList
{
    public class TagToDoItemViewModelProfile:Profile
    {
        public TagToDoItemViewModelProfile()
        {
            CreateMap<TagToDoItemDao, TagToDoItemViewModel>();
        }
    }
}
