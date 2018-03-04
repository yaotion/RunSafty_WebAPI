using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using TF.RunSafty.Entry;
using Newtonsoft.Json;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Reflection; 
namespace TF.RunSafty.DBUtils
{

    public class SearchPage
    {
        private static PageBase p = new PageBase();

        #region ===================属性============================
        private int pageIndex = 0;
        private int pageSize = 15;
        private string sort = "asc";
        private string order_By = "";
        private string idColumn = "nid";
        private string selectedColumns = "*";
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        public string Sort
        {
            get { return sort; }
            set { sort = value; }
        }
        public string Order_By
        {
            get { return order_By; }
            set { order_By = value; }
        }
        public string IdColumn
        {
            get { return idColumn; }
            set { idColumn = value; }
        }
        public string SelectedColumns
        {
            get { return selectedColumns; }
            set { selectedColumns = value; }
        }
        #endregion

        #region --------------构建查询语句的Where部分---------------
        /// <summary>
        /// 构建查询语句的Where部分
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string BuildWhereString(IList<TF.RunSafty.Entry.SearchParameter> parameters)
        {
            StringBuilder builder = new StringBuilder();
            string filt = "";
            if (parameters != null)
            {
                foreach (SearchParameter param in parameters)
                {
                    if (param.Filt != null)
                    {
                        filt = Enum.GetName(typeof(FiltOperation), param.Filt);
                    }
                    if (param.SubSearchItems != null && param.SubSearchItems.Count > 0)
                    {
                        builder.AppendFormat(" {0} ({1})", filt, BuildWhereString(param.SubSearchItems));
                    }
                    else
                    {
                        builder.AppendFormat(" {0} ({1} {2} @{3} )", filt, param.ColumnName, param.Operator, param.param.ParameterName);
                    }
                }
            }
           
            return builder.ToString();
        }
        #endregion



        public void GetContextParams(System.Web.HttpContext context)
        { 
            int page = p.ext_int(context.Request["page"]) - 1;
            int rows = p.ext_int(context.Request["rows"]); 
            string sort = p.ext_string(context.Request["sort"]);
            string order = p.ext_string(context.Request["order"]);
            if (string.IsNullOrEmpty(order))
                order = "asc";
            if (string.IsNullOrEmpty(sort))
                sort = "nid";
            this.pageSize = rows;
            this.pageIndex = page;
            this.order_By = sort;
            this.sort = order;
        }

        /// <summary>
        /// 获取查询结果的总数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="where"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int GetRecordCount(string tableName, string where,IList<SearchParameter> parameters)
        {string sqlCommandText="";
            try
            {
                DataSet set = new DataSet();
                 sqlCommandText = string.Format("select  count(*) as recordcount from {0} where 1=1 {1} ", tableName, where);
                SqlCommand command = new SqlCommand(sqlCommandText, new SqlConnection(SqlHelper.ConnString));
                SetSqlCommandParameter(command, parameters);
                command.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(set);
                if (set != null)
                {
                    if (set.Tables.Count > 0)
                    {
                        return Convert.ToInt32(set.Tables[0].Rows[0]["recordcount"]);
                    }
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                TF.CommonUtility.LogClass.log(sqlCommandText);
            }
            return 0;
        }


        /// <summary>
        /// 设置查询的参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameters"></param>
        public static void SetSqlCommandParameter(SqlCommand cmd, IList<SearchParameter> parameters)
        {
            if (parameters != null)
            {
                foreach (SearchParameter parameter in parameters)
                {
                    if (parameter.SubSearchItems != null && parameters.Count > 0)
                    {
                        SetSqlCommandParameter(cmd, parameter.SubSearchItems);
                    }
                    else
                    {
                        if (parameter.Operator.ToLower().Equals("like"))
                        {
                            cmd.Parameters.AddWithValue("@" + parameter.param.ParameterName, "%" + parameter.param.Value.ToString() + "%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@" + parameter.param.ParameterName, parameter.param.Value);
                        }
                    }
                }
            }
        }



        #region ==========================分页查询================================

        public string GetSearchData(string tableName, IList<TF.RunSafty.Entry.SearchParameter> parameters)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
            int recordCount = 0;
            string where = "";
            DataSet set = new DataSet();
            string sqlCommandText = "";

            where = BuildWhereString(parameters);
            recordCount = GetRecordCount(tableName, where, parameters);
            sqlCommandText = GetCommandText(tableName, where);
            jsonModel.total = recordCount;
            
                DataTable table = GetQueryDataTable(sqlCommandText, parameters);
                jsonModel.rows = table;
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
            }
            return GetFormatedJsonString(jsonModel);
        }


        public int GetRecordCount(string sqlCommand, IList<SqlParameter> parameters)
        {
            int recordCount = 0;
            SqlConnection connection=new SqlConnection(SqlHelper.ConnString);
            try
            {
                return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sqlCommand, parameters.ToArray<SqlParameter>());
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
            }
            return recordCount;
        }
        public string GetDataFromQueryString(string sqlCommand,string recordCountSql, IList<SqlParameter> parameters)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int recordCount = 0; 
                DataSet set = new DataSet(); 
                recordCount = GetRecordCount(recordCountSql, parameters); 
                jsonModel.total = recordCount;
                DataTable table = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sqlCommand, parameters.ToArray<SqlParameter>()).Tables[0];
                jsonModel.rows = table;
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
            }
            return GetFormatedJsonString(jsonModel);
        }



        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="tableName">表名、视图名、查询语句</param>
        /// <param name="selectedColumns">要显示的字段列表,</param>
        /// <param name="idColumn">主键</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">分页大小</param>
        /// <param name="parameters">参数列表</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="sort">排序， 只能取desc或者asc</param>
        /// <returns></returns>
        private static string GetSearchData(string tableName, string selectedColumns, string idColumn, int pageIndex, int pageCount, IList<TF.RunSafty.Entry.SearchParameter> parameters, string orderby, string sort)
        {
            int recordCount = 0;
            string where = "";
            DataSet set = new DataSet();
            string sqlCommandText = "";

            where = BuildWhereString(parameters);
            recordCount = GetRecordCount(tableName, where,parameters);
            if (pageIndex == 0)
            {
                sqlCommandText = string.Format(@"select top {0}  {1} from {2}  where 1=1 {3}  
order by  {4}  {5} ", pageCount, selectedColumns, tableName, where, orderby,sort);
            }
            else
            {
                if (sort.ToLower().Equals("desc"))
                {
                    sqlCommandText = string.Format(@"select top {0}  {1} from {2}  where 1=1 {3} and 
{4} < ( select min( {4}  ) from (select top {6}   {4}   from  {0}  where 1=1 {3}   order by   {5}  desc) as t) 
order by  {5}  desc ", pageCount, selectedColumns, tableName, where, idColumn, orderby, pageCount * pageIndex);
                }
                else
                {
                    sqlCommandText = string.Format(@"select top {0}  {1} from {2}  where 1=1 {3} and 
{4} > ( select max( {4}  ) from (select top {6}   {4}   from  {2}  where 1=1 {3}   order by   {5}  asc) as t) 
order by  {5}  asc ", pageCount, selectedColumns, tableName, where, idColumn, orderby, pageCount * pageIndex);
                }
            }
            JsonModel jsonModel = new JsonModel();
            jsonModel.total = recordCount; 
            try
            {
                DataTable table = GetQueryDataTable(sqlCommandText, parameters);
                jsonModel.rows = table;
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
            }
            return GetFormatedJsonString(jsonModel);
        }
        public static DataTable GetQueryDataTable(string sqlCommandText, IList<TF.RunSafty.Entry.SearchParameter> parameters)
        {
            
            DataSet set = new DataSet();
            SqlCommand command = new SqlCommand(sqlCommandText, new SqlConnection(SqlHelper.ConnString));
            command.CommandType = CommandType.Text;
            SetSqlCommandParameter(command, parameters);
            try
            {
                command.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(set);
                if (set != null)
                {
                    if (set.Tables.Count > 0)
                    {
                        return set.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
                command.Dispose();
            }
            return null;
        }


        private  string GetCommandText(string tableName, string where)
        {
            string sqlCommandText = "";
            if (pageIndex == 0)
            {
                sqlCommandText = string.Format(@"select top {0}  {1} from {2}  where 1=1 {3}  
order by  {4}  {5} ", this.pageSize, selectedColumns, tableName, where, this.order_By, sort);
            }
            else
            {
                if (sort.ToLower().Equals("desc"))
                {
                    sqlCommandText = string.Format(@"select top {0}  {1} from {2}  where 1=1 {3} and 
{4} < ( select min( {4}  ) from (select top {6}   {4}   from  {2}  where 1=1 {3}   order by   {5}  desc) as t) 
order by  {5}  desc ", this.pageSize, selectedColumns, tableName, where, idColumn, this.order_By, this.pageSize * pageIndex);
                }
                else
                {
                    sqlCommandText = string.Format(@"select top {0}  {1} from {2}  where 1=1 {3} and 
{4} > ( select max( {4}  ) from (select top {6}   {4}   from  {2}  where 1=1 {3}   order by   {5}  asc) as t) 
order by  {5}  asc ", this.pageSize, selectedColumns, tableName, where, idColumn, this.order_By, this.pageSize * pageIndex);
                }
            }
            return sqlCommandText;
        }

        public static string GetFormatedJsonString(JsonModel mode)
        {
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            JsonSerializerSettings nullValueSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include };
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            nullValueSetting.Converters.Add(timeConverter);
            return Newtonsoft.Json.JsonConvert.SerializeObject(mode, Newtonsoft.Json.Formatting.None, nullValueSetting);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="tableName">表名、视图名、查询语句</param>
        /// <param name="selectedColumns">要显示的字段列表,</param>
        /// <param name="idColumn">主键</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">分页大小</param>
        /// <param name="where">过滤条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="sort">排序， 只能取desc或者asc</param>
        /// <returns></returns>
        private static string GetSearchData(string tableName, string selectedColumns, string idColumn, int pageIndex, int pageCount, string where, string orderby, string sort)
        {
            int recordCount = 0;
            JsonModel jsonModel = new JsonModel();
            jsonModel.total = 0;
            SqlParameter parameter = new SqlParameter("RecordCount", recordCount);
            parameter.Direction = ParameterDirection.Output;
            DataSet set = new DataSet();
            SqlCommand command = new SqlCommand("pro_page", new SqlConnection(SqlHelper.ConnString));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(parameter);
            command.Parameters.AddWithValue("QueryStr", tableName);
            command.Parameters.AddWithValue("PageSize", pageCount);
            command.Parameters.AddWithValue("PageCurrent", pageIndex);
            command.Parameters.AddWithValue("FdShow",selectedColumns);
            command.Parameters.AddWithValue("IdentityStr", idColumn);
            command.Parameters.AddWithValue("WhereStr", where);
            command.Parameters.AddWithValue("OrderField", orderby);
            command.Parameters.AddWithValue("FdOrder", sort);
            try
            {
                command.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(set);
                if (set != null)
                {
                    if (set.Tables.Count > 0)
                    {
                        jsonModel.total = (int)command.Parameters["RecordCount"].Value;
                        jsonModel.rows = set.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
            }
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel, Newtonsoft.Json.Formatting.None, timeConverter);
        }


        #region =========================Excel导出===========================

        public static DataTable GetExportData(string tableName, string selectedColumns, IList<TF.RunSafty.Entry.SearchParameter> parameters, string orderby, string sort)
        {
            DataTable table = null;
            string where = "";
            DataSet set = new DataSet();
            string sqlCommandText = "";
            where = BuildWhereString(parameters);
            sqlCommandText = string.Format(@"select  {0}  from {1}  where 1=1 {2}  order by  {3}  {4} ", selectedColumns, tableName, where, orderby, sort);
            SqlCommand command = new SqlCommand(sqlCommandText, new SqlConnection(SqlHelper.ConnString));
            command.CommandType = CommandType.Text;
            SetSqlCommandParameter(command, parameters);
            try
            {
                command.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(set);
                if (set != null)
                {
                    if (set.Tables.Count > 0)
                    {
                        table = set.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return table;
        }
        #endregion

        #endregion



        public class NullToEmptyStringResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                return type.GetProperties()
                        .Select(p =>
                        {
                            var jp = base.CreateProperty(p, memberSerialization);
                            jp.ValueProvider = new NullToEmptyStringValueProvider1(p);
                            return jp;
                        }).ToList();
            }
        }

        public class NullToEmptyStringValueProvider1 : IValueProvider
        {
            PropertyInfo _MemberInfo;
            public NullToEmptyStringValueProvider1(PropertyInfo memberInfo)
            {
                _MemberInfo = memberInfo;
            }

            public object GetValue(object target)
            {
                object result = _MemberInfo.GetValue(target,null);
                if (_MemberInfo.PropertyType == typeof(string) && result == null) result = "";
                return result;

            }

            public void SetValue(object target, object value)
            {
                _MemberInfo.SetValue(target, value,null);
            }
        }
    }
}
