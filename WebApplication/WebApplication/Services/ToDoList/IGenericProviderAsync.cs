using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services.ToDoList
{
    public interface IGenericProviderAsync<TypeOfValue>
    {
        Task<List<TypeOfValue>> GetAllAsync();
        Task <TypeOfValue> GetAsync(int id);
        Task AddAsync(TypeOfValue type);
        Task RemoveAsync(TypeOfValue type);
        Task UpdateAsync(TypeOfValue type);
    }
}
