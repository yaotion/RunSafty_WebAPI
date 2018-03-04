using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using TF.RunSafty.Plan;
using ThinkFreely.DBUtility;
using System.Data;
using ThinkFreely.RunSafty;
using TF.RunSafty.DrinkLogic;
using TF.Runsafty.Plan.MD;

namespace TF.Runsafty.Plan.DB
{
    public class DBDrink
    {

        /// <summary>
        /// 提交测酒记录
        /// </summary>
        /// <param name="?"></param>
        public void SubmitDrink(MDDrink MDR, SqlTransaction trans)
        {
            #region 添加测酒记录

            MDR.strPlaceName = MDR.strPlaceName != "" ? MDR.strPlaceName : getPlaceName(MDR.strPlaceID);
            SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("strTrainmanGUID", MDR.trainmanID),
                    new SqlParameter("dtCreateTime", MDR.createTime),
                    new SqlParameter("nVerifyID", MDR.verifyID),
                    new SqlParameter("strPlaceID", MDR.oPlaceId.ToString()),
                    new SqlParameter("strGUID", MDR.strGuid),
                    new SqlParameter("nDrinkResult", MDR.drinkResult),
                    new SqlParameter("strAreaGUID", MDR.strAreaGUID),
                    new SqlParameter("strDutyGUID",MDR.dutyUserID),
                    new SqlParameter("bLocalAreaTrainman", MDR.nLocalAreaTrainman),
                    new SqlParameter("strTrainmanName", MDR.strTrainmanName),
                    new SqlParameter("strTrainmanNumber", MDR.strTrainmanNumber),
                    new SqlParameter("strTrainNo", MDR.strTrainNo),
                    new SqlParameter("strTrainNumber", MDR.strTrainNumber),
                    new SqlParameter("strTrainTypeName", MDR.strTrainTypeName),
                    new SqlParameter("strWorkShopGUID", MDR.strWorkShopGUID),
                    new SqlParameter("strWorkShopName", MDR.strWorkShopName),
                    new SqlParameter("strPlaceIDs", MDR.strPlaceID),
                    new SqlParameter("strPlaceName", MDR.strPlaceName),
                    new SqlParameter("strSiteGUID", MDR.strSiteGUID),
                    new SqlParameter("strSiteName", MDR.strSiteName),
                    new SqlParameter("dwAlcoholicity", MDR.dwAlcoholicity),
                    new SqlParameter("strWorkID", MDR.strWorkID),
                    new SqlParameter("nWorkTypeID", MDR.nWorkTypeID),
                    new SqlParameter("strImagePath", MDR.imagePath),
                    new SqlParameter("strDepartmentID",MDR.strDepartmentID),
                    new SqlParameter("strDepartmentName",MDR.strDepartmentName),
                    new SqlParameter("nCadreTypeID",MDR.nCadreTypeID),
                    new SqlParameter("strCadreTypeName",MDR.strCadreTypeName)
                };
            string sqlText = @"insert into TAB_Drink_Information (strGUID,strTrainmanGUID,nDrinkResult,dtCreateTime,strAreaGUID,strDutyGUID,nVerifyID,strWorkID,nWorkTypeID,
strImagePath,strTrainmanName,strTrainmanNumber,strTrainNo,strTrainNumber,strTrainTypeName,strWorkShopGUID,strWorkShopName,strPlaceID,
strPlaceName,strSiteGUID,strSiteName,dwAlcoholicity,bLocalAreaTrainman,strDepartmentID,strDepartmentName,nCadreTypeID,strCadreTypeName) 
                 values (@strGUID,@strTrainmanGUID,@nDrinkResult,@dtCreateTime,
                 @strAreaGUID,@strDutyGUID,@nVerifyID,@strWorkID,@nWorkTypeID,@strImagePath,@strTrainmanName,@strTrainmanNumber,
@strTrainNo,@strTrainNumber,@strTrainTypeName,@strWorkShopGUID,@strWorkShopName,@strPlaceIDs,@strPlaceName,@strSiteGUID,@strSiteName,
@dwAlcoholicity,@bLocalAreaTrainman,@strDepartmentID,@strDepartmentName,@nCadreTypeID,@strCadreTypeName)";
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sqlText, sqlParams);
            #endregion

            #region 插入消息记录
            MDR.msgType = MSGTYPE.MSG_DRINK;
            MDR.workTypeID = MDR.nWorkTypeID;
            string strMsg = AttentionMsg.ReturnStrJson(MDR);
            AttentionMsg msg = new AttentionMsg();
            msg.msgType = MSGTYPE.MSG_DRINK;//测酒消息类别
            msg.param = strMsg;
            msg.CreatMsg(trans);
            #endregion
        }


        //根据出勤点的编号  获取

        public string getPlaceName(string strPlaceID)
        {
            if (string.IsNullOrEmpty(strPlaceID))
            {
                return "未知出勤点";
            }
            string sqlCommandText = string.Format(@"select * from  TAB_Base_DutyPlace   where  strPlaceID=@strPlaceID ");
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strPlaceID",strPlaceID) 
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlCommandText, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["strPlaceName"].ToString();
            }
            return "未知出勤点";
        }
    }
}
