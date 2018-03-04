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
///WebInterface 的摘要说明
/// </summary>
public class WebInterface
{
	public WebInterface()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
  
   
    /// <summary>
    /// 获取需要超劳预警的数据的JSON形式
    /// </summary>
    /// <returns></returns>
    public static string GetOutOfWorkTimeJson()
    {
        string strSql = "Proc_WorkTime_GetOutOfWorkTime";

        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.StoredProcedure, strSql).Tables[0];

        string rltJson = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string strTemp = " 'id':'{0}','number':'{1}','name':'{2}','cc':'{3}','cx':'{4}','ch':'{5}','cqxj':'{6}','cysj':'{7}','tmtype':'{8}','kh':'{9}','outminutes':'{10}','alarmminutes':'{11}'";
            string strChuQinTime = "";
            string strOutRoomTime = "";
            if (dt.Rows[i]["nRestBack"].ToString() == "1")
            {
                strOutRoomTime = DateTime.Parse(dt.Rows[i]["dtStartTime"].ToString()).ToString("yyyyMMddHHmmss");
            }
            else
            {
                strChuQinTime = DateTime.Parse(dt.Rows[i]["dtStartTime"].ToString()).ToString("yyyyMMddHHmmss");
            }
           
            strTemp = "{" + string.Format(strTemp, dt.Rows[i]["nid"].ToString(), dt.Rows[i]["strTrainmanNumber"].ToString(),
                dt.Rows[i]["strTrainmanName"].ToString(), dt.Rows[i]["strTrainNo"].ToString(),
                dt.Rows[i]["strTrainTypeName"].ToString(), dt.Rows[i]["strTrainNumber"].ToString(),
                strChuQinTime, strOutRoomTime,
                dt.Rows[i]["nTrainmanTypeID"].ToString(), dt.Rows[i]["nKehuoID"].ToString(), dt.Rows[i]["outminutes"].ToString(), dt.Rows[i]["alarmminutes"].ToString()) + "}";
            if (rltJson == "")
            {
                rltJson = strTemp;
            }
            else
            {
                rltJson = rltJson + "," + strTemp;
            }
        }
        rltJson = "[" +rltJson+ "]";
        return rltJson;
    }
    /// <summary>
    /// 确认超劳预警
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static void ConfirmOutOfWorkTime(string id)
    {
        string strSql = "update TAB_WorkTime_Turn_Branch set nNoticeState = 1 where nid = @ID";
        SqlParameter[] sqlParams = { 
                                       new SqlParameter("ID",id)
                                   };
        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
    }
}
