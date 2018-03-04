using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.NamePlate
{
    public class DBAllMingPai
    {


        #region =======================获取所有的车队信息，然后放在内存中=======================
        public List<tmAndCheDui> tmAndCheDui = null;
        private DataTable dt;
        public DataTable addTmAndInfo()
        {

            //获取该车间下的所有干部信息
            string strSql = @"   select t.strTrainmanNumber,g.strGuideGroupName 
  from  TAB_Org_Trainman t left join TAB_Org_GuideGroup g on g.strGuideGroupGUID=t.strGuideGroupGUID where len(strGuideGroupName)>0";
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        }
        public void getTmAndCheDui()
        {
            dt = addTmAndInfo();
        }

        public string getTG(string strTn)
        {
            if (string.IsNullOrEmpty(strTn))
            {
                return "";
            }
            DataRow[] drs = dt.Select("strTrainmanNumber='" + strTn + "'");
            if (drs.Length > 0)
            {
                DataRow dr = drs[0];
                return dr["strGuideGroupName"].ToString();
            }
            return "";
        }

        #endregion

        #region  ================================获取干部的数据库操作方法================================
        public DataTable GetGanBuList(string strWorkShopGUID)
        {
            //获取该车间下的所有干部信息
            string strSql = @" select g.*,gt.nOrder,c.nPostID from TAB_Org_GanBu g  left join TAB_Base_GanBuType gt on g.nTypeID=gt.nTypeID 
   left join TAB_Org_Trainman c on g.strTrainmanID=c.strTrainmanGUID where c.strWorkShopGUID = @strWorkShopGUID order by gt.nOrder, g.strTrainmanNumber";
            SqlParameter[] sqlParams = {
                                               new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

        }


        public class ganbus
        {
            public string strTypeName;
            public string strTmName;
            public string strTmNumber;
        }

        public class strType
        {
            public string strTypeName;
            public List<trainMan> trainMan = new List<trainMan>();
            public void addtm(string strtname, string strtnumber)
            {
                trainMan t = new trainMan();
                t.tmName = strtname;
                t.tmNumber = strtnumber;
                trainMan.Add(t);
            }

        }
        List<strType> strTypeList = new List<strType>();
        public List<strType> GanBuDtToList2(string strWorkShopGUID)
        {
            DataTable dt = this.GetGanBuList("3b50bf66-dabb-48c0-8b6d-05db80591090");
            List<ganbus> ganbus = new List<ganbus>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ganbus g = new ganbus();
                g.strTypeName = dt.Rows[i]["strTypeName"].ToString();
                g.strTmName = dt.Rows[i]["strTrainmanName"].ToString();
                g.strTmNumber = dt.Rows[i]["strTrainmanNumber"].ToString();
                ganbus.Add(g);
            }
            strType strType;
            for (int i = 0; i < ganbus.Count; i++)
            {
                strType = FindstrType(ganbus[i].strTypeName);
                if (strType == null)
                {
                    strType = new strType();
                    strType.strTypeName = ganbus[i].strTypeName;
                    strTypeList.Add(strType);
                }
                strType.addtm(ganbus[i].strTmName, ganbus[i].strTmName);
            }
            return strTypeList;
        }
        private strType FindstrType(string strTypename)
        {
            for (int i = 0; i < strTypeList.Count; i++)
            {
                if (strTypeList[i].strTypeName == strTypename)
                {
                    return strTypeList[i];
                }
            }
            return null;
        }


        public List<ganBu> GanBuDtToList(string strWorkShopGUID)
        {
            DataTable dt = this.GetGanBuList(strWorkShopGUID);
            List<ganBu> lgb = new List<ganBu>();
            List<trainMan> Ltm = new List<trainMan>();
            string strAllZhiWei = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strTypeID = dt.Rows[i]["nTypeID"].ToString();
                string strTypeName = dt.Rows[i]["strTypeName"].ToString();
                string strTrainmanID = dt.Rows[i]["strTrainmanID"].ToString();
                string strGn = getTG(dt.Rows[i]["strTrainmanNumber"].ToString());
                string strTrainmanNumber = ChangeStrTn(dt.Rows[i]["strTrainmanNumber"].ToString());
                string strTrainmanName = dt.Rows[i]["strTrainmanName"].ToString();
                string nPostID = dt.Rows[i]["nPostID"].ToString();
                ganBu gb = new ganBu();
                gb.strTypeID = strTypeID;
                gb.strTypeName = strTypeName;
                trainMan tm = new trainMan();
                tm.tmGUID = strTrainmanID;
                tm.tmName = strTrainmanName;
                tm.tmNumber = strTrainmanNumber;
                tm.tmGuideGroupName = strGn;
                tm.nPostID = nPostID;
                if (!strAllZhiWei.Contains("'" + strTypeID + "'"))
                {
                    Ltm = new List<trainMan>();
                    Ltm.Add(tm);
                }
                else
                {
                    Ltm.Add(tm);
                }
                gb.trainMan = Ltm;

                strAllZhiWei += "'" + strTypeID + "',";
                if (i + 1 < dt.Rows.Count)
                {
                    if (!strAllZhiWei.Contains("'" + dt.Rows[i + 1]["nTypeID"].ToString() + "'"))
                    {
                        lgb.Add(gb);
                    }
                }
                else
                {
                    lgb.Add(gb);
                }
            }
            return lgb;
        }
        #endregion

        #region  ================================获取非运转（请销假）的数据库操作方法================================
        public DataTable GetAskLeaveList(string strWorkShopGUID, string strTmJiaoLuGUIDs)
        {
            string strWhere = "";
            if (!string.IsNullOrEmpty(strTmJiaoLuGUIDs))
                strWhere += " and o.strTrainmanJiaoluGUID in (" + strTmJiaoLuGUIDs + ") ";

            if (!string.IsNullOrEmpty(strWorkShopGUID))
                strWhere += " and strWorkShopGUID=@strWorkShopGUID ";

            //获取该车间下的所有干部信息
            string strSql = @"  select strTrainmanGUID,strTrainmanNumber,strTrainmanName,nPostID,o.strTrainmanJiaoluGUID,tj.strTrainmanJiaoluName, bIsKey,
     (select top 1 strLeaveTypeGUID from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainmanID=strTrainmanNumber order by dBeginTime desc) as strLeaveTypeGUID, 
     (select top 1 strTypeName from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainmanID=strTrainmanNumber order by dBeginTime desc) as strLeaveTypeName
     from TAB_Org_Trainman o left join TAB_Base_TrainmanJiaolu tj on tj.strTrainmanJiaoluGUID=o.strTrainmanJiaoluGUID   where 
      nTrainmanState = 0 " + strWhere + " order by strLeaveTypeGUID,strTrainmanNumber";

            SqlParameter[] sqlParams = {
                                               new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                       };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

        }

        public List<unRun> unRunDtToList(string strWorkShopGUID, string strTmJiaoLuGUIDs)
        {
            DataTable dt = this.GetAskLeaveList(strWorkShopGUID, strTmJiaoLuGUIDs);
            List<unRun> lunRun = new List<unRun>();
            List<trainMan> Ltm = new List<trainMan>();

            string strAllZhiWei = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //请假类别的GUID和名称
                string strLeaveTypeGUID = dt.Rows[i]["strLeaveTypeGUID"].ToString();
                string strLeaveTypeName = dt.Rows[i]["strLeaveTypeName"].ToString();

                //人员的相关信息
                string strTrainmanID = dt.Rows[i]["strTrainmanGUID"].ToString();
                string strTrainmanNumber = ChangeStrTn(dt.Rows[i]["strTrainmanNumber"].ToString());
                string strTrainmanName = dt.Rows[i]["strTrainmanName"].ToString();
                string nPostID = dt.Rows[i]["nPostID"].ToString();
                string strGn = getTG(dt.Rows[i]["strTrainmanNumber"].ToString());
                unRun unRun = new unRun();

                unRun.strLeaveTypeGUID = strLeaveTypeGUID;
                unRun.strLeaveTypeName = strLeaveTypeName;


                //实力化人员，并赋值
                trainMan tm = new trainMan();
                tm.tmGUID = strTrainmanID;
                tm.tmName = strTrainmanName;
                tm.tmNumber = strTrainmanNumber;
                tm.nPostID = nPostID;
                tm.tmGuideGroupName = strGn;


                if (!strAllZhiWei.Contains("'" + strLeaveTypeGUID + "'"))
                {
                    Ltm = new List<trainMan>();
                    Ltm.Add(tm);
                }
                else
                {
                    Ltm.Add(tm);
                }
                unRun.trainMan = Ltm;

                strAllZhiWei += "'" + strLeaveTypeGUID + "',";
                if (i + 1 < dt.Rows.Count)
                {
                    if (!strAllZhiWei.Contains("'" + dt.Rows[i + 1]["strLeaveTypeGUID"].ToString() + "'"))
                    {
                        lunRun.Add(unRun);
                    }
                }
                else
                {
                    lunRun.Add(unRun);
                }
            }
            return lunRun;
        }
        #endregion

        #region  ================================获取预备的的数据库操作方法================================
        public DataTable GetReadyList(string strWorkShopGUID, string strTmJiaoLuGUIDs)
        {

            string strWhere = "";
            if (!string.IsNullOrEmpty(strTmJiaoLuGUIDs))
                strWhere += " and o.strTrainmanJiaoluGUID in (" + strTmJiaoLuGUIDs + ") ";

            if (!string.IsNullOrEmpty(strWorkShopGUID))
                strWhere += " and strWorkShopGUID=@strWorkShopGUID ";


            //获取该车间下的所有干部信息
            string strSql = @"select strTrainmanGUID,strTrainmanNumber,strTrainmanName,nPostID,o.strTrainmanJiaoluGUID,tm.strTrainmanJiaoluName,o.strWorkShopGUID,
     (case when DATEADD(hour,24,ISNULL(dtBecomeReady,getdate())) < GETDATE()   then 1 else 0 end) as 
     nReadyOverTime,bIsKey from TAB_Org_Trainman o left join  TAB_Base_TrainmanJiaolu tm on o.strTrainmanJiaoluGUID=tm.strTrainmanJiaoluGUID
      where  len(o.strTrainmanJiaoluGUID)>0 and len(tm.strTrainmanJiaoluName)>0 
     and nTrainmanState = 1 " + strWhere + "   order by o.strTrainmanJiaoluGUID,strTrainmanNumber";

            SqlParameter[] sqlParams = {
                                               new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                       };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

        }

        public List<ready> ReadyDtToList(string strWorkShopGUID, string strTmJiaoLuGUIDs)
        {
            DataTable dt = this.GetReadyList(strWorkShopGUID, strTmJiaoLuGUIDs);
            List<ready> lready = new List<ready>();
            List<trainMan> Ltm = new List<trainMan>();
            string strAllZhiWei = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strTrainmanJiaoluGUID = dt.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                string strTrainmanJiaoluName = dt.Rows[i]["strTrainmanJiaoluName"].ToString();
                string strGn = getTG(dt.Rows[i]["strTrainmanNumber"].ToString());
                string strTrainmanID = dt.Rows[i]["strTrainmanGUID"].ToString();
                string strTrainmanNumber = ChangeStrTn(dt.Rows[i]["strTrainmanNumber"].ToString());
                string strTrainmanName = dt.Rows[i]["strTrainmanName"].ToString();
                string nPostID = dt.Rows[i]["nPostID"].ToString();

                ready ready = new ready();
                ready.strTrainmanJiaoluGUID = strTrainmanJiaoluGUID;
                ready.strTrainmanJiaoluName = strTrainmanJiaoluName;
                trainMan tm = new trainMan();
                tm.tmGUID = strTrainmanID;
                tm.tmName = strTrainmanName;
                tm.tmNumber = strTrainmanNumber;
                tm.tmGuideGroupName = strGn;
                tm.nPostID = nPostID;
                if (!strAllZhiWei.Contains("'" + strTrainmanJiaoluGUID + "'"))
                {
                    Ltm = new List<trainMan>();
                    Ltm.Add(tm);
                }
                else
                {
                    Ltm.Add(tm);
                }
                ready.trainMan = Ltm;

                strAllZhiWei += "'" + strTrainmanJiaoluGUID + "',";
                if (i + 1 < dt.Rows.Count)
                {
                    if (!strAllZhiWei.Contains("'" + dt.Rows[i + 1]["strTrainmanJiaoluGUID"].ToString() + "'"))
                    {
                        lready.Add(ready);
                    }
                }
                else
                {
                    lready.Add(ready);
                }
            }
            return lready;
        }
        #endregion

        #region  ================================获取轮乘数据库操作方法================================
        public DataTable GetLunChengList(string strWorkShopGUID, string strTmJiaoLuGUIDs)
        {

            string strWhere = "";
            if (!string.IsNullOrEmpty(strTmJiaoLuGUIDs))
                strWhere += " and strTrainmanJiaoluGUID in(" + strTmJiaoLuGUIDs + ") ";


            if (!string.IsNullOrEmpty(strWorkShopGUID))
                strWhere += " and strWorkShopGUID1=@strWorkShopGUID ";

            //获取该车间下的所有轮乘
            string strSql = @"select * from VIEW_Nameplate_TrainmanJiaolu_Order where 
         ((LEN(strTrainmanName1) > 0) or (LEN(strTrainmanName2) > 0)or (LEN(strTrainmanName3) > 0)or 
        (LEN(strTrainmanName4) > 0)) " + strWhere + "  order by strTrainmanJiaoluGUID,groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),dtLastArriveTime,strTrainmanName1,strTrainmanName2";

            SqlParameter[] sqlParams = {
                                               new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                       };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

        }

        public List<LunCheng> LunChengDtToList(string strWorkShopGUID, string strTmJiaoLuGUIDs)
        {
            DataTable dt = this.GetLunChengList(strWorkShopGUID, strTmJiaoLuGUIDs);
            int order = 1;
            List<LunCheng> lLunCheng = new List<LunCheng>();
            List<grps> Lgrps = new List<grps>();
            string strAllZhiWei = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strTrainmanJiaoluGUID = dt.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                string strTrainmanJiaoluName = dt.Rows[i]["strTrainmanJiaoluName"].ToString() == "" ? "未归属人员交路" : dt.Rows[i]["strTrainmanJiaoluName"].ToString();

                string nOrder = order.ToString();
                string GroupState = dt.Rows[i]["GroupState"].ToString();

                string strTrainmanNumber1 = ChangeStrTn(dt.Rows[i]["strTrainmanNumber1"].ToString());
                string strTrainmanGUID1 = dt.Rows[i]["strTrainmanGUID1"].ToString();
                string strTrainmanName1 = dt.Rows[i]["strTrainmanName1"].ToString();
                string nPost1 = (dt.Rows[i]["nPost1"].ToString() == "" || dt.Rows[i]["nPost1"].ToString() == "0") && strTrainmanName1 != "" ? "1" : dt.Rows[i]["nPost1"].ToString();
                string strGn1 = getTG(dt.Rows[i]["strTrainmanNumber1"].ToString());

                string strTrainmanNumber2 = ChangeStrTn(dt.Rows[i]["strTrainmanNumber2"].ToString());
                string strTrainmanGUID2 = dt.Rows[i]["strTrainmanGUID2"].ToString();
                string strTrainmanName2 = dt.Rows[i]["strTrainmanName2"].ToString();
                string nPost2 = (dt.Rows[i]["nPost2"].ToString() == "" || dt.Rows[i]["nPost2"].ToString() == "0") && strTrainmanName2 != "" ? "2" : dt.Rows[i]["nPost2"].ToString();
                string strGn2 = getTG(dt.Rows[i]["strTrainmanNumber2"].ToString());


                string strTrainmanNumber3 = ChangeStrTn(dt.Rows[i]["strTrainmanNumber3"].ToString());
                string strTrainmanGUID3 = dt.Rows[i]["strTrainmanGUID3"].ToString();
                string strTrainmanName3 = dt.Rows[i]["strTrainmanName3"].ToString();
                string nPost3 = (dt.Rows[i]["nPost3"].ToString() == "" || dt.Rows[i]["nPost3"].ToString() == "0") && strTrainmanName3 != "" ? "3" : dt.Rows[i]["nPost3"].ToString();
                string strGn3 = getTG(dt.Rows[i]["strTrainmanNumber3"].ToString());


                string strTrainmanNumber4 = ChangeStrTn(dt.Rows[i]["strTrainmanNumber4"].ToString());
                string strTrainmanGUID4 = dt.Rows[i]["strTrainmanGUID4"].ToString();
                string strTrainmanName4 = dt.Rows[i]["strTrainmanName4"].ToString();
                string nPost4 = dt.Rows[i]["nPost4"].ToString();



                LunCheng lc = new LunCheng();
                Jl Jl = new Jl();
                Jl.JlGUID = strTrainmanJiaoluGUID;
                Jl.JlName = strTrainmanJiaoluName;

                grps grps = new grps();
                grps.orderNo = nOrder;
                grps.groupState = GroupState;

                tms tms = new tms();


                tm1 tm1 = new tm1();
                tm1.tmGUID = strTrainmanGUID1;
                tm1.tmName = strTrainmanName1;
                tm1.tmNumber = strTrainmanNumber1;
                tm1.post = nPost1;
                tm1.tmGuideGroupName = strGn1;
                tms.tm1 = tm1;

                tm2 tm2 = new tm2();
                tm2.tmGUID = strTrainmanGUID2;
                tm2.tmName = strTrainmanName2;
                tm2.tmNumber = strTrainmanNumber2;
                tm2.post = nPost2;
                tm2.tmGuideGroupName = strGn2;
                tms.tm2 = tm2;


                tm3 tm3 = new tm3();
                tm3.tmGUID = strTrainmanGUID3;
                tm3.tmName = strTrainmanName3;
                tm3.tmNumber = strTrainmanNumber3;
                tm3.post = nPost3;
                tm3.tmGuideGroupName = strGn3;
                tms.tm3 = tm3;

                tm4 tm4 = new tm4();
                tm4.tmGUID = strTrainmanGUID4;
                tm4.tmName = strTrainmanName4;
                tm4.tmNumber = strTrainmanNumber4;
                tm4.post = nPost4;
                tms.tm4 = tm4;
                grps.tms = tms;
                if (!strAllZhiWei.Contains("'" + strTrainmanJiaoluGUID + "'"))
                {
                    Lgrps = new List<grps>();
                    Lgrps.Add(grps);
                }
                else
                {
                    Lgrps.Add(grps);
                }
                lc.grps = Lgrps;
                lc.Jl = Jl;
                strAllZhiWei += "'" + strTrainmanJiaoluGUID + "',";
                if (i + 1 < dt.Rows.Count)
                {
                    if (!strAllZhiWei.Contains("'" + dt.Rows[i + 1]["strTrainmanJiaoluGUID"].ToString() + "'"))
                    {
                        lLunCheng.Add(lc);
                        order = 0;
                    }
                }
                else
                {
                    lLunCheng.Add(lc);
                }
                order++;
            }
            return lLunCheng;
        }
        #endregion

        #region  ================================获取记名式交路数据库操作方法================================
        public DataTable GetNamedList(string strWorkShopGUID, string strTmJiaoLuGUIDs)
        {
            string strWhere = "";
            if (!string.IsNullOrEmpty(strTmJiaoLuGUIDs))
                strWhere += " and strTrainmanJiaoluGUID in(" + strTmJiaoLuGUIDs + ") ";

            if (!string.IsNullOrEmpty(strWorkShopGUID))
                strWhere += " and strWorkShopGUID1=@strWorkShopGUID  ";

            //获取该车间下的所有记名式机车交路
            string strSql = @"select * from [VIEW_Nameplate_TrainmanJiaolu_Named] where 
             ((LEN(strTrainmanName1) > 0) or (LEN(strTrainmanName2) > 0)or (LEN(strTrainmanName3) > 0)or (LEN(strTrainmanName4) > 0)) " + strWhere + "  order by strTrainmanJiaoluName,nCheciOrder";
            SqlParameter[] sqlParams = {
                                               new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }


        public List<Named> NamedDtToList(string strWorkShopGUID, string strTmJiaoLuGUIDs)
        {
            DataTable dt = this.GetNamedList(strWorkShopGUID, strTmJiaoLuGUIDs);

            List<Named> lNamed = new List<Named>();
            List<grps> Lgrps = new List<grps>();
            string strAllZhiWei = "";

            int order = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strTrainmanJiaoluGUID = dt.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                string strTrainmanJiaoluName = dt.Rows[i]["strTrainmanJiaoluName"].ToString();
                string strCheci1 = dt.Rows[i]["strCheci1"].ToString();
                string strCheci2 = dt.Rows[i]["strCheci2"].ToString();
                string GroupState = dt.Rows[i]["GroupState"].ToString();


                string strTrainmanNumber1 = ChangeStrTn(dt.Rows[i]["strTrainmanNumber1"].ToString());
                string strTrainmanGUID1 = dt.Rows[i]["strTrainmanGUID1"].ToString();
                string strTrainmanName1 = dt.Rows[i]["strTrainmanName1"].ToString();
                string nPost1 = (dt.Rows[i]["nPost1"].ToString() == "" || dt.Rows[i]["nPost1"].ToString() == "0") && strTrainmanName1 != "" ? "1" : dt.Rows[i]["nPost1"].ToString();
                string strGn1 = getTG(dt.Rows[i]["strTrainmanNumber1"].ToString());

                string strTrainmanNumber2 = ChangeStrTn(dt.Rows[i]["strTrainmanNumber2"].ToString());
                string strTrainmanGUID2 = dt.Rows[i]["strTrainmanGUID2"].ToString();
                string strTrainmanName2 = dt.Rows[i]["strTrainmanName2"].ToString();
                string nPost2 = (dt.Rows[i]["nPost2"].ToString() == "" || dt.Rows[i]["nPost2"].ToString() == "0") && strTrainmanName2 != "" ? "2" : dt.Rows[i]["nPost2"].ToString();
                string strGn2 = getTG(dt.Rows[i]["strTrainmanNumber2"].ToString());


                string strTrainmanNumber3 = ChangeStrTn(dt.Rows[i]["strTrainmanNumber3"].ToString());
                string strTrainmanGUID3 = dt.Rows[i]["strTrainmanGUID3"].ToString();
                string strTrainmanName3 = dt.Rows[i]["strTrainmanName3"].ToString();
                string nPost3 = (dt.Rows[i]["nPost3"].ToString() == "" || dt.Rows[i]["nPost3"].ToString() == "0") && strTrainmanName3 != "" ? "3" : dt.Rows[i]["nPost3"].ToString();
                string strGn3 = getTG(dt.Rows[i]["strTrainmanNumber3"].ToString());



                string strTrainmanNumber4 = ChangeStrTn(dt.Rows[i]["strTrainmanNumber4"].ToString());
                string strTrainmanGUID4 = dt.Rows[i]["strTrainmanGUID4"].ToString();
                string strTrainmanName4 = dt.Rows[i]["strTrainmanName4"].ToString();
                string nPost4 = dt.Rows[i]["nPost4"].ToString();



                Named Named = new Named();
                JlNamed JlNamed = new JlNamed();
                JlNamed.JlGUID = strTrainmanJiaoluGUID;
                JlNamed.JlName = strTrainmanJiaoluName;


                grps grps = new grps();

                tms tms = new tms();

                grps.strCheci1 = strCheci1;
                grps.strCheci2 = strCheci2;
                grps.groupState = GroupState;

                tm1 tm1 = new tm1();
                tm1.tmGUID = strTrainmanGUID1;
                tm1.tmName = strTrainmanName1;
                tm1.tmNumber = strTrainmanNumber1;
                tm1.post = nPost1;
                tm1.tmGuideGroupName = strGn1;
                tms.tm1 = tm1;

                tm2 tm2 = new tm2();
                tm2.tmGUID = strTrainmanGUID2;
                tm2.tmName = strTrainmanName2;
                tm2.tmNumber = strTrainmanNumber2;
                tm2.post = nPost2;
                tm2.tmGuideGroupName = strGn2;
                tms.tm2 = tm2;


                tm3 tm3 = new tm3();
                tm3.tmGUID = strTrainmanGUID3;
                tm3.tmName = strTrainmanName3;
                tm3.tmNumber = strTrainmanNumber3;
                tm3.post = nPost3;
                tm3.tmGuideGroupName = strGn3;
                tms.tm3 = tm3;
                tm4 tm4 = new tm4();
                tm4.tmGUID = strTrainmanGUID4;
                tm4.tmName = strTrainmanName4;
                tm4.tmNumber = strTrainmanNumber4;
                tm4.post = nPost4;
                tms.tm4 = tm4;
                grps.tms = tms;
                grps.orderNo = order.ToString();
                if (!strAllZhiWei.Contains("'" + strTrainmanJiaoluGUID + "'"))
                {
                    Lgrps = new List<grps>();
                    Lgrps.Add(grps);
                }
                else
                {
                    Lgrps.Add(grps);
                }
                Named.grps = Lgrps;
                Named.Jl = JlNamed;

                strAllZhiWei += "'" + strTrainmanJiaoluGUID + "',";
                if (i + 1 < dt.Rows.Count)
                {
                    if (!strAllZhiWei.Contains("'" + dt.Rows[i + 1]["strTrainmanJiaoluGUID"].ToString() + "'"))
                    {
                        lNamed.Add(Named);
                        order = 0;
                    }
                }
                else
                {
                    lNamed.Add(Named);
                }
                order++;
            }
            return lNamed;
        }
        #endregion

        #region  ================================获取包乘交路数据库操作方法================================
        public DataTable GetBaoChengList(string strWorkShopGUID, string strTmJiaoLuGUIDs)
        {
            string strWhere = "";
            if (!string.IsNullOrEmpty(strTmJiaoLuGUIDs))
                strWhere += " and strTrainmanJiaoluGUID in(" + strTmJiaoLuGUIDs + ") ";

            if (!string.IsNullOrEmpty(strWorkShopGUID))
                strWhere += " and   strWorkShopGUID1=@strWorkShopGUID or strWorkShopGUID2=@strWorkShopGUID or strWorkShopGUID3=@strWorkShopGUID or strWorkShopGUID4=@strWorkShopGUID    ";


            //获取该车间下的所有记名式机车交路
            string strSql = @" select * from [VIEW_Nameplate_TrainmanJiaolu_TogetherTrain] where  
     ((LEN(strTrainmanName1) > 0)  or (LEN(strTrainmanName2) > 0)or (LEN(strTrainmanName3) > 0)or (LEN(strTrainmanName4) > 0)) " + strWhere + " order by nOrder";
            SqlParameter[] sqlParams = {
                                               new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
        }


        public List<BaoCheng> BaoChengDtToList(string strWorkShopGUID, string strTmJiaoLuGUIDs)
        {
            DataTable dt = this.GetBaoChengList(strWorkShopGUID, strTmJiaoLuGUIDs);
            List<BaoCheng> lxx = new List<BaoCheng>();
            List<grps> ljz = new List<grps>();
            string strCXCH = "";
            foreach (DataRow dr in dt.Rows)
            {
                BaoCheng cxch = new BaoCheng();
                cxch.CheXing = dr["strTrainTypeName"].ToString();
                cxch.CheHao = dr["strTrainNumber"].ToString();
                if (!strCXCH.Contains("'" + cxch.CheXing + cxch.CheHao + "'"))
                {
                    strCXCH += "'" + cxch.CheXing + cxch.CheHao + "',";
                    ljz = getJiZuList(dt, cxch.CheXing, cxch.CheHao);
                    cxch.grps = ljz;
                    lxx.Add(cxch);
                }
            }
            return lxx;
        }

        public List<grps> getJiZuList(DataTable dt, string cx, string ch)
        {
            DataView view = dt.DefaultView;
            view.RowFilter = string.Format("strTrainTypeName={0} and strTrainNumber={1}", "'" + cx.ToString() + "'", "'" + ch.ToString() + "'");
            List<grps> jzl = new List<grps>();
            int i = 1;
            foreach (DataRow dr in view.ToTable().Rows)
            {
                tm1 bTm1 = new NamePlate.tm1();
                bTm1.tmName = dr["strTrainmanName1"].ToString();
                bTm1.tmNumber = ChangeStrTn(dr["strTrainmanNumber1"].ToString());
                bTm1.post = (dr["nPost1"].ToString() == "" || dr["nPost1"].ToString() == "0") && bTm1.tmName != "" ? "1" : dr["nPost1"].ToString();
                bTm1.tmGuideGroupName = getTG(dr["strTrainmanNumber1"].ToString());


                tm2 bTm2 = new tm2();
                bTm2.tmName = dr["strTrainmanName2"].ToString();
                bTm2.tmNumber = ChangeStrTn(dr["strTrainmanNumber2"].ToString());
                bTm2.post = (dr["nPost2"].ToString() == "" || dr["nPost2"].ToString() == "0") && bTm2.tmName != "" ? "2" : dr["nPost2"].ToString();
                bTm2.tmGuideGroupName = getTG(dr["strTrainmanNumber2"].ToString());


                tm3 bTm3 = new tm3();
                bTm3.tmName = dr["strTrainmanName3"].ToString();
                bTm3.tmNumber = ChangeStrTn(dr["strTrainmanNumber3"].ToString());
                bTm3.post = (dr["nPost3"].ToString() == "" || dr["nPost3"].ToString() == "0") && bTm3.tmName != "" ? "3" : dr["nPost3"].ToString();
                bTm3.tmGuideGroupName = getTG(dr["strTrainmanNumber3"].ToString());


                tm4 bTm4 = new tm4();
                bTm4.tmNumber = ChangeStrTn(dr["strTrainmanNumber4"].ToString());
                bTm4.post = dr["nPost4"].ToString();
                bTm4.tmName = dr["strTrainmanName4"].ToString();

                grps grps = new grps();

                grps.groupState = dr["GroupState"].ToString();
                grps.orderNo = i.ToString();
                i++;
                tms tm = new tms();
                tm.tm1 = bTm1;
                tm.tm2 = bTm2;
                tm.tm3 = bTm3;
                tm.tm4 = bTm4;
                grps.tms = tm;
                jzl.Add(grps);
            }
            return jzl;

        }
        #endregion

        #region  ================================获取值班员数据库操作方法================================
        public DataTable GetDutyUserList(string strWorkShopGUID)
        {
            //获取该车间下的所有干部信息
            string strSql = @"   select * from TAB_Org_NeiWaiQin  where strWorkShopGUID = @strWorkShopGUID order by strTypeId";
            SqlParameter[] sqlParams = {
                                               new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

        }

        public List<neiWaiQin> DutyUserDtToList(string strWorkShopGUID)
        {
            DataTable dt = this.GetDutyUserList(strWorkShopGUID);
            List<neiWaiQin> lnwq = new List<neiWaiQin>();
            List<trainMan> Ltm = new List<trainMan>();
            string strAllZhiWei = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strTypeId = dt.Rows[i]["strTypeId"].ToString();
                string strTrainmanNumber = dt.Rows[i]["strNumber"].ToString();
                string strTrainmanName = dt.Rows[i]["strName"].ToString();
                string strTypeName = "";
                if (strTypeId == "1")
                    strTypeName = "甲";
                else if (strTypeId == "2")
                    strTypeName = "乙";
                else if (strTypeId == "3")
                    strTypeName = "丙";
                else if (strTypeId == "4")
                    strTypeName = "丁";
                else if (strTypeId == "5")
                    strTypeName = "备";
                else
                    strTypeName = "";

                neiWaiQin nwq = new neiWaiQin();
                nwq.strTypeID = strTypeId;
                nwq.strTypeName = strTypeName;
                trainMan tm = new trainMan();
                tm.tmName = strTrainmanName;
                tm.tmNumber = strTrainmanNumber;
                if (!strAllZhiWei.Contains("'" + strTypeId + "'"))
                {
                    Ltm = new List<trainMan>();
                    Ltm.Add(tm);
                }
                else
                {
                    Ltm.Add(tm);
                }
                nwq.trainMan = Ltm;

                strAllZhiWei += "'" + strTypeId + "',";
                if (i + 1 < dt.Rows.Count)
                {
                    if (!strAllZhiWei.Contains("'" + dt.Rows[i + 1]["strTypeId"].ToString() + "'"))
                    {
                        lnwq.Add(nwq);
                    }
                }
                else
                {
                    lnwq.Add(nwq);
                }
            }
            return lnwq;
        }
        #endregion


        #region  ================================获取调休数据库操作方法================================
        public DataTable GetTXList(string strWorkShopGUID, string strTmJiaoLuGUIDs)
        {

            string strWhere = "";
            if (!string.IsNullOrEmpty(strTmJiaoLuGUIDs))
                strWhere += " and strTrainmanJiaoluGUID in(" + strTmJiaoLuGUIDs + ") ";


            if (!string.IsNullOrEmpty(strWorkShopGUID))
                strWhere += " and strWorkShopGUID1=@strWorkShopGUID ";

            //获取该车间下的所有轮乘
            string strSql = @"select * from VIEW_Nameplate_Group  where 1=1  and nTXState=1 " + strWhere + "  order by groupState,(case when year(dtLastArriveTime)=1899  then 1 else 0 end ),dtLastArriveTime,strTrainmanName1,strTrainmanName2";

            SqlParameter[] sqlParams = {
                                               new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                       };

            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

        }

        public List<LunCheng> TXDtToList(string strWorkShopGUID, string strTmJiaoLuGUIDs)
        {
            DataTable dt = this.GetTXList(strWorkShopGUID, strTmJiaoLuGUIDs);
            int order = 1;
            List<LunCheng> lLunCheng = new List<LunCheng>();
            List<grps> Lgrps = new List<grps>();
            string strAllZhiWei = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strTrainmanJiaoluGUID = dt.Rows[i]["strTrainmanJiaoluGUID"].ToString();
                string strTrainmanJiaoluName = dt.Rows[i]["strTrainmanJiaoluName"].ToString() == "" ? "未归属人员交路" : dt.Rows[i]["strTrainmanJiaoluName"].ToString();

                string nOrder = order.ToString();
                string GroupState = dt.Rows[i]["GroupState"].ToString();

                string strTrainmanNumber1 = ChangeStrTn(dt.Rows[i]["strTrainmanNumber1"].ToString());
                string strTrainmanGUID1 = dt.Rows[i]["strTrainmanGUID1"].ToString();
                string strTrainmanName1 = dt.Rows[i]["strTrainmanName1"].ToString();
                string nPost1 = (dt.Rows[i]["nPost1"].ToString() == "" || dt.Rows[i]["nPost1"].ToString() == "0") && strTrainmanName1 != "" ? "1" : dt.Rows[i]["nPost1"].ToString();
                string strGn1 = getTG(dt.Rows[i]["strTrainmanNumber1"].ToString());

                string strTrainmanNumber2 = ChangeStrTn(dt.Rows[i]["strTrainmanNumber2"].ToString());
                string strTrainmanGUID2 = dt.Rows[i]["strTrainmanGUID2"].ToString();
                string strTrainmanName2 = dt.Rows[i]["strTrainmanName2"].ToString();
                string nPost2 = (dt.Rows[i]["nPost2"].ToString() == "" || dt.Rows[i]["nPost2"].ToString() == "0") && strTrainmanName2 != "" ? "2" : dt.Rows[i]["nPost2"].ToString();
                string strGn2 = getTG(dt.Rows[i]["strTrainmanNumber2"].ToString());


                string strTrainmanNumber3 = ChangeStrTn(dt.Rows[i]["strTrainmanNumber3"].ToString());
                string strTrainmanGUID3 = dt.Rows[i]["strTrainmanGUID3"].ToString();
                string strTrainmanName3 = dt.Rows[i]["strTrainmanName3"].ToString();
                string nPost3 = (dt.Rows[i]["nPost3"].ToString() == "" || dt.Rows[i]["nPost3"].ToString() == "0") && strTrainmanName3 != "" ? "3" : dt.Rows[i]["nPost3"].ToString();
                string strGn3 = getTG(dt.Rows[i]["strTrainmanNumber3"].ToString());


                string strTrainmanNumber4 = ChangeStrTn(dt.Rows[i]["strTrainmanNumber4"].ToString());
                string strTrainmanGUID4 = dt.Rows[i]["strTrainmanGUID4"].ToString();
                string strTrainmanName4 = dt.Rows[i]["strTrainmanName4"].ToString();
                string nPost4 = dt.Rows[i]["nPost4"].ToString();



                LunCheng lc = new LunCheng();
                Jl Jl = new Jl();
                Jl.JlGUID = strTrainmanJiaoluGUID;
                Jl.JlName = strTrainmanJiaoluName;

                grps grps = new grps();
                grps.orderNo = nOrder;
                grps.groupState = GroupState;

                tms tms = new tms();


                tm1 tm1 = new tm1();
                tm1.tmGUID = strTrainmanGUID1;
                tm1.tmName = strTrainmanName1;
                tm1.tmNumber = strTrainmanNumber1;
                tm1.post = nPost1;
                tm1.tmGuideGroupName = strGn1;
                tms.tm1 = tm1;

                tm2 tm2 = new tm2();
                tm2.tmGUID = strTrainmanGUID2;
                tm2.tmName = strTrainmanName2;
                tm2.tmNumber = strTrainmanNumber2;
                tm2.post = nPost2;
                tm2.tmGuideGroupName = strGn2;
                tms.tm2 = tm2;


                tm3 tm3 = new tm3();
                tm3.tmGUID = strTrainmanGUID3;
                tm3.tmName = strTrainmanName3;
                tm3.tmNumber = strTrainmanNumber3;
                tm3.post = nPost3;
                tm3.tmGuideGroupName = strGn3;
                tms.tm3 = tm3;

                tm4 tm4 = new tm4();
                tm4.tmGUID = strTrainmanGUID4;
                tm4.tmName = strTrainmanName4;
                tm4.tmNumber = strTrainmanNumber4;
                tm4.post = nPost4;
                tms.tm4 = tm4;
                grps.tms = tms;
                if (!strAllZhiWei.Contains("'" + strTrainmanJiaoluGUID + "'"))
                {
                    Lgrps = new List<grps>();
                    Lgrps.Add(grps);
                }
                else
                {
                    Lgrps.Add(grps);
                }
                lc.grps = Lgrps;
                lc.Jl = Jl;
                strAllZhiWei += "'" + strTrainmanJiaoluGUID + "',";
                if (i + 1 < dt.Rows.Count)
                {
                    if (!strAllZhiWei.Contains("'" + dt.Rows[i + 1]["strTrainmanJiaoluGUID"].ToString() + "'"))
                    {
                        lLunCheng.Add(lc);
                        order = 0;
                    }
                }
                else
                {
                    lLunCheng.Add(lc);
                }
                order++;
            }
            return lLunCheng;
        }
        #endregion


        #region ============================将工号转换成四位的数字============================================

        public string ChangeStrTn(string strTrainNumber)
        {
            if (strTrainNumber.Length == 7)
            {
                return strTrainNumber.Substring(3, 4);
            }
            else
            {
                return strTrainNumber;
            }

        }

        #endregion



    }
}
