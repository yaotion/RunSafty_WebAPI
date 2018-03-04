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

namespace TF.RunSafty.Logic
{
    /// <summary>
    ///类名：StandardHandleRecord
    ///描述：标准整备记录基础数据操作
    /// </summary>
    public class StandardHandleRecord
    {
        #region 属性
        public string nID;
        public string strID;
        public string banCi;
        public string strUserNumber;//操作人工号
        public string strUserName;//操作人姓名
        public string strStepID;//步骤ID

        public int nStepType;//步骤类型
        public int JiCheID;//机车ID
        public int nState;//状态
        public int ApanageId;//属地ID
        public int UnitId;//单位ID
        public int DepartmentId;//部门ID

        public DateTime? dtBeginTime;//开始时间
        public DateTime? dtEndTime;//结束时间

        public string LocoType;
        public string LocoNum;
        //标准时间
        public int nStandardMinute;

        #endregion 属性

        #region 构造函数
        public StandardHandleRecord()
        {
        }

        public StandardHandleRecord(string stepid, int JiCheID)
        {
            string strSql = "select (select nStandardMinute from TAB_HandleStep where a.strStepID=strID) as nStandardMinute,a.* from [View_StandardHandleFinish] a where a.JiCheID=@JiCheID and a.strStepID=@strStepID order by a.dtBeginTime asc";
            SqlParameter[] sqlParams = {
                    new SqlParameter("JiCheID",JiCheID),
                    new SqlParameter("strStepID",stepid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }

        public static StandardHandleRecord SetValue(StandardHandleRecord StandardHandle, DataRow dr)
        {
            if (dr != null)
            {
                StandardHandle.nID = PageBase.static_ext_string(dr["nID"]);
                StandardHandle.strID = PageBase.static_ext_string(dr["strID"]);
                StandardHandle.nStepType = PageBase.static_ext_int(dr["nStepType"]);
                StandardHandle.strStepID = PageBase.static_ext_string(dr["strStepID"]);
                StandardHandle.JiCheID = PageBase.static_ext_int(dr["JiCheID"]);
                StandardHandle.dtBeginTime = PageBase.static_ext_date(dr["dtBeginTime"]);
                StandardHandle.dtEndTime = PageBase.static_ext_date(dr["dtEndTime"]);
                StandardHandle.nState = PageBase.static_ext_int(dr["nState"]);
                StandardHandle.banCi = PageBase.static_ext_string(dr["banCi"]);
                StandardHandle.ApanageId = PageBase.static_ext_int(dr["ApanageId"]);
                StandardHandle.UnitId = PageBase.static_ext_int(dr["UnitId"]);
                StandardHandle.DepartmentId = PageBase.static_ext_int(dr["DepartmentId"]);
                StandardHandle.strUserNumber = PageBase.static_ext_string(dr["strUserNumber"]);
                StandardHandle.strUserName = PageBase.static_ext_string(dr["strUserName"]);
                StandardHandle.LocoType = PageBase.static_ext_string(dr["LocoType"]);
                StandardHandle.LocoNum = PageBase.static_ext_string(dr["LocoNum"]);
                StandardHandle.nStandardMinute = PageBase.static_ext_int(dr["nStandardMinute"]);
            }
            return StandardHandle;
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "INSERT INTO [TAB_StandardHandleRecord]  ([strID],[nStepType],[strStepID],[JiCheID],[dtBeginTime],[dtEndTime],[nState],[banCi],[ApanageId],[UnitId],[DepartmentId],[strUserNumber],[strUserName])  VALUES (@strID, @nStepType, @strStepID, @JiCheID, @dtBeginTime, @dtEndTime, @nState, @banCi, @ApanageId, @UnitId, @DepartmentId, @strUserNumber, @strUserName)";
            SqlParameter[] sqlParams = {
new SqlParameter("@strID",strID),
new SqlParameter("@nStepType",nStepType),
new SqlParameter("@strStepID",strStepID),
new SqlParameter("@JiCheID",JiCheID),
new SqlParameter("@dtBeginTime",dtBeginTime),
new SqlParameter("@dtEndTime",dtEndTime),
new SqlParameter("@nState",nState),
new SqlParameter("@banCi",banCi),
new SqlParameter("@ApanageId",ApanageId),
new SqlParameter("@UnitId",UnitId),
new SqlParameter("@DepartmentId",DepartmentId),
new SqlParameter("@strUserNumber",strUserNumber),
new SqlParameter("@strUserName",strUserName)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update()
        {
            string strSql = "UPDATE [TAB_StandardHandleRecord] SET [strID]=@strID, [nStepType]=@nStepType, [strStepID]=@strStepID, [JiCheID]=@JiCheID, [dtBeginTime]=@dtBeginTime, [dtEndTime]=@dtEndTime, [nState]=@nState, [banCi]=@banCi, [ApanageId]=@ApanageId, [UnitId]=@UnitId, [DepartmentId]=@DepartmentId, [strUserNumber]=@strUserNumber, [strUserName]=@strUserName WHERE  [JiCheID]=@JiCheID and [strStepID]=@strStepID";
            SqlParameter[] sqlParams = {
new SqlParameter("@strID",strID),
new SqlParameter("@nStepType",nStepType),
new SqlParameter("@strStepID",strStepID),
new SqlParameter("@JiCheID",JiCheID),
new SqlParameter("@dtBeginTime",dtBeginTime),
new SqlParameter("@dtEndTime",dtEndTime),
new SqlParameter("@nState",nState),
new SqlParameter("@banCi",banCi),
new SqlParameter("@ApanageId",ApanageId),
new SqlParameter("@UnitId",UnitId),
new SqlParameter("@DepartmentId",DepartmentId),
new SqlParameter("@strUserNumber",strUserNumber),
new SqlParameter("@strUserName",strUserName),
        };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string nID)
        {
            string strSql = "delete [TAB_StandardHandleRecord] where [nID]=@nID";
            SqlParameter[] sqlParams = {
            new SqlParameter("@nID",nID)
            };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int JiCheID, string strStepID)
        {
            string strSql = "select count(*) from [TAB_StandardHandleRecord] where [JiCheID]=@JiCheID and [strStepID]=@strStepID";
            SqlParameter[] sqlParams = {
                new SqlParameter("@JiCheID",JiCheID),
                    new SqlParameter("@strStepID",strStepID)
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        /// <summary>
        /// 根据传入where条件获取风速记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static StandardHandleRecord StandardHandle(string where)
        {
            string strSql = "select (select nStandardMinute from TAB_HandleStep where a.strStepID=strID) as nStandardMinute,a.* from View_StandardHandleFinish a where 1=1 " + where;
            DataTable dtStandardHandle = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql).Tables[0];
            StandardHandleRecord StandardHandle = new StandardHandleRecord();
            return SetValue(StandardHandle, dtStandardHandle.Rows[0]);
        }
    }
}