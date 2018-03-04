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
    public class EquipmentTypeFault
    {
        #region 属性
        public int nID;
        public string strID;
        public int nApanageID;
        public int nUnitID;
        public int nEquipmentType;
        public int nState;
        public string strEquipmentNumber;
        public string strFaultReason;
        public string strIntroducer;
        public DateTime? dtPresentDate;//提出时间
        public DateTime? dtRepairDate;//维修时间
        public DateTime? dtTehdasasetuksetDate;//返厂时间
        public DateTime? dtReplacement;//替换时间
        #endregion 属性

        #region 构造函数
        public EquipmentTypeFault()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public EquipmentTypeFault(string strid)
        {
            string strSql = "select top 1 * from TAB_EquipmentTypeFault where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                Read(this,dt.Rows[0]);
            }
        }
        #endregion 构造函数


        //功能:读取版本信息
        private static void Read(EquipmentTypeFault etf, DataRow Row)
        {
            etf.nID = PageBase.static_ext_int(Row["nID"].ToString());
            etf.strID = PageBase.static_ext_string(Row["strID"].ToString());
            etf.nApanageID = PageBase.static_ext_int(Row["nApanageID"].ToString());
            etf.nEquipmentType = PageBase.static_ext_int(Row["nEquipmentType"].ToString());
            etf.nState = PageBase.static_ext_int(Row["nState"].ToString());
            etf.nUnitID = PageBase.static_ext_int(Row["nUnitID"].ToString());
            etf.strID = Row["strID"].ToString();
            etf.strEquipmentNumber = PageBase.static_ext_string(Row["strEquipmentNumber"].ToString());
            etf.strFaultReason = PageBase.static_ext_string(Row["strFaultReason"].ToString());
            etf.strIntroducer = PageBase.static_ext_string(Row["strIntroducer"].ToString());
            etf.dtPresentDate = PageBase.static_ext_date(Row["dtPresentDate"].ToString());
            etf.dtRepairDate = PageBase.static_ext_date(Row["dtRepairDate"].ToString());
            etf.dtTehdasasetuksetDate = PageBase.static_ext_date(Row["dtTehdasasetuksetDate"].ToString());
            etf.dtReplacement = PageBase.static_ext_date(Row["dtReplacement"].ToString());
        }

        //功能:获得整备场历史更新信息
        public static List<EquipmentTypeFault> getEquipmentTypeFaultList(int unitid, int apanageid)
        {
            List<EquipmentTypeFault> EquipmentTypeFaultList = new List<EquipmentTypeFault>();
            string strSql = "select * from TAB_EquipmentTypeFault where nUnitID=@UnitID and nApanageID=@ApanageID";
            SqlParameter[] sqlParams = {
                                            new SqlParameter("UnitID",unitid),
                                           new SqlParameter("ApanageID",apanageid)
                                       };
            DataTable dtEquipmentTypeFault = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            foreach (DataRow dr in dtEquipmentTypeFault.Rows)
            {
                EquipmentTypeFault etf = new EquipmentTypeFault();
                EquipmentTypeFault.Read(etf, dr);
                EquipmentTypeFaultList.Add(etf);
            }
            return EquipmentTypeFaultList;

        }


        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_EquipmentTypeFault (nUnitID,nApanageID,nEquipmentType,nState,strEquipmentNumber,strFaultReason,strIntroducer,dtPresentDate,dtRepairDate,dtTehdasasetuksetDate,dtReplacement) values (@nUnitID,@nApanageID,@nEquipmentType,@nState,@strEquipmentNumber,@strFaultReason,@strIntroducer,@dtPresentDate,@dtRepairDate,@dtTehdasasetuksetDate,@dtReplacement)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nUnitID",nUnitID),
                                           new SqlParameter("nApanageID",nApanageID),
                                           new SqlParameter("nEquipmentType",nEquipmentType),
                                           new SqlParameter("nState",nState),
                                           new SqlParameter("strEquipmentNumber",strEquipmentNumber),
                                            new SqlParameter("strFaultReason",strFaultReason),
                                           new SqlParameter("strIntroducer",strIntroducer),
                                           new SqlParameter("dtPresentDate",dtPresentDate),
                                           new SqlParameter("dtRepairDate",dtRepairDate),
                                           new SqlParameter("dtTehdasasetuksetDate",dtTehdasasetuksetDate),
                                           new SqlParameter("dtReplacement",dtReplacement)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_EquipmentTypeFault set nUnitID=@nUnitID,nApanageID=@nApanageID,nEquipmentType=@nEquipmentType,nState=@nState,strEquipmentNumber=@strEquipmentNumber,strFaultReason=@strFaultReason,strIntroducer=@strIntroducer,dtPresentDate=@dtPresentDate,dtRepairDate=@dtRepairDate,dtTehdasasetuksetDate=@dtTehdasasetuksetDate,dtReplacement=@dtReplacement where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strID),
                                         new SqlParameter("nUnitID",nUnitID),
                                           new SqlParameter("nApanageID",nApanageID),
                                           new SqlParameter("nEquipmentType",nEquipmentType),
                                           new SqlParameter("nState",nState),
                                           new SqlParameter("strEquipmentNumber",strEquipmentNumber),
                                            new SqlParameter("strFaultReason",strFaultReason),
                                           new SqlParameter("strIntroducer",strIntroducer),
                                           new SqlParameter("dtPresentDate",dtPresentDate),
                                           new SqlParameter("dtRepairDate",dtRepairDate),
                                           new SqlParameter("dtTehdasasetuksetDate",dtTehdasasetuksetDate),
                                           new SqlParameter("dtReplacement",dtReplacement)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
       
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_EquipmentTypeFault where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        //public static bool Exist(int apanageid, string strVersionName, string strid)
        //{
        //    string strSql = "select count(*) from TAB_EquipmentTypeFault where ApanageID=@ApanageID and strVersionName=@strVersionName and strAPKFileName=@strAPKFileName";
        //    strSql += strid != "" ? " and strID=@strID" : "";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("ApanageID",apanageid),
        //                                   new SqlParameter("strVersionName",strVersionName),
        //                                   new SqlParameter("strAPKFileName",strAPKFileName)
        //                               };
        //    return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        //}
        #endregion 增删改

        #region 扩展方法

        ///// <summary>
        ///// 由站场id 获取当前站场手持机版本号
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <returns></returns>
        //public static string GetVersion(int apanageid,int unitid)
        //{
        //    string strSql = "select strVersionName from TAB_EquipmentTypeFault where ApanageID = @ApanageID and UnitID=@UnitID";
        //    strSql += " order by dtPublished desc";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("ApanageID",apanageid),
        //                                   new SqlParameter("UnitID",unitid)
        //                               };
        //    return PageBase.static_ext_string(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams));
        //}
        #endregion

    }
}
