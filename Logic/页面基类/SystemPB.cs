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
using System.Xml;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Net;
using System.Runtime.Serialization.Json;

    /// <summary>
    ///SystemPB 的摘要说明 系统业务公用方法
    /// </summary>
    public class SystemPB : System.Web.UI.Page
    {
        public SystemPB()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 机班状态
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string sjzt(string type)
        {
            switch (type)
            {
                case "0":
                    return "非运转";
                    break;
                case "1":
                    return "预备";
                    break;
                case "2":
                    return "正常";
                    break;
                case "3":
                    return "计划";
                    break;
                case "4":
                    return "入寓";
                    break;
                case "5":
                    return "离寓";
                    break;
                case "6":
                    return "出勤";
                    break;
                case "7":
                    return "空";
                    break;
            }
            return "";
        }
        /// <summary>
        ///格式化json格式 返回json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string returnstrjson(DataTable dt)
        {
            string strJson = PageBase.Serialize(dt);
            strJson = strJson.Substring(1);
            strJson = strJson.Insert(0, "{\"Success\":1,\"ResultText\":\"\",\"Total\":" + dt.Rows.Count + ",\"Items\":[");
            strJson += "}";
            return strJson;
        }
        /// <summary>
        /// 根据步骤类型 和步骤id 和机车id 返回步骤详细信息连接地址
        /// </summary>
        /// <param name="steptype"></param>
        /// <param name="stepid"></param>
        /// <param name="jcid"></param>
        /// <returns></returns>
        public static string getStepUrlFromStepType(int steptype, string stepid, int jcid)
        {
            int i = 0;
            if (steptype == 100)
            {
                return  "/Page/SerachMaster/ShowSerach.aspx?pageid=5901&where= and RepairState<>@作废@ and JiCheID=" + jcid;
            }
            else
            {
                if (steptype >= 7 && steptype <= 99)
                {
                    return "/Page/HandleProcess/ItemShow/StandardHandleShow.aspx?pageid=5708&where= and strStepID='" + stepid + "' and JiCheID=" + jcid;
                }
            }
            switch (steptype)
            {
                case 0:
                    return "/Page/HandleProcess/ItemShow/FengSuShow.aspx?pageid=5701&where= and strStepID='" + stepid + "' and JiCheID=" + jcid+"&jcid="+jcid;
                    break;
                case 1:
                    return "/Page/HandleProcess/ItemShow/LvWangShow.aspx?where= and strStepID='" + stepid + "' and JiCheID=" + jcid;
                    break;
                case 6:
                    return "/Page/HandleProcess/ItemShow/lsDatHuaBanShow.aspx?where= and strStepID='" + stepid + "' and JiCheID=" + jcid;
                    break;
            }
            return "/Page/SerachMaster/ShowSerach.aspx?pageid=570" + i + "&where= and strStepID='" + stepid + "' and JiCheID=" + jcid;
        }
    }
