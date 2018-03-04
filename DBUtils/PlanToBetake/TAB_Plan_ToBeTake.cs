using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using System.Data;

namespace TF.RunSafty.DAL
{
    public partial class TAB_Plan_ToBeTake
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TF.RunSafty.Model.Model_Plan_ToBeTake model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Plan_ToBeTake(");
            strSql.Append("strPlanGUID,strCheCi,dtCallWorkTime,dtWaitWorkTime,strTrainmanGUID1,strTrainmanGUID2,strTrainmanGUID3,strTrainmanGUID4)");
            strSql.Append(" values (");
            strSql.Append("@strPlanGUID,@strCheCi,@dtCallWorkTime,@dtWaitWorkTime,@strTrainmanGUID1,@strTrainmanGUID2,@strTrainmanGUID3,@strTrainmanGUID4)");
            SqlParameter[] parameters = {
					new SqlParameter("@strPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strCheCi", SqlDbType.VarChar,50),
					new SqlParameter("@dtCallWorkTime", SqlDbType.DateTime),
					new SqlParameter("@dtWaitWorkTime", SqlDbType.DateTime),
					new SqlParameter("@strTrainmanGUID1", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID2", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID3", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID4", SqlDbType.VarChar,50)};
            parameters[0].Value = model.strPlanGUID;
            parameters[1].Value = model.strCheCi;
            parameters[2].Value = model.dtCallWorkTime;
            parameters[3].Value = model.dtWaitWorkTime;
            parameters[4].Value = model.strTrainmanGUID1;
            parameters[5].Value = model.strTrainmanGUID2;
            parameters[6].Value = model.strTrainmanGUID3;
            parameters[7].Value = model.strTrainmanGUID4;

            int rows = (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
