using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TFApiCommon
{
    /// <summary>
    /// 原有ObjectConvertClass 类，类名及方法名太长 使用不方便，
    /// 此类暂时不使用，等ObjectConvertClass类移除后再统一使用
    /// </summary>
    public class DBConvert
    {
        public static string ToString(object obj)
        {
            string ret = string.Empty;
            if (obj != null)
            {
                if (obj != DBNull.Value)
                {
                    ret = Convert.ToString(obj);
                }
            }

            return ret;
        }

        public static int ToInt(object obj)
        {
            int ret = 0;
            if (obj != null)
            {
                if (obj != DBNull.Value)
                {
                    ret = Convert.ToInt32(obj);
                }
            }

            return ret;
        }

        public static DateTime ToDateTime(object obj)
        {
            DateTime ret = DateTime.MinValue;
            if (obj != null)
            {
                if (obj != DBNull.Value)
                {
                    ret = Convert.ToDateTime(obj);
                }
            }

            return ret;            
        }

        public static DateTime? ToDateTime_N(object obj)
        {
            DateTime? ret = null;
            if (obj != null)
            {
                if (obj != DBNull.Value)
                {
                    ret = Convert.ToDateTime(obj);
                }
            }

            return ret;

        }

        public static double ToDouble(object obj)
        {
            int ret = 0;
            if (obj != null)
            {
                if (obj != DBNull.Value)
                {
                    ret = Convert.ToInt32(obj);
                }
            }

            return ret;        
        }
    }
}
