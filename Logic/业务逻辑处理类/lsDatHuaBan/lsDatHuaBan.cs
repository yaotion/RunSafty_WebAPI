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
    public class lsDatHuaBan
    {
        #region 属性
        public int ID;
        public string LocoType;
        public string LocoNum;
        public string buwei;
        public string FenLei;
        public string XingHao;
        public string BuWei2;
        public string FenLei2;
        public string XingHao2;
        public DateTime? jianCeRiQi;
        public decimal? houdu;
        public decimal? houdu2;
        public string jianCeRen;
        public int GovernUnitId;
        public int UnitId;
        public int DepartmentId;
        public int ApanageId;

        public decimal? YaLi;
        public decimal? ShengGong;
        public decimal? JiangGong;
        public decimal? YaLi2;
        public decimal? ShengGong2;
        public decimal? JiangGong2;
        public decimal? HouDu21;
        public decimal? HouDu22;
        public string strStepID;
        public int JiCheID;
        public string strID;
        public int nState;
        public DateTime? dtBeginTime;
        public DateTime? dtEndTime;
        public int nIsQualified;
        public int nStandardMinute;
        #endregion 属性


         #region 构造函数
        public lsDatHuaBan()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public lsDatHuaBan(string stepid, int JiCheID)
        {
            string Condition = " 1=1";
            Condition += stepid!=""?" and a.strStepID=@strStepID":"";
            string strSql = "select (select nStandardMinute from TAB_HandleStep where a.strStepID=strID) as nStandardMinute,a.* from lsDatHuaBan a where " + Condition + " and a.JiCheID=@JiCheID order by a.dtBeginTime asc";
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
        /// <summary>
        /// 结构体赋值
        /// </summary>
        /// <param name="HuaBan"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static lsDatHuaBan SetValue(lsDatHuaBan HuaBan, DataRow dr)
        {
            if (dr != null)
            {
                HuaBan.LocoType = dr["LocoType"].ToString();
                HuaBan.LocoNum = dr["LocoNum"].ToString();
                HuaBan.FenLei = dr["FenLei"].ToString();
                HuaBan.FenLei2 = dr["FenLei2"].ToString();
                HuaBan.XingHao = dr["XingHao"].ToString();
                HuaBan.buwei = dr["buwei"].ToString();
                HuaBan.jianCeRiQi = PageBase.static_ext_date(dr["jianCeRiQi"].ToString());
                HuaBan.houdu = PageBase.static_ext_decimal(dr["houdu"].ToString());
                HuaBan.houdu2 = PageBase.static_ext_decimal(dr["houdu2"].ToString());
                HuaBan.HouDu21 = PageBase.static_ext_decimal(dr["HouDu21"].ToString());
                HuaBan.HouDu22 = PageBase.static_ext_decimal(dr["HouDu22"].ToString());
                HuaBan.YaLi = PageBase.static_ext_decimal(dr["YaLi"].ToString());
                HuaBan.ShengGong = PageBase.static_ext_decimal(dr["ShengGong"].ToString());
                HuaBan.JiangGong = PageBase.static_ext_decimal(dr["JiangGong"].ToString());
                HuaBan.YaLi2 = PageBase.static_ext_decimal(dr["YaLi2"].ToString());
                HuaBan.ShengGong2 = PageBase.static_ext_decimal(dr["ShengGong2"].ToString());
                HuaBan.JiangGong2 = PageBase.static_ext_decimal(dr["JiangGong2"].ToString());
                HuaBan.jianCeRen = dr["jianCeRen"].ToString();
                HuaBan.GovernUnitId = PageBase.static_ext_int(dr["GovernUnitId"].ToString());
                HuaBan.UnitId = PageBase.static_ext_int(dr["UnitId"].ToString());
                HuaBan.DepartmentId = PageBase.static_ext_int(dr["DepartmentId"].ToString());
                HuaBan.ApanageId = PageBase.static_ext_int(dr["ApanageId"].ToString());
                HuaBan.strStepID = dr["strStepID"].ToString();
                HuaBan.JiCheID =PageBase.static_ext_int(dr["JiCheID"].ToString());
                HuaBan.dtBeginTime = PageBase.static_ext_date(dr["dtBeginTime"].ToString());
                HuaBan.strID = dr["strID"].ToString();
                HuaBan.dtEndTime = PageBase.static_ext_date(dr["dtEndTime"].ToString());
                HuaBan.nState = PageBase.static_ext_int(dr["nState"].ToString());
                HuaBan.nIsQualified = PageBase.static_ext_int(dr["nIsQualified"].ToString());
                HuaBan.nStandardMinute = PageBase.static_ext_int(dr["nStandardMinute"].ToString());
            }
            return HuaBan;
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = @"insert into lsDatHuaBan (LocoType,LocoNum,FenLei,XingHao,buwei,jianCeRiQi,houdu,houdu2,YaLi,ShengGong,JiangGong,YaLi2,ShengGong2,JiangGong2,jianCeRen,GovernUnitId,UnitId,DepartmentId,ApanageId,strStepID,JiCheID,dtBeginTime,strID,dtEndTime,HouDu22,HouDu21,FenLei2,BuWei2,XingHao2,nState,nIsQualified) 
            values (@LocoType,@LocoNum,@FenLei,@XingHao,@buwei,@jianCeRiQi,@houdu,@houdu2,@YaLi,@ShengGong,@JiangGong,@YaLi2,@ShengGong2,@JiangGong2,@jianCeRen,@GovernUnitId,@UnitId,@DepartmentId,@ApanageId,@strStepID,@JiCheID,@dtBeginTime,@strID,@dtEndTime,@HouDu22,@HouDu21,@FenLei2,@BuWei2,@XingHao2,@nState,@nIsQualified)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum),
                                           new SqlParameter("FenLei",FenLei),
                                           new SqlParameter("XingHao",XingHao),
                                           new SqlParameter("buwei",buwei),
                                           new SqlParameter("jianCeRiQi",jianCeRiQi),
                                           new SqlParameter("houdu",houdu),
                                           new SqlParameter("houdu2",houdu2),
                                           new SqlParameter("YaLi",YaLi),
                                           new SqlParameter("ShengGong",ShengGong),
                                           new SqlParameter("JiangGong",JiangGong),
                                           new SqlParameter("YaLi2",YaLi2),
                                           new SqlParameter("ShengGong2",ShengGong2),
                                           new SqlParameter("JiangGong2",JiangGong2),
                                           new SqlParameter("jianCeRen",jianCeRen),
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
                                            new SqlParameter("HouDu22",HouDu22),
                                            new SqlParameter("HouDu21",HouDu21),
                                            new SqlParameter("FenLei2",FenLei2),
                                            new SqlParameter("BuWei2",BuWei2),
                                            new SqlParameter("XingHao2",XingHao2),
                                            new SqlParameter("nIsQualified",nIsQualified)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = @"update lsDatHuaBan set LocoType=@LocoType,LocoNum=@LocoNum,FenLei=@FenLei,XingHao=@XingHao,buwei=@buwei,jianCeRiQi=@jianCeRiQi,
houdu=@houdu,houdu2=@houdu2,YaLi=@YaLi,ShengGong=@ShengGong,JiangGong=@JiangGong,YaLi2=@YaLi2,ShengGong2=@ShengGong2,
JiangGong2=@JiangGong2,jianCeRen=@jianCeRen,GovernUnitId=@GovernUnitId,UnitId=@UnitId,ApanageId=@ApanageId,
DepartmentId=@DepartmentId,strStepID=@strStepID,JiCheID=@JiCheID,dtBeginTime=@dtBeginTime,dtEndTime=@dtEndTime,nState=@nState,
HouDu22=@HouDu22,HouDu21=@HouDu21,FenLei2=@FenLei2,BuWei2=@BuWei2,XingHao2=@XingHao2,nIsQualified=@nIsQualified where JiCheID=@JiCheID and strStepID=@strStepID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum),
                                           new SqlParameter("FenLei",FenLei),
                                           new SqlParameter("XingHao",XingHao),
                                           new SqlParameter("buwei",buwei),
                                           new SqlParameter("jianCeRiQi",jianCeRiQi),
                                           new SqlParameter("houdu",houdu),
                                           new SqlParameter("houdu2",houdu2),
                                           new SqlParameter("YaLi",YaLi),
                                           new SqlParameter("ShengGong",ShengGong),
                                           new SqlParameter("JiangGong",JiangGong),
                                           new SqlParameter("YaLi2",YaLi2),
                                           new SqlParameter("ShengGong2",ShengGong2),
                                           new SqlParameter("JiangGong2",JiangGong2),
                                           new SqlParameter("jianCeRen",jianCeRen),
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
                                            new SqlParameter("HouDu22",HouDu22),
                                            new SqlParameter("HouDu21",HouDu21),
                                            new SqlParameter("FenLei2",FenLei2),
                                            new SqlParameter("BuWei2",BuWei2),
                                            new SqlParameter("XingHao2",XingHao2),
                                            new SqlParameter("nIsQualified",nIsQualified)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete lsDatHuaBan where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int JiCheID, string stepid)
        {
            string strSql = "select count(*) from lsDatHuaBan where JiCheID=@JiCheID and strStepID=@strStepID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("strStepID",stepid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }

        public static bool IsQualified(int JiCheID, string stepid)
        {
            string strSql = "select nIsQualified from lsDatHuaBan where JiCheID=@JiCheID and strStepID=@strStepID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("strStepID",stepid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        /// <summary>
        /// 根据id值判断风速值是否有空 true 有空
        /// </summary>
        /// <param name="strid"></param>
        /// <returns></returns>
        public static int getCountFromStepID(string stepid)
        {
            string strSql = "select count(*) from lsDatHuaBan where strStepID=@stepid and nState=1";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("stepid",stepid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams));
        }
        #endregion 增删改
        /// <summary>
        /// 根据传入where条件获取风速记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static lsDatHuaBan HuaBanRecord(string where)
        {
            string strSql = "select (select nStandardMinute from TAB_HandleStep where a.strStepID=strID) as nStandardMinute,a.* from lsDatHuaBan a where 1=1 " + where;
            DataTable dtHuaBan = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql).Tables[0];
            lsDatHuaBan HuaBan = new lsDatHuaBan();
            return SetValue(HuaBan, dtHuaBan.Rows[0]);
        }
    }
}
