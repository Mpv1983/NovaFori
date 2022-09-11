using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Demo.Api.Controllers;
using ToDo.Demo.Services.Abstract;
using ToDo.Demo.Services.Models;
using Xunit;

namespace ToDo.Demo.Api.Tests
{
    public class ToDoControllerTests
    {
        private readonly Mock<ILogger<ToDoController>> _mockLogger;
        private readonly Mock<IToDoService> _mockService;
        private readonly Fixture _fixture;

        public ToDoControllerTests()
        {
            _mockLogger = new Mock<ILogger<ToDoController>>();
            _mockService = new Mock<IToDoService>();
            _fixture = new Fixture();
        }

        [Fact]
        public void Get_ReturnsList()
        {
            //  Arrange
            var list = _fixture.Create<List<ToDoItem>>();
            _mockService.Setup(m => m.GetToDoItems()).Returns(list);
            var controller = new ToDoController(_mockLogger.Object, _mockService.Object);

            //  Act
            var result = controller.Get();

            //  Assert
            Assert.Equal(list.Count, result.ToList().Count);
        }
    }
}
