using ToDoList.Business.Models;

namespace ToDoList.Business.Models
{
    public class TagToDoItem
    {
        public int TagId { get; set; }
        public Tag TagDao { get; set; }

        public int ToDoItemId { get; set; }
        public ToDoItem ToDoItem { get; set; }
    }
}
