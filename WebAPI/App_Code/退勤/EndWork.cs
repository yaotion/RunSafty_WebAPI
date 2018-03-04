using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSafty.Model;
using TF.RunSafty.Model.InterfaceModel;
using TF.Api.Utilities;
using TF.RunSafty.BLL;
namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    /// EndWork 的摘要说明
    /// </summary>
    public class IExecTuiQin : IQueryResult
    {
        public IExecTuiQin()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private class InputParam : ParamBase
        {
            public InputData data;

        }
        public class InputData
        {
            public string siteID = "";
            [NotNull]
            public EndWorkData endwork { get; set; }
            [NotNull]
            public DrinkData drink { get; set; }
            [NotNull]
            public DutyInfo dutyUser { get; set; }
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
            jsonModel.result = 1;
            jsonModel.resultStr = "数据提交失败：未找到指定的机组信息";
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                InputParam paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<InputParam>(this.Data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    TF.RunSafty.BLL.RCEndWork rcEndWork = new RunSafty.BLL.RCEndWork();
                    if (rcEndWork.Endwork(paramModel.data.siteID, paramModel.data.endwork, paramModel.data.drink, paramModel.data.dutyUser))
                    {
                        jsonModel.result = 0;
                        jsonModel.resultStr = "提交成功";
                    }
                    else
                    {

                        jsonModel.result = 1;
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