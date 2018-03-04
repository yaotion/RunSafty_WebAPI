using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Synchronization
{

    public class ISynchronization
    {

        #region 获取所有客户端
        public class Get_In
        {
            public string DBName;
        }

        public class Get_OutError
        {
            public string result = "";
            public string resultStr = "";
        }

        public class Get_OutBase_Site
        {
            public string result = "";
            public string resultStr = "";
            public List<Base_Site> data;
        }

        public class Get_OutBase_Site_Limit
        {
            public string result = "";
            public string resultStr = "";
            public List<Base_Site_Limit> data;
        }

        public class Get_OutBase_Station
        {
            public string result = "";
            public string resultStr = "";
            public List<Base_Station> data;
        }

        public class Get_OutBase_TrainJiaolu
        {
            public string result = "";
            public string resultStr = "";
            public List<Base_TrainJiaolu> data;
        }
        public class Get_OutBase_TrainJiaoluInSite
        {
            public string result = "";
            public string resultStr = "";
            public List<Base_TrainJiaoluInSite> data;
        }

        public class Get_OutBase_ZFQJ
        {
            public string result = "";
            public string resultStr = "";
            public List<Base_ZFQJ> data;
        }
        public class Get_OutOrg_Area
        {
            public string result = "";
            public string resultStr = "";
            public List<Org_Area> data;
        }

        public class Get_OutOrg_DutyUser
        {
            public string result = "";
            public string resultStr = "";
            public List<Org_DutyUser> data;
        }

        public class Get_OutOrg_WorkShop
        {
            public string result = "";
            public string resultStr = "";
            public List<Org_WorkShop> data;
        }

        public class Get_OutBase_TrainNo
        {
            public string result = "";
            public string resultStr = "";
            public List<Base_TrainNo> data;
        }



        public object GetSynchronization(string data)
        {
            try
            {
                Get_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_In>(data);
                if (input.DBName == "TABBaseSite")
                {
                    Get_OutBase_Site json = new Get_OutBase_Site();
                    DBGetBase_Site db = new DBGetBase_Site();
                    json.data = db.GetBase_SiteList();
                    json.result = "0";
                    json.resultStr = "返回成功";
                    return json;

                }
                else if (input.DBName == "TABBaseSiteLimit")
                {
                    Get_OutBase_Site_Limit json = new Get_OutBase_Site_Limit();
                    DBGetBase_Site_Limit db = new DBGetBase_Site_Limit();
                    json.data = db.GetBase_Site_LimitList();
                    json.result = "0";
                    json.resultStr = "返回成功";
                    return json;

                }
                else if (input.DBName == "TABBaseStation")
                {
                    Get_OutBase_Station json = new Get_OutBase_Station();
                    DBGetBase_Station db = new DBGetBase_Station();
                    json.data = db.GetBase_StationList();
                    json.result = "0";
                    json.resultStr = "返回成功";
                    return json;
                }

                else if (input.DBName == "TABBaseTrainJiaolu")
                {
                    Get_OutBase_TrainJiaolu json = new Get_OutBase_TrainJiaolu();
                    DBGetBase_TrainJiaolu db = new DBGetBase_TrainJiaolu();
                    json.data = db.GetTrainJiaoLuList();
                    json.result = "0";
                    json.resultStr = "返回成功";

                    return json;
                }

                else if (input.DBName == "TABBaseTrainJiaoluInSite")
                {
                    Get_OutBase_TrainJiaoluInSite json = new Get_OutBase_TrainJiaoluInSite();
                    DBGetBase_TrainJiaoluInSite db = new DBGetBase_TrainJiaoluInSite();
                    json.data = db.GetTrainJiaoluInSite();
                    json.result = "0";
                    json.resultStr = "返回成功";
                    return json;
                }

                else if (input.DBName == "TABBaseZFQJ")
                {
                    Get_OutBase_ZFQJ json = new Get_OutBase_ZFQJ();
                    DBGetBase_ZFQJ db = new DBGetBase_ZFQJ();
                    json.data = db.GetZFQJ();
                    json.result = "0";
                    json.resultStr = "返回成功";
                    return json;
                }

                else if (input.DBName == "TABOrgArea")
                {
                    Get_OutOrg_Area json = new Get_OutOrg_Area();
                    DBGetOrg_Area db = new DBGetOrg_Area();
                    json.data = db.GetOrg_Area();
                    json.result = "0";
                    json.resultStr = "返回成功";
                    return json;
                }
                else if (input.DBName == "TABOrgDutyUser")
                {
                    Get_OutOrg_DutyUser json = new Get_OutOrg_DutyUser();
                    DBGetOrg_DutyUser db = new DBGetOrg_DutyUser();
                    json.data = db.GetOrg_DutyUser();
                    json.result = "0";
                    json.resultStr = "返回成功";
                    return json;
                }

                else if (input.DBName == "TABOrgWorkShop")
                {
                    Get_OutOrg_WorkShop json = new Get_OutOrg_WorkShop();
                    DBGetOrg_WorkShop db = new DBGetOrg_WorkShop();
                    json.data = db.GetOrg_WorkShop();
                    json.result = "0";
                    json.resultStr = "返回成功";
                    return json;
                }
                else if (input.DBName == "TABBaseTrainNo")
                {
                    Get_OutBase_TrainNo json = new Get_OutBase_TrainNo();
                    DBGetBase_TrainNo db = new DBGetBase_TrainNo();
                    json.data = db.GetBase_TrainNo();
                    json.result = "0";
                    json.resultStr = "返回成功";
                    return json;
                }
                else
                {
                    Get_OutError json = new Get_OutError();
                    json.result = "1";
                    json.resultStr = "未找到当前接口,从新输入key值";
                    return json;
                }
            }
            catch (Exception ex)
            {
                Get_OutError json = new Get_OutError();
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
                return Newtonsoft.Json.JsonConvert.SerializeObject(json);
            }
        }
        #endregion
    }

}
