using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.RunEvent
{
    public class LCRunEvent
    {

        #region 1.13.1获取计划的所有事件信息(途中详情)
        public class Get_InGetPlanRunEvents
        {
            public string TrainPlanGUID;
        }
        public class Get_OutGetPlanRunEvents
        {
            public string result = "";
            public string resultStr = "";
            public RunEventResult data;
        }

        public class RunEventResult
        {
            public List<RunEvent> eventArray;
        
        }
        public Get_OutGetPlanRunEvents GetPlanRunEvents(string data)
        {
            Get_OutGetPlanRunEvents json = new Get_OutGetPlanRunEvents();
            try
            {
                Get_InGetPlanRunEvents input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetPlanRunEvents>(data);
                DBRunEvent db = new DBRunEvent();
                RunEventResult RE = new RunEventResult();
                RE.eventArray = db.GetPlanRunEvents(input.TrainPlanGUID);
                json.data = RE;
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion


        #region 1.13.2	删除事件
        public class Get_InDeleteRunEvent
        {
            public string EventGUID;
        }
        public class Get_OutDeleteRunEvent
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutDeleteRunEvent DeleteRunEvent(string data)
        {
            Get_OutDeleteRunEvent json = new Get_OutDeleteRunEvent();
            try
            {
                Get_InDeleteRunEvent input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InDeleteRunEvent>(data);
                DBRunEvent db = new DBRunEvent();
                int i = db.DeleteRunEvent(input.EventGUID);
                if (i == 0)
                {
                    json.result = "1";
                    json.resultStr = "删除失败！未找到该条记录！";
                   
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "返回成功,共删除" + i + "条数据！";
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


        #region 1.13.3 重新计算事件信息

        public class Get_InReCountRunEvent
        {
            public string TrainPlanGUID;
        }
        public class Get_OutReCountRunEvent
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutReCountRunEvent ReCountRunEvent(string data)
        {
            Get_OutReCountRunEvent json = new Get_OutReCountRunEvent();
            try
            {
                Get_InReCountRunEvent input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InReCountRunEvent>(data);
                DBRunEvent db = new DBRunEvent();
                int i = db.ReCountRunEvent(input.TrainPlanGUID);
                if (i == 0)
                {
                    json.result = "1";
                    json.resultStr = "更新失败！未找到该条记录！";

                }
                else
                {
                    json.result = "0";
                    json.resultStr = "返回成功,共更新" + i + "条数据！";
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


        #region 1.13.4 添加运行事件

        public class Get_InAddRunEvent
        {
            public RunEvent runEvent;
        }
        public class Get_OutAddRunEvent
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutAddRunEvent AddRunEvent(string data)
        {
            Get_OutAddRunEvent json = new Get_OutAddRunEvent();
            try 
            {
                Get_InAddRunEvent input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InAddRunEvent>(data);
                if (input.runEvent.nEventID == 0)
                {
                    json.result = "1";
                    json.resultStr = "更新失败";
                    return json;
                }
                DBRunEvent_Add db = new DBRunEvent_Add();
                int i = db.SubmitRunEvent(input.runEvent);
                if (i == 1)
                {
                    json.result = "1";
                    json.resultStr = "更新失败";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "返回成功";
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

        public class Get_InGetPlanStationRunEvents
        {
            public string TrainPlanGUID;
        }

        public Get_OutGetPlanRunEvents GetPlanStationRunEvents(string data)
        {
            Get_OutGetPlanRunEvents json = new Get_OutGetPlanRunEvents();
            try
            {
                Get_InGetPlanStationRunEvents input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetPlanStationRunEvents>(data);
                DBRunEvent db = new DBRunEvent();
                RunEventResult RE = new RunEventResult();
                RE.eventArray = db.GetPlanStationRunEvents(input.TrainPlanGUID);
                json.data = RE;
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }



        #region 1.13.5	执行计算劳时
        public class Get_InComputeWorkTime
        {
            public string strTrainPlanGUID;
        }
        public class Get_OutComputeWorkTime
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutComputeWorkTime ComputeWorkTime(string data)
        {
            Get_OutComputeWorkTime json = new Get_OutComputeWorkTime();
            try
            {
                Get_InComputeWorkTime input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InComputeWorkTime>(data);
                DBRunEvent db = new DBRunEvent();
                int i = db.ComputeWorkTime(input.strTrainPlanGUID);
                if (i != 0)
                {
                    json.result = "0";
                    json.resultStr = "执行成功！";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "执行失败！";
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
