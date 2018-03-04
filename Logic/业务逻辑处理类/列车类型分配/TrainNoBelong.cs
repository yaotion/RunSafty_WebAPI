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
    ///TrainNoBelong功能：提供列车类型分配增删改查
    /// </summary>
    public class TrainNoBelong
    {
        #region 属性
        public string strBelongGUID;
        public string strTrainNoHead;
        public string nBeginNumber;
        public string nEndNumber;
        public string nKehuoID;
        public string strKehuoName;
        public string strTrainHead;
        #endregion 属性

        #region 构造函数
        public TrainNoBelong()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public TrainNoBelong(string strid)
        {
            string strSql = "select * from VIEW_Base_TrainNoBelong where strBelongGUID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strBelongGUID = dt.Rows[0]["strBelongGUID"].ToString();
                strTrainNoHead = dt.Rows[0]["strTrainNoHead"].ToString();
                nBeginNumber = dt.Rows[0]["nBeginNumber"].ToString();
                nEndNumber = dt.Rows[0]["nEndNumber"].ToString();
                nKehuoID = dt.Rows[0]["nKehuoID"].ToString();
                strKehuoName = dt.Rows[0]["strKehuoName"].ToString();
                strTrainHead = dt.Rows[0]["strTrainNoHead"].ToString();
            }
        }
        #endregion 构造函数

        #region 增删改
        public bool Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Base_TrainNoBelong (strBelongGUID,strTrainNoHead,nBeginNumber,nEndNumber,nKehuoID) values (@strBelongGUID,@strTrainNoHead,@nBeginNumber,@nEndNumber,@nKehuoID)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strBelongGUID",guid),
                                           new SqlParameter("strTrainNoHead",strTrainNoHead),
                                           new SqlParameter("nBeginNumber",nBeginNumber),
                                           new SqlParameter("nEndNumber",nEndNumber),
                                           new SqlParameter("nKehuoID",nKehuoID) 
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams)> 0;
        }
        public bool Update()
        {
            string strSql = "update TAB_Base_TrainNoBelong set strTrainNoHead = @strTrainNoHead,nBeginNumber=@nBeginNumber,nEndNumber=@nEndNumber,nKehuoID=@nKehuoID where strBelongGUID=@strBelongGUID";
            SqlParameter[] sqlParams = {
                                          new SqlParameter("strBelongGUID",strBelongGUID),
                                           new SqlParameter("strTrainNoHead",strTrainNoHead),
                                           new SqlParameter("nBeginNumber",nBeginNumber),
                                           new SqlParameter("nEndNumber",nEndNumber),
                                           new SqlParameter("nKehuoID",nKehuoID) 
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string strid)
        {
            string strSql = "delete TAB_Base_TrainNoBelong where strBelongGUID=@strBelongGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strBelongGUID",strid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Exist(ThinkFreely.RunSafty.TrainNoBelong model)
        {
            string strSql = "select count(*) from TAB_Base_TrainNoBelong where nKehuoID=@nKehuoID and strTrainNoHead=@strTrainNoHead and nBeginNumber=@nBeginNumber and nEndNumber=@nEndNumber";
            if (model.strBelongGUID!= "")
            {
                strSql += " and strBelongGUID <> @strBelongGUID";
            }
            SqlParameter[] sqlParams = {
                                            new SqlParameter("strBelongGUID",model.strBelongGUID),
                                           new SqlParameter("strTrainNoHead",model.strTrainNoHead),
                                           new SqlParameter("nBeginNumber",model.nBeginNumber),
                                           new SqlParameter("nEndNumber",model.nEndNumber),
                                           new SqlParameter("nKehuoID",model.nKehuoID) 
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法

        public void GetByGUID(string strGUID)
        {
            string strSql = "select * from TAB_Base_TrainNoBelong";
            if (strGUID != "")
            {
                strSql += " and strBelongGUID = @strBelongGUID ";
            }
            SqlParameter[] sqlParams = {
                                               new SqlParameter("strBelongGUID",strGUID)
                                           };
            DataTable table= SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        
        }
        //public static DataTable GetAllTrainNoBelong(string name)
        //{
        //    string strSql = "select * from TAB_Base_TrainNoBelong";
        //    if (name != "")
        //    {
        //        strSql += " and strWorkShopName like @name ";
        //    }
        //    strSql += " order by nid ";
        //    SqlParameter[] sqlParams = {
        //                                   new SqlParameter("name","%" +name+ "%")
        //                               };
        //    return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        //}
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
