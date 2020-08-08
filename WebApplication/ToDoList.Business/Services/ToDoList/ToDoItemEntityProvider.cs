using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Data;
using ToDoList.Business.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public class ToDoItemEntityProvider : IProviderAsync<ToDoItemDao>
    {
        private readonly WebApplicationContext context;
        public ToDoItemEntityProvider(WebApplicationContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(ToDoItemDao toDoItemDao)
        {
            toDoItemDao.CreationDate = DateTime.UtcNow;
            context.Add(toDoItemDao);
            await context.SaveChangesAsync();
        }

        public async Task<ToDoItemDao> GetAsync(int id)
        {
            return await context.ToDoItem.Include(t => t.Category).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<ToDoItemDao>> GetAllAsync()
        {
            return await context.ToDoItem.Include(t => t.Category).ToListAsync();
        }

        public async Task RemoveAsync(ToDoItemDao toDoItemDao)
        {
            context.ToDoItem.Remove(toDoItemDao);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ToDoItemDao toDoItemDao)
        {
            context.Update(toDoItemDao);
           await context.SaveChangesAsync();
        }
    }
}
