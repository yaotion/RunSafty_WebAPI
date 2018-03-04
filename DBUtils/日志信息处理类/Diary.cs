using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TF.RunSafty.DBUtils
{
   public class Diary:DBOperator
   {

       public Diary(string ConnectionString)
           :base(ConnectionString){}

       #region ========================================获得数据列表========================================
       /// <summary>
        ///获得数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable QueryCheckDiary(int pageIndex, int pageCount, string Begintime, string EndTimeStr,string UsersId)
        {
            StringBuilder strSqlWhere = new StringBuilder();
            strSqlWhere.Append(" and 1=1");
            if (Begintime != "" && Begintime != null)
                strSqlWhere.Append(" and convert(varchar, addtime, 23)>='" + Begintime + "'");

            if (EndTimeStr != "" && EndTimeStr != null)
                strSqlWhere.Append(" and convert(varchar, addtime, 23)<='" + EndTimeStr + "'");

            if (UsersId != "" && UsersId != null)
                strSqlWhere.Append(" and UserId='" + UsersId + "'");


            string strSql = @"select top " + pageCount.ToString() + " * from VIEW_Diary where Id not in (select top " + ((pageIndex - 1) * pageCount).ToString() + @" Id from Tab_Diary where 1=1" + strSqlWhere.ToString() + " order by addtime desc)" + strSqlWhere.ToString() + " order by addtime desc";
            return SqlHelps.ReturnDataSet(ConnectionString, CommandType.Text, strSql).Tables[0];
        }
        #endregion

       #region ========================================获得数据列表个数====================================
        /// <summary>
        /// 获得数据列表个数
        /// </summary>
        /// <returns></returns>
        public int QueryCheckDiaryCount(string Begintime, string EndTimeStr)
        {

            StringBuilder strSqlWhere = new StringBuilder();
            strSqlWhere.Append(" 1=1");
            if (Begintime != "" && Begintime != null)
                strSqlWhere.Append(" and convert(varchar, addtime, 23)>='" + Begintime + "'");

            if (EndTimeStr != "" && EndTimeStr != null)
                strSqlWhere.Append(" and convert(varchar, addtime, 23)<='" + EndTimeStr + "'");

            string strSql = "select count(*) from VIEW_Diary where " + strSqlWhere + "";
            return Convert.ToInt32(SqlHelps.ExecuteScalar(ConnectionString, CommandType.Text, strSql));
        }
        #endregion

       #region ========================================获取对象实体========================================
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Entry.Diary GetModel(int Id)
        {
            string strSql = "select * from Tab_Diary where Id='" + Id + "'";
            DataSet ds = SqlHelps.ReturnDataSet(ConnectionString, CommandType.Text, strSql);

            TF.RunSafty.Entry.Diary model = new TF.RunSafty.Entry.Diary();

            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Entry.Diary DataRowToModel(DataRow row)
        {
            TF.RunSafty.Entry.Diary model = new TF.RunSafty.Entry.Diary();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["AddTime"] != null && row["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(row["AddTime"].ToString());
                }
                if (row["DiaryContent"] != null)
                {
                    model.DiaryContent = row["DiaryContent"].ToString();
                }
                if (row["FileName"] != null)
                {
                    model.FileName = row["FileName"].ToString();
                }
                if (row["FileUrl"] != null)
                {
                    model.FileUrl = row["FileUrl"].ToString();
                }
            }
            return model;
        }
        
        #endregion

       #region ========================================添加一条数据========================================
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TF.RunSafty.Entry.Diary model)
        {
            string strSql = @"insert into Tab_Diary (AddTime,DiaryContent,UserId) values 
                            (@AddTime,@DiaryContent,@UserId)";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("AddTime",model.AddTime),
                                              new SqlParameter("DiaryContent",model.DiaryContent),
                                              new SqlParameter("UserId",model.UserId)
                                          };
            object obj = SqlHelps.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql, sqlParameter);
            if (DBNull.Value.Equals(obj))
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        } 
        #endregion

       #region ========================================更新一条数据========================================
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(TF.RunSafty.Entry.Diary model)
        {
            string strSql = @"update Tab_Diary set AddTime=@AddTime,DiaryContent=@DiaryContent,UserId=@UserId where Id=@id";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("id",model.Id),
                                              new SqlParameter("AddTime",model.AddTime),
                                               new SqlParameter("DiaryContent",model.DiaryContent),
                                               new SqlParameter("UserId",model.UserId)
                                          };
            return SqlHelps.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql, sqlParameter);
        }
        
        #endregion

       #region ========================================删除一条数据========================================
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public int Delete(string id)
        {
            string strSql = "delete Tab_Diary where Id= @id";
            SqlParameter[] sqlParameter = {
                                              new SqlParameter("id",id)
                                          };
            return SqlHelps.ExecuteNonQuery(ConnectionString, CommandType.Text, strSql, sqlParameter);
        }
        #endregion

       #region ====================================通过时间查询出在这段时间内每一天的写日志的数量====================================
        /// <summary>
        ///通过时间查询出在这段时间内每一天的数量
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetallCount(string beginTime, string endTime)
        {
            string strSql = "select convert(varchar(100), AddTime, 23) as strDate,count(*) as countNum from Tab_Diary where AddTime>='" + beginTime + "' and AddTime<='" + endTime + "' group by convert(varchar(100), AddTime, 23) order by strDate desc";
            return SqlHelps.ReturnDataSet(ConnectionString, CommandType.Text, strSql).Tables[0];
        }
        #endregion
    }
}
