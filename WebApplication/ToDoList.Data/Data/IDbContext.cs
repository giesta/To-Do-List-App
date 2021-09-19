using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Data.Data
{
    public interface IDbContext
    {
        IQueryable<T> Set<T>() where T : class;
        DbSet<CategoryDao> Category { get; }
        DbSet<ToDoItemDao> ToDoItem { get; }
        int SaveChanges();
    }
}
