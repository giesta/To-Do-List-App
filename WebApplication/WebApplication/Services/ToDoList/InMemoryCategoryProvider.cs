using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services.ToDoList
{
    public class InMemoryCategoryProvider : IInMemoryCategoryProvider
    {
        static private List<Category> dataPile = new List<Category>();
        public void Add(Category category)
        {
            dataPile.Add(category);
        }

        public Category Get(int id)
        {
            return dataPile[id];
        }

        public List<Category> GetAll()
        {
            return dataPile;
        }

        public int GetIndexToInsert()
        {
            return dataPile.Count();
        }

        public void Remove(Category category)
        {
            dataPile.Remove(category);
        }
    }
}
