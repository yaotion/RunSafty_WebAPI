using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Plan.MD
{
    public class MsgTool
    {
        /// <summary>
        /// c#时间转java时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>    
        public static long DateTimeToMilliseconds(DateTime dt)
        {
            DateTime dt_1970 = new DateTime(1970, 1, 1);
            TimeSpan span = dt - dt_1970;

            // .net开发中计算的都是标准时区的差，但java的解析时间跟时区有关，
            // 而我们的java服务器系统时区不是标准时区，解析时间会差8个小时。
            span -= TimeSpan.FromHours(8);
            return (long)span.TotalMilliseconds;
        }
    }
    public class EndworkMsgData
    {
        //乘务员GUID
        public string tmGuid = "";
        //乘务员工号
        public string tmid = "";
        //乘务员姓名
        public string tmname = "";
        /// <summary>
        /// 出勤时间
        /// </summary>
        public long dttime = 0;
        //计划编号
        public string planGuid = "";
        //交路名称
        public string jiaoLuName = "";
        //交路GUID
        public string jiaoLuGUID = "";
        //测酒结果
        public int cjjg = 0;
        //计划出勤时间
        public long dtStartTime = 0;
        //出勤站点编号
        public int Tmis = 0;
        //车次
        public string strTrainNo = "";

        public ThinkFreely.RunSafty.AttentionMsg ToMsg()
        {
            ThinkFreely.RunSafty.AttentionMsg result = new ThinkFreely.RunSafty.AttentionMsg();
            result.msgID = 0;
            //退勤消息
            result.msgType = 10201;
            result.param = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            result.clientID = 0;
            return result;
        }
    }

    public class CancelPlanData
    {
        public string GUIDS = "";
        public string jiaoLuGUID = "";
        public ThinkFreely.RunSafty.AttentionMsg ToMsg()
        {
            ThinkFreely.RunSafty.AttentionMsg result = new ThinkFreely.RunSafty.AttentionMsg();
            result.msgID = 0;
            //退勤消息
            result.msgType = 10303;
            result.param = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            result.clientID = 0;
            return result;
        }
    }
    public class BeginworkMsgData
    {
        //乘务员GUID
        public string tmGuid = "";
        //乘务员工号
        public string tmid = "";
        //乘务员姓名
        public string tmname = "";
        /// <summary>
        /// 出勤时间
        /// </summary>
        public long dttime = 0;
        //计划编号
        public string planGuid = "";
        //交路名称
        public string jiaoLuName = "";
        //交路GUID
        public string jiaoLuGUID = "";
        //测酒结果
        public int cjjg = 0;
        //计划出勤时间
        public long dtStartTime = 0;
        //出勤站点编号
        public int Tmis = 0;
        //车次
        public string strTrainNo = "";
        //车型
        public string strTrainTypeName = "";
        //车号
        public string strTrainNumber = "";

        public ThinkFreely.RunSafty.AttentionMsg ToMsg()
        {
            ThinkFreely.RunSafty.AttentionMsg result = new ThinkFreely.RunSafty.AttentionMsg();
            result.msgID = 0;
            //退勤消息
            result.msgType = 10101;
            result.param = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            result.clientID = 0;
            return result;
        }
    }
  
}
