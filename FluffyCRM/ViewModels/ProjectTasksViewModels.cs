using FluffyCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FluffyCRM.ViewModels
{
    public class TaskAssignee : TaskAssignment 
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }

    }

}