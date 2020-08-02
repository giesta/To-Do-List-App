using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services.ToDoList
{
    public interface IProvider<TValue>
    {
        List<TValue> GetAll();
        TValue Get(int id);
        void Add(TValue type);
        void Remove(TValue type);
        void Update(TValue type);
    }
}
