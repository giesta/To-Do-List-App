using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Services.ToDoList
{
    public class CategoryEntityProvider : IGenericProvider<Category> 
    {
        private readonly WebApplicationContext context;

        public CategoryEntityProvider(WebApplicationContext context) : base()
        {
            this.context = context;
        }

        public void Add(Category category)
        {
            context.Add(category);
            context.SaveChanges();
        }

        public Category Get(int id)
        {
            Category category = context.Category
                .FirstOrDefault(m => m.Id == id);
            return category;
        }

        public List<Category> GetAll()
        {
            return context.Category.ToList();
        }

        public int GetIndexToInsert()
        {
            throw new NotImplementedException();
        }

        public void Remove(Category category)
        {            
            context.Category.Remove(category);
            context.SaveChanges();
        }

        public void Update(Category category)
        {
            context.Update(category);
            context.SaveChanges();
        }
    }
}
