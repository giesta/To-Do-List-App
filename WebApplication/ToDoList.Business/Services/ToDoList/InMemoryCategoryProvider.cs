﻿using System.Collections.Generic;
using ToDoList.Business.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public class InMemoryCategoryProvider : ICategoryProvider
    {
        static private List<CategoryDao> dataPile = new List<CategoryDao>();
        public void Add(CategoryDao category)
        {
            category.Id = GetUniqueId();
            dataPile.Add(category);
        }

        public CategoryDao Get(int id)
        {
            return dataPile[id];
        }

        public List<CategoryDao> GetAll()
        {
            return dataPile;
        }

        public int GetIndexToInsert()
        {
            return GetUniqueId();
        }

        public void Remove(CategoryDao category)
        {
            dataPile.Remove(category);
        }

        public void Update(CategoryDao category)
        {
            dataPile.Remove(category);
            dataPile.Add(category);
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
                foreach (CategoryDao category in dataPile)
                {
                    if (index == category.Id)
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
