using FluffyCRM.Models;
using FluffyCRM.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FluffyCRM.DAL
{
    partial class DataRepository
    {
        private class clID
        {
            public string UserID { get; set; }
            public int ClientID { get; set; }

        }

        /*
           public string       Subject { get; set; }
                public int          CategoryId { get; set; }
                public DateTime     CreateDate { get; set; }
                public ticketStatus Status { get; set; }
                public int          ClientId { get; set; }
                public DateTime     StartDate { get; set; }
                public DateTime     CompletedDate { get; set; }
                [StringLength(128)]
                public string       CreatedBy { get; set; }
                public string       LastName { get; set; }
                public String       FirstName { get; set; }
                public String       FullName { get; set; }
                [StringLength(35)]
                public string       ShortDesc { get; set; }

        */
        public IEnumerable<TicketList> GetTicketList(string UserID = "")
        {
            IEnumerable<TicketList> model;
            Guid result;
            if (UserID.Length == 0)
            {
                model = _dc.Database.SqlQuery<TicketList>("Select TicketId, Subject, CategoryId, CreateDate, Status, ClientId, StartDate, CompletedDate, CreatedBy, LastName, FirstName, FullName, left(Description,35) as ShortDesc from vTickets").ToList();
            }
            else
            {
                if (Guid.TryParse(UserID, out result))
                {
                    model = _dc.Database.SqlQuery<TicketList>("Select TicketId, Subject, CategoryId, CreateDate, Status, ClientId, StartDate, CompletedDate, CreatedBy, LastName, FirstName, FullName, left(Description,35) as ShortDesc from vTickets").Where(s => s.CreatedBy.ToString() == result.ToString()).ToList();

                }
                else
                {
                    model = _dc.Database.SqlQuery<TicketList>("Select TicketId, Subject, CategoryId, CreateDate, Status, ClientId, StartDate, CompletedDate, CreatedBy, LastName, FirstName, FullName, left(Description,35) as ShortDesc from vTickets where [TicketId] is null").ToList(); // should not happen. return empty model

                }

            }
            return model;
        }



        public int GetClientByUID(string uid)
        {
            var idParam = new SqlParameter
            {
                ParameterName = "uid",
                Value = uid
            };
            clID rc = _dc.Database.SqlQuery<clID>("Select top 1 UserID, ClientID, ContactID from ClientUsers where UserId = @uid", idParam).SingleOrDefault();
            return rc.ClientID;

        }

    }
}