using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.CommonUtility;
using TF.RunSafty.Drink.MD;
using ThinkFreely.RunSafty;

namespace TF.RunSafty.Drink.DB
{
    #region 测酒信息数据库操作类
    /// <summary>                             
    ///类名: DBDrink
    ///说明: 测酒信息数据库操作类
    /// </summary> 
    public class DBLCDrink
    {
        #region 获取测酒信息
        public string GetWhere(DrinkQueryParam model)
        {
            StringBuilder strSqlOption = new StringBuilder();
            strSqlOption.Append(!string.IsNullOrEmpty(model.PlaceId) ? " and strPlaceID = @strPlaceID" : "");
            strSqlOption.Append(!string.IsNullOrEmpty(model.strJwdId) ? " and strAreaGUID = @strAreaGUID" : "");
            strSqlOption.Append(!string.IsNullOrEmpty(model.WorkShopGUID) ? " and strWorkShopGUID = @strWorkShopGUID" : "");
            strSqlOption.Append(!string.IsNullOrEmpty(model.TrainmanNumber) ? " and strTrainmanNumber = @strTrainmanNumber" : "");
            strSqlOption.Append(model.VerifyID > -1 ? " and nVerifyID = @nVerifyID" : "");
            strSqlOption.Append(model.DrinkTypeID > -1 ? " and nWorkTypeID = @nWorkTypeID" : "");
            strSqlOption.Append(model.DrinkResultID > -1 ? " and nDrinkResult = @nDrinkResult" : "");
            strSqlOption.Append(!string.IsNullOrEmpty(model.TrainmanName) ? " and strTrainmanName like @strTrainmanName" : "");
            strSqlOption.Append(!string.IsNullOrEmpty(model.DepartmentID) ? " and strDepartmentID = @strDepartmentID" : "");
            strSqlOption.Append(model.CadreTypeID != "" && model.CadreTypeID != "-1" ? " and nCadreTypeID = @nCadreTypeID" : "");

            return strSqlOption.ToString();
        }

        /// <summary>
        /// 获得数据List
        /// </summary>
        public DrinkInfoArray GetDrinkDataList(DrinkQueryParam QueryCondition)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Drink_query where dtCreateTime >= @dtBeginTime and dtCreateTime <= @dtEndTime ");
            strSql.Append(GetWhere(QueryCondition));
            strSql.Append(" order by dtCreateTime desc");

            SqlParameter[] sqlParams ={
                      new SqlParameter("dtBeginTime",QueryCondition.dtBeginTime),
                      new SqlParameter("dtEndTime",QueryCondition.dtEndTime),
                      new SqlParameter("strPlaceID",QueryCondition.PlaceId),
                      new SqlParameter("strAreaGUID",QueryCondition.strJwdId),
                      new SqlParameter("strWorkShopGUID",QueryCondition.WorkShopGUID),
                      new SqlParameter("strTrainmanNumber",QueryCondition.TrainmanNumber),
                      new SqlParameter("nVerifyID",QueryCondition.VerifyID),
                      new SqlParameter("nWorkTypeID",QueryCondition.DrinkTypeID),
                      new SqlParameter("nDrinkResult",QueryCondition.DrinkResultID),
                      new SqlParameter("strTrainmanName",QueryCondition.TrainmanName+"%"),
                      new SqlParameter("nCadreTypeID",QueryCondition.CadreTypeID),
                      new SqlParameter("strDepartmentID",QueryCondition.DepartmentID),
                                      };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            DrinkInfoArray list = new DrinkInfoArray();
            foreach (DataRow dr in dt.Rows)
            {
                DrinkInfo _Drink_Query = new DrinkInfo();
                DrinkDataRowToModel(_Drink_Query, dr);
                list.Add(_Drink_Query);
            }
            return list;
        }
        /// <summary>
        /// 读取DataRow数据到Model中
        /// </summary>
        private void DrinkDataRowToModel(DrinkInfo model, DataRow dr)
        {
            model.strGUID = ObjectConvertClass.static_ext_string(dr["strGUID"]);
            model.nDrinkResult = ObjectConvertClass.static_ext_int(dr["nDrinkResult"]);
            model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);
            model.nVerifyID = ObjectConvertClass.static_ext_int(dr["nVerifyID"]);
            model.nWorkTypeID = ObjectConvertClass.static_ext_int(dr["nWorkTypeID"]);
            model.strWorkTypeName = ObjectConvertClass.static_ext_string(dr["strWorkTypeName"]);
            model.strDrinkResultName = ObjectConvertClass.static_ext_string(dr["strDrinkResultName"]);
            model.strVerifyName = ObjectConvertClass.static_ext_string(dr["strVerifyName"]);
            model.strImagePath = ObjectConvertClass.static_ext_string(dr["strImagePath"]);
            model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
            //人员
            model.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanGUID"]);
            model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
            model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
            //车次
            model.strTrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
            model.strTrainNumber = ObjectConvertClass.static_ext_string(dr["strTrainNumber"]);
            model.strTrainTypeName = ObjectConvertClass.static_ext_string(dr["strTrainTypeName"]);
            //出勤点
            model.strPlaceID = ObjectConvertClass.static_ext_string(dr["strPlaceID"]);
            model.strPlaceName = ObjectConvertClass.static_ext_string(dr["strPlaceName"]);
            model.strSiteGUID = ObjectConvertClass.static_ext_string(dr["strSiteGUID"]);
            model.strSiteName = ObjectConvertClass.static_ext_string(dr["strSiteName"]);
            //车间
            model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
            model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
            //酒精度
            model.dwAlcoholicity = ObjectConvertClass.static_ext_int(dr["dwAlcoholicity"]);

            model.strWorkID = ObjectConvertClass.static_ext_string(dr["strWorkID"]);
            model.strAreaGUID = ObjectConvertClass.static_ext_string(dr["strAreaGUID"]);
            model.strDutyGUID = ObjectConvertClass.static_ext_string(dr["strDutyGUID"]);
            model.strDutyName = ObjectConvertClass.static_ext_string(dr["strDutyName"]);
            model.strDutyNumber = ObjectConvertClass.static_ext_string(dr["strDutyNumber"]);                    
            if (!(dr["bLocalAreaTrainman"] is DBNull))
                model.bLocalAreaTrainman = Convert.ToBoolean(dr["bLocalAreaTrainman"]);
            model.strSiteNumber = ObjectConvertClass.static_ext_string(dr["strSiteNumber"]);


            model.strDepartmentID = ObjectConvertClass.static_ext_string(dr["strDepartmentID"]);
            model.strDepartmentName = ObjectConvertClass.static_ext_string(dr["strDepartmentName"]);
            model.nCadreTypeID = ObjectConvertClass.static_ext_string(dr["nCadreTypeID"]);
            model.strCadreTypeName = ObjectConvertClass.static_ext_string(dr["strCadreTypeName"]);

        }
        #endregion

        #region 上传测酒记录
        /// <summary>
        /// 添加数据
        /// </summary>
        public bool AddDrinkInfo(DrinkInfo model)
        {
          
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Drink_Information");
            strSql.Append("( strGUID,bLocalAreaTrainman,strTrainmanGUID,strTrainmanNumber,strTrainmanName ,dwAlcoholicity ,nDrinkResult,dtCreateTime ,");
            strSql.Append(" strTrainNo , strTrainNumber , strTrainTypeName , strPlaceID , strPlaceName, strSiteGUID , strSiteNumber,strSiteName ,");
            strSql.Append(" strWorkShopGUID , strWorkShopName ,strAreaGUID,strDutyNumber,strDutyName,nVerifyID,strWorkID,nWorkTypeID,strImagePath,strDepartmentID,strDepartmentName,nCadreTypeID,strCadreTypeName )");
            strSql.Append("values (newid(),@bLocalAreaTrainman,@strTrainmanGUID,@strTrainmanNumber,@strTrainmanName,@dwAlcoholicity,@nDrinkResult,@dtCreateTime,");
            strSql.Append(" @strTrainNo , @strTrainNumber , @strTrainTypeName , @strPlaceID , @strPlaceName, @strSiteGUID ,@strSiteNumber, @strSiteName ,");
            strSql.Append(" @strWorkShopGUID , @strWorkShopName ,@strAreaGUID,@strDutyNumber,@strDutyName,@nVerifyID,@strWorkID,@nWorkTypeID,@strImagePath,@strDepartmentID,@strDepartmentName,@nCadreTypeID,@strCadreTypeName)");
            SqlParameter[] parameters = {
                  new SqlParameter("@bLocalAreaTrainman", model.bLocalAreaTrainman),
                  new SqlParameter("@strTrainmanGUID", model.strTrainmanGUID),
                  new SqlParameter("@strTrainmanNumber", model.strTrainmanNumber),
                  new SqlParameter("@strTrainmanName", model.strTrainmanName),
                  new SqlParameter("@dwAlcoholicity", model.dwAlcoholicity.ToString()),
                  new SqlParameter("@nDrinkResult", model.nDrinkResult),
                  new SqlParameter("@dtCreateTime", model.dtCreateTime),
                  new SqlParameter("@strTrainNo", model.strTrainNo),
                  new SqlParameter("@strTrainNumber", model.strTrainNumber),
                  new SqlParameter("@strTrainTypeName", model.strTrainTypeName),
                  new SqlParameter("@strPlaceID", model.strPlaceID),
                  new SqlParameter("@strPlaceName", model.strPlaceName),
                  new SqlParameter("@strSiteGUID", model.strSiteGUID),
                  new SqlParameter("@strSiteNumber", model.strSiteNumber),
                  new SqlParameter("@strSiteName", model.strSiteName),
                  new SqlParameter("@strWorkShopGUID", model.strWorkShopGUID),
                  new SqlParameter("@strWorkShopName", model.strWorkShopName),
                  new SqlParameter("@strAreaGUID", model.strAreaGUID),
                  new SqlParameter("@strDutyNumber", model.strDutyNumber),
                  new SqlParameter("@strDutyName", model.strDutyName),
                  new SqlParameter("@nVerifyID", model.nVerifyID),
                  new SqlParameter("@strWorkID", model.strWorkID),
                  new SqlParameter("@nWorkTypeID", model.nWorkTypeID),
                  new SqlParameter("@strImagePath", model.strImagePath),
                  new SqlParameter("strDepartmentID",model.strDepartmentID),
                  new SqlParameter("strDepartmentName",model.strDepartmentName),
                  new SqlParameter("nCadreTypeID",model.nCadreTypeID),
                  new SqlParameter("strCadreTypeName",model.strCadreTypeName)

                  //new SqlParameter("@strWorkTypeName", model.strWorkTypeName),
                  //new SqlParameter("@strDrinkResultName", model.strDrinkResultName),
                  //new SqlParameter("@strVerifyName", model.strVerifyName),
                  //new SqlParameter("@strDutyGUID", model.strDutyGUID),
                  };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);

            #region 插入消息记录
            MDDrink4Msg MDMsg = new MDDrink4Msg();
            MDMsg = this.getModel(model);
            string strMsg = AttentionMsg.ReturnStrJson(MDMsg);
            AttentionMsg msg = new AttentionMsg();
            msg.msgType = MSGTYPE.MSG_DRINK;//测酒消息类别
            msg.param = strMsg;
            msg.CreatMsg();
            #endregion

            return true;
        }

        public bool ExistDrinkInfo(string strTrainmanGUID,DateTime DrinkTime, out string DrinkGUID)
        {
            string sqlText = "select strGUID from tab_drink_information where strTrainmanGUID=@strTrainmanGUID and dtCreateTime=@dtCreateTime";
            SqlParameter[] sqlParamsDrink = new SqlParameter[] { 
                    new SqlParameter("strTrainmanGUID",strTrainmanGUID),
                    new SqlParameter("dtCreateTime",DrinkTime)
                };
            DrinkGUID = "";
            object objDrink = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsDrink);
            if ((objDrink != null) && (!DBNull.Value.Equals(objDrink)))
            {
                DrinkGUID = objDrink.ToString();
                return true;
            }
            return false;
        }
        #endregion



        #region 消息接收



        public MDDrink4Msg getModel(DrinkInfo model)
        {
            MDDrink4Msg msg = new MDDrink4Msg();

            msg.nLocalAreaTrainman = model.bLocalAreaTrainman == true ? 0 : 1;
            msg.trainmanID = model.strTrainmanGUID;
            msg.strTrainmanNumber = model.strTrainmanNumber;
            msg.strTrainmanName = model.strTrainmanName;
            msg.dwAlcoholicity = model.dwAlcoholicity.ToString();
            msg.drinkResult = model.nDrinkResult.ToString();
            msg.createTime = model.dtCreateTime.ToString();
            msg.strTrainNo = model.strTrainNo;
            msg.strTrainNumber = model.strTrainNumber;
            msg.strTrainTypeName = model.strTrainTypeName;
            msg.strPlaceID = model.strPlaceID;
            msg.strPlaceName = model.strPlaceName;
            msg.strSiteGUID = model.strSiteGUID;
            msg.strSiteName = model.strSiteName;
            msg.strWorkShopGUID = model.strWorkShopGUID;
            msg.strWorkShopName = model.strWorkShopName;
            msg.strAreaGUID = model.strAreaGUID;
            msg.verifyID = model.nVerifyID;
            msg.strWorkID = model.strWorkID;
            msg.workTypeID = model.nWorkTypeID;
            msg.imagePath = model.strImagePath;
            msg.strDepartmentID = model.strDepartmentID;
            msg.strDepartmentName = model.strDepartmentName;
            msg.nCadreTypeID = model.nCadreTypeID;
            msg.strCadreTypeName = model.strCadreTypeName;
            msg.strGuid = model.strGUID;
            msg.oPlaceId = "";
            msg.dutyUserID = model.strDutyGUID;
            msg.nWorkTypeID = model.nWorkTypeID;
            msg.msgType = MSGTYPE.MSG_DRINK;
            return msg;
        
        
        }



        public class MDDrink4Msg : MsgType
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
            public int nWorkTypeID;
            public string strAreaGUID;
            public int nLocalAreaTrainman;

            //干部相关（部门名称（id），职位名称（id）)
            public string strDepartmentID = "";//部门id（车间DUID）
            public string strDepartmentName = "";//部门名称
            public string nCadreTypeID = "";//职位id
            public string strCadreTypeName = "";//职位名称
            public int verifyID;//验证方式
            public string oPlaceId;//出勤点id
            public string dutyUserID;//用户id
            public string strWorkID;//出退勤id
        }
        #endregion





        #region 获取测酒明细
        /// <summary>
        /// 获得一个实体对象
        /// </summary>
        public DrinkInfo GetDrinkInfo(string strGUID, out Boolean bExist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * FROM VIEW_Drink_Query where strGUID='" + strGUID + "'");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            DrinkInfo _Drink_Query = null;
            if (dt.Rows.Count > 0)
            {
                bExist = true;
                _Drink_Query = new DrinkInfo();
                DrinkInfoDataRowToModel(_Drink_Query, dt.Rows[0]);
            }
            else
            {
                bExist = false;
            }
            return _Drink_Query;
        }
        private void DrinkInfoDataRowToModel(DrinkInfo model, DataRow dr)
        {
            model.strGUID = ObjectConvertClass.static_ext_string(dr["strGUID"]);
            model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
            model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
            model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
            model.nDrinkResult = ObjectConvertClass.static_ext_int(dr["nDrinkResult"]);
            model.strDrinkResultName = ObjectConvertClass.static_ext_string(dr["strDrinkResultName"]);
            model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);
            model.nVerifyID = ObjectConvertClass.static_ext_int(dr["nVerifyID"]);
            model.strVerifyName = ObjectConvertClass.static_ext_string(dr["strVerifyName"]);
            model.nWorkTypeID = ObjectConvertClass.static_ext_int(dr["nWorkTypeID"]);
            model.strWorkTypeName = ObjectConvertClass.static_ext_string(dr["strWorkTypeName"]);
            model.strImagePath = ObjectConvertClass.static_ext_string(dr["strImagePath"]);

            model.strDepartmentID = ObjectConvertClass.static_ext_string(dr["strDepartmentID"]);
            model.strDepartmentName = ObjectConvertClass.static_ext_string(dr["strDepartmentName"]);
            model.nCadreTypeID = ObjectConvertClass.static_ext_string(dr["nCadreTypeID"]);
            model.strCadreTypeName = ObjectConvertClass.static_ext_string(dr["strCadreTypeName"]);

        }
        #endregion
        
        #region 根据车次和客户端获取测酒记录
        public DrinkInfoArray GetTrainNoDrinkInfo(DateTime dtStartTime, string strTrainNo, string strPlaceID, int ncount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + ncount + " strTrainmanNumber,strTrainmanName,strTrainmanGUID, dwAlcoholicity,nDrinkResult,dtCreateTime ");
            strSql.Append(" from TAB_Drink_Information WHERE dtCreateTime in (select max(dtCreateTime) from TAB_Drink_Information");
            strSql.Append(" where dtcreatetime >= @dtStartTime and strTrainNo = @strTrainNo ");
            if (!string.IsNullOrEmpty(strPlaceID))
                strSql.Append(" and strPlaceID=@strPlaceID");
            strSql.Append(" group by strTrainmanNumber)");
            SqlParameter[] sqlParams ={
                      new SqlParameter("dtStartTime",dtStartTime),
                      new SqlParameter("strTrainNo",strTrainNo),
                      new SqlParameter("strPlaceID",strPlaceID)
                                      };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            DrinkInfoArray list = new DrinkInfoArray();
            foreach (DataRow dr in dt.Rows)
            {
                DrinkInfo _Drink_Query = new DrinkInfo();
                _Drink_Query.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanGUID"]);
                _Drink_Query.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
                _Drink_Query.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
                _Drink_Query.dwAlcoholicity = ObjectConvertClass.static_ext_int(dr["dwAlcoholicity"]);
                _Drink_Query.nDrinkResult = ObjectConvertClass.static_ext_int(dr["nDrinkResult"]);
                _Drink_Query.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);
                list.Add(_Drink_Query);
            }
            return list;
        }
        #endregion

        #region 获取没有出勤计划的测酒记录
        public DrinkInfoArray QueryNoPlanDrink(DateTime dtBeginTime, DateTime dtEndTime, string TrainmanNumber, int DrinkTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Drink_Query where dtCreateTime >= @dtBeginTime and dtCreateTime <= @dtEndTime ");
            strSql.Append(!string.IsNullOrEmpty(TrainmanNumber) ? " and strTrainmanNumber = @strTrainmanNumber" : "");
            strSql.Append(DrinkTypeID > -1 ? " and nWorkTypeID = @nWorkTypeID" : "");
            strSql.Append(" and  (strWorkID = '' or strWorkID is Null ) order by dtCreateTime desc");
            SqlParameter[] sqlParams ={
                      new SqlParameter("dtBeginTime",dtBeginTime),
                      new SqlParameter("dtEndTime",dtEndTime),
                      new SqlParameter("strTrainmanNumber",TrainmanNumber),
                      new SqlParameter("nWorkTypeID",DrinkTypeID)
                                      };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            DrinkInfoArray list = new DrinkInfoArray();
            foreach (DataRow dr in dt.Rows)
            {
                DrinkInfo _Drink_Query = new DrinkInfo();
                DrinkDataRowToModel(_Drink_Query, dr);
                list.Add(_Drink_Query);
            }
            return list;
        }
        #endregion

        #region 据客户端获取从某个时间开始的某个人的最后一条测酒记录
        public DrinkInfo GetTMLastDrinkInfo(string strSiteNumber, string strTrainmanNumber, DateTime dtStartTime, out Boolean bExist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 strTrainmanGUID, strTrainmanNumber,strTrainmanName,dwAlcoholicity,nDrinkResult,dtCreateTime");
            strSql.Append(" from TAB_Drink_Information  where dtcreatetime >= @dtStartTime and strTrainmanNumber = @strTrainmanNumber and strSiteNumber = @strSiteNumber");
            strSql.Append(" order by dtcreateTime desc");
            SqlParameter[] sqlParams ={
                      new SqlParameter("dtStartTime",dtStartTime),
                      new SqlParameter("strTrainmanNumber",strTrainmanNumber),
                      new SqlParameter("strSiteNumber",strSiteNumber),
                                      };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            DrinkInfo _Drink_Query = null;
            if (dt.Rows.Count > 0)
            {
                bExist = true;
                _Drink_Query = new DrinkInfo();
                _Drink_Query.strTrainmanGUID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanGUID"]);
                _Drink_Query.strTrainmanNumber = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanNumber"]);
                _Drink_Query.strTrainmanName = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanName"]);
                _Drink_Query.dwAlcoholicity = ObjectConvertClass.static_ext_int(dt.Rows[0]["dwAlcoholicity"]);
                _Drink_Query.nDrinkResult = ObjectConvertClass.static_ext_int(dt.Rows[0]["nDrinkResult"]);
                _Drink_Query.dtCreateTime = ObjectConvertClass.static_ext_date(dt.Rows[0]["dtCreateTime"]);
            }
            else
            {
                bExist = false;
            }
            return _Drink_Query;
        }
        #endregion

        #region 获取测酒记录
        public DrinkInfo GetTrainmanDrinkInfo(string strTrainmanGUID, string strTrainPlanGUID, int WorkType, out Boolean bExist)
        {
            StringBuilder strSql = new StringBuilder();
            switch (WorkType)
            {
                case 2://出勤
                    strSql.Append("select * from TAB_Drink_Information where strWorkID = (");
                    strSql.Append("select top 1 strBeginWorkGUID from TAB_Plan_BeginWork where ");
                    strSql.Append(" strTrainPlanGUID = @strTrainPlanGUID and strTrainmanGUID = @strTrainmanGUID) ORDER BY dtCreateTime DESC");
                    break;
                case 3://退勤
                    strSql.Append("select * from TAB_Drink_Information where strWorkID = (");
                    strSql.Append("select top 1 strEndWorkGUID from TAB_Plan_EndWork where ");
                    strSql.Append(" strTrainPlanGUID = @strTrainPlanGUID and strTrainmanGUID = @strTrainmanGUID) ORDER BY dtCreateTime DESC");
                    break;
            }
            SqlParameter[] sqlParams ={
                      new SqlParameter("strTrainPlanGUID",strTrainPlanGUID),
                      new SqlParameter("strTrainmanGUID",strTrainmanGUID),
                                      };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParams).Tables[0];
            DrinkInfo _Drink_Query = null;
            if (dt.Rows.Count > 0)
            {
                bExist = true;
                _Drink_Query = new DrinkInfo();
                _Drink_Query.nDrinkResult = ObjectConvertClass.static_ext_int(dt.Rows[0]["nDrinkResult"]);
                _Drink_Query.dtCreateTime = ObjectConvertClass.static_ext_date(dt.Rows[0]["dtCreateTime"]);
                _Drink_Query.strImagePath = ObjectConvertClass.static_ext_string(dt.Rows[0]["strImagePath"]);
                //DrinkDataRowToModel(_Drink_Query, dt.Rows[0]);
            }
            else
            {
                bExist = false;
            }
            return _Drink_Query;
        }
        #endregion
    }
    #endregion
}
