using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Services.ToDoList;

namespace WebApplication.Models
{
    public class Tag : IHasId
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
