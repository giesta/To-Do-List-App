using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services.ToDoList
{
    public interface ICategoryProvider
    {
        List<Category> GetAll();
        Category Get(int id);
        void Add(Category category);
        int GetIndexToInsert();
        void Remove(Category category);
    }
}
