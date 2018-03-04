using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace TF.RunSafty.WorkFlow
{
    //接口返回信息
    public class InterfaceRet
    {
        public int result;
        public string resultStr;
        public object data;
        public void Clear()
        {
            result = 0;
            resultStr = string.Empty;
            data = null;
        }
    }

    //接口基类
    public class InterfaceBase
    {
        protected InterfaceRet _ret = new InterfaceRet();
    }


    public class WorkFlowLogic : InterfaceBase
    {
        protected DBWorkFlow _dbWorkFlow = new DBWorkFlow();

        //添加流程信息
        protected void AddFlow(WorkFlow workFlow)
        {           
            //查找区配计划
            _dbWorkFlow.Add(workFlow);
           
        }

        //在流程列表中，通过流程标识查找流程信息
        private WorkFlow FindFlow(string flowType,string flowIdentify,List<WorkFlow> flows)
        {
            foreach (WorkFlow workFlow in flows)
            {
                if (workFlow.flowIdentify == flowIdentify && workFlow.flowType == flowType)
                {
                    return workFlow;
                }
            }

            return null;
        }

        private string FindFlowName(string flowIdentify, List<WorkFlowCfg> cfgs)
        {
            foreach (var cfg in cfgs)
            {
                if (cfg.flowIdentify == flowIdentify)
                {
                    return cfg.flowName;
                }
            }
            return "";
        }

        //检测指定计划的流程是否通过验证
        protected Boolean CheckWorkFlows(string flowType,string workShopID,string tmid,string planID,List<WorkFlow> flows)
        {                      

            List<WorkFlowCfg> cfgs = new List<WorkFlowCfg>();
            DBWorkFlowCfg _dbWorkFlowCfg = new DBWorkFlowCfg();
            _dbWorkFlowCfg.Query(flowType,workShopID, cfgs);

            if (string.IsNullOrEmpty(planID))
            {
                _dbWorkFlow.Query(flowType, tmid, DateTime.Now, flows);
            }
            else
            {
                _dbWorkFlow.Query(flowType, tmid, planID, flows);
            }
            

            WorkFlow flow;
            foreach (WorkFlowCfg cfg in cfgs)
            {
                if (cfg.necessary == 1)
                {
                    if (FindFlow(flowType, cfg.flowIdentify, flows) == null)
                    {
                        flow = new WorkFlow();
                        flow.flowIdentify = cfg.flowIdentify;
                        flow.flowType = cfg.flowType;
                        flow.flowName = cfg.flowName;
                        if (string.IsNullOrEmpty(cfg.lackFlowHint))
                        {
                            flow.description = "流程缺失";
                        }
                            
                        else
	                    {
                            flow.description = cfg.lackFlowHint;
	                    }

                        flow.success = 2;

                        flows.Add(flow);
                        //没有必需流程
                    }
                }
            }


            foreach (WorkFlow workFlow in flows)
            {
                workFlow.flowName = FindFlowName(workFlow.flowIdentify,cfgs);
            }

            foreach (WorkFlow workFlow in flows)
            {
                //流程未通过
                if (workFlow.success != 0)
                {
                    return false;
                }
            }
            return true ;
        }
  

    }

    public class LCBeginWorkFlow : WorkFlowLogic
    {

        //添加流程
        public InterfaceRet Add(string data)
        {
            _ret.Clear();
            WorkFlow workFlow = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkFlow>(data);
            workFlow.flowType = WorkFlowType.BeginWork;

            WorkFlowCfg cfg = new DBWorkFlowCfg().Find(workFlow.flowType, workFlow.flowIdentify);

            if (cfg != null)
            {
                if (cfg.withPlanID == 0)
                {
                    _dbWorkFlow.FillPlanID(workFlow, cfg.timeBoundary_left, cfg.timeBoundary_right);                    
                }
                
            }
            
            _dbWorkFlow.Add(workFlow);
            return _ret;
        }

        //添加流程列表
        public InterfaceRet AddFlows(string data)
        {
            List<WorkFlow> workFlows = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WorkFlow>>(data);
            foreach (WorkFlow workFlow in workFlows)
            {
                workFlow.flowType = WorkFlowType.BeginWork;
                WorkFlowCfg cfg = new DBWorkFlowCfg().Find(workFlow.flowType, workFlow.flowIdentify);

                if (cfg != null)
                {
                    if (cfg.withPlanID == 0)
                    {
                        _dbWorkFlow.FillPlanID(workFlow, cfg.timeBoundary_left, cfg.timeBoundary_right);
                    }

                }
                _dbWorkFlow.Add(workFlow);
            }            
            return _ret;
        }
        

        //检查流程是否通过验证
        public InterfaceRet Check(string data)
        {
            var input = new { tmid = "", planID = "", workShopID = "" };

            input = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(data, input);            


            List<WorkFlow> workFlows = new List<WorkFlow>();
					
			Boolean ret = CheckWorkFlows(WorkFlowType.BeginWork, input.workShopID, input.tmid, input.planID, workFlows);	
						
						
            
            _ret.data = new { checkRet = ret, flows = workFlows };            
            return _ret;
        }
        
        //查询流程列表
        public InterfaceRet Query(string data)
        {
            var input = new { tmid = "", planID = ""};

            input = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(data, input);            


            List<WorkFlow> workFlows = new List<WorkFlow>();
            _dbWorkFlow.Query(WorkFlowType.BeginWork, input.tmid, input.planID, workFlows);
            _ret.data = workFlows; 
            return _ret;
        }
    }

    public class LCEndWorkFlow : WorkFlowLogic
    {
        //添加流程
        public InterfaceRet Add(string data)
        {
            _ret.Clear();
            WorkFlow workFlow = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkFlow>(data);
            workFlow.flowType = WorkFlowType.EndWork;
            WorkFlowCfg cfg = new DBWorkFlowCfg().Find(workFlow.flowType, workFlow.flowIdentify);

            if (cfg != null)
            {
                if (cfg.withPlanID == 0)
                {
                    _dbWorkFlow.FillPlanID(workFlow, cfg.timeBoundary_left, cfg.timeBoundary_right);
                }

            }
            _dbWorkFlow.Add(workFlow);
            return _ret;
        }

        //添加流程列表
        public InterfaceRet AddFlows(string data)
        {
            List<WorkFlow> workFlows = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WorkFlow>>(data);
            foreach (WorkFlow workFlow in workFlows)
            {
                workFlow.flowType = WorkFlowType.EndWork;
                WorkFlowCfg cfg = new DBWorkFlowCfg().Find(workFlow.flowType, workFlow.flowIdentify);

                if (cfg != null)
                {
                    if (cfg.withPlanID == 0)
                    {
                        _dbWorkFlow.FillPlanID(workFlow, cfg.timeBoundary_left, cfg.timeBoundary_right);
                    }

                }
                _dbWorkFlow.Add(workFlow);
            }
            return _ret; ;
        }

        //检查流程是否通过验证
        public InterfaceRet Check(string data)
        {
            var input = new { tmid = "", planID = "", workShopID = "" };

            input = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(data, input);            

            List<WorkFlow> workFlows = new List<WorkFlow>();
            Boolean ret = CheckWorkFlows(WorkFlowType.EndWork,input.workShopID, input.tmid, input.planID, workFlows);
            _ret.data = new { checkRet = ret, flows = workFlows };      
            return _ret;
        }

        //查询流程
        public InterfaceRet Query(string data)
        {
            var input = new { tmid = "", planID = "" };

            input = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(data, input);   
            List<WorkFlow> workFlows = new List<WorkFlow>();
            _dbWorkFlow.Query(WorkFlowType.EndWork, input.tmid, input.planID, workFlows);
            _ret.data = workFlows;
            return _ret;
        }
    }


    //流程基础信息管理接口类
    public class LCWorkFlowCfg : InterfaceBase
    {
        private DBWorkFlowCfg _dbWorkFlowCfg = new DBWorkFlowCfg();

        //添加配置信息
        public InterfaceRet Add(string data)
        {
            _ret.Clear();
            WorkFlowCfg workFlowCfg = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkFlowCfg>(data);

            _dbWorkFlowCfg.Add(workFlowCfg);

            return _ret;
        }

        //更新配置信息
        public InterfaceRet Update(string data)
        {
            _ret.Clear();
            WorkFlowCfg workFlowCfg = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkFlowCfg>(data);

            _dbWorkFlowCfg.Update(workFlowCfg);
            
            return _ret;
        }
        //删除配置信息
        public InterfaceRet Del(string data)
        {
            _ret.Clear();
            WorkFlowCfg workFlowCfg = Newtonsoft.Json.JsonConvert.DeserializeObject<WorkFlowCfg>(data);

            _dbWorkFlowCfg.Del(workFlowCfg.id);

            return _ret;
        }

        //查询配置信息列表
        public InterfaceRet Query(string data)
        {
            _ret.Clear();
            List<WorkFlowCfg> cfgs = new List<WorkFlowCfg>();
            _dbWorkFlowCfg.Query(null,null,cfgs);
            _ret.data = cfgs;
            return _ret;
        }
    }


    
    /// <summary>
    /// 出勤流程确认类
    /// </summary>
    public class LCChuQinConfirm:InterfaceBase
    {
        private class IsConfirmIn
        {
            public string number = "";
        }
        private class IsConfirmOut
        {
            public bool exist = false;
        }
        //添加配置信息
        public InterfaceRet IsUser(string data)
        {            
            IsConfirmIn input = Newtonsoft.Json.JsonConvert.DeserializeObject<IsConfirmIn>(data);
            string strSql = "select count(*) TrainmanCount from TAB_Plan_WorkFlow_User where strUserNumber = @strUserNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                new SqlParameter("strUserNumber",input.number)
            };
            _ret.Clear();
            IsConfirmOut resultdata = new IsConfirmOut();
            resultdata.exist = false;
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            resultdata.exist = Convert.ToInt32(dt.Rows[0]["TrainmanCount"].ToString()) > 0;
            _ret.data = resultdata;
            return _ret;
        }
        /// <summary>
        /// 确认信息
        /// </summary>
        private class ConfirmInfo
        {
            /// <summary>
            /// 确认人工号
            /// </summary>
            public string userNumber = "";
            /// <summary>
            /// 确认人姓名
            /// </summary>
            public string userName = "";
            /// <summary>
            /// 确认时间
            /// </summary>
            public DateTime confirmTime = DateTime.MinValue;
            /// <summary>
            /// 乘务员工号
            /// </summary>
            public string trainmanNumber = "";
            /// <summary>
            /// 乘务员姓名
            /// </summary>
            public string trainmanName = "";
            /// <summary>
            /// 计划出勤时间
            /// </summary>
            public DateTime chuqinTime = DateTime.MinValue;
            /// <summary>
            /// 值乘车次
            /// </summary>
            public string trainNo = "";
            /// <summary>
            /// 计划ID
            /// </summary>
            public string planID = "";
            /// <summary>
            /// 流程标示
            /// </summary>
            public string flowType = "";
            /// <summary>
            /// 确认描述
            /// </summary>
            public string confirmBrief = "";
            /// <summary>
            /// 车间ID
            /// </summary>
            public string workShopID;
            /// <summary>
            ///车间名称 
            /// </summary>
            public string workShopName;

        }

        public InterfaceRet CommitConfirm(string data)
        {
            _ret.Clear();
            ConfirmInfo input = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfirmInfo>(data);
            string strSql = @"insert into TAB_Plan_WorkFlow_Confirm (strUserNumber,strUserName,dtConfirmTime,strTrainmanNumber,
                strTrainmanName,dtChuqinTime,strTrainNo,strPlanID,strFlowType,strConfirmBrief,strWorkShopGUID,strWorkShopName) values (@strUserNumber,@strUserName,@dtConfirmTime,@strTrainmanNumber,
                @strTrainmanName,@dtChuqinTime,@strTrainNo,@strPlanID,@strFlowType,@strConfirmBrief,@strWorkShopID,@strWorkShopName)";
            SqlParameter[] sqlParams = new SqlParameter[]{
                new SqlParameter("strUserNumber",input.userNumber),
                new SqlParameter("strUserName",input.userName),
                new SqlParameter("dtConfirmTime",input.confirmTime),
                new SqlParameter("strTrainmanNumber",input.trainmanNumber),
                new SqlParameter("strTrainmanName",input.trainmanName),
                new SqlParameter("dtChuqinTime",input.chuqinTime),
                new SqlParameter("strTrainNo",input.trainNo),
                new SqlParameter("strPlanID",input.planID),
                new SqlParameter("strFlowType",input.flowType),
                new SqlParameter("strConfirmBrief",input.confirmBrief),
                new SqlParameter("strWorkShopID",input.workShopID),
                new SqlParameter("strWorkShopName",input.workShopName)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
            return _ret;
        }
    }

    
}
