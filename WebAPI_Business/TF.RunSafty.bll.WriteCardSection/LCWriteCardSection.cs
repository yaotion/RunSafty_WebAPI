using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.WriteCardSection
{
    public class LCWriteCardSection
    {
        #region 1.13.1获取计划的所有事件信息(途中详情)
        public class Get_InGetPlanAllSections
        {
            public string TrainPlanGUID;
        }
        public class Get_OutGetPlanAllSections
        {
            public string result = "";
            public string resultStr = "";
            public GetPlanAllSectionsResult data;
        }

        public class GetPlanAllSectionsResult
        {
            public List<WriteCardSection> SectionArray;

        }
        public Get_OutGetPlanAllSections GetPlanAllSections(string data)
        {
            Get_OutGetPlanAllSections json = new Get_OutGetPlanAllSections();
            try
            {
                Get_InGetPlanAllSections input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetPlanAllSections>(data);
                DBWriteCardSection db = new DBWriteCardSection();
                GetPlanAllSectionsResult PE = new GetPlanAllSectionsResult();
                PE.SectionArray = db.GetPlanAllSections(input.TrainPlanGUID);
                json.data = PE;
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

        #region 1.14.2获取计划已经选定的写卡区段
        public class Get_InGetPlanSelectedSections
        {
            public string TrainPlanGUID;
        }
        public class Get_OutGetPlanSelectedSections
        {
            public string result = "";
            public string resultStr = "";
            public GetPlanSelectedSectionsResult data;
        }

        public class GetPlanSelectedSectionsResult
        {
            public List<WriteCardSection> SectionArray;

        }
        public Get_OutGetPlanSelectedSections GetPlanSelectedSections (string data)
        {
            Get_OutGetPlanSelectedSections json = new Get_OutGetPlanSelectedSections();
            try
            {
                Get_InGetPlanSelectedSections input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetPlanSelectedSections>(data);
                DBWriteCardSection db = new DBWriteCardSection();
                GetPlanSelectedSectionsResult RE = new GetPlanSelectedSectionsResult();
                RE.SectionArray = db.GetPlanSelectedSections(input.TrainPlanGUID);
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


        #region 1.14.3指定计划的写卡区段
        public class Get_InSetPlanSections
        {
            public string TrainPlanGUID;
            public List<WriteCardSection> SectionArray;
            public string DutyUserGUID;
            public string DutyUserNumber;
            public string DutyUserName;
        }
        public class Get_OutSetPlanSections
        {
            public string result = "";
            public string resultStr = "";
        }


        public Get_OutSetPlanSections SetPlanSections(string data)
        {
            Get_OutSetPlanSections json = new Get_OutSetPlanSections();
            try
            {
                Get_InSetPlanSections input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InSetPlanSections>(data);
                DBWriteCardSection db = new DBWriteCardSection();
                db.SetPlanSections(input.TrainPlanGUID, input.SectionArray, input.DutyUserGUID, input.DutyUserNumber, input.DutyUserName);
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


    }
}
