using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.CommonUtility;
using TF.RunSafty.TeamGuide.MD;

namespace TF.RunSafty.TeamGuide.DB
{
    #region 指导队数据操作类
    /// <summary>                             
    ///类名: DBTeamGuide
    ///说明: 指导队数据操作类
    /// </summary> 
    public class DBTeamGuide
    {
        #region 获取所有车间
        /// <summary>
        /// 获得数据List
        /// </summary>
        public SimpleInfoArray GetWorkShopDataList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_Org_WorkShop order by strWorkShopName");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            SimpleInfoArray list = new SimpleInfoArray();
            foreach (DataRow dr in dt.Rows)
            {
                SimpleInfo _Org_WorkShop = new SimpleInfo();
                WorkShopDataRowToModel(_Org_WorkShop, dr);
                list.Add(_Org_WorkShop);
            }
            return list;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void WorkShopDataRowToModel(SimpleInfo model, DataRow dr)
        {
            model.strGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
            model.strName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
        }
        #endregion

        #region 获取车间GUID
        public bool GetWorkShopGUID(string strGuideGroupGUID,out string WorkShopGUID)
        {
            string strSql = "select strWorkShopGUID from TAB_Org_GuideGroup where strGuideGroupGUID=@strGuideGroupGUID";
            SqlParameter[] parameters = { 
                                            new SqlParameter("strGuideGroupGUID",strGuideGroupGUID)
                                        };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, parameters).Tables[0];
            WorkShopGUID = "";
            if (dt.Rows.Count > 0)
            {
                WorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();
                return true;
            }
            return false;
        }
        #endregion

        #region 获取指导队名称
        public string GetGuideGroupName(string strGuideGroupGUID)
        {
            string strSql = "select strGuideGroupName from TAB_Org_GuideGroup where strGuideGroupGUID=@strGuideGroupGUID";
            SqlParameter[] parameters = { 
                                            new SqlParameter("strGuideGroupGUID",strGuideGroupGUID)
                                        };
            return ObjectConvertClass.static_ext_string(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, parameters));
        }
        #endregion

        #region 获取车间名称－指导队名称
        public bool GetWorkShop_GuideGroup(string strGuideGroupGUID,out string GuideName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.strGuideGroupName,B.strWorkShopName from TAB_Org_GuideGroup A left join TAB_Org_WorkShop B ");
            strSql.Append(" on A.strWorkShopGUID=B.strWorkShopGUID where A.strGuideGroupGUID=@strGuideGroupGUID");
            SqlParameter[] parameters = { 
                                            new SqlParameter("strGuideGroupGUID",strGuideGroupGUID)
                                        };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters).Tables[0];
            GuideName = "";
            if (dt.Rows.Count > 0)
            {
                GuideName = ObjectConvertClass.static_ext_string(dt.Rows[0]["strWorkShopName"]) + "."+ ObjectConvertClass.static_ext_string(dt.Rows[0]["strGuideGroupName"]);
                return true;
            }
            return false;
        }
        #endregion

        #region 根据车间，获取指导队
        /// <summary>
        /// 获得数据List
        /// </summary>
        public SimpleInfoArray GetGroupDataList(string strWorkShopGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_Org_GuideGroup where strWorkShopGUID=@strWorkShopGUID order by nid");
            SqlParameter[] parameters = { 
                                            new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                        };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters).Tables[0];
            SimpleInfoArray list = new SimpleInfoArray();
            foreach (DataRow dr in dt.Rows)
            {
                SimpleInfo _Org_GuideGroup = new SimpleInfo();
                GroupDataRowToModel(_Org_GuideGroup, dr);
                list.Add(_Org_GuideGroup);
            }
            return list;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void GroupDataRowToModel(SimpleInfo model, DataRow dr)
        {
            model.strGUID = ObjectConvertClass.static_ext_string(dr["strGuideGroupGUID"]);
            model.strName = ObjectConvertClass.static_ext_string(dr["strGuideGroupName"]);
        }
        #endregion

        //=========================================//

        #region 按人员ID更新所属指导队ID
        /// <summary>
        /// 更新数据
        /// </summary>
        public bool UpdateGroupByID(string strTrainmanGUID, string strGuideGroupGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_Org_Trainman set strGuideGroupGUID=@strGuideGroupGUID where strTrainmanGUID=@strTrainmanGUID ");
            SqlParameter[] parameters = {
                  new SqlParameter("@strTrainmanGUID",strTrainmanGUID),
                  new SqlParameter("@strGuideGroupGUID", strGuideGroupGUID)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region 按工号更新所属指导队ID
        /// <summary>
        /// 更新数据
        /// </summary>
        public bool UpdateGroupByNumber(string strGuideGroupGUID, string strTrainmanNumber, Boolean bNotUpdateExist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_Org_Trainman set strGuideGroupGUID=@strGuideGroupGUID where strTrainmanNumber=@strTrainmanNumber ");
            strSql.Append(bNotUpdateExist == true ? " and (strGuideGroupGUID='' or (strGuideGroupGUID is null))" : "");
            SqlParameter[] parameters = {
                  new SqlParameter("@strGuideGroupGUID",strGuideGroupGUID),
                  new SqlParameter("@strTrainmanNumber", strTrainmanNumber)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region 更新司机职位
        public bool UpdatePostID(string strTrainmanGUID, int nPostID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_Org_Trainman set nPostID=@nPostID where strTrainmanGUID=@strTrainmanGUID");
            SqlParameter[] parameters = {
                  new SqlParameter("@nPostID",nPostID),
                  new SqlParameter("@strTrainmanGUID", strTrainmanGUID)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region 根据查询条件和过滤条件，得到司机列表
        /// <summary>
        /// 获得数据List
        /// </summary>
        public TeamGuideTrainmanArray GetTrainmanDataList(RsQueryTrainman QueryTrainman, RsQueryTrainman FilterTrainman)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder SqlCondition = new StringBuilder();
            SqlCondition.Append(QueryTrainman.strWorkShopGUID != "" ? " and A.strWorkShopGUID = @strWorkShopGUID" : "");
            SqlCondition.Append(QueryTrainman.strGuideGroupGUID != "" ? " and A.strGuideGroupGUID = @strGuideGroupGUID" : "");
            SqlCondition.Append(FilterTrainman.strWorkShopGUID != "" ? " and (A.strWorkShopGUID is null or A.strWorkShopGUID <> @strWorkShopGUIDFil)" : "");
            SqlCondition.Append(FilterTrainman.strGuideGroupGUID != "" ? " and (A.strGuideGroupGUID is null or A.strGuideGroupGUID <> @strGuideGroupGUIDFil)" : "");
            SqlParameter[] sqlParams ={
                      new SqlParameter("strWorkShopGUID",QueryTrainman.strWorkShopGUID),
                      new SqlParameter("strGuideGroupGUID",QueryTrainman.strGuideGroupGUID),
                      new SqlParameter("strWorkShopGUIDFil",FilterTrainman.strWorkShopGUID),
                      new SqlParameter("strGuideGroupGUIDFil",FilterTrainman.strGuideGroupGUID)
                                      };
            strSql.Append("select A.strTrainmanGUID,A.strTrainmanNumber,A.strTrainmanName,A.strWorkShopGUID,B.strWorkShopName,A.strGuideGroupGUID,C.strGuideGroupName From Tab_Org_Trainman A");
            strSql.Append(" left join TAB_Org_WorkShop B on A.strWorkShopGUID=B.strWorkShopGUID");
            strSql.Append(" left join TAB_Org_GuideGroup C on A.strGuideGroupGUID=C.strGuideGroupGUID");
            strSql.Append(" where 1=1 " + SqlCondition.ToString() + " order by strTrainmanNumber");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            TeamGuideTrainmanArray list = new TeamGuideTrainmanArray();
            foreach (DataRow dr in dt.Rows)
            {
                TeamGuideTrainman _Org_Trainman = new TeamGuideTrainman();
                TrainmanDataRowToModel(_Org_Trainman, dr);
                list.Add(_Org_Trainman);
            }
            return list;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void TrainmanDataRowToModel(TeamGuideTrainman model, DataRow dr)
        {
            model.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanGUID"]);
            model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
            model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
            model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
            model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
            model.strGuideGroupGUID = ObjectConvertClass.static_ext_string(dr["strGuideGroupGUID"]);
            model.strGuideGroupName = ObjectConvertClass.static_ext_string(dr["strGuideGroupName"]);
        }
        #endregion

        #region 按职位获取人员列表
        /// <summary>
        /// 获得数据List
        /// </summary>
        public TeamGuideTrainmanArray GetTrainmanDataListByPost(string strWorkShopGUID, int nPostID, int cmdType)
        {
            StringBuilder strSql = new StringBuilder();

            #region 查询条件
            StringBuilder SqlCondition = new StringBuilder();
            SqlCondition.Append(strWorkShopGUID != "" ? " and A.strWorkShopGUID = @strWorkShopGUID" : "");
            switch (cmdType)
            {
                case 0:
                    SqlCondition.Append(((nPostID >= 1) && (nPostID <= 3)) ? " and  A.nPostID = @nPostID" : "");
                    break;
                case 1:
                    SqlCondition.Append(nPostID == 0 ? " and (A.nPostID is null or A.nPostID = 0)" : "");
                    SqlCondition.Append(((nPostID >= 1) && (nPostID <= 3)) ? " and (A.nPostID is null or A.nPostID <> @nPostID)" : "");
                    break;
            }

            SqlParameter[] sqlParams ={
                      new SqlParameter("strWorkShopGUID",strWorkShopGUID),
                      new SqlParameter("nPostID",nPostID)
                                      };
            #endregion 查询条件

            strSql.Append("select A.strTrainmanGUID,A.strTrainmanNumber,A.strTrainmanName,A.strWorkShopGUID,B.strWorkShopName,A.nPostID From Tab_Org_Trainman A");
            strSql.Append(" left join TAB_Org_WorkShop B on A.strWorkShopGUID=B.strWorkShopGUID");
            strSql.Append(" where 1=1 " + SqlCondition.ToString() + " order by A.strTrainmanNumber");

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            TeamGuideTrainmanArray list = new TeamGuideTrainmanArray();
            foreach (DataRow dr in dt.Rows)
            {
                TeamGuideTrainman _Org_Trainman = new TeamGuideTrainman();
                TrainmanDataRowToModelByPost(_Org_Trainman, dr);
                list.Add(_Org_Trainman);
            }
            return list;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void TrainmanDataRowToModelByPost(TeamGuideTrainman model, DataRow dr)
        {
            model.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanGUID"]);
            model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
            model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
            model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
            model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
            model.nPostID = ObjectConvertClass.static_ext_int(dr["nPostID"]);
        }
        #endregion
    }
    #endregion

    #region 签到信息数据操作类
    /// <summary>                             
    ///类名: DBSign_GuideGroup
    ///说明: 签到信息数据操作类
    /// </summary> 
    public class DBSign_GuideGroup
    {
        #region 添加签到信息
        public bool AddGuideSignIn(GuideSignEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Sign_GuideGroup");
            strSql.Append("(strTrainmanNumber,strTrainmanName,strWorkShopGUID,strGuideGroupGUID,nSignInFlag");
            strSql.Append(!string.IsNullOrEmpty(model.strGuideSignGUID) ? ",strGuideSignGUID" : "");
            strSql.Append(model.dtSignInTime >= DateTimeMinValue.dtMinValue ? ",dtSignInTime" : "");
            strSql.Append(")");
            strSql.Append("values(@strTrainmanNumber,@strTrainmanName,@strWorkShopGUID,@strGuideGroupGUID,@nSignInFlag");
            strSql.Append(!string.IsNullOrEmpty(model.strGuideSignGUID) ? ",@strGuideSignGUID" : "");
            strSql.Append(model.dtSignInTime >= DateTimeMinValue.dtMinValue ? ",@dtSignInTime" : "");
            strSql.Append(")");
            SqlParameter[] parameters = {
                  new SqlParameter("@strGuideSignGUID", model.strGuideSignGUID),
                  new SqlParameter("@strTrainmanNumber", model.strTrainmanNumber),
                  new SqlParameter("@strTrainmanName", model.strTrainmanName),
                  new SqlParameter("@strWorkShopGUID", model.strWorkShopGUID),
                  new SqlParameter("@strGuideGroupGUID", model.strGuideGroupGUID),
                  new SqlParameter("@dtSignInTime", model.dtSignInTime),
                  new SqlParameter("@nSignInFlag", model.nSignInFlag)};

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region 添加签退信息
        public bool AddGuideSignOut(GuideSignEntity model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_Sign_GuideGroup set");
            strSql.Append(" nSignOutFlag = @nSignOutFlag");
            strSql.Append(model.dtSignOutTime >= DateTimeMinValue.dtMinValue ? ",dtSignOutTime = @dtSignOutTime" : "");
            strSql.Append(" where strGuideSignGUID=@strGuideSignGUID");
            SqlParameter[] parameters = {
                  new SqlParameter("@strGuideSignGUID", model.strGuideSignGUID),
                  new SqlParameter("@dtSignOutTime", model.dtSignOutTime),
                  new SqlParameter("@nSignOutFlag", model.nSignOutFlag)};

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region 查询签到信息
        public string GetWhere(GuideSignQryParam model)
        {
            StringBuilder strSqlOption = new StringBuilder();
            strSqlOption.Append(!string.IsNullOrEmpty(model.strTrainmanName) ? " and strTrainmanName = @strTrainmanName" : "");
            strSqlOption.Append(!string.IsNullOrEmpty(model.strWorkShopGUID) ? " and strWorkShopGUID = @strWorkShopGUID" : "");
            strSqlOption.Append(!string.IsNullOrEmpty(model.strGuideGroupGUID) ? " and strGuideGroupGUID = @strGuideGroupGUID" : "");
            strSqlOption.Append(model.dtSignTimeBegin >= DateTimeMinValue.dtMinValue && model.dtSignTimeEnd >= DateTimeMinValue.dtMinValue ?
                " and (dtSignInTime >= @dtSignTimeBegin and dtSignInTime <= @dtSignTimeEnd)" : "");
            strSqlOption.Append(model.nSignFlag > 0 ? " and nSignInFlag = @nSignFlag" : "");
            strSqlOption.Append(!string.IsNullOrEmpty(model.strTrainmanNumber) ? " and strTrainmanNumber like @strTrainmanNumber" : "");

            return strSqlOption.ToString();
        }
        /// <summary>
        /// 获得数据List
        /// </summary>
        public GuideSignArray GetSignDataList(GuideSignQryParam QueryCondition)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM VIEW_Sign_GuideGroup where 1=1");
            strSql.Append(GetWhere(QueryCondition));
            strSql.Append(" order by dtSignInTime desc");
            SqlParameter[] sqlParams ={
                  new SqlParameter("strTrainmanName",QueryCondition.strTrainmanName),
                  new SqlParameter("strWorkShopGUID",QueryCondition.strWorkShopGUID),
                  new SqlParameter("strGuideGroupGUID",QueryCondition.strGuideGroupGUID),
                  new SqlParameter("dtSignTimeBegin",QueryCondition.dtSignTimeBegin),
                  new SqlParameter("dtSignTimeEnd",QueryCondition.dtSignTimeEnd),
                  new SqlParameter("nSignFlag",QueryCondition.nSignFlag),
                  new SqlParameter("strTrainmanNumber","%"+QueryCondition.strTrainmanNumber+"%")};
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            GuideSignArray list = new GuideSignArray();
            foreach (DataRow dr in dt.Rows)
            {
                GuideSignEntity _Sign_GuideGroup = new GuideSignEntity();
                GuideSignDataRowToModel(_Sign_GuideGroup, dr);
                list.Add(_Sign_GuideGroup);
            }
            return list;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void GuideSignDataRowToModel(GuideSignEntity model, DataRow dr)
        {
            model.strGuideSignGUID = ObjectConvertClass.static_ext_string(dr["strGuideSignGUID"]);
            model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
            model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
            model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
            model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
            model.strGuideGroupGUID = ObjectConvertClass.static_ext_string(dr["strGuideGroupGUID"]);
            model.strGuideGroupName = ObjectConvertClass.static_ext_string(dr["strGuideGroupName"]);
            model.dtSignInTime = ObjectConvertClass.static_ext_date(dr["dtSignInTime"]);
            model.nSignInFlag = ObjectConvertClass.static_ext_int(dr["nSignInFlag"]);
            model.dtSignOutTime = ObjectConvertClass.static_ext_date(dr["dtSignOutTime"]);
            model.nSignOutFlag = ObjectConvertClass.static_ext_int(dr["nSignOutFlag"]);
        }
        #endregion

        #region 查询未签到信息
        public GuideSignArray GetNotSignDataList(GuideSignQryParam QueryCondition)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder sqlCondition = new StringBuilder();
            sqlCondition.Append("select strTrainmanNumber From VIEW_Sign_GuideGroup where 1=1");
            sqlCondition.Append(QueryCondition.dtSignTimeBegin >= DateTimeMinValue.dtMinValue && QueryCondition.dtSignTimeEnd >= DateTimeMinValue.dtMinValue
                ? " and (dtSignInTime >= @dtSignTimeBegin and dtSignInTime <= @dtSignTimeEnd)" : "");

            strSql.Append("select strTrainmanNumber,strTrainmanName from TAB_Org_Trainman ");
            strSql.Append(" where strGuideGroupGUID=@strGuideGroupGUID and strTrainmanNumber not in (" + sqlCondition + ") order by strTrainmanNumber");

            SqlParameter[] sqlParams ={
                  new SqlParameter("strGuideGroupGUID",QueryCondition.strGuideGroupGUID),
                  new SqlParameter("dtSignTimeBegin",QueryCondition.dtSignTimeBegin),
                  new SqlParameter("dtSignTimeEnd",QueryCondition.dtSignTimeEnd)
                                      };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            GuideSignArray list = new GuideSignArray();
            foreach (DataRow dr in dt.Rows)
            {
                GuideSignEntity _Sign_GuideGroup = new GuideSignEntity();
                _Sign_GuideGroup.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
                _Sign_GuideGroup.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
                list.Add(_Sign_GuideGroup);
            }
            return list;
        }
        #endregion

        #region 获取指定GUID的签到信息
        /// <summary>
        /// 获取指定GUID的签到信息
        /// </summary>
        /// <param name="strGuideSignGUID">记录ID</param>
        /// <param name="bExist">返回参数，是否存在</param>
        /// <returns></returns>
        public GuideSignEntity GetSignRecord(string strGuideSignGUID, out Boolean bExist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Sign_GuideGroup where strGuideSignGUID = @strGuideSignGUID");
            SqlParameter[] sqlParams ={
                      new SqlParameter("strGuideSignGUID",strGuideSignGUID)
                                      };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            GuideSignEntity _Sign_GuideGroup = null;
            if (dt.Rows.Count > 0)
            {
                bExist = true;
                _Sign_GuideGroup = new GuideSignEntity();
                GuideSignDataRowToModel(_Sign_GuideGroup, dt.Rows[0]);
            }
            else
            {
                bExist = false;
            }
            return _Sign_GuideGroup;
        }
        #endregion

        #region 获取指定司机工号的签到信息
        /// <summary>
        /// 获取指定司机工号的签到信息
        /// </summary>
        /// <param name="strTrainmanNumber">人员工号</param>
        /// <param name="strGuideGroupGUID">指导队ID</param>
        /// <param name="bExist">返回参数，是否存在</param>
        /// <returns></returns>
        public GuideSignEntity GetSignRecordByTrainmanNumber(string strTrainmanNumber, string strGuideGroupGUID, out Boolean bExist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from VIEW_Sign_GuideGroup where strTrainmanNumber=@strTrainmanNumber and strGuideGroupGUID=@strGuideGroupGUID order by dtSignInTime desc");
            SqlParameter[] sqlParams ={
                      new SqlParameter("strTrainmanNumber",strTrainmanNumber),
                      new SqlParameter("strGuideGroupGUID",strGuideGroupGUID)
                                      };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            GuideSignEntity _Sign_GuideGroup = null;
            if (dt.Rows.Count > 0)
            {
                bExist = true;
                _Sign_GuideGroup = new GuideSignEntity();
                GuideSignDataRowToModel(_Sign_GuideGroup, dt.Rows[0]);
            }
            else
            {
                bExist = false;
            }
            return _Sign_GuideGroup;
        }
        #endregion
    }
    #endregion
}
