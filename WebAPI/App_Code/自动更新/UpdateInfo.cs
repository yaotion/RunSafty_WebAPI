using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections;



namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///UpdateInfo 的摘要说明
    /// </summary>
    public class IUpdateInfo : IQueryResult
    {
        internal class DataModel
        {
            public string pid;
            public string version;
        }
        public IUpdateInfo()
        {
        }
        public override string QueryResult()
        {
            DataModel dataModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DataModel>(this.Data);
            string responseString = "{\"NeedUpdate\":false,\"strProjectVersion\":\"\",\"strUpdateBrief\":\"未知的版本信息\",\"strPackageUrl\":\"\",\"strMainExeName\":\"\"}";
            try
            {

                string strProjectID = dataModel.pid;
                string strProjectVersion = dataModel.version;
                DBUpdateVersion newVersion = new DBUpdateVersion();

                if (DBUpdateVersion.GetProjectVersion(strProjectID, newVersion))
                {
                    if (newVersion.strProjectVersion != strProjectVersion)
                    {
                        responseString = "{\"NeedUpdate\":true,\"strProjectVersion\":\"" + newVersion.strProjectVersion + "\",\"strUpdateBrief\":\"" +
                            newVersion.strUpdateBrief + "\",\"strPackageUrl\":\"http://" + Context.Request.Url.Authority + newVersion.strPackageUrl + "\",\"strMainExeName\":\"" + newVersion.strMainExeName + "\"}";
                    }
                }

            }
            finally
            {
            }
            return responseString;
        }
    }
}