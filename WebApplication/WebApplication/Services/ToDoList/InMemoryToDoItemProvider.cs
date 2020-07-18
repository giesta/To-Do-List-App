using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services.ToDoList
{
    public class InMemoryToDoItemProvider : IInMemoryToDoItemProvider
    {

        static private List<ToDoItem> dataPile = new List<ToDoItem>()
        {
            new ToDoItem(){Id = 0, Name = "Clean Room", Description = "to clean room", Priority = 5},
            new ToDoItem(){Id = 1, Name = "Homework", Description = "to do homeworks", Priority = 1}
        };
        public void Add(ToDoItem toDoItem)
        {
            dataPile.Add(toDoItem);
        }

        public ToDoItem Get(int id)
        {
            return dataPile[id];
        }

        public List<ToDoItem> GetAll()
        {
            return dataPile;
        }
    }
}
