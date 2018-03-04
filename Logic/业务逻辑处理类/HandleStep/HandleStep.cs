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
    public class HandleStep
    {
        #region 属性
        public string strID;
        public string strStepName;
        public int nStandardMinute;
        public int nOrder;
        public string strCaseID;
        public int GroupsId;
        public int UnitID;
        public int DepartmentId;
        public string strStandardText;
        public int nStepType;
        public string strNeedStepID;
        public string dtPostTime;
        public string nPostType;
        public int X;
        public int Y;
        public int nEnabled;
        public int nRecVoice;
        public int nPostOrder;
        #endregion 属性

        #region 构造函数
        public HandleStep()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public HandleStep(string strid)
        {
            string strSql = "select * from TAB_HandleStep where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strID = dt.Rows[0]["strID"].ToString();
                nStandardMinute = PageBase.static_ext_int(dt.Rows[0]["nStandardMinute"].ToString());
                nOrder = PageBase.static_ext_int(dt.Rows[0]["nOrder"].ToString());
                SetValue(this, dt.Rows[0]);
            }
        }

        public static HandleStep SetValue(HandleStep handlestep, DataRow dr)
        {
            if (dr != null)
            {
                handlestep.strStepName = dr["strStepName"].ToString();
                handlestep.strCaseID = dr["strCaseID"].ToString();
                handlestep.GroupsId = PageBase.static_ext_int(dr["GroupsId"].ToString());
                handlestep.UnitID = PageBase.static_ext_int(dr["UnitID"].ToString());
                handlestep.DepartmentId = PageBase.static_ext_int(dr["DepartmentId"].ToString());
                handlestep.strStandardText = dr["strStandardText"].ToString();
                handlestep.strNeedStepID = dr["strNeedStepID"].ToString();
                handlestep.nStepType = PageBase.static_ext_int(dr["nStepType"].ToString());
                handlestep.dtPostTime = dr["dtPostTime"].ToString();
                handlestep.X = PageBase.static_ext_int(dr["X"].ToString());
                handlestep.Y = PageBase.static_ext_int(dr["Y"].ToString());
                handlestep.nEnabled = PageBase.static_ext_int(dr["nEnabled"].ToString());
                handlestep.nRecVoice = PageBase.static_ext_int(dr["nRecVoice"].ToString());
                handlestep.nPostType = dr["nPostType"].ToString();
                handlestep.nPostOrder = PageBase.static_ext_int(dr["nPostOrder"].ToString());
            }
            return handlestep;
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_HandleStep (strID,strStepName,strCaseID,nStandardMinute,GroupsId,DepartmentId,UnitID,strStandardText,nStepType,strNeedStepID,nOrder,nEnabled,nRecVoice,nPostType) select @strID,@strStepName,@strCaseID,@nStandardMinute,@GroupsId,@DepartmentId,@UnitID,@strStandardText,@nStepType,@strNeedStepID,(count(*)+1) as nder,@nEnabled,@nRecVoice,@nPostType from TAB_HandleStep where strCaseID=@strCaseID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strID),
                                           new SqlParameter("strStepName",strStepName),
                                           new SqlParameter("nStandardMinute",nStandardMinute),
                                           new SqlParameter("strCaseID",strCaseID),
                                           new SqlParameter("GroupsId",GroupsId),
                                           new SqlParameter("UnitID",UnitID),
                                           new SqlParameter("DepartmentId",DepartmentId),
                                           new SqlParameter("strStandardText",strStandardText),
                                           new SqlParameter("nStepType",nStepType),
                                           new SqlParameter("strNeedStepID",strNeedStepID),
                                           new SqlParameter("nEnabled",nEnabled),
                                           new SqlParameter("nRecVoice",nRecVoice),
                                           new SqlParameter("nPostType",nPostType)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_HandleStep set strStepName = @strStepName,nStandardMinute=@nStandardMinute,GroupsId=@GroupsId,UnitID=@UnitID,DepartmentId=@DepartmentId,strStandardText=@strStandardText,nStepType=@nStepType,strNeedStepID=@strNeedStepID,nEnabled = @nEnabled,nRecVoice=@nRecVoice,nPostType=@nPostType where strID=@strID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strID",strID),
                                            new SqlParameter("strStepName",strStepName),
                                           new SqlParameter("nStandardMinute",nStandardMinute),
                                           new SqlParameter("GroupsId",GroupsId),
                                           new SqlParameter("UnitID",UnitID),
                                           new SqlParameter("DepartmentId",DepartmentId),
                                           new SqlParameter("strStandardText",strStandardText),
                                           new SqlParameter("nStepType",nStepType),
                                           new SqlParameter("strNeedStepID",strNeedStepID),
                                           new SqlParameter("nEnabled",nEnabled),
                                           new SqlParameter("nRecVoice",nRecVoice),
                                           new SqlParameter("nPostType",nPostType)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }

        public bool UpdateXY()
        {
            string strSql = "update TAB_HandleStep set X = @X,Y=@Y where strID=@strID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strID",strID),
                                            new SqlParameter("X",X),
                                           new SqlParameter("Y",Y)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_HandleStep where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strid, string Name,string caseid)
        {
            string strSql = "select count(*) from TAB_HandleStep where strStepName=@strStepName and strCaseID=@strCaseID";
            if (strid != "")
            {
                strSql += " and strID <> @strID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid),
                                           new SqlParameter("strStepName",Name),
                                           new SqlParameter("strCaseID",caseid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        public bool UpdateSortid()
        {
            string strSql = "update TAB_HandleStep set nOrder=@nOrder where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nOrder",nOrder),
                                           new SqlParameter("strID",strID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool UpdatePostOrder()
        {
            string strSql = "update TAB_HandleStep set nPostOrder=@nPostOrder where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nPostOrder",nPostOrder),
                                           new SqlParameter("strID",strID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        /// <summary>
        /// 除了strid外环节下其他
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="strid"></param>
        /// <returns></returns>
        public static DataTable GetAllTAB_HandleStep(string uid,string strid)
        {
            string strSql = "select * from VIEW_HandleStep where 1=1";
            if (uid != "")
            {
                strSql += " and strCaseID = @uid ";
            }
            strSql += strid != "" ? " and strID <> @strid" : "";
            strSql += " order by nOrder ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("uid",uid),
                                           new SqlParameter("strid",strid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        public static DataTable GetStepStateFromJcid(int jcid,string caseids)
        {
            string strSql = "select nState,strStepName,strID,nStepType,dtBeginTime,dtEndTime,3 as nIsQualified from View_CheckCiPingFinish where JiCheID=@jcid and strCaseID in ("+caseids+@")
            union
            select nState,strStepName,strID,nStepType,dtBeginTime,dtEndTime,3 as nIsQualified from View_CheckDanJieFinish where JiCheID=@jcid and strCaseID in ("+caseids+@")
            union
            select nSstate as nState,strStepName,strID,nStepType,dtBeginTime,dtEndTime,nFinalIsQualified as nIsQualified from View_CheckFengSuFinish where JiCheID=@jcid and strCaseID in (" + caseids + @")
            union
            select nState,strStepName,strID,nStepType,dtBeginTime,dtEndTime,nIsQualified from View_CheckHuaBanFinish where JiCheID=@jcid and strCaseID in (" +caseids+@")
            union
            select nState,strStepName,strID,nStepType,dtBeginTime,dtEndTime,3 as nIsQualified from View_CheckLiCheGuanFinish where JiCheID=@jcid and strCaseID in ("+caseids+@")
            union
            select nState,strStepName,strID,nStepType,dtBeginTime,dtEndTime,3 as nIsQualified from View_ChecklvWangFinish where JiCheID=@jcid and strCaseID in ("+caseids+@")
            union all
            select nState,strStepName,strID,nStepType,dtBeginTime,dtEndTime,3 as nIsQualified from View_CheckStandardHandleFinish where JiCheID=@jcid and strCaseID in (" + caseids + @")
            union
            select nState,strStepName,strID,nStepType,dtBeginTime,dtEndTime,3 as nIsQualified from View_CheckZhiGongDianFinish where JiCheID=@jcid and strCaseID in (" + caseids + @")";
           
            SqlParameter[] sqlParams = {
                                           new SqlParameter("jcid",jcid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        /// <summary>
        /// 根据岗位类型获取该岗位下所有步骤
        /// </summary>
        /// <param name="posttype"></param>
        /// <returns></returns>
        public static List<HandleStep> GetHandleStepFromPostType(int posttype,string Processid)
        {
            string strSql = "select stepID,strStepName,nPostOrder from VIEW_Process where nPostType=@nPostType and strProcessID=@strProcessID group by stepID,strStepName,nPostOrder order by nPostOrder ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nPostType",posttype),
                                           new SqlParameter("strProcessID",Processid)
                                       };
            return ListValue(SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0]);
        }

        public static List<HandleStep> ListValue(DataTable dt)
        {
            List<HandleStep> list = new List<HandleStep>();
            HandleStep handlestep;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    handlestep = new HandleStep();
                    handlestep.strID = dt.Rows[i]["stepID"].ToString();
                    handlestep.strStepName = dt.Rows[i]["strStepName"].ToString();
                    list.Add(handlestep);
                }
            }
            return list;
        }
        #endregion
    }
}
