using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

public partial class TestPage_XML_Edit : System.Web.UI.Page
{

    public string NodeText = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            NodeText = Request.QueryString["NodeText"].ToString();
        }

    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        NodeText = Request.QueryString["NodeText"].ToString();
        XmlDocument xmlDoc = new XmlDocument();
        string xmlPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\接口文档.xml";
        xmlDoc.Load(xmlPath);
        XmlNode root = xmlDoc.SelectSingleNode("WebAPI");
        XmlNode toot1 = root.SelectSingleNode("Project");
        XmlNode toot2 = toot1.SelectSingleNode("APIList");
        XmlNode toot3 = null;
        XmlNodeList xnl = toot2.ChildNodes;
        foreach (XmlNode xnf in xnl)
        {
            XmlElement xe = (XmlElement)xnf;
            if (xe.GetAttribute("TypeBrief") == NodeText)
            {
                toot3 = xnf;
                break;
            }
        }
        string APIBrief = this.TXT_APIBrief.Text.Trim();
        string APIName = this.TXT_APIName.Text.Trim();
        string MethodName = this.TXT_MethodName.Text.Trim();
        string TypeName = this.TXT_TypeName.Text.Trim();
        XmlElement xe1 = xmlDoc.CreateElement("APIItem");
        xe1.SetAttribute("APIBrief", APIBrief);
        xe1.SetAttribute("APIName", APIName);
        xe1.SetAttribute("APIVersion", "");
        xe1.SetAttribute("TypeName", "");
        xe1.SetAttribute("TableName", TypeName);
        xe1.SetAttribute("MethodName", MethodName);

        XmlElement xesub1 = xmlDoc.CreateElement("InputData");
        xesub1.SetAttribute("ObjectName", "");
        xesub1.SetAttribute("ObjectBrief", "");
        xesub1.SetAttribute("ObjectRemark", "");
        xesub1.SetAttribute("TypeName", "");
        xesub1.SetAttribute("TypeSort", "");
        xe1.AppendChild(xesub1);
        XmlElement xesub2 = xmlDoc.CreateElement("OutputData");
        xesub2.SetAttribute("ObjectName", "");
        xesub2.SetAttribute("ObjectBrief", "");
        xesub2.SetAttribute("ObjectRemark", "");
        xesub2.SetAttribute("TypeName", "");
        xesub2.SetAttribute("TypeSort", "");

        xe1.AppendChild(xesub2);
        toot3.AppendChild(xe1);
        xmlDoc.Save(xmlPath);

        PageBase.static_Message_ext(this, "var win = art.dialog.open.origin;win.ReloadPage();art.dialog.close();");
    }
}