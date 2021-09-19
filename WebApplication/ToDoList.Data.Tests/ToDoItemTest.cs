using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using ToDoList.Data.Data;
using ToDoList.Data.Models.ToDoList;
using Xunit;

namespace ToDoList.Data.Tests
{
   public class ToDoItemTest
    {
        [Fact]
        public void Get_List_Of_ToDoItems()
        {
            //Arange
            var data = new[] { new ToDoItemDao { Id = 1, Name = "Homework", CreationDate = new DateTime(), DeadLineDate = new DateTime(), Priority = 1} }.AsQueryable();
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<ToDoItemDao>()).Returns(data);
            //Act
            var context = mock.Object;
            var toDoItems = context.Set<ToDoItemDao>();
            //Assert
            Assert.Equal(data, toDoItems);
        }
        [Fact]
        public void Get_Specific_ToDoItem()
        {
            //Arange
            var data = new[] { new ToDoItemDao { Id = 1, Name = "Homework", CreationDate = new DateTime(), DeadLineDate = new DateTime(), Priority = 1 } }.AsQueryable();
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<ToDoItemDao>()).Returns(data);
            //Act
            var context = mock.Object;
            var toDoItem = context.Set<ToDoItemDao>().FirstOrDefault(m => m.Id == 1);
            context.SaveChanges();
            //Assert
            Assert.Equal(data.FirstOrDefault(m => m.Id == 1), toDoItem);
        }
        [Fact]
        public void Delete_Specific_ToDoItem()
        {
            //Arange
            var data = new List<ToDoItemDao> { new ToDoItemDao { Id = 1, Name = "Homework", CreationDate = new DateTime(), DeadLineDate = new DateTime(), Priority = 1 }};
            var mock = new Mock<IDbContext>();
            var toDo = new ToDoItemDao { Id = 1, Name = "Homework", CreationDate = new DateTime(), DeadLineDate = new DateTime(), Priority = 1 } ;
            mock.Setup(x => x.ToDoItem.Remove(It.IsAny<ToDoItemDao>())).Callback<ToDoItemDao>((entity) => data.Remove(entity));
            //Act
            var context = mock.Object;
            context.ToDoItem.Remove(toDo);
            //Assert
            Assert.True(data.Count == 0);
        }
        [Fact]
        public void Add_Category()
        {
            //Arange
            var data = new List<ToDoItemDao> { new ToDoItemDao { Id = 1, Name = "Homework", CreationDate = new DateTime(), DeadLineDate = new DateTime(), Priority = 1 } };
            var mock = new Mock<IDbContext>();
            var toDo = new ToDoItemDao { Id = 2, Name = "Homework", CreationDate = new DateTime(), DeadLineDate = new DateTime(), Priority = 1 };
            mock.Setup(x => x.ToDoItem.Add(It.IsAny<ToDoItemDao>())).Callback<ToDoItemDao>((entity) => data.Add(entity));
            //Act
            var context = mock.Object;
            context.ToDoItem.Add(toDo);
            //Assert
            Assert.True(data.Count == 2);
        }
    }
}
