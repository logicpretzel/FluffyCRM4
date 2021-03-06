﻿using FluffyCRM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static FluffyCRM.Models.TaskNote;

namespace FluffyCRM.ViewModels
{
   
        public class StaffDashBoard
        {

            [DisplayName("Client Count")]
            public int ClientCount { get; set; }

            [DisplayName("User Count")]
            public int UserCount { get; set; }

            [DisplayName("Awaiting Validation")]
            public int UsersAwaitingValidation { get; set; }

        } 

        public class ClientTicket
        {
           
            [Required]
            [DisplayName("Subject")]
            [StringLength(255)]
            public string Subject { get; set; }
            
            [Required]
            [DataType(DataType.MultilineText)]
            [DisplayName("Description")]
            [StringLength(8000)]
            public string Description { get; set; }


            [Key]
            [HiddenInput]
            public int? TicketId { get; set; }

            [HiddenInput]
            public DateTime CreateDate { get; set; }
             
            [HiddenInput]
            public ticketStatus Status { get; set; }

    }




    public class TicketList {
        /*      ,[Subject]
      ,[CategoryId]
      ,[CreateDate]
      ,[Description]
      ,[Status]
      ,[DeleteInd]
      ,[ClientId]
      ,[StartDate]
      ,[CompletedDate]
      ,[DueDate]
      ,[CreatedBy]
      ,[LocalTime]
      ,[LastName]
      ,[FirstName]
      ,[FullName]*/
        [Key]
        public int TicketId { get; set; }

        public string       Subject { get; set; }

        [DisplayName("Category")]
        public string          Category { get; set; }

        [DisplayName("Create Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy HH:mm}")]
        public DateTime     CreateDate { get; set; }

        public ticketStatus Status { get; set; }

        [DisplayName("Client")]
        public string CompanyName { get; set; }

        [DisplayName("ClientId")]
        public int?          ClientId { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        public DateTime?     StartDate { get; set; }

        [DisplayName("Date Completed")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        public DateTime?     CompletedDate { get; set; }

        [DisplayName("Due Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}")]
        public DateTime? DueDate { get; set; }

        [StringLength(128)]
        public string       CreatedBy { get; set; }

        [DisplayName("Last Name")]
        public string       LastName { get; set; }

        [DisplayName("First Name")]
        public String       FirstName { get; set; }

        [DisplayName("Created By")]
        public String       FullName { get; set; }

        [DisplayName("Short Desc")]
        [StringLength(35)]
        public string       ShortDesc { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
    }

    public class TaskNoteList {
        [Key]
        public int Id { get; set; }

        [DisplayName("Note Type")]
        public string NoteType { get; set; }

        [HiddenField]
        public int? CategoryId { get; set; }

        [StringLength(255)]
        public string Subject { get; set; }

        [StringLength(8000)]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [HiddenField]
        public int JobTask_Id { get; set; }

        [DisplayName("Task Name")]
        public string TaskName { get; set; }

        [HiddenField]
        [StringLength(128)]
        public string CreatedBy { get; set; }

        [DisplayName("Created")]
        public DateTime? CreateDate { get; set; }

        [DefaultValue(0)]
        public FlNoteStatus? Status { get; set; }

        [DisplayName("Added By")]
        public string AddedByName { get; set; }

        [DisplayName("Assigned To")]
        public string AssignedName { get; set; }

        [DisplayName("Client")]
        public string ClientName { get; set; }


        [HiddenField]
        public int? ClientId { get; set; }

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

       
        /*
NoteType, 	
CategoryId, 
Subject, 	
Comment, 
JobTask_Id, 
TaskName, 
CreatedBy, 
CreateDate, 
Status, 
AddedByName, 
AssignedName, 
ClientName, 
ClientId, 
StartDate, 
CompletedDate, 
DueDate, 
LocalTime
         */

    }

    public class TicketCommentList {
        [Key]
        public int Id { get; set; }

        [DisplayName("Ticket Number")]
        public int TicketId { get; set; }

        [DisplayName("Subject")]
        [StringLength(255)]
        public string Subject { get; set; }

        [DisplayName("Description")]
        [StringLength(8000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayName("Created")]
        public DateTime? CreateDate { get; set; }

        public CommentStatus Status { get; set; }

        [DisplayName("Create Date")]
        public DateTime? LocalTime { get; set; }

        [StringLength(128)]
        public string CreatedByName { get; set; }
    }

    public class TicketAddVM
    {
       
        
        [Key]
        [DisplayName("Ticket Number")]
        public int TicketId { get; set; }

        [DisplayName("Subject")]
        [StringLength(255)]
        public string Subject { get; set; }

        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        


        [DisplayName("Description")]
        [StringLength(8000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DefaultValue(0)]
        public ticketStatus? Status { get; set; }

     

        [DisplayName("Customer")]
        public int? ClientId { get; set; }

        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }
        [DisplayName("Date Completed")]
        public DateTime? CompletedDate { get; set; }
        [DisplayName("Due Date")]
        public DateTime? DueDate { get; set; }
       


    }


    public class EmpList
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(3)]
        public string Initials { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

       
    }

}