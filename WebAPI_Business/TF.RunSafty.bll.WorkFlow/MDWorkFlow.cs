using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.WorkFlow
{
    //工作流程类型
    public class WorkFlowType
    { 
        public const string BeginWork = "RS.RUNSAFTY.FLOW.BEGINWORK";
        public const string EndWork = "RS.RUNSAFTY.FLOW.ENDWORK";        
    }

    //工作流程实体
    public class WorkFlow
    {
        //流程类型
        public string flowType;
        //流程名称
        public string flowName;
        //工号
        public string tmid;
        //姓名
        public string tmName;
        //流程标识
        public string flowIdentify;
        //流程执行描述信息
        public string description;
        //执行结果
        public int success;
        //流程事件时间
        public DateTime eventTime;
        //计划ID
        public string planID;
    }

    public class WorkFlowCfg
    {
        //记录ID
        public int id;
        //流程类型
        public string flowType;
        //所属车间
        public string workShop;
        //车间名称
        public string workShopName;
        //流程标识
        public string flowIdentify;
        //流程名称
        public string flowName;
        //时间左边界
        public int timeBoundary_left;
        //时间右边界
        public int timeBoundary_right;
        //是否带有计划ID
        public int withPlanID;
        //必需的流程
        public int necessary;
        //流程缺失提示
        public string lackFlowHint;
    }
}
