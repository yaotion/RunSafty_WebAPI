using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ThinkFreely.DBUtility;
using TF.CommonUtility;

namespace TF.RunSafty.Synchronization
{

    #region ===============================获取Base_Site===============================
    public class DBGetBase_Site
    {
        public List<Base_Site> GetBase_SiteList()
        {

            DataSet set = this.GetList();
            return DataTableToList(set.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from TAB_Base_Site");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Base_Site> DataTableToList(DataTable dt)
        {
            List<Base_Site> modelList = new List<Base_Site>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Base_Site model;
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Base_Site DataRowToModel(DataRow dr)
        {
            Base_Site model = new Base_Site();
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
    }
    #endregion

    #region ===============================获取Base_Site_Limit===============================
    public class DBGetBase_Site_Limit
    {
        public List<Base_Site_Limit> GetBase_Site_LimitList()
        {

            DataSet set = this.GetList();
            return DataTableToList(set.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from TAB_Base_Site_Limit");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Base_Site_Limit> DataTableToList(DataTable dt)
        {
            List<Base_Site_Limit> modelList = new List<Base_Site_Limit>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Base_Site_Limit model;
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Base_Site_Limit DataRowToModel(DataRow dr)
        {
            Base_Site_Limit model = new Base_Site_Limit();
            if (dr != null)
            {
                model.strSiteGUID = ObjectConvertClass.static_ext_string(dr["strSiteGUID"]);
                model.nJobID = ObjectConvertClass.static_ext_int(dr["nJobID"]);
                model.nJobLimit = ObjectConvertClass.static_ext_int(dr["nJobLimit"]);
            }
            return model;
        }
    }
    #endregion

    #region ===============================获取Base_Station===============================
    public class DBGetBase_Station
    {
        public List<Base_Station> GetBase_StationList()
        {

            DataSet set = this.GetList();
            return DataTableToList(set.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from TAB_Base_Station");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Base_Station> DataTableToList(DataTable dt)
        {
            List<Base_Station> modelList = new List<Base_Station>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Base_Station model;
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Base_Station DataRowToModel(DataRow dr)
        {
            Base_Station model = new Base_Station();
            if (dr != null)
            {
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.strStationGUID = ObjectConvertClass.static_ext_string(dr["strStationGUID"]);
                model.strStationNumber = ObjectConvertClass.static_ext_int(dr["strStationNumber"]);
                model.strStationName = ObjectConvertClass.static_ext_string(dr["strStationName"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
                model.strStationPY = ObjectConvertClass.static_ext_string(dr["strStationPY"]);
            }
            return model;
        }
    }
    #endregion

    #region ===============================获取DBGetBase_TrainJiaolu===============================
    public class DBGetBase_TrainJiaolu
    {
        public List<Base_TrainJiaolu> GetTrainJiaoLuList()
        {

            DataSet set = this.GetList();
            return DataTableToList(set.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from TAB_Base_TrainJiaolu");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Base_TrainJiaolu> DataTableToList(DataTable dt)
        {
            List<Base_TrainJiaolu> modelList = new List<Base_TrainJiaolu>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Base_TrainJiaolu model;
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Base_TrainJiaolu DataRowToModel(DataRow dr)
        {
            Base_TrainJiaolu model = new Base_TrainJiaolu();
            if (dr != null)
            {
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
                model.strTrainJiaoluName = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluName"]);
                model.strStartStation = ObjectConvertClass.static_ext_string(dr["strStartStation"]);
                model.strEndStation = ObjectConvertClass.static_ext_string(dr["strEndStation"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
                model.bIsBeginWorkFP = ObjectConvertClass.static_ext_int(dr["bIsBeginWorkFP"]);
                model.bIsDir = ObjectConvertClass.static_ext_int(dr["bIsDir"]);
                model.nWorkTimeTypeID = ObjectConvertClass.static_ext_int(dr["nWorkTimeTypeID"]);
            }
            return model;
        }



    }
    #endregion


    #region ===============================获取Base_TrainJiaoluInSite===============================
    public class DBGetBase_TrainJiaoluInSite
    {
        public List<Base_TrainJiaoluInSite> GetTrainJiaoluInSite()
        {

            DataSet set = this.GetList();
            return DataTableToList(set.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from TAB_Base_TrainJiaoluInSite");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Base_TrainJiaoluInSite> DataTableToList(DataTable dt)
        {
            List<Base_TrainJiaoluInSite> modelList = new List<Base_TrainJiaoluInSite>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Base_TrainJiaoluInSite model;
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Base_TrainJiaoluInSite DataRowToModel(DataRow dr)
        {
            Base_TrainJiaoluInSite model = new Base_TrainJiaoluInSite();
            if (dr != null)
            {
                model.strSiteGUID = ObjectConvertClass.static_ext_string(dr["strSiteGUID"]);
                model.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
                model.strJiaoluInSiteGUID = ObjectConvertClass.static_ext_string(dr["strJiaoluInSiteGUID"]);
            }
            return model;
        }



    }
    #endregion


    #region ===============================获取DBGetBase_ZFQJ===============================
    public class DBGetBase_ZFQJ
    {
        public List<Base_ZFQJ> GetZFQJ()
        {

            DataSet set = this.GetList();
            return DataTableToList(set.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from TAB_Base_ZFQJ");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Base_ZFQJ> DataTableToList(DataTable dt)
        {
            List<Base_ZFQJ> modelList = new List<Base_ZFQJ>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Base_ZFQJ model;
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Base_ZFQJ DataRowToModel(DataRow dr)
        {
            Base_ZFQJ model = new Base_ZFQJ();
            if (dr != null)
            {
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.strZFQJGUID = ObjectConvertClass.static_ext_string(dr["strZFQJGUID"]);
                model.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
                model.nQuJianIndex = ObjectConvertClass.static_ext_int(dr["nQuJianIndex"]);
                model.strBeginStationGUID = ObjectConvertClass.static_ext_string(dr["strBeginStationGUID"]);
                model.strEndStationGUID = ObjectConvertClass.static_ext_string(dr["strEndStationGUID"]);
                model.nSortid = ObjectConvertClass.static_ext_int(dr["nSortid"]);
            }
            return model;
        }



    }
    #endregion


    #region ===============================获取DBGetOrg_Area===============================
    public class DBGetOrg_Area
    {
        public List<Org_Area> GetOrg_Area()
        {

            DataSet set = this.GetList();
            return DataTableToList(set.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from TAB_Org_Area");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Org_Area> DataTableToList(DataTable dt)
        {
            List<Org_Area> modelList = new List<Org_Area>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Org_Area model;
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Org_Area DataRowToModel(DataRow dr)
        {
            Org_Area model = new Org_Area();
            if (dr != null)
            {
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.strGUID = ObjectConvertClass.static_ext_string(dr["strGUID"]);
                model.strAreaName = ObjectConvertClass.static_ext_string(dr["strAreaName"]);
                model.strJWDNumber = ObjectConvertClass.static_ext_string(dr["strJWDNumber"]);
            }
            return model;
        }



    }
    #endregion


    #region ===============================获取DBGetOrg_DutyUser===============================
    public class DBGetOrg_DutyUser
    {
        public List<Org_DutyUser> GetOrg_DutyUser()
        {

            DataSet set = this.GetList();
            return DataTableToList(set.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from TAB_Org_DutyUser");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Org_DutyUser> DataTableToList(DataTable dt)
        {
            List<Org_DutyUser> modelList = new List<Org_DutyUser>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Org_DutyUser model;
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Org_DutyUser DataRowToModel(DataRow dr)
        {
            Org_DutyUser model = new Org_DutyUser();
            if (dr != null)
            {
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.strDutyGUID = ObjectConvertClass.static_ext_string(dr["strDutyGUID"]);
                model.strDutyNumber = ObjectConvertClass.static_ext_string(dr["strDutyNumber"]);
                model.strDutyName = ObjectConvertClass.static_ext_string(dr["strDutyName"]);
                model.strPassword = ObjectConvertClass.static_ext_string(dr["strPassword"]);
                model.nDeleteState = ObjectConvertClass.static_ext_int(dr["nDeleteState"]);
                model.strAreaGUID = ObjectConvertClass.static_ext_string(dr["strAreaGUID"]);
                model.nRoleID = ObjectConvertClass.static_ext_int(dr["nRoleID"]);
                model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);
                model.strTokenID = ObjectConvertClass.static_ext_string(dr["strTokenID"]);
                model.dtTokenTime = ObjectConvertClass.static_ext_date(dr["dtTokenTime"]);
                model.dtLoginTime = ObjectConvertClass.static_ext_date(dr["dtLoginTime"]);
            }
            return model;
        }



    }
    #endregion



    #region ===============================获取Org_WorkShop===============================
    public class DBGetOrg_WorkShop
    {
        public List<Org_WorkShop> GetOrg_WorkShop()
        {

            DataSet set = this.GetList();
            return DataTableToList(set.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from TAB_Org_WorkShop");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Org_WorkShop> DataTableToList(DataTable dt)
        {
            List<Org_WorkShop> modelList = new List<Org_WorkShop>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Org_WorkShop model;
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Org_WorkShop DataRowToModel(DataRow dr)
        {
            Org_WorkShop model = new Org_WorkShop();
            if (dr != null)
            {
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
                model.strAreaGUID = ObjectConvertClass.static_ext_string(dr["strAreaGUID"]);
                model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
                model.strWorkShopNumber = ObjectConvertClass.static_ext_string(dr["strWorkShopNumber"]);
            }
            return model;
        }



    }
    #endregion


    #region ===============================获取Base_TrainNo===============================
    public class DBGetBase_TrainNo
    {
        public List<Base_TrainNo> GetBase_TrainNo()
        {

            DataSet set = this.GetList();
            return DataTableToList(set.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from TAB_Base_TrainNo where nNeedRest =1");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Base_TrainNo> DataTableToList(DataTable dt)
        {
            List<Base_TrainNo> modelList = new List<Base_TrainNo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Base_TrainNo model;
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Base_TrainNo DataRowToModel(DataRow dr)
        {
            Base_TrainNo model = new Base_TrainNo();
            if (dr != null)
            {
                model.strGUID = ObjectConvertClass.static_ext_string(dr["strGUID"]);
                model.strTrainTypeName = ObjectConvertClass.static_ext_string(dr["strTrainTypeName"]);
                model.strTrainNumber = ObjectConvertClass.static_ext_string(dr["strTrainNumber"]);
                model.strTrainNo = ObjectConvertClass.static_ext_string(dr["strTrainNo"]);
                model.dtStartTime = ObjectConvertClass.static_ext_date(dr["dtStartTime"]);
                model.dtRealStartTime = ObjectConvertClass.static_ext_date(dr["dtRealStartTime"]);
                model.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
                model.strStartStation = ObjectConvertClass.static_ext_string(dr["strStartStation"]);
                model.strEndStation = ObjectConvertClass.static_ext_string(dr["strEndStation"]);
                model.nTrainmanTypeID = ObjectConvertClass.static_ext_int(dr["nTrainmanTypeID"]);
                model.nPlanType = ObjectConvertClass.static_ext_int(dr["nPlanType"]);
                model.nDragType = ObjectConvertClass.static_ext_int(dr["nDragType"]);
                model.nKehuoID = ObjectConvertClass.static_ext_int(dr["nKehuoID"]);
                model.nRemarkType = ObjectConvertClass.static_ext_int(dr["nRemarkType"]);
                model.strRemark = ObjectConvertClass.static_ext_string(dr["strRemark"]);
                model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);
                model.strCreateSiteGUID = ObjectConvertClass.static_ext_string(dr["strCreateSiteGUID"]);
                model.strCreateUserGUID = ObjectConvertClass.static_ext_string(dr["strCreateUserGUID"]);
                model.strPlaceID = ObjectConvertClass.static_ext_string(dr["strPlaceID"]);
                model.dtPlanStartTime = ObjectConvertClass.static_ext_date(dr["dtPlanStartTime"]);
                model.nNeedRest = ObjectConvertClass.static_ext_int(dr["nNeedRest"]);
                model.dtArriveTime = ObjectConvertClass.static_ext_date(dr["dtArriveTime"]);
                model.dtCallTime = ObjectConvertClass.static_ext_date(dr["dtCallTime"]);
                model.strWorkDay = ObjectConvertClass.static_ext_string(dr["strWorkDay"]);
            }
            return model;
        }



    }
    #endregion
}
