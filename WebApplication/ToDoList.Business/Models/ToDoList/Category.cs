using System;
using ToDoList.Business.Services.ToDoList;

namespace ToDoList.Business.Models.ToDoList
{
    public class Category : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }

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
