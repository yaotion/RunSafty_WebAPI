using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.CommonUtility;
using System.Text;

namespace TF.RunSafty.SyncDemo
{
    public enum ChangeType {ctAdd = 1,ctUpdate = 2,ctDel = 3};

    public class ChangeLog
    {
        public ChangeType ChangeType;
        public string Key;
        public string Identifier;
        public int Version;
    }

    /// <summary>
    /// 在实际使用中，由于Notifyer要生成版本号及保持CHANGELOG最多只有1000条记录，需要把此类做成全局对象，使用缓冲的方式实现
    /// </summary>
    public class ChangeLogNotifyer
    {

      
        public void NotifyChange(string Identifyer,string key,ChangeType changeType)
        {
         
            SqlParameter[] sqlparams = { 
                                           new SqlParameter("nChangeType",Convert.ToInt32(changeType)),
                                           new SqlParameter("strIdentifier",Identifyer),
                                           new SqlParameter("strKey",key)

                                       };
            string sql = "insert into TAB_Sync_LogQueue (nChangeType,strIdentifier,strKey) values(@nChangeType,@strIdentifier,@strKey)";

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, sqlparams);
        }


        /// <summary>
        /// 传入开始版本号及要获取的个数，返回实际获取个数
        /// </summary>
        /// <param name="startVersion"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int GetChangeLog(string identifier,int startVersion,int count,List<ChangeLog> logs)
        {
            string sql = "select top {0} * from TAB_Sync_ChangeLog where strIdentifier = @strIdentifier and nVersion >= @nVersion";
            sql = string.Format(sql, count);
            SqlParameter[] sqlparams = { 
                                           new SqlParameter("strIdentifier",identifier),
                                           new SqlParameter("nVersion",startVersion),
                                       };


            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, sqlparams).Tables[0];

            foreach (DataRow dr in dt.Rows)
            { 
                ChangeLog changeLog = new ChangeLog();
                logs.Add(changeLog);
                changeLog.ChangeType = (ChangeType)Convert.ToInt32(dr["nChangeType"]);
                changeLog.Identifier = dr["strIdentifier"].ToString();
                changeLog.Key = dr["strKey"].ToString();
                changeLog.Version = Convert.ToInt32(dr["nVersion"]); 
            }

            return dt.Rows.Count;
        }

        public static void GetVersionRange(string Identifier,out int minVersion,out int maxVersion)
        {
            string sql = "select nMaxVersion,nMinVersion from TAB_Sync_ChangeVersion where strIdentifier = @strIdentifier";
            SqlParameter[] param = {
                                       new SqlParameter("strIdentifier",Identifier)                              
                                   };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, param).Tables[0];

            if (dt.Rows.Count > 0)
            {
                maxVersion = Convert.ToInt32(dt.Rows[0]["nMaxVersion"]);
                minVersion = Convert.ToInt32(dt.Rows[0]["nMinVersion"]);                
            }

            else
            {
                maxVersion = 1;
                minVersion = 1;                
            }
        }

        public static Boolean CheckVersionExpires(DataVersion version)
        {
            int minVersion, maxVersion;
            ChangeLogNotifyer.GetVersionRange(version.Identifier, out minVersion, out maxVersion);
            return version.Version < minVersion;
        }
    }
    
    
    public class DBTMAccess
    {
        private ChangeLogNotifyer notifyer = new ChangeLogNotifyer();
        private const string Identifyer = "RSTM";

        public void Add(RsTM tm)
        {
            string sql = @"insert into TAB_Org_Trainman (strTrainmanGUID,strTrainmanNumber,strTrainmanName,
strTelNumber,strTrainmanJiaoluGUID) values(@tmGUID,@tmNumber,@tmName,@telNumber,@jlGUID)";
            SqlParameter[] sqlparams = {
                                           new SqlParameter("tmGUID",tm.ID),
                                           new SqlParameter("tmNumber",tm.Number),
                                           new SqlParameter("tmName",tm.Name),
                                           new SqlParameter("telNumber",tm.Tel),
                                           new SqlParameter("jlGUID",tm.Jl),

                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,sql,sqlparams);
            notifyer.NotifyChange(Identifyer, tm.ID, ChangeType.ctAdd);
        }
        public void Update(RsTM tm)
        {
            string sql = @"update TAB_Org_Trainman set strTrainmanNumber = @tmNumber,strTrainmanName = @tmName,
strTelNumber = @telNumber,strTrainmanJiaoluGUID = @jlGUID where strTrainmanGUID = @tmGUID";
            SqlParameter[] sqlparams = {
                                           new SqlParameter("tmGUID",tm.ID),
                                           new SqlParameter("tmNumber",tm.Number),
                                           new SqlParameter("tmName",tm.Name),
                                           new SqlParameter("telNumber",tm.Tel),
                                           new SqlParameter("jlGUID",tm.Jl),

                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, sqlparams);
            notifyer.NotifyChange(Identifyer,tm.ID, ChangeType.ctUpdate);
        }
        public void Del(string id)
        {
            string sql = "delete from TAB_Org_Trainman where strTrainmanGUID = @TrainmanGUID";
            SqlParameter[] sqlparams = {
                                           new SqlParameter("TrainmanGUID",id)
                                       };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, sqlparams);
            notifyer.NotifyChange(Identifyer,id, ChangeType.ctDel);
        
        }
        public RsTM Get(string id)
        {
            string sql = "select * from TAB_Org_Trainman where strTrainmanGUID = @TrainmanGUID";
            SqlParameter[] sqlparams = {
                                           new SqlParameter("TrainmanGUID",id)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, sqlparams).Tables[0];

            if (dt.Rows.Count > 0)
            { 
                RsTM tm = new RsTM();

                tm.ID = dt.Rows[0]["strTrainmanGUID"].ToString();
                tm.Number = dt.Rows[0]["strTrainmanNumber"].ToString();
                tm.Name = dt.Rows[0]["strTrainmanName"].ToString();
                tm.Tel = dt.Rows[0]["strTelNumber"].ToString();
                tm.Jl = dt.Rows[0]["strTrainmanJiaoluGUID"].ToString();                


                return tm;
            }
            else
                return null;
        }

        public List<RsTM> GetAll()
        {
            List<RsTM> _ret = new List<RsTM>();

            string sql = "select strTrainmanGUID,strTrainmanNumber,strTrainmanName,strTelNumber,strTrainmanJiaoLuGUID from TAB_Org_Trainman";

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                RsTM tm = new RsTM();

                tm.ID = dr["strTrainmanGUID"].ToString();
                tm.Number = dr["strTrainmanNumber"].ToString();
                tm.Name = dr["strTrainmanName"].ToString();
                tm.Tel = dr["strTelNumber"].ToString();
                tm.Jl = dr["strTrainmanJiaoluGUID"].ToString();
                _ret.Add(tm);
            }          

            return _ret;
        }

    }
}
