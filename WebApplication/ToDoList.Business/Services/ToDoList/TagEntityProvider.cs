using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Data;
using ToDoList.Business.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
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
            return await context.Tag.Include(t => t.TagToDoItems).ThenInclude(t => t.ToDoItem).AsNoTracking().ToListAsync();
        }

        public async Task<Tag> GetAsync(int id)
        {
            return await context.Tag.Include(t => t.TagToDoItems).ThenInclude(t=>t.ToDoItem).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task RemoveAsync(Tag tag)
        {
            context.Remove(tag);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tag tag)
        {
           
            List<TagToDoItem> tagToDoItems = context.TagToDoItem.Where(t=>t.TagId==tag.Id).ToList();
            if(tagToDoItems!= null)
            {
                foreach(TagToDoItem tagToDoItem in tagToDoItems)
                 {
                    context.Remove(tagToDoItem);
                 }
            }            
            context.Update(tag);
            await context.SaveChangesAsync();
        }
    }
}
