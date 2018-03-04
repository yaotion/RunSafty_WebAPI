using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TF.CommonUtility;


namespace TF.RunSafty.Trainman
{
    public class PSTrainman
    {
        #region   Base64和数组之间的转换
        public static object ConvertBase64ToByte(object str)
        {
            //将url解码
            string BeStr = System.Web.HttpUtility.UrlDecode(str.ToString());
            //转换成byte数组
            return Convert.FromBase64String(str.ToString());
        }
        public static string ToBase64String(object o)
        {
            byte[] b = o as byte[];
            if (b == null)
            {
                return "";
            }
            else
            {

                return Convert.ToBase64String(b);
            }


        }
        #endregion

        public static void TrainmanFromDB(VTrainman model, DataRow dr,int Option)
        {
            model.strTrainmanNumber = ObjectConvertClass.static_ext_string(dr["strTrainmanNumber"]);
            model.strTrainmanName = ObjectConvertClass.static_ext_string(dr["strTrainmanName"]);
            model.strTelNumber = ObjectConvertClass.static_ext_string(dr["strTelNumber"]);
            model.strRemark = ObjectConvertClass.static_ext_string(dr["strRemark"]);
            model.nID = ObjectConvertClass.static_ext_int(dr["nID"]);
            model.dtCreateTime = ObjectConvertClass.static_ext_date(dr["dtCreateTime"]);

            if (Option == 0)//都不要
            {
                model.FingerPrint1 = "";
                model.Picture = "";
                model.FingerPrint2 = "";
            }
            else if (Option == 1)//只要指纹
            {
                model.FingerPrint1 = ToBase64String(dr["FingerPrint1"]);
                model.FingerPrint2 = ToBase64String(dr["FingerPrint2"]);
                model.Picture = "";
            }
            else if (Option == 2)//要照片
            {
                model.Picture = ToBase64String(dr["Picture"]);
                model.FingerPrint1 = "";
                model.FingerPrint2 = "";
            }
            else if (Option == 3)//都要
            {

                model.FingerPrint1 = ToBase64String(dr["FingerPrint1"]);
                model.FingerPrint2 = ToBase64String(dr["FingerPrint2"]);
                model.Picture = ToBase64String(dr["Picture"]);
            }
            else
            {
                model.FingerPrint1 = "错误的类别";
                model.Picture = "错误的类别";
                model.FingerPrint2 = "错误的类别";
            }
            model.nTrainmanState = ObjectConvertClass.static_ext_int(dr["nTrainmanState"]);
            model.nPostID = ObjectConvertClass.static_ext_int(dr["nPostID"]);
            model.strWorkShopGUID = ObjectConvertClass.static_ext_string(dr["strWorkShopGUID"]);
            model.strGuideGroupGUID = ObjectConvertClass.static_ext_string(dr["strGuideGroupGUID"]);
            model.strMobileNumber = ObjectConvertClass.static_ext_string(dr["strMobileNumber"]);
            model.strAddress = ObjectConvertClass.static_ext_string(dr["strAddress"]);
            model.strTrainmanGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanGUID"]);
            model.nDriverType = ObjectConvertClass.static_ext_int(dr["nDriverType"]);
            model.bIsKey = ObjectConvertClass.static_ext_int(dr["bIsKey"]);
            model.dtRuZhiTime = ObjectConvertClass.static_ext_date(dr["dtRuZhiTime"]);
            model.dtJiuZhiTime = ObjectConvertClass.static_ext_date(dr["dtJiuZhiTime"]);
            model.nDriverLevel = ObjectConvertClass.static_ext_int(dr["nDriverLevel"]);
            model.strABCD = ObjectConvertClass.static_ext_string(dr["strABCD"]);
            model.nKeHuoID = ObjectConvertClass.static_ext_string(dr["nKeHuoID"]);
            model.strTrainJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluGUID"]);
            model.dtLastEndWorkTime = ObjectConvertClass.static_ext_date(dr["dtLastEndWorkTime"]);
            model.nDeleteState = ObjectConvertClass.static_ext_int(dr["nDeleteState"]);
            model.strTrainJiaoluName = ObjectConvertClass.static_ext_string(dr["strTrainJiaoluName"]);
            model.strGuideGroupName = ObjectConvertClass.static_ext_string(dr["strGuideGroupName"]);
            model.strWorkShopName = ObjectConvertClass.static_ext_string(dr["strWorkShopName"]);
            model.strAreaName = ObjectConvertClass.static_ext_string(dr["strAreaName"]);
            model.strJWDNumber = ObjectConvertClass.static_ext_string(dr["strJWDNumber"]);
            model.strJP = ObjectConvertClass.static_ext_string(dr["strJP"]);
            model.strTrainmanJiaoluGUID = ObjectConvertClass.static_ext_string(dr["strTrainmanJiaoluGUID"]);
            model.strareaguid = ObjectConvertClass.static_ext_string(dr["strareaguid"]);
            model.dtLastInRoomTime = ObjectConvertClass.static_ext_date(dr["dtLastInRoomTime"]);
            model.dtLastOutRoomTime = ObjectConvertClass.static_ext_date(dr["dtLastOutRoomTime"]);
        }
    }

    #region 输出类 类方法
    public class InterfaceRet
    {
        public int result;
        public string resultStr;
        public object data;
        public void Clear()
        {
            result = 0;
            resultStr = "执行成功！";
            data = null;
        }
    }
    #endregion


  


}
