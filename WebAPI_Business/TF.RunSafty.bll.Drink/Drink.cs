using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Server;
using TF.RunSafty.Model.InterfaceModel;
using ThinkFreely.DBUtility;

namespace TF.RunSafty.Drink
{
    public class Drink
    {
        #region 获取测酒信息列表

        public class Query_In
        {
            public string dtBeginTime = string.Empty;
            public string dtEndTime = string.Empty;
            public string WorkShopGUID = string.Empty;
            public string TrainmanName = string.Empty;
            public string TrainmanNumber = string.Empty;
            public int VerifyID=-1, DrinkResultID=-1;
            public int DrinkTypeID = -1;
        
        }

        public class Query_Out : JsonOutBase
        {
            public List<RRsDrink> data;
        }

        public string GetWhere(Query_In model)
        {
            StringBuilder builder=new StringBuilder();
            if (!string.IsNullOrEmpty(model.WorkShopGUID))
            {
                builder.AppendFormat(" and strWorkShopGUID =@strWorkShopGUID");
            }
            if (!string.IsNullOrEmpty(model.TrainmanNumber))
            {
                builder.AppendFormat(" and strTrainmanNumber =@strTrainmanNumber");
            }
            if (model.VerifyID>-1)
            {
                builder.AppendFormat(" and nVerifyID =@nVerifyID");
            }
            if (model.DrinkTypeID>-1)
            {
                builder.AppendFormat(" and nWorkTypeID =@nWorkTypeID");
            }
            if (model.DrinkResultID>-1)
            {
                builder.AppendFormat(" and nDrinkResult =@nDrinkResult");
            }
            if (!string.IsNullOrEmpty(model.TrainmanName))
            {
                builder.AppendFormat(" and strTrainmanName like @strTrainmanName");
            }
             
            return builder.ToString();
        }
        public Query_Out QueryDrink(string input)
        {
            Query_Out json=new Query_Out();
            json.data=new List<RRsDrink>();
            RRsDrink drink = null;
            Query_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Query_In>(input);
                string strSql = "select * from VIEW_Drink_Query where dtCreateTime >= @dtBeginTime and dtCreateTime <=@dtEndTime  ";
                strSql += GetWhere(model);
                strSql += " order by dtCreateTime desc";
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("dtBeginTime",model.dtBeginTime), 
                    new SqlParameter("dtEndTime",model.dtEndTime), 
                    new SqlParameter("strWorkShopGUID",model.WorkShopGUID), 
                    new SqlParameter("strTrainmanNumber",model.TrainmanNumber), 
                    new SqlParameter("nVerifyID",model.VerifyID), 
                    new SqlParameter("nWorkTypeID",model.DrinkTypeID), 
                    new SqlParameter("nDrinkResult",model.DrinkResultID), 
                    new SqlParameter("strTrainmanName",model.TrainmanName), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                DateTime dtCreateTime;
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        drink = GetSingleDrinkInfo(row);
                        json.data.Add(drink);
                    }
                    json.result = "0";
                    json.resultStr = "获取测酒记录成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "获取测酒记录失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }

        public RRsDrink GetSingleDrinkInfo(DataRow row)
        {
            RRsDrink drink = new RRsDrink();
            DateTime dtCreateTime;
            drink.strGUID = row["strGUID"].ToString();
            drink.strTrainmanName = row["strTrainmanName"].ToString();
            drink.strTrainmanNumber = row["strTrainmanNumber"].ToString();
            drink.strWorkShopName = row["strWorkShopName"].ToString();
            drink.strDrinkResultName = row["strDrinkResultName"].ToString();
            drink.strVerifyName = row["strVerifyName"].ToString();
            drink.strWorkTypeName = row["strWorkTypeName"].ToString();
            drink.nDrinkResult = Convert.ToInt32(row["nDrinkResult"]);
            drink.nVerifyID = Convert.ToInt32(row["nVerifyID"]);
            drink.nWorkTypeID = Convert.ToInt32(row["nWorkTypeID"]);
            if (DBNull.Value != row["dtCreateTime"] &&
                DateTime.TryParse(row["dtCreateTime"].ToString(), out dtCreateTime))
            {
                drink.dtCreateTime = dtCreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            drink.strPictureURL = row["strImagePath"].ToString();
            return drink;
        }
        #endregion
        #region 查询单次测酒信息

        public class DrinkInfo_In
        {
            public string strGUID = String.Empty;
        }

        public class DrinkInfo_Out : JsonOutBase
        {
            public RRsDrink data;
        }

        public DrinkInfo_Out GetDrinkInfo(string input)
        {
            DrinkInfo_Out json=new DrinkInfo_Out();
            RRsDrink drink = null;
            DrinkInfo_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<DrinkInfo_In>(input);
                string strSql = "select * From VIEW_Drink_Query where strGUID=@strGUID";
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("strGUID",model.strGUID), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    drink = GetSingleDrinkInfo(table.Rows[0]);
                    json.data = drink;
                    json.result = "0";
                    json.resultStr = "获取测酒信息成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "获取测酒信息失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 更新测酒时间

        public class UpdateTime_In
        {
            public string strGUID = string.Empty;
            public string DrinkTime = string.Empty;
        }

        public class UpdateTime_Out:JsonOutBase
        { 
        }

        public UpdateTime_Out UpdateDrinkTime(string input)
        {
            UpdateTime_Out json=new UpdateTime_Out();
            UpdateTime_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateTime_In>(input);
                string strSql = "update  TAB_Drink_Information set dtCreateTime =@dtCreateTime   where strGUID=@strGUID";
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("dtCreateTime",model.DrinkTime), 
                    new SqlParameter("strGUID",model.strGUID), 
                };
                int count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters);
                if (count > 0)
                {
                    json.result = "0";
                    json.resultStr = "更新测酒时间成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "更新测酒时间失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }
        #endregion

        #region 插入外段测酒记录

        public class Insert_In
        {
            public string TrainmanNumber = string.Empty;
            public RRsDrink drink;
            public string DutyUserGUID = string.Empty;
            public int nVerifyID;
        }
        public class Insert_Out : JsonOutBase
        {
            
        }

        public Insert_Out AddForeignDrink(string input)
        {
            Insert_Out json=new Insert_Out();
            Insert_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Insert_In>(input);
                bool result = InsertDrink(model.TrainmanNumber, model.drink, model.DutyUserGUID, model.nVerifyID);
                if (result)
                {
                    json.result = "0";
                    json.resultStr = "插入测酒记录成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "插入测酒记录失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }

        public bool InsertDrink(string TrainmanNumber, RRsDrink drink, string DutyUserGUID,int nVerifyID)
        {
            bool result = false;
            string strSql = @"insert into TAB_Drink_Information (strGUID,strTrainmanGUID,   nDrinkResult,dtCreateTime,
strAreaGUID,strDutyGUID,nVerifyID,strWorkID,nWorkTypeID,strImagePath)  
values (@strGUID,@strTrainmanGUID,@nDrinkResult,@dtCreateTime,
         @strAreaGUID,@strDutyGUID,@nVerifyID,@strWorkID,@nWorkTypeID,@strImagePath)";
            SqlParameter[] sqlParameters=new SqlParameter[]
            {
                new SqlParameter("strGUID",Guid.NewGuid().ToString()), 
                new SqlParameter("strTrainmanGUID",TrainmanNumber), 
                new SqlParameter("nDrinkResult",drink.nDrinkResult), 
                new SqlParameter("strImagePath",drink.strPictureURL),
                new SqlParameter("dtCreateTime",drink.dtCreateTime), 
                new SqlParameter("strAreaGUID",""), 
                new SqlParameter("strDutyGUID", DutyUserGUID), 
                new SqlParameter("nVerifyID",nVerifyID), 
                new SqlParameter("strWorkID",""), 
                new SqlParameter("nWorkTypeID",drink.nWorkTypeID),  
            };
            result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters) > 0;
            return result;
        }
        #endregion

        #region 获取乘务员的测酒信息

        public class TrainmanDrink_In
        {
            public string strTrainmanGUID = "";
            public string strTrainPlanGUID = "";
            public TRsWorkTypeID WorkType; 
        }

        public class TrainmanDrink_Out:JsonOutBase
        {
            public RRsDrink data;
        }

        public TrainmanDrink_Out GetTrainmanDrinkInfo(string input)
        {
            TrainmanDrink_Out json=new TrainmanDrink_Out();
            TrainmanDrink_In model = null;
            RRsDrink drink = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<TrainmanDrink_In>(input);
                string strSql = "";
                switch (model.WorkType)
                {
                    case TRsWorkTypeID.wtBeginWork:
                        strSql = @"select * from TAB_Drink_Information where strWorkID = ( 
            select top 1 strBeginWorkGUID from TAB_Plan_BeginWork where 
            strTrainPlanGUID =@strTrainPlanGUID and strTrainmanGUID =@strTrainmanGUID) ORDER BY dtCreateTime DESC";
                        break;
                    case TRsWorkTypeID.wtEndWork:
                        strSql = @"select * from TAB_Drink_Information where strWorkID = ( 
            select top 1 strEndWorkGUID from TAB_Plan_EndWork where 
            strTrainPlanGUID = @strTrainPlanGUID and strTrainmanGUID = @strTrainmanGUID) ORDER BY dtCreateTime DESC";
                        break;
                }
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("strTrainPlanGUID",model.strTrainPlanGUID), 
                    new SqlParameter("strTrainmanGUID",model.strTrainmanGUID), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    json.result = "0";
                    drink=new RRsDrink();
                    drink.nDrinkResult = Convert.ToInt32(table.Rows[0]["nDrinkResult"]);
                    drink.dtCreateTime = Convert.ToDateTime(table.Rows[0]["dtCreateTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    drink.strPictureURL = table.Rows[0]["strImagePath"].ToString();
                    json.resultStr = "获取测酒信息成功";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "获取测酒信息失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }

        #endregion

        #region 获取包括测酒信息在内的行车计划

        public class FromDrink_In
        {
            public string TrainmanGUID = string.Empty;
            public string SiteGUID = string.Empty;
            public DateTime DrinkTime;
        }

        public class FromDrink_Out : JsonOutBase
        {
            public object data;
        }

        public FromDrink_Out GetTrainmanChuQinPlanFromDrink(string input)
        {
            FromDrink_Out json=new FromDrink_Out();
            FromDrink_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<FromDrink_In>(input);
                string strSql =string.Format(@"select top 1 * from VIEW_Plan_BeginWork 
     where strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID = '{0}') and 
     (nPlanState in ({1},{2})) and (strTrainmanGUID1 = '{3}' or strTrainmanGUID2 = '{3}' or strTrainmanGUID3 = '{3}' or strTrainmanGUID4 = '{3}')",model.SiteGUID,(int)TRsPlanState.psPublish,(int)TRsPlanState.psBeginWork,model.TrainmanGUID);
                DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                if (table.Rows.Count > 0)
                {
                    DateTime ChuQinTime = Convert.ToDateTime(table.Rows[0]["dtStartTime"]);
                    TimeSpan span = ChuQinTime - model.DrinkTime;
                    if (span.Days >= 1 || span.Hours >= 1 || span.Minutes > 30)
                    {
                        json.result = "1";
                        json.resultStr = "获取出勤计划失败，晚出勤超过30分钟";
                    }
                    else
                    {
                        DataRow row = table.Rows[0];
                        TF.CommonUtility.JsonConvert.FormatDataRow(table, row);
                        json.data =table;
                    }
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "获取出勤计划失败，没有找到出勤计划";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }

       
        #endregion

        #region 获取包括测酒信息的退勤计划

        public class EndWorkDrink_In
        {
            public string TrainmanGUID = string.Empty;
            public string SiteGUID = string.Empty;
            public DateTime DrinkTime;
        }

        public class EndWorkDrink_Out:JsonOutBase
        {
            public object data;
        }

        public EndWorkDrink_Out GetTrainmanTuiQinPlanFromDrink(string input)
        {
            EndWorkDrink_Out json=new EndWorkDrink_Out();
            EndWorkDrink_In model = null;
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<EndWorkDrink_In>(input);
                string strSql = @"select top 1 * from VIEW_Plan_EndWork  
where strTrainJiaoluGUID in (select strTrainJiaoluGUID from TAB_Base_TrainJiaoluInSite where strSiteGUID =@strSiteGUID) and 
     nPlanState =@nPlanState AND (strTrainmanGUID1 = @strTrainmanGUID or strTrainmanGUID2 =@strTrainmanGUID or strTrainmanGUID3 =@strTrainmanGUID or strTrainmanGUID4 = @strTrainmanGUID) order by dtStartTime desc";
                SqlParameter[] sqlParameters=new SqlParameter[]
                {
                    new SqlParameter("strSiteGUID",model.SiteGUID), 
                    new SqlParameter("nPlanState",(int)TRsPlanState.psBeginWork), 
                    new SqlParameter("strTrainmanGUID",model.TrainmanGUID), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    DateTime dtStartTime = Convert.ToDateTime(row["dtStartTime"]);
                    TimeSpan span = model.DrinkTime - dtStartTime;
                    //测酒时间查过计划出勤时间3天
                    if (span.Days >= 3 || span.Ticks<0)
                    {
                        json.result = "1";
                        json.resultStr = "获取出勤计划失败";
                    }
                    else
                    {
                        TF.CommonUtility.JsonConvert.FormatDataRow(table, row);
                        json.data = table;
                        json.result = "0";
                        json.resultStr = "获取退勤计划成功";
                    }
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "获取退勤计划失败";
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex,"");
                throw ex;
            }
            return json;
        }
        #endregion
    }
}
