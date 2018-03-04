using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TF.CommonUtility;
using System.Data;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;

namespace TF.RunSafty.TMIS
{
   public class DBTmisStation
    {

       //public List<MDStation> getRegionList()
       //{
       //    StringBuilder strSql = new StringBuilder();
       //    strSql.Append("select * from Tab_Tmis_TrainJiaoLuAndTmisStation TTJL left join TAB_Base_BYRegion b on TTJL.strBYRegionID=b.RegionID ");
       //    DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
       //    List<MDStation> Lmd = new List<MDStation>();
       //    for (int i = 0; i < dt.Rows.Count; i++)
       //    {
       //        MDStation md = new MDStation();
       //        md.RegionID = dt.Rows[i]["RegionID"].ToString();
       //        md.RegionName=dt.Rows[i]["RegionName"].ToString();
       //        md.strTrainJiaoluGUID = dt.Rows[i]["strTrainJiaoluGUID"].ToString();
       //        Lmd.Add(md);
       //    }
       //    return Lmd;
       //}

       //public DataTable getTjl()
       //{
       //    StringBuilder strSql = new StringBuilder();
       //    strSql.Append("select * from TAB_Base_TrainJiaolu");
       //    DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
       //    return dt;
       
       //}

       public List<MDClientAndSection> getList(string strClientID)
       {
           List<MDClientAndSection> lm = new List<MDClientAndSection>();
           StringBuilder strSql = new StringBuilder();
           strSql.Append("    select distinct(SectionID),j.SectionName   from Tab_Tmis_ClientAndJDSection c left join TAB_Base_JDSection j on c.strSectionID=j.nID where 1=1 ");
           if (!string.IsNullOrEmpty(strClientID))
           {
               strSql.Append("and strClientID='" + strClientID + "'");
           }
           DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql.ToString()).Tables[0];
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               MDClientAndSection md = new MDClientAndSection();
               string strsectionid = dt.Rows[i]["SectionID"].ToString();
               string strsectionname = dt.Rows[i]["SectionName"].ToString();
               md.Section_id = strsectionid;
               md.Section_name = strsectionname;
               lm.Add(md);
           }
           return lm;
       }








    }
}
