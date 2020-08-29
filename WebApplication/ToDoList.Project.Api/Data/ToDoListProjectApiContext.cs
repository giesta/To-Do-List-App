using Microsoft.EntityFrameworkCore;
using ToDoList.ProjectManage.Api.Model;

namespace ToDoList.ProjectManage.Api.Data
{
    public class ToDoListProjectApiContext : DbContext
    {
        public ToDoListProjectApiContext (DbContextOptions<ToDoListProjectApiContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }

        public DbSet<Project> Project { get; set; }
    }
}
