using System;
using System.Collections.Generic;
using ToDoList.Business.Models;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Web.ViewModel.ToDoList
{
    public class TagViewModel
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
