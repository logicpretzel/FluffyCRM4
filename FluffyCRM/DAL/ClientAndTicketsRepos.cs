using FluffyCRM.Models;
using FluffyCRM.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
        #region TICKETS
        public IEnumerable<TicketList> GetTicketList(string UserID = "")
        {
            IEnumerable<TicketList> model;
            Guid result;
            if (UserID.Length == 0)
            {
                model = _dc.Database.SqlQuery<TicketList>("Select TicketId, Subject, CategoryId, Category,  CreateDate, Status, ClientId, CompanyName, StartDate, CompletedDate, DueDate, CreatedBy, LastName, FirstName, FullName, left(Description,35) as ShortDesc, Description from vTickets").ToList();
            }
            else
            {
                if (Guid.TryParse(UserID, out result))
                {
                    model = _dc.Database.SqlQuery<TicketList>("Select TicketId, Subject, CategoryId,Category, CreateDate, Status, ClientId, CompanyName, StartDate, CompletedDate, DueDate, CreatedBy, LastName, FirstName, FullName, left(Description,35) as ShortDesc, Description from vTickets").Where(s => s.CreatedBy.ToString() == result.ToString()).ToList();

                }
                else
                {
                    model = _dc.Database.SqlQuery<TicketList>("Select TicketId, Subject, CategoryId,Category, CreateDate, Status, ClientId, CompanyName, StartDate, CompletedDate, DueDate, CreatedBy, LastName, FirstName, FullName, left(Description,35) as ShortDesc, Description from vTickets where [TicketId] is null").ToList(); // should not happen. return empty model

                }

            }
            return model;
        }


        public bool UpdateClientTicket(Ticket ticket)
        {
            bool rc = false;



            string sql = "UPDATE [Tickets]   SET [Subject] = @Subject,[CategoryId]=@CategoryId,[Description]= @Description WHERE TicketID = @TicketID";

            //try
            //     {

            _dc.Database.ExecuteSqlCommand(sql
                     , new SqlParameter("@Subject", ticket.Subject.ToString())
                     , new SqlParameter("@CategoryId", ticket.CategoryId.Value)
                     , new SqlParameter("@Description", ticket.Description.ToString())
                     , new SqlParameter("@TicketId", ticket.TicketId)

                     );
            rc = true;
            //    }
            //     catch
            //     {
            //         rc = false;
            //     }
            return rc;

        }
        public bool UpdateTicket(Ticket ticket)
        {
            bool rc = false;



            string sql = "UPDATE [Tickets]   SET [Subject] = @Subject,[CategoryId]=@CategoryId,[Description]= @Description,[Status]= @Status,[DeleteInd] = @DeleteInd ,[ClientId]= @ClientId,[StartDate]= @StartDate,[CompletedDate]= @CompletedDate,[DueDate]= @DueDate WHERE TicketID = @TicketID";

            //try
            //     {

            _dc.Database.ExecuteSqlCommand(sql
                     , new SqlParameter("@Subject", ticket.Subject.ToString())
                     , new SqlParameter("@CategoryId", ticket.CategoryId.Value)
                     , new SqlParameter("@Description", ticket.Description.ToString())
                     , new SqlParameter("@Status", Convert.ToInt32(ticket.Status.Value))
                     , new SqlParameter("@DeleteInd", Convert.ToBoolean(ticket.DeleteInd))
                     , new SqlParameter("@ClientId", Convert.ToInt32(ticket.ClientId.Value))
                     , new SqlParameter("@StartDate", ticket.StartDate == null ? DBNull.Value : (object)ticket.StartDate)
                     , new SqlParameter("@DueDate", ticket.DueDate == null ? DBNull.Value : (object)ticket.DueDate)
                     , new SqlParameter("@CompletedDate", ticket.CompletedDate == null ? DBNull.Value : (object)ticket.CompletedDate)
                     , new SqlParameter("@TicketId", ticket.TicketId)

                     );
            rc = true;
            //    }
            //     catch
            //     {
            //         rc = false;
            //     }
            return rc;

        }
        #endregion

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

        public IEnumerable<TaskAssignee> AssigneeList(int TaskId) {

            var idParam1 = new SqlParameter
            {
                ParameterName = "TaskId",
                Value = TaskId
            };

            IEnumerable<TaskAssignee> lst = _dc.Database.SqlQuery<TaskAssignee>("select 	ta.FirstName,	ta.LastName,	left(isnull(ta.FirstName,''),1) + left(isnull(ta.LastName,''),1) as Initials,	ta.TaskId,	ta.UserId,	ta.Id	 from TaskAssignments ta where ta.TaskId = @TaskId", idParam1).ToList();

            
            return lst;
        }


        public IEnumerable<EmpList> EmpList()
        {
            IEnumerable<EmpList> model;

         

            model = _dc.Database.SqlQuery<EmpList>("Select Id, FirstName, LastName, IsNull(FirstName,'') + ' ' + IsNull(LastName,'') as Name, UserId from Employees order by LastName").ToList();



            return model;
        }
        public IEnumerable<TaskListNarrow> GetTaskList(int? _ProjectId, string assignedto, string kw)
        {
            int ProjectId = 0;
            if (_ProjectId != null) { ProjectId = (int)_ProjectId; }


            var idParam1 = new SqlParameter
            {
                ParameterName = "ProjectId",
                Value = ProjectId
            };

            var idParam2 = new SqlParameter
            {
                ParameterName = "assignedto",
                Value = assignedto.Length > 0 ? assignedto : SqlString.Null
            };

            var idParam3 = new SqlParameter
            {
                ParameterName = "kw",
                Value = kw.Length > 0 ? kw : SqlString.Null
               
            };

            IEnumerable<TaskListNarrow> lst = _dc.Database.SqlQuery<TaskListNarrow>("EXEC webuser.GetTaskList @projectid=@ProjectId, @assignedto=@assignedto, @kw=@kw", idParam1, idParam2, idParam3).ToList();


            return lst;
        }

        public bool LinkClientByUID(string uid, string createdBy, int? _clientID)
        {
            int ClientID = 0;
            Guid result;
            if (_clientID != null) { ClientID = (int)_clientID; } else { ClientID = GetClientByUID(uid); }


            bool rc = false;

            if (Guid.TryParse(createdBy, out result)) { 
                createdBy = result.ToString();
            }
            if (Guid.TryParse(uid, out result)) { 
                uid = result.ToString();
            }


            var idParam1 = new SqlParameter
            {
                ParameterName = "uid",
                Value = uid
            };

            var idParam2 = new SqlParameter
            {
                ParameterName = "clientid",
                Value = ClientID
            };

            var idParam3 = new SqlParameter
            {
                ParameterName = "userID",
                Value = createdBy
            };
            // 
            try
            {

                _dc.Database.ExecuteSqlCommand("EXEC webuser.LinkUserToClient @UID=@uid, @clientid=@clientid, @UserId=@userId", idParam1, idParam2, idParam3);
                rc = true;
            }
            catch
            {
                rc = false;
            }
            return rc;

      
        }


        public int GetClientByUID(string uid)
        {
            var idParam = new SqlParameter
            {
                ParameterName = "uid",
                Value = uid
            };



            clID rc = _dc.Database.SqlQuery<clID>("Select top 1 u.Id as UserId, u.ClientId as ClientId, cu.ContactId from AspNetUsers u  left outer join ClientUsers cu on cu.UserId = u.Id where u.Id = @uid", idParam).SingleOrDefault();
            if (rc != null)
            {
                return rc.ClientID;
            }
            else return -1;
        }

        public StaffDashBoard GetUserClientCounts()
        {
            StaffDashBoard model = new StaffDashBoard();
            model  = _dc.Database.SqlQuery<StaffDashBoard>("select   (select count(*) from Clients u) ClientCount, (select count(*) from AspNetUsers u) UserCount, (select count(*) from AspNetUsers u2 where u2.EmailConfirmed = 'False' ) UsersAwaitingValidation").SingleOrDefault();
            return model;

        }

        public IEnumerable<DevNotes> TaskNoteList(int TaskId, string assignedto, string kw)
        {

            var idParam1 = new SqlParameter
            {
                ParameterName = "TaskId",
                Value = TaskId
            };
            var idParam2 = new SqlParameter
            {
                ParameterName = "assignedto",
                Value = assignedto.Length > 0 ? assignedto : SqlString.Null
            };

            var idParam3 = new SqlParameter
            {
                ParameterName = "kw",
                Value = kw.Length > 0 ? kw : SqlString.Null

            };
            IEnumerable<DevNotes> lst = _dc.Database.SqlQuery<DevNotes>("exec webuser.TaskNotesList  @TaskId=@TaskId, @assignedTo=@assignedTo,@kw=@kw", idParam1, idParam2, idParam3).ToList();


            return lst;
        }

       

        public IEnumerable<TaskNoteList> TaskNoteListing(int WorkTaskId, string assignedTo, string CreatedBy, string kw)
        {

            var idParam1 = new SqlParameter
            {
                ParameterName = "taskId",
                Value = WorkTaskId
            };
            var idParam2 = new SqlParameter
            {
                ParameterName = "assignedTo",
                Value = assignedTo.Length > 0 ? assignedTo : SqlString.Null
            };
            var idParam3 = new SqlParameter
            {
                ParameterName = "CreatedBy",
                Value = CreatedBy.Length > 0 ? CreatedBy : SqlString.Null
            };
            var idParam4 = new SqlParameter
            {
                ParameterName = "kw",
                Value = kw.Length > 0 ? kw : SqlString.Null

            };
            IEnumerable<TaskNoteList> lst = _dc.Database.SqlQuery<TaskNoteList>("exec webuser.TaskNotesListing  @taskId=@taskId, @assignedTo=@assignedTo, @CreatedBy=@CreatedBy, @kw=@kw", idParam1, idParam2, idParam3, idParam4).ToList();


            return lst;
        }



        public IEnumerable<TicketCommentList> TicketCommentListing(int TicketId, string CreatedBy, string kw)
        {

            var idParam1 = new SqlParameter
            {
                ParameterName = "TicketId",
                Value = TicketId
            };
            var idParam2 = new SqlParameter
            {
                ParameterName = "CreatedBy",
                Value = CreatedBy.Length > 0 ? CreatedBy : SqlString.Null
            };

            var idParam3 = new SqlParameter
            {
                ParameterName = "kw",
                Value = kw.Length > 0 ? kw : SqlString.Null

            };
            IEnumerable<TicketCommentList> lst = _dc.Database.SqlQuery<TicketCommentList>("exec webuser.TicketCommentList  @TicketId=@TicketId, @CreatedBy=@CreatedBy, @kw=@kw", idParam1, idParam2, idParam3).ToList();


            return lst;
        }






    }
}