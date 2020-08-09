using System;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Web.ViewModel.ToDoList
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

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
