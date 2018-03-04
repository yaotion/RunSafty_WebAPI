using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TF.RunSafty.Model.InterfaceModel;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.VoiceCard
{

    public class Site
    {

        #region 获取客户端信息

        public class JsonModel
        {
            public int result;
            public string resultStr;
        }
        public class SiteInfo_In
        {
            public string strClientNumber { get; set; }
        }

        public class SiteInfo_Out
        {
            public int result;
            public string resultStr;
            public string siteIP = "";
            public string siteName = "";
            public string siteID = "";
            public string sitejob = "";
        }
        public object GetSiteInfo(string data)
        {
            object result = null;
            try
            {
                SiteInfo_In paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteInfo_In>(data);
                result = gotoSiteInfo(paramModel.strClientNumber);

            }
            catch (Exception ex)
            {
                JsonModel jsonModel = new JsonModel();
                jsonModel.result = 1;
                jsonModel.resultStr = "提交失败" + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 1.3	获取客户端基础信息
        /// </summary>
        /// <param name="hct"></param>
        /// <returns></returns>
        private object gotoSiteInfo(string strClientNumber)
        { 
            string sWhere = " Where (strSiteIP  = '" + strClientNumber + "')";
            string strSql = "SELECT strSiteName, nSiteJob, strSiteGUID as cnum, strSiteIP FROM TAB_Base_Site " + sWhere;
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (dt.Rows.Count < 1)
            {
                JsonModel model=new JsonModel();
                model.result = 1;
                model.resultStr = "未找到编号为" + strClientNumber + "的客户端信息";
                return model;

            }
            SiteInfo_Out json = new SiteInfo_Out();
            string strSiteIP = dt.Rows[0]["strSiteIP"].ToString();
            string strSiteName = dt.Rows[0]["strSiteName"].ToString();
            string strcnum = dt.Rows[0]["cnum"].ToString();
            string strnSiteJob = dt.Rows[0]["nSiteJob"].ToString();
            json.result = 0;
            json.resultStr = "返回成功";
            json.siteIP = strSiteIP;
            json.siteName = strSiteName;
            json.siteID = strcnum;
            json.sitejob = strnSiteJob;
            return json;
        }
        #endregion
    }
}
