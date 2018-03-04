using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.Model.InterfaceModel;

namespace TF.RunSafty.SignPlan
{
   public class ISignPlan
    {
        #region ==========================获取签点计划========================================
        public class ParamModel
        {
            public string dtStartTime;
            public string dtEndTime;
            public string strJiaoluGUID;
        }
        public class JsonModel
        {
            public int result;
            public string resultStr;
            public planArrays data;
        }
        public class planArrays
        {
            public string strTrainJiaoLuGUID;
            public string strCurPlanGUID;
            public object planArray;
        }
        public JsonModel Getsignplanlist(string input)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                ParamModel paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(input);
                Bll_Plan_Rest bllPlace = new Bll_Plan_Rest();
                List<Modal_Plan_Rest> placeList = bllPlace.GetPlanTrain(paramModel.dtStartTime, paramModel.dtEndTime, paramModel.strJiaoluGUID);
                jsonModel.result = 0;
                jsonModel.resultStr = "提交成功";
                planArrays p = new planArrays();
                jsonModel.data = p;
                p.strTrainJiaoLuGUID = paramModel.strJiaoluGUID;
                p.strCurPlanGUID = "";
                p.planArray = bllPlace.GetPlaceList(placeList);
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
            return jsonModel;
        }
        #endregion

        #region =========================添加签点计划========================================
        public class DataAddSignPlan
        {
            public string SignPlanGUID;

        }
        public class OutAddSignPlan
        {
            public int result;
            public string resultStr;
            public DataAddSignPlan data;
        }

        public OutAddSignPlan Addsignplan(string input)
        {
            OutAddSignPlan jsonModel = new OutAddSignPlan();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                TF.RunSafty.Model.InterfaceModel.PlanSign paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TF.RunSafty.Model.InterfaceModel.PlanSign>(input);
                //验证数据正确性,非空字段不能为空

                AddPlanSign bllSignPlan = new AddPlanSign();
                Modal_Plan_Rest PlanSign = new Modal_Plan_Rest();
                DataAddSignPlan t = new DataAddSignPlan();
                if (bllSignPlan.AddByParamModel(paramModel, PlanSign) != 0)
                {
                    t.SignPlanGUID = PlanSign.strGUID;
                    jsonModel.result = 0;
                    jsonModel.resultStr = "返回成功";
                    jsonModel.data = t;
                }

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                jsonModel.resultStr = "提交失败" + ex.Message;
            }
            return jsonModel;
        }
        #endregion

        #region =========================修改签点计划========================================

        public string Editsignplan(string input)
        {
            OutAddSignPlan jsonModel = new OutAddSignPlan();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                TF.RunSafty.Model.InterfaceModel.PlanSign paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TF.RunSafty.Model.InterfaceModel.PlanSign>(input);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel))
                {
                    AddPlanSign bllSignPlan = new AddPlanSign();
                    Modal_Plan_Rest PlanSign = new Modal_Plan_Rest();
                    DataAddSignPlan t = new DataAddSignPlan();
                    if (bllSignPlan.EditParamModel(paramModel, PlanSign) != 0)
                    {
                        t.SignPlanGUID = PlanSign.strGUID;
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
        #endregion

        #region =========================删除签点计划========================================
        public class DelSinPlan
        {
            public string strGUID;
        }

        //输出
        private class strSignPlans
        {
            public string strSignPlan;

        }
        private class DelJsonModel
        {
            public int result;
            public string resultStr;
            public strSignPlans data;
        }

        public string Delsignplan(string input)
        {
            DelJsonModel jsonModel = new DelJsonModel();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                DelSinPlan paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DelSinPlan>(input);

                AddPlanSign bllSignPlan = new AddPlanSign();
                Modal_Plan_Rest PlanSign = new Modal_Plan_Rest();
                strSignPlans t = new strSignPlans();
                if (bllSignPlan.DelParamModel(paramModel.strGUID) != 0)
                {
                    t.strSignPlan = "删除成功";
                    jsonModel.result = 0;
                    jsonModel.resultStr = "返回成功";
                    jsonModel.data = t;
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

        #endregion

        #region ==================通过交路列表获取签点计划==============================

        public class TrainmanData
        {
            public List<JiaoLu> JiaoLuAry;
            public string dtStartTime;
            public string dtEndTime;
        }



        public class JiaoLu
        {
            public string strTrainJiaoLuGUID;
        }


        private class OutJsonModel
        {
            public int result;
            public string resultStr;
            public object data;
        }

        private class ArrayName
        {
            public object signplanary;
        }


        public string Getsignplanlistbyjiaoluary(string input)
        {
            OutJsonModel jsonModel = new OutJsonModel();
            try
            {
                TrainmanData paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TrainmanData>(input);

                Bll_Plan_Rest bllPlace = new Bll_Plan_Rest();
                string strJiaoLu = "";
                foreach (JiaoLu JiaoLuId in paramModel.JiaoLuAry)
                {
                    strJiaoLu += "'" + JiaoLuId.strTrainJiaoLuGUID + "',";
                }
                strJiaoLu = strJiaoLu.Substring(0, strJiaoLu.Length - 1);
                List<Modal_Plan_Rest> placeList = bllPlace.GetPlanListByTimeAndWorkShop(strJiaoLu, paramModel.dtStartTime, paramModel.dtEndTime);
                jsonModel.result = 0;
                jsonModel.resultStr = "提交成功";
                ArrayName an = new ArrayName();
                jsonModel.data = an;
                an.signplanary = bllPlace.GetPlaceList(placeList);
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

        #endregion

        #region ======================修改图定车次======================================
        private class EditTrainplanJsonModel
        {
            public int result;
            public string resultStr;
            public object data;
        }
        public string Modifysignplan(string input)
        {
            EditTrainplanJsonModel jsonModel = new EditTrainplanJsonModel();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                PlanRestModels paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<PlanRestModels>(input);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel))
                {
                    Bll_Plan_Rest bllPlace = new Bll_Plan_Rest();
                    if (bllPlace.UpdateByParamModel(paramModel, ""))
                    {
                        jsonModel.result = 0;
                        jsonModel.resultStr = "返回成功";
                    }
                    else
                    {
                        jsonModel.result = 2;
                        jsonModel.resultStr = "返回失败";
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

        #endregion

        #region==================== 修改签点人员=========================================

        public class EditTrainman
        {
            public string strPlanGUID { get; set; }
            public string strNewTrainmanGUID { get; set; }
            public string strOldTrainmanGUID { get; set; }
            public string nTrainmanIndex { get; set; }
            public string strReason { get; set; }
            public string dtModifyTime { get; set; }
            public string strWorkGroupGUID { get; set; }
            public int ePlanState { get; set; }
        }
        private class EditPersonJsonModel
        {
            public int result;
            public string resultStr;
            public object data = new object();
        }
        public string Modifysignplantrainman(string input)
        {
            EditPersonJsonModel jsonModel = new EditPersonJsonModel();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                EditTrainman paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<EditTrainman>(input);
                //验证数据正确性,非空字段不能为空
                if (validater.IsNotNullPropertiesValidated(paramModel))
                {
                    Dal_Plan_Rest dalTrain = new Dal_Plan_Rest();
                    if (dalTrain.UpdateTrainman(paramModel.strPlanGUID, paramModel.strNewTrainmanGUID, paramModel.strOldTrainmanGUID, paramModel.nTrainmanIndex, paramModel.strReason, paramModel.dtModifyTime, paramModel.strWorkGroupGUID, paramModel.ePlanState))
                    {
                        jsonModel.result = 0;
                        jsonModel.resultStr = "返回成功";
                    }
                    else
                    {
                        jsonModel.result = 2;
                        jsonModel.resultStr = "返回失败";
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
        #endregion

        #region ===========================获取待乘计划===============================


        private class JiaoLuData
        {
            public string strPlanGUID;
        }

        private class JsonModelss
        {
            public int result;
            public string resultStr;
            public object data;
        }

        public string Getwaitworksignplan(string input)
        {
            JsonModelss jsonModel = new JsonModelss();
            try
            {
                JiaoLuData paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<JiaoLuData>(input);
                getAllSign bllPlace =new getAllSign();
                List<Model_Plan_ToBeTake> plans = bllPlace.GetPlanTrain(paramModel.strPlanGUID);
                jsonModel.result = 0;
                jsonModel.resultStr = "提交成功";
                //jsonModel.data = bllPlace.GetPlaceList(placeList);

                if (plans.Count > 0)
                {
                    PlanBeginWorks work = new PlanBeginWorks();
                    work.strCheCi = plans[0].StrTrainNo;
                    work.dtCallWorkTime = plans[0].dtCallWorkTime.ToString();
                    work.dtWaitWorkTime = plans[0].dtWaitWorkTime.ToString();

                    work.strTrainmanGUID1 = plans[0].strTrainmanGUID1;
                    work.strTrainmanGUID2 = plans[0].strTrainmanGUID2;
                    work.strTrainmanGUID3 = plans[0].strTrainmanGUID3;
                    work.strTrainmanGUID4 = plans[0].strTrainmanGUID4;

                    work.strTrainmanName1 = plans[0].strTrainmanName1;
                    work.strTrainmanName2 = plans[0].strTrainmanName2;
                    work.strTrainmanName3 = plans[0].strTrainmanName3;
                    work.strTrainmanName4 = plans[0].strTrainmanName4;

                    work.strTrainmanNumber1 = plans[0].strTrainmanNumber1;
                    work.strTrainmanNumber2 = plans[0].strTrainmanNumber2;
                    work.strTrainmanNumber3 = plans[0].strTrainmanNumber3;
                    work.strTrainmanNumber4 = plans[0].strTrainmanNumber4;


                    work.NNeedRest = plans[0].NNeedRest;
                    work.NPlanState = plans[0].NPlanState;

                    jsonModel.data = work;
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
            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, timeConverter);
        }
        #endregion

    }
}

