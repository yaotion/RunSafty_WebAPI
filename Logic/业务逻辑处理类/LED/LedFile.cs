using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace TF.Api.DBUtility
{
    public class LedFile
    {
        public DataTable GetLedFileList(string clientId)
        {
            string sqlCommandText = string.Format("select * from VIEW_LED_Files where clientid=@clientid");
            SqlParameter[] sqlParams = {
                                           new SqlParameter("clientid",clientId)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlCommandText, sqlParams).Tables[0];
        }

        public bool IsLedFileExist(string fileName,string nid)
        {
            string sqlCommandText = "";
            if (string.IsNullOrEmpty(nid))
            {
                sqlCommandText = string.Format("select count(*) from VIEW_LED_Files where strFileName=@fileName ");
            }
            else
            {
                sqlCommandText = string.Format("select count(*) from VIEW_LED_Files where strFileName=@fileName  nid != @nid");
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("fileName",fileName),
                                           new SqlParameter("nid",nid)
                                       };
            return ((int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlCommandText,sqlParams)) > 0;
        }
        public DataTable GetLefFileByNid(string nid)
        {
            string sqlCommandText = string.Format("select * from tab_led_file where nid=@nid ");
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nid",nid)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlCommandText, sqlParams).Tables[0];
        }

        public bool UpdateLedFile(int nid, string fileGUID, string strWorkShopGUID, DateTime DtUpdate, string strFilePathName, string strFileName)
        {
            string sqlCommandText = "update tab_led_file set strFileGUID=@fileGUID,strWorkShopGUID=@strWorkShopGUID,DtUpdate=@DtUpdate,strFilePathName=@strFilePathName,strFileName=@strFileName where nid=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("fileGUID",fileGUID),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("DtUpdate",DtUpdate),
                                           new SqlParameter("strFilePathName",strFilePathName),
                                           new SqlParameter("strFileName",strFileName),
                                           new SqlParameter("nid",nid),
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlCommandText, sqlParams) > 0;
        }
        /// <summary>
        /// 添加LED文件记录
        /// </summary>
        /// <param name="fileGUID">文件编号</param>
        /// <param name="strWorkShopGUID">车间编号</param>
        /// <param name="DtUpdate">更新日期</param>
        /// <param name="strFilePathName">文件在接口站中的相对路径</param>
        /// <param name="strFileName">文件源名</param>
        /// <returns></returns>
        public bool AddLedFile(string fileGUID, string strWorkShopGUID, DateTime DtUpdate, string strFilePathName, string strFileName)
        {
            string sqlCommandText = "insert into tab_led_file values(@fileGUID,@strWorkShopGUID,@DtUpdate,@strFilePathName,@strFileName)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("fileGUID",fileGUID),
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                                           new SqlParameter("DtUpdate",DtUpdate),
                                           new SqlParameter("strFilePathName",strFilePathName),
                                           new SqlParameter("strFileName",strFileName),
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlCommandText, sqlParams) > 0;
        }
        public static bool Delete(string nid)
        {
            string strSql = "delete from tab_led_file where nid=@nid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("nid",nid)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
    }
}
