namespace ToDoList.Business.Models.ToDoList
{
    public class TagToDoItem
    {
        public int TagId { get; set; }
        public TagDao TagDao { get; set; }

        public int ToDoItemId { get; set; }
        public ToDoItem ToDoItem { get; set; }
    }
}
