using System;
using ToDoList.Models.Entity;

namespace ToDoList.Models
{
    public class TodoViewModel
    {
        public TodoItem[] Items {get; set;} = null!;
    }
}