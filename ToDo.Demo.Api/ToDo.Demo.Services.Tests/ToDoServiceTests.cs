using AutoFixture;
using System;
using System.Collections.Generic;
using ToDo.Demo.Services.Concrete;
using ToDo.Demo.Services.Models;
using Xunit;

namespace ToDo.Demo.Services.Tests
{
    public class ToDoServiceTests
    {
        private readonly Fixture _fixture;

        public ToDoServiceTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void GetToDoItems_NoItems_ReturnsEmptyList()
        {
            //  Arrange
            var dataStore = new Dictionary<string, ToDoItem>();
            var service = new ToDoService(dataStore);

            //  Act
            var result = service.GetToDoItems();

            //  Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetToDoItems_TwoItems_ReturnsList()
        {
            //  Arrange
            var dataStore = new Dictionary<string, ToDoItem>();
            var itemOne = _fixture.Create<ToDoItem>();
            var itemTwo = _fixture.Create<ToDoItem>();
            dataStore.Add(itemOne.Id, itemOne);
            dataStore.Add(itemTwo.Id, itemTwo);

            var service = new ToDoService(dataStore);

            //  Act
            var result = service.GetToDoItems();

            //  Assert
            Assert.Equal(2,result.Count);
        }

        [Fact]
        public void Add_Valid_ReturnsSuccess()
        {
            //  Arrange
            var dataStore = new Dictionary<string, ToDoItem>();
            var item = new ToDoItem { Description = "Something cool" } ;
            var service = new ToDoService(dataStore);

            //  Act
            var result = service.Add(item);

            //  Assert
            Assert.Single(dataStore);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Add_InValid_ReturnsExpectedMessage()
        {
            //  Arrange
            var dataStore = new Dictionary<string, ToDoItem>();
            var item = new ToDoItem ();
            var service = new ToDoService(dataStore);

            //  Act
            var result = service.Add(item);

            //  Assert
            Assert.Empty(dataStore);
            Assert.False(result.IsSuccess);
            Assert.Equal("Description property is required. ", result.ErrorMessage);
        }

        [Fact]
        public void Update_Valid_ReturnsSuccess()
        {
            //  Arrange
            var dataStore = new Dictionary<string, ToDoItem>();
            var item = _fixture.Create<ToDoItem>();
            dataStore.Add(item.Id, item);
            var service = new ToDoService(dataStore);

            //  Act
            var result = service.Update(item);

            //  Assert
            Assert.True(result.IsSuccess);
        }

        [Theory]
        [InlineData("ValidId", null, "Description property is required. ")]
        [InlineData(null, "some description", "Id property is required. ")]
        [InlineData(null, null, "Id property is required. Description property is required. ")]
        [InlineData("InValidId", "some description", "No Item found for Id InValidId")]
        public void Update_InValid_ReturnsExpectedMessage(string id, string description, string expectedMessage)
        {
            //  Arrange
            var dataStore = new Dictionary<string, ToDoItem>();
            var existingItem = new ToDoItem 
            { 
                Id = "ValidId",
                Description = "Something cool" 
            };
            var item = new ToDoItem { Id = id, Description = description };
            var service = new ToDoService(dataStore);

            //  Act
            var result = service.Update(item);

            //  Assert
            Assert.Equal(expectedMessage, result.ErrorMessage);
            Assert.False(result.IsSuccess);
        }
    }
}
