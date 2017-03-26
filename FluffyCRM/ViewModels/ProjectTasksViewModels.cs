using FluffyCRM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.ViewModels
{
    public class TaskAssignee : TaskAssignment 
    {

        public string   LastName    { get; set; }

        public string   FirstName   { get; set; }

        public string   Initials    { get; set; }

       

    }

    public class TaskListNarrow {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(80)]
        [DataType(DataType.MultilineText)]
        public string ShortDesc { get; set; }

       
        public int? TaskType { get; set; }
        [StringLength(50)]
        public string TaskTypeName { get; set; }

       
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [HiddenField]
        public int? ProjectId { get; set; }
       
        [DisplayName("Created By")]
        [StringLength(128)]
        public string CreatedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date Completed")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CompletedDate { get; set; }

        [DisplayName("Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }
    }

}