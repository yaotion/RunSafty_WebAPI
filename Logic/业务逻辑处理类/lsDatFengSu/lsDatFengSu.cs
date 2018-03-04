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
    public class lsDatFengSu
    {
        #region 属性
        public int ID;
        public string LocoType;
        public string LocoNum;
        public string FenLei;
        public string banci;
        public string buwei;
        public DateTime? celiangriqi;
        public decimal? dian1;
        public decimal? dian2;
        public decimal? dian3;
        public decimal? dian4;
        public decimal? dian5;
        public decimal? dian6;
        public decimal? dian7;
        public decimal? pingjun;
        public string celiangren;
        public int GovernUnitId;
        public int UnitId;
        public int DepartmentId;
        public int ApanageId;
        public string strStepID;
        public int JiCheID;
        public DateTime? dtBeginTime;
        public string strID;
        public DateTime? dtEndTime;
        public int nState;
        public int nSstate;
        public int nCheckCount;
        public int nIsQualified;
        public int nFinalIsQualified;
        public int nStandardMinute;
        #endregion 属性
         #region 构造函数
        public lsDatFengSu()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public lsDatFengSu(string stepid, int JiCheID)
        {
            string strSql = "select (select nStandardMinute from TAB_HandleStep where a.strStepID=strID) as nStandardMinute,a.* from lsDatFengSu a where a.JiCheID=@JiCheID and a.strStepID=@strStepID order by a.dtBeginTime asc";
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
        public static lsDatFengSu SetValue(lsDatFengSu FengSu,DataRow dr)
        {
            if (dr!=null)
            {
                FengSu.LocoType = dr["LocoType"].ToString();
                FengSu.LocoNum = dr["LocoNum"].ToString();
                FengSu.FenLei = dr["FenLei"].ToString();
                FengSu.banci = dr["banci"].ToString();
                FengSu.buwei = dr["buwei"].ToString();
                FengSu.celiangriqi = PageBase.static_ext_date(dr["celiangriqi"].ToString());
                FengSu.dian1 = PageBase.static_ext_decimal(dr["dian1"].ToString());
                FengSu.dian2 = PageBase.static_ext_decimal(dr["dian2"].ToString());
                FengSu.dian3 = PageBase.static_ext_decimal(dr["dian3"].ToString());
                FengSu.dian4 = PageBase.static_ext_decimal(dr["dian4"].ToString());
                FengSu.dian5 = PageBase.static_ext_decimal(dr["dian5"].ToString());
                FengSu.dian6 = PageBase.static_ext_decimal(dr["dian6"].ToString());
                FengSu.dian7 = PageBase.static_ext_decimal(dr["dian7"].ToString());
                FengSu.pingjun = PageBase.static_ext_int(dr["pingjun"].ToString());
                FengSu.celiangren = dr["celiangren"].ToString();
                //FengSu.GovernUnitId = PageBase.static_ext_int(dr["GovernUnitId"].ToString());
                //FengSu.UnitId = PageBase.static_ext_int(dr["UnitId"].ToString());
                //FengSu.DepartmentId = PageBase.static_ext_int(dr["DepartmentId"].ToString());
                //FengSu.ApanageId = PageBase.static_ext_int(dr["ApanageId"].ToString());
                FengSu.strStepID = dr["strStepID"].ToString();
                FengSu.JiCheID =PageBase.static_ext_int(dr["JiCheID"].ToString());
                FengSu.dtBeginTime = PageBase.static_ext_date(dr["dtBeginTime"].ToString());
                FengSu.strID = dr["strID"].ToString();
                FengSu.dtEndTime = PageBase.static_ext_date(dr["dtEndTime"].ToString());
                FengSu.nState = PageBase.static_ext_int(dr["nState"].ToString());
                FengSu.nSstate = PageBase.static_ext_int(dr["nSstate"].ToString());
                FengSu.nCheckCount = PageBase.static_ext_int(dr["nCheckCount"].ToString());
                FengSu.nIsQualified = PageBase.static_ext_int(dr["nFinalIsQualified"].ToString());
                FengSu.nStandardMinute = PageBase.static_ext_int(dr["nStandardMinute"].ToString());
            }
            return FengSu;
        }
        #endregion 构造函数
        #region 增删改
        public void Add()
        {
            string strSql = @"insert into lsDatFengSu (LocoType,LocoNum,FenLei,banci,buwei,celiangriqi,dian1,dian2,dian3,dian4,dian5,dian6,dian7,pingjun,celiangren,GovernUnitId,UnitId,DepartmentId,ApanageId,strStepID,JiCheID,dtBeginTime,strID,dtEndTime,nState,nSstate,nCheckCount,nIsQualified,nFinalIsQualified) 
            values (@LocoType,@LocoNum,@FenLei,@banci,@buwei,@celiangriqi,@dian1,@dian2,@dian3,@dian4,@dian5,@dian6,@dian7,@pingjun,@celiangren,@GovernUnitId,@UnitId,@DepartmentId,@ApanageId,@strStepID,@JiCheID,@dtBeginTime,@strID,@dtEndTime,@nState,@nSstate,@nCheckCount,@nIsQualified,@nFinalIsQualified)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum),
                                           new SqlParameter("FenLei",FenLei),
                                           new SqlParameter("banci",banci),
                                           new SqlParameter("buwei",buwei),
                                           new SqlParameter("celiangriqi",celiangriqi),
                                           new SqlParameter("dian1",dian1),
                                           new SqlParameter("dian2",dian2),
                                           new SqlParameter("dian3",dian3),
                                           new SqlParameter("dian4",dian4),
                                           new SqlParameter("dian5",dian5),
                                           new SqlParameter("dian6",dian6),
                                           new SqlParameter("dian7",dian7),
                                           new SqlParameter("pingjun",pingjun),
                                           new SqlParameter("celiangren",celiangren),
                                           new SqlParameter("GovernUnitId",GovernUnitId),
                                           new SqlParameter("UnitId",UnitId),
                                           new SqlParameter("DepartmentId",DepartmentId),
                                           new SqlParameter("ApanageId",ApanageId),
                                           new SqlParameter("strStepID",strStepID),
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("dtBeginTime",dtBeginTime),
                                           new SqlParameter("strID",strID),
                                           new SqlParameter("dtEndTime",dtEndTime),
                                           new SqlParameter("nState",nState),
                                            new SqlParameter("nSstate",nSstate),
                                            new SqlParameter("nCheckCount",nCheckCount),
                                            new SqlParameter("nIsQualified",nIsQualified),
                                            new SqlParameter("nFinalIsQualified",nFinalIsQualified)
                                       };
            SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams);
        }






        public bool Update()
        {
            string strSql = @"update lsDatFengSu set LocoType=@LocoType,LocoNum=@LocoNum,FenLei=@FenLei,banci=@banci,buwei=@buwei,
celiangriqi=@celiangriqi,dian1=@dian1,dian2=@dian2,dian3=@dian3,dian4=@dian4,dian5=@dian5,dian6=@dian6,dian7=@dian7,
pingjun=@pingjun,celiangren=@celiangren,GovernUnitId=@GovernUnitId,UnitId=@UnitId,ApanageId=@ApanageId,DepartmentId=@DepartmentId,
strStepID=@strStepID,JiCheID=@JiCheID,dtBeginTime=@dtBeginTime,dtEndTime=@dtEndTime,nState=@nState,nCheckCount=@nCheckCount,nIsQualified=@nIsQualified 
where buwei=@buwei and JiCheID=@JiCheID and strStepID=@strStepID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum),
                                           new SqlParameter("FenLei",FenLei),
                                           new SqlParameter("banci",banci),
                                           new SqlParameter("buwei",buwei),
                                           new SqlParameter("celiangriqi",celiangriqi),
                                           new SqlParameter("dian1",dian1),
                                           new SqlParameter("dian2",dian2),
                                           new SqlParameter("dian3",dian3),
                                           new SqlParameter("dian4",dian4),
                                           new SqlParameter("dian5",dian5),
                                           new SqlParameter("dian6",dian6),
                                           new SqlParameter("dian7",dian7),
                                           new SqlParameter("pingjun",pingjun),
                                           new SqlParameter("celiangren",celiangren),
                                           new SqlParameter("GovernUnitId",GovernUnitId),
                                           new SqlParameter("UnitId",UnitId),
                                           new SqlParameter("DepartmentId",DepartmentId),
                                           new SqlParameter("ApanageId",ApanageId),
                                           new SqlParameter("strStepID",strStepID),
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("dtBeginTime",dtBeginTime),
                                           new SqlParameter("strID",strID),
                                           new SqlParameter("dtEndTime",dtEndTime),
                                           new SqlParameter("nState",nState),
                                           new SqlParameter("nCheckCount",nCheckCount),
                                           new SqlParameter("nIsQualified",nIsQualified)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        

        public bool UpdatenSstate()
        {
            string strSql = "update lsDatFengSu set nSstate=1 where strStepID=@strStepID and JiCheID = @JiCheID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStepID",strStepID),
                                           new SqlParameter("JiCheID",JiCheID)
                                           
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }

        public bool UpdatenFinalIsQualified()
        {
            string strSql = "update lsDatFengSu set nFinalIsQualified=@nFinalIsQualified where strStepID=@strStepID and JiCheID = @JiCheID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStepID",strStepID),
                                           new SqlParameter("JiCheID",JiCheID),
                                            new SqlParameter("nFinalIsQualified",nFinalIsQualified)
                                           
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }


        public static bool Delete(string strid)
        {
            string strSql = "delete lsDatFengSu where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string stepid, int jcid,string bwei)
        {
            string strSql = "select count(*) from lsDatFengSu where strStepID=@strStepID and JiCheID=@JiCheID and buwei=@buwei";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStepID",stepid),
                                           new SqlParameter("JiCheID",jcid),
                                           new SqlParameter("buwei",bwei)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }

        public static bool IsQualified(string stepid, int jcid, string bwei)
        {
            string strSql = "select nIsQualified from lsDatFengSu where strStepID=@strStepID and JiCheID=@JiCheID and buwei=@buwei";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStepID",stepid),
                                           new SqlParameter("JiCheID",jcid),
                                           new SqlParameter("buwei",bwei)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }

        #endregion 增删改

        /// <summary>
        /// 获取风速记录数量
        /// </summary>
        /// <param name="strid"></param>
        /// <returns></returns>
        public static int getCountFromStepID(string stepid, int  JiCheID)
        {
            string strSql = "select count(*) from lsDatFengSu where strStepID=@stepid and JiCheID=@JiCheID and nState=1";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("stepid",stepid),
                                           new SqlParameter("JiCheID",JiCheID)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams));
        }

        /// <summary>
        ///根据stepid获取验证通过数量
        /// </summary>
        /// <param name="stepid"></param>
        /// <param name="JiCheID"></param>
        /// <returns></returns>
        public static int getQualifiedCountFromStepID(string stepid, string JiCheID)
        {
            string strSql = "select count(*) from lsDatFengSu where strStepID=@stepid and JiCheID=@JiCheID and nIsQualified=1";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("stepid",stepid),
                                           new SqlParameter("JiCheID",JiCheID)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams));
        }

        /// <summary>
        /// 获取不合格数量
        /// </summary>
        /// <param name="stepid"></param>
        /// <param name="JiCheID"></param>
        /// <returns></returns>
        public static int getQualifiedFalseCount(string stepid, string JiCheID)
        {
            string strSql = "select count(*) from lsDatFengSu where strStepID=@stepid and JiCheID=@JiCheID  and nIsQualified=-1";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("stepid",stepid),
                                           new SqlParameter("JiCheID",JiCheID)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams));
        }

        public static DataTable getAllFromStepID(string stepid, int JiCheID)
        {
            string Condition = " 1=1 ";
            Condition+=stepid!=""?" and strStepID=@stepid":"";
            string strSql = "select * from lsDatFengSu where "+Condition+" and JiCheID=@JiCheID and nIsQualified=1";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("stepid",stepid),
                                           new SqlParameter("JiCheID",JiCheID)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }


        //获得整备后的风速测试步骤ID，如果整备后没有配置风速测试步骤,那么返回空
        public static string getHandleEndFengSuStepID(string strCaseID)
        {
            //获得整备后的环节ID SQL语句
            string strGetCaseID_SQLText = "(select strID from TAB_HandleCase where strProcessID = " +
                "(select strProcessID from TAB_HandleCase where strid = '" + strCaseID + "')" +
                " and nCaseType = 2)";

            //获得风速测试步骤ID的 SQL语句 nStepType = 0 表示步骤类型等于0(风速测试)
            string strSQLText = "select strID from TAB_HandleStep where strCaseID = " + strGetCaseID_SQLText + " and nStepType = 0";

            return  SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSQLText).ToString();
        }

        /// <summary>
        /// 根据传入where条件获取风速记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static lsDatFengSu FengSuRecord(string where)
        {
            string strSql = "select (select nStandardMinute from TAB_HandleStep where a.strStepID=strID) as nStandardMinute, a.LocoType,a.LocoNum,a.FenLei,a.banci,a.celiangriqi,a.celiangren,a.buwei,a.strID,a.strStepID,a.JiCheID,a.nState,a.nSstate,a.nCheckCount,a.nIsQualified,a.nFinalIsQualified,a.dian1,a.dian2,a.dian3,a.dian4,a.dian5,a.dian6,a.dian7,a.pingjun,(select min(dtBeginTime) from lsDatFengSu where 1=1 " + where + ")as dtBeginTime,(select max(dtEndTime) from lsDatFengSu where 1=1 " + where + ")as dtEndTime from lsDatFengSu a where 1=1 " + where;
            DataTable dtFengSu=SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql).Tables[0];
            lsDatFengSu FengSu = new lsDatFengSu();
            return SetValue(FengSu, dtFengSu.Rows[0]);
        }
    }
}
