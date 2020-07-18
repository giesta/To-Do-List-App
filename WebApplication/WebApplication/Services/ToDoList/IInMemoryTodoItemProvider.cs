using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services.ToDoList
{
    interface IInMemoryToDoItemProvider
    {
        List<ToDoItem> GetAll();
        ToDoItem Get(int id);
        void Add(ToDoItem toDoItem);
    }
}
