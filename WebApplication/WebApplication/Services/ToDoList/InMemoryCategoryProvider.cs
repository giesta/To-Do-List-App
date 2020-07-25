﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services.ToDoList
{
    public class InMemoryCategoryProvider : ICategoryProvider
    {
        static private List<Category> dataPile = new List<Category>();
        public void Add(Category category)
        {
            dataPile.Add(category);
        }

        public Category Get(int id)
        {
            return dataPile[id];
        }

        public List<Category> GetAll()
        {
            return dataPile;
        }

        public int GetIndexToInsert()
        {
            return GetUniqueId();
        }

        public void Remove(Category category)
        {
            dataPile.Remove(category);
        }

        public void Update(Category category)
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
                foreach (Category category in dataPile)
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
