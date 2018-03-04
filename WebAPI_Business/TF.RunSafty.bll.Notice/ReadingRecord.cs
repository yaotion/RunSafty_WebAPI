using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq; 
namespace TF.RunSafty.Notice
{
    public class ReadingRecord
    {
        #region 获取阅读记录

        public class Record_In
        { 
            public string strTrainmanGUID;
        }
        public class TypeList
        {
            public string strTypeGUID;
            public string strTypeName;
            public DataTable FileList;
        }
        public class InnerData
        {
            public List<TypeList> TypeList;
        }
        public class JsonData
        {
            public InnerData data;
        }
        public  class Record_Out
        {
            public string result;
            public string returnStr;
            public JsonData Data;
        }

        public  Record_Out GetReadingRecord(string input)
        {
            Record_Out model = new Record_Out();
            DataTable table = null; 
            JsonData data = new JsonData();
            List<TypeList> typeList = new List<TypeList>();
            data.data = new InnerData();
            data.data.TypeList = typeList;
            DataTable tblReading=null;
            Record_In param = Newtonsoft.Json.JsonConvert.DeserializeObject<Record_In>(input);
            TF.RunSafty.BLL.TAB_FileGroup bllGroup = new TF.RunSafty.BLL.TAB_FileGroup();
            TF.RunSafty.BLL.TAB_ReadDocPlan bllPlan = new TF.RunSafty.BLL.TAB_ReadDocPlan();
            try
            {
                table = bllGroup.GetAllList().Tables[0];
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        TypeList list = new TypeList();
                        list.strTypeGUID = row["strTypeGUID"].ToString();
                        list.strTypeName = row["strTypeName"].ToString();
                        tblReading = bllPlan.GetReadingHistoryOfTrainman(param.strTrainmanGUID,list.strTypeGUID);
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
                model.result = "1";
                model.returnStr = "提交失败："+ex.Message;
            }  
            return model;
        }
        #endregion

        #region 阅读记录同步 
        public class Sync_Out
        {
            public string result;
            public string returnStr;
            public DataTable TypeList;
            public DataTable FileList;
        }
        public  class Sync_In
        {
            public string cid;
        }
        public Sync_Out RecordSync(string input)
        {
            Sync_Out model = new Sync_Out();
            DataTable table = null;
            DataTable tblFile = null;
            Sync_In param = Newtonsoft.Json.JsonConvert.DeserializeObject<Sync_In>(input);
            //this.clientid = param.cid;
            TF.RunSafty.BLL.TAB_FileGroup bllFileGroup = new TF.RunSafty.BLL.TAB_FileGroup();
            TF.RunSafty.BLL.TAB_ReadDoc bllFile = new TF.RunSafty.BLL.TAB_ReadDoc();
            try
            {
                table = bllFileGroup.GetAllList().Tables[0];
                tblFile = bllFile.GetAllListWithFileType(param.cid).Tables[0];
                model.result = "0";
                model.returnStr = "返回成功";
                model.TypeList = table;
                model.FileList = tblFile;
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                model.result = "1";
                model.returnStr = "提交失败：" + ex.Message;
            }
            return model;
        }
 
        #endregion
        #region 阅读记录上传


        
        private class ParamModel
        {
            public List<jsBase> Data;
        }
        public  class jsBase
        {
        }
        public  class Reading : jsBase
        {
            public rData data;
        }
        public  class printData : jsBase
        {
            public printModel data;
        }
        public  class printModel
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
        private class Upload_Out
        {
            public string result;
            public string returnStr;
        } 
        TF.RunSafty.BLL.TAB_ReadDocPlan bllPlan = new TF.RunSafty.BLL.TAB_ReadDocPlan();
        TF.RunSafty.BLL.TAB_ReadHistory bllRead = new TF.RunSafty.BLL.TAB_ReadHistory();
        TF.RunSafty.BLL.Tab_DeliverJSPrint bllPrint = new TF.RunSafty.BLL.Tab_DeliverJSPrint();

        /// <summary>
        /// 保存打印记录
        /// </summary>
        /// <param name="print"></param>
        public void SavePrintData(printModel print,string cid)
        {
            TF.RunSafty.Model.Tab_DeliverJSPrint modelPlan = new TF.RunSafty.Model.Tab_DeliverJSPrint();
            modelPlan.StrPlanGUID = print.strPlanGUID;
            modelPlan.StrSiteGUID = cid;
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
        public void SaveReadings(rData reading,string cid)
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
                                readingHistory.SiteGUID = cid;
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

        public  string RecordUpload(string input,string cid)
        {
            Upload_Out model = new Upload_Out();
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
                List<jsBase> array = Newtonsoft.Json.JsonConvert.DeserializeObject<List<jsBase>>(input, jvc);
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
                                SaveReadings(reading.data,cid);
                            }
                            else if (t is printData)
                            {
                                printData print = (printData)t;
                                SavePrintData(print.data,cid);
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
        #endregion
    }
}
