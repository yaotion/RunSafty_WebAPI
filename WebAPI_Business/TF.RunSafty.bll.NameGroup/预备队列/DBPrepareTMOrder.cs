using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.NamePlate
{
    public class DBPrepareTMOrder
    {
        /// <summary>
        /// 获取预备人员的排序信息
        /// </summary>
        public static List<PrepareTMOrder> GetPrepareOrders(string TrainmanjiaoluGUID)
        {
            string strSql =
                   "select * from VIEW_Nameplate_TrainmanJiaolu_PrepareOrder where TrainmanjiaoluGUID = @TrainmanJiaoluGUID  order by TrainmanOrder";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("TrainmanJiaoluGUID",TrainmanjiaoluGUID),
            };
            List<PrepareTMOrder> result = new List<PrepareTMOrder>();

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PrepareTMOrder tmOrder = new PrepareTMOrder();
                tmOrder.TrainmanJiaoluGUID = TF.Utils.TFConvert.DBToString(dt.Rows[i]["TrainmanJiaoluGUID"], "");
                tmOrder.TrainmanJiaoluName = TF.Utils.TFConvert.DBToString(dt.Rows[i]["TrainmanJiaoluName"], "");
                tmOrder.TrainmanNumber = TF.Utils.TFConvert.DBToString(dt.Rows[i]["TrainmanNumber"], "");
                tmOrder.TrainmanOrder = TF.Utils.TFConvert.DBToInt(dt.Rows[i]["TrainmanOrder"], 0);
                tmOrder.PostID = TF.Utils.TFConvert.DBToInt(dt.Rows[i]["PostID"], 0);
                tmOrder.TrainmanName = TF.Utils.TFConvert.DBToString(dt.Rows[i]["TrainmanName"], "");

                tmOrder.nTrainmanState = TF.Utils.TFConvert.DBToInt(dt.Rows[i]["nTrainmanState"], 1);
                tmOrder.dtLastEndWorkTime = TF.Utils.TFConvert.DBToDateTimeD(dt.Rows[i]["dtLastEndWorkTime"]);

                tmOrder.strTelNumber = TF.Utils.TFConvert.DBToString(dt.Rows[i]["strTelNumber"], "");
                tmOrder.nPostID = TF.Utils.TFConvert.DBToInt(dt.Rows[i]["nPostID"], 1);
                tmOrder.nid = TF.Utils.TFConvert.DBToInt(dt.Rows[i]["nid"], 0);
                result.Add(tmOrder);
            }

            return result;
        }

        public static List<PrepareTMOrder> GetCJPrepareOrders(string WorkShopGUID)
        {
            string strSql = @"select * from VIEW_Nameplate_TrainmanJiaolu_PrepareOrder where 
            TrainmanjiaoluGUID in (select strTrainmanJiaoluGUID from VIEW_Base_JiaoluRelation where strWorkShopGUID=@strWorkShopGUID and nJiaoluType=3)   
            order by TrainmanjiaoluGUID,PostID,TrainmanOrder";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("strWorkShopGUID",WorkShopGUID),
            };
            List<PrepareTMOrder> result = new List<PrepareTMOrder>();

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PrepareTMOrder tmOrder = new PrepareTMOrder();
                tmOrder.TrainmanJiaoluGUID = TF.Utils.TFConvert.DBToString(dt.Rows[i]["TrainmanJiaoluGUID"], "");
                tmOrder.TrainmanJiaoluName = TF.Utils.TFConvert.DBToString(dt.Rows[i]["TrainmanJiaoluName"], "");
                tmOrder.TrainmanNumber = TF.Utils.TFConvert.DBToString(dt.Rows[i]["TrainmanNumber"], "");
                tmOrder.TrainmanOrder = TF.Utils.TFConvert.DBToInt(dt.Rows[i]["TrainmanOrder"], 0);
                tmOrder.PostID = TF.Utils.TFConvert.DBToInt(dt.Rows[i]["PostID"], 0);
                tmOrder.TrainmanName = TF.Utils.TFConvert.DBToString(dt.Rows[i]["TrainmanName"], "");

                tmOrder.nTrainmanState = TF.Utils.TFConvert.DBToInt(dt.Rows[i]["nTrainmanState"], 1);
                tmOrder.dtLastEndWorkTime = TF.Utils.TFConvert.DBToDateTimeD(dt.Rows[i]["dtLastEndWorkTime"]);

                tmOrder.strTelNumber = TF.Utils.TFConvert.DBToString(dt.Rows[i]["strTelNumber"], "");
                tmOrder.nPostID = TF.Utils.TFConvert.DBToInt(dt.Rows[i]["nPostID"], 1);
                tmOrder.nid = TF.Utils.TFConvert.DBToInt(dt.Rows[i]["nid"], 0);
                result.Add(tmOrder);
            }

            return result;
        }
        public static List<PrepareTMOrder> GetTrainmanSubOrders(string TrainmanJiaoluGUID, string TrainmanNumber, int PostID, int TMOrder, bool HasSelf)
        {
            List<PrepareTMOrder> result = new List<PrepareTMOrder>();
            string strSql = @"select * from Tab_Nameplate_TrainmanJiaolu_PrepareOrder where TrainmanjiaoluGUID = @TrainmanJiaoluGUID 
            and PostID=@PostID and TrainmanOrder >= @TrainmanOrder  order by TrainmanOrder";
            if (!HasSelf)
            {
                strSql = @"select * from Tab_Nameplate_TrainmanJiaolu_PrepareOrder where TrainmanjiaoluGUID = @TrainmanJiaoluGUID 
            and PostID=@PostID and TrainmanOrder > @TrainmanOrder  order by TrainmanOrder";
            }
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("TrainmanJiaoluGUID",TrainmanJiaoluGUID),
                new SqlParameter("TrainmanOrder",TMOrder),
                new SqlParameter("PostID",PostID)                
    
            };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PrepareTMOrder tmOrder = new PrepareTMOrder();
                tmOrder.TrainmanJiaoluGUID = TF.Utils.TFConvert.DBToString(dt.Rows[i]["TrainmanJiaoluGUID"], "");
                tmOrder.TrainmanJiaoluName = TF.Utils.TFConvert.DBToString(dt.Rows[i]["TrainmanJiaoluName"], "");
                tmOrder.TrainmanNumber = TF.Utils.TFConvert.DBToString(dt.Rows[i]["TrainmanNumber"], "");
                tmOrder.TrainmanOrder = TF.Utils.TFConvert.DBToInt(dt.Rows[i]["TrainmanOrder"], 0);
                tmOrder.PostID = TF.Utils.TFConvert.DBToInt(dt.Rows[i]["PostID"], 0);
                tmOrder.nid = TF.Utils.TFConvert.DBToInt(dt.Rows[i]["nid"], 0);
                result.Add(tmOrder);
            }

            return result;
        }
        public static bool GetTrainmanOrder(string TrainmanNumber, PrepareTMOrder TMOrder)
        {
            string strSql =
                              "select * from Tab_Nameplate_TrainmanJiaolu_PrepareOrder where TrainmanNumber=@TrainmanNumber";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("TrainmanNumber",TrainmanNumber)

            };

            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                TMOrder.TrainmanJiaoluGUID = TF.Utils.TFConvert.DBToString(dt.Rows[0]["TrainmanJiaoluGUID"], "");
                TMOrder.TrainmanJiaoluName = TF.Utils.TFConvert.DBToString(dt.Rows[0]["TrainmanJiaoluName"], "");
                TMOrder.TrainmanNumber = TF.Utils.TFConvert.DBToString(dt.Rows[0]["TrainmanNumber"], "");
                TMOrder.TrainmanOrder = TF.Utils.TFConvert.DBToInt(dt.Rows[0]["TrainmanOrder"], 0);
                TMOrder.PostID = TF.Utils.TFConvert.DBToInt(dt.Rows[0]["PostID"], 0);
                return true;
            }
            return false;
        }

        public static int GetMaxTMOrder(string TrainmanJiaoluGUID, int PostID)
        {
            string strSql = "select max(TrainmanOrder) from Tab_Nameplate_TrainmanJiaolu_PrepareOrder where TrainmanJiaoluGUID=@TrainmanJiaoluGUID and PostID=@PostID";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("TrainmanJiaoluGUID",TrainmanJiaoluGUID),
                new SqlParameter("PostID",PostID)
            };
            object objOrder = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
            if ((objOrder == null) || (DBNull.Value.Equals(objOrder)))
            {
                return 0;
            }
            return Convert.ToInt32(objOrder);
        }
        
        /// <summary>
        /// 添加预备人员编号
        /// </summary>
        /// <param name="TMOrder"></param>
        public static void AddTrainmanOrder(PrepareTMOrder TMOrder)
        {
            string strSql =
                  @"insert into Tab_Nameplate_TrainmanJiaolu_PrepareOrder (TrainmanJiaoluGUID,TrainmanJiaoluName,PostID,TrainmanNumber,TrainmanName,TrainmanOrder) 
                    values (@TrainmanJiaoluGUID,@TrainmanJiaoluName,@PostID,@TrainmanNumber,@TrainmanName,@TrainmanOrder) ";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("TrainmanJiaoluGUID",TMOrder.TrainmanJiaoluGUID),
                new SqlParameter("TrainmanJiaoluName",TMOrder.TrainmanJiaoluName),
                new SqlParameter("PostID",TMOrder.PostID),
                new SqlParameter("TrainmanNumber",TMOrder.TrainmanNumber),
                new SqlParameter("TrainmanName",TMOrder.TrainmanName),                
                new SqlParameter("TrainmanOrder",TMOrder.TrainmanOrder)
            };
            SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        }
        /// <summary>
        /// 修改预备人员序号
        /// </summary>
        /// <param name="TMOrder"></param>
        public static void UpdateTrainmanOrder(PrepareTMOrder TMOrder)
        {
            string strSql = @"update Tab_Nameplate_TrainmanJiaolu_PrepareOrder set TrainmanNumber=@TrainmanNumber,TrainmanName=@TrainmanName,TrainmanJiaoluName = @TrainmanJiaoluName
                where  TrainmanJiaoluGUID=@TrainmanJiaoluGUID and PostID=@PostID and TrainmanOrder = @TrainmanOrder ";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("TrainmanJiaoluGUID",TMOrder.TrainmanJiaoluGUID),
                new SqlParameter("TrainmanJiaoluName",TMOrder.TrainmanJiaoluName),
                new SqlParameter("PostID",TMOrder.PostID),
                new SqlParameter("TrainmanNumber",TMOrder.TrainmanNumber),
                new SqlParameter("TrainmanName",TMOrder.TrainmanName),
                new SqlParameter("TrainmanOrder",TMOrder.TrainmanOrder)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        }
        public static void ReorderTrainmanOrder(int nid, int NewOrder)
        {
            string strSql = @"update Tab_Nameplate_TrainmanJiaolu_PrepareOrder set TrainmanOrder=@NewOrder
                where  nid = @nid ";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("nid",nid),
                new SqlParameter("NewOrder",NewOrder)
                
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        }
        /// <summary>
        /// 删除预备人员序号
        /// </summary>
        /// <param name="TMOrder"></param>
        public static bool DeleteTrainmanOrder(PrepareTMOrder TMOrder)
        {
            string strSql = @"delete from Tab_Nameplate_TrainmanJiaolu_PrepareOrder  where  TrainmanJiaoluGUID=@TrainmanJiaoluGUID and
                    TrainmanNumber=@TrainmanNumber and PostID=@PostID and TrainmanOrder=@TrainmanOrder ";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("TrainmanJiaoluGUID",TMOrder.TrainmanJiaoluGUID),
                new SqlParameter("PostID",TMOrder.PostID),
                new SqlParameter("TrainmanNumber",TMOrder.TrainmanNumber),
                new SqlParameter("TrainmanOrder",TMOrder.TrainmanOrder)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams) > 0;
        }

        public static bool IsTurnPrepare(string TMJiaoluGUID)
        {
            string strSql = @"select strValue from TAB_System_Config where strSection=@strSection and strIdent=@strIdent";
            SqlParameter[] sqlParamsSysconfig = new SqlParameter[] { 
                    new SqlParameter("strSection","TQTurnPerpare"),
                    new SqlParameter("strIdent",TMJiaoluGUID)
                };
            bool bTurnPrepare = false;
            object objTurn = SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, strSql, sqlParamsSysconfig);
            if ((objTurn != null) && !DBNull.Value.Equals(objTurn))
            {
                bTurnPrepare = Convert.ToString(objTurn) == "1";
            }
            return bTurnPrepare;
        }
        
        public static void AddLog(PrepareTMOrderLog Log)
        {
            string strSql = @"insert Tab_Nameplate_TrainmanJiaolu_PrepareOrderLog  (LogTime,UserNumber,UserName,TMJiaoluGUID,TMJiaoluName,ChangeType,LogText) 
                values (@LogTime,@UserNumber,@UserName,@TMJiaoluGUID,@TMJiaoluName,@ChangeType,@LogText)  ";
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("LogTime",Log.LogTime),
                new SqlParameter("UserNumber",Log.UserNumber),
                new SqlParameter("UserName",Log.UserName),
                new SqlParameter("TMJiaoluGUID",Log.TMJiaoluGUID),
                new SqlParameter("TMJiaoluName",Log.TMJiaoluName),
                new SqlParameter("ChangeType",Log.ChangeType),
                new SqlParameter("LogText",Log.LogText)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams);
        }

        public static List<PrepareTMOrderLog> QueryLog(DateTime BeginTime,DateTime EndTime,string TMJiaoluGUID,string LogText)
        {
            List<PrepareTMOrderLog> result = new List<PrepareTMOrderLog>();

            string strSql = @"select * from Tab_Nameplate_TrainmanJiaolu_PrepareOrderLog  where LogTime >=@BeginTime and LogTime <=@EndTime and TMJiaoluGUID=@TMJiaoluGUID  ";
            if (LogText.Trim().Length > 0)
            {
                strSql += " and LogText like @LogText";
            }
            SqlParameter[] sqlParams = new SqlParameter[] { 
                new SqlParameter("BeginTime",BeginTime),
                new SqlParameter("EndTime",EndTime),
                new SqlParameter("TMJiaoluGUID",TMJiaoluGUID),
                new SqlParameter("LogText","%" + LogText + "%")
            };
            strSql += " order by LogTime ";            
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PrepareTMOrderLog logItem = new PrepareTMOrderLog();
                logItem.LogTime = Convert.ToDateTime(dt.Rows[i]["LogTime"]);
                logItem.TMJiaoluGUID = dt.Rows[i]["TMJiaoluGUID"].ToString();
                logItem.TMJiaoluName = dt.Rows[i]["TMJiaoluName"].ToString();
                logItem.UserNumber = dt.Rows[i]["UserNumber"].ToString();
                logItem.UserName = dt.Rows[i]["UserName"].ToString();
                logItem.LogText = dt.Rows[i]["LogText"].ToString();
                logItem.ChangeType = Convert.ToInt32(dt.Rows[i]["ChangeType"]);
                result.Add(logItem);
            }            
            return result;
        }

   
    }
}
