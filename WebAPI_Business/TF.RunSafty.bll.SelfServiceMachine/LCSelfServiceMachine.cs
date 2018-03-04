using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Text;
using System.Security.Policy;
using System.Windows.Forms;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;
using TF.RunSafty.SpecificOnDuty;
namespace TF.RunSafty.SelfServiceMachine
{
    #region 个性化出勤操作类库
    public class LCSpecificOnDuty
    {
        public class PSpecificOnduty
        {
            public List<gh> gh;
            public string cx;
            public string ch;
            public string xcjl;
            public string cc;
            public string cid;
            public string remarkTypeName;

        }
        public class gh
        {
            public string ghID;
        }
        //获取考试记录
        public class test
        {
            public string result;
            public string resultStr;
            public data data;
        }

        public class data
        {
            public List<ArrayError> ArrayError;
        }

        public class ArrayError
        {
            public string strTestName;
            public string strSelectAnswer;
            public string strTrueAnswer;
            public List<OPtion> OPtion;
        }

        public class OPtion
        {
            public string Name;
        }

        public class strAllTest
        {
            public string strName { get; set; }
            public string strTrue { get; set; }
            public string strSelect { get; set; }
            public string Answers { get; set; }
        }

        public class ResultData
        {
            public string strPDFURL;
            public string strExcelURL;
        }
        public class JsonModel
        {
            public string result;
            public string resultStr;
            public ResultData data;

        }

        public class strAllZhanJie
        {
            public int TrackId;//股道号
            public string TrackName;
        }
        //站接输出数据
        public class ZhanOrKu
        {
            public string Success;
            public string ResultText;
            public List<strAllZhanJie> Items;

        }

        //零碎修信息上传参数
        public class Datas
        {
            public string LocoType;
            public string LocoNum;
            public string rows;
        }

        //零碎修输出数据
        public class strLingSuiXiuResult
        {
            public string Success;
            public string ResultText;
            public List<strAllLinSuiXiu> Items;
        }

        public class strAllLinSuiXiu
        {
            public string PresenterDate { get; set; }//机车提出日期
            public string Question { get; set; }//机车反应问题
            public string StrCheXing { get; set; }
            public string StrCheHao { get; set; }
        }

        public JsonModel Get(string Data)
        {

            string strcx = "";//车型
            string strch = "";//车号
            string strxcjl = "";//行车交路
            string strgh = "";//工号字符串
            string strcc = "";//车次
            string strcid = "";
            string remarkTypeName = "";//站接，库接


            PSpecificOnduty paramModel = TF.CommonUtility.JsonConvert.JsonDeserialize<PSpecificOnduty>(Data);
            strcx = paramModel.cx;//车型
            strch = paramModel.ch;//车号
            strxcjl = HttpUtility.UrlDecode(paramModel.xcjl);//行车交路
            strcc = paramModel.cc;//车次
            strcid = paramModel.cid;//客户端id
            remarkTypeName = paramModel.remarkTypeName;

            if (string.IsNullOrEmpty(strcid))
            {
                return FengRunSpecific(strgh, strxcjl, strcid, strcx, strch, strcc, remarkTypeName, paramModel);
            }
            else
            {
                DBSpecificOnDuty ea = new DBSpecificOnDuty();
                string strWorkShopGUID = ea.getWorkShopGUID(strcid);
                if (strWorkShopGUID == "292be3ca-b357-4386-80dc-eb148e5a595e")
                {
                    return FengRunSpecific(strgh, strxcjl, strcid, strcx, strch, strcc, remarkTypeName, paramModel);

                }
                else if (strWorkShopGUID == "3b50bf66-dabb-48c0-8b6d-05db80591090")
                {
                    SpecificOnDuty_ts ts = new SpecificOnDuty_ts();
                    return ts.TangShanSpecific(strgh, strxcjl, strcid, strcx, strch, strcc, paramModel);
                }
                else
                {
                    return FengRunSpecific(strgh, strxcjl, strcid, strcx, strch, strcc, remarkTypeName, paramModel);
                }


            }
        }




        #region  获取丰润车间的个性化出勤
        public JsonModel FengRunSpecific(string strgh, string strxcjl, string strcid, string strcx, string strch, string strcc, string remarkTypeName, PSpecificOnduty paramModel)
        {
            DBSpecificOnDuty ea = new DBSpecificOnDuty();
            strgh = "";//工号字符串 
            foreach (gh g in paramModel.gh)
            {
                if (g.ghID.Length == 7)
                {
                    strgh += g.ghID + ",";
                }
                else
                {

                }
            }
            strgh = strgh.Substring(0, strgh.Length - 1);

            //查询完毕后渲染html
            string html = TF.CommonUtility.CommonHelper.RenderHtml("top.htm", "");//头部的html代码

            //获取关键站天气预报信息
            DataTable dtTianQi = ea.GetAllTianQi(strxcjl);
            string strTianQi = "";
            for (int t = 0; t < dtTianQi.Rows.Count; t++)
            {
                strTianQi += "<tr><td class='Juleft' style=''>" + dtTianQi.Rows[t]["strCheZhan"].ToString();
                strTianQi += "&nbsp;" + dtTianQi.Rows[t]["strTianQi"].ToString();
                strTianQi += "&nbsp;" + dtTianQi.Rows[t]["strWenDu"].ToString() + "</td></tr>";
            }

            //预警，操作要求，车间要求
            DataTable dtYuJin = ea.GetYuJing(strxcjl);
            string strYuJing = "";
            if (dtYuJin.Rows.Count > 0)
                strYuJing += "<tr><td class='Juleft'  style='font-weight:400;line-height:1.5; '>&nbsp;&nbsp;" + dtYuJin.Rows[0]["strChuanDa"].ToString() + "</td></tr>";
            else
                strYuJing = "";
            //事故传达
            DataTable dtShiGu = ea.GetShiGu();
            string strShiGu = "";
            if (dtShiGu.Rows.Count > 0)
            {
                strShiGu += "<tr><td class='Juleft' style='font-size:14px;line-height:1.2'>七、事故通报</td></tr>";
                for (int kk = 0; kk < dtShiGu.Rows.Count; kk++)
                {
                    strShiGu += "<tr><td class='Juleft'>" + (kk + 1).ToString() + "、" + dtShiGu.Rows[kk]["strShiGu"].ToString() + "</td></tr>";
                }
            }



            //人员信息，考试情况,两违通报
            string strRenYuan = "<tr><td  align='left'  style='font-weight:400;line-height:1.5;'>";


            DataTable dtRenYuan = ea.GetRenYuan(strgh.Split(',')[0]);

            string strRenYuan1 = "";
            if (dtRenYuan.Rows.Count > 0)
            {

                strRenYuan1 += "&nbsp;司机:" + dtRenYuan.Rows[0]["strTrainmanName1"].ToString() + "&nbsp;";
                strRenYuan1 += "副(换乘)司机:" + dtRenYuan.Rows[0]["strTrainmanName2"].ToString();
            }
            strRenYuan += strRenYuan1;

            strRenYuan += "</td></tr>";

            //机车临碎修
            string strLinSuiXiu = "<tr><td align='left' >" + TF.CommonUtility.CommonHelper.RenderHtml("Tab_SpecificOnDuty_LingSuiXiu.htm", AllLingSuiXiu(strcx, strch)) + "</td></tr>";


            html += "<div style='border:solid 0px #000;padding:5px;'><table style='width:100%; font-size:14px;'>";
            html += "<tr><td class='Juleft' style='font-weight:400;line-height:1.5; '>一、出勤时间：" + DateTime.Now.ToString("yy年MM月dd日HH:mm") + "</td></tr>";

            html += "<tr><td class='Juleft' style='font-weight:400;line-height:1.5;'>二、出勤机班：</td></tr>" + strRenYuan;

            html += "<tr><td align='left' class='Juleft' style='font-weight:400;line-height:1.5; '>三、担当区段&nbsp;车次：" + strxcjl + "&nbsp;" + strcc + "次</td></tr>";
            html += "<tr><td align='left'  style=' font-weight:400; line-height:2'>" + getZhanOrKu(strcx, strch, remarkTypeName) + "</td></tr>";
            html += "<tr><td class='Juleft'>五、使用机车质量信息&nbsp;" + strcx + "&nbsp;-&nbsp;" + strch + "</td></tr>";
            html += strLinSuiXiu;
            html += "<tr><td  class='Juleft'>六、安全提示</td></tr>";
            html += strYuJing;
            html += strShiGu;
            html += "<tr><td class='Juleft' style='font-size:14px;line-height:1.2'>八、天气预报</td></tr>";
            html += strTianQi;
            html += "<tr><td style=' font-weight:400; line-height:1.2;text-align:center;font-size: 20px' ><b>" + ea.GetYanYu() + "</b></td></tr>";
            html += "</table></div>";

            string strfilePath = "/SpecificOnDuty";
            CreatHtmlFile(System.Web.Hosting.HostingEnvironment.MapPath(strfilePath), html, "" + strcid + ".htm");

            CreatPdf(strcid);

            //生成EXCEL
            string strExcelHtml = "";
            strExcelHtml += html;
            strExcelHtml += "</td></tr></table></body></html>";
            CreatHtmlFile(System.Web.Hosting.HostingEnvironment.MapPath(strfilePath), strExcelHtml, "" + strcid + ".xls");
            JsonModel jsonModel = new JsonModel();
            jsonModel.result = "0";
            jsonModel.resultStr = "返回成功";
            Random r = new Random();
            jsonModel.data = new ResultData();
            jsonModel.data.strPDFURL = "http://" + HttpContext.Current.Request.Url.Authority + "/SpecificOnDuty/" + strcid + ".pdf?r=" + r.Next(1, 2000) + "";
            jsonModel.data.strExcelURL = "http://" + HttpContext.Current.Request.Url.Authority + "/SpecificOnDuty/" + strcid + ".xls?r=" + r.Next(1, 2000) + "";
            return jsonModel;
        }
        #endregion

        #region 生成pdf的代码

        private System.Drawing.Bitmap bitmap;
        private string url;
        private int w = 660, h = 1200;
        public void setBitmap()
        {
            using (WebBrowser wb = new WebBrowser())
            {
                wb.Width = w;
                wb.Height = h;
                wb.ScrollBarsEnabled = false;
                wb.Navigate(url);
                //确保页面被解析完全
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
                bitmap = new System.Drawing.Bitmap(w, h);
                wb.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, w, h));
                wb.Dispose();
            }
        }
        private void CreatPdf(string cid)
        {
            Document doc = new Document(PageSize.A4, 0, 0, 0, 0);//左右上下
            MemoryStream ms = new MemoryStream();
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                writer.CloseStream = false;
                doc.Open();
                url = System.Web.Hosting.HostingEnvironment.MapPath("/SpecificOnDuty/" + cid + ".htm");
                Thread thread = new Thread(new ThreadStart(setBitmap));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                while (thread.IsAlive)
                    Thread.Sleep(100);
                bitmap.Save(System.Web.Hosting.HostingEnvironment.MapPath("/SpecificOnDuty/" + cid + ".bmp"));

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bitmap, System.Drawing.Imaging.ImageFormat.Bmp);
                img.ScalePercent(75);//560 630
                doc.Add(img);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                doc.Close();
                using (FileStream fs = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("/SpecificOnDuty/" + cid + ".pdf"), FileMode.Create))
                {
                    ms.Position = 0;
                    byte[] bit = new byte[ms.Length];
                    ms.Read(bit, 0, (int)ms.Length);
                    fs.Write(bit, 0, bit.Length);
                }
            }
        }



        public static void CreatHtmlFile(string FilePath, string content, string fileName)
        {
            if (Directory.Exists(FilePath) == false)
            {
                Directory.CreateDirectory(FilePath);
            }
            using (StreamWriter _mstreamwriter = new StreamWriter(FilePath + "\\" + fileName, false, System.Text.UnicodeEncoding.GetEncoding("UTF-8")))
            {
                _mstreamwriter.WriteLine(content);
                _mstreamwriter.Flush();
                _mstreamwriter.Close();
            }

        }

        #endregion

        #region 获取考试记录的代码
        public List<strAllTest> AllTasts(string userNumber)
        {
            try
            {
                string strUrl = ConstCommon.TestIPString + "?UserNumber=23" + userNumber + "";
                Uri uri = new Uri(strUrl);
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.AllowAutoRedirect = false;
                request.Timeout = 5000;
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                string retext = readStream.ReadToEnd().ToString();
                readStream.Close();
                test testModel = TF.CommonUtility.JsonConvert.JsonDeserialize<test>(retext);
                List<strAllTest> strAllTest = new List<strAllTest>();
                if (Convert.ToInt32(testModel.result) == 1)
                {
                }
                else
                {
                    data da = testModel.data;
                    int n = 0;
                    foreach (ArrayError a in da.ArrayError)
                    {
                        if (n >= 3)
                        {
                            break;
                        }
                        else
                        {
                            n++;
                        }
                        if (a.strTrueAnswer == "F")
                            a.strTrueAnswer = "错误";
                        else if (a.strTrueAnswer == "T")
                            a.strTrueAnswer = "正确";
                        else
                        { }
                        if (a.strSelectAnswer == "F")
                            a.strSelectAnswer = "错误";
                        else if (a.strSelectAnswer == "T")
                            a.strSelectAnswer = "正确";
                        else
                        { }
                        string strAnswers = "";
                        foreach (OPtion op in a.OPtion)
                            strAnswers += op.Name + "&nbsp;";

                        strAllTest.Add(new strAllTest { strName = a.strTestName, strTrue = a.strTrueAnswer, strSelect = a.strSelectAnswer, Answers = strAnswers });
                    }
                }
                return strAllTest;
            }
            catch
            {
                return null;
            }
            //获取考试记录结束
        }
        #endregion

        #region 获取站接或者库接信息
        public string getZhanOrKu(string cx, string ch, string remarkTypeName)
        {
            Datas das = new Datas();
            das.LocoType = cx;
            das.LocoNum = ch;
            das.rows = "3";
            string strDas = Newtonsoft.Json.JsonConvert.SerializeObject(das);
            try
            {
                string strUrl = "";
                if (remarkTypeName == "库接")
                {
                    strUrl = ConstCommon.JiCheWeiZiZhanJieIPString + "?DataType=36&Data=" + strDas + "";
                    Uri uri = new Uri(strUrl);
                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
                    request.Method = "GET";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.AllowAutoRedirect = false;
                    request.Timeout = 5000;
                    System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    string retext = readStream.ReadToEnd().ToString();
                    readStream.Close();
                    ZhanOrKu testModel = TF.CommonUtility.JsonConvert.JsonDeserialize<ZhanOrKu>(retext);
                    if (Convert.ToInt32(testModel.Success) == 0)
                    {
                        return "获取站接信息失败！";
                    }
                    else
                    {
                        foreach (strAllZhanJie a in testModel.Items)
                        {
                            return "四、接车类型：库接&nbsp; 机车位置：" + a.TrackName.ToString() + "道";
                        }
                    }
                }
                else if (remarkTypeName == "站接")
                {
                    strUrl = ConstCommon.JiCheWeiZiKuJieIPString + "?traintype=" + cx + "&trainid=" + ch + "";
                    Uri uri = new Uri(strUrl);
                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
                    request.Method = "GET";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.AllowAutoRedirect = false;
                    request.Timeout = 5000;
                    System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    string retext = readStream.ReadToEnd().ToString();
                    readStream.Close();
                    return "四、接车类型：站接&nbsp; 机车位置：" + retext;
                }
                else
                {
                    strUrl = "";
                    return "尚未传入机车位置信息";
                }
            }
            catch
            {
                return "四、接车类型：获取机车类型失败，请联系畅想高科现场服务人员";
            }





            return "";
        }

        #endregion

        #region 获取零碎修的代码
        public List<strAllLinSuiXiu> AllLingSuiXiu(string cx, string ch)
        {

            Datas das = new Datas();
            das.LocoType = cx;
            das.LocoNum = ch;
            das.rows = "3";
            string strDas = Newtonsoft.Json.JsonConvert.SerializeObject(das);

            try
            {
                string strUrl = ConstCommon.LingSuiXiuIPString + "?DataType=69&Data=" + strDas + "";
                Uri uri = new Uri(strUrl);
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.AllowAutoRedirect = false;
                request.Timeout = 5000;
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                string retext = readStream.ReadToEnd().ToString();
                readStream.Close();
                strLingSuiXiuResult testModel = TF.CommonUtility.JsonConvert.JsonDeserialize<strLingSuiXiuResult>(retext);
                List<strAllLinSuiXiu> AllLinSuiXiu = new List<strAllLinSuiXiu>();
                if (Convert.ToInt32(testModel.Success) == 0)
                {
                }
                else
                {
                    foreach (strAllLinSuiXiu a in testModel.Items)
                    {
                        AllLinSuiXiu.Add(new strAllLinSuiXiu { PresenterDate = a.PresenterDate, Question = a.Question, StrCheXing = cx, StrCheHao = ch });
                    }
                }
                return AllLinSuiXiu;

            }
            catch
            {
                return null;
            }
            //获取考试记录结束
        }
        #endregion


    }
    #endregion



    #region  记名式传达
    public class LCReadingRecord
    {

        public class ParamModel
        {
            public string strTrainmanGUID;
        }
        public class TypeList
        {
            public string strTypeGUID;
            public string strTypeName;
            public List<filelist> FileList;
        }

        public class filelist
        {
            public string strFileGUID;
            public string strFileName;
            public string dtReadTime;
        }

        public class InnerData
        {
            public List<TypeList> TypeList;
        }
        /*
        public class JsonData
        {
            public InnerData data;
        }*/
        public class JsonModel
        {
            public string result;
            public string returnStr;
            public InnerData data;
        }


        //获取记名式传达
        public JsonModel Get(string Data)
        {
            JsonModel model = new JsonModel();
            DataTable table = null;
            //JsonData data = new JsonData();
            List<TypeList> typeList = new List<TypeList>();
            InnerData data = new InnerData();
            data.TypeList = typeList;
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(Data);
            DBReadingRecord drr = new DBReadingRecord();
            try
            {
                //string where = string.Format(" strTypeName='{0}'", "记名式传达");
                table = drr.GetList("").Tables[0];

                //table = bllGroup.GetAllList().Tables[0];
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        TypeList list = new TypeList();
                        list.strTypeGUID = row["strTypeGUID"].ToString();
                        list.strTypeName = row["strTypeName"].ToString();
                        list.FileList = drr.GetReadingHistoryOfTrainman(param.strTrainmanGUID, list.strTypeGUID);
                        typeList.Add(list);
                    }
                }
                model.result = "0";
                model.returnStr = "返回成功";
                model.data = data;
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                model.result = "1";
                model.returnStr = ex.Message.ToString();
            }
            return model;

        }

        public class Reading
        {
            public rData data;
        }

        public class rData
        {
            public List<fReader> ReaderArray;
            public List<fTypeList> TypeList;
        }

        public class fReader
        {
            public string TrainmanGUID;
            public string cid;
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

        // 保存阅读记录
        public JsonModel SaveReadings(string Data)
        {
            MDSelfServiceMachine.MDReadHistory readingHistory = new MDSelfServiceMachine.MDReadHistory();
            DBReadingRecord dbs = new DBReadingRecord();
            Reading reading = Newtonsoft.Json.JsonConvert.DeserializeObject<Reading>(Data);
            JsonModel model = new JsonModel();
            try
            {
                if (reading.data != null && reading.data.ReaderArray != null)
                {
                    foreach (fReader trainman in reading.data.ReaderArray)
                    {
                        if (reading.data.TypeList != null)
                        {
                            foreach (fTypeList fType in reading.data.TypeList)
                            {
                                foreach (rFile file in fType.FileList)
                                {
                                    readingHistory = new MDSelfServiceMachine.MDReadHistory();
                                    readingHistory.SiteGUID = trainman.cid;
                                    readingHistory.strTrainmanGUID = trainman.TrainmanGUID;
                                    readingHistory.strFileGUID = file.strFileGUID;
                                    readingHistory.DtReadTime = file.dtReadTime;
                                    dbs.AddReadHistory(readingHistory);
                                    //更新阅读计划里边的第一次阅读时间、阅读次数
                                    dbs.UpdateReadTime(file.strFileGUID, trainman.TrainmanGUID, file.dtReadTime);
                                }
                            }
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
            return model;
        }



        public class JsonModelForReadingSync
        {
            public string result;
            public string returnStr;
            public DataTable TypeList;
            public DataTable FileList;
        }
        public class ParamModelIReadingSync
        {
            public string clientid;
        }
        //通过客户端编号  获取所有文件列表 以及所有的文件类型
        public JsonModelForReadingSync IReadingSync(string Data)
        {
            JsonModelForReadingSync model = new JsonModelForReadingSync();
            DataTable table = null;
            DataTable tblFile = null;
            ParamModelIReadingSync param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModelIReadingSync>(Data);
            SelfServiceMachine.DBReadingRecord bllFileGroup = new SelfServiceMachine.DBReadingRecord();
            DBReadingRecord bllFile = new DBReadingRecord();
            try
            {
                table = bllFileGroup.GetListForReadingSync("").Tables[0];
                tblFile = bllFile.GetAllListWithFileType(param.clientid).Tables[0];
                model.result = "0";
                model.returnStr = "返回成功";
                model.TypeList = table;
                model.FileList = tblFile;
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                model.result = "0";
                model.returnStr = "返回失败" + ex.ToString();
            }
            return model;

        }


    }
    #endregion

    #region  打印
    public class LCPrint
    {
        public class printData
        {
            public printModel data;
        }
        public class printModel
        {
            public string dtPrintTime;
            public string strTrainmanGUID;
            public string strPlanGUID;
            public string cid;
        }

        public class fReader
        {
            public string TrainmanGUID;
            public string cid;
        }


        public class JsonModel
        {
            public string result;
            public string returnStr;
        }

        /// 保存打印记录
        /// </summary>
        /// <param name="print"></param>
        public JsonModel SavePrintData(string Data)
        {
            DBDeliverJSPrint dbs = new DBDeliverJSPrint();
            printData print = Newtonsoft.Json.JsonConvert.DeserializeObject<printData>(Data);
            JsonModel model = new JsonModel();
            MDSelfServiceMachine.MDDeliverJSPrint modelPlan = new MDSelfServiceMachine.MDDeliverJSPrint();
            try
            {
                modelPlan.StrPlanGUID = print.data.strPlanGUID;
                modelPlan.StrSiteGUID = print.data.cid;
                modelPlan.StrTrainmanGUID = print.data.strTrainmanGUID;
                string strPrintTime = print.data.dtPrintTime;
                DateTime dtPrint;
                if (DateTime.TryParse(strPrintTime, out dtPrint))
                {
                    modelPlan.dtPrintTime = dtPrint;
                }
                dbs.AddDeliverJSPrint(modelPlan);
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                model.result = "1";
                model.returnStr = "提交失败:" + ex.Message;
            }
            return model;
        }

        public class ParamModel
        {
            public string cid;
            public string strPlanGUID;
            public string strTrainmanGUID;
            public string dtBeginWorkTime;
        }

        public class JsonModelJsPrintCheck
        {
            public string result;
            public string returnStr;
            public string bPrint;
        }

        /// <summary>
        /// 判断交付揭示是否已经打印
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public JsonModelJsPrintCheck JsPrintCheck(string Data)
        {
            JsonModelJsPrintCheck model = new JsonModelJsPrintCheck();
            ParamModel param = Newtonsoft.Json.JsonConvert.DeserializeObject<ParamModel>(Data);
            DBDeliverJSPrint dbDeliverJSPrint = new DBDeliverJSPrint();
            MDSelfServiceMachine.MDDeliverJSPrint modelPlan = new MDSelfServiceMachine.MDDeliverJSPrint();
            try
            {
                int bPrint = dbDeliverJSPrint.IsJsPrintable(param.strPlanGUID, param.strTrainmanGUID, param.dtBeginWorkTime);
                modelPlan.StrPlanGUID = param.strPlanGUID;
                modelPlan.StrSiteGUID = param.cid;
                modelPlan.StrTrainmanGUID = param.strTrainmanGUID;
                model.result = "0";
                model.returnStr = "返回成功";
                model.bPrint = bPrint.ToString();
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                model.result = "1";
                model.returnStr = "提交失败" + ex.Message;
            }
            return model;
        }




    }




    #endregion






}
