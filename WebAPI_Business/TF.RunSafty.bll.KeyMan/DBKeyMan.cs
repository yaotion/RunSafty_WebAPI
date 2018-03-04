using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.CommonUtility;

namespace TF.RunSafty.KeyMan
{
    public enum ParamDataType 
    {
        dtUnknown,
        dtString,
        dtInt,
        dtDateTime
    }

    public class ParamValuePair
    {
        public string fieldName;        
        public object value;
        public ParamDataType dataType;
    }
    public class DbParamDict
    {        
        private List<ParamValuePair> FieldsDict = new List<ParamValuePair>();
        
        private string GetParamString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < FieldsDict.Count; i++)
            {
                sb.Append("@" + FieldsDict[i].fieldName);
                if (i < FieldsDict.Count - 1)
                {
                    sb.Append(",");
                }

            }

            return sb.ToString();
        }

        public string GetFieldsString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < FieldsDict.Count; i++)
            {
                sb.Append(FieldsDict[i].fieldName);
                if (i < FieldsDict.Count - 1)
                {
                    sb.Append(",");
                }

            }

            return sb.ToString();
        }

        public void Add(string FieldName,object value,ParamDataType dataType)
        {
            ParamValuePair paramPair = new ParamValuePair();
            
            paramPair.value = value;
            paramPair.fieldName = FieldName;
            paramPair.dataType = dataType;
            FieldsDict.Add(paramPair);
        }

        public void Add(string FieldName, object value)
        {
            Add(FieldName,value,ParamDataType.dtUnknown);
        }

        public string GetSqlString()
        {
            StringBuilder sb = new StringBuilder();
         
            sb.Append(" (");
            sb.Append(GetFieldsString());            
            sb.Append(") ");
            sb.Append("  values(");
            sb.Append(GetParamString());            
            sb.Append(")");

            return sb.ToString();
        }

        public string GetUpdateFieldsString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" set ");
            for (int i = 0; i < FieldsDict.Count; i++)
            {
                sb.Append(FieldsDict[i].fieldName + "=@" + FieldsDict[i].fieldName);

                if (i < FieldsDict.Count - 1)
                {
                    sb.Append(",");
                }
                
            }
                    
            return sb.ToString();
        }
      
        public SqlParameter[] GetParams()
        {
            List<SqlParameter> ret = new List<SqlParameter>();
            SqlParameter param;
            for (int i = 0; i < FieldsDict.Count; i++)
            {
                if (FieldsDict[i].value == null)
                {
                    param = new SqlParameter(FieldsDict[i].fieldName, DBNull.Value);
                }
                else
                {
                    param = new SqlParameter(FieldsDict[i].fieldName, FieldsDict[i].value);
                }

                switch (FieldsDict[i].dataType)
                {
                    case ParamDataType.dtString: param.SqlDbType = SqlDbType.VarChar; break;
                    case ParamDataType.dtInt: param.SqlDbType = SqlDbType.Int; break;
                    case ParamDataType.dtDateTime: param.SqlDbType = SqlDbType.DateTime; break;                                     
                }

             
                ret.Add(param);
            }


            return ret.ToArray();            
        }


        public void Clear()
        {
            FieldsDict.Clear();
        }

    
    }

    class DBKeyMan
    {
        private void UpdateTMKeyState(int bIsKey,string number)
        {
            string sql = "update tab_org_Trainman set bIsKey = @bIsKey where strTrainmanNumber = @strTrainmanNumber";
            DbParamDict dbParamDict = new DbParamDict();
            dbParamDict.Add("bIsKey",bIsKey);
            dbParamDict.Add("strTrainmanNumber",number);

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, dbParamDict.GetParams());
        }
        private void AddAllFields(DbParamDict dbParamDict, KeyMan keyman)
        {
            dbParamDict.Add("strGUID", keyman.ID);
            dbParamDict.Add("strTrainmanNumber", keyman.KeyTMNumber);
            dbParamDict.Add("strTrainmanName", keyman.KeyTMName);
            dbParamDict.Add("strWorkShopGUID", keyman.KeyTMWorkShopGUID);
            dbParamDict.Add("strWorkShopName", keyman.KeyTMWorkShopName);
            dbParamDict.Add("strCheJianGUID", keyman.KeyTMCheDuiGUID);
            dbParamDict.Add("strCheJianName", keyman.KeyTMCheDuiName);
            dbParamDict.Add("dtStartTime", keyman.KeyStartTime);
            dbParamDict.Add("dtEndTime", keyman.KeyEndTime);
            dbParamDict.Add("strReason", keyman.KeyReason);
            dbParamDict.Add("strAnnouncements", keyman.KeyAnnouncements);
            dbParamDict.Add("strRegisterNumber", keyman.RegisterNumber);
            dbParamDict.Add("strRegisterName", keyman.RegisterName);
            dbParamDict.Add("dtRegisterTime", keyman.RegisterTime);
            dbParamDict.Add("strClientNumber", keyman.ClientNumber);
            dbParamDict.Add("strClientName", keyman.ClientName);
            dbParamDict.Add("eOpt", keyman.eOpt);
        }
        private void InnerAdd(KeyMan keyman,string TableName)
        {
            DbParamDict dbParamDict = new DbParamDict();
            
            AddAllFields(dbParamDict,keyman);

            string sql = "insert into " + TableName + dbParamDict.GetSqlString();
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, dbParamDict.GetParams());

        }

        private Boolean TMIsKey(string number)
        {
            string sql = "select count(*) from tab_keyTrainman where strTrainmanNumber = @strTrainmanNumber";
            SqlParameter[] param = { 
                                       new SqlParameter("strTrainmanNumber",number)
                                   };
            param[0].SqlDbType = SqlDbType.VarChar;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, param)) > 0;            
        }

        public void Add(KeyMan keyman)
        {
            if (TMIsKey(keyman.KeyTMNumber))
            {
                throw new Exception(string.Format("[{0}]{1}已经是关键人!", keyman.KeyTMNumber, keyman.KeyTMName));
            }
            keyman.eOpt = Convert.ToInt32(EKeyTrainmanOpt.KTMAdd);
            InnerAdd(keyman, "tab_keyTrainman");
            UpdateTMKeyState(1, keyman.KeyTMNumber);
            InnerAdd(keyman, "tab_keyTrainman_His");
        }

        public void Del(KeyMan keyman)
        {
            string sql = "delete from tab_keyTrainman where strGUID = @strGUID";
            DbParamDict dbParamDict = new DbParamDict();

            dbParamDict.Add("strGUID",keyman.ID);

            keyman.eOpt = Convert.ToInt32(EKeyTrainmanOpt.KTMdel);

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, dbParamDict.GetParams());
            UpdateTMKeyState(0, keyman.KeyTMNumber);

            InnerAdd(keyman, "tab_keyTrainman_His");
        }

        public void Update(KeyMan keyman)
        {
            
            DbParamDict dbParamDict = new DbParamDict();

            keyman.eOpt = Convert.ToInt32(EKeyTrainmanOpt.KTMModify);

            AddAllFields(dbParamDict, keyman);

            string sql = "update tab_keyTrainman " + dbParamDict.GetUpdateFieldsString() + " where strGUID = @strGUID";

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, dbParamDict.GetParams());

            InnerAdd(keyman, "tab_keyTrainman_His");
        }

        public void Clear(string workShopGUID,string cheDuiGUID)
        {
            DbParamDict dbParamDict = new DbParamDict();
            dbParamDict.Add("strGUID","");
            dbParamDict.Add("strTrainmanNumber", "");
            dbParamDict.Add("strTrainmanName", "");
            dbParamDict.Add("strWorkShopGUID", "");
            dbParamDict.Add("strWorkShopName", "");
            dbParamDict.Add("strCheJianGUID", "");
            dbParamDict.Add("strCheJianName", "");
            dbParamDict.Add("dtStartTime", "");
            dbParamDict.Add("dtEndTime", "");
            dbParamDict.Add("dtRegisterTime", "");
            dbParamDict.Add("strReason", "");
            dbParamDict.Add("strAnnouncements", "");
            dbParamDict.Add("strRegisterNumber", "");
            dbParamDict.Add("strRegisterName", "");
            dbParamDict.Add("strClientNumber", "");
            dbParamDict.Add("strClientName", "");
            string sql = "insert into  tab_keyTrainman_His ("+ dbParamDict.GetFieldsString()

                + ",eOpt) select " +
                dbParamDict.GetFieldsString()
                +",2 from  tab_keyTrainman where strWorkShopGUID = @strWorkShopGUID ";


            if (!string.IsNullOrEmpty(cheDuiGUID))
            {
                sql += " and strCheJianGUID = @strCheJianGUID";
            }
            dbParamDict.Clear();
            dbParamDict.Add("strWorkShopGUID", workShopGUID);
            dbParamDict.Add("strCheJianGUID", cheDuiGUID);

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, dbParamDict.GetParams());

            sql = "delete from tab_keyTrainman where strWorkShopGUID = @strWorkShopGUID";

            if (!string.IsNullOrEmpty(cheDuiGUID))
            {
                sql += " and strCheJianGUID = @strCheJianGUID";
            }

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, dbParamDict.GetParams());
        
        }

        private List<KeyMan> InnerGet(KMQueryQC queryQC,string TableName)
        {
            List<KeyMan> ret = new List<KeyMan>();
            string sql = "select * from " + TableName + " where 1=1 ";

            if (!string.IsNullOrEmpty(queryQC.WorkShopGUID))
            {
                sql += " and strWorkShopGUID = @strWorkShopGUID";
            }

            if (!string.IsNullOrEmpty(queryQC.CheDuiGUID))
            {
                sql += " and strCheJianGUID = @strCheJianGUID";
            }


            if (!string.IsNullOrEmpty(queryQC.KeyTMNumber))
            {
                sql += " and strTrainmanNumber = @strTrainmanNumber";
            }

            if (!string.IsNullOrEmpty(queryQC.KeyTMName))
            {
                sql += " and strTrainmanName = @strTrainmanName";
            }


            if (queryQC.RegisterEndTime != null)
            {
                sql += " and dtRegisterTime >= @dtRegisterStartTime";
            }


            if (queryQC.RegisterStartTime != null)
            {
                sql += " and dtRegisterTime <= @dtRegisterEndTime";
            }

            sql += " order by dtRegisterTime desc,eOpt desc";

            DbParamDict dbParamDict = new DbParamDict();

            dbParamDict.Add("strWorkShopGUID", queryQC.WorkShopGUID,ParamDataType.dtString);
            dbParamDict.Add("strCheJianGUID", queryQC.CheDuiGUID, ParamDataType.dtString);
            dbParamDict.Add("strTrainmanNumber", queryQC.KeyTMNumber, ParamDataType.dtString);
            dbParamDict.Add("strTrainmanName", queryQC.KeyTMName, ParamDataType.dtString);
            dbParamDict.Add("dtRegisterStartTime", queryQC.RegisterStartTime, ParamDataType.dtDateTime);
            dbParamDict.Add("dtRegisterEndTime", queryQC.RegisterEndTime, ParamDataType.dtDateTime);

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, dbParamDict.GetParams()).Tables[0];

            KeyMan keyman;
            foreach (DataRow dr in dt.Rows)
            {
                keyman = new KeyMan();

                keyman.ID = ObjectConvertClass.static_ext_string(dr["strGUID"]);
                keyman.KeyAnnouncements = ObjectConvertClass.static_ext_string(dr["strAnnouncements"]);
                keyman.KeyEndTime = ObjectConvertClass.static_ext_date(dr["dtEndTime"]);
                keyman.KeyReason = ObjectConvertClass.static_ext_string(dr["strReason"]);
                keyman.KeyStartTime = ObjectConvertClass.static_ext_date(dr["dtStartTime"]);
                keyman.KeyTMCheDuiGUID = ObjectConvertClass.static_ext_string(dr["strCheJianGUID"]);
                keyman.KeyTMCheDuiName = ObjectConvertClass.static_ext_string(dr["strCheJianName"]);
                keyman.KeyTMName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
                keyman.KeyTMNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
                keyman.KeyTMWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
                keyman.KeyTMWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
                keyman.RegisterName = ObjectConvertClass.static_ext_string(dr["strRegisterName"]);
                keyman.RegisterNumber = ObjectConvertClass.static_ext_string(dr["strRegisterNumber"]);
                keyman.RegisterTime = ObjectConvertClass.static_ext_date(dr["dtRegisterTime"]);
                keyman.ClientNumber = ObjectConvertClass.static_ext_string(dr["strClientNumber"]);
                keyman.ClientName = ObjectConvertClass.static_ext_string(dr["strClientName"]);
                keyman.eOpt = ObjectConvertClass.static_ext_int(dr["eOpt"]);

                ret.Add(keyman);
            }


            return ret;
        }
        public List<KeyMan> Get(KMQueryQC queryQC)
        {
            return InnerGet(queryQC, "tab_keyTrainman");            
        }

        public List<KeyMan> GetHistory(KMQueryQC queryQC)
        {
            return InnerGet(queryQC, "tab_keyTrainman_His");
        }
    }
}
