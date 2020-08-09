using System.Collections.Generic;

namespace ToDoList.Business.Services.ToDoList
{
    public class Provider<TValue> : IProvider<TValue> where TValue : IHasId
    {
        static private List<TValue> dataPile = new List<TValue>();
        static private int counter = 0;
        public Provider(List<TValue> initialData)
        {
            dataPile = initialData;
        }

        public Provider() : this(new List<TValue>())
        {
        }
        public virtual void Add(TValue type)
        {
            type.Id = counter;
            dataPile.Add(type);
            counter++;
        }

        public TValue Get(int id)
        {
            return dataPile[id];
        }

        public List<TValue> GetAll()
        {
            return dataPile;
        }


        public virtual void Remove(TValue type)
        {
            dataPile.Remove(type);
        }

        public virtual void Update(TValue type)
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
