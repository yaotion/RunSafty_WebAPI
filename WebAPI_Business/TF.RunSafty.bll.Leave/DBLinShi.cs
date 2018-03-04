using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace TF.RunSafty.Leave
{
    public class DBLinShi
    {
        public static bool GetTrainman(string TrainmanGUID , MD.TrainmanMin TM)
        {
            string strSql = "select top 1 * from TAB_Org_Trainman where strTrainmanGUID = @strTrainmanGUID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("strTrainmanGUID",TrainmanGUID)
            };
            DataTable dt= SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                TM.TrainmanGUID = dt.Rows[0]["strTrainmanGUID"].ToString();
                TM.TrainmanNumber = dt.Rows[0]["strTrainmanNumber"].ToString();
                TM.TrainmanName = dt.Rows[0]["strTrainmanName"].ToString();
                TM.TrainmanState = Convert.ToInt32(dt.Rows[0]["nTrainmanState"]);
                TM.TrainmanJLGUID = dt.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                TM.PostID = Convert.ToInt32(dt.Rows[0]["nPostID"]);
                return true;
            }
            return false;
        }
        public static bool GetTrainmanJL(string TMJiaoluGUID, MD.TrainmanJLMin TMJL)
        {
            string strSql = "select top 1 * from TAB_Base_TrainmanJiaolu where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("strTrainmanJiaoluGUID",TMJiaoluGUID)
            };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                TMJL.TrainmanJLGUID = dt.Rows[0]["strTrainmanJiaoluGUID"].ToString();
                TMJL.TrainmanJLName = dt.Rows[0]["strTrainmanJiaoluName"].ToString();
                TMJL.TrainmanJLType = Convert.ToInt32(dt.Rows[0]["nJiaoluType"]);   
                return true;
            }
            return false;
        }

    }
}
