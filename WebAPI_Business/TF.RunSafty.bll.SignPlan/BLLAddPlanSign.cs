using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.SignPlan
{
    public partial class AddPlanSign
    {
        private readonly Dal_Plan_Rest dal = new Dal_Plan_Rest();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Modal_Plan_Rest model)
        {
            model.strGUID = System.Guid.NewGuid().ToString();
            return dal.AddPlan(model);
        }




        public int AddByParamModel(TF.RunSafty.Model.InterfaceModel.PlanSign paramModel, Modal_Plan_Rest PlanSign)
        {
            SetModelValue(paramModel, PlanSign);
            return this.Add(PlanSign);
        }


        public int EditParamModel(TF.RunSafty.Model.InterfaceModel.PlanSign paramModel, Modal_Plan_Rest PlanSign)
        {
            SetModelValue(paramModel, PlanSign);
            return this.Edit(PlanSign);
        }

        //编辑
        public int Edit(Modal_Plan_Rest model)
        {
            return dal.UpdatePlan(model);
        }


        public int DelParamModel(string strGUID)
        {
            return dal.deletePlan(strGUID);
        }




        public void SetModelValue(TF.RunSafty.Model.InterfaceModel.PlanSign paramModel, Modal_Plan_Rest PlanSign)
        {
            PlanSign.strGUID = paramModel.strGUID;
            PlanSign.nNeedRest = paramModel.nNeedRest;
            PlanSign.strTrainJiaoLuGUID = paramModel.strTrainJiaoLuGUID;
            PlanSign.strTrainNo = paramModel.strTrainNo;
            PlanSign.dtArriveTime = paramModel.dtArriveTime;
            PlanSign.dtCallTime = paramModel.dtCallTime;
            PlanSign.dtChuQinTime = paramModel.dtChuQinTime;
            PlanSign.dtStartTrainTime = paramModel.dtStartTrainTime;

        }
    }
}