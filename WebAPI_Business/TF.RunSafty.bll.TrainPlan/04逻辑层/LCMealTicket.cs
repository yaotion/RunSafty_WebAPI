using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Data;
using TF.RunSafty.Plan.MD;
using TF.CommonUtility;
using ThinkFreely.DBUtility;


namespace TF.RunSafty.Plan
{
    public class LCMealTicket
    {
        #region '根据饭票人员信息获取饭票张数'
        public class InGetTicket
        {
            //人员饭票信息
            public MealTicketPerson TickPersion = new MealTicketPerson();
        }

        public class OutGetTicket
        {
            //早晨券数
            public int CanQuanA = 0;
            //午餐卷数
            public int CanQuanB = 0;
            //是否找到
            public int Exist = 0;
        }

        /// <summary>
        /// 根据饭票人员信息获取饭票张数
        /// </summary>
        public InterfaceOutPut GetTicket(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTicket InParams = javaScriptSerializer.Deserialize<InGetTicket>(Data);
                OutGetTicket OutParams = new OutGetTicket();
                output.data = OutParams;
                string strQuDuan = InParams.TickPersion.strQuDuan;
                string strCheCi = InParams.TickPersion.strCheCi;
                DateTime dtPaiBanTime = InParams.TickPersion.dtPaiBan;
                string strWorkShopGUID =InParams.TickPersion.strWorkShopGUID;

                DataTable dt = getRoleticket(strCheCi, strQuDuan, dtPaiBanTime, strWorkShopGUID);
                if (dt != null)
                {
                    OutParams.CanQuanA = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[0]["iA"], 0);
                    OutParams.CanQuanB = TF.RunSafty.Utils.Parse.TFParse.DBToInt(dt.Rows[0]["iB"], 0);
                    OutParams.Exist = 1;
                    output.result = 0;
                }
                else
                {
                    OutParams.CanQuanA = 0;
                    OutParams.CanQuanB = 0;
                    OutParams.Exist = 1;
                    output.result = 0;
                }

            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTicket:" + ex.Message);
                throw ex;
            }
            return output;
        }

        public DataTable getRoleticket(string strCheCi, string strQuDuan, DateTime dtPaiBanTime,string strWorkShopGUID)
        {
            SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strCheCi",strCheCi),
                    new SqlParameter("strQuDuan",strQuDuan),
                    new SqlParameter("PaiBanTime",dtPaiBanTime),
                    new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                };
            //检查是是否有指定车次或者指定区段
            string strSql = @"select iA,iB from  Tab_MealTicket_Rule where strGUID = 
                        (select top 1 strRuleGUID from Tab_MealTicket_CheCi where ( strCheCi = '" + strCheCi
                    + "'  and strQuDuan = @strQuDuan )   and (convert(varchar(50),@PaiBanTime,108) between CONVERT(varchar(50),dtStartTime,108) and CONVERT(varchar(50),dtEndTime,108) ) and strWorkShopGUID = @strWorkShopGUID )";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
                return dt;
            else
            {
                //指定区段 车次为空
                strSql = @"select iA,iB from  Tab_MealTicket_Rule where strGUID = 
                        (select top 1 strRuleGUID from Tab_MealTicket_CheCi where ( strCheCi = ''  and strQuDuan = @strQuDuan ) 
                        and (convert(varchar(50),@PaiBanTime,108) between CONVERT(varchar(50),dtStartTime,108) and CONVERT(varchar(50),dtEndTime,108) ) and strWorkShopGUID = @strWorkShopGUID )";
                DataTable dt1 = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                if (dt1.Rows.Count > 0)
                    return dt1;
                else
                {
                    //检查是否有通用车次                    
                    strSql = @"select iA,iB from  Tab_MealTicket_Rule where strGUID = (select top 1 strRuleGUID from Tab_MealTicket_CheCi 
                        where  ( strQuDuan = '' )and ( strCheCi = '' ) and (convert(varchar(50),@PaiBanTime,108) between CONVERT(varchar(50),dtStartTime,108) and CONVERT(varchar(50),dtEndTime,108) ) and strWorkShopGUID =@strWorkShopGUID)";
                    DataTable dt2 = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                    if (dt2.Rows.Count > 0)
                        return dt2;
                    else
                        return null;

                }
            }
        }

        #endregion

        #region 查询规则
        public class InQueryRule
        {
            //车间GUID
            public string WorkShopGUID;
            //交路类型
            public int AType;
        }

        public class OutQueryRule
        {
            //规则列表
            public MealTicketRuleList RuleList = new MealTicketRuleList();
        }

        /// <summary>
        /// 查询规则
        /// </summary>
        public InterfaceOutPut QueryRule(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InQueryRule InParams = javaScriptSerializer.Deserialize<InQueryRule>(Data);
                OutQueryRule OutParams = new OutQueryRule();
                output.data = OutParams;
                string strSql = "SELECT * from Tab_MealTicket_Rule where strWorkShopGUID = @strWorkShopGUID and iType = @iType";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID),
                    new SqlParameter("iType",InParams.AType)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MealTicketRule rule = new MealTicketRule();
                    PS.PSMealTicket.MealTicketRuleFromDB(rule,dt.Rows[i]);
                    OutParams.RuleList.Add(rule);
                }
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.QueryRule:" + ex.Message);
                throw ex;
            }
            return output;
        }   

        #endregion

        #region 添加规则
        public class InAddRule
        {
            //规则
            public MealTicketRule Rule = new MealTicketRule();
        }

        /// <summary>
        /// 增加规则
        /// </summary>
        public InterfaceOutPut AddRule(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InAddRule InParams = javaScriptSerializer.Deserialize<InAddRule>(Data);
                string strSql = @"insert into Tab_MealTicket_Rule(strName,strGUID,strWorkShopGUID,iA,iB,iType) 
                    values (@strName,@strGUID,@strWorkShopGUID,@iA,@iB,@iType)";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strName",InParams.Rule.strName),
                    new SqlParameter("strGUID",InParams.Rule.strGUID),
                    new SqlParameter("strWorkShopGUID",InParams.Rule.strWorkShopGUID),
                    new SqlParameter("iA",InParams.Rule.iA),
                    new SqlParameter("iB",InParams.Rule.iB),
                    new SqlParameter("iType",InParams.Rule.iType)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AddRule:" + ex.Message);
                throw ex;
            }
            return output;
        }      
        #endregion

        #region 删除规则
        public class InDeleteRule
        {
            //规则GUID
            public string RuleGUID;
        }

        /// <summary>
        /// 删除规则
        /// </summary>
        public InterfaceOutPut DeleteRule(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InDeleteRule InParams = javaScriptSerializer.Deserialize<InDeleteRule>(Data);
                string strSql = "delete from  Tab_MealTicket_CheCi where strRuleGUID = @strRuleGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strRuleGUID",InParams.RuleGUID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

                strSql = "delete from  Tab_MealTicket_Rule where strGUID = @strRuleGUID";             
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.DeleteRule:" + ex.Message);
                throw ex;
            }
            return output;
        }     
        #endregion

        #region 修改规则
        public class InModifyRule
        {
            //规则GUID
            public string RuleGUID;
            //修改后的规则
            public MealTicketRule Rule = new MealTicketRule();
        }

        /// <summary>      
        /// 修改规则
        /// </summary>
        public InterfaceOutPut ModifyRule(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InModifyRule InParams = javaScriptSerializer.Deserialize<InModifyRule>(Data);
                string strSql = @"update Tab_MealTicket_Rule set strName=@strName,strWorkShopGUID=@strWorkShopGUID,
                   iA=@iA,iB=@iB,iType=@iType where strGUID = @strGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strName",InParams.Rule.strName),
                    new SqlParameter("strGUID",InParams.RuleGUID),
                    new SqlParameter("strWorkShopGUID",InParams.Rule.strWorkShopGUID),
                    new SqlParameter("iA",InParams.Rule.iA),
                    new SqlParameter("iB",InParams.Rule.iB),
                    new SqlParameter("iType",InParams.Rule.iType)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.ModifyRule:" + ex.Message);
                throw ex;
            }
            return output;
        }         

        #endregion

        #region 根据RULE的GUID查询下面的所有车次
        public class InQueryCheCiInfo
        {
            //规则GUID                                
            public string RuleGUID;
        }

        public class OutQueryCheCiInfo
        {
            //规则列表
            public MealTicketCheCiList RuleList = new MealTicketCheCiList();
        }

        /// <summary>
        /// 根据RULE的GUID查询下面的所有车次
        /// </summary>
        public InterfaceOutPut QueryCheCiInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InQueryCheCiInfo InParams = javaScriptSerializer.Deserialize<InQueryCheCiInfo>(Data);
                OutQueryCheCiInfo OutParams = new OutQueryCheCiInfo();
                output.data = OutParams;
                string strSql = @"SELECT * from Tab_MealTicket_CheCi where strRuleGUID = @strRuleGUID ";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strRuleGUID",InParams.RuleGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MealTicketCheCi cc = new MealTicketCheCi();
                    PS.PSMealTicket.MealTicketCheciFromDB(cc,dt.Rows[i]);
                    OutParams.RuleList.Add(cc);
                }
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.QueryCheCiInfo:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 添加车次规则
        public class InAddCheCiInfo
        {
            //车次规则
            public MealTicketCheCi CheciRule = new MealTicketCheCi();


        }

        /// <summary>
        /// 添加车次规则
        /// </summary>
        public InterfaceOutPut AddCheCiInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InAddCheCiInfo InParams = javaScriptSerializer.Deserialize<InAddCheCiInfo>(Data);
                string strSql = @"insert into Tab_MealTicket_CheCi(strWorkShopGUID,iType,strQuDuan,strRuleGUID,strGUID,strCheCi,dtStartTime,dtEndTime) 
                    values (@strWorkShopGUID,@iType,@strQuDuan,@strRuleGUID,@strGUID,@strCheCi,@dtStartTime,@dtEndTime)";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strWorkShopGUID",InParams.CheciRule.strWorkShopGUID),
                    new SqlParameter("iType",InParams.CheciRule.iType),
                    new SqlParameter("strQuDuan",InParams.CheciRule.strQuDuan),
                    new SqlParameter("strRuleGUID",InParams.CheciRule.strRuleGUID),
                    new SqlParameter("strGUID",InParams.CheciRule.strGUID),
                    new SqlParameter("strCheCi",InParams.CheciRule.strCheCi),
                    new SqlParameter("dtStartTime",InParams.CheciRule.dtStartTime),
                    new SqlParameter("dtEndTime",InParams.CheciRule.dtEndTime)                    
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);

                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.AddCheCiInfo:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 删除车次规则
        public class InDeleteChiCiInfo
        {
            //车次规则GUID
            public string CheCiGUID;
        }
                
        /// <summary>
        /// 删除车次规则
        /// </summary>
        public InterfaceOutPut DeleteChiCiInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InDeleteChiCiInfo InParams = javaScriptSerializer.Deserialize<InDeleteChiCiInfo>(Data);
                string strSql = "delete from  Tab_MealTicket_CheCi where strGUID = @strRuleGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strRuleGUID",InParams.CheCiGUID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.DeleteChiCiInfo:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion

        #region 修改车次规则
        public class InModifyCheCiInfo
        {
            //车次规则GUID
            public string CheCiGUID;
            //CheCiInfo
            public MealTicketCheCi Rule = new MealTicketCheCi();
        }

        /// <summary>
        /// 修改车次规则
        /// </summary>
        public InterfaceOutPut ModifyCheCiInfo(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InModifyCheCiInfo InParams = javaScriptSerializer.Deserialize<InModifyCheCiInfo>(Data);
                string strSql = @"update Tab_MealTicket_CheCi set strWorkShopGUID=strWorkShopGUID,iType=@iType,strQuDuan=@strQuDuan,
                        strRuleGUID=@strRuleGUID,strCheCi=@strCheCi,dtStartTime=@dtStartTime,dtEndTime = @dtEndTime where strGUID = @strGUID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strWorkShopGUID",InParams.Rule.strWorkShopGUID),
                    new SqlParameter("iType",InParams.Rule.iType),
                    new SqlParameter("strQuDuan",InParams.Rule.strQuDuan),
                    new SqlParameter("strRuleGUID",InParams.Rule.strRuleGUID),
                    new SqlParameter("strGUID",InParams.CheCiGUID),
                    new SqlParameter("strCheCi",InParams.Rule.strCheCi),
                    new SqlParameter("dtStartTime",InParams.Rule.dtStartTime),
                    new SqlParameter("dtEndTime",InParams.Rule.dtEndTime)                    
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.ModifyCheCiInfo:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion


        public class InterfaceOutPut
        {
            public int result;
            public string resultStr;
            public object data;
        }
        #region  获取饭票日志
      

        public class InData
        {
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string DriverCode { get; set; }
            public string ShenHeCode { get; set; }
        }
        public InterfaceOutPut GetMealTicket_log(string Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            InData InParams = javaScriptSerializer.Deserialize<InData>(Data);
            List<SqlParameter> p = new List<SqlParameter>();
            string sql = "select * from TAB_MealTicket_log where 1=1 {0}";
            string sqlwhere = "";
            if (string.IsNullOrEmpty(InParams.StartDate) || string.IsNullOrEmpty(InParams.EndDate))
            {
                if (!string.IsNullOrEmpty(InParams.StartDate))
                {
                    sqlwhere += " and dtCreateTime>=@StartDate ";
                    p.Add(new SqlParameter("StartDate", Convert.ToDateTime(InParams.StartDate)));
                }
                if (!string.IsNullOrEmpty(InParams.EndDate))
                {
                    sqlwhere += " and dtCreateTime<=@EndDate ";
                    p.Add(new SqlParameter("EndDate", Convert.ToDateTime(InParams.EndDate)));
                }
            }
            else if (!string.IsNullOrEmpty(InParams.StartDate) && !string.IsNullOrEmpty(InParams.EndDate))
            {
                sqlwhere += " and dtCreateTime between @StartDate and @EndDate ";
                p.Add(new SqlParameter("StartDate", Convert.ToDateTime(InParams.StartDate)));
                p.Add(new SqlParameter("EndDate", Convert.ToDateTime(InParams.EndDate)));
            }
            if (!string.IsNullOrEmpty(InParams.DriverCode))
                sqlwhere += " and strDriverCode=@DriverCode ";
            if (!string.IsNullOrEmpty(InParams.ShenHeCode))
                sqlwhere += " and strFaFangCode=@ShenHeCode";
            p.Add(new SqlParameter("DriverCode", InParams.DriverCode));
            p.Add(new SqlParameter("ShenHeCode", InParams.ShenHeCode));
            try
            {
                sql = string.Format(sql, sqlwhere);
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql, p.ToArray()).Tables[0];

                MDMealTicket Ticket = null;
                List<MDMealTicket> list = list = new List<MDMealTicket>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Ticket = new MDMealTicket();
                        Ticket.DRIVER_CODE = dt.Rows[i]["strDriverCode"].ToString();
                        Ticket.DRIVER_NAME = dt.Rows[i]["strDriverName"].ToString();
                        Ticket.CHEJIAN_NAME = dt.Rows[i]["strCheJianName"].ToString();
                        Ticket.PAIBAN_CHECI = dt.Rows[i]["strCheCi"].ToString();
                        Ticket.CHUQIN_TIME = dt.Rows[i]["dtChuQin"].ToString();
                        Ticket.CANQUAN_A = dt.Rows[i]["nTicketA"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[i]["nTicketA"].ToString());
                        Ticket.CANQUAN_B = dt.Rows[i]["nTicketB"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[i]["nTicketB"].ToString());
                        Ticket.SHENHEREN_CODE = dt.Rows[i]["strFaFangCode"].ToString();
                        Ticket.SHENHEREN_NAME = dt.Rows[i]["strFaFangName"].ToString();
                        Ticket.REC_TIME = Convert.ToDateTime(dt.Rows[i]["dtCreateTime"].ToString()).ToString("yyyy-MM-dd HH:mm");
                        Ticket.ID = Convert.ToInt32(dt.Rows[i]["nID"].ToString());
                        list.Add(Ticket);
                    }
                }
                output.result = 0;
                output.data = list;
            }
            catch (Exception ex)
            {
                output.result = 1;
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetMealTicket_log:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion


        #region 记录发放饭票的日志
        public class InLogMealTicket
        {
            //饭票信息
            public MealTicket Ticket = new MealTicket();
        }

        /// <summary>
        /// 记录发放饭票的日志
        /// </summary>
        public InterfaceOutPut LogMealTicket(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InLogMealTicket InParams = javaScriptSerializer.Deserialize<InLogMealTicket>(Data);
                string strSql = @"insert into TAB_MealTicket_log (strDriverCode,strDriverName,strCheJianName,strCheCi,dtChuQin,
                    nTicketA,nTicketB,strFaFangCode,strFaFangName,dtCreateTime) values (@strDriverCode,@strDriverName,@strCheJianName,@strCheCi,@dtChuQin,
                    @nTicketA,@nTicketB,@strFaFangCode,@strFaFangName,@dtCreateTime)";
                SqlParameter[] sqlParams = new SqlParameter[]{
                    new SqlParameter("strDriverCode",InParams.Ticket.DRIVER_CODE),
                    new SqlParameter("strDriverName",InParams.Ticket.DRIVER_NAME),
                    new SqlParameter("strCheJianName",InParams.Ticket.CHEJIAN_NAME),
                    new SqlParameter("strCheCi",InParams.Ticket.PAIBAN_CHECI),
                    new SqlParameter("dtChuQin",InParams.Ticket.CHUQIN_TIME),
                    new SqlParameter("nTicketA",InParams.Ticket.CANQUAN_A),
                    new SqlParameter("nTicketB",InParams.Ticket.CANQUAN_B),
                    new SqlParameter("strFaFangCode",InParams.Ticket.SHENHEREN_CODE),
                    new SqlParameter("strFaFangName",InParams.Ticket.SHENHEREN_NAME),
                    new SqlParameter("dtCreateTime",DateTime.Now)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
               
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.LogMealTicket:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion


        #region 删除发放饭票日志
        public class InDeleteMealTicketLog
        {
            //车次规则GUID
            public int nID;
        }

        /// <summary>
        ///  删除发放饭票日志
        /// </summary>
        public InterfaceOutPut DeleteMealTicketLog(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InDeleteMealTicketLog InParams = javaScriptSerializer.Deserialize<InDeleteMealTicketLog>(Data);
                string strSql = "delete from  TAB_MealTicket_log where nID = @nID";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("nID",InParams.nID)
                };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.DeleteMealTicketLog:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion
    }
}
