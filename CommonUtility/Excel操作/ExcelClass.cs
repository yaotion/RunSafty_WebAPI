using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Net;
using System.IO;
using System.Web.UI;
using System.Data;

namespace TF.CommonUtility
{
    /// <summary>
    ///ExcelClass 的摘要说明
    /// </summary>
    public class ExcelClass 
    {
        public ExcelClass()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        #region 连接Excel  读取Excel数据   并返回DataSet数据集合
        /// <summary>
        /// 连接Excel  读取Excel数据   并返回DataSet数据集合
        /// </summary>
        /// <param name="filepath">Excel服务器路径</param>
        /// <param name="tableName">Excel表名称</param>
        /// <returns></returns>
        public static System.Data.DataSet ExcelSqlConnection(string filepath, string tableName)
        {
            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
            OleDbConnection ExcelConn = new OleDbConnection(strCon);
            try
            {

                ExcelConn.Open();

                DataTable dtSheetName = ExcelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
                //包含excel中表名的字符串数组
                string[] strTableNames = new string[dtSheetName.Rows.Count];
                for (int k = 0; k < dtSheetName.Rows.Count; k++)
                {
                    strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
                }
                string strCom = string.Format("SELECT * FROM [" + strTableNames[0] + "]");
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, ExcelConn);
                DataSet ds = new DataSet();
                myCommand.Fill(ds, strTableNames[0]);
                ExcelConn.Close();
                return ds;
            }
            catch
            {
                ExcelConn.Close();
                return null;
            }
        }

        /// <summary>
        /// 根据url删除ftp文件
        /// </summary>
        public static bool DeleteFileOnServer(Uri serverUri, string ftpUser, string ftpPassWord)
        {
            try
            {
                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    return false;
                }
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //Console.WriteLine("Delete status: {0}", response.StatusDescription);
                response.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }


        /// <summary>Excel导出</summary>
        /// <param name="dt">要导入的数据(存储于DataTable中的)</param>
        /// <param name="arrayList">需要导出的列名集合</param>
        public static bool ExportToExcel(Page page, DataTable dt, System.Collections.ArrayList arrayList)
        {
            StringWriter sw = new StringWriter();
            string columnName = "";

            for (int i = 0; i < arrayList.Count; i++)
            {
                if (columnName.Length == 0)
                {
                    columnName = columnName + ((KeyValue)arrayList[i]).Value.ToString();
                }
                else
                {
                    columnName = columnName + '\t' + ((KeyValue)arrayList[i]).Value.ToString();
                }
            }

            sw.WriteLine(columnName);

            foreach (DataRow dr in dt.Rows)
            {
                string rowString = "";
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (rowString.Length == 0)
                    {
                        string str = dr[((KeyValue)arrayList[i]).Key.ToString()].ToString().Trim().Length == 0 ? "  " : dr[((KeyValue)arrayList[i]).Key.ToString()].ToString();
                        rowString = rowString + str;
                    }
                    else
                    {
                        string str = dr[((KeyValue)arrayList[i]).Key.ToString()].ToString().Trim().Length == 0 ? "  " : dr[((KeyValue)arrayList[i]).Key.ToString()].ToString();
                        rowString = rowString + '\t' + str;
                    }
                }



                sw.WriteLine(rowString);
            }
            sw.Close();
            page.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(dt.TableName, System.Text.Encoding.UTF8) + ".xls\"");
            page.Response.ContentType = "application/ms-excel";
            page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            page.Response.Write(sw);
            page.Response.End();
            return true;
        }
        #endregion
    }
    public class KeyValue
    {
        public object Key;
        public object Value;
        public KeyValue(object key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}


