using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace TF.RunSafty.Entry
{
    public class Pages
    {
        public Pages(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
        private string id;
        private string name;

        public string _name
        {
            set { _name = value; }
            get { return this.name; }
        }
        public string _id
        {
            set { _id = value; }
            get { return this.id; }
        }
    }
    public class JsonModel
    {
        public int total;
        public object rows;
    }

    public class AttributeModel
    {
        public string url;
    }
    public class MenuModel
    {
        public int id;
        public string text;
        public AttributeModel attributes;
        public IList<MenuModel> children;
    }
    public enum FiltOperation { And, Or }
    public class SearchParameter
    {
        public string ColumnName { get; set; }
        public string Operator { get; set; }
        public SqlParameter param { get; set; }
        public bool IsNeedQuotation { get; set; }
        public List<SearchParameter> SubSearchItems;
        public FiltOperation? Filt;
        public SearchParameter()
        {
        }
        public SearchParameter(string columnName, string operatorName, string parameterName, object parameterValue, bool isNeedQuotation)
        {
            this.ColumnName = columnName;
            Operator = operatorName;
            param = new SqlParameter(parameterName, parameterValue);
            IsNeedQuotation = isNeedQuotation;
            //默认为AND
            this.Filt = FiltOperation.And;
        }

        public SearchParameter(string columnName, string operatorName, string parameterName, object parameterValue, bool isNeedQuotation,FiltOperation filt)
        {
            this.ColumnName = columnName;
            Operator = operatorName;
            param = new SqlParameter(parameterName, parameterValue);
            IsNeedQuotation = isNeedQuotation;
            this.Filt = filt;
        }

        public void AddSubSearchParam(string columnName, string operatorName, string parameterName, object parameterValue, bool isNeedQuotation, FiltOperation? filt)
        {
            if (this.SubSearchItems == null)
                this.SubSearchItems = new List<SearchParameter>();
            SearchParameter subParam = new SearchParameter();
            subParam.ColumnName = columnName;
            subParam.Operator = operatorName;
            subParam.param = new SqlParameter(parameterName, parameterValue);
            subParam.IsNeedQuotation = IsNeedQuotation;
            subParam.Filt = filt;
            this.SubSearchItems.Add(subParam);
        }
        public void AddSubSearchParam(string columnName, string operatorName, string parameterName, object parameterValue, bool isNeedQuotation)
        {
            if (this.SubSearchItems == null)
                this.SubSearchItems = new List<SearchParameter>();
            SearchParameter subParam = new SearchParameter();
            subParam.ColumnName = columnName;
            subParam.Operator = operatorName;
            subParam.param = new SqlParameter(parameterName, parameterValue);
            subParam.IsNeedQuotation = IsNeedQuotation;
            this.SubSearchItems.Add(subParam);
        }
    }
}
