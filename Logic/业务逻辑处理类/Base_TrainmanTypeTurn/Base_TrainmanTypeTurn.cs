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
    ///Base_TrainmanTypeTurn功能：
    /// </summary>
    public class Base_TrainmanTypeTurn
    {
        #region 属性
        public int nID;
        public int nTrainmanTypeID;
        public int nKeHuoID;
        public int nTurnMinutes;
        public int nTurnAlarmMinutes;

        #endregion 属性

        #region 构造函数
        public Base_TrainmanTypeTurn()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public Base_TrainmanTypeTurn(string id)
        {
            string strSql = "select * from VIEW_Base_TrainmanTypeTurn where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",PageBase.static_ext_int(id))
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static Base_TrainmanTypeTurn SetValue(Base_TrainmanTypeTurn trainmantypeturn, DataRow dr)
        {
            if (dr!=null)
            {
                trainmantypeturn.nID = PageBase.static_ext_int(dr["nID"]);
                trainmantypeturn.nTrainmanTypeID = PageBase.static_ext_int(dr["nTrainmanTypeID"]);
                trainmantypeturn.nKeHuoID = PageBase.static_ext_int(dr["nKeHuoID"]);
                trainmantypeturn.nTurnMinutes = PageBase.static_ext_int(dr["nTurnMinutes"]);
                trainmantypeturn.nTurnAlarmMinutes = PageBase.static_ext_int(dr["nTurnAlarmMinutes"]);
            }
            return trainmantypeturn;
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_Base_TrainmanTypeTurn (nTrainmanTypeID,nKeHuoID,nTurnMinutes,nTurnAlarmMinutes) values (@nTrainmanTypeID,@nKeHuoID,@nTurnMinutes,@nTurnAlarmMinutes)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nTrainmanTypeID",nTrainmanTypeID),
                                           new SqlParameter("nKeHuoID",nKeHuoID),
                                           new SqlParameter("nTurnMinutes",nTurnMinutes),
                                           new SqlParameter("nTurnAlarmMinutes",nTurnAlarmMinutes)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Base_TrainmanTypeTurn set nTrainmanTypeID = @nTrainmanTypeID,nKeHuoID=@nKeHuoID,nTurnMinutes=@nTurnMinutes,nTurnAlarmMinutes=@nTurnAlarmMinutes where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",nID),
                                           new SqlParameter("nTrainmanTypeID",nTrainmanTypeID),
                                           new SqlParameter("nKeHuoID",nKeHuoID),
                                           new SqlParameter("nTurnMinutes",nTurnMinutes),
                                           new SqlParameter("nTurnAlarmMinutes",nTurnAlarmMinutes)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_Base_TrainmanTypeTurn where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",PageBase.static_ext_int(strid))
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int trainmantypeid,int kehuoid,string id)
        {
            string strSql = "select count(*) from TAB_Base_TrainmanTypeTurn where nTrainmanTypeID=@nTrainmanTypeID and nKeHuoID=@nKeHuoID";
            if (id != "")
            {
                strSql += " and nID <> @id ";
            }
            SqlParameter[] sqlParams = {
                                            new SqlParameter("nTrainmanTypeID",trainmantypeid),
                                           new SqlParameter("nKeHuoID",kehuoid),
                                           new SqlParameter("id",PageBase.static_ext_int(id))
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
        //public static List<Base_TrainmanTypeTurn> ListValue(DataTable dt)
        //{
        //    List<Base_TrainmanTypeTurn> planparamList = new List<Base_TrainmanTypeTurn>();
        //    Base_TrainmanTypeTurn planparam;
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            planparam = new Base_TrainmanTypeTurn();
        //            planparam = SetValue(planparam, dt.Rows[i]);
        //            planparamList.Add(planparam);
        //        }
        //    }
        //    return planparamList;
        //}
        #endregion
    }
}
