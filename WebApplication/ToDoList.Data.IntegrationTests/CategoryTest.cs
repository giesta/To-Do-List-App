using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ToDoList.Data.Models.ToDoList;
using ToDoList.Data.Data;
using Xunit;

namespace ToDoList.Data.IntegrationTests
{
    public class CategoryTest
    {
        [Fact]
        public void Get_List_Of_Category()
        {
            //Arange
            var data = new[] {new CategoryDao{Id = 1, Name = "Home"}, new CategoryDao{Id = 2, Name = "School"}}.AsQueryable();
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
            //Assert
            Assert.Equal(data.FirstOrDefault(m => m.Id == 1), category);
        }
    }
}
