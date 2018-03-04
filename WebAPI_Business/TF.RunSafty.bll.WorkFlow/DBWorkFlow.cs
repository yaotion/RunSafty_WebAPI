using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.CommonUtility;
using System.Linq;

namespace TF.RunSafty.WorkFlow
{
    public class DBWorkFlowCfg
    {
        //添加配置
        public void Add(WorkFlowCfg flowCfg)
        {
            SqlParameter[] sqlParams = {
                                           new SqlParameter("flowType",flowCfg.flowType),
                                           new SqlParameter("workShop",flowCfg.flowType),
                                           new SqlParameter("workShopName",flowCfg.flowType),
                                           new SqlParameter("flowIdentify",flowCfg.flowType),
                                           new SqlParameter("flowName",flowCfg.flowType),
                                           new SqlParameter("timeBoundary_left",flowCfg.flowType),
                                           new SqlParameter("timeBoundary_right",flowCfg.flowType),
                                           new SqlParameter("withPlanID",flowCfg.flowType),
                                           new SqlParameter("enable",flowCfg.flowType),
                                           new SqlParameter("necessary",flowCfg.necessary)
                                       };


            string sql = @"select count(*) from TAB_Plan_WorkFlowCfg where strFlowIdentify = @flowIdentify and strflowType = @flowType";

            if (Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, sqlParams)) > 0)
            {
                throw new Exception(string.Format("已存在流程类型为\"{0}\" 流程标识为\"{1}\"的配置信息.", flowCfg.flowType, flowCfg.flowIdentify));
            }


            sql = @"insert into TAB_Plan_WorkFlowCfg (strflowType,strWorkShop,strWorkShopName,strFlowIdentify,
            strFlowName,nTimeBoundary_left,nTimeBoundary_right,nWithPlanID,nEnable,nNecessary) 
            values(@flowType,@workShop,@workShopName,@flowIdentify,@flowName,
            @timeBoundary_left,@timeBoundary_right,@withPlanID,@enable,@necessary)";

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, sqlParams);
        }

        //更新配置
        public void Update(WorkFlowCfg flowCfg)
        {
            SqlParameter[] sqlParams = {
                                           new SqlParameter("id",flowCfg.id),
                                           new SqlParameter("flowType",flowCfg.flowType),
                                           new SqlParameter("workShop",flowCfg.flowType),
                                           new SqlParameter("workShopName",flowCfg.flowType),
                                           new SqlParameter("flowIdentify",flowCfg.flowType),
                                           new SqlParameter("flowName",flowCfg.flowType),
                                           new SqlParameter("timeBoundary_left",flowCfg.flowType),
                                           new SqlParameter("timeBoundary_right",flowCfg.flowType),
                                           new SqlParameter("withPlanID",flowCfg.flowType),
                                           new SqlParameter("enable",flowCfg.flowType),
                                           new SqlParameter("necessary",flowCfg.necessary)
                                       };

            string sql = @"select count(*) from TAB_Plan_WorkFlowCfg where strFlowIdentify = @flowIdentify and strflowType = @flowType and @nID <> @id";

            if (Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, sqlParams)) > 0)
            {
                throw new Exception(string.Format("已存在流程类型为\"{0}\" 流程标识为\"{1}\"的配置信息.", flowCfg.flowType, flowCfg.flowIdentify));
            }


            sql = @"udpate TAB_Plan_WorkFlowCfg set strflowType = @flowType,strWorkShop = @workShop,strWorkShopName = @workShopName,
                strFlowIdentify = @flowIdentify,strFlowName = @flowName,nTimeBoundary_left = @timeBoundary_left,
                nTimeBoundary_right=@timeBoundary_right,nWithPlanID=@withPlanID,nEnable=@enable,nNecessary = @necessary) where nID = @id";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, sqlParams);
        }

        //删除配置
        public void Del(int id)
        {
            SqlParameter[] sqlParams = {
                                           new SqlParameter("id",id),
                                       };

            string sql = @"delete from TAB_Plan_WorkFlowCfg where nID = @id";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, sqlParams);
        }

        //数据行信息填充到实体
        private void DataRowToCfg(DataRow dr, WorkFlowCfg cfg)
        {
            cfg.id = ObjectConvertClass.static_ext_int(dr["nID"]);
            cfg.flowType = ObjectConvertClass.static_ext_string(dr["strFlowType"]);
            cfg.flowIdentify = ObjectConvertClass.static_ext_string(dr["strFlowIdentify"]);
            cfg.flowName = ObjectConvertClass.static_ext_string(dr["strFlowName"]);
            cfg.timeBoundary_left = ObjectConvertClass.static_ext_int(dr["nTimeBoundary_left"]);
            cfg.timeBoundary_right = ObjectConvertClass.static_ext_int(dr["nTimeBoundary_right"]);
            cfg.withPlanID = ObjectConvertClass.static_ext_int(dr["nWithPlanID"]);
            cfg.workShop = ObjectConvertClass.static_ext_string(dr["strWorkShop"]);
            cfg.workShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
            cfg.necessary = ObjectConvertClass.static_ext_int(dr["nNecessary"]);
            cfg.lackFlowHint = ObjectConvertClass.static_ext_string(dr["strLackFlowHint"]);
        }

        //通过flowType,flowIdentify查找配置
        public WorkFlowCfg Find(string flowType, string flowIdentify)
        {
            string sql = @"select * from TAB_Plan_WorkFlowCfg where strFlowType = '{0}' and strFlowIdentify = '{1}'";
            sql = string.Format(sql, flowType, flowIdentify);

            WorkFlowCfg cfg;
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                cfg = new WorkFlowCfg();
                DataRowToCfg(dt.Rows[0], cfg);
                return cfg;
            }
            else
                return null;

        }

        //查询配置列表
        public void Query(string flowType, string workShop, List<WorkFlowCfg> cfgs)
        {
            cfgs.Clear();
            string sql = @"select * from TAB_Plan_WorkFlowCfg where 1=1";
            if (!string.IsNullOrEmpty(flowType))
            {
                sql += string.Format(" and strFlowType = '{0}'", flowType);
            }


            if (!string.IsNullOrEmpty(workShop))
            {
                sql += string.Format(" and strWorkShop = '{0}'", workShop);
            }

            WorkFlowCfg cfg;
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                cfg = new WorkFlowCfg();
                DataRowToCfg(dr, cfg);

                cfgs.Add(cfg);
            }
        }
    }

    public class DBWorkFlow
    {
        //添加流程数据
        public void Add(WorkFlow workFlow)
        {
            SqlParameter[] sqlParams = {
                                           new SqlParameter("flowType",workFlow.flowType),
                                           new SqlParameter("tmid",workFlow.tmid),
                                           new SqlParameter("tmName",workFlow.tmName),
                                           new SqlParameter("flowIdentify",workFlow.flowIdentify),
                                           new SqlParameter("description",workFlow.description),
                                           new SqlParameter("success",workFlow.success),
                                           new SqlParameter("eventTime",workFlow.eventTime),
                                           new SqlParameter("planID",workFlow.planID)
                                       };

            //检查记录是否存在
            string sql = @"select nID from TAB_Plan_WorkFlow where strFlowType = @flowType 
            and strFlowIdentify = @flowIdentify and strTmid = @tmid and strPlanID = @planID";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, sqlParams).Tables[0];


            //存在则更新
            if (dt.Rows.Count > 0)
            {
                sql = @"update TAB_Plan_WorkFlow set strDescription = @description,nSuccess=@success,dtEventTime = @eventTime where 
            strFlowType = @flowType and strFlowIdentify = @flowIdentify and strTmid = @tmid and strPlanID = @planID";

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, sqlParams);
            }
            else
            {
                //不存在则插入记录
                sql = @"insert into TAB_Plan_WorkFlow (strFlowType,strTmid,strTmName,strFlowIdentify,
                strDescription,nSuccess,dtEventTime,strPlanID) 
                values(@flowType,@tmid,@tmName,@flowIdentify,@description,
                @success,@eventTime,@planID)";

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, sqlParams);
            }



        }

        //通过工号及时间边界，匹配计划
        public Boolean FillPlanID(WorkFlow workFlow, int timeBoundary_left, int timeBoundary_right)
        {
						string sql = string.Format("select strTrainmanGUID from TAB_Org_Trainman where strTrainmanNumber = '{0}'",workFlow.tmid);
						
						DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];
						
						if (dt.Rows.Count == 0)
							return false;
						
						string tmGUID = dt.Rows[0]["strTrainmanGUID"].ToString();
						
            sql = @"select trainPlan.strTrainPlanGUID from TAB_Plan_Train as trainPlan
            INNER JOIN TAB_Plan_Trainman AS tmPlan ON trainPlan.strTrainPlanGUID = tmPlan.strTrainPlanGUID            
            WHERE (tmPlan.strTrainmanGUID1 = @tmid or tmPlan.strTrainmanGUID2 = @tmid or tmPlan.strTrainmanGUID3 = @tmid or 
            tmPlan.strTrainmanGUID4 = @tmid) and (trainPlan.dtStartTime > @timebegin and trainPlan.dtStartTime < @timeend)";


            SqlParameter[] sqlparams = {
                                           new SqlParameter("tmid",tmGUID),
                                           new SqlParameter("timebegin",workFlow.eventTime.AddMinutes(-timeBoundary_left)),
                                           new SqlParameter("timeend",workFlow.eventTime.AddMinutes(timeBoundary_right))

                                       };



            dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, sqlparams).Tables[0];

            if (dt.Rows.Count > 0)
            {
                workFlow.planID = dt.Rows[0]["strTrainPlanGUID"].ToString();
                return true;
            }
            else
                return false;
        }

        //查询流程列表
        public void Query(string flowType, string tmid, string planID, List<WorkFlow> flows)
        {
            flows.Clear();
            string sql = "select * from TAB_Plan_WorkFlow where strTmID = @tmid and strPlanID = @planID and strFlowType = @flowType order by dtEventTime";
            SqlParameter[] sqlParams = {
                                           
                                           new SqlParameter("tmid",tmid),                                           
                                           new SqlParameter("planID",planID),
                                           new SqlParameter("flowType",flowType)
                                       };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, sqlParams).Tables[0];
            WorkFlow workFlow;
            foreach (DataRow dr in dt.Rows)
            {
                workFlow = new WorkFlow();
                workFlow.planID = ObjectConvertClass.static_ext_string(dr["strPlanID"]);
                workFlow.description = ObjectConvertClass.static_ext_string(dr["strDescription"]);
                workFlow.eventTime = ObjectConvertClass.static_ext_Date(dr["dtEventTime"]);
                workFlow.flowIdentify = ObjectConvertClass.static_ext_string(dr["strFlowIdentify"]);
                workFlow.flowType = ObjectConvertClass.static_ext_string(dr["strFlowType"]);
                workFlow.success = ObjectConvertClass.static_ext_int(dr["nSuccess"]);
                workFlow.tmid = ObjectConvertClass.static_ext_string(dr["strTmid"]);
                workFlow.tmName = ObjectConvertClass.static_ext_string(dr["strTmName"]);

                flows.Add(workFlow);
            }

        }

        public void Query(string flowType, string tmid, DateTime cqTime, List<WorkFlow> flows)
        {
            flows.Clear();
            string sql = "select * from TAB_Plan_WorkFlow where strTmID = @tmid   and strFlowType = @flowType and dtEventTime > @bTime and dtEventTime < @eTime order by dtEventTime desc";
            SqlParameter[] sqlParams = {                                           
                                           new SqlParameter("tmid",tmid),                                           
                                           new SqlParameter("bTime",cqTime.AddHours(-4)),
																					 new SqlParameter("eTime",cqTime.AddMinutes(30)),
                                           new SqlParameter("flowType",flowType)
                                       };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, sqlParams).Tables[0];
            WorkFlow workFlow;
            string flowIdentify = "";
            foreach (DataRow dr in dt.Rows)
            {
                flowIdentify = ObjectConvertClass.static_ext_string(dr["strFlowIdentify"]);
                WorkFlow retFlow = flows.Where(obj => obj.flowIdentify == flowIdentify).FirstOrDefault();

                if (retFlow != null)
                {
                    continue;
                }
                workFlow = new WorkFlow();
                workFlow.planID = ObjectConvertClass.static_ext_string(dr["strPlanID"]);
                workFlow.description = ObjectConvertClass.static_ext_string(dr["strDescription"]);
                workFlow.eventTime = ObjectConvertClass.static_ext_Date(dr["dtEventTime"]);
                workFlow.flowIdentify = ObjectConvertClass.static_ext_string(dr["strFlowIdentify"]);
                workFlow.flowType = ObjectConvertClass.static_ext_string(dr["strFlowType"]);
                workFlow.success = ObjectConvertClass.static_ext_int(dr["nSuccess"]);
                workFlow.tmid = ObjectConvertClass.static_ext_string(dr["strTmid"]);
                workFlow.tmName = ObjectConvertClass.static_ext_string(dr["strTmName"]);

                flows.Add(workFlow);
            }
        }
    }
}
