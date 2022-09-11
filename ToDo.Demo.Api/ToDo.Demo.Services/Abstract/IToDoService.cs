using System.Collections.Generic;
using ToDo.Demo.Services.Models;

namespace ToDo.Demo.Services.Abstract
{
    public interface IToDoService
    {
        List<ToDoItem> GetToDoItems();

        Outcome Add(ToDoItem item);

        Outcome Update(ToDoItem item);
    }
}
