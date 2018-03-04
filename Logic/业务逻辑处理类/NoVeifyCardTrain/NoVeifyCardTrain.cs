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
    ///NoVeifyCardTrain 的摘要说明
    /// </summary>
    public class NoVeifyCardTrain
    {
        #region 属性
        public string strID{ get; set;}
        public string strTrainNo { get; set; }
        public string strMark { get; set;}
        #endregion 属性

        #region 构造函数
        public NoVeifyCardTrain()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public NoVeifyCardTrain(string strid)
        {
            string strSql = "select * from Tab_NoVeifyCardTrain where strID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strID = dt.Rows[0]["strID"].ToString();
                strTrainNo = dt.Rows[0]["strTrainNo"].ToString();
                strMark = dt.Rows[0]["strMark"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string strSql = "insert into Tab_NoVeifyCardTrain (strTrainNo,strMark) values (@strTrainNo,@strMark)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainNo",strTrainNo),
                                           new SqlParameter("strMark",strMark) 
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public bool Update()
        {
            string strSql = "update Tab_NoVeifyCardTrain set strTrainNo = @strTrainNo,strMark=@strMark where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strID),
                                           new SqlParameter("strTrainNo",strTrainNo),
                                           new SqlParameter("strMark",strMark)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete Tab_NoVeifyCardTrain where strID=@strID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(string trainno, string strMark)
        {
            string strSql = "select count(*) from Tab_NoVeifyCardTrain where strTrainNo = @trainno and strMark=@strMark";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("trainno",trainno),
                                           new SqlParameter("strMark",strMark)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        //public static DataTable GetAllAreas(string AreaName)
        //{
        //    string strSql = "select * from VIEW_Org_Area ";
        //    if (AreaName != "")
        //    {
        //        strSql += " and strTrainNo like @strTrainNo ";
        //    }
        //    strSql += " order by strTrainNo ";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("strTrainNo","%" +AreaName+ "%")
        //                               };
        //    return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        //}
        #endregion
    }
}