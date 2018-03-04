using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.DAL
{
    public class Tab_Config_LED_Class_Order
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TF.RunSafty.Model.Tab_Config_LED_Class_Order model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_Config_LED_Class_Order(");
            strSql.Append("strCheJianGUID,strCheJianNickName,strJiaoLuGUID,strJiaoLuNickName,strTitle,strKeHuDuanGUID)");
            strSql.Append(" values (");
            strSql.Append("@strCheJianGUID,@strCheJianNickName,@strJiaoLuGUID,@strJiaoLuNickName,@strTitle,@strKeHuDuanGUID)");
            SqlParameter[] parameters = {
					new SqlParameter("@strCheJianGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strCheJianNickName", SqlDbType.VarChar,50),
					new SqlParameter("@strJiaoLuGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strJiaoLuNickName", SqlDbType.VarChar,50),
					new SqlParameter("@strTitle", SqlDbType.VarChar,50),
                   new SqlParameter("@strKeHuDuanGUID", SqlDbType.VarChar,50)};
            parameters[0].Value = model.strCheJianGUID;
            parameters[1].Value = model.strCheJianNickName;
            parameters[2].Value = model.strJiaoLuGUID;
            parameters[3].Value = model.strJiaoLuNickName;
            parameters[4].Value = model.strTitle;
            parameters[5].Value = model.strKeHuDuanGUID;

            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }



        public DataTable QueryCheckList(int pageIndex, int pageCount, string strCheJianGUID, string strJiaoluGUID, string strTitle)
        {
            StringBuilder strSqlWhere = new StringBuilder();



            if (strJiaoluGUID != "" && strJiaoluGUID != "1")
                strSqlWhere.Append(" and strCheJianGUID= '" + strJiaoluGUID + "'");

            if (strJiaoluGUID != "" && strJiaoluGUID != "1")
                strSqlWhere.Append(" and strJiaoLuGUID= '" + strJiaoluGUID + "'");

            if (strTitle != "")
                strSqlWhere.Append(" and strTitle like '%" + strTitle + "%'");


            string strSql = @"select top " + pageCount.ToString()
                + " * from VIEW_Config_LED_Class_Order where nID not in (select top " + ((pageIndex - 1) * pageCount).ToString() + @" nID from VIEW_Config_LED_Class_Order where 1=1"
                + strSqlWhere.ToString() + " order by nID desc)" + strSqlWhere.ToString() + " order by nID desc";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }


        public int QueryCheckListCount(string strCheJianGUID, string strJiaoluGUID, string strTitle)
        {

            StringBuilder strSqlWhere = new StringBuilder();


            if (strJiaoluGUID != "" && strJiaoluGUID != "1")
                strSqlWhere.Append(" and strCheJianGUID= '" + strJiaoluGUID + "'");

            if (strJiaoluGUID != "" && strJiaoluGUID != "1")
                strSqlWhere.Append(" and strJiaoLuGUID= '" + strJiaoluGUID + "'");

            if (strTitle != "")
                strSqlWhere.Append(" and strTitle like '%" + strTitle + "%'");


            string strSql = "select count(*) from VIEW_Config_LED_Class_Order where 1=1  " + strSqlWhere + "";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
        }

        

        public bool Delete(string nid)
        {
            string strSql = "delete Tab_Config_LED_Class_Order where nid = @nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nid",nid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        public bool DeleteAllKud(string strKeHuDuanGUID)
        {
            string strSql = "delete Tab_Config_LED_Class_Order where strKeHuDuanGUID = @strKeHuDuanGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strKeHuDuanGUID",strKeHuDuanGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }




        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TF.RunSafty.Model.Tab_Config_LED_Class_Order DataRowToModel(DataRow row)
        {
            TF.RunSafty.Model.Tab_Config_LED_Class_Order model = new TF.RunSafty.Model.Tab_Config_LED_Class_Order();
            if (row != null)
            {
                if (row["nid"] != null && row["nid"].ToString() != "")
                {
                    model.nid = int.Parse(row["nid"].ToString());
                }
                if (row["strCheJianGUID"] != null)
                {
                    model.strCheJianGUID = row["strCheJianGUID"].ToString();
                }
                if (row["strCheJianName"] != null)
                {
                    model.strCheJianName = row["strCheJianName"].ToString();
                }
                if (row["strCheJianNickName"] != null)
                {
                    model.strCheJianNickName = row["strCheJianNickName"].ToString();
                }
                if (row["strJiaoLuGUID"] != null)
                {
                    model.strJiaoLuGUID = row["strJiaoLuGUID"].ToString();
                }
                if (row["strJiaoLuName"] != null)
                {
                    model.strJiaoLuName = row["strJiaoLuName"].ToString();
                }
                if (row["strJiaoLuNickName"] != null)
                {
                    model.strJiaoLuNickName = row["strJiaoLuNickName"].ToString();
                }
                if (row["strTitle"] != null)
                {
                    model.strTitle = row["strTitle"].ToString();
                }
            }
            return model;
        }


        public DataSet GetClass_Order(string strCheJianGUID)
        {
            string strWhere = string.Format(" strKeHuDuanGUID='{0}' ", strCheJianGUID);
            return GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM VIEW_Config_LED_Class_Order ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

    }
}
