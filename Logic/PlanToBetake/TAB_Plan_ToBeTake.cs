using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using System.Data;

namespace TF.RunSafty.BLL
{
    public partial class TAB_Plan_ToBeTake
    {

        private readonly TF.RunSafty.DAL.TAB_Plan_ToBeTake dal = new TF.RunSafty.DAL.TAB_Plan_ToBeTake();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TF.RunSafty.Model.Model_Plan_ToBeTake model)
        {
            return dal.Add(model);
        }


        public bool AddByParamModel(TF.RunSafty.Model.InterfaceModel.PlanToTakes paramModel, TF.RunSafty.Model.Model_Plan_ToBeTake PlanTake)
        {
            string strGUID = Guid.NewGuid().ToString();
            PlanTake.strPlanGUID = strGUID;
            SetModelValue(paramModel, PlanTake);
            return this.Add(PlanTake);
        }



        public void SetModelValue(TF.RunSafty.Model.InterfaceModel.PlanToTakes paramModel, TF.RunSafty.Model.Model_Plan_ToBeTake PlanTake)
        {

            PlanTake.strPlanGUID = paramModel.strPlanGUID;
            PlanTake.strCheCi = paramModel.strCheCi;
            PlanTake.dtCallWorkTime = paramModel.dtCallWorkTime;
            PlanTake.dtWaitWorkTime = paramModel.dtWaitWorkTime;
            PlanTake.strTrainmanGUID1 = paramModel.strTrainmanGUID1;
            PlanTake.strTrainmanGUID2 = paramModel.strTrainmanGUID2;
            PlanTake.strTrainmanGUID3 = paramModel.strTrainmanGUID3;
            PlanTake.strTrainmanGUID4 = paramModel.strTrainmanGUID4;
        }
    }
}
