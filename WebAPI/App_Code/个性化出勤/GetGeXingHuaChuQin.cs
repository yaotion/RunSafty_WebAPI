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

namespace TF.RunSaftyAPI.App_Api.Public
{
    /// <summary>
    ///GetGeXingHuaChuQin 的摘要说明
    /// </summary>
    public class GetGeXingHuaChuQin : IQueryResult
    {

        public class PSpecificOnduty
        {
            public List<gh> gh;
            public string cx;
            public string ch;
            public string xcjl;
            public string cc;
            public string cid;
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

        internal class JsonModel
        {
            public string result;
            public string resultStr;
            public string strPDFURL;
            public string strExcelURL;
        }

        //零碎修信息上传参数
        internal class Datas
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


        public override string QueryResult()
        {
            TF.RunSafty.DAL.ExcelAdd ea = new TF.RunSafty.DAL.ExcelAdd();
            string strcx = "";//车型
            string strch = "";//车号
            string strxcjl = "";//行车交路
            string strgh = "";//工号字符串
            string strcc = "";//车次
            string strcid = "";


            PSpecificOnduty paramModel = TF.CommonUtility.JsonConvert.JsonDeserialize<PSpecificOnduty>(this.Data);
            strcx = paramModel.cx;//车型
            strch = paramModel.ch;//车号
            strxcjl = HttpUtility.UrlDecode(paramModel.xcjl);//行车交路
            strcc = paramModel.cc;//车次
            strcid = paramModel.cid;//客户端id
           


            strgh = "";//工号字符串 

            foreach (gh g in paramModel.gh)
            {
                if (g.ghID.Length == 7)
                {
                    strgh += g.ghID.Substring(2, 5) + ",";
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
                strTianQi += "&nbsp;" + dtTianQi.Rows[t]["strCheZhan"].ToString();
                strTianQi += "&nbsp;" + dtTianQi.Rows[t]["strTianQi"].ToString();
                strTianQi += "&nbsp;" + dtTianQi.Rows[t]["strWenDu"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            }

            //预警，操作要求，车间要求
            DataTable dtYuJin = ea.GetYuJing(strxcjl);
            string strYuJing = "";
            if (dtYuJin.Rows.Count > 0)
            {
                strYuJing += "<tr><td>关键站：" + dtYuJin.Rows[0]["strGuanJianZhan"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;操纵要求：" + dtYuJin.Rows[0]["strCaoZongYaoQiu"].ToString() + "</td></tr>";
                strYuJing += "<tr><td>预警：" + dtYuJin.Rows[0]["strYuJin"].ToString() + "</td></tr>";
            }
            else
            {
                strYuJing = "";
            }
            //人员信息，考试情况,两违通报
            string strRenYuan = "";
            for (int sp = 0; sp < strgh.Split(',').Length; sp++)
            {
                DataTable dtRenYuan = ea.GetRenYuan(strgh.Split(',')[sp]);
                string strRenYuan1 = "";
                string strRenYuan2 = "";
                string strRenYuan3 = "";
                string strRenYuan4 = "";
                if (dtRenYuan.Rows.Count > 0)
                {
                    strRenYuan1 = "<tr><td>姓名：" + dtRenYuan.Rows[0]["strName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;";
                    strRenYuan2 = "重点人：" + dtRenYuan.Rows[0]["strZhongDianPerple"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;";
                    strRenYuan3 = "车间要求：" + dtRenYuan.Rows[0]["strCheJianYaoQiu"].ToString() + "</td></tr>";
                    strRenYuan4 = "<tr><td>人性化提示：" + dtRenYuan.Rows[0]["strJianKang"].ToString() + "</td></tr>";
                }
                strRenYuan += strRenYuan1;
                strRenYuan += strRenYuan2;
                strRenYuan += strRenYuan3;
                strRenYuan += "<tr><td>" + TF.CommonUtility.CommonHelper.RenderHtml("Tab_Test.htm", AllTasts(strgh.Split(',')[sp])) + "</td></tr>";
                strRenYuan += "<tr><td>" + TF.CommonUtility.CommonHelper.RenderHtml("Tab_SpecificOnDuty_LiangWei.htm", ea.GetAllLiangWei(strgh.Split(',')[sp]).Rows) + "</td></tr>";
                strRenYuan += strRenYuan4;
                strRenYuan += "<tr><td>&nbsp;</td></tr>";
            }
            //机车临碎修

            //DataTable dtLinSuiXiu = ea.GetLinSuixiu(strcx, strch);
            //string strLinSuiXiu = "<tr><td>" + TF.CommonUtility.CommonHelper.RenderHtml("Tab_SpecificOnDuty_LingSuiXiu.htm", dtLinSuiXiu.Rows) + "</td></tr>";

            string strLinSuiXiu = "<tr><td>" + TF.CommonUtility.CommonHelper.RenderHtml("Tab_SpecificOnDuty_LingSuiXiu.htm", AllLingSuiXiu(strcx, strch)) + "</td></tr>";

            html += "<div style='border:solid 1px #000'><table><tr><td><table  style='width:100%; font-size:12px;text-decoration:underline'><tr><td align='center'><b>个性提示</b></td></tr></table></td></tr>";
            html += "<tr><td align='left'>" + strxcjl + "&nbsp;&nbsp;&nbsp;&nbsp;车次：" + strcc + "</td></tr>";
           
            html += "<tr><td>天气预报：" + strTianQi + "</td></tr>";
            html += strRenYuan;
            html += "<tr><td><table style='width:100%; font-size:12px;text-decoration:underline; line-height:1.2'><tr><td align='center'><b>机车状态</b></td></tr></table></td></tr>";
            html += strLinSuiXiu;
            html += "<tr><td><table style='width:100%; font-size:12px;text-decoration:underline; line-height:1.2'><tr><td align='center'><b>预警提示</b></td></tr></table></td></tr>";
            html += strYuJing;
            html += "</table></div>";

            string strfilePath = "/SpecificOnDuty";
            CreatHtmlFile(System.Web.Hosting.HostingEnvironment.MapPath(strfilePath), html, "" + strcid + ".htm");

            CreatPdf(strcid);

            //生成EXCEL
            string strExcelHtml = TF.CommonUtility.CommonHelper.RenderHtml("Excel.htm", "");
            strExcelHtml += "<img alt='' src='http://" + HttpContext.Current.Request.Url.Authority + "/SpecificOnDuty/" + strcid + ".bmp' width='80%' height='145%'  />";
            strExcelHtml += "</div></body></html>";
            CreatHtmlFile(System.Web.Hosting.HostingEnvironment.MapPath(strfilePath), strExcelHtml, "" + strcid + ".xls");
            JsonModel jsonModel = new JsonModel();
            jsonModel.result = "0";
            jsonModel.resultStr = "返回成功";
            Random r = new Random();
            jsonModel.strPDFURL = "http://" + HttpContext.Current.Request.Url.Authority + "/SpecificOnDuty/" + strcid + ".pdf?r=" + r.Next(1, 2000) + "";
            jsonModel.strExcelURL = "http://" + HttpContext.Current.Request.Url.Authority + "/SpecificOnDuty/" + strcid + ".xls?r=" + r.Next(1, 2000) + "";
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(jsonModel);
            return result;

        }

        #region 生成pdf的代码

        private System.Drawing.Bitmap bitmap;
        private string url;
        private int w = 660, h = 1000;
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
            Document doc = new Document(PageSize.B5, 0, 0, 0, 0);//左右上下
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
                string strUrl = ConstCommon.TestIPString + "?UserNumber=33" + userNumber + "";
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
                            strAnswers += op.Name + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

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
}