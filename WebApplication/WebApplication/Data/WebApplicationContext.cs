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
        public DbSet<WebApplication.Models.Tag> Tag { get; set; }
        public DbSet<WebApplication.Models.TagToDoItem> TagToDoItem { get; set; }

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
