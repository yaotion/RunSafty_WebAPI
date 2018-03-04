using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;

namespace TF.Runsafty.TestWeb
{
   public class DBTestWeb
    {
        #region GetStationList方法（获取所有的车站）
        public List<Station> GetStationList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_Base_Station  order by strStationNumber ");
            return GetStationListDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<Station> GetStationListDTToList(DataTable dt)
        {
            List<Station> modelList = new List<Station>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Station model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetStationListDRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public Station GetStationListDRToModelDTToList(DataRow row)
        {
            Station model = new Station();
            if (row != null)
            {
                if (row["nid"] != null && row["nid"].ToString() != "")
                {
                    model.nid = int.Parse(row["nid"].ToString());
                }
                if (row["strStationGUID"] != null)
                {
                    model.strStationGUID = row["strStationGUID"].ToString();
                }
                if (row["strWorkShopGUID"] != null)
                {
                    model.strWorkShopGUID = row["strWorkShopGUID"].ToString();
                }
                if (row["strStationName"] != null && row["strStationName"].ToString() != "")
                {
                    model.strStationName = row["strStationName"].ToString();
                }
                if (row["strStationNumber"] != null)
                {
                    model.strStationNumber = Convert.ToInt32(row["strStationNumber"].ToString());
                }
                if (row["strStationPY"] != null)
                {
                    model.strStationPY = row["strStationPY"].ToString();
                }
            }
            return model;
        }
        #endregion


        #region 添加车站

        public void Add(Station Station)
        {
            SqlParameter[] param = { 
                                   new SqlParameter("strStationGUID",Station.strStationGUID),
                                   new SqlParameter("strStationName",Station.strStationName),
                                   new SqlParameter("strStationNumber",Station.strStationNumber)
                                   };
            string sql = "insert into TAB_Base_Station(strStationGUID,strStationName,strStationNumber) values(@strStationGUID,@strStationName,@strStationNumber)";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, param);
        }
        #endregion





    }
}
