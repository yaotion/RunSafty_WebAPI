using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;
using TF.CommonUtility;

namespace TF.RunSafty.Drink
{
   public class DBDrink
    {
       public SystemDict.DrinkImageInfo GetDrinkImageList(string strTrainmanGUID,string strTrainPlanGUID,int WorkTypeID)
        {
            DataSet set = this.GetList(strTrainmanGUID,strTrainPlanGUID,WorkTypeID);
            SystemDict.DrinkImageInfo dii = new SystemDict.DrinkImageInfo();
            if (set.Tables[0].Rows.Count > 0)
            {
                dii.dtCreateTime = ObjectConvertClass.static_ext_Date(set.Tables[0].Rows[0]["dtCreateTime"]);
                dii.nDrinkResult = ObjectConvertClass.static_ext_int(set.Tables[0].Rows[0]["nDrinkResult"]);
                dii.strPictureURL = ObjectConvertClass.static_ext_string(set.Tables[0].Rows[0]["strImagePath"]);
            }
           return dii;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
       public DataSet GetList(string strTrainmanGUID, string strTrainPlanGUID, int WorkTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            if (WorkTypeID == 0)
            {
                strSql.Append("select * from TAB_Drink_Information where strWorkID = ( ");
                strSql.Append("select top 1 strBeginWorkGUID from TAB_Plan_BeginWork where ");
                strSql.Append("strTrainPlanGUID ='" + strTrainPlanGUID + "' and strTrainmanGUID = '" + strTrainmanGUID + "') ORDER BY dtCreateTime DESC");
            }
            else if (WorkTypeID == 1)
            {
                strSql.Append("select * from TAB_Drink_Information where strWorkID = ( ");
                strSql.Append("select top 1 strEndWorkGUID from TAB_Plan_EndWork where ");
                strSql.Append("strTrainPlanGUID = '" + strTrainPlanGUID + "' and strTrainmanGUID = '" + strTrainmanGUID + "') ORDER BY dtCreateTime DESC");
            }
          
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }
    }
}
