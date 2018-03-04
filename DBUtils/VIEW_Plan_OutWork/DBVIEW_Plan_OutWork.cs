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
using TF.RunSafty.Entry;
using TF.RunSafty.Model;

namespace TF.RunSafty.DBUtils
{
	/// <summary>
	///类名: VIEW_Plan_OutWorkQueryCondition
	///说明: 外勤计划查询条件类
	/// </summary>
	public class VIEW_Plan_OutWorkQueryCondition
	{
        //获取统计信息
        public DataTable GetAllTongJiChuQingAndTuiQing(string strBegin, string strEnd)
        {

            string strWhereEnd = " 1=1 ";
            string strwhereBegin = " 1=1";
            string strwhereOutDrink = " 1=1";
            string strwhereChick = " 1=1";
            string strwhereLocalDrink = "1=1";


            if (!string.IsNullOrEmpty(strBegin))
                strWhereEnd += "and TAB_Plan_EndWork.dtCreateTime>='" + strBegin + " 00:00:00'";
            else
                strWhereEnd += "and TAB_Plan_EndWork.dtCreateTime>='" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "'";
            if (!string.IsNullOrEmpty(strEnd))
                strWhereEnd += " and TAB_Plan_EndWork.dtCreateTime<='" + strEnd + " 23:59:59'";
            else
                strWhereEnd += "and TAB_Plan_EndWork.dtCreateTime<='" + DateTime.Now.ToString("yyyy-MM-dd 23:59:59") + "'";


            if (!string.IsNullOrEmpty(strBegin))
                strwhereBegin += "and TAB_Plan_BeginWork.dtCreateTime>='" + strBegin + " 00:00:00'";
            else
                strwhereBegin += "and TAB_Plan_BeginWork.dtCreateTime>='" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "'";
            if (!string.IsNullOrEmpty(strEnd))
                strwhereBegin += " and TAB_Plan_BeginWork.dtCreateTime<='" + strEnd + " 23:59:59'";
            else
                strwhereBegin += "and TAB_Plan_BeginWork.dtCreateTime<='" + DateTime.Now.ToString("yyyy-MM-dd 23:59:59") + "'";



            if (!string.IsNullOrEmpty(strBegin))
                strwhereOutDrink += "and VIEW_Drink_OutSide.dtCreateTime>='" + strBegin + " 00:00:00'";
            else
                strwhereOutDrink += "and VIEW_Drink_OutSide.dtCreateTime>='" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "'";
            if (!string.IsNullOrEmpty(strEnd))
                strwhereOutDrink += " and VIEW_Drink_OutSide.dtCreateTime<='" + strEnd + " 23:59:59'";
            else
                strwhereOutDrink += "and VIEW_Drink_OutSide.dtCreateTime<='" + DateTime.Now.ToString("yyyy-MM-dd 23:59:59") + "'";



            if (!string.IsNullOrEmpty(strBegin))
                strwhereLocalDrink += "and VIEW_Drink_Local.dtCreateTime>='" + strBegin + " 00:00:00'";
            else
                strwhereLocalDrink += "and VIEW_Drink_Local.dtCreateTime>='" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "'";
            if (!string.IsNullOrEmpty(strEnd))
                strwhereLocalDrink += " and VIEW_Drink_Local.dtCreateTime<='" + strEnd + " 23:59:59'";
            else
                strwhereLocalDrink += "and VIEW_Drink_Local.dtCreateTime<='" + DateTime.Now.ToString("yyyy-MM-dd 23:59:59") + "'";



            if (!string.IsNullOrEmpty(strBegin))
                strwhereChick += "and TAB_Plan_RunEvent_IDICCard.dtCreateTime>='" + strBegin + " 00:00:00'";
            else
                strwhereChick += "and TAB_Plan_RunEvent_IDICCard.dtCreateTime>='" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "'";
            if (!string.IsNullOrEmpty(strEnd))
                strwhereChick += " and TAB_Plan_RunEvent_IDICCard.dtCreateTime<='" + strEnd + " 23:59:59'";
            else
                strwhereChick += "and TAB_Plan_RunEvent_IDICCard.dtCreateTime<='" + DateTime.Now.ToString("yyyy-MM-dd 23:59:59") + "'";



            string strSql = string.Format(@"
select strWorkShopName=isnull(isnull(isnull(isnull(a.strWorkShopName,b.strWorkShopName),c.strWorkShopName),d.strWorkShopName),l.strWorkShopName), BeginWorkCount=ISNULL(BeginWorkCount,0),
EndWorkCount=ISNULL(EndWorkCount,0),OutDrinkCount=ISNULL(OutDrinkCount,0),LocalDrinkCount=ISNULL(LocalDrinkCount,0),ChickCount=ISNULL(ChickCount,0)
from
(select  COUNT(*) as EndWorkCount, strWorkShopName from 
dbo.TAB_Org_WorkShop RIGHT OUTER JOIN dbo.TAB_Base_TrainJiaolu ON dbo.TAB_Org_WorkShop.strWorkShopGUID = dbo.TAB_Base_TrainJiaolu.strWorkShopGUID
 RIGHT OUTER JOIN dbo.TAB_Plan_Train ON dbo.TAB_Base_TrainJiaolu.strTrainJiaoluGUID = dbo.TAB_Plan_Train.strTrainJiaoluGUID RIGHT OUTER JOIN
   dbo.TAB_Plan_EndWork ON dbo.TAB_Plan_Train.strTrainPlanGUID = dbo.TAB_Plan_EndWork.strTrainPlanGUID where {0} group by strWorkShopName
                       having strWorkShopName is not null) a full join
 (SELECT COUNT(*) as BeginWorkCount, strWorkShopName FROM dbo.TAB_Org_WorkShop RIGHT OUTER JOIN dbo.TAB_Base_TrainJiaolu ON 
dbo.TAB_Org_WorkShop.strWorkShopGUID = dbo.TAB_Base_TrainJiaolu.strWorkShopGUID RIGHT OUTER JOIN dbo.TAB_Plan_Train ON 
dbo.TAB_Base_TrainJiaolu.strTrainJiaoluGUID = dbo.TAB_Plan_Train.strTrainJiaoluGUID RIGHT OUTER JOIN dbo.TAB_Plan_BeginWork 
ON dbo.TAB_Plan_Train.strTrainPlanGUID = dbo.TAB_Plan_BeginWork.strTrainPlanGUID where {1} group by strWorkShopName having strWorkShopName
 is not null) b on a.strWorkShopName=b.strWorkShopName
 full join 
 (select COUNT(*) as OutDrinkCount,strWorkShopName from VIEW_Drink_OutSide where {2} group by strWorkShopName having strWorkShopName is not null) c
 on c.strWorkShopName=b.strWorkShopName
full join 
 (select COUNT(*) as LocalDrinkCount,strWorkShopName from VIEW_Drink_Local where {4} group by strWorkShopName having strWorkShopName is not null) l
 on l.strWorkShopName=b.strWorkShopName
 full join 
 ( SELECT COUNT(*) as ChickCount, strWorkShopName FROM dbo.TAB_Org_WorkShop RIGHT OUTER JOIN dbo.TAB_Base_TrainJiaolu ON 
dbo.TAB_Org_WorkShop.strWorkShopGUID = dbo.TAB_Base_TrainJiaolu.strWorkShopGUID RIGHT OUTER JOIN dbo.TAB_Plan_Train ON 
dbo.TAB_Base_TrainJiaolu.strTrainJiaoluGUID = dbo.TAB_Plan_Train.strTrainJiaoluGUID RIGHT OUTER JOIN dbo.TAB_Plan_RunEvent_IDICCard 
ON dbo.TAB_Plan_Train.strTrainPlanGUID = dbo.TAB_Plan_RunEvent_IDICCard.strTrainPlanGUID where {3} group by strWorkShopName having strWorkShopName
 is not null) d on a.strWorkShopName=d.strWorkShopName 
", strWhereEnd, strwhereBegin, strwhereOutDrink, strwhereChick, strwhereLocalDrink);
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }
	}
}
