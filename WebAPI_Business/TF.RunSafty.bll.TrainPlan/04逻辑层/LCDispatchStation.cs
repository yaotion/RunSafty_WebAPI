using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.Runsafty.Plan;

namespace TF.RunSafty.Plan
{
    public class LCDispatchStation
    {


        #region 添加常用意见

        public class Get_Out_AddDispatchStation
        {
            public string result = "";
            public string resultStr = "";
        }

        public class Get_In_AddDispatchStation
        {
            public string Key;
            public string Action;
            public string KeyUrl;
        }

        public Get_Out_AddDispatchStation AddDispatchStation(string data)
        {

            Get_In_AddDispatchStation model = null;
            Get_Out_AddDispatchStation json = new Get_Out_AddDispatchStation();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_In_AddDispatchStation>(data);
                DBDispatchStation db = new DBDispatchStation();
                if (db.AddDispatchStation(model))
                {
                    json.result = "0";
                    json.resultStr = "返回成功，成功插入一条数据！";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "未找该计划的相关信息！";
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
