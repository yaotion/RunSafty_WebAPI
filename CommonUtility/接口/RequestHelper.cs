using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.Api.Utilities
{
    public class RequestHelper
    {
        /// <summary>
        /// 转换request["name"]到指定类型
        /// </summary>
        #region GetRequestToDataType

        public static string GetRequestToString(string requestParamName)
        {
            return PageBase.static_ext_string(System.Web.HttpContext.Current.Request[requestParamName]);
        }

        public static int GetRequestToInt(string requestParamName)
        {
            return PageBase.static_ext_int(System.Web.HttpContext.Current.Request[requestParamName]);
        }

        public static DateTime? GetRequestToDateTime(string requestParamName)
        {
            return PageBase.static_ext_date(System.Web.HttpContext.Current.Request[requestParamName]);
        }

        #endregion
    }
}
