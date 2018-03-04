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

namespace ZbcXxgl.TF.ZbcJxDBUtility
{
    public class lsDatFlowChartData
    {
        #region 属性
        public int ID;
        public int JiHuaID;
        public int JDID;
        public int OperateGroup;
        public string KaiGongID;
        public string KaiGongName;
        public string WanGongID;
        public string WanGongName;
        public string RepairState;
        public string Remarks;
        public DateTime? KaiGongDate;
        public DateTime? WanGongDate;
     
        #endregion 属性

        #region 构造函数
        public lsDatFlowChartData()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public lsDatFlowChartData(int id)
        {
            string strSql = "select * from lsDatFlowChartData where ID=@ID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ID",id)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static lsDatFlowChartData SetValue(lsDatFlowChartData IDFCD, DataRow dr)
        {
            if (dr!=null)
            {
                IDFCD.KaiGongID = dr["KaiGongID"].ToString();
                IDFCD.KaiGongName = dr["KaiGongName"].ToString();
                IDFCD.WanGongID = dr["WanGongID"].ToString();
                IDFCD.WanGongName = dr["WanGongName"].ToString();
                IDFCD.RepairState = dr["RepairState"].ToString();
                IDFCD.Remarks = dr["Remarks"].ToString();
                IDFCD.ID = PageBase.static_ext_int(dr["ID"].ToString());
                IDFCD.JiHuaID = PageBase.static_ext_int(dr["JiHuaID"].ToString());
                IDFCD.JDID = PageBase.static_ext_int(dr["JDID"].ToString());
                IDFCD.OperateGroup = PageBase.static_ext_int(dr["OperateGroup"].ToString());
                IDFCD.WanGongDate = PageBase.static_ext_date(dr["WanGongDate"].ToString());
                IDFCD.KaiGongDate = PageBase.static_ext_date(dr["KaiGongDate"].ToString());
              
            }
            return IDFCD;
        }
 
        #endregion 构造函数

        #region 扩展方法
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="jcid"></param>
        ///// <returns></returns>
        //public static DataTable GetAllBearingAlarmDT(int jcid)
        //{
        //    string strSql = "select * from lsDatFlowChartData where JiCheID=@JiCheID";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("JiCheID",jcid)
        //                               };
        //    return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0];
        //}

        public static List<lsDatFlowChartData> GetFlowChartDataList(int jhid, int jdid)
        {
            string strSql = "select * from lsDatFlowChartData where JiHuaID=@JiHuaID and JDID=@JDID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiHuaID",jhid),
                                           new SqlParameter("JDID",jdid)
                                       };
            return ListValue(SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0]);
        }

        public static List<lsDatFlowChartData> GetFlowChartDataList(int jhid)
        {
            string strSql = "select * from lsDatFlowChartData where JiHuaID=@JiHuaID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiHuaID",jhid)
                                       };
            return ListValue(SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0]);
        }


        /// <summary>
        /// 返回list对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<lsDatFlowChartData> ListValue(DataTable dt)
        {
            List<lsDatFlowChartData> FlowChartDataList = new List<lsDatFlowChartData>();
            lsDatFlowChartData FlowChart;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    FlowChart = new lsDatFlowChartData();
                    FlowChartDataList.Add(SetValue(FlowChart, dt.Rows[i]));
                }
            }
            return FlowChartDataList;
        }
        #endregion
    }
}
