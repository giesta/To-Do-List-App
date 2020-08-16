using System;
using System.Collections.Generic;

namespace ToDoList.Data.Models.ToDoList
{
    public class TagDao
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<TagToDoItemDao> TagToDoItems { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CategoryDao category &&
                   Id == category.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
