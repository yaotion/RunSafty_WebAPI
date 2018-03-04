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
    ///Base_PlanParam功能：提供车间信息增删改查
    /// </summary>
    public class Base_PlanParam
    {
        #region 属性
        public int nID;
        public DateTime? dtAutoSaveTime1;
        public DateTime? dtAutoSaveTime2;
        public DateTime? dtPlanBeginTime;
        public DateTime? dtPlanEndTime;
        public DateTime? dtKeepSleepTime;
        public string strWorkShopGUID;
        public string strWorkShopName;
        public int bEnableSleepCheck = 0;
        public string strWebHost;

        #endregion 属性

        #region 构造函数
        public Base_PlanParam()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public Base_PlanParam(string id)
        {
            string strSql = "select * from VIEW_Base_PlanParam where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",PageBase.static_ext_int(id))
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static Base_PlanParam SetValue(Base_PlanParam PlanParam, DataRow dr)
        {
            if (dr!=null)
            {
                PlanParam.nID = PageBase.static_ext_int(dr["nID"]);
                PlanParam.dtAutoSaveTime1 = PageBase.static_ext_date(dr["dtAutoSaveTime1"]);
                PlanParam.dtAutoSaveTime2 = PageBase.static_ext_date(dr["dtAutoSaveTime2"]);
                PlanParam.dtPlanBeginTime = PageBase.static_ext_date(dr["dtPlanBeginTime"]);
                PlanParam.dtPlanEndTime = PageBase.static_ext_date(dr["dtPlanEndTime"]);
                PlanParam.dtKeepSleepTime = PageBase.static_ext_date(dr["dtKeepSleepTime"]);
                PlanParam.strWorkShopGUID = dr["strWorkShopGUID"].ToString();
                PlanParam.strWorkShopName = dr["strWorkShopName"].ToString();
                PlanParam.strWebHost = dr["strWebHost"].ToString();
                PlanParam.bEnableSleepCheck = dr["bEnableSleepCheck"].ToString()=="True"?1:0;
            }
            return PlanParam;
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_Base_PlanParam (dtAutoSaveTime1,dtAutoSaveTime2,dtPlanBeginTime,dtPlanEndTime,dtKeepSleepTime,strWorkShopGUID,strWebHost,bEnableSleepCheck) values (@dtAutoSaveTime1,@dtAutoSaveTime2,@dtPlanBeginTime,@dtPlanEndTime,@dtKeepSleepTime,@strWorkShopGUID,@strWebHost,@bEnableSleepCheck)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("dtAutoSaveTime1",dtAutoSaveTime1),
                                           new SqlParameter("dtAutoSaveTime2",dtAutoSaveTime2),
                                           new SqlParameter("dtPlanBeginTime",dtPlanBeginTime),
                                           new SqlParameter("dtPlanEndTime",dtPlanEndTime),
                                           new SqlParameter("dtKeepSleepTime",dtKeepSleepTime),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("strWebHost",strWebHost),
                                           new SqlParameter("bEnableSleepCheck",bEnableSleepCheck)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Base_PlanParam set dtAutoSaveTime1 = @dtAutoSaveTime1,dtAutoSaveTime2=@dtAutoSaveTime2,dtPlanBeginTime=@dtPlanBeginTime,dtPlanEndTime=@dtPlanEndTime,dtKeepSleepTime=@dtKeepSleepTime,strWebHost=@strWebHost,bEnableSleepCheck=@bEnableSleepCheck where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",nID),
                                           new SqlParameter("dtAutoSaveTime1",dtAutoSaveTime1),
                                           new SqlParameter("dtAutoSaveTime2",dtAutoSaveTime2),
                                           new SqlParameter("dtPlanBeginTime",dtPlanBeginTime),
                                           new SqlParameter("dtPlanEndTime",dtPlanEndTime),
                                           new SqlParameter("dtKeepSleepTime",dtKeepSleepTime),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("strWebHost",strWebHost),
                                           new SqlParameter("bEnableSleepCheck",bEnableSleepCheck)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_Base_PlanParam where nID=@nID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nID",PageBase.static_ext_int(strid))
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string workshopid,string id)
        {
            string strSql = "select count(*) from TAB_Base_PlanParam where strWorkShopGUID=@strWorkShopGUID ";
            if (id != "")
            {
                strSql += " and nID = @id ";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strWorkShopGUID",workshopid),
                                           new SqlParameter("id",PageBase.static_ext_int(id))
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetAllPlanParam()
        {
            string strSql = "select * from VIEW_Base_PlanParam";
            strSql += " order by nid ";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 返回list对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Base_PlanParam> ListValue(DataTable dt)
        {
            List<Base_PlanParam> planparamList = new List<Base_PlanParam>();
            Base_PlanParam planparam;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    planparam = new Base_PlanParam();
                    planparam = SetValue(planparam, dt.Rows[i]);
                    planparamList.Add(planparam);
                }
            }
            return planparamList;
        }
        #endregion
    }
}
