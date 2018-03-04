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

namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///ReadingSync 的摘要说明
    /// </summary>
    public class IReadingSync : IQueryResult
    {
        private string clientid;
        private class JsonModel
        {
            public string result;
            public string returnStr;
            public DataTable TypeList;
            public DataTable FileList;
        }
        private class ParamModel
        {
            public string cid;
        }
        public IReadingSync()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public override string QueryResult()
        {
            JsonModel model = new JsonModel();
            ParamModel p = new ParamModel();
            DataTable table = null;
            DataTable tblFile = null;
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
            this.clientid = param.cid;
            TF.RunSafty.BLL.TAB_FileGroup bllFileGroup = new TF.RunSafty.BLL.TAB_FileGroup();
            TF.RunSafty.BLL.TAB_ReadDoc bllFile = new TF.RunSafty.BLL.TAB_ReadDoc();
            try
            {
                table = bllFileGroup.GetAllList().Tables[0];
                tblFile = bllFile.GetAllListWithFileType(clientid).Tables[0];
                model.result = "0";
                model.returnStr = "返回成功";
                model.TypeList = table;
                model.FileList = tblFile;
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
            }
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(model, timeConverter).Replace(":null", ":\"\"");
            return result;
        }
    }
}
