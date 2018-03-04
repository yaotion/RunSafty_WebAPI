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
    public class lsDatZhiGongDian
    {
        #region 属性
        public string ID;
        public string LocoType;
        public string LocoNum;
        public string XingHao;
        public string FenLei;
        public string BuWei;
        public string banCi;
        public DateTime? JianCeRiQi;
        public string JianCeRen;
        public int GovernUnitId;
        public int UnitId;
        public int DepartmentId;
        public int ApanageId;
        public string strStepID;
        public string strID;
        public int JiCheID;
        public int nState;
        public int nSstate;
        public DateTime? dtBeginTime;
        public DateTime? dtEndTime;
        public decimal? DianYa;
        public decimal? DianYa2;
        #endregion 属性
         #region 构造函数
        public lsDatZhiGongDian()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public lsDatZhiGongDian(string stepid, int jcid)
        {
            string strSql = "select * from lsDatZhiGongDian where JiCheID=@JiCheID and strStepID=@stepid order by dtBeginTime asc";
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
                XingHao = dt.Rows[0]["XingHao"].ToString();
                FenLei = dt.Rows[0]["FenLei"].ToString();
                BuWei = dt.Rows[0]["BuWei"].ToString();
                banCi = dt.Rows[0]["banCi"].ToString();
                JianCeRiQi =PageBase.static_ext_date(dt.Rows[0]["JianCeRiQi"].ToString());
                DianYa = PageBase.static_ext_decimal(dt.Rows[0]["DianYa"].ToString());
                DianYa2 = PageBase.static_ext_decimal(dt.Rows[0]["DianYa2"].ToString());
                JianCeRen = dt.Rows[0]["JianCeRen"].ToString();
                GovernUnitId = PageBase.static_ext_int(dt.Rows[0]["GovernUnitId"].ToString());
                UnitId = PageBase.static_ext_int(dt.Rows[0]["UnitId"].ToString());
                DepartmentId = PageBase.static_ext_int(dt.Rows[0]["DepartmentId"].ToString());
                ApanageId = PageBase.static_ext_int(dt.Rows[0]["ApanageId"].ToString());
                strStepID = dt.Rows[0]["strStepID"].ToString();
                strID = dt.Rows[0]["strID"].ToString();
                JiCheID = PageBase.static_ext_int(dt.Rows[0]["JiCheID"].ToString());
                nState = PageBase.static_ext_int(dt.Rows[0]["nState"].ToString());
                nSstate = PageBase.static_ext_int(dt.Rows[0]["nSstate"].ToString());
                dtBeginTime = PageBase.static_ext_date(dt.Rows[0]["dtBeginTime"].ToString());
                dtEndTime = PageBase.static_ext_date(dt.Rows[0]["dtEndTime"].ToString());
            }
        }
        #endregion 构造函数
        #region 增删改
        public bool Add()
        {
            string strSql = "insert into lsDatZhiGongDian (strID,LocoType,LocoNum,XingHao,FenLei,BuWei,banCi,JianCeRiQi,DianYa,DianYa2,JianCeRen,GovernUnitId,UnitId,DepartmentId,ApanageId,strStepID,JiCheID,nState,nSstate,dtBeginTime,dtEndTime) values (@strID,@LocoType,@LocoNum,@XingHao,@FenLei,@BuWei,@banCi,@JianCeRiQi,@DianYa,@DianYa2,@JianCeRen,@GovernUnitId,@UnitId,@DepartmentId,@ApanageId,@strStepID,@JiCheID,@nState,@nSstate,@dtBeginTime,@dtEndTime)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strID),
                                           new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum),
                                           new SqlParameter("XingHao",XingHao),
                                            new SqlParameter("FenLei",FenLei),
                                             new SqlParameter("BuWei",BuWei),
                                           new SqlParameter("banCi",banCi),
                                           new SqlParameter("JianCeRiQi",JianCeRiQi),
                                           new SqlParameter("DianYa",DianYa),
                                           new SqlParameter("DianYa2",DianYa2),
                                           new SqlParameter("JianCeRen",JianCeRen),
                                           new SqlParameter("GovernUnitId",GovernUnitId),
                                           new SqlParameter("UnitId",UnitId),
                                           new SqlParameter("DepartmentId",DepartmentId),
                                           new SqlParameter("ApanageId",ApanageId),
                                           new SqlParameter("strStepID",strStepID),
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("nState",nState),
                                           new SqlParameter("nSstate",nSstate),
                                           new SqlParameter("dtBeginTime",dtBeginTime),
                                           new SqlParameter("dtEndTime",dtEndTime)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update lsDatZhiGongDian set LocoType=@LocoType,LocoNum=@LocoNum,XingHao=@XingHao,FenLei=@FenLei,BuWei=@BuWei,banCi=@banCi,DianYa=@DianYa,DianYa2=@DianYa2,JianCeRiQi=@JianCeRiQi,JianCeRen=@JianCeRen,GovernUnitId=@GovernUnitId,UnitId=@UnitId,DepartmentId=@DepartmentId,ApanageId=@ApanageId,strStepID=@strStepID,JiCheID=@JiCheID,nState=@nState,nSstate=@nSstate,dtBeginTime=@dtBeginTime,dtEndTime=@dtEndTime where JiCheID=@JiCheID and strStepID=@strStepID";
            SqlParameter[] sqlParams = {
                                         new SqlParameter("strID",strID),
                                           new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum),
                                           new SqlParameter("XingHao",XingHao),
                                           new SqlParameter("FenLei",FenLei),
                                             new SqlParameter("BuWei",BuWei),
                                           new SqlParameter("banCi",banCi),
                                           new SqlParameter("JianCeRiQi",JianCeRiQi),
                                           new SqlParameter("DianYa",DianYa),
                                           new SqlParameter("DianYa2",DianYa2),
                                           new SqlParameter("JianCeRen",JianCeRen),
                                           new SqlParameter("GovernUnitId",GovernUnitId),
                                           new SqlParameter("UnitId",UnitId),
                                           new SqlParameter("DepartmentId",DepartmentId),
                                           new SqlParameter("ApanageId",ApanageId),
                                           new SqlParameter("strStepID",strStepID),
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("nState",nState),
                                           new SqlParameter("nSstate",nSstate),
                                           new SqlParameter("dtBeginTime",dtBeginTime),
                                           new SqlParameter("dtEndTime",dtEndTime)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool UpdatenSstate()
        {
            string strSql = "update lsDatZhiGongDian set nSstate=1 where strStepID=@strStepID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strStepID",strStepID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete lsDatZhiGongDian where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strID)
        {
            string strSql = "select count(*) from lsDatZhiGongDian where strID=@strID ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strID)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        /// <summary>
        /// 根据stepid值获取数据数目
        /// </summary>
        /// <param name="strid"></param>
        /// <returns></returns>
        public static int getCountFromStepID(string stepid, int JiCheID)
        {
            string strSql = "select count(*) from lsDatZhiGongDian where strStepID=@stepid and JiCheID=@JiCheID and nState=1";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("stepid",stepid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams));
        }
        #endregion 增删改
    }
}
