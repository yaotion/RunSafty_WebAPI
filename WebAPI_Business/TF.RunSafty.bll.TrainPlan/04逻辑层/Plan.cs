using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TF.Api.Utilities;
using TF.RunSafty.Entry;
using TF.RunSafty.Model;
using TF.RunSafty.Model.InterfaceModel;
using ThinkFreely.DBUtility;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using TF.RunSafty.BLL;
using System.Reflection;
using TF.CommonUtility;
using System.Web.Script.Serialization;
using TF.RunSafty.DDML;
using TF.RunSafty.ICCard;
using VIEW_Plan_Trainman = TF.RunSafty.BLL.Plan.VIEW_Plan_Trainman;
using TF.RunSafty.DBUtils;
using ThinkFreely.RunSafty;
using TF.Runsafty.Plan.DB;
using TF.Runsafty.Plan.MD;
using TF.RunSafty.DrinkLogic;

namespace TF.RunSafty.Plan
{

    public class DutyInfo
    {
        public string dutyUserID = "";
    }
    public class EndWorkData
    {
        public string endworkID = "";
        public string trainmanID = "";
        public string planID = "";
        public int verifyID = 0;
        public string dutyUserID = "";
        public string stationID = "";
        public string placeID = "";
        public string arriveTime = "";
        public string lastEndWorkTime = "";
        public string remark = "";

    }
    public class DrinkData
    {
        public string strGuid = "";
        public string trainmanID = "";
        public string drinkResult = "0";
        public int workTypeID = 0;
        public string createTime = "";
        public string imagePath = "";

        //人员信息
        public string strTrainmanName;
        public string strTrainmanNumber;

        //车次信息
        public string strTrainNo;
        public string strTrainNumber;
        public string strTrainTypeName;

        //车间信息
        public string strWorkShopGUID;
        public string strWorkShopName;
        //退勤点信息
        public string strPlaceID;
        public string strPlaceName;
        public string strSiteGUID;
        public string strSiteName;
        //酒精度
        public string dwAlcoholicity;
        public string nWorkTypeID;

        public string strAreaGUID;
        public Boolean bLocalAreaTrainman;


        //干部相关（部门名称（id），职位名称（id）)

        public string strDepartmentID = "";//部门id（车间DUID）
        public string strDepartmentName = "";//部门名称
        public string nCadreTypeID = "";//职位id
        public string strCadreTypeName = "";//职位名称



    }


    public class RCEndWork
    {

    

        public bool Endwork(string siteID, EndWorkData endworkData, DrinkData drinkdata, DutyInfo dutyUser)
        {

            //判断是否有出勤计划
            if (!DBEndwork.ExistEndWorkPlan(siteID, endworkData.trainmanID))
            {
                return false;
            }
            try
            {

                #region 获取计划信息
                string strSqlPlan = "select strTrainJiaoluGUID,strTrainJiaoluName,strTrainNo,dtStartTime from view_Plan_Train where strTrainPlanGUID=@strTrainPlanGUID";
                SqlParameter[] sqlParamsPlan = new SqlParameter[]{
                    new SqlParameter("strTrainPlanGUID",endworkData.planID)
                };
                DataTable dtPlan = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlPlan, sqlParamsPlan).Tables[0];
                if (dtPlan.Rows.Count == 0)
                {
                    throw new Exception("指定的计划信息不存在,关联退勤记录失败");
                }
                string strTrainJiaoluGUID = dtPlan.Rows[0]["strTrainJiaoluGUID"].ToString();
                string strTrainJiaoluName = dtPlan.Rows[0]["strTrainJiaoluName"].ToString();
                DateTime dtStartTime = Convert.ToDateTime(dtPlan.Rows[0]["dtStartTime"]);
                string strTrainNo = dtPlan.Rows[0]["strTrainNo"].ToString();
                #endregion

                #region 获取人员信息
                string strSqlTM = "select strTrainmanNumber,strTrainmanName  from TAB_Org_Trainman where strTrainmanGUID=@strTrainmanGUID";
                SqlParameter[] sqlParamsTM = new SqlParameter[]{
                    new SqlParameter("strTrainmanGUID",endworkData.trainmanID)
                };
                DataTable dtTM = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlTM, sqlParamsTM).Tables[0];
                if (dtTM.Rows.Count == 0)
                {
                    throw new Exception("指定的人员信息不存在,关联退勤记录失败");

                }
                string strTrainmanName = dtTM.Rows[0]["strTrainmanName"].ToString();
                string strTrainmanNumber = dtTM.Rows[0]["strTrainmanNumber"].ToString();
                #endregion

            
               
                //根据planid获取交路，然后根据客户端编号和交路获取退勤端的第一个出勤点
                string strSql =
                    "select strTrainJiaoluGUID from tab_plan_train where strTrainPlanGUID=@strTrainPlanGUID    ";
                SqlParameter[] sqlParamsFind = new SqlParameter[]
                {
                    new SqlParameter("strTrainPlanGUID", endworkData.planID),
                    new SqlParameter("strTrainmanGUID", endworkData.trainmanID)
                };
                object oPlaceId = null;
                SqlParameter[] sqlParameters;
                object oTrainjiaolu = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql,
                    sqlParamsFind);
                if (oTrainjiaolu != null)
                {
                    strSql =
                        "select top 1 strPlaceID from TAB_Base_Site_DutyPlace where strSiteGUID=@strSiteGUID and strTrainJiaoluGUID=strTrainJiaoluGUID";
                    sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("strSiteGUID", siteID),
                        new SqlParameter("strTrainJiaoluGUID", oTrainjiaolu.ToString()),
                    };
                    oPlaceId = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
                }
                if (oPlaceId == null)
                {
                    throw new Exception("找不到该退勤端所管理的出勤点，请在网站中配置。");
                }
                string sqlText = "select strGroupGUID from TAB_Plan_Trainman where strTrainPlanGUID = @strTrainPlanGUID";
                object obj =
                    Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText,
                        sqlParamsFind));
                string strGroupGUID = "";
                if (obj != null)
                {
                    strGroupGUID = Convert.ToString(obj);
                }

                sqlText = @"select nJiaoluType,strTrainmanJiaoluGUID,strTrainmanJiaoluName,nTrainmanRunType from VIEW_Nameplate_TrainmanInJiaolu_All where strTrainmanGUID = @strTrainmanGUID";
                DataTable dtJiaolu =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsFind).Tables[0];
                if (dtJiaolu.Rows.Count == 0)
                {
                    throw new Exception("该机组没有处于任何人员交路中");
                }
                string trainmanJiaoluGUID = dtJiaolu.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                string trainmanJiaoluName = dtJiaolu.Rows[0]["strTrainmanJiaoluName"].ToString();
                int nTrainmanJiaoluType, nRunType;
                int.TryParse(dtJiaolu.Rows[0]["nJiaoluType"].ToString(), out nTrainmanJiaoluType);
                int.TryParse(dtJiaolu.Rows[0]["nTrainmanRunType"].ToString(), out nRunType);

              
                sqlText = "select strGUID from tab_drink_information where strTrainmanGUID=@strTrainmanGUID and dtCreateTime=@dtCreateTime";
                SqlParameter[] sqlParamsDrink = new SqlParameter[] { 
                    new SqlParameter("strTrainmanGUID",endworkData.trainmanID),
                    new SqlParameter("dtCreateTime",drinkdata.createTime)
                };
                string drinkGUID = "";
                object objDrink = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsDrink);
                if ((objDrink != null) && (!DBNull.Value.Equals(objDrink)))
                {
                    drinkGUID = objDrink.ToString();
                }

                sqlText = "select top 1 strEndWorkGUID from TAB_Plan_EndWork where strTrainPlanGUID = @strTrainPlanGUID and strTrainmanGUID = @strTrainmanGUID";

                obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsFind);

                string endworkID = Guid.NewGuid().ToString();

                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("strEndWorkGUID", endworkID),
                    new SqlParameter("strTrainPlanGUID", endworkData.planID),
                    new SqlParameter("strTrainmanGUID", endworkData.trainmanID),
                    new SqlParameter("nVerifyID", endworkData.verifyID),
                    new SqlParameter("strStationGUID", endworkData.stationID),
                    new SqlParameter("strRemark", endworkData.remark),
                    new SqlParameter("strGroupGUID", strGroupGUID),
                    new SqlParameter("strPlaceID", oPlaceId.ToString()),
                    new SqlParameter("nPlanState", 8),
                    new SqlParameter("NullTrainPlanGUID", ""),
                    new SqlParameter("strGUID", Guid.NewGuid().ToString()),
                    new SqlParameter("strTrainmanJiaoluGUID", trainmanJiaoluGUID),
                    new SqlParameter("strDutyGUID", dutyUser.dutyUserID),
                    new SqlParameter("strWorkID", endworkID),
                    new SqlParameter("nWorkTypeID", WORKTYPE.WORKTYPE_END),
                    new SqlParameter("dtLastArriveTime", endworkData.arriveTime),
                    new SqlParameter("dtLastEndWorkTime", endworkData.lastEndWorkTime),
                    new SqlParameter("dtCreateTime", drinkdata.createTime),
                    new SqlParameter("nTrainmanState", 2)
                };
                if (obj != null)
                {
                    endworkID = Convert.ToString(obj);
                }
                bool finishedPlan = false;
                SqlTrans sqlTrans = new SqlTrans();
                try
                {
                    sqlTrans.Begin();
                    if (obj == null)
                    {
                        sqlText =
                             @"insert into TAB_Plan_EndWork(strEndWorkGUID,strTrainPlanGUID,strTrainmanGUID,dtCreateTime,nVerifyID,strStationGUID,strRemark) values (
                    @strEndWorkGUID,@strTrainPlanGUID,@strTrainmanGUID,@dtCreateTime,@nVerifyID,@strStationGUID,@strRemark)";

                        SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, sqlText, sqlParams);
                    }
                    else
                    {
                        sqlText = "update TAB_Plan_EndWork set nVerifyID=@nVerifyID,strRemark=@strRemark where strEndWorkGUID=@strEndWorkGUID";
                        SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, sqlText, sqlParams);
                    }
                    


                    #region 添加测酒记录
                    //将之前的测酒记录和计划关联取消
                    sqlText = "update TAB_Drink_Information set strWorkID='' where strWorkID = @strWorkID and nWorkTypeID=@nWorkTypeID and strTrainmanGUID = @strTrainmanGUID";
                    SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, sqlText, sqlParams);

                    if (drinkGUID == "")
                    {
                        MDDrink MDDr = new MDDrink();
                        DBDrink DBDr = new DBDrink();

                        //职位信息----- 开始----------
                        DBDrinkLogic dbdl = new DBDrinkLogic();
                        MDDrinkLogic mddl = new MDDrinkLogic();
                        mddl = dbdl.GetDrinkCadreEntity(drinkdata.strTrainmanNumber);
                        if (mddl != null)
                        {
                            MDDr.strDepartmentID = mddl.strDepartmentID;
                            MDDr.strDepartmentName = mddl.strDepartmentName;
                            MDDr.nCadreTypeID = mddl.nCadreTypeID;
                            MDDr.strCadreTypeName = mddl.strCadreTypeName;
                        }
                        //职位信息----- 结束----------

                        //是否是本段
                        MDDr.nLocalAreaTrainman = drinkdata.bLocalAreaTrainman == true ? 1 : 0;
                        MDDr.trainmanID = endworkData.trainmanID;
                        MDDr.createTime = drinkdata.createTime;
                        MDDr.verifyID = endworkData.verifyID;
                        MDDr.oPlaceId = oPlaceId.ToString();
                        MDDr.strGuid = Guid.NewGuid().ToString();
                        MDDr.drinkResult = drinkdata.drinkResult;
                        MDDr.strAreaGUID = drinkdata.strAreaGUID;
                        MDDr.dutyUserID = dutyUser.dutyUserID;
                        MDDr.strTrainmanName = drinkdata.strTrainmanName;
                        MDDr.strTrainmanNumber = drinkdata.strTrainmanNumber;
                        MDDr.strTrainNo = drinkdata.strTrainNo;
                        MDDr.strTrainNumber = drinkdata.strTrainNumber;
                        MDDr.strTrainTypeName = drinkdata.strTrainTypeName;
                        MDDr.createTime = drinkdata.createTime;
                        MDDr.strWorkShopGUID = drinkdata.strWorkShopGUID;
                        MDDr.strWorkShopName = drinkdata.strWorkShopName;
                        MDDr.strPlaceID = drinkdata.strPlaceID;
                        MDDr.strPlaceName = drinkdata.strPlaceName;
                        MDDr.strSiteGUID = drinkdata.strSiteGUID;
                        MDDr.strSiteName = drinkdata.strSiteName;
                        MDDr.dwAlcoholicity = drinkdata.dwAlcoholicity;
                        MDDr.strWorkID = endworkID;
                        MDDr.nWorkTypeID = WORKTYPE.WORKTYPE_END;//工作类型为退勤
                        MDDr.imagePath = drinkdata.imagePath;
                        DBDr.SubmitDrink(MDDr, sqlTrans.trans);
                    }
                    else
                    {
                        strSql = @"Update Tab_Drink_Information set strWorkID = @strWorkID,nWorkTypeID =nWorkTypeID,
                            strTrainNo=@strTrainNo,strTrainTypeName=@strTrainTypeName,strTrainNumber=@strTrainNumber where strGUID = @strGUID";
                        SqlParameter[] sqlParamsDrinkU = new SqlParameter[] { 
                            new SqlParameter("strWorkID",endworkID),
                            new SqlParameter("nWorkTypeID",WORKTYPE.WORKTYPE_END),
                            new SqlParameter("strGUID",drinkGUID),
                            new SqlParameter("strTrainNo",drinkdata.strTrainNo),
                            new SqlParameter("strTrainTypeName",drinkdata.strTrainTypeName),
                            new SqlParameter("strTrainNumber",drinkdata.strTrainNumber)
                        };
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParamsDrinkU);
                    }
                    #endregion


                    #region 修改计划状态为已退勤
                    sqlText =
                        @"update TAB_Plan_Train set nPlanState=@nPlanState where strTrainPlanGUID=@strTrainPlanGUID and  
                   (select count(*) from VIEW_Plan_EndWork where strTrainPlanGUID=TAB_Plan_Train.strTrainPlanGUID  and  
                   ((strTrainmanGUID1 is null) or (strTrainmanGUID1 = '') or not(dtTestTime1 is null)) and 
                   ((strTrainmanGUID2 is null) or (strTrainmanGUID2 = '') or not(dtTestTime2 is null))  and 
                   ((strTrainmanGUID3 is null) or (strTrainmanGUID3 = '') or not(dtTestTime3 is null))  and 
                   ((strTrainmanGUID4 is null) or (strTrainmanGUID4 = '') or not(dtTestTime4 is null))) > 0";
                    finishedPlan = SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, sqlText, sqlParams) > 0;
                    if (finishedPlan)
                    {
                        strSql =
                            "update TAB_Nameplate_Group set strTrainPlanGUID = @NullTrainPlanGUID where strGroupGUID = @strGroupGUID";
                        SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, strSql, sqlParams);
                        //翻牌
                        //jltTogether
                        if (nTrainmanJiaoluType == 4)
                        {

                            DB_Plan_EndWork.TurnTogetherTrainGroup(strGroupGUID, TF.CommonUtility.ObjectConvertClass.static_ext_Date(endworkData.arriveTime));
                        }

                        //jltOrder,轮乘交路需要修改出勤点
                        if (nTrainmanJiaoluType == 3)
                        {
                            sqlText =
                                "update TAB_Nameplate_Group set strStationGUID = @strStationGUID,strPlaceID=@strPlaceID where strGroupGUID = @strGroupGUID";
                            int count = SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, sqlText, sqlParams);

                            sqlText =
                                "update TAB_Nameplate_TrainmanJiaolu_Order set dtLastArriveTime = @dtLastArriveTime where strGroupGUID = @strGroupGUID";
                            SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, sqlText, sqlParams);
                        }

                    }
                    #endregion

                    //修改计划终到时间
                    sqlText =
                        @"update TAB_Plan_Train set dtLastArriveTime = @dtLastArriveTime where strTrainPlanGUID = @strTrainPlanGUID";
                    SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, sqlText, sqlParams);

                    //修改人员最后一次退勤时间
                    sqlText =
                        "update TAB_Org_Trainman set dtLastEndWorkTime=@dtLastEndWorkTime,nTrainmanState=@nTrainmanState where strTrainmanGUID = @strTrainmanGUID";
                    SqlHelper.ExecuteNonQuery(sqlTrans.trans, CommandType.Text, sqlText, sqlParams);
                   
                }
                catch (Exception ex)
                {
                    sqlTrans.RollBack();
                    throw ex;
                }
                sqlTrans.Commit();


                //轮乘机组退勤机组转预备

                if (finishedPlan)
                {
                    if (nTrainmanJiaoluType == 3)
                    {
                        LogClass.log(string.Format("计划完成开始设置备用队列{0}-{1},{2}", trainmanJiaoluGUID, trainmanJiaoluName, strGroupGUID));
                        TF.RunSafty.NamePlate.LCGroup.DeleteOrderGroup(trainmanJiaoluGUID, trainmanJiaoluName, strGroupGUID);
                    }
                }                

                //发送退勤消息
                TF.RunSafty.Plan.MD.EndworkMsgData msgData = new TF.RunSafty.Plan.MD.EndworkMsgData();
                msgData.cjjg = Convert.ToInt32(drinkdata.drinkResult);
                msgData.dtStartTime = TF.RunSafty.Plan.MD.MsgTool.DateTimeToMilliseconds(dtStartTime);
                msgData.dttime = TF.RunSafty.Plan.MD.MsgTool.DateTimeToMilliseconds(Convert.ToDateTime(endworkData.arriveTime));

                msgData.jiaoLuGUID = strTrainJiaoluGUID;
                msgData.jiaoLuName = strTrainJiaoluName;
                msgData.planGuid = endworkData.planID;
                msgData.strTrainNo = strTrainNo;
                msgData.tmGuid = endworkData.trainmanID;
                msgData.tmid =  strTrainmanNumber;
                msgData.tmname = strTrainmanName;
                msgData.Tmis = 0;
                ThinkFreely.RunSafty.AttentionMsg msg = msgData.ToMsg();
                msg.CreatMsg();


            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return true;
        }

        
    }
   

    public class RCTrainmanplan
    {
        #region 获取指定客户端在指定时间范围下的指定区段的人员计划
        public class Client_In
        {
            public string siteID = "";
            public string trainjiaoluID = "";
            public string begintime = "";
            public string endtime = "";
        }
        public class Client_Out
        {
            public string result = "";
            public string resultStr = "";
            public List<TF.RunSafty.Model.InterfaceModel.PlansOfClient> data;
        }
        public Client_Out GetTrainmanplanOfSite(string data)
        {
            Client_Out json = new Client_Out();
            Client_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Client_In>(data);
            TF.RunSafty.BLL.Plan.VIEW_Plan_Trainman bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_Trainman();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                string starttime = input.begintime;
                string endtime = input.endtime;
                string strTrainjiaoluGUID = input.trainjiaoluID;
                string clientID = input.siteID;
                json.data = bllPlan.GetPlans(starttime, endtime, strTrainjiaoluGUID);
                json.result = "0";
                json.resultStr = "提交成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
        #endregion
        #region 获取指定客户端在指定时间范围下的指定区段的已下发的人员计划
        public class Sent_In
        {
            public string siteID { get; set; }
            public string begintime { get; set; }
            public string endtime { get; set; }
            public string trainjiaoluID { get; set; }
        }
        public class Sent_Out
        {
            public string result;
            public string resultStr;
            public List<TF.RunSafty.Model.InterfaceModel.PlansOfClient> data;
        }
        public Sent_Out GetSentPlanOfTrainman(string data)
        {
            Sent_Out json = new Sent_Out();
            Sent_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Sent_In>(data);
            TF.RunSafty.BLL.Plan.VIEW_Plan_Trainman bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_Trainman();
            try
            {
                string starttime = input.begintime;
                string endtime = input.endtime;
                string strTrainjiaoluGUID = input.trainjiaoluID;
                string clientID = input.siteID;
                List<TF.RunSafty.Model.InterfaceModel.PlansOfClient> plans = bllPlan.GetTrainjiaoluPlansSentOfClient(clientID, starttime, endtime, strTrainjiaoluGUID);
                json.data = plans;
                json.result = "0";
                json.resultStr = "提交成功";

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
        #endregion
        #region 获取指定客户端的出勤计划列表
        public class BeginWorkList_In
        {
            public string siteID { get; set; }
            public string begintime { get; set; }
            public string endtime { get; set; }
        }
        public class BeginWorkList_Out
        {
            public string result;
            public string resultStr;
            public List<TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient> data;
        }
        public BeginWorkList_Out GetBeginWorkList(string data)
        {
            BeginWorkList_Out json = new BeginWorkList_Out();
            BeginWorkList_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<BeginWorkList_In>(data);
            TF.RunSafty.BLL.Plan.VIEW_Plan_BeginWork bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_BeginWork();
            try
            {
                string starttime = input.begintime;
                string endtime = input.endtime;
                string clientID = input.siteID;
                List<TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient> plans = bllPlan.GetChuqinPlansOfSite(clientID, starttime, endtime);
                
                json.data = plans;
                json.result = "0";
                json.resultStr = "提交成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
        #endregion
        #region 获取客户端下的退勤计划列表
        public class EndWorkList_In
        {
            public string siteID { get; set; }
            public string begintime { get; set; }
            public string endtime { get; set; }
            public int showAll { get; set; }
        }
        public class EndWorkList_Out
        {
            public string result;
            public string resultStr;
            public List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> data;
        }
        public EndWorkList_Out GetEndWorkList(string data)
        {
            EndWorkList_Out json = new EndWorkList_Out();
            EndWorkList_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<EndWorkList_In>(data);
            TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork_Full bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork_Full();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                string starttime = input.begintime;
                string endtime = input.endtime;
                string clientID = input.siteID;
                int isAll = input.showAll;
                List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> plans = bllPlan.GetTuiqinPlansOfSite(clientID, starttime, endtime, isAll);
                json.data = plans;
                json.result = "0";
                json.resultStr = "提交成功";

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
        #endregion
        #region 获取指定人员在指定客户端下的出勤计划
        public class BeginWork_In
        {
            public string siteID { get; set; }
            public string trainmanID { get; set; }
        }
        public class BeginWork_Out
        {
            public string result;
            public string resultStr;
            public TrainmanPlan data;
        }
        public BeginWork_Out GetBeginWorkOfTrainman(string data)
        {
            BeginWork_Out json = new BeginWork_Out();
            BeginWork_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<BeginWork_In>(data);
            TF.RunSafty.BLL.Plan.VIEW_Plan_BeginWork bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_BeginWork();
            try
            {
                string clientID = input.siteID;
                string strTrainmanid = input.trainmanID;
                // List<TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient> plans = bllPlan.GetChuqinPlansOfTrainmanInSite(clientID, strTrainmanid);
                List<TF.RunSafty.Model.VIEW_Plan_BeginWork> vplans = bllPlan.GetBeginWorkOfTrainmanInSite(clientID, strTrainmanid);
                List<TrainmanPlan> plans = GetPlanList(vplans);
                if (plans != null && plans.Count > 0)
                {
                    json.data = plans[0];
                    json.result = "0";
                    json.resultStr = "提交成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "没有该乘务员的出勤计划";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }

        private List<TrainmanPlan> GetPlanList(List<TF.RunSafty.Model.VIEW_Plan_BeginWork> vPlans)
        {
            List<TrainmanPlan> lPlans = new List<TrainmanPlan>();
            TrainmanPlan clientPlan = null;
            if (vPlans != null)
            {
                foreach (TF.RunSafty.Model.VIEW_Plan_BeginWork plan in vPlans)
                {
                    clientPlan = new TrainmanPlan();
                    ChuqinGroup chuqinGroup = new ChuqinGroup();
                    clientPlan.chuqinGroup = chuqinGroup;
                    DutyPlace cPlace = new DutyPlace();
                    chuqinGroup.group = new NameGroup();
                    chuqinGroup.group.place = cPlace;
                    cPlace.placeID = plan.strPlaceID;
                    cPlace.placeName = plan.strPlaceName;
                    chuqinGroup.group.groupID = plan.strGroupGUID;
                    chuqinGroup.group.station = new Station();
                    chuqinGroup.group.station.stationID = plan.strStationGUID;
                    chuqinGroup.group.station.stationName = plan.strStationName;
                    if (plan.strStationNumber.HasValue)
                    {
                        chuqinGroup.group.station.stationNumber = plan.strStationNumber.ToString();
                    }
                    chuqinGroup.group.trainman1 = new TF.RunSafty.Plan.Trainman();
                    chuqinGroup.group.trainman1.ABCD = plan.strABCD1;
                    if (plan.nDriverType1.HasValue)
                    {
                        chuqinGroup.group.trainman1.driverTypeID = plan.nDriverType1.Value;
                    }
                    if (plan.isKey1.HasValue)
                    {
                        chuqinGroup.group.trainman1.isKey = plan.isKey1.Value;
                    }
                    if (plan.nPostID1.HasValue)
                    {
                        chuqinGroup.group.trainman1.postID = plan.nPostID1.Value;
                    }
                    chuqinGroup.group.trainman1.trainmanID = plan.strTrainmanGUID1;
                    chuqinGroup.group.trainman1.trainmanName = plan.strTrainmanName1;
                    chuqinGroup.group.trainman1.trainmanNumber = plan.strTrainmanNumber1;
                    chuqinGroup.group.trainman2 = new TF.RunSafty.Plan.Trainman();
                    chuqinGroup.group.trainman2.ABCD = plan.strABCD2;
                    if (plan.nDriverType2.HasValue)
                    {
                        chuqinGroup.group.trainman2.driverTypeID = plan.nDriverType2.Value;
                    }
                    if (plan.isKey2.HasValue)
                    {
                        chuqinGroup.group.trainman2.isKey = plan.isKey2.Value;
                    }
                    if (plan.nPostID2.HasValue)
                    {
                        chuqinGroup.group.trainman2.postID = plan.nPostID2.Value;
                    }
                    chuqinGroup.group.trainman2.trainmanID = plan.strTrainmanGUID2;
                    chuqinGroup.group.trainman2.trainmanName = plan.strTrainmanName2;
                    chuqinGroup.group.trainman2.trainmanNumber = plan.strTrainmanNumber2;
                    chuqinGroup.group.trainman3 = new TF.RunSafty.Plan.Trainman();
                    chuqinGroup.group.trainman3.ABCD = plan.strABCD3;
                    if (plan.nDriverType3.HasValue)
                    {
                        chuqinGroup.group.trainman3.driverTypeID = plan.nDriverType3.Value;
                    }
                    if (plan.isKey3.HasValue)
                    {
                        chuqinGroup.group.trainman3.isKey = plan.isKey3.Value;
                    }
                    if (plan.nPostID3.HasValue)
                    {
                        chuqinGroup.group.trainman3.postID = plan.nPostID3.Value;
                    }
                    chuqinGroup.group.trainman3.trainmanID = plan.strTrainmanGUID3;
                    chuqinGroup.group.trainman3.trainmanName = plan.strTrainmanName3;
                    chuqinGroup.group.trainman3.trainmanNumber = plan.strTrainmanNumber3;
                    chuqinGroup.group.trainman4 = new TF.RunSafty.Plan.Trainman();
                    chuqinGroup.group.trainman4.ABCD = plan.strABCD4;
                    if (plan.nDriverType4.HasValue)
                    {
                        chuqinGroup.group.trainman4.driverTypeID = plan.nDriverType4.Value;
                    }
                    if (plan.isKey4.HasValue)
                    {
                        chuqinGroup.group.trainman4.isKey = plan.isKey4.Value;
                    }
                    if (plan.nPostID4.HasValue)
                    {
                        chuqinGroup.group.trainman4.postID = plan.nPostID4.Value;
                    }
                    chuqinGroup.group.trainman4.trainmanID = plan.strTrainmanGUID4;
                    chuqinGroup.group.trainman4.trainmanName = plan.strTrainmanName4;
                    chuqinGroup.group.trainman4.trainmanNumber = plan.strTrainmanNumber4;
                    clientPlan.icCheckResult = plan.strICCheckResult;
                    TF.RunSafty.Plan.TrainPlan trainPlan = new TrainPlan();
                    clientPlan.trainPlan = trainPlan;
                    if (plan.dtCreateTime.HasValue)
                    {
                        trainPlan.createTime = plan.dtCreateTime.Value;
                    }
                    trainPlan.createSiteGUID = plan.strCreateSiteGUID;
                    trainPlan.createSiteName = plan.strCreateSiteName;
                    trainPlan.createUserGUID = plan.strCreateUserGUID;
                    trainPlan.createUserName = plan.strCreateUserName;
                    if (plan.nDragType.HasValue)
                    {
                        trainPlan.dragTypeID = plan.nDragType.ToString();
                    }
                    trainPlan.endStationID = plan.strEndStation;
                    trainPlan.endStationName = plan.strEndStationName;
                    if (plan.nKehuoID.HasValue)
                    {
                        trainPlan.kehuoID = plan.nKehuoID.ToString();
                    }
                    trainPlan.kehuoName = plan.strKehuoName;
                    trainPlan.mainPlanGUID = plan.strMainPlanGUID;
                    trainPlan.placeID = plan.strPlaceID;
                    trainPlan.placeName = plan.strPlaceName;
                    trainPlan.planID = plan.strTrainPlanGUID;
                    if (plan.nPlanState.HasValue)
                    {
                        trainPlan.planStateID = plan.nPlanState.Value;
                    }
                    trainPlan.planStateName = plan.strPlanStateName;
                    if (plan.nPlanType.HasValue)
                    {
                        trainPlan.planTypeID = plan.nPlanType.ToString();
                    }
                    trainPlan.planTypeName = plan.strPlanTypeName;
                    if (plan.nRemarkType.HasValue)
                    {
                        trainPlan.remarkTypeID = plan.nRemarkType.ToString();
                    }
                    trainPlan.remarkTypeName = plan.strRemarkTypeName;

                    trainPlan.startStationID = plan.strStartStation;
                    trainPlan.startStationName = plan.strStartStationName;
                    trainPlan.dragTypeName = plan.nDragTypeName;
                    trainPlan.planID = plan.strTrainPlanGUID;
                    trainPlan.trainJiaoluGUID = plan.strTrainJiaoluGUID;
                    trainPlan.trainJiaoluName = plan.strTrainJiaoluName;
                    trainPlan.strTrainPlanGUID = plan.strTrainPlanGUID;
                    if (plan.dtRealStartTime.HasValue)
                    {
                        trainPlan.realStartTime = plan.dtRealStartTime.Value;
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        trainPlan.startTime = plan.dtStartTime.Value;
                    }
                    if (plan.dtFirstStartTime.HasValue)
                    {
                        trainPlan.firstStartTime = plan.dtFirstStartTime.Value;
                    }
                    if (plan.dtChuQinTime.HasValue)
                    {
                        clientPlan.beginWorkTime = plan.dtChuQinTime.Value;
                        clientPlan.trainPlan.kaiCheTime = plan.dtChuQinTime.Value;
                    }
                    else
                    {
                    }
                    if (plan.nTrainmanTypeID.HasValue)
                    {
                        trainPlan.trainmanTypeID = plan.nTrainmanTypeID.ToString();
                    }
                    trainPlan.trainNo = plan.strTrainNo;
                    trainPlan.trainNumber = plan.strTrainNumber;
                    trainPlan.trainTypeName = plan.strTrainTypeName;
                    trainPlan.trainmanTypeName = plan.strTrainmanTypeName;
                   
                    if (plan.nVerifyID1.HasValue)
                    {
                        chuqinGroup.verifyID1 = plan.nVerifyID1.Value;
                    }
                    chuqinGroup.testAlcoholInfo1 = new TestAlcoholInfo();
                    chuqinGroup.testAlcoholInfo1.picture = "";
                    if (plan.dtTestTime1.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo1.testTime = plan.dtTestTime1.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult1.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo1.testAlcoholResult = plan.nDrinkResult1.Value;
                    }

                    if (plan.nVerifyID2.HasValue)
                    {
                        chuqinGroup.verifyID2 = plan.nVerifyID2.Value;
                    }
                    chuqinGroup.testAlcoholInfo2 = new TestAlcoholInfo();
                    chuqinGroup.testAlcoholInfo2.picture = "";
                    if (plan.dtTestTime2.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo2.testTime = plan.dtTestTime2.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult2.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo2.testAlcoholResult = plan.nDrinkResult2.Value;
                    }

                    if (plan.nVerifyID3.HasValue)
                    {
                        chuqinGroup.verifyID3 = plan.nVerifyID3.Value;
                    }
                    chuqinGroup.testAlcoholInfo3 = new TestAlcoholInfo();
                    chuqinGroup.testAlcoholInfo3.picture = "";
                    if (plan.dtTestTime3.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo3.testTime = plan.dtTestTime3.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult3.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo3.testAlcoholResult = plan.nDrinkResult3.Value;
                    }

                    if (plan.nVerifyID4.HasValue)
                    {
                        chuqinGroup.verifyID4 = plan.nVerifyID4.Value;
                    }
                    chuqinGroup.testAlcoholInfo4 = new TestAlcoholInfo();
                    chuqinGroup.testAlcoholInfo4.picture = "";
                    if (plan.dtTestTime4.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo4.testTime = plan.dtTestTime4.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult4.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo4.testAlcoholResult = plan.nDrinkResult4.Value;
                    }
                    lPlans.Add(clientPlan);
                }
            }
            return lPlans;
        }

        #endregion
        #region 获取指定人员在指定客户端下的退勤计划

        public class EndWork_In
        {
            public string siteID { get; set; }
            public string trainmanID { get; set; }
        }
        public class EndWork_Out
        {
            public string result;
            public string resultStr;
            public TuiQinPlan data;
        }
        public EndWork_Out GetEndWorkOfTrainman(string data)
        {
            EndWork_Out json = new EndWork_Out();
            EndWork_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<EndWork_In>(data);
            TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork_Full bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork_Full();
            try
            {
                string clientID = input.siteID;
                string strTrainmanGUID = input.trainmanID;
                List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> plans = bllPlan.GetEndWorkOfTrainmanInSite(clientID, strTrainmanGUID);
                if (plans != null && plans.Count > 0)
                {
                    json.data = GetPlanList(plans)[0];
                    json.result = "0";
                    json.resultStr = "提交成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "没有改乘务员的退勤计划";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
        private List<TuiQinPlan> GetPlanList(List<TF.RunSafty.Model.VIEW_Plan_EndWork_Full> vPlans)
        {
            List<TuiQinPlan> lPlans = new List<TuiQinPlan>();
            TuiQinPlan clientPlan = null;
            if (vPlans != null)
            {
                foreach (TF.RunSafty.Model.VIEW_Plan_EndWork_Full plan in vPlans)
                {
                    clientPlan = new TuiQinPlan();
                    TuiQinGroup tuiqinGroup = new TuiQinGroup();
                    clientPlan.tuiqinGroup = tuiqinGroup;
                    tuiqinGroup.group = new NameGroup();
                    DutyPlace cPlace = new DutyPlace();
                    tuiqinGroup.group.place = cPlace;
                    cPlace.placeID = plan.strPlaceID;
                    cPlace.placeName = plan.strPlaceName;
                    tuiqinGroup.group.groupID = plan.strGroupGUID;
                    clientPlan.trainPlan = new TrainPlan();
                    clientPlan.trainPlan.createSiteGUID = plan.strCreateSiteGUID;
                    clientPlan.trainPlan.createSiteName = plan.strCreateSiteName;
                    if (plan.dtCreateTime.HasValue)
                    {
                        clientPlan.trainPlan.createTime = plan.dtCreateTime.Value;
                    }
                    clientPlan.trainPlan.createUserGUID = plan.strCreateUserGUID;
                    clientPlan.trainPlan.createUserName = plan.strCreateUserName;
                    clientPlan.trainPlan.dragTypeID = plan.nDragType.ToString();
                    clientPlan.trainPlan.dragTypeName = plan.nDragTypeName;
                    clientPlan.trainPlan.endStationID = plan.strEndStation;
                    clientPlan.trainPlan.endStationName = plan.strEndStationName;
                    clientPlan.trainPlan.kehuoID = plan.nKehuoID.ToString();
                    clientPlan.trainPlan.kehuoName = plan.strKehuoName;
                    clientPlan.trainPlan.mainPlanGUID = plan.strMainPlanGUID;
                    clientPlan.trainPlan.placeID = plan.strPlaceID;
                    clientPlan.trainPlan.placeName = plan.strPlaceName;
                    if (plan.nPlanState.HasValue)
                    {
                        clientPlan.trainPlan.planStateID = plan.nPlanState.Value;
                    }
                    clientPlan.trainPlan.planStateName = plan.strPlanStateName;
                    clientPlan.trainPlan.planTypeID = plan.nPlanType.ToString();
                    clientPlan.trainPlan.planTypeName = plan.strPlanTypeName;
                    clientPlan.trainPlan.remarkTypeID = plan.nRemarkType.ToString();
                    clientPlan.trainPlan.remarkTypeName = plan.strRemarkTypeName;
                    clientPlan.trainPlan.startStationID = plan.strStartStation;
                    clientPlan.trainPlan.startStationName = plan.strStartStationName;
                    clientPlan.trainPlan.mainPlanGUID = plan.strMainPlanGUID;
                    clientPlan.trainPlan.planID = plan.strTrainPlanGUID;
                    clientPlan.trainPlan.strTrainPlanGUID = plan.strTrainPlanGUID;
                    clientPlan.trainPlan.trainJiaoluGUID = plan.strTrainJiaoluGUID;
                    clientPlan.trainPlan.trainJiaoluName = plan.strTrainJiaoluName;
                    if (plan.dtLastArriveTime.HasValue)
                    {
                        clientPlan.trainPlan.lastArriveTime = plan.dtLastArriveTime.Value;
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        clientPlan.trainPlan.startTime = plan.dtStartTime.Value;
                    }
                    if (plan.dtRealStartTime.HasValue)
                    {
                        clientPlan.trainPlan.realStartTime = plan.dtRealStartTime.Value;
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        clientPlan.trainPlan.startTime = plan.dtStartTime.Value;
                    }
                    if (plan.dtFirstStartTime.HasValue)
                    {
                        clientPlan.trainPlan.firstStartTime = plan.dtFirstStartTime.Value;
                    }
                    if (plan.dtChuQinTime.HasValue)
                    {
                        clientPlan.beginWorkTime = plan.dtChuQinTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        clientPlan.trainPlan.kaiCheTime = plan.dtChuQinTime.Value;
                    }
                    else
                    {
                        clientPlan.beginWorkTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    clientPlan.trainPlan.trainmanTypeID = plan.nTrainmanTypeID.ToString();
                    clientPlan.trainPlan.trainmanTypeName = plan.strTrainmanTypeName;
                    clientPlan.trainPlan.trainNo = plan.strTrainNo;
                    clientPlan.trainPlan.trainNumber = plan.strTrainNumber;
                    clientPlan.trainPlan.trainTypeName = plan.strTrainTypeName;
                    tuiqinGroup.group.station = new Station();
                    tuiqinGroup.group.station.stationID = plan.strStationGUID;
                    tuiqinGroup.group.station.stationName = plan.strStationName;
                    tuiqinGroup.group.station.stationNumber = plan.strStationNumber.ToString();
                    tuiqinGroup.group.trainman1 = new TF.RunSafty.Plan.Trainman();
                    tuiqinGroup.group.trainman1.ABCD = plan.strABCD1;
                    if (plan.nDriverType1.HasValue)
                    {
                        tuiqinGroup.group.trainman1.driverTypeID = plan.nDriverType1.Value;
                    }
                    if (plan.isKey1.HasValue)
                    {
                        tuiqinGroup.group.trainman1.isKey = plan.isKey1.Value;
                    }
                    if (plan.nPostID1.HasValue)
                    {
                        tuiqinGroup.group.trainman1.postID = plan.nPostID1.Value;
                    }
                    tuiqinGroup.group.trainman1.trainmanID = plan.strTrainmanGUID1;
                    tuiqinGroup.group.trainman1.trainmanName = plan.strTrainmanName1;
                    tuiqinGroup.group.trainman1.trainmanNumber = plan.strTrainmanNumber1;
                    tuiqinGroup.group.trainman2 = new TF.RunSafty.Plan.Trainman();
                    tuiqinGroup.group.trainman2.ABCD = plan.strABCD2;
                    if (plan.nDriverType2.HasValue)
                    {
                        tuiqinGroup.group.trainman2.driverTypeID = plan.nDriverType2.Value;
                    }
                    if (plan.isKey2.HasValue)
                    {
                        tuiqinGroup.group.trainman2.isKey = plan.isKey2.Value;
                    }
                    if (plan.nPostID2.HasValue)
                    {
                        tuiqinGroup.group.trainman2.postID = plan.nPostID2.Value;
                    }
                    tuiqinGroup.group.trainman2.trainmanID = plan.strTrainmanGUID2;
                    tuiqinGroup.group.trainman2.trainmanName = plan.strTrainmanName2;
                    tuiqinGroup.group.trainman2.trainmanNumber = plan.strTrainmanNumber2;
                    tuiqinGroup.group.trainman3 = new TF.RunSafty.Plan.Trainman();
                    tuiqinGroup.group.trainman3.ABCD = plan.strABCD3;
                    if (plan.nDriverType3.HasValue)
                    {
                        tuiqinGroup.group.trainman3.driverTypeID = plan.nDriverType3.Value;
                    }
                    if (plan.isKey3.HasValue)
                    {
                        tuiqinGroup.group.trainman3.isKey = plan.isKey3.Value;
                    }
                    if (plan.nPostID3.HasValue)
                    {
                        tuiqinGroup.group.trainman3.postID = plan.nPostID3.Value;
                    }
                    tuiqinGroup.group.trainman3.trainmanID = plan.strTrainmanGUID3;
                    tuiqinGroup.group.trainman3.trainmanName = plan.strTrainmanName3;
                    tuiqinGroup.group.trainman3.trainmanNumber = plan.strTrainmanNumber3;
                    tuiqinGroup.group.trainman4 = new TF.RunSafty.Plan.Trainman(); ;
                    tuiqinGroup.group.trainman4.ABCD = plan.strABCD4;
                    if (plan.nDriverType4.HasValue)
                    {
                        tuiqinGroup.group.trainman4.driverTypeID = plan.nDriverType4.Value;
                    }
                    if (plan.isKey4.HasValue)
                    {
                        tuiqinGroup.group.trainman4.isKey = plan.isKey4.Value;
                    }
                    if (plan.nPostID4.HasValue)
                    {
                        tuiqinGroup.group.trainman4.postID = plan.nPostID4.Value;
                    }
                    tuiqinGroup.group.trainman4.trainmanID = plan.strTrainmanGUID4;
                    tuiqinGroup.group.trainman4.trainmanName = plan.strTrainmanName4;
                    tuiqinGroup.group.trainman4.trainmanNumber = plan.strTrainmanNumber4;

                    tuiqinGroup.turnMinutes = plan.nTurnMinutes.ToString();
                    tuiqinGroup.turnAlarmMinutes = plan.nTurnAlarmMinutes.ToString();
                    tuiqinGroup.isOver = plan.bIsOver.ToString();
                    tuiqinGroup.signed = plan.bSigned.ToString();
                    if (plan.dtTurnStartTime.HasValue)
                    {
                        tuiqinGroup.turnStartTime = plan.dtTurnStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    //clientPlan.icCheckResult = plan.strICCheckResult;


                    if (plan.nVerifyID1.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID1 = plan.nVerifyID1.Value;
                    }
                    clientPlan.tuiqinGroup.testAlcoholInfo1 = new TestAlcoholInfo();
                    clientPlan.tuiqinGroup.testAlcoholInfo1.picture = "";
                    if (plan.dtTestTime1.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo1.testTime = plan.dtTestTime1.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult1.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo1.testAlcoholResult = plan.nDrinkResult1.Value;
                    }

                    if (plan.nVerifyID2.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID2 = plan.nVerifyID2.Value;
                    }
                    clientPlan.tuiqinGroup.testAlcoholInfo2 = new TestAlcoholInfo();
                    clientPlan.tuiqinGroup.testAlcoholInfo2.picture = "";
                    if (plan.dtTestTime2.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo2.testTime = plan.dtTestTime2.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult2.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo2.testAlcoholResult = plan.nDrinkResult2.Value;
                    }


                    if (plan.nVerifyID3.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID3 = plan.nVerifyID3.Value;
                    }
                    clientPlan.tuiqinGroup.testAlcoholInfo3 = new TestAlcoholInfo();
                    clientPlan.tuiqinGroup.testAlcoholInfo3.picture = "";
                    if (plan.dtTestTime3.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo3.testTime = plan.dtTestTime3.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult3.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo3.testAlcoholResult = plan.nDrinkResult3.Value;
                    }

                    if (plan.nVerifyID4.HasValue)
                    {
                        clientPlan.tuiqinGroup.verifyID4 = plan.nVerifyID4.Value;
                    }
                    clientPlan.tuiqinGroup.testAlcoholInfo4 = new TestAlcoholInfo();
                    clientPlan.tuiqinGroup.testAlcoholInfo4.picture = "";
                    if (plan.dtTestTime4.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo4.testTime = plan.dtTestTime4.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult4.HasValue)
                    {
                        clientPlan.tuiqinGroup.testAlcoholInfo4.testAlcoholResult = plan.nDrinkResult4.Value;
                    }

                    lPlans.Add(clientPlan);
                }
            }
            return lPlans;
        }
        #endregion
        #region 执行出勤

        public class RsDrink
        {
            public string strGUID = "";
            public string strTrainmanGUID = "";
            public string nDrinkResult = "";
            public DateTime dtCreateTime;
            public string strAreaGUID = "";
            public string strDutyGUID = "";
            public string nVerifyID = "";
            public string strWorkID = "";
            public string nWorkTypeID = "";
            public string strImagePath = "";
            public int Alcoholicity = 0;
            public string strSiteGUID = "";
            public string strSiteName = "";
        }
        public class BeginWork
        {
            public string strBeginWorkGUID = "";
            public string strTrainPlanGUID = "";
            public string strTrainmanGUID = "";
            public DateTime dtCreateTime;
            public string nVerifyID = "";
            public string strStationGUID = "";
            public string strRemark = "";
        }
        public class Execute_In
        {
            public string TrainmanGUID = "";
            public string DutyUserGUID = "";
            public BeginWork beginwork;
            public RsDrink drink;
        }
        public class Execute_out
        {
            public string result;
            public string resultStr;
        }


        private DataTable GetTrainman(string strTrainmanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select strTrainmanNumber,strTrainmanName from TAB_Org_Trainman where strTrainmanGUID = '" + strTrainmanGUID + "'");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            return dt;
        }

        public class SubTestAlcoholInfo
        {
            public int TestAlcoholResult = 0;
            public int VerfiyID = 0;
            public DateTime TestTime;
            public string TestPicturePath = "";
            public int Alcoholicity = 0;

        }

        /// <summary>
        /// 保存出勤测酒步骤数据
        /// </summary>
        /// <param name="command"></param>
        /// <param name="strTrainPlanGUID"></param>
        /// <param name="strTrainmanGUID"></param>
        /// <param name="strTrainmanNumber"></param>
        /// <param name="strTrainmanName"></param>
        private void SaveDrinkStepData(SqlCommand command, string strTrainPlanGUID, string strTrainmanGUID, string strTrainmanNumber, string strTainmanName, RsDrink drinkInfo)
        {
            DBPlan_Beginwork_Step db = new DBPlan_Beginwork_Step();
            Plan_Beginwork_Step step = new Plan_Beginwork_Step();


            SubTestAlcoholInfo alcoholInfo = new SubTestAlcoholInfo();
            alcoholInfo.TestAlcoholResult = int.Parse(drinkInfo.nDrinkResult);
            alcoholInfo.VerfiyID = int.Parse(drinkInfo.nVerifyID);
            alcoholInfo.TestTime = drinkInfo.dtCreateTime;
            alcoholInfo.TestPicturePath = drinkInfo.strImagePath;
            alcoholInfo.Alcoholicity = drinkInfo.Alcoholicity;


            step.nStepID = 1003;
            step.nStepResultID = 1;
            step.strTrainmanGUID = strTrainmanGUID;
            step.strTrainmanNumber = strTrainmanNumber;
            step.strTrainmanName = strTainmanName;
            step.strTrainPlanGUID = strTrainPlanGUID;
            step.dtEventTime = drinkInfo.dtCreateTime;
            step.dtEventEndTime = drinkInfo.dtCreateTime;

            // step.strStepResultText = string.Format("{\"TestAlcoholResult\":\"{0}\",\"VerfiyID\":\"{1}\",\"TestTime\":\"{2}\",\"TestPicturePath\":\"{3}\",\"Alcoholicity\":\"{4}\"}",
            //            drinkInfo.nDrinkResult, drinkInfo.nVerifyID, drinkInfo.dtCreateTime, drinkInfo.strImagePath, drinkInfo.Alcoholicity);
            step.strStepResultText = Newtonsoft.Json.JsonConvert.SerializeObject(alcoholInfo);

            db.Add(step, command);

        }
        /// <summary>
        /// 执行出勤
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        
        public Execute_out ExecuteBeginWork(string input)
        {
            Execute_out json = new Execute_out();
            Execute_In model = Newtonsoft.Json.JsonConvert.DeserializeObject<Execute_In>(input);
            string beginWorkID = Guid.NewGuid().ToString();
            BeginWork beginwork = model.beginwork;
            RsDrink drinkInfo = model.drink;
            string strTrainmanGUID = model.TrainmanGUID;
            string strDutyUserGUID = model.DutyUserGUID;
            int nTurnOrder = 0;
            string strTrainJiaoluGUID = "";
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            connection.ConnectionString = SqlHelper.ConnString;
            beginwork.strTrainmanGUID = strTrainmanGUID;
            drinkInfo.strDutyGUID = strDutyUserGUID;
            beginwork.nVerifyID = drinkInfo.nVerifyID;
            string strSql = "select dtStartTime ,strTrainNo,strTrainTypeName,strTrainNumber,strTrainJiaoluName from VIEW_Plan_Train where strTrainPlanGUID = @strTrainPlanGUID";
            SqlParameter[] sqlParamsPlan = new SqlParameter[] { 
                new SqlParameter("strTrainPlanGUID",beginwork.strTrainPlanGUID)
            };
            string strTrainNo = "";
            string strTrainTypeName = "";
            string strTrainNumber = "";
            string strTrainJiaoluName = "";
            DateTime dtStartTime = DateTime.MinValue;
            DataTable dtPlan = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParamsPlan).Tables[0];
            if (dtPlan.Rows.Count > 0)
            {
                strTrainNo = dtPlan.Rows[0]["strTrainNo"].ToString();
                strTrainTypeName = dtPlan.Rows[0]["strTrainTypeName"].ToString();
                strTrainNumber = dtPlan.Rows[0]["strTrainNumber"].ToString();
                strTrainJiaoluName = dtPlan.Rows[0]["strTrainJiaoluName"].ToString();
                dtStartTime = Convert.ToDateTime(dtPlan.Rows[0]["strTrainTypeName"]);
            }
            try
            {
                connection.Open();
                string strTrainmanNumber = "";
                string strTrainmanName = "";
                DataTable dt = GetTrainman(beginwork.strTrainmanGUID);
                if (dt.Rows.Count > 0)
                {
                    strTrainmanNumber = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanNumber"]);
                    strTrainmanName = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanName"]);
                }

                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                nTurnOrder = GetTurnOrder(command, strTrainJiaoluGUID);
                //保存出勤记录
                AddOrUpdateBeginWork(command, beginwork);
                //保存测酒记录
                SaveDrinkInfo(command, drinkInfo, beginwork, drinkInfo.strDutyGUID);
                //修改计划状态,设置为已出勤
                SavePlanState(command, (int)TF.RunSafty.Model.InterfaceModel.TRsPlanState.psBeginWork, beginwork.strTrainPlanGUID, nTurnOrder);
                //更新计划的实际出勤时间
                UpdateBeginWorkTime(command,beginwork.strTrainPlanGUID);
                SaveDrinkStepData(command, beginwork.strTrainPlanGUID, beginwork.strTrainmanGUID, strTrainmanNumber, strTrainmanName, drinkInfo);
                transaction.Commit();

      
                //发送出勤消息
                TF.RunSafty.Plan.MD.BeginworkMsgData msgData = new TF.RunSafty.Plan.MD.BeginworkMsgData();
                msgData.cjjg = Convert.ToInt32(drinkInfo.nDrinkResult);
                msgData.dtStartTime = TF.RunSafty.Plan.MD.MsgTool.DateTimeToMilliseconds(dtStartTime);
                msgData.dttime = TF.RunSafty.Plan.MD.MsgTool.DateTimeToMilliseconds(beginwork.dtCreateTime);

                msgData.jiaoLuGUID = strTrainJiaoluGUID;
                msgData.jiaoLuName = strTrainJiaoluName;
                msgData.strTrainNo = strTrainNo;
                msgData.strTrainTypeName = strTrainTypeName;
                msgData.strTrainNumber = strTrainNumber;

                msgData.planGuid = beginwork.strTrainPlanGUID;
                
                msgData.tmGuid = beginwork.strTrainmanGUID;
                msgData.tmid = strTrainmanNumber;
                msgData.tmname = strTrainmanName;
                msgData.Tmis = 0;
                ThinkFreely.RunSafty.AttentionMsg msg = msgData.ToMsg();
                msg.CreatMsg();

                json.result = "0";
                json.resultStr = "出勤成功";
            }
            catch (Exception ex)
            { 
                json.result = "1";
                json.resultStr = "出勤失败";
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    TF.CommonUtility.LogClass.logex(ex2, "");
                }
                throw ex;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
                command.Dispose();
                connection.Dispose();
            }

            return json;
        }

        /// <summary>
        /// 获取出勤翻牌
        /// </summary>
        /// <param name="command"></param>
        /// <param name="strTrainJiaoluGUID"></param>
        /// <returns></returns>
        private int GetTurnOrder(SqlCommand command, string strTrainJiaoluGUID)
        {
            int nTurnOrder = 0;
            string sqlText = string.Format("select bIsBeginWorkFP from TAB_Base_TrainJiaolu where strTrainJiaoluGUID = '{0}'", strTrainJiaoluGUID);
            command.CommandText = sqlText;
            object count = command.ExecuteScalar();
            if (count != null)
            {
                int.TryParse(count.ToString(), out nTurnOrder);
            }
            return nTurnOrder;
        }
        /// <summary>
        /// 添加更新出勤记录
        /// </summary>
        /// <param name="command"></param>
        /// <param name="beginwork"></param>
        private void AddOrUpdateBeginWork(SqlCommand command, BeginWork beginwork)
        {
            string sqlText = string.Format("select * from TAB_Plan_BeginWork where strTrainPlanGUID = '{0}' and strTrainmanGUID = '{1}'", beginwork.strTrainPlanGUID, beginwork.strTrainmanGUID);
            command.CommandText = sqlText;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet set = new DataSet();
            adapter.Fill(set);
            DataTable table = set.Tables[0];
            if (table == null || table.Rows.Count == 0)
            {
                AddBeginWork(command, beginwork);
            }
            else
            {
                beginwork.strBeginWorkGUID = table.Rows[0]["strBeginWorkGUID"].ToString();
                UpdateBeginWork(command, beginwork);
            }
        }
        /// <summary>
        /// 添加出勤记录
        /// </summary>
        /// <param name="command"></param>
        /// <param name="beginwork"></param>
        private void AddBeginWork(SqlCommand command, BeginWork beginwork)
        {
            beginwork.strBeginWorkGUID = Guid.NewGuid().ToString();
            string sqlText = string.Format(@"insert into TAB_Plan_BeginWork 
values(@strBeginWorkGUID,@strTrainPlanGUID,@strTrainmanGUID,@dtCreateTime,@nVerifyID,@strStationGUID,@strRemark);");
            command.CommandText = sqlText;
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("strBeginWorkGUID",beginwork.strBeginWorkGUID ),
                new SqlParameter("strTrainPlanGUID",beginwork.strTrainPlanGUID),
                new SqlParameter("strTrainmanGUID",beginwork.strTrainmanGUID),
                new SqlParameter("dtCreateTime",beginwork.dtCreateTime),
                new SqlParameter("nVerifyID",beginwork.nVerifyID),
                new SqlParameter("strStationGUID",beginwork.strStationGUID),
                new SqlParameter("strRemark",beginwork.strRemark),
            };
            command.Parameters.Clear();
            command.Parameters.AddRange(parameters);
            command.ExecuteNonQuery();
        }
        /// <summary>
        /// 更新出勤记录
        /// </summary>
        /// <param name="command"></param>
        /// <param name="beginwork"></param>
        private void UpdateBeginWork(SqlCommand command, BeginWork beginwork)
        {

            string sqlText = string.Format(@"update TAB_Plan_BeginWork set 
nVerifyID=@nVerifyID,strRemark=@strRemark where strTrainPlanGUID=@strTrainPlanGUID and     
          strTrainmanGUID=@strTrainmanGUID

");
            SqlParameter[] parameters = new SqlParameter[] {
               new SqlParameter("nVerifyID",beginwork.nVerifyID),
               new SqlParameter("strRemark",beginwork.strRemark),
                new SqlParameter("strTrainPlanGUID",beginwork.strTrainPlanGUID) ,
                new SqlParameter("strTrainmanGUID",beginwork.strTrainmanGUID)
            };
            command.CommandText = sqlText;
            command.Parameters.Clear();
            command.Parameters.AddRange(parameters);
            int count = command.ExecuteNonQuery();
        }

        /// <summary>
        /// 保存测酒记录
        /// </summary>
        /// <param name="drinkInfo"></param>
        /// <param name="beginwork"></param>
        /// <param name="DutyUserGUID"></param>
        /// <param name="beginWorkID"></param>
        private void SaveDrinkInfo(SqlCommand command, RsDrink drinkInfo, BeginWork beginwork, string DutyUserGUID)
        {
            //删除旧的测酒记录
            string sqlText = string.Format("update  TAB_Drink_Information set strWorkID=''  where strWorkID = '{0}' and nWorkTypeID='{1}' and strTrainmanGUID = '{2}'", beginwork.strBeginWorkGUID, 2, beginwork.strTrainmanGUID);
            command.CommandText = sqlText;
            int count = command.ExecuteNonQuery();

            sqlText = "select strGUID from tab_drink_information where strTrainmanGUID=@strTrainmanGUID and dtCreateTime=@dtCreateTime";
            SqlParameter[] sqlParamsDrink = new SqlParameter[] { 
                    new SqlParameter("strTrainmanGUID",beginwork.strTrainmanGUID),
                    new SqlParameter("dtCreateTime",drinkInfo.dtCreateTime)
                };
            string drinkGUID = "";
            object objDrink = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsDrink);
            if ((objDrink != null) && (!DBNull.Value.Equals(objDrink)))
            {
                drinkGUID = objDrink.ToString();
            }
            if (drinkGUID == "")
            {
                //插入新的测酒记录
                sqlText = "insert into TAB_Drink_Information(strGUID,strTrainmanGUID,  nDrinkResult,dtCreateTime,strAreaGUID,strDutyGUID,nVerifyID,strWorkID,nWorkTypeID,strImagePath) values (@strGUID,@strTrainmanGUID,@nDrinkResult,@dtCreateTime,@strAreaGUID,@strDutyGUID,@nVerifyID,@strWorkID,@nWorkTypeID,@strImagePath) ";
                SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("strGUID",Guid.NewGuid().ToString()),
                new SqlParameter("strTrainmanGUID",beginwork.strTrainmanGUID),
                new SqlParameter("nDrinkResult",drinkInfo.nDrinkResult),
                new SqlParameter("dtCreateTime",DateTime.Now),
                new SqlParameter("strAreaGUID",""),
                new SqlParameter("strDutyGUID",DutyUserGUID),
                new SqlParameter("nVerifyID",drinkInfo.nVerifyID),
                new SqlParameter("strWorkID",beginwork.strBeginWorkGUID),
                new SqlParameter("nWorkTypeID",2),
                new SqlParameter("strImagePath",drinkInfo.strImagePath)
                };
            
                command.CommandText = sqlText;
                command.Parameters.Clear();
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
            else
            {


                sqlText = "Update Tab_Drink_Information set strWorkID = @strWorkID,nWorkTypeID =nWorkTypeID where strGUID = @strGUID";
                SqlParameter[] sqlParamsDrinkU = new SqlParameter[] { 
                            new SqlParameter("strWorkID",beginwork.strBeginWorkGUID),
                            new SqlParameter("strWorkID",WORKTYPE.WORKTYPE_BEGIN),
                            new SqlParameter("strGUID",drinkGUID)
                        };
                command.CommandText = sqlText;
                command.Parameters.Clear();
                command.Parameters.AddRange(sqlParamsDrinkU);
                command.ExecuteNonQuery();
            }
            
        }

        /// <summary>
        /// 修改计划状态为出勤
        /// </summary>
        /// <param name="command"></param>
        /// <param name="nPlanState"></param>
        /// <param name="strTrainmanGUID"></param>
        /// <param name="nTurnOrder"></param>
        private void SavePlanState(SqlCommand command, int nPlanState, string strTrainPlanGUID, int nTurnOrder)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            string sqlText = string.Format(@"update TAB_Plan_Train set nPlanState={0} where strTrainPlanGUID='{1}' and     
          (select count(*) from VIEW_Plan_BeginWork where strTrainPlanGUID=TAB_Plan_Train.strTrainPlanGUID  and     
          ((strTrainmanGUID1 is null) or (strTrainmanGUID1 ='') or not(dtTestTime1 is null)) and    
            ((strTrainmanGUID2 is null) or (strTrainmanGUID2 = '') or not(dtTestTime2 is null))  and    
            ((strTrainmanGUID3 is null) or (strTrainmanGUID3 = '') or not(dtTestTime3 is null))   and    
            ((strTrainmanGUID4 is null) or (strTrainmanGUID4 = '') or not(dtTestTime4 is null))) > 0", nPlanState, strTrainPlanGUID);
            command.CommandText = sqlText;
            int count = command.ExecuteNonQuery();
        }
        /// <summary>
        /// 更新计划的实际出勤时间
        /// </summary>
        /// <param name="command"></param>
        /// <param name="strTrainPlanGUID"></param>
        private void UpdateBeginWorkTime(SqlCommand command, string strTrainPlanGUID)
        {
            string sqlText = string.Format(@"update TAB_Plan_Train set dtBeginWorkTime=getdate() where strTrainPlanGUID=@strTrainPlanGUID and dtBeginWorkTime is null");
            SqlParameter[] sqlParameters=new SqlParameter[]
            {
                new SqlParameter("strTrainPlanGUID",strTrainPlanGUID), 
            };
            command.CommandText = sqlText;
            command.Parameters.Clear();
            command.Parameters.AddRange(sqlParameters);
            int count = command.ExecuteNonQuery();
        }
        #endregion
        #region 执行退勤
        
        private class InputParam
        {
            public string siteID = "";
            public EndWorkData endwork { get; set; }
            public DrinkData drink { get; set; }
            public DutyInfo dutyUser { get; set; }

        }
        public class JsonModel
        {
            public int result;
            public string resultStr;
            public object data;
        }
        public JsonModel ExecuteEndWork(string input)
        {
            JsonModel jsonModel = new JsonModel();
            jsonModel.result = 1;
            jsonModel.resultStr = "数据提交失败：未找到指定的机组信息";
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                InputParam paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<InputParam>(input);

                RCEndWork rcEndWork = new RCEndWork();
                if (rcEndWork.Endwork(paramModel.siteID, paramModel.endwork, paramModel.drink, paramModel.dutyUser))
                {
                    jsonModel.result = 0;
                    jsonModel.resultStr = "提交成功";
                }
                else
                {

                    jsonModel.result = 1;
                }

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                jsonModel.resultStr = "提交失败" + ex.Message;
            }
            return jsonModel;
        }

       
        #endregion
        #region 获取指定人员的退勤计划

        public class TrainmanTuiqin_In
        {
            public string TrainmanGUID = string.Empty;
            public string SiteGUID = string.Empty;
        }

        public class TrainmanTuiqin_Out : JsonOutBase
        {
            public object data;
        }

        public TrainmanTuiqin_Out GetTrainmanTuiQinPlan(string input)
        {
            TrainmanTuiqin_Out json=new TrainmanTuiqin_Out();
            TrainmanTuiqin_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<TrainmanTuiqin_In>(input);
                string strSql = @"select top 1 * from VIEW_Plan_EndWork  
where strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID =@strSiteGUID) and 
     nPlanState =@nPlanState AND (strTrainmanGUID1 = @strTrainmanGUID or strTrainmanGUID2 =@strTrainmanGUID or strTrainmanGUID3 =@strTrainmanGUID or strTrainmanGUID4 = @strTrainmanGUID) order by dtStartTime desc";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strSiteGUID", model.SiteGUID),
                    new SqlParameter("nPlanState", (int) TRsPlanState.psBeginWork),
                    new SqlParameter("strTrainmanGUID", model.TrainmanGUID),
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];

                    TF.CommonUtility.JsonConvert.FormatDataRow(table, row);
                    json.data = table;
                    json.result = "0";
                    json.resultStr = "获取退勤计划成功";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "获取退勤计划失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }

       
        #endregion
        #region 根据GUID获取人员计划

        public class ByGUID_In
        {
            public string TrainPlanGUID = "";
        }
        public InterfaceOutPut GetTrainmanPlanByGUID(string data)
        {
            InterfaceOutPut outPut=new InterfaceOutPut();
            ByGUID_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<ByGUID_In>(data);
                string strTrainplanGUID = model.TrainPlanGUID;
                TF.RunSafty.BLL.Plan.VIEW_Plan_Trainman bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_Trainman();
                TF.RunSafty.Model.InterfaceModel.PlansOfClient plan = bllPlan.GetPlanByGUID(strTrainplanGUID);
                outPut.data = plan;
                outPut.result = 0;
                outPut.resultStr = "获取人员计划成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return outPut;
        }
        #region 根据计划GUID列表获取需要侯班的计划
        public class GuidList_In
        {
            public string TrainPlanGUIDList = "";
        }
        public InterfaceOutPut GetTrainmanPlanOfNeedRest(string data)
        {
            InterfaceOutPut outPut = new InterfaceOutPut();
            GuidList_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<GuidList_In>(data);
                string strTrainplanGUIDList = model.TrainPlanGUIDList;
                TF.RunSafty.BLL.Plan.VIEW_Plan_Trainman bllPlan = new TF.RunSafty.BLL.Plan.VIEW_Plan_Trainman();
                List<TF.RunSafty.Model.InterfaceModel.PlansOfClient> plans = bllPlan.GetPlansOfNeedRest(strTrainplanGUIDList);
                outPut.data = plans;
                outPut.result = 0;
                outPut.resultStr = "获取人员计划成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return outPut;
        }
        #endregion
        #endregion

        #region 执行出勤步骤
        public class InSaveBeginWorkStepData
        {
            public Plan_Beginwork_StepList beginWorkStepList;
        }

        /// <summary>
        /// 保存出勤步骤数据
        /// </summary>
        public InterfaceOutPut SaveBeginWorkStepData(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                output.result = 0;
                CommonJsonModel cjm = new CommonJsonModel(Data);
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                //InSaveBeginWorkStepData InData = javaScriptSerializer.Deserialize<InSaveBeginWorkStepData>(Data);
                List<Plan_Beginwork_Step> stepList = javaScriptSerializer.Deserialize<List<Plan_Beginwork_Step>>(Data);
                //Plan_Beginwork_StepList beginWorkStepList = javaScriptSerializer.Deserialize<Plan_Beginwork_StepList>(Data);
                //InSaveBeginWorkStepData InParam = javaScriptSerializer.Deserialize<InSaveBeginWorkStepData>(Data);
                DBPlan_Beginwork_Step dbStep = new DBPlan_Beginwork_Step();
                dbStep.AddList(stepList);
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.SaveBeginWorkStepData:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion
        #region 查询出勤步骤信息
        public class InGetBeginWorkStepData
        {
            //出勤计划guid
            public string strTrainPlanGUID;
            //步骤ID
            public int nStepID;
            //TrainmanID
            public string strTrainmanGUID;

        }

        public class OutGetBeginWorkStepData
        {
            //出勤步骤数据列表
            public Plan_Beginwork_StepList BeginWorkStepInfoList = new Plan_Beginwork_StepList();
        }

        /// <summary>
        /// 获取出勤步骤数据
        /// </summary>
        public InterfaceOutPut GetBeginWorkStepData(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                CommonJsonModel cjm = new CommonJsonModel(Data);
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetBeginWorkStepData InParams = javaScriptSerializer.Deserialize<InGetBeginWorkStepData>(Data);
                OutGetBeginWorkStepData OutParams = new OutGetBeginWorkStepData();
                DBPlan_Beginwork_Step db = new DBPlan_Beginwork_Step();
                Plan_Beginwork_StepQueryCondition cd = new Plan_Beginwork_StepQueryCondition();
                cd.strTrainPlanGUID = InParams.strTrainPlanGUID;
                cd.nStepID = InParams.nStepID;
                cd.strTrainmanGUID = InParams.strTrainmanGUID;
                OutParams.BeginWorkStepInfoList = db.GetDataList(cd);
                if (OutParams.BeginWorkStepInfoList != null)
                {
                    output.data = OutParams;
                }
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetBeginWorkStepData:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion
        #region 查询在线预警

        public class InGetZXYJInfoS
        {
            //司机工号
            public string strTrainmanNum;
            //计划guid
            public string strPlanGUID;
        }

        public class OutGetZXYJInfoS
        {
            //在线预警信息列表
            public ZXYJInfoList zxyjInfoList = new ZXYJInfoList();
        }

        /// <summary>
        /// 获取在线预警信息
        /// </summary>
        public InterfaceOutPut GetZXYJInfoS(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                CommonJsonModel cjm = new CommonJsonModel(Data);
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetZXYJInfoS InParams = javaScriptSerializer.Deserialize<InGetZXYJInfoS>(Data);
                OutGetZXYJInfoS OutParams = new OutGetZXYJInfoS();
                DBPlan_RunEvent_ZXYJ db = new DBPlan_RunEvent_ZXYJ();
                ZXYJQueryCondition condition = new ZXYJQueryCondition();
                condition.strTrainPlanGUID = InParams.strPlanGUID;
                output.data = null;
                OutParams.zxyjInfoList = db.GetDataList(condition);
                if (OutParams.zxyjInfoList.Count > 0)
                {
                    output.data = OutParams;
                }
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetZXYJInfoS:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion
    }

    public class LCTrainPlan
    {
        private Hashtable hash = null;
        public class planGUID
        {
            public string strTrainPlanGUID = "";
        }

        #region

        private void SetPlanValue(TF.RunSafty.Model.InterfaceModel.CreatedTrainPlan trainPlan, User user, Site site,
            TF.RunSafty.Model.TAB_Plan_Train plan)
        {

            DateTime dtStartTime, dtKaiCheTime;
            if (DateTime.TryParse(trainPlan.startTime, out dtStartTime))
            {
                plan.dtStartTime = dtStartTime;
            }
            if (!string.IsNullOrEmpty(trainPlan.dragTypeID))
            {
                plan.nDragType = int.Parse(trainPlan.dragTypeID);
            }
            if (!string.IsNullOrEmpty(trainPlan.kehuoID))
            {
                plan.nKehuoID = int.Parse(trainPlan.kehuoID);
            }
            if (!string.IsNullOrEmpty(trainPlan.planTypeID))
            {
                plan.nPlanType = int.Parse(trainPlan.planTypeID);
            }
            if (!string.IsNullOrEmpty(trainPlan.remarkTypeID))
            {
                plan.nRemarkType = int.Parse(trainPlan.remarkTypeID);
            }
            if (!string.IsNullOrEmpty(trainPlan.trainmanTypeID))
            {
                plan.nTrainmanTypeID = int.Parse(trainPlan.trainmanTypeID);
            }
            plan.strCreateSiteGUID = site.siteID;
            plan.strCreateUserGUID = user.userID;
            plan.strPlaceID = trainPlan.placeID;
            plan.strStartStation = trainPlan.startStationID;
            plan.strEndStation = trainPlan.endStationID;
            plan.strTrainJiaoluGUID = trainPlan.trainjiaoluID;
            plan.strTrainNo = trainPlan.trainNo;
            plan.strTrainNumber = trainPlan.trainNumber;
            plan.strTrainTypeName = trainPlan.trainTypeName;
            plan.dtCreateTime = DateTime.Now;
            plan.dtRealStartTime = plan.dtStartTime;
            string planGUID = Guid.NewGuid().ToString();
            plan.strTrainPlanGUID = planGUID;
            plan.strMainPlanGUID = trainPlan.strMainPlanGUID;
            if (DateTime.TryParse(trainPlan.kaiCheTime, out dtKaiCheTime))
            {
                plan.dtChuQinTime = dtKaiCheTime;
            }

            plan.strTrackNumber = trainPlan.strTrackNumber;
            plan.strWaiQinClientGUID = trainPlan.strWaiQinClientGUID;
            plan.strWaiQinClientNumber = trainPlan.strWaiQinClientNumber;
            plan.strWaiQinClientName = trainPlan.strWaiQinClientName;
        }
    
        #endregion
        #region 新建已编辑机车计划
        public class Add_In
        {
            public TF.RunSafty.Model.InterfaceModel.CreatedTrainPlan trainPlan;
            public User user;
            public Site site;
        }
        public class User
        {
            public string userID = "";
            public string userName = "";
        }
        public class Site
        {
            public string siteID = "";
            public string siteName = "";
        }
        public class PlanInfo
        {
            public string planID = "";
        }
        public class Add_Out
        {
            public string result = "";
            public string resultStr = "";
            public PlanInfo data = new PlanInfo();
        }

        public Add_Out AddEditable(string data)
        {
            Add_Out jsonModel = new Add_Out();
            Add_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Add_In>(data);
            TF.RunSafty.BLL.Plan.TAB_Plan_Train bllPlan = new TF.RunSafty.BLL.Plan.TAB_Plan_Train();
            TF.RunSafty.Model.TAB_Plan_Train plan = new TF.RunSafty.Model.TAB_Plan_Train();
            try
            {
                SetPlanValue(input.trainPlan, input.user, input.site, plan);

                plan.nPlanState = (int)TF.RunSafty.Model.InterfaceModel.TRsPlanState.psEdit;
                if (!string.IsNullOrEmpty(input.trainPlan.kaiCheTime))
                {
                    DateTime dtChuqinTime;
                    if (DateTime.TryParse(input.trainPlan.kaiCheTime, out dtChuqinTime))
                    {
                        plan.dtChuQinTime = dtChuqinTime;
                    }
                }
                if (bllPlan.Add(plan) > 0)
                {
                    jsonModel.data.planID = plan.strTrainPlanGUID;
                    jsonModel.result = "0";
                    jsonModel.resultStr = "返回成功";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = "1";
                jsonModel.resultStr = "提交失败" + ex.Message;
            }
            return jsonModel;
        }
        #endregion

        #region 新建已接收机车计划


        public class Edit_In
        {
            public TF.RunSafty.Model.InterfaceModel.CreatedTrainPlan trainPlan;
            public User user;
            public Site site;
        }

        public class Edit_Out
        {
            public string result = "";
            public string resultStr = "";
            public PlanInfo data = new PlanInfo();
        }
        public Edit_Out AddAccepted(string data)
        {
            Edit_Out jsonModel = new Edit_Out();
            Edit_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Edit_In>(data);
            TF.RunSafty.BLL.Plan.TAB_Plan_Train bllPlan = new TF.RunSafty.BLL.Plan.TAB_Plan_Train();
            TF.RunSafty.Model.TAB_Plan_Train plan = new TF.RunSafty.Model.TAB_Plan_Train();
            try
            {
                SetPlanValue(input.trainPlan, input.user, input.site, plan);
                plan.nPlanState = (int)TF.RunSafty.Model.InterfaceModel.TRsPlanState.psReceive;
                if (bllPlan.Add(plan) > 0)
                {
                    jsonModel.data.planID = plan.strTrainPlanGUID;
                    jsonModel.result = "0";
                    jsonModel.resultStr = "返回成功";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = "1";
                jsonModel.resultStr = "提交失败" + ex.Message;
            }
            return jsonModel;
        }
        #endregion

        #region 更新计划

        public class Plan : CreatedTrainPlan
        {
            public string trainPlanGUID { get; set; }
            public string trainjiaoluID { get; set; }
            public string placeID { get; set; }
            public string trainTypeName { get; set; }
            public string trainNumber { get; set; }
            public string trainNo { get; set; }
            public string startTime { get; set; }
            public string kaiCheTime { get; set; }
            public string startStationID { get; set; }
            public string endStationID { get; set; }
            public string trainmanTypeID { get; set; }
            public string planTypeID { get; set; }
            public string dragTypeID { get; set; }
            public string kehuoID { get; set; }
            public string remarkTypeID { get; set; } 
            public string strRemark { get; set; }
            public string waiqinID { get; set; }
            public string waiqinNumber { get; set; }
            public string waiqinName { get; set; }
        }
        public class Update_In
        {
            public Plan trainPlan;
            public Site site;
            public User user;
        }
        public class Update_Out
        {
            public string result = "";
            public string resultStr = "";
        }

        public void WritePlanChangeLog(string PlanGUID,string SiteGUID,string DutyGUID)
        {
            string sql = "select nPlanState from TAB_Plan_Train where strTrainPlanGUID = @PlanGUID";
            SqlParameter[] param = { new SqlParameter("PlanGUID", PlanGUID) };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, param).Tables[0];

            if (dt.Rows.Count == 0)
            {
                return;
            }            
            else
            {
                if (Convert.ToInt32(dt.Rows[0]["nPlanState"]) < 2)
                {
                    return;
                }
                
            }

            sql = "PROC_Plan_WriteChangeLog @PlanGUID,@DutyGUID,@SiteGUID";

            SqlParameter[] Proc_Param = { 
                                            new SqlParameter("PlanGUID",PlanGUID),
            new SqlParameter("DutyGUID",DutyGUID),
            new SqlParameter("SiteGUID",SiteGUID)
                      
                  };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, Proc_Param);

        }
        public Update_Out Update(string input)
        {
            Update_In model = Newtonsoft.Json.JsonConvert.DeserializeObject<Update_In>(input);
            Update_Out json = new Update_Out();
            if (UpdatePlan(model.trainPlan))
            {
                WritePlanChangeLog(model.trainPlan.trainPlanGUID, model.site.siteID, model.user.userID);
                json.result = "0";
                json.resultStr = "更新成功";                
            }
            else
            {
                json.result = "0";
                json.resultStr = "更新失败";
            }
            return json;
        }

        private string GetDataColumnNameByProperty(string propertyName)
        {
            string strColumnName = "";
            if (hash == null)
            {
                hash = new Hashtable();
                hash.Add("trainjiaoluID", "strTrainJiaoluGUID");
                hash.Add("placeID", "strPlaceID");
                hash.Add("trainTypeName", "strTrainTypeName");
                hash.Add("trainNumber", "strTrainNumber");
                hash.Add("trainNo", "strTrainNo");
                hash.Add("startTime", "dtStartTime");
                hash.Add("kaiCheTime", "dtChuQinTime");
                hash.Add("startStationID", "strStartStation");
                hash.Add("endStationID", "strEndStation");
                hash.Add("trainmanTypeID", "nTrainmanTypeID");
                hash.Add("planTypeID", "nPlanType");
                hash.Add("dragTypeID", "nDragType");
                hash.Add("kehuoID", "nKehuoID");
                hash.Add("remarkTypeID", "nRemarkType");
                hash.Add("strRemark", "strRemark");
                hash.Add("waiqinID", "strWaiQinClientGUID");
                hash.Add("waiqinNumber", "strWaiQinClientNumber");
                hash.Add("waiqinName", "strWaiQinClientName");
            }
            strColumnName = hash[propertyName] as string;
            return strColumnName;
        }

        public bool UpdatePlan(Plan trainPlan)
        {
            bool result = false;
            try
            { 
                StringBuilder builder = new StringBuilder();
                builder.Append(" update TAB_Plan_Train set ");
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                object pValue;
                //构造sql语句
                Type type = typeof(Plan);
                PropertyInfo[] properties = type.GetProperties();
                string propertyName;
                string columnName;
                foreach (PropertyInfo property in properties)
                {
                    propertyName = property.Name;
                    if (propertyName == "trainPlanGUID") continue;
                    pValue = property.GetValue(trainPlan, null);
                    if (pValue != null)
                    {
                        columnName = GetDataColumnNameByProperty(propertyName);
                        builder.AppendFormat(" {0}={1}{2} ", columnName, "@", columnName);
                        sqlParams.Add(new SqlParameter(columnName, pValue));
                        builder.Append(",");
                    }
                }
                if (builder.ToString().EndsWith(","))
                {
                    builder.Remove(builder.Length - 1, 1);
                }
                builder.Append(" where strTrainPlanGUID=@strTrainPlanGUID ");
                sqlParams.Add(new SqlParameter("strTrainPlanGUID", trainPlan.trainPlanGUID));
                TF.CommonUtility.LogClass.log(builder.ToString());
                int count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, builder.ToString(), sqlParams.ToArray());
                if (count == 1)
                {
                    result = true;
                }


            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return result;
        }
        #endregion

        #region 删除计划

        public class Delete_In
        {

            public string strTrainPlanGUID = "";
        }

        public class Delete_Out
        {
            public string result = "";
            public string resultStr = "";
        }

        public Delete_Out Delete(string input)
        {
            Delete_Out json = new Delete_Out();
            Delete_In model = Newtonsoft.Json.JsonConvert.DeserializeObject<Delete_In>(input);
            string sqlText = string.Format("delete from tab_plan_train where strTrainPlanGUID=@strTrainPlanGUID ");
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("strTrainPlanGUID",model.strTrainPlanGUID), 
            };
            try
            {
                int count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
                if (count == 1)
                {
                    json.result = "0";
                    json.resultStr = "删除成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "删除失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 撤销计划

        public class Cancel_In
        {
            public List<planGUID> plans;
            public string SiteGUID;
            public string DutyUserGUID;
            public int CanDeleteMainPlan;
            public bool IgnoreCheck;
        }

        public class Cancel_Out : TF.RunSafty.Model.InterfaceModel.JsonOutBase
        {

        }

        private bool CancelPlan(Cancel_In model)
        {
            bool result = true;
            StringBuilder builder = new StringBuilder();
            int nPlanState = (int)TF.RunSafty.Model.InterfaceModel.TRsPlanState.psCancel;
            List<string> subGUIDs = new List<string>();
            List<string> PlanGUIDs = new List<string>();
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.ConnectionString = SqlHelper.ConnString;
            string strGroupGUID = "";
            int count = 0;
            string strPlanGUIDs = "";
            try
            {

                connection.Open();
                GetPlanGUIDS(model, PlanGUIDs);
                GetSubPlans(PlanGUIDs, subGUIDs);
                PlanGUIDs.AddRange(subGUIDs);
                foreach (string planGuiD in PlanGUIDs)
                {
                    if (builder.Length == 0)
                        builder.AppendFormat("'{0}'", planGuiD);
                    else
                    {
                        builder.Append(",");
                        builder.AppendFormat("'{0}'", planGuiD);
                    }
                }
                strPlanGUIDs = builder.ToString();


                command.CommandText =
                  string.Format("update TAB_TMIS_Train set strTrainPlanGUID = null where strTrainPlanGUID in ({0})", strPlanGUIDs);
                count = command.ExecuteNonQuery();

                command.CommandText =
                    string.Format("update TAB_Plan_Train set nPlanState = {0} where strTrainPlanGUID in ({1})",
                        nPlanState, strPlanGUIDs);
                count = command.ExecuteNonQuery();

                foreach (var planGuiD in PlanGUIDs)
                {
                    command.CommandText =
                        string.Format(
                            "insert into TAB_Plan_Cancel (strTrainPlanGUID,dtCancelTime,strCancelDutyGUID,strFlowSiteGUID) values ('{0}',getdate(),'{1}','{2}')",
                            planGuiD, model.DutyUserGUID, model.SiteGUID);
                    count = command.ExecuteNonQuery();
                    if (count == 0)
                    {
                        result = false;

                        break;
                    }
                    string strSql = "select strGroupGUID from TAB_Nameplate_Group where strTrainPlanGUID = @strTrainPlanGUID";
                    SqlParameter[] sqlParams = new SqlParameter[] {
                            new SqlParameter("strTrainPlanGUID",planGuiD)
                        };
                    DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.Text, strSql, sqlParams).Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strGroupGUID = dt.Rows[i]["strGroupGUID"].ToString();
                        string TMJiaoluGUID = TF.RunSafty.NamePlate.DB.DBNameBoard.GetGroupTMJiaoluGUID(strGroupGUID);
                        MD.TrainmanJiaoluMin tmJiaolu = new MD.TrainmanJiaoluMin();
                        if (TF.Runsafty.Plan.DBLinShi.GetTrainmanJiaolu(TMJiaoluGUID, ref tmJiaolu))
                        {
                            ///轮乘交路移除计划机组后删除机组
                            if (tmJiaolu.jiaoluType == 3)
                            {
                                TF.RunSafty.NamePlate.LCGroup.DeleteOrderGroup(tmJiaolu.jiaoluID, tmJiaolu.jiaoluName, strGroupGUID);
                            }
                        }
                    }

                    command.CommandText =
                        string.Format(
                            "update TAB_Nameplate_Group set strTrainPlanGUID = '' where strTrainPlanGUID = '{0}'",
                            planGuiD);
                    command.ExecuteNonQuery();
                    //清除计划关联的出勤测酒记录对应的WORKID
                    command.CommandText =
                        string.Format(
                            @"update TAB_Drink_Information set strworkid = ''  where strWorkID in
                                (
                                select strbeginworkguid from TAB_Plan_BeginWork where strTrainPlanGUID = '{0}'
                                )",
                            planGuiD);
                    command.ExecuteNonQuery();
                    //清除计划关联的退勤测酒记录对应的WORKID
                    command.CommandText =
                        string.Format(
                            @"update TAB_Drink_Information set strworkid = ''  where strWorkID in
                                (
                                select strEndworkGUID from TAB_Plan_EndWork where strTrainPlanGUID = '{0}'
                                )",
                            planGuiD);
                    command.ExecuteNonQuery();

                    //发送退勤消息
                    TF.RunSafty.Plan.MD.CancelPlanData msgData = new TF.RunSafty.Plan.MD.CancelPlanData();
                   
                    msgData.jiaoLuGUID = "";
                    msgData.GUIDS = planGuiD;
                   
                    ThinkFreely.RunSafty.AttentionMsg msg = msgData.ToMsg();
                    LogClass.log(Newtonsoft.Json.JsonConvert.SerializeObject(msg));
                    msg.CreatMsg();

                }
            }
           
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
                command.Dispose();
                connection.Dispose();
            }
            return result;
        }

        public void GetPlanGUIDS(Cancel_In model, List<string> guids)
        {
            guids.Clear();
            guids.AddRange(model.plans.Select(planGuid => planGuid.strTrainPlanGUID));
        }


        public Cancel_Out Cancel(string input)
        {
            Cancel_Out json = new Cancel_Out();
            Cancel_In model = Newtonsoft.Json.JsonConvert.DeserializeObject<Cancel_In>(input);
            string strError = "";
            List<string> PlanGUIDs = model.plans.Select(plan => plan.strTrainPlanGUID).ToList();


            TF.RunSafty.Model.InterfaceModel.PlansOfClient trainPlan = new PlansOfClient();
            try
            {
                //判断计划是否可以被删除
                if (model.IgnoreCheck == false && !CanCancelPlan(PlanGUIDs, trainPlan, out strError))
                {
                    json.result = "1";
                    json.resultStr = strError;
                }
                else
                {
                    //判断计划是否已经被删除
                    bool any = false;
                    foreach (string planGuiD in PlanGUIDs)
                    {
                        if (!GetTrainmanPlan(planGuiD, trainPlan))
                        {
                            json.result = "1";
                            json.resultStr = "计划已被删除，请稍后重试";
                            return json;
                        }
                        if (model.CanDeleteMainPlan == 0 && trainPlan.trainPlan.mainPlanGUID == "")
                        {
                            json.result = "1";
                            json.resultStr = "只能撤销附挂计划";
                            return json;
                        }
                    }
                    if (CancelPlan(model))
                    {
                        json.result = "0";
                        json.resultStr = "撤销成功";
                    }
                    else
                    {
                        json.result = "1";
                        json.resultStr = "撤销失败";
                    }
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw;
            }
            return json;
        }


        private bool GetTrainmanPlan(string strTrainmanPlanGUID, TF.RunSafty.Model.InterfaceModel.PlansOfClient plan)
        {
            bool result = false;
            string sqlText = string.Format("select top 1 * from VIEW_Plan_Trainman where strTrainPlanGUID = '{0}'",
                strTrainmanPlanGUID);
            DataSet set = new DataSet();
            set = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText);
            DataTable table = set.Tables[0];
            if (table.Rows.Count == 0)
            {
                result = false;
            }
            else
            {
                result = true;
                plan.trainPlan=new Model.InterfaceModel.TrainPlan();
                plan.group=new Model.InterfaceModel.NameGroup();
                plan.group.groupID = table.Rows[0]["strGroupGUID"].ToString();
                plan.trainPlan.mainPlanGUID = table.Rows[0]["strMainPlanGUID"].ToString();
            }
            return result;
        }

        /// <summary>
        /// 获取附加计划
        /// </summary>
        /// <param name="PlanGUIDs"></param>
        /// <param name="subGUIDS"></param>
        public void GetSubPlans(List<string> PlanGUIDs, List<string> subGUIDS)
        {
            StringBuilder builder = new StringBuilder();
            //拼接SQL语句
            foreach (string planGuiD in PlanGUIDs)
            {
                if (builder.Length == 0)
                    builder.AppendFormat("'{0}'", planGuiD);
                else
                {
                    builder.Append(",");
                    builder.AppendFormat("'{0}'", planGuiD);
                }
            }
            int nPlanState = (int)TF.RunSafty.Model.InterfaceModel.TRsPlanState.psCancel;
            string sqlText =
                string.Format(
                    "select strTrainPlanGUID from TAB_Plan_Train where nPlanState > {0}  and strMainPlanGUID in ({1})", nPlanState, builder.ToString());
            DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText).Tables[0];
            subGUIDS.Clear();
            subGUIDS.AddRange(from DataRow row in table.Rows select row["strTrainPlanGUID"].ToString());
        }


        private bool CanCancelPlan(List<string> PlanGUIDs,PlansOfClient trainPlan, out string strError)
        {
            bool result = true;
            strError = "";
            string strGroupGUID;
            List<string> subGUIDS=new List<string>();
            foreach (var planGuiD in PlanGUIDs)
            {
                if (!GetTrainmanPlan(planGuiD, trainPlan))
                { 
                    strError = "已有计划已被删除，请刷新后重试";
                    return false;
                }
                if (!string.IsNullOrEmpty(trainPlan.group.groupID))
                {
                    strError = "不能撤销已经安排人员的计划！";
                    return false;
                }
            }
            GetSubPlans(PlanGUIDs, subGUIDS);
            strGroupGUID = "";
            foreach (var subGUID in subGUIDS)
            {
                if (!GetTrainmanPlan(subGUID, trainPlan))
                    continue;
                if (!string.IsNullOrEmpty(strGroupGUID))
                {
                    strError = "附加计划已安排人员,不能撤销计划!";
                    return false;
                }
            }
            return result;
        }
        #endregion

        #region 下发计划

        public class Send_In
        {
            public List<planGUID> plans;
            public string SiteGUID;
            public string DutyUserGUID;
        }

        public class Send_Out : TF.RunSafty.Model.InterfaceModel.JsonOutBase
        {
        }

        public Send_Out Send(string input)
        {
            Send_Out json = new Send_Out();
            Send_In model = Newtonsoft.Json.JsonConvert.DeserializeObject<Send_In>(input);
            int nPlanState = (int)TF.RunSafty.Model.InterfaceModel.TRsPlanState.psSent;
            int count;
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.ConnectionString = SqlHelper.ConnString;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                if (model.plans != null)
                {
                    foreach (planGUID pGuid in model.plans)
                    {
                        SetPlanStateToSent(command, pGuid.strTrainPlanGUID);
                        Execute_PROC_Plan_AddSendRecord(command, pGuid.strTrainPlanGUID, model.DutyUserGUID,
                            model.SiteGUID);
                    }
                    transaction.Commit();
                }
                json.result = "0";
                json.resultStr = "下发成功";
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    TF.CommonUtility.LogClass.logex(ex2, "");
                }
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
                command.Dispose();
                connection.Dispose();
            }
            return json;
        }

        /// <summary>
        /// 设置计划状态为已下发
        /// </summary>
        /// <param name="command"></param>
        /// <param name="strTrainPlangGUID"></param>
        private void SetPlanStateToSent(SqlCommand command, string strTrainPlanGUID)
        {
            int nPlanState = (int)TF.RunSafty.Model.InterfaceModel.TRsPlanState.psSent;
            string sqlText = "update tab_plan_train set nPlanState=@nPlanState,dtSendTime=getdate() where strTrainPlanGUID=@strTrainPlanGUID";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("nPlanState", nPlanState), new SqlParameter("strTrainPlanGUID", strTrainPlanGUID),
            };
            command.CommandText = sqlText;
            command.CommandType = CommandType.Text;
            command.Parameters.Clear();
            command.Parameters.AddRange(sqlParameters);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="command"></param>
        /// <param name="strTrainPlanGUID"></param>
        /// <param name="DutyUserGUID"></param>
        /// <param name="SiteGUID"></param>
        private void Execute_PROC_Plan_AddSendRecord(SqlCommand command, string strTrainPlanGUID, string DutyUserGUID,
            string SiteGUID)
        {
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PROC_Plan_AddSendRecord";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@strTrainPlanGUID", strTrainPlanGUID),
                new SqlParameter("@strDutyUserGUID", DutyUserGUID),
                new SqlParameter("@strSiteGUID",SiteGUID), 
            };
            command.Parameters.Clear();
            command.Parameters.AddRange(sqlParameters);
            command.ExecuteNonQuery();
        }
        #endregion

        #region 接收计划

        public class Receive_In
        {
            public string strSiteGUID = "";
            public string strDutyGUID = "";
        }

        public class Receive_Out : JsonOutBase
        {
            public object data = new object();
        }

        public class PlanGUID
        {
            public PlanGUID(string strPlanGUID)
            {
                this.planGUID = strPlanGUID;
            }

            public string planGUID { get; set; }
        }

        public void ADOQueryToSendLog(RRsTrainPlanSendLog SendLog, DataRow row)
        {
            SendLog.strTrainNo = row["strTrainNo"].ToString();
            SendLog.strSendGUID = row["strSendGUID"].ToString();
            SendLog.strTrainPlanGUID = row["strTrainPlanGUID"].ToString();
            SendLog.strTrainJiaoluName = row["strTrainJiaoluName"].ToString();
            DateTime dtStartTime, dtRealStartTime, strSendSiteName, dtSendTime;
            if (DateTime.TryParse(row["dtStartTime"].ToString(), out dtStartTime))
            {
                SendLog.dtStartTime = dtStartTime;
            }
            if (DateTime.TryParse(row["dtRealStartTime"].ToString(), out dtRealStartTime))
            {
                SendLog.dtRealStartTime = dtRealStartTime;
            }
            SendLog.strSendSiteName = row["strSendSiteName"].ToString();
            if (DateTime.TryParse(row["dtSendTime"].ToString(), out dtSendTime))
            {
                SendLog.dtSendTime = dtSendTime;
            }
        }

        public List<string> GetNotReceivedPlanGUIDs(string SiteGUID)
        {
            List<string> list=new List<string>();
            string strSql = string.Format(@"SELECT strTrainPlanGUID
FROM   tab_plan_send tsend
WHERE  bIsRec = 0 
       AND ( strTrainJiaoluGUID IN (SELECT strTrainJiaoluGUID
                                    FROM   TAB_Base_TrainJiaoluInSite
                                    WHERE  strSiteGUID =@strSiteGUID)
              OR strTrainJiaoluGUID IN (SELECT strSubTrainJiaoluGUID
                                        FROM   TAB_Base_TrainJiaolu_SubDetail
                                        WHERE  strTrainJiaoluGUID IN (SELECT strTrainJiaoluGUID
                                                                      FROM   TAB_Base_TrainJiaoluInSite
                                                                      WHERE  strSiteGUID = @strSiteGUID)) )                                                                                                                                                   
ORDER  BY dtSendTime desc ");
            SqlParameter[] sqlParameters = new SqlParameter[]
            { new SqlParameter("strSiteGUID",SiteGUID), 
            };
            DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
            foreach (DataRow row in table.Rows)
            {
                list.Add(row["strTrainPlanGUID"].ToString());
            }
            return list;
        }
        /// <summary>
        /// 获取发送日志
        /// </summary>
        /// <param name="SiteGUID"></param>
        /// <param name="LastTime"></param>
        /// <returns></returns>
        public List<RRsTrainPlanSendLog> GetSendLog(string SiteGUID, DateTime LastTime)
        {
            List<RRsTrainPlanSendLog> list=new List<RRsTrainPlanSendLog>();
            string strSql = string.Format(@"select * from tab_plan_send  tsend where (dtSendTime > @dtSendTime) and 
    (strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID=@strSiteGUID) 
     or strTrainJiaoluGUID in (select strSubTrainJiaoluGUID from TAB_Base_TrainJiaolu_SubDetail where strTrainJiaoluGUID in
(select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID=@strSiteGUID))) 
     and 
	   (dtSendTime = (select max(dtSendTime) from tab_plan_send where strTrainPlanGUID=tsend.strTrainPlanGUID))
     order by dtSendTime");
            SqlParameter[] sqlParameters=new SqlParameter[]
            {
                new SqlParameter("dtSendTime",LastTime), new SqlParameter("strSiteGUID",SiteGUID), 
            };
            DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql,sqlParameters).Tables[0];
            RRsTrainPlanSendLog SendLog;
            int bIsRec;
            foreach (DataRow row in table.Rows)
            {
                if (row["bIsRec"] != null && int.TryParse(row["bIsRec"].ToString(), out bIsRec) && bIsRec == 0)
                {
                    SendLog = new RRsTrainPlanSendLog();
                    ADOQueryToSendLog(SendLog, row);
                    list.Add(SendLog);
                }
            }
            return list;
        }
        public Receive_Out Receive(string input)
        {
            Receive_Out json=new Receive_Out();
            Receive_In receive = Newtonsoft.Json.JsonConvert.DeserializeObject<Receive_In>(input);
            List<PlanGUID> GUIDS = new List<PlanGUID>();
            DateTime dtLastTime=new DateTime(1899,12,30);
            //List<RRsTrainPlanSendLog> sendLogArray = null;
            List<string> sendLogArray = null;
            try
            {
                sendLogArray = GetNotReceivedPlanGUIDs(receive.strSiteGUID);
                foreach (var planGUID in sendLogArray)
                {
                    GUIDS.Add(new PlanGUID(planGUID));
                }
                if (GUIDS.Count > 0)
                {
                    bool result=ReceivePlan(GUIDS,receive.strSiteGUID,receive.strDutyGUID);
                    if (result)
                    {
                        json.result = "0";
                        json.resultStr = "接收成功";
                        json.data = GUIDS;
                    }
                    else
                    {
                        json.result = "0";
                        json.resultStr = "接收失败";
                    }
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "没有需要接收的计划";
                }
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "接收失败";
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }

        public bool ReceivePlan(List<PlanGUID> PlanGUIDs, string SiteGUID, string DutyUserGUID)
        {
            bool result = true;
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.ConnectionString = SqlHelper.ConnString;
            string strGUIDs, strSql, siteName, dutyUserName, dutyUserID;
            StringBuilder builder = new StringBuilder();
            foreach (var planGuiD in PlanGUIDs)
            {
                if (builder.Length == 0)
                    builder.AppendFormat("'{0}'", planGuiD.planGUID);
                else
                {
                    builder.AppendFormat(",'{0}'", planGuiD.planGUID);
                }
            }
            strGUIDs = builder.ToString();
            strSql = string.Format("select strSiteName from TAB_Base_Site where strSiteGUID='{0}'", SiteGUID);
            DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (table == null || table.Rows.Count == 0)
            {
                throw new Exception("本客户端信息没有登记！");
            }
            siteName = table.Rows[0]["strSiteName"].ToString();
            strSql = string.Format("select strDutyName,strDutyNumber from TAB_Org_DutyUser where strDutyGUID='{0}'",
                DutyUserGUID);
            table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (table == null || table.Rows.Count == 0)
            {
                throw new Exception("当前登录用户没有登记！");
            }
            dutyUserName = table.Rows[0]["strDutyName"].ToString();
            dutyUserID = table.Rows[0]["strDutyNumber"].ToString();
            int count = 0;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                //更新计划状态
                strSql = string.Format("update tab_Plan_Train set nPlanState = {0} ,dtRecvTime = '{3}' where strTrainPlanGUID in ({1}) and nPlanState = {2}", (int)TRsPlanState.psReceive, strGUIDs, (int)TRsPlanState.psSent, DateTime.Now);
                command.CommandText = strSql;
                count=command.ExecuteNonQuery();

                //将接收表的字段bIsRec 置为1，表示已接收
                strSql = string.Format(@" update tab_plan_send set bIsRec = 1,dtRecTime = getdate(),strRecSiteGUID='{0}', 
    strRecSiteName='{1}',strRecUserGUID='{2}',strRecUserName='{3}',strRecUserID='{4}'
      where strTrainPlanGUID in ({5}) and   (dtSendTime = (select max(dtSendTime) 
from tab_plan_send tSend where tSend.strTrainPlanGUID=tab_plan_send.strTrainPlanGUID) and bIsRec = 0)", SiteGUID, siteName, DutyUserGUID, dutyUserName, dutyUserID,strGUIDs);
                command.CommandText = strSql;
                count=command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    TF.CommonUtility.LogClass.logex(ex2, "");
                }
                TF.CommonUtility.LogClass.logex(ex,"");
                result = false;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
                command.Dispose();
                connection.Dispose();
            }
            return result;
        }

        #endregion

        #region 移除人员

        public class RemoveTrainman_In
        {
            public string trainmanGUID = "";
            public string trainmanPlanGUID = "";
            public string strGroupGUID = "";
            public int trainmanIndex;
        }

        public class RemoveTrainman_Out : JsonOutBase
        {
            
        }

        public RemoveTrainman_Out RemoveTrainman(string input)
        {
            RemoveTrainman_Out json = new RemoveTrainman_Out();
            try
            {

                RemoveTrainman_In model = Newtonsoft.Json.JsonConvert.DeserializeObject<RemoveTrainman_In>(input);
                string strTrainmanGUID = model.trainmanGUID,
                    strTrainPlanGUID = model.trainmanPlanGUID,
                    strGroupGUID = model.strGroupGUID;
                int nTrainmanIndex = model.trainmanIndex;

                if (IsBeginWorking(strTrainmanGUID, strTrainPlanGUID))
                {
                    json.result = "1";
                    json.resultStr = "该乘务员已经出勤不能移除！";
                }
                else
                {
                    //判断是否是最后一个人员,不能对最后一个人员操作
                    if (IsLastPerson(strTrainmanGUID, strTrainPlanGUID))
                    {
                        json.result = "1";
                        json.resultStr = "不能对最后一个人员操作";
                        return json;
                    }
                    DeleteTrainmanFromPlan(strTrainmanGUID, nTrainmanIndex, strTrainPlanGUID);
                    UpdatePlanState(strTrainPlanGUID);
                    DeleteTrainman(strTrainmanGUID);
                    DeleteTrainman(strTrainmanGUID, strGroupGUID);
                    json.result = "0";
                    json.resultStr = "移除人员成功";

                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }


        private bool IsLastPerson(string strTrainmanGUID, string trainmanPlanGUID)
        {
            bool result = false;
            TF.RunSafty.BLL.Plan.VIEW_Plan_Trainman bllPlan=new VIEW_Plan_Trainman();
            string strWhere = string.Format(" strTrainPlanGUID='{0}' ", trainmanPlanGUID);
            List<TF.RunSafty.Model.VIEW_Plan_Trainman> plans = bllPlan.GetModelList(strWhere);
            TF.RunSafty.Model.VIEW_Plan_Trainman plan = null;
            if (plans != null && plans.Count > 0)
            {
                plan = plans[0];
                if (
                    (plan.strTrainmanGUID1 + plan.strTrainmanGUID2 + plan.strTrainmanGUID3 + plan.strTrainmanGUID4)
                        .Equals(strTrainmanGUID))
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 删除机组人员
        /// </summary>
        /// <param name="TrainmanGUID"></param>
        private void DeleteTrainman(string TrainmanGUID)
        {
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.ConnectionString = SqlHelper.ConnString;
            string strGroupGUID = "";
            int count = 0;
            string strPlanGUIDs = "";
            try
            {
                
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                int nTrainmanState = (int) TRsTrainmanState.tsReady;
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strTrainmanGUID",TrainmanGUID), 
                    new SqlParameter("nTrainmanState",nTrainmanState), 
                };
                command.Parameters.AddRange(sqlParameters);
                string strSql = string.Format(@"update Tab_Org_Trainman set nTrainmanState = @nTrainmanState,dtBecomeReady = getdate()  where strTrainmanGUID =@strTrainmanGUID");
                command.CommandText = strSql;
                count=command.ExecuteNonQuery();
                command.CommandText = string.Format(@"update TAB_Nameplate_Group   set 
strTrainmanGUID1=(case when strTrainmanGUID1=@strTrainmanGUID then '' else strTrainmanGUID1 end),
strTrainmanGUID2=(case when strTrainmanGUID2=@strTrainmanGUID then '' else strTrainmanGUID2 end),
strTrainmanGUID3=(case when strTrainmanGUID3=@strTrainmanGUID then '' else strTrainmanGUID3 end),
strTrainmanGUID4=(case when strTrainmanGUID4=@strTrainmanGUID then '' else strTrainmanGUID4 end)
where strTrainmanGUID1=@strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID
");
                count=command.ExecuteNonQuery();
                transaction.Commit();
                 
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    TF.CommonUtility.LogClass.logex(ex2, "");
                }
                TF.CommonUtility.LogClass.logex(ex,"");
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
                command.Dispose();
                connection.Dispose();
            } 
        }
        /// <summary>
        /// 判断是否已经安排工作
        /// </summary>
        /// <param name="strTrainmanGUID"></param>
        /// <param name="strTrainPlanGUID"></param>
        /// <returns></returns>
        private bool IsBeginWorking(string strTrainmanGUID, string strTrainPlanGUID)
        {
            bool result = false;
            string strSql = "select count(*) from tab_plan_BeginWork where strTrainmanGUID = @strTrainmanGUID and strTrainPlanGUID = @strTrainPlanGUID";
            SqlParameter[] sqlParameters=new SqlParameter[]
            {
                new SqlParameter("strTrainmanGUID",strTrainmanGUID), new SqlParameter("strTrainPlanGUID",strTrainPlanGUID), 
            };
            object count = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
            if (count != null)
            {
                result = (Convert.ToInt32(count)) > 0;
            }
            return result;
        }

        /// <summary>
        /// 从计划中移除人员
        /// </summary>
        /// <param name="strTrainmanGUID"></param>
        /// <param name="trainmanIndex"></param>
        /// <param name="strTrainPlanGUID"></param>
        private void DeleteTrainmanFromPlan(string strTrainmanGUID, int trainmanIndex, string strTrainPlanGUID)
        {
            int nTrainmanState = (int) TRsTrainmanState.tsReady;
            SqlParameter[] sqlParameters=new SqlParameter[]
            {
                new SqlParameter("nTrainmanState",nTrainmanState), 
                new SqlParameter("strTrainmanGUID",strTrainmanGUID), 
                new SqlParameter("strTrainPlanGUID",strTrainPlanGUID), 
            };
            string strSql = "update tab_Org_Trainman set nTrainmanState = @nTrainmanState where strTrainmanGUID = @strTrainmanGUID ";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
            strSql = string.Format("update tab_Plan_Trainman set strTrainmanGUID{0} = null where strTrainPlanGUID = @strTrainPlanGUID ",trainmanIndex);
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
            string strSqlGroup = "update TAB_Nameplate_Group set strTrainmanGUID1 = null where strTrainPlanGUID = @strTrainPlanGUID ";
            strSqlGroup =
                string.Format(
                    "update TAB_Nameplate_Group set strTrainmanGUID{0} = null where strTrainPlanGUID = @strTrainPlanGUID",trainmanIndex);
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSqlGroup, sqlParameters);
        }
        /// <summary>
        /// 更新计划状态为已出勤
        /// </summary>
        /// <param name="strTrainPlanGUID"></param>
        private void UpdatePlanState(string strTrainPlanGUID)
        {
            int nPlanState = (int) TRsPlanState.psBeginWork;
            int psPublish = (int) TRsPlanState.psPublish;
            string strSql =string.Format(@" update TAB_Plan_Train set nPlanState = {0}
        where strTrainPlanGUID = '{1}' and nPlanState = {2}
         and (select count(*) from VIEW_Plan_BeginWork where strTrainPlanGUID = TAB_Plan_Train.strTrainPlanGUID
        and 
           ((strTrainmanGUID1 is null or strTrainmanGUID1 = '' or not(dtTestTime1 is null)) 
           and  (strTrainmanGUID2 is null or  strTrainmanGUID2 = '' or not(dtTestTime2 is null)) 
           and (strTrainmanGUID3 is null or strTrainmanGUID3 = '' or not(dtTestTime3 is null)) 
           and (strTrainmanGUID4 is null or strTrainmanGUID4 = '' or not(dtTestTime4 is null)))
        ) > 0 ;",nPlanState,strTrainPlanGUID,psPublish);
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql);
        }

        private bool GetTrainmanJiaoluOfGroup(string GroupGUID, out string TrainmanJiaolu)
        {
            bool result = true;
            TrainmanJiaolu = "";
            string strSql = string.Format("select strTrainmanJiaoluGUID from VIEW_Nameplate_Group where strGroupGUID = '{0}'", GroupGUID);
            object tmp = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql);
            if (tmp == null)
            { 
                return false;
            }
            TrainmanJiaolu = tmp.ToString();
            return result;
        }

        private bool GetTrainmanJiaolu(string TrainmanJiaoluGUID, string TrainmanJiaolu)
        {
            string strSql = string.Format("select * from VIEW_Base_TrainmanJiaolu where strTrainmanJiaoluGUID ='{0}' order by strTrainmanJiaoluName", TrainmanJiaoluGUID);
            DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (table == null || table.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 移除机组

        public class RemoveGroup_In
        {
            public string strTrainPlanGUID = "";
            public string strGroupGUID = "";
        }

        public class RemoveGroup_Out:JsonOutBase
        {
            
        }

        public RemoveGroup_Out RemoveGroup(string input)
        {
            RemoveGroup_Out json=new RemoveGroup_Out();
            RemoveGroup_In data = Newtonsoft.Json.JsonConvert.DeserializeObject<RemoveGroup_In>(input);
            if (!CanRemovePlanGroup(data.strTrainPlanGUID))
            {
                json.result = "1";
                json.resultStr = "该计划中的机组已有人出勤！";
                return json;
            }
            string TMJiaoluGUID = TF.RunSafty.NamePlate.DB.DBNameBoard.GetGroupTMJiaoluGUID(data.strGroupGUID);
            MD.TrainmanJiaoluMin tmJiaolu = new MD.TrainmanJiaoluMin();
            if (!TF.Runsafty.Plan.DBLinShi.GetTrainmanJiaolu(TMJiaoluGUID,ref tmJiaolu))
            {
                json.result = "1";
                json.resultStr = "机组所在交路查询不到，请检查:" + TMJiaoluGUID;
            }
            if (!RemovePlanGroup(data.strTrainPlanGUID))
            {
                json.result = "1";
                json.resultStr = "移除机组失败，请检查！";
                return json;
            }
            ///轮乘交路移除计划机组后删除机组
            if (tmJiaolu.jiaoluType == 3)
            {
                TF.RunSafty.NamePlate.LCGroup.DeleteOrderGroup(tmJiaolu.jiaoluID,tmJiaolu.jiaoluName,data.strGroupGUID);
            }
            //删除索引表
            DeleteGroup(data.strGroupGUID);

            
            json.result = "0";
            json.resultStr = "移除成功";
            return json;
        }
        /// <summary>
        /// 判断是否可以删除机组，有人出勤的话就不能删除
        /// </summary>
        /// <param name="TrainPlanGUID"></param>
        /// <returns></returns>
        public bool CanRemovePlanGroup(string TrainPlanGUID)
        {
            bool result = true;
            string strSql = string.Format("select count(*) from tab_Plan_Trainman where strTrainPlanGUID=@strTrainPlanGUID and (select count(*) from tab_Plan_BeginWork where strTrainPlanGUID =@strTrainPlanGUID ) > 0");
            SqlParameter[] sqlParameters=new SqlParameter[]
            {
                new SqlParameter("strTrainPlanGUID",TrainPlanGUID), 
            };
            object count = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
            result = Convert.ToInt32(count) <= 0;
            return result;
        }

        public bool RemovePlanGroup(string strTrainPlanGUID)
        {
            bool result = true;
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.ConnectionString = SqlHelper.ConnString;
            int count = 0;
            try
            {

                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = @"delete from tab_Plan_Trainman where strTrainPlanGUID = @strTrainPlanGUID  
          and (select count(*) from tab_Plan_BeginWork where strTrainPlanGUID =@strTrainPlanGUID) = 0";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strTrainPlanGUID", strTrainPlanGUID),
                };
                command.Parameters.AddRange(sqlParameters);
                count = command.ExecuteNonQuery();
                if (count <= 0)
                {
                    transaction.Rollback();
                    return false;
                }
                command.CommandText =
                    "update TAB_Nameplate_Group set strTrainPlanGUID = null where strTrainPlanGUID=@strTrainPlanGUID";
                count = command.ExecuteNonQuery();
                if (count <= 0)
                {
                    transaction.Rollback();
                    return false;
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    TF.CommonUtility.LogClass.logex(ex2, "");
                }
                result = false;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
                command.Dispose();
                connection.Dispose();
            }
            return result;
        }

        public void DeleteGroup(string GroupGUID)
        {
            string strSql = "delete from TAB_Nameplate_Group_TrainmanIndex where strGroupGUID=@strGroupGUID";
            SqlParameter[] sqlParameters=new SqlParameter[]
            {
                new SqlParameter("strGroupGUID",GroupGUID), 
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
        }

        public void DeleteTrainman(string strTrainmanGUID, string strGroupGUID)
        {
            string strSql = "delete from TAB_Nameplate_Group_TrainmanIndex where strGroupGUID =@strGroupGUID and strTrainmanGUID =@strTrainmanGUID ";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("strGroupGUID",strGroupGUID), 
                new SqlParameter("strTrainmanGUID",strTrainmanGUID), 
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
        }
        #endregion

        #region 根据PlanGuid获取行车计划

        public class PlanGuid_In
        {
            public string TrainPlanGUID = string.Empty;
        }
        public class PlanGuid_OutData
        {
            public TrainPlan Plan = new TrainPlan();
            public int Exist = 0;
        }
        public class PlanGuid_Out:JsonOutBase
        {
            public PlanGuid_OutData data = new PlanGuid_OutData();
        }

        public PlanGuid_Out GetPlanByGUID(string input)
        {
            PlanGuid_Out json=new PlanGuid_Out();
            PlanGuid_In model = null;
            string strSql = string.Empty;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<PlanGuid_In>(input);
                strSql = "Select * from VIEW_Plan_Train where strTrainPlanGUID=@strTrainPlanGUID";
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("strTrainPlanGUID",model.TrainPlanGUID), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {                  
                    PS.PSPlan.TrainPlanFromDB(json.data.Plan,table.Rows[0]);
                    json.data.Exist = 1;
                    json.result = "0";
                    json.resultStr = "获取计划成功";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "获取计划失败";
                }
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "获取计划失败：" + ex.Message;
            }
            return json;
        }

        public void ADOQueryToTrainPlan(DataRow row, RRsTrainPlan plan)
        {
            const string dateFormat = "yyyy-MM-dd HH:mm:ss";
            plan.strTrainPlanGUID = row["strTrainPlanGUID"].ToString();
            plan.strPlaceID = row["strPlaceID"].ToString();
            plan.strTrainTypeName = row["strTrainTypeName"].ToString();
            plan.strTrainNumber = row["strTrainNumber"].ToString();
            plan.strTrainNo = row["strTrainNo"].ToString();
            DateTime dtStartTime, dtRealStartTime;
            plan.dtStartTime = string.Empty;
            plan.dtRealStartTime = string.Empty;
            plan.dtFirstStartTime = string.Empty;
            plan.dtCreateTime = string.Empty;
            plan.dtLastArriveTime = string.Empty;
            if (row["dtStartTime"] != DBNull.Value)
            {
                if (DateTime.TryParse(row["dtStartTime"].ToString(), out dtStartTime))
                {
                    plan.dtStartTime = dtStartTime.ToString(dateFormat);
                }
            }
            if (row["dtRealStartTime"] != DBNull.Value)
            {
                if (DateTime.TryParse(row["dtRealStartTime"].ToString(), out dtRealStartTime))
                {
                    plan.dtRealStartTime = dtRealStartTime.ToString(dateFormat);
                    plan.dtFirstStartTime = dtRealStartTime.ToString(dateFormat);
                }
            }
            plan.strTrainJiaoluGUID = row["strTrainJiaoluGUID"].ToString();
            plan.strTrainJiaoluName = row["strTrainJiaoluName"].ToString();
            plan.strStartStation = row["strStartStation"].ToString();
            plan.strStartStationName = row["strStartStationName"].ToString();
            plan.strEndStation = row["strEndStation"].ToString();
            plan.strEndStationName = row["strEndStationName"].ToString();
            int nTrainmanTypeID, nPlanType, nDragType, nKeHuoID, nRemarkType;
            if (row["nTrainmanTypeID"] != DBNull.Value)
            {
                if (int.TryParse(row["nTrainmanTypeID"].ToString(), out nTrainmanTypeID))
                {
                    plan.nTrainmanTypeID = nTrainmanTypeID;
                }
            }
            if (row["nPlanType"] != DBNull.Value)
            {
                if (int.TryParse(row["nPlanType"].ToString(), out nPlanType))
                {
                    plan.nPlanType = nPlanType;
                }
            }
            if (row["nDragType"] != DBNull.Value)
            {
                if (int.TryParse(row["nDragType"].ToString(), out nDragType))
                {
                    plan.nDragType = nDragType;
                }
            }
            if (row["nRemarkType"] != DBNull.Value)
            {
                if (int.TryParse(row["nRemarkType"].ToString(), out nRemarkType))
                {
                    plan.nRemarkType = nRemarkType;
                }
            }
            if (row["nKeHuoID"] != DBNull.Value)
            {
                if (int.TryParse(row["nKeHuoID"].ToString(), out nKeHuoID))
                {
                    plan.nKeHuoID = nKeHuoID;
                }
            }
            plan.strRemark = row["strRemark"].ToString();
            int nPlanState;
            if (row["nPlanState"] != DBNull.Value)
            {
                if (int.TryParse(row["nPlanState"].ToString(), out nPlanState))
                {
                    plan.nPlanState = nPlanState;
                    plan.planStateID = nPlanState;
                }
            }

            
            DateTime dtLastArriveTime, dtCreateTime;
            if (row["dtLastArriveTime"] != DBNull.Value)
            {
                if (DateTime.TryParse(row["dtLastArriveTime"].ToString(), out dtLastArriveTime))
                {
                    plan.dtLastArriveTime = dtLastArriveTime.ToString(dateFormat);
                }
            } plan.dtLastArriveTime = string.Empty;
            if (row["dtCreateTime"] != DBNull.Value)
            {
                if (DateTime.TryParse(row["dtCreateTime"].ToString(), out dtCreateTime))
                {
                    plan.dtCreateTime = dtCreateTime.ToString(dateFormat);
                }
            } 
            plan.strCreateSiteGUID = row["strCreateSiteGUID"].ToString();
            plan.strCreateUserGUID = row["strCreateUserGUID"].ToString();
            plan.strMainPlanGUID = row["strMainPlanGUID"].ToString();
        }

        #endregion

        #region 导出机车计划

        public class Export_In
        {
            public string BeginTime = string.Empty;
            public string EndTime = string.Empty;
            public string TrainJiaoluGUID = string.Empty;
        }

        public class Export_Out : JsonOutBase
        {
            public List<TrainPlan> data;
        }

        public Export_Out GetExportTrainPlans(string input)
        {
            Export_Out json=new Export_Out();
            Export_In model = null;
            string strSql = string.Empty;
            TrainPlan plan = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Export_In>(input);
                if (model.TrainJiaoluGUID != string.Empty)
                {
                    strSql = "select * from VIEW_Plan_Train where nPlanState>=2 and dtStartTime>=@dtStartTime and dtStartTime<=@dtEndTime and strTrainJiaoluGUID = @TrainJiaoLuGUID order by dtStartTime";
                }
                else
                {
                    strSql = "select * from VIEW_Plan_Train where nPlanState>=2 and dtStartTime>=@dtStartTime and dtStartTime<=@dtEndTime order by dtStartTime";
                }

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("dtStartTime",model.BeginTime),
                    new SqlParameter("dtEndTime",model.EndTime), 
                    new SqlParameter("TrainJiaoLuGUID",model.TrainJiaoluGUID)
                };

                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                json.data=new List<TrainPlan>();
                
                foreach (DataRow dataRow in table.Rows)
                {
                    plan = new TrainPlan();
                    PS.PSPlan.TrainPlanFromDB(plan, dataRow);
                    //ADOQueryToTrainPlan(dataRow,plan);
                    json.data.Add(plan);
                }
                json.result = "0";
                json.resultStr = "获取导出机车计划成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 计算出勤时间

        public class Calc_In
        {
            public string PlaceID = string.Empty;
            public int RemakType;
        }

        public class CalcModel
        {
            public int ATime ;
        }
        public class Calc_Out:JsonOutBase
        {
            public CalcModel data;
        }

        public Calc_Out CalcPlanBeginWorkTime(string input)
        {
            Calc_Out json=new Calc_Out();
            Calc_In model = null;
            string strSql = string.Empty;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Calc_In>(input);
                strSql = "select nMinute from Tab_Base_ChuQinTimeRule where nRemarkType = @nRemarkType and strPlaceID =@strPlaceID ";
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("nRemarkType",model.RemakType), 
                    new SqlParameter("strPlaceID",model.PlaceID), 
                };
                object oMinute = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
                int nMinute;
                if (oMinute != null && int.TryParse(oMinute.ToString(), out nMinute))
                {
                    json.data = new CalcModel();
                    json.data.ATime = nMinute;
                    json.result = "0";
                    json.resultStr = "计算出勤时间成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "计算出勤时间失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 获取计划终到时间

        public class ArriveTime_In
        {
            public string TrainPlanGUID = string.Empty;
        }

        public class ArriveTimeModel
        {
            public string ArriveTime = string.Empty;
        }
        public class ArriveTime_Out:JsonOutBase
        {
            public ArriveTimeModel data=new ArriveTimeModel();
        }

        public ArriveTime_Out GetPlanLastArriveTime(string input)
        {
            ArriveTime_Out json=new ArriveTime_Out();
            ArriveTime_In model = null;
            DateTime dtLastArriveTime;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<ArriveTime_In>(input);
                string strSql = string.Format("Select dtLastArriveTime from TAB_Plan_Train where  strTrainPlanGUID ='{0}'",model.TrainPlanGUID);
                object tmp = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql);
                if (tmp != null)
                {
                    if (DateTime.TryParse(tmp.ToString(), out dtLastArriveTime))
                    {
                        json.data.ArriveTime = dtLastArriveTime.ToString("yyyy-MM-dd HH:mm:ss");
                        json.result = "0";
                        json.resultStr = "获取终到时间成功";
                    }
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "无法获取计划";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 更新计划退勤测酒结果

        public class EndWorkDrink_In
        {
            public string strEndWorkGUID = string.Empty;
            public RRsDrink drink;
        }

        public class EndWorkDrink_Out:JsonOutBase
        {
            
        }

        public EndWorkDrink_Out UpdateEndWorkTestResult(string input)
        {
            EndWorkDrink_Out json=new EndWorkDrink_Out();
            EndWorkDrink_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<EndWorkDrink_In>(input);
                string strSql = string.Format("update TAB_Drink_Information set nDrinkResult=@nDrinkResult ,dtCreateTime=@dtCreateTime,strImagePath=@strImagePath where strWorkID=@strWorkID");
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("nDrinkResult",model.drink.nDrinkResult), 
                    new SqlParameter("dtCreateTime",model.drink.dtCreateTime), 
                    new SqlParameter("strImagePath",model.drink.strPictureURL),
                    new SqlParameter("strWorkID",model.strEndWorkGUID), 
                };
                int count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
                if (count > 0)
                {
                    json.result = "0";
                    json.resultStr = "更新测酒记录成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "更新测酒记录失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 获取乘务员的最近一次退勤记录

        public class LastTuiQin_In
        {
            public string TrainmanGUID = string.Empty;
        }

        public class LastTuiQin
        {
            public string strEndWorkGUID = string.Empty;
            public string EndWorkTime = string.Empty;
        }
        public class LastTuiQin_Out : JsonOutBase
        {
            public LastTuiQin data=new LastTuiQin();
        }

        public LastTuiQin_Out GetLastTuiQinRecord(string input)
        {
            LastTuiQin_Out json=new LastTuiQin_Out();
            LastTuiQin_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<LastTuiQin_In>(input);
                string strSql = "select top 1 * from TAB_Plan_EndWork  where strTrainmanGUID =@strTrainmanGUID order by dtCreateTime DESC";
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("strTrainmanGUID",model.TrainmanGUID), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    DateTime dtCreateTime;
                    if (row["dtCreateTime"] != DBNull.Value &&
                        DateTime.TryParse(row["dtCreateTime"].ToString(), out dtCreateTime))
                    {
                        json.data.EndWorkTime = dtCreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    json.data.strEndWorkGUID = row["strEndworkGUID"].ToString();
                    json.result = "0";
                    json.resultStr = "获取退勤记录成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "获取最近一次退勤记录失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 根据GUID获取退勤计划

        public class TuiQinGUID_In
        {
            public string TuiQinPlanGUID = string.Empty;
        }

        public class TuiQinGUID_Out : JsonOutBase
        {
            public object data;
        }

        public TuiQinGUID_Out GetTuiQinPlanByGUID(string input)
        {
            TuiQinGUID_Out json=new TuiQinGUID_Out();
            TuiQinGUID_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<TuiQinGUID_In>(input);
                string strSql = @"select top 1 * from VIEW_Plan_EndWork where strTrainPlanGUID=@strTrainPlanGUID";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strTrainPlanGUID", model.TuiQinPlanGUID), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    TF.CommonUtility.JsonConvert.FormatDataRow(table, row);
                    json.data = table;
                    json.result = "0";
                    json.resultStr = "获取退勤计划成功";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "获取退勤计划失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 获取计划对应调令的写卡区段
        public class InGetWriteCardSecList
        {
            //行车计划GUID
            public string strTrainPlanGUID;
        }

        public class OutGetWriteCardSecList
        {
            //写卡区段列表
            public PlanWriteCardSectionList WriteCardSecList = new PlanWriteCardSectionList();
        }

        /// <summary>
        /// 获取计划的写卡区段列表
        /// </summary>
        public InterfaceOutPut GetWriteCardSecList(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            output.resultStr = "返回成功";
            try
            {
                CommonJsonModel cjm = new CommonJsonModel(Data);
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetWriteCardSecList InParams = javaScriptSerializer.Deserialize<InGetWriteCardSecList>(Data);
                OutGetWriteCardSecList OutParams = new OutGetWriteCardSecList();

                DBPlan_WriteCardSection dbWriteCard = new DBPlan_WriteCardSection(SqlHelper.ConnString);
                PlanWriteCardSectionQueryCondition condition = new PlanWriteCardSectionQueryCondition();
                condition.strTrainPlanGUID = InParams.strTrainPlanGUID;
                OutParams.WriteCardSecList = dbWriteCard.GetDataList(condition);
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetWriteCardSecList:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 设置计划的写卡区段列表
        /// <summary>
        ///类名: LCTrainPlan
        ///说明: 设置计划的写卡区段列表
        /// </summary>             

        public class InSetWriteCardSection
        {
            //写卡区段列表
            public PlanWriteCardSectionList writeCardSectionList = new PlanWriteCardSectionList();
        }

        /// <summary>
        /// 设置计划的写卡区段列表
        /// </summary>
        public InterfaceOutPut SetWriteCardSection(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 0;
            try
            {
                CommonJsonModel cjm = new CommonJsonModel(Data);
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InSetWriteCardSection InParams = javaScriptSerializer.Deserialize<InSetWriteCardSection>(Data);
                output.result = 1;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.SetWriteCardSection:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion

        #region 保存转储文件记录
        public class InSubMitRunRecordInfo
        {
            //运行转储文件信息
            public TF.RunSafty.Plan.MD.RunRecordFileMain runRecordFileMain = new TF.RunSafty.Plan.MD.RunRecordFileMain();
        }

        /// <summary>
        /// 提交运行记录转储记录
        /// </summary>
        public InterfaceOutPut SubMitRunRecordInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.ConnectionString = SqlHelper.ConnString;

            DBRunRecordFileMain dbMain = new DBRunRecordFileMain(SqlHelper.ConnString);
            DBRunRecrodDetail dbDetail = new DBRunRecrodDetail(SqlHelper.ConnString);
            try
            {
                CommonJsonModel cjm = new CommonJsonModel(Data);
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InSubMitRunRecordInfo tParams = new InSubMitRunRecordInfo();
                tParams.runRecordFileMain.RunRecordFileDetailList.Add(new TF.RunSafty.Plan.MD.RunRecordFileDetail());
                string str = javaScriptSerializer.Serialize(tParams);
                InSubMitRunRecordInfo InParams = javaScriptSerializer.Deserialize<InSubMitRunRecordInfo>(Data);
                connection.Open();
                transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                string strTrainPlanGUID = "";
                Boolean isExitPlan = true;
                //客户端如未传入计划GUID,则根据工号和当前时间检索数据库内符合的退勤计划
                if (string.IsNullOrEmpty(InParams.runRecordFileMain.strPlanGUID))
                {
                    strTrainPlanGUID = dbMain.getTrainPlanGUID(InParams.runRecordFileMain.strTrainmanNumber1);
                    if (string.IsNullOrEmpty(strTrainPlanGUID))
                    {
                        InParams.runRecordFileMain.strPlanGUID = Guid.NewGuid().ToString();
                        isExitPlan = false;
                    }
                    else
                    {
                        InParams.runRecordFileMain.strPlanGUID = strTrainPlanGUID;
                        isExitPlan = true;
                    }
                }
                else
                {
                    strTrainPlanGUID = InParams.runRecordFileMain.strPlanGUID;
                    isExitPlan = true;
                }

                //判断表中是否已经存在主记录
                int MainCount = dbMain.getMainCount(strTrainPlanGUID);
                string strGUID = "";
                if (MainCount < 1)
                    strGUID = dbMain.Add(command, InParams.runRecordFileMain, isExitPlan,InParams.runRecordFileMain.RunRecordFileDetailList.Count);
                else
                    strGUID = Guid.NewGuid().ToString();


                //向子表中插入转储记录
                for (int i = 0; i < InParams.runRecordFileMain.RunRecordFileDetailList.Count; i++)
                {
                    InParams.runRecordFileMain.RunRecordFileDetailList[i].strRecordGUID = strGUID;
                    dbDetail.Add(command, InParams.runRecordFileMain.RunRecordFileDetailList[i]);
                }
                transaction.Commit();
                output.result = 0;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                output.resultStr = ex.Message;
                LogClass.log("Interface.SubMitRunRecordInfo:" + ex.Message);
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return output;
        }
        #endregion


        #region 获取退勤计划（运行记录转储-最后一个文件结束时间）
        public class EndWorkZhuanChuList_In
        {
            public string BeginTime { get; set; }
            public string EndTime { get; set; }
            public string WorkShopGUID { get; set; }
        }
        public class EndWorkZhuanChuList_Out
        {
            public string result;
            public string resultStr;
            public List<MDZhuanChu> data;
        }
        public EndWorkZhuanChuList_Out GetEndWorkList4ZhuanChuByLastTime(string data)
        {
            EndWorkZhuanChuList_Out json = new EndWorkZhuanChuList_Out();
            EndWorkZhuanChuList_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<EndWorkZhuanChuList_In>(data);
            DBZhuanChu DB = new DBZhuanChu();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                string starttime = input.BeginTime;
                string endtime = input.EndTime;
                string strWorkShopGUID = input.WorkShopGUID;
                List<MDZhuanChu> plans = DB.GetTuiqinPlan4ZhuanChuByLastTime(strWorkShopGUID, starttime, endtime);
                json.data = plans;
                json.result = "0";
                json.resultStr = "提交成功";

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 获取退勤计划（运行记录转储时间）

        public EndWorkZhuanChuList_Out GetEndWorkList4ZhuanChuByCreatTime(string data)
        {
            EndWorkZhuanChuList_Out json = new EndWorkZhuanChuList_Out();
            EndWorkZhuanChuList_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<EndWorkZhuanChuList_In>(data);
            DBZhuanChu DB = new DBZhuanChu();
            TF.Api.Utilities.Validation validater = new TF.Api.Utilities.Validation();
            try
            {
                string starttime = input.BeginTime;
                string endtime = input.EndTime;
                string strWorkShopGUID = input.WorkShopGUID;
                List<MDZhuanChu> plans = DB.GetTuiqinPlan4ZhuanChuByCreatTime(strWorkShopGUID, starttime, endtime);
                json.data = plans;
                json.result = "0";
                json.resultStr = "提交成功";

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }
        #endregion




        #region 根据工号获取指定人员在指定客户端下的出勤计划
        public class BeginWorkByNumber_In
        {
            public string siteID { get; set; }
            public string trainmanNumber { get; set; }
        }
        public class BeginWorkByNumber_Out
        {
            public string result;
            public string resultStr;
            public TrainmanPlan data;
        }

        public class BeginWork_In
        {
            public string siteID { get; set; }
            public string trainmanID { get; set; }
        }
        public class BeginWork_Out
        {
            public string result;
            public string resultStr;
            public TrainmanPlan data;
        }

        public BeginWork_Out GetBeginWorkOfTrainmanByNumber(string data)
        {
            BeginWork_Out json = new BeginWork_Out();
            BeginWorkByNumber_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<BeginWorkByNumber_In>(data);
            TF.RunSafty.BLL.Plan.DB_Plan_BeginWork bllPlan = new TF.RunSafty.BLL.Plan.DB_Plan_BeginWork();
            try
            {
                string clientID = input.siteID;
                string strTrainmanNumber = input.trainmanNumber;
                List<TF.RunSafty.Model.VIEW_Plan_BeginWork> vplans = bllPlan.GetBeginWorkOfTrainmanByNumber(clientID, strTrainmanNumber);
                List<TrainmanPlan> plans = GetPlanList(vplans);
                if (plans != null && plans.Count > 0)
                {
                    json.data = plans[0];
                    json.result = "0";
                    json.resultStr = "提交成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "没有该乘务员的出勤计划";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "提交失败" + ex.Message;
            }
            return json;
        }

        private List<TrainmanPlan> GetPlanList(List<TF.RunSafty.Model.VIEW_Plan_BeginWork> vPlans)
        {
            List<TrainmanPlan> lPlans = new List<TrainmanPlan>();
            TrainmanPlan clientPlan = null;
            if (vPlans != null)
            {
                foreach (TF.RunSafty.Model.VIEW_Plan_BeginWork plan in vPlans)
                {
                    clientPlan = new TrainmanPlan();
                    ChuqinGroup chuqinGroup = new ChuqinGroup();
                    clientPlan.chuqinGroup = chuqinGroup;
                    DutyPlace cPlace = new DutyPlace();
                    chuqinGroup.group = new NameGroup();
                    chuqinGroup.group.place = cPlace;
                    cPlace.placeID = plan.strPlaceID;
                    cPlace.placeName = plan.strPlaceName;
                    chuqinGroup.group.groupID = plan.strGroupGUID;
                    chuqinGroup.group.station = new Station();
                    chuqinGroup.group.station.stationID = plan.strStationGUID;
                    chuqinGroup.group.station.stationName = plan.strStationName;
                    if (plan.strStationNumber.HasValue)
                    {
                        chuqinGroup.group.station.stationNumber = plan.strStationNumber.ToString();
                    }
                    chuqinGroup.group.trainman1 = new TF.RunSafty.Plan.Trainman();
                    chuqinGroup.group.trainman1.ABCD = plan.strABCD1;
                    if (plan.nDriverType1.HasValue)
                    {
                        chuqinGroup.group.trainman1.driverTypeID = plan.nDriverType1.Value;
                    }
                    if (plan.isKey1.HasValue)
                    {
                        chuqinGroup.group.trainman1.isKey = plan.isKey1.Value;
                    }
                    if (plan.nPostID1.HasValue)
                    {
                        chuqinGroup.group.trainman1.postID = plan.nPostID1.Value;
                    }
                    chuqinGroup.group.trainman1.trainmanID = plan.strTrainmanGUID1;
                    chuqinGroup.group.trainman1.trainmanName = plan.strTrainmanName1;
                    chuqinGroup.group.trainman1.trainmanNumber = plan.strTrainmanNumber1;
                    chuqinGroup.group.trainman2 = new TF.RunSafty.Plan.Trainman();
                    chuqinGroup.group.trainman2.ABCD = plan.strABCD2;
                    if (plan.nDriverType2.HasValue)
                    {
                        chuqinGroup.group.trainman2.driverTypeID = plan.nDriverType2.Value;
                    }
                    if (plan.isKey2.HasValue)
                    {
                        chuqinGroup.group.trainman2.isKey = plan.isKey2.Value;
                    }
                    if (plan.nPostID2.HasValue)
                    {
                        chuqinGroup.group.trainman2.postID = plan.nPostID2.Value;
                    }
                    chuqinGroup.group.trainman2.trainmanID = plan.strTrainmanGUID2;
                    chuqinGroup.group.trainman2.trainmanName = plan.strTrainmanName2;
                    chuqinGroup.group.trainman2.trainmanNumber = plan.strTrainmanNumber2;
                    chuqinGroup.group.trainman3 = new TF.RunSafty.Plan.Trainman();
                    chuqinGroup.group.trainman3.ABCD = plan.strABCD3;
                    if (plan.nDriverType3.HasValue)
                    {
                        chuqinGroup.group.trainman3.driverTypeID = plan.nDriverType3.Value;
                    }
                    if (plan.isKey3.HasValue)
                    {
                        chuqinGroup.group.trainman3.isKey = plan.isKey3.Value;
                    }
                    if (plan.nPostID3.HasValue)
                    {
                        chuqinGroup.group.trainman3.postID = plan.nPostID3.Value;
                    }
                    chuqinGroup.group.trainman3.trainmanID = plan.strTrainmanGUID3;
                    chuqinGroup.group.trainman3.trainmanName = plan.strTrainmanName3;
                    chuqinGroup.group.trainman3.trainmanNumber = plan.strTrainmanNumber3;
                    chuqinGroup.group.trainman4 = new TF.RunSafty.Plan.Trainman();
                    chuqinGroup.group.trainman4.ABCD = plan.strABCD4;
                    if (plan.nDriverType4.HasValue)
                    {
                        chuqinGroup.group.trainman4.driverTypeID = plan.nDriverType4.Value;
                    }
                    if (plan.isKey4.HasValue)
                    {
                        chuqinGroup.group.trainman4.isKey = plan.isKey4.Value;
                    }
                    if (plan.nPostID4.HasValue)
                    {
                        chuqinGroup.group.trainman4.postID = plan.nPostID4.Value;
                    }
                    chuqinGroup.group.trainman4.trainmanID = plan.strTrainmanGUID4;
                    chuqinGroup.group.trainman4.trainmanName = plan.strTrainmanName4;
                    chuqinGroup.group.trainman4.trainmanNumber = plan.strTrainmanNumber4;
                    clientPlan.icCheckResult = plan.strICCheckResult;
                    TF.RunSafty.Plan.TrainPlan trainPlan = new TrainPlan();
                    clientPlan.trainPlan = trainPlan;
                    if (plan.dtCreateTime.HasValue)
                    {
                        trainPlan.createTime = plan.dtCreateTime.Value;
                    }
                    trainPlan.createSiteGUID = plan.strCreateSiteGUID;
                    trainPlan.createSiteName = plan.strCreateSiteName;
                    trainPlan.createUserGUID = plan.strCreateUserGUID;
                    trainPlan.createUserName = plan.strCreateUserName;
                    if (plan.nDragType.HasValue)
                    {
                        trainPlan.dragTypeID = plan.nDragType.ToString();
                    }
                    trainPlan.endStationID = plan.strEndStation;
                    trainPlan.endStationName = plan.strEndStationName;
                    if (plan.nKehuoID.HasValue)
                    {
                        trainPlan.kehuoID = plan.nKehuoID.ToString();
                    }
                    trainPlan.kehuoName = plan.strKehuoName;
                    trainPlan.mainPlanGUID = plan.strMainPlanGUID;
                    trainPlan.placeID = plan.strPlaceID;
                    trainPlan.placeName = plan.strPlaceName;
                    trainPlan.planID = plan.strTrainPlanGUID;
                    if (plan.nPlanState.HasValue)
                    {
                        trainPlan.planStateID = plan.nPlanState.Value;
                    }
                    trainPlan.planStateName = plan.strPlanStateName;
                    if (plan.nPlanType.HasValue)
                    {
                        trainPlan.planTypeID = plan.nPlanType.ToString();
                    }
                    trainPlan.planTypeName = plan.strPlanTypeName;
                    if (plan.nRemarkType.HasValue)
                    {
                        trainPlan.remarkTypeID = plan.nRemarkType.ToString();
                    }
                    trainPlan.remarkTypeName = plan.strRemarkTypeName;

                    trainPlan.startStationID = plan.strStartStation;
                    trainPlan.startStationName = plan.strStartStationName;
                    trainPlan.dragTypeName = plan.nDragTypeName;
                    trainPlan.planID = plan.strTrainPlanGUID;
                    trainPlan.trainJiaoluGUID = plan.strTrainJiaoluGUID;
                    trainPlan.trainJiaoluName = plan.strTrainJiaoluName;
                    trainPlan.strTrainPlanGUID = plan.strTrainPlanGUID;
                    if (plan.dtRealStartTime.HasValue)
                    {
                        trainPlan.realStartTime = plan.dtRealStartTime.Value;
                    }
                    if (plan.dtStartTime.HasValue)
                    {
                        trainPlan.startTime = plan.dtStartTime.Value;
                    }
                    if (plan.dtFirstStartTime.HasValue)
                    {
                        trainPlan.firstStartTime = plan.dtFirstStartTime.Value;
                    }
                    if (plan.dtChuQinTime.HasValue)
                    {
                        clientPlan.beginWorkTime = plan.dtChuQinTime.Value;
                        clientPlan.trainPlan.kaiCheTime = plan.dtChuQinTime.Value;
                    }
                    else
                    {
                    }
                    if (plan.nTrainmanTypeID.HasValue)
                    {
                        trainPlan.trainmanTypeID = plan.nTrainmanTypeID.ToString();
                    }
                    trainPlan.trainNo = plan.strTrainNo;
                    trainPlan.trainNumber = plan.strTrainNumber;
                    trainPlan.trainTypeName = plan.strTrainTypeName;
                    trainPlan.trainmanTypeName = plan.strTrainmanTypeName;
                    if (plan.nVerifyID1.HasValue)
                    {
                        chuqinGroup.verifyID1 = plan.nVerifyID1.Value;
                    }
                    chuqinGroup.testAlcoholInfo1 = new TestAlcoholInfo();
                    chuqinGroup.testAlcoholInfo1.picture = "";
                    if (plan.dtTestTime1.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo1.testTime = plan.dtTestTime1.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult1.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo1.testAlcoholResult = plan.nDrinkResult1.Value;
                    }

                    if (plan.nVerifyID2.HasValue)
                    {
                        chuqinGroup.verifyID2 = plan.nVerifyID2.Value;
                    }
                    chuqinGroup.testAlcoholInfo2 = new TestAlcoholInfo();
                    chuqinGroup.testAlcoholInfo2.picture = "";
                    if (plan.dtTestTime2.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo2.testTime = plan.dtTestTime2.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult2.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo2.testAlcoholResult = plan.nDrinkResult2.Value;
                    }

                    if (plan.nVerifyID3.HasValue)
                    {
                        chuqinGroup.verifyID3 = plan.nVerifyID3.Value;
                    }
                    chuqinGroup.testAlcoholInfo3 = new TestAlcoholInfo();
                    chuqinGroup.testAlcoholInfo3.picture = "";
                    if (plan.dtTestTime3.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo3.testTime = plan.dtTestTime3.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult3.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo3.testAlcoholResult = plan.nDrinkResult3.Value;
                    }

                    if (plan.nVerifyID4.HasValue)
                    {
                        chuqinGroup.verifyID4 = plan.nVerifyID4.Value;
                    }
                    chuqinGroup.testAlcoholInfo4 = new TestAlcoholInfo();
                    chuqinGroup.testAlcoholInfo4.picture = "";
                    if (plan.dtTestTime4.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo4.testTime = plan.dtTestTime4.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (plan.nDrinkResult4.HasValue)
                    {
                        chuqinGroup.testAlcoholInfo4.testAlcoholResult = plan.nDrinkResult4.Value;
                    }
                    lPlans.Add(clientPlan);
                }
            }
            return lPlans;
        }

        #endregion
        public class QueryCQRecord_In
        {
            public string begintime { get; set; }
            public string endtime { get; set; }

            public string siteID { get; set; }
        }
 
        public static InterfaceOutPut QueryCQRecord(string Data)
        {
            BeginWork_Out json = new BeginWork_Out();
            QueryCQRecord_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryCQRecord_In>(Data);
            InterfaceOutPut result = new InterfaceOutPut();
            List<TF.RunSafty.Model.InterfaceModel.mChuqinPlansOfClient> cqPlans = new TF.RunSafty.BLL.Plan.DB_Plan_BeginWork().GetCQTZ(input.siteID, input.begintime, input.endtime);
            result.data = cqPlans;
            return result;
        }

        public class QueryTQRecord_In
        {
            public string begintime { get; set; }
            public string endtime { get; set; }

            public string siteID { get; set; }
        }
        public static InterfaceOutPut QueryTQRecord(string Data)
        {
            BeginWork_Out json = new BeginWork_Out();
            QueryTQRecord_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryTQRecord_In>(Data);
            InterfaceOutPut result = new InterfaceOutPut();
            List<TF.RunSafty.Model.InterfaceModel.mTuiqinPlansOfSite> cqPlans = new DB_Plan_EndWork().GetTQTZ(input.siteID, input.begintime, input.endtime);
            result.data = cqPlans;
            return result;
        }
    }

    /// <summary>
    ///类名: LCDDML
    ///说明: 审核调令
    /// </summary>
    public class LCDDML
    {
        public class InConfirmDDML
        {
            //调令审核
            public DDML_Confirm DDML_Confirm = new DDML_Confirm();
        }

        /// <summary>
        /// 审核调令
        /// </summary>
        public InterfaceOutPut ConfirmDDML(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                CommonJsonModel cjm = new CommonJsonModel(Data);
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InConfirmDDML inparam = new InConfirmDDML();
                string str = javaScriptSerializer.Serialize(inparam);
                InConfirmDDML InParams = javaScriptSerializer.Deserialize<InConfirmDDML>(Data);
                DBDDML_Confirm db = new DBDDML_Confirm(SqlHelper.ConnString);
                db.Save(InParams.DDML_Confirm);

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.ConfirmDDML:" + ex.Message);
                throw ex;
            }
            return output;
        }
        public class IngetLastDDMlConfirm
        {
            //车间guid
            public string strClientGUID;
        }

        public class OutgetLastDDMlConfirm
        {
            //调令审核
            public DDML_Confirm DDML_Confirm = new DDML_Confirm();
        }

        /// <summary>
        /// 获取车间最新调令审核记录
        /// </summary>
        public InterfaceOutPut getLastDDMlConfirm(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                CommonJsonModel cjm = new CommonJsonModel(Data);
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                IngetLastDDMlConfirm InParams = javaScriptSerializer.Deserialize<IngetLastDDMlConfirm>(Data);
                OutgetLastDDMlConfirm OutParams = new OutgetLastDDMlConfirm();
                DBDDML_Confirm db = new DBDDML_Confirm(SqlHelper.ConnString);


                DDML_Confirm DDML_Confirm = db.GetModel(InParams.strClientGUID);
                if (DDML_Confirm == null)
                {
                    OutParams = null;
                }
                else
                {
                    OutParams.DDML_Confirm = DDML_Confirm;
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.getLastDDMlConfirm:" + ex.Message);
                throw ex;
            }
            return output;
        }

    }


    public class WORKTYPE
    {
        //工作类型为出勤
        public static int WORKTYPE_BEGIN = 2;
        //工作类型为退勤
        public static int WORKTYPE_END = 3;
    
    }


}