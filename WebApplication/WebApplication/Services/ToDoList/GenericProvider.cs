using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Services.ToDoList
{
    public class GenericProvider<TypeOfValue> : IGenericProvider<TypeOfValue> where TypeOfValue : IHasId
    {
        static private List<TypeOfValue> dataPile = new List<TypeOfValue>();
        static private int counter = 0;
        public GenericProvider(List<TypeOfValue> initialData)
        {
            dataPile = initialData;
        }

        public GenericProvider() : this(new List<TypeOfValue>())
        {
        }
        public virtual void Add(TypeOfValue type)
        {
            type.Id = counter;
            dataPile.Add(type);
            counter++;
        }

        public TypeOfValue Get(int id)
        {
            return dataPile[id];
        }

        public List<TypeOfValue> GetAll()
        {
            return dataPile;
        }


        public virtual void Remove(TypeOfValue type)
        {
            dataPile.Remove(type);
        }

        public virtual void Update(TypeOfValue type)
        {
            dataPile.Remove(type);
            dataPile.Add(type);
        }

        /// <summary>
        /// Ensuring that ID attributes are unique
        /// </summary>
        /// <returns>Returns ID</returns>
        private int FindId()
        {
            int index = counter;
            counter++;
            return index;
        }
    }
}
