using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Data;
using ToDoList.Business.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public class ToDoItemEntityProvider : IProviderAsync<ToDoItem>
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
