using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.BLL
{
    public partial class TAB_Plan_OutRoom
    {
        private readonly TF.RunSafty.DAL.TAB_Plan_InOutRoom dal = new TF.RunSafty.DAL.TAB_Plan_InOutRoom();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TF.RunSafty.Model.Model_Plan_OutRoom model)
        {
            return dal.AddOutRoom(model);
        }


        public bool AddByParamModel(TF.RunSafty.Model.InterfaceModel.PlanOutRoom paramModel, TF.RunSafty.Model.Model_Plan_OutRoom PlanInRoom)
        {
            SetModelValue(paramModel, PlanInRoom);
            return this.Add(PlanInRoom);
        }



        public void SetModelValue(TF.RunSafty.Model.InterfaceModel.PlanOutRoom paramModel, TF.RunSafty.Model.Model_Plan_OutRoom PlanOutRoom)
        {
            PlanOutRoom.strOutRoomGUID = paramModel.strOutRoomGUID;
            PlanOutRoom.strTrainPlanGUID = paramModel.strTrainPlanGUID;
            PlanOutRoom.strTrainmanGUID = paramModel.strTrainmanGUID;
            PlanOutRoom.dtOutRoomTime = paramModel.dtOutRoomTime;
            PlanOutRoom.nOutRoomVerifyID = paramModel.nOutRoomVerifyID;
            PlanOutRoom.strDutyUserGUID = paramModel.strDutyUserGUID;
            PlanOutRoom.strTrainmanNumber = paramModel.strTrainmanNumber;
            PlanOutRoom.dtCreateTime = paramModel.dtCreateTime;
            PlanOutRoom.strInRoomGUID = paramModel.strInRoomGUID;
            PlanOutRoom.strSiteGUID = paramModel.strSiteGUID;
            PlanOutRoom.dtArriveTime = paramModel.dtArriveTime;
            PlanOutRoom.strWaitPlanGUID = paramModel.strWaitPlanGUID;
            PlanOutRoom.ePlanType = paramModel.ePlanType;
            PlanOutRoom.nBedNumber = paramModel.nBedNumber;
            PlanOutRoom.strRoomNumber = paramModel.strRoomNumber;

        }
    }
}