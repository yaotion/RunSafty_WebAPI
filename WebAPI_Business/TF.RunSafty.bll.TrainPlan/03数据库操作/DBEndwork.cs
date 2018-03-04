using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.Model.InterfaceModel;


namespace TF.RunSafty.Plan
{
    public class DBEndwork
    {
        public static bool ExistEndWorkPlan(string strSiteGUID, string strTrainmanGUID)
        {
            string strWhere = string.Format(@" strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = '{0}') and 
nPlanState >= {1} AND (strTrainmanGUID1 = '{2}' or strTrainmanGUID2 = '{2}'
or strTrainmanGUID3 = '{2}' or strTrainmanGUID4 = '{2}') order by dtStartTime desc", strSiteGUID, (int)TRsPlanState.psBeginWork, strTrainmanGUID);
            TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork bllEndWork = new TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork();
            return bllEndWork.GetModelList(strWhere).Count > 0;
        }

    }
}
