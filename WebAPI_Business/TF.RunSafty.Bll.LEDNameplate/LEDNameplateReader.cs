using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;
using System.Data.SqlClient;
namespace TF.RunSafty.LEDNameplate
{
    public class NpTrainman
    {        
        public string tmNumber = string.Empty;
        public string tmName = string.Empty;
        public int tmPost = 0;
    }

    public class NpUnrun
    {
        public string leaveTypeId = string.Empty;
        public string leaveTypeName = string.Empty;
        public List<NpTrainman> tms = new List<NpTrainman>();
    }

    public class NpGanbu
    {
        public string zhiWuId = string.Empty;
        public string zhiWuName = string.Empty;
        public List<NpTrainman> tms = new List<NpTrainman>();
    }

    public class NpGroup
    {
        public string grpId = string.Empty;
        public int order;
        public int state;
        public NpTrainman tm1;
        public NpTrainman tm2;
        public NpTrainman tm3;
        public NpTrainman tm4;
    }
 
    public class NpNamedGroup : NpGroup
    {        
        public string cc1 = string.Empty;
        public string cc2 = string.Empty;
    }

 
    public class NpTogetherTrain
    {
        public string trainName = string.Empty;
        public string trainId = string.Empty;
        public int order;
        public List<NpGroup> groups = new List<NpGroup>();
    }
    public class NpCommon
    {
        public string jlId = string.Empty;
        public string jlName = string.Empty;
        public int jlType;
        public List<object> dataArray = new List<object>();
    }
    
    public class NpOutput
    {
        public List<NpGanbu> ganbu = new List<NpGanbu>();
        public List<NpCommon> ready = new List<NpCommon>();
        public List<NpCommon> unrun = new List<NpCommon>();
        public List<NpCommon> jlOrderGroups = new List<NpCommon>();
        public List<NpCommon> jlNamedGroups = new List<NpCommon>();
        public List<NpCommon> jlTogetherTrains = new List<NpCommon>();        
    }


    public class LEDNameplateReader
    {
        private int DBObjToInt(object obj)
        { 
            if (obj == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
               
        }

        private string DBObjToString(object obj)
        {
            if (obj == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }
        private void DBRowToGroup(DataRow dr,NpGroup group)
        {
            group.grpId = dr["strGroupGUID"].ToString();            
            group.state = 2;
            if (dr["GroupState"].ToString() != "")
            {
                group.state = 3;
                if (int.Parse(dr["GroupState"].ToString()) == 7)
                {
                    group.state = 6;
                }
            }
            if (dr["strTrainmanGUID1"].ToString() != "")
            {
                group.tm1 = new NpTrainman();                
                group.tm1.tmName = dr["strTrainmanName1"].ToString();
                group.tm1.tmNumber = dr["strTrainmanNumber1"].ToString();
                group.tm1.tmPost = 1;
                if (dr["nPost1"].ToString() != "")
                    group.tm1.tmPost = int.Parse(dr["nPost1"].ToString());
            }
            if (dr["strTrainmanGUID2"].ToString() != "")
            {
                group.tm2 = new NpTrainman();                
                group.tm2.tmName = dr["strTrainmanName2"].ToString();
                group.tm2.tmNumber = dr["strTrainmanNumber2"].ToString();
                group.tm2.tmPost = 1;
                if (dr["nPost2"].ToString() != "")
                    group.tm2.tmPost = int.Parse(dr["nPost2"].ToString());

            }
            if (dr["strTrainmanGUID3"].ToString() != "")
            {
                group.tm3 = new NpTrainman();                
                group.tm3.tmName = dr["strTrainmanName3"].ToString();
                group.tm3.tmNumber = dr["strTrainmanNumber3"].ToString();
                group.tm3.tmPost = 1;
                if (dr["nPost3"].ToString() != "")
                {
                    group.tm3.tmPost = int.Parse(dr["nPost3"].ToString());
                }
            }
            if (dr["strTrainmanGUID4"].ToString() != "")
            {
                group.tm4 = new NpTrainman();                
                group.tm4.tmName = dr["strTrainmanName4"].ToString();
                group.tm4.tmNumber = dr["strTrainmanNumber4"].ToString();
                group.tm4.tmPost = 1;
                if (dr["nPost4"].ToString() != "")
                {
                    group.tm4.tmPost = int.Parse(dr["nPost4"].ToString());
                }
            }

        
        }

    
        private void GetNpCommon(DataRow dr, ref NpCommon npCommon, List<NpCommon> npCommons)
        {
            if ((npCommon == null) || (npCommon.jlId != dr["strTrainmanJiaoluGUID"].ToString()))
            {
                npCommon = new NpCommon();
                npCommons.Add(npCommon);
                npCommon.jlId = dr["strTrainmanJiaoluGUID"].ToString();

                if (dr.Table.Columns.Contains("strTrainmanJiaoluName"))
                {
                    npCommon.jlName = dr["strTrainmanJiaoluName"].ToString();
                }

                if (dr.Table.Columns.Contains("nJiaoluType"))
                {
                    npCommon.jlType = DBObjToInt(dr["nJiaoluType"]);
                }
                
            }
        }


        private void GetJlInfo(string jlId,out string jlName,out int jlType)
        {
            jlName = string.Empty;
            jlType = 0;

            string sql = "select * from TAB_Base_TrainmanJiaolu where strTrainmanJiaoluGUID = @jlId";

            SqlParameter[] sqlParamsNamed = {
                                            new SqlParameter("jlId",jlId)
                                        };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, sqlParamsNamed).Tables[0];

            if (dt.Rows.Count > 0)
            {
                jlName = dt.Rows[0]["strTrainmanJiaoluName"].ToString();
                jlType = DBObjToInt(dt.Rows[0]["nJiaoluType"]);
            }

        }
        private void ReadNamedGroups(string workShopGUID, List<NpCommon> npCommons)
        {
            //获取所有的记名式交路名牌
            string strSql = string.Format(@"select * from VIEW_Nameplate_TrainmanJiaolu_Named where strTrainmanJiaoluGUID in 
            (select strTrainmanJiaoluGUID from tab_base_jiaolurelation where strTrainJiaoluGUID in
            (select strTrainJiaoluGUID from TAB_Base_TrainJiaolu where strWorkShopGUID=@strWorkShopGUID))
            order by strTrainmanJiaoluGUID,nCheciOrder");
            SqlParameter[] sqlParamsNamed = {
                                            new SqlParameter("strWorkShopGUID",workShopGUID)
                                        };
            DataTable tabNamed = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsNamed).Tables[0];
            
            NpCommon npCommon = null;
            foreach (DataRow dr in tabNamed.Rows)
            {
                GetNpCommon(dr,ref npCommon,npCommons);

                NpNamedGroup grp = new NpNamedGroup();
                grp.cc1 = dr["strCheci1"].ToString();
                grp.cc2 = dr["strCheci2"].ToString();
                grp.order = DBObjToInt(dr["nCheciOrder"]);

                DBRowToGroup(dr,grp);
                npCommon.dataArray.Add(grp);                
            }
        }

        private void ReadOrderGroups(string workShopGUID, List<NpCommon> npCommons)
        {
            //获取所有的轮乘交路名牌
            string strSql = string.Format(@"select * from VIEW_Nameplate_TrainmanJiaolu_Order where strTrainmanJiaoluGUID in 
            (select strTrainmanJiaoluGUID from tab_base_jiaolurelation where strTrainJiaoluGUID in

            (select strTrainJiaoluGUID from TAB_Base_TrainJiaolu where strWorkShopGUID=@strWorkShopGUID))
            order by strTrainmanJiaoluGUID,GroupState,dtLastArriveTime");
            SqlParameter[] sqlParamsOrder = {
                                            new SqlParameter("strWorkShopGUID",workShopGUID)
                                        };
            DataTable tabOrder = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsOrder).Tables[0];


            NpCommon npCommon = null;
            foreach (DataRow dr in tabOrder.Rows)
            {
                GetNpCommon(dr, ref npCommon, npCommons);

                NpGroup grp = new NpGroup();
                grp.order = DBObjToInt(dr["nOrder"]);
                DBRowToGroup(dr, grp);
                npCommon.dataArray.Add(grp);
            }
        }

        /// <summary>
        /// 现场LED大屏，暂时没有显示包乘名牌，接口在用的时候再写
        /// </summary>
        /// <param name="workShopGUID"></param>
        /// <param name="npCommons"></param>
        private void ReadTogetherGroups(string workShopGUID, List<NpCommon> npCommons)
        {
            //获取所有的包乘交路名牌
//            string strSql = string.Format(@"select * from VIEW_Nameplate_TrainmanJiaolu_TogetherTrain where strTrainmanJiaoluGUID in 
//            (select strTrainmanJiaoluGUID from tab_base_jiaolurelation where strTrainJiaoluGUID in 
//            (select strTrainJiaoluGUID from TAB_Base_TrainJiaolu where strWorkShopGUID=@strWorkShopGUID))
//            order by strTrainmanJiaoluGUID,strTrainGUID,GroupState,nOrder");
//            SqlParameter[] sqlParamsTrain = {
//                                                            new SqlParameter("strWorkShopGUID",workShopGUID)
//                                                        };
//            DataTable tabTrain = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsTrain).Tables[0];


        }


        private void ReadUnrun(string workShopGUID, List<NpCommon> npCommons)
        {
            //获取所有请销假人员
            string strSql = @"select strTrainmanGUID,strTrainmanNumber,strTrainmanName,nPostID,strTrainmanJiaoluGUID,
                (select top 1 strLeaveTypeGUID from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainmanID=strTrainmanNumber order by dBeginTime desc) as strLeaveTypeGUID, 
                (select top 1 strTypeName from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainmanID=strTrainmanNumber order by dBeginTime desc) as strLeaveTypeName
                from VIEW_Org_Trainman where strWorkShopGUID=@strWorkShopGUID  
                and nTrainmanState = 0 and isnull(strTrainmanJiaoluGUID,'') <> '' order by strTrainmanJiaoluGUID,strLeaveTypeGUID,strTrainmanNumber ";
            SqlParameter[] sqlParamsUnrun = {
                                            new SqlParameter("strWorkShopGUID",workShopGUID)
                                        };
            DataTable tabUnun = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsUnrun).Tables[0];


            NpCommon npCommon = null;
            NpUnrun unrun = null;

            foreach (DataRow dr in tabUnun.Rows)
            {   
                
                GetNpCommon(dr, ref npCommon, npCommons);

                
                if ((unrun == null) || (unrun.leaveTypeName != dr["strLeaveTypeName"].ToString()))
                {
                    unrun = new NpUnrun();
                    npCommon.dataArray.Add(unrun);
                    unrun.leaveTypeId = dr["strLeaveTypeGUID"].ToString();
                    unrun.leaveTypeName = dr["strLeaveTypeName"].ToString();
                }

                NpTrainman tm = new NpTrainman();
                unrun.tms.Add(tm);                
                tm.tmName = dr["strTrainmanName"].ToString();
                tm.tmNumber = dr["strTrainmanNumber"].ToString();
                tm.tmPost = 1;
                if (dr["nPostID"].ToString() != "")
                {
                    tm.tmPost = int.Parse(dr["nPostID"].ToString());
                }             
                
            }

  
            foreach (NpCommon item in npCommons)
            {
                GetJlInfo(item.jlId, out item.jlName, out item.jlType);                
                
            }
        }

        private void ReadReadyPlates(string workShopGUID, List<NpCommon> npCommons)
        {
            //获取所有预备人员
            string strSql = @"select strTrainmanGUID,strTrainmanNumber,strTrainmanName,nPostID,strTrainmanJiaoluGUID 
                from VIEW_Org_Trainman where strWorkShopGUID=@strWorkShopGUID  
                and nTrainmanState = 1 and isnull(strTrainmanJiaoluGUID,'') <> '' order by strTrainmanJiaoluGUID,strTrainmanNumber ";
            SqlParameter[] sqlParamsPrepare = {
                                            new SqlParameter("strWorkShopGUID",workShopGUID)
                                        };
            DataTable tabPrepare = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsPrepare).Tables[0];

            NpCommon npCommon = null;
            foreach (DataRow dr in tabPrepare.Rows)
            {
                GetNpCommon(dr, ref npCommon, npCommons);

                NpTrainman tm = new NpTrainman();
                
                tm.tmName = dr["strTrainmanName"].ToString();
                tm.tmNumber = dr["strTrainmanNumber"].ToString();
                tm.tmPost = 1;
                if (dr["nPostID"].ToString() != "")
                {
                    tm.tmPost = int.Parse(dr["nPostID"].ToString());
                }

                npCommon.dataArray.Add(tm);
            }


            foreach (NpCommon item in npCommons)
            {
                GetJlInfo(item.jlId, out item.jlName, out item.jlType);

            }

        }
        private string GetWorkShopGUID(string workShopName)
        {
            string strSql = @"select * from TAB_Org_WorkShop where strWorkShopName = @workShopName";
            SqlParameter[] sqlParamsPrepare = {
                                            new SqlParameter("workShopName",workShopName)
                                        };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsPrepare).Tables[0];

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["strWorkShopGUID"].ToString();
            }
            else
            {
                return string.Empty;
            }


        }
        public NpOutput ReadNameplates(string workShopName)
        {                    
            NpOutput output = new NpOutput();

            string workShopGUID = GetWorkShopGUID(workShopName);

            if (workShopGUID == string.Empty)
            {
                return output;
            }

            ReadNamedGroups(workShopGUID,output.jlNamedGroups);
            ReadOrderGroups(workShopGUID, output.jlOrderGroups);
            ReadTogetherGroups(workShopGUID, output.jlTogetherTrains);
            ReadReadyPlates(workShopGUID, output.ready);
            ReadUnrun(workShopGUID, output.unrun);

            return output;  
        }

    }
}
