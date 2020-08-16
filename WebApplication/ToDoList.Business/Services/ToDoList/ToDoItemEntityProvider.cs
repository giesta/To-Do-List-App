using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Models;
using ToDoList.Data.Data;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public class ToDoItemEntityProvider : IProviderAsync<ToDoItem>
    {
        private readonly WebApplicationContext context;
        private readonly IMapper mapper;
        public ToDoItemEntityProvider(WebApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task AddAsync(ToDoItem toDoItem)
        {
            toDoItem.CreationDate = DateTime.UtcNow;
            context.Add(mapper.Map<ToDoItemDao>(toDoItem));
            await context.SaveChangesAsync();
        }

        public async Task<ToDoItem> GetAsync(int id)
        {
            return mapper.Map<ToDoItem>(await context.ToDoItem.Include(t => t.Category).FirstOrDefaultAsync(m => m.Id == id));
        }

        public async Task<List<ToDoItem>> GetAllAsync()
        {
            return mapper.Map <List<ToDoItemDao>, List<ToDoItem>> (await context.ToDoItem.Include(t => t.Category).ToListAsync());
        }

        public async Task RemoveAsync(ToDoItem toDoItem)
        {
            context.ToDoItem.Remove(mapper.Map < ToDoItemDao > (toDoItem));
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ToDoItem toDoItem)
        {
            context.Update(mapper.Map < ToDoItemDao > (toDoItem));
           await context.SaveChangesAsync();
        }
    }
}
