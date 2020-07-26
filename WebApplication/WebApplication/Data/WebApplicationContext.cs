using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class WebApplicationContext : DbContext
    {
        public WebApplicationContext (DbContextOptions<WebApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplication.Models.Category> Category { get; set; }
        public object TypeOfValue { get; internal set; }
      
        public DbSet<WebApplication.Models.ToDoItem> ToDoItem { get; set; }
    }
}
