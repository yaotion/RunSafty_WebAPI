using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.SelfServiceMachine
{
    #region   关于个性化出勤数据库操作类库
    public class DBSpecificOnDuty
    {
        public DataTable GetRenYuan(string strNumber)
        {
          
            string str = "select top 1 strTrainmanName1,strTrainmanName2,strTrainmanName3,strTrainmanName4 from VIEW_Nameplate_Group where strTrainmanNumber1 = '" + strNumber + "' or strTrainmanNumber2 = '" + strNumber + "' or strTrainmanNumber3 = '" + strNumber + "'or strTrainmanNumber4 = '" + strNumber + "'";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString()).Tables[0];
        }


        //获取人员消息
        public DataTable GetRenYuanByExcel(string strNumber)
        {
            string str = "select top 1 * from Tab_SpecificOnDuty_RenYuan where strNumber='" + strNumber + "'";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString()).Tables[0];
        }



        public DataTable GetAllTianQi(string strJiaoLu)
        {

            string str = "select StrAllCheZhan from Tab_SpecificOnDuty_JiaoLus where JiaoLuName='" + strJiaoLu + "'";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString()).Tables[0];
            string sp = "";
            if (dt.Rows.Count > 0)
            {
                sp = dt.Rows[0]["StrAllCheZhan"].ToString();
                sp = sp.Substring(0, sp.Length - 1);
            }
            string strAllCol = "";
            for (int k = 0; k < sp.Split(',').Length; k++)
            {
                strAllCol += "'" + sp.Split(',')[k] + "',";
            }
            strAllCol = strAllCol.Substring(0, strAllCol.Length - 1);

            string CheJianGUID = "";
            string strSql="";
            string strGetCheJianGUID = "select strWorkShopGUID from TAB_Base_TrainJiaolu where strTrainJiaoluName='" + strJiaoLu + "'";
            DataTable dtGetCheJianGUID = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strGetCheJianGUID.ToString()).Tables[0];

            if (dtGetCheJianGUID.Rows.Count > 0)
            {
                CheJianGUID = dtGetCheJianGUID.Rows[0]["strWorkShopGUID"].ToString();
            }
            if (CheJianGUID != "")
            {
                strSql = "select * from Tab_SpecificOnDuty_TianQi where strCheZhan in (" + strAllCol + ") and strCheJian='" + CheJianGUID + "'";
            }
            else
            {
                strSql = "select * from Tab_SpecificOnDuty_TianQi where strCheZhan in (" + strAllCol + ")";
            }


           
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
        }
        public DataTable GetAllLiangWei(string UserNumber)
        {
            string strSql = "select top 3 * from Tab_SpecificOnDuty_LiangWeiInfo where strShouPaiRen in (select UserName from Tab_SpecificOnDuty_LiangWei_Person where UserNember='" + UserNumber + "')";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];

            System.Data.DataTable tblDatas = new System.Data.DataTable();
            tblDatas.Columns.Add("strFaPeiContent", Type.GetType("System.String"));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string FaPeiContent = dt.Rows[i]["strFaPeiContent"].ToString();
                string strFaPeiContent = "";
                if (FaPeiContent.LastIndexOf("发现:") != -1)
                    strFaPeiContent = FaPeiContent.Substring(FaPeiContent.LastIndexOf("发现:") + 3).ToString();
                if (FaPeiContent.LastIndexOf("发现：") != -1)
                    strFaPeiContent = strFaPeiContent.Substring(strFaPeiContent.LastIndexOf("发现：") + 3).ToString();
                tblDatas.Rows.Add(new object[] { strFaPeiContent });

            }
            return tblDatas;
        }



        public DataTable GetLinSuiXiu(string cx,string ch)
        {
            string str = "select * from Tab_SpecificOnDuty_LingSuiXiu where StrCheXing='" + cx + "' and StrCheHao='" + ch + "'";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString()).Tables[0];
        }

        public DataTable GetGuZhang(string cx, string ch)
        {
            string str = "select * from Tab_SpecificOnDuty_GuZhang where StrCheXing='" + cx + "' and StrCheHao='" + ch + "' and DateDiff(dd,日期,getdate())<=10 order by CONVERT(datetime,日期,101) desc";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString()).Tables[0];
        }


        public DataTable GetYuJing(string strJiaoLu)
        {
            string str = "select * from Tab_SpecificOnDuty_YuJing where JiaoLuName='" + strJiaoLu + "'";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString()).Tables[0];
        }

        public DataTable GetShiGu()
        {
            string str = "select * from Tab_SpecificOnDuty_ShiGu ";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString()).Tables[0];
        }

        public string GetYanYu()
        {
            string str = "select * from TAB_System_Config where strIdent='YanYu'";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["strValue"].ToString();
            }
            else
            {
                return "暂无谚语，请在网站上设置！";
            }
        }

        public string getWorkShopGUID(string strCID)
        {
            string str = "select  strWorkShopGUID from  [TAB_Base_Site] where strSiteGUID='" + strCID + "'";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, str.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["strWorkShopGUID"].ToString();
            }
            else
            {
                return "";
            }
        }


    }
    #endregion

    #region 阅读记录
    public class DBReadingRecord
    {

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select nid,strTypeGUID,strTypeName,strType ");
            strSql.Append(" FROM TAB_FileGroup ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1  " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        public List<LCReadingRecord.filelist> GetReadingHistoryOfTrainman(string strTrainmanGUID, string strFileType)
        {
            string dtDateTime = DateTime.Now.ToString();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select strFileGUID,strFileName,dtReadTime from VIEW_ReadingRecordByTrainman where strTrainmanGUID=@strTrainmanGUID and StrTypeGUID=@StrTypeGUID and dtEndTime>@dtEndTime");
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("strTrainmanGUID",strTrainmanGUID)
               ,new SqlParameter("StrTypeGUID",strFileType)
               ,new SqlParameter("dtEndTime",dtDateTime)
            };
            return DataTableToFilelist(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters).Tables[0]); ;
        }

        public List<LCReadingRecord.filelist> DataTableToFilelist(DataTable dt)
        {
            List<LCReadingRecord.filelist> modelList = new List<LCReadingRecord.filelist>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LCReadingRecord.filelist model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = this.DataRowTofileModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }


        public LCReadingRecord.filelist DataRowTofileModel(DataRow row)
        {
            LCReadingRecord.filelist model = new LCReadingRecord.filelist();
            if (row != null)
            {

                if (row["strFileGUID"] != null)
                {
                    model.strFileGUID = row["strFileGUID"].ToString();
                }
                else
                {
                    model.strFileGUID = "";
                }
                if (row["strFileName"] != null)
                {
                    model.strFileName = row["strFileName"].ToString();
                }
                else
                {
                    model.strFileName = "";
                }
                if (row["dtReadTime"] != null)
                {
                    model.dtReadTime = row["dtReadTime"].ToString();
                }
                else
                {
                    model.dtReadTime = "";
                }
            }
            return model;
        }






        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddReadHistory(MDSelfServiceMachine.MDReadHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_ReadHistory(");
            strSql.Append("strFileGUID,strTrainmanGUID,DtReadTime,SiteGUID)");
            strSql.Append(" values (");
            strSql.Append("@strFileGUID,@strTrainmanGUID,@DtReadTime,@SiteGUID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@strFileGUID", SqlDbType.VarChar,50),
					new SqlParameter("@strTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@DtReadTime", SqlDbType.VarChar,50),
					new SqlParameter("@SiteGUID", SqlDbType.VarChar,50)};
            parameters[0].Value = model.strFileGUID;
            parameters[1].Value = model.strTrainmanGUID;
            parameters[2].Value = model.DtReadTime;
            parameters[3].Value = model.SiteGUID;
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);

            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(MDSelfServiceMachine.MDReadDocPlan model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_ReadDocPlan set ");
            strSql.Append("StrTrainmanGUID=@StrTrainmanGUID,");
            strSql.Append("StrFileGUID=@StrFileGUID,");
            strSql.Append("NReadCount=@NReadCount,");
            strSql.Append("DtFirstReadTime=@DtFirstReadTime,");
            strSql.Append("DtLastReadTime=@DtLastReadTime");
            strSql.Append(" where nId=@nId");
            SqlParameter[] parameters = {
					new SqlParameter("@StrTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrFileGUID", SqlDbType.VarChar,50),
					new SqlParameter("@NReadCount", SqlDbType.Int,4),
					new SqlParameter("@DtFirstReadTime", SqlDbType.DateTime),
					new SqlParameter("@DtLastReadTime", SqlDbType.DateTime),
					new SqlParameter("@nId", SqlDbType.Int,4)};
            parameters[0].Value = model.StrTrainmanGUID;
            parameters[1].Value = model.StrFileGUID;
            parameters[2].Value = model.NReadCount;
            parameters[3].Value = model.DtFirstReadTime;
            parameters[4].Value = model.DtLastReadTime;
            parameters[5].Value = model.nId;

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


        public void UpdateReadTime(string strFileGUID, string strTrainmanGUID, string strReadTime)
        {
            string strWhere = string.Format(" StrFileGUID='{0}' AND StrTrainmanGUID='{1}' ", strFileGUID, strTrainmanGUID);
            TF.CommonUtility.LogClass.log(strWhere);
            List<MDSelfServiceMachine.MDReadDocPlan> plans = this.GetModelList(strWhere);
            if (plans != null && plans.Count > 0)
            {
                MDSelfServiceMachine.MDReadDocPlan plan = plans[0];
                DateTime readTime = DateTime.Parse(strReadTime);
                if (plan.NReadCount.HasValue && plan.NReadCount.Value > 0) //已经阅读
                {
                    plan.NReadCount++;
                    plan.DtLastReadTime = readTime;
                }
                else
                {
                    plan.DtFirstReadTime = readTime;
                    plan.DtLastReadTime = readTime;
                    plan.NReadCount = 1;

                }
                if (this.Update(plan))
                {
                    TF.CommonUtility.LogClass.log("阅读记录更新成功");
                }
                else
                {
                    TF.CommonUtility.LogClass.log("阅读记录更新失败");
                }
            }
        }

        public List<MDSelfServiceMachine.MDReadDocPlan> GetModelList(string strWhere)
        {
            DataSet ds = this.GetReadDocPlanList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetReadDocPlanList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select nId,StrTrainmanGUID,StrFileGUID,NReadCount,DtFirstReadTime,DtLastReadTime ");
            strSql.Append(" FROM TAB_ReadDocPlan ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MDSelfServiceMachine.MDReadDocPlan> DataTableToList(DataTable dt)
        {
            List<MDSelfServiceMachine.MDReadDocPlan> modelList = new List<MDSelfServiceMachine.MDReadDocPlan>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MDSelfServiceMachine.MDReadDocPlan model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = this.DataRowToModel(dt.Rows[n]);
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
        public MDSelfServiceMachine.MDReadDocPlan DataRowToModel(DataRow row)
        {
            MDSelfServiceMachine.MDReadDocPlan model = new MDSelfServiceMachine.MDReadDocPlan();
            if (row != null)
            {
                if (row["nId"] != null && row["nId"].ToString() != "")
                {
                    model.nId = int.Parse(row["nId"].ToString());
                }
                if (row["StrTrainmanGUID"] != null)
                {
                    model.StrTrainmanGUID = row["StrTrainmanGUID"].ToString();
                }
                if (row["StrFileGUID"] != null)
                {
                    model.StrFileGUID = row["StrFileGUID"].ToString();
                }
                if (row["NReadCount"] != null && row["NReadCount"].ToString() != "")
                {
                    model.NReadCount = int.Parse(row["NReadCount"].ToString());
                }
                if (row["DtFirstReadTime"] != null && row["DtFirstReadTime"].ToString() != "")
                {
                    model.DtFirstReadTime = DateTime.Parse(row["DtFirstReadTime"].ToString());
                }
                if (row["DtLastReadTime"] != null && row["DtLastReadTime"].ToString() != "")
                {
                    model.DtLastReadTime = DateTime.Parse(row["DtLastReadTime"].ToString());
                }
            }
            return model;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListForReadingSync(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select nid,strTypeGUID,strTypeName,strType ");
            strSql.Append(" FROM TAB_FileGroup ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }

        public DataSet GetAllListWithFileType(string cid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.strTypeGUID,strFileGUID,strFilePath strfilePathName ,strType,
strFileName,a.nReadInterval as nReadTimeCount
from TAB_ReadDoc a inner join TAB_FileGroup b on a.StrTypeGUID=b.strTypeGUID where dtEndTime > GetDate()");
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());

        }





    }
    #endregion

    #region 打印

    public class DBDeliverJSPrint
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddDeliverJSPrint(MDSelfServiceMachine.MDDeliverJSPrint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Tab_DeliverJSPrint(");
            strSql.Append("StrTrainmanGUID,StrPlanGUID,StrSiteGUID,dtPrintTime)");
            strSql.Append(" values (");
            strSql.Append("@StrTrainmanGUID,@StrPlanGUID,@StrSiteGUID,@dtPrintTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@StrTrainmanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrPlanGUID", SqlDbType.VarChar,50),
					new SqlParameter("@StrSiteGUID", SqlDbType.VarChar,50),
					new SqlParameter("@dtPrintTime", SqlDbType.DateTime)};
            parameters[0].Value = model.StrTrainmanGUID;
            parameters[1].Value = model.StrPlanGUID;
            parameters[2].Value = model.StrSiteGUID;
            parameters[3].Value = model.dtPrintTime;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        /// <summary>
        /// 判断交付揭示是否可以打印
        /// </summary>
        /// <param name="strPlanGUID"></param>
        /// <param name="strTrainmanGUID"></param>
        /// <param name="strPrintTime"></param>
        /// <returns></returns>
        public int IsJsPrintable(string strPlanGUID, string strTrainmanGUID, string strPrintTime)
        {
            if (string.IsNullOrEmpty(strPlanGUID))
            {
                return 1;
            }
            DateTime dtPrintTime = DateTime.Parse(strPrintTime);
            DateTime dtNow = DateTime.Now;
            if (DateTime.Compare(dtNow.AddMinutes(10), dtPrintTime) < 0)
                return 2;
            if (IsDriver(strPlanGUID, strTrainmanGUID))
            {
                if (this.IsJsPrinted(strPlanGUID, strTrainmanGUID))
                    return 1;
            }
            else
            {
                return 3;
            }
            return 0;
        }


        /// <summary>
        /// 判断交付揭示是否已经打印
        /// </summary>
        /// <param name="strPlanGUID"></param>
        /// <param name="strTrainmanGUID"></param>
        /// <returns></returns>
        private bool IsJsPrinted(string strPlanGUID, string strTrainmanGUID)
        {
            string strWhere = string.Format(" StrTrainmanGUID='{0}' and StrPlanGUID='{1}' ", strTrainmanGUID, strPlanGUID);
            List<MDSelfServiceMachine.MDDeliverJSPrint> printList = GetModelList(strWhere);
            return printList != null && printList.Count > 0;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MDSelfServiceMachine.MDDeliverJSPrint> GetModelList(string strWhere)
        {
            DataSet ds = this.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MDSelfServiceMachine.MDDeliverJSPrint> DataTableToList(DataTable dt)
        {
            List<MDSelfServiceMachine.MDDeliverJSPrint> modelList = new List<MDSelfServiceMachine.MDDeliverJSPrint>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MDSelfServiceMachine.MDDeliverJSPrint model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = this.DataRowToModel(dt.Rows[n]);
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
        public MDSelfServiceMachine.MDDeliverJSPrint DataRowToModel(DataRow row)
        {
            MDSelfServiceMachine.MDDeliverJSPrint model = new MDSelfServiceMachine.MDDeliverJSPrint();
            if (row != null)
            {
                if (row["nID"] != null && row["nID"].ToString() != "")
                {
                    model.nID = int.Parse(row["nID"].ToString());
                }
                if (row["StrTrainmanGUID"] != null)
                {
                    model.StrTrainmanGUID = row["StrTrainmanGUID"].ToString();
                }
                if (row["StrPlanGUID"] != null)
                {
                    model.StrPlanGUID = row["StrPlanGUID"].ToString();
                }
                if (row["StrSiteGUID"] != null)
                {
                    model.StrSiteGUID = row["StrSiteGUID"].ToString();
                }
                if (row["dtPrintTime"] != null && row["dtPrintTime"].ToString() != "")
                {
                    model.dtPrintTime = DateTime.Parse(row["dtPrintTime"].ToString());
                }
            }
            return model;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select nID,StrTrainmanGUID,StrPlanGUID,StrSiteGUID,dtPrintTime ");
            strSql.Append(" FROM Tab_DeliverJSPrint ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }


        /// <summary>
        /// 判断是否是司机
        /// </summary>
        /// <param name="strPlanGUID"></param>
        /// <param name="strTrainmanGUID"></param>
        /// <returns></returns>
        private bool IsDriver(string strPlanGUID, string strTrainmanGUID)
        {
            DataTable tblPlan = this.GetTrainmanTypeByPlanGUID(strPlanGUID);
            if (tblPlan != null && tblPlan.Rows.Count > 0)
            {
                string strTrainmanTypeName = tblPlan.Rows[0]["strTrainmanTypeName"].ToString();
                string strTrainmanGUID1 = tblPlan.Rows[0]["strTrainmanGUID1"].ToString();
                string strTrainmanGUID2 = tblPlan.Rows[0]["strTrainmanGUID2"].ToString();
                if (strTrainmanTypeName == "双司机")
                {
                    if (strTrainmanGUID1 == strTrainmanGUID || strTrainmanGUID2 == strTrainmanGUID)
                    {
                        return true;
                    }
                }
                else if (strTrainmanTypeName == "标准班")
                {
                    if (strTrainmanGUID1 == strTrainmanGUID)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 根据行车计划GUID获取司机类型
        /// </summary>
        /// <param name="strPlanGUID"></param>
        /// <returns></returns>
        public DataTable GetTrainmanTypeByPlanGUID(string strPlanGUID)
        {
            string Sql = " select * from view_plan_trainman where strTrainPlanGUID =@strTrainPlanGUID";
            SqlParameter[] sqlParams = {
                                       new SqlParameter("strTrainPlanGUID",strPlanGUID)
                                   };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, Sql, sqlParams).Tables[0];
        }



    }
    #endregion


}
