using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TF.Utils
{
    public class TFConvert
    {
       
        /// <summary>
        /// 数据库字段转化为字符串
        /// 当转化失败将以用户指定的默认值返回
        /// </summary>
        /// <param name="DBData"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static string DBToString(object DBData,string DefaultValue)
        {
            if (DBData == null)
            {
                return DefaultValue;
            }
            if (DBNull.Value.Equals(DBData))
            {
                return DefaultValue;
            }         
            return  DBData.ToString();                
        }
        /// <summary>
        /// 数据库字段转化为字符串
        /// 当转化失败时将返回true，false
        /// </summary>
        /// <param name="DBData"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool TryDBToString(object DBData,out string Value)
        {
            Value = "";
            if (DBData == null)
            {
                return false;
            }
            if (DBNull.Value.Equals(DBData))
            {
                return false;
            }
            Value = DBData.ToString();
            return true;    
        }
        /// <summary>
        /// 数据库字段转化为字符串
        /// 当转化失败将返回""
        /// </summary>
        /// <param name="DBData"></param>
        /// <returns></returns>
        public static string DBToStringD(object DBData)
        {
            return DBToString(DBData,"");
        }
      
        /// <summary>
        /// 数据库字段转化为整形
        /// 当转化失败将以用户指定的默认值返回
        /// </summary>
        /// <param name="DBData"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static int DBToInt(object DBData, int DefaultValue)
        {
            if (DBData == null)
            {
                return DefaultValue;
            }
            if (DBNull.Value.Equals(DBData))
            {
                return DefaultValue;
            }
            int nTemp;
            if (!int.TryParse(DBData.ToString(), out nTemp))
            {
                return DefaultValue;
            }
            return nTemp;
        }
        /// <summary>
        /// 数据库字段转化为整形
        /// 当转化失败时将返回true，false
        /// </summary>
        /// <param name="DBData"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool TryDBToInt(object DBData, out int Value)
        {
            Value = 0;
            if (DBData == null)
            {
                return false;
            }
            if (DBNull.Value.Equals(DBData))
            {
                return false;
            }
            
            if (!int.TryParse(DBData.ToString(),out Value))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 数据库字段转化为整形
        /// 当转化失败将返回0
        /// </summary>
        /// <param name="DBData"></param>
        /// <returns></returns>
        public static int DBToIntD(object DBData)
        {
            return DBToInt(DBData,0);
        }

        public static double DBToDouble(object DBData, double DefaultValue)
        {
            if (DBData == null)
            {
                return DefaultValue;
            }
            if (DBNull.Value.Equals(DBData))
            {
                return DefaultValue;
            }
            double nTemp;
            if (!double.TryParse(DBData.ToString(), out nTemp))
            {
                return DefaultValue;
            }
            return nTemp;
        }
        public static double DBToDoubleD(object DBData)
        {
            return DBToDouble(DBData, 0);
        }

        /// <summary>
        /// 数据库字段转化为布尔型
        /// 当转化失败将以用户指定的默认值返回
        /// </summary>
        /// <param name="DBData"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static bool DBToBool(object DBData, bool DefaultValue)
        {
            if (DBData == null)
            {
                return DefaultValue;
            }
            if (DBNull.Value.Equals(DBData))
            {
                return DefaultValue;
            }
            bool bTemp;
            if (!bool.TryParse(DBData.ToString(), out bTemp))
            {
                return DefaultValue;
            }
            return bTemp;
        }
        /// <summary>
        /// 数据库字段转化为字符串
        /// 当转化失败时将返回true，false
        /// 
        /// </summary>
        /// <param name="DBData"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool TryDBToBool(object DBData, out bool Value)
        {
            Value = false;
            if (DBData == null)
            {
                return false;
            }
            if (DBNull.Value.Equals(DBData))
            {
                return false;
            }

            if (!bool.TryParse(DBData.ToString(), out Value))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 数据库字段转化为布尔型
        /// 当转化失败将返回false
        /// </summary>
        /// <param name="DBData"></param>
        /// <returns></returns>
        public static bool DBToBoolD(object DBData)
        {
            return DBToBool(DBData, false);
        }

        /// <summary>
        /// 数据库字段转化为日期函数
        /// 当转化失败将以用户指定的默认值返回
        /// </summary>
        /// <param name="DBData"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static DateTime DBToDateTime(object DBData, DateTime DefaultValue)
        {
            if (DBData == null)
            {
                return DefaultValue;
            }
            if (DBNull.Value.Equals(DBData))
            {
                return DefaultValue;
            }
            DateTime dtTemp;
            if (!DateTime.TryParse(DBData.ToString(), out dtTemp))
            {
                return DefaultValue;
            }
            return dtTemp;
        }
        /// <summary>
        /// 数据库字段转化为日期时间型
        /// 当转化失败时将返回true，false
        /// </summary>
        /// <param name="DBData"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool TryDBToDateTime(object DBData, out DateTime Value)
        {
            Value = DateTime.MinValue;
            if (DBData == null)
            {
                return false;
            }
            if (DBNull.Value.Equals(DBData))
            {
                return false;
            }
            if (!DateTime.TryParse(DBData.ToString(), out Value))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 数据库字段转化为布尔型
        /// 当转化失败将返回DateTime.Parse("1899-01-01")        
        /// </summary>
        /// <param name="DBData"></param>
        /// <returns></returns>
        public static DateTime DBToDateTimeD(object DBData)
        {
            return DBToDateTime(DBData,DateTime.Parse("1899-01-01"));
        }

        /// <summary>
        /// 数据库字段转化为日期的字符串形式
        /// 当转化失败将以用户指定的默认值返回
        /// </summary>
        /// <param name="DBData"></param>
        /// <param name="Default"></param>
        /// <returns></returns>
        public static string DBToDateTimeString(object DBData, string DefaultValue)
        {
            if (DBData == null)
            {
                return DefaultValue;
            }
            if (DBNull.Value.Equals(DBData))
            {
                return DefaultValue;
            }
            DateTime dtTemp;
            if (!DateTime.TryParse(DBData.ToString(), out dtTemp))
            {
                return DefaultValue;
            }
            return dtTemp.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 数据库字段转化为日期的字符串形式
        /// 当转化失败将返回固定的"1899-01-01"
        /// </summary>
        /// <param name="DBData"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static string DBToDateTimeStringD(object DBData)
        {
            return DBToDateTimeString(DBData, "1899-01-01");
        }
    }
}
