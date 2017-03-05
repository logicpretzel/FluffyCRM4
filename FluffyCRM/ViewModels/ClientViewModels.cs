using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FluffyCRM.ViewModels
{
    public class ClientViewModels
    {
        public class ClientTicket
        {
            [Key]
            [DisplayName("Ticket Number")]
            public int TicketId { get; set; }

            [DisplayName("Subject")]
            [StringLength(255)]
            public string Subject { get; set; }

          

            [DisplayName("Description")]
            [StringLength(8000)]
            public string Description { get; set; }

       

            [DisplayName("Customer")]
            public int ClientId { get; set; }





        }

    }
}