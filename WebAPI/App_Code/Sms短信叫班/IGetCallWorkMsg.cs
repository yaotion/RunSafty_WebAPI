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
using TF.Api.Utilities;
using TF.RunSafty.Model;
using TF.RunSafty.Model.InterfaceModel;
using System.Collections.Generic;



namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///IGetCallWorkMsg 的摘要说明
    /// </summary>
    public class IGetCallWorkMsg : IQueryResult
    {
        private class pTrainjiaoluPlan : ParamBase
        {
            public pMsg data;
        }
        public class pMsg
        {
            [NotNull]
            public string strPlanGUID { get; set; }
            [NotNull]
            public string strTrainmanGUID { get; set; }
            [NotNull]
            public string strContent { get; set; }
            [NotNull]
            public string dtCallTime { get; set; }
        }
        public IGetCallWorkMsg()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public override string QueryResult()
        {
            throw new NotImplementedException();
        }
    }
}