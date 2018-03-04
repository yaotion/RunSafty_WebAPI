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
namespace TF.RunSaftyAPI.App_Api.Public
{

    /// <summary>
    ///IGetTrainnosByTime 的摘要说明
    /// </summary>
    public class IGetTrainnosByTime : IQueryResult
    {
        public IGetTrainnosByTime()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private class ParamModel
        {
            public string cid;
            public Train data;
        }
        private class Train
        {
            public string trainjiaoluID;
            public string beginTime;
            public string endTime;
            public int planState;
        }
        private class JsonModel
        {
            public int result;
            public string resultStr = "";
            public string data = "";
        }

        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                ParamModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
                TF.RunSafty.Model.TAB_Base_TrainNo train = new TF.RunSafty.Model.TAB_Base_TrainNo();
                TF.RunSafty.BLL.TAB_Base_TrainNo bllTrain = new TF.RunSafty.BLL.TAB_Base_TrainNo();
                DateTime dtBegin, dtEnd;
                string strTrainjiao = paramModel.data.trainjiaoluID;
                dtBegin = DateTime.Parse(paramModel.data.beginTime);
                dtEnd = DateTime.Parse(paramModel.data.endTime);
                int PlanState = paramModel.data.planState <= 0 ? 1 : paramModel.data.planState;
               
                bllTrain.GetTrainnoByTime(strTrainjiao, dtBegin, dtEnd, PlanState);
                jsonModel.result = 0;
                jsonModel.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                jsonModel.resultStr = "提交失败" + ex.Message;
            }
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel);
            return result;
        }
    }
}