using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TF.RunSafty.Model;
using TF.RunSafty.Model.InterfaceModel;
using ThinkFreely.DBUtility;
using TF.Api.Utilities;
namespace TF.RunSafty.BLL
{
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
        public string trainmanID = "";
        public string drinkResult = "0";
        public int workTypeID = 0;
        public string createTime = "";
        public string imagePath = "";

        public string strSiteNumber = "";
        public string strAreaGUID = "";
        public string bLocalAreaTrainman = "";
        public string strDutyNumber = "";
        public string strDutyName = "";


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


    }

    public class DutyInfo
    {
        public string dutyUserID = "";
    }

    public partial class RCEndWork
    {

        private bool HasEndWorkPlan(string strSiteGUID, string strTrainmanGUID)
        {
            string strWhere = string.Format(@" strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = '{0}') and 
nPlanState = {1} AND (strTrainmanGUID1 = '{2}' or strTrainmanGUID2 = '{2}'
or strTrainmanGUID3 = '{2}' or strTrainmanGUID4 = '{2}') order by dtStartTime desc", strSiteGUID, (int)TRsPlanState.psBeginWork, strTrainmanGUID);
            TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork bllEndWork = new TF.RunSafty.BLL.Plan.VIEW_Plan_EndWork();
            return bllEndWork.GetModelList(strWhere).Count > 0;
        }

        public bool Endwork(string siteID, EndWorkData endworkData, DrinkData drinkdata, DutyInfo dutyUser)
        {

            //判断是否有出勤计划
            if (!HasEndWorkPlan(siteID, endworkData.trainmanID))
            {
                return false;
            }
            SqlParameter[] sqlParamsFind = new SqlParameter[]{ 
                    new SqlParameter("strTrainPlanGUID",endworkData.planID),
                    new SqlParameter("strTrainmanGUID",endworkData.trainmanID)
             };

            string sqlText = "select strGroupGUID from TAB_Plan_Trainman where strTrainPlanGUID = @strTrainPlanGUID";
            object obj = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsFind));
            string strGroupGUID = "";
            if (obj != null)
            {
                strGroupGUID = Convert.ToString(obj);
            }

            sqlText = @"select nJiaoluType,strTrainmanJiaoluGUID,nTrainmanRunType from VIEW_Nameplate_TrainmanInJiaolu_All 
             where strTrainmanGUID = @strTrainmanGUID";
            DataTable dtJiaolu = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsFind).Tables[0];
            if (dtJiaolu.Rows.Count == 0)
            {
                throw new Exception("该机组没有处于任何人员交路中");
            }
            string trainmanJiaoluGUID = dtJiaolu.Rows[0]["strTrainmanJiaoluGUID"].ToString();
            //int nTrainmanJiaoluType = int.Parse(dtJiaolu.Rows[0]["nJiaoluType"].ToString());
            int nTrainmanJiaoluType, nRunType;
            int.TryParse(dtJiaolu.Rows[0]["nJiaoluType"].ToString(), out nTrainmanJiaoluType);
            int.TryParse(dtJiaolu.Rows[0]["nTrainmanRunType"].ToString(), out nRunType);
            //int nRunType = int.Parse(dtJiaolu.Rows[0]["nTrainmanRunType"].ToString());

            sqlText = "select top 1 strEndWorkGUID from TAB_Plan_EndWork where strTrainPlanGUID = @strTrainPlanGUID and strTrainmanGUID = @strTrainmanGUID";

            obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsFind);

            string endworkID = Guid.NewGuid().ToString();
            if (obj != null)
            {
                endworkID = Convert.ToString(obj);
            }



            #region '添加或修改退勤记录'
            SqlParameter[] sqlParams = new SqlParameter[]{ 
                new SqlParameter("strEndWorkGUID",endworkID),
                new SqlParameter("strTrainPlanGUID",endworkData.planID),
                new SqlParameter("strTrainmanGUID",endworkData.trainmanID),
                new SqlParameter("dtCreateTime",drinkdata.createTime),
                new SqlParameter("nVerifyID",endworkData.verifyID),
                new SqlParameter("strStationGUID",endworkData.stationID),
                new SqlParameter("strRemark",endworkData.remark),
                new SqlParameter("strGroupGUID",strGroupGUID),
                new SqlParameter("strPlaceID",endworkData.placeID),
                new SqlParameter("nPlanState",8),
                new SqlParameter("NullTrainPlanGUID",""),

                new SqlParameter("strGUID",Guid.NewGuid().ToString()),
                new SqlParameter("strTrainmanJiaoluGUID",trainmanJiaoluGUID),
                new SqlParameter("nDrinkResult",drinkdata.drinkResult),                
                new SqlParameter("strAreaGUID",drinkdata.strAreaGUID),
                new SqlParameter("strDutyGUID",dutyUser.dutyUserID),
                
                new SqlParameter("strWorkID",endworkID),
                new SqlParameter("nWorkTypeID",3),
                new SqlParameter("strImagePath",drinkdata.imagePath),
                new SqlParameter("dtLastArriveTime",endworkData.arriveTime),
                new SqlParameter("dtLastEndWorkTime",endworkData.lastEndWorkTime),
                new SqlParameter("nTrainmanState",2),

                new SqlParameter("strSiteNumber",drinkdata.strSiteNumber),
                new SqlParameter("strAreaGUID1",drinkdata.strAreaGUID),
                new SqlParameter("bLocalAreaTrainman",drinkdata.bLocalAreaTrainman),
                new SqlParameter("strDutyNumber",drinkdata.strDutyNumber),
                new SqlParameter("strDutyName",drinkdata.strDutyName),

                new SqlParameter("strTrainmanName", drinkdata.strTrainmanName),
                new SqlParameter("strTrainmanNumber", drinkdata.strTrainmanNumber),

                new SqlParameter("strTrainNo", drinkdata.strTrainNo),
                new SqlParameter("strTrainNumber", drinkdata.strTrainNumber),
                new SqlParameter("strTrainTypeName", drinkdata.strTrainTypeName),

                new SqlParameter("strWorkShopGUID", drinkdata.strWorkShopGUID),
                new SqlParameter("strWorkShopName", drinkdata.strWorkShopName),
                new SqlParameter("strPlaceIDs", drinkdata.strPlaceID),
                new SqlParameter("strPlaceName", drinkdata.strPlaceName),
                new SqlParameter("strSiteGUID", drinkdata.strSiteGUID),
                new SqlParameter("strSiteName", drinkdata.strSiteName),

                new SqlParameter("dwAlcoholicity", drinkdata.dwAlcoholicity),
                new SqlParameter("nWorkTypeIDs", drinkdata.nWorkTypeID),


            
            };
            if (obj == null)
            {
                sqlText = @"insert into TAB_Plan_EndWork(strEndWorkGUID,strTrainPlanGUID,strTrainmanGUID,dtCreateTime,nVerifyID,strStationGUID,strRemark) values (
                    @strEndWorkGUID,@strTrainPlanGUID,@strTrainmanGUID,@dtCreateTime,@nVerifyID,@strStationGUID,@strRemark)";

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
            }
            else
            {
                sqlText = "update TAB_Plan_EndWork set nVerifyID=@nVerifyID,strRemark=@strRemark where strEndWorkGUID=@strEndWorkGUID";
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
            }
            #endregion '添加或修改退勤记录'

            #region '添加或修改测酒记录'
            sqlText = "delete TAB_Drink_Information where strWorkID = @strWorkID and nWorkTypeID=@nWorkTypeID and strTrainmanGUID = @strTrainmanGUID";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);

            sqlText = @"insert into TAB_Drink_Information (strGUID,strTrainmanGUID, 
                 nDrinkResult,dtCreateTime,strAreaGUID,strDutyGUID,nVerifyID,strWorkID,nWorkTypeID,strImagePath,strSiteNumber,bLocalAreaTrainman,strDutyNumber,strDutyName,strTrainmanName,strTrainmanNumber,strTrainNo,strTrainNumber,strTrainTypeName,strWorkShopGUID,strWorkShopName,strPlaceID,strPlaceName,strSiteGUID,strSiteName,dwAlcoholicity) 
                 values (@strGUID,@strTrainmanGUID,@nDrinkResult,@dtCreateTime,                                  
                 @strAreaGUID,@strDutyGUID,@nVerifyID,@strWorkID,@nWorkTypeID,@strImagePath,@strSiteNumber,@bLocalAreaTrainman,@strDutyNumber,@strDutyName,@strTrainmanName,@strTrainmanNumber,@strTrainNo,@strTrainNumber,@strTrainTypeName,@strWorkShopGUID,@strWorkShopName,@strPlaceIDs,@strPlaceName,@strSiteGUID,@strSiteName,@dwAlcoholicity)";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
            #endregion

            #region '修改计划状态为已退勤'
            sqlText = @"update TAB_Plan_Train set nPlanState=@nPlanState where strTrainPlanGUID=@strTrainPlanGUID and  
                   (select count(*) from VIEW_Plan_EndWork where strTrainPlanGUID=TAB_Plan_Train.strTrainPlanGUID  and  
                   ((strTrainmanGUID1 is null) or (strTrainmanGUID1 = '') or not(dtTestTime1 is null)) and 
                   ((strTrainmanGUID2 is null) or (strTrainmanGUID2 = '') or not(dtTestTime2 is null))  and 
                   ((strTrainmanGUID3 is null) or (strTrainmanGUID3 = '') or not(dtTestTime3 is null))  and 
                   ((strTrainmanGUID4 is null) or (strTrainmanGUID4 = '') or not(dtTestTime4 is null))) > 0";

            if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams) > 0)
            {
                sqlText = "update TAB_Nameplate_Group set strTrainPlanGUID = @NullTrainPlanGUID where strGroupGUID = @strGroupGUID";
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);

                //翻牌
                //jltTogether
                if (nTrainmanJiaoluType == 4)
                {
                    string stTrainGUID = "";
                    sqlText = "select strTrainGUID from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strGroupGUID=@strGroupGUID";
               
                    object objTrainGUID = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
                    if ((objTrainGUID != null) && (!DBNull.Value.Equals(objTrainGUID)))
                    {
                        stTrainGUID = objTrainGUID.ToString();
                    }

                    ///将所有非自己的机组按照从1开始排序
                    sqlText = "select strGroupGUID from TAB_Nameplate_TrainmanJiaolu_OrderInTrain where strTrainGUID=@strTrainGUID and strGroupGUID <> @strGroupGUID order by nOrder";
                    SqlParameter[] sqlParamsSubs = new SqlParameter[]{
                            new SqlParameter("strTrainGUID",stTrainGUID),
                            new SqlParameter("strGroupGUID",strGroupGUID)
                        };
                    DataTable dtReOrder = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParamsSubs).Tables[0];
                    for (int i = 0; i < dtReOrder.Rows.Count; i++)
                    {
                        sqlText = "update TAB_Nameplate_TrainmanJiaolu_OrderInTrain set nOrder = @nOrder where strGroupGUID=@strGroupGUID";
                        SqlParameter[] sqlReOrder = new SqlParameter[] { 
                            new SqlParameter("nOrder",i+ 1),
                            new SqlParameter("strGroupGUID",dtReOrder.Rows[i]["strGroupGUID"].ToString())
                        };
                        SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlReOrder);
                    }
                    //将自己设置为最大的
                    sqlText = "update TAB_Nameplate_TrainmanJiaolu_OrderInTrain set nOrder = @nOrder where strGroupGUID=@strGroupGUID";
                    SqlParameter[] sqlReOrder2 = new SqlParameter[] { 
                            new SqlParameter("nOrder",dtReOrder.Rows.Count + 1),
                            new SqlParameter("strGroupGUID",strGroupGUID)
                        };
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlReOrder2);

                    sqlText =
                        @"update TAB_Nameplate_TrainmanJiaolu_OrderInTrain set dtLastArriveTime = @dtLastArriveTime where strGroupGUID = @strGroupGUID";

                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
                }

                //jltOrder
                if (nTrainmanJiaoluType == 3)
                {
                    sqlText = "update TAB_Nameplate_Group set strStationGUID = @strStationGUID,strPlaceID=@strPlaceID where strGroupGUID = @strGroupGUID";
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);

                    sqlText = "update TAB_Nameplate_TrainmanJiaolu_Order set dtLastArriveTime = @dtLastArriveTime where strGroupGUID = @strGroupGUID";
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
                }
            #endregion

            }
            //修改计划终到时间
            sqlText = @"update TAB_Plan_Train set dtLastArriveTime = @dtLastArriveTime where strTrainPlanGUID = @strTrainPlanGUID";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
            //修改人员最后一次退勤时间
            sqlText = "update TAB_Org_Trainman set dtLastEndWorkTime=@dtLastEndWorkTime,nTrainmanState=@nTrainmanState where strTrainmanGUID = @strTrainmanGUID";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams);
            return true;
        }
    }
}
