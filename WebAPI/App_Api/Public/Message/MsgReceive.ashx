<%@ WebHandler Language="C#" Class="MsgReceive" %>

using System;
using System.Web;
using System.Data;
using TF.Api.Utilities;

public class MsgReceive : IHttpHandler
{
    int msgType = 0;
    string msgID = "0";
    string param = "";
    int clientID = 0;
    int mode = 0;
    int maxCount = 0;
    /// <summary>
    /// 去掉字符串首尾fh
    /// </summary>
    /// <param name="StrIndex"></param>
    /// <returns></returns>
    public static string CutComma(string StrIndex, string fh, int index)
    {
        if (StrIndex.Length > 0)
        {
            StrIndex = StrIndex.Substring(0, index) == fh ? StrIndex.Substring(1) : StrIndex;
            StrIndex = StrIndex.Substring(StrIndex.Length - index) == fh ? StrIndex.Substring(0, StrIndex.Length - index) : StrIndex;
        }
        return StrIndex;
    }
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            if (context.Request["msgType"] != null)
            {
                int.TryParse(context.Request["msgType"].ToString(),out msgType);
            }

            if (context.Request["msgID"] != null)
            {
                msgID = context.Request["msgID"].ToString();
            }
            if (msgID == "")
                msgID = "0";
            if (context.Request["param"] != null)
            {
                param = context.Request["param"].ToString(); ;
            }
            if (context.Request["clientID"] != null)
            {
                int.TryParse(context.Request["clientID"].ToString(), out clientID);
            }
            if (context.Request["mode"] != null)
            {
                int.TryParse(context.Request["mode"].ToString(), out mode);
            }
            if (context.Request["maxCount"] != null)
            {
                int.TryParse(context.Request["maxCount"].ToString(), out maxCount);
            }
            switch (mode)
            {
                case 0:
                    PostAction(context);
                    break;
                case 1:
                    GetAction(context);
                    break;
                case 2:
                    ReplayAction();
                    break;
            }
        }
        catch (Exception e)
        {

        }
    }

    private void PostAction(HttpContext context)
    {
        string str = "result=0,strError=";
        try
        {
            DataTable dt = ThinkFreely.RunSafty.AttentionTypeConfig.GetClient(clientID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strMsgList = dt.Rows[i]["attentionTypeids"].ToString();
                string[] msgArray = strMsgList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int k = 0; k < msgArray.Length; k++)
                {
                    if (msgArray[k] == msgType.ToString())
                    {
                        ThinkFreely.RunSafty.AttentionMsg am = new ThinkFreely.RunSafty.AttentionMsg();
                        am.msgType = msgType;
                        am.param = param;
                        am.clientID = PageBase.static_ext_int(dt.Rows[i]["clientID"]);
                        am.Add();
                        break;
                    }
                }        
                //if (dt.Rows[i]["attentionTypeids"].ToString().Contains(msgType.ToString()))
                //{
                //    ThinkFreely.RunSafty.AttentionMsg am = new ThinkFreely.RunSafty.AttentionMsg();
                //    am.msgType = msgType;
                //    am.param = param;
                //    am.clientID =  PageBase.static_ext_int(dt.Rows[i]["clientID"]);
                //    am.Add();
                //}
            }
        }
        catch(Exception e)
        {
            str = "result=1,strError="+e;
        }
        finally
        {
            context.Response.ContentType = "application/json";
            context.Response.Write(str);
            context.Response.End();
        }
    }

    private void GetAction(HttpContext context)
    {
        try
        {
            DataTable dt = ThinkFreely.RunSafty.AttentionMsg.GetAllAttentionMsg(clientID, maxCount,Convert.ToInt32(msgID));
            string str = "[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str += dt.Rows[i]["param"].ToString();
                if (str != "[")
                {
                    str = CutComma(str, "}", 1) + ",\"msgID\":\"" + dt.Rows[i]["msgID"].ToString() + "\"},";
                }
            }
            str = CutComma(str, ",", 1);
            str += "]";
            context.Response.ContentType = "application/json";
            context.Response.Write(str);
            context.Response.End();
        }
        catch { }
    }

    private void ReplayAction()
    {
        try
        {
            string[] msgIDArray = msgID.Split(new char[] {',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < msgIDArray.Length; i++)
            {
                 ThinkFreely.RunSafty.AttentionMsg.Delete(msgIDArray[i]);
            }
           
        }
        catch { }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}