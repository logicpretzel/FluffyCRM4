using FluffyCRM.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace FluffyCRM.DAL
{
    public class CardsDAL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public Card getCard(int id)
        {
            var idParam = new SqlParameter
            {
                ParameterName = "id",
                Value = id
            };
            var _card = db.Database.SqlQuery<Card>("exec dbo.getCard @id", idParam).SingleOrDefault();
            return _card;
        }
        /*
         		    @CreateDt datetime
           ,@ProjectId int
           ,@EntityId int
           ,@SprintId int
           ,@Title nvarchar(255)
           ,@Detail varchar(max)
           ,@ExpStartDt datetime
           ,@ExpStopDt datetime
           ,@BudgetHours decimal(18,2)
           ,@ParentId int
           ,@CatId int
           ,@BackgroundColor int
           ,@StatusId int

             */
        public int AddCard(CardAddViewModel card)
        {
            int rc = 0;
            SqlParameter catId = new SqlParameter
            {
                ParameterName = "@CatId",
                Value = card.CatId == null ? 0 : card.CatId
            };

          

            SqlParameter title = new SqlParameter("@Title", System.Data.SqlDbType.VarChar)
            {
                Value = card.Title //!=null ? card.Title : ""
            };


            SqlParameter detail = new SqlParameter("@Detail", string.IsNullOrEmpty(card.Detail) ? DBNull.Value : (object)card.Detail);


            SqlParameter entityId = new SqlParameter
            {
                ParameterName = "@EntityId",
                Value = card.EntityId == null ? 0 : card.EntityId//
            };

            SqlParameter budgetHours = new SqlParameter("@BudgetHours", card.BudgetHours == null ? DBNull.Value : (object)card.BudgetHours);
            SqlParameter expStartDt = new SqlParameter("@ExpStartDt", card.ExpStartDt == null ? SqlDateTime.Null : (object)card.ExpStartDt)
            ;

            SqlParameter expStopDt = new SqlParameter("@ExpStopDt", card.ExpStopDt == null ? SqlDateTime.Null : (object)card.ExpStopDt)
            ;

            SqlParameter projectId = new SqlParameter
            {
                ParameterName = "@ProjectId",
                Value = card.ProjectId == null ? 0 : card.ProjectId//
            };

            SqlParameter sprintId = new SqlParameter
            {
                ParameterName = "@SprintId",
                Value = card.SprintId == null ? 0 : card.SprintId//
            };

            SqlParameter statusId = new SqlParameter
            {
                ParameterName = "@StatusId",
                Value = card.StatusId //
            };
            string sql = "INSERT INTO[dbo].[Cards]  ([CreateDt]  ,[ProjectId]   ,[EntityId] ,[Title] ,[Detail]  ,[SprintId] ,[ExpStartDt] ,[ExpStopDt] ,[BudgetHours]  ,[CatId] ,[StatusId])"
                       + "                    VALUES (getdate()  ,@ProjectId    ,@EntityId  ,@Title  ,@Detail   ,@SprintId  ,@ExpStartDt  ,@ExpStopDt  ,@BudgetHours   ,@CatId  ,@StatusId )";
            rc = db.Database.ExecuteSqlCommand(sql, projectId, entityId, title, detail, sprintId, expStartDt, expStopDt, budgetHours, catId, statusId );
            return rc;
        }

      

    }
}