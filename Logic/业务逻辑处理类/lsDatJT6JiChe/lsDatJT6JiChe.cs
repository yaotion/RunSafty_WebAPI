using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ThinkFreely.DBUtility;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TF.RunSafty.Logic
{
    public class lsDatJT6JiChe
    {
        #region 属性
        public int ID;
        public string LocoType;
        public string LocoCode;
        public string LocoNum;
        public DateTime? RuDuanDate;
        public string RuDuanID;
        public string RuDuanName;
        public DateTime? JiaoCheDate;
        public string JiaoCheID;
        public string JiaoCheName;
        public DateTime? ChuDuanDate;
        public string ChuDuanID;
        public string ChuDuanName;
        public int Mark = 0;
        public int GovernUnitId;
        public int UnitId;
        public int DepartmentId;
        public int ApanageId;
        public int TrackId;
        public string ZBSJ;
        public DateTime? CreateDate;
        public DateTime? dtHandleBeginTime;
        public string strHandleCaseID;
        public int nState;
        public DateTime? dtHandleEndTime;
        public string nBeginHandleUserID = "";
        public string UnitName = "";
        public string ApanageName="";
        public int nIsPrint = 0;
        public double nMapX = 0;
        public double nMapY = 0;
        public string strTrainUpHandleUserID = "";
        public string strTrainInHandleUserID = "";
        public string strTrainDownHandleUserID = "";
        public int nDirection = 0;

        public string strTrainDownNum = "";
        public string strTrainDownName = "";
        public string strTrainDownPictureFileName = "";

        public string strTrainUpNum = "";
        public string strTrainUpName = "";
        public string strTrainUpPictureFileName = "";

        public string strTrainInNum = "";
        public string strTrainInName = "";
        public string strTrainInPictureFileName = "";

        public string UserName = "";
        public string UserNum = "";
        public string strPictureFileName = "";

        public int nDetailedState = 0;
        public int nTieXieState = 0;
        public int nRuDuanType = 0;
        public int nLocationLock = 0;

        public int nGL = 0;
        #endregion 属性

        #region 构造函数
        public lsDatJT6JiChe()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public lsDatJT6JiChe(int strid)
        {
            string strSql = "select * from View_TrainHandleRecordMore where ID=@strid";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("strid",strid)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
               
                UnitName = dt.Rows[0]["UnitName"].ToString();
                nIsPrint = PageBase.static_ext_int(dt.Rows[0]["nIsPrint"].ToString());
                strTrainUpNum = dt.Rows[0]["strTrainUpNum"].ToString();
                strTrainUpName = dt.Rows[0]["strTrainUpName"].ToString();
                strTrainUpPictureFileName = dt.Rows[0]["strTrainUpPictureFileName"].ToString();
                strTrainInName = dt.Rows[0]["strTrainInName"].ToString();
                strTrainInNum = dt.Rows[0]["strTrainInNum"].ToString();
                strTrainInPictureFileName = dt.Rows[0]["strTrainInPictureFileName"].ToString();
                strTrainDownName = dt.Rows[0]["strTrainDownName"].ToString();
                strTrainDownNum = dt.Rows[0]["strTrainDownNum"].ToString();
                strTrainDownPictureFileName = dt.Rows[0]["strTrainDownPictureFileName"].ToString();
                ApanageName = dt.Rows[0]["Name"].ToString();
                SetValue(this, dt.Rows[0]);
            }
        }
        public lsDatJT6JiChe(string loctype, string locnum)
        {
            string strSql = "select top 1 * from View_TrainHandleRecordMore where LocoType=@loctype and LocoNum=@locnum and (Mark <> 0) and (Mark <> 4) order by RuDuanDate desc";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("loctype",loctype),
                                           new SqlParameter("locnum",locnum)
                                       };
            DataTable dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                
                UnitName = dt.Rows[0]["UnitName"].ToString();
                nIsPrint = PageBase.static_ext_int(dt.Rows[0]["nIsPrint"].ToString());
                strTrainUpNum = dt.Rows[0]["strTrainUpNum"].ToString();
                strTrainUpName = dt.Rows[0]["strTrainUpName"].ToString();
                strTrainUpPictureFileName = dt.Rows[0]["strTrainUpPictureFileName"].ToString();
                strTrainInName = dt.Rows[0]["strTrainInName"].ToString();
                strTrainInNum = dt.Rows[0]["strTrainInNum"].ToString();
                strTrainInPictureFileName = dt.Rows[0]["strTrainInPictureFileName"].ToString();
                strTrainDownName = dt.Rows[0]["strTrainDownName"].ToString();
                strTrainDownNum = dt.Rows[0]["strTrainDownNum"].ToString();
                strTrainDownPictureFileName = dt.Rows[0]["strTrainDownPictureFileName"].ToString();
                ApanageName = dt.Rows[0]["Name"].ToString();
                SetValue(this, dt.Rows[0]);
            }
        }

        public static lsDatJT6JiChe SetValue(lsDatJT6JiChe JiChe, DataRow dr)
        {
            if (dr != null)
            {
                JiChe.ID =PageBase.static_ext_int(dr["ID"]);
                JiChe.LocoType = dr["LocoType"].ToString();
                JiChe.LocoCode = dr["LocoCode"].ToString();
                JiChe.LocoNum = dr["LocoNum"].ToString();
                JiChe.RuDuanDate = PageBase.static_ext_date(dr["RuDuanDate"].ToString());
                JiChe.RuDuanID = dr["RuDuanID"].ToString();
                JiChe.RuDuanName = dr["RuDuanName"].ToString();
                JiChe.JiaoCheDate = PageBase.static_ext_date(dr["JiaoCheDate"].ToString());
                JiChe.JiaoCheID = dr["JiaoCheID"].ToString();
                JiChe.JiaoCheName = dr["JiaoCheName"].ToString();
                JiChe.ChuDuanDate = PageBase.static_ext_date(dr["ChuDuanDate"].ToString());
                JiChe.ChuDuanID = dr["ChuDuanID"].ToString();
                JiChe.ChuDuanName = dr["ChuDuanName"].ToString();
                JiChe.Mark = PageBase.static_ext_int(dr["Mark"].ToString());
                JiChe.GovernUnitId = PageBase.static_ext_int(dr["GovernUnitId"].ToString());
                JiChe.UnitId = PageBase.static_ext_int(dr["UnitId"].ToString());
                JiChe.DepartmentId = PageBase.static_ext_int(dr["DepartmentId"].ToString());
                JiChe.ApanageId = PageBase.static_ext_int(dr["ApanageId"].ToString());
                JiChe.TrackId = PageBase.static_ext_int(dr["TrackId"].ToString());
                JiChe.ZBSJ = dr["ZBSJ"].ToString();
                JiChe.CreateDate = PageBase.static_ext_date(dr["CreateDate"].ToString());
                JiChe.dtHandleBeginTime = PageBase.static_ext_date(dr["dtHandleBeginTime"].ToString());
                JiChe.strHandleCaseID = dr["strHandleCaseID"].ToString();
                JiChe.nState = PageBase.static_ext_int(dr["nState"].ToString());
                JiChe.dtHandleEndTime = PageBase.static_ext_date(dr["dtHandleEndTime"].ToString());
                JiChe.nBeginHandleUserID = PageBase.static_ext_string(dr["nBeginHandleUserID"]);
                JiChe.nDirection = PageBase.static_ext_int(dr["nDirection"]);
                JiChe.nMapX = PageBase.static_ext_double(dr["nMapX"]);
                JiChe.nMapY = PageBase.static_ext_double(dr["nMapY"]);
                JiChe.nDetailedState = PageBase.static_ext_int(dr["nDetailedState"]);
                JiChe.nTieXieState = PageBase.static_ext_int(dr["nTieXieState"]);
                JiChe.nRuDuanType = PageBase.static_ext_int(dr["nRuDuanType"]);
                JiChe.nLocationLock = PageBase.static_ext_int(dr["nLocationLock"]);
                JiChe.nGL = PageBase.static_ext_int(dr["nGL"]);
            }
            return JiChe;
        }
        #endregion 构造函数

        #region 增删改
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string sqlcondition = "";
            sqlcondition += dtHandleEndTime != null ? ",dtHandleEndTime=@dtHandleEndTime" : "";
            sqlcondition += dtHandleBeginTime != null ? ",dtHandleBeginTime=@dtHandleBeginTime" : "";
            PageBase.log(sqlcondition);
            string strSql = "update lsDatJT6JiChe set nState = @nState,nBeginHandleUserID=@nBeginHandleUserID" + sqlcondition + " where ID=@ID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ID",ID),
                                           new SqlParameter("nState",nState),
                                           new SqlParameter("nBeginHandleUserID",nBeginHandleUserID),
                                           new SqlParameter("dtHandleBeginTime",dtHandleBeginTime),
                                           new SqlParameter("dtHandleEndTime",dtHandleEndTime)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }

        /// <summary>
        /// 更新车顶司机状态
        /// </summary>
        /// <returns></returns>
        public static bool UpdateSJstate(string userid,string gxid,int jcid)
        {
         string strSql = "update lsDatJT6JiChe set " + userid + "='" + gxid + "' where ID='" + jcid + "'";
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql) > 0;
        }

        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <returns></returns>
        public bool UpdatePrintState()
        {
            string strSql = "update lsDatJT6JiChe set nIsPrint =@nIsPrint where ID=@ID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ID",ID),
                                           new SqlParameter("nIsPrint",nIsPrint)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        /// <summary>
        /// 更新当前环节
        /// </summary>
        /// <returns></returns>
        public bool UpdateCaseID()
        {
            string sqlcondition = "";
            sqlcondition += Mark != 0 ? ",Mark=@Mark" : "";
            sqlcondition += JiaoCheDate != null ? ",JiaoCheDate=@JiaoCheDate" : "";
            sqlcondition += PageBase.static_ext_string(JiaoCheID) != "" ? ",JiaoCheID=@JiaoCheID" : "";
            sqlcondition += PageBase.static_ext_string(JiaoCheName) != "" ? ",JiaoCheName=@JiaoCheName" : "";
            sqlcondition += PageBase.static_ext_string(strHandleCaseID) != "" ? ",strHandleCaseID=@strHandleCaseID" : "";
            //sqlcondition += dtHandleEndTime != null ? ",dtHandleEndTime=@dtHandleEndTime" : "";
            sqlcondition += nDetailedState != 0 ? ",nDetailedState=@nDetailedState" : "";
            string strSql = "update lsDatJT6JiChe set nState=@nState" + sqlcondition + " where ID=@ID";
            
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ID",ID),
                                           new SqlParameter("strHandleCaseID",strHandleCaseID),
                                           new SqlParameter("nState",nState),
                                           new SqlParameter("nDetailedState",nDetailedState),
                                           new SqlParameter("Mark",Mark),
                                           new SqlParameter("JiaoCheDate",JiaoCheDate),
                                           new SqlParameter("JiaoCheID",JiaoCheID),
                                           new SqlParameter("JiaoCheName",JiaoCheName),
                                           new SqlParameter("dtHandleEndTime",dtHandleEndTime)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }


        /// <summary>
        ///  设置开始时间 及当前环节id
        /// </summary>
        /// <param name="lcid"></param>
        /// <returns></returns>
        public bool Update(string lcid)
        {
            string strSql = "update lsDatJT6JiChe set strHandleCaseID=(select strID from TAB_HandleCase where strProcessID=@lcid and nOrder=1) where ID=@ID";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("ID",ID),
                                           new SqlParameter("lcid",lcid)
                                       };
            return SqlHelper.ExecuteNonQuery(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams) > 0;
        }
        #endregion 增删改

        #region 扩展方法
        public static DataTable GetAlllsDatJT6JiChe(string uid)
        {
            string strSql = "select * from lsDatJT6JiChe where 1=1";
            if (uid != "")
            {
                strSql += " and ID = @uid ";
            }
            strSql += " order by ID ";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("uid",uid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }
        /// <summary>
        /// 获取所有在段已整备机车
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAlllsDatJT6JiCheZDYZB(string apanageid)
        {
            string strSql = "select LocoType,LocoNum,dtHandleEndTime from lsDatJT6JiChe where (Mark <> 0) and (Mark <> 4) and nDetailedState=5 and ApanageId=@apanageid order by dtHandleEndTime asc";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("apanageid",apanageid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }
        /// <summary>
        /// 获取所有在段机车
        /// </summary>
        /// <returns></returns>
        public static List<lsDatJT6JiChe> GetAlllsDatJT6JiCheZD(int apanageid)
        {
            string strSql = "select * from View_lsDatJT6JiChe where (Mark <> 0) and (Mark <> 4) and ApanageId=@apanageid  and ((trackid <> '' and ndirection>0) or (nmapx<>0 and nmapy<>0)) order by RuDuanDate asc";
            List<lsDatJT6JiChe> JiCheList=new List<lsDatJT6JiChe>();
            SqlParameter[] sqlParams = {
                                           new SqlParameter("apanageid",apanageid)
                                       };
            DataTable jcdt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if(jcdt.Rows.Count>0)
            {
                for(int i=0;i<jcdt.Rows.Count;i++)
                {
                    lsDatJT6JiChe jc=new lsDatJT6JiChe();
                    jc.UserName = jcdt.Rows[i]["UserName"].ToString();
                    jc.UserNum = jcdt.Rows[i]["UserNum"].ToString();
                    jc.strPictureFileName = jcdt.Rows[i]["strPictureFileName"].ToString();
                    JiCheList.Add(SetValue(jc,jcdt.Rows[i]));
                }
            }
            return JiCheList;
        }

        /// <summary>
        /// 获取所有在段整备中机车
        /// </summary>
        /// <returns></returns>
        public static List<lsDatJT6JiChe> GetAllJiCheZBZ(int apanageid)
        {
            string sqloption = "";
            sqloption += apanageid == 0 ? "" : "and ApanageID=@ApanageID";
            string strSql = "select * from lsDatJT6JiChe where Mark <> 0 and Mark <> 4 and LocoType in (select strTrainType from TAB_HandleProcessTrainType) and TrackId in(select TrackID from TAB_ZbcAreaConfig where 1=1 " + sqloption + " )  and nState=1 " + sqloption + " order by RuDuanDate desc";
            List<lsDatJT6JiChe> JiCheList = new List<lsDatJT6JiChe>();
            SqlParameter[] sqlParams = {
                                           new SqlParameter("apanageid",apanageid)
                                       };
            DataTable jcdt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
            if (jcdt.Rows.Count > 0)
            {
                for (int i = 0; i < jcdt.Rows.Count; i++)
                {
                    lsDatJT6JiChe jc = new lsDatJT6JiChe();
                    JiCheList.Add(SetValue(jc, jcdt.Rows[i]));
                }
            }
            return JiCheList;
        }

        /// <summary>
        /// 获取所有在段整备中机车
        /// </summary>
        /// <returns></returns>
        public static int GetJiCheZBZTop1ID(int apanageid)
        {
            string strSql = "select top 1 ID from lsDatJT6JiChe where Mark=1 and nState=1 and ApanageId=@apanageid order by RuDuanDate asc";
            List<lsDatJT6JiChe> JiCheList = new List<lsDatJT6JiChe>();
            SqlParameter[] sqlParams = {
                                           new SqlParameter("apanageid",apanageid)
                                       };
            return PageBase.static_ext_int(SqlHelper.ExecuteScalar(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams));
        }
        #endregion


        /// <summary>
        /// 更新机车状态,包括当前环节,以及机车状态
        /// </summary>
        /// <returns></returns>
        public static void UpdateJiCheState(int nJiCheID)
        {
            
            //1.根据机车ID,获得当前环节ID
            lsDatJT6JiChe JiChe = new lsDatJT6JiChe(nJiCheID);

            //2.如果当前环节ID为空,则跳过,否则,根据环节ID,检查下面所有步骤是否整备完毕,如果整备完毕,则切换到下一个环节ID(如果没有,则不改变环节ID)
            if (JiChe.strHandleCaseID != "")
            {
                UpdateJiCheCase(JiChe);
            }
            
            //3.检查当前机车状态是否为1,如果为1的话,检查该机车是否全部步骤整备完毕,如果整备完毕,设置nState为2
            if (JiChe.nState == 1)
            { 
                List<HandleCase> HandleCaseList = HandleCase.getJieCheCaseList(PageBase.static_ext_int(JiChe.ID));
                Boolean bIsAllHandle = true;
                for (int i = 0; i < HandleCaseList.Count; i++)
                {
                    if (CheckJiCheCase(JiChe, HandleCaseList[i]) == false)
                    {
                        bIsAllHandle = false;
                        break; 
                    }
                }

                if (Lxd.GetCountJt6tpFromJiCheID(JiChe.ID) > 0)
                {
                    bIsAllHandle = false;
                }
                if (JiChe.LocoType == "HXD2B")
                {
                    if (LKJDataAnalysis.GetAllTAB_LKJDataAnalysis(JiChe.ID) <= 0)
                    {
                        bIsAllHandle = false;
                    }
                }
                if (bIsAllHandle)
                {
                    JiChe.nState = 2;
                    PageBase.log("要更新endtime");
                    JiChe.dtHandleEndTime = DateTime.Now;
                    JiChe.Update();
                }
            }

        }

        //功能:更新机车环节
        private static void UpdateJiCheCase(lsDatJT6JiChe JiChe)
        {
            HandleCase Case = new HandleCase(JiChe.strHandleCaseID);
            CheckNextCase(JiChe, Case);
        }

        private static void CheckNextCase(lsDatJT6JiChe JiChe, HandleCase Case)
        {
            if (CheckJiCheCase(JiChe, Case))
            {
                //更新环节记录
                UpdateCaseRecord(JiChe.ID, JiChe.strHandleCaseID);

                //获得下一个环节信息
                HandleCase NextCase = Case.getNextCase(Case.nOrder, Case.strID);
                if (NextCase != null)
                {
                    PageBase.log("下一个环节" + NextCase.strID);
                    JiChe.strHandleCaseID = NextCase.strID;
                    JiChe.UpdateCaseID();
                    addCaseRecord(JiChe.ID, NextCase.strID);
                    CheckNextCase(JiChe, NextCase);
                }
            }
        }
        /// <summary>
        /// 更新环节记录
        /// </summary>
        /// <param name="jcid"></param>
        /// <param name="caseid"></param>
        private static void UpdateCaseRecord(int jcid, string caseid)
        {
            HandleCaseRecord CaseRecord = new HandleCaseRecord();
            CaseRecord.dtEndTime = DateTime.Now;
            CaseRecord.nIsComplete = 1;
            CaseRecord.strTrainHandleID = jcid;
            CaseRecord.strCaseID = caseid;
            //更新环节信息完成状态 及时间
            CaseRecord.Update();
        }

        /// <summary>
        /// 增加一条环节信息记录
        /// </summary>
        private static void addCaseRecord(int jcid,string caseid)
        {
            HandleCaseRecord CaseRecord = new HandleCaseRecord();
            CaseRecord.dtBeginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            CaseRecord.strTrainHandleID = jcid;
            CaseRecord.strCaseID = caseid;
            CaseRecord.nIsComplete = 0;
            CaseRecord.Addhadcaseid();//添加一条整备环节记录
        }
        //功能:检查机车某一个环节是否整备完毕
        private static bool CheckJiCheCase(lsDatJT6JiChe JiChe,HandleCase Case)
        {
            return HandleCase.boolCheckCaseFinish(Case.strID, JiChe.ID);
        }

        /// <summary>
        /// 获取dayCount天内机车数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSevenDaylsDatJT6JiChe(int apanageid,int dayCount)
        {
            string strSql = @"select CONVERT(varchar(100), RuDuanDate, 23) as RuDuanDate,Mark,nState
 from lsDatJT6JiChe 
where RuDuanDate > DATEADD(dd, -"+dayCount.ToString()+ @",CONVERT(varchar(100), getdate(), 23)) and RuDuanDate< getdate()
 and apanageid=@apanageid and mark<>4
order by RuDuanDate";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("apanageid",apanageid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }

        /// <summary>
        /// 获取当天整备工时数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCurentDaylsDatJT6JiChe(int apanageid)
        {
            string strSql = @"select LocoType,LocoNum,DATEDIFF(minute,dtHandleBeginTime,dtHandleEndTime) as Zbys
 from lsDatJT6JiChe  
where RuDuanDate > CONVERT(varchar(100), getdate(), 23) 
 and apanageid=@apanageid and nState>=2 and mark<>4
order by RuDuanDate";
            SqlParameter[] sqlParams = {
                                           new SqlParameter("apanageid",apanageid)
                                       };
            return SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig("57"), CommandType.Text, strSql, sqlParams).Tables[0];
        }
    }
}
