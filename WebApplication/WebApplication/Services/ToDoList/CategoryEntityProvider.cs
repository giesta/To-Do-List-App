using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Services.ToDoList
{
    public class CategoryEntityProvider : IGenericProviderAsync<Category> 
    {
        private readonly WebApplicationContext context;

        public CategoryEntityProvider(WebApplicationContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Category category)
        {
            context.Add(category);
            await context.SaveChangesAsync();
        }

        public async Task<Category> GetAsync(int id)
        {
            return await context.Category.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await context.Category.ToListAsync();
        }

        public async Task RemoveAsync(Category category)
        {
            context.Category.Remove(category);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            context.Update(category);
            await context.SaveChangesAsync();
        }
    }
}
