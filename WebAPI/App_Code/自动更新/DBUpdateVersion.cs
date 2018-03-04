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
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using System.Collections;

/// <summary>
///自动更新数据库操作
/// </summary>
public class DBUpdateVersion
{
    public int nid = 0;
    public string strProjectID = "";
    public DateTime dtCreateTime = DateTime.MinValue;
    public string strProjectVersion = "";
    public string strUpdateBrief = "";
    public string strPackageUrl = "";
    public string strMainExeName = "";

	public DBUpdateVersion()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static bool GetProjectVersion(string ProjectID,DBUpdateVersion NewVersion)
    {
        string strSql = "select top 1 * from VIEW_AutoUpdate_Version where strProjectID = @strProjectID order by dtCreateTime desc";
        SqlParameter[] sqlParams = {
                                       new SqlParameter("strProjectID",ProjectID)
                                   };
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        if (dt.Rows.Count > 0)
        {
            NewVersion.nid = int.Parse(dt.Rows[0]["nid"].ToString());
            NewVersion.strProjectID = dt.Rows[0]["strProjectID"].ToString();
            NewVersion.dtCreateTime = DateTime.Parse(dt.Rows[0]["dtCreateTime"].ToString());
            NewVersion.strProjectVersion = dt.Rows[0]["strProjectVersion"].ToString();
            NewVersion.strUpdateBrief = dt.Rows[0]["strUpdateBrief"].ToString();
            NewVersion.strPackageUrl = dt.Rows[0]["strPackageUrl"].ToString();
            NewVersion.strMainExeName = dt.Rows[0]["strMainExeName"].ToString();
            return true;
        }

        return false;
        
    }
}
