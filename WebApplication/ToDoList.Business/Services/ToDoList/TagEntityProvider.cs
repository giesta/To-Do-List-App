using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Data;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public class TagEntityProvider : IProviderAsync<TagDao>
    {
        private readonly WebApplicationContext context;
        public TagEntityProvider(WebApplicationContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(TagDao tagDao)
        {            
            context.Add(tagDao);
            await context.SaveChangesAsync();
        }

        public async Task<List<TagDao>> GetAllAsync()
        {
            return await context.Tag.Include(t => t.TagToDoItems).ThenInclude(t => t.ToDoItemDao).AsNoTracking().ToListAsync();
        }

        public async Task<TagDao> GetAsync(int id)
        {
            return await context.Tag.Include(t => t.TagToDoItems).ThenInclude(t=>t.ToDoItemDao).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task RemoveAsync(TagDao tagDao)
        {
            context.Remove(tagDao);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TagDao tagDao)
        {
           
            List<TagToDoItemDao> tagToDoItems = context.TagToDoItem.Where(t=>t.TagId==tagDao.Id).ToList();
            if(tagToDoItems!= null)
            {
                foreach(TagToDoItemDao tagToDoItem in tagToDoItems)
                 {
                    context.Remove(tagToDoItem);
                 }
            }            
            context.Update(tagDao);
            await context.SaveChangesAsync();
        }
    }
}
