using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace TF.RunSafty.Trainman
{

    /// <summary>  
    /// 有关HTTP请求的辅助类  
    /// </summary>  
    public class HttpCommon
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";     
        public static String GetStr(string Url, out string error)
        {
            error = string.Empty;
            string strconn = string.Empty;
            //  RespData resultdata = null;
            string type = "UTF-8";
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                // Get the response instance.
                wReq.Method = "GET";
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding(type)))
                {
                    strconn = reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                error = ex.Message;
            }
            return strconn;
        }
        
    }

}
