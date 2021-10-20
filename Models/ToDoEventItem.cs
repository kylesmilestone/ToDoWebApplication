using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoWebApplication.Models
{
    public class ToDoEventItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ToDoId { get; set; }
        public string EventActionEnum { get; set; }
        public DateTime EventTime { get; set; }
        public ICollection<Property> Properties { get; set; }
    }

    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
    }
}
