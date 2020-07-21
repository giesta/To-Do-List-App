using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services.ToDoList
{
    public interface IToDoItemProvider
    {
        List<ToDoItem> GetAll();
        ToDoItem Get(int id);
        void Add(ToDoItem toDoItem);
        int GetIndexToInsert();
        void Remove(ToDoItem toDoItem);
    }
}
