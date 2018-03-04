using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;

/// <summary>
///GetServerNowTime 的摘要说明
/// </summary>
public class GetServerNowTime : IQueryResult
{
	public GetServerNowTime()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    private class JsonModel
    {
        public int result;
        public string resultStr;
        public string DateTimeNow;
    }

    public override string QueryResult()
    {
        JsonModel jsonModel = new JsonModel();

        jsonModel.result = 0;
        jsonModel.resultStr = "返回成功";
        jsonModel.DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel);
        return result;
    }
}