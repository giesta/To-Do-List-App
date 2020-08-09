using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Models;
using ToDoList.Data.Data;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public class CategoryEntityProvider : IProviderAsync<Category> 
    {
        private readonly WebApplicationContext context;
        private readonly IMapper mapper;

        public CategoryEntityProvider(WebApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task AddAsync(Category category)
        {
            context.Add(mapper.Map<CategoryDao>(category));
            await context.SaveChangesAsync();
        }

        public async Task<Category> GetAsync(int id)
        {
            return mapper.Map<Category>(await context.Category.FirstOrDefaultAsync(m => m.Id == id));
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return mapper.Map<List<CategoryDao>, List<Category>>(await context.Category.ToListAsync());
        }

        public async Task RemoveAsync(Category category)
        {
            context.Category.Remove(mapper.Map<CategoryDao>(category));
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            context.Update(mapper.Map<CategoryDao>(category));
            await context.SaveChangesAsync();
        }
    }
}
