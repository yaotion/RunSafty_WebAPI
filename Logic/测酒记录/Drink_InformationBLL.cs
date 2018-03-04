using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TF.Api.Entity;
using ThinkFreely.DBUtility;

namespace TF.Api.BLL
{
    /// <summary>
    /// 类名：Drink_InformationBLL
    /// 描述：测酒记录逻辑操作类
    /// </summary>
    public class Drink_InformationBLL
    {
        #region 添加测酒记录
        /// <summary
        /// 类名 ：Add
        /// 描述：添加测酒记录
        /// </summary>
        /// <param name="drinkInfoEntity">测酒记录实体</param>
        /// <param name="exceptInfo">out 异常信息</param>
        /// <returns></returns>
        public static bool Add(Drink_InformationEntity drinkInfoEntity, out string exceptInfo)
        {
            exceptInfo = string.Empty;
            string strSql = @"insert into TAB_Drink_Information (strGUID,strTrainmanGUID,nDrinkResult,dtCreateTime,nVerifyID,strWorkID) 
                            values (@strGUID,@strTrainmanGUID,@nDrinkResult,@dtCreateTime,@nVerifyID,@strWorkID)";

            SqlParameter[] sqlParams = {
                                                       new SqlParameter("strGUID",drinkInfoEntity.strid),
                                                       new SqlParameter("strTrainmanGUID",drinkInfoEntity.trainmanID),
                                                       new SqlParameter("nDrinkResult",drinkInfoEntity.result),
                                                       new SqlParameter("dtCreateTime",drinkInfoEntity.testTime),
                                                       new SqlParameter("nVerifyID",drinkInfoEntity.verify),
                                                       new SqlParameter("strWorkID",drinkInfoEntity.flowID)
                                                   };
            try
            {

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                exceptInfo = ex.ToString();
                return false;
            }
        }
        #endregion
    }
}