<%@ WebHandler Language="C#" Class="DataTypeProcess" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;


public class DataTypeProcess : IHttpHandler {

    private Hashtable hash = new Hashtable();
    public class DataObject
    {
        public string ObjectName;
        public string ObjectBrief;
        public string ObjectRemark;
        public string TypeName;
         
    }

    
   
    public class DataCategory
    {
        public string IsSystem;
        public string CategoryName;
        public string CategoryBrief;
        public List<DataObject> items;
    }
    public class DataList
    {
        public List<DataCategory> categories;
    }
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/json";
        ApiManager manager = ApiManager.GetInstance();
        string apiType = context.Request.QueryString["apiType"];
        string apiName = context.Request.QueryString["apiName"];
        try
        {
            hash = manager.InitDataList();
            APIItem item =manager.GetApiItemByTypeAndName(apiType, apiName);
            Property property = new Property();
            List<Property> properties = new List<Property>();
            if (item.input.properties != null)
            {
                foreach (PropertyObject p in item.input.properties)
                {
                    string _typeName = p.TypeName.ToLower();
                    if (_typeName == "string" || _typeName == "int" || _typeName == "datetime" || _typeName == "boolean")
                    {
                        property = new Property();
                        property.ObjectBrief = p.ObjectBrief;
                        property.ObjectName = p.ObjectName;
                        property.ObjectRemark = p.ObjectRemark;
                        property.TypeName = p.TypeName;
                        properties.Add(property);
                    }
                    else
                    {
                        property = GetPropertyObject(property, p.TypeName);
                        property.ObjectName = p.ObjectName;
                        SetSubProperty(property);
                        properties.Add(property);
                    }

                }
            }
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(properties);

            context.Response.Write(result);

        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }

    
    
    
    /// <summary>
    /// 根据DataType获取接口文档.XML中对应的输入参数定义
    /// </summary>
    /// <param name="property"></param>
    /// <param name="dataType"></param>
    /// <returns></returns>
    public Property GetPropertyObject(Property property, string dataType)
    {
        Property dataObj = hash[dataType] as Property;

        if (dataObj != null)
        {
            switch (dataObj.TypeSort)
            {
                case "0":
                    break;
                case "1":
                    break;
                case "2":
                    if (!string.IsNullOrEmpty(dataObj.TypeName))
                    {
                        Property t = hash[dataObj.TypeName] as Property;
                        if (t != null)
                        {
                            dataObj.subItems = t.subItems;
                        }
                    }
                    break;
                default: break;
            }
            if (string.IsNullOrEmpty(dataObj.TypeName))
            {
                for (int i = 0; i < dataObj.subItems.Count; i++)
                {
                    Property p = dataObj.subItems[i];
                    string _typeName = p.TypeName.ToLower();
                    if (_typeName == "string" || _typeName == "int" || _typeName == "datetime" || _typeName == "boolean")
                    {
                    }
                    else
                    {
                        _typeName = p.TypeName;
                        p.subItems = new List<Property>();
                        p.subItems.Add(GetPropertyObject(p, _typeName));
                    }
                }
            }
        }
        return dataObj;
    }

    public void SetSubProperty(Property property)
    {
        foreach (Property p in property.subItems)
        {
            if (p.subItems != null)
            {
                foreach (Property subProperty in p.subItems)
                {
                    SetSubProperty(subProperty);
                }
            }
            if (property.TypeSort == "2")
            {
                p.ObjectName = property.ObjectName + "[0]." + p.ObjectName;
            }
            else
            {
                p.ObjectName = property.ObjectName + "." + p.ObjectName;
            }
        }
    }
     
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}