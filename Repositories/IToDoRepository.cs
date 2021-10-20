using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoWebApplication.Models;

namespace ToDoWebApplication.Repositories
{
    public interface IToDoRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllToDoItems();
        Task<TEntity> GetToDoItem(Guid toDoId);
        Task<Guid> CreateNewToDoItem(TodoItemBase todoItem);
        Task<IActionResult> UpdateToDoItem(TodoItem todoItem);
        Task<IActionResult> DeleteToDoItem(Guid toDoId);

    }
}
