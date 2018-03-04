using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Leave.MD
{
    #region 请假类别
    /// <summary>
    ///类名: LeaveClass
    ///说明: 请假类别
    /// </summary>
    public class LeaveClass
    {
        public LeaveClass()
        { }

        /// <summary>
        /// 类别ID
        /// </summary>
        public string nClassID;

        /// <summary>
        /// 类别名称
        /// </summary>
        public string strClassName;
    }
    /// <summary>
    ///类名: LeaveClassArray
    ///说明: 请假类别数组
    /// </summary>
    public class LeaveClassArray : List<LeaveClass>
    {
        public LeaveClassArray()
        { }
    }
    #endregion

    #region 请假类型
    /// <summary>
    ///类名: LeaveType
    ///说明: 请假类型
    /// </summary>
    public class LeaveType
    {
        public LeaveType()
        { }

        /// <summary>
        /// 类型ID
        /// </summary>
        public string strTypeGUID;

        /// <summary>
        /// 类型名称
        /// </summary>
        public string strTypeName;

        /// <summary>
        /// 类别ID
        /// </summary>
        public int nClassID;

        /// <summary>
        /// 类别名称
        /// </summary>
        public string strClassName;

    }
    /// <summary>
    ///类名: LeaveTypeArray
    ///说明: 请假类型数组
    /// </summary>
    public class LeaveTypeArray : List<LeaveType>
    {
        public LeaveTypeArray()
        { }
    }
    #endregion

    #region 请假信息
    /// <summary>
    ///类名: AskLeaveEntity
    ///说明: 请假信息
    /// </summary>
    public class AskLeaveEntity
    {
        public AskLeaveEntity()
        { }

        /// <summary>
        /// 记录ID
        /// </summary>
        public string strAskLeaveGUID = string.Empty;

        /// <summary>
        /// 人员ID
        /// </summary>
        public string strTrainManID = string.Empty;

        /// <summary>
        /// 姓名
        /// </summary>
        public string strTrainmanName = string.Empty;

        /// <summary>
        /// 请假开始时间
        /// </summary>
        public DateTime? dtBeginTime;

        /// <summary>
        /// 请假结束时间
        /// </summary>
        public DateTime? dtEndTime;

        /// <summary>
        /// 类型ID
        /// </summary>
        public string strLeaveTypeGUID = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public int nStatus;

        /// <summary>
        /// 批准人ID
        /// </summary>
        public string strAskProverID = string.Empty;

        /// <summary>
        /// 批准人姓名
        /// </summary>
        public string strAskProverName = string.Empty;

        /// <summary>
        /// 请假时间
        /// </summary>
        public DateTime? dtAskCreateTime;

        /// <summary>
        /// 值班员姓名
        /// </summary>
        public string strAskDutyUserName = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string strMemo = string.Empty;

        /// <summary>
        /// 职位
        /// </summary>
        public int nPostID;

        /// <summary>
        /// 指导组名称
        /// </summary>
        public string strGuideGroupName = string.Empty;

    }
    #endregion

    #region 请假信息(带类型)
    /// <summary>
    ///类名: AskLeaveWithType
    ///说明: 请假信息(带类型)
    /// </summary>
    public class AskLeaveWithType
    {
        public AskLeaveWithType()
        { }

        /// <summary>
        /// 请假类型
        /// </summary>
        public string strTypeName = string.Empty;

        /// <summary>
        /// 请假信息
        /// </summary>
        public AskLeaveEntity AskLeave;

    }

    /// <summary>
    ///类名: AskLeaveWithTypeArray
    ///说明: 请假信息数组
    /// </summary>
    public class AskLeaveWithTypeArray : List<AskLeaveWithType>
    {
        public AskLeaveWithTypeArray()
        { }

    }
    #endregion

    #region 请假明细
    /// <summary>
    ///类名: AskLeaveDetailEntity
    ///说明: 请假明细
    /// </summary>
    public class AskLeaveDetailEntity
    {
        public AskLeaveDetailEntity()
        { }

        /// <summary>
        /// 明细记录ID
        /// </summary>
        public string strAskLeaveDetailGUID;

        /// <summary>
        /// 请假记录ID
        /// </summary>
        public string strAskLeaveGUID;

        /// <summary>
        /// 备注
        /// </summary>
        public string strMemo;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? dtBeginTime;

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? dtEndTime;

        /// <summary>
        /// 批准人ID
        /// </summary>
        public string strProverID;

        /// <summary>
        /// 批准人姓名
        /// </summary>
        public string strProverName;

        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime? dtCreateTime;

        /// <summary>
        /// 值班员ID
        /// </summary>
        public string strDutyUserID;

        /// <summary>
        /// 值班员姓名
        /// </summary>
        public string strDutyUserName;

        /// <summary>
        /// 客户端ID
        /// </summary>
        public string strSiteID;

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string strSiteName;

        /// <summary>
        /// 验证方式
        /// </summary>
        public int Verify;

    }
    #endregion

    #region 销假明细
    /// <summary>
    ///类名: CancelLeaveDetailEntity
    ///说明: 销假明细
    /// </summary>
    public class CancelLeaveDetailEntity : MsgType
    {
        public CancelLeaveDetailEntity()
        { }

        /// <summary>
        /// 明细ID
        /// </summary>
        public string strCancelLeaveGUID;

        /// <summary>
        /// 请假记录ID
        /// </summary>
        public string strAskLeaveGUID;

        /// <summary>
        /// 人员ID
        /// </summary>
        public string strTrainmanID;

        /// <summary>
        /// 批准人ID
        /// </summary>
        public string strProverID;

        /// <summary>
        /// 批准人姓名
        /// </summary>
        public string strProverName;

        /// <summary>
        /// 销假时间
        /// </summary>
        public DateTime? dtCancelTime;

        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime? dtCreateTime;

        /// <summary>
        /// 值班员DI
        /// </summary>
        public string strDutyUserID;

        /// <summary>
        /// 值班员姓名
        /// </summary>
        public string strDutyUserName;

        /// <summary>
        /// 客户端ID
        /// </summary>
        public string strSiteID;

        /// <summary>
        /// 客户端姓名
        /// </summary>
        public string strSiteName;

        /// <summary>
        /// 验证方式
        /// </summary>
        public int Verify;

    }
    #endregion

    #region 请假申请信息
    /// <summary>
    ///类名: LeaveApplyEntity
    ///说明: 请假申请信息
    /// </summary>
    public class LeaveApplyEntity : MsgType
    {
        public LeaveApplyEntity()
        { }

        public string strAskLeaveGUID;
        /// <summary>
        /// 人员ID
        /// </summary>
        public string strTrainmanGUID;

        /// <summary>
        /// 人员工号
        /// </summary>
        public string strTrainmanNumber;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime dtBeginTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime dtEndTime;
        /// <summary>
        /// 请假类型ID
        /// </summary>
        public string strTypeGUID;

        /// <summary>
        /// 请假类型名称
        /// </summary>
        public string strTypeName;

        /// <summary>
        /// 备注
        /// </summary>
        public string strRemark;

        /// <summary>
        /// 批准人ID
        /// </summary>
        public string strProverID;

        /// <summary>
        /// 批准人姓名
        /// </summary>
        public string strProverName;

        /// <summary>
        /// 值班员ID
        /// </summary>
        public string strDutyUserID;

        /// <summary>
        /// 值班员姓名
        /// </summary>
        public string strDutyUserName;

        /// <summary>
        /// 客户端ID
        /// </summary>
        public string strSiteID;

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string strSiteName;

        /// <summary>
        /// 验证方式
        /// </summary>
        public int Verify;
    }
    #endregion

    public class MsgType
    {
        public int msgType;
    
    }


    #region 年休假

    /// <summary>
    /// 年休假实体
    /// </summary>
    public class AnnualLeaveEntity
    {
        public string ID;
        //车间ID
        public string WorkShopGUID;
        //乘务员工号
        public string TrainmanNumber;
        //乘务员姓名
        public string TrainmanName;
        //年
        public int Year;
        //月
        public int Month;
        //休假状态 0未休假 1已休假 2已销假
        public int LeaveState;
        //已休假天数
        public int LeaveDays;
        //请假时间
        public DateTime? LeaveTime;
        //销假时间
        public DateTime? UnleaveTime;
        //假记录ID
        public string LeaveGUID;
        //记录创建时间
        public DateTime? CreateTime;
        //应休天数
        public int NeedDays;
        //删除原因
        public string DelReason;
    }

    /// <summary>
    /// 年休假查询条件实体
    /// </summary>
    public class AnnualQC
    {
        public string ID;
        public int Year;
        public int Month;        
        public string TrainmanNumber;                
        public string WorkShopGUID;
        public int State;
    }
    #endregion


}
