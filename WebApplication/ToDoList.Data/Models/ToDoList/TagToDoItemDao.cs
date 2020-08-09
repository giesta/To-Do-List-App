namespace ToDoList.Business.Models.ToDoList
{
    public class TagToDoItemDao
    {
        public int TagId { get; set; }
        public TagDao TagDao { get; set; }

        public int ToDoItemId { get; set; }
        public ToDoItemDao ToDoItemDao { get; set; }
    }
}
