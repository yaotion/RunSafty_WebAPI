using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.CommonUtility
{
    /// <summary>
    ///TF.CommonUtility.ObjectConvertClass 的摘要说明
    /// </summary>
    public class ObjectConvertClass 
    {
        public ObjectConvertClass()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary> 精度</summary>
        /// <param name="o"></param>
        /// <param name="len">保留长度</param>
        /// <returns></returns>
        public static decimal ext_decimalRound(object o, int len)
        {
            return decimal.Round(Convert.ToDecimal(static_ext_decimal(o)), len);
        }
        /// <summary> 数据类型转换（对象转整型 失败返回0 ）</summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int static_ext_int(object o)
        {
            if (o != null && o != "undefined")
            {
                try
                {
                    return int.Parse(static_ext_string(o));
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        /// <summary> 数据类型转换（对象转整型 失败返回0 ） </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static double static_ext_double(object o)
        {
            if (o != null && o != "undefined")
            {
                try
                {
                    return double.Parse(static_ext_string(o));
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        /// <summary>数据类型转换（对象转整型 失败返回null ）</summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static decimal? static_ext_decimal(object o)
        {
            if (o != null && o != "undefined")
            {
                try
                {
                    return decimal.Parse(static_ext_string(o));
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        /// <summary>数据类型转换为字符串</summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string static_ext_string(object o)
        {
            if (o != null)
            {
                try
                {
                    if (o.ToString().Trim() == "undefined")
                    {
                        return "";
                    }
                    else
                    {
                        return o.ToString().Trim();
                    }
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>数据类型转换为日期</summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DateTime? static_ext_date(object o)
        {
            if (o != null)
            {
                try
                {
                    return Convert.ToDateTime(o);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>数据类型转换为日期</summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DateTime static_ext_Date(object o)
        {
            if (o != null)
            {
                try
                {
                    return Convert.ToDateTime(o);
                }
                catch (Exception)
                {
                    return DateTime.Now;
                }
            }
            else
            {
                return DateTime.Now;
            }
        }
        /// <summary>自定义时间格式</summary>
        /// <param name="o"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string static_ext_date1(object o, string format)
        {
            if (o != null)
            {
                try
                {
                    return Convert.ToDateTime(o) > DateTime.Parse("1899-12-30") ? Convert.ToDateTime(o).ToString(format) : "";
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return "";
            }
        }
    }
}


