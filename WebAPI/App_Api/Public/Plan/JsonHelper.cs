using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Serialization.Json;//在System.Runtime.Serialization.dll中  
using System.Web.Script.Serialization;  //在System.Web.Extensions.dll中  

namespace TF.RunSaftyAPI.App_Api.Public.Plan
{
    /// <summary>  
    /// Json序列化和反序列化辅助类   
    /// </summary>  
    public class JsonHelper
    {
        /// <summary>  
        /// Json序列化   
        /// </summary>  
        /// <typeparam name="T">对象类型</typeparam>  
        /// <param name="obj">对象实例</param>  
        /// <returns>json字符串</returns>  
        public static string JsonSerializer<T>(T obj)
        {
            string jsonString = string.Empty;
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));


                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, obj);
                    jsonString = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch
            {
                jsonString = string.Empty;
            }
            return jsonString;
        }


        /// <summary>  
        /// Json反序列化  
        /// </summary>  
        /// <typeparam name="T">对象类型</typeparam>  
        /// <param name="jsonString">json字符串</param>  
        /// <returns>对象实例</returns>  
        public static T JsonDeserialize<T>(string jsonString)
        {
            T obj = Activator.CreateInstance<T>();
            try
            {
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());//typeof(T)  
                    T jsonObject = (T)ser.ReadObject(ms);
                    ms.Close();


                    return jsonObject;
                }
            }
            catch
            {
                return default(T);
            }
        }


        /// <summary>  
        /// 将 DataTable 序列化成 json 字符串  
        /// </summary>  
        /// <param name="dt">DataTable对象</param>  
        /// <returns>json 字符串</returns>  
        public static string DataTableToJson(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return "\"\"";
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();


            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();


            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }
            return myJson.Serialize(list);
        }


        /// <summary>  
        /// 将对象序列化成 json 字符串  
        /// </summary>  
        /// <param name="obj">对象实例</param>  
        /// <returns>json 字符串</returns>  
        public static string ObjectToJson(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();


            return myJson.Serialize(obj);
        }


        /// <summary>  
        /// 将 json 字符串反序列化成对象  
        /// </summary>  
        /// <param name="json">json字符串</param>  
        /// <returns>对象实例</returns>  
        public static object JsonToObject(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();


            return myJson.DeserializeObject(json);
        }


        /// <summary>  
        /// 将 json 字符串反序列化成对象  
        /// </summary>  
        /// <typeparam name="T">对象类型</typeparam>  
        /// <param name="json">json字符串</param>  
        /// <returns>对象实例</returns>  
        public static T JsonToObject<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();


            return myJson.Deserialize<T>(json);
        }
    }
}