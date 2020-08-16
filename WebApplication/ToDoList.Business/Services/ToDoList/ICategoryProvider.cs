using System.Collections.Generic;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public interface ICategoryProvider
    {
        List<CategoryDao> GetAll();
        CategoryDao Get(int id);
        void Add(CategoryDao categoryDao);
        int GetIndexToInsert();
        void Remove(CategoryDao categoryDao);
        void Update(CategoryDao categoryDao);
    }
}
