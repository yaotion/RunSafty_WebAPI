using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.CommonUtility
{
    /// <summary>
    ///TextClass 的摘要说明
    /// </summary>
    public class TextClass
    {
        public TextClass()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 字符串剪切
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public string stringCut(string str, int len)
        {
            try
            {
                if (str.Length < len)
                {
                    return str;
                }
                else
                {
                    return str.Substring(0, len) + "...";
                }
            }
            catch
            {
                return "";
            }
        }
        /// <summary>去掉字符串首尾逗号</summary>
        /// <param name="StrIndex"></param>
        /// <returns></returns>
        public static string CutComma(string StrIndex)
        {
            if (StrIndex.Length > 0)
            {
                StrIndex = StrIndex.Substring(0, 1) == "," ? StrIndex.Substring(1) : StrIndex;
                StrIndex = StrIndex.Substring(StrIndex.Length - 1) == "," ? StrIndex.Substring(0, StrIndex.Length - 1) : StrIndex;
            }
            return StrIndex;
        }
        /// <summary>去掉字符串首尾fh </summary>
        /// <param name="StrIndex"></param>
        /// <returns></returns>
        public static string CutComma(string StrIndex, string fh, int index)
        {
            if (StrIndex.Length > 0)
            {
                StrIndex = StrIndex.Substring(0, index) == fh ? StrIndex.Substring(1) : StrIndex;
                StrIndex = StrIndex.Substring(StrIndex.Length - index) == fh ? StrIndex.Substring(0, StrIndex.Length - index) : StrIndex;
            }
            return StrIndex;
        }
        /// <summary>如不为空字符串加括号返回</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string jkh(string str)
        {
            if (str != "")
            {
                return "(" + str + ")";
            }
            return str;
        }
        /// <summary> 字符串满len后自动换行 </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string TextAutoNewLine(string str, int len)
        {
            string s = "";
            if (str.Length > len)
            {
                for (int i = 1; i <= str.Length; i++)
                {
                    int r = i % len;
                    int last = (str.Length / len) * len;
                    if (i != 0 && i <= last)
                    {

                        if (r == 0)
                        {
                            s += str.Substring(i - len, len) + "\\n";
                        }

                    }
                    else if (i > last)
                    {
                        s += str.Substring(i - 1);
                        break;
                    }

                }
            }
            else
            {
                s = str;
            }
            s = TF.CommonUtility.TextClass.CutComma(s, "\\n", 2);
            return s;
        }
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string urlencode(object s)
        {
            return System.Web.HttpContext.Current.Server.UrlEncode(s.ToString());
        }
        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string urldecode(string s)
        {
            return System.Web.HttpContext.Current.Server.UrlDecode(s.ToString());
        }
        /// <summary>
        /// 字符前加逗号
        /// </summary>
        /// <param name="ccstr">储存str</param>
        /// <param name="clstr">待处理str</param>
        /// <returns></returns>
        public static string static_AddCumma(string ccstr, string clstr)
        {
            if (ccstr == "" && clstr != "")
            {
                ccstr += clstr;
            }
            else
            {
                if (ccstr != "" && clstr != "")
                {
                    ccstr += ",";
                    ccstr += clstr;
                }
            }
            return ccstr;
        }


        /// <summary>
        /// 去掉特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string replace(string str)
        {
            str = str.Replace("&", "");
            str = str.Replace("'", "");
            str = str.Replace("\"", "");
            str = str.Replace(" ", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("\n", "");
            str = str.Replace("+", "");
            str = str.Replace("=", "");
            str = str.Replace("\r", "");
            return str;
        }

    }
}


