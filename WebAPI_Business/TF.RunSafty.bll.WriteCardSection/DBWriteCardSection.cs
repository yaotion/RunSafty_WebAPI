using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;
using TF.CommonUtility;
using System.Data.SqlClient;

namespace TF.RunSafty.WriteCardSection
{
    public class DBWriteCardSection
    {
        #region GetPlanAllSections方法（获取计划所有可选的写卡区段）
        public List<WriteCardSection> GetPlanAllSections(string TrainPlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Base_TrainJiaolu_Section ");
            strSql.Append("WHERE strTrainJiaoluGUID in ");
            strSql.Append("(select strtrainjiaoluguid from TAB_Plan_Train where strTrainPlanGUID='" + TrainPlanGUID + "') ");
            strSql.Append(" order by strSectionID ");
            return GetPlanAllSectionsDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<WriteCardSection> GetPlanAllSectionsDTToList(DataTable dt)
        {
            List<WriteCardSection> modelList = new List<WriteCardSection>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                WriteCardSection model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetPlanAllSectionsDRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public WriteCardSection GetPlanAllSectionsDRToModelDTToList(DataRow dr)
        {
            WriteCardSection model = new WriteCardSection();
            if (dr != null)
            {
                model.strJWDNumber = ObjectConvertClass.static_ext_string(dr["strJWDNumber"]);
                model.strSectionID = ObjectConvertClass.static_ext_string(dr["strSectionID"]);
                model.strSectionName = ObjectConvertClass.static_ext_string(dr["strSectionName"]);
            }
            return model;
        }
        #endregion


        #region GetPlanSelectedSections方法（获取计划所有可选的写卡区段）
        public List<WriteCardSection> GetPlanSelectedSections(string TrainPlanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Tab_Plan_WriteCardSection WHERE strTrainPlanGUID = '" + TrainPlanGUID + "' order by strSectionID  ");
            return GetPlanAllSectionsDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        #endregion



        #region
        public bool SetPlanSections(string TrainPlanGUID, List<WriteCardSection> SectionArray, string DutyUserGUID, string DutyUserNumber, string DutyUserName)
        {
            string strSqldel = "delete from Tab_Plan_WriteCardSection where strTrainPlanGUID ='" + TrainPlanGUID + "'";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSqldel);
            foreach (WriteCardSection W in SectionArray)
            {
                string strSql = "insert into Tab_Plan_WriteCardSection (strTrainPlanGUID,strSectionID,strSectionName,strJWDNumber,dtCreateTime,strDutyUserGUID,strDutyUserNumber,strDutyUserName)";
                strSql += " values(@strTrainPlanGUID,@strSectionID,@strSectionName,@strJWDNumber,@dtCreateTime,@strDutyUserGUID,@strDutyUserNumber,@strDutyUserName)";
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("@strTrainPlanGUID",TrainPlanGUID),
                         new SqlParameter("@strSectionID",W.strSectionID),
                         new SqlParameter("@strSectionName",W.strSectionName),
                         new SqlParameter("@strJWDNumber",W.strJWDNumber),
                         new SqlParameter("@dtCreateTime",DateTime.Now),
                         new SqlParameter("@strDutyUserGUID",DutyUserGUID),
                         new SqlParameter("@strDutyUserNumber",DutyUserNumber),
                         new SqlParameter("strDutyUserName",DutyUserName),
                    };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
            }
            return true;
        }
        #endregion
    }
}