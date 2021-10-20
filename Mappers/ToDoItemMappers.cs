using System.Collections.Generic;
using System.Linq;
using ToDoWebApplication.Enums;
using ToDoWebApplication.Models;
using System.Reflection;
using System.Threading.Tasks;

namespace ToDoWebApplication.Mappers
{
    public class ToDoItemMappers
    {
        public static async Task<TodoItem> GetToDoItemFromEvents(List<ToDoEventItem> toDoEventItems)
        {
            if (toDoEventItems.Any(e => e.EventActionEnum == EventActionEnum.Create) && !toDoEventItems.Any(e => e.EventActionEnum == EventActionEnum.Delete))
            {
                var createEvent = toDoEventItems.FirstOrDefault(e => e.EventActionEnum == EventActionEnum.Create);
                var todoItem = new TodoItem();
                todoItem.Id = createEvent.ToDoId;
                foreach (var toDoEventItem in toDoEventItems)
                {
                    if (toDoEventItem.Properties != null)
                    {
                        foreach (var property in toDoEventItem.Properties)
                        {
                            SetToDoItemValue(todoItem, property.PropertyName, property.Value);
                        }
                    }
                }
                return todoItem;
            } else
            {
                return null;
            }
        }

        public static void SetToDoItemValue(TodoItem todoItem, string propertyName, string value)
        {
            var todoItemType = typeof(TodoItem);
            PropertyInfo propertyInfo = todoItemType.GetProperty(propertyName);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(todoItem, value);
            }
        }

        public static List<Property> GetProperties(TodoItemBase todoItem)
        {
            var properties = new List<Property>();
            var todoItemType = typeof(TodoItemBase);
            foreach (var propertyInfo in todoItemType.GetProperties())
            {
                var property = new Property();
                property.PropertyName = propertyInfo.Name;
                var value = propertyInfo.GetValue(todoItem);
                if (value != null)
                {
                    property.Value = value.ToString();
                    properties.Add(property);
                }
            }
            return properties;
        }
    }
}
