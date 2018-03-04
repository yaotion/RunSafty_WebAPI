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

namespace ThinkFreely.RunSafty
{
    /// <summary>
    ///SmallGuidGroup功能：提供指导组信息增删改查
    /// </summary>
    public class SmallGuidGroup
    {
        #region 属性
        public string strSmallGuideGroupGUID;
        public string strGuideGroupGUID;
        public string strSmallGuideGroupName;
        public string strSmallGuideGroupNumber;
        #endregion 属性

        #region 构造函数
        public SmallGuidGroup()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public SmallGuidGroup(string strid)
        {
            string strSql = "select * from VIEW_Org_SmallGuidGroup where strSmallGuideGroupGUID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strSmallGuideGroupGUID = dt.Rows[0]["strSmallGuideGroupGUID"].ToString();
                strGuideGroupGUID = dt.Rows[0]["strGuideGroupGUID"].ToString();
                strSmallGuideGroupName = dt.Rows[0]["strSmallGuideGroupName"].ToString();
                strSmallGuideGroupNumber = dt.Rows[0]["strSmallGuideGroupNumber"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Org_SmallGuidGroup (strSmallGuideGroupGUID,strGuideGroupGUID,strSmallGuideGroupName,strSmallGuideGroupNumber) values (@strSmallGuideGroupGUID,@strGuideGroupGUID,@strSmallGuideGroupName,@strSmallGuideGroupNumber)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSmallGuideGroupGUID",guid),
                                           new SqlParameter("strGuideGroupGUID",strGuideGroupGUID),
                                           new SqlParameter("strSmallGuideGroupName",strSmallGuideGroupName),
                                           new SqlParameter("strSmallGuideGroupNumber",strSmallGuideGroupNumber)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Org_SmallGuidGroup set strGuideGroupGUID = @strGuideGroupGUID,strSmallGuideGroupName=@strSmallGuideGroupName,strSmallGuideGroupNumber=@strSmallGuideGroupNumber where strSmallGuideGroupGUID=@strSmallGuideGroupGUID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strSmallGuideGroupGUID",strSmallGuideGroupGUID),
                                           new SqlParameter("strGuideGroupGUID",strGuideGroupGUID),
                                           new SqlParameter("strSmallGuideGroupName",strSmallGuideGroupName),
                                           new SqlParameter("strSmallGuideGroupNumber",strSmallGuideGroupNumber)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_Org_SmallGuidGroup where strSmallGuideGroupGUID=@strSmallGuideGroupGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSmallGuideGroupGUID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string strid, string strSmallGuideGroupName, string GuideGroupGuid)
        {
            string strSql = "select count(*) from TAB_Org_SmallGuidGroup where strSmallGuideGroupName=@strSmallGuideGroupName and strGuideGroupGUID=@strGuideGroupGUID";
            if (strid != "")
            {
                strSql += " and strSmallGuideGroupGUID <> @strSmallGuideGroupGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strSmallGuideGroupGUID",strid),
                                           new SqlParameter("strSmallGuideGroupName",strSmallGuideGroupName),
                                           new SqlParameter("strGuideGroupGUID",GuideGroupGuid)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetAllSmallGuideGroup(string name)
        {
            string strSql = "select * from TAB_Org_SmallGuidGroup";
            if (name != "")
            {
                strSql += " and strSmallGuideGroupName like @name ";
            }
            strSql += " order by nid ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("name","%" +name+ "%")
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        //public static DataTable GetAllAreasDic(string DefaultName)
        //{
        //    DataTable dtResult = GetAllAreas("");
        //    DataRow dr = dtResult.NewRow();
        //    dr["strGUID"] = "";
        //    dr["strAreaGUID"] = DefaultName;
        //    dtResult.Rows.InsertAt(dr, 0);
        //    return dtResult;
        //}

        #endregion
    }
}
