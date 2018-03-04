using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TF.RunSafty.Logic
{
    /// <summary>
    ///Base_Branch_TrainFilter功能：
    /// </summary>
    public class Base_Branch_TrainFilter
    {
        #region 属性
        public int nID;
        public string strTrainNo;

        #endregion 属性

        #region 构造函数
        public Base_Branch_TrainFilter()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public Base_Branch_TrainFilter(string id)
        {
            string strSql = "select * from TAB_Base_Branch_TrainFilter where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",PageBase.static_ext_int(id))
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static Base_Branch_TrainFilter SetValue(Base_Branch_TrainFilter TrainFilter, DataRow dr)
        {
            if (dr!=null)
            {
                TrainFilter.nID = PageBase.static_ext_int(dr["nID"]);
                TrainFilter.strTrainNo = dr["strTrainNo"].ToString();
            }
            return TrainFilter;
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_Base_Branch_TrainFilter (strTrainNo) values (@strTrainNo)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainNo",strTrainNo)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Base_Branch_TrainFilter set strTrainNo = @strTrainNo where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",nID),
                                           new SqlParameter("strTrainNo",strTrainNo)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_Base_Branch_TrainFilter where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",PageBase.static_ext_int(strid))
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string trainno, string id)
        {
            string strSql = "select count(*) from TAB_Base_Branch_TrainFilter where strTrainNo=@trainno ";
            if (id != "")
            {
                strSql += " and nID <> @id ";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("trainno",trainno),
                                           new SqlParameter("id",id)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
      
        ///// <summary>
        ///// 返回list对象
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public static List<Base_Branch_TrainFilter> ListValue(DataTable dt)
        //{
        //    List<Base_Branch_TrainFilter> planparamList = new List<Base_Branch_TrainFilter>();
        //    Base_Branch_TrainFilter planparam;
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            planparam = new Base_Branch_TrainFilter();
        //            planparam = SetValue(planparam, dt.Rows[i]);
        //            planparamList.Add(planparam);
        //        }
        //    }
        //    return planparamList;
        //}
        #endregion
    }
}
