using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.RunSafty.Leave.MD;
using TF.CommonUtility;
using TFApiCommon.DBParam;
using ThinkFreely.RunSafty;

namespace TF.RunSafty.Leave.DB
{
    #region 请假记录查询条件类
    /// <summary>
    ///类名: LeaveMgr_AskLeaveQueryCondition
    ///说明: 请假记录查询条件类
    /// </summary>
    public class LeaveMgr_AskLeaveQueryCondition
    {
        //开始时间
        public string strBeginDateTime = "";
        //结束时间
        public string strEndDateTime = "";
        //请假类型ID
        public string strLeaveTypeGUID = "";
        //状态
        public string strStatus = "";
        //车间ID
        public string strWorkShopGUID = "";
        //职位
        public string strPost = "";
        //机组ID
        public string strGroupGUID = "";
        //工号
        public string strNumber = "";

        public Boolean ShowAllUnEnd = false;
        public void OutPut(out StringBuilder SqlCondition, out SqlParameter[] Params)
        {            
            SqlCondition = new StringBuilder();
            if (ShowAllUnEnd && strEndDateTime != "")
            {
                SqlCondition.Append(" and ((1=1 ");
                SqlCondition.Append(strBeginDateTime != "" ? " and A.dBeginTime >= @dBeginTime" : "");
                SqlCondition.Append(strEndDateTime != "" ? " and A.dBeginTime <= @dEndTime" : "");
                SqlCondition.Append(strLeaveTypeGUID != "" ? " and A.strLeaveTypeGUID = @strLeaveTypeGUID" : "");
                SqlCondition.Append(strStatus != "" ? " and A.nStatus = @nStatus" : "");
                SqlCondition.Append(strWorkShopGUID != "" ? " and B.strWorkShopGUID = @strWorkShopGUID" : "");
                SqlCondition.Append(strPost != "" ? " and B.nPostID = @nPostID" : "");
                SqlCondition.Append(strGroupGUID != "" ? " and B.strGuideGroupGUID = @strGuideGroupGUID" : "");
                SqlCondition.Append(strNumber != "" ? " and A.strTrainmanID like @strTrainmanID" : "");
                SqlCondition.Append(")");
                SqlCondition.Append(" or ");
                SqlCondition.Append(" ( 1=1 ");
                SqlCondition.Append(strLeaveTypeGUID != "" ? " and A.strLeaveTypeGUID = @strLeaveTypeGUID" : "");
                SqlCondition.Append(" and (A.nStatus = 1 or A.nStatus = 2)");
                SqlCondition.Append(strWorkShopGUID != "" ? " and B.strWorkShopGUID = @strWorkShopGUID" : "");
                SqlCondition.Append(strPost != "" ? " and B.nPostID = @nPostID" : "");
                SqlCondition.Append(strGroupGUID != "" ? " and B.strGuideGroupGUID = @strGuideGroupGUID" : "");
                SqlCondition.Append(strNumber != "" ? " and A.strTrainmanID like @strTrainmanID" : "");
                SqlCondition.Append("))");
            }
            else
            {
                SqlCondition.Append(strBeginDateTime != "" ? " and A.dBeginTime >= @dBeginTime" : "");
                SqlCondition.Append(strEndDateTime != "" ? " and A.dBeginTime <= @dEndTime" : "");
                SqlCondition.Append(strLeaveTypeGUID != "" ? " and A.strLeaveTypeGUID = @strLeaveTypeGUID" : "");
                SqlCondition.Append(strStatus != "" ? " and A.nStatus = @nStatus" : "");
                SqlCondition.Append(strWorkShopGUID != "" ? " and B.strWorkShopGUID = @strWorkShopGUID" : "");
                SqlCondition.Append(strPost != "" ? " and B.nPostID = @nPostID" : "");
                SqlCondition.Append(strGroupGUID != "" ? " and B.strGuideGroupGUID = @strGuideGroupGUID" : "");
                SqlCondition.Append(strNumber != "" ? " and A.strTrainmanID like @strTrainmanID" : "");
            }
            
            SqlParameter[] sqlParams ={
                  new SqlParameter("dBeginTime",strBeginDateTime),
                  new SqlParameter("dEndTime",strEndDateTime),
                  new SqlParameter("strLeaveTypeGUID",strLeaveTypeGUID),
                  new SqlParameter("nStatus",strStatus),
                  new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                  new SqlParameter("nPostID",strPost),
                  new SqlParameter("strGuideGroupGUID",strGroupGUID),
                  new SqlParameter("strTrainmanID","%"+strNumber+"%")
                                      };
            Params = sqlParams;
        }
    }
    #endregion

    #region DBLCLeaveType
    /// <summary>                             
    ///类名: DBLCLeaveType
    ///说明: 请销假数据操作类
    /// </summary>   
    public class DBLCLeaveType
    {
        #region 获取请假类别
        /// <summary>
        /// 获得数据List
        /// </summary>
        public LeaveClassArray GetLeaveMgr_LeaveClassDataList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM TAB_LeaveMgr_LeaveClass");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            LeaveClassArray list = new LeaveClassArray();
            foreach (DataRow dr in dt.Rows)
            {
                LeaveClass _LeaveMgr_LeaveClass = new LeaveClass();
                LeaveMgr_LeaveClassDataRowToModel(_LeaveMgr_LeaveClass, dr);
                list.Add(_LeaveMgr_LeaveClass);
            }
            return list;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void LeaveMgr_LeaveClassDataRowToModel(LeaveClass model, DataRow dr)
        {
            model.nClassID = ObjectConvertClass.static_ext_string(dr["nClassID"]);
            model.strClassName = ObjectConvertClass.static_ext_string(dr["strClassName"]);
        }
        #endregion

        #region 添加请假类型
        /// <summary>
        /// 添加请假类型
        /// </summary>
        public bool AddLeaveType(LeaveType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_LeaveMgr_LeaveType");
            strSql.Append("(strTypeGUID,strTypeName,nClassID)");
            strSql.Append("values(@strTypeGUID,@strTypeName,@nClassID)");
            SqlParameter[] parameters = {
                  new SqlParameter("@strTypeGUID", model.strTypeGUID),
                  new SqlParameter("@strTypeName", model.strTypeName),
                  new SqlParameter("@nClassID", model.nClassID)
                                        };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region 删除请假类型
        /// <summary>
        /// 删除请假类型
        /// </summary>
        public bool DeleteLeaveType(string strID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_LeaveMgr_LeaveType ");
            strSql.Append(" where strTypeGUID = @strID ");
            SqlParameter[] parameters = {
          new SqlParameter("strID",strID)};

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region 更新请假类型
        /// <summary>
        /// 更新数据
        /// </summary>
        public bool UpdateLeaveType(LeaveType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update TAB_LeaveMgr_LeaveType set ");
            strSql.Append(" strTypeName = @strTypeName, ");
            strSql.Append(" nClassID = @nClassID ");
            strSql.Append(" where strTypeGUID = @strTypeGUID ");

            SqlParameter[] parameters = {
          new SqlParameter("@strTypeGUID", model.strTypeGUID),
          new SqlParameter("@strTypeName", model.strTypeName),
          new SqlParameter("@nClassID", model.nClassID)};

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region 判断是否存在请假类型
        /// <summary>
        /// 检查数据是否存在
        /// </summary>
        public bool ExistsLeaveType(LeaveType _LeaveMgr_LeaveType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from TAB_LeaveMgr_LeaveType where strTypeName=@strTypeName");
            SqlParameter[] parameters = {
                   new SqlParameter("strTypeName",_LeaveMgr_LeaveType.strTypeName)
                                        };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters)) > 0;
        }
        #endregion

        #region 判断是否存在请假类型(增加ClassID判断)
        /// <summary>
        /// 检查数据是否存在
        /// </summary>
        public bool ExistsLeaveTypeWhenEdit(LeaveType _LeaveMgr_LeaveType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from TAB_LeaveMgr_LeaveType where strTypeName=@strTypeName and nClassID=@nClassID");
            SqlParameter[] parameters = {
                        new SqlParameter("strTypeName",_LeaveMgr_LeaveType.strTypeName),
                        new SqlParameter("nClassID",_LeaveMgr_LeaveType.nClassID),
                                        };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters)) > 0;
        }
        #endregion

        #region 获取请假类型列表
        /// <summary>
        /// 获得数据List
        /// </summary>
        public LeaveTypeArray GetLeaveTypesDataList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM VIEW_LeaveMgr_AllLeaveTypes");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            LeaveTypeArray list = new LeaveTypeArray();
            foreach (DataRow dr in dt.Rows)
            {
                LeaveType _LeaveMgr_LeaveType = new LeaveType();
                LeaveTypeDataRowToModel(_LeaveMgr_LeaveType, dr);
                list.Add(_LeaveMgr_LeaveType);
            }
            return list;
        }

        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void LeaveTypeDataRowToModel(LeaveType model, DataRow dr)
        {
            model.strTypeGUID = ObjectConvertClass.static_ext_string(dr["strTypeGUID"]);
            model.strTypeName = ObjectConvertClass.static_ext_string(dr["strTypeName"]);
            model.nClassID = ObjectConvertClass.static_ext_int(dr["nClassID"]);
            model.strClassName = ObjectConvertClass.static_ext_string(dr["strClassName"]);
        }
        #endregion

        #region 获取指定名称的请假类型
        /// <summary>
        /// 获得数据List
        /// </summary>
        public LeaveTypeArray GetLeaveTypeDataList(string strTypeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM VIEW_LeaveMgr_AllLeaveTypes where strTypeName like @strTypeName");
            SqlParameter[] sqlParams ={
                  new SqlParameter("strTypeName","%"+strTypeName+"%")
                                      };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            LeaveTypeArray list = new LeaveTypeArray();
            if (dt.Rows.Count > 0)
            {
                LeaveType _LeaveMgr_LeaveType = new LeaveType();
                LeaveTypeDataRowToModel(_LeaveMgr_LeaveType, dt.Rows[0]);
                list.Add(_LeaveMgr_LeaveType);
            }
            return list;
        }
        #endregion
    }
    #endregion
     
    #region DBLCAskLeave
    /// <summary>                             
    ///类名: DBLCAskLeave
    ///说明: 请销假数据操作类
    /// </summary>
    public class DBLCAskLeave
    {
        #region 根据工号判断该职工是否有未销假的记录
        /// <summary>
        /// 判断是否有未销假的记录
        /// </summary>
        public bool CheckWhetherAskLeaveByID(string strTrainManID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from TAB_LeaveMgr_AskLeave where strTrainManID = @strTrainManID and nStatus < 3");
            SqlParameter[] parameters = {
                   new SqlParameter("strTrainManID",strTrainManID)
                                        };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters)) > 0;
        }
        #endregion

        #region 请假
        /// <summary>
        /// 更新数据
        /// </summary>
        public int AskLeave(LeaveApplyEntity model)
        {
            //判断人员机组是否处于调休状态
            if (ISTXState(model.strTrainmanGUID))
            {
                return 2;
            }

            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    //第一步AddAskLeave
                    string strAskLeaveGUID = System.Guid.NewGuid().ToString();
                    model.strAskLeaveGUID = strAskLeaveGUID;
                    int obj = AddAskLeave(trans, model, strAskLeaveGUID);
                    if (obj > 0)
                    {
                        //第二步AddAskLeaveDetail
                        obj = AddAskLeaveDetail(trans, model, strAskLeaveGUID);
                        if (obj > 0)
                        {
                            //第三步DeleteTrainman
                            obj = DeleteTrainman(trans, model);

                            //第四步 插入请销假消息
                            model.msgType = MSGTYPE.MSG_LEAVE;
                            string strMsg = AttentionMsg.ReturnStrJson(model);
                            CreatMsg(trans, strMsg);
                            trans.Commit();
                            return 0;
                        }
                    }
                    trans.Rollback();
                    return 1;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="model"></param>
        /// <param name="strAskLeaveGUID"></param>
        private void CreatMsg(SqlTransaction trans, string strMsg)
        {
            AttentionMsg msg = new AttentionMsg();
            msg.msgType = MSGTYPE.MSG_LEAVE;//请销假消息类别
            msg.param = strMsg;
            msg.CreatMsg(trans);
        }
             



        private bool ISTXState(string strTrainmanGUID)
        {
            //获取目标人员所在机组信息
            string strSql = @"select nTXState from TAB_Nameplate_Group where strTrainmanGUID1= @strTrainmanGUID 
                    or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID  or strTrainmanGUID4= @strTrainmanGUID";
            SqlParameter[] sqlParamsGroup = new SqlParameter[] { 
                    new SqlParameter("strTrainmanGUID",strTrainmanGUID)
                };
            DataTable dtGroup = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsGroup).Tables[0];

            //判断目标人员所在机组是否处于调休状态
            if (dtGroup.Rows.Count > 0)
            {
                string nTXState = ObjectConvertClass.static_ext_string(dtGroup.Rows[0]["nTXState"]);
                if (nTXState == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }



        //第一步AddAskLeave
        private int AddAskLeave(SqlTransaction trans, LeaveApplyEntity model, string strAskLeaveGUID)
        {
            StringBuilder strSqlAddAskLeave = new StringBuilder();
            strSqlAddAskLeave.Append("insert into TAB_LeaveMgr_AskLeave");
            strSqlAddAskLeave.Append("(strAskLeaveGUID,strTrainManID,dBeginTime,dEndTime,strLeaveTypeGUID,nStatus)");
            strSqlAddAskLeave.Append("values(@strAskLeaveGUID,@strTrainManID,@dBeginTime,@dEndTime,@strLeaveTypeGUID,1);");
            SqlParameter[] paraAddAskLeave = {
                              new SqlParameter("@strAskLeaveGUID", strAskLeaveGUID),
                              new SqlParameter("@strTrainManID", model.strTrainmanNumber),
                              new SqlParameter("@dBeginTime", model.dtBeginTime),
                              new SqlParameter("@dEndTime", model.dtEndTime),
                              new SqlParameter("@strLeaveTypeGUID", model.strTypeGUID)
                                                         };
            strSqlAddAskLeave.Append("update TAB_Nameplate_Group set strTrainmanGUID1= ''  " +
" where strTrainmanGUID1 = (select top 1 strTrainmanGUID from TAB_Org_Trainman where strTrainmanNumber = '" + model.strTrainmanNumber + "');");
            strSqlAddAskLeave.Append("update TAB_Nameplate_Group set strTrainmanGUID2= ''  " +
" where strTrainmanGUID2 = (select top 1 strTrainmanGUID from TAB_Org_Trainman where strTrainmanNumber = '" + model.strTrainmanNumber + "');");
            strSqlAddAskLeave.Append("update TAB_Nameplate_Group set strTrainmanGUID3= ''  " +
" where strTrainmanGUID3 = (select top 1 strTrainmanGUID from TAB_Org_Trainman where strTrainmanNumber = '" + model.strTrainmanNumber + "');");
            //tsUnRuning {非运转}
            strSqlAddAskLeave.Append("update TAB_Org_Trainman set nTrainmanState = 0 where strTrainmanNumber = '" + model.strTrainmanGUID + "';");
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSqlAddAskLeave.ToString(), paraAddAskLeave);
        }

        //第二步AddAskLeaveDetail
        private int AddAskLeaveDetail(SqlTransaction trans, LeaveApplyEntity model, string strAskLeaveGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_LeaveMgr_AskLeaveDetail");
            strSql.Append("(strAskLeaveDetailGUID,strAskLeaveGUID,strMemo,dBeginTime,dEndTime,strProverID,strProverName,dCreateTime,strDutyUserID,strDutyUserName,strSiteID,strSiteName,nValidWay)");
            strSql.Append("values(newid(),@strAskLeaveGUID,@strMemo,@dBeginTime,@dEndTime,@strProverID,@strProverName,getdate(),@strDutyUserID,@strDutyUserName,@strSiteID,@strSiteName,@nValidWay)");
            SqlParameter[] parameters = {
                            //new SqlParameter("@strAskLeaveDetailGUID", model.strAskLeaveDetailGUID),
                            new SqlParameter("@strAskLeaveGUID", strAskLeaveGUID),
                            new SqlParameter("@strMemo", model.strRemark),
                            new SqlParameter("@dBeginTime", model.dtBeginTime), 
                            new SqlParameter("@dEndTime", model.dtEndTime), 
                            new SqlParameter("@strProverID", model.strProverID),
                            new SqlParameter("@strProverName", model.strProverName),
                            new SqlParameter("@strDutyUserID", model.strDutyUserID),
                            new SqlParameter("@strDutyUserName", model.strDutyUserName),
                            new SqlParameter("@strSiteID", model.strSiteID),
                            new SqlParameter("@strSiteName", model.strSiteName),
                            new SqlParameter("@nValidWay", model.Verify)
                                                };
            return ObjectConvertClass.static_ext_int(SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters));
        }

        //第三步DeleteTrainman
        private int DeleteTrainman(SqlTransaction trans, LeaveApplyEntity model)
        {
            StringBuilder strSqlDeleteTrainman = new StringBuilder();
            //tsReady {预备}
            strSqlDeleteTrainman.Append("update Tab_Org_Trainman set nTrainmanState = 0 where strTrainmanGUID = '" + model.strTrainmanGUID + "'");
            strSqlDeleteTrainman.Append("update TAB_Nameplate_Group set strTrainmanGUID1='' where strTrainmanGUID1 = '" + model.strTrainmanGUID + "'");
            strSqlDeleteTrainman.Append("update TAB_Nameplate_Group set strTrainmanGUID2='' where strTrainmanGUID2 = '" + model.strTrainmanGUID + "'");
            strSqlDeleteTrainman.Append("update TAB_Nameplate_Group set strTrainmanGUID3='' where strTrainmanGUID3 = '" + model.strTrainmanGUID + "'");
            strSqlDeleteTrainman.Append("update TAB_Nameplate_Group set strTrainmanGUID4='' where strTrainmanGUID4 = '" + model.strTrainmanGUID + "'");
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSqlDeleteTrainman.ToString());
        }
        #endregion

        #region 撤销请假记录
        /// <summary>
        /// 更新数据
        /// </summary>
        public void CancelLeave(string strAskLeaveGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update TAB_LeaveMgr_AskLeave set ");
            strSql.Append(" nStatus = 10000 ");
            strSql.Append(" where strAskLeaveGUID  = @strAskLeaveGUID  ");
            SqlParameter[] parameters = {
                          new SqlParameter("@strAskLeaveGUID", strAskLeaveGUID)
                                        };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);

            //tsReady {预备}
            string strSqlUpTrainman = "update tab_org_trainman set nTrainmanState = 1 where strTrainmanNumber = " +
                        " (select strTrainManID from TAB_LeaveMgr_AskLeave where strAskLeaveGUID = '" + strAskLeaveGUID + "')";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSqlUpTrainman);
        }
        #endregion

        #region 查询请假记录
        /// <summary>
        /// 获得数据List
        /// </summary>
        public AskLeaveWithTypeArray GetLeavesDataList(LeaveMgr_AskLeaveQueryCondition QueryCondition)
        {
            SqlParameter[] sqlParams;
            StringBuilder strSqlOption = new StringBuilder();
            QueryCondition.OutPut(out strSqlOption, out sqlParams);
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@"select A.*,B.strTrainmanName,B.nPostID,D.strGuideGroupName,C.strProverID strAskProverID,
             C.strProverName strAskProverName,C.strDutyUserName strAskDutyUserName,
             C.dCreateTime dtAskCreateTime,C.strMemo strMemo 
             from VIEW_LeaveMgr_AskLeaveWithTypeName A
             left join TAB_Org_Trainman B on A.strTrainmanID=B.strTrainmanNumber
             left join TAB_LeaveMgr_AskLeaveDetail C on A.strAskLeaveGUID=C.strAskLeaveGUID
             left join TAB_Org_GuideGroup D on B.strGuideGroupGUID=D.strGuideGroupGUID");
            strSql.Append("  where 1=1 " + strSqlOption.ToString() + " order by A.dBeginTime desc");

            
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            AskLeaveWithTypeArray list = new AskLeaveWithTypeArray();
            foreach (DataRow dr in dt.Rows)
            {
                AskLeaveWithType _LeaveMgr_AskLeave = new AskLeaveWithType();
                LeavesDataRowToModel(_LeaveMgr_AskLeave, dr);
                list.Add(_LeaveMgr_AskLeave);
            }
            return list;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void LeavesDataRowToModel(AskLeaveWithType model, DataRow dr)
        {
            model.strTypeName = ObjectConvertClass.static_ext_string(dr["strTypeName"]);
            AskLeaveEntity AskLeave = new AskLeaveEntity();
            AskLeave.strAskLeaveGUID = ObjectConvertClass.static_ext_string(dr["strAskLeaveGUID"]);
            AskLeave.strTrainManID = ObjectConvertClass.static_ext_string(dr["strTrainManID"]);
            AskLeave.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
            AskLeave.dtBeginTime = ObjectConvertClass.static_ext_date(dr["dBeginTime"]);
            AskLeave.dtEndTime = ObjectConvertClass.static_ext_date(dr["dEndTime"]);
            AskLeave.strLeaveTypeGUID = ObjectConvertClass.static_ext_string(dr["strLeaveTypeGUID"]);
            AskLeave.nStatus = ObjectConvertClass.static_ext_int(dr["nStatus"]);
            AskLeave.strAskProverID = ObjectConvertClass.static_ext_string(dr["strAskProverID"]);
            AskLeave.strAskProverName = ObjectConvertClass.static_ext_string(dr["strAskProverName"]);
            AskLeave.dtAskCreateTime = ObjectConvertClass.static_ext_date(dr["dtAskCreateTime"]);
            AskLeave.strAskDutyUserName = ObjectConvertClass.static_ext_string(dr["strAskDutyUserName"]);
            AskLeave.strMemo = ObjectConvertClass.static_ext_string(dr["strMemo"]);
            AskLeave.nPostID = ObjectConvertClass.static_ext_int(dr["nPostID"]);
            AskLeave.strGuideGroupName = ObjectConvertClass.static_ext_string(dr["strGuideGroupName"]);

            model.AskLeave = AskLeave;
        }
        #endregion

        #region 给定一个工号，返回该职工的请假信息以及所请假的请假类型名称
        /// <summary>
        /// 获得数据List
        /// </summary>
        public AskLeaveEntity GetLeavesDataListByID(string strTrainManID, out string strTypeName, out Boolean bExist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainManID = '" + strTrainManID + "' and nStatus < 3");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            AskLeaveEntity _LeaveMgr_AskLeave = null;
            if (dt.Rows.Count > 0)
            {
                bExist = true;
                strTypeName = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTypeName"]);
                _LeaveMgr_AskLeave = new AskLeaveEntity();
                LeavesByIDDataRowToModel(_LeaveMgr_AskLeave, dt.Rows[0]);
            }
            else
            {
                strTypeName = "";
                bExist = false;
            }
            return _LeaveMgr_AskLeave;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void LeavesByIDDataRowToModel(AskLeaveEntity model, DataRow dr)
        {
            model.strAskLeaveGUID = ObjectConvertClass.static_ext_string(dr["strAskLeaveGUID"]);
            model.strTrainManID = ObjectConvertClass.static_ext_string(dr["strTrainManID"]);
            model.dtBeginTime = ObjectConvertClass.static_ext_date(dr["dBeginTime"]);
            model.dtEndTime = ObjectConvertClass.static_ext_date(dr["dEndTime"]);
            model.strLeaveTypeGUID = ObjectConvertClass.static_ext_string(dr["strLeaveTypeGUID"]);
            model.nStatus = ObjectConvertClass.static_ext_int(dr["nStatus"]);
        }
        #endregion

        #region 获取请假明细
        /// <summary>
        /// 获得数据List
        /// </summary>
        public AskLeaveDetailEntity GetAskLeaveDetailDataList(string strAskLeaveGUID, out Boolean bExist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_LeaveMgr_AskLeaveDetail where strAskLeaveGUID=@strAskLeaveGUID");
            SqlParameter[] parameters = {
                   new SqlParameter("strAskLeaveGUID",strAskLeaveGUID)
                                        };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters).Tables[0];
            AskLeaveDetailEntity _AskLeaveDetailEntity = null;
            if (dt.Rows.Count > 0)
            {
                bExist = true;
                _AskLeaveDetailEntity = new AskLeaveDetailEntity();
                AskLeaveDetailDataRowToModel(_AskLeaveDetailEntity, dt.Rows[0]);
            }
            else
            {
                bExist = false;
            }
            return _AskLeaveDetailEntity;
        }

        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void AskLeaveDetailDataRowToModel(AskLeaveDetailEntity model, DataRow dr)
        {
            model.strAskLeaveDetailGUID = ObjectConvertClass.static_ext_string(dr["strAskLeaveDetailGUID"]);
            model.strAskLeaveGUID = ObjectConvertClass.static_ext_string(dr["strAskLeaveGUID"]);
            model.strMemo = ObjectConvertClass.static_ext_string(dr["strMemo"]);
            model.dtBeginTime = ObjectConvertClass.static_ext_date(dr["dBeginTime"]);
            model.dtEndTime = ObjectConvertClass.static_ext_date(dr["dEndTime"]);
            model.strProverID = ObjectConvertClass.static_ext_string(dr["strProverID"]);
            model.strProverName = ObjectConvertClass.static_ext_string(dr["strProverName"]);
            model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dCreateTime"]);
            model.strDutyUserID = ObjectConvertClass.static_ext_string(dr["strDutyUserID"]);
            model.strDutyUserName = ObjectConvertClass.static_ext_string(dr["strDutyUserName"]);
            model.strSiteID = ObjectConvertClass.static_ext_string(dr["strSiteID"]);
            model.strSiteName = ObjectConvertClass.static_ext_string(dr["strSiteName"]);
            model.Verify = ObjectConvertClass.static_ext_int(dr["nValidWay"]);
        }
        #endregion

        #region 获取销假明细
        /// <summary>
        /// 获得数据List
        /// </summary>
        public CancelLeaveDetailEntity GetCancelLeaveDetailDataList(string strAskLeaveGUID, out Boolean bExist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_LeaveMgr_CancelLeaveDetail where strAskLeaveGUID=@strAskLeaveGUID");
            SqlParameter[] parameters = {
                   new SqlParameter("strAskLeaveGUID",strAskLeaveGUID)
                                        };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters).Tables[0];
            CancelLeaveDetailEntity _CancelLeaveDetailEntity = null;
            if (dt.Rows.Count > 0)
            {
                bExist = true;
                _CancelLeaveDetailEntity = new CancelLeaveDetailEntity();
                CancelLeaveDetailDataRowToModel(_CancelLeaveDetailEntity, dt.Rows[0]);
            }
            else
            {
                bExist = false;
            }
            return _CancelLeaveDetailEntity;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void CancelLeaveDetailDataRowToModel(CancelLeaveDetailEntity model, DataRow dr)
        {
            model.strCancelLeaveGUID = ObjectConvertClass.static_ext_string(dr["strCancelLeaveGUID"]);
            model.strAskLeaveGUID = ObjectConvertClass.static_ext_string(dr["strAskLeaveGUID"]);
            model.strProverID = ObjectConvertClass.static_ext_string(dr["strProverID"]);
            model.strProverName = ObjectConvertClass.static_ext_string(dr["strProverName"]);
            model.dtCancelTime = ObjectConvertClass.static_ext_date(dr["dtCancelTime"]);
            model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dCreateTime"]);
            model.strDutyUserID = ObjectConvertClass.static_ext_string(dr["strDutyUserID"]);
            model.strDutyUserName = ObjectConvertClass.static_ext_string(dr["strDutyUserName"]);
            model.strSiteID = ObjectConvertClass.static_ext_string(dr["strSiteID"]);
            model.strSiteName = ObjectConvertClass.static_ext_string(dr["strSiteName"]);
            model.Verify = ObjectConvertClass.static_ext_int(dr["nValidWay"]);
        }
        #endregion

        #region 销假

        //销假主函数
        public void CancelLeave(CancelLeaveDetailEntity model)
        {
        
            SqlConnection conn = new SqlConnection(SqlHelper.ConnString);
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    //第一步 操作TAB_LeaveMgr_CancelLeaveDetail，并修改人员表状态
                    AddCancelLeaveDetail(trans, model);

                    //第二步 操作TAB_LeaveMgr_AnnualLeave
                    new DBAnnualLeave().ExecuteUnLeave(trans, model);

                    //第三步 插入请销假消息
                    model.msgType = MSGTYPE.MSG_LEAVE;
                    string strMsg = AttentionMsg.ReturnStrJson(model);
                  
                    CreatMsg(trans, strMsg);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        public void AddCancelLeaveDetail(SqlTransaction trans, CancelLeaveDetailEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_LeaveMgr_CancelLeaveDetail");
            strSql.Append("(strCancelLeaveGUID,strAskLeaveGUID,strProverID,strProverName,dtCancelTime,dCreateTime,strDutyUserID,strDutyUserName,strSiteID,strSiteName,nValidWay)");
            strSql.Append("values(@strCancelLeaveGUID,@strAskLeaveGUID,@strProverID,@strProverName,@dtCancelTime,@dCreateTime,@strDutyUserID,@strDutyUserName,@strSiteID,@strSiteName,@nValidWay)");
            SqlParameter[] parameters = {
                  new SqlParameter("@strCancelLeaveGUID", model.strCancelLeaveGUID),
                  new SqlParameter("@strAskLeaveGUID", model.strAskLeaveGUID),
                  new SqlParameter("@strProverID", model.strProverID),
                  new SqlParameter("@strProverName", model.strProverName),
                  new SqlParameter("@dtCancelTime", model.dtCancelTime),
                  new SqlParameter("@dCreateTime", model.dtCreateTime),
                  new SqlParameter("@strDutyUserID", model.strDutyUserID),
                  new SqlParameter("@strDutyUserName", model.strDutyUserName),
                  new SqlParameter("@strSiteID", model.strSiteID),
                  new SqlParameter("@strSiteName", model.strSiteName),
                  new SqlParameter("@nValidWay", model.Verify)};
            var obj = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), parameters);
            if (obj > 0)
            {
                string strSqlUpdate = "update TAB_LeaveMgr_AskLeave set nStatus = 3, dEndTime = @dEndTime where strAskLeaveGUID = @strAskLeaveGUID;";
                //tsReady {预备}
                strSqlUpdate += "update TAB_Org_Trainman set nTrainmanState = 1 where strTrainmanNumber = @strTrainmanNumber";
                SqlParameter[] paraAskLeave = {
                  new SqlParameter("@dEndTime", model.dtCancelTime),
                  new SqlParameter("@strAskLeaveGUID", model.strAskLeaveGUID),
                  new SqlParameter("@strTrainmanNumber", model.strTrainmanID)
                                            };
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSqlUpdate, paraAskLeave);
            }
        }
        #endregion


        #region 获取最后一条请假记录，用于判断是当前是否处于年休假
        public AskLeaveWithType GetLastLeaveInfo(string strTrainmanID)
        {
            AskLeaveWithType ret = new AskLeaveWithType();


            string sql = "select top 1 * from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainmanID = @strTrainmanID order by dBeginTime desc";
            

            DbParamDict dbParamDict = new DbParamDict();
            dbParamDict.Add("strTrainmanID",strTrainmanID,ParamDataType.dtString);
            

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,sql,dbParamDict.GetParams()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr;
                dr = dt.Rows[0];
                ret.strTypeName = ObjectConvertClass.static_ext_string(dr["strTypeName"]);
                AskLeaveEntity AskLeave = new AskLeaveEntity();
                AskLeave.strAskLeaveGUID = ObjectConvertClass.static_ext_string(dr["strAskLeaveGUID"]);
                AskLeave.strTrainManID = ObjectConvertClass.static_ext_string(dr["strTrainManID"]);                
                AskLeave.dtBeginTime = ObjectConvertClass.static_ext_date(dr["dBeginTime"]);
                AskLeave.dtEndTime = ObjectConvertClass.static_ext_date(dr["dEndTime"]);                
                AskLeave.nStatus = ObjectConvertClass.static_ext_int(dr["nStatus"]);                
                ret.AskLeave = AskLeave;
                return ret;
            }
            else
            {
                return null;
            }
                        
        }
        #endregion
        
    
    
    }
    #endregion
}
