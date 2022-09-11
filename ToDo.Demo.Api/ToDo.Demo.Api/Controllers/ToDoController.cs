using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ToDo.Demo.Services.Abstract;
using ToDo.Demo.Services.Models;

namespace ToDo.Demo.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IToDoService _toDoService;

        public ToDoController(ILogger<ToDoController> logger, IToDoService toDoService)
        {
            _logger = logger;
            _toDoService = toDoService;
        }

        [HttpGet]
        public IEnumerable<ToDoItem> Get()
        {
            _logger.LogInformation("Get ToDoItems called");

            var result = _toDoService.GetToDoItems();

            _logger.LogInformation("Get ToDoItems returning");

            return result;
        }

        [HttpPost]
        public IActionResult Add(ToDoItem item)
        {
            _logger.LogInformation("Add ToDoItems called");

            var result = _toDoService.Add(item);

            if (!result.IsSuccess)
            {
                _logger.LogInformation($"Failed to insert item {result.ErrorMessage}");
                return new BadRequestObjectResult(result);
            }

            _logger.LogInformation("Item added");

            return new OkObjectResult(result);
        }

        [HttpPost]
        public IActionResult Update(ToDoItem item)
        {
            _logger.LogInformation("Update ToDoItems called");

            var result = _toDoService.Update(item);

            if (!result.IsSuccess)
            {
                _logger.LogInformation($"Failed to update item {result.ErrorMessage}");
                return new BadRequestObjectResult(result);
            }

            _logger.LogInformation("Item updated");

            return new OkObjectResult(result);
        }
    }
}
