using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class TagToDoItem
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public int ToDoItemId { get; set; }
        public ToDoItem ToDoItem { get; set; }
    }
}
