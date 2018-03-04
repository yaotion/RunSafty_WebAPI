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

namespace TF.RunSafty.Logic
{
    public class HandleCase
    {
        #region 属性
        public string strID;
        public string strCaseName;
        public int nStandardMinute;
        public int nOrder;
        public string strProcessID;
        public string dtPostTime;
        public int X;
        public int W;
        public int nCaseType;
        #endregion 属性

        #region 构造函数
        public HandleCase()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public HandleCase(string strid)
        {
            string strSql = "select * from TAB_HandleCase where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ReadCase(this,dt.Rows[0]);
            }
        }
        #endregion 构造函数


        //功能:读取环节信息
        private static void ReadCase(HandleCase hcase,DataRow Row)
        {
            hcase.strID = Row["strID"].ToString();
            hcase.strCaseName = Row["strCaseName"].ToString();
            hcase.nStandardMinute = PageBase.static_ext_int(Row["nStandardMinute"].ToString());
            hcase.strProcessID = Row["strProcessID"].ToString();
            hcase.nOrder = PageBase.static_ext_int(Row["nOrder"].ToString());
            hcase.dtPostTime = Row["dtPostTime"].ToString();
            hcase.X = PageBase.static_ext_int(Row["X"].ToString());
            hcase.W = PageBase.static_ext_int(Row["W"].ToString());
            hcase.nCaseType = PageBase.static_ext_int(Row["nCaseType"].ToString());
        }

        //功能:获得机车所有的环节信息
        public static List<HandleCase> getJieCheCaseList(int nJiCheID)
        {
            List<HandleCase> HandleCaseList = new List<HandleCase>();
            string strSql = "select * from VIEW_Jt6JicheProcess where jcid = @JiCheID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",nJiCheID)
                                       };
            DataTable dtCaseList = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            foreach (DataRow drCase in dtCaseList.Rows)
            {
                HandleCase Case = new HandleCase();
                Case.strID = drCase["caseid"].ToString();
                HandleCaseList.Add(Case);
            }
            return HandleCaseList;

        }


        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_HandleCase (strID,strCaseName,nStandardMinute,strProcessID,nOrder,nCaseType)select @strID,@strCaseName,@nStandardMinute,@strProcessID, (count(*)+1) as nder,@nCaseType from TAB_HandleCase where strProcessID=@strProcessID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",guid),
                                           new SqlParameter("strCaseName",strCaseName),
                                           new SqlParameter("nStandardMinute",nStandardMinute),
                                           new SqlParameter("strProcessID",strProcessID),
                                           new SqlParameter("nCaseType",nCaseType)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_HandleCase set strCaseName = @strCaseName,nStandardMinute=@nStandardMinute,nCaseType=@nCaseType where strID=@strID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strID",strID),
                                           new SqlParameter("strCaseName",strCaseName),
                                           new SqlParameter("nStandardMinute",nStandardMinute),
                                           new SqlParameter("nCaseType",nCaseType)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool UpdateXW()
        {
            string strSql = "update TAB_HandleCase set X = @X,W=@W where strID=@strID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("X",X),
                                           new SqlParameter("W",W),
                                           new SqlParameter("strID",strID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_HandleCase where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strid, string Name,string processID)
        {
            string strSql = "select count(*) from TAB_HandleCase where strCaseName=@strCaseName and strProcessID=@strProcessID";
            if (strid != "")
            {
                strSql += " and strID <> @strID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid),
                                           new SqlParameter("strCaseName",Name),
                                           new SqlParameter("strProcessID",processID)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        public bool UpdateSortid()
        {
            string strSql = "update TAB_HandleCase set nOrder=@nOrder where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nOrder",nOrder),
                                           new SqlParameter("strID",strID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        #endregion 增删改

        #region 扩展方法

        /// <summary>
        /// 由流程id获取所有环节
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static DataTable GetAllTAB_HandleCase(string uid)
        {
            string strSql = "select * from TAB_HandleCase where 1=1";
            if (uid != "")
            {
                strSql += " and strProcessID = @uid ";
            }
            strSql += " order by nOrder ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("uid",uid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        /// <summary>
        /// 根据步骤id获取环节id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetCaseidfromPhotostepid(string id)
        {
            string strSql = "select strCaseID from TAB_HandleStep where strID = @id";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("id",id)
                                       };
            return SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).ToString();
        }

        /// <summary>
        /// 根据步骤id获取环节id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetCaseidfromWindstepid(string id)
        {
                string strSql = "select strCaseID from TAB_HandleStep where strID = @id";
                SqlParameter[] sqlParams = {
                                           new SqlParameter("id",id)
                                       };
                return SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).ToString();
        }
        /// <summary>
        /// 根据环节id获取是否所有步骤已完成 false为全完成
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool boolCheckCaseFinish(string id,int zbid)
        {
            string hssql = @"select a.strID,a.nStepType,b.nCaseType from TAB_HandleStep a,TAB_HandleCase b where b.strID=a.strCaseID and a.strCaseID = @id and a.nEnabled=1";
            SqlParameter[] hssqlParams = {
                                           new SqlParameter("id",id),
                                           new SqlParameter("zbid",zbid)
                                       };
            DataTable dtHandlerStepStrid=SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, hssql, hssqlParams).Tables[0];
            foreach (DataRow dr in dtHandlerStepStrid.Rows)
            {
                
                int type=PageBase.static_ext_int(dr["nStepType"].ToString());

                if (type >= 7 && type <= 16)
                {
                    String strSQLText = "select count(*) from View_CheckStandardHandleFinish where JiCheID=@zbid and nEnabled=1 And strCaseID = @id and nState=1 and nStepType = " + type;
                    if (PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSQLText, hssqlParams)) <= 0)
                    {
                        return false;
                    }
                }
                else
                {
                    switch (type)
                    {
                           //卡控整备后的数据
                        case 0://风速
                            if (dr["nCaseType"].ToString() == "2")
                            {
                                string Sql2 = @"select strCaseID from View_CheckFengSuFinish where strCaseID = @id and nEnabled=1 and JiCheID=@zbid and nSstate=1 group by strCaseID,JiCheID;";
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
                                lsDatLvWang lsdatlv = lsDatLvWang.getLastLvWangJianCeRiQi("", "", zbid);
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
                            string Sql3 = @"select count(*) from View_CheckDanJieFinish where strCaseID = @id and nEnabled=1 and JiCheID=@zbid and nState=1;";
                            if (PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, Sql3, hssqlParams)) <= 0)
                            {
                                return false;
                            }
                            break;
                        case 3://直供电
                            string Sql4 = @"select strCaseID from View_CheckZhiGongDianFinish where strCaseID = @id and nEnabled=1 and JiCheID=@zbid and nSstate=1 group by strCaseID,JiCheID;";
                            if (PageBase.static_ext_int(SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, Sql4, hssqlParams).Tables[0].Rows.Count) <= 0)
                            {
                                return false;
                            }
                            break;
                        case 4://瓷瓶
                            string Sql5 = @"select count(*) from View_CheckCiPingFinish where strCaseID = @id and nEnabled=1 and JiCheID=@zbid and nState=1;";
                            if (PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, Sql5, hssqlParams)) <= 0)
                            {
                                return false;
                            }
                            break;
                        case 5://列车管
                            if (SysConfig.GetSingleSysconfig("Scjkk", "Lcg") == "True")
                            {
                                lsDatLieCheGuan lsdatlcg = lsDatLieCheGuan.getLastLcgJianCeRiQi("", "", zbid);
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
                            string Sql7 = @"select count(*) from View_CheckHuaBanFinish where strCaseID = @id and nEnabled=1 and JiCheID=@zbid and nState=1;";
                            if (PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, Sql7, hssqlParams)) <= 0)
                            {
                                return false;
                            }
                            break;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 根据机车整备id看是否所有环节已整备完毕
        /// </summary>
        /// <param name="zbid"></param>
        /// <returns></returns>
        public static bool boolCheckLcFinish(int zbid)
        {
            string hssql = @"select caseid,nState from VIEW_Jt6JicheProcess where jcid = @zbid order by nOrder desc";
            SqlParameter[] hssqlParams = {
                                           new SqlParameter("zbid",zbid)
                                       };
            DataTable dtLcCase = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, hssql, hssqlParams).Tables[0];
            if (dtLcCase.Rows[0]["nState"].ToString() == "1")//只有状态为进行中的时候才检查所有步骤是否已完成
            {
                foreach (DataRow dr in dtLcCase.Rows)
                {
                    if (!boolCheckCaseFinish(dr["caseid"].ToString(), zbid))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            if (Lxd.GetCountJt6tpFromJiCheID(zbid) > 0)
            {
                return false;
            }
            if (LKJDataAnalysis.GetAllTAB_LKJDataAnalysis(zbid) <= 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 由环节id获取流程下所有环节信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetAllCaseInProcessFromCaseID(string id)
        {
            string strSql = "select * from TAB_HandleCase where strProcessID=(select strProcessID from TAB_HandleCase where strID = @id) order by nOrder asc";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("id",id)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        public static DataTable GetAlllCaseIDbeforeCaseOrder(string caseid,string cx)
        {
            string strSql = "select strCaseID,strID from VIEW_ProceeCase where nOrder<=(select nOrder from VIEW_ProceeCase where strCaseID=@caseid and strTrainType=@cx) and strTrainType=@cx";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("caseid",caseid),
                                           new SqlParameter("cx",cx)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        /// <summary>
        /// 获得下一个环节
        /// </summary>
        public HandleCase getNextCase(int order,string caseid)
        {
            HandleCase NextCase = null;

            DataTable dtCaseList = HandleCase.GetAllCaseInProcessFromCaseID(caseid);

            if (order < dtCaseList.Rows.Count)//判断是否有下个环节
            {
                if (dtCaseList.Select("nOrder=" + (order + 1))[0]["strID"].ToString() != "")
                {
                    NextCase = new HandleCase(dtCaseList.Select("nOrder=" + (order + 1))[0]["strID"].ToString());
                }
            }
            else
            {
                return null;
            }
            return NextCase;
        }

        #endregion

    }
}
