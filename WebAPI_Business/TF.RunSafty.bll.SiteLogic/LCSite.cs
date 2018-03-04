using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace TF.RunSafty.SiteLogic
{
    public class LCSite
    {
        //实例化返回数据
        InterfaceRet _ret = new InterfaceRet();

        #region 增删该操作的枚举数据
        public class OperationType
        {
            /// <summary>
            /// 增加
            /// </summary>
            public const string Insert = "1";
            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = "0";
            /// <summary>
            /// 修改
            /// </summary>
            public const string Update = "2";

        }
        #endregion

        #region 外点网站人员操作日志的增删该
        public class InputOperationLog
        {
            public string strUrlDescription { get; set; }
            public string Strid { get; set; }
            public int Pageid { get; set; }
            public DateTime OperationTime { get; set; }
            public int nid { get; set; }
            public string TrainmanNumber { get; set; }
            public string OperationType { get; set; }
        }
        public InterfaceRet OperationLog(string data)
        {
           
                _ret.Clear();
                data = System.Web.HttpUtility.UrlDecode(data);
                InputOperationLog input = Newtonsoft.Json.JsonConvert.DeserializeObject<InputOperationLog>( data);

                //判断所传对象是否为空
                if (input.OperationType == null)
                {
                    _ret.resultStr = "所传类型为空，无法执行下一步操作";
                    _ret.result = 1;
                    return _ret;
                }
                try
                {
                    DBSite db = new DBSite();
                    switch (input.OperationType)
                    {
                        case OperationType.Insert:
                            db.AddOL(input);
                            break;
                        case OperationType.Delete:
                            db.DelOL(input);
                            break;
                        case OperationType.Update:
                            db.UpdateOL(input);
                            break;
                        default:
                            throw new Exception("操作类型有误，请核对");
                    }
                }
                catch (Exception ex)
                {
                    _ret.resultStr = ex.Message;
                    _ret.result = 1;
                }
            return _ret;
        }
        #endregion

        #region 干部出入寓增删该
        public class InputGBInOutRoom
        {
            public string strDuty{ get; set; }
            public int nOutRoomLoginType{ get; set; }
            public int nIsOutRoom{ get; set; }
            public string strTypeName{ get; set; }
            public string strInRoomQuDuan{ get; set; }
            public string strTrainmanID{ get; set; }
            public string strCadresDutyTypeID{ get; set; }
            public string strID{ get; set; }
            public string strOutRoomQuDuan{ get; set; }
            public string strInRoomType{ get; set; }
            public string strInRoomPhotoID{ get; set; }
            public string strNumber{ get; set; }
            public int nSiteID{ get; set; }
            public string strJianChaContent{ get; set; }
            public DateTime? dtOutRoomDateTime{ get; set; }
            public DateTime? dtInputDateTime{ get; set; }
            public string strSiteNumber{ get; set; }
            public DateTime? dtInRoomDateTime{ get; set; }
            public int nInRoomLoginType{ get; set; }
            public DateTime? dtOutRoomCheCiDateTime{ get; set; }
            public string strDepartmentName{ get; set; }
            public string strInRoomCheCi{ get; set; }
            public string strRemark{ get; set; }
            public string strZhengGaiYiJian{ get; set; }
            public int nID{ get; set; }
            public string strCheJianName{ get; set; }
            public string strOutRoomCheCi{ get; set; }
            public string strName{ get; set; }
            public string strOutRoomPhotoID{ get; set; }
            public string strSiteName { get; set; }
            public string OperationType { get; set; }
        }
        public InterfaceRet GBInOutRoom(string data)
        {
            _ret.Clear();
            data = System.Web.HttpUtility.UrlDecode(data);
            InputGBInOutRoom input = Newtonsoft.Json.JsonConvert.DeserializeObject<InputGBInOutRoom>(data);
            //判断所传对象是否为空
            if (input.OperationType == null)
            {
                _ret.resultStr = "所传类型为空，无法执行下一步操作";
                _ret.result = 1;
                return _ret;
            }

            try
            {
                DBSite db = new DBSite();
                switch (input.OperationType)
                {
                    case OperationType.Insert:
                        db.AddInOutRoom(input);
                        break;
                    case OperationType.Delete:
                        db.DelInOutRoom(input);
                        break;
                    case OperationType.Update:
                        db.UpdateInOutRoom(input);
                        break;
                    default:
                        throw new Exception("操作类型有误，请核对");
                }
            }
            catch (Exception ex)
            {
                _ret.resultStr = ex.Message;
                _ret.result = 1;
            }
            return _ret;
        }
        #endregion

        #region 房间巡查记录
        public class InputRoomPatrol
        {
            public DateTime? dtPatrolTime{ get; set; }
            public int nSiteID{ get; set; }
            public string strSiteName{ get; set; }
            public int nID{ get; set; }
            public int nLoginType{ get; set; }
            public string strNumber{ get; set; }
            public string strPhotoID{ get; set; }
            public string strTrainmanID{ get; set; }
            public string strID{ get; set; }
            public string strName { get; set; }
            public string OperationType { get; set; }
        }
        public InterfaceRet RoomPatrol(string data)
        {
            _ret.Clear();
            data = System.Web.HttpUtility.UrlDecode(data);
            InputRoomPatrol input = Newtonsoft.Json.JsonConvert.DeserializeObject<InputRoomPatrol>(data);
            //判断所传对象是否为空
            if (input.OperationType == null)
            {
                _ret.resultStr = "所传类型为空，无法执行下一步操作";
                _ret.result = 1;
                return _ret;
            }

            try
            {
                DBSite db = new DBSite();
                switch (input.OperationType)
                {
                    case OperationType.Insert:
                        db.AddRoomPatrol(input);
                        break;
                    case OperationType.Delete:
                        db.DelRoomPatrol(input);
                        break;
                    case OperationType.Update:
                        db.UpdateRoomPatrol(input);
                        break;
                    default:
                        throw new Exception("操作类型有误，请核对");
                }
            }
            catch (Exception ex)
            {
                _ret.resultStr = ex.Message;
                _ret.result = 1;
            }
            return _ret;
        }
        #endregion

        #region  司机请假记录
        public class InputTMLeave
        {
            public string strLeaveRemark { get; set; }
            public int nEndLeaveLoginType { get; set; }
            public string strName { get; set; }
            public int nEndLeaveAdminLoginType { get; set; }
            public string strTrainmanID { get; set; }
            public int nAdminLoginType { get; set; }
            public string strTimeOutReason { get; set; }
            public string strEndAdminPhotoID { get; set; }
            public int nTimeOutMinute { get; set; }
            public string strAdminPhotoID { get; set; }
            public int nID { get; set; }
            public int nIsEndLeave { get; set; }
            public string strUserNumber { get; set; }
            public string strLeavePhotoID { get; set; }
            public int nSiteID { get; set; }
            public DateTime? dtLeaveTime { get; set; }
            public string strEndLeaveName { get; set; }
            public string strSiteName { get; set; }
            public string strEndLeavePhotoID { get; set; }
            public DateTime? dtEndLeaveTime { get; set; }
            public string strNumber { get; set; }
            public int nLeaveLoginType { get; set; }
            public string strid { get; set; }
            public string strUserID { get; set; }
            public string strUserName { get; set; }
            public string strEndLeaveTestAlcoholID { get; set; }
            public int nIsTimeOut { get; set; }
            public string strEndLeaveNumber { get; set; }
            public int dtLeaveCount { get; set; }
            public int nTestAlcoholResult { get; set; }
            public string strEndLeaveAdminID { get; set; }
            public string OperationType { get; set; }
        }
        public InterfaceRet TMLeave(string data)
        {
            _ret.Clear();
            data = System.Web.HttpUtility.UrlDecode(data);
            InputTMLeave input = Newtonsoft.Json.JsonConvert.DeserializeObject<InputTMLeave>(data);
            //判断所传对象是否为空
            if (input.OperationType == null)
            {
                _ret.resultStr = "所传类型为空，无法执行下一步操作";
                _ret.result = 1;
                return _ret;
            }

            try
            {
                DBSite db = new DBSite();
                switch (input.OperationType)
                {
                    case OperationType.Insert:
                        db.AddTMLeave(input);
                        break;
                    case OperationType.Delete:
                        db.DelTMLeave(input);
                        break;
                    case OperationType.Update:
                        db.UpdateTMLeave(input);
                        break;
                    default:
                        throw new Exception("操作类型有误，请核对");
                }
            }
            catch (Exception ex)
            {
                _ret.resultStr = ex.Message;
                _ret.result = 1;
            }
            return _ret;
        }
        #endregion

        #region ========================外点测酒照片========================================

        public class SitePic
        {
            public string nType;
            public List<ImgInfo> data;
        }


        public class ImgInfo
        {
            public string EventTime;
            public string GUID;
            public string Pic;
        }
        public InterfaceRet SavPic(string input)
        {
            input = HttpUtility.UrlDecode(input);
            _ret.Clear();
            try
            {
                SitePic paramModel = Newtonsoft.Json.JsonConvert.DeserializeObject<SitePic>(input);
                DBSite db = new DBSite();
                foreach (ImgInfo imginfo in paramModel.data)
                {
                    if (string.IsNullOrEmpty(imginfo.GUID))
                    {
                        TF.CommonUtility.LogClass.log("图片的GUID为空，跳过本次循环");
                        continue;
                    }
                    //相对路径
                    string logicPath = "/SiteImage/" + Convert.ToDateTime(imginfo.EventTime).ToString("yyyyMMdd") + "/";
                    //绝对路径
                    string phsycalPath = HttpContext.Current.Server.MapPath(logicPath);
                    if (!Directory.Exists(phsycalPath))
                    {
                        Directory.CreateDirectory(phsycalPath);
                    }
                    string fileName = phsycalPath + imginfo.GUID + ".jpg";
                    byte[] imageBytes = Convert.FromBase64String(imginfo.Pic);
                    FileStream fs = File.Create(fileName);
                    try
                    {
                        fs.Write(imageBytes, 0, imageBytes.Length);
                        fs.Flush();
                        db.AddSite_Photo(imginfo.GUID, imginfo.EventTime);
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
                _ret.result = 0;
                _ret.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                _ret.result = 1;
                _ret.resultStr = "提交失败" + ex.InnerException.Message.ToString();
            }
            return _ret;
        }
        #endregion



    }
}
