using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;

namespace TF.RunSafty.Trainman
{
    public class LCYAPlatTrainman
    {
        public static bool UsePlat
        {
            get
            {
                object obj = null;
                if (System.Configuration.ConfigurationManager.ConnectionStrings != null)
                {
                    if (System.Configuration.ConfigurationManager.ConnectionStrings["UsePlat"] != null)
                    {
                        obj = System.Configuration.ConfigurationManager.ConnectionStrings["UsePlat"].ConnectionString;
                    }
                }

                if (obj == null)
                {
                    return false;
                }
                int nUserPlat = 0;
                int.TryParse(obj.ToString(), out nUserPlat);
                return nUserPlat > 0;
            }
        }
        public static string PlatHost
        {
            get
            {
                object obj = null;
                if (System.Configuration.ConfigurationManager.ConnectionStrings != null)
                {
                    if (System.Configuration.ConfigurationManager.ConnectionStrings["PlatHost"] != null)
                    {
                        obj = System.Configuration.ConfigurationManager.ConnectionStrings["PlatHost"].ConnectionString;
                    }
                }


                string frameHost = string.Format("http://{0}:{1}", HttpContext.Current.Request.ServerVariables["server_name"], HttpContext.Current.Request.ServerVariables["server_port"]);
                if ((obj != null) && (obj.ToString() != ""))
                {
                    frameHost = obj.ToString();
                }
                return frameHost;
            }
        }
        private class UpdateUserTelIn
        {
            public string UserNumber = "";
            public string UserTel = "";
        }
        public static bool UpdateUserTel(string UserNumber,string UserTel)
        {
            if (!UsePlat)
            {
                return true;
            }

            UpdateUserTelIn inparams = new UpdateUserTelIn();
            inparams.UserNumber = UserNumber;
            inparams.UserTel = UserTel;
            string strobj = Newtonsoft.Json.JsonConvert.SerializeObject(inparams);
            string msg = "";

            string apiUrl = PlatHost + "/AshxService/QueryProcess.ashx?DataType=TF.YA.Org.UpdateUserTel&Data=" + strobj;
            try
            {
                String bret = HttpCommon.GetStr(apiUrl, out msg);
                return true;
            }
            catch
            {
                return false;
            }



        }
    }
}
