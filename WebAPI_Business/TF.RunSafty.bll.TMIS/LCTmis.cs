using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.TMIS
{
    public class LCTmis
    {
        #region 查询阶段计划
        public class Get_InJDPlan
        {            
	        public string JlID;
            public DateTime BTime;
            public DateTime ETime;
            public string SiteNumber;


        }
        public class Get_OutPeriodicPlanList
        {
            public string result = "";
            public string resultStr = "";
            public List<MDJDPlan> data;
        }
        public Get_OutPeriodicPlanList GetJDPlanList(string data)
        {
            Get_OutPeriodicPlanList json = new Get_OutPeriodicPlanList();
            try
            {
                Get_InJDPlan input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InJDPlan>(data);
                DBJDPlan db = new DBJDPlan();
                json.data = db.GetJDPlan(input);
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

        #region 生成阶段计划
        public class get_inProduceJDPlan
        {
            public string TrainID;
            public string SectionID;
            public string SiteNumber;
            public string SiteName;
            public string JiaoluGUID;
            public string SiteGUID;
            public string UserGUID;
        }


        public class Get_OutProduceJDPlan
        {
            public string result = "";
            public string resultStr = "";
        }


        public Get_OutProduceJDPlan ProduceJDPlan(string data)
        {
            Get_OutProduceJDPlan json = new Get_OutProduceJDPlan();
            try
            {
                get_inProduceJDPlan model = Newtonsoft.Json.JsonConvert.DeserializeObject<get_inProduceJDPlan>(data);
                DBJDPlan db = new DBJDPlan();
                if (db.ProduceJDPlan(model.SectionID, model.SiteName, model.SiteNumber, model.TrainID, model.JiaoluGUID, model.SiteGUID, model.UserGUID))
                {
                    json.result = "0";
                    json.resultStr = "生成成功！";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "更新0条数据！";
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

        #region 获取区段列表
        public class Get_InGetQD
        {
            public string SiteNumber;
        }
        public class Get_OutGetQD
        {
            public string result = "";
            public string resultStr = "";
            public List<MDQuDuan> data;
        }
        public Get_OutGetQD GetQDList(string data)
        {
            Get_OutGetQD json = new Get_OutGetQD();
            try
            {
                Get_InGetQD input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetQD>(data);
                DBJDPlan db = new DBJDPlan();
                json.data = db.GetQDList();
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

        #region 确认计划计划

        public class Get_InConfirmPlan
        {
            public string TrainID;
        }
        public class Get_OutConfirmPlan
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutConfirmPlan ConfirmPlan(string data)
        {

            Get_OutConfirmPlan json = new Get_OutConfirmPlan();
            try
            {
                Get_InConfirmPlan model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InConfirmPlan>(data);
                DBJDPlan db = new DBJDPlan();
                if (db.ConfirmPlan(model.TrainID))
                {
                    json.result = "0";
                    json.resultStr = "确定成功！";
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

        #region 删除阶段计划

        public class Get_InDeleteJDPlan
        {
            public string strTrainPlanGUID;
        }
        public class Get_OutDeleteJDPlan
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutDeleteJDPlan DeleteJDPlan(string data)
        {

            Get_OutDeleteJDPlan json = new Get_OutDeleteJDPlan();
            try
            {
                Get_InDeleteJDPlan model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InDeleteJDPlan>(data);
                DBJDPlan db = new DBJDPlan();
                if (db.DeleteJDPlan(model.strTrainPlanGUID))
                {
                    json.result = "0";
                    json.resultStr = "删除成功！";
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


        #region 获取客户端和section的对应关系

        public class Get_OutSection
        {
            public string result = "";
            public string resultStr = "";
            public List<MDClientAndSection> data;
        }

        public class Get_InSection
        {
            public string strClientID;
        }


        public Get_OutSection GetSectionByClient(string data)
        {
            Get_InSection model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InSection>(data);
            Get_OutSection json = new Get_OutSection();
            try
            {
                DBTmisStation db = new DBTmisStation();
                json.data = db.getList(model.strClientID);
                json.result = "0";
                json.resultStr = "获取成功！";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "获取失败：" + ex.Message;
            }
            return json;
        }



        #endregion

    }
}
