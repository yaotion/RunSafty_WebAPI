using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.Api.Entity;
using ThinkFreely.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace TF.Api.DBUtility 
{
    public class DBBanXu
    {
        /// <summary>
        /// 根据车间GUID,人员交路GUID获取班序信息
        /// </summary>
        /// <param name="strWorkShopGUID">车间GUID</param>
        /// <param name="strTrainmanJiaoLuGUID">交路GUID</param>
        /// <returns></returns>
        public DataTable GetBanXuByJiaoLuGUID(string strWorkShopGUID, string strTrainmanJiaoLuGUID)
        {
             string sqlCommandText = string.Format(@"select * from  VIEW_Nameplate_Group 
                where  strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID and  GroupState is null    order by groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),dtLastArriveTime,strTrainmanName1,strTrainmanName2");
             SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainmanJiaoluGUID",strTrainmanJiaoLuGUID) 
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlCommandText, sqlParams).Tables[0];
        }
    }
}
