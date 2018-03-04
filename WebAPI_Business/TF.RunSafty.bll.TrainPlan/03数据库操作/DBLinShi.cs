using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ThinkFreely.DBUtility;

namespace TF.Runsafty.Plan
{
    public class DBLinShi
    {
        public static bool GetTrainmanJiaolu(string TrainmanJiaoluGUID,ref TF.RunSafty.Plan.MD.TrainmanJiaoluMin TrainmanJiaolu)
        {
            string sqlText = "select * from TAB_Base_TrainmanJiaolu where strTrainmanJiaoluGUID= @strTrainmanJiaoluGUID";
            SqlParameter[] sqlParams = new SqlParameter[]{
                new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoluGUID)
            };
            DataTable dtJiaolu = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlText, sqlParams).Tables[0];
            if (dtJiaolu.Rows.Count > 0)
            {
                TrainmanJiaolu.jiaoluID = dtJiaolu.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                TrainmanJiaolu.jiaoluName = dtJiaolu.Rows[0]["strTrainmanJiaoluName"].ToString();
                TrainmanJiaolu.jiaoluType = Convert.ToInt32(dtJiaolu.Rows[0]["nJiaoluType"]);
                return true;
            }
            return false;
        }
    }
}
