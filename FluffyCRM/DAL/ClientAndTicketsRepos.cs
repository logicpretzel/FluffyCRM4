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
        public IEnumerable<ContactPhone> ClientPhonesList(int ClientID = 0)
        {
            IEnumerable<ContactPhone> model;

            var idParam1 = new SqlParameter
            {
                ParameterName = "ClientID",
                Value = ClientID
            };

            model = _dc.Database.SqlQuery<ContactPhone>("Select * from ContactPhones where ParentId = @ClientId and ParentRecordType = 1 order by PhoneType", idParam1).ToList();
           

            
            return model;
        }

        public IEnumerable<TicketList> GetTicketList(string UserID = "")
        {
            IEnumerable<TicketList> model;
            Guid result;
            if (UserID.Length == 0)
            {
                model = _dc.Database.SqlQuery<TicketList>("Select TicketId, Subject, CategoryId, Category,  CreateDate, Status, ClientId, CompanyName, StartDate, CompletedDate, DueDate, CreatedBy, LastName, FirstName, FullName, left(Description,35) as ShortDesc from vTickets").ToList();
            }
            else
            {
                if (Guid.TryParse(UserID, out result))
                {
                    model = _dc.Database.SqlQuery<TicketList>("Select TicketId, Subject, CategoryId,Category, CreateDate, Status, ClientId, CompanyName, StartDate, CompletedDate, DueDate, CreatedBy, LastName, FirstName, FullName, left(Description,35) as ShortDesc from vTickets").Where(s => s.CreatedBy.ToString() == result.ToString()).ToList();

                }
                else
                {
                    model = _dc.Database.SqlQuery<TicketList>("Select TicketId, Subject, CategoryId,Category, CreateDate, Status, ClientId, CompanyName, StartDate, CompletedDate, DueDate, CreatedBy, LastName, FirstName, FullName, left(Description,35) as ShortDesc from vTickets where [TicketId] is null").ToList(); // should not happen. return empty model

                }

            }
            return model;
        }

        public IEnumerable<Category> GetCategoryList(FLCatType CategoryType = FLCatType.None)
        {
            IEnumerable<Category> model;

            int catType = 0;

            catType = Convert.ToInt32(CategoryType);

            var idParam = new SqlParameter
            {
                ParameterName = "catType",
                Value = catType
            };

            model = _dc.Database.SqlQuery<Category>("Select * from Categories where [Type] = @catType", idParam).ToList();
          

            
            return model;
        }

        public IEnumerable<Client> GetClientListAll()
        {
            IEnumerable<Client> model;

            //int catType = 0;

            //catType = Convert.ToInt32(CategoryType);

            //var idParam = new SqlParameter
            //{
            //    ParameterName = "catType",
            //    Value = catType
            //};

            model = _dc.Database.SqlQuery<Client>("Select * from Clients order by CompanyName ").ToList();



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

        public StaffDashBoard GetUserClientCounts()
        {
            StaffDashBoard model = new StaffDashBoard();
            model  = _dc.Database.SqlQuery<StaffDashBoard>("select   (select count(*) from Clients u) ClientCount, (select count(*) from AspNetUsers u) UserCount, (select count(*) from AspNetUsers u2 where u2.EmailConfirmed = 'False' ) UsersAwaitingValidation").SingleOrDefault();
            return model;

        }


    }
}