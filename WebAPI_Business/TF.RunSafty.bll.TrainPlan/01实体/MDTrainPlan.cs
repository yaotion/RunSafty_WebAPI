using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Plan.MD
{
    /// <summary>
    ///类名: TrainPlanMin
    ///说明: 行车计划简单形式
    /// </summary>
    public class TrainPlanMin
    {
        public TrainPlanMin()
        { }

        /// <summary>
        /// 行车计划GUID
        /// </summary>
        public string strTrainPlanGUID;

        /// <summary>
        /// 车次
        /// </summary>
        public string strTrainNo;

        /// <summary>
        /// 计划出勤时间
        /// </summary>
        public DateTime dtChuQinTime;

        /// <summary>
        /// 区段GUID
        /// </summary>
        public string strTrainJiaoluGUID;

        /// <summary>
        /// 行车区段名称
        /// </summary>
        public string strTrainJiaoluName;

        /// <summary>
        /// 计划状态
        /// </summary>
        public int nPlanState;

    }
    
    /// <summary>
  ///类名: Rest
  ///说明: 寓休信息
  /// </summary>
    public class Rest
    {
        public Rest()
        { }

        /// <summary>
        /// 是否需要抢修(0不需要,1需要)
        /// </summary>
        public int nNeedRest;

        /// <summary>
        /// 到达时间
        /// </summary>
        public DateTime dtArriveTime;

        /// <summary>
        /// 叫班时间
        /// </summary>
        public DateTime dtCallTime;
    }

    public class TrainmanPlan
    {
        public TF.RunSafty.Plan.TrainPlan trainPlan = new TF.RunSafty.Plan.TrainPlan();
        public NameGroup group = new NameGroup();
        public Rest rest = new Rest();
    }

    public class TrainmanPlanList : List<TrainmanPlan>
    {
 
    }

    /// <summary>
    ///类名: TrainPlanChangeLog
    ///说明: 行车计划变动日志
    /// </summary>
    public class TrainPlanChangeLog
    {
        public TrainPlanChangeLog()
        { }

        /// <summary>
        /// 
        /// </summary>
        public int nid;

        /// <summary>
        /// 
        /// </summary>
        public string strLogGUID;

        /// <summary>
        /// 计划GUID
        /// </summary>
        public string strTrainPlanGUID;

        /// <summary>
        /// 车型
        /// </summary>
        public string strTrainTypeName;

        /// <summary>
        /// 车号
        /// </summary>
        public string strTrainNumber;

        /// <summary>
        /// 车次
        /// </summary>
        public string strTrainNo;

        /// <summary>
        /// 计划开车时间
        /// </summary>
        public DateTime dtStartTime;

        /// <summary>
        /// 交路GUID
        /// </summary>
        public string strTrainJiaoluGUID;

        /// <summary>
        /// 交路名
        /// </summary>
        public string strTrainJiaoluName;

        /// <summary>
        /// 开车站
        /// </summary>
        public string strStartStation;

        /// <summary>
        /// 开车站名
        /// </summary>
        public string strStartStationName;

        /// <summary>
        /// 终到站
        /// </summary>
        public string strEndStation;

        /// <summary>
        /// 终到站名
        /// </summary>
        public string strEndStationName;

        /// <summary>
        /// 司机类型
        /// </summary>
        public int nTrainmanTypeID;

        /// <summary>
        /// 计划类型
        /// </summary>
        public int nPlanType;

        /// <summary>
        /// 牵引类型
        /// </summary>
        public int nDragType;

        /// <summary>
        /// 客货类型
        /// </summary>
        public int nKehuoID;

        /// <summary>
        /// 备注类型
        /// </summary>
        public int nRemarkType;

        /// <summary>
        /// 备注
        /// </summary>
        public string strRemark;

        /// <summary>
        /// 计划状态
        /// </summary>
        public int nPlanState;

        /// <summary>
        /// 计划创建时间
        /// </summary>
        public DateTime dtCreateTime;

        /// <summary>
        /// 操作端GUID
        /// </summary>
        public string strOperateSiteGUID;

        /// <summary>
        /// 操作端名称
        /// </summary>
        public string strOperateSiteName;

        /// <summary>
        /// 操作员GUID
        /// </summary>
        public string strOperateUserGUID;

        /// <summary>
        /// 操作员名称
        /// </summary>
        public string strOperateUserName;

        /// <summary>
        /// 操作工号
        /// </summary>
        public string strOperateUserID;

        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime dtChangeTime;    

    }

    /// <summary>
    ///类名: TrainPlanChangeLogList
    ///说明: 行车计划变动日志列表
    /// </summary>
    public class TrainPlanChangeLogList : List<TrainPlanChangeLog>
    {
        public TrainPlanChangeLogList()
        { }

    }       
}
