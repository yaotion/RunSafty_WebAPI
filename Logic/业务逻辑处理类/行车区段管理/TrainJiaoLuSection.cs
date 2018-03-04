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
    ///类名：TrainJiaoLuSection
    ///描述：行车区段写卡区段关系
    /// </summary>
    public class TrainJiaoLuSection
    {
        /// <summary>
        /// 更新行车区段与写卡区段关系
        /// </summary>
        public static bool UpdateTrainJiaoLuWriteCardSectionRelation(string strTrainJiaoluGUID, System.Collections.Generic.List<string> strWriteCardSections)
        {
            if (strTrainJiaoluGUID == string.Empty)
            { return false; }
            #region 清理之前保存的关系
            SqlParameter sqlParam = new SqlParameter("@strTrainJiaoluGUID", strTrainJiaoluGUID);
            string strSqlDelete = "delete from [TAB_Base_TrainJiaolu_Section] where strTrainJiaoluGUID=@strTrainJiaoluGUID";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSqlDelete, sqlParam);
            #endregion

            #region 逐条保存新的对应的关系
            try
            {

                foreach (string strWriteCardSectionID in strWriteCardSections)
                {
                    SqlParameter[] sqlParams = {
                                                new SqlParameter("@strTrainJiaoluGUID",strTrainJiaoluGUID),
                                                new SqlParameter("@strSectionID", strWriteCardSectionID)
                                        };
                    string strSqlInsert = "insert into [TAB_Base_TrainJiaolu_Section] ([strTrainJiaoluGUID],[strSectionGUID]) values (@strTrainJiaoluGUID,@strSectionID)";
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSqlInsert, sqlParams);
                }
                return true;
            }
            catch { return false; }
            #endregion

        }


        /// <summary>
        /// 根据行车区段获取对应写卡区段
        /// </summary>
        /// <param name="strTrainJiaoLuGUID"></param>
        /// <returns></returns>
        public static DataTable GetAll(string strTrainJiaoLuGUID)
        {
            string strSql = "select * from [TAB_Base_TrainJiaolu_Section] where strTrainJiaoLuGUID=@strTrainJiaoLuGUID";
            SqlParameter sqlParam = new SqlParameter("@strTrainJiaoLuGUID", strTrainJiaoLuGUID);
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParam).Tables[0];
        }
    }
}