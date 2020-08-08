using Microsoft.EntityFrameworkCore;
using ToDoList.Business.Models.ToDoList;

namespace ToDoList.Business.Data
{
    public class WebApplicationContext : DbContext
    {
        public WebApplicationContext (DbContextOptions<WebApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryDao> Category { get; set; }
        public object TypeOfValue { get; internal set; }
      
        public DbSet<ToDoItem> ToDoItem { get; set; }
        public DbSet<TagDao> Tag { get; set; }
        public DbSet<TagToDoItem> TagToDoItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagToDoItem>()
                .HasKey(cs => new { cs.TagId, cs.ToDoItemId });
            modelBuilder.Entity<TagToDoItem>()
            .HasOne<TagDao>(sc => sc.TagDao)
            .WithMany(s => s.TagToDoItems)
            .HasForeignKey(sc => sc.TagId);


            modelBuilder.Entity<TagToDoItem>()
            .HasOne<ToDoItem>(sc => sc.ToDoItem)
            .WithMany(s => s.TagToDoItems)
            .HasForeignKey(sc => sc.ToDoItemId);
        }
        
    }
}
