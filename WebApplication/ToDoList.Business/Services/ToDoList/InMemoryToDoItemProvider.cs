using System.Collections.Generic;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public class InMemoryToDoItemProvider : IToDoItemProvider
    {

        static private List<ToDoItemDao> dataPile = new List<ToDoItemDao>();
        public void Add(ToDoItemDao toDoItemDao)
        {
            toDoItemDao.Id = GetUniqueId();
            dataPile.Add(toDoItemDao);
        }

        public ToDoItemDao Get(int id)
        {
            return dataPile[id];
        }

        public List<ToDoItemDao> GetAll()
        {
            return dataPile;
        }

        public int GetIndexToInsert()
        {
            return GetUniqueId();
        }

        public void Remove(ToDoItemDao toDoItemDao)
        {
            dataPile.Remove(toDoItemDao);
        }

        public void Update(ToDoItemDao toDoItemDao)
        {
            dataPile.Remove(toDoItemDao);
            dataPile.Add(toDoItemDao);
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
                foreach (ToDoItemDao toDoItem in dataPile)
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
