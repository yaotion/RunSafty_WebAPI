using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.RunSafty.bll.SynLog.Model;
using Dapper;

namespace TF.RunSafty.bll.SynLog
{
    public class DBSynLog : DBUtility
    {
        public static void GetVersionRange(string Identifier, out int minVersion, out int maxVersion)
        {
            minVersion = 0;
            maxVersion = 0;
            ChangeVersion_Model model = null;
            string sql = "select MinVersion,MaxVersion from TAB_Sync_ChangeVersion where Identifier=@Identifier;";
            using (var conn = GetConnection())
            {
                model = conn.Query<ChangeVersion_Model>(sql, new { Identifier = Identifier }).FirstOrDefault();
            }
            if (model != null)
            {
                minVersion = model.MinVersion;
                maxVersion = model.MaxVersion;
            }
            else
            {
                minVersion = 1;
                maxVersion = 1;
            }
        }

        public static List<ChangeLog_Model> GetChangeLog(string strIdentifier, int nVersion)
        {

            string sql = "select * from TAB_Sync_ChangeLog where Identifier=@Identifier and Version>@Version;";
            using (var conn = GetConnection())
            {
                return conn.Query<ChangeLog_Model>(sql, new { Version = nVersion, Identifier = strIdentifier }).ToList();
            }
        }


        public static bool AddLog(LogQueue_Model model)
        {

            string sql = @"if object_id(N'TAB_Sync_LogQueue',N'U') is not null
                            insert into TAB_Sync_LogQueue(ChangeType,Identifier,[Key],data) values(@ChangeType,@Identifier,@Key,@data);";
            model.data = Encoding.UTF8.GetBytes(model.strdata);
            using (var conn = GetConnection())
            {
                return conn.Execute(sql, model) > 0;
            }

        }



        public static LogQueue_Model getLogQueue(string nid)
        {
            string sql = "select * from TAB_Sync_LogQueue where ID=@ID";
            using (var conn = GetConnection())
            {
                return conn.Query<LogQueue_Model>(sql, new { ID = nid }).FirstOrDefault();
            }
        }
    }
}
