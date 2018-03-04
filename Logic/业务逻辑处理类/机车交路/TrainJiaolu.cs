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
    ///TrainJiaolu 的摘要说明
    /// </summary>
    public class TrainJiaolu
    {
        #region 属性
        public string strTrainJiaoluGUID = "";
        public string strTrainJiaoluName = "";
        public string strLineGUID = "";
        public string strStartStation = "";
        public string strEndStation = "";
        public int nDeleteState = 0;
        public string strWorkShopGUID = "";
        #endregion


        #region 构造函数
        public TrainJiaolu()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public TrainJiaolu(string TrainJiaoluGUID)
        {
            string strSql = "select * from TAB_Base_TrainJiaolu where strTrainJiaoluGUID=@strTrainJiaoluGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",TrainJiaoluGUID)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strTrainJiaoluGUID = dt.Rows[0]["strTrainJiaoluGUID"].ToString();
                strTrainJiaoluName = dt.Rows[0]["strTrainJiaoluName"].ToString();
                
                strStartStation = dt.Rows[0]["strStartStation"].ToString();
                strEndStation = dt.Rows[0]["strEndStation"].ToString();
                strWorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();                
                
            }
        }
        #endregion 构造函数

        #region 增删改
        public string Add()
        {
            string guid = Guid.NewGuid().ToString();
            string strSql = "insert into TAB_Base_TrainJiaolu (strTrainJiaoluGUID,strTrainJiaoluName,strLineGUID,strStartStation,strEndStation,nDeleteState) " +
                "values (@strTrainJiaoluGUID,@strTrainJiaoluName,@strLineGUID,@strStartStation,@strEndStation,0)";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",guid),
                                           new SqlParameter("strTrainJiaoluName",strTrainJiaoluName),
                                           new SqlParameter("strLineGUID",strLineGUID),
                                           new SqlParameter("strStartStation",strStartStation),
                                           new SqlParameter("strEndStation",strEndStation)
                                       };
            if (SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) == 0)
                return "";
            return guid;
        }
        public bool Update()
        {
            string strSql = "update TAB_Base_TrainJiaolu set strTrainJiaoluName = @strTrainJiaoluName,strLineGUID=@strLineGUID,strStartStation=@strStartStation,strEndStation=@strEndStation where strTrainJiaoluGUID=@strTrainJiaoluGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUID),
                                           new SqlParameter("strTrainJiaoluName",strTrainJiaoluName),
                                           new SqlParameter("strLineGUID",strLineGUID),
                                           new SqlParameter("strStartStation",strStartStation),
                                           new SqlParameter("strEndStation",strEndStation)
                                           
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool Delete(string TrainJiaoluGUID)
        {
            string strSql = "update TAB_Base_TrainJiaolu set nDeleteState = 1 where strTrainJiaoluGUID=@strTrainJiaoluGUID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",TrainJiaoluGUID)
                                       };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }
        public static bool ExistJiaoluName(string TrainJiaoluGUID, string LineGUID, string TrainJiaoluName)
        {
            string strSql = "select count(*) from TAB_Base_TrainJiaolu where nDeleteState = 0 and strLineGUID=@strLineGUID and strTrainJiaoluName=@strTrainJiaoluName ";
            if (TrainJiaoluGUID != "")
            {
                strSql += " and strTrainJiaoluGUID <> @strTrainJiaoluGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",TrainJiaoluGUID),
                                           new SqlParameter("strLineGUID",LineGUID),
                                           new SqlParameter("strTrainJiaoluName",TrainJiaoluName)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }


        public static bool ExistJiaoluStation(string TrainJiaoluGUID, string LineGUID, string StartStation, string EndStation)
        {
            string strSql = "select count(*) from TAB_Base_TrainJiaolu where nDeleteState = 0 and strLineGUID=@strLineGUID and strStartStation=@strStartStation and strEndStation=@strEndStation ";
            if (TrainJiaoluGUID != "")
            {
                strSql += " and strTrainJiaoluGUID <> @strTrainJiaoluGUID";
            }
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",TrainJiaoluGUID),
                                           new SqlParameter("strLineGUID",LineGUID),
                                           new SqlParameter("strStartStation",StartStation),
                                           new SqlParameter("strEndStation",EndStation)
                                       };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams)) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetTrainJiaolus(int Pageindex, int PageCount, string AreaGUID, string LineGUID, string TrainJiaoluName)
        {

            string strSqlCondition = " where 1=1 ";
            if (AreaGUID != "")
            {
                strSqlCondition += " and strAreaGUID = @strAreaGUID ";
            }
            if (LineGUID != "")
            {
                strSqlCondition += " and strLineGUID = @strLineGUID ";
            }
            if (TrainJiaoluName != "")
            {
                strSqlCondition += " and strTrainJiaoluName like @strTrainJiaoluName ";
            }

            string strSql = "select top " + PageCount.ToString() + " * from VIEW_Base_TrainJiaolu " + strSqlCondition +
                " and strTrainJiaoluGUID not in (select top " + ((Pageindex - 1) * PageCount).ToString() + " strTrainJiaoluGUID from VIEW_Base_TrainJiaolu " + strSqlCondition +
                "  order by strTrainJiaoluName)  order by strTrainJiaoluName";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strAreaGUID",AreaGUID),
                                           new SqlParameter("strLineGUID",LineGUID),
                                           new SqlParameter("strTrainJiaoluName","%" +TrainJiaoluName+ "%")
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        public static int GetTrainJiaoluCount(string AreaGUID, string LineGUID, string TrainJiaoluName)
        {
    
            

            string strSql = " select count(*) from VIEW_Base_TrainJiaolu where nDeleteState = 0 " ;
            if (AreaGUID != "")
            {
                strSql += " and strAreaGUID =  '" +AreaGUID+ "'";
            }
            if (LineGUID != "")
            {
                strSql += " and strLineGUID = '" +LineGUID+ "' ";
            }
            if (TrainJiaoluName != "")
            {
                strSql += " and strTrainJiaoluName like '%" + TrainJiaoluName + "%' ";
            }
           
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql));
        }

        public static DataTable GetAllTrainJiaolus(string LineGUID)
        {
            return GetTrainJiaolus(1, 10000, "", LineGUID, ""); 
        }

        public static DataTable GetTrainJiaolusForMonthWorkTime()
        {
            string strSql = "select * from TAB_Base_TrainJiaolu where strTrainJiaoluGUID not in (select strTrainJiaoluGUID from TAB_WorkTime_MonthSectionFilter) ";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        public static DataTable GetTrainJiaolusDic(string LineGUID,string DefaultName)
        {
            DataTable dtResult = GetAllTrainJiaolus(LineGUID);
            DataRow dr = dtResult.NewRow();
            dr["strTrainJiaoluGUID"] = "";
            dr["strTrainJiaoluName"] = DefaultName;
            dtResult.Rows.InsertAt(dr, 0);
            return dtResult;
        }

        public static DataTable GetSiteTrainJiaolus(string SiteGUID)
        {
            string strSql = "select * from VIEW_Base_TrainJiaoluInSite where strSiteGUID = @SiteGUID order by strTrainJiaoluName";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@SiteGUID",SiteGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        public static DataTable GetSiteTrainJiaolusKehuo()
        {
            string strSql = "select * from TAB_Base_TrainJiaoluInSite_Kehuo";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        //更新站点管辖交路
        public static bool AddSiteTrainJiaolus(string SiteGUID, string trainJiaolus,string jiaolukehuo)
        {
            string strSql = "delete TAB_Base_TrainJiaoluInSite_Kehuo where strJiaoluInSiteGUID in (select strJiaoluInSiteGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = @strSiteGUID);  delete TAB_Base_TrainJiaoluInSite where strSiteGUID = @strSiteGUID ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@strSiteGUID",SiteGUID)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
            string[] jiaolus = trainJiaolus.Split(',');
            string[] kehuos = jiaolukehuo.Split(',');
            if (trainJiaolus.Trim()!= "")
            {
                int SucessCount = 0;
                for (int i = 0; i < jiaolus.Length; i++)
                {
                    string guid = Guid.NewGuid().ToString();
                    strSql = "insert into TAB_Base_TrainJiaoluInSite (strJiaoluInSiteGUID,strSiteGUID,strTrainJiaoluGUID) values (@strJiaoluInSiteGUID,@strSiteGUID,@strTrainJiaoluGUID)";
                    SqlParameter[] sqlParamsSub = {
                                            new SqlParameter("@strJiaoluInSiteGUID",guid),
                                           new SqlParameter("@strSiteGUID",SiteGUID),
                                           new SqlParameter("@strTrainJiaoluGUID",jiaolus[i])
                                       };
                    SucessCount += SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsSub);
                    if (jiaolukehuo.Trim() != "")
                    {
                        for (int j = 0; j < kehuos.Length; j++)
                        {
                            if (kehuos[j].Contains(jiaolus[i].Trim()))
                            {
                                string[] kh = kehuos[j].Split('@');
                                SqlParameter[] sParams = {
                                            new SqlParameter("@strJiaoluInSiteGUID",guid),
                                           new SqlParameter("@nKehuoID",kh[1])
                                       };
                                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, "insert into TAB_Base_TrainJiaoluInSite_Kehuo (strJiaoluInSiteGUID,nKehuoID) values (@strJiaoluInSiteGUID,@nKehuoID)", sParams);
                            }
                        }
                    }
                }
                return SucessCount > 0;
            }
            return true;
        }

        /// <summary>
        /// 根据机车交路
        /// </summary>
        /// <param name="areaid"></param>
        /// <returns></returns>
        public static DataTable GetTrainJiaoluById(string areaid)
        {
            string strSql = "select * from VIEW_Base_TrainJiaolu where  strAreaGUID = @areaid and nDeleteState=0";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("@areaid",areaid)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }

        public static DataTable GetTrainmanJiaolus(string TrainJiaoluGUID)
        {
            string strSql = "select * from VIEW_Base_JiaoluRelation where strTrainJiaoluGUID = @strTrainJiaoluGUID  order by strTrainJiaoluName";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",TrainJiaoluGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        public static void UpdateTrainmanJiaolus(string TrainJiaoluGUID, string TrainmanJiaoluGUIDs)
        {
            string strSql = "delete TAB_Base_JiaoluRelation where strTrainJiaoluGUID = @strTrainJiaoluGUID";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",TrainJiaoluGUID)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

            string[] strTrainmanJiaoluGUIDArray = TrainmanJiaoluGUIDs.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strTrainmanJiaoluGUIDArray.Length; i++)
            {
                strSql = "insert into TAB_Base_JiaoluRelation (strTrainJiaoluGUID,strTrainmanJiaoluGUID) values (@strTrainJiaoluGUID,@strTrainmanJiaoluGUID)";
                SqlParameter[] sqlParamsAdd = {
                                           new SqlParameter("strTrainJiaoluGUID",TrainJiaoluGUID),
                                           new SqlParameter("strTrainmanJiaoluGUID",strTrainmanJiaoluGUIDArray[i])
                                       };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsAdd);
            }
        }

        public static DataTable GetTrainJiaoLuByName(string name)
        {
            string strSql = string.Format("select * from TAB_Base_TrainJiaolu where strTrainJiaoluName like @strTrainJiaoluName order by nID");
            SqlParameter[] sqlParamsAdd = {
                                           new SqlParameter("strTrainJiaoluName","%"+name+"%"), 
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsAdd).Tables[0];
        }
        public static DataTable GetAllTrainmanJiaolus()
        {
            string strSql = "select * from VIEW_Base_TrainmanJiaolu order by nID";

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        public static DataTable GetTrainJiaoluOfTrainmanJiaolu(string TrainmanJiaoluGUID)
        {
            string strSql = "select * from VIEW_Base_JiaoluRelation where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID  order by strTrainmanJiaoluName";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoluGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }

        public static void UpdateTrainJiaolus(string TrainmanJiaoluGUID, string TrainJiaoluGUIDs)
        {
            string strSql = "delete TAB_Base_JiaoluRelation where strTrainmanJiaoluGUID = @strTrainmanJiaoluGUID";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoluGUID)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

            string[] strTrainJiaoluGUIDArray = TrainJiaoluGUIDs.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strTrainJiaoluGUIDArray.Length; i++)
            {
                strSql = "insert into TAB_Base_JiaoluRelation (strTrainJiaoluGUID,strTrainmanJiaoluGUID) values (@strTrainJiaoluGUID,@strTrainmanJiaoluGUID)";
                SqlParameter[] sqlParamsAdd = {
                                           new SqlParameter("strTrainmanJiaoluGUID",TrainmanJiaoluGUID),
                                           new SqlParameter("strTrainJiaoluGUID",strTrainJiaoluGUIDArray[i])
                                       };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsAdd);
            }
        }

        public static DataTable GetAllTrainJiaolus()
        {
            string strSql = "select * from VIEW_Base_TrainJiaolu order by nID";

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }

        public static DataTable GetTrueTrainJiaolusOfWorkShop(string WorkShopGUID)
        {
            string strSql = "select * from VIEW_Base_TrainJiaolu where strWorkShopGUID = @strWorkShopGUID and bIsDir = 0 order by strTrainJiaoluName";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strWorkShopGUID",WorkShopGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }
        public static DataTable GetTrainJaioluOfDir(string DirTrainJiaoluGUID)
        {
            string strSql = @"select * from VIEW_Base_TrainJiaolu where strTrainJiaoluGUID in 
                    (select strSubTrainJiaoluGUID from TAB_Base_TrainJiaolu_SubDetail where strTrainJiaoluGUID=@strTrainJiaoluGUID) order by strTrainJiaoluName";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",DirTrainJiaoluGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }

        public static void UpdateSubTrainJiaolus(string TrainJiaoluGUID, string TrainJiaoluGUIDs)
        {
            string strSql = "delete TAB_Base_TrainJiaolu_SubDetail where strTrainJiaoluGUID = @strTrainJiaoluGUID";

            SqlParameter[] sqlParams = {
                                           new SqlParameter("strTrainJiaoluGUID",TrainJiaoluGUID)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

            string[] strTrainJiaoluGUIDArray = TrainJiaoluGUIDs.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strTrainJiaoluGUIDArray.Length; i++)
            {
                strSql = "insert into TAB_Base_TrainJiaolu_SubDetail (strTrainJiaoluGUID,strSubTrainJiaoluGUID) values (@strTrainJiaoluGUID,@strSubTrainJiaoluGUID)";
                SqlParameter[] sqlParamsAdd = {
                                           new SqlParameter("strTrainJiaoluGUID",TrainJiaoluGUID),
                                           new SqlParameter("strSubTrainJiaoluGUID",strTrainJiaoluGUIDArray[i])
                                       };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsAdd);
            }
        }
        #endregion
    }
}
