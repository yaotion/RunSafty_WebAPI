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
    public class lsDatLvWang
    {
        #region 属性
        public string ID;
        public string LocoType;
        public string LocoNum;
        public string banCi;
        public DateTime? genghuanriqi;
        public string genghuanren;
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
        public lsDatLvWang()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public lsDatLvWang(string jcid)
        {
            string strSql = "select * from lsDatLvWang where  JiCheID=@JiCheID";
            SqlParameter[] sqlParams = {
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
        public static lsDatLvWang SetValue(lsDatLvWang LvWang, DataRow dr)
        {
            if (dr != null)
            {
                LvWang.ID = dr["strID"].ToString();
                LvWang.LocoType = dr["LocoType"].ToString();
                LvWang.LocoNum = dr["LocoNum"].ToString();
                LvWang.banCi = dr["banCi"].ToString();
                LvWang.genghuanriqi = PageBase.static_ext_date(dr["genghuanriqi"].ToString());
                LvWang.genghuanren = dr["genghuanren"].ToString();
                LvWang.GovernUnitId = PageBase.static_ext_int(dr["GovernUnitId"].ToString());
                LvWang.UnitId = PageBase.static_ext_int(dr["UnitId"].ToString());
                LvWang.DepartmentId = PageBase.static_ext_int(dr["DepartmentId"].ToString());
                LvWang.ApanageId = PageBase.static_ext_int(dr["ApanageId"].ToString());
                LvWang.strStepID = dr["strStepID"].ToString();
                LvWang.strID = dr["strID"].ToString();
                LvWang.JiCheID = PageBase.static_ext_int(dr["JiCheID"].ToString());
                LvWang.nState = PageBase.static_ext_int(dr["nState"].ToString());
                LvWang.dtBeginTime = PageBase.static_ext_date(dr["dtBeginTime"].ToString());
                LvWang.dtEndTime = PageBase.static_ext_date(dr["dtEndTime"].ToString());
                LvWang.strFileName = dr["strFileName"].ToString();
            }
            return LvWang;
        }
        #endregion 构造函数
        #region 增删改
        public bool Add()
        {
            string strSql = "insert into lsDatLvWang (strID,LocoType,LocoNum,banCi,genghuanriqi,genghuanren,GovernUnitId,UnitId,DepartmentId,ApanageId,strStepID,JiCheID,nState,dtBeginTime,dtEndTime,strFileName) values (@strID,@LocoType,@LocoNum,@banCi,@genghuanriqi,@genghuanren,@GovernUnitId,@UnitId,@DepartmentId,@ApanageId,@strStepID,@JiCheID,@nState,@dtBeginTime,@dtEndTime,@strFileName)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strID),
                                           new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum),
                                           new SqlParameter("banCi",banCi),
                                           new SqlParameter("genghuanriqi",genghuanriqi),
                                           new SqlParameter("genghuanren",genghuanren),
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
            string strSql = "update lsDatLvWang set LocoType=@LocoType,LocoNum=@LocoNum,banCi=@banCi,genghuanriqi=@genghuanriqi,genghuanren=@genghuanren,GovernUnitId=@GovernUnitId,UnitId=@UnitId,DepartmentId=@DepartmentId,ApanageId=@ApanageId,strStepID=@strStepID,JiCheID=@JiCheID,nState=@nState,dtBeginTime=@dtBeginTime,dtEndTime=@dtEndTime,strFileName=@strFileName where JiCheID=@JiCheID and strStepID=@strStepID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strID",strID),
                                           new SqlParameter("LocoType",LocoType),
                                           new SqlParameter("LocoNum",LocoNum),
                                           new SqlParameter("banCi",banCi),
                                           new SqlParameter("genghuanriqi",genghuanriqi),
                                           new SqlParameter("genghuanren",genghuanren),
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
            string strSql = "delete lsDatLvWang where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string JiCheID, string stepid)
        {
            string strSql = "select count(*) from lsDatLvWang where JiCheID=@JiCheID and strStepID=@strStepID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("JiCheID",JiCheID),
                                           new SqlParameter("strStepID",stepid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        /// <summary>
        /// 根据传入where条件获取滤网记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static lsDatLvWang LvWangRecord(string where)
        {
            string strSql = "select * from lsDatLvWang where 1=1 " + where;
            DataTable dtLvWang = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql).Tables[0];
            lsDatLvWang LvWang = new lsDatLvWang();
            return SetValue(LvWang, dtLvWang.Rows[0]);
        }

        /// <summary>
        /// 根据车型车号获取滤网上次更换时间
        /// </summary>
        /// <param name="locotype"></param>
        /// <param name="loconum"></param>
        /// <returns></returns>
        public static lsDatLvWang getLastLvWangJianCeRiQi(string locotype, string loconum, int jcid)
        {
            string strSql = "SELECT TOP (1) a.*,(SELECT TianShu FROM dbo.lsDicJianCeTianShu WHERE (LocoType = b.LocoType) AND (FenLei = '滤网') AND (UnitId = b.UnitId)) as ts FROM dbo.lsDatLvWang a,lsDatJT6JiChe b WHERE (a.LocoType = b.LocoType) AND (a.LocoNum = b.LocoNum) AND (a.LocoNum = @LocoNum) AND (a.LocoType = @LocoType) ORDER BY a.genghuanriqi DESC";
            if (jcid != 0)
            {
                strSql = "SELECT TOP (1) a.*,(SELECT TianShu FROM dbo.lsDicJianCeTianShu WHERE (LocoType = b.LocoType) AND (FenLei = '滤网') AND (UnitId = b.UnitId)) as ts FROM dbo.lsDatLvWang a,lsDatJT6JiChe b WHERE a.LocoType = b.LocoType AND a.LocoNum = b.LocoNum and b.ID=@ID ORDER BY a.genghuanriqi DESC";
            }
            
            SqlParameter[] sqlParams = {
                                           new SqlParameter("LocoType",locotype),
                                           new SqlParameter("LocoNum",loconum),
                                           new SqlParameter("ID",jcid)
                                       };
            DataTable dt= SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            lsDatLvWang lsdatlvwang = new lsDatLvWang();
            if (dt.Rows.Count > 0)
            {
                lsdatlvwang.nGhts = PageBase.static_ext_int(dt.Rows[0]["ts"]);
                SetValue(lsdatlvwang, dt.Rows[0]);
            }
            return lsdatlvwang;
        }
    }
}
