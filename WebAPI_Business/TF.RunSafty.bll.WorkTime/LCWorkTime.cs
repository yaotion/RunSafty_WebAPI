using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.WorkTime
{    
    public class Trainman
    {
        public string strTrainmanGUID;
        public string strTrainmanName;
        public string strTrainmanNumber;
    }


    public class TrainPlan
    {
        public string strTrainJiaoluName;
        public string strTrainPlanGUID;
        public string strTrainTypeName;
        public string strTrainNumber;
        public string strTrainNo;
        public int nKeHuoID;
    }


    public class Group
    {
        public Trainman Trainman1 = new Trainman();
        public Trainman Trainman2 = new Trainman();
        public Trainman Trainman3 = new Trainman();       
    
    }
    public class TrainmanPlan 
    {
        public TrainPlan TrainPlan = new TrainPlan();
        public Group Group = new Group();
    
    }
    public class WorkTimeEntity
    {    
        public int nid;
        //流程ID
        public string strFlowID;
        //所属车间
        public string strWorkShopGUID;
        //司机工号
        public string strTrainmanNumber;
        //司机姓名
        public string strTrainmanName;
        //车间名称
        public string strWorkShopName;
        //出勤时间
        public DateTime? dtBeginWorkTime;
        //开车时间
        public DateTime dtStartTime;
        //到达时间
        public DateTime dtArriveTime;
        //入寓时间
        public DateTime dtInRoomTime;
        //离寓时间
        public DateTime dtOutRoomTime;
        //回路开车时间
        public DateTime dtStartTime2;
        //回路到达时间
        public DateTime dtArriveTime2;
        //退勤时间
        public DateTime dtEndWorkTime;
        //出勤辅时
        public int fBeginTotalTime;
        //旅时
        public int fRunTotalTime;
        //退勤辅时
        public int fEndTotalTime;
        //总劳时
        public int fTotalTime;
        //
        public int nFlowState;
        //
        public int nkehuoID;
        //
        public int nNoticeState;
        //车型
        public string strTrainTypeName;
        //车号
        public string strTrainNumber;
        //车次
        public string strTrainNo;
        //车间号
        public string strWorkShopNumber;
        //终到站TMIS
        public string strDestStationTMIS;
        //终到站名称
        public string strDestStationName;
        //是否本段出库
        public int bLocalOutDepots;
        //本段出库时间
        public DateTime dtLocalOutDepotsTime;
        //是否外段入库
        public int bDestInDepots;
        //外段入库时间
        public DateTime dtDestInDepotsTime;
        //是否外段出库
        public int bDestOutDepots;
        //外段出库时间
        public DateTime dtDestOutDepotsTime;
        //是否本段入库
        public int bLocalInDepots;
        //本段入库时间
        public DateTime dtLocalInDepotsTime;
        //回路车型
        public string strBackTrainTypeName;
        //回路车号
        public string strBackTrainNumber;
        //回路车次
        public string strBackTrainNo;
        //实际到达时间
        public DateTime dtRealArriveTime;
        //是否已经手工确认
        public int bConfirm;
        //手工确认时间
        public DateTime dtConfirmTime;
        //手工确认人
        public string strConfirmDutyUser;
        //往路停留时间(分钟)
        public int nLocalStopMinutes;
        //回路停留时间(分钟)
        public int nRemoteStopMinutes;
        //退勤终到车站TMIS
        public string strArriveStationTMIS;
        //退勤终到车站名
        public string strArriveStationName;
        //
        public int nAlarmMinutes;
        //
        public int nOutMinutes;
        //
        public int nOutTotalTime;
        //
        public DateTime dtFileBeginTime;
        //
        public DateTime dtFileEndTime;
        //
        public int nChuQinTypeID;
        //
        public int nTuiQinTypeID;
        //修改意见
        public string strConfirmRemark;

        public string strDutyUserName;

        public DateTime dtCreateTime;
        //旅行时间
        public int nGoRunTotalMinutes;
        public int nBackRunTotalMinutes;
        //技术速度
        public double fGoSpeed;
        public double fBackSpeed;

            //值乘计划信息
        public TrainmanPlan TrainmanPlan = new TrainmanPlan();
    }
    public class LCWorkTime
    {
        public class InputParam
        {
            public string TrainPlanGUID;
        }

        public class OutputParam
        {
            public string result = "";
            public string resultStr = "";
            public object data;
        }


        public OutputParam CalcWorkTime(string data)
        {

            InputParam inputParam = Newtonsoft.Json.JsonConvert.DeserializeObject<InputParam>(data);
            OutputParam result = new OutputParam();           
            try
            {               
                DBWorkTime dbWorkTime = new DBWorkTime();
                dbWorkTime.CalcWorkTime(inputParam.TrainPlanGUID);
            }
            catch (Exception ex)
            {
                result.result = "1";
                result.resultStr = ex.Message.ToString();
            }

            return result;
        }

        public class OutputWorkTime
        {
            public Boolean bExist= false;
            public WorkTimeEntity workTime;
        }
        
        public OutputParam GetWorkTime(string data)
        {

            InputParam inputParam = Newtonsoft.Json.JsonConvert.DeserializeObject<InputParam>(data);
            OutputParam result = new OutputParam();
            OutputWorkTime outputWorkTime = new OutputWorkTime();
            try
            {
                result.data = outputWorkTime;

                DBWorkTime dbWorkTime = new DBWorkTime();                
                outputWorkTime.workTime = dbWorkTime.GetWorkTime(inputParam.TrainPlanGUID);
                outputWorkTime.bExist = outputWorkTime.workTime != null;


                
            }
            catch (Exception ex)
            {
                result.result = "1";
                result.resultStr = ex.Message.ToString();
            }
     
            return result;
            
        }
    }
}
