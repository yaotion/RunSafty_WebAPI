<%@ WebHandler Language="C#" Class="QueryProcess" %>

using System;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Reflection;
using System.Web;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Web.Caching;
using TF.Utils;


public class QueryProcess : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/json";
        ///获取API名称及输入数据
        System.Collections.Specialized.NameValueCollection urlParams = context.Request.Params;
        string action = urlParams["DataType"];
        string data = urlParams["Data"];
        
        //开始监控函数的执行时间
        System.Diagnostics.Stopwatch watch = new Stopwatch();
        watch.Start();
        //执行函数
        string resultText;
        string apiChinaName = "";
        bool isSuccess = APIManage.ExecAPI(action, data, out resultText, ref apiChinaName);
        watch.Stop();
        //记录日志
        string strWatch = string.Format("\r\n*接口名称:{1}({0})\r\n*执行用时:{2}毫秒", apiChinaName, action, watch.ElapsedMilliseconds);
        strWatch += "\r\n*接口参数:" + data;
        if (this.IsPrint(action))
        {
            TF.CommonUtility.LogClass.log(strWatch);
        }
        
        //如果失败输出异常信息
        if (!isSuccess)
        {
            LogClass.log(resultText);
        }
        //向页面返回
        context.Response.Write(resultText);

    }

    public bool IsPrint(string itemAPIName)
    {
        if (itemAPIName == "TF.Runsafty.Utility.TGlobalDM.GetNow")//获取当前时间
            return false;
        else if (itemAPIName == "TF.RunSafty.SiteLogic.SiteInterface.SubmitDrinkPic") //上传测酒照片
            return false;
        else if (itemAPIName == "TF.RunSafty.BaseDict.CheXin.GetCheXinBySite") //获取车型
            return false;
        else if (itemAPIName == "TF.Runsafty.LCAllMingPai.GetAllMingPai")//电子名牌（电子名牌web版本）
            return false;
        else if (itemAPIName == "TF.Runsafty.Plan.Site.Trainman.Sent.Get") //获取指定客户端在指定时间范围下的指定区段的已下发的人员计划
            return false;
        else if (itemAPIName == "TF.Runsafty.Plan.Site.Trainmanplan.Get")//获取指定客户端在指定时间范围下的指定区段的人员计划
            return false;
        else if (itemAPIName == "TF.RunSafty.BaseDict.LCTrainmanJL.GetTMJLByTrainJLWithSiteVirtual")//获取人员交路
            return false;
        else if (itemAPIName == "TF.RunSafty.BaseDict.LCTrainJL.IsJiaoLuInSite")//判断交路是否属于客户端管辖
            return false;
        return true;
    }
    



   
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}
   