using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.BLL
{
    public partial class VIEW_Plan_Trainman_TrainPlan
    {
        private readonly TF.RunSafty.DAL.VIEW_Plan_Trainman_TrainPlan dal = new TF.RunSafty.DAL.VIEW_Plan_Trainman_TrainPlan();

        public List<TF.RunSafty.Model.VIEW_Plan_Trainman_TrainPlan> GetPlan_Trainman(string strGUID)
        {
            return dal.GetTrainPlan(strGUID);
        }

    }
}

