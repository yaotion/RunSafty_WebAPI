using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TF.RunSafty.Model;
using TF.RunSafty.Model.InterfaceModel;
using TF.Api.Utilities;
namespace TF.RunSaftyAPI.App_Api.Public
{

    /// <summary>
    /// 轮乘交路机组
    /// </summary>
    public class IOrderGroup : IQueryResult
    {
        public IOrderGroup()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private class OrderGroupQuery : ParamBase
        {
            public OrderGroupQueryParam data;

        }
        public class OrderGroupQueryParam
        {
            [NotNull]
            public string placeID { get; set; }
            [NotNull]
            public string trainmanjiaoluID { get; set; }
            [NotNull]
            public string trainmanID { get; set; }

        }
        //public class GroupParam
        //{
        //    public string planID { get; set; }
        //    public string trainmanJiaoluID { get; set; }
        //}
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public List<OrderGroup> data;
        }
        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                OrderGroupQuery paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderGroupQuery>(this.Data);

                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    string jiaoluID = paramModel.data.trainmanjiaoluID;
                    List<string> placeIDs = new List<string>();
                    string trainmanID = paramModel.data.trainmanID;
                    placeIDs.AddRange(paramModel.data.placeID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));


                    TF.RunSafty.BLL.LCNameGroup bllNameGroup = new RunSafty.BLL.LCNameGroup();
                    List<OrderGroup> groups = bllNameGroup.GetOrderGroups(jiaoluID, placeIDs, trainmanID);

                    jsonModel.data = groups;
                    jsonModel.result = 0;
                    jsonModel.resultStr = "提交成功";
                }

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
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, timeConverter);
            return result;
        }
    }

    /// <summary>
    /// 普通机组
    /// </summary>
    public class INameGroup : IQueryResult
    {
        public INameGroup()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private class OrderGroupQuery : ParamBase
        {
            public OrderGroupQueryParam data;

        }
        public class OrderGroupQueryParam
        {
            public string placeID { get; set; }
            [NotNull]
            public string trainmanjiaoluID { get; set; }
            [NotNull]
            public string trainmanID { get; set; }

        }
        public class GroupParam
        {
            public string planID { get; set; }
            [NotNull]
            public string trainmanjiaoluID { get; set; }
            [NotNull]
            public string trainmanID { get; set; }
        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public List<NameGroup> data;
        }
        public override string QueryResult()
        {
            JsonModel jsonModel = new JsonModel();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                OrderGroupQuery paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderGroupQuery>(this.Data);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel.data))
                {
                    string jiaoluID = paramModel.data.trainmanjiaoluID;
                    List<string> placeIDs = new List<string>();
                    string trainmanID = paramModel.data.trainmanID;
                    if (!string.IsNullOrEmpty(paramModel.data.placeID))
                    {
                        placeIDs.AddRange(paramModel.data.placeID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                    }
                    TF.RunSafty.BLL.LCNameGroup bllNameGroup = new RunSafty.BLL.LCNameGroup();
                    List<NameGroup> groups = bllNameGroup.GetGroups(jiaoluID, placeIDs, trainmanID);
                    //List<NameGroup> groups = null;
                    jsonModel.data = groups;
                    jsonModel.result = 0;
                    jsonModel.resultStr = "提交成功";
                    #region
                    //GroupParam param = paramModel.data;
                    //string jiaoluID = param.trainmanjiaoluID;
                    //string trainmanID = param.trainmanID;
                    ////根据PlanID获取计划信息,没有planID返回全部
                    //if (string.IsNullOrEmpty( jiaoluID))
                    //{
                    //    jsonModel.data = null;
                    //}
                    //else
                    //{
                    //    if (!string.IsNullOrEmpty(param.planID))
                    //    {
                    //        TF.RunSafty.BLL.TAB_Plan_Train bllPlan = new TF.RunSafty.BLL.TAB_Plan_Train();
                    //        string strWhere = string.Format(" strTrainPlanGUID='{0}' ", param.planID);
                    //        List<TF.RunSafty.Model.TAB_Plan_Train> plans = bllPlan.GetModelList(strWhere);
                    //        TF.RunSafty.Model.TAB_Plan_Train plan = new TAB_Plan_Train();
                    //        if (plans != null && plans.Count > 0)
                    //        {
                    //            plan = plans[0];
                    //            placeIDs.Add(plan.strPlaceID);
                    //        }
                    //        //获取人员交路类型
                    //        ThinkFreely.RunSafty.TrainmanJiaolu trainmanJiaolu = new ThinkFreely.RunSafty.TrainmanJiaolu(param.trainmanjiaoluID);
                    //        switch (trainmanJiaolu.nJiaoluType)
                    //        { 
                    //            case 4:
                    //                //包乘根据车号查找机组,步骤如下：
                    //                //1：从表TAB_Nameplate_TrainmanJiaolu_Train根据车型车号查找trainGUID
                    //                //2：从表TAB_Nameplate_TrainmanJiaolu_OrderInTrain根据trainGUID过滤查找strGroupGUID
                    //                groups = bllNameGroup.GetOrderInTrainGroups(plan.strTrainTypeName, plan.strTrainNumber);
                    //                if (groups == null || groups.Count == 0)
                    //                {
                    //                    groups = bllNameGroup.GetGroups(jiaoluID, placeIDs, trainmanID);
                    //                }
                    //                break;
                    //            default:
                    //                groups = bllNameGroup.GetGroups(jiaoluID, placeIDs, trainmanID);
                    //                break;
                    //        }

                    //    }
                    //    else
                    //    {
                    //        groups = bllNameGroup.GetGroups(jiaoluID, placeIDs, trainmanID);
                    //    }
                    //    //如果groups为空或者count为0,取出全部的
                    //    if (groups == null || groups.Count == 0)
                    //    {
                    //        placeIDs.Clear();
                    //        groups = bllNameGroup.GetGroups(jiaoluID, placeIDs, "");
                    //    }
                    //    jsonModel.data = groups;

                    //}
                    #endregion

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

    /// <summary>
    /// 变换机组所在出勤点
    /// </summary>
    public class ChangeGroupPlace : IQueryResult
    {
        private class InputParam : ParamBase
        {
            public InputData data;

        }
        public class InputData
        {
            [NotNull]
            public string groupID { get; set; }
            [NotNull]
            public string sourcePlaceID { get; set; }
            [NotNull]
            public string destPlaceID { get; set; }

        }
        private class JsonModel
        {
            public int result;
            public string resultStr;
            public object data = new object();
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

                    TF.RunSafty.BLL.LCNameGroup bllNameGroup = new RunSafty.BLL.LCNameGroup();
                    if (bllNameGroup.ChangeGroupPlace(paramModel.data.groupID, paramModel.data.sourcePlaceID, paramModel.data.destPlaceID))
                    {
                        jsonModel.result = 0;
                        jsonModel.resultStr = "提交成功";
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