using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.CommonUtility;
using System.Collections.Generic;


namespace TF.RunSafty.BeginworkFlow
{
  /// <summary>
  ///类名: Plan_Beginwork_FlowQueryCondition
  ///说明: 出勤计划GUID查询条件类
  /// </summary>
  public class Plan_Beginwork_FlowQueryCondition
  {
    public int page = 0;
    public int rows = 0;
    //
    public int nID = 0;
    //出勤计划GUID
    public string strTrainPlanGUID = "";
    //值班员姓名
    public string strUserName = "";
    //值班员工号
    public string strUserNumber = "";
    //确认时间
    public DateTime? dtConfirmTime;
    //确认类型(0自动,1手工)
    public int? nConfirmType = 0;
    //
    public string strConfirmBrief = "";
    //流程状态
    public int? nFlowState = 0;
    //创建时间
    public DateTime? dtCreateTime;
    //开始时间
    public DateTime? dtBeginTime;
    //结束时间
    public DateTime? dtEndTime;
    //执行时长(单位分钟)
    public int? nExecLength = 0;
    public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
    {
      SqlCondition = new StringBuilder();
      SqlCondition.Append(nID != null ? " and nID = @nID" : "");
      SqlCondition.Append(strTrainPlanGUID != "" ? " and strTrainPlanGUID = @strTrainPlanGUID" : "");
      SqlCondition.Append(strUserName != "" ? " and strUserName = @strUserName" : "");
      SqlCondition.Append(strUserNumber != "" ? " and strUserNumber = @strUserNumber" : "");
      SqlCondition.Append(dtConfirmTime != null ? " and dtConfirmTime = @dtConfirmTime" : "");
      SqlCondition.Append(nConfirmType != null ? " and nConfirmType = @nConfirmType" : "");
      SqlCondition.Append(strConfirmBrief != "" ? " and strConfirmBrief = @strConfirmBrief" : "");
      SqlCondition.Append(nFlowState != null ? " and nFlowState = @nFlowState" : "");
      SqlCondition.Append(dtCreateTime != null ? " and dtCreateTime = @dtCreateTime" : "");
      SqlCondition.Append(dtBeginTime != null ? " and dtBeginTime = @dtBeginTime" : "");
      SqlCondition.Append(dtEndTime != null ? " and dtEndTime = @dtEndTime" : "");
      SqlCondition.Append(nExecLength != null ? " and nExecLength = @nExecLength" : "");
      SqlParameter[] sqlParams ={
          new SqlParameter("nID",nID),
          new SqlParameter("strTrainPlanGUID",strTrainPlanGUID),
          new SqlParameter("strUserName",strUserName),
          new SqlParameter("strUserNumber",strUserNumber),
          new SqlParameter("dtConfirmTime",dtConfirmTime),
          new SqlParameter("nConfirmType",nConfirmType),
          new SqlParameter("strConfirmBrief",strConfirmBrief),
          new SqlParameter("nFlowState",nFlowState),
          new SqlParameter("dtCreateTime",dtCreateTime),
          new SqlParameter("dtBeginTime",dtBeginTime),
          new SqlParameter("dtEndTime",dtEndTime),
          new SqlParameter("nExecLength",nExecLength)};
      Params = sqlParams;
    }
  }
  /// <summary>
  ///类名: DBPlan_Beginwork_Flow
  ///说明: 出勤计划GUID数据操作类
  /// </summary>
  public class DBPlan_Beginwork_Flow
  {
    #region 增删改
    /// <summary>
    /// 添加数据
    /// </summary>
    public int Add(Plan_Beginwork_Flow model)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("insert into TAB_Plan_Beginwork_Flow");
      strSql.Append("(strTrainPlanGUID,strUserName,strUserNumber,dtConfirmTime,nConfirmType,strConfirmBrief,nFlowState,dtCreateTime,dtBeginTime,dtEndTime,nExecLength)");
      strSql.Append("values(@strTrainPlanGUID,@strUserName,@strUserNumber,@dtConfirmTime,@nConfirmType,@strConfirmBrief,@nFlowState,@dtCreateTime,@dtBeginTime,@dtEndTime,@nExecLength)");
      strSql.Append(";select @@IDENTITY");
      SqlParameter[] parameters = {
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
          new SqlParameter("@strUserName", model.strUserName),
          new SqlParameter("@strUserNumber", model.strUserNumber),
          new SqlParameter("@dtConfirmTime", model.dtConfirmTime),
          new SqlParameter("@nConfirmType", model.nConfirmType),
          new SqlParameter("@strConfirmBrief", model.strConfirmBrief),
          new SqlParameter("@nFlowState", model.nFlowState),
          new SqlParameter("@dtCreateTime", model.dtCreateTime),
          new SqlParameter("@dtBeginTime", model.dtBeginTime),
          new SqlParameter("@dtEndTime", model.dtEndTime),
          new SqlParameter("@nExecLength", model.nExecLength)};

      return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters));
    }
    /// <summary>
    /// 更新数据
    /// </summary>
    public bool Update(Plan_Beginwork_Flow model)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("Update TAB_Plan_Beginwork_Flow set ");
      strSql.Append(" strTrainPlanGUID = @strTrainPlanGUID, ");
      strSql.Append(" strUserName = @strUserName, ");
      strSql.Append(" strUserNumber = @strUserNumber, ");
      strSql.Append(" dtConfirmTime = @dtConfirmTime, ");
      strSql.Append(" nConfirmType = @nConfirmType, ");
      strSql.Append(" strConfirmBrief = @strConfirmBrief, ");
      strSql.Append(" nFlowState = @nFlowState, ");
      strSql.Append(" dtCreateTime = @dtCreateTime, ");
      strSql.Append(" dtBeginTime = @dtBeginTime, ");
      strSql.Append(" dtEndTime = @dtEndTime, ");
      strSql.Append(" nExecLength = @nExecLength ");
      strSql.Append(" where nID = @nID ");

      SqlParameter[] parameters = {
          new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
          new SqlParameter("@strUserName", model.strUserName),
          new SqlParameter("@strUserNumber", model.strUserNumber),
          new SqlParameter("@dtConfirmTime", model.dtConfirmTime),
          new SqlParameter("@nConfirmType", model.nConfirmType),
          new SqlParameter("@strConfirmBrief", model.strConfirmBrief),
          new SqlParameter("@nFlowState", model.nFlowState),
          new SqlParameter("@dtCreateTime", model.dtCreateTime),
          new SqlParameter("@dtBeginTime", model.dtBeginTime),
          new SqlParameter("@dtEndTime", model.dtEndTime),
          new SqlParameter("@nExecLength", model.nExecLength)};

      return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }
    /// <summary>
    /// 删除数据
    /// </summary>
    public bool Delete(string nID)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("delete from TAB_Plan_Beginwork_Flow ");
      strSql.Append(" where nID = @nID ");
      SqlParameter[] parameters = {
          new SqlParameter("nID",nID)};

      return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
    }
    #endregion
    /// <summary>
    /// 检查数据是否存在
    /// </summary>
    public bool Exists(Plan_Beginwork_Flow _Plan_Beginwork_Flow)
    {
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) from TAB_Plan_Beginwork_Flow where nID=@nID");
       SqlParameter[] parameters = {
           new SqlParameter("nID",_Plan_Beginwork_Flow.nID)};

      return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters)) > 0;
    }
    /// <summary>
    /// 获得数据DataTable
    /// </summary>
    public DataTable GetDataTable(Plan_Beginwork_FlowQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      if (QueryCondition.page == 0)
      {
        strSql.Append("select * ");
        strSql.Append(" FROM TAB_Plan_Beginwork_Flow where 1=1 " + strSqlOption.ToString());
      }else
      {
        strSql.Append(@"select top "+QueryCondition.rows.ToString() + " * from TAB_Plan_Beginwork_Flow where 1 = 1 "+
        strSqlOption.ToString() + " and nID not in ( select top " + (QueryCondition.page - 1) * QueryCondition.rows +
        " nID from TAB_Plan_Beginwork_Flow where  1=1 " + strSqlOption.ToString() + " order by nID desc) order by nID desc");
      }
      return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
    }
    /// <summary>
    /// 获得数据List
    /// </summary>
    public List<TF.RunSafty.Model.VIEW_Plan_Trainman> GetData(string clientGUID, string strTrainmanid)
    {
        string strWhere = string.Format(@" strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite
            where strSiteGUID = '{0}') and  
     (nPlanState in (4,7)) and (strTrainmanGUID1 ='{1}' or strTrainmanGUID2 = '{1}'  or strTrainmanGUID3 = '{1}'  or strTrainmanGUID4 = '{1}'     
) order by nPlanState asc ,dtStartTime desc ", clientGUID, strTrainmanid);
        DataSet set = GetList(strWhere);
        List<TF.RunSafty.Model.VIEW_Plan_Trainman> vPlans = DTToList(set.Tables[0]);
        return vPlans;
    }

    public List<TF.RunSafty.Model.VIEW_Plan_Trainman> DTToList(DataTable dt)
    {
        List<TF.RunSafty.Model.VIEW_Plan_Trainman> modelList = new List<TF.RunSafty.Model.VIEW_Plan_Trainman>();
        int rowsCount = dt.Rows.Count;
        if (rowsCount > 0)
        {
            TF.RunSafty.Model.VIEW_Plan_Trainman model;
            for (int n = 0; n < rowsCount; n++)
            {
                model = DataRowToModel(dt.Rows[n]);
                if (model != null)
                {
                    modelList.Add(model);
                }
            }
        }
        return modelList;
    }


    /// <summary>
    /// 获得数据列表
    /// </summary>
    public DataSet GetList(string strWhere)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * ");
        strSql.Append(" FROM VIEW_Plan_Trainman ");
        if (strWhere.Trim() != "")
        {
            strSql.Append(" where " + strWhere);
        }
        return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
    }

    public TF.RunSafty.Model.VIEW_Plan_Trainman DataRowToModel(DataRow row)
    {
        TF.RunSafty.Model.VIEW_Plan_Trainman model = new TF.RunSafty.Model.VIEW_Plan_Trainman();
        if (row != null)
        {
            if (row["strTrainJiaoluName"] != null)
            {
                model.strTrainJiaoluName = row["strTrainJiaoluName"].ToString();
            }
            if (row["strTrainmanTypeName"] != null)
            {
                model.strTrainmanTypeName = row["strTrainmanTypeName"].ToString();
            }
            if (row["strTrainPlanGUID"] != null)
            {
                model.strTrainPlanGUID = row["strTrainPlanGUID"].ToString();
            }
            if (row["strTrainJiaoluGUID"] != null)
            {
                model.strTrainJiaoluGUID = row["strTrainJiaoluGUID"].ToString();
            }
            if (row["strTrainNo"] != null)
            {
                model.strTrainNo = row["strTrainNo"].ToString();
            }
            if (row["strTrainNumber"] != null)
            {
                model.strTrainNumber = row["strTrainNumber"].ToString();
            }
            if (row["dtStartTime"] != null && row["dtStartTime"].ToString() != "")
            {
                DateTime dtStartTime;
                if (DateTime.TryParse(row["dtStartTime"].ToString(), out dtStartTime))
                {
                    model.dtStartTime = dtStartTime;
                }
            }
            if (row["dtChuQinTime"] != null && row["dtChuQinTime"].ToString() != "")
            {
                DateTime dtChuQinTime;
                if (DateTime.TryParse(row["dtChuQinTime"].ToString(), out dtChuQinTime))
                {
                    model.dtChuQinTime = dtChuQinTime;
                }
            }
            if (row["strStartStation"] != null)
            {
                model.strStartStation = row["strStartStation"].ToString();
            }
            if (row["strEndStation"] != null)
            {
                model.strEndStation = row["strEndStation"].ToString();
            }
            if (row["dtCreateTime"] != null && row["dtCreateTime"].ToString() != "")
            {
                DateTime dtCreateTime;
                if (DateTime.TryParse(row["dtCreateTime"].ToString(), out dtCreateTime))
                {
                    model.dtCreateTime = dtCreateTime;
                }
            }
            if (row["nPlanState"] != null && row["nPlanState"].ToString() != "")
            {
                model.nPlanState = int.Parse(row["nPlanState"].ToString());
            }
            if (row["strTrainTypeName"] != null)
            {
                model.strTrainTypeName = row["strTrainTypeName"].ToString();
            }
            if (row["strStartStationName"] != null)
            {
                model.strStartStationName = row["strStartStationName"].ToString();
            }
            if (row["strEndStationName"] != null)
            {
                model.strEndStationName = row["strEndStationName"].ToString();
            }
            if (row["nTrainmanTypeID"] != null && row["nTrainmanTypeID"].ToString() != "")
            {
                model.nTrainmanTypeID = int.Parse(row["nTrainmanTypeID"].ToString());
            }
            if (row["SendPlan"] != null && row["SendPlan"].ToString() != "")
            {
                model.SendPlan = int.Parse(row["SendPlan"].ToString());
            }
            if (row["nNeedRest"] != null && row["nNeedRest"].ToString() != "")
            {
                model.nNeedRest = int.Parse(row["nNeedRest"].ToString());
            }
            if (row["dtArriveTime"] != null && row["dtArriveTime"].ToString() != "")
            {
                model.dtArriveTime = DateTime.Parse(row["dtArriveTime"].ToString());
            }
            if (row["dtCallTime"] != null && row["dtCallTime"].ToString() != "")
            {
                //model.dtCallTime = DateTime.Parse(row["dtCallTime"].ToString());
                DateTime dtCallTime;
                if (DateTime.TryParse(row["dtCallTime"].ToString(), out dtCallTime))
                {
                    model.dtCallTime = dtCallTime;
                }
            }
            if (row["nKehuoID"] != null && row["nKehuoID"].ToString() != "")
            {
                model.nKehuoID = int.Parse(row["nKehuoID"].ToString());
            }
            if (row["dtRealStartTime"] != null && row["dtRealStartTime"].ToString() != "")
            {
                //model.dtRealStartTime = DateTime.Parse(row["dtRealStartTime"].ToString());
                DateTime dtRealStartTime;
                if (DateTime.TryParse(row["dtRealStartTime"].ToString(), out dtRealStartTime))
                {
                    model.dtRealStartTime = dtRealStartTime;
                }
            }
            if (row["nPlanType"] != null && row["nPlanType"].ToString() != "")
            {
                model.nPlanType = int.Parse(row["nPlanType"].ToString());
            }
            if (row["nDragType"] != null && row["nDragType"].ToString() != "")
            {
                model.nDragType = int.Parse(row["nDragType"].ToString());
            }
            if (row["nRemarkType"] != null && row["nRemarkType"].ToString() != "")
            {
                model.nRemarkType = int.Parse(row["nRemarkType"].ToString());
            }
            if (row["strRemark"] != null)
            {
                model.strRemark = row["strRemark"].ToString();
            }
            if (row["strWaiQinClientGUID"] != null)
            {
                model.strWaiQinClientGUID = row["strWaiQinClientGUID"].ToString();
            }
            if (row["strWaiQinClientNumber"] != null)
            {
                model.strWaiQinClientNumber = row["strWaiQinClientNumber"].ToString();
            }
            if (row["strWaiQinClientName"] != null)
            {
                model.strWaiQinClientName = row["strWaiQinClientName"].ToString();
            }
            if (row["dtSendTime"] != null && row["dtSendTime"].ToString() != "")
            {
                DateTime dtSendTime;
                if (DateTime.TryParse(row["dtSendTime"].ToString(), out dtSendTime))
                {
                    model.dtSendTime = dtSendTime;
                }
            }
            if (row["dtRecvTime"] != null && row["dtRecvTime"].ToString() != "")
            {
                DateTime dtRecvTime;
                if (DateTime.TryParse(row["dtRecvTime"].ToString(), out dtRecvTime))
                {
                    model.dtRecvTime = dtRecvTime;
                }
            }
            if (row["strCreateSiteGUID"] != null)
            {
                model.strCreateSiteGUID = row["strCreateSiteGUID"].ToString();
            }
            if (row["strCreateUserGUID"] != null)
            {
                model.strCreateUserGUID = row["strCreateUserGUID"].ToString();
            }
            if (row["strCreateUserID"] != null)
            {
                model.strCreateUserID = row["strCreateUserID"].ToString();
            }
            if (row["strCreateUserName"] != null)
            {
                model.strCreateUserName = row["strCreateUserName"].ToString();
            }
            if (row["strCreateSiteName"] != null)
            {
                model.strCreateSiteName = row["strCreateSiteName"].ToString();
            }
            if (row["strPlaceName"] != null)
            {
                model.strPlaceName = row["strPlaceName"].ToString();
            }
            if (row["strTrainmanNumber1"] != null)
            {
                model.strTrainmanNumber1 = row["strTrainmanNumber1"].ToString();
            }
            if (row["strTrainmanName1"] != null)
            {
                model.strTrainmanName1 = row["strTrainmanName1"].ToString();
            }
            if (row["nPostID1"] != null && row["nPostID1"].ToString() != "")
            {
                model.nPostID1 = int.Parse(row["nPostID1"].ToString());
            }
            if (row["strWorkShopGUID1"] != null)
            {
                model.strWorkShopGUID1 = row["strWorkShopGUID1"].ToString();
            }
            if (row["strTelNumber1"] != null)
            {
                model.strTelNumber1 = row["strTelNumber1"].ToString();
            }
            if (row["dtLastEndWorkTime1"] != null && row["dtLastEndWorkTime1"].ToString() != "")
            {
                // model.dtLastEndWorkTime1 = DateTime.Parse(row["dtLastEndWorkTime1"].ToString());
                DateTime dtLastEndWorkTime1;
                if (DateTime.TryParse(row["dtLastEndWorkTime1"].ToString(), out dtLastEndWorkTime1))
                {
                    model.dtLastEndWorkTime1 = dtLastEndWorkTime1;
                }
            }
            if (row["strTrainmanGUID1"] != null)
            {
                model.strTrainmanGUID1 = row["strTrainmanGUID1"].ToString();
            }
            if (row["strTrainmanGUID2"] != null)
            {
                model.strTrainmanGUID2 = row["strTrainmanGUID2"].ToString();
            }
            if (row["strTrainmanNumber2"] != null)
            {
                model.strTrainmanNumber2 = row["strTrainmanNumber2"].ToString();
            }
            if (row["strTrainmanName2"] != null)
            {
                model.strTrainmanName2 = row["strTrainmanName2"].ToString();
            }
            if (row["strWorkShopGUID2"] != null)
            {
                model.strWorkShopGUID2 = row["strWorkShopGUID2"].ToString();
            }

            if (row["strTelNumber2"] != null)
            {
                model.strTelNumber2 = row["strTelNumber2"].ToString();
            }
            if (row["strTrainmanNumber3"] != null)
            {
                model.strTrainmanNumber3 = row["strTrainmanNumber3"].ToString();
            }
            if (row["strTrainmanGUID3"] != null)
            {
                model.strTrainmanGUID3 = row["strTrainmanGUID3"].ToString();
            }
            if (row["dtLastEndWorkTime2"] != null && row["dtLastEndWorkTime2"].ToString() != "")
            {
                // model.dtLastEndWorkTime2 = DateTime.Parse(row["dtLastEndWorkTime2"].ToString());
                DateTime dtLastEndWorkTime2;
                if (DateTime.TryParse(row["dtLastEndWorkTime2"].ToString(), out dtLastEndWorkTime2))
                {
                    model.dtLastEndWorkTime2 = dtLastEndWorkTime2;
                }
            }
            if (row["nPostID2"] != null && row["nPostID2"].ToString() != "")
            {
                model.nPostID2 = int.Parse(row["nPostID2"].ToString());
            }
            if (row["strTrainmanName3"] != null)
            {
                model.strTrainmanName3 = row["strTrainmanName3"].ToString();
            }
            if (row["nPostID3"] != null && row["nPostID3"].ToString() != "")
            {
                model.nPostID3 = int.Parse(row["nPostID3"].ToString());
            }
            if (row["strWorkShopGUID3"] != null)
            {
                model.strWorkShopGUID3 = row["strWorkShopGUID3"].ToString();
            }

            if (row["strTelNumber3"] != null)
            {
                model.strTelNumber3 = row["strTelNumber3"].ToString();
            }
            if (row["dtLastEndWorkTime3"] != null && row["dtLastEndWorkTime3"].ToString() != "")
            {
                //model.dtLastEndWorkTime3 = DateTime.Parse(row["dtLastEndWorkTime3"].ToString());
                DateTime dtLastEndWorkTime3;
                if (DateTime.TryParse(row["dtLastEndWorkTime3"].ToString(), out dtLastEndWorkTime3))
                {
                    model.dtLastEndWorkTime3 = dtLastEndWorkTime3;
                }
            }
            if (row["strDutyGUID"] != null)
            {
                model.strDutyGUID = row["strDutyGUID"].ToString();
            }
            if (row["strGroupGUID"] != null)
            {
                model.strGroupGUID = row["strGroupGUID"].ToString();
            }
            if (row["strDutySiteGUID"] != null)
            {
                model.strDutySiteGUID = row["strDutySiteGUID"].ToString();
            }
            if (row["dtTrainmanCreateTime"] != null && row["dtTrainmanCreateTime"].ToString() != "")
            {
                DateTime dtTrainmanCreateTime;
                if (DateTime.TryParse(row["dtTrainmanCreateTime"].ToString(), out dtTrainmanCreateTime))
                {
                    model.dtTrainmanCreateTime = dtTrainmanCreateTime;
                }
            }
            if (row["dtFirstStartTime"] != null && row["dtFirstStartTime"].ToString() != "")
            {
                //model.dtFirstStartTime = DateTime.Parse(row["dtFirstStartTime"].ToString());
                DateTime dtFirstStartTime;
                if (DateTime.TryParse(row["dtFirstStartTime"].ToString(), out dtFirstStartTime))
                {
                    model.dtFirstStartTime = dtFirstStartTime;
                }
            }
            if (row["nid"] != null && row["nid"].ToString() != "")
            {
                model.nid = int.Parse(row["nid"].ToString());
            }
            if (row["nDragTypeName"] != null)
            {
                model.nDragTypeName = row["nDragTypeName"].ToString();
            }
            if (row["strKehuoName"] != null)
            {
                model.strKehuoName = row["strKehuoName"].ToString();
            }
            if (row["nTrainmanState1"] != null && row["nTrainmanState1"].ToString() != "")
            {
                model.nTrainmanState1 = int.Parse(row["nTrainmanState1"].ToString());
            }
            if (row["nTrainmanState2"] != null && row["nTrainmanState2"].ToString() != "")
            {
                model.nTrainmanState2 = int.Parse(row["nTrainmanState2"].ToString());
            }
            if (row["nTrainmanState3"] != null && row["nTrainmanState3"].ToString() != "")
            {
                model.nTrainmanState3 = int.Parse(row["nTrainmanState3"].ToString());
            }
            if (row["strBak1"] != null)
            {
                model.strBak1 = row["strBak1"].ToString();
            }
            if (row["dtLastArriveTime"] != null && row["dtLastArriveTime"].ToString() != "")
            {
                DateTime dtLastArriveTime;
                if (DateTime.TryParse(row["dtLastArriveTime"].ToString(), out dtLastArriveTime))
                {
                    model.dtLastArriveTime = dtLastArriveTime;
                }
            }
            if (row["strMainPlanGUID"] != null)
            {
                model.strMainPlanGUID = row["strMainPlanGUID"].ToString();
            }
            if (row["strTrainmanName4"] != null)
            {
                model.strTrainmanName4 = row["strTrainmanName4"].ToString();
            }
            if (row["strTrainmanNumber4"] != null)
            {
                model.strTrainmanNumber4 = row["strTrainmanNumber4"].ToString();
            }
            if (row["nPostID4"] != null && row["nPostID4"].ToString() != "")
            {
                model.nPostID4 = int.Parse(row["nPostID4"].ToString());
            }
            if (row["strTelNumber4"] != null)
            {
                model.strTelNumber4 = row["strTelNumber4"].ToString();
            }

            if (row["dtLastEndWorkTime4"] != null && row["dtLastEndWorkTime4"].ToString() != "")
            {
                DateTime dtLastEndWorkTime4;
                if (DateTime.TryParse(row["dtLastEndWorkTime4"].ToString(), out dtLastEndWorkTime4))
                {
                    model.dtLastEndWorkTime4 = dtLastEndWorkTime4;
                }
            }
            if (row["nTrainmanState"] != null && row["nTrainmanState"].ToString() != "")
            {
                model.nTrainmanState = int.Parse(row["nTrainmanState"].ToString());
            }
            if (row["strTrainmanGUID4"] != null)
            {
                model.strTrainmanGUID4 = row["strTrainmanGUID4"].ToString();
            }
            if (row["strRemarkTypeName"] != null)
            {
                model.strRemarkTypeName = row["strRemarkTypeName"].ToString();
            }
            if (row["strPlanTypeName"] != null)
            {
                model.strPlanTypeName = row["strPlanTypeName"].ToString();
            }
            if (row["strPlaceID"] != null)
            {
                model.strPlaceID = row["strPlaceID"].ToString();
            }
            if (row["strStateName"] != null)
            {
                model.strStateName = row["strStateName"].ToString();
            }
            if (row["nDriverType1"] != null && row["nDriverType1"].ToString() != "")
            {
                model.nDriverType1 = int.Parse(row["nDriverType1"].ToString());
            }
            if (row["nDriverType2"] != null && row["nDriverType2"].ToString() != "")
            {
                model.nDriverType2 = int.Parse(row["nDriverType2"].ToString());
            }
            if (row["nDriverType3"] != null && row["nDriverType3"].ToString() != "")
            {
                model.nDriverType3 = int.Parse(row["nDriverType3"].ToString());
            }
            if (row["nDriverType4"] != null && row["nDriverType4"].ToString() != "")
            {
                model.nDriverType4 = int.Parse(row["nDriverType4"].ToString());
            }
            if (row["strABCD1"] != null)
            {
                model.strABCD1 = row["strABCD1"].ToString();
            }
            if (row["strABCD2"] != null)
            {
                model.strABCD2 = row["strABCD2"].ToString();
            }
            if (row["strABCD3"] != null)
            {
                model.strABCD3 = row["strABCD3"].ToString();
            }
            if (row["strABCD4"] != null)
            {
                model.strABCD4 = row["strABCD4"].ToString();
            }
            if (row["isKey1"] != null && row["isKey1"].ToString() != "")
            {
                model.isKey1 = int.Parse(row["isKey1"].ToString());
            }
            if (row["isKey2"] != null && row["isKey2"].ToString() != "")
            {
                model.isKey2 = int.Parse(row["isKey2"].ToString());
            }
            if (row["isKey3"] != null && row["isKey3"].ToString() != "")
            {
                model.isKey3 = int.Parse(row["isKey3"].ToString());
            }
            if (row["isKey4"] != null && row["isKey4"].ToString() != "")
            {
                model.isKey4 = int.Parse(row["isKey4"].ToString());
            }
            if (row["strPlanStateName"] != null)
            {
                model.strPlanStateName = row["strPlanStateName"].ToString();
            }
            model.nMsgState1 = 0;
            if (row["nMsgState1"] != null && row["nMsgState1"].ToString() != "")
            {
                model.nMsgState1 = int.Parse(row["nMsgState1"].ToString());
            }
            if (row["strMsgID1"] != null)
            {
                model.strMsgID1 = row["strMsgID1"].ToString();
            }
            model.nMsgState2 = 0;
            if (row["nMsgState2"] != null && row["nMsgState2"].ToString() != "")
            {
                model.nMsgState2 = int.Parse(row["nMsgState2"].ToString());
            }
            if (row["strMsgID2"] != null)
            {
                model.strPlanStateName = row["strMsgID2"].ToString();
            }
            model.nMsgState3 = 0;
            if (row["nMsgState3"] != null && row["nMsgState3"].ToString() != "")
            {
                model.nMsgState3 = int.Parse(row["nMsgState3"].ToString());
            }

            if (row["strMsgID3"] != null)
            {
                model.strMsgID3 = row["strMsgID3"].ToString();
            }
            model.nMsgState4 = 0;
            if (row["nMsgState4"] != null && row["nMsgState4"].ToString() != "")
            {
                model.nMsgState4 = int.Parse(row["nMsgState4"].ToString());
            }
            if (row["strMsgID4"] != null)
            {
                model.strMsgID4 = row["strMsgID4"].ToString();
            }

            if (row["strMobileNumber1"] != null)
            {
                model.strMobileNumber1 = row["strMobileNumber1"].ToString();
            }
            if (row["strMobileNumber2"] != null)
            {
                model.strMobileNumber2 = row["strMobileNumber2"].ToString();
            }
            if (row["strMobileNumber3"] != null)
            {
                model.strMobileNumber3 = row["strMobileNumber3"].ToString();
            }
            if (row["strMobileNumber4"] != null)
            {
                model.strMobileNumber4 = row["strMobileNumber4"].ToString();
            }
            DateTime _dtBeginWorkTime;
            if (row["dtBeginWorkTime"] != DBNull.Value)
            {
                if (DateTime.TryParse(row["dtBeginWorkTime"].ToString(), out _dtBeginWorkTime))
                {
                    model.dtBeginWorkTime = _dtBeginWorkTime;
                }
            }
        }
        return model;
    }




    /// <summary>
    /// 获得记录总数
    /// </summary>
    public int GetDataCount(Plan_Beginwork_FlowQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select count(*) ");
      strSql.Append(" FROM TAB_Plan_Beginwork_Flow where 1=1" + strSqlOption.ToString());
      return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams));
    }
    /// <summary>
    /// 获得一个实体对象
    /// </summary>
    public Plan_Beginwork_Flow GetModel(Plan_Beginwork_FlowQueryCondition QueryCondition)
    {
      SqlParameter[] sqlParams;
      StringBuilder strSqlOption = new StringBuilder();
      QueryCondition.OutPut(out strSqlOption, out sqlParams);
      StringBuilder strSql = new StringBuilder();
      strSql.Append("select top 1 * ");
      strSql.Append(" FROM TAB_Plan_Beginwork_Flow where 1=1 " + strSqlOption.ToString());
      DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
      Plan_Beginwork_Flow _Plan_Beginwork_Flow = null;
      if (dt.Rows.Count > 0)
      {
        _Plan_Beginwork_Flow = new Plan_Beginwork_Flow();
        DataRowToModel(_Plan_Beginwork_Flow,dt.Rows[0]);
      }
      return _Plan_Beginwork_Flow;
    }
    /// <summary>
    /// 读取DataRow数据到Model中
    /// </summary>
    private void DataRowToModel(Plan_Beginwork_Flow model,DataRow dr)
    {
      model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
      model.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
      model.strUserName = ObjectConvertClass.static_ext_string(dr["strUserName"]);
      model.strUserNumber = ObjectConvertClass.static_ext_string(dr["strUserNumber"]);
      model.dtConfirmTime = ObjectConvertClass.static_ext_date(dr["dtConfirmTime"]);
      model.nConfirmType = ObjectConvertClass.static_ext_int(dr["nConfirmType"]);
      model.strConfirmBrief = ObjectConvertClass.static_ext_string(dr["strConfirmBrief"]);
      model.nFlowState = ObjectConvertClass.static_ext_int(dr["nFlowState"]);
      model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);
      model.dtBeginTime = ObjectConvertClass.static_ext_date(dr["dtBeginTime"]);
      model.dtEndTime = ObjectConvertClass.static_ext_date(dr["dtEndTime"]);
      model.nExecLength = ObjectConvertClass.static_ext_int(dr["nExecLength"]);
    }
  }
}
