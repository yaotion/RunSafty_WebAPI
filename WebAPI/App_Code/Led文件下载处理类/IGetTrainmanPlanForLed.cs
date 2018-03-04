using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSaftyAPI.App_Api.Public;

/// <summary>
///IGetTrainmanPlanForLed 的摘要说明
/// </summary>
public class IGetTrainmanPlanForLed : IQueryResult
{
    public IGetTrainmanPlanForLed()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    private class ParamModel
    {
        public string cid;
        public string strClientGUID;
        public string strMinute;
    }


    private class trainplan
    {
        public string strtrainno;
        public string strtraintypename;
        public string strtrainnumber;
        public string dtstarttraintime;
        public string dtchuqintime;
        public string strtrainmanname1;
        public string strtrainmanname2;
        public string strstartstation;
    }


    private class cheJian
    {
        public string strworkshopname;
        public List<trainplan> trainplanArr = new List<trainplan>();
        public void addtrainplan(string strtrainno, string strtraintypename, string strtrainnumber, string dtstarttaintime, string dtchuqintime, string strtrainmanname1, string strtrainmanname2, string strstartstation)
        {
            trainplan t_trainplan = new trainplan();
            t_trainplan.strtrainno = strtrainno;
            t_trainplan.strtraintypename = strtraintypename;
            t_trainplan.strtrainnumber = strtrainnumber;
            t_trainplan.dtstarttraintime = dtstarttaintime;
            t_trainplan.dtchuqintime = dtchuqintime;
            t_trainplan.strtrainmanname1 = strtrainmanname1;
            t_trainplan.strtrainmanname2 = strtrainmanname2;
            t_trainplan.strstartstation = strstartstation;
            trainplanArr.Add(t_trainplan);
        }

    }

    private class JsonModel
    {
        public int result;
        public string resultStr;
        public object data;
        private List<cheJian> chejianList = new List<cheJian>();

        //根据车间GUID，查找车间对象
        private cheJian FindCheJian(string Workshopname)
        {
            for (int i = 0; i < chejianList.Count; i++)
            {
                if (chejianList[i].strworkshopname == Workshopname)
                {
                    return chejianList[i];
                }
            }
            return null;
        }


        public void Convert(List<TF.RunSafty.Model.TrainManPlanforLed> recordList)
        {
            cheJian t_chejian;
            for (int i = 0; i < recordList.Count; i++)
            {
                t_chejian = FindCheJian(recordList[i].Workshopname);
                if (t_chejian == null)
                {
                    t_chejian = new cheJian();
                    t_chejian.strworkshopname = recordList[i].Workshopname;
                    chejianList.Add(t_chejian);
                }
                t_chejian.addtrainplan(recordList[i].Strtrainno, recordList[i].StrTrainTypeName, recordList[i].Strtrainnumber, recordList[i].Dtstarttaintime, recordList[i].Dtchuqintime, recordList[i].Strtrainmanname1, recordList[i].Strtrainmanname2, recordList[i].Strstartstation);
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
            TF.RunSafty.BLL.Plan.TAB_Plan_Train bll_Plan_Train = new TF.RunSafty.BLL.Plan.TAB_Plan_Train();
            List<TF.RunSafty.Model.TrainManPlanforLed> placeList = bll_Plan_Train.GetPlanTrain(paramModel.strClientGUID, paramModel.strMinute);
            jsonModel.result = 0;
            jsonModel.resultStr = "提交成功";
            jsonModel.Convert(placeList);

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