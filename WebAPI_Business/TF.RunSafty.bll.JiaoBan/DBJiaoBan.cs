using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ThinkFreely.DBUtility;
using TF.CommonUtility;

namespace TF.RunSafty.JiaoBan
{
   public class DBJiaoBan
   {
       #region CancelNotify（1.10.1取消叫班通知）
       public bool CancelNotify(string strGUID, string strUser, DateTime dtCancelTime, string strReason)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("Update tab_MsgCallWork set ");
           strSql.Append(" nCancel = @nCancel, ");
           strSql.Append(" strCancelUser = @strUser, ");
           strSql.Append(" dtCancelTime = @dtCancelTime, ");
           strSql.Append(" strCancelReason = @strReason ");
           strSql.Append(" where strMsgGUID = @strGUID ");

           SqlParameter[] parameters = { 
                new SqlParameter("@nCancel", 1),
                new SqlParameter("@strUser", strUser),
                new SqlParameter("@dtCancelTime", dtCancelTime),
                new SqlParameter("@strReason",strReason),
                new SqlParameter("@strGUID", strGUID)};
           return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
       }
       #endregion

       #region CancelNotify（添加叫班通知）
       public bool AddNotify(CallNotify model)
       {
           DataTable dtCallNotify = null;
           if (FindUnCancel(model.strTrainmanGUID, model.strTrainPlanGUID, ref dtCallNotify))
           {
               string strMsgGUID = dtCallNotify.Rows[0]["strMsgGUID"].ToString();
               string strCancelUser = dtCallNotify.Rows[0]["strCancelUser"].ToString();
               string strCancelReason = dtCallNotify.Rows[0]["strCancelReason"].ToString();
               DateTime dateTimeNow = DateTime.Now;
               CancelNotify(strMsgGUID, strCancelUser, dateTimeNow, strCancelReason);
           }
           if (add(model))
           {
               return true;
           }
           else
           {
               return false;
           }
       }
       public bool add(CallNotify model)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("insert into tab_MsgCallWork");
           strSql.Append("(strTrainPlanGUID,strMsgGUID,strTrainmanGUID,strSendMsgContent,strRecvMsgContent,dtSendTime,strSendUser,dtRecvTime,strRecvUser,eCallState,eCallType,nCancel,strCancelReason,dtCancelTime,strCancelUser)");
           strSql.Append("values(@strTrainPlanGUID,@strMsgGUID,@strTrainmanGUID,@strSendMsgContent,@strRecvMsgContent,@dtSendTime,@strSendUser,@dtRecvTime,@strRecvUser,@eCallState,@eCallType,@nCancel,@strCancelReason,@dtCancelTime,@strCancelUser)");
           SqlParameter[] parameters = {
                new SqlParameter("@strTrainPlanGUID", model.strTrainPlanGUID),
                new SqlParameter("@strMsgGUID", model.strMsgGUID),
                new SqlParameter("@strTrainmanGUID", model.strTrainmanGUID),
                new SqlParameter("@strSendMsgContent", model.strSendMsgContent),
                new SqlParameter("@strRecvMsgContent", model.strRecvMsgContent),
                new SqlParameter("@dtSendTime", model.dtSendTime),
                new SqlParameter("@strSendUser", model.strSendUser),
                new SqlParameter("@dtRecvTime", model.dtRecvTime),
                new SqlParameter("@strRecvUser", model.strRecvUser),
                new SqlParameter("@eCallState", model.eCallState),
                new SqlParameter("@eCallType", model.eCallType),
                new SqlParameter("@nCancel", model.nCancel),
                new SqlParameter("@strCancelReason", model.strCancelReason),
                new SqlParameter("@dtCancelTime", model.dtCancelTime),
                new SqlParameter("@strCancelUser", model.strCancelUser)
                                       };
           return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
       }
       public bool FindUnCancel(string strTrainmanGUID, string strTrainPlanGUID, ref DataTable CallNotify)
       {
           DataTable dt;
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select * from view_msgCallWork where strTrainmanGUID ='" + strTrainmanGUID + "' and strTrainPlanGUID ='" + strTrainPlanGUID + "' and nCancel = 0 ");
           dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
           if (dt.Rows.Count > 0)
           {
               CallNotify = dt;
               return true;
           }
           else
           {
               return false;
           }
       }

     
       #endregion

       #region GetStationList方法（获取所有的车站）
       public CallNotify FindUnCancelforNone(string strTrainmanGUID, string strTrainPlanGUID, ref bool Boolresult)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select * from view_msgCallWork where strTrainmanGUID ='" + strTrainmanGUID + "' and strTrainPlanGUID ='" + strTrainPlanGUID + "' and nCancel = 0 ");
           DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
           if (dt.Rows.Count > 0)
           {
               Boolresult = true;
               return FindUnCancelforNone_dtToModel(dt);
           }
           else
           {
               Boolresult = false;
               return new CallNotify();
           }
       }



       public CallNotify FindUnCancelforNone_dtToModel(DataTable dt)
       {
           CallNotify model = new CallNotify();
           if (dt.Rows.Count > 0)
           {
               model.nId = ObjectConvertClass.static_ext_int(dt.Rows[0]["nId"]);
               model.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainPlanGUID"]);
               model.strMsgGUID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strMsgGUID"]);
               model.strTrainmanGUID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanGUID"]);
               model.strSendMsgContent = ObjectConvertClass.static_ext_string(dt.Rows[0]["strSendMsgContent"]);
               model.strRecvMsgContent = ObjectConvertClass.static_ext_string(dt.Rows[0]["strRecvMsgContent"]);
               model.dtSendTime = ObjectConvertClass.static_ext_date(dt.Rows[0]["dtSendTime"]);
               model.strSendUser = ObjectConvertClass.static_ext_string(dt.Rows[0]["strSendUser"]);
               model.dtRecvTime = ObjectConvertClass.static_ext_date(dt.Rows[0]["dtRecvTime"]);
               model.strRecvUser = ObjectConvertClass.static_ext_string(dt.Rows[0]["strRecvUser"]);
               model.eCallState = ObjectConvertClass.static_ext_int(dt.Rows[0]["eCallState"]);
               model.eCallType = ObjectConvertClass.static_ext_int(dt.Rows[0]["eCallType"]);
               model.nCancel = ObjectConvertClass.static_ext_int(dt.Rows[0]["nCancel"]);
               model.strCancelReason = ObjectConvertClass.static_ext_string(dt.Rows[0]["strCancelReason"]);
               model.strCancelUser = ObjectConvertClass.static_ext_string(dt.Rows[0]["strCancelUser"]);
               model.dtCancelTime = ObjectConvertClass.static_ext_date(dt.Rows[0]["dtCancelTime"]);
               model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanNumber"]);
               model.strTrainmanName = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanName"]);
               model.strTrainNo = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainNo"]);
               model.dtStartTime = ObjectConvertClass.static_ext_date(dt.Rows[0]["dtStartTime"]);
               model.strMobileNumber = ObjectConvertClass.static_ext_string(dt.Rows[0]["strMobileNumber"]);
               model.dtChuQinTime = ObjectConvertClass.static_ext_date(dt.Rows[0]["dtChuQinTime"]);
               model.dtCallTime = ObjectConvertClass.static_ext_date(dt.Rows[0]["dtCallTime"]);
           }
           return model;
       }
       #endregion

       #region GetByStateSec方法（获取所有的车站）
       public List<CallNotify> GetByStateSec(int startState, int endState, DateTime dtStartSendTime, bool NotCancel)
       {

           string strsql = "select * from View_msgCallWork where eCallState >=" + startState + " and eCallState <=" + endState + " and dtSendTime >= '" + dtStartSendTime + "'";
           if (NotCancel == true)
               strsql += " and nCancel = 0 ";
           strsql += "  order by eCallState,dtSendTime";
           return GetByStateSecDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strsql.ToString()).Tables[0]);
       }

       public List<CallNotify> GetByStateSecDTToList(DataTable dt)
       {
           List<CallNotify> modelList = new List<CallNotify>();
           int rowsCount = dt.Rows.Count;
           if (rowsCount > 0)
           {
               CallNotify model;
               for (int n = 0; n < rowsCount; n++)
               {
                   model = GetByStateSecDRToModelDTToList(dt.Rows[n]);
                   if (model != null)
                   {
                       modelList.Add(model);
                   }
               }
           }
           return modelList;
       }
       public CallNotify GetByStateSecDRToModelDTToList(DataRow dr)
       {
           CallNotify model = new CallNotify();
           if (dr != null)
           {
               model.nId = ObjectConvertClass.static_ext_int(dr["nId"]);
               model.strTrainPlanGUID = ObjectConvertClass.static_ext_string(dr["strTrainPlanGUID"]);
               model.strMsgGUID = ObjectConvertClass.static_ext_string(dr["strMsgGUID"]);
               model.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanGUID"]);
               model.strSendMsgContent = ObjectConvertClass.static_ext_string(dr["strSendMsgContent"]);
               model.strRecvMsgContent = ObjectConvertClass.static_ext_string(dr["strRecvMsgContent"]);
               model.dtSendTime = ObjectConvertClass.static_ext_date(dr["dtSendTime"]);
               model.strSendUser = ObjectConvertClass.static_ext_string(dr["strSendUser"]);
               model.dtRecvTime = ObjectConvertClass.static_ext_date(dr["dtRecvTime"]);
               model.strRecvUser = ObjectConvertClass.static_ext_string(dr["strRecvUser"]);
               model.eCallState = ObjectConvertClass.static_ext_int(dr["eCallState"]);
               model.eCallType = ObjectConvertClass.static_ext_int(dr["eCallType"]);
               model.nCancel = ObjectConvertClass.static_ext_int(dr["nCancel"]);
               model.strCancelReason = ObjectConvertClass.static_ext_string(dr["strCancelReason"]);
               model.strCancelUser = ObjectConvertClass.static_ext_string(dr["strCancelUser"]);
               model.dtCancelTime = ObjectConvertClass.static_ext_date(dr["dtCancelTime"]);
               model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
               model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
               model.strTrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
               model.dtStartTime = ObjectConvertClass.static_ext_date(dr["dtStartTime"]);
               model.strMobileNumber = ObjectConvertClass.static_ext_string(dr["strMobileNumber"]);
               model.dtChuQinTime = ObjectConvertClass.static_ext_date(dr["dtChuQinTime"]);
               model.dtCallTime = ObjectConvertClass.static_ext_date(dr["dtCallTime"]);
           }
           return model;
       }
       #endregion

    }
}
