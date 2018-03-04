using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.BLL
{
    public partial class TAB_Plan_InRoom
    {
        private readonly TF.RunSafty.DAL.TAB_Plan_InOutRoom dal = new TF.RunSafty.DAL.TAB_Plan_InOutRoom();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TF.RunSafty.Model.Model_Plan_InRoom model)
        {
            return dal.AddInRoom(model);
        }


        public bool AddByParamModel(TF.RunSafty.Model.InterfaceModel.PlanInRoom paramModel, TF.RunSafty.Model.Model_Plan_InRoom PlanInRoom)
        {
            SetModelValue(paramModel, PlanInRoom);
            return this.Add(PlanInRoom);
        }



        public void SetModelValue(TF.RunSafty.Model.InterfaceModel.PlanInRoom paramModel, TF.RunSafty.Model.Model_Plan_InRoom PlanInRoom)
        {

            PlanInRoom.strInRoomGUID = paramModel.strInRoomGUID;
            PlanInRoom.strTrainPlanGUID = paramModel.strTrainPlanGUID;
            PlanInRoom.strTrainmanGUID = paramModel.strTrainmanGUID;
            PlanInRoom.dtInRoomTime = paramModel.dtInRoomTime;
            PlanInRoom.nInRoomVerifyID = paramModel.nInRoomVerifyID;
            PlanInRoom.strDutyUserGUID = paramModel.strDutyUserGUID;
            PlanInRoom.strTrainmanNumber = paramModel.strTrainmanNumber;
            PlanInRoom.dtCreateTime = paramModel.dtCreateTime;
            PlanInRoom.strSiteGUID = paramModel.strSiteGUID;
            PlanInRoom.strRoomNumber = paramModel.strRoomNumber;
            PlanInRoom.nBedNumber = paramModel.nBedNumber;
            PlanInRoom.dtArriveTime = paramModel.dtArriveTime;
            PlanInRoom.strWaitPlanGUID = paramModel.strWaitPlanGUID;
            PlanInRoom.ePlanType = paramModel.ePlanType;
        }
    }
}