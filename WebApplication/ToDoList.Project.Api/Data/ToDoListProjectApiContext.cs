using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Project.Api.Model;

namespace ToDoList.Project.Api.Data
{
    public class ToDoListProjectApiContext : DbContext
    {
        public ToDoListProjectApiContext (DbContextOptions<ToDoListProjectApiContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoList.Project.Api.Model.Client> Client { get; set; }
    }
}
