using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.JiaoBan
{
    public class LCJiaoBan
    {

        #region 1.10.1取消叫班通知
        public class Get_InCancelNotify
        {
            public string strGUID;
            public string strUser;
            public DateTime dtCancelTime;
            public string strReason;

        }
        public class Get_OutCancelNotify
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutCancelNotify CancelNotify(string data)
        {
            Get_InCancelNotify model = null;
            Get_OutCancelNotify json = new Get_OutCancelNotify();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InCancelNotify>(data);
                DBJiaoBan db = new DBJiaoBan();
                if (db.CancelNotify(model.strGUID, model.strUser, model.dtCancelTime, model.strReason))
                {
                    json.result = "0";
                    json.resultStr = "返回成功，成功更新一条数据！";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "返回失败，请查找原因！";
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

        #region 1.10.2 添加叫班通知
        public class Get_InAddNotify
        {
            public CallNotify callWork;
        }
        public class Get_OutAddNotify
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutAddNotify AddNotify(string data)
        {
            Get_InAddNotify model = null;
            Get_OutAddNotify json = new Get_OutAddNotify();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InAddNotify>(data);
                DBJiaoBan db = new DBJiaoBan();
                if (db.AddNotify(model.callWork))
                {
                    json.result = "0";
                    json.resultStr = "返回成功，成功更新一条数据！";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "返回失败，请查找原因！";
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

        #region 1.10.3查询未取消的叫班通知
        public class Get_InFindUnCancel
        {
            public string strTrainmanGUID;
            public string strTrainPlanGUID;

        }
        public class Get_OutFindUnCancel
        {
            public string result = "";
            public string resultStr = "";
            public FindUnCancelResult data;
        }

        public class FindUnCancelResult
        {
            public bool result;
            public CallNotify callWork;


        }
        public Get_OutFindUnCancel FindUnCancel(string data)
        {
            Get_InFindUnCancel model = null;
            Get_OutFindUnCancel json = new Get_OutFindUnCancel();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InFindUnCancel>(data);
                DBJiaoBan db = new DBJiaoBan();
                FindUnCancelResult fr = new FindUnCancelResult();
                bool Boolresult = false;
                fr.callWork = db.FindUnCancelforNone(model.strTrainmanGUID, model.strTrainPlanGUID, ref Boolresult);
                fr.result = Boolresult;
                json.data = fr;
                json.result = "0";
                json.resultStr = "返回成功！";

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 1.10.3查询未取消的叫班通知
        public class Get_InGetByStateSec
        {
            public int startState;
            public int endState;
            public DateTime dtStartSendTime;
            public bool NotCancel;
        }
        public class Get_OutGetByStateSec
        {
            public string result = "";
            public string resultStr = "";
            public GetByStateSecResult data;
        }

        public class GetByStateSecResult
        {
            public List<CallNotify> callWorkAry;
        }
        public Get_OutGetByStateSec GetByStateRange(string data)
        {
            Get_InGetByStateSec model = null;
            Get_OutGetByStateSec json = new Get_OutGetByStateSec();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetByStateSec>(data);
                DBJiaoBan db = new DBJiaoBan();
                GetByStateSecResult fr = new GetByStateSecResult();
                fr.callWorkAry = db.GetByStateSec(model.startState, model.endState, model.dtStartSendTime, model.NotCancel);
                json.data = fr;
                json.result = "0";
                json.resultStr = "返回成功！";

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

