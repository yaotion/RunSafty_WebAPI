using System;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;

namespace TF.Api.BLL
{
    /// <summary>
    /// 类名：BytimeBLL
    /// 描述：添乘记录数据库操作类
    /// </summary>
    public class BytimeBLL
    {
        #region 添乘记录添加
        /// <summary>
        /// 添乘记录添加
        /// </summary>
        /// <param name="byTimeRecordEntity">添乘记录对象</param>
        /// <param name="exceptInfo">异常信息</param>
        /// <returns></returns>
        public static bool Add(TF.Api.Entity.ByTimeRecordEntity byTimeRecordEntity, out string exceptInfo)
        {
            exceptInfo = string.Empty;
            string strSql = "INSERT INTO [TAB_ByTime_Record] ([strByTimeGUID] ,[strTrainmanGUID] ,[dtTime] ,[nVerifyID] ,[strRemark] ,[nByTimeType] ,[strSiteGUID]) VALUES (@strByTimeGUID ,@strTrainmanGUID ,@dtTime ,@nVerifyID ,@strRemark ,@nByTimeType ,@strSiteGUID)";
            SqlParameter[] sqlParams = { 
                                                                    new SqlParameter("@strByTimeGUID",byTimeRecordEntity.flowID),
                                                                    new SqlParameter("@strTrainmanGUID",byTimeRecordEntity.tid),
                                                                    new SqlParameter("@dtTime",byTimeRecordEntity.time),
                                                                    new SqlParameter("@nVerifyID",byTimeRecordEntity.verify),
                                                                    new SqlParameter("@strRemark",byTimeRecordEntity.remark),
                                                                    new SqlParameter("@nByTimeType",byTimeRecordEntity.byTimeType),
                                                                    new SqlParameter("@strSiteGUID",byTimeRecordEntity.cid)
                                                          };
            try
            {
                 SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, System.Data.CommandType.Text, strSql, sqlParams);
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
