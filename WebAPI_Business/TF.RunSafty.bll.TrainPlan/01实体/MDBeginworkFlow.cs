using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TF.RunSafty.Plan.MD
{
    /// <summary>
    ///类名: AllowBeginworkData
    ///说明: 允许出勤流程
    /// </summary>
    public class AllowBeginworkData
    {
        public AllowBeginworkData()
        { }

        /// <summary>
        /// 计划GUID
        /// </summary>
        public string strTrainPlanGUID;

        /// <summary>
        /// 所属车间GUID
        /// </summary>
        public string strWorkShopGUID;

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime dtCreateTime;

        /// <summary>
        /// 流程状态
        /// </summary>
        public int nFlowState;

        /// <summary>
        /// 值班员姓名
        /// </summary>
        public string strDutyUserName;

        /// <summary>
        /// 值班员GUID
        /// </summary>
        public string strDutyUserGUID;

        /// <summary>
        /// 值班员工号
        /// </summary>
        public string strDutyUserNumber;

        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime dtConfirmTime;

        /// <summary>
        /// 描述
        /// </summary>
        public string strBrief;

    }


    /// <summary>
    ///类名: BeginworkFlow
    ///说明: BeginworkFlow
    /// </summary>
    public class BeginworkFlow
    {
        public BeginworkFlow()
        { }

        /// <summary>
        /// 自助编号
        /// </summary>
        public int nid;

        /// <summary>
        /// 所属车间GUID
        /// </summary>
        public string strWorkShopGUID;

        /// <summary>
        /// 步骤编号
        /// </summary>
        public int nStepID;

        /// <summary>
        /// 步骤序号
        /// </summary>
        public int nStepIndex;

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string strStepName;

        /// <summary>
        /// 步骤类型
        /// </summary>
        public int nStepType;

    }


    /// <summary>
    ///类名: BeginworkFlowList
    ///说明: BeginworkFlowList
    /// </summary>
    public class BeginworkFlowList : List<BeginworkFlow>
    {
        public BeginworkFlowList()
        { }

    }


    /// <summary>
    ///类名: TrainmanBeginworkStep
    ///说明: TrainmanBeginworkStep
    /// </summary>
    public class TrainmanBeginworkStep
    {
        public TrainmanBeginworkStep()
        { }

        /// <summary>
        /// strTrainPlanGUID
        /// </summary>
        public string strTrainPlanGUID;

        /// <summary>
        /// nStepID
        /// </summary>
        public int nStepID;

        /// <summary>
        /// 人员GUID
        /// </summary>
        public string strTrainmanGUID;

        /// <summary>
        /// 人员工号
        /// </summary>
        public string strTrainmanNumber;

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string strTrainmanName;

        /// <summary>
        /// 处理结果
        /// </summary>
        public int nStepResultID;

        /// <summary>
        /// 结果描述
        /// </summary>
        public string strStepResultText;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime dtCreateTime;

        /// <summary>
        /// 事件发生时间
        /// </summary>
        public DateTime dtEventTime;

    }

    ///类名: TrainmanBeginworkStepList
    ///说明: TrainmanBeginworkStepList
    /// </summary>
    public class TrainmanBeginworkStepList : List<TrainmanBeginworkStep>
    {
        public TrainmanBeginworkStepList()
        { }

    }

    /// <summary>
    ///类名: TrainplanBeginworkFlow
    ///说明: TrainplanBeginworkFlow
    /// </summary>
    public class TrainplanBeginworkFlow
    {
        public TrainplanBeginworkFlow()
        { }

        /// <summary>
        /// 行车计划GUID
        /// </summary>
        public string strTrainPlanGUID;

        /// <summary>
        /// 所在车间GUID
        /// </summary>
        public string strWorkShopGUID;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime dtCreateTime;

        /// <summary>
        /// 流程状态
        /// </summary>
        public int nFlowState;

        /// <summary>
        /// 值班员姓名
        /// </summary>
        public string strDutyUserName;

        /// <summary>
        /// 值班员GUID
        /// </summary>
        public string strDutyUserGUID;

        /// <summary>
        /// 值班员工号
        /// </summary>
        public string strDutyUserNumber;

        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime dtConfirmTime;

        /// <summary>
        /// 说明
        /// </summary>
        public string strBrief;

    }
}
