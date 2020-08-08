using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Web.Data;
using ToDoList.Web.Models;

namespace ToDoList.Web.Services.ToDoList
{
    public class CategoryEntityProvider : IProviderAsync<Category> 
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
