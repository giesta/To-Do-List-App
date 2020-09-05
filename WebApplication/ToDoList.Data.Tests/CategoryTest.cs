using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using ToDoList.Data.Data;
using ToDoList.Data.Models.ToDoList;
using Xunit;

namespace ToDoList.Data.Tests
{
    public class CategoryTest
    {
        [Fact]
        public void Get_List_Of_Category()
        {
            //Arange
            var data = new[] { new CategoryDao { Id = 1, Name = "Home" }, new CategoryDao { Id = 2, Name = "School" } }.AsQueryable();
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<CategoryDao>()).Returns(data);
            //Act
            var context = mock.Object;
            var categories = context.Set<CategoryDao>();
            //Assert
            Assert.Equal(data, categories);
        }
        [Fact]
        public void Get_Specific_Category()
        {
            //Arange
            var data = new[] { new CategoryDao { Id = 1, Name = "Home" }, new CategoryDao { Id = 2, Name = "School" } }.AsQueryable();
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<CategoryDao>()).Returns(data);
            //Act
            var context = mock.Object;
            var category = context.Set<CategoryDao>().FirstOrDefault(m => m.Id == 1);
            context.SaveChanges();
            //Assert
            Assert.Equal(data.FirstOrDefault(m => m.Id == 1), category);
        }
        [Fact]
        public void Delete_Specific_Category()
        {
            //Arange
            var data = new List<CategoryDao> { new CategoryDao { Id = 1, Name = "Home" }, new CategoryDao { Id = 2, Name = "School" } };
            var mock = new Mock<IDbContext>();
            var cat = new CategoryDao {Id = 1, Name = "Home"};
            mock.Setup(x => x.Category.Remove(It.IsAny<CategoryDao>())).Callback<CategoryDao>((entity) => data.Remove(entity));
            //Act
            var context = mock.Object;
            var category = cat;
            context.Category.Remove(category);
            //Assert
            Assert.True(data.Count == 1);
        }
        [Fact]
        public void Add_Category()
        {
            //Arange
            var data = new List<CategoryDao> { new CategoryDao { Id = 1, Name = "Home" }, new CategoryDao { Id = 2, Name = "School" } };
            var mock = new Mock<IDbContext>();
            var category = new CategoryDao { Id = 3, Name = "Work" };
            mock.Setup(x => x.Category.Add(It.IsAny<CategoryDao>())).Callback<CategoryDao>((entity) => data.Add(entity));
            //Act
            var context = mock.Object;
            context.Category.Add(category);
            //Assert
            Assert.True(data.Count == 3);
        }
        
    }
}
