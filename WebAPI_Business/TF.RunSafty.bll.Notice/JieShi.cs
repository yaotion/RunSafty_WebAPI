using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Notice
{
    public class JieShi
    {
        #region 增加揭示打印记录
        public  class Add_In
        {
            public string PlanGUID;
            public string TrainmanGUID;
            public string PrintTime;
            public string cid;
        }
        
        public  class Add_Out
        {
            public string result;
            public string returnStr; 
        }
        public Add_Out AddtPrintRecord(string data)
        {
            Add_Out model = new Add_Out();
            Add_In param = Newtonsoft.Json.JsonConvert.DeserializeObject<Add_In>(data);
            TF.RunSafty.BLL.Tab_DeliverJSPrint bllPrint = new TF.RunSafty.BLL.Tab_DeliverJSPrint();
            TF.RunSafty.Model.Tab_DeliverJSPrint modelPlan = new TF.RunSafty.Model.Tab_DeliverJSPrint();
            try
            {
                modelPlan.StrPlanGUID = param.PlanGUID;
                modelPlan.StrSiteGUID = param.cid;
                modelPlan.StrTrainmanGUID = param.TrainmanGUID;
                DateTime dtPrint;
                if (DateTime.TryParse(param.PrintTime, out dtPrint))
                {
                    modelPlan.dtPrintTime = dtPrint;
                }
                if (bllPrint.Add(modelPlan) > 0)
                {
                    model.result = "0";
                    model.returnStr = "提交成功";
                }
                else
                {
                    model.result = "1";
                    model.returnStr = "提交失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                model.result = "1";
                model.returnStr = "提交失败";
            }
            return model;
        }
        #endregion
        #region 揭示打印判断
        public class Check_In
        {
            public string cid;
            public string strPlanGUID;
            public string strTrainmanGUID;
            public string dtBeginWorkTime;
        }

        public  class Check_Out
        {
            public string result;
            public string returnStr;
            public string bPrint;
        }


        public Check_Out PrintCheck(string data)
        {
            Check_Out model = new Check_Out();
            Check_In param = Newtonsoft.Json.JsonConvert.DeserializeObject<Check_In>(data);
            TF.RunSafty.BLL.Tab_DeliverJSPrint bllPrint = new TF.RunSafty.BLL.Tab_DeliverJSPrint();
            TF.RunSafty.Model.Tab_DeliverJSPrint modelPlan = new TF.RunSafty.Model.Tab_DeliverJSPrint();
            try
            {
                int bPrint = bllPrint.IsJsPrintable(param.strPlanGUID, param.strTrainmanGUID, param.dtBeginWorkTime);
                modelPlan.StrPlanGUID = param.strPlanGUID;
                modelPlan.StrSiteGUID = param.cid;
                modelPlan.StrTrainmanGUID = param.strTrainmanGUID;
                model.result = "0";
                model.returnStr = "返回成功";
                model.bPrint = bPrint.ToString();

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                model.result = "1";
                model.returnStr = "提交失败" + ex.Message;
            } 
            return model;
        }
        #endregion
    }
}
