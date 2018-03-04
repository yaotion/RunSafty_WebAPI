using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.Runsafty.TrainNo
{
    public partial class LCTrainno
    {
        #region 添加图定车次


        public class Add_In
        {
            public TrainNo trainnoInfo;
        }

        public class Add_Out
        {
            public string result = "";
            public string resultStr = "";
            public Train data;
        }
        /// <summary>
        /// 添加图定车次
        /// </summary>
        /// <returns></returns>
        public Add_Out Add(string data)
        {
            Add_Out json = new Add_Out();
            Add_In trainNo = Newtonsoft.Json.JsonConvert.DeserializeObject<Add_In>(data);
            TF.RunSafty.BLL.TAB_Base_TrainNo bllTrain = new TF.RunSafty.BLL.TAB_Base_TrainNo();
            TF.RunSafty.Model.TAB_Base_TrainNo train = new TF.RunSafty.Model.TAB_Base_TrainNo();
            Train t = new Train();
            try
            {
                string strGUID = Guid.NewGuid().ToString();
                train.strGUID = strGUID;
                SetModelValue(trainNo.trainnoInfo, train);
                if (bllTrain.Add(train))
                {
                    t.trainnoID = train.strGUID;
                    json.result = "0";
                    json.resultStr = "返回成功";
                    json.data = t;
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }
        public void SetModelValue(TrainNo paramModel, TF.RunSafty.Model.TAB_Base_TrainNo train)
        {
            train.dtCreateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(paramModel.dragTypeID))
                train.nDragType = int.Parse(paramModel.dragTypeID);
            if (!string.IsNullOrEmpty(paramModel.kehuoID))
                train.nKehuoID = int.Parse(paramModel.kehuoID);
            if (!string.IsNullOrEmpty(paramModel.planTypeID))
                train.nPlanType = int.Parse(paramModel.planTypeID);
            train.nRemarkType = int.Parse(paramModel.remarkTypeID);
            train.nTrainmanTypeID = int.Parse(paramModel.trainmanTypeID);
            train.strEndStation = paramModel.endStationID;
            train.strPlaceID = paramModel.placeID;
            train.strRemark = paramModel.remark;
            train.strStartStation = paramModel.startStationID;
            train.strTrainJiaoluGUID = paramModel.trainjiaoluID;
            train.strTrainNo = paramModel.trainNo;
            train.strTrainNumber = paramModel.trainNumber;
            train.strTrainTypeName = paramModel.trainTypeName;
            DateTime dt = paramModel.startTime;
            train.dtStartTime = dt;
            train.dtRealStartTime = dt;
            DateTime dtChuQinTime;
            if (DateTime.TryParse(paramModel.kaiCheTime.ToString(), out dtChuQinTime))
            {
                train.dtPlanStartTime = dtChuQinTime;
            }
            train.nNeedRest = paramModel.nNeedRest;
            train.dtCallTime = paramModel.dtCallTime;
            train.dtArriveTime = paramModel.dtArriveTime;
            train.strWorkDay = paramModel.strWorkDay;
        }
        #endregion

        #region 删除图定车次
        public class Delete_In
        {
            public string trainnoID = "";
        }
        public class Delete_Out
        {
            public string result = "";
            public string resultStr = "";
            //public object data;
        }
        public Delete_Out Delete(string data)
        {
            Delete_Out json = new Delete_Out();
            Delete_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Delete_In>(data);
            TF.RunSafty.BLL.TAB_Base_TrainNo bllTrain = new TF.RunSafty.BLL.TAB_Base_TrainNo();
            try
            {
                if (bllTrain.Delete(input.trainnoID))
                {
                    json.result = "0";
                    json.resultStr = "返回成功";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 删除一个交路的图定车次
        public class DeleteByJiaoLu_In
        {
            public string TrainjiaoluID;
        }

        public Delete_Out DeleteByJiaoLu(string data)
        {
            Delete_Out json = new Delete_Out();
            DeleteByJiaoLu_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<DeleteByJiaoLu_In>(data);
            TF.RunSafty.BLL.TAB_Base_TrainNo bllTrain = new TF.RunSafty.BLL.TAB_Base_TrainNo();
            try
            {
                if (bllTrain.DeleteByJiaoLu(input.TrainjiaoluID))
                {
                    json.result = "0";
                    json.resultStr = "返回成功";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }

        #endregion


        #region 修改图定车次
        public class Update_In
        {
            public TrainNo trainnoInfo;
        }
        public class Update_Out
        {
            public string result = "";
            public string resultStr = "";
        }
        public Update_Out Update(string data)
        {
            Update_Out json = new Update_Out();
            Update_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Update_In>(data);
            try
            {
                TF.RunSafty.BLL.TAB_Base_TrainNo bllTrain = new TF.RunSafty.BLL.TAB_Base_TrainNo();
                TF.RunSafty.Model.TAB_Base_TrainNo train = bllTrain.GetModel(input.trainnoInfo.trainNoID);
                SetModelValue(input.trainnoInfo, train);
                if (bllTrain.Update(train))
                {
                    json.result = "0";
                    json.resultStr = "返回成功";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.InnerException.Message;
            }
            return json;
        }
        #endregion



        #region 从图定车次加载行车计划
        public class LoadTrainnos_In
        {
            public string trainjiaoluID = "";
            public string beginTime = "";
            public string endTime = "";
            public int planState;
        }
        public class LoadTrainnos_Out
        {
            public int result;
            public string resultStr = "";
        }
        public LoadTrainnos_Out LoadTrainnos(string data)
        {
            LoadTrainnos_Out json = new LoadTrainnos_Out();
            LoadTrainnos_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<LoadTrainnos_In>(data);
            try
            {
                TF.RunSafty.BLL.TAB_Base_TrainNo bllTrain = new TF.RunSafty.BLL.TAB_Base_TrainNo();
                DateTime dtBegin, dtEnd;
                string strTrainjiao = input.trainjiaoluID;
                dtBegin = DateTime.Parse(input.beginTime);
                dtEnd = DateTime.Parse(input.endTime);
                int PlanState = input.planState <= 0 ? 1 : input.planState;

                bllTrain.GetTrainnoByTime(strTrainjiao, dtBegin, dtEnd, PlanState);
                json.result = 0;
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = 1;
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 获取指定的图定车次信息
        public class Find_In
        {
            public string trainnoID = "";
        }
        public class Find_Out
        {
            public string result = "";
            public string resultStr = "";
            public TrainNo data;
        }
        public Find_Out GetTrainnoByID(string data)
        {
            Find_Out json = new Find_Out();
            Find_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Find_In>(data);
            try
            {
                TF.RunSafty.BLL.VIEW_Base_TrainNo bllPlace = new TF.RunSafty.BLL.VIEW_Base_TrainNo();
                List<TF.RunSafty.Model.VIEW_Base_TrainNo> list = bllPlace.GetTrainnosByID(input.trainnoID);
                json.result = "0";
                json.resultStr = "提交成功";
                if (list != null && list.Count > 0)
                {
                    json.data = GetTrainNo(list[0]);
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
        private TrainNo GetTrainNo(TF.RunSafty.Model.VIEW_Base_TrainNo trainNo)
        {
            TrainNo model = new TrainNo();
            model.trainjiaoluID = trainNo.strTrainJiaoluGUID;
            model.trainjiaoluName = trainNo.strTrainJiaoluName;
            model.placeID = trainNo.strPlaceID;
            model.placeName = trainNo.strPlaceName;
            model.trainTypeName = trainNo.strTrainTypeName;
            model.trainNumber = trainNo.strTrainNumber;
            model.trainNo = trainNo.strTrainNo;
            model.remark = trainNo.strRemark;
            if (trainNo.dtStartTime.HasValue)
            {
                model.startTime = trainNo.dtStartTime.Value;
            }
            model.startStationID = trainNo.strStartStation;
            model.startStationName = trainNo.strStartStationName;
            model.endStationID = trainNo.strEndStation;
            model.endStationName = trainNo.strEndStationName;
            model.trainmanTypeID = trainNo.nTrainmanTypeID.ToString();
            model.trainmanTypeName = trainNo.strTrainmanTypeName;
            model.planTypeID = trainNo.nPlanType.ToString();
            model.planTypeName = trainNo.strPlanTypeName;
            model.dragTypeID = trainNo.nDragType.ToString();
            model.dragTypeName = trainNo.nDragTypeName;
            model.kehuoID = trainNo.nKehuoID.ToString();
            model.kehuoName = trainNo.strKehuoName;
            model.remarkTypeID = trainNo.nRemarkType.ToString();
            model.remarkTypeName = trainNo.strRemarkTypeName;
            model.trainNoID = trainNo.strGUID;
            if (trainNo.dtPlanStartTime.HasValue)
            {
                model.kaiCheTime = trainNo.dtPlanStartTime.Value;
            }
            else
            {
                if (trainNo.dtStartTime.HasValue)
                {
                    model.kaiCheTime = trainNo.dtStartTime.Value;
                }
            }
            model.strWorkDay = trainNo.strWorkDay;
            model.dtCallTime = trainNo.dtCallTime;
            model.dtArriveTime = trainNo.dtArriveTime;
            model.nNeedRest = trainNo.nNeedRest;
            return model;
        }
        #endregion

        #region 获取区段下图定车次列表

        public class TrainJiaolu_In
        {
            public string trainjiaoluID;
        }
        public class Trainjiaolu_Out
        {
            public object data;
            public string result = "";
            public string resultStr = "";
        }
        public Trainjiaolu_Out GetTrainnosOfTrainjiaolu(string data)
        {
            Trainjiaolu_Out json = new Trainjiaolu_Out();
            TrainJiaolu_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<TrainJiaolu_In>(data);
            try
            {
                TF.RunSafty.BLL.VIEW_Base_TrainNo bllPlace = new TF.RunSafty.BLL.VIEW_Base_TrainNo();
                List<TF.RunSafty.Model.VIEW_Base_TrainNo> placeList = bllPlace.GetTrainnosByTrainJiaolu(input.trainjiaoluID);
                json.result = "0";
                json.resultStr = "提交成功";
                json.data = bllPlace.GetPlaceList(placeList);
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }

        #endregion

    }

}