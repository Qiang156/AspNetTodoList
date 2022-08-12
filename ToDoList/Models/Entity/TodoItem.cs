using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Entity
{
    public class TodoItem
    {
        public Guid id {get; set;}
        
        public bool isDone {get; set;}
        
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100)]
        public string title {get; set;} = null!;

        public DateTimeOffset? dueAt {get; set;}

        public DateTimeOffset createdAt {get; set;}
        
        public DateTimeOffset updatedAt {get; set;}

        public bool isDelete {get; set;}
        
        public String? userId {get; set;}
    }
}