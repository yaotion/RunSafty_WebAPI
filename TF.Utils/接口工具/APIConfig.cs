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
using System.Reflection;

namespace TF.Utils
{

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
        public bool ObjectMode = false;
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


    /// <summary>
    ///ApiManager 的摘要说明
    /// </summary>
    public class APIManage
    {
        #region '私有变量定义'
        //单例模式存储对象
        private static APIManage _Instance = null;
        //用于控制多线程访问的互斥锁
        private static object _Lock = new object();
        //接口文件的缺省全路径
        private string _FullConfigName = HttpRuntime.AppDomainAppPath.ToString() + "接口文档.xml";
        //接口文档存储对象
        private WebAPI _WebAPI = null;
        #endregion '私有变量定义'

        #region 从接口文档读取接口定义
        /// <summary>
        /// 读取项目信息
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 读取数据定义列表
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 读取数据分类信息
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 读取单个数据定义信息
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 读取数据定义的属性信息
        /// </summary>
        /// <param name="element"></param>
        /// <param name="typeSort"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 读取API列表信息
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 读取API分类信息
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 读取单个API信息
        /// </summary>
        /// <param name="element"></param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
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
            item.ObjectMode = false;
            bool tempMode;
            if (element.Attributes("ObjectMode").FirstOrDefault() != null)
            {
                if (bool.TryParse(element.Attributes("ObjectMode").FirstOrDefault().Value, out  tempMode))
                {
                    item.ObjectMode = tempMode;
                }
            }
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
        /// APIManage的构造函数
        /// </summary>
        private APIManage()
        {
            _WebAPI = new WebAPI();
            Load();
        }
        /// <summary>
        /// 从Config文件中加载数据
        /// </summary>
        private void Load()
        {
            XElement root = XElement.Load(_FullConfigName);
            XElement eProject = root.Element("Project");
            _WebAPI._project = GetProjectFromElement(eProject);
        }

        /// <summary>
        /// 获取ApiManager的单例形式
        /// </summary>
        /// <returns></returns>
        public static APIManage GetInstance()
        {
            if (_Instance != null) return _Instance;
            lock (_Lock)
            {
                if (_Instance == null)
                {
                    _Instance = new APIManage();
                }
            }
            return _Instance;
        }


        /// <summary>
        /// 从接口文档.XML获取接口定义
        /// </summary>
        /// <returns></returns>
        public List<APIItem> GetApiList()
        {
            List<APIItem> items = new List<APIItem>();
            Load();
            foreach (APIType type in _WebAPI._project._ApiList._ApiTypes)
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
        private void CacheItemUpdateCallback(string key, CacheItemUpdateReason reason, out Object expensiveObject,
            out CacheDependency dependency, out DateTime absoluteExpiration, out TimeSpan slidingExpiration)
        {
            expensiveObject = GetApiList();
            absoluteExpiration = Cache.NoAbsoluteExpiration;
            dependency = new CacheDependency(_FullConfigName);
            slidingExpiration = TimeSpan.FromHours(1.0);
        }
        /// <summary>
        /// 缓存接口列表
        /// </summary>
        /// <param name="items"></param>
        private void CacheApiList(List<APIItem> items)
        {
            CacheDependency dependency = new CacheDependency(_FullConfigName);
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
            var apiTypes = this._WebAPI._project._ApiList._ApiTypes;
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
            foreach (DataGategory dataCategory in this._WebAPI._project._dataList._DataCategories)
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
            return this._WebAPI._project.NameSpace;
        }

        /// <summary>
        /// 从缓存中获取接口定义
        /// </summary>
        /// <param name="strApiName">接口名称</param>
        /// <returns></returns>
        public APIItem GetApiFromCache(string strApiName)
        {
            APIItem item = null;
            Cache cache = HttpRuntime.Cache;
            List<APIItem> items = (List<APIItem>)cache.Get("apilist");

            if (items == null)
            {
                APIManage manager = APIManage.GetInstance();
                items = manager.GetApiList();
            }
            item = GetApiByName(items, strApiName);

            return item;
        }
        /// <summary>
        /// 根据接口名称，从接口定义列表中获取指定的接口定义
        /// </summary>
        /// <param name="items">接口定义列表</param>
        /// <param name="strApiName">接口名称</param>
        /// <returns></returns>
        public APIItem GetApiByName(List<APIItem> items, string strApiName)
        {
            APIItem item = null;
            if (items != null)
            {
                foreach (APIItem api in items)
                {
                    if (api.APIName.ToLower().Equals(strApiName.ToLower()))
                    {
                        item = api;
                        break;
                    }
                }
            }
            return item;
        }

        /// <summary>
        /// 执行API方法并把执行结果返回
        /// </summary>
        /// <param name="APIName"></param>
        /// <param name="APIData"></param>
        /// <returns></returns>
        public static bool ExecAPI(string APIName, string APIData, out string ExecResult,ref string apiChinaName)
        {
            bool isSuccess = false;
            //实例化API管理对象
            APIManage manager = APIManage.GetInstance();
            //获取指定API名称的详细信息
            APIItem item = manager.GetApiFromCache(APIName);
            //API执行结果描述            
            APIResult apiResult = new APIResult();
            try
            {
                if (item == null)
                    throw new Exception("接口不存在，找不到此接口：" + APIName);
                //获取命名空间
                string strNameSpace = manager.GetNameSpace();
                //程序集名称
                string strAssemblyName = item.AssemblyName;
                //类型全名称
                string strTypeFullName = strNameSpace + "." + item.TypeName;
                //方法名
                string strMethodName = item.MethodName;

                apiChinaName = item.APIBrief;
                ///接口不存在
             
                object obj = Reflect.ExecAPI(strAssemblyName, strTypeFullName, strMethodName, APIData, item.ObjectMode);
                if (item.ObjectMode)
                {
                    apiResult.result = 0;
                    apiResult.resultStr = "执行成功";
                    apiResult.data = obj;
                    ExecResult = ReturnStrJson(apiResult);
                }
                else
                {
                    ExecResult = ReturnStrJson(obj);
                }

                isSuccess = true;
            }
            catch (Exception e)
            {
                apiResult.result = 1;
                if (e.InnerException != null)
                    apiResult.resultStr = e.InnerException.Message.Replace("\r\n", "");
                else
                    apiResult.resultStr = e.Message.Replace("\r\n", "");

                ExecResult = ReturnStrJson(apiResult);
            }
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式

            return isSuccess;
        }

        public static string ReturnStrJson(object o)
        {
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(o, timeConverter);
        }

    }


}
