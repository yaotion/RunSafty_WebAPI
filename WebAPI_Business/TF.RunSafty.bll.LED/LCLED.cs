using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 
namespace TF.RunSafty.LED
{
    public class LCLED
    {
        #region 获取班序信息
        public class Get_In
        {
            public string TrainmanJiaoluGUID = "";
            public string WorkShopGUID = "";
        }
        public class Get_Out
        {
            public string result = "";
            public string resultStr = "";
            public List<GroupItem> Content;
        }
        public Get_Out GetGroupOrder(string data)
        {
            Get_Out json = new Get_Out();
            try
            {
                Get_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_In>(data);
                string strTrainmanJiaoLuGUID = input.TrainmanJiaoluGUID;
                string strWorkShopGUID = input.WorkShopGUID;
                TF.Api.BLL.BanXuBLL bllBanXu = new TF.Api.BLL.BanXuBLL();
                json.Content = new List<GroupItem>();
                DataTable table = bllBanXu.GetBanXuByJiaoLuGUID(strWorkShopGUID, strTrainmanJiaoLuGUID);
                SimpleTrainman man = null;
                if (table != null)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table.Rows[i];
                        GroupItem record = new GroupItem();
                        DateTime endWorkTime;
                        string strEndWorkTime = row["dtLastEndWorkTime1"] == null ? "" : row["dtLastEndWorkTime1"].ToString();
                        if (DateTime.TryParse(strEndWorkTime, out endWorkTime))
                        {
                            record.EndWorkTime = endWorkTime.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            record.EndWorkTime = "";
                        }
                        record.Index = (i + 1).ToString();
                        record.TrainmanList = new SimpleTrainmanList();
                        json.Content.Add(record);
                        for (int j = 1; j < 5; j++)
                        {
                            man = new SimpleTrainman();
                            man.TrainmanName = row["strTrainmanName" + j.ToString()].ToString();
                            man.TrainmanNo = row["strTrainmanNumber" + j.ToString()].ToString();
                            man.trianmanGUID = row["strTrainmanGUID" + j.ToString()].ToString();
                            record.TrainmanList.Add(man);
                        }
                    }
                }
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败："+ex.Message;
            }
            return json;
        }
        #endregion

        #region 获取考勤信息
        public class GetKaoQinInfo_In
        {
            public string cid = "";
            public string WorkShopGUID = "";
        }
        public class GetKaoQinInfo_Out
        {
            public string result = "";
            public string resultStr = "";
            public SimpleTrainmanList Content;
        }
        public GetKaoQinInfo_Out GetKaoQinInfo(string data)
        {
            GetKaoQinInfo_Out json = new GetKaoQinInfo_Out();
            GetKaoQinInfo_In input = null;
            try
            {
                input = Newtonsoft.Json.JsonConvert.DeserializeObject<GetKaoQinInfo_In>(data);

                string strWorkShopGUID = input.WorkShopGUID;
                string cid = input.cid;
                json.result = "0";
                json.resultStr = "返回成功";
                TF.Api.BLL.KaoQinBLL bllKaoQin = new TF.Api.BLL.KaoQinBLL();
                DataTable table = bllKaoQin.GetKaoQinByWorkShopGUID(strWorkShopGUID);
                json.Content = GetListFromTable(table);
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败："+ex.Message;
            }
            return json;
        }
        public SimpleTrainmanList GetListFromTable(DataTable table)
        {
            SimpleTrainmanList list = new SimpleTrainmanList();
            SimpleTrainman man = null;
            if (list != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    man = new SimpleTrainman();
                    man.DutyName = row["DutyName"].ToString();
                    DateTime.TryParse(row["leaveEndTime"].ToString(), out man.leaveEndTime);
                    DateTime.TryParse(row["LeaveStartTime"].ToString(), out man.LeaveStartTime);
                    man.LeaveTypeName = row["LeaveTypeName"].ToString();
                    man.TrainmanJiaoLuGUID = row["TrainmanJiaoLuGUID"].ToString();
                    man.TrainmanName = row["TrainmanName"].ToString();
                    man.TrainmanNo = row["TrainmanNo"].ToString();
                    man.trianmanGUID = row["trianmanGUID"].ToString();
                    list.Add(man);
                }
            }
            return list;
        }
        #endregion

        #region 获取LED文件

        public class LedFile_In
        {
            public string cid = "";
        }
        public class LedFile_Out
        {
            public string result = "";
            public string resultStr = "";
            public List<TF.RunSafty.Model.VIEW_LED_Files_Clients> FileList;
        }
        public LedFile_Out GetLedFile(string data)
        {
            LedFile_Out model = new LedFile_Out();
            LedFile_In param = Newtonsoft.Json.JsonConvert.DeserializeObject<LedFile_In>(data);
            TF.RunSafty.BLL.VIEW_LED_Files_Clients bllLed=new TF.RunSafty.BLL.VIEW_LED_Files_Clients();
            try
            {
                model.FileList = bllLed.GetLedFilesOfClient(param.cid);
                model.result = "0";
                model.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                model.resultStr = "";
                model.result = "1";
            }
            return model;
        }
        #endregion
    }
}
