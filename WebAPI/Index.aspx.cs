using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

  /// <summary>
    ///  Api 接口列表索引页
    ///   2014-06  
    ///   by Mr.Tang
    /// </summary>
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //StringBuilder sb = new StringBuilder();
                   
                //sb.Append("请求url路径:"+Request.RawUrl+"<br/>");
                //sb.Append("绝对路径:" + Request.Url.AbsolutePath + "<br/>");
                //sb.Append("物理路径:" + Request.PhysicalPath + "<br/>");
                //sb.Append("绝对url地址:" + Request.Url.AbsoluteUri + "<br/><p>");

                //if (Request.Params.Count>0)
                //{
                //    foreach (string s in Request.QueryString)
                //        sb.Append("<br/>收到的参数:" + s);
                //}
                //Response.Write(sb.ToString());
            }
        }
    }
