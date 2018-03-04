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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

/// <summary>
///StationInWorkShop 的摘要说明
/// </summary>
public class StationInWorkShop
{
    public string RecordGUID = "";
    public string WorkShopGUID = "";
    public string StationGUID = "";
    public int TMIS = 0;
    public int StationIndex = 0;
    public int IsLocal = 0;
    public int IsGoBack = 0;
    public string StationName = "";
    public int bIsLocalStart = 0;

	public StationInWorkShop()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public static List<StationInWorkShop> GetStations(string WorkShopGUID)
    {

        List<StationInWorkShop> StationList = new List<StationInWorkShop>();
        string strSql = "select * from VIEW_Base_StationInWorkShop where strWorkShopGUID = @WorkShopGUID order by nStationIndex";
        SqlParameter[] sqlParams = {
                                       new SqlParameter("WorkShopGUID",WorkShopGUID)
                                   };
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            StationInWorkShop station = new StationInWorkShop();
            station.RecordGUID = dt.Rows[i]["strRecordGUID"].ToString();
            station.WorkShopGUID = dt.Rows[i]["strWorkShopGUID"].ToString();
            station.StationGUID = dt.Rows[i]["strStationGUID"].ToString();
            station.TMIS = Int32.Parse(dt.Rows[i]["nTMIS"].ToString());
            station.StationIndex = Int32.Parse(dt.Rows[i]["nStationIndex"].ToString());
            station.IsLocal = Int32.Parse(dt.Rows[i]["nIsLocal"].ToString());
            station.IsGoBack = Int32.Parse(dt.Rows[i]["nIsGoBack"].ToString());
            station.StationName = dt.Rows[i]["strStationName"].ToString();
            station.bIsLocalStart = Int32.Parse(dt.Rows[i]["nIsLocalStart"].ToString());
            
            StationList.Add(station);
        }

        return StationList;
    }
}
