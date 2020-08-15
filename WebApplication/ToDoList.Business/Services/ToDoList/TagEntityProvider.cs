using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Models;
using ToDoList.Data.Data;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public class TagEntityProvider : IProviderAsync<Tag>
    {
        private readonly WebApplicationContext context;
        private readonly IMapper mapper;
        public TagEntityProvider(WebApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task AddAsync(Tag tag)
        {            
            context.Add(mapper.Map < TagDao > (tag));
            await context.SaveChangesAsync();
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return mapper.Map<List<TagDao>, List<Tag>>(await context.Tag.Include(t => t.TagToDoItems).ThenInclude(t => t.ToDoItemDao).AsNoTracking().ToListAsync());
        }

        public async Task<Tag> GetAsync(int id)
        {
            return mapper.Map< Tag > (source: await context.Tag.Include(t => t.TagToDoItems).ThenInclude(t=>t.ToDoItemDao).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id));
        }

        public async Task RemoveAsync(Tag tag)
        {
            context.Remove(mapper.Map<TagDao>(tag));
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tag tag)
        {

            List<TagToDoItem> tagToDoItems = mapper.Map<List<TagToDoItem>>(context.TagToDoItem.Where(t => t.TagId == tag.Id).ToList());
            if (tagToDoItems != null)
            {
                foreach (TagToDoItem tagToDoItem in tagToDoItems)
                {
                    context.Remove(mapper.Map<TagToDoItemDao>(tagToDoItem));
                }
            }
            context.Update(mapper.Map<TagDao>(tag));
            await context.SaveChangesAsync();
        }
    }
}
