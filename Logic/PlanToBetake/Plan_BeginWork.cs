using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TF.RunSafty.BLL
{
    public partial class Plan_BeginWork
    {

        private readonly TF.RunSafty.DAL.Plan_BeginWork dal = new TF.RunSafty.DAL.Plan_BeginWork();

        public List<TF.RunSafty.Model.Model_Plan_ToBeTake> GetPlan_BeginWork(string strGUID)
        {
            return dal.GetBeginWork(strGUID);
        }
    }
}
