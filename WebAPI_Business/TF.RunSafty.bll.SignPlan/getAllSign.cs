using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TF.RunSafty.SignPlan
{
    public partial class getAllSign
    {

        private readonly DALGetAllSign dal = new DALGetAllSign();
        public List<Model_Plan_ToBeTake> GetPlanTrain(string strJiaoluGUID)
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
        public List<Model_Plan_ToBeTake> DataTableToList(DataTable dt)
        {
            List<Model_Plan_ToBeTake> modelList = new List<Model_Plan_ToBeTake>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model_Plan_ToBeTake model;
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





        public List<Model_Plan_ToBeTake> GetPlaceList(List<Model_Plan_ToBeTake> placeList)
        {
            if (placeList != null)
            {
                List<Model_Plan_ToBeTake> resultList = new List<Model_Plan_ToBeTake>();
                foreach (Model_Plan_ToBeTake place in placeList)
                {
                    Model_Plan_ToBeTake model = new Model_Plan_ToBeTake();
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




        public void SetModelValue(TF.RunSafty.Model.InterfaceModel.PlanRestModels PlanRestModel, Modal_Plan_Rest Plan_Rest)
        {
            string dt = DateTime.Now.ToString();
            if (PlanRestModel.dtarrivetime.ToString() != "")
                Plan_Rest.dtArriveTime = PlanRestModel.dtarrivetime.ToString();
            else
                Plan_Rest.dtArriveTime = dt;
            if (PlanRestModel.dtcalltime.ToString() != "")
                Plan_Rest.dtCallTime = PlanRestModel.dtcalltime.ToString();
            else
                Plan_Rest.dtCallTime = dt;

            if (PlanRestModel.dtchuqintime.ToString() != "")
                Plan_Rest.dtChuQinTime = PlanRestModel.dtchuqintime.ToString();
            else
                Plan_Rest.dtChuQinTime = dt;

            if (PlanRestModel.dtStartTrainTime.ToString() != "")
                Plan_Rest.dtStartTrainTime = PlanRestModel.dtStartTrainTime.ToString();
            else
                Plan_Rest.dtStartTrainTime = dt;

            if (PlanRestModel.dtSignTime.ToString() != "")
                Plan_Rest.dtSignTime = PlanRestModel.dtSignTime.ToString();
            else
                Plan_Rest.dtSignTime = dt;




            Plan_Rest.strGUID = PlanRestModel.strguid;
            Plan_Rest.strTrainJiaoLuGUID = PlanRestModel.strtrainjiaoluguid;
            Plan_Rest.strTrainmanGUID1 = PlanRestModel.strtrainmanguid1;
            Plan_Rest.strTrainmanGUID2 = PlanRestModel.strtrainmanguid2;
            Plan_Rest.strTrainmanGUID3 = PlanRestModel.strtrainmanguid3;
            Plan_Rest.strTrainmanGUID4 = PlanRestModel.strtrainmanguid4;
            Plan_Rest.strTrainNo = PlanRestModel.strtrainno;
            Plan_Rest.strTrainNoGUID = PlanRestModel.strtrainnoguid;
            Plan_Rest.strWorkGrouGUID = PlanRestModel.strworkgrouguid;
            Plan_Rest.nNeedRest = PlanRestModel.nNeedRest;
            if (PlanRestModel.ePlanState != "")
                Plan_Rest.ePlanState = int.Parse(PlanRestModel.ePlanState);
            else
                Plan_Rest.ePlanState = 0;

        }





    }
}
