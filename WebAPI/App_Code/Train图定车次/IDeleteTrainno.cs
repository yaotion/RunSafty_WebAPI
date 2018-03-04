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
    ///IDeleteTrainno 的摘要说明
    /// </summary>
    public class IDeleteTrainno : IQueryResult
    {
        public IDeleteTrainno()
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
            public string trainnoID;
        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public object data;
        }

        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                ParamModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
                TF.RunSafty.Model.TAB_Base_TrainNo train = new TF.RunSafty.Model.TAB_Base_TrainNo();
                TF.RunSafty.BLL.TAB_Base_TrainNo bllTrain = new TF.RunSafty.BLL.TAB_Base_TrainNo();
                if (bllTrain.Delete(paramModel.data.trainnoID))
                {
                    jsonModel.result = 0;
                    jsonModel.resultStr = "返回成功";
                }
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
