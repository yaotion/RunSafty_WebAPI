using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestPage_TextToSql_TextToSql_ListInfo : System.Web.UI.Page
{
    public string strName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if (Request.QueryString["strName"] != null)
            {
                strName = Request.QueryString["strName"].ToString();
            }
        
        }

    }
}