using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Services.ToDoList
{
    public class ToDoItemEntityProvider : IGenericProvider<ToDoItem>
    {
        private readonly WebApplicationContext context;
        public ToDoItemEntityProvider(WebApplicationContext context) : base()
        {
            this.context = context;
        }
        public void Add(ToDoItem toDoItem)
        {
            toDoItem.CreationDate = DateTime.UtcNow;
            context.Add(toDoItem);
            context.SaveChanges();
        }

        public ToDoItem Get(int id)
        {
            ToDoItem toDoItem = context.ToDoItem
                .FirstOrDefault(m => m.Id == id);
            return toDoItem;
        }

        public List<ToDoItem> GetAll()
        {
            return context.ToDoItem.ToList();
        }

        public void Remove(ToDoItem toDoItem)
        {
            context.ToDoItem.Remove(toDoItem);
            context.SaveChanges();
        }

        public void Update(ToDoItem toDoItem)
        {
            context.Update(toDoItem);
            context.SaveChanges();
        }
    }
}
