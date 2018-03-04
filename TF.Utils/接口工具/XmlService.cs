using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Collections;


namespace TF.Utils
{

    public class cParam
    {
        public string strParamID;
        public string strParamName;

    }


    public class XmlService
    {
        private XmlDocument xDoc = new XmlDocument();
        public List<cParam> getParams(string xmlPath, string mName)
        {
            List<cParam> Lc = new List<cParam>();
            xDoc.Load(xmlPath);
            XmlNode xn = xDoc.SelectSingleNode("doc");
            XmlNodeList xnlist = xn.ChildNodes;
            foreach (XmlNode xnf in xnlist)
            {
                XmlElement xe = (XmlElement)xnf;
                if (xe.Name == "members")
                {
                    XmlNodeList xnMenberList = xe.ChildNodes;
                    foreach (XmlNode xMembers in xnMenberList)
                    {
                        XmlElement xm = (XmlElement)xMembers;
                        if (xm.GetAttribute("name").ToString().Contains(mName))
                        {
                            XmlNodeList xnParamList = xm.ChildNodes;
                            foreach (XmlNode xParams in xnParamList)
                            {
                                XmlElement xp = (XmlElement)xParams;
                                if (xp.GetAttribute("name") != "")
                                {
                                    cParam c = new cParam();
                                    c.strParamID = xp.GetAttribute("name");
                                    if (xp.ChildNodes.Count == 0)
                                        c.strParamName = "";
                                    else
                                        c.strParamName = xp.ChildNodes[0].InnerText;
                                    Lc.Add(c);
                                }
                            }
                        }
                    
                    }
                
                }
            }
            return Lc;

        }

        public string getParams4Object(string xmlPath, string mName)
        {
            List<cParam> Lc = new List<cParam>();
            xDoc.Load(xmlPath);
            XmlNode xn = xDoc.SelectSingleNode("doc");
            XmlNodeList xnlist = xn.ChildNodes;
            foreach (XmlNode xnf in xnlist)
            {
                XmlElement xe = (XmlElement)xnf;
                if (xe.Name == "members")
                {
                    XmlNodeList xnMenberList = xe.ChildNodes;
                    foreach (XmlNode xMembers in xnMenberList)
                    {
                        XmlElement xm = (XmlElement)xMembers;
                        if (xm.GetAttribute("name").ToString().Contains(mName))
                        {
                            XmlNodeList xnParamList = xm.ChildNodes;
                            foreach (XmlNode xParams in xnParamList)
                            {
                                XmlElement xp = (XmlElement)xParams;
                                if (xp.Name == "summary")
                                    return xp.ChildNodes[0].InnerText;
                            }
                        }

                    }

                }
            }
            return "";
        }




    }
}
