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
using ThinkFreely.DBUtility;
using System.Data.SqlClient;

namespace ThinkFreely.RunSafty
{
    /// <summary>
    ///SystemDic 的摘要说明
    /// </summary>
    public class SystemDic
    {
        public SystemDic()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 获取设备类型
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllEquipmentType()
        {
            string strSql = "select * from TAB_Dic_EquipmentType";
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 获取整备场所有股道
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllTrackInfo(int unitid, int apanageid)
        {
            string strSql = "select * from lsDicTrack where ApanageId=@ApanageId and UnitId=@UnitId and Status=1 order by id asc";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ApanageId",apanageid),
                                           new SqlParameter("UnitId",unitid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        /// <summary>
        /// 获取所有整备场
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAlllsDicDepartmentInfo(int UnitId)
        {
            string strSql = "select * from lsDicDepartmentInfo where UnitId=@UnitId and status=1 order by id asc";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("UnitId",UnitId)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql,sqlParams).Tables[0];
        }

        /// <summary>
        /// 获取所有机务段
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAlllsDicUnitInfo()
        {
            string strSql = "select * from lsDicUnitInfo where UnitType='段'order by id asc";
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 获取所有合格证检测项目
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllTAB_Dic_Qualified()
        {
            string strSql = "select * from TAB_Dic_Qualified order by nOrder";
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql).Tables[0];
        }
        /// <summary>
        /// 获取所有步骤类型
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllTAB_StepType()
        {
            string strSql = "select * from TAB_StepType";
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql).Tables[0];
        }
        /// <summary>
        /// 获取所有Module信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllLeaveMgrClass()
        {
            string strSql = "select * from TAB_LeaveMgr_LeaveClass";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        /// <summary>
        /// 获取所有Module信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllModuleInformations()
        {
            string strSql = "select * from TAB_Module_Information";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        public static DataTable GetAllUnitInformations()
        {
            string strSql = "select * from TAB_Unit_Information";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        public static DataTable GetAllUnitInformationsWithParent()
        {
            string strSql = @"select a.*,b.nParentID as topParent from TAB_Unit_Information a
left join TAB_Unit_Information b
on a.nParentID=b.nID
order by a.nID asc";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        public static DataTable GetAllModuleInformationsWithParent()
        {
            string strSql = @"select a.*,b.nParentID as topParent from TAB_Module_Information a
left join TAB_Module_Information b
on a.nParentID=b.nID
order by a.nID asc";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        /// <summary>
        /// 获取所有用户角色信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllRunType()
        {
            string strSql = "select nRunTypeID,strRunTypeName from TAB_System_RunType";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 获取所有用户角色信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllUserRole()
        {
            string strSql = "select * from TAB_UserRole";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        /// <summary>
        /// 获取所有机车型号信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllTrainType()
        {
            string strSql = "select * from TAB_System_TrainType";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        /// <summary>
        /// 获取所有车次头信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllTrainHead()
        {
            string strSql = "select * from TAB_Base_TrainHead";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 获取所有模块权限信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllJobLimit()
        {
            string strSql = "select * from TAB_System_Job_Limit order by nLimitID";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 获取所有的客户端岗位信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllJobs()
        {
            string strSql = "select * from TAB_System_Job order by nJobID";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql).Tables[0];
        }
        /// <summary>
        ///  获取所有的客户端岗位信息的键值形式
        /// </summary>
        /// <param name="DefaultName"></param>
        /// <returns></returns>
        public static DataTable GetAllJobsDic(string DefaultName)
        {
            DataTable dtResult = GetAllJobs();
            DataRow dr = dtResult.NewRow();
            dr["nJobID"] = "0";
            dr["strJobName"] = DefaultName;
            dtResult.Rows.InsertAt(dr, 0);
            return dtResult;
        }

        /// <summary>
        /// 获取司机职位信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllPost()
        {
            string strSql = "select * from TAB_System_Post";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        ///  获取所有的人员交路类型信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllJiaoluTypes()
        {
            string strSql = "select * from TAB_System_JiaoluType order by nJiaoluType";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        /// <summary>
        /// 获取所有的人员交路类型信息的键值形式
        /// </summary>
        /// <param name="DefaultName"></param>
        /// <returns></returns>
        public static DataTable GetAllJiaoluTypeDic(string DefaultName)
        {
            DataTable dtResult = GetAllJiaoluTypes();
            DataRow dr = dtResult.NewRow();
            dr["nJiaoluType"] = "0";
            dr["strJiaoluTypeName"] = DefaultName;
            dtResult.Rows.InsertAt(dr, 0);
            return dtResult;
        }

        /// <summary>
        /// 获取司机状态信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTrainmanState()
        {
            string strSql = "select * from TAB_System_TrainmanState ";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 获取所有运行状态的人员交路
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllRuningJiaoluTypes()
        {
            string strSql = "select * from TAB_System_JiaoluType where nJiaoluType > 1 order by nJiaoluType";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 获取计划状态
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPlanState()
        {
            string strSql = "select * from TAB_Plan_PlanState ";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 获取机车交路
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTrainJiaolu()
        {
            string strSql = "select * from TAB_Base_TrainJiaolu where nDeleteState=0 ";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 获取值乘类型
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTrainmanType()
        {
            string strSql = "select * from TAB_System_TrainmanType";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        } 

        /// <summary>
        /// 获取 所有运行状态的人员交路的键值形式
        /// </summary>
        /// <param name="DefaultName"></param>
        /// <returns></returns>
        public static DataTable GetAllRuningJiaoluTypeDic(string DefaultName)
        {
            DataTable dtResult = GetAllRuningJiaoluTypes();
            DataRow dr = dtResult.NewRow();
            dr["nJiaoluType"] = "0";
            dr["strJiaoluTypeName"] = DefaultName;
            dtResult.Rows.InsertAt(dr, 0);
            return dtResult;
        }
        
        
        /// <summary>
        /// 获取所有的客货类型
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllKehuos()
        {
            string strSql = "select * from TAB_System_Kehuo order by nKehuoID";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 获取所有的牵引类型
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllDragType()
        {
            string strSql = "select * from TAB_System_DragType order by nDragTypeID";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 获取所有的客货类型的键值类型
        /// </summary>
        /// <param name="DefaultName"></param>
        /// <returns></returns>
        public static DataTable GetAllKehuoDic(string DefaultName)
        {
            DataTable dtResult = GetAllKehuos();
            DataRow dr = dtResult.NewRow();
            dr["nKehuoID"] = "0";
            dr["strKehuoName"] = DefaultName;
            dtResult.Rows.InsertAt(dr, 0);
            return dtResult;
        }

        /// <summary>
        /// 获取支撑类型信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllTrainmanTypes()
        {
            string strSql = "select * from TAB_System_TrainmanType order by nTrainmanTypeID";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        /// <summary>
        /// 获取支撑类型信息的键值形式
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllTrainmanTypeDic(string DefaultName)
        {
            DataTable dtResult = GetAllTrainmanTypes();
            DataRow dr = dtResult.NewRow();
            dr["nTrainmanTypeID"] = "0";
            dr["strTrainmanTypeName"] = DefaultName;
            dtResult.Rows.InsertAt(dr, 0);
            return dtResult;
        }

        /// <summary>
        /// 获取所有的测酒结果信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllDrinkResults()
        {
            string strSql = "select * from TAB_System_DrinkResult order by nDrinkResult";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        /// <summary>
        /// 获取所有的测酒结果信息键值形式
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllDrinkResultDic(string DefaultName)
        {
            DataTable dtResult = GetAllDrinkResults();
            DataRow dr = dtResult.NewRow();
            dr["nDrinkResult"] = "-1";
            dr["strDrinkResultName"] = DefaultName;
            dtResult.Rows.InsertAt(dr, 0);
            return dtResult;
        }

        /// <summary>
        /// 获取所有的身份验证类型信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllverify()
        {
            string strSql = "select * from TAB_System_Verify order by nVerifyID";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        public static DataTable GetWorkPlanState()
        {
            string strSql = "select nState,strStateName from TAB_System_PlanState";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

    }
}
//public class KeyValue
//{
//    public object Key;
//    public object Value;
//    public KeyValue(object key, object value)
//    {
//        Key = key;
//        Value = value;
//    }
//}