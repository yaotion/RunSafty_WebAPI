using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.NamePlate
{
    public class DBNPControlRest
    {
        /// <summary>
        /// 获取指定人员交路下的各个出勤点本段休息卡控信息
        /// </summary>
        /// <param name="TrainmanJiaoluGUID">人员交路GUID</param>
        /// <param name="PlaceID">出勤点编号，可为空，为空代表所有出勤点</param>
        /// <returns></returns>
        public static List<MDNPControlRest> GetControlResultList(string TrainmanJiaoluGUID,string PlaceID)
        {
            string strSql = "select * from TAB_Nameplate_TrainmanJiaolu_RestControl where TrainmanJiaoluGUID = @TrainmanJiaoluGUID ";
            if (PlaceID.Trim() != "")
            {
                strSql = strSql + " and PlaceID = @PlaceID ";
            }
            strSql = strSql + " order by LocalPlace,PlaceID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("TrainmanJiaoluGUID",TrainmanJiaoluGUID),
                new SqlParameter("PlaceID",PlaceID)
            };
            List<MDNPControlRest> result = new List<MDNPControlRest>();
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MDNPControlRest cr = new MDNPControlRest();
                cr.LocalPlace = TF.Utils.TFConvert.DBToIntD(dt.Rows[i]["LocalPlace"]);

                cr.MinLocalRestMinutes = TF.Utils.TFConvert.DBToIntD(dt.Rows[i]["MinLocalRestMinutes"]);
                cr.PlaceID = TF.Utils.TFConvert.DBToStringD(dt.Rows[i]["PlaceID"]);
                cr.PlaceName = TF.Utils.TFConvert.DBToStringD(dt.Rows[i]["PlaceName"]);
                cr.TrainmanJiaoluGUID = TF.Utils.TFConvert.DBToStringD(dt.Rows[i]["LocalPlace"]);
                cr.TrainmanJiaoluName = TF.Utils.TFConvert.DBToStringD(dt.Rows[i]["TrainmanJiaoluName"]);
                cr.ControlLocalRest = TF.Utils.TFConvert.DBToIntD(dt.Rows[i]["ControlLocalRest"]);

                result.Add(cr);
            }        
            return result;
        }
    }
}
