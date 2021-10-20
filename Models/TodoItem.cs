using System;

namespace ToDoWebApplication.Models
{
    public class TodoItem: TodoItemBase
    {
        public Guid Id { get; set; }
    }
    public class TodoItemBase
    {
        public string Title { get; set; }
        public string ToDoState { get; set; }
    }
}
