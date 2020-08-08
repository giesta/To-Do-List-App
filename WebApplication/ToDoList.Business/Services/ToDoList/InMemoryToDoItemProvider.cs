using System.Collections.Generic;
using ToDoList.Business.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public class InMemoryToDoItemProvider : IToDoItemProvider
    {

        static private List<ToDoItem> dataPile = new List<ToDoItem>();
        public void Add(ToDoItem toDoItem)
        {
            toDoItem.Id = GetUniqueId();
            dataPile.Add(toDoItem);
        }

        public ToDoItem Get(int id)
        {
            return dataPile[id];
        }

        public List<ToDoItem> GetAll()
        {
            return dataPile;
        }

        public int GetIndexToInsert()
        {
            return GetUniqueId();
        }

        public void Remove(ToDoItem toDoItem)
        {
            dataPile.Remove(toDoItem);
        }

        public void Update(ToDoItem toDoItem)
        {
            dataPile.Remove(toDoItem);
            dataPile.Add(toDoItem);
        }

        /// <summary>
        /// Get ID that is unique
        /// </summary>
        /// <returns>Returns ID</returns>
        private int GetUniqueId()
        {            
            int index = 0;
            bool find;
            for (int i = 0; i < dataPile.Count; i++)
            {
                find = true;
                foreach (ToDoItem toDoItem in dataPile)
                {
                    if (index == toDoItem.Id)
                    {
                        index++;
                        find = false;
                        break;
                    }
                }
                if (find)
                {
                    return index;
                }
            }
            return index;
        }
    }
}
