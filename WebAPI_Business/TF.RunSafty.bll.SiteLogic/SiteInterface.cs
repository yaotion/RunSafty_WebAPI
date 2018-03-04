using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Drawing;

namespace TF.RunSafty.SiteLogic
{
    public class SiteInterface
    {
        #region ========================提交入寓记录========================================
        public class InterfaceResult
        {
            public int result;
            public string resultStr;
        }

       
        public InterfaceResult SubmitInRoom(string input)
        {
            InterfaceResult jsonModel = new InterfaceResult();
            try
            {
                input = HttpUtility.UrlDecode(input);
                SubmitInOutRoom paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<SubmitInOutRoom>(input);

                DBSiteLogic dbSiteLogic = new DBSiteLogic();

                dbSiteLogic.SubmitInRoom(paramModel);

                
                jsonModel.result = 0;
                jsonModel.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                jsonModel.resultStr = "提交失败入寓事件失败:" + ex.InnerException.Message.ToString();
            }
            return jsonModel;
        }
        #endregion

        #region ========================提交出寓记录========================================
        public InterfaceResult SubmitOutRoom(string input)
        {
            input = HttpUtility.UrlDecode(input);
            InterfaceResult jsonModel = new InterfaceResult();
            try
            {
                SubmitInOutRoom paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<SubmitInOutRoom>(input);

                DBSiteLogic dbSiteLogic = new DBSiteLogic();

                dbSiteLogic.SubmitOutRoom(paramModel);

                jsonModel.result = 0;
                jsonModel.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                if (ex.InnerException != null)
                {
                    jsonModel.resultStr = "提交离寓事件失败:" + ex.InnerException.Message.ToString();
                }
                else
                {
                    jsonModel.resultStr = "提交离寓事件失败:" + ex.Message.ToString();
                }
                
            }
            return jsonModel;
        }
        #endregion

        #region ========================提交测酒记录=======================================
        public InterfaceResult SubmitDrinkRec(string input)
        {
            input = HttpUtility.UrlDecode(input);
            InterfaceResult jsonModel = new InterfaceResult();
            try
            {
                SubmitDrinkRec paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<SubmitDrinkRec>(input);
                
                DBSiteLogic dbSiteLogic = new DBSiteLogic();

                dbSiteLogic.SubmitDrinkRecord(paramModel);

                jsonModel.result = 0;
                jsonModel.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                jsonModel.result = 1;
                jsonModel.resultStr = "提交测酒事件失败" + ex.InnerException.Message.ToString();
            }
            return jsonModel;
        }
        #endregion

        #region ========================提交照片========================================

        public InterfaceResult SubmitDrinkPic(string input)
        {
            input = HttpUtility.UrlDecode(input);
            InterfaceResult jsonModel = new InterfaceResult();
            try
            {
                DrinkPic paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DrinkPic>(input);
                string logicPath = "/DrinkImage/" + paramModel.etime.Year.ToString() + "/"
                    + paramModel.etime.Month.ToString() + "/"
                    + paramModel.etime.Day.ToString() + "/";

                string phsycalPath = HttpContext.Current.Server.MapPath(logicPath);

                if (!Directory.Exists(phsycalPath))
                {
                    Directory.CreateDirectory(phsycalPath);
                }

                string fileName = phsycalPath + paramModel.testid + ".jpg";

                byte[] imageBytes = Convert.FromBase64String(paramModel.pic);
              

                FileStream fs = File.Create(fileName);
                try
                {
                    fs.Write(imageBytes, 0, imageBytes.Length);
                    fs.Flush();
                    jsonModel.result = 0;
                    jsonModel.resultStr = "返回成功";
                }
                finally
                {                    
                    fs.Close();
                }

                
                
            }
            catch (Exception ex)
            {
                //TF.CommonUtility.LogClass.log(ex.Message.ToString());
                jsonModel.result = 1;
                jsonModel.resultStr = "提交失败" + ex.Message.ToString();
            }
            return jsonModel;
        }
        #endregion


        


    }
}