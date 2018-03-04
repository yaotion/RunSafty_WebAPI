<%@ WebHandler Language="C#" Class="Tree" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
public class Tree : IHttpHandler {
    
    public class tNode{
        public int id;
        public string text="";
        public string iconCls="";
        public Attribute attributes=new Attribute();
        public List<tNode> children;
    }
    public class Attribute
    {
        public string ApiTypeName = "";
        public string APIName = "";
    }
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/json";
        string xmlPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\接口文档.xml";
        XElement root = XElement.Load(xmlPath);
        XElement eProject = root.Element("Project");
        XElement eDataList = eProject.Element("DataList");
        XElement eApiList = eProject.Element("APIList");
        IEnumerable<XElement> eApiTypes = eApiList.Elements("APIType");
        List<tNode> tNodes = new List<tNode>();
        int index = 1;
        if (eApiTypes != null)
        {
            foreach (XElement ele in eApiTypes)
            {
                tNode node = new tNode();
                node.id = index;
                node.text = ele.Attributes("TypeBrief").First().Value;
                tNodes.Add(node);
                node.children = new List<tNode>();
                IEnumerable<XElement> apiList = ele.Elements("APIItem");
                if (apiList != null)
                {
                    foreach (XElement xEl in apiList)
                    {
                        tNode sNode = new tNode();
                        sNode.text = xEl.Attributes("APIBrief").First().Value;
                        sNode.attributes.ApiTypeName = ele.Attributes("TypeName").FirstOrDefault().Value;
                        sNode.attributes.APIName = xEl.Attributes("APIName").FirstOrDefault().Value;
                        node.children.Add(sNode);
                        index++;
                    }
                }
                index++;
            }
        }
        string result = Newtonsoft.Json.JsonConvert.SerializeObject(tNodes);
        context.Response.Write(result);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}