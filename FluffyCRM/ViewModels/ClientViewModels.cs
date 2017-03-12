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
        public int?          CategoryId { get; set; }
        public DateTime     CreateDate { get; set; }
        public ticketStatus Status { get; set; }
        public int?          ClientId { get; set; }
        public DateTime?     StartDate { get; set; }
        public DateTime?     CompletedDate { get; set; }
        [StringLength(128)]
        public string       CreatedBy { get; set; }
        public string       LastName { get; set; }
        public String       FirstName { get; set; }
        public String       FullName { get; set; }
        [StringLength(35)]
        public string       ShortDesc { get; set; }

    }


}