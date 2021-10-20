using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoWebApplication.Models;
using ToDoWebApplication.Repositories;

namespace ToDoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public TodoController(IToDoRepository<TodoItem> repository)
        {
            _repository = repository;
        }

        private readonly IToDoRepository<TodoItem> _repository;
        // GET: api/<TodoController>
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetAllTodoItems()
        {
            return await _repository.GetAllToDoItems();
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public async Task<TodoItem> GetTodoItem(Guid id)
        {
            return await _repository.GetToDoItem(id);
        }

        // POST api/<TodoController>/create
        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateTodoItem([FromBody] TodoItemBase item)
        {
            return await _repository.CreateNewToDoItem(item);
        }

        // POST api/<TodoController>/update
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateTodoItem([FromBody] TodoItem item)
        {
            return await _repository.UpdateToDoItem(item);
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _repository.DeleteToDoItem(id);
        }
    }
}
