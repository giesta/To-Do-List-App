using ToDoList.Business.Models.ToDoList;

namespace ToDoList.Web.ViewModel.ToDoList
{
    public class TagToDoItemViewModel
    {
        public int TagId { get; set; }
        public TagDao TagDao { get; set; }

        public int ToDoItemId { get; set; }
        public ToDoItemViewModel ToDoItemDao { get; set; }
    }
}
