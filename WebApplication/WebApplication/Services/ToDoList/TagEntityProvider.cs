using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Services.ToDoList
{
    public class TagEntityProvider : IProviderAsync<Tag>
    {
        private readonly WebApplicationContext context;
        public TagEntityProvider(WebApplicationContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Tag tag)
        {            
            context.Add(tag);
            await context.SaveChangesAsync();
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await context.Tag.Include(t => t.TagToDoItems).ToListAsync();
        }

        public async Task<Tag> GetAsync(int id)
        {
            return await context.Tag.Include(t => t.TagToDoItems).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task RemoveAsync(Tag tag)
        {
            context.Tag.Remove(tag);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tag tag)
        {
            context.Update(tag);
            await context.SaveChangesAsync();
        }
    }
}
