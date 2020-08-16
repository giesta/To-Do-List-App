using System.Collections.Generic;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public interface IToDoItemProvider
    {
        List<ToDoItemDao> GetAll();
        ToDoItemDao Get(int id);
        void Add(ToDoItemDao toDoItemDao);
        int GetIndexToInsert();
        void Remove(ToDoItemDao toDoItemDao);
        void Update(ToDoItemDao toDoItemDao);
    }
}
