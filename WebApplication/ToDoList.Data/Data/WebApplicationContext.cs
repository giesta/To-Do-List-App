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
      
        public DbSet<ToDoItemDao> ToDoItem { get; set; }
        public DbSet<TagDao> Tag { get; set; }
        public DbSet<TagToDoItemDao> TagToDoItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagToDoItemDao>()
                .HasKey(cs => new { cs.TagId, cs.ToDoItemId });
            modelBuilder.Entity<TagToDoItemDao>()
            .HasOne<TagDao>(sc => sc.TagDao)
            .WithMany(s => s.TagToDoItems)
            .HasForeignKey(sc => sc.TagId);


            modelBuilder.Entity<TagToDoItemDao>()
            .HasOne<ToDoItemDao>(sc => sc.ToDoItemDao)
            .WithMany(s => s.TagToDoItems)
            .HasForeignKey(sc => sc.ToDoItemId);
        }
        
    }
}
