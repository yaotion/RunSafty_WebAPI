using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TF.CommonUtility;
using TF.RunSafty.Entry;
using TF.RunSafty.Model.InterfaceModel;
using TF.Runsafty.Utility;
using ThinkFreely.DBUtility;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace TF.RunSafty.Utility
{
    public class TGlobalDM
    {
        #region 获取服务器配置
        public class DB_SysConfig_In
        {
            public string SectionName = "";
            public string Ident = "";
        }

        public class DB_SysConfig_Out : JsonOutBase
        {
            public DB_SysConfig_Data data = new DB_SysConfig_Data();
        }

        public class DB_SysConfig_Data
        {
            public string strValue = String.Empty;
        }

        public DB_SysConfig_Out GetDBSysConfig(string input)
        {
            DB_SysConfig_Out json = new DB_SysConfig_Out();
            DB_SysConfig_In model = null;
            try
            {
                model = JsonConvert.DeserializeObject<DB_SysConfig_In>(input);
                string strSql = " select * from TAB_System_Config where strSection =@strSection and strIdent =@strIdent ";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strSection",model.SectionName), new SqlParameter("strIdent",model.Ident), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    json.data.strValue = table.Rows[0]["strValue"].ToString();
                    json.result = "0";
                    json.resultStr = "获取配置成功";
                }
                else
                {
                    json.data.strValue = String.Empty;
                    json.result = "1";
                    json.resultStr = "获取配置失败";
                }
            }
            catch (Exception ex)
            {
                LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }

        #endregion


        #region 获取所有的配置信息

        public class SysConfig
        {
            public string Section;
            public string Ident;
            public string Value;
        }

        public class Get_In
        {
        }
        public class Get_Out
        {
            public string result = "";
            public string resultStr = "";
            public List<SysConfig> data;
        }
        public Get_Out GetDBConfigValues(string data)
        {
            Get_Out json = new Get_Out();
            try
            {
                string strSql = "select * from TAB_System_Config";

                DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                json.data = new List<SysConfig>();
                SysConfig config = null;
                foreach (DataRow dataRow in table.Rows)
                {
                    config = new SysConfig();
                    config.Ident = dataRow["strIdent"].ToString();
                    config.Section = dataRow["strSection"].ToString();
                    config.Value = dataRow["strValue"].ToString();
                    json.data.Add(config);
                }
                json.result = "0";
                json.resultStr = "获取列表成功！";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }

        #endregion

        #region 设置数据库配置

        public class SetConfig_In
        {
            public string SectionName = String.Empty;
            public string Ident = String.Empty;
            public string Value = String.Empty;
        }
        public class SetConfig_Out : JsonOutBase { }

        public SetConfig_Out SetDBSysConfig(string input)
        {
            SetConfig_Out json = new SetConfig_Out();
            SetConfig_In model = null;
            string strSql = String.Empty;
            int count = -1;
            try
            {
                model = JsonConvert.DeserializeObject<SetConfig_In>(input);
                strSql = "select count(*) from TAB_System_Config where strSection = @strSection and strIdent =@strIdent ";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strSection",model.SectionName), new SqlParameter("strIdent",model.Ident), 
                    new SqlParameter("strValue",model.Value), 
                };
                count =
                    Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql,
                        sqlParameters));
                if (count == 0)
                {
                    strSql =
                        "insert into TAB_System_Config(strSection,strIdent,strValue) values(@strSection,@strIdent,@strValue)";
                }
                else
                {
                    strSql =
                        "update TAB_System_Config set strValue=@strValue where strSection = @strSection and strIdent =@strIdent ";
                }
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
                json.result = "0";
                json.resultStr = "设置数据库配置成功";

            }
            catch (Exception ex)
            {
                LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 获取服务器时间

        public class Now
        {
            public string now = String.Empty;
        }
        public class GetNow_Out : JsonOutBase
        {
            public Now data = new Now();
        }

        public GetNow_Out GetNow(string input)
        {
            GetNow_Out json = new GetNow_Out();
            DateTime dtNow = DateTime.Now;
            json.result = "0";
            json.resultStr = "获取服务器时间成功";
            json.data.now = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
            return json;
        }


        #endregion

        #region 获取客户端配置

        public class SiteConfig_In
        {
            public string SiteNumber = String.Empty;
        }

        public class SiteConfig
        {
            public string strName = String.Empty;
            public string strValue = String.Empty;
        }

        public class SiteConfig_Out : JsonOutBase
        {
            public List<SiteConfig> data;
        }

        public SiteConfig_Out GetSiteConfig(string input)
        {
            SiteConfig_Out json = new SiteConfig_Out();
            SiteConfig_In model = null;
            try
            {
                model = JsonConvert.DeserializeObject<SiteConfig_In>(input);
                string strSql = "select * from TAB_System_SiteConfig where strSiteNumber=@strSiteNumber ";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strSiteNumber",model.SiteNumber), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                json.data = new List<SiteConfig>();
                SiteConfig config = null;
                foreach (DataRow dataRow in table.Rows)
                {
                    config = new SiteConfig();
                    config.strName = dataRow["strName"].ToString();
                    config.strValue = dataRow["strValue"].ToString();
                    json.data.Add(config);
                }
                json.result = "0";
                json.resultStr = "获取客户端配置成功";
            }
            catch (Exception ex)
            {
                LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 判断交路是否归客户端管理

        public class IsJiaoLuInSite_In
        {
            public string TrainJiaoluGUID = String.Empty;
            public string SiteGUID = String.Empty;
        }

        public class IsJiaoLuInSiteResult
        {
            public int isInSite;
        }
        public class IsJiaoLuInSite_Out : JsonOutBase
        {
            public IsJiaoLuInSiteResult data;
        }

        public IsJiaoLuInSite_Out IsJiaoLuInSite(string input)
        {
            IsJiaoLuInSite_Out json = new IsJiaoLuInSite_Out();
            json.data = new IsJiaoLuInSiteResult();
            IsJiaoLuInSite_In model = null;
            string strSql = String.Empty;
            DataTable table = null;
            try
            {
                model = JsonConvert.DeserializeObject<IsJiaoLuInSite_In>(input);
                strSql = @"select strTrainJiaoluGUID  from TAB_Base_TrainJiaoluInSite where strSiteGUID =@strSiteGUID
               and strTrainJiaoluGUID = @strTrainJiaoluGUID
               union  
               select strSubTrainJiaoLuGUID from TAB_Base_TrainJiaolu_SubDetail where strTrainJiaoluGUID in  
               (select strTrainJiaoluGUID  from TAB_Base_TrainJiaoluInSite where strSiteGUID =@strSiteGUID ) 
                 and strSubTrainJiaoLuGUID =@strTrainJiaoluGUID ";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strSiteGUID",model.SiteGUID), new SqlParameter("strTrainJiaoluGUID",model.TrainJiaoluGUID), 
                };
                table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    json.data.isInSite = 1;
                    json.result = "0";
                    json.resultStr = "判断交路是否在客户端的管理范围成功";
                }
                else
                {
                    json.data.isInSite = 0;
                }

            }
            catch (Exception ex)
            {
                LogClass.logex(ex, "");
                json.result = "1";
                json.resultStr = "判断交路是否在客户端的管理范围失败";
                throw ex;
            }
            return json;
        }
        #endregion

        #region 获取车站

        public class GetStation_In
        {
            public string strJiaoLuID = String.Empty;
        }
        public class Station
        {
            public string strStationGUID = String.Empty;
            public string strStationNumber = String.Empty;
            public string strStationName = String.Empty;
        }
        public class GetStation_Out : JsonOutBase
        {
            public List<Station> data;
        }

        public GetStation_Out GetStationsOfJiaoJu(string input)
        {
            GetStation_Out json = new GetStation_Out();
            json.data = new List<Station>();
            GetStation_In model = null;
            Station station = null;
            string strSql = String.Empty;
            try
            {
                model = JsonConvert.DeserializeObject<GetStation_In>(input);
                strSql = @"select B.strStationGUID,B.strStationNumber,B.strStationName from  
     TAB_Base_StationInTrainJiaolu AS A   inner join TAB_Base_Station AS B 
ON B.strStationGUID = A.strStationGUID  where A.strTrainJiaoluGUID = @strTrainJiaoluGUID order by nSortid";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strTrainJiaoluGUID",model.strJiaoLuID), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    station = new Station();
                    station.strStationGUID = row["strStationGUID"].ToString();
                    station.strStationName = row["strStationName"].ToString();
                    station.strStationNumber = row["strStationNumber"].ToString();
                    json.data.Add(station);
                }
                json.result = "0";
                json.resultStr = "获取车站成功";
            }
            catch (Exception ex)
            {
                LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 根据车次获取客货类别

        public class CheCi_In
        {
            public string TrainNo = String.Empty;
        }

        public class CheCiKehuo
        {
            public string Kehuo = String.Empty;
            public string Remarks = String.Empty;
        }
        public class CheCi_Out : JsonOutBase
        {
            public CheCiKehuo data = new CheCiKehuo();
        }

        public CheCi_Out GetKehuoByCheCi(string input)
        {
            CheCi_Out json = new CheCi_Out();
            CheCi_In model = null;
            //List<TrainNoBelong> trainNoBelongs = null;
            //string strReg = String.Empty;
            //Regex regex = null;
            //int nBeginNumber, nEndNumber;
            //bool isMatch = false;
            try
            {
                #region 原来的代码
                //model = JsonConvert.DeserializeObject<CheCi_In>(input);
                //trainNoBelongs = GetTrainNoBelongArray();
                //if (trainNoBelongs != null)
                //{
                //    foreach (var trainNoBelong in trainNoBelongs)
                //    {
                //        nBeginNumber = trainNoBelong.nBeginNumber;
                //        nEndNumber = trainNoBelong.nEndNumber;
                //        int CheCi;
                //        json.result = "0";
                //        json.resultStr = "获取车次成功";
                //        //构造正则表达式
                //        if (String.IsNullOrEmpty(trainNoBelong.strTrainNoHead))
                //        {
                //            if (Int32.TryParse(model.TrainNo, out CheCi) && CheCi >= nBeginNumber && CheCi <= nEndNumber)
                //            {
                //                json.data.Kehuo = trainNoBelong.strKehuoName;
                //                isMatch = true;
                //                break;
                //            }
                //        }
                //        else
                //        {
                //            strReg = String.Format("^{0}", trainNoBelong.strTrainNoHead);
                //        }
                //        regex = new Regex(strReg);
                //        Match match = regex.Match(model.TrainNo);
                //        string strNum = String.Empty;
                //        if (match.Success)
                //        {
                //            int num;
                //            strNum = model.TrainNo.Substring(match.Length, model.TrainNo.Length - match.Length);
                //            if (strNum.Length > 0)
                //            {
                //                if ((Int32.TryParse(strNum, out num) && num >= nBeginNumber && num <= nEndNumber))
                //                {
                //                    json.data.Kehuo = trainNoBelong.strKehuoName;
                //                    isMatch = true;
                //                    break;
                //                }
                //            }
                //            else
                //            {
                //                if (trainNoBelong.nBeginNumber == 0 && trainNoBelong.nEndNumber == 0)
                //                {
                //                    json.data.Kehuo = trainNoBelong.strKehuoName;
                //                    isMatch = true;
                //                    break;
                //                }
                //            }

                //        }

                //    }
                //}
                //if (!isMatch)
                //{
                //    //json.result = "1";
                //    //json.resultStr = "根据车次获取客货类型失败，没有找到匹配的规则";
                //    json.data.Kehuo = "";
                //}
                #endregion
                string Remarks = "";
                model = JsonConvert.DeserializeObject<CheCi_In>(input);
                json.data.Kehuo = getKeHuoByTrainNo(model.TrainNo,ref Remarks);
                json.data.Remarks = Remarks;
                json.result = "0";
                json.resultStr = "获取客货成功";
            }
            catch (Exception ex)
            {
                LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }

        //public List<TrainNoBelong> GetTrainNoBelongArray()
        //{
        //    List<TrainNoBelong> list = null;
        //    TrainNoBelong trainno = null;
        //    string strSql = "select a.*,b.strKehuoName from TAB_Base_TrainNoBelong a left join TAB_System_Kehuo b on a.nKehuoID=b.nKehuoID order by nid";
        //    DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
        //    if (table.Rows.Count > 0)
        //    {
        //        list = new List<TrainNoBelong>();
        //        foreach (DataRow row in table.Rows)
        //        {
        //            trainno = new TrainNoBelong();
        //            trainno.nBeginNumber = Convert.ToInt32(row["nBeginNumber"].ToString());
        //            trainno.nEndNumber = Convert.ToInt32(row["nEndNumber"].ToString());
        //            trainno.nKehuoID = row["nKehuoID"].ToString();
        //            trainno.strTrainNoHead = row["strTrainNoHead"].ToString();
        //            trainno.strKehuoName = row["strKehuoName"].ToString();
        //            list.Add(trainno);
        //        }
        //    }
        //    return list;
        //}

        #endregion


        #region 根据车次获取客货类型---业务逻辑处理

        public string getKeHuoByTrainNo(string strTrainNo, ref string Remarks)
        {
            if (string.IsNullOrEmpty(strTrainNo))//如果传过来的车次为空  则为货车
            {
                Remarks = " 车次为空，默认返回货车";
                return "2";
            }
            if (strTrainNo.Contains('调') || strTrainNo.Contains('字') || strTrainNo.Contains("非常") || strTrainNo.Contains('备'))//如果车次中包含这几个字，则为调车
            {
                Remarks = "包含调车的关键字，为调车";
                return "3";
            }
            string oneLetter = "", TwoLetter = "", strRange = "";
            int nRange;
            DataTable dt;

            //判断前两位是否是零
            if (strTrainNo.Length > 2)
            {
                 TwoLetter = strTrainNo.Substring(0, 2);
                 if (TwoLetter == "00")
                 {
                     Remarks = "回送客车底列车";
                     return "1";
                 }
            }

            //判断是否直接是数字
            if (isNumberic(strTrainNo.ToString(), out nRange))
            {
                dt = dtGetKeHuo("EMPTY", nRange);
                if (dt.Rows.Count > 0)
                {
                    Remarks = dt.Rows[0]["strRemarks"].ToString();
                    return dt.Rows[0]["nKehuoID"].ToString();
                }
            }


            //判断第一个字符，判断取值范围
            if (strTrainNo.Length > 1)
            {
                oneLetter = strTrainNo.Substring(0, 1);
                strRange = strTrainNo.Substring(1, strTrainNo.Length - 1);
                if (isNumberic(strRange.ToString(), out nRange))
                {
                    dt = dtGetKeHuo(oneLetter, nRange);
                    if (dt.Rows.Count > 0)
                    {
                        Remarks = dt.Rows[0]["strRemarks"].ToString();
                        return dt.Rows[0]["nKehuoID"].ToString();
                    }
                }

            }
            //判断第二个字符，判断取值范围
            if (strTrainNo.Length > 2)
            {
                oneLetter = strTrainNo.Substring(0, 2);
                strRange = strTrainNo.Substring(2, strTrainNo.Length - 2);
                if (isNumberic(strRange.ToString(), out nRange))
                {
                    dt = dtGetKeHuo(oneLetter, nRange);
                    if (dt.Rows.Count > 0)
                    {
                        Remarks = dt.Rows[0]["strRemarks"].ToString();
                        return dt.Rows[0]["nKehuoID"].ToString();
                    }
                }

            }
            Remarks = "未找到与车次匹配的客货类型";
            return "2";
        }


        protected bool isNumberic(string message, out int result)
        {
            System.Text.RegularExpressions.Regex rex =
            new System.Text.RegularExpressions.Regex(@"^\d+$");
            result = -1;
            if (rex.IsMatch(message))
            {
                result = int.Parse(message);
                return true;
            }
            else
                return false;
        }


        public DataTable dtGetKeHuo(string Letter, int nRange)
        {
            string strSql = "SELECT  top 1 *   FROM Tab_System_KeHuoByTrainNo where strInitialIetter='" + Letter + "' and nRangeBegin<= " + nRange + " and nRangeEnd>=" + nRange + "";
            DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            return table;
        }



        #endregion

        #region 根据车次获取客货类型 批量处理

        public class CheCiList_Out : JsonOutBase
        {
            public List<CheCiKehuos> data = new List<CheCiKehuos>();
        }

        public class get_In
        {
            public string BeginTime;
            public string EndTime;
        }


        public class CheCiKehuos
        {
            public string strTrainNo;
            public string KeHuo;
            public string strmark;
        
        }
        public CheCiList_Out GetKehuoByCheCiPL(string input)
        {
            CheCiList_Out co = new CheCiList_Out();
            get_In model = JsonConvert.DeserializeObject<get_In>(input);
            string strWhere = "";
            if (model.BeginTime != ""&&model.BeginTime!=null)
                strWhere += " and dtCreateTime>='" + model.BeginTime + "'";

            if (model.EndTime != "" && model.EndTime != null)
                strWhere += " and dtCreateTime<='" + model.EndTime + "'";


            string strSql = "SELECT  strTrainPlanGUID,strTrainNo  from  TAB_Plan_Train where 1=1 " + strWhere + "";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            List<CheCiKehuos> lcc = new List<CheCiKehuos>();
            int temp = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                temp++;
                string strmark = "";
                string strData = "";
                string strKH = getKeHuoByTrainNo(dt.Rows[i]["strTrainNo"].ToString(), ref strmark);
                CheCiKehuos cc = new CheCiKehuos();
                strSql = "update TAB_Plan_Train set nKehuoID=" + strKH + "    where strTrainPlanGUID='" + dt.Rows[i]["strTrainPlanGUID"].ToString() + "'";
                try
                {
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql);
                }
                catch (Exception ex)
                {
                    strData = ex.Message.ToString();
                    cc.KeHuo = strKH;
                    cc.strmark = strData;
                    cc.strTrainNo = dt.Rows[i]["strTrainNo"].ToString();
                    lcc.Add(cc);
                }
            }
            co.data = lcc;
            co.result = "0";
            co.resultStr = "成功修改" + temp.ToString() + "条记录";
            return co;
        }



        #endregion




        #region 根据交路GUID获取交路

        public class TrainJiaolu_In
        {
            public string strTrainJiaoluGUID = String.Empty;
        }

        public class TrainJiaolu_Out : JsonOutBase
        {
            public List<RRsTrainJiaolu> data;
        }

        public TrainJiaolu_Out GetTrainJiaolu(string input)
        {
            TrainJiaolu_Out json = new TrainJiaolu_Out();
            TrainJiaolu_In model = null;
            RRsTrainJiaolu jiaolu = null;
            try
            {
                model = JsonConvert.DeserializeObject<TrainJiaolu_In>(input);
                string strSql = "select * from VIEW_Base_TrainJiaolu where  strTrainJiaoluGUID=@strTrainJiaoluGUID ";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strTrainJiaoluGUID",model.strTrainJiaoluGUID), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                json.data = new List<RRsTrainJiaolu>();
                foreach (DataRow dataRow in table.Rows)
                {
                    jiaolu = new RRsTrainJiaolu();
                    jiaolu.strEndStation = dataRow["strEndStation"].ToString();
                    jiaolu.strEndStationName = dataRow["strEndStationName"].ToString();
                    jiaolu.strStartStation = dataRow["strStartStation"].ToString();
                    jiaolu.strStartStationName = dataRow["strStartStationName"].ToString();
                    jiaolu.strTrainJiaoluGUID = dataRow["strTrainJiaoluGUID"].ToString();
                    jiaolu.strTrainJiaoluName = dataRow["strTrainJiaoluName"].ToString();
                    jiaolu.strWorkShopGUID = dataRow["strWorkShopGUID"].ToString();
                    jiaolu.bIsBeginWorkFP = Convert.ToInt32(dataRow["bIsBeginWorkFP"].ToString());
                    json.data.Add(jiaolu);
                }
                json.result = "0";
                json.resultStr = "获取交路成功";
            }
            catch (Exception ex)
            {
                LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 根据Number获取乘务员信息

        public class RRsTrainman
        {
            public string strTrainmanGUID = String.Empty;
            public string strTrainmanNumber = String.Empty;
            public string strTrainmanName = String.Empty;
            public int nPostID;
            public string strWorkShopGUID = String.Empty;
            public string strWorkShopName = String.Empty;
            public string strGuideGroupGUID = String.Empty;
            public string strGuideGroupName = String.Empty;
            public string strTelNumber = String.Empty;
            public string strMobileNumber = String.Empty;
            public string strAdddress = String.Empty;
            public int nDriverType;
            public int bIsKey;
            public string dtRuZhiTime = String.Empty;
            public string dtJiuZhiTime = String.Empty;
            public int nDriverLevel;
            public string strABCD = String.Empty;
            public int nKeHuoID;
            public string strRemark = String.Empty;
            public string strTrainJiaoluGUID = String.Empty;
            public string strTrainmanJiaoluGUID = String.Empty;
            public string strTrainJiaoluName = String.Empty;
            public string dtLastEndworkTime = String.Empty;
            public string dtCreateTime = String.Empty;
            public int nTrainmanState;
            public int nID;
            public string FingerPrint1;
            public string FingerPrint2;
            public string Picture;
        }

        public class Trainman_In
        {
            public string TrainmanNumber = string.Empty;
        }

        public class Trainman_Out : JsonOutBase
        {
            public RRsTrainman data;
        }

        public Trainman_Out GetTrainmanByNumber(string input)
        {
            Trainman_In model = null;
            Trainman_Out json = new Trainman_Out();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Trainman_In>(input);
                string strSql = "Select Top 1 * From VIEW_Org_Trainman Where strTrainmanNumber=@strTrainmanNumber";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strTrainmanNumber",model.TrainmanNumber), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    json.data = GetTrainmanInfo(table.Rows[0]);
                    json.result = "0";
                    json.resultStr = "获取乘务员信息成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "获取乘务员信息失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }

        public RRsTrainman GetTrainmanInfo(DataRow row)
        {
            RRsTrainman man = new RRsTrainman();
            man.strTrainmanGUID = row["strTrainmanGUID"].ToString();
            man.strTrainmanNumber = row["strTrainmanNumber"].ToString();
            man.strTrainmanName = row["strTrainmanName"].ToString();
            int nPostID;
            if (row["nPostID"] != DBNull.Value && int.TryParse(row["nPostID"].ToString(), out nPostID))
            {
                man.nPostID = nPostID;
            }

            man.strWorkShopGUID = row["strWorkShopGUID"].ToString();
            man.strWorkShopName = row["strWorkShopName"].ToString();
            man.strGuideGroupGUID = row["strGuideGroupGUID"].ToString();
            man.strGuideGroupName = row["strGuideGroupName"].ToString();
            man.strTelNumber = row["strTelNumber"].ToString();
            man.strMobileNumber = row["strMobileNumber"].ToString();
            man.strAdddress = row["strAddress"].ToString();
            int nDriverType, bIsKey, nKeHuoID, nID, nTrainmanState, nDriverLevel;
            if (row["nDriverType"] != DBNull.Value && int.TryParse(row["nDriverType"].ToString(), out nDriverType))
            {
                man.nDriverType = nDriverType;
            }
            if (row["nKeHuoID"] != DBNull.Value && int.TryParse(row["nKeHuoID"].ToString(), out nKeHuoID))
            {
                man.nKeHuoID = nKeHuoID;
            }
            else
            {
                man.nKeHuoID = (int)TRsKehuo.khNone;
            }
            if (row["bIsKey"] != DBNull.Value && int.TryParse(row["bIsKey"].ToString(), out bIsKey))
            {
                man.bIsKey = bIsKey;
            }
            if (row["nID"] != DBNull.Value && int.TryParse(row["nID"].ToString(), out nID))
            {
                man.nID = nID;
            }
            if (row["nTrainmanState"] != DBNull.Value &&
                int.TryParse(row["nTrainmanState"].ToString(), out nTrainmanState))
            {
                man.nTrainmanState = nTrainmanState;
            }
            if (row["nDriverLevel"] != DBNull.Value && int.TryParse(row["nDriverLevel"].ToString(), out nDriverLevel))
            {
                man.nDriverLevel = nDriverLevel;
            }
            man.dtRuZhiTime = GetFormatedDateString(row, "dtRuZhiTime");
            man.dtJiuZhiTime = GetFormatedDateString(row, "dtJiuZhiTime");
            man.dtLastEndworkTime = GetFormatedDateString(row, "dtLastEndworkTime");
            man.dtCreateTime = GetFormatedDateString(row, "dtLastEndworkTime");
            man.strABCD = row["strABCD"].ToString();
            man.strRemark = row["strRemark"].ToString();
            man.strRemark = row["strRemark"].ToString();
            man.strTrainJiaoluGUID = row["strTrainJiaoluGUID"].ToString();
            man.strTrainmanJiaoluGUID = row["strTrainmanJiaoluGUID"].ToString();
            man.strTrainJiaoluName = row["strTrainJiaoluName"].ToString();
            man.FingerPrint1 = GetBase64FormatedImage(row, "FingerPrint1");
            man.FingerPrint2 = GetBase64FormatedImage(row, "FingerPrint2");
            man.Picture = GetBase64FormatedImage(row, "Picture");
            return man;
        }

        public string GetFormatedDateString(DataRow row, string column)
        {
            DateTime dateTime;
            string result = "";
            if (row[column] != DBNull.Value && DateTime.TryParse(row[column].ToString(), out dateTime))
            {
                result = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return result;
        }

        public string GetBase64FormatedImage(DataRow row, string column)
        {
            string result = string.Empty;
            if (row[column] != DBNull.Value)
            {
                byte[] bytes = (byte[])row[column];
                result = Convert.ToBase64String(bytes);
            }
            return result;
        }
        #endregion

        #region 根据GUID获取乘务员信息
        public class GUID_In
        {
            public string TrainmanGUID = string.Empty;
        }

        public class GUID_Out : JsonOutBase
        {
            public RRsTrainman data;
        }

        public GUID_Out GetTrainmanByGUID(string input)
        {
            GUID_In model = null;
            GUID_Out json = new GUID_Out();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<GUID_In>(input);
                string strSql = "Select Top 1 * From VIEW_Org_Trainman Where strTrainmanGUID=@strTrainmanGUID";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strTrainmanGUID",model.TrainmanGUID), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    json.data = GetTrainmanInfo(table.Rows[0]);
                    json.result = "0";
                    json.resultStr = "获取乘务员信息成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "获取乘务员信息失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }

        #endregion

        #region 获取机务段信息

        public InterfaceOutPut GetJwdInfo(string input)
        {
            InterfaceOutPut json = new InterfaceOutPut();
            try
            {
                string strSql = "select * from TAB_Base_Jwd_Address";
                json.data = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                json.result = 0;
                json.resultStr = "获取机务段信息成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 清除乘务员的指纹信息
        /// <summary>
        /// 清除乘务员的指纹信息
        /// <summary>
        public InterfaceOutPut ClearFingerPrint(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                CommonJsonModel cjm = new CommonJsonModel(Data);
                string strTrainmanGUID = ObjectConvertClass.static_ext_string(cjm.GetValue("strTrainmanGUID"));
                string strSql = @"update TAB_Org_Trainman set Fingerprint1=null,Fingerprint2=null 
     Where strTrainmanGUID =@strTrainmanGUID";
                SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("strTrainmanGUID", strTrainmanGUID), };
                int count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
                if (count > 0)
                {
                    output.result = 0;
                    output.resultStr = "指纹信息清除成功";
                }
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.ClearFingerPrint:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion
    }
}
