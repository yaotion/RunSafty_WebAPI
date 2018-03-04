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
    public class MBManage
    {
        #region 属性
        public int nID;
        public string strID;
        public int nEquipmentType;//设备类型
        public int bIsSpares;//是否备品
        public string strEquipmentNumber;//设备编号
        public string strHolderNumber;//持有人工号
        public int nApanageID;//整备场id
        public int nUnitID;//机务段id
        #endregion 属性

        #region 构造函数
        public MBManage()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public MBManage(string strid)
        {
            string strSql = "select top 1 * from TAB_MBManage where strID=@strID";
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


        //功能:读取信息
        private static void Read(MBManage mbm, DataRow Row)
        {
            mbm.nApanageID = PageBase.static_ext_int(Row["nApanageID"].ToString());
            mbm.nUnitID = PageBase.static_ext_int(Row["nUnitID"].ToString());
            mbm.nID = PageBase.static_ext_int(Row["nID"].ToString());
            mbm.nEquipmentType = PageBase.static_ext_int(Row["nEquipmentType"].ToString());
            mbm.bIsSpares = PageBase.static_ext_int(Row["bIsSpares"].ToString());
            mbm.strID = Row["strID"].ToString();
            mbm.strEquipmentNumber = Row["strEquipmentNumber"].ToString();
            mbm.strHolderNumber = Row["strHolderNumber"].ToString();
        }

        //功能:获得整备场所有终端管理信息
        public static List<MBManage> getMBManageList(int nApanageID, int nUnitID)
        {
            List<MBManage> MBManageList = new List<MBManage>();
            string strSql = "select * from TAB_MBManage where nApanageID=@nApanageID and nUnitID=@nUnitID";
            SqlParameter[] sqlParams = {
                                            new SqlParameter("nApanageID",nApanageID),
                                           new SqlParameter("nUnitID",nUnitID)
                                       };
            DataTable dtZbAreaConfig = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            foreach (DataRow dr in dtZbAreaConfig.Rows)
            {
                MBManage mbm = new MBManage();
                MBManage.Read(mbm, dr);
                MBManageList.Add(mbm);
            }
            return MBManageList;

        }


        #region 增删改
        public bool Add()
        {
            string strSql = "insert into TAB_MBManage (nEquipmentType,bIsSpares,strEquipmentNumber,strHolderNumber,nApanageID,nUnitID) values (@nEquipmentType,@bIsSpares,@strEquipmentNumber,@strHolderNumber,@nApanageID,@nUnitID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nApanageID",nApanageID),
                                           new SqlParameter("nUnitID",nUnitID),
                                           new SqlParameter("nEquipmentType",nEquipmentType),
                                           new SqlParameter("bIsSpares",bIsSpares),
                                           new SqlParameter("strEquipmentNumber",strEquipmentNumber),
                                           new SqlParameter("strHolderNumber",strHolderNumber)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"),CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_MBManage set strEquipmentNumber=@strEquipmentNumber,strHolderNumber=@strHolderNumber,nEquipmentType=@nEquipmentType,bIsSpares=@bIsSpares,nApanageID=@nApanageID,nUnitID=@nUnitID where strID=@strID";
            SqlParameter[] sqlParams = {
                                            new SqlParameter("nApanageID",nApanageID),
                                           new SqlParameter("nUnitID",nUnitID),
                                           new SqlParameter("nEquipmentType",nEquipmentType),
                                           new SqlParameter("bIsSpares",bIsSpares),
                                           new SqlParameter("strEquipmentNumber",strEquipmentNumber),
                                           new SqlParameter("strHolderNumber",strHolderNumber),
                                           new SqlParameter("strID",strID)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
       
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_MBManage where 1=1";
            strSql += strid != "" ? " and strID=@strID" : "";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(int nEquipmentType, string strEquipmentNumber,string strID)
        {
            string strSql = "select count(*) from TAB_MBManage where nEquipmentType=@nEquipmentType and strEquipmentNumber=@strEquipmentNumber";
            strSql += strID == "" ? "" : " and strID<>@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nEquipmentType",nEquipmentType),
                                           new SqlParameter("strEquipmentNumber",strEquipmentNumber),
                                           new SqlParameter("strID",strID)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法

        ///// <summary>
        ///// 由站场id 获取当前站场手持机版本号
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <returns></returns>
        //public static string GetVersion(int bIsSpares,int nEquipmentType)
        //{
        //    string strSql = "select strEquipmentNumber from TAB_MBManage where bIsSpares = @bIsSpares and nEquipmentType=@nEquipmentType";
        //    strSql += " order by dtPublished desc";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("bIsSpares",bIsSpares),
        //                                   new SqlParameter("nEquipmentType",nEquipmentType)
        //                               };
        //    return PageBase.static_ext_string(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams));
        //}
        #endregion

    }
}
