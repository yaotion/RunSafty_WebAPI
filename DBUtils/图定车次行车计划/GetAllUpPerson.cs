using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;

namespace TF.RunSafty.DAL
{
   public partial  class GetAllUpPerson
    {
       public DataTable QueryCheckUpPersonList(int pageIndex, int pageCount, string PeoPleName, string bgTime, string EdTime)
        {
            StringBuilder strSqlWhere = new StringBuilder();


            if (bgTime != "")
                strSqlWhere.Append(" and dtModifyTime>'" + bgTime + "'");

            if (EdTime != "")
                strSqlWhere.Append(" and dtModifyTime<'" + EdTime + "'");


            if (PeoPleName != "")
            {
                strSqlWhere.Append(" and (strNewTrainmanName like '%" + PeoPleName + "%' or strOldTrainmanName like '%" + PeoPleName + "%')");
            }


           


            string strSql = @"select top " + pageCount.ToString()
                + " * from VIEW_Sign_UpPerson where nID not in (select top " + ((pageIndex - 1) * pageCount).ToString() + @" nID from VIEW_Sign_UpPerson where 1=1"
                + strSqlWhere.ToString() + " order by nID desc)" + strSqlWhere.ToString() + " order by nID desc";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }


       public int QueryCheckUpPersonCount(string PeoPleName, string bgTime, string EdTime)
        {

            StringBuilder strSqlWhere = new StringBuilder();
            if (bgTime != "")
                strSqlWhere.Append(" and dtModifyTime>'" + bgTime + "'");

            if (EdTime != "")
                strSqlWhere.Append(" and dtModifyTime<'" + EdTime + "'");

            if (PeoPleName != "")
            {
                strSqlWhere.Append(" and (strNewTrainmanName like '%" + PeoPleName + "%' or strOldTrainmanName like '%" + PeoPleName + "%')");
            }

            string strSql = "select count(*) from VIEW_Sign_UpPerson where 1=1  " + strSqlWhere + "";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString()));
        }

    }
}
