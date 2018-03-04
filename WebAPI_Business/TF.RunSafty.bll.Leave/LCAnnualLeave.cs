using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.Leave.MD;
using TF.CommonUtility;
namespace TF.RunSafty.Leave
{
    class LCAnnualLeave
    {
        private InterfaceOutPut output = new InterfaceOutPut();
        private void ClearOutput()
        {
            output.result = 0;
            output.resultStr = string.Empty;
            output.data = null;
        }

        public InterfaceOutPut Add(string data)
        {
            ClearOutput();
            AnnualLeaveEntity entity = Newtonsoft.Json.JsonConvert.DeserializeObject<AnnualLeaveEntity>(data);

            new DBAnnualLeave().Add(entity);
            return output;
        
        }

        public InterfaceOutPut Del(string data)
        {
            ClearOutput();
            CommonJsonModel model = new CommonJsonModel(data);
            string ID = model.GetValue("ID");
            string log = model.GetValue("log");
            if (string.IsNullOrEmpty(ID))
            {
                output.result = 1;
                output.resultStr = "传入ID为空，无法删除记录!";
                return output;
            }

            try
            {
                new DBAnnualLeave().Del(ID,log);
            }
            catch(Exception ex)
            {
                output.result = 1;
                output.resultStr = ex.Message.ToString();
            }
            
            return output;
        }

        public InterfaceOutPut Get(string data)
        {
            ClearOutput();
            try
            {
                AnnualQC QcEntity = Newtonsoft.Json.JsonConvert.DeserializeObject<AnnualQC>(data);
                output.data = new DBAnnualLeave().Get(QcEntity);
                
            }
            catch (Exception ex)
            {
                output.result = 1;
                output.resultStr = ex.Message.ToString();
            }
            
            return output;
        }

        public InterfaceOutPut BatchAdd(string data)
        {
            ClearOutput();
            try
            {
                List<AnnualLeaveEntity> Entity = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AnnualLeaveEntity>>(data);
                new DBAnnualLeave().BatchAdd(Entity);
            }
            catch (Exception ex)
            {
                output.result = 1;
                output.resultStr = ex.Message.ToString();
            }

            return output;
        }
    }
}
