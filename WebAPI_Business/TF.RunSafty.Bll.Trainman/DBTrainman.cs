using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using System.Data;
using TF.CommonUtility;
using Dapper;

namespace TF.RunSafty.Trainman
{
    public class DBTrainman : DBUtility
    {
        //SQL语句中，对二进制字段增加是否为空的判断
        const string SQL_BlobFieldNull = @"case WHEN  FingerPrint1 IS NULL THEN 0   ELSE 1 END AS FG1ISNULL ,
        CASE WHEN  Picture IS NULL THEN 0  ELSE 1 END AS PICISNULL  ,
        CASE WHEN   FingerPrint2 IS NULL THEN 0 ELSE 1 END AS FG2ISNULL ";

        private string DBFieldToString(DataRow dr,string FieldName)
        {
            if (dr.Table.Columns.Contains(FieldName))
            {
                return ObjectConvertClass.static_ext_string(dr[FieldName]);
            }
            else
                return "";
        }
        private DateTime? DBFieldToTime(DataRow dr,string FieldName)
        {
            if (dr.Table.Columns.Contains(FieldName))
            {
                return ObjectConvertClass.static_ext_date(dr[FieldName]);
            }
            else
                return null;
        }
        private int DBFieldToInt(DataRow dr,string FieldName)
        {
            if (dr.Table.Columns.Contains(FieldName))
            {
                return ObjectConvertClass.static_ext_int(dr[FieldName]);
            }
            else
                return 0;
        
        }
        private VTrainman DrToVTrainman(DataRow dr,int Option)
        {
            VTrainman model = new VTrainman();
            if (dr != null)
            {
                model.nID = DBFieldToInt(dr, "nID");
                model.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanGUID"]);
                model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
                model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
                model.strTelNumber = DBFieldToString(dr,"strTelNumber");
                model.strRemark = DBFieldToString(dr,"strRemark");

                model.dtCreateTime = DBFieldToTime(dr,"dtCreateTime");

                if ((Option & 1) != 0)
                {
                    if (dr.Table.Columns.Contains("FingerPrint1"))
                    {
                        model.FingerPrint1 = ToBase64String(dr["FingerPrint1"]);
                    }


                    if (dr.Table.Columns.Contains("FingerPrint2"))
                    {
                        model.FingerPrint2 = ToBase64String(dr["FingerPrint2"]);
                    }
                    
                }

                if ((Option & 2) != 0)
                {
                    if (dr.Table.Columns.Contains("Picture"))
                    {
                        model.Picture = ToBase64String(dr["Picture"]);
                    }
                    
                }


                //此三个字段，总是同时出现
                if (dr.Table.Columns.Contains("FG1ISNULL"))
                {
                    model.nFingerPrint1_Null = ObjectConvertClass.static_ext_int(dr["FG1ISNULL"]);
                    model.nFingerPrint2_Null = ObjectConvertClass.static_ext_int(dr["FG2ISNULL"]);
                    model.nPicture_Null = ObjectConvertClass.static_ext_int(dr["PICISNULL"]); ;
                }


                model.nTrainmanState = DBFieldToInt(dr, "nTrainmanState");
                model.nPostID = DBFieldToInt(dr, "nPostID");
                model.strWorkShopGUID = DBFieldToString(dr, "strWorkShopGUID");
                model.strGuideGroupGUID = DBFieldToString(dr, "strGuideGroupGUID");
                model.strMobileNumber = DBFieldToString(dr, "strMobileNumber");
                model.strAddress = DBFieldToString(dr, "strAddress");
                model.nDriverType = DBFieldToInt(dr, "nDriverType");
                model.bIsKey = DBFieldToInt(dr, "bIsKey");
                model.dtRuZhiTime = DBFieldToTime(dr, "dtRuZhiTime");
                model.dtJiuZhiTime = DBFieldToTime(dr, "dtJiuZhiTime");
                model.nDriverLevel = DBFieldToInt(dr, "nDriverLevel");
                model.strABCD = DBFieldToString(dr, "strABCD");
                model.nKeHuoID = DBFieldToString(dr, "nKeHuoID");
                model.strTrainJiaoluGUID = DBFieldToString(dr, "strTrainJiaoluGUID");
                model.dtLastEndWorkTime = DBFieldToTime(dr, "dtLastEndWorkTime");
                model.nDeleteState = DBFieldToInt(dr, "nDeleteState");
                model.strTrainJiaoluName = DBFieldToString(dr, "strTrainJiaoluName");
                model.strGuideGroupName = DBFieldToString(dr, "strGuideGroupName");
                model.strWorkShopName = DBFieldToString(dr, "strWorkShopName");
                model.strAreaName = DBFieldToString(dr, "strAreaName");
                model.strJWDNumber = DBFieldToString(dr, "strJWDNumber");
                model.strJP = DBFieldToString(dr, "strJP");
                model.strTrainmanJiaoluGUID = DBFieldToString(dr, "strTrainmanJiaoluGUID");
                model.strareaguid = DBFieldToString(dr, "strareaguid");
                model.dtLastInRoomTime = DBFieldToTime(dr, "dtLastInRoomTime");
                model.dtLastOutRoomTime = DBFieldToTime(dr, "dtLastOutRoomTime");
            }


            return model;
        }

        public List<VTrainman> TableToVTrainmanList(DataTable dt, int Option)
        {
            List<VTrainman> modelList = new List<VTrainman>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                VTrainman model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DrToVTrainman(dt.Rows[n], Option);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        #region  通过GUID获取人员工号
        public string GetTrainmanNumberByGuid(string strTrainmanGuid)
        {
            string sql = "select strTrainmanNumber from TAB_Org_Trainman where strTrainmanGUID=@strTrainmanGUID";
            SqlParameter[] p = { new SqlParameter("strTrainmanGUID", strTrainmanGuid) };
            return SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, p).ToString();
        }
        #endregion

        #region   Base64和bate之间的转换
        object ConvertBase64ToByte(object str)
        {
            //将url解码
            string BeStr = System.Web.HttpUtility.UrlDecode(str.ToString());
            //转换成byte数组
            return Convert.FromBase64String(str.ToString());
        }



        string ToBase64String(object o)
        {
            byte[] b = o as byte[];
            if (b == null)
            {
                return "";
            }
            else
            {

                return Convert.ToBase64String(b);
            }


        }
        #endregion

        #region AddTrainman（添加人员）
        public bool AddTrainman(Trainman model)
        {
            string strFingerPrintAndPicture = "";
            string strFingerPrintAndPicture1 = "";

            if (!string.IsNullOrEmpty(model.FingerPrint1.ToString()))
            {
                strFingerPrintAndPicture += "FingerPrint1,";
                strFingerPrintAndPicture1 += "@FingerPrint1,";
                model.FingerPrint1 = ConvertBase64ToByte(model.FingerPrint1);
            }
            if (!string.IsNullOrEmpty(model.FingerPrint2.ToString()))
            {
                strFingerPrintAndPicture += "FingerPrint2,";
                strFingerPrintAndPicture1 += "@FingerPrint2,";
                model.FingerPrint2 = ConvertBase64ToByte(model.FingerPrint2.ToString());
            }
            if (!string.IsNullOrEmpty(model.Picture.ToString()))
            {
                strFingerPrintAndPicture += "Picture,";
                strFingerPrintAndPicture1 += "@Picture,";
                model.Picture = ConvertBase64ToByte(model.Picture.ToString());
            }


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TAB_Org_Trainman");
            strSql.Append("(strTrainmanGUID,strTrainmanNumber,strTrainmanName,nPostID,strWorkShopGUID," + strFingerPrintAndPicture + "strGuideGroupGUID,strTelNumber,strMobileNumber,strAddress,nDriverType,bIsKey,dtRuZhiTime,dtJiuZhiTime,nDriverLevel,strABCD,strRemark,nKeHuoID,strTrainJiaoluGUID,dtCreateTime,nTrainmanState,strJP,strTrainmanJiaoluGUID,strareaguid,dtLastEndworkTime)");
            strSql.Append("values(@strTrainmanGUID,@strTrainmanNumber,@strTrainmanName,@nPostID,@strWorkShopGUID," + strFingerPrintAndPicture1 + "@strGuideGroupGUID,@strTelNumber,@strMobileNumber,@strAddress,@nDriverType,@bIsKey,@dtRuZhiTime,@dtJiuZhiTime,@nDriverLevel,@strABCD,@strRemark,@nKeHuoID,@strTrainJiaoluGUID,@dtCreateTime,@nTrainmanState,@strJP,@strTrainmanJiaoluGUID,@strareaguid,@dtLastEndworkTime)");
            SqlParameter[] parameters = {
                new SqlParameter("@strTrainmanGUID", model.strTrainmanGUID),
                new SqlParameter("@strTrainmanNumber", model.strTrainmanNumber),
                new SqlParameter("@strTrainmanName", model.strTrainmanName),
                new SqlParameter("@nPostID", model.nPostID),
                new SqlParameter("@strWorkShopGUID", model.strWorkShopGUID),
                new SqlParameter("@FingerPrint1", model.FingerPrint1),
                new SqlParameter("@FingerPrint2", model.FingerPrint2),
                new SqlParameter("@Picture", model.Picture),
                new SqlParameter("@strGuideGroupGUID", model.strGuideGroupGUID),
                new SqlParameter("@strTelNumber", model.strTelNumber),
                new SqlParameter("@strMobileNumber", model.strMobileNumber),
                new SqlParameter("@strAddress", model.strAddress),
                new SqlParameter("@nDriverType", model.nDriverType),
                new SqlParameter("@bIsKey", model.bIsKey),
                new SqlParameter("@dtRuZhiTime", model.dtRuZhiTime),
                new SqlParameter("@dtJiuZhiTime", model.dtJiuZhiTime),
                new SqlParameter("@nDriverLevel", model.nDriverLevel),
                new SqlParameter("@strABCD", model.strABCD),
                new SqlParameter("@strRemark", model.strRemark),
                new SqlParameter("@nKeHuoID", model.nKeHuoID),
                new SqlParameter("@strTrainJiaoluGUID", model.strTrainJiaoluGUID),
                new SqlParameter("@dtCreateTime", model.dtCreateTime),
                new SqlParameter("@nTrainmanState", 7),
                new SqlParameter("@strJP", model.strJP),
                new SqlParameter("@strTrainmanJiaoluGUID", model.strTrainmanJiaoluGUID),
                new SqlParameter("@strareaguid", model.strareaguid),
                new SqlParameter("@dtLastEndworkTime", model.dtLastEndWorkTime)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region UpdateTrainman（修改人员信息）
        public bool UpdateTrainman(Trainman model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update TAB_Org_Trainman set ");
            strSql.Append(" strTrainmanNumber = @strTrainmanNumber, ");
            strSql.Append(" strTrainmanName = @strTrainmanName, ");
            strSql.Append(" nPostID = @nPostID, ");
            strSql.Append(" strTelNumber = @strTelNumber, ");
            strSql.Append(" strMobileNumber = @strMobileNumber, ");
            strSql.Append(" strAddress = @strAddress, ");
            strSql.Append(" nDriverType = @nDriverType, ");
            strSql.Append(" bIsKey = @bIsKey, ");
            strSql.Append(" dtRuZhiTime = @dtRuZhiTime, ");
            strSql.Append(" dtJiuZhiTime = @dtJiuZhiTime, ");
            strSql.Append(" nDriverLevel = @nDriverLevel, ");
            strSql.Append(" strABCD = @strABCD, ");
            strSql.Append(" strRemark = @strRemark, ");
            strSql.Append(" nKeHuoID = @nKeHuoID, ");

            if (model.FingerPrint1 != null && !string.IsNullOrEmpty(Convert.ToString(model.FingerPrint1)))
                strSql.Append(" FingerPrint1 = @FingerPrint1, ");

            if (model.FingerPrint2 != null && !string.IsNullOrEmpty(Convert.ToString(model.FingerPrint2)))
                strSql.Append(" FingerPrint2 = @FingerPrint2, ");

            if (model.Picture != null && !string.IsNullOrEmpty(Convert.ToString(model.Picture)))
                strSql.Append(" Picture = @Picture, ");

            strSql.Append(" strJP = @strJP ");


            strSql.Append(" where strTrainmanGUID = @strTrainmanGUID ");
        

            SqlParameter[] parameters = {
                new SqlParameter("@strTrainmanGUID", model.strTrainmanGUID),
                new SqlParameter("@strTrainmanNumber", model.strTrainmanNumber),
                new SqlParameter("@strTrainmanName", model.strTrainmanName),
                new SqlParameter("@nPostID", model.nPostID),
                new SqlParameter("@FingerPrint1", ConvertBase64ToByte(model.FingerPrint1)),
                new SqlParameter("@FingerPrint2", ConvertBase64ToByte(model.FingerPrint2)),
                new SqlParameter("@Picture", ConvertBase64ToByte(model.Picture)),
                new SqlParameter("@strTelNumber", model.strTelNumber),
                new SqlParameter("@strMobileNumber", model.strMobileNumber),
                new SqlParameter("@strAddress", model.strAddress),
                new SqlParameter("@nDriverType", model.nDriverType),
                new SqlParameter("@bIsKey", model.bIsKey),
                new SqlParameter("@dtRuZhiTime", model.dtRuZhiTime),
                new SqlParameter("@dtJiuZhiTime", model.dtJiuZhiTime),
                new SqlParameter("@nDriverLevel", model.nDriverLevel),
                new SqlParameter("@strABCD", model.strABCD),
                new SqlParameter("@strRemark", model.strRemark),
                new SqlParameter("@nKeHuoID", model.nKeHuoID),
                new SqlParameter("@strJP", model.strJP)
                                        };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region DelTrainman（删除人员信息）
        public bool DelTrainman(string strTrainmanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TAB_Org_Trainman ");
            strSql.Append(" where strTrainmanGUID = @strTrainmanGUID ");
            SqlParameter[] parameters = {
            new SqlParameter("strTrainmanGUID",strTrainmanGUID)};
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region GetTrainmanByNumber（获取单个人的详细信息）        
        public List<VTrainman> GetTrainmanByNumber(string strTrainmanNumber, int Option)
        {           
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select Top 1 *, " + SQL_BlobFieldNull + "From VIEW_Org_Trainman Where strTrainmanNumber = '" + strTrainmanNumber + "'");

            return TableToVTrainmanList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0], Option);
        }
       
        #endregion

        #region QueryTrainmans_blobFlag（分页获取人员的信息列表）
        public List<VTrainman> QueryTrainmans_blobFlag(QurryModel model, int PageIndex,ref int allcount)
        {
            if (model == null)
            {
                return new List<VTrainman>();
            }

            string sqlCondition = "";
            string sqlPageCondition = " where RowNumber >'" + 100 * (PageIndex - 1) + "'";
            sqlCondition += " where 1=1 ";
            if (model.strTrainmanNumber != "" && model.strTrainmanNumber!=null)
            {
                sqlCondition += " and strTrainmanNumber = '" + model.strTrainmanNumber + "'";
            }
            if (model.strAreaGUID != "" && model.strAreaGUID != null)
            {
                sqlCondition += " and strAreaGUID ='" + model.strAreaGUID + "'";

            }

            if (model.strWorkShopGUID != "" && model.strWorkShopGUID != null)
            {
                sqlCondition += " and strWorkShopGUID ='" + model.strWorkShopGUID + "'";

            }
            if (model.strGuideGroupGUID != "" && model.strGuideGroupGUID != null)
            {
                sqlCondition += " and strGuideGroupGUID ='" + model.strGuideGroupGUID + "'";

            }
            if (model.strTrainJiaoluGUID != "" && model.strTrainJiaoluGUID != null)
            {
                sqlCondition += " and strTrainJiaoluGUID ='" + model.strTrainJiaoluGUID + "'";

            }
            if (model.strTrainmanName != "" && model.strTrainmanName != null)
            {
                sqlCondition += " and strTrainmanName like '%" + model.strTrainmanName + "%'";

            }
            if (model.nFingerCount >= 0)
            {
                if (model.nFingerCount == 0)
                {
                    sqlCondition += " and ((FingerPrint1 is null) and (FingerPrint2 is null))";
                }
                if (model.nFingerCount == 1)
                {
                    sqlCondition += " and (((FingerPrint1 is null) and not (FingerPrint2 is null)) or  ((FingerPrint2 is null) and not (FingerPrint1 is null)))";
                }
                if (model.nFingerCount == 2)
                {
                    sqlCondition += " and (not(FingerPrint1 is null) and not(FingerPrint2 is null)) ";

                }

            }

            if (model.nPhotoCount >= 0)
            {

                if (model.nPhotoCount == 0)
                {

                    sqlCondition += " and (Picture is null or datalength(Picture)=0) ";
                }
                if (model.nPhotoCount > 0)
                {
                    sqlCondition += " and not (Picture is null)";
                }

            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  top 100  strTrainmanNumber,strTrainmanName,strTelNumber,strRemark,nID,dtCreateTime,case WHEN   FingerPrint1 IS NULL THEN 0 ");
            strSql.Append("  ELSE 1 END AS FG1ISNULL ,CASE WHEN  Picture IS NULL THEN 0  ELSE 1 END AS PICISNULL  ,CASE WHEN   FingerPrint2 IS NULL THEN 0 ELSE 1 END AS FG2ISNULL ");
            strSql.Append(" ,nTrainmanState ");
            strSql.Append(" ,nPostID,strWorkShopGUID,strGuideGroupGUID,strMobileNumber,strAddress,strTrainmanGUID,nDriverType");
            strSql.Append(" ,bIsKey,dtRuZhiTime,dtJiuZhiTime,nDriverLevel,strABCD,nKeHuoID,strTrainJiaoluGUID,dtLastEndWorkTime,nDeleteState,strTrainJiaoluName");
            strSql.Append(" ,strGuideGroupName,strWorkShopName,strAreaGUID,strAreaName,strJWDNumber,strJP,strTrainmanJiaoluGUID,dtLastInRoomTime,dtLastOutRoomTime ");
            strSql.Append(" FROM ");
            strSql.Append("(select ROW_NUMBER() over(order by nid) as rownumber,* from VIEW_Org_Trainman " + sqlCondition + ") A " + sqlPageCondition + "");

            string strAllcounts = "select count(*) from VIEW_Org_Trainman " + sqlCondition + "";
            allcount = Convert.ToInt32(ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strAllcounts)));
            return TableToVTrainmanList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0],0);

        }
        #endregion

        #region GetPopupTrainmans（获取输入人员时弹出人员信息）
        public List<VTrainman> GetPopupTrainmans(string WorkShopGUID, string strKeyName, int pageindex,out int nTotalCount)
        {
            string strWhere = "where 1=1 ";
            if (pageindex <= 0)
            {
                pageindex = 1;
            }
            if (WorkShopGUID != "" && WorkShopGUID!=null)
            {
                strWhere += " and strWorkShopGUID='" + WorkShopGUID + "'";
            }

            if (strKeyName != "" && strKeyName != null)
            {
                strWhere += "and ((strTrainmanNumber like '%" + strKeyName + "%') or (strJP like '%" + strKeyName + "%') or (strTrainmanName like '%" + strKeyName + "%'))";
            }

            string sqlPageCondition = " where RowNumber >" + 10 * (pageindex - 1) + "";
            string strSql = "SELECT  top 10 strTrainmanGUID,strTrainmanNumber,strTrainmanName,nPostID,nKeHuoID,";
            strSql += "bIsKey,strABCD,strMobileNumber,nTrainmanState,strTelNumber from ";
            strSql += "(select ROW_NUMBER() over(order by nid) as rownumber,* from TAB_Org_Trainman " + strWhere + ") A " + sqlPageCondition + "";
            strSql += " order by strTrainmanNumber";

            nTotalCount = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, "select count(*) from TAB_Org_Trainman " + strWhere));
            return TableToVTrainmanList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0],0);
        }
        #endregion

        #region GetTrainmansBrief（获取人员简要信息，主要用于加载指纹）




        private VTrainman DrToVTrainmanBrief(DataRow dr, int Option)
        {
            VTrainman model = new VTrainman();
            model.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanGUID"]);
            model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
            model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);           
            model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
            model.strJP = ObjectConvertClass.static_ext_string(dr["strJP"]);            
            if ((Option & 1) != 0)
            {
                if (dr.Table.Columns.Contains("FingerPrint1"))
                {
                    model.FingerPrint1 = ToBase64String(dr["FingerPrint1"]);
                }


                if (dr.Table.Columns.Contains("FingerPrint2"))
                {
                    model.FingerPrint2 = ToBase64String(dr["FingerPrint2"]);
                }

            }

            if ((Option & 2) != 0)
            {
                if (dr.Table.Columns.Contains("Picture"))
                {
                    model.Picture = ToBase64String(dr["Picture"]);
                }

            }

            return model;
                
               
        }
        public List<VTrainman> GetTrainmansBrief(int startNid,int nCount, int Option,out int nTotalCount)
        {
            string strSql = "select count(*) from TAB_Org_Trainman";
            
                
            nTotalCount = (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql);


            strSql = "SELECT TOP " + nCount + " strTrainmanGUID, strTrainmanNumber, strTrainmanName, FingerPrint1, FingerPrint2, Picture, nID"
                + ",strWorkShopGUID,strTelNumber,strMobileNumber,strJP FROM TAB_Org_Trainman  WHERE (nid > " + startNid + ") ORDER BY nid";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];

            List<VTrainman> result = new List<VTrainman>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(DrToVTrainmanBrief(dr,Option));
                
            }
            return result;
        }
      
        #endregion

        #region UpdateTrainmanTel（修改人员的联系方式）
        public bool UpdateTrainmanTel(string TrainmanGUID, string TrainmanTel,string TrainmanMobile, string TrainmanAddress,string TrainmanRemark)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update TAB_Org_Trainman set ");

            strSql.Append(" strTelNumber = @strTelNumber, ");
            strSql.Append(" strMobileNumber = @strMobileNumber, ");
            strSql.Append(" strAddress = @strAddress, ");
            strSql.Append(" strRemark = @strRemark ");
            strSql.Append(" where strTrainmanGUID = @strTrainmanGUID ");

            SqlParameter[] parameters = {
                new SqlParameter("@strTrainmanGUID", TrainmanGUID),
                new SqlParameter("@strTelNumber", TrainmanTel),
                new SqlParameter("@strMobileNumber",TrainmanMobile),
                new SqlParameter("@strAddress", TrainmanAddress),
                new SqlParameter("@strRemark",TrainmanRemark)
                                        };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters) > 0;
        }
        #endregion

        #region ExistNumber（判断人员是否存在）
        public bool ExistNumber(string TrainmanGUID, string TrainmanNumber)
        {
            string strAllcounts = "Select count(*) From VIEW_Org_Trainman Where strTrainmanNumber ='" + TrainmanNumber + "' and strTrainmanGUID <> '" + TrainmanGUID + "'";
            return Convert.ToInt32(ObjectConvertClass.static_ext_int(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strAllcounts))) > 0;
        }

      
       
        #endregion

        #region GetTrainman（通过GUID获取人员信息）        
        public List<VTrainman> GetTrainman(string strTrainmanGUID, int Option)
        {           
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select Top 1 *," + SQL_BlobFieldNull + " From VIEW_Org_Trainman Where strTrainmanGUID = '" + strTrainmanGUID + "'");
            return TableToVTrainmanList(SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0], Option);
        }
      
        #endregion

        #region ClearFinger（清除人员的指纹照片）
        public bool ClearFinger(string TrainmanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update TAB_Org_Trainman set ");

            strSql.Append(" Fingerprint1 = null, ");
            strSql.Append(" Fingerprint2 = null ");
            strSql.Append(" where strTrainmanGUID = '" + TrainmanGUID + "'");
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString()) > 0;
        }
        #endregion

        #region UpdateFingerAndPic（修改人员的指纹照片信息）
        public int UpdateFingerAndPic(Trainman model, out bool updateFingerPrint, out bool updatePicture)
        {
            updateFingerPrint = false;
            updatePicture = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update TAB_Org_Trainman set ");
            string strValue = string.Empty;

            if (model == null)
            {
                return 2;
            }
            strValue = "";

            if ((model.FingerPrint1 != null) && ((string)model.FingerPrint1 != string.Empty))
            {
                strValue += " FingerPrint1 = @FingerPrint1 ";
                model.FingerPrint1 = ConvertBase64ToByte(model.FingerPrint1);
                updateFingerPrint = true;
            }
            if ((model.FingerPrint2 != null) && ((string)model.FingerPrint2 != string.Empty))
            {
                if (strValue != "")
                {
                    strValue += " , FingerPrint2 = @FingerPrint2";
                }
                else
                {
                    strValue += " FingerPrint2 = @FingerPrint2";
                }
                model.FingerPrint2 = ConvertBase64ToByte(model.FingerPrint2.ToString());
                updateFingerPrint = true;
            }

            if ((model.Picture != null) && ((string)model.Picture != string.Empty))
            {
                if (strValue != "")
                {
                    strValue += " , Picture = @Picture ";
                }
                else
                {
                    strValue += "  Picture = @Picture ";
                }

                model.Picture = ConvertBase64ToByte(model.Picture.ToString());
                updatePicture = true;
            }



            if (string.IsNullOrEmpty(strValue))
            {
                return 3;
            }


            strSql.Append(strValue);
            strSql.Append(" where strTrainmanGUID = @strTrainmanGUID ");
            SqlParameter[] parameters = {
                new SqlParameter("@strTrainmanGUID", model.strTrainmanGUID),
                new SqlParameter("@FingerPrint1", model.FingerPrint1),
                new SqlParameter("@FingerPrint2", model.FingerPrint2),
                new SqlParameter("@Picture", model.Picture)
              };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion

        #region SetTrainmanState（SetTrainmanState）
        public int SetTrainmanState(int nTrainmanState,string TrainmanGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TAB_Org_Trainman set nTrainmanState = " + nTrainmanState + " where strTrainmanGUID='" + TrainmanGUID + "' ");
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql.ToString());
        }
        #endregion

        #region 修改人员所属机构

        #region  ====================================修改人员的所属机务段====================================

        /// <summary>
        /// 修改人员的所属机务段
        /// </summary>
        /// <param name="TrainmanGUID"></param>
        /// <param name="strWorkShopGUID"></param>
        /// <returns></returns>
        public bool UpdateArea(string TrainmanGUID, string strAreaGUID)
        {
            string sql = @"update TAB_Org_Trainman set strAreaGUID=@strAreaGUID,strGuideGroupGUID='' where strTrainmanGUID=@strTrainmanGUID";
            using (var conn = GetConnection())
            {
                try
                {
                    return conn.Execute(sql, new { strAreaGUID = strAreaGUID, strTrainmanGUID = TrainmanGUID }) > 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 添加名牌变动日志
        /// </summary>
        /// <param name="strTrainmanGUID"></param>
        /// <param name="strWorkShopGUID"></param>
        /// <param name="DutyUserGUID"></param>
        /// <param name="DutyUserNumber"></param>
        /// <param name="DutyUserName"></param>
        public void addLog4UpdateArea(Tm tm, string strAreaGUID, string DutyUserGUID, string DutyUserNumber, string DutyUserName)
        {
            Area area = getArea(strAreaGUID);
            if (area == null)
                throw new Exception("找不到该务段！,请核对后再试！");
            string strContent = string.Format("司机：{0}【{1}】 改变了机务段 ：由【{2}】 更改为【{3}】", tm.strTrainmanName, tm.strTrainmanNumber, tm.strAreaName, area.strAreaName);
            SaveChangeLog(strContent, tm.strTrainmanJiaoluGUID, tm.strTrainmanJiaoluName, 12, DutyUserGUID, DutyUserNumber, DutyUserName);
        }

        /// <summary>
        /// 通过机务段号获取机务段信息
        /// </summary>
        /// <param name="strWorkShopGUID"></param>
        /// <returns></returns>
        public Area getArea(string strGUID)
        {
            string sql = "select * from TAB_Org_Area where strGUID=@strGUID";
            using (var conn = GetConnection())
            {
                return conn.Query<Area>(sql, new { strGUID = strGUID }).FirstOrDefault();
            }
        }


        /// <summary>
        /// 车间相关属性
        /// </summary>
        public class Area
        {
            public string strGUID;
            public string strAreaName;
        }

        #endregion

        #region  ====================================修改人员的所属车间====================================
        /// <summary>
        /// 更新人员所属车间
        /// </summary>
        /// <param name="TrainmanGUID"></param>
        /// <param name="strWorkShopGUID"></param>
        /// <returns></returns>
        public bool UpdateWorkShop(string TrainmanGUID, string strWorkShopGUID)
        {
            string sql = @"update TAB_Org_Trainman set strWorkShopGUID=@strWorkShopGUID,strGuideGroupGUID='' where strTrainmanGUID=@strTrainmanGUID";
            using (var conn = GetConnection())
            {
                try
                {
                    return conn.Execute(sql, new { strWorkShopGUID = strWorkShopGUID, strTrainmanGUID = TrainmanGUID }) > 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 判断人员是否在牌子里，如果不在就可以执行修改
        /// </summary>
        /// <param name="TrainmanGUID"></param>
        /// <returns></returns>
        public bool CheckIsCanUpdata(string TrainmanGUID)
        {
            string sql = @"  select COUNT(*) from  TAB_Nameplate_Group where strTrainmanGUID1=@strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID";
            using (var conn = GetConnection())
            {
                return conn.Query<int>(sql, new { strTrainmanGUID = TrainmanGUID }).FirstOrDefault() <= 0;
            }
        }



        public bool CheckIsCanDelUnRun(string strTrainmanGUID)
        {

            string sql = @"  select COUNT(*) from  TAB_Org_Trainman where strTrainmanGUID=@strTrainmanGUID and nTrainmanState=0";
            using (var conn = GetConnection())
            {
                return conn.Query<int>(sql, new { strTrainmanGUID = strTrainmanGUID }).FirstOrDefault() <= 0;
            }
        }


        /// <summary>
        /// 判断人员是否有计划，如果有计划则不能修改
        /// </summary>
        /// <param name="TrainmanGUID"></param>
        /// <returns></returns>
        public bool CheckIsCanUpdataByPlan(string TrainmanGUID)
        {
            string sql = @"  select COUNT(*) from  TAB_Plan_Trainman where strTrainmanGUID1=@strTrainmanGUID or strTrainmanGUID2=@strTrainmanGUID or strTrainmanGUID3=@strTrainmanGUID or strTrainmanGUID4=@strTrainmanGUID";
            using (var conn = GetConnection())
            {
                return conn.Query<int>(sql, new { strTrainmanGUID = TrainmanGUID }).FirstOrDefault() <= 0;
            }
        }



        /// <summary>
        /// 添加车间变动日志
        /// </summary>
        /// <param name="strTrainmanGUID"></param>
        /// <param name="strWorkShopGUID"></param>
        /// <param name="DutyUserGUID"></param>
        /// <param name="DutyUserNumber"></param>
        /// <param name="DutyUserName"></param>
        public void addLog4UpdateWs(Tm tm, string strWorkShopGUID, string DutyUserGUID, string DutyUserNumber, string DutyUserName)
        {
            workshop ws = new workshop();
            string strNewName = "";
            if (strWorkShopGUID != "")
            {
                 ws = getWorkshop(strWorkShopGUID);
                 strNewName = ws.strWorkShopName;
            }
            string strContent = string.Format("司机：{0}【{1}】 改变了车间 ：由【{2}】 更改为【{3}】", tm.strTrainmanName, tm.strTrainmanNumber, tm.strWorkShopName, strNewName);
            SaveChangeLog(strContent, tm.strTrainmanJiaoluGUID, tm.strTrainmanJiaoluName, 11, DutyUserGUID, DutyUserNumber, DutyUserName);
        }

        /// <summary>
        /// 通过司车间id获取车间名称
        /// </summary>
        /// <param name="strWorkShopGUID"></param>
        /// <returns></returns>
        public workshop getWorkshop(string strWorkShopGUID)
        {
            string sql = "select * from TAB_Org_WorkShop where strWorkShopGUID=@strWorkShopGUID";
            using (var conn = GetConnection())
            {
                return conn.Query<workshop>(sql, new { strWorkShopGUID = strWorkShopGUID }).FirstOrDefault();
            }
        }

        /// <summary>
        /// 车间相关属性
        /// </summary>
        public class workshop
        {
            public string strWorkShopName;
            public string strWorkShopGUID;
        }

        #endregion

        #region  ====================================修改人员的所属行车区段====================================

        /// <summary>
        /// 修改人员的所属机务段
        /// </summary>
        /// <param name="TrainmanGUID"></param>
        /// <param name="strWorkShopGUID"></param>
        /// <returns></returns>
        public bool UpdateTrainJiaolu(string TrainmanGUID, string strTrainJiaoluGUID)
        {
            string sql = @"update TAB_Org_Trainman set strTrainJiaoluGUID=@strTrainJiaoluGUID,strGuideGroupGUID='' where strTrainmanGUID=@strTrainmanGUID";
            using (var conn = GetConnection())
            {
                try
                {
                    return conn.Execute(sql, new { strTrainJiaoluGUID = strTrainJiaoluGUID, strTrainmanGUID = TrainmanGUID }) > 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 添加名牌变动日志
        /// </summary>
        /// <param name="strTrainmanGUID"></param>
        /// <param name="strWorkShopGUID"></param>
        /// <param name="DutyUserGUID"></param>
        /// <param name="DutyUserNumber"></param>
        /// <param name="DutyUserName"></param>
        public void addLog4UpdateTrainmanJiaolu(Tm tm, string TrainJiaoluid, string DutyUserGUID, string DutyUserNumber, string DutyUserName)
        {
            TrainJiaolu trainJiaolu = new TrainJiaolu();
            string jiaoluname = "";
            if (TrainJiaoluid != "")
            {
                trainJiaolu = getTrainJiaolu(TrainJiaoluid);
                jiaoluname = trainJiaolu.strTrainJiaoluName;
            }
            string strContent = string.Format("司机：{0}【{1}】 改变了行车区段 ：由【{2}】 更改为【{3}】", tm.strTrainmanName, tm.strTrainmanNumber, tm.strTrainJiaoluName, jiaoluname);
            SaveChangeLog(strContent, tm.strTrainmanJiaoluGUID, tm.strTrainmanJiaoluName, 13, DutyUserGUID, DutyUserNumber, DutyUserName);
        }

        /// <summary>
        /// 通过机务段号获取机务段信息
        /// </summary>
        /// <param name="strWorkShopGUID"></param>
        /// <returns></returns>
        public TrainJiaolu getTrainJiaolu(string strTrainJiaoluGUID)
        {
            string sql = "select * from TAB_Base_TrainJiaolu where strTrainJiaoluGUID=@strTrainJiaoluGUID";
            using (var conn = GetConnection())
            {
                return conn.Query<TrainJiaolu>(sql, new { strTrainJiaoluGUID = strTrainJiaoluGUID }).FirstOrDefault();
            }
        }

        /// <summary>
        /// 车间相关属性
        /// </summary>
        public class TrainJiaolu
        {
            public string strTrainJiaoluGUID;
            public string strTrainJiaoluName;
        }

        #endregion

        #region  ====================================删除人员的 名牌变动日志====================================
        /// <summary>
        /// 添加名牌变动日志
        /// </summary>
        /// <param name="strTrainmanGUID"></param>
        /// <param name="strWorkShopGUID"></param>
        /// <param name="DutyUserGUID"></param>
        /// <param name="DutyUserNumber"></param>
        /// <param name="DutyUserName"></param>
        public void addLog4DelTrainMan(Tm tm, string DutyUserGUID, string DutyUserNumber, string DutyUserName)
        {
            string strContent = string.Format("司机：{0}【{1}】 被删除", tm.strTrainmanName, tm.strTrainmanNumber);
            SaveChangeLog(strContent, tm.strTrainmanJiaoluGUID, tm.strTrainmanJiaoluName, 14, DutyUserGUID, DutyUserNumber, DutyUserName);
        }
        #endregion

        #region   ====================================公共方法====================================


        /// <summary>
        /// 判断车间是否位于该机务段下
        /// </summary>
        /// <param name="AreaId"></param>
        /// <param name="WorkShopId"></param>
        /// <returns></returns>
        public bool checkWorkShopInArea(string strAreaGUID, string strWorkShopGUID)
        {
            string sql = "select * from TAB_Org_WorkShop where strAreaGUID=@strAreaGUID and strWorkShopGUID=@strWorkShopGUID";
            using (var conn = GetConnection())
            {
                return conn.Query<object>(sql, new { strAreaGUID = strAreaGUID, strWorkShopGUID = strWorkShopGUID }).ToList().Count() > 0;
            }
        }


        /// <summary>
        /// 判断车间是否位于该机务段下
        /// </summary>
        /// <param name="AreaId"></param>
        /// <param name="WorkShopId"></param>
        /// <returns></returns>
        public bool checkJiaoluInWorkShop(string strWorkShopGUID, string strTrainJiaoluGUID)
        {
            string sql = "select * from TAB_Base_TrainJiaolu where strWorkShopGUID=@strWorkShopGUID and strTrainJiaoluGUID=@strTrainJiaoluGUID";
            using (var conn = GetConnection())
            {
                return conn.Query<object>(sql, new { strWorkShopGUID = strWorkShopGUID, strTrainJiaoluGUID = strTrainJiaoluGUID }).ToList().Count() > 0;
            }
        }



        /// <summary>
        /// 通过司机的工号获取司机的相关信息
        /// </summary>
        /// <param name="strTrainmanGUID"></param>
        /// <returns></returns>
        public Tm getTm(string strTrainmanNumber)
        {
            string sql = "select strTrainJiaoluName,strTrainmanNumber,strTrainmanGUID,strTrainmanName,strTrainmanJiaoluGUID,strTrainmanJiaoluName,strWorkShopName,strAreaName,strWorkShopGUID,strJiWuDuanGUID,strTrainJiaoluGUID from VIEW_Org_Trainman where strTrainmanNumber=@strTrainmanNumber";
            using (var conn = GetConnection())
            {
                return conn.Query<Tm>(sql, new { strTrainmanNumber = strTrainmanNumber }).FirstOrDefault();
            }
        }


        /// <summary>
        /// 通过司机的工号获取司机的相关信息
        /// </summary>
        /// <param name="strTrainmanGUID"></param>
        /// <returns></returns>
        public Tm getTmByID(string strTrainmanGUID)
        {
            string sql = "select strTrainJiaoluName,strTrainmanNumber,strTrainmanGUID,strTrainmanName,strTrainmanJiaoluGUID,strTrainmanJiaoluName,strWorkShopName,strAreaName,strWorkShopGUID,strJiWuDuanGUID,strTrainJiaoluGUID from VIEW_Org_Trainman where strTrainmanGUID=@strTrainmanGUID";
            using (var conn = GetConnection())
            {
                return conn.Query<Tm>(sql, new { strTrainmanGUID = strTrainmanGUID }).FirstOrDefault();
            }
        }


        /// <summary>
        /// 保存变动日志
        /// </summary>
        /// <param name="LogContent"></param>
        /// <param name="jiaoluID"></param>
        /// <param name="jiaoluName"></param>
        /// <param name="changeType"></param>
        /// <param name="DutyUserGUID"></param>
        /// <param name="DutyUserNumber"></param>
        /// <param name="DutyUserName"></param>
        public void SaveChangeLog(string LogContent, string jiaoluID, string jiaoluName, int changeType, string DutyUserGUID, string DutyUserNumber, string DutyUserName)
        {
            string strLogGUID = Guid.NewGuid().ToString();
            string strSql = @"insert into TAB_Nameplate_Log (strLogGUID,strTrainmanJiaoluGUID,
                        strTrainmanJiaoluName,nBoardChangeType,strContent,strDutyUserGUID,
                        strDutyUserNumber,strDutyUserName,dtEventTime) values 
                        (@strLogGUID,@strTrainmanJiaoluGUID,@strTrainmanJiaoluName,@nBoardChangeType,
                        @strContent,@strDutyUserGUID,@strDutyUserNumber,@strDutyUserName,getdate())";
            SqlParameter[] sqlParams = new SqlParameter[] {
                      new SqlParameter("strLogGUID",strLogGUID),
                      new SqlParameter("strTrainmanJiaoluGUID",jiaoluID),
                      new SqlParameter("strTrainmanJiaoluName",jiaoluName),
                      new SqlParameter("nBoardChangeType",changeType),
                      new SqlParameter("strContent",LogContent),
                      new SqlParameter("strDutyUserGUID",DutyUserGUID),
                      new SqlParameter("strDutyUserNumber",DutyUserNumber),
                      new SqlParameter("strDutyUserName",DutyUserName)
                  };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        }

        /// <summary>
        /// 司机的相关日志的属性
        /// </summary>
        public class Tm
        {
            public string strTrainmanNumber;
            public string strTrainmanName;
            public string strTrainmanJiaoluGUID;
            public string strTrainmanJiaoluName;
            public string strWorkShopName;
            public string strAreaName;
            public string strWorkShopGUID;
            public string strJiWuDuanGUID;
            public string strTrainJiaoluGUID;
            public string strTrainmanGUID;
            public string strTrainJiaoluName;
        }

        #endregion
        #endregion

    }

}
