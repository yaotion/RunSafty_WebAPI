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
using TF.RunSafty.Logic;

namespace ZbcXxgl.TF.ZbcJxDBUtility
{
    public class lsDicFlowChart
    {
        #region 属性
        public int ID;
        public int JDNum;
        public int OperateGroup;
        public int KSLevel;
        public int JSLevel;
        public int KSDay;
        public int JSDay;
        public int Status;
        public string LocoType;
        public string JDName;
        public string KSTime;
        public string JSTime;
        public string RepairCycle;

        //状态,0为未开工，1为开工，2为完工,3为提前，4为延期
        public string RepairState;
        //需要验收数量,如果大于0，就表示是一个需要验收的步骤
        public int nAcceptItemCount;
        //质检通过数,如果和nAcceptItemCount相同，就标示已经质检通过
        public int nCheckQualifiedCount;
        //验收通过数量,如果和nAcceptItemCount相同，就标示已经验收通过
        public int nAcceptQualifiedCount;

        //验收name
        public string YanShouName;
        //质检name
        public string ZhiJianName;

        //X坐标
        public int X;
        //Y坐标
        public int Y;
        //验收Y坐标
        public int YanShouY;
        //质检Y坐标
        public int ZhiJianY;
        //是否已计算过坐标
        public bool IsCalcPoint = false;
        //水平方向，True为上面，False为下面
        public bool IsDrawUp = true;

        //宽度
        public int Width;
        //高度
        public int Height;
        //验收高度
        public int YanShoHeight;
        //质检高度
        public int ZhiJianHeight;
        //字体颜色
        public string FontColor;
        ///验收字体颜色
        public string YanShoFontColor;
        //质检字体颜色
        public string ZhiJianFontColor;
        //背景颜色
        public string BackgroundColor;
        //验收框背景颜色
        public string YanShouBackgroundColor;
        //质检框背景颜色
        public string ZhiJianBackgroundColor;
        #endregion 属性

        #region 构造函数
        public lsDicFlowChart()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public lsDicFlowChart(int ID)
        {
            string strSql = "select * from lsDicFlowChart where ID=@ID and Status=1";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ID",ID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static lsDicFlowChart SetValue(lsDicFlowChart dfc, DataRow dr)
        {
            if (dr!=null)
            {
                dfc.LocoType = dr["LocoType"].ToString();
                dfc.JDName = dr["JDName"].ToString();
                dfc.KSTime = dr["KSTime"].ToString();
                dfc.JSTime = dr["JSTime"].ToString();
                dfc.RepairCycle = dr["RepairCycle"].ToString();
                dfc.ID = PageBase.static_ext_int(dr["ID"].ToString());
                dfc.JDNum = PageBase.static_ext_int(dr["JDNum"].ToString());
                dfc.OperateGroup = PageBase.static_ext_int(dr["OperateGroup"].ToString());
                dfc.KSLevel = PageBase.static_ext_int(dr["KSLevel"].ToString());
                dfc.JSLevel = PageBase.static_ext_int(dr["JSLevel"].ToString());
                dfc.KSDay = PageBase.static_ext_int(dr["KSDay"].ToString());
                dfc.JSDay = PageBase.static_ext_int(dr["JSDay"].ToString());
                dfc.Status = PageBase.static_ext_int(dr["Status"].ToString());
            }
            return dfc;
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
        //    string strSql = "select * from lsDicFlowChart where JiCheID=@JiCheID";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("JiCheID",jcid)
        //                               };
        //    return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0];
        //}

        public static List<lsDicFlowChart> GetAllFlowChartList(int Day, string LocoType)
        {
            string strSql = "select * from lsDicFlowChart where LocoType=@LocoType and KSDay<=@Day and JSDay>=@Day and Status=1 and RepairCycle=15";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("Day",Day),
                                           new SqlParameter("LocoType",LocoType)
                                       };
            DataTable dtFlowChart = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0];
            //增加时间段数
            dtFlowChart.Columns.Add("TimeQuantumCount");
            if (dtFlowChart.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFlowChart.Rows)
                {
                    int TimeQuantumCount=0;
                    if (PageBase.static_ext_int(dr["JSDay"]) - Day > 0)
                    {
                        TimeQuantumCount = 16;
                    }
                    else
                    {
                        TimeQuantumCount = FlowChartDraw.DateSubtract(dr["KSTime"].ToString(), dr["JSTime"].ToString());
                    }
                    if (FlowChartDraw.DateComparison("13:00", dr["JSTime"].ToString()) > 0 && FlowChartDraw.DateComparison("13:00", dr["KSTime"].ToString()) < 0)
                    {
                        TimeQuantumCount -= 2;
                    }
                    dr["TimeQuantumCount"] = TimeQuantumCount.ToString();
                }
            }
            return ListValue(dtFlowChart);
        }

        /// <summary>
        /// 返回list对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<lsDicFlowChart> ListValue(DataTable dt)
        {
            DataRow[] drs = dt.Select("1=1", "TimeQuantumCount asc");
            List<lsDicFlowChart> FlowChartList = new List<lsDicFlowChart>();
            lsDicFlowChart dfc;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    dfc = new lsDicFlowChart();
                    FlowChartList.Add(SetValue(dfc, drs[i]));
                }
            }
            return FlowChartList;
        }
        #endregion
    }
}
