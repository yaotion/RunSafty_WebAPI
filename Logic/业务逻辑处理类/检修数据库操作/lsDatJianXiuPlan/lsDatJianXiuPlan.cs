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
    public class lsDatJianXiuPlan
    {
        #region 属性
        public int ID;
        public string CheXing;
        public string CheXingDaiHao;
        public string CheHao;
        public string XiuCheng;
        public string XiuChengDaiHao;
        public DateTime? JiHuaDate;
        public string JiHuaCheJian;
        public DateTime? ShiJiDate;
        public string ShiJiCheJian;
        public string ShuDi;
        public string ZhiDingRenID;
        public string ZhiDingRenName;
        public DateTime? ZhiDingDate;
        public string KaiGongRenID;
        public string KaiGongRenName;
        public DateTime? KaiGongDate;
        public string QuXiaoRenID;
        public string QuXiaoRenName;
        public DateTime? QuXiaoDate;
        public string QuXiaoYuanYin;
        public string JiaoCheRenID;
        public string JiaoCheRenName;
        public DateTime? JiaoCheDate;
        public int PositionId;
        public int PositionCreateId;
        public string PositionCreateName;
        public DateTime? PositionCreateDate;
        public string BeiZhu;
        public int ZhuangTai;
        public string RepairCycle;
        #endregion 属性

        #region 构造函数
        public lsDatJianXiuPlan()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public lsDatJianXiuPlan(int id)
        {
            string strSql = "select * from lsDatJianXiuPlan where ID=@ID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ID",id)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        public static lsDatJianXiuPlan SetValue(lsDatJianXiuPlan jxplan, DataRow dr)
        {
            if (dr!=null)
            {
                jxplan.CheXing = dr["CheXing"].ToString();
                jxplan.CheXingDaiHao = dr["CheXingDaiHao"].ToString();
                jxplan.CheHao = dr["CheHao"].ToString();
                jxplan.XiuCheng = dr["XiuCheng"].ToString();
                jxplan.XiuChengDaiHao = dr["XiuChengDaiHao"].ToString();
                jxplan.JiHuaCheJian = dr["JiHuaCheJian"].ToString();
                jxplan.ShiJiCheJian = dr["ShiJiCheJian"].ToString();
                jxplan.ShuDi = dr["ShuDi"].ToString();
                jxplan.ZhiDingRenID = dr["ZhiDingRenID"].ToString();
                jxplan.ZhiDingRenName = dr["ZhiDingRenName"].ToString();
                jxplan.KaiGongRenID = dr["KaiGongRenID"].ToString();
                jxplan.KaiGongRenName = dr["KaiGongRenName"].ToString();
                jxplan.QuXiaoRenID = dr["QuXiaoRenID"].ToString();
                jxplan.QuXiaoRenName = dr["QuXiaoRenName"].ToString();
                jxplan.QuXiaoYuanYin = dr["QuXiaoYuanYin"].ToString();
                jxplan.JiaoCheRenID = dr["JiaoCheRenID"].ToString();
                jxplan.JiaoCheRenName = dr["JiaoCheRenName"].ToString();
                jxplan.PositionCreateName = dr["PositionCreateName"].ToString();
                jxplan.BeiZhu = dr["BeiZhu"].ToString();
                jxplan.RepairCycle = dr["RepairCycle"].ToString();
                jxplan.ID = PageBase.static_ext_int(dr["ID"].ToString());
                jxplan.PositionId = PageBase.static_ext_int(dr["PositionId"].ToString());
                jxplan.PositionCreateId = PageBase.static_ext_int(dr["PositionCreateId"].ToString());
                jxplan.ZhuangTai = PageBase.static_ext_int(dr["ZhuangTai"].ToString());
                jxplan.JiHuaDate = PageBase.static_ext_date(dr["JiHuaDate"].ToString());
                jxplan.ShiJiDate = PageBase.static_ext_date(dr["ShiJiDate"].ToString());
                jxplan.ZhiDingDate = PageBase.static_ext_date(dr["ZhiDingDate"].ToString());
                jxplan.KaiGongDate = PageBase.static_ext_date(dr["KaiGongDate"].ToString());
                jxplan.QuXiaoDate = PageBase.static_ext_date(dr["QuXiaoDate"].ToString());
                jxplan.JiaoCheDate = PageBase.static_ext_date(dr["JiaoCheDate"].ToString());
                jxplan.PositionCreateDate = PageBase.static_ext_date(dr["PositionCreateDate"].ToString());
            }
            return jxplan;
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
        //    string strSql = "select * from lsDatJianXiuPlan where JiCheID=@JiCheID";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("JiCheID",jcid)
        //                               };
        //    return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0];
        //}

        public static List<lsDatJianXiuPlan> GetAlljxplanList(int id)
        {
            string strSql = "select * from lsDatJianXiuPlan where ID=@ID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ID",id)
                                       };
            return ListValue(SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("86"), CommandType.Text, strSql, sqlParams).Tables[0]);
        }

        /// <summary>
        /// 返回list对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<lsDatJianXiuPlan> ListValue(DataTable dt)
        {
            List<lsDatJianXiuPlan> jxplanList = new List<lsDatJianXiuPlan>();
            lsDatJianXiuPlan jxplan;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    jxplan = new lsDatJianXiuPlan();
                    jxplanList.Add(SetValue(jxplan,dt.Rows[i]));
                }
            }
            return jxplanList;
        }
        #endregion
    }
}
