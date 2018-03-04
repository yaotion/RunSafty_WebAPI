using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;

namespace TF.CommonUtility
{
    /// <summary>
    ///JsonConvert 的摘要说明
    /// </summary>
    public class JsonConvert
    {
        public JsonConvert()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //            
        }
        /// <summary>
        /// json反序列化为list对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
         public static string GetAjaxString(DataTable DataSource,Hashtable OutPutParams)
         {
                          
             string strItemCollection = "";
             for (int i = 0; i < DataSource.Rows.Count; i++)
             {
                 string strItemContent = "";
                 foreach (DictionaryEntry item in OutPutParams)
                 {
                     if (strItemContent == "")
                     {
                         strItemContent += "'{0}':'{1}'";
                     }
                     else
                     {
                         strItemContent += ",'{0}':'{1}'";
                     }
                     strItemContent = string.Format(strItemContent, item.Key, DataSource.Rows[i][item.Value.ToString()].ToString());
                 }
                 if (strItemCollection == "")
                 {
                     strItemCollection += "{" + strItemContent + "}";
                 }
                 else
                 {
                     strItemCollection += ",{" + strItemContent + "}";
                 }
             }


             return "{'Items':[" + strItemCollection + "]} ";
         }

         /// <summary>
         /// 序列化方法（带分页）
         /// </summary>
         /// <param name="dt"></param>
         /// <returns></returns>
         public static string SerializeP(DataTable dt, int count)
         {
             List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
             foreach (DataRow dr in dt.Rows)
             {
                 Dictionary<string, object> result = new Dictionary<string, object>();
                 foreach (DataColumn dc in dt.Columns)
                 {
                     if (dc.DataType == System.Type.GetType("System.DateTime"))
                     {
                         result.Add(dc.ColumnName, dr[dc].ToString() == "" ? "" : Convert.ToDateTime(dr[dc].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                     }
                     else
                     {
                         result.Add(dc.ColumnName, dr[dc].ToString());
                     }
                 }
                 list.Add(result);
             }
             string strReturn = "";
             if (count == 0)
             {
                 strReturn = "{\"total\":0,\"rows\":[]}";
             }
             else
             {
                 strReturn = static_ConventToJson(list, count);
             }
             return strReturn;
         }
         /// <summary>
         /// 序列化方法（带分页）
         /// </summary>
         /// <param name="dt"></param>
         /// <returns></returns>
         public static string static_SerializeP(DataTable dt, int count)
         {
             List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
             foreach (DataRow dr in dt.Rows)
             {
                 Dictionary<string, object> result = new Dictionary<string, object>();
                 foreach (DataColumn dc in dt.Columns)
                 {
                     if (dc.DataType == System.Type.GetType("System.DateTime"))
                     {
                         result.Add(dc.ColumnName, dr[dc].ToString() == "" ? "" : Convert.ToDateTime(dr[dc].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                     }
                     else
                     {
                         result.Add(dc.ColumnName, dr[dc].ToString());
                     }
                 }
                 list.Add(result);
             }
             string strReturn = "";
             if (count == 0)
             {
                 strReturn = "{\"total\":0,\"rows\":[]}";
             }
             else
             {
                 strReturn = static_ConventToJson(list, count);
             }
             return strReturn;
         }
         /// <summary>
         /// 转换为JSON对象
         /// </summary>
         /// <returns></returns>
         public static string static_ConventToJson<T>(List<T> list, int count)
         {
             JavaScriptSerializer serializer = new JavaScriptSerializer();
             string strJson = serializer.Serialize(list);
             strJson = strJson.Substring(1);
             strJson = strJson.Insert(0, "{\"total\":" + count + ",\"rows\":[");
             strJson += "}";

             return strJson;
         }

         /// <summary>
         /// 序列化方法不需要分页
         /// </summary>
         /// <param name="dt"></param>
         /// <returns></returns>
         public static string Serialize(DataTable dt)
         {
             JavaScriptSerializer serializer = new JavaScriptSerializer();
             List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
             foreach (DataRow dr in dt.Rows)
             {
                 Dictionary<string, object> result = new Dictionary<string, object>();
                 foreach (DataColumn dc in dt.Columns)
                 {
                     if (dc.DataType == System.Type.GetType("System.DateTime"))
                     {
                         result.Add(dc.ColumnName, dr[dc].ToString() == "" ? "" : Convert.ToDateTime(dr[dc].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                     }
                     else
                     {
                         result.Add(dc.ColumnName, dr[dc].ToString());
                     }
                 }
                 list.Add(result);
             }
             return serializer.Serialize(list); ;
         }

         /// <summary>
         /// 序列化方法不需要分页
         /// </summary>
         /// <param name="dt"></param>
         /// <returns></returns>
         public static string DatatableSerialize(DataTable dt)
         {
             JavaScriptSerializer serializer = new JavaScriptSerializer();
             List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
             foreach (DataRow dr in dt.Rows)
             {
                 Dictionary<string, object> result = new Dictionary<string, object>();
                 foreach (DataColumn dc in dt.Columns)
                 {
                     if (dc.DataType == System.Type.GetType("System.DateTime"))
                     {
                         result.Add(dc.ColumnName, dr[dc].ToString() == "" ? "" : Convert.ToDateTime(dr[dc].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                     }
                     else
                     {
                         result.Add(dc.ColumnName, dr[dc]);
                     }
                 }
                 
                 list.Add(result);
             }
             return serializer.Serialize(list); ;
         }

         /// <summary>
         /// 序列化方法不需要分页 
         /// </summary>
         /// <param name="list"></param>
         /// <returns></returns>
         public static string Serialize<T>(List<T> t)
         {
             JavaScriptSerializer js = new JavaScriptSerializer();
             string str = js.Serialize(t);
             str = Regex.Replace(str, @"\\/Date\((\d+)\)\\/", match =>
             {
                 DateTime dt = new DateTime(1970, 1, 1);
                 dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                 dt = dt.ToLocalTime();
                 return dt.ToString("yyyy-MM-dd HH:mm:ss");
             });
             return str;
         }

         /// <summary>
         /// json反序列化为list对象
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="jsonString"></param>
         /// <returns></returns>
         public static List<T> JSONStringToList<T>(string JsonStr)
         {
             JavaScriptSerializer Serializer = new JavaScriptSerializer();
             List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);
             return objs;
         }
         public static T Deserialize<T>(string json)
         {
             T obj = Activator.CreateInstance<T>();
             using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
             {
                 DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                 return (T)serializer.ReadObject(ms);
             }
         }

         /// <summary>
         ///格式化json格式 返回json
         /// </summary>
         /// <param name="dt"></param>
         /// <returns></returns>
         public static string returnstrjson(DataTable dt)
         {
             string strJson = Serialize(dt);
             strJson = strJson.Substring(1);
             strJson = strJson.Insert(0, "{\"Success\":1,\"ResultText\":\"\",\"Total\":" + dt.Rows.Count + ",\"Items\":[");
             strJson += "}";
             return strJson;
         }

         public static void FormatDataRow(DataTable table, DataRow row)
         {
             Type _type = null;
             foreach (DataColumn col in table.Columns)
             {
                 if (row[col.ColumnName] != DBNull.Value)
                     continue;
                 _type = col.DataType;
                 if (_type == typeof(int) || _type == typeof(double))
                 {
                     row[col.ColumnName] = 0;
                 }
                 if (_type == typeof(string))
                 {
                     row[col.ColumnName] = string.Empty;

                 }
                 if (_type == typeof(DateTime))
                 {
                     row[col.ColumnName] = new DateTime(1899, 12, 30);
                 }
             }
         }
    }
   
}
