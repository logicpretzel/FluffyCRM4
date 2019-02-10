using FluffyCRM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.ViewModels
{
    public class TaskAssignee
    {
        [Key]
        public int Id { get; set; }

        [StringLength(128)]
        public string   UserId { get; set; }

        [StringLength(100)]
        public string   LastName    { get; set; }

        [StringLength(100)]
        public string   FirstName   { get; set; }

        [StringLength(3)]
        public string   Initials    { get; set; }

       

    }

    public class ProductListVM {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(32)]
        public string Version { get; set; }
    }

    public class DevNotes : TaskNote {
        [DisplayName("Note Type")]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(255)]
        [DisplayName("Linked To Task")]
        public string ParentTaskName { get; set; }

        [StringLength(255)]
        [DisplayName("Created By")]
        public string CreatedByName { get; set; }

        [StringLength(255)]
        [DisplayName("Client Name")]
        public string ClientName { get; set; }
        
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