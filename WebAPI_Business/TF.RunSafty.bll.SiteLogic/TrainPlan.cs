using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.SiteLogic
{
    public class DataTypeConvert
    {
        public static DateTime? ToDateTime(object obj)
        {   
            if (DBNull.Value.Equals(obj) || obj == null)
                return null;
            else
                return Convert.ToDateTime(obj);

        }
    
    }
    public class TrainPlan
    {  
        //计划出勤时间
        public DateTime? startTime;

        //计划编号
        public string planID;

        //创建时间
        public DateTime? createTime;
   
        //最后到达时间
        public DateTime? lastArriveTime;

    }


    public class TrainmanPlan
    {
        //机车计划
        public TrainPlan trainPlan = new TrainPlan();

        //乘务员GUID1 -- GUID4
        public string tmGUID1 = string.Empty;
        public string tmGUID2 = string.Empty;
        public string tmGUID3 = string.Empty;
        public string tmGUID4 = string.Empty;

        //乘务员工号1 -- 4
        public string tmid1 = string.Empty;
        public string tmid2 = string.Empty;
        public string tmid3 = string.Empty;
        public string tmid4 = string.Empty;

        //乘务员姓名1 -- 4
        public string tmname1 = string.Empty;
        public string tmname2 = string.Empty;
        public string tmname3 = string.Empty;
        public string tmname4 = string.Empty;
    }

    //人员信息
    public class Trainman
    {
        //人员GUID
        public string tmGUID = string.Empty;
        //人员工号
        public string tmid = string.Empty;
        //人员姓名
        public string tmname = string.Empty;
        //车间ID
        public string workShopID = string.Empty;
        //车间名称
        public string workShopName = string.Empty;

    }

}
