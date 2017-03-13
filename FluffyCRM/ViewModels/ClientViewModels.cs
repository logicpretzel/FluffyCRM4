using FluffyCRM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.ViewModels
{
   
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
        public int?          CategoryId { get; set; }
        [DisplayName("Create Date")]
        public DateTime     CreateDate { get; set; }
        public ticketStatus Status { get; set; }

        [DisplayName("Client")]
        public int?          ClientId { get; set; }
        [DisplayName("Start Date")]
        public DateTime?     StartDate { get; set; }
        [DisplayName("Date Completed")]
        public DateTime?     CompletedDate { get; set; }
        [StringLength(128)]
        public string       CreatedBy { get; set; }
        [DisplayName("Last Name")]
        public string       LastName { get; set; }
        [DisplayName("First Name")]
        public String       FirstName { get; set; }

        [DisplayName("Created By")]
        public String       FullName { get; set; }

        [DisplayName("Description")]
        [StringLength(35)]
        public string       ShortDesc { get; set; }

    }


}