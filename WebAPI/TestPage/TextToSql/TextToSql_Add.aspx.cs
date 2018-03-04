using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using System.Data;
using System.Text.RegularExpressions;

public partial class TestPage_TextToSql_TextToSql_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string TxtPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\TestPage\\TextToSql\\TextFile\\1.txt";
        StreamReader sr = new StreamReader(TxtPath, Encoding.UTF8);
        string nextLine;

        logs l = new logs();
        while ((nextLine = sr.ReadLine()) != null)
         {
             if (nextLine.Contains("IP"))
             {
                 try
                 {
                     string[] TimeAndIp = Regex.Split(nextLine, "IP:", RegexOptions.IgnoreCase);
                     l.strIp = TimeAndIp[1].ToString();
                     string time1 = TimeAndIp[0];
                     l.dtCreatTime = time1.Trim();
                 }
                 catch
                 {
                     l.strIp = "无ip";
                     l.dtCreatTime = "无时间";
                 }
             }

             if (nextLine.Contains("接口名称"))
             {
                 l.strName = nextLine;
             }
            
             if (nextLine.Contains("执行用时"))
             {
                 string[] strTimes = nextLine.Split(':');
                 string strTime1 = strTimes[1].ToString();
                 strTime1 = strTime1.Substring(0, strTime1.Length - 2);
                 l.nTimes = Convert.ToInt32(strTime1);
             }
           
             if (nextLine.Contains("接口参数"))
             {
                 l.strData = nextLine;
             }
             if (nextLine == "---------------------------------------------------------------------")
             {
                 Addlogs(l);
             }
         }
        sr.Close(); 
    }

    public class logs
    {
        public string dtCreatTime;
        public string strName;
        public int nTimes;
        public string strData;
        public string strIp;
    }

    public bool Addlogs(logs model)
    {
        string strSql = "insert into [Tab_System_TxtLog] (dtCreatTime,strName,nTimes,strData,strIp) values (@dtCreatTime,@strName,@nTimes,@strData,@strIp)";
        SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("dtCreatTime",model.dtCreatTime),
                new SqlParameter("strName",model.strName),
                new SqlParameter("nTimes",model.nTimes),
                new SqlParameter("strData",model.strData),
                new SqlParameter("strIp",model.strIp)
            };
        return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters) > 0;
    }

}