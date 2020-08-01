using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services.ToDoList
{
    public interface IGenericProvider<TypeOfValue>
    {
        List<TypeOfValue> GetAll();
        TypeOfValue Get(int id);
        void Add(TypeOfValue type);
        void Remove(TypeOfValue type);
        void Update(TypeOfValue type);
    }
}
