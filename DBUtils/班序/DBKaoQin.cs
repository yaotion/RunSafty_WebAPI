using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace TF.Api.DBUtility 
{
    public class DBKaoQin
    {
        /// <summary>
        /// 根据车间GUID获取考勤信息
        /// </summary>
        /// <param name="strWorkShopGUID"></param>
        /// <returns></returns>
        public DataTable GetKaoQinByWorkShopGUID(string strWorkShopGUID)
        {
            string sqlCommandText = string.Format(@" 
SELECT [strTrainmanGUID] as TrainmanGUID
      ,[strTrainmanNumber] as TrainmanNo
      ,[strTrainmanName] as TrainmanName
       ,p.strPostName as DutyName
      ,isnull([strTrainmanJiaoluGUID],'') as TrainmanJiaoLuGUID
      ,isnull(s.strTypeName,'') as LeaveTypeName
      ,isnull(CONVERT(varchar, l.dBeginTime, 120 ),'')  as LeaveStartTime 
      ,isnull(CONVERT(varchar, l.dEndTime, 120 ),'')  as  leaveEndTime
  FROM  [TAB_Org_Trainman] t
  left join TAB_System_Post p
  on t.nPostID=p.nPost
  left join
  (
SELECT a.*
  FROM [RunSafty].[dbo].[TAB_LeaveMgr_AskLeave] a
  inner join (select strTrainManID,MAX([dBeginTime]) as [dBeginTime1] from [TAB_LeaveMgr_AskLeave] group by strTrainManID) b
  on a.strTrainManID=b.strTrainManID and a.dBeginTime=b.dBeginTime1
 
  ) l
  on t.strTrainmanNumber=l.strTrainManID
  left join TAB_LeaveMgr_LeaveType s
  on l.strLeaveTypeGUID=s.strTypeGUID
  where p.strPostName in ('司机','副司机','学员')  and t.strWorkShopGUID=@strWorkShopGUID and l.dBeginTime is not null and l.dEndTime is not null
order by l.dBeginTime asc
  ");
            SqlParameter[] sqlParams = { 
                                           new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlCommandText, sqlParams).Tables[0];
        }
    }
}
