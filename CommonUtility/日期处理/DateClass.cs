using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.CommonUtility
{
    /// <summary>
    ///DateFactory 的摘要说明
    /// </summary>
    public class DateClass 
    {
        public DateClass()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 例如200分钟 返回03：20格式 也可是秒 小时等
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string dateturn(string str)
        {
            decimal i = Convert.ToDecimal(str);
            decimal m = 0;
            m = i >= 60 ? Math.Floor(i / 60) : 0;
            decimal s = i - m * 60;
            string t = "";
            t += m < 10 ? "0" + m.ToString() : m.ToString();
            t += ":";
            t += s < 10 ? "0" + s.ToString() : s.ToString();
            return t;
        }

        public static string DateTimeStr(string str)
        {
            try
            {
                return Convert.ToDateTime(str).ToString("yyyy-MM-dd");
            }
            catch
            {
                return str;
            }
        }

        /// <summary>
        /// 两时间相减返回秒数
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static string datediff(string str1, string str2)
        {
            string str = "";
            if (str1 != "" && str2 != "")
            {
                DateTime dt1 = Convert.ToDateTime(str1);
                DateTime dt2 = Convert.ToDateTime(str2);
                if (dt1 < dt2 || dt1 < Convert.ToDateTime("1990-09-01") || dt2 < Convert.ToDateTime("1990-09-01"))
                {
                    str = "0";
                }
                else
                {
                    TimeSpan ts = dt1 - dt2;
                    str = ts.TotalSeconds.ToString();
                }
            }
            return str;
        }
        /// <summary>
        /// 时间差 
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <param name="type">1 1小时1分钟 2 02:02</param>
        /// <returns></returns>
        public string DateDiff(object time1, object time2, int type)
        {
            string date = "";
            if (TF.CommonUtility.ObjectConvertClass.static_ext_string(time1) != "" && TF.CommonUtility.ObjectConvertClass.static_ext_string(time2) != "")
            {
                DateTime t1 = Convert.ToDateTime(time1);
                DateTime t2 = Convert.ToDateTime(time2);
                TimeSpan ts1 = t2 - t1;
                int tsMin = ts1.Minutes;
                int tsHou = ts1.Hours;
                switch (type)
                {
                    case 0:
                        date = tsHou.ToString() + "小时" + tsMin.ToString() + "分钟";
                        break;
                    case 1:
                        string s1 = tsHou < 10 ? "0" + tsHou.ToString() : tsHou.ToString();
                        string s2 = tsMin < 10 ? "0" + tsMin.ToString() : tsMin.ToString();
                        date = s1 + ":" + s2;
                        break;
                }
            }
            return date;
        }
        /// <summary>
        /// 时间差 
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <param name="type">1 1小时1分钟 2 02:02</param>
        /// <returns></returns>
        public static string static_DateDiff(object time1, object time2, int type)
        {
            string date = "";
            if (TF.CommonUtility.ObjectConvertClass.static_ext_string(time1) != "" && TF.CommonUtility.ObjectConvertClass.static_ext_string(time2) != "")
            {
                DateTime t1 = Convert.ToDateTime(time1);
                DateTime t2 = Convert.ToDateTime(time2);
                TimeSpan ts1 = t2 - t1;
                int tsMin = ts1.Minutes;
                int tsHou = ts1.Hours;
                switch (type)
                {
                    case 0:
                        date += tsHou != 0 ? tsHou.ToString() + "时" : "";
                        date += tsMin.ToString() + "分";
                        break;
                    case 1:
                        string s1 = tsHou < 10 ? "0" + tsHou.ToString() : tsHou.ToString();
                        string s2 = tsMin < 10 ? "0" + tsMin.ToString() : tsMin.ToString();
                        date = s1 + ":" + s2;
                        break;
                }
            }
            return date;
        }

        /// <summary>
        /// 时间差返回分钟数
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <param name="type">0 相差天数 1 小时 2 分钟</param>
        /// <returns></returns>
        public static decimal static_DecimalDateDiff(object time1, object time2, int type)
        {
            DateTime t1 = Convert.ToDateTime(time1);
            DateTime t2 = Convert.ToDateTime(time2);
            TimeSpan ts1 = t2 - t1;
            decimal tsDay = decimal.Round(Convert.ToDecimal(ts1.TotalDays), 1);
            decimal tsMin = decimal.Round(Convert.ToDecimal(ts1.TotalMinutes), 1);
            decimal tsHou = decimal.Round(Convert.ToDecimal(ts1.TotalSeconds), 1);
            switch (type)
            {
                case 0:
                    return tsDay;
                    break;
                case 1:
                    return tsHou;
                    break;
                case 2:
                    return tsMin;
                    break;
            }
            return 0;
        }

        /// <summary>
        /// 格式化小时分钟 2014-01-15 15:50:30 -> 15点30  
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string static_FormatHourMinute(string time)
        {
            try
            {
                return Convert.ToDateTime(time).ToShortTimeString().ToString().Replace(":", "点");
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.log(ex.Message);
            }
            return "";
        }

        //例如200分钟 返回03：20格式 也可是秒 小时等
        public static string TimeFormat(string str)
        {
            double i = Convert.ToDouble(TF.CommonUtility.ObjectConvertClass.static_ext_int(str));
            if (i < 0 || str == "")
            {
                return "";
            }

            double h = 0;
            h = i >= 3600 ? Math.Floor(i / 3600) : 0;
            double m = (i - h * 3600) >= 60 ? Math.Floor((i - h * 3600) / 60) : 0;
            string t = "";
            t += h < 10 ? "0" + h.ToString() : h.ToString();
            t += ":";
            m = m >= 60 ? Math.Floor(m / 60) : m;
            t += m < 10 ? "0" + m.ToString() : m.ToString();
            double s = i % 60;
            t += ":";
            t += s < 10 ? "0" + s.ToString() : s.ToString();
            return t;
        }

        /// <summary>
        ///  两时间相减后返回timespan格式 ts
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static TimeSpan diffTimeReturnTimeSpan(object o1, object o2)
        {
            TimeSpan ts = Convert.ToDateTime(o2) - Convert.ToDateTime(o1);
            return ts;
        }
    }
}


