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
using System.Collections.Generic;
namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///ReadingUpload 的摘要说明
    /// </summary>
    public class IReadingUpload : IQueryResult
    {
        private class ParamModel
        {
            public List<jsBase> Data;
        }
        public class jsBase
        {
        }
        public class Reading : jsBase
        {
            public rData data;
        }
        public class printData : jsBase
        {
            public printModel data;
        }
        public class printModel
        {
            public string dtPrintTime;
            public string strTrainmanGUID;
            public string strPlanGUID;
        }
        public class rData
        {
            public List<fReader> ReaderArray;
            public List<fTypeList> TypeList;
        }
        public class fReader
        {
            public string TrainmanGUID;
        }
        public class fTypeList
        {
            public string strTypeGUID;
            public string strTypeName;
            public List<rFile> FileList;
        }
        public class rFile
        {
            public string strFileGUID;
            public string dtReadTime;
        }
        private class JsonModel
        {
            public string result;
            public string returnStr;
        }
        public IReadingUpload()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        TF.RunSafty.BLL.TAB_ReadDocPlan bllPlan = new TF.RunSafty.BLL.TAB_ReadDocPlan();
        TF.RunSafty.BLL.TAB_ReadHistory bllRead = new TF.RunSafty.BLL.TAB_ReadHistory();
        TF.RunSafty.BLL.Tab_DeliverJSPrint bllPrint = new TF.RunSafty.BLL.Tab_DeliverJSPrint();

        /// <summary>
        /// 保存打印记录
        /// </summary>
        /// <param name="print"></param>
        public void SavePrintData(printModel print)
        {
            TF.RunSafty.Model.Tab_DeliverJSPrint modelPlan = new TF.RunSafty.Model.Tab_DeliverJSPrint();
            modelPlan.StrPlanGUID = print.strPlanGUID;
            modelPlan.StrSiteGUID = this.cid;
            modelPlan.StrTrainmanGUID = print.strTrainmanGUID;
            string strPrintTime = print.dtPrintTime;
            DateTime dtPrint;
            if (DateTime.TryParse(strPrintTime, out dtPrint))
            {
                modelPlan.dtPrintTime = dtPrint;
            }
            bllPrint.Add(modelPlan);
        }
        /// <summary>
        /// 保存阅读记录
        /// </summary>
        /// <param name="reading"></param>
        public void SaveReadings(rData reading)
        {
            TF.RunSafty.Model.TAB_ReadHistory readingHistory = new TF.RunSafty.Model.TAB_ReadHistory();
            rData data = reading;
            if (data != null && data.ReaderArray != null)
            {
                foreach (fReader trainman in data.ReaderArray)
                {
                    if (data.TypeList != null)
                    {
                        foreach (fTypeList fType in data.TypeList)
                        {
                            foreach (rFile file in fType.FileList)
                            {
                                readingHistory = new TF.RunSafty.Model.TAB_ReadHistory();
                                readingHistory.SiteGUID = this.cid;
                                readingHistory.strTrainmanGUID = trainman.TrainmanGUID;
                                readingHistory.strFileGUID = file.strFileGUID;
                                readingHistory.DtReadTime = file.dtReadTime;
                                bllRead.Add(readingHistory);
                                //更新阅读计划里边的第一次阅读时间、阅读次数
                                bllPlan.UpdateReadTime(file.strFileGUID, trainman.TrainmanGUID, file.dtReadTime);
                            }
                        }
                    }
                }
            }
        }

        public override string QueryResult()
        {
            JsonModel model = new JsonModel();
            {
                Newtonsoft.Json.Converters.JsonVirtualConverter<jsBase> jvc = new Newtonsoft.Json.Converters.JsonVirtualConverter<jsBase>("stepID", t =>
                {
                    switch (t)
                    {
                        case "1004":
                            return new Reading();
                        case "1012":
                            return new printData();
                        default:
                            return null;
                    }
                });
                List<jsBase> array = Newtonsoft.Json.JsonConvert.DeserializeObject<List<jsBase>>(this.Data, jvc);
                TF.RunSafty.Model.TAB_ReadHistory read = new TF.RunSafty.Model.TAB_ReadHistory();
                try
                {
                    if (array != null)
                    {
                        foreach (jsBase t in array)
                        {
                            if (t is Reading)
                            {
                                Reading reading = (Reading)t;
                                SaveReadings(reading.data);
                            }
                            else if (t is printData)
                            {
                                printData print = (printData)t;
                                SavePrintData(print.data);
                            }
                        }
                    }
                    model.result = "0";
                    model.returnStr = "提交成功";
                }
                catch (Exception ex)
                {
                    TF.CommonUtility.LogClass.logex(ex, "");
                    model.result = "1";
                    model.returnStr = "提交失败:" + ex.Message;
                }
                string result = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                return result;
            }
        }
    }
}