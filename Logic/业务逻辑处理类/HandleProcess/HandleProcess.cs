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
using TF.RunSafty.Logic;

namespace TF.RunSafty.Logic
{
    public class HandleProcess
    {
        #region 属性
        public string strID;
        public string strProcessName;
        public string strZBCID;
        public string strUnidID;
        public string dtPostTime;
        #endregion 属性

        #region 构造函数
        public HandleProcess()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public HandleProcess(string strid)
        {
            string strSql = "select * from View_HandleProcess where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strID = dt.Rows[0]["strID"].ToString();
                strProcessName = dt.Rows[0]["strProcessName"].ToString();
                strZBCID = dt.Rows[0]["strZBCID"].ToString();
                dtPostTime = dt.Rows[0]["dtPostTime"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_HandleProcess (strID,strProcessName,strZBCID,strUnidID) values (@strID,@strProcessName,@strZBCID,@strUnidID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",guid),
                                           new SqlParameter("strProcessName",strProcessName),
                                           new SqlParameter("strZBCID",strZBCID),
                                           new SqlParameter("strUnidID",strUnidID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_HandleProcess set strProcessName = @strProcessName,strZBCID=@strZBCID,strUnidID=@strUnidID where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strID),
                                           new SqlParameter("strProcessName",strProcessName),
                                           new SqlParameter("strUnidID",strUnidID),
                                           new SqlParameter("strZBCID",strZBCID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_HandleProcess where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strid, string Name)
        {
            string strSql = "select count(*) from TAB_HandleProcess where strProcessName=@strProcessName ";
            if (strid != "")
            {
                strSql += " and strID <> @strID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid),
                                           new SqlParameter("strProcessName",Name)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetAllHandleProcess(string uid)
        {
            string strSql = "select * from TAB_HandleProcess where 1=1";
            if (uid != "")
            {
                strSql += " and strID = @uid ";
            }
            strSql += " order by nid ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("uid",uid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }
        /// <summary>
        /// 根据整备场id
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string strHandleProcessfromzbcid(string zbcid,string locotype)
        {
            string strSql = "select strID from VIEW_ProceeCase where 1=1";
            if (zbcid != "")
            {
                strSql += " and strZBCID = @strZBCID ";
            }
            strSql += locotype != "" ? " and strTrainType=@strTrainType" : "";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strZBCID",zbcid),
                                           new SqlParameter("strTrainType",locotype)
                                       };
            return PageBase.static_ext_string(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams));
        }
        /// <summary>
        /// 根据整备场id获取流程至步骤
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static DataTable GetProcessfromzbcid(string zbcid,string cx)
        {
            string strSql = "select * from VIEW_Process where 1=1 and strTrainType=@cx";
            if (zbcid != "")
            {
                strSql += " and strZBCID = @zbcid ";
            }
            strSql += " order by CaseOrder,StepOrder ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("zbcid",zbcid),
                                           new SqlParameter("cx",cx)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }
        /// <summary>
        /// 根据环节id获取流程至步骤
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static DataTable GetProcessfromCaseid(string caseid)
        {
            string strSql = "select * from View_StepRelation where 1=1";
            if (caseid != "")
            {
                strSql += " and strCaseID = @caseid ";
            }
            strSql += " order by nOrder ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("caseid",caseid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }
        /// <summary>
        /// 根据整备场id获取流程至环节
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static DataTable GetProcessCasefromzbcid(string zbcid,string cx)
        {
            string strSql = "select * from VIEW_ProceeCase where 1=1 and strTrainType=@cx";
            if (zbcid != "")
            {
                strSql += " and strZBCID = @zbcid ";
            }
            strSql += " order by nOrder ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("zbcid",zbcid),
                                            new SqlParameter("cx",cx)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }
        /// <summary>
        /// 根据环节id获取是否所有步骤已完成 false为全完成
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool boolCheckStepFinish(DataRow dr, int jcid)
        {
            SqlParameter[] hssqlParams = {
                                           new SqlParameter("stepid",dr["stepID"].ToString()),
                                           new SqlParameter("jcid",jcid)
                                       };
            int type = PageBase.static_ext_int(dr["nStepType"].ToString());
            if (dr["nEnabled"].ToString() == "0")
            {
                return true;
            }
            if (type >= 7 && type <= 99&& type!=17)
            {
                string sql = @"select count(*) from View_CheckStandardHandleFinish where strID = @stepid and nEnabled=1 and JiCheID=@jcid and nState=1;";
                if (PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, sql, hssqlParams)) <= 0)
                {
                    return false;
                }
            }
            else
            {
                switch (type)
                {
                    case 0://风速 只检验整备后风速
                        if (dr["nCaseType"].ToString() == "2")
                        {
                            string Sql2 = @"select strID,JiCheID from View_CheckFengSuFinish where strID = @stepid and nEnabled=1 and JiCheID=@jcid and nFinalIsQualified=1 and nSstate=1 group by strID,JiCheID;";
                            if (PageBase.static_ext_int(SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, Sql2, hssqlParams).Tables[0].Rows.Count) <= 0)
                            {
                                return false;
                            }
                        }
                        break;
                    case 1://滤网

                        ///读取手持机卡控 是否启用该步骤
                        if (SysConfig.GetSingleSysconfig("Scjkk", "Lw") == "True")
                        {
                            lsDatLvWang lsdatlv = lsDatLvWang.getLastLvWangJianCeRiQi("","",jcid);
                            TimeSpan ts = PageBase.diffTimeReturnTimeSpan(lsdatlv.genghuanriqi, DateTime.Now);
                            if (ts.Days > lsdatlv.nGhts)
                            {
                                string Sql1 = @"select count(*) from View_ChecklvWangFinish where strID = @stepid and nEnabled=1 and JiCheID=@jcid and nState=1;";
                                if (PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, Sql1, hssqlParams)) <= 0)
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case 2://单节
                        string Sql3 = @"select count(*) from View_CheckDanJieFinish where strID = @stepid and nEnabled=1 and JiCheID=@jcid and nState=1;";
                        if (PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, Sql3, hssqlParams)) <= 0)
                        {
                            return false;
                        }
                        break;
                    case 3://直供电
                        string Sql4 = @"select strID,JiCheID from View_CheckZhiGongDianFinish where strID = @stepid and nEnabled=1 and JiCheID=@jcid and nSstate=1 group by strID,JiCheID;";
                        if (PageBase.static_ext_int(SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, Sql4, hssqlParams).Tables[0].Rows.Count) <= 0)
                        {
                            return false;
                        }
                        break;
                    case 4://瓷瓶
                        string Sql5 = @"select count(*) from View_CheckCiPingFinish where strID = @stepid and nEnabled=1 and JiCheID=@jcid and nState=1;";
                        if (PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, Sql5, hssqlParams)) <= 0)
                        {
                            return false;
                        }
                        break;
                    case 5://列车管
                        if (SysConfig.GetSingleSysconfig("Scjkk", "Lcg") == "True")
                        {
                            lsDatLieCheGuan lsdatlcg = lsDatLieCheGuan.getLastLcgJianCeRiQi("", "", jcid);
                            TimeSpan timespan = PageBase.diffTimeReturnTimeSpan(lsdatlcg.JianChaDate, DateTime.Now);
                            if (timespan.Days > lsdatlcg.nGhts)
                            {
                                string Sql6 = @"select count(*) from View_CheckLiCheGuanFinish where strID = @stepid and nEnabled=1 and JiCheID=@jcid and nState=1;";
                                if (PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, Sql6, hssqlParams)) <= 0)
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case 6://受电弓
                        string Sql7 = @"select count(*) from View_CheckHuaBanFinish where strID = @stepid and nEnabled=1 and JiCheID=@jcid and nState=1;";
                        if (PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, Sql7, hssqlParams)) <= 0)
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }
        #endregion
    }
}
