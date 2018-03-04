using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.WorkSteps;

namespace TF.RunSafty.WorkSteps
{
   public class LCPlan
    {
        #region 获取出勤计划列表
        public class Get_InPlanWorkOutList
        {
            public string siteID { get; set; }
            public DateTime BeginTime { get; set; }
            public DateTime Endime { get; set; }
            public int nWorkTypeID { get; set; }
            public string str { get; set; }
        }
        public class Get_OutPlanWorkOutList
        {
            public string result = "";
            public string resultStr = "";
            public object data;
        }
        public Get_OutPlanWorkOutList GetPlanWorkOutList(string data)
        {
            Get_OutPlanWorkOutList json = new Get_OutPlanWorkOutList();
            try
            {
                Get_InPlanWorkOutList input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InPlanWorkOutList>(data);
                DBPlanToList db = new DBPlanToList();
                List<MDPlanBase> vplans = db.GetPlanWorkOutList(input.siteID, input.BeginTime, input.Endime, input.nWorkTypeID);
                List<TrainmanPlan> plans = db.GetBeginFlowPlanList(vplans);

                json.data = plans;
                json.result = "0";
                json.resultStr = "提交成功";

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }


        #endregion

        #region 获取乘务员当前正在值乘的出勤计划
        public class Get_InBeginworkFlow
        {
            public string siteID { get; set; }
            public string trainmanID { get; set; }
            public int nWorkTypeID { get; set; }


        }
        public class Get_OutGetBeginworkFlow
        {
            public string result = "";
            public string resultStr = "";
            public TrainmanPlan data;
        }
        public Get_OutGetBeginworkFlow GetOnePlanInfo(string data)
        {
            Get_OutGetBeginworkFlow json = new Get_OutGetBeginworkFlow();
            try
            {
                Get_InBeginworkFlow input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InBeginworkFlow>(data);
                DBPlanToOnInfo db = new DBPlanToOnInfo();
                List<MDPlanBase> vplans = db.GetData(input.siteID, input.trainmanID);
                TrainmanPlan model = new TrainmanPlan();
                List<TrainmanPlan> plans = db.GetPlanList(vplans,input.nWorkTypeID);
                if (plans != null && plans.Count > 0)
                {
                    json.data = plans[0];
                    json.result = "0";
                    json.resultStr = "提交成功";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "没有该乘务员的出勤计划";
                }
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion


        #region 获取退勤计划列表

        public class Get_InPlanEndWorkOutList
        {
            public string siteID { get; set; }
            public string begintime { get; set; }
            public string endtime { get; set; }
            public int showAll { get; set; }
        }
       
        public Get_OutPlanWorkOutList GetPlanEndWorkOutList(string data)
        {
            Get_OutPlanWorkOutList json = new Get_OutPlanWorkOutList();
            try
            {
                Get_InPlanEndWorkOutList input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InPlanEndWorkOutList>(data);
                DBPlanToList db = new DBPlanToList();
                List<MDEndWork_Full> vplans = db.GetPlanEndWorkOutList(input.siteID,input.begintime,2,input.showAll);
                BeginEndWork bll = new BeginEndWork();
                List<TF.RunSafty.WorkSteps.BeginEndWork.TuiQinPlan> plans = bll.GetPlanList(vplans);
                if (vplans != null && vplans.Count>0)
                {
                    json.data = plans;
                    json.result = "0";
                    json.resultStr = "提交成功";
                }

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion


        #region 获取指定人员在指定客户端下退勤计划
        public class Get_InPlanEndWorkOut
        {
            public string siteID { get; set; }
            public string trainmanID { get; set; }
        }
        public Get_OutPlanWorkOutList GetPlanEndWorkOut(string data)
        {
            Get_OutPlanWorkOutList json = new Get_OutPlanWorkOutList();
            try
            {
                Get_InPlanEndWorkOut input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InPlanEndWorkOut>(data);
                DBPlanToList db = new DBPlanToList();
                List<MDEndWork_Full> vplans = db.GetPlanEndWorkOut(input.siteID, input.trainmanID, 2);
                BeginEndWork bll = new BeginEndWork();
                List<TF.RunSafty.WorkSteps.BeginEndWork.TuiQinPlan> plans = bll.GetPlanList(vplans);
                if (vplans != null && vplans.Count > 0)
                {
                    json.data = (plans.Count>0&&plans[0]!=null)?plans[0]:null;
                    json.result = "0";
                    json.resultStr = "提交成功";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "没有该乘务员的退勤计划";
                }
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

    }
}
