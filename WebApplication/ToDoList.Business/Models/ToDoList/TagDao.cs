using System;
using System.Collections.Generic;
using ToDoList.Business.Services.ToDoList;

namespace ToDoList.Business.Models.ToDoList
{
    public class TagDao : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<TagToDoItem> TagToDoItems { get; set; }

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
