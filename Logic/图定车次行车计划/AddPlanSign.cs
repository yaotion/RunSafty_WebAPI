using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.BLL
{
    public partial class AddPlanSign
    {
        private readonly TF.RunSafty.DAL.Tab_Plan_Rest dal = new TF.RunSafty.DAL.Tab_Plan_Rest();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TF.RunSafty.Model.TAB_Plan_Rest model)
        {
            model.strGUID = System.Guid.NewGuid().ToString();
            return dal.AddPlan(model);
        }


      

        public int AddByParamModel(TF.RunSafty.Model.InterfaceModel.PlanSign paramModel, TF.RunSafty.Model.TAB_Plan_Rest PlanSign)
        {
            SetModelValue(paramModel, PlanSign);
            return this.Add(PlanSign);
        }


        public int EditParamModel(TF.RunSafty.Model.InterfaceModel.PlanSign paramModel, TF.RunSafty.Model.TAB_Plan_Rest PlanSign)
        {
            SetModelValue(paramModel, PlanSign);
            return this.Edit(PlanSign);
        }

        //编辑
        public int Edit(TF.RunSafty.Model.TAB_Plan_Rest model)
        {
            return dal.UpdatePlan(model);
        }


        public int DelParamModel(string strGUID)
        {
            return dal.deletePlan(strGUID);
        }




        public void SetModelValue(TF.RunSafty.Model.InterfaceModel.PlanSign paramModel, TF.RunSafty.Model.TAB_Plan_Rest PlanSign)
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