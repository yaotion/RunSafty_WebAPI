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
using System.Collections;
using System.Collections.Generic;

namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///ReadingRecord 的摘要说明
    /// </summary>
    public class IReadingRecord : IQueryResult
    {
        private class ParamModel
        {
            public string strTrainmanGUID;
        }
        private class TypeList
        {
            public string strTypeGUID;
            public string strTypeName;
            public DataTable FileList;
        }
        private class InnerData
        {
            public List<TypeList> TypeList;
        }
        private class JsonData
        {
            public InnerData data;
        }
        private class JsonModel
        {
            public string result;
            public string returnStr;
            public JsonData Data;
        }
        public IReadingRecord()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public override string QueryResult()
        {
            JsonModel model = new JsonModel();
            DataTable table = null;
            JsonData data = new JsonData();
            List<TypeList> typeList = new List<TypeList>();
            data.data = new InnerData();
            data.data.TypeList = typeList;
            DataTable tblReading = null;
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(this.Data);
            TF.RunSafty.BLL.TAB_FileGroup bllGroup = new TF.RunSafty.BLL.TAB_FileGroup();
            TF.RunSafty.BLL.TAB_ReadDocPlan bllPlan = new TF.RunSafty.BLL.TAB_ReadDocPlan();
            try
            {
                string where = string.Format(" strTypeName='{0}'", "记名式传达");
                table = bllGroup.GetList(where).Tables[0];

                //table = bllGroup.GetAllList().Tables[0];
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        TypeList list = new TypeList();
                        list.strTypeGUID = row["strTypeGUID"].ToString();
                        list.strTypeName = row["strTypeName"].ToString();
                        tblReading = bllPlan.GetReadingHistoryOfTrainman(param.strTrainmanGUID, list.strTypeGUID);
                        list.FileList = tblReading;
                        typeList.Add(list);
                    }
                }
                model.result = "0";
                model.returnStr = "返回成功";
                model.Data = data;
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