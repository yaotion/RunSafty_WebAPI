using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TF.Utils
{
    /// <summary>
    /// 畅想SQL数据库操作类
    /// </summary>
    public class TFSQLDB
    {
        /// <summary>
        /// 创建SQL参数
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="ParamValue">参数值</param>
        /// <param name="ParamType">参数类型</param>
        /// <returns></returns>
        public static SqlParameter CreateParam(string ParamName,string ParamValue,SqlDbType ParamType)
        {
            SqlParameter param = new SqlParameter(ParamName, ParamType);
            param.Value = ParamValue;
            return param;
        }
        public static string FormatSplit(string SplitText,char Sign)
        {
            string[] splitArray = SplitText.Split(new char[1] { Sign }, StringSplitOptions.RemoveEmptyEntries);
            string strSplittedResult = "";
            for (int i = 0; i < splitArray.Length; i++)
            {
                if (strSplittedResult == "")
                {
                    strSplittedResult = string.Format("'{0}'", splitArray[i]);
                }
                else
                {
                    strSplittedResult += string.Format(",'{0}'", splitArray[i]);
                }
            }
            if (strSplittedResult == "")
            {
                strSplittedResult = string.Format("('{0}')", strSplittedResult);
            }
            else
            {
                strSplittedResult = string.Format("({0})", strSplittedResult);
            }
            return strSplittedResult;
        }
    }
}
