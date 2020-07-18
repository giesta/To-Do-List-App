using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services.ToDoList
{
    public class InMemoryToDoItemProvider : IInMemoryToDoItemProvider
    {

        static private List<ToDoItem> dataPile = new List<ToDoItem>();
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

        public int GetIndexToInsert()
        {
            return dataPile.Count();
        }

        public void Remove(ToDoItem toDoItem)
        {
            dataPile.Remove(toDoItem);
        }
    }
}
