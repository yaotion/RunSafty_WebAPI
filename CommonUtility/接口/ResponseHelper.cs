using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.Api.Utilities
{
    public class ResponseHelper
    {
        /// <summary>
        /// 设置json输出头
        /// </summary>
        public static void SetJsonHeader()
        {
            System.Web.HttpContext.Current.Response.ContentType = GlobalSetting.JsonHeader;
        }

    }
}
