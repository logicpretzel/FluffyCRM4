﻿using FluffyCRM.Models;
using FluffyCRM.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.SqlTypes;
using Microsoft.AspNet.Identity;

namespace FluffyCRM.DAL
{


    partial class DataRepository
    {
        ApplicationDbContext _dc = new ApplicationDbContext();
        private class u
        {
            public string UserID { get; set; }
        }

        #region ROLES
        public string GetRoleByName(string role)
        {
            var idParam = new SqlParameter
            {
                ParameterName = "role",
                Value = role
            };
            RoleNameVM rc = _dc.Database.SqlQuery<RoleNameVM>("Select id from AspNetRoles where Name = @role", idParam).SingleOrDefault<RoleNameVM>();
            return rc.id.ToString();

        }

        public IEnumerable<UserRoleVM> GetUserRolesList(string UserID = "")
        {
            IEnumerable<UserRoleVM> model;
            Guid result;
            if (UserID.Length == 0)
            {
                model = _dc.Database.SqlQuery<UserRoleVM>("Select Email, UserName, LastName, FirstName, UserId, RoleId, RoleName from vUsrRoles").ToList();
            }
            else
            {
                if (Guid.TryParse(UserID, out result))
                {
                    model = _dc.Database.SqlQuery<UserRoleVM>("Select Email, UserName, LastName, FirstName, UserId, RoleId, RoleName from vUsrRoles").Where(s => s.UserId.ToString() == result.ToString()).ToList();

                }
                else
                {
                    model = _dc.Database.SqlQuery<UserRoleVM>("Select Email, UserName, LastName, FirstName, UserId, RoleId, RoleName from vUsrRoles where UserId is null").ToList(); // should not happen. return empty model

                }

            }
            return model;
        }




        public bool RemoveAllRolesFromUser(string id, string userManagerID)
        {
            bool rc = false;
            string sql = "";
            //    if (role == "Admin") return false;  // can't use this method to create an admin role


            var idParam1 = new SqlParameter
            {
                ParameterName = "UID",
                Value = id
            };

            if (id == userManagerID)
            {
                sql = "Delete from AspNetUserRoles where UserId = @UID and RoleID not in (select Id from AspNetRoles where Name like 'admin%')";
            }
            else
            {
                sql = "Delete from AspNetUserRoles where UserId = @UID";
            }


            try
            {

                _dc.Database.ExecuteSqlCommand(sql, idParam1);
                rc = true;
            }
            catch
            {
                rc = false;
            }
            return rc;
        } // END RemoveAllRolesFromUser

        #endregion

        #region USERS

        public string GetUserIDByUserName(string userName)
        {
            var idParam = new SqlParameter
            {
                ParameterName = "username",
                Value = userName
            };
            u rc = _dc.Database.SqlQuery<u>("Select id from AspNetUsers where UserName = @username", idParam).SingleOrDefault<u>();
            return rc.UserID;

        }

        public IEnumerable<UserList> GetUserList(string UserID = "")
        {
            IEnumerable<UserList> model;
            Guid result;
            if (UserID.Length == 0)
            {
                model = _dc.Database.SqlQuery<UserList>("Select Email, UserName, LastName, FirstName, Id as UserId from AspNetUsers").ToList();
            }
            else
            {
                if (Guid.TryParse(UserID, out result))
                {
                    model = _dc.Database.SqlQuery<UserList>("Select Email, UserName, LastName, FirstName, Id as UserId from AspNetUsers").Where(s => s.UserId.ToString() == result.ToString()).ToList();


                }
                else
                {
                    model = _dc.Database.SqlQuery<UserList>("Select Email, UserName, LastName, FirstName, Id as UserId from AspNetUsers").ToList();

                }

            }
            return model;
        } // END GetUserList


        public IEnumerable<ApplicationUser> GetUserSearchableList(string userName = "", string lastName = "", string email = "", string role = "")
        {


            var idParam1 = new SqlParameter
            {
                ParameterName = "UserName",
                Value = userName.Length > 0 ? userName : SqlString.Null
            };

            var idParam2 = new SqlParameter
            {
                ParameterName = "LastName",
                Value = lastName.Length > 0 ? lastName : SqlString.Null
            };

            var idParam3 = new SqlParameter
            {
                ParameterName = "Email",
                Value = email.Length > 0 ? email : SqlString.Null
            };

            var idParam4 = new SqlParameter
            {
                ParameterName = "Role",
                Value = role.Length > 0 ? role : SqlString.Null
            };


            var model = _dc.Database.SqlQuery<ApplicationUser>(
                "spGetUserList @UserName, @LastName, @Email, @Role", idParam1, idParam2, idParam3, idParam4);

            return model;
        } // END GetUserList


        public IEnumerable<RolesForUser> GetRolesForUser(string userID, string userManagerID)
        {
            IEnumerable<RolesForUser> model;


            Guid result;
            if (!Guid.TryParse(userID, out result))
            {
                return null;

            }


            var idParam = new SqlParameter
            {
                ParameterName = "UserID",
                Value = userID
            };

            var idParam2 = new SqlParameter
            {
                ParameterName = "UserManagerID",
                Value = userManagerID
            };

            model = _dc.Database.SqlQuery<RolesForUser>("spRolesForUser @UserID, @UserManagerID", idParam, idParam2);

            return model.ToList();
        } // GetRolesForUser

        public bool UpdateTicket(Ticket ticket)
        {
            bool rc = false;
            
          

            string sql = "UPDATE [Tickets]   SET [Subject] = @Subject,[CategoryId]=@CategoryId,[Description]= @Description,[Status]= @Status,[DeleteInd] = @DeleteInd ,[ClientId]= @ClientId,[StartDate]= @StartDate,[CompletedDate]= @CompletedDate,[DueDate]= @DueDate WHERE TicketID = @TicketID";
            
            //try
            //     {

            _dc.Database.ExecuteSqlCommand(sql
                     , new SqlParameter("@Subject",ticket.Subject.ToString())
                     , new SqlParameter("@CategoryId", ticket.CategoryId.Value)
                     , new SqlParameter("@Description", ticket.Description.ToString())
                     , new SqlParameter("@Status", Convert.ToInt32(ticket.Status.Value))
                     , new SqlParameter("@DeleteInd", Convert.ToBoolean(ticket.DeleteInd))
                     , new SqlParameter("@ClientId", Convert.ToInt32(ticket.ClientId.Value))
                     , new SqlParameter("@StartDate",  ticket.StartDate == null ? DBNull.Value : (object)ticket.StartDate)
                     , new SqlParameter("@DueDate", ticket.DueDate == null ? DBNull.Value : (object)ticket.DueDate)
                     , new SqlParameter("@CompletedDate", ticket.CompletedDate == null ? DBNull.Value : (object)ticket.CompletedDate)
                     , new SqlParameter("@TicketId",ticket.TicketId)

                     );
                rc = true;
        //    }
       //     catch
       //     {
       //         rc = false;
       //     }
            return rc;

        }

        public bool AddRoleToUser(string userID, string role, string userManagerID)
        {
            bool rc = false;
            if (userID == userManagerID)
            {
                if (role.ToLower().Trim() == "admin") return true;  // can't use this method to create an admin role to your own user account.
            }

            var idParam1 = new SqlParameter
            {
                ParameterName = "UID",
                Value = userID
            };

            var idParam2 = new SqlParameter
            {
                ParameterName = "Role",
                Value = GetRoleByName(role)
            };
            try
            {

                _dc.Database.ExecuteSqlCommand("Insert into AspNetUserRoles (UserId,RoleId) values (@UID, @Role)", idParam1, idParam2);
                rc = true;
            }
            catch
            {
                rc = false;
            }
            return rc;
        } // END AddRoleToUser


    }
    #endregion

}
