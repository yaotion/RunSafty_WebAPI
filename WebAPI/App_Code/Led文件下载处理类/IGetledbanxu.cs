using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;

/// <summary>
///IGetledbanxu 的摘要说明
/// </summary>
public class IGetledbanxu : IQueryResult
{
    public IGetledbanxu()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    private class JiaoLuData
    {
        public string strClientGUID;
    }
    private class ParamModel
    {
        public string cid;
        public JiaoLuData data;
        public string strClientGUID;
    }

    private class jiaoLu
    {
        public string strGUID;
        public string strName;
        public string strNickName;
        public string strClassOrderName;
    }

    private class cheJian
    {
        public string strGUID;
        public string strName;
        public string strNickName;

        public List<jiaoLu> jiaoluList = new List<jiaoLu>();
        public void addjiaolu(string strguid, string strName, string strNickName,string chejianNickName)
        {
            jiaoLu t_jiaolu = new jiaoLu();
            t_jiaolu.strGUID = strguid;
            t_jiaolu.strName = strName;
            t_jiaolu.strNickName = strNickName;
            t_jiaolu.strClassOrderName = chejianNickName + "_" + strNickName;
            jiaoluList.Add(t_jiaolu);
        }

    }
    private class JsonModel
    {
        public int result;
        public string resultStr;
        public object data;
        private List<cheJian> chejianList = new List<cheJian>();


        //根据车间GUID，查找车间对象
        private cheJian FindCheJian(string strCheJianGUID)
        {
            for (int i = 0; i < chejianList.Count; i++)
            {
                if (chejianList[i].strGUID == strCheJianGUID)
                {
                    return chejianList[i];
                }
            }
            return null;
        }

        public void Convert(List<TF.RunSafty.Model.Tab_Config_LED_Class_Order> recordList)
        {
            cheJian t_chejian;
            for (int i = 0; i < recordList.Count; i++)
            {
                t_chejian = FindCheJian(recordList[i].strCheJianGUID);
                if (t_chejian == null)
                {
                    t_chejian = new cheJian() ;
                    t_chejian.strGUID = recordList[i].strCheJianGUID;
                    t_chejian.strName = recordList[i].strCheJianName;
                    t_chejian.strNickName = recordList[i].strCheJianNickName;
                    chejianList.Add(t_chejian);
                }
                t_chejian.addjiaolu(recordList[i].strJiaoLuGUID, recordList[i].strJiaoLuName, recordList[i].strJiaoLuNickName, recordList[i].strCheJianNickName);
            }
            this.data = chejianList;
        }
    }

    
    


   
         


    public override string QueryResult()
    {
        JsonModel jsonModel = new JsonModel();
        try
        {
            ParamModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
            TF.RunSafty.BLL.Tab_Config_LED_Class_Order bllPlace = new TF.RunSafty.BLL.Tab_Config_LED_Class_Order();
            List<TF.RunSafty.Model.Tab_Config_LED_Class_Order> recordList = bllPlace.GetTrainnosOfTrainJiaolu(paramModel.strClientGUID);
            jsonModel.result = 0;
            jsonModel.resultStr = "提交成功";
            jsonModel.Convert(recordList);
            
        }
        catch (Exception ex)
        {
            TF.CommonUtility.LogClass.logex(ex, "");
            jsonModel.result = 1;
            jsonModel.resultStr = "提交失败" + ex.Message;
        }
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
        timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        return Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, timeConverter);
    }
}