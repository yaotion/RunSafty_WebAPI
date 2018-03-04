using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;
using TF.CommonUtility;
using System.Data.SqlClient;

namespace TF.RunSafty.GoodsMgr
{
    public class DBGoodsMgr
    {

        #region GetGoodType方法（1.5.1获取物品类型）
        public List<LendingType> GetGoodType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select * from TAB_System_LendingType ");
            return GetGoodType_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<LendingType> GetGoodType_DTToList(DataTable dt)
        {
            List<LendingType> modelList = new List<LendingType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LendingType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetGoodType_DRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public LendingType GetGoodType_DRToModelDTToList(DataRow dr)
        {
            LendingType model = new LendingType();
            if (dr != null)
            {
                model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.nLendingTypeID = ObjectConvertClass.static_ext_int(dr["nLendingTypeID"]);
                model.strLendingTypeName = ObjectConvertClass.static_ext_string(dr["strLendingTypeName"]);
                model.strAlias = ObjectConvertClass.static_ext_string(dr["strAlias"]);
            }
            return model;
        }
        #endregion

        #region GetStateNames方法（1.5.2获取物品状态类型）
        public List<ReturnStateType> GetStateNames()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select * from TAB_System_ReturnStateType ");
            return GetStateNames_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<ReturnStateType> GetStateNames_DTToList(DataTable dt)
        {
            List<ReturnStateType> modelList = new List<ReturnStateType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ReturnStateType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetStateNames_DRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public ReturnStateType GetStateNames_DRToModelDTToList(DataRow dr)
        {
            ReturnStateType model = new ReturnStateType();
            if (dr != null)
            {
                model.nid = ObjectConvertClass.static_ext_int(dr["nid"]);
                model.nReturnStateID = ObjectConvertClass.static_ext_int(dr["nReturnStateID"]);
                model.strStateName = ObjectConvertClass.static_ext_string(dr["strStateName"]);
            }
            return model;
        }
        #endregion

        #region （1.5.3发放物品）
        public bool CheckLendAble(LendingInfoDetail lifd, string strWorkShop)
        {
            if (lifd.strLendingExInfo == 0)
            {
                return true;
            }
            string strSQL = "select TAB_LendingDetail.nid,TAB_Org_Trainman.strWorkShopGUID ";
            strSQL += " from TAB_LendingDetail Left Join TAB_Org_Trainman On ";
            strSQL += " TAB_LendingDetail.strBorrowTrainmanGUID = Tab_Org_Trainman.strTrainmanGUID ";
            strSQL += " where strWorkShopGUID = '" + strWorkShop + "' and strLendingExInfo = " + lifd.strLendingExInfo + " and nReturnState = 0 and nLendingType = " + lifd.nLendingType + "";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSQL.ToString()).Tables[0].Rows.Count > 0;
        }

        public bool IsGoodInRange(int nLendingType, int strLendingExInfo, string WorkShopGUID)
        {

            string strsql = "select top 1 * from Tab_Base_LendingManager where strWorkShopGUID = '" + WorkShopGUID + "' and ";
            strsql += " nLendingTypeID = " + nLendingType + " and " + strLendingExInfo + " between nStartCode and nStopCode ";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strsql.ToString()).Tables[0].Rows.Count > 0;

        }

        public void SendLendingInfo(string TrainmanGUID, string remark, List<LendingInfoDetail> lifd)
        {
            string strSql = "select strGUID from View_LendingInfo where strBorrowTrainmanGUID = '" + TrainmanGUID + "' and nReturnState = 0";
            string strGUID = "";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
            if (dt.Rows.Count == 0)
            {
                strGUID = Guid.NewGuid().ToString();
                InsertLendingInfo(remark, strGUID);
            }
            else
            {
                strGUID = dt.Rows[0]["strGUID"].ToString();
            }
            foreach (LendingInfoDetail l in lifd)
            {
                l.strLendingInfoGUID = strGUID;
            }

            AddLendingDetails(lifd);

        }

        public void AddLendingDetails(List<LendingInfoDetail> lifd)
        {
            if (lifd.Count <= 0)
            {
                return;
            }

            foreach (LendingInfoDetail l in lifd)
            {
                string strSql = "insert into TAB_LendingDetail(strGUID,dtBorrowTime,nBorrowLoginType,strBorrowTrainmanGUID,strLenderGUID,strLendingInfoGUID,nLendingType,strLendingExInfo,nReturnState,dtModifyTime)";
                strSql += " values(@strGUID,@dtBorrowTime,@nBorrowLoginType,@strBorrowTrainmanGUID,@strLenderGUID,@strLendingInfoGUID,@nLendingType,@strLendingExInfo,@nReturnState,@dtModifyTime)";
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                            new SqlParameter("@strGUID",l.strGUID),
                            new SqlParameter("@dtBorrowTime",l.dtBorrwoTime),
                            new SqlParameter("@nBorrowLoginType",l.nBorrowVerifyType),
                            new SqlParameter("@strBorrowTrainmanGUID",l.strTrainmanGUID),
                            new SqlParameter("@strLenderGUID",l.strLenderGUID),
                            new SqlParameter("@strLendingInfoGUID",l.strLendingInfoGUID),
                            new SqlParameter("@nLendingType",l.nLendingType),
                            new SqlParameter("strLendingExInfo",l.strLendingExInfo),
                            new SqlParameter("nReturnState",l.nReturnState),
                            new SqlParameter("dtModifyTime",l.dtModifyTime),
                    };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
            }
        }

        public void InsertLendingInfo(string strRemark, string strGUID)
        {
            string strSql = "insert into TAB_LendingManage(strGUID,strRemark) values ('" + strGUID + "','" + strRemark + "')";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql);
        }

        #endregion

        #region （1.5.4归还物品）
        public bool CheckReturnAble(string TrainmanGUID, LendingInfoDetail lifd)
        {
            string strSQL = "select * from View_LendingInfoDetail where strBorrowTrainmanGUID =  ";
            strSQL += " '" + TrainmanGUID + "' and nReturnState = 0 and nLendingType = " + lifd.nLendingType + " and strLendingExInfo = " + lifd.strLendingExInfo + " ";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSQL.ToString()).Tables[0];

            if (dt.Rows.Count > 0)
            {
                lifd.strGUID = dt.Rows[0]["strGUID"].ToString();
                lifd.strLendingInfoGUID = dt.Rows[0]["strLendingInfoGUID"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GiveBackDetail(List<LendingInfoDetail> lifd)
        {
            foreach (LendingInfoDetail l in lifd)
            {
                string UPDATE_SQL = "Update TAB_LendingDetail set dtModifyTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',nReturnState = 2 ";
                UPDATE_SQL += " ,dtGiveBackTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                UPDATE_SQL += ",nGiveBackLoginType = " + l.nGiveBackVerifyType + ",strGiveBackTrainmanGUID ='" + l.strGiveBackTrainmanGUID + "' where strGUID = '" + l.strGUID + "'";
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, UPDATE_SQL);
            }
        }
        public void UpdateLendingInfoRemark(string strGUID, string remark)
        {
            string strSQL = "update TAB_LendingManage set  strRemark = '" + remark + "' where strGUID = '" + strGUID + "'";
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSQL);
        }
        #endregion

        #region QueryRecord方法（1.5.5查询发放记录）
        public List<LendingInfo> QueryRecord(GoodsQueryParam GoodsQueryParam)
        {
            string strSqlWhere = " where 1=1 ";
            if (GoodsQueryParam.dtBeginTime != null && GoodsQueryParam.dtEndTime != null)
            {
                strSqlWhere += " and dtBorrowTime>='" + GoodsQueryParam.dtBeginTime + "' and dtBorrowTime<='" + GoodsQueryParam.dtEndTime + "'";
            }
            if (GoodsQueryParam.nReturnState > -1)
            {
                strSqlWhere += " and (nReturnState = " + GoodsQueryParam.nReturnState + ") ";
            }
            if (GoodsQueryParam.nLendingType > -1)
            {
                strSqlWhere += " and (" + GoodsQueryParam.nLendingType + " in (select nLendingType from TAB_LendingDetail where strLendingInfoGUID = View_LendingInfo.strGUID) )";
            }

            if (GoodsQueryParam.strTrainmanNumber != null && GoodsQueryParam.strTrainmanNumber.ToString() != "")
            {
                strSqlWhere += " and (strBorrowTrainmanNumber = '" + GoodsQueryParam.strTrainmanNumber + "') ";
            }

            if (GoodsQueryParam.strTrainmanName != null && GoodsQueryParam.strTrainmanName.ToString() != "")
            {
                strSqlWhere += " and (strBorrowTrainmanName = '" + GoodsQueryParam.strTrainmanName + "') ";
            }
            if (GoodsQueryParam.strWorkShopGUID != null && GoodsQueryParam.strWorkShopGUID.ToString() != "")
            {
                strSqlWhere += " and (strWorkShopGUID = '" + GoodsQueryParam.strWorkShopGUID + "') ";
            }
            if (GoodsQueryParam.nLendingNumber > -1)
            {
                strSqlWhere += " and (strGUID in (select strLendingInfoGUID from TAB_LendingDetail where strLendingExInfo = " + GoodsQueryParam.nLendingNumber + ") ) ";
            }
            string strSql = "Select * from View_LendingInfo " + strSqlWhere + " order by dtBorrowTime desc";
            return QueryRecord_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<LendingInfo> QueryRecord_DTToList(DataTable dt)
        {
            List<LendingInfo> modelList = new List<LendingInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LendingInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = QueryRecord_DRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public LendingInfo QueryRecord_DRToModelDTToList(DataRow dr)
        {
            LendingInfo model = new LendingInfo();
            if (dr != null)
            {
                model.strStateName = ObjectConvertClass.static_ext_string(dr["strStateName"]);
                model.strGiveBackLoginName = ObjectConvertClass.static_ext_string(dr["strGiveBackLoginTypeName"]);
                model.strRemark = ObjectConvertClass.static_ext_string(dr["strRemark"]);
                model.strBorrowLoginName = ObjectConvertClass.static_ext_string(dr["strBorrowLoginTypeName"]);
                model.strGiveBackTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strGiveBackTrainmanNumber"]);
                model.strLenderName = ObjectConvertClass.static_ext_string(dr["strLenderName"]);
                model.strGiveBackTrainmanName = ObjectConvertClass.static_ext_string(dr["strGiveBackTrainmanName"]);
                model.strLenderNumber = ObjectConvertClass.static_ext_string(dr["strLenderNumber"]);
                model.strBorrowTrainmanName = ObjectConvertClass.static_ext_string(dr["strBorrowTrainmanName"]);
                model.strBorrowTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strBorrowTrainmanNumber"]);
                model.strGiveBackTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strGiveBackTrainmanGUID"]);
                model.strBorrowTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strBorrowTrainmanGUID"]);
                model.strLenderGUID = ObjectConvertClass.static_ext_string(dr["strLenderGUID"]);
                model.strGUID = ObjectConvertClass.static_ext_string(dr["strGUID"]);
                model.nReturnState = ObjectConvertClass.static_ext_int(dr["nReturnState"]);
                model.strDetails = ObjectConvertClass.static_ext_string(dr["strLendingDetail"]);
                model.strLendingInfoGUID = ObjectConvertClass.static_ext_string(dr["strLendingInfoGUID"]);
                model.dtModifyTime = ObjectConvertClass.static_ext_date(dr["dtModifyTime"]);
                model.dtGiveBackTime = ObjectConvertClass.static_ext_date(dr["dtGiveBackTime"]);
                model.nBorrowLoginType = ObjectConvertClass.static_ext_int(dr["nBorrowLoginType"]);
                model.dtBorrowTime = ObjectConvertClass.static_ext_date(dr["dtBorrowTime"]);
                model.nGiveBackLoginType = ObjectConvertClass.static_ext_int(dr["nGiveBackLoginType"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
            }
            return model;
        }
        #endregion

        #region QueryGoodsNow方法（1.5.6查询物品最新情况已借出则显示借出情况，已归还仅显示物品情况）
        public List<LendingInfoDetail> QueryGoodsNow(string WorkShopGUID, int GoodType, int GoodID, int orderType)
        {
            string strSqlWhere = " select * from VIEW_Lending_Last where 1=1 ";
            if (WorkShopGUID != null && WorkShopGUID != "")
            {
                strSqlWhere += " and strWorkShopGUID='" + WorkShopGUID + "'";
            }
            if (GoodType >= 0)
            {
                strSqlWhere += " and nLendingType = " + GoodType + " ";
            }
            if (GoodID != -1)
            {
                strSqlWhere += " and strLendingExInfo =" + GoodID + "";
            }

            if (orderType == 1)
            {
                strSqlWhere += " order by cast (strLendingExInfo as int) ";
            }
            else
            {
                strSqlWhere += "  order by nReturnState,dtBorrowTime  ";
            }


            return QueryGoodsNow_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlWhere.ToString()).Tables[0]);
        }
        public List<LendingInfoDetail> QueryGoodsNow_DTToList(DataTable dt)
        {
            List<LendingInfoDetail> modelList = new List<LendingInfoDetail>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LendingInfoDetail model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = QueryGoodsNow_DRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public LendingInfoDetail QueryGoodsNow_DRToModelDTToList(DataRow dr)
        {
            LendingInfoDetail model = new LendingInfoDetail();
            if (dr != null)
            {
                model.strLendingInfoGUID = ObjectConvertClass.static_ext_string(dr["strLendingInfoGUID"]);
                model.nLendingType = ObjectConvertClass.static_ext_int(dr["nLendingType"]);
                model.strLendingExInfo = ObjectConvertClass.static_ext_int(dr["strLendingExInfo"]);
                model.nReturnState = ObjectConvertClass.static_ext_int(dr["nReturnState"]);
                model.dtModifyTime = ObjectConvertClass.static_ext_date(dr["dtModifyTime"]);
                model.strGUID = ObjectConvertClass.static_ext_string(dr["strGUID"]);
                model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.dtBorrwoTime = ObjectConvertClass.static_ext_date(dr["dtBorrowTime"]);
                model.dtGiveBackTime = ObjectConvertClass.static_ext_date(dr["dtGiveBackTime"]);
                model.nBorrowVerifyType = ObjectConvertClass.static_ext_int(dr["nBorrowLoginType"]);
                model.nGiveBackVerifyType = ObjectConvertClass.static_ext_int(dr["nGiveBackLoginType"]);
                model.strLenderGUID = ObjectConvertClass.static_ext_string(dr["strLenderGUID"]);
                model.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strBorrowTrainmanGUID"]);
                model.strGiveBackTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strGiveBackTrainmanGUID"]);
                model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strBorrowTrainmanNumber"]);
                model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strBorrowTrainmanName"]);
                model.strLenderNumber = ObjectConvertClass.static_ext_string(dr["strLenderNumber"]);
                model.strLenderName = ObjectConvertClass.static_ext_string(dr["strLenderName"]);
                model.strGiveBackTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strGiveBackTrainmanNumber"]);
                model.strGiveBackTrainmanName = ObjectConvertClass.static_ext_string(dr["strGiveBackTrainmanName"]);
                model.strBorrowVerifyTypeName = ObjectConvertClass.static_ext_string(dr["strBorrowLoginTypeName"]);
                model.strGiveBackVerifyTypeName = ObjectConvertClass.static_ext_string(dr["strGiveBackLoginTypeName"]);
                model.strLendingTypeName = ObjectConvertClass.static_ext_string(dr["strLendingTypeName"]);
                model.strLendingTypeAlias = ObjectConvertClass.static_ext_string(dr["strAlias"]);
                model.strStateName = ObjectConvertClass.static_ext_string(dr["strStateName"]);
                model.nKeepMunites = ObjectConvertClass.static_ext_int(dr["nMinutes"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
            }
            return model;
        }
        #endregion

        #region QueryDetails方法（1.5.7查询发放明细）
        public List<LendingInfoDetail> QueryDetails(GoodsDetailQueryParam queryParam)
        {
            string strSqlWhere = " select * from View_LendingInfoDetail where 1=1 and strWorkShopGUID = '" + queryParam.workShopGUID + "' ";
            if (queryParam.nReturnState != -1)
            {
                strSqlWhere += " and nReturnState =" + queryParam.nReturnState + "";
            }
            if (queryParam.nLendingType != -1)
            {
                strSqlWhere += " and nLendingType =" + queryParam.nLendingType + "";
            }
            if (queryParam.strTrainmanNumber != null && queryParam.strTrainmanNumber != "")
            {
                strSqlWhere += " and strBorrowTrainmanNumber =" + queryParam.strTrainmanNumber + "";
            }
            if (queryParam.strTrainmanName != null && queryParam.strTrainmanName != "")
            {
                strSqlWhere += " and strBorrowTrainmanName =" + queryParam.strTrainmanName + "";
            }
            if (queryParam.nBianHao != -1)
            {
                strSqlWhere += " and strLendingExInfo =" + queryParam.nBianHao + "";
            }
            if ((queryParam.strOrderField == null) || (queryParam.strOrderField == ""))
            {
                strSqlWhere += " order by dtBorrowTime Desc ";
            }
            else
            {
                strSqlWhere += " order by " + queryParam.strOrderField + " Desc";
            }
            return QueryDetails_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlWhere.ToString()).Tables[0]);
        }
        public List<LendingInfoDetail> QueryDetails_DTToList(DataTable dt)
        {
            List<LendingInfoDetail> modelList = new List<LendingInfoDetail>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LendingInfoDetail model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = QueryDetails_DRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public LendingInfoDetail QueryDetails_DRToModelDTToList(DataRow dr)
        {
            LendingInfoDetail model = new LendingInfoDetail();
            if (dr != null)
            {
                model.strLendingInfoGUID = ObjectConvertClass.static_ext_string(dr["strLendingInfoGUID"]);
                model.nLendingType = ObjectConvertClass.static_ext_int(dr["nLendingType"]);
                model.strLendingExInfo = ObjectConvertClass.static_ext_int(dr["strLendingExInfo"]);
                model.nReturnState = ObjectConvertClass.static_ext_int(dr["nReturnState"]);
                model.dtModifyTime = ObjectConvertClass.static_ext_date(dr["dtModifyTime"]);
                model.strGUID = ObjectConvertClass.static_ext_string(dr["strGUID"]);
                model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.dtBorrwoTime = ObjectConvertClass.static_ext_date(dr["dtBorrowTime"]);
                model.dtGiveBackTime = ObjectConvertClass.static_ext_date(dr["dtGiveBackTime"]);
                model.nBorrowVerifyType = ObjectConvertClass.static_ext_int(dr["nBorrowLoginType"]);
                model.nGiveBackVerifyType = ObjectConvertClass.static_ext_int(dr["nGiveBackLoginType"]);
                model.strLenderGUID = ObjectConvertClass.static_ext_string(dr["strLenderGUID"]);
                model.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strBorrowTrainmanGUID"]);
                model.strGiveBackTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strGiveBackTrainmanGUID"]);
                model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strBorrowTrainmanNumber"]);
                model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strBorrowTrainmanName"]);
                model.strLenderNumber = ObjectConvertClass.static_ext_string(dr["strLenderNumber"]);
                model.strLenderName = ObjectConvertClass.static_ext_string(dr["strLenderName"]);
                model.strGiveBackTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strGiveBackTrainmanNumber"]);
                model.strGiveBackTrainmanName = ObjectConvertClass.static_ext_string(dr["strGiveBackTrainmanName"]);
                model.strBorrowVerifyTypeName = ObjectConvertClass.static_ext_string(dr["strBorrowLoginTypeName"]);
                model.strGiveBackVerifyTypeName = ObjectConvertClass.static_ext_string(dr["strGiveBackLoginTypeName"]);
                model.strLendingTypeName = ObjectConvertClass.static_ext_string(dr["strLendingTypeName"]);
                model.strLendingTypeAlias = ObjectConvertClass.static_ext_string(dr["strAlias"]);
                model.strStateName = ObjectConvertClass.static_ext_string(dr["strStateName"]);
                model.nKeepMunites = ObjectConvertClass.static_ext_int(dr["nMinutes"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
            }
            return model;
        }
        #endregion

        #region GetTongJiInfo方法（1.5.8获取统计信息）
        public List<LendingTjInfo> GetTongJiInfo(string strWorkShopGUID)
        {
            string strSqlWhere = " select * from VIEW_LendingTongJi where strWorkShopGUID = '" + strWorkShopGUID + "' ";
            return GetTongJiInfo_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlWhere.ToString()).Tables[0]);
        }
        public List<LendingTjInfo> GetTongJiInfo_DTToList(DataTable dt)
        {
            List<LendingTjInfo> modelList = new List<LendingTjInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LendingTjInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetTongJiInfo_DRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public LendingTjInfo GetTongJiInfo_DRToModelDTToList(DataRow dr)
        {
            LendingTjInfo model = new LendingTjInfo();
            if (dr != null)
            {
                model.nLendingType = ObjectConvertClass.static_ext_int(dr["nLendingType"]);
                model.nTotalCount = ObjectConvertClass.static_ext_int(dr["nTotalCount"]);
                model.nNoReturnCount = ObjectConvertClass.static_ext_int(dr["nNoReturnCount"]);
                model.strLendingTypeName = ObjectConvertClass.static_ext_string(dr["strLendingTypeName"]);
                model.strTypeAlias = ObjectConvertClass.static_ext_string(dr["strAlias"]);
            }
            return model;
        }
        #endregion

        #region IsHaveNotReturnGoods （1.5.9判断指定人员是否有未归还的物品）
        public bool IsHaveNotReturnGoods(string TrainmanGUID)
        {
            string strSQL = "select * from View_LendingInfo where strBorrowTrainmanGUID='" + TrainmanGUID + "'  and nReturnState = 0 ";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSQL.ToString()).Tables[0].Rows.Count > 0;

        }
        #endregion

        #region GetTrainmanNotReturnLendingInfo方法（1.5.10获得指定人员未归还物品列表）
        public List<LendingInfoDetail> GetTrainmanNotReturnLendingInfo(string TrainmanGUID)
        {
            string strSqlWhere = "select strGUID from View_LendingInfo where strBorrowTrainmanGUID = '" + TrainmanGUID + "'  and nReturnState = 0 ";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSqlWhere.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string strGUID = dt.Rows[0]["strGUID"].ToString();
                string strSql = "select * from View_LendingInfoDetail where strLendingInfoGUID = '" + strGUID + "'";
                return GetTrainmanNotReturnLendingInfo_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
            }
            else
            {
                return new List<LendingInfoDetail>();
            }

        }
        public List<LendingInfoDetail> GetTrainmanNotReturnLendingInfo_DTToList(DataTable dt)
        {
            List<LendingInfoDetail> modelList = new List<LendingInfoDetail>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LendingInfoDetail model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetTrainmanNotReturnLendingInfo_DRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public LendingInfoDetail GetTrainmanNotReturnLendingInfo_DRToModelDTToList(DataRow dr)
        {
            LendingInfoDetail model = new LendingInfoDetail();
            if (dr != null)
            {
                model.strLendingInfoGUID = ObjectConvertClass.static_ext_string(dr["strLendingInfoGUID"]);
                model.nLendingType = ObjectConvertClass.static_ext_int(dr["nLendingType"]);
                model.strLendingExInfo = ObjectConvertClass.static_ext_int(dr["strLendingExInfo"]);
                model.nReturnState = ObjectConvertClass.static_ext_int(dr["nReturnState"]);
                model.dtModifyTime = ObjectConvertClass.static_ext_date(dr["dtModifyTime"]);
                model.strGUID = ObjectConvertClass.static_ext_string(dr["strGUID"]);
                model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.dtBorrwoTime = ObjectConvertClass.static_ext_date(dr["dtBorrowTime"]);
                model.dtGiveBackTime = ObjectConvertClass.static_ext_date(dr["dtGiveBackTime"]);
                model.nBorrowVerifyType = ObjectConvertClass.static_ext_int(dr["nBorrowLoginType"]);
                model.nGiveBackVerifyType = ObjectConvertClass.static_ext_int(dr["nGiveBackLoginType"]);
                model.strLenderGUID = ObjectConvertClass.static_ext_string(dr["strLenderGUID"]);
                model.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strBorrowTrainmanGUID"]);
                model.strGiveBackTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strGiveBackTrainmanGUID"]);
                model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strBorrowTrainmanNumber"]);
                model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strBorrowTrainmanName"]);
                model.strLenderNumber = ObjectConvertClass.static_ext_string(dr["strLenderNumber"]);
                model.strLenderName = ObjectConvertClass.static_ext_string(dr["strLenderName"]);
                model.strGiveBackTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strGiveBackTrainmanNumber"]);
                model.strGiveBackTrainmanName = ObjectConvertClass.static_ext_string(dr["strGiveBackTrainmanName"]);
                model.strBorrowVerifyTypeName = ObjectConvertClass.static_ext_string(dr["strBorrowLoginTypeName"]);
                model.strGiveBackVerifyTypeName = ObjectConvertClass.static_ext_string(dr["strGiveBackLoginTypeName"]);
                model.strLendingTypeName = ObjectConvertClass.static_ext_string(dr["strLendingTypeName"]);
                model.strLendingTypeAlias = ObjectConvertClass.static_ext_string(dr["strAlias"]);
                model.strStateName = ObjectConvertClass.static_ext_string(dr["strStateName"]);
                model.nKeepMunites = ObjectConvertClass.static_ext_int(dr["nMinutes"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
            }
            return model;
        }
        #endregion

        #region  删除物品及物品相关的发放归还记录
        public void DeleteGoods(int LendingType, string LendingExInfo, string WorkShopGUID)
        {
            string sql = @"delete from TAB_LendingManage where strGUID in (
                    select strLendingInfoGUID from View_LendingInfoDetail where  nLendingType = @LendingType and 
                    strLendingExInfo = @LendingExInfo and strWorkShopGUID = @WorkShopGUID)";

            SqlParameter[] param = { new SqlParameter("LendingType",LendingType),
                                        new SqlParameter("LendingExInfo",LendingExInfo),
                                        new SqlParameter("WorkShopGUID",WorkShopGUID)};


            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, param);


            sql = @"delete from TAB_LendingDetail where strGUID in  (
                    select strGUID from View_LendingInfoDetail where  nLendingType = @LendingType and 
                    strLendingExInfo = @LendingExInfo and strWorkShopGUID = @WorkShopGUID)";

            SqlParameter[] paramDetail = { new SqlParameter("LendingType",LendingType),
                                        new SqlParameter("LendingExInfo",LendingExInfo),
                                        new SqlParameter("WorkShopGUID",WorkShopGUID)};


            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, paramDetail);
        }
        #endregion




    }


    public class DBCodeRange
    {
        #region GetGoodsCodeRange方法（1.5.11获取编码范围）
        public List<LendingManager> GetGoodsCodeRange(string WorkShopGUID, int lendingType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select * from Tab_Base_LendingManager where strWorkShopGUID = '" + WorkShopGUID + "'");
            if (lendingType != 0)
            {
                strSql.Append(" and nLendingTypeID = " + lendingType + "");
            }
            return GetGoodsCodeRange_DTToList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0]);
        }
        public List<LendingManager> GetGoodsCodeRange_DTToList(DataTable dt)
        {
            List<LendingManager> modelList = new List<LendingManager>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LendingManager model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = GetGoodsCodeRange_DRToModelDTToList(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        public LendingManager GetGoodsCodeRange_DRToModelDTToList(DataRow dr)
        {
            LendingManager model = new LendingManager();
            if (dr != null)
            {
                model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
                model.strGUID = ObjectConvertClass.static_ext_string(dr["strGUID"]);
                model.nLendingTypeID = ObjectConvertClass.static_ext_int(dr["nLendingTypeID"]);
                model.nStartCode = ObjectConvertClass.static_ext_int(dr["nStartCode"]);
                model.nStopCode = ObjectConvertClass.static_ext_int(dr["nStopCode"]);
                model.strExceptCodes = ObjectConvertClass.static_ext_string(dr["strExceptCodes"]);
                model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
            }
            return model;
        }
        #endregion


        #region InsertGoodsCodeRange（1.5.12增加编号范围）
        public bool InsertGoodsCodeRange(LendingManager model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_Base_LendingManager");
            strSql.Append("(strGUID,nLendingTypeID,nStartCode,nStopCode,strExceptCodes,strWorkShopGUID)");
            strSql.Append("values(@strGUID,@nLendingTypeID,@nStartCode,@nStopCode,@strExceptCodes,@strWorkShopGUID)");
            SqlParameter[] sqlParameters = new SqlParameter[] 
            {
                  new SqlParameter("@strGUID", model.strGUID),
                  new SqlParameter("@nLendingTypeID", model.nLendingTypeID),
                  new SqlParameter("@nStartCode", model.nStartCode),
                  new SqlParameter("@nStopCode", model.nStopCode),
                  new SqlParameter("@strExceptCodes", model.strExceptCodes),
                  new SqlParameter("@strWorkShopGUID", model.strWorkShopGUID)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParameters) > 0;
        }
        #endregion


        #region UpdateGoodsCodeRange（1.5.13修改编号范围）
        public bool UpdateGoodsCodeRange(LendingManager model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update Tab_Base_LendingManager set ");
            strSql.Append(" nLendingTypeID = @nLendingTypeID, ");
            strSql.Append(" nStartCode = @nStartCode, ");
            strSql.Append(" nStopCode = @nStopCode, ");
            strSql.Append(" strExceptCodes = @strExceptCodes, ");
            strSql.Append(" strWorkShopGUID = @strWorkShopGUID ");
            strSql.Append(" where strGUID = @strGUID ");
            SqlParameter[] parameters = {
          new SqlParameter("@strGUID", model.strGUID),
          new SqlParameter("@nLendingTypeID", model.nLendingTypeID),
          new SqlParameter("@nStartCode", model.nStartCode),
          new SqlParameter("@nStopCode", model.nStopCode),
          new SqlParameter("@strExceptCodes", model.strExceptCodes),
          new SqlParameter("@strWorkShopGUID", model.strWorkShopGUID)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion



        #region DeleteGoodsCodeRange（1.5.14删除编号范围）
        public bool DeleteGoodsCodeRange(string rangeGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tab_Base_LendingManager ");
            strSql.Append(" where strGUID = @strGUID ");
            SqlParameter[] sqlParameters = {
                 new SqlParameter("strGUID",rangeGUID)
                                           };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), sqlParameters) > 0;
        }
        #endregion


    }
}