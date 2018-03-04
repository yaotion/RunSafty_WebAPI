<%@ WebHandler Language="C#" Class="bootstrap" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using TF.RunSaftyAPI.App_Api.Public;
public class bootstrap : IHttpHandler {

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
            //DataList list = InitDataList();
            hash = manager.InitDataList();
            APIItem item =manager.GetApiItemByTypeAndName(apiType, apiName);
            Property property = new Property();
            property.subItems=new List<Property>();
            List<Property> properties = new List<Property>();
            if (item.input.properties != null)
            {
                foreach (PropertyObject p in item.input.properties)
                {
                    property=GetProperty(p);
                    GetInnerProperty(property); 
                    properties.Add(property);
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

    private void GetInnerProperty(Property property)
    {
        Property o = hash[property.TypeName] as Property;
        string _tname = "";
        if (o != null)
        {
            if (o.subItems != null)
            {
                foreach (Property t in o.subItems)
                {
                    if (property.subItems == null)
                        property.subItems = new List<Property>();
                    _tname = t.TypeName.ToLower();
                    if (_tname == "int" || _tname == "string" || _tname == "datetime")
                    {
                        property.subItems.Add(t);
                    }
                    else
                    {
                        property.subItems.Add(t);
                        GetInnerProperty(t);
                    }
                }
            }
        }
    }
    private Property GetProperty(PropertyObject p)
    {
        Property property=new Property();
        property.ObjectBrief = p.ObjectBrief;
        property.ObjectName = p.ObjectName;
        property.ObjectRemark = p.ObjectRemark;
        property.TypeName = p.TypeName;
        property.TypeSort = p.TypeSort;
        property.subItems=new List<Property>();
        return property;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}