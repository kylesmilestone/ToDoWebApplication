using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApplication.DbContexts;
using ToDoWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using ToDoWebApplication.Mappers;
using ToDoWebApplication.Enums;

namespace ToDoWebApplication.Repositories
{
    public class ToDoRepository : IToDoRepository<TodoItem>
    {
        private readonly ToDoEventItemContext _context;
        public ToDoRepository(ToDoEventItemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllToDoItems()
        {
            var allToDoEvents = await GetAll();
            var groups = allToDoEvents.GroupBy(a => a.ToDoId);
            var tasks = new List<Task<TodoItem>>();
            foreach (var group in groups)
            {
                Task<TodoItem> task = ToDoItemMappers.GetToDoItemFromEvents(group.ToList());
                tasks.Add(task);
            }
            var todoItems = await Task.WhenAll(tasks);
            return todoItems.Where(x => x != null);
        }

        public async Task<TodoItem> GetToDoItem(Guid toDoId)
        {
            var toDoEvents = await GetAllEventsForTodo(toDoId);
            var todoItem = await ToDoItemMappers.GetToDoItemFromEvents(toDoEvents);
            return todoItem;
        }

        public async Task<Guid> CreateNewToDoItem(TodoItemBase todoItem)
        {
            var toDoEventItem = new ToDoEventItem();
            toDoEventItem.EventActionEnum = EventActionEnum.Create;
            toDoEventItem.EventTime = DateTime.Now;
            toDoEventItem.ToDoId = Guid.NewGuid();

            var properties = ToDoItemMappers.GetProperties(todoItem);
            toDoEventItem.Properties = properties;

            await Add(toDoEventItem);
            return toDoEventItem.ToDoId;
        }

        public async Task<IActionResult> UpdateToDoItem(TodoItem todoItem)
        {
            var toDoEventItem = new ToDoEventItem();
            toDoEventItem.ToDoId = todoItem.Id;
            toDoEventItem.EventActionEnum = EventActionEnum.Update;
            toDoEventItem.EventTime = DateTime.Now;

            var oldItem = await GetToDoItem(todoItem.Id);
            
            var properties = ToDoItemMappers.GetProperties(todoItem);

            var eventProperties = new List<Property>();
            var todoItemType = typeof(TodoItem).GetProperties();

            // prevent to log unchanged properties
            foreach (var p in properties)
            {
                var thisProperty = todoItemType.FirstOrDefault(t => t.Name == p.PropertyName);
                if (thisProperty?.GetValue(oldItem).ToString() != p.Value)
                {
                    eventProperties.Add(p);
                }
            }
            toDoEventItem.Properties = eventProperties;

            await Add(toDoEventItem);
            return new OkResult();
        }

        public async Task<IActionResult> DeleteToDoItem(Guid toDoId)
        {
            var toDoEventItem = new ToDoEventItem();
            toDoEventItem.ToDoId = toDoId;
            toDoEventItem.EventActionEnum = EventActionEnum.Delete;
            toDoEventItem.EventTime = DateTime.Now;

            await Add(toDoEventItem);
            return new OkResult();
        }

        private async Task<List<ToDoEventItem>> GetAll()
        {
            return await _context.ToDoEventItems
                    .Include(car => car.Properties)
                    .OrderBy(t => t.EventTime)
                    .ToListAsync();
        }

        private async Task<List<ToDoEventItem>> GetAllEventsForTodo(Guid toDoId)
        {
            return await _context.ToDoEventItems
                    .Where(e => e.ToDoId == toDoId)
                    .Include(car => car.Properties)
                    .OrderBy(t => t.EventTime).ToListAsync();
        }

        private async Task Add(ToDoEventItem entity)
        {
            _context.ToDoEventItems.Add(entity);
           await  _context.SaveChangesAsync();
        }
    }
}
