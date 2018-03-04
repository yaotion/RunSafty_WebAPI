using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using ThinkFreely.DBUtility;

    /// <summary>
    ///Operation made by 赵文龙 from 2013.9.16
    /// </summary>
public class Operation : System.Web.UI.Page
    {
        public Operation()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary> 功能：根据将测酒记录字段转化文文字描述</summary>
        public static string GetTestResultText(object TestResult)
        {
            try
            {
                int nTestID = Convert.ToInt32(TestResult);
                switch (nTestID)
                {
                    case 0:
                        return "正常";
                        break;
                    case 1:
                        return "饮酒";
                        break;
                    case 2:
                        return "酗酒";
                        break;
                    case 3:
                        return "未测试";
                        break;
                    case 4:
                        return "故障";
                        break;
                }
            }
            catch
            {
            };
            return "未测试";
        }
        public static string ctqRegisterType(string val)
        {
            if (val != "")
            {
                return val == "1" ? "指纹" : "工号";
            }
            return "";
        }

        public static string Cxdyb(string cx)
        {
            switch (cx)
            {
                case "235":
                    return "HXD2B";
                    break;
                case "104":
                    return "DF4";
                    break;
                case "111":
                    return "DF8";
                    break;
                case "232":
                    return "HXD2";
                    break;
                case "233":
                    return "HXD3";
                    break;
                case "231":
                    return "HXD1";
                    break;
                case "236":
                    return "HXD3B";
                    break;
                case "239":
                    return "HXD3C";
                    break;
                case "234":
                    return "HXD1B";
                    break;
            }
            return cx;
        }
    }