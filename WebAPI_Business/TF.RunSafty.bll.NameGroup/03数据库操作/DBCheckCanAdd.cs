using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TF.RunSafty.NamePlate.MD;
using TF.RunSafty.NamePlate.PS;
using ThinkFreely.DBUtility;
using TF.CommonUtility;

namespace TF.RunSafty.NamePlate.DB
{
    public class DBCheckCanAdd
    {
        /// <summary>
        /// 判断所有的参数是否为空
        /// </summary>
        /// <param name="InParams"></param>
        /// <param name="checkBrief"></param>
        /// <returns></returns>
        public static bool checkIsNull(TF.RunSafty.NamePlate.LCGroup.InCheckCanAdd InParams, ref string checkBrief)
        {
            if (string.IsNullOrEmpty(InParams.TMNumber))
            {
                checkBrief = "人员工号不能为空！";
                return false;
            }
            if (string.IsNullOrEmpty(InParams.AimWSID))
            {
                checkBrief = "目标车间不能为空！";
                return false;
            }
            if (string.IsNullOrEmpty(InParams.AimGroupID))
            {
                checkBrief = "目标机组不能为空！";
                return false;
            }
            if (string.IsNullOrEmpty(InParams.AimTMJLID))
            {
                checkBrief = "目标交路不能为空！";
                return false;
            }
            checkBrief = "";
            return true;
        }




        /// <summary>
        /// 判断目的车间和人员所在车间是否一致
        /// </summary>
        /// <param name="TMNumber"></param>
        /// <param name="AimWSID"></param>
        /// <param name="checkBrief"></param>
        /// <returns></returns>
        public static bool CheckTMInWs(string TMNumber, string AimWSID, ref string checkBrief, ref string strTMGUID)
        {
            string strSql = @"Select top 1 * from TAB_Org_Trainman where  strTrainmanNumber = @strTrainmanNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                 new SqlParameter("strTrainmanNumber",TMNumber)
            };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            string strWorkShopGUID = "";
            if (dt.Rows.Count > 0)
            {
                strWorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();
                strTMGUID = dt.Rows[0]["strTrainmanGUID"].ToString();
            }
            else
            {
                strTMGUID = "";
            }

            if (AimWSID != strWorkShopGUID)
            {
                checkBrief = "目标车间不是该乘务员所在车间，不能添加！";
                return false;
            }
            return true;

        }


        /// <summary>
        /// 判断是否处于请假状态
        /// </summary>
        /// <param name="TMNumber"></param>
        /// <param name="checkBrief"></param>
        /// <returns></returns>
        public static bool CheckIsLeave(string TMNumber, ref string checkBrief)
        {
            string strSql = @"Select top 1 * from TAB_Org_Trainman where  strTrainmanNumber = @strTrainmanNumber";
            SqlParameter[] sqlParams = new SqlParameter[]{
                 new SqlParameter("strTrainmanNumber",TMNumber)
            };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count == 0) return false;
            string strTrainmanName = dt.Rows[0]["strTrainmanName"].ToString();
            int nState = 7;  //tsnil
            if (!TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[0]["nTrainmanState"], ref nState))
            {
                nState = 7;
            }
            if (nState == 0)
            {
                checkBrief = "司机：" + strTrainmanName + " 处于请假状态，不能添加";
                return false;
            }
            checkBrief = "";
            return true;

        }

        /// <summary>
        /// 判断是否在计划中
        /// </summary>
        /// <param name="GroupGUID"></param>
        /// <param name="checkBrief"></param>
        /// <returns></returns>
        public static bool CheckIsInPlan(string GroupGUID, ref string checkBrief)
        {

            Group G = new Group();
            string strSql = "select top 1 * from VIEW_Nameplate_Group   where  strGroupGUID=@strGroupGUID";
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("strGroupGUID",GroupGUID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                PSNameBoard.GroupFromDB(G, dt.Rows[0]);
                if (G.groupState == 3)
                {
                    checkBrief = string.Format("机组已被安排了计划，不能添加");
                    return false;
                }
                if (G.groupState == 6)
                {
                    checkBrief = string.Format("机组已出勤，不能添加");
                    return false;
                }
                return true;
            }
            return true;
        }

        /// <summary>
        /// 检测人员所在机组状态是否是已出勤状态
        /// </summary>
        /// <param name="TMNumber"></param>
        /// <param name="checkBrief"></param>
        /// <returns></returns>
        public static bool CheckOldGroup(string TMNumber, ref string checkBrief, string strTMGUID)
        {
            //获取待加人员原来所属的机组信息
            string strSql = @"select top 1 *,pt.nPlanState from TAB_Nameplate_Group ng left join TAB_Plan_Train pt on ng.strTrainPlanGUID=pt.strTrainPlanGUID
                     where  strTrainmanGUID1 = @strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID";
            SqlParameter[] sqlParamsOldGroup = new SqlParameter[]{
                    new SqlParameter("strTrainmanGUID",strTMGUID)
                };
           
            DataTable dtOldGroup = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsOldGroup).Tables[0];
            if (dtOldGroup.Rows.Count > 0)
            {
                if (dtOldGroup.Rows[0]["nPlanState"].ToString() == "7")
                {
                    checkBrief = string.Format("[{0}]所在的机组正在值乘中，不能添加", TMNumber);
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 判断原有机组是否与目标机组是否在同一个交路   判断是否是从一个机组移动到另一个机组
        /// </summary>
        /// <param name="TMNumber"></param>
        /// <param name="checkBrief"></param>
        /// <returns></returns>
        public static bool CheckTMJL(string TMNumber, string AimTMJLID, ref string checkBrief, string strTMGUID)
        {
            //获取待加人员原来所属的机组信息
            string strSql = @"select top 1 * from VIEW_Nameplate_Group 
                      where  strTrainmanGUID1 = @strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID";
            SqlParameter[] sqlParamsOldGroup = new SqlParameter[]{
                    new SqlParameter("strTrainmanGUID",strTMGUID)
                };
           
            DataTable dtOldGroup = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsOldGroup).Tables[0];
            if (dtOldGroup.Rows.Count > 0)
            {
                if (dtOldGroup.Rows[0]["strTrainmanJiaoluGUID"].ToString() != AimTMJLID)
                {
                    checkBrief = string.Format("[{0}]处于[{1}]交路名牌中，移除后才能添加！", TMNumber, dtOldGroup.Rows[0]["strTrainmanJiaoluName"].ToString());
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断是否位于其他机组内
        /// </summary>
        /// <param name="AimGroupID"></param>
        /// <param name="TMNumber"></param>
        /// <param name="checkBrief"></param>
        /// <returns></returns>
        public static bool CheckIsInOtherGroup(string AimGroupID, string TMNumber, ref string checkBrief, string strTMGUID)
        {
            //获取待加人员原来所属的机组信息
            string strSql = @"select top 1 * from VIEW_Nameplate_Group 
                     where  strTrainmanGUID1 = @strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID";
            SqlParameter[] sqlParamsOldGroup = new SqlParameter[]{
                    new SqlParameter("strTrainmanGUID",strTMGUID)
                };
           
            DataTable dtOldGroup = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsOldGroup).Tables[0];
            if (dtOldGroup.Rows.Count > 0)
            {
                Group G = new Group();
                PSNameBoard.GroupFromDB(G, dtOldGroup.Rows[0]);
                if (G.groupID != AimGroupID)
                {
                    checkBrief = string.Format("该乘务员位于其他机组中，司机：[{0}]{1},副司机：[{2}]{3}", G.trainman1.trainmanNumber, G.trainman1.trainmanName, G.trainman2.trainmanNumber, G.trainman2.trainmanName);
                    return false;
                }
            }
            return true;
        }


        
    }
}
