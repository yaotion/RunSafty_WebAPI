using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.NamePlate.MD;
using System.Web.Script.Serialization;

namespace TF.RunSafty.NamePlate
{
    public class IFNameplate
    {
        private class InGetUserLimit
        {
            //所属车间GUID
            public string UserNumber;
        }
        public class OutGetUserLimit
        {
            //预备人员列表
            public int SetNameboard;
        }
        public InterfaceOutPut GetUserLimit(string Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetUserLimit InParams = javaScriptSerializer.Deserialize<InGetUserLimit>(Data);
                OutGetUserLimit userlimitOutput = new OutGetUserLimit();
                userlimitOutput.SetNameboard = 0;
                NameplateLimit limit = new NameplateLimit();
                if (LCNameplateLimit.GetUserLimit(InParams.UserNumber,limit))
                {
                    userlimitOutput.SetNameboard = limit.SetNameboard;
                }
                output.data = userlimitOutput;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                TF.CommonUtility.LogClass.log("Interface.GetPrepareTrainmans:" + ex.Message);
                throw ex;
            }
            return output;
        }


        private class InTurnTogetherTrainGroup
        {
            public string TrainGUID = "";
        }
        public InterfaceOutPut TurnTogetherTrainGroup(string Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InTurnTogetherTrainGroup InParams = javaScriptSerializer.Deserialize<InTurnTogetherTrainGroup>(Data);

                TF.RunSafty.NamePlate.DB.DBNameBoard.TurnTogetherTrainGroup(InParams.TrainGUID);                
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                TF.CommonUtility.LogClass.log("Interface.TurnTogetherTrainGroup:" + ex.Message);
                throw ex;
            }
            return output;
        }
        
    }
}
