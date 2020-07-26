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
        public void Add(TypeOfValue type)
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


        public void Remove(TypeOfValue type)
        {
            dataPile.Remove(type);
        }

        public void Update(TypeOfValue type)
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
