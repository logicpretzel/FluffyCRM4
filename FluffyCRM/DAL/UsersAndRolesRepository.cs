using FluffyCRM.Models;
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



        private class eml
        {
            public string Email { get; set; }
        }

        public string GetEmailByUID(string userId)
        {
            

        var idParam = new SqlParameter
            {
                ParameterName = "userId",
                Value = userId
            };
            var rc = _dc.Database.SqlQuery<eml>("Select Email from AspNetUsers where Id = @userId", idParam).SingleOrDefault<eml>();
            return rc.Email;
        }

        public string GetRoleName(string role)
        {


            string rc = "";
            


            var idParam = new SqlParameter
            {
                ParameterName = "role",
                Value = role
            };

          

            rc = _dc.Database.SqlQuery<string>("webuser.GetRoleName(@role)", idParam).Single();

            return rc;
        } // 

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

        

        public bool AddRoleToUser(string userID, string role, string userManagerID)
        {
            bool rc = false;
            string roleNm = "";
            Guid result;
            int? cd = 0;
            //TODO: Need first admin function to override for first user
            if (userID == userManagerID)
            {
                if (role.ToLower().Trim() == "admin") return true;  // can't use this method to create an admin role to your own user account.
            }

 
            if (!Guid.TryParse(userID, out result))
            {
                return false;

            }

            if (Guid.TryParse(role, out result))
            {
                roleNm = GetRoleName(role);

            }
            else {
                roleNm = role;
            }
            
            var idParam1 = new SqlParameter
            {
                ParameterName = "UID",
                Value = userID
            };

            var idParam2 = new SqlParameter
            {
                ParameterName = "Role",
                Value = roleNm
            };


            //TODO: If you're trying to add a client role and user is not attached to a client, should retuen error
            if (roleNm == "Client")
            {
                cd = GetClientByUID(userID) ;
                if (cd == null || cd == 0)
                {
                    return false;
                }
            }

            try
            {

                _dc.Database.ExecuteSqlCommand("EXEC webuser.AddRoleToUser @UID=@UID, @Role=@Role", idParam1, idParam2);
                rc = true;
            }
            catch
            {
                rc = false;
            }
            return rc;
        } // END AddRoleToUser

        public void AddEmpFromUser(string userID)
        {
            //bool rc = false;
            var idParam1 = new SqlParameter
            {
                ParameterName = "UID",
                Value = userID
            };
            _dc.Database.ExecuteSqlCommand("INSERT INTO [Employees] ([FirstName],[LastName],[Initials],[JobTitle],[UserId],[Address],[City],[State],[Zip],[Phone1],[PhoneType1]) SELECT [FirstName],[LastName], left(isnull(FirstName,''),1) + left(isnull(LastName,''),1) ,'' ,[Id] ,[Address]  ,[City] ,[State]  ,[Zip] ,[PhoneNumber],1  FROM [AspNetUsers] where [Id] = @UID and not exists (select * from employees where [UserId] = @UID )  ", idParam1);
            //rc = true;
        }
        #endregion

        public string CreateAuthCode(string userID, int ClientID, string CreatedBy)
        {
            string rc = "";
     // CREATE PROCEDURE webuser.NewAuthCode
     //@UserID nvarchar(128),
     //@ClientID int,
     //@CreatedBy nvarchar(128),
     //@AuthCode  CHAR(12) output
 

             var userIDParam = new SqlParameter
            {
                ParameterName = "UserID",
                Value = userID
            };

            var ClientIDParam = new SqlParameter
            {
                ParameterName = "ClientID",
                DbType = System.Data.DbType.Int32,
                Value = ClientID
            };

            var CreatedByParam = new SqlParameter
            {
                ParameterName = "CreatedBy",
                Value = CreatedBy
            };

            var AuthCodeParam = new SqlParameter
            {
                ParameterName = "AuthCode",
                Direction = System.Data.ParameterDirection.Output,
                Value = rc
            };

            try
            {
                
                _dc.Database.ExecuteSqlCommand("webuser.NewAuthCode @UserID=@UserID,@ClientID=@ClientID,@CreatedBy=@CreatedBy,@AuthCode=@AuthCode", userIDParam, ClientIDParam,CreatedByParam ,AuthCodeParam);
               
            }
            catch (Exception e)
            {
                rc = "Error: " + e.Message;
            }
            return rc;
        } // END AddRoleToUser

    }




}
