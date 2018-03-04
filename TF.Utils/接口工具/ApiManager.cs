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
using System.Web.SessionState;
using System.Collections;
using System.Collections.Generic;
using System.Web.Caching;

#region 类型定义
public class Property
{
    public string ObjectName;
    public string ObjectBrief;
    public string ObjectRemark;
    public string TypeName;
    public string TypeSort = "";
    public List<Property> subItems;
}


public class PropertyObject
{
    public string ObjectName = "";
    public string ObjectBrief = "";
    public string ObjectRemark = "";
    public string TypeName = "";
    public string TypeSort = "1";
}

/// <summary>
/// 接口定义
/// </summary>
public class APIItem
{
    public string APIName = "";
    public string APIBrief = "";
    public string TypeName = "";
    public string MethodName = "";
    public string AssemblyName = "";
    public ParamDef input;
    public ParamDef output;
}
/// <summary>
/// 参数定义
/// </summary>
public class ParamDef
{
    public string ObjectName = "";
    public string ObjectBrief = "";
    public string ObjectRemark = "";
    public string TypeName = "";
    public string TypeSort = "0";
    public List<PropertyObject> properties;
}
public class InputData : ParamDef
{
}
public class OutputData : ParamDef
{
}
public class APIType
{
    public string TypeName;
    public string TypeBrief;
    public string TypeOrder;
    public string AssemblyName;
    public List<APIItem> Apies;
}

public class DataObject
{
    public string ObjectName = "";
    public string ObjectBrief = "";
    public string ObjectRemark = "";
    public string TypeName = "";
    public string TypeSort = "0";
    public List<PropertyObject> properties;
}

public class DataGategory
{
    public string IsSystem { get; set; }
    public string CategoryName { get; set; }
    public string CategoryBrief { get; set; }
    public List<DataObject> _DataObjects;
}
public class DataList
{
    public List<DataGategory> _DataCategories;
}

public class APIList
{
    public List<APIType> _ApiTypes;
}
public class Project
{
    public string SavePath { get; set; }
    public string NameSpace { get; set; }
    public string Version { get; set; }
    public string ProjectName { get; set; }
    public DataList _dataList;
    public APIList _ApiList;
}
public class WebAPI
{
    public Project _project;
}

#endregion
/// <summary>
///ApiManager 的摘要说明
/// </summary>
public class ApiManager
{
    private static ApiManager instance = null;
    private static object _lock = new object();
    private string filePath = HttpRuntime.AppDomainAppPath.ToString() + "接口文档.xml";
    private WebAPI _webApi = null;

    public static ApiManager GetInstance()
    {
        if (instance != null) return instance;
        lock (_lock)
        {
            if (instance == null)
            {
                instance = new ApiManager();
            }
        }
        return instance;
    }

    private ApiManager()
    {
        _webApi = new WebAPI();
        Load();
    }

    private void Load()
    {
        XElement root = XElement.Load(filePath);
        XElement eProject = root.Element("Project");
        _webApi._project = GetProjectFromElement(eProject);
    }
    #region 从接口文档读取接口定义
    private Project GetProjectFromElement(XElement element)
    {
        Project project = new Project();
        project.SavePath = element.Attributes("SavePath").FirstOrDefault().Value;
        project.NameSpace = element.Attributes("NameSpace").FirstOrDefault().Value;
        project.ProjectName = element.Attributes("ProjectName").FirstOrDefault().Value;
        project.Version = element.Attributes("Version").FirstOrDefault().Value;
        XElement dataElement = element.Element("DataList");
        XElement apiElement = element.Element("APIList");
        project._dataList = GetDataListFromElement(dataElement);
        project._ApiList = GetApiListFromElement(apiElement);
        return project;
    }

    private DataList GetDataListFromElement(XElement element)
    {
        DataList _list = new DataList();
        _list._DataCategories = new List<DataGategory>();
        foreach (var xElement in element.Elements())
        {
            DataGategory category = GetDataCategoryFromElement(xElement);
            _list._DataCategories.Add(category);
        }
        return _list;
    }

    private DataGategory GetDataCategoryFromElement(XElement element)
    {
        DataGategory category = new DataGategory();
        category.CategoryBrief = element.Attributes("CategoryBrief").FirstOrDefault().Value;
        category.CategoryName = element.Attributes("CategoryName").FirstOrDefault().Value;
        category.IsSystem = element.Attributes("IsSystem").FirstOrDefault().Value;
        category._DataObjects = new List<DataObject>();
        foreach (var xElement in element.Elements())
        {
            DataObject dataObject = GetDataObjectFromElement(xElement);
            category._DataObjects.Add(dataObject);
        }
        return category;
    }

    private DataObject GetDataObjectFromElement(XElement element)
    {
        DataObject dataObject = new DataObject();
        dataObject.ObjectBrief = element.Attributes("ObjectBrief").FirstOrDefault().Value;
        dataObject.ObjectName = element.Attributes("ObjectName").FirstOrDefault().Value;
        dataObject.ObjectRemark = element.Attributes("ObjectRemark").FirstOrDefault().Value;
        dataObject.TypeName = element.Attributes("TypeName").FirstOrDefault().Value;
        dataObject.TypeSort = element.Attributes("TypeSort").FirstOrDefault().Value;
        dataObject.properties = new List<PropertyObject>();
        IEnumerable<XElement> propertyElements = element.Elements("PropertyObject");
        if (propertyElements != null)
        {
            foreach (var propertyElement in propertyElements)
            {
                PropertyObject property = GetPropertyObjectFromElement(propertyElement, dataObject.TypeSort);
                dataObject.properties.Add(property);
            }
        }
        return dataObject;
    }

    private PropertyObject GetPropertyObjectFromElement(XElement element, string typeSort)
    {
        PropertyObject property = new PropertyObject();
        property.ObjectBrief = element.Attributes("ObjectBrief").FirstOrDefault().Value;
        property.ObjectName = element.Attributes("ObjectName").FirstOrDefault().Value;
        property.ObjectRemark = element.Attributes("ObjectRemark").FirstOrDefault().Value;
        property.TypeName = element.Attributes("TypeName").FirstOrDefault().Value;
        property.TypeSort = typeSort;
        return property;
    }
    private APIList GetApiListFromElement(XElement element)
    {
        APIList _list = new APIList();
        _list._ApiTypes = new List<APIType>();
        foreach (XElement xElement in element.Elements())
        {
            APIType api = GetApiTypeFromElement(xElement);
            _list._ApiTypes.Add(api);
        }
        return _list;
    }

    private APIType GetApiTypeFromElement(XElement element)
    {
        APIType api = new APIType();
        api.AssemblyName = element.Attributes("AssemblyName").FirstOrDefault().Value;
        api.TypeBrief = element.Attributes("TypeBrief").FirstOrDefault().Value;
        api.TypeName = element.Attributes("TypeName").FirstOrDefault().Value;
        api.TypeOrder = element.Attributes("TypeOrder").FirstOrDefault().Value;
        api.Apies = new List<APIItem>();
        foreach (var xElement in element.Elements())
        {
            APIItem item = GetApiItemFromElement(xElement, api.AssemblyName);
            api.Apies.Add(item);
        }
        return api;
    }

    private APIItem GetApiItemFromElement(XElement element, string assemblyName)
    {
        APIItem item = new APIItem();
        item.APIBrief = element.Attributes("APIBrief").FirstOrDefault().Value;
        item.APIName = element.Attributes("APIName").FirstOrDefault().Value;
        item.AssemblyName = assemblyName;
        string parentType = element.Parent.Attributes("TypeName").FirstOrDefault().Value;
        item.MethodName = element.Attributes("MethodName").FirstOrDefault().Value;
        item.TypeName = parentType + "." + element.Attributes("TypeName").FirstOrDefault().Value;
        XElement inputElement = element.Element("InputData");
        XElement outputElement = element.Element("OutputData");
        item.input = GetParamDefFromElement(inputElement);
        item.output = GetParamDefFromElement(outputElement);
        return item;
    }

    /// <summary>
    /// 从XML节点获取数据定义
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    private ParamDef GetParamDefFromElement(XElement element)
    {
        ParamDef param = new ParamDef();
        System.Diagnostics.Debug.Assert(element != null);
        param.ObjectBrief = element.Attributes("ObjectBrief").FirstOrDefault().Value;
        param.ObjectName = element.Attributes("ObjectName").FirstOrDefault().Value;
        param.ObjectRemark = element.Attributes("ObjectRemark").FirstOrDefault().Value;
        param.TypeName = element.Attributes("TypeName").FirstOrDefault().Value;
        param.TypeSort = element.Attributes("TypeSort").FirstOrDefault().Value;
        IEnumerable<XElement> subElements = element.Elements("PropertyObject");
        PropertyObject p = null;
        if (subElements != null && subElements.Count() > 0)
        {
            param.properties = new List<PropertyObject>();
            foreach (XElement elm in subElements)
            {
                p = new PropertyObject();
                p.ObjectBrief = elm.Attributes("ObjectBrief").FirstOrDefault().Value;
                p.ObjectName = elm.Attributes("ObjectName").FirstOrDefault().Value;
                p.ObjectRemark = elm.Attributes("ObjectRemark").FirstOrDefault().Value;
                p.TypeName = elm.Attributes("TypeName").FirstOrDefault().Value;
                //p.TypeSort = element.Attributes("TypeSort").FirstOrDefault().Value;
                param.properties.Add(p);
            }
        }
        return param;
    }
    #endregion
    /// <summary>
    /// 从接口文档.XML获取接口定义
    /// </summary>
    /// <returns></returns>
    public List<APIItem> GetApiList()
    {
        List<APIItem> items = new List<APIItem>();
        Load();
        foreach (APIType type in _webApi._project._ApiList._ApiTypes)
        {
            items.AddRange(type.Apies);
        }
        return items;
    }

    //刷新接口列表，并缓存
    public void RefreshApiList()
    {
        List<APIItem> apiItems = GetApiList();
        CacheApiList(apiItems);
    }

    /// <summary>
    /// 缓存改变回调函数
    /// 目的是当接口文档.XML文件发生改变的时候，重新加载接口文档定义。通常发生在接口更新的时候
    /// </summary>
    /// <param name="key"></param>
    /// <param name="reason"></param>
    /// <param name="expensiveObject"></param>
    /// <param name="dependency"></param>
    /// <param name="absoluteExpiration"></param>
    /// <param name="slidingExpiration"></param>
    private void CacheItemUpdateCallback(string key, CacheItemUpdateReason reason, out Object expensiveObject, out CacheDependency dependency,
out DateTime absoluteExpiration,
out TimeSpan slidingExpiration)
    {
        expensiveObject = GetApiList();
        absoluteExpiration = Cache.NoAbsoluteExpiration;
        dependency = new CacheDependency(filePath);
        slidingExpiration = TimeSpan.FromHours(1.0);
    }
    /// <summary>
    /// 缓存接口列表
    /// </summary>
    /// <param name="items"></param>
    private void CacheApiList(List<APIItem> items)
    {
        CacheDependency dependency = new CacheDependency(filePath);
        Cache cache = HttpRuntime.Cache;
        cache.Insert("apilist", items, dependency, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1.0), CacheItemUpdateCallback);
    }

    /// <summary>
    /// 通过接口类型和接口名称获取接口定义
    /// </summary>
    /// <param name="apiType"></param>
    /// <param name="apiName"></param>
    /// <returns></returns>
    public APIItem GetApiItemByTypeAndName(string apiType, string apiName)
    {
        var apiTypes = this._webApi._project._ApiList._ApiTypes;
        APIItem apiItem = null;
        if (apiTypes != null)
        {
            foreach (var api in apiTypes)
            {
                if (!api.TypeName.Equals(apiType)) continue;
                foreach (APIItem item in api.Apies)
                {
                    if (item.APIName.Equals(apiName))
                    {
                        apiItem = item;
                        break;
                    }
                }
            }
        }
        return apiItem;
    }

    public Hashtable InitDataList()
    {
        Hashtable hash = new Hashtable();
        foreach (DataGategory dataCategory in this._webApi._project._dataList._DataCategories)
        {
            foreach (DataObject dataObject in dataCategory._DataObjects)
            {
                Property p = new Property();
                p.ObjectBrief = dataObject.ObjectBrief;
                p.ObjectName = dataObject.ObjectName;
                p.ObjectRemark = dataObject.ObjectRemark;
                p.TypeName = dataObject.TypeName;
                p.TypeSort = dataObject.TypeSort;
                p.subItems = new List<Property>();
                foreach (PropertyObject pObject in dataObject.properties)
                {
                    Property po = new Property();
                    po.ObjectBrief = pObject.ObjectBrief;
                    po.ObjectName = pObject.ObjectName;
                    po.ObjectRemark = pObject.ObjectRemark;
                    po.TypeName = pObject.TypeName;
                    p.TypeSort = pObject.TypeSort;
                    p.subItems.Add(po);
                }
                hash.Add(dataObject.ObjectName, p);
            }

        }
        return hash;
    }

    public string GetNameSpace()
    {
        return this._webApi._project.NameSpace;
    }
}
