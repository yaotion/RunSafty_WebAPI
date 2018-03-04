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
    ///    类名：DutyReading_Information
    ///    描述：记名式传达数据库操作类
    /// </summary>
    public class DutyReading_Information
    {

        #region 属性
        public string strDescription;
        public string strFileName;
        public int nFileSize;
        public DateTime? dtUpTime;
        public int nReadTimeCount;
        public int nReadingType;
        public int nReadMode;
        public string strWorkShopGUID;
        public string strOrginFileName;

        #endregion

        #region 构造函数
        public DutyReading_Information()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        #endregion

        #region 扩展方法

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dri"></param>
        public static bool Add(DutyReading_Information dri)
        {
            string newGUID = Guid.NewGuid().ToString();
            string strSql = @"insert into [TAB_DutyReading_Information] (
                [strReadingGUID]
                ,[strDescription]
                ,[strFileName]
                ,[nFileSize]
                ,[dtUpTime]
                ,[nReadTimeCount]
                ,[nReadingType]
                ,[nReadMode]
                ,[strWorkShopGUID]
                ,[strOrginName]) 
                values
                (
                @strReadingGUID
                ,@strDescription
                ,@strFileName
                ,@nFileSize
                ,@dtUpTime
                ,@nReadTimeCount
                ,@nReadingType
                ,@nReadMode
                ,@strWorkShopGUID
                ,@strOrginName
                )";
            SqlParameter[] sqlParams ={
                                                    new SqlParameter("@strReadingGUID",newGUID),
                                                    new SqlParameter("@strDescription",dri.strDescription),
                                                    new SqlParameter("@strFileName",dri.strFileName),
                                                    new SqlParameter("@nFileSize",dri.nFileSize),
                                                    new SqlParameter("@dtUpTime",DateTime.Now),
                                                    new SqlParameter("@nReadTimeCount",dri.nReadTimeCount),
                                                    new SqlParameter("@nReadingType",0),
                                                    new SqlParameter("@nReadMode",dri.nReadMode),
                                                    new SqlParameter("@strWorkShopGUID",dri.strWorkShopGUID),
                                                    new SqlParameter("@strOrginName",dri.strOrginFileName)
                                                    
                                     };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;

        }



        public static bool AddWithGUID(DutyReading_Information dri, string newGUID)
        {
            string strSql = @"insert into [TAB_DutyReading_Information] (
                [strReadingGUID]
                ,[strDescription]
                ,[strFileName]
                ,[nFileSize]
                ,[dtUpTime]
                ,[nReadTimeCount]
                ,[nReadingType]
                ,[nReadMode]
                ,[strWorkShopGUID]
                ,[strOrginName]) 
                values
                (
                @strReadingGUID
                ,@strDescription
                ,@strFileName
                ,@nFileSize
                ,@dtUpTime
                ,@nReadTimeCount
                ,@nReadingType
                ,@nReadMode
                ,@strWorkShopGUID
                ,@strOrginName
                )";
            SqlParameter[] sqlParams ={
                                                    new SqlParameter("@strReadingGUID",newGUID),
                                                    new SqlParameter("@strDescription",dri.strDescription),
                                                    new SqlParameter("@strFileName",dri.strFileName),
                                                    new SqlParameter("@nFileSize",dri.nFileSize),
                                                    new SqlParameter("@dtUpTime",DateTime.Now),
                                                    new SqlParameter("@nReadTimeCount",dri.nReadTimeCount),
                                                    new SqlParameter("@nReadingType",0),
                                                    new SqlParameter("@nReadMode",dri.nReadMode),
                                                    new SqlParameter("@strWorkShopGUID",dri.strWorkShopGUID),
                                                    new SqlParameter("@strOrginName",dri.strOrginFileName)
                                                    
                                     };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;

        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strReadingGUID"></param>
        /// <returns></returns>
        public static bool Delete(string strReadingGUID)
        {
            string strSql = "delete from [TAB_DutyReading_Information] where strReadingGUID=@strReadingGUID";
            SqlParameter sqlParam = new SqlParameter("@strReadingGUID", strReadingGUID);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam) > 0;
        }
        #endregion

        #endregion
    }
}