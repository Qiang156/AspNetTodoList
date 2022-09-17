using System;
using System.Collections.Generic;

namespace ToDoList.Models
{
    public class UsersViewModel
    {
        public ApplicationUser[] Administrators { get; set; } = {};

        public ApplicationUser[] Everyone { get; set;} = {};
    }
}