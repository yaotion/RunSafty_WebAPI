using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TF.RunSafty.BLL
{
    public partial class getAllSign
    {

        private readonly TF.RunSafty.DAL.GetAllSign dal = new TF.RunSafty.DAL.GetAllSign();
        public List<TF.RunSafty.Model.Model_Plan_ToBeTake> GetPlanTrain(string strJiaoluGUID)
        {
            DataSet set = dal.GetAllList(strJiaoluGUID);
            DataTable table = set.Tables[0];
            DataView view = table.DefaultView;
            table = view.ToTable();
            return DataTableToList(table);
        }




        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TF.RunSafty.Model.Model_Plan_ToBeTake> DataTableToList(DataTable dt)
        {
            List<TF.RunSafty.Model.Model_Plan_ToBeTake> modelList = new List<TF.RunSafty.Model.Model_Plan_ToBeTake>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TF.RunSafty.Model.Model_Plan_ToBeTake model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModelBeginWork(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }





        public List<TF.RunSafty.Model.Model_Plan_ToBeTake> GetPlaceList(List<TF.RunSafty.Model.Model_Plan_ToBeTake> placeList)
        {
            if (placeList != null)
            {
                List<TF.RunSafty.Model.Model_Plan_ToBeTake> resultList = new List<TF.RunSafty.Model.Model_Plan_ToBeTake>();
                foreach (TF.RunSafty.Model.Model_Plan_ToBeTake place in placeList)
                {
                    TF.RunSafty.Model.Model_Plan_ToBeTake model = new TF.RunSafty.Model.Model_Plan_ToBeTake();
                    model.strCheCi = place.strCheCi;
                    model.dtCallWorkTime = place.dtCallWorkTime;
                    model.dtWaitWorkTime = place.dtWaitWorkTime;
                
                    model.strTrainmanGUID1 = place.strTrainmanGUID1;
                    model.strTrainmanGUID2 = place.strTrainmanGUID2;
                    model.strTrainmanGUID3 = place.strTrainmanGUID3;
                    model.strTrainmanGUID4 = place.strTrainmanGUID4;

                    model.NNeedRest = place.NNeedRest;
                    model.NPlanState = place.NPlanState;
                    model.StrTrainNo = place.StrTrainNo;
                    model.strPlanGUID = place.strPlanGUID;

                    resultList.Add(model);
                }
                return resultList;
            }
            return null;
        }


 

        public void SetModelValue(TF.RunSafty.Model.InterfaceModel.PlanRestModel PlanRestModel, TF.RunSafty.Model.TAB_Plan_Rest Plan_Rest)
        {
            string dt = DateTime.Now.ToString();
            if (PlanRestModel.data.dtarrivetime.ToString() != "")
                Plan_Rest.dtArriveTime = PlanRestModel.data.dtarrivetime.ToString();
            else
                Plan_Rest.dtArriveTime = dt;
            if (PlanRestModel.data.dtcalltime.ToString() != "")
                Plan_Rest.dtCallTime = PlanRestModel.data.dtcalltime.ToString();
            else
                Plan_Rest.dtCallTime = dt;

            if (PlanRestModel.data.dtchuqintime.ToString() != "")
                Plan_Rest.dtChuQinTime = PlanRestModel.data.dtchuqintime.ToString();
            else
                Plan_Rest.dtChuQinTime = dt;

            if (PlanRestModel.data.dtStartTrainTime.ToString() != "")
                Plan_Rest.dtStartTrainTime = PlanRestModel.data.dtStartTrainTime.ToString();
            else
                Plan_Rest.dtStartTrainTime = dt;

            if (PlanRestModel.data.dtSignTime.ToString() != "")
                Plan_Rest.dtSignTime = PlanRestModel.data.dtSignTime.ToString();
            else
                Plan_Rest.dtSignTime = dt;




            Plan_Rest.strGUID = PlanRestModel.data.strguid;
            Plan_Rest.strTrainJiaoLuGUID = PlanRestModel.data.strtrainjiaoluguid;
            Plan_Rest.strTrainmanGUID1 = PlanRestModel.data.strtrainmanguid1;
            Plan_Rest.strTrainmanGUID2 = PlanRestModel.data.strtrainmanguid2;
            Plan_Rest.strTrainmanGUID3 = PlanRestModel.data.strtrainmanguid3;
            Plan_Rest.strTrainmanGUID4 = PlanRestModel.data.strtrainmanguid4;
            Plan_Rest.strTrainNo = PlanRestModel.data.strtrainno;
            Plan_Rest.strTrainNoGUID = PlanRestModel.data.strtrainnoguid;
            Plan_Rest.strWorkGrouGUID = PlanRestModel.data.strworkgrouguid;
            Plan_Rest.nNeedRest = PlanRestModel.data.nNeedRest;
            if (PlanRestModel.data.ePlanState != "")
                Plan_Rest.ePlanState = int.Parse(PlanRestModel.data.ePlanState);
            else
                Plan_Rest.ePlanState = 0;

        }





    }
}
