using System.Collections.Generic;

namespace ToDoList.Web.Services.ToDoList
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
