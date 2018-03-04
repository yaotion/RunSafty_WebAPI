<%@ WebHandler Language="C#" Class="MsgReceive" %>

using System;
using System.Web;
using System.Data;

public class MsgReceive : IHttpHandler
{
    int msgType = 0;
    string msgID = "0";
    string param = "";
    int clientID = 0;
    int mode = 0;
    int maxCount = 0;
    
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            
            msgType = PageBase.static_ext_int(context.Request["msgType"]);
            if (context.Request["msgID"] != null)
            {
                msgID = context.Request["msgID"].ToString();
            }
            if (msgID == "")
                msgID = "0";
            param = PageBase.static_ext_string(context.Request["param"]);
            clientID = PageBase.static_ext_int(context.Request["clientID"]);
            mode = PageBase.static_ext_int(context.Request["mode"]);
            maxCount = PageBase.static_ext_int(context.Request["maxCount"]);
            //TF.CommonUtility.LogClass.log("msgID:" + msgID);
            //TF.CommonUtility.LogClass.log("mode:" + mode);
            //TF.CommonUtility.LogClass.log("IP:" + context.Request.UserHostAddress);
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
            }
        }
        catch (Exception e)
        {
            str = "result=1,strError=" + e;
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
                    str = PageBase.CutComma(str, "}", 1) + ",\"msgID\":\"" + dt.Rows[i]["msgID"].ToString() + "\"},";
                }
            }
            str = PageBase.CutComma(str, ",", 1);
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
            foreach (string s in msgIDArray)
            {
                if (string.IsNullOrEmpty(s) || s.Equals("0"))
                    continue;
                ThinkFreely.RunSafty.AttentionMsg.Delete(s);
            }
             
           
        }
        catch ( Exception ex)
        {
            TF.CommonUtility.LogClass.logex(ex,"");
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}