using System.Collections.Generic;
using ToDoList.Business.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public interface ICategoryProvider
    {
        List<Category> GetAll();
        Category Get(int id);
        void Add(Category category);
        int GetIndexToInsert();
        void Remove(Category category);
        void Update(Category category);
    }
}
