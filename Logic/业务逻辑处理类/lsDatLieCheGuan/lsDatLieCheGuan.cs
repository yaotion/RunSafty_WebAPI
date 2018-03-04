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
    public class lsDatLieCheGuan
    {
        #region 属性
        public string ID;
        public string LocoType;
        public string LocoNum;
        public string banCi;
        public DateTime? JianChaDate;
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
        public int nGhts = 0;
        #endregion 属性
         #region 构造函数
        public lsDatLieCheGuan()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public lsDatLieCheGuan(string stepid, int jcid)
        {
            string Condition = " 1=1";
            Condition += stepid != "" ? " and strStepID=@stepid" : "";
            string strSql = "select * from lsDatLieCheGuan where "+Condition+" and JiCheID=@JiCheID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("stepid",stepid),
                                           new SqlParameter("JiCheID",jcid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                SetValue(this, dt.Rows[0]);
            }
        }
        /// <summary>
        /// 结构体赋值
        /// </summary>
        /// <param name="StandardHandle"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static lsDatLieCheGuan SetValue(lsDatLieCheGuan lcg, DataRow dr)
        {
            if (dr != null)
            {
                lcg.ID = dr["strID"].ToString();
                lcg.LocoType = dr["LocoType"].ToString();
                lcg.LocoNum = dr["LocoNum"].ToString();
                lcg.banCi = dr["BanCi"].ToString();
                lcg.JianChaDate = PageBase.static_ext_date(dr["JianChaDate"].ToString());
                lcg.JianChaRen = dr["JianChaRen"].ToString();
                lcg.GovernUnitId = PageBase.static_ext_int(dr["GovernUnitId"].ToString());
                lcg.UnitId = PageBase.static_ext_int(dr["UnitId"].ToString());
                lcg.DepartmentId = PageBase.static_ext_int(dr["DepartmentId"].ToString());
                lcg.ApanageId = PageBase.static_ext_int(dr["ApanageId"].ToString());
                lcg.strStepID = dr["strStepID"].ToString();
                lcg.strID = dr["strID"].ToString();
                lcg.JiCheID = PageBase.static_ext_int(dr["JiCheID"].ToString());
                lcg.nState = PageBase.static_ext_int(dr["nState"].ToString());
                lcg.dtBeginTime = PageBase.static_ext_date(dr["dtBeginTime"].ToString());
                lcg.dtEndTime = PageBase.static_ext_date(dr["dtEndTime"].ToString());
                lcg.strFileName = dr["strFileName"].ToString();
            }
            return lcg;
        }
        #endregion 构造函数
        #region 增删改
        public bool Add()
        {
            string strSql = "insert into lsDatLieCheGuan (strID,LocoType,LocoNum,banCi,JianChaDate,JianChaRen,GovernUnitId,UnitId,DepartmentId,ApanageId,strStepID,JiCheID,nState,dtBeginTime,dtEndTime,strFileName) values (@strID,@LocoType,@LocoNum,@banCi,@JianChaDate,@JianChaRen,@GovernUnitId,@UnitId,@DepartmentId,@ApanageId,@strStepID,@JiCheID,@nState,@dtBeginTime,@dtEndTime,@strFileName)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strID),
                                           new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum),
                                           new SqlParameter("banCi",banCi),
                                           new SqlParameter("JianChaDate",JianChaDate),
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
            string strSql = "update lsDatLieCheGuan set LocoType=@LocoType,LocoNum=@LocoNum,banCi=@banCi,JianChaDate=@JianChaDate,JianChaRen=@JianChaRen,GovernUnitId=@GovernUnitId,UnitId=@UnitId,DepartmentId=@DepartmentId,ApanageId=@ApanageId,strStepID=@strStepID,JiCheID=@JiCheID,nState=@nState,dtBeginTime=@dtBeginTime,dtEndTime=@dtEndTime,strFileName=@strFileName where JiCheID=@JiCheID and strStepID=@strStepID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strID",strID),
                                           new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum),
                                           new SqlParameter("banCi",banCi),
                                           new SqlParameter("JianChaDate",JianChaDate),
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
            string strSql = "delete lsDatLieCheGuan where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int JiCheID, string stepid)
        {
            string strSql = "select count(*) from lsDatLieCheGuan where JiCheID=@JiCheID and strStepID=@strStepID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("strStepID",stepid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        /// <summary>
        /// 根据车型车号获取列车管上次更换时间
        /// </summary>
        /// <param name="locotype"></param>
        /// <param name="loconum"></param>
        /// <returns></returns>
        public static lsDatLieCheGuan getLastLcgJianCeRiQi(string locotype, string loconum, int jcid)
        {
            string strSql = "SELECT TOP (1) a.*,(SELECT TianShu FROM dbo.lsDicJianCeTianShu WHERE (LocoType = b.LocoType) AND (FenLei = '列车管') AND (UnitId = b.UnitId)) as ts FROM dbo.lsDatLieCheGuan a,lsDatJT6JiChe b WHERE (a.LocoType = b.LocoType) AND (a.LocoNum = b.LocoNum) AND (a.LocoNum = @LocoNum) AND (a.LocoType = @LocoType) ORDER BY a.JianChaDate DESC";
            if (jcid !=0)
            {
                strSql = "SELECT TOP (1) a.*,(SELECT TianShu FROM dbo.lsDicJianCeTianShu WHERE (LocoType = b.LocoType) AND (FenLei = '列车管') AND (UnitId = b.UnitId)) as ts FROM dbo.lsDatLieCheGuan a,lsDatJT6JiChe b WHERE a.LocoType = b.LocoType AND a.LocoNum = b.LocoNum and b.ID=@ID ORDER BY a.JianChaDate DESC";
            }

            SqlParameter[] sqlParams = {
                                           new SqlParameter("LocoType",locotype),
                                           new SqlParameter("LocoNum",loconum),
                                           new SqlParameter("ID",jcid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            lsDatLieCheGuan lsdatlcg = new lsDatLieCheGuan();
            if (dt.Rows.Count > 0)
            {
                lsdatlcg.nGhts = PageBase.static_ext_int(dt.Rows[0]["ts"]);
                SetValue(lsdatlcg,dt.Rows[0]);
            }
            return lsdatlcg;
        }
    }
}
