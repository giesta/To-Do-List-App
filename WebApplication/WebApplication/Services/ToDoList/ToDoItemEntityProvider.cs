using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Services.ToDoList
{
    public class ToDoItemEntityProvider : IGenericProviderAsync<ToDoItem>
    {
        private readonly WebApplicationContext context;
        public ToDoItemEntityProvider(WebApplicationContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(ToDoItem toDoItem)
        {
            toDoItem.CreationDate = DateTime.UtcNow;
            context.Add(toDoItem);
            await context.SaveChangesAsync();
        }

        public async Task<ToDoItem> GetAsync(int id)
        {
            return await context.ToDoItem.Include(t => t.Category).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<ToDoItem>> GetAllAsync()
        {
            return await context.ToDoItem.Include(t => t.Category).ToListAsync();
        }

        public async Task RemoveAsync(ToDoItem toDoItem)
        {
            context.ToDoItem.Remove(toDoItem);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ToDoItem toDoItem)
        {
            context.Update(toDoItem);
           await context.SaveChangesAsync();
        }
    }
}
