﻿namespace ToDoList.Web.Models
{
    public class TagToDoItem
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int ToDoItemId { get; set; }
        public ToDoItem ToDoItem { get; set; }
    }
}