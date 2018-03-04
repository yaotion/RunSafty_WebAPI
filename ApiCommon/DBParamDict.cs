using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TFApiCommon.DBParam
{
    public enum ParamDataType
    {
        dtUnknown,
        dtString,
        dtInt,
        dtDateTime
    }

    public class ParamValuePair
    {
        public string fieldName;
        public object value;
        public ParamDataType dataType;
    }
    public class DbParamDict
    {
        private List<ParamValuePair> FieldsDict = new List<ParamValuePair>();

        private string GetParamString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < FieldsDict.Count; i++)
            {
                sb.Append("@" + FieldsDict[i].fieldName);
                if (i < FieldsDict.Count - 1)
                {
                    sb.Append(",");
                }

            }

            return sb.ToString();
        }

        public string GetFieldsString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < FieldsDict.Count; i++)
            {
                sb.Append(FieldsDict[i].fieldName);
                if (i < FieldsDict.Count - 1)
                {
                    sb.Append(",");
                }

            }

            return sb.ToString();
        }

        public void Add(string FieldName, object value, ParamDataType dataType)
        {
            ParamValuePair paramPair = new ParamValuePair();

            paramPair.value = value;
            paramPair.fieldName = FieldName;
            paramPair.dataType = dataType;
            FieldsDict.Add(paramPair);
        }

        public void Add(string FieldName, object value)
        {
            Add(FieldName, value, ParamDataType.dtUnknown);
        }

        public string GetInsertSqlString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" (");
            sb.Append(GetFieldsString());
            sb.Append(") ");
            sb.Append("  values(");
            sb.Append(GetParamString());
            sb.Append(")");

            return sb.ToString();
        }

        public string GetUpdateFieldsString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" set ");
            for (int i = 0; i < FieldsDict.Count; i++)
            {
                sb.Append(FieldsDict[i].fieldName + "=@" + FieldsDict[i].fieldName);

                if (i < FieldsDict.Count - 1)
                {
                    sb.Append(",");
                }

            }

            return sb.ToString();
        }

        public SqlParameter[] GetParams()
        {
            List<SqlParameter> ret = new List<SqlParameter>();
            SqlParameter param;
            for (int i = 0; i < FieldsDict.Count; i++)
            {
                if (FieldsDict[i].value == null)
                {
                    param = new SqlParameter(FieldsDict[i].fieldName, DBNull.Value);
                }
                else
                {
                    param = new SqlParameter(FieldsDict[i].fieldName, FieldsDict[i].value);
                }

                switch (FieldsDict[i].dataType)
                {
                    case ParamDataType.dtString: param.SqlDbType = SqlDbType.VarChar; break;
                    case ParamDataType.dtInt: param.SqlDbType = SqlDbType.Int; break;
                    case ParamDataType.dtDateTime: param.SqlDbType = SqlDbType.DateTime; break;
                }


                ret.Add(param);
            }


            return ret.ToArray();
        }


        public void Clear()
        {
            FieldsDict.Clear();
        }



        
    }


    public class DbCDBuilder<T>
    {
        private Dictionary<string, string> dict_Field = new Dictionary<string, string>();
        private string GetDBField(string objField)
        {
            string ret = objField;
            if (dict_Field.ContainsKey(objField))
            {
                ret = dict_Field[objField];
            }
   
            return ret;
                        
        }
        private void AddFieldCondition(StringBuilder sqlBuilder,string objField)
        {
            sqlBuilder.Append(" and " + GetDBField(objField) + "=@" + GetDBField(objField)); 
        }
        public void AddFieldPairs(string objField,string dbField)
        {
            dict_Field.Add(objField, dbField);
        }

        public void ClearFieldPair()
        { 
            dict_Field.Clear();
        }

        private void AddCondition(Type fieldType,object Value, string fieldName, StringBuilder sqlBuilder)
        {
            if (Value == null) return;

            switch (fieldType.Name)
            {
                case "DateTime":
                    if (Convert.ToDateTime(Value) > DateTime.MinValue)
                    {                        
                        AddFieldCondition(sqlBuilder, fieldName);
                    }                    
                    break;
                case "Double":
                    if (Convert.ToDouble(Value) != 0)
                    {
                        AddFieldCondition(sqlBuilder, fieldName);
                    }
                    break;
                case "Int32":
                    if (Convert.ToInt32(Value) > 0)
                    {
                        AddFieldCondition(sqlBuilder, fieldName);
                    }
                    break;
                case "String":
                    if (Value.ToString() != string.Empty)
                    {
                        AddFieldCondition(sqlBuilder, fieldName);
                    }

                    break;
                case "UInt32":
                    if (Value.ToString() != string.Empty)
                    {
                        AddFieldCondition(sqlBuilder, fieldName);
                    }
                    break;
                default: break;
            }
        }
        public string Field_Sql(T obj)
        {
            StringBuilder ret = new StringBuilder();
            ret.Append(" 1=1 ");
            
            Type objType = obj.GetType();
            Type fieldType;
            object fieldValue;
            System.Reflection.FieldInfo[] fields = objType.GetFields();

            foreach (System.Reflection.FieldInfo field in fields)
            {
                if (field.IsPublic)
                {
                    fieldValue = field.GetValue(obj);
                    fieldType = field.FieldType;

                    if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        Type[] typeArray = field.FieldType.GetGenericArguments();
                        fieldType = typeArray[0];                       
                    }

                    AddCondition(fieldType,
                        fieldValue,field.Name,ret);
                }
            }
            return ret.ToString();
        }

        public string Property_Sql(T obj)
        {
            StringBuilder ret = new StringBuilder();
            ret.Append(" 1=1 ");

            Type objType = obj.GetType();
            object propertyValue;
            System.Reflection.PropertyInfo[] propertys = objType.GetProperties();

            foreach (System.Reflection.PropertyInfo property in propertys)
            {

                propertyValue = property.GetValue(obj,null);

                
            }
            return ret.ToString();
        }
        
    }
   
}
