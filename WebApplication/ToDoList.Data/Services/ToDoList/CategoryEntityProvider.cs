using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Data;
using ToDoList.Business.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public class CategoryEntityProvider : IProviderAsync<CategoryDao> 
    {
        private readonly WebApplicationContext context;

        public CategoryEntityProvider(WebApplicationContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(CategoryDao categoryDao)
        {
            context.Add(categoryDao);
            await context.SaveChangesAsync();
        }

        public async Task<CategoryDao> GetAsync(int id)
        {
            return await context.Category.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<CategoryDao>> GetAllAsync()
        {
            return await context.Category.ToListAsync();
        }

        public async Task RemoveAsync(CategoryDao categoryDao)
        {
            context.Category.Remove(categoryDao);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryDao categoryDao)
        {
            context.Update(categoryDao);
            await context.SaveChangesAsync();
        }
    }
}
