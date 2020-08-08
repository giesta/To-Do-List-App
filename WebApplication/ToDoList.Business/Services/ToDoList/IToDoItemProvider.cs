using System.Collections.Generic;
using ToDoList.Business.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public interface IToDoItemProvider
    {
        List<ToDoItem> GetAll();
        ToDoItem Get(int id);
        void Add(ToDoItem toDoItem);
        int GetIndexToInsert();
        void Remove(ToDoItem toDoItem);
        void Update(ToDoItem toDoItem);
    }
}
