using Microsoft.EntityFrameworkCore;
using ToDoList.Web.Models;

namespace ToDoList.Web.Data
{
    public class WebApplicationContext : DbContext
    {
        public WebApplicationContext (DbContextOptions<WebApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public object TypeOfValue { get; internal set; }
      
        public DbSet<ToDoItem> ToDoItem { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<TagToDoItem> TagToDoItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagToDoItem>()
                .HasKey(cs => new { cs.TagId, cs.ToDoItemId });
            modelBuilder.Entity<TagToDoItem>()
            .HasOne<Tag>(sc => sc.Tag)
            .WithMany(s => s.TagToDoItems)
            .HasForeignKey(sc => sc.TagId);


            modelBuilder.Entity<TagToDoItem>()
            .HasOne<ToDoItem>(sc => sc.ToDoItem)
            .WithMany(s => s.TagToDoItems)
            .HasForeignKey(sc => sc.ToDoItemId);
        }
        
    }
}
