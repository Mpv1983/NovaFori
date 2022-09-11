using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Demo.Services.Abstract;
using ToDo.Demo.Services.Models;

namespace ToDo.Demo.Services.Concrete
{
    public class ToDoService : IToDoService
    {
        private readonly Dictionary<string, ToDoItem> _dataStore;

        public ToDoService(Dictionary<string, ToDoItem> dataStore)
        {
            _dataStore = dataStore;
        }

        public Outcome Add(ToDoItem item)
        {
            item.Id = Guid.NewGuid().ToString();
            var outcome = Validate(item);

            if (!outcome.IsSuccess)
            {
                return outcome;
            }

            _dataStore.Add(item.Id, item);
            return outcome;
        }

        public List<ToDoItem> GetToDoItems()
        {
            if (!_dataStore.Any())
            {
                return new List<ToDoItem>();
            }

            return _dataStore.Values.ToList();
        }

        public Outcome Update(ToDoItem item)
        {
            var outcome = Validate(item);
            if (!outcome.IsSuccess)
            {
                return outcome;
            }

            if (!_dataStore.ContainsKey(item.Id))
            {
                outcome.AddError($"No Item found for Id {item.Id}");
            }

            _dataStore[item.Id] = item;

            return outcome;
        }

        private Outcome Validate(ToDoItem item)
        {
            var outcome = new Outcome
            {
                ErrorMessage = string.Empty,
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(item.Id))
            {
                outcome.AddError("Id property is required. ");
            }

            if (string.IsNullOrEmpty(item.Description))
            {
                outcome.AddError("Description property is required. ");
            }

            return outcome;
        }
    }
}
