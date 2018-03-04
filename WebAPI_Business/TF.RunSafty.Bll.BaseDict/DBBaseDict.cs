using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ThinkFreely.DBUtility;
using TF.CommonUtility;
using System.Data.SqlClient;
using Dapper;

namespace TF.RunSafty.BaseDict
{

    #region  获取车型

    public class CheXing
    {
        #region GetStationsOfJiaoJu方法（获取该交路下的所有的车站）
        public List<CheXin.CheXinInSite> GetCheXinBySite(string strSiteGuid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Tab_System_CheXin where  strSiteGUID = '" + strSiteGuid + "' ");
            return GetCheXinDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<CheXin.CheXinInSite> GetCheXinDTToList(DataTable dt)
        {
            List<CheXin.CheXinInSite> modelList = new List<CheXin.CheXinInSite>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                CheXin.CheXinInSite model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetStationsOfJiaoJuDRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public CheXin.CheXinInSite GetStationsOfJiaoJuDRToModel(DataRow dr)
        {
            CheXin.CheXinInSite model = new CheXin.CheXinInSite();
            if (dr != null)
            {
                model.strTrainType = ObjectConvertClass.static_ext_string(dr["strTrainTypeName"]);
                model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
            }
            return model;
        }
        #endregion


    }



    #endregion



    public class DBStation
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







        #region GetStationsOfJiaoJu方法（获取该交路下的所有的车站）
        public List<StationInTrainJiaolu> GetStationsOfJiaoJu(string strTrainJiaoluGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Base_StationInTrainJiaolu where  strTrainJiaoluGUID = '" + strTrainJiaoluGUID + "' order by nSortID ");
            return GetStationsOfJiaoJuDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<StationInTrainJiaolu> GetStationsOfJiaoJuDTToList(DataTable dt)
        {
            List<StationInTrainJiaolu> modelList = new List<StationInTrainJiaolu>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                StationInTrainJiaolu model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetStationsOfJiaoJuDRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public StationInTrainJiaolu GetStationsOfJiaoJuDRToModel(DataRow dr)
        {
            StationInTrainJiaolu model = new StationInTrainJiaolu();
            if (dr != null)
            {
                model.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
                model.strStationGUID = ObjectConvertClass.static_ext_string(dr["strStationGUID"]);
                model.strStationNumber = ObjectConvertClass.static_ext_int(dr["strStationNumber"]);
                model.strStationName = ObjectConvertClass.static_ext_string(dr["strStationName"]);
                model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.nSortid = ObjectConvertClass.static_ext_int(dr["nSortid"]);
            }
            return model;
        }
        #endregion
    }

    public class DBTrainJiaolu : DBUtility
    {
        #region GetTrainJiaoluArrayOfSite方法（获取客户端下的所有行车区段）
        public List<TrainJiaoluInSite> GetTrainJiaoluArrayOfSite(string SiteGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Base_TrainJiaoluInSite where  strSiteGUID ='" + SiteGUID + "'");
            return GetTrainJiaoluArrayOfSiteDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<TrainJiaoluInSite> GetTrainJiaoluArrayOfSiteDTToList(DataTable dt)
        {
            List<TrainJiaoluInSite> modelList = new List<TrainJiaoluInSite>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TrainJiaoluInSite model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetTrainJiaoluArrayOfSiteDRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public TrainJiaoluInSite GetTrainJiaoluArrayOfSiteDRToModel(DataRow dr)
        {
            TrainJiaoluInSite model = new TrainJiaoluInSite();
            if (dr != null)
            {
                model.strSiteName = ObjectConvertClass.static_ext_string(dr["strSiteName"]);
                model.strSiteNumber = ObjectConvertClass.static_ext_string(dr["strSiteNumber"]);
                model.strSiteGUID = ObjectConvertClass.static_ext_string(dr["strSiteGUID"]);
                model.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
                model.strTrainJiaoluName = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluName"]);
                model.strStartStation = ObjectConvertClass.static_ext_string(dr["strStartStation"]);
                model.strEndStation = ObjectConvertClass.static_ext_string(dr["strEndStation"]);
                model.strAreaGUID = ObjectConvertClass.static_ext_string(dr["strAreaGUID"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
                model.bIsBeginWorkFP = ObjectConvertClass.static_ext_int(dr["bIsBeginWorkFP"]);
                model.strStartStationName = ObjectConvertClass.static_ext_string(dr["strStartStationName"]);
                model.strEndStationName = ObjectConvertClass.static_ext_string(dr["strEndStationName"]);
                model.Expr1 = ObjectConvertClass.static_ext_string(dr["Expr1"]);
                model.strJiaoluInSiteGUID = ObjectConvertClass.static_ext_string(dr["strJiaoluInSiteGUID"]);
                model.bIsDir = ObjectConvertClass.static_ext_int(dr["bIsDir"]);
            }
            return model;
        }
        #endregion

        #region GetTrainJiaoluArrayOfWorkShop方法（获取该车间下的所有行车区段）
        public List<TrainJiaolu> GetTrainJiaoluArrayOfWorkShop(string strWorkShopGUID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Base_TrainJiaolu where  strWorkShopGUID ='" + strWorkShopGUID + "'");
            return GetTrainJiaoluArrayOfWorkShopDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<TrainJiaolu> GetTrainJiaoluArrayOfWorkShopDTToList(DataTable dt)
        {
            List<TrainJiaolu> modelList = new List<TrainJiaolu>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TrainJiaolu model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetTrainJiaoluArrayOfWorkShopDRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public TrainJiaolu GetTrainJiaoluArrayOfWorkShopDRToModel(DataRow dr)
        {
            TrainJiaolu model = new TrainJiaolu();
            if (dr != null)
            {
                model.strStartStationNumber = ObjectConvertClass.static_ext_int(dr["strStartStationNumber"]);
                model.strStartStationName = ObjectConvertClass.static_ext_string(dr["strStartStationName"]);
                model.strEndStationNumber = ObjectConvertClass.static_ext_int(dr["strEndStationNumber"]);
                model.strEndStationName = ObjectConvertClass.static_ext_string(dr["strEndStationName"]);
                model.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
                model.strTrainJiaoluName = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluName"]);
                model.strStartStation = ObjectConvertClass.static_ext_string(dr["strStartStation"]);
                model.strEndStation = ObjectConvertClass.static_ext_string(dr["strEndStation"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
                model.bIsBeginWorkFP = ObjectConvertClass.static_ext_int(dr["bIsBeginWorkFP"]);
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
                model.ZfqjCount = ObjectConvertClass.static_ext_int(dr["ZfqjCount"]);
                model.bIsDir = ObjectConvertClass.static_ext_int(dr["bIsDir"]);
            }
            return model;
        }
        #endregion

        #region AllTrainJiaolu方法（获取所有的交路信息）
        public List<TrainJiaolu> AllTrainJiaolu()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Base_TrainJiaolu");
            return AllTrainJiaoluDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }

        public List<TrainJiaolu> AllTrainJiaoluDTToList(DataTable dt)
        {
            List<TrainJiaolu> modelList = new List<TrainJiaolu>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TrainJiaolu model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = AllTrainJiaoluDRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public TrainJiaolu AllTrainJiaoluDRToModel(DataRow dr)
        {
            TrainJiaolu model = new TrainJiaolu();
            if (dr != null)
            {
                model.strStartStationNumber = ObjectConvertClass.static_ext_int(dr["strStartStationNumber"]);
                model.strStartStationName = ObjectConvertClass.static_ext_string(dr["strStartStationName"]);
                model.strEndStationNumber = ObjectConvertClass.static_ext_int(dr["strEndStationNumber"]);
                model.strEndStationName = ObjectConvertClass.static_ext_string(dr["strEndStationName"]);
                model.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
                model.strTrainJiaoluName = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluName"]);
                model.strStartStation = ObjectConvertClass.static_ext_string(dr["strStartStation"]);
                model.strEndStation = ObjectConvertClass.static_ext_string(dr["strEndStation"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
                model.bIsBeginWorkFP = ObjectConvertClass.static_ext_int(dr["bIsBeginWorkFP"]);
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
                model.ZfqjCount = ObjectConvertClass.static_ext_int(dr["ZfqjCount"]);
                model.bIsDir = ObjectConvertClass.static_ext_int(dr["bIsDir"]);
            }
            return model;
        }
        #endregion

        #region GetTrainJiaoluGUIDByName通过交路名称获取交路GUID
        public string GetTrainJiaoluGUIDByName(string TrainJiaoluName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select strTrainJiaoluGUID from TAB_Base_TrainJiaolu where  strTrainJiaoluName ='" + TrainJiaoluName + "'");

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["strTrainJiaoluGUID"].ToString();

            }
            else
            {
                return "";
            }
        }
        #endregion

        #region GetTrainJiaolu通过交路的GUID获取一条交路的信息
        public List<TrainJiaolu> GetTrainJiaolu(string strTrainJiaoluGUID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Base_TrainJiaolu where  strTrainJiaoluGUID ='" + strTrainJiaoluGUID + "'");
            return GetTrainJiaoluDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<TrainJiaolu> GetTrainJiaoluDTToList(DataTable dt)
        {
            List<TrainJiaolu> modelList = new List<TrainJiaolu>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TrainJiaolu model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetTrainJiaoluDRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public TrainJiaolu GetTrainJiaoluDRToModel(DataRow dr)
        {
            TrainJiaolu model = new TrainJiaolu();
            if (dr != null)
            {
                model.strStartStationNumber = ObjectConvertClass.static_ext_int(dr["strStartStationNumber"]);
                model.strStartStationName = ObjectConvertClass.static_ext_string(dr["strStartStationName"]);
                model.strEndStationNumber = ObjectConvertClass.static_ext_int(dr["strEndStationNumber"]);
                model.strEndStationName = ObjectConvertClass.static_ext_string(dr["strEndStationName"]);
                model.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
                model.strTrainJiaoluName = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluName"]);
                model.strStartStation = ObjectConvertClass.static_ext_string(dr["strStartStation"]);
                model.strEndStation = ObjectConvertClass.static_ext_string(dr["strEndStation"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
                model.bIsBeginWorkFP = ObjectConvertClass.static_ext_int(dr["bIsBeginWorkFP"]);
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
                model.ZfqjCount = ObjectConvertClass.static_ext_int(dr["ZfqjCount"]);
                model.bIsDir = ObjectConvertClass.static_ext_int(dr["bIsDir"]);
            }
            return model;
        }
        #endregion

        #region IsJiaoLuInSite（判断交路是否属于客户端管辖）
        public bool IsJiaoLuInSite(string TrainJiaoluGUID, string SiteGUID)
        {


            StringBuilder strSql = new StringBuilder();
            strSql.Append("select strTrainJiaoluGUID  from TAB_Base_TrainJiaoluInSite where strSiteGUID = '" + SiteGUID + "' and strTrainJiaoluGUID='" + TrainJiaoluGUID + "' union ");
            strSql.Append("select strSubTrainJiaoLuGUID from TAB_Base_TrainJiaolu_SubDetail where strTrainJiaoluGUID in ");
            strSql.Append("(select strTrainJiaoluGUID  from TAB_Base_TrainJiaoluInSite where strSiteGUID ='" + SiteGUID + "' and strSubTrainJiaoLuGUID ='" + TrainJiaoluGUID + "')");
            strSql.Append(" union select strTrainJiaoluGUID as strTrainJiaoluGUID from TAB_Base_TrainJiaolu_SubDetail where strSubTrainJiaoLuGUID in (select strTrainJiaoluGUID  from TAB_Base_TrainJiaoluInSite where strSiteGUID =  '" + SiteGUID + "' ) and strTrainJiaoluGUID = '" + TrainJiaoluGUID + "'");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0].Rows.Count > 0;

        }
        #endregion

        #region 通过客户端编号  获取该客户端下所有的行车区段信息


        public class TmJL
        {
            public string strTrainmanJiaoluGUID;
            public string strTrainmanJiaoluName;
            public int nJiaoluType;
        }

        public class Place
        {
            public string strPlaceID;
            public string strPlaceName;
        }

        /// <summary>
        /// 获取客户端管辖下的人员区段
        /// </summary>
        /// <param name="SiteNumber"></param>
        /// <returns></returns>
        public List<TmJL> getList_TMJL(string SiteNumber)
        {
            string sql = @"select distinct(r.strTrainmanJiaoluGUID),b.nJiaoluType,b.strTrainmanJiaoluName  FROM [TAB_Base_JiaoluRelation] r left join
                        TAB_Base_TrainmanJiaolu b  on r.strTrainmanJiaoluGUID=b.strTrainmanJiaoluGUID  where r.strTrainJiaoluGUID in  
                        ( select s.strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite s where
                          strSiteGUID=(select strSiteGUID from TAB_Base_Site where strSiteIP=@SiteNumber)) and b.strTrainmanJiaoluGUID is not null";
            using (var conn = GetConnection())
            {
                return conn.Query<TmJL>(sql, new { SiteNumber = SiteNumber }).ToList();
            }
        }




        /// <summary>
        /// 获取人员区段下的所有出勤点信息
        /// </summary>
        /// <param name="strTrainmanJiaoluGUID"></param>
        /// <returns></returns>
        public List<Place> getList_Place(string strTrainmanJiaoluGUID)
        {
            string sql = @"select distinct(d.strPlaceID),dp.strPlaceName  from TAB_Base_TrainJiaolu_DutyPlace d left join TAB_Base_DutyPlace dp
                    on d.strPlaceID= dp.strPlaceID  where strTrainJiaoluGUID in(
                    select strTrainJiaoluGUID FROM [TAB_Base_JiaoluRelation] where strTrainmanJiaoluGUID=@strTrainmanJiaoluGUID
                    )";
            using (var conn = GetConnection())
            {
                return conn.Query<Place>(sql, new { strTrainmanJiaoluGUID = strTrainmanJiaoluGUID }).ToList();
            }
        }






        #endregion




    }

    public class DBTrainManJiaoLu
    {

        #region GetTrainmanJiaolusOfTrainJiaoluEx 人员交路
        public List<TrainManJiaoLu> GetTrainmanJiaolusOfTrainJiaoluEx(string SiteGUID, string TrainJiaoluGUID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Base_TrainmanJiaolu where strTrainmanJiaoluGUID in (select DISTINCT strTrainmanJiaoluGUID from VIEW_Base_JiaoluRelation where 1=1");

            if (TrainJiaoluGUID != "")
            {
                strSql.Append(" and strTrainJiaoluGUID in (select '" + TrainJiaoluGUID + "' as strTrainJiaoluGUID union select strSubTrainJiaoluGUID");
                strSql.Append(" as strTrainJiaoluGUID from TAB_Base_TrainJiaolu_SubDetail where strTrainJiaoluGUID='" + TrainJiaoluGUID + "')");
            }
            else
            {
                strSql.Append(" and strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = '" + SiteGUID + "')");
            }
            strSql.Append(") order by strTrainmanJiaoluName");
            return DataTableToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }

        public List<TrainManJiaoLu> DataTableToList(DataTable dt)
        {
            List<TrainManJiaoLu> modelList = new List<TrainManJiaoLu>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TrainManJiaoLu model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public TrainManJiaoLu DataRowToModel(DataRow dr)
        {
            TrainManJiaoLu model = new TrainManJiaoLu();
            if (dr != null)
            {
                model.strTrainmanJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanJiaoluGUID"]);
                model.strTrainmanJiaoluName = ObjectConvertClass.static_ext_string(dr["strTrainmanJiaoluName"]);
                model.nJiaoluType = ObjectConvertClass.static_ext_int(dr["nJiaoluType"]);
                model.strJiaoluTypeName = ObjectConvertClass.static_ext_string(dr["strJiaoluTypeName"]);
                model.strLineGUID = ObjectConvertClass.static_ext_string(dr["strLineGUID"]);
                model.strLineName = ObjectConvertClass.static_ext_string(dr["strLineName"]);
                model.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
                model.strTrainJiaoluName = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluName"]);
                model.nKehuoID = ObjectConvertClass.static_ext_int(dr["nKehuoID"]);
                model.strKehuoName = ObjectConvertClass.static_ext_string(dr["strKehuoName"]);
                model.nDragTypeID = ObjectConvertClass.static_ext_int(dr["nDragTypeID"]);
                model.nTrainmanTypeID = ObjectConvertClass.static_ext_int(dr["nTrainmanTypeID"]);
                model.strTrainmanTypeName = ObjectConvertClass.static_ext_string(dr["strTrainmanTypeName"]);
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.nDragTypeName = ObjectConvertClass.static_ext_string(dr["nDragTypeName"]);
                model.nTrainmanRunType = ObjectConvertClass.static_ext_int(dr["nTrainmanRunType"]);
                model.strRunTypeName = ObjectConvertClass.static_ext_string(dr["strRunTypeName"]);
            }
            return model;
        }
        #endregion

        #region GetTrainmanJiaolusOfTrainJiaolu（获取机车交路下的人员交路信息）

        public List<TrainManJiaoluRelation> GetTrainmanJiaolusOfTrainJiaolu(string TrainJiaoluGUID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Base_JiaoluRelation where 1=1 ");

            if (TrainJiaoluGUID != "")
            {
                strSql.Append(" and strTrainJiaoluGUID = '" + TrainJiaoluGUID + "'");
            }
            strSql.Append(" order by strTrainmanJiaoluName");
            return DataTableToList2(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }

        public List<TrainManJiaoluRelation> DataTableToList2(DataTable dt)
        {
            List<TrainManJiaoluRelation> modelList = new List<TrainManJiaoluRelation>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TrainManJiaoluRelation model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel2(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public TrainManJiaoluRelation DataRowToModel2(DataRow dr)
        {
            TrainManJiaoluRelation model = new TrainManJiaoluRelation();
            if (dr != null)
            {
                model.strTrainmanJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanJiaoluGUID"]);
                model.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
                model.strTrainmanJiaoluName = ObjectConvertClass.static_ext_string(dr["strTrainmanJiaoluName"]);
                model.nJiaoluType = ObjectConvertClass.static_ext_int(dr["nJiaoluType"]);
                model.strJiaoluTypeName = ObjectConvertClass.static_ext_string(dr["strJiaoluTypeName"]);
                model.nDragTypeID = ObjectConvertClass.static_ext_int(dr["nDragTypeID"]);
                model.strKehuoName = ObjectConvertClass.static_ext_string(dr["strKehuoName"]);
                model.nTrainmanTypeID = ObjectConvertClass.static_ext_int(dr["nTrainmanTypeID"]);
                model.strTrainmanTypeName = ObjectConvertClass.static_ext_string(dr["strTrainmanTypeName"]);
                model.nDragTypeName = ObjectConvertClass.static_ext_string(dr["nDragTypeName"]);
                model.nKehuoID = ObjectConvertClass.static_ext_int(dr["nKehuoID"]);
                model.strStartStationNumber = ObjectConvertClass.static_ext_int(dr["strStartStationNumber"]);
                model.strStartStationName = ObjectConvertClass.static_ext_string(dr["strStartStationName"]);
                model.strEndStationNumber = ObjectConvertClass.static_ext_int(dr["strEndStationNumber"]);
                model.strEndStationName = ObjectConvertClass.static_ext_string(dr["strEndStationName"]);
                model.strTrainJiaoluName = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluName"]);
                model.strStartStation = ObjectConvertClass.static_ext_string(dr["strStartStation"]);
                model.strEndStation = ObjectConvertClass.static_ext_string(dr["strEndStation"]);
                model.bIsBeginWorkFP = ObjectConvertClass.static_ext_int(dr["bIsBeginWorkFP"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
                model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
                model.ZfqjCount = ObjectConvertClass.static_ext_int(dr["ZfqjCount"]);
                model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.nTrainmanRunType = ObjectConvertClass.static_ext_int(dr["nTrainmanRunType"]);
            }
            return model;
        }

        #endregion

        #region GetTrainmanJiaolusOfTrainJiaolu  override （获取指定区段内的人员交路信息）

        public List<TrainManJiaoluRelation> GetTrainmanJiaolusOfTrainJiaolu(string SiteGUID, string TrainJiaoluGUID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from VIEW_Base_JiaoluRelation where 1=1  ");

            if (TrainJiaoluGUID != "")
            {
                strSql.Append(" and strTrainJiaoluGUID = '" + TrainJiaoluGUID + "'");
            }
            else
            {
                strSql.Append(" and strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = " + TrainJiaoluGUID + ")");
            }

            strSql.Append(" order by strTrainmanJiaoluName");
            return DataTableToList2(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }


        #endregion

    }

    public class DBJWDCoding
    {
        #region GetBase_JWDCodingLuList 获取行车区段信息
        public List<JWDCoding> GetBase_JWDCodingLuList()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Tab_Base_JWDCoding where 1 = 1  order by nid ");
            return GetBase_JWDCodingLuListDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }

        public List<JWDCoding> GetBase_JWDCodingLuListDTToList(DataTable dt)
        {
            List<JWDCoding> modelList = new List<JWDCoding>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                JWDCoding model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetBase_JWDCodingLuListDRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public JWDCoding GetBase_JWDCodingLuListDRToModel(DataRow dr)
        {
            JWDCoding model = new JWDCoding();
            if (dr != null)
            {
                model.strGUID = ObjectConvertClass.static_ext_string(dr["strGUID"]);
                model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.strCode = ObjectConvertClass.static_ext_string(dr["strCode"]);
                model.strName = ObjectConvertClass.static_ext_string(dr["strName"]);
                model.strShortName = ObjectConvertClass.static_ext_string(dr["strShortName"]);
                model.strPinYinCode = ObjectConvertClass.static_ext_string(dr["strPinYinCode"]);
                model.strStatCode = ObjectConvertClass.static_ext_string(dr["strStatCode"]);
                model.strUserCode = ObjectConvertClass.static_ext_string(dr["strUserCode"]);
                model.strLJCode = ObjectConvertClass.static_ext_string(dr["strLJCode"]);
                model.dtLastModify = ObjectConvertClass.static_ext_date(dr["dtLastModify"]);
                model.bIsVisible = ObjectConvertClass.static_ext_int(dr["bIsVisible"]);
            }
            return model;
        }
        #endregion
    }

    public class DBGetPlanTimes
    {
        #region GetPlanTimes 获取配置信息
        public int GetPlanTimes(int nRemarkType, string strPlaceID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select nMinute from Tab_Base_ChuQinTimeRule where nRemarkType = " + nRemarkType + " and strPlaceID = '" + strPlaceID + "'");
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["nMinute"].ToString());

            }
            else
            {
                return 0;
            }
        }

        #endregion
    }

    public class DBOrg_GuideGroup
    {
        #region GetGuideGroupOfWorkShop（获取该车间下的所有指导组）
        public List<GuideGroup> GetGuideGroupOfWorkShop(string strWorkShopGUID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_Org_GuideGroup where strWorkShopGUID ='" + strWorkShopGUID + "' order by strGuideGroupName");
            return GetGuideGroupOfWorkShopDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<GuideGroup> GetGuideGroupOfWorkShopDTToList(DataTable dt)
        {
            List<GuideGroup> modelList = new List<GuideGroup>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                GuideGroup model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetGuideGroupOfWorkShopDRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public GuideGroup GetGuideGroupOfWorkShopDRToModel(DataRow dr)
        {
            GuideGroup model = new GuideGroup();
            if (dr != null)
            {
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.strGuideGroupGUID = ObjectConvertClass.static_ext_string(dr["strGuideGroupGUID"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
                model.strGuideGroupName = ObjectConvertClass.static_ext_string(dr["strGuideGroupName"]);
            }
            return model;
        }
        #endregion

        #region GetGuideGroupGUIDByName 根据指导队名称获取对应的GUID
        public string GetGuideGroupGUIDByName(string GuideGroupName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select strGuideGroupGUID from TAB_Org_GuideGroup where  strGuideGroupName='" + GuideGroupName + "'");

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["strGuideGroupGUID"].ToString();

            }
            else
            {
                return "";
            }
        }
        #endregion
    }

    public class DBSignType
    {

        #region GetSignType（查询常见意见）
        public List<SignType> GetSignType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_WorkTime_SignType order by nid");
            return GetSignTypeDTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<SignType> GetSignTypeDTToList(DataTable dt)
        {
            List<SignType> modelList = new List<SignType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SignType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetSignTypeDRToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public SignType GetSignTypeDRToModel(DataRow dr)
        {
            SignType model = new SignType();
            if (dr != null)
            {
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.strSignText = ObjectConvertClass.static_ext_string(dr["strSignText"]);
            }
            return model;
        }
        #endregion

        #region AddSignType（添加常见意见）
        public bool AddSignType(SignType model)
        {
            string strSql = "insert into TAB_WorkTime_SignType (strSignText) values (@strSignText)";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("strSignText",model.strSignText)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters) > 0;
        }
        #endregion

        #region DeleteSignType（删除常见意见）
        public bool DeleteSignType(SignType model)
        {
            string strSql = "delete from TAB_WorkTime_SignType where strSignText = @strSignText";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("strSignText",model.strSignText)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters) > 0;
        }
        #endregion

    }

    public class DBSystemDict
    {

        #region GetDrinkTypeArray方法（获取测酒类型数据）
        public List<DictTable> GetDrinkTypeArray()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_System_DrinkType order by nDrinkTypeID ");
            return GetDrinkTypeArray_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }

        public List<DictTable> GetDrinkTypeArray_DTToList(DataTable dt)
        {
            List<DictTable> modelList = new List<DictTable>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DictTable model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetDrinkTypeArray_DRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public DictTable GetDrinkTypeArray_DRToModelDTToList(DataRow dr)
        {
            DictTable model = new DictTable();
            if (dr != null)
            {
                model.TypeID = ObjectConvertClass.static_ext_int(dr["nDrinkTypeID"]);
                model.TypeName = ObjectConvertClass.static_ext_string(dr["nDrinkTypeName"]);
            }
            return model;
        }

        #endregion


        #region GetVerifyArray方法（获取验证方式数据）
        public List<DictTable> GetVerifyArray()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_System_Verify order by nVerifyID ");
            return GetVerifyArray_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }

        public List<DictTable> GetVerifyArray_DTToList(DataTable dt)
        {
            List<DictTable> modelList = new List<DictTable>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DictTable model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetVerifyArray_DRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public DictTable GetVerifyArray_DRToModelDTToList(DataRow dr)
        {
            DictTable model = new DictTable();
            if (dr != null)
            {
                model.TypeID = ObjectConvertClass.static_ext_int(dr["nVerifyID"]);
                model.TypeName = ObjectConvertClass.static_ext_string(dr["strVerifyName"]);
            }
            return model;
        }
        #endregion



        #region GetDrinkTypeArray方法（获取测酒类型数据）
        public List<DictTable> GetDrinkResult()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from TAB_System_DrinkResult order by nDrinkResult ");
            return GetDrinkResult_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }

        public List<DictTable> GetDrinkResult_DTToList(DataTable dt)
        {
            List<DictTable> modelList = new List<DictTable>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DictTable model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetDrinkResult_DRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public DictTable GetDrinkResult_DRToModelDTToList(DataRow dr)
        {
            DictTable model = new DictTable();
            if (dr != null)
            {
                model.TypeID = ObjectConvertClass.static_ext_int(dr["nDrinkResult"]);
                model.TypeName = ObjectConvertClass.static_ext_string(dr["strDrinkResultName"]);
            }
            return model;
        }

        #endregion

        #region 获取查询页配置
        public static List<EmbeddedPage> GetEmbeddedPages(int ClientJobType)
        {
            List<EmbeddedPage> result = new List<EmbeddedPage>();
            string sql = @"select * from TAB_System_EmbeddedPages where nEnable = 1 and (nClientJobType = {0} or nClientJobType is null)
            order by strCatalog,nOrder";

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, string.Format(sql, ClientJobType)).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                EmbeddedPage page = new EmbeddedPage();
                page.Caption = dr["strCaption"].ToString();
                page.URL = dr["strURL"].ToString();
                page.Catalog = dr["strCatalog"].ToString();                
                page.ClientJobType = Convert.ToInt32(dr["nClientJobType"]);
                result.Add(page);
            }

            return result;
        }
        #endregion
        
    }


    public class DBSite
    {

        #region GetSiteByRelationIP方法（根据编号获取映射的客户端的岗位信息）
        public Site GetSiteByRelationIP(string strSrcSiteIP, int nToSiteJob, out string strRealIP)
        {
            Site model;
            string strSql;
            strSql = "select strDstSiteIP from Tab_Base_SiteRelation where strSrcSiteIP = '" + strSrcSiteIP + "' and nToSiteJob = " + nToSiteJob + "";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count == 0)
                strRealIP = strSrcSiteIP;
            else
                strRealIP = dt.Rows[0]["strDstSiteIP"].ToString();

            model = GetSiteByIP(strRealIP);
            return model;
        }
        #endregion

        #region GetSiteByIP方法（根据编号获取岗位信息）
        public Site GetSiteByIP(string strSiteIP)
        {
            Site model = new Site();
            string strSql, strSiteGUID;
            strSql = "select * from TAB_Base_Site where strSiteIP = '" + strSiteIP + "'";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count == 0)
            {
                return new Site();
            }

            else
            {
                strSiteGUID = dt.Rows[0]["strSiteGUID"].ToString();
                model.nid = ObjectConvertClass.static_ext_int(dt.Rows[0]["nid"]);
                model.strSiteGUID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strSiteGUID"]);
                model.strSiteNumber = ObjectConvertClass.static_ext_string(dt.Rows[0]["strSiteNumber"]);
                model.strSiteName = ObjectConvertClass.static_ext_string(dt.Rows[0]["strSiteName"]);
                model.strAreaGUID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strAreaGUID"]);
                model.nSiteEnable = ObjectConvertClass.static_ext_int(dt.Rows[0]["nSiteEnable"]);
                model.strSiteIP = ObjectConvertClass.static_ext_string(dt.Rows[0]["strSiteIP"]);
                model.nSiteJob = ObjectConvertClass.static_ext_int(dt.Rows[0]["nSiteJob"]);
                model.strStationGUID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strStationGUID"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strWorkShopGUID"]);
                model.strTMIS = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTMIS"]);
            }
            strSql = "select * from TAB_Base_TrainJiaoluInSite where strSiteGUID = '" + strSiteGUID + "'";
            DataTable dt2 = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];

            List<string> LTrainJiaolus = new List<string>();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                LTrainJiaolus.Add(dt2.Rows[i]["strTrainJiaoluGUID"].ToString());
            }
            model.TrainJiaolus = LTrainJiaolus;


            strSql = "select * from TAB_Base_Site_Limit where strSiteGUID = '" + strSiteGUID + "'";
            DataTable dt3 = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];

            List<LJobLimits> LJobLimits = new List<LJobLimits>();

            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                LJobLimits ljl = new LJobLimits();
                ljl.Job = ObjectConvertClass.static_ext_int(dt3.Rows[i]["nJobID"]);
                ljl.Limimt = ObjectConvertClass.static_ext_int(dt3.Rows[i]["nJobLimit"]);
                LJobLimits.Add(ljl);
            }
            model.JobLimits = LJobLimits;
            return model;
        }
        #endregion

        #region GetSites方法（获取全部客户端）
        public List<Site> GetSites()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select * From TAB_Base_Site ");
            return GetSites_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }

        public List<Site> GetSites_DTToList(DataTable dt)
        {
            List<Site> modelList = new List<Site>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Site model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetSites_DRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public Site GetSites_DRToModelDTToList(DataRow dr)
        {
            Site model = new Site();
            if (dr != null)
            {
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.strSiteGUID = ObjectConvertClass.static_ext_string(dr["strSiteGUID"]);
                model.strSiteNumber = ObjectConvertClass.static_ext_string(dr["strSiteNumber"]);
                model.strSiteName = ObjectConvertClass.static_ext_string(dr["strSiteName"]);
                model.strAreaGUID = ObjectConvertClass.static_ext_string(dr["strAreaGUID"]);
                model.nSiteEnable = ObjectConvertClass.static_ext_int(dr["nSiteEnable"]);
                model.strSiteIP = ObjectConvertClass.static_ext_string(dr["strSiteIP"]);
                model.nSiteJob = ObjectConvertClass.static_ext_int(dr["nSiteJob"]);
                model.strStationGUID = ObjectConvertClass.static_ext_string(dr["strStationGUID"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
                model.strTMIS = ObjectConvertClass.static_ext_string(dr["strTMIS"]);
            }
            return model;
        }
        #endregion

    }



    public class DBGanBuType
    {
        private Boolean TypeIsExist(string TypeName,string WorkShopGUID)
        {
            string sql = "select count(*) from  TAB_Base_GanBuType where strWorkShopGUID = @workShopGUID and strTypeName = @typeName";
            SqlParameter[] param = { 
                                   new SqlParameter("typeName",TypeName),
                                   new SqlParameter("workShopGUID",WorkShopGUID)
                                   };

            return (Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, param)) > 0);

        }
        public GanBuType Get(int TypeID)
        {
            string sql = "select * from TAB_Base_GanBuType where nTypeID = @TypeID";
            SqlParameter[] param = { 
                                    new SqlParameter("TypeID",TypeID)                                   
                                   };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, param).Tables[0];
            if (dt.Rows.Count > 0)
            {
                GanBuType ganbuType = new GanBuType();

                ganbuType.TypeID = dt.Rows[0]["nTypeID"].ToString();
                ganbuType.TypeName = dt.Rows[0]["strTypeName"].ToString();
                ganbuType.WorkShopGUID = dt.Rows[0]["strWorkShopGUID"].ToString();
                return ganbuType;
            }
            else
            {
                return null;
            }
        }

        private int GetMaxOrder(string WorkShopGUID)
        {
            SqlParameter[] param = {                                    
                                   new SqlParameter("workShopGUID",WorkShopGUID)
                                   };
            string sql = "select Isnull(max(nOrder),0) from TAB_Base_GanBuType where strWorkShopGUID = @workShopGUID";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, param));
            
        }

        public void Add(GanBuType ganBuType)
        {            
            if (TypeIsExist(ganBuType.TypeName, ganBuType.WorkShopGUID))
            {
                throw new Exception(string.Format("{0}类型已经存在!", ganBuType.TypeName));
            }

            int nOrder = GetMaxOrder(ganBuType.WorkShopGUID) + 1;

            SqlParameter[] param = { 
                                   new SqlParameter("typeName",ganBuType.TypeName),
                                   new SqlParameter("workShopGUID",ganBuType.WorkShopGUID),
                                   new SqlParameter("nOrder",nOrder)
                                   };

            
            string sql = "insert into TAB_Base_GanBuType(strTypeName,strWorkShopGUID,nOrder) values(@typeName,@workShopGUID,@nOrder)";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, param);
        }


        public void Delete(int TypeID)
        {
            string sql = "delete from TAB_Base_GanBuType where nTypeID = @TypeID";
            SqlParameter[] param = { 
                                   new SqlParameter("TypeID",TypeID)                                   
                                   };


            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, param);
        }

        public void ExchangeOrder(string WorkShopGUID,int nTypeID1,int nTypeID2)
        {
            string sql = "Execute PROC_GanBuType_ExchangeOrder @workShopGUID,@TypeID1,@TypeID2";

            SqlParameter[] param = {                                 
                                    new SqlParameter("workShopGUID",WorkShopGUID),
                                    new SqlParameter("TypeID1",nTypeID1),                                   
                                    new SqlParameter("TypeID2",nTypeID2)                                   
                                   };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, param);
        }

        public void Update(GanBuType ganBuType)
        {
            if (TypeIsExist(ganBuType.TypeName, ganBuType.WorkShopGUID))
            {
                throw new Exception(string.Format("{0}类型已经存在!", ganBuType.TypeName));
            }
            string sql = "update TAB_Base_GanBuType set strTypeName = @TypeName,strWorkShopGUID = @workShopGUID where nTypeID = @TypeID";

            SqlParameter[] param = {
                                        new SqlParameter("TypeName",ganBuType.TypeName),
                                        new SqlParameter("workShopGUID",ganBuType.WorkShopGUID),
                                        new SqlParameter("TypeID",ganBuType.TypeID)                                   
                                   };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, param);
        }
        public List<GanBuType> Query(string workShopGUID)
        {
            List<GanBuType> result = new List<GanBuType>();
            string sql = "select * from TAB_Base_GanBuType where 1=1 ";
            if (!string.IsNullOrEmpty(workShopGUID))
            {
                sql += " and strWorkShopGUID = @workShopGUID";
            }
            sql += " order by nOrder";
             
            

            SqlParameter[] param = {                                    
                                   new SqlParameter("workShopGUID",workShopGUID)                                                                    
                                   };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, param).Tables[0];


            GanBuType ganbuType;
            string strSql = "select * from TAB_Org_WorkShop where nIsYunZhuan =1 and strWorkShopGUID='" + workShopGUID + "'";
            DataTable dtWorkShop = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, param).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ganbuType = new GanBuType();
                ganbuType.TypeID = "T-1";
                ganbuType.TypeName = "司机";
                ganbuType.WorkShopGUID = workShopGUID;
                result.Add(ganbuType);

                ganbuType = new GanBuType();
                ganbuType.TypeID = "T-2";
                ganbuType.TypeName = "副司机";
                ganbuType.WorkShopGUID = workShopGUID;
                result.Add(ganbuType);

                ganbuType = new GanBuType();
                ganbuType.TypeID = "T-3";
                ganbuType.TypeName = "学员";
                ganbuType.WorkShopGUID = workShopGUID;
                result.Add(ganbuType);
            }

           
            foreach (DataRow dr in dt.Rows)
            {
                ganbuType = new GanBuType();

                ganbuType.TypeID = dr["nTypeID"].ToString();
                ganbuType.TypeName = dr["strTypeName"].ToString();
                ganbuType.WorkShopGUID = dr["strWorkShopGUID"].ToString();
                result.Add(ganbuType);

            }


          


            return result;
        }
    }

    public class DBGanBu
    {
        private void DataRowToObj(DataRow dr, GanBu ganbu)
        {            
            ganbu.RecID = Convert.ToInt32(dr["nID"]);
            ganbu.TypeID = ObjectConvertClass.static_ext_string(dr["nTypeID"]);
            ganbu.TypeName = ObjectConvertClass.static_ext_string(dr["strTypeName"]);
            ganbu.WorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
            ganbu.TrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanID"]);
            ganbu.TrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
            ganbu.TrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
        }
        public GanBu Get(int RecID)
        {
            string sql = "select * from TAB_Org_GanBu where nID = @RecID";

            SqlParameter[] param = { 
                                   new SqlParameter("RecID",RecID)                                   
                                   };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, param).Tables[0];
            if (dt.Rows.Count > 0)
            {
                GanBu ganbu = new GanBu();
                DataRowToObj(dt.Rows[0],ganbu);                
                return ganbu;
            }
            else
            {
                return null;
            }
        }

        public void Add(GanBu ganBu)
        {
            string sql = @"select count(*) from TAB_Org_GanBu where strTrainmanID = @TrainmanGUID and strWorkShopGUID = @workShopGUID";

            

            SqlParameter[] param = { 
                                       new SqlParameter("TypeID",ganBu.TypeID),
                                       new SqlParameter("TrainmanGUID",ganBu.TrainmanGUID),
                                       new SqlParameter("TrainmanNumber",ganBu.TrainmanNumber),
                                       new SqlParameter("TrainmanName",ganBu.TrainmanName),
                                       new SqlParameter("TypeName",ganBu.TypeName),
                                       new SqlParameter("workShopGUID",ganBu.WorkShopGUID)
                                   };


            if (Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, param)) > 0)
            {
                throw new Exception(string.Format("干部[{0}]{1} 已经存在!", ganBu.TrainmanNumber, ganBu.TrainmanName));
            }
            

            sql = @"insert into TAB_Org_GanBu(nTypeID,strTrainmanID,strTrainmanNumber,strTrainmanName,strTypeName,strWorkShopGUID) 
                values(@TypeID,@TrainmanGUID,@TrainmanNumber,@TrainmanName,@TypeName,@workShopGUID)";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, param);
        }

        public void Delete(int RecID)
        {
            string sql = "delete from TAB_Org_GanBu where nID = @RecID";

            SqlParameter[] param = { 
                                   new SqlParameter("RecID",RecID)                                   
                                   };


            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, param);
        }


       


      


        public void Update(GanBu ganBu)
        {
            string sql = @"update TAB_Org_GanBu set nTypeID = @TypeID,strTrainmanID=@TrainmanGUID,strTrainmanNumber=@TrainmanNumber,
                strTrainmanName=@TrainmanName,strTypeName=@TypeName,strWorkShopGUID=@workShopGUID";

            SqlParameter[] param = { 
                                       new SqlParameter("TypeID",ganBu.TypeID),
                                       new SqlParameter("TrainmanGUID",ganBu.TrainmanGUID),
                                       new SqlParameter("TrainmanNumber",ganBu.TrainmanNumber),
                                       new SqlParameter("TrainmanName",ganBu.TrainmanName),
                                       new SqlParameter("TypeName",ganBu.TypeName),
                                       new SqlParameter("workShopGUID",ganBu.WorkShopGUID)
                                   };


            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, param);
        }

        public List<GanBu> Query(string workShopGUID,string TrainmanNumber,string TypeID)
        {
            List<GanBu> result = new List<GanBu>();
            string sql = @"select TAB_Org_GanBu.* from TAB_Org_GanBu inner join TAB_Base_GanBuType ON 
                TAB_Org_GanBu.nTypeID = TAB_Base_GanBuType.nTypeID where TAB_Org_GanBu.strWorkShopGUID = @workShopGUID ";

            if (!string.IsNullOrEmpty(TrainmanNumber))
            {
                sql = sql + " and strTrainmanNumber = @TrainmanNumber ";
            }

            if (TypeID != "0" && TypeID != "")
            {
                sql = sql + " and nTypeID = @TypeID ";
            }


            sql = sql + " order by TAB_Base_GanBuType.nOrder, strTrainmanNumber";


            SqlParameter[] param = {                                    
                                   new SqlParameter("workShopGUID",workShopGUID),
                                   new SqlParameter("TrainmanNumber",TrainmanNumber),
                                   new SqlParameter("TypeID",TypeID)
                                   };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, param).Tables[0];

            GanBu ganbu;
            foreach (DataRow dr in dt.Rows)
            {
                ganbu = new GanBu();
                DataRowToObj(dr,ganbu);
                result.Add(ganbu);
            }
            return result;
        }
    }


    public class DBDepartment
    {
        public class Department
        {
            public string ID;
            public string Name;
            public string DType;
        }

        public List<Department> GetDepartmentList()
        {
            List<Department> result = new List<Department>();
            string sql = "select * from View_Org_DepartMentAndWorkShop ";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];
            Department Department;
            foreach (DataRow dr in dt.Rows)
            {
                Department = new Department();
                Department.ID = dr["strWorkShopGUID"].ToString();
                Department.Name = dr["strWorkShopName"].ToString();
                Department.DType = dr["type"].ToString();
                result.Add(Department);
            }
            return result;
        }
    }


}