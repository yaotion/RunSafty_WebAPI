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
using System.Collections;
using System.Collections.Generic;
using TF.RunSafty.Model;
using TF.RunSafty.Model.InterfaceModel;
namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///IAddTrainno 的摘要说明
    /// </summary>
    public class IAddTrainno : IQueryResult
    {
        public IAddTrainno()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private class Train
        {
            public string trainnoID;

        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public Train data;
        }
        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                TF.RunSafty.Model.InterfaceModel.ParamModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TF.RunSafty.Model.InterfaceModel.ParamModel>(this.Data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    TF.RunSafty.BLL.TAB_Base_TrainNo bllTrain = new TF.RunSafty.BLL.TAB_Base_TrainNo();
                    TF.RunSafty.Model.TAB_Base_TrainNo train = new TF.RunSafty.Model.TAB_Base_TrainNo();
                    Train t = new Train();
                    if (bllTrain.AddByParamModel(paramModel, train))
                    {
                        t.trainnoID = train.strGUID;
                        jsonModel.result = 0;
                        jsonModel.resultStr = "返回成功";
                        jsonModel.data = t;
                    }
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
