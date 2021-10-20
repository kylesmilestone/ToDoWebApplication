using Microsoft.EntityFrameworkCore;
using ToDoWebApplication.Models;

namespace ToDoWebApplication.DbContexts
{
    public class ToDoEventItemContext : DbContext
    {
        public ToDoEventItemContext(DbContextOptions options)
               : base(options)
        {
        }
        public DbSet<ToDoEventItem> ToDoEventItems { get; set; }
    }
}
