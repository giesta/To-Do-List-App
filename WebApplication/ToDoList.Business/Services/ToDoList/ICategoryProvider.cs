using System.Collections.Generic;
using ToDoList.Business.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public interface ICategoryProvider
    {
        List<CategoryDao> GetAll();
        CategoryDao Get(int id);
        void Add(CategoryDao category);
        int GetIndexToInsert();
        void Remove(CategoryDao category);
        void Update(CategoryDao category);
    }
}
