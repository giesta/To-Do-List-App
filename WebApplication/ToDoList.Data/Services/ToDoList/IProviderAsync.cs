using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.Business.Services.ToDoList
{
    public interface IProviderAsync<TValue>
    {
        Task<List<TValue>> GetAllAsync();
        Task <TValue> GetAsync(int id);
        Task AddAsync(TValue type);
        Task RemoveAsync(TValue type);
        Task UpdateAsync(TValue type);
    }
}
