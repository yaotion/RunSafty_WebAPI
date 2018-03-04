using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.Runsafty.TestWeb
{
    public class LCTestWeb
    {
        #region 获取所有车站信息
        public class Get_In
        {
        }
        public class Get_Out
        {
            public string result = "";
            public string resultStr = "";
            public List<Station> data;
        }
        public Get_Out GetStations(string data)
        {
            Get_Out json = new Get_Out();
            try
            {
                Get_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_In>(data);
                DBTestWeb db = new DBTestWeb();
                json.data = db.GetStationList();
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
