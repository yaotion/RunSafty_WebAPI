using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;


/// <summary>
/// 计划流事件类型枚举
/// </summary>
public enum EventTypeEnmu
{
    eteNull = 0,
    eteInRoom = 10001,
    eteOutRoom = 10002,
    eteBeginWork = 10003,
    eteOutDepots = 10004,
    eteEndWork = 10005,
    eteInDepots = 10006,
    eteEnterStation = 10007,
    eteLeaveStation = 10008,
    eteVerifyCard = 20001,
    eteDrinkTest = 30001
}
/// <summary>
/// 机组数据类
/// </summary>
public class TrainmanGroup
{
    public TrainmanGroup()
    { }
    /// <summary>
    ///机组GUID
    /// </summary>
    public string GroupGUID = "";
    /// <summary>
    /// 计划GUID
    /// </summary>
    public string TrainPlanGUID = "";
    /// <summary>
    /// 司机工号
    /// </summary>
    public string TrainmanNumber1 = "";
    /// <summary>
    /// 司机姓名
    /// </summary>
    public string TrainmanNumber2 = "";
}
/// <summary>
/// 机车计划数据类
/// </summary>
public class TrainPlan
{
    public string TrainPlanGUID = "";
    public string TrainNo = "";
    public string TrainNumber = "";
    public string TrainTypeName = "";
}
/// <summary>
/// 计划流事件数据类
/// </summary>
public class EventInfo
{
    public EventInfo()
    { }
    public string RunEventGUID = "";
    public string TrainPlanGUID = "";
    public EventTypeEnmu EventID = EventTypeEnmu.eteNull;
    public DateTime EventTime = DateTime.MinValue;
    public string TrainNo = "";
    public string TrainTypeName = "";
    public string TrainNumber = "";
    public Int32 nTMIS = 0;
    public Int32 nRealTMIS = 0;
    public Int32 KeHuoID = -1;
    public string GroupGUID = "";
    public string TrainmanNumber1 = "";
    public string TrainmanNumber2 = "";
    public string TrainmanName1 = "";
    public string TrainmanName2 = "";
    public DateTime CreateTime = DateTime.MinValue;
    public string FlowID = "";
    public int nID = 0;
    public int nResult = 0;
    public string strResult = "";
   
}

public class EventList
{
    public List<EventInfo> Items = new List<EventInfo>();
}
/// <summary>
///WEB接口数据库操作类
/// </summary>
public class DBInterface
{
	public DBInterface()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static DataTable GetAllAlias()
    {
        string strSql = "select * from TAB_Base_StationInWorkShop_TMIS order by strRecordGUID";
        SqlParameter[] sqlParams = {
                                      
                                   };
        return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
    }
    public static bool IsAlias(Int32 TMIS, StationInWorkShop station, DataTable dtAlias)
    {
        for (int i = 0; i < dtAlias.Rows.Count; i++)
        {
            if ((dtAlias.Rows[i]["strRecordGUID"].ToString() == station.RecordGUID) && (dtAlias.Rows[i]["nTMIS"].ToString() == TMIS.ToString()))
            {
                return true;
            }
        }       
        return false;
    }
    public static int GetAlias(Int32 TMIS, List<StationInWorkShop> stationList, DataTable dtAlias)
    {
       
        for (int i = 0; i < stationList.Count; i++)
        {
            if (stationList[i].TMIS == TMIS)
            {
                return TMIS;
            }
            for (int k = 0; k < dtAlias.Rows.Count; k++)
            {
                if ((dtAlias.Rows[k]["strRecordGUID"].ToString() == stationList[i].RecordGUID) && (dtAlias.Rows[k]["nTMIS"].ToString() == TMIS.ToString()))
                {
                    return stationList[i].TMIS;
                }
            }            
        }
        
        return 0;
    }
    /// <summary>
    /// 根据司机工号获取司机所在的机组信息
    /// </summary>
    /// <param name="TrainmanNumber">司机工号</param>
    /// <param name="Group">司机所在的机组信息</param>
    /// <returns></returns>
    public static Boolean GetTrainmanGroup(string TrainmanNumber, TrainmanGroup Group)
    {
        string strSql = "select * from View_Nameplate_Group where strTrainmanNumber1=@TrainmanNumber or strTrainmanNumber2=@TrainmanNumber";
        SqlParameter[] sqlParams = {
                                       new SqlParameter("TrainmanNumber",TrainmanNumber)
                                   };
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        if (dt.Rows.Count > 0)
        {
            Group.GroupGUID = dt.Rows[0]["strGroupGUID"].ToString();
            Group.TrainPlanGUID = dt.Rows[0]["strTrainPlanGUID"].ToString();
            Group.TrainmanNumber1 = dt.Rows[0]["strTrainmanNumber1"].ToString();
            Group.TrainmanNumber2 = dt.Rows[0]["strTrainmanNumber2"].ToString();
            return true;
        }
        return false;
    }

    public static bool GetTrainmanPlan(string TrainmaNumber,DateTime EventTime,ref Int32 TrainmanIndex, TrainPlan Plan)
    {
        //获取计划信息
        string strSqlPlan = @"select top 1 * from VIEW_Plan_Trainman where @EventTime >= dateAdd(n,-120,dtStartTime) and @EventTime<=dateAdd(hh,30,dtStartTime)
        and nPlanState >= 4
        and  ((strTrainmanNumber1 = @TrainmaNumber) or (strTrainmanNumber2 = @TrainmaNumber)) order by dtStartTime desc";
        SqlParameter[] sqlParamPlan = {
                                        new SqlParameter("EventTime",EventTime)   ,
                                        new SqlParameter("TrainmaNumber",TrainmaNumber)   
                                      };
        DataTable dtPlan = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlPlan, sqlParamPlan).Tables[0];
        if (dtPlan.Rows.Count > 0)
        {
            Plan.TrainPlanGUID = dtPlan.Rows[0]["strTrainPlanGUID"].ToString();
            Plan.TrainNo = dtPlan.Rows[0]["strTrainNo"].ToString();
            Plan.TrainTypeName = dtPlan.Rows[0]["strTrainNumber"].ToString();
            Plan.TrainNumber = dtPlan.Rows[0]["strTrainTypeName"].ToString();
            if (dtPlan.Rows[0]["strTrainmanNumber1"].ToString() == TrainmaNumber)
            {
                TrainmanIndex = 1;
            }
            if (dtPlan.Rows[0]["strTrainmanNumber2"].ToString() == TrainmaNumber)
            {
                TrainmanIndex = 2;
            }
            if (dtPlan.Rows[0]["strTrainmanNumber3"].ToString() == TrainmaNumber)
            {
                TrainmanIndex = 3;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 根据正副司机获取机组信息
    /// </summary>
    /// <param name="TrainmanNumber1">乘务员1工号</param>
    /// <param name="TrainmanNumber2">乘务员2工号</param>
    /// <param name="Group">乘务员所在的机组</param>
    /// <returns>是否找到机组</returns>
    public static Boolean GetTrainmansGroup(string TrainmanNumber1, string TrainmanNumber2, TrainmanGroup Group)
    {
        string strSql = "select * from View_Nameplate_Group where (strTrainmanNumber1=@TrainmanNumber1 and strTrainmanNumber2=@TrainmanNumber2) or (strTrainmanNumber2=@TrainmanNumber1 and strTrainmanNumber1=@TrainmanNumber2)";
        SqlParameter[] sqlParams = {
                                       new SqlParameter("TrainmanNumber1",TrainmanNumber1),
                                       new SqlParameter("TrainmanNumber2",TrainmanNumber2)
                                   };
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        if (dt.Rows.Count > 0)
        {
            Group.GroupGUID = dt.Rows[0]["strGroupGUID"].ToString();
            Group.TrainPlanGUID = dt.Rows[0]["strTrainPlanGUID"].ToString();
            Group.TrainmanNumber1 = dt.Rows[0]["strTrainmanNumber1"].ToString();
            Group.TrainmanNumber2 = dt.Rows[0]["strTrainmanNumber2"].ToString();
            return true;
        }
        return false;
    }
    /// <summary>
    /// 获取机车计划信息
    /// </summary>
    /// <param name="TrainPlanGUID">机车计划GUID</param>
    /// <param name="Plan">返回的计划</param>
    /// <returns>是否成功</returns>
    public static Boolean GetTrainPlan(string TrainPlanGUID, TrainPlan Plan)
    {
        //获取计划信息
        string strSqlPlan = "select top 1 * from TAB_Plan_Train where strTrainPlanGUID = @strTrainPlanGUID";
        SqlParameter[] sqlParamPlan = {
                                        new SqlParameter("strTrainPlanGUID",TrainPlanGUID)   
                                      };
        DataTable dtPlan = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlPlan, sqlParamPlan).Tables[0];
        if (dtPlan.Rows.Count > 0)
        {
            Plan.TrainPlanGUID = dtPlan.Rows[0]["strTrainPlanGUID"].ToString();
            Plan.TrainNo = dtPlan.Rows[0]["strTrainNo"].ToString();
            Plan.TrainTypeName = dtPlan.Rows[0]["strTrainNumber"].ToString();
            Plan.TrainNumber = dtPlan.Rows[0]["strTrainTypeName"].ToString();
            return true;
        }
        return false;
    }
    /// <summary>
    /// 获取指定类型的计划流信息
    /// </summary>
    /// <param name="TrainPlanGUID">机车计划GUID</param>
    /// <param name="EventType">事件类型</param>
    /// <param name="PEvent">返回的计划流时间</param>
    /// <returns>是否成功</returns>
    public static bool GetPlanEvent(string FlowID, EventTypeEnmu EventType, EventInfo PEvent)
    {
        string strSql = "select top 1 * from TAB_Plan_RunEvent where strFlowID = @strFlowID and nEventID = @EventID order by dtEventTime desc ";
        SqlParameter[] sqlParams = {
                                        new SqlParameter("strFlowID",FlowID),
                                        new SqlParameter("EventID",EventType)      
                                   };
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        if (dt.Rows.Count > 0)
        {
            PEvent.RunEventGUID = dt.Rows[0]["strRunEventGUID"].ToString();
            PEvent.TrainPlanGUID = dt.Rows[0]["strTrainPlanGUID"].ToString();
            PEvent.EventID = (EventTypeEnmu)int.Parse(dt.Rows[0]["nEventID"].ToString());
            PEvent.EventTime = DateTime.Parse(dt.Rows[0]["dtEventTime"].ToString());
            PEvent.TrainNo = dt.Rows[0]["strTrainNo"].ToString();
            PEvent.TrainTypeName = dt.Rows[0]["strTrainTypeName"].ToString();
            PEvent.TrainNumber = dt.Rows[0]["strTrainNumber"].ToString();
            PEvent.nTMIS = int.Parse(dt.Rows[0]["nTMIS"].ToString());
            PEvent.KeHuoID = int.Parse(dt.Rows[0]["nKeHuo"].ToString());
            PEvent.GroupGUID = dt.Rows[0]["strGroupGUID"].ToString(); ;
            PEvent.TrainmanNumber1 = dt.Rows[0]["strTrainmanNumber1"].ToString();
            PEvent.TrainmanNumber2 = dt.Rows[0]["strTrainmanNumber2"].ToString();
            PEvent.CreateTime = DateTime.Parse(dt.Rows[0]["dtCreateTime"].ToString());
            PEvent.nID = int.Parse(dt.Rows[0]["nID"].ToString());
            PEvent.nResult = int.Parse(dt.Rows[0]["nResult"].ToString());
            PEvent.strResult = dt.Rows[0]["strResult"].ToString();
            PEvent.FlowID = dt.Rows[0]["strFlowID"].ToString();
            return true;
        }
        return false;
    }

    public static bool GetPlanEventOfGroup(string TrainPlanGUID, EventTypeEnmu EventType, EventInfo PEvent)
    {
        string strSql = "select top 1 * from TAB_Plan_RunEvent where strTrainPlanGUID = @TrainPlanGUID and  nEventID=@EventID order by dtEventTime desc ";
        SqlParameter[] sqlParams = {
                                        new SqlParameter("TrainPlanGUID",TrainPlanGUID),
                                        new SqlParameter("EventID",EventType)
                                   };
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        if (dt.Rows.Count > 0)
        {
          
            PEvent.RunEventGUID = dt.Rows[0]["strRunEventGUID"].ToString();
            PEvent.TrainPlanGUID = dt.Rows[0]["strTrainPlanGUID"].ToString();
            PEvent.EventID = (EventTypeEnmu)int.Parse(dt.Rows[0]["nEventID"].ToString());
            PEvent.EventTime = DateTime.Parse(dt.Rows[0]["dtEventTime"].ToString());
            PEvent.TrainNo = dt.Rows[0]["strTrainNo"].ToString();
            PEvent.TrainTypeName = dt.Rows[0]["strTrainTypeName"].ToString();
            PEvent.TrainNumber = dt.Rows[0]["strTrainNumber"].ToString();
            PEvent.nTMIS = int.Parse(dt.Rows[0]["nTMIS"].ToString());
            PEvent.KeHuoID = int.Parse(dt.Rows[0]["nKeHuo"].ToString());
            PEvent.GroupGUID = dt.Rows[0]["strGroupGUID"].ToString(); ;
            PEvent.TrainmanNumber1 = dt.Rows[0]["strTrainmanNumber1"].ToString();
            PEvent.TrainmanNumber2 = dt.Rows[0]["strTrainmanNumber2"].ToString();
            PEvent.CreateTime = DateTime.Parse(dt.Rows[0]["dtCreateTime"].ToString());
            PEvent.nID = int.Parse(dt.Rows[0]["nID"].ToString());
            PEvent.nResult = int.Parse(dt.Rows[0]["nResult"].ToString());
            PEvent.strResult = dt.Rows[0]["strResult"].ToString();
            PEvent.FlowID = dt.Rows[0]["strFlowID"].ToString();
            return true;
        }
        return false;
    }
    /// <summary>
    /// 添加计划流
    /// </summary>
    /// <param name="SourceEvent">计划流信息</param>
    public static void AddPlanEvent(EventInfo SourceEvent)
    {
        string strSql = @"insert into TAB_Plan_RunEvent (strRunEventGUID,strTrainPlanGUID,nEventID,dtEventTime,strTrainNo,strTrainTypeName,
            strTrainNumber,nTMIS,nKeHuo,strGroupGUID,strTrainmanNumber1,strTrainmanNumber2,dtCreateTime,nResult,strResult,strFlowID) values 
            (@strRunEventGUID,@strTrainPlanGUID,@nEventID,@dtEventTime,@strTrainNo,@strTrainTypeName,
            @strTrainNumber,@nTMIS,@nKeHuo,@strGroupGUID,@strTrainmanNumber1,@strTrainmanNumber2,getdate(),@nResult,@strResult,@strFlowID)";
        SqlParameter[] sqlParams = {
                                            new SqlParameter("strRunEventGUID",SourceEvent.RunEventGUID),
                                            new SqlParameter("strTrainPlanGUID",SourceEvent.TrainPlanGUID),
                                            new SqlParameter("nEventID",SourceEvent.EventID),
                                            new SqlParameter("dtEventTime",SourceEvent.EventTime.ToString("yyyy-MM-dd HH:mm:ss")),
                                            new SqlParameter("strTrainNo",SourceEvent.TrainNo),
                                            new SqlParameter("strTrainTypeName",SourceEvent.TrainTypeName),
                                            new SqlParameter("strTrainNumber",SourceEvent.TrainNumber),
                                            new SqlParameter("nTMIS",SourceEvent.nTMIS),
                                            new SqlParameter("nKeHuo",SourceEvent.KeHuoID),
                                            new SqlParameter("strGroupGUID",SourceEvent.GroupGUID),
                                            new SqlParameter("strTrainmanNumber1",SourceEvent.TrainmanNumber1),
                                            new SqlParameter("strTrainmanNumber2",SourceEvent.TrainmanNumber2),
                                            new SqlParameter("nResult",SourceEvent.nResult),
                                            new SqlParameter("strResult",SourceEvent.strResult),
                                            new SqlParameter("strFlowID",SourceEvent.FlowID)                                            
                                       };
        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
    }
    /// <summary>
    /// 判断计划是否已经存在
    /// </summary>
    /// <param name="SourceEvent">计划流信息</param>
    public static bool ExistEvent(EventInfo SourceEvent)
    {
        string strSql = @"select top 1 * from TAB_Plan_RunEvent where strTrainPlanGUID = @TrainPlanGUID and nEventID = @EventID and dtEventTime >=@BeginEventTime and dtEventTime <= @EndEventTime";
        SqlParameter[] sqlParams = {
                                           
                                            new SqlParameter("TrainPlanGUID",SourceEvent.TrainPlanGUID),
                                            new SqlParameter("EventID",SourceEvent.EventID),                                                                                       
                                            new SqlParameter("BeginEventTime",SourceEvent.EventTime.AddMinutes(-1)),
                                            new SqlParameter("EndEventTime",SourceEvent.EventTime.AddMinutes(1))
                                                                               
                                       };
        return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0].Rows.Count > 0;
    }
    

    
    /// <summary>
    /// 修改计划流的人员和时间信息
    /// </summary>
    /// <param name="RunEventGUID"></param>
    /// <param name="TrainmanNumber1"></param>
    /// <param name="TrainmanNumber2"></param>
    /// <param name="EventTime"></param>
    public static void UpdateEventTrainman(string RunEventGUID, string TrainmanNumber1, string TrainmanNumber2, DateTime EventTime)
    {
        string strSql = "update TAB_Plan_RunEvent set strTrainmanNumber1=@strTrainmanNumber1,strTrainmanNumber2=@strTrainmanNumber2,dtEventTime=@dtEventTime where strRunEventGUID=@strRunEventGUID";
        SqlParameter[] sqlParams = {
                                            new SqlParameter("strRunEventGUID",RunEventGUID),                     
                                            new SqlParameter("strTrainmanNumber1",TrainmanNumber1),
                                            new SqlParameter("strTrainmanNumber2",TrainmanNumber2),
                                            new SqlParameter("dtEventTime",EventTime.ToString("yyyy-MM-dd HH:mm:ss"))
                                       };
        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        return;
    }
    /// <summary>
    /// 获取指定时间段内的所有人员的事件信息，按照人员工号和时间顺序排序
    /// </summary>
    /// <param name="BeginTime"></param>
    /// <param name="EndTime"></param>
    /// <returns></returns>
    public static EventList GetEventList(DateTime BeginTime, DateTime EndTime,string WorkShopGUID,string KehuoID)
    {
        EventList Result = new EventList();

        string strSql = @"select *,(select top 1 strTrainmanName from tab_org_trainman where strTrainmanNumber = VIEW_Plan_RunEvent.strTrainmanNumber1) as TrainmanName1,
            (select top 1 strTrainmanName from tab_org_trainman where strTrainmanNumber = VIEW_Plan_RunEvent.strTrainmanNumber2) as TrainmanName2
            from VIEW_Plan_RunEvent where dtEventTime >= @BeginTime and dtEventTime < @EndTime ";
        if (WorkShopGUID != "")
        {
            strSql += " and (strWorkShopGUID1 = @WorkShopGUID or strWorkShopGUID2=@WorkShopGUID)";
        }
        if (KehuoID != "")
        {
            strSql += " and nKehuoID = @KehuoID";
        }
        strSql += " order by strGroupGUID,dtEventTime";
                                                                                                  
        SqlParameter[] sqlParams = {
                                       new SqlParameter("BeginTime",BeginTime.ToString("yyyy-MM-dd HH:00:00")),
                                       new SqlParameter("EndTime",EndTime.ToString("yyyy-MM-dd HH:00:00")),
                                       new SqlParameter("WorkShopGUID",WorkShopGUID),
                                       new SqlParameter("KehuoID",KehuoID)
                                   };
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            EventInfo PEvent = new EventInfo();
            PEvent.RunEventGUID = dt.Rows[i]["strRunEventGUID"].ToString();
            PEvent.TrainPlanGUID = dt.Rows[i]["strTrainPlanGUID"].ToString();
            PEvent.EventID = (EventTypeEnmu)int.Parse(dt.Rows[i]["nEventID"].ToString());
            PEvent.EventTime = DateTime.Parse(dt.Rows[i]["dtEventTime"].ToString());
            PEvent.TrainNo = dt.Rows[i]["strTrainNo"].ToString();
            PEvent.TrainTypeName = dt.Rows[i]["strTrainTypeName"].ToString();
            PEvent.TrainNumber = dt.Rows[i]["strTrainNumber"].ToString();
            PEvent.nTMIS = int.Parse(dt.Rows[i]["nTMIS"].ToString());
            PEvent.KeHuoID = int.Parse(dt.Rows[i]["nKeHuoID"].ToString());
            PEvent.GroupGUID = dt.Rows[i]["strGroupGUID"].ToString(); ;
            PEvent.TrainmanNumber1 = dt.Rows[i]["strTrainmanNumber1"].ToString();
            PEvent.TrainmanNumber2 = dt.Rows[i]["strTrainmanNumber2"].ToString();
            PEvent.TrainmanName1 = dt.Rows[i]["TrainmanName1"].ToString();
            PEvent.TrainmanName2 = dt.Rows[i]["TrainmanName2"].ToString();
            PEvent.CreateTime = DateTime.Parse(dt.Rows[i]["dtCreateTime"].ToString());
            PEvent.nID = int.Parse(dt.Rows[i]["nID"].ToString());
            PEvent.nResult = int.Parse(dt.Rows[i]["nResult"].ToString());
            PEvent.strResult = dt.Rows[i]["strResult"].ToString();
            PEvent.FlowID = dt.Rows[i]["strFlowID"].ToString();           
            Result.Items.Add(PEvent);
        }
        return Result;
    }
    /// <summary>
    /// 获取该流程内最新的一条非退勤或入寓的记录
    /// </summary>
    /// <param name="FlowID">流程GUID</param>
    /// <param name="lastEvent">最新的记录</param>
    /// <returns>是否有记录</returns>
    public static bool GetLastRunEvent(string FlowID,EventInfo LastEvent)
    {
        string strSql = "select top 1 * from TAB_Plan_RunEvent where strFlowID = @strFlowID order by dtEventTime desc ";
        SqlParameter[] sqlParams = {
                                        new SqlParameter("strFlowID",FlowID)     
                                   };
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        if (dt.Rows.Count > 0)
        {
            string[] a = {"",""};
            if (((EventTypeEnmu)int.Parse(dt.Rows[0]["nEventID"].ToString()) == EventTypeEnmu.eteEndWork) || ((EventTypeEnmu)int.Parse(dt.Rows[0]["nEventID"].ToString()) == EventTypeEnmu.eteInRoom))
                return false;
            LastEvent.RunEventGUID = dt.Rows[0]["strRunEventGUID"].ToString();
            LastEvent.TrainPlanGUID = dt.Rows[0]["strTrainPlanGUID"].ToString();
            LastEvent.EventID = (EventTypeEnmu)int.Parse(dt.Rows[0]["nEventID"].ToString());
            LastEvent.EventTime = DateTime.Parse(dt.Rows[0]["dtEventTime"].ToString());
            LastEvent.TrainNo = dt.Rows[0]["strTrainNo"].ToString();
            LastEvent.TrainTypeName = dt.Rows[0]["strTrainTypeName"].ToString();
            LastEvent.TrainNumber = dt.Rows[0]["strTrainNumber"].ToString();
            LastEvent.nTMIS = int.Parse(dt.Rows[0]["nTMIS"].ToString());
            LastEvent.KeHuoID = int.Parse(dt.Rows[0]["nKeHuo"].ToString());
            LastEvent.GroupGUID = dt.Rows[0]["strGroupGUID"].ToString(); ;
            LastEvent.TrainmanNumber1 = dt.Rows[0]["strTrainmanNumber1"].ToString();
            LastEvent.TrainmanNumber2 = dt.Rows[0]["strTrainmanNumber2"].ToString();
            LastEvent.CreateTime = DateTime.Parse(dt.Rows[0]["dtCreateTime"].ToString());
            LastEvent.nID = int.Parse(dt.Rows[0]["nID"].ToString());
            LastEvent.nResult = int.Parse(dt.Rows[0]["nResult"].ToString());
            LastEvent.strResult = dt.Rows[0]["strResult"].ToString();
            LastEvent.FlowID = dt.Rows[0]["strFlowID"].ToString();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 获取机组的流程GUID
    /// </summary>
    /// <param name="GroupGUID">机组的GUID</param>
    /// <returns>机组的GUID</returns>
    public static string GetFlowOfGroup(string GroupGUID)
    {
        string result = "";
        string strSql = "select top 1 * from TAB_Plan_RunEvent where strGroupGUID = @strGroupGUID order by dtEventTime desc ";
        SqlParameter[] sqlParams = {
                                        new SqlParameter("strGroupGUID",GroupGUID)     
                                   };
        DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        if (dt.Rows.Count > 0)
        {
            string[] a = { "", "" };
            if (((EventTypeEnmu)int.Parse(dt.Rows[0]["nEventID"].ToString()) == EventTypeEnmu.eteEndWork) || ((EventTypeEnmu)int.Parse(dt.Rows[0]["nEventID"].ToString()) == EventTypeEnmu.eteInRoom))
                return result;

            return dt.Rows[0]["strFlowID"].ToString();
        }
        return result;
    }

    /// <summary>
    /// 获取乘务员状态
    /// </summary>

    public static DataTable GetTrainmanState()
    {
        string strSql = @"select tm.*,p.nPlanState,w.strWorkShopName,
(select top 1 nEventID from tab_plan_RunEvent where tm.strTrainPlanGUID = strTrainPlanGUID order by dtEventTime desc) as nEventID
 from 
(
select strTrainmanNumber1,strWorkShopGUID1,strTrainPlanGUID from VIEW_Nameplate_Group where len(strTrainmanGUID1) > 0
union
select strTrainmanNumber2,strWorkShopGUID2,strTrainPlanGUID from VIEW_Nameplate_Group where len(strTrainmanGUID2) > 0
union
select strTrainmanNumber3,strWorkShopGUID3,strTrainPlanGUID from VIEW_Nameplate_Group where len(strTrainmanGUID3) > 0
union
select strTrainmanNumber4,strWorkShopGUID4,strTrainPlanGUID from VIEW_Nameplate_Group where len(strTrainmanGUID4) > 0
) as tm
left join tab_plan_train as p on  tm.strTrainPlanGUID = p.strTrainPlanGUID 
left join TAB_Org_WorkShop as w on  tm.strWorkShopGUID1 = w.strWorkShopGUID";
        return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
    }
}
