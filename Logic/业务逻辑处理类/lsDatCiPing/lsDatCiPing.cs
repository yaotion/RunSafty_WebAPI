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
    public class lsDatCiPing
    {
        #region 属性
        public string ID;
        public string LocoType;
        public string LocoNum;
        public string banCi;
        public DateTime? CaShiRiQi;
        public string caoZuoRen;
        public string JianChaRen;
        public int GovernUnitId;
        public int UnitId;
        public int DepartmentId;
        public int ApanageId;
        public string strStepID;
        public string strID;
        public int JiCheID;
        public int nState;
        public DateTime? dtBeginTime;
        public DateTime? dtEndTime;
        public string strFileName;
        #endregion 属性
         #region 构造函数
        public lsDatCiPing()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public lsDatCiPing(string stepid, int jcid)
        {
            string strSql = "select * from lsDatCiPing where strStepID=@stepid and JiCheID=@JiCheID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("stepid",stepid),
                                           new SqlParameter("JiCheID",jcid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ID = dt.Rows[0]["strID"].ToString();
                LocoType = dt.Rows[0]["LocoType"].ToString();
                LocoNum = dt.Rows[0]["LocoNum"].ToString();
                banCi = dt.Rows[0]["banCi"].ToString();
                CaShiRiQi =PageBase.static_ext_date(dt.Rows[0]["CaShiRiQi"].ToString());
                caoZuoRen = dt.Rows[0]["caoZuoRen"].ToString();
                JianChaRen = dt.Rows[0]["JianChaRen"].ToString();
                GovernUnitId = PageBase.static_ext_int(dt.Rows[0]["GovernUnitId"].ToString());
                UnitId = PageBase.static_ext_int(dt.Rows[0]["UnitId"].ToString());
                DepartmentId = PageBase.static_ext_int(dt.Rows[0]["DepartmentId"].ToString());
                ApanageId = PageBase.static_ext_int(dt.Rows[0]["ApanageId"].ToString());
                strStepID = dt.Rows[0]["strStepID"].ToString();
                strID = dt.Rows[0]["strID"].ToString();
                JiCheID = PageBase.static_ext_int(dt.Rows[0]["JiCheID"].ToString());
                nState = PageBase.static_ext_int(dt.Rows[0]["nState"].ToString());
                dtBeginTime = PageBase.static_ext_date(dt.Rows[0]["dtBeginTime"].ToString());
                dtEndTime = PageBase.static_ext_date(dt.Rows[0]["dtEndTime"].ToString());
                strFileName = dt.Rows[0]["strFileName"].ToString();
            }
        }
        #endregion 构造函数
        #region 增删改
        public bool Add()
        {
            string strSql = "insert into lsDatCiPing (strID,LocoType,LocoNum,banCi,CaShiRiQi,caoZuoRen,JianChaRen,GovernUnitId,UnitId,DepartmentId,ApanageId,strStepID,JiCheID,nState,dtBeginTime,dtEndTime,strFileName) values (@strID,@LocoType,@LocoNum,@banCi,@CaShiRiQi,@caoZuoRen,@JianChaRen,@GovernUnitId,@UnitId,@DepartmentId,@ApanageId,@strStepID,@JiCheID,@nState,@dtBeginTime,@dtEndTime,@strFileName)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strID),
                                           new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum),
                                           new SqlParameter("banCi",banCi),
                                           new SqlParameter("CaShiRiQi",CaShiRiQi),
                                           new SqlParameter("caoZuoRen",caoZuoRen),
                                           new SqlParameter("JianChaRen",JianChaRen),
                                           new SqlParameter("GovernUnitId",GovernUnitId),
                                           new SqlParameter("UnitId",UnitId),
                                           new SqlParameter("DepartmentId",DepartmentId),
                                           new SqlParameter("ApanageId",ApanageId),
                                           new SqlParameter("strStepID",strStepID),
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("nState",nState),
                                           new SqlParameter("dtBeginTime",dtBeginTime),
                                           new SqlParameter("dtEndTime",dtEndTime),
                                           new SqlParameter("strFileName",strFileName)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update lsDatCiPing set LocoType=@LocoType,LocoNum=@LocoNum,banCi=@banCi,CaShiRiQi=@CaShiRiQi,caoZuoRen=@caoZuoRen,JianChaRen=@JianChaRen,GovernUnitId=@GovernUnitId,UnitId=@UnitId,DepartmentId=@DepartmentId,ApanageId=@ApanageId,strStepID=@strStepID,JiCheID=@JiCheID,nState=@nState,dtBeginTime=@dtBeginTime,dtEndTime=@dtEndTime,strFileName=@strFileName where JiCheID=@JiCheID and strStepID=@strStepID";
            SqlParameter[] sqlParams = {
                                         new SqlParameter("strID",strID),
                                           new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum),
                                           new SqlParameter("banCi",banCi),
                                           new SqlParameter("CaShiRiQi",CaShiRiQi),
                                           new SqlParameter("caoZuoRen",caoZuoRen),
                                           new SqlParameter("JianChaRen",JianChaRen),
                                           new SqlParameter("GovernUnitId",GovernUnitId),
                                           new SqlParameter("UnitId",UnitId),
                                           new SqlParameter("DepartmentId",DepartmentId),
                                           new SqlParameter("ApanageId",ApanageId),
                                           new SqlParameter("strStepID",strStepID),
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("nState",nState),
                                           new SqlParameter("dtBeginTime",dtBeginTime),
                                           new SqlParameter("dtEndTime",dtEndTime),
                                           new SqlParameter("strFileName",strFileName)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete lsDatCiPing where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int JiCheID, string stepid)
        {
            string strSql = "select count(*) from lsDatCiPing where JiCheID=@JiCheID and strStepID=@strStepID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("strStepID",stepid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改
    }
}
