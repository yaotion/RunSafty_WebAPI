using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

namespace TF.CommonUtility
{
    /// <summary>
    ///UriClass 的摘要说明
    /// </summary>
    public class UriClass
    {
        public UriClass()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>去掉上传文件中 文件名包含#</summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static Uri ProcessSpecialCharacters(string Url)
        {
            Uri uriTarget = new Uri(Url);
            if (!Url.Contains("#"))
            {
                return uriTarget;
            }

            UriBuilder msPage = new UriBuilder();
            msPage.Host = uriTarget.Host;
            msPage.Scheme = uriTarget.Scheme;
            msPage.Port = uriTarget.Port;
            msPage.Path = uriTarget.LocalPath + uriTarget.Fragment;
            msPage.Fragment = uriTarget.Fragment;
            Uri uri = msPage.Uri;

            return uri;
        }
    }
}


