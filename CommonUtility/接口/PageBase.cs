using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace TF.Api.Utilities
{

    /// <summary>
    ///PageBase 的摘要说明
    /// </summary>
    public class PageBase : Page
    {

        /// <summary>
        /// 数据类型转换（对象转整型 失败返回0 ）
        /// </summary>
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
        /// <summary>
        /// 数据类型转换（对象转整型 失败返回0 ）
        /// </summary>
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

        /// <summary>
        /// 自定义格式
        /// </summary>
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