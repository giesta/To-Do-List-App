using System;
using System.Collections.Generic;
using ToDoList.Business.Services.ToDoList;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Business.Models
{
    public class Tag:IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<TagToDoItem> TagToDoItems { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Category category &&
                   Id == category.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
