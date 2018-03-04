using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using System.Data;
using TF.CommonUtility;
using System.Web.Script.Serialization;
using System.Web;
using TF.RunSafty.bll.SynLog.Model;
using TF.RunSafty.bll.SynLog;

namespace TF.RunSafty.Trainman
{
    public class LCTrainmanMgr
    {
        const string strIdentifier_user = "SYNC.TM";   //人员
        const string strIdentifier_zhiwen = "SYNC.TM.FINGER";   //指纹
        const string strIdentifier_zhaopian = "SYNC.TM.PICTURE";   //照片

        InterfaceRet _ret = new InterfaceRet();

        #region 添加操作日志
        public void AddLog(int ChangeType, string data, string strKey, string strIdentifier)
        {
            LogQueue_Model m = new LogQueue_Model();
            m.strdata = data;
            m.ChangeType = ChangeType;
            m.Identifier = strIdentifier;
            m.Key = strKey;
            DBSynLog.AddLog(m);
        }

        #endregion

        #region 添加人员信息
        public class Get_InAddTrainmanMgr
        {
            public Trainman Trainman;
        }
        public class Get_OutAddTrainmanMgr
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutAddTrainmanMgr AddTrainman(string data)
        {
            Get_InAddTrainmanMgr model = null;
            Get_OutAddTrainmanMgr json = new Get_OutAddTrainmanMgr();
            //data = HttpUtility.UrlDecode(data);
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InAddTrainmanMgr>(data);
                DBTrainman db = new DBTrainman();
                if (db.AddTrainman(model.Trainman))
                {
                    json.result = "0";
                    json.resultStr = "返回成功，成功插入一条数据！";
                    AddLog(1, Newtonsoft.Json.JsonConvert.SerializeObject(model.Trainman), model.Trainman.strTrainmanGUID, strIdentifier_user);
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "返回失败，请查找原因！";
                }
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
          
            return json;
        }
        #endregion

        #region 修改人员信息
        public class Get_InUpTrainmanMgr
        {
            public Trainman trainman;
        }
        public class Get_OutUpTrainmanMgr
        {
            public string result = "";
            public string resultStr = "";
        }
        public InterfaceRet UpdateTrainman(string data)
        {
            _ret.Clear();
            try
            {
                Get_InUpTrainmanMgr input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InUpTrainmanMgr>(data);
                DBTrainman db = new DBTrainman();
                if (!db.UpdateTrainman(input.trainman))
                {
                    _ret.result = 1;
                    _ret.resultStr = "未找到待修改的人员，刷新后再试！";
                }
                else
                    AddLog(2, Newtonsoft.Json.JsonConvert.SerializeObject(input.trainman), input.trainman.strTrainmanGUID, strIdentifier_user);

               
            }
            catch (Exception ex)
            {
                _ret.result = 1;
                _ret.resultStr = "提交失败：" + ex.Message;
            }

            return _ret;
        }
        #endregion


        #region 删除人员信息
        public class Get_InDelTrainmanMgr
        {
            public string TrainmanGUID;
            public string DutyUserGUID;
            public string DutyUserNumber;
            public string DutyUserName;
        }

        public InterfaceRet DelTrainman(string data)
        {
            _ret.Clear();
            try
            {
                //反序列化传入的参数
                Get_InDelTrainmanMgr input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InDelTrainmanMgr>(data);

               

                DBTrainman db = new DBTrainman();
                if (!db.CheckIsCanUpdata(input.TrainmanGUID))
                    throw new Exception("请将该人员移除名牌后，再进行修改操作！");

                if (!db.CheckIsCanUpdataByPlan(input.TrainmanGUID))
                    throw new Exception("该司机有出勤计划，不能删除！");

                if (!db.CheckIsCanDelUnRun(input.TrainmanGUID))
                    throw new Exception("该司机处于请假状态，不能删除");

                //获取待删除人员的所有信息
                _ret.result = 1;
                _ret.resultStr = "未找到要修改的人员，刷新后再试！";
                DBTrainman.Tm tm = db.getTmByID(input.TrainmanGUID);
                if (db.DelTrainman(input.TrainmanGUID))
                {
                    _ret.result = 0;
                    _ret.resultStr = "返回成功";
                    AddLog(3, "", input.TrainmanGUID, strIdentifier_user);
                    //添加名牌变动日志
                    db.addLog4DelTrainMan(tm, input.DutyUserGUID, input.DutyUserNumber, input.DutyUserName);
                }
            }
            catch (Exception ex)
            {
                _ret.result = 1;
                _ret.resultStr = "提交失败：" + ex.Message;
            }
            return _ret;
        }
        #endregion

        #region 获取一个人员的所有信息
        public class Get_InGetTrainmanByNumber
        {
            public string TrainmanNumber;
            public int option;
        }
        public class Get_OutGetTrainmanByNumber
        {
            public string result = "";
            public string resultStr = "";
            public ResultGetTrainmanByNumber data;
        }

        public class ResultGetTrainmanByNumber
        {
            public List<VTrainman> trainmanArray;
        }



        public Get_OutGetTrainmanByNumber GetTrainmanByNumber(string data)
        {
            Get_OutGetTrainmanByNumber json = new Get_OutGetTrainmanByNumber();
            try
            {
                Get_InGetTrainmanByNumber input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetTrainmanByNumber>(data);
                DBTrainman db = new DBTrainman();
                ResultGetTrainmanByNumber r = new ResultGetTrainmanByNumber();
                r.trainmanArray = db.GetTrainmanByNumber(input.TrainmanNumber, input.option);
                json.data = r;
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 获取输入人员时弹出人员信息
        public class Get_InGetPopupTrainmans
        {
            public string WorkShopGUID;
            public string strKeyName;
            public int pageindex;
        }       

        public class Get_OutGetPopupTrainmans
        {
            public string result = "";
            public string resultStr = "";
            public ResultGetPopupTrainmans data;
        }

        public class ResultGetPopupTrainmans
        {
            public int nTotalCount;
            public List<VTrainman> trainmanArray;
        }

        public Get_OutGetPopupTrainmans GetPopupTrainmans(string data)
        {
            Get_OutGetPopupTrainmans json = new Get_OutGetPopupTrainmans();
            try
            {
                Get_InGetPopupTrainmans input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetPopupTrainmans>(data);
                DBTrainman db = new DBTrainman();
                ResultGetPopupTrainmans r = new ResultGetPopupTrainmans();
                int allcount = 0;
                r.trainmanArray = db.GetPopupTrainmans(input.WorkShopGUID, input.strKeyName, input.pageindex, out allcount); ;
                r.nTotalCount = allcount;
                json.data = r;
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 获取人员列表（分组获取）
        public class Get_InGetTrainmansBrief
        {
            public int startNid;
            public int nCount;
            public int option;
        }
        public class Get_OutGetTrainmansBrief
        {
            public string result = "";
            public string resultStr = "";
            public ResultGetTrainmansBrief data;
        }

        public class ResultGetTrainmansBrief
        {
            public List<VTrainman> trainmanArray;
            public int nTotalCount = 0;
        }
        public Get_OutGetTrainmansBrief GetTrainmansBrief(string data)
        {
            Get_OutGetTrainmansBrief json = new Get_OutGetTrainmansBrief();
            try
            {
                Get_InGetTrainmansBrief input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetTrainmansBrief>(data);
                DBTrainman db = new DBTrainman();
                ResultGetTrainmansBrief r = new ResultGetTrainmansBrief();
                r.trainmanArray = db.GetTrainmansBrief(input.startNid, input.nCount, input.option, out r.nTotalCount);
                
                json.data = r;
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 获取人员信息列表（分页）
        public class Get_InQueryTrainmans_blobFlag
        {
            public QurryModel QueryTrainman;
            public int pageindex;
        }

        public class Get_OutQueryTrainmans_blobFlag
        {
            public string result = "";
            public string resultStr = "";
            public Result data;
        }

        public class Result
        {
            public int nTotalCount;
            public List<VTrainman> trainmanArray;
        }

        public Get_OutQueryTrainmans_blobFlag QueryTrainmans_blobFlag(string data)
        {
            Get_OutQueryTrainmans_blobFlag json = new Get_OutQueryTrainmans_blobFlag();
            try
            {
                Get_InQueryTrainmans_blobFlag input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InQueryTrainmans_blobFlag>(data);
                DBTrainman db = new DBTrainman();
                Result r = new Result();
                int allcount = 0;
                r.trainmanArray = db.QueryTrainmans_blobFlag(input.QueryTrainman, input.pageindex, ref allcount); ;
                r.nTotalCount = allcount;
                json.data = r;
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 修改人员的联系方式
        public class Get_InUpdateTrainmanTel
        {
            public string TrainmanGUID;
            public string TrainmanTel;
            public string TrainmanMobile;
            public string TrainmanAddress;
            public string TrainmanRemark;

        }
        public class Get_OutUpdateTrainmanTel
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutUpdateTrainmanTel UpdateTrainmanTel(string data)
        {
            Get_InUpdateTrainmanTel model = null;
            Get_OutUpdateTrainmanTel json = new Get_OutUpdateTrainmanTel();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InUpdateTrainmanTel>(data);
                DBTrainman db = new DBTrainman();
                if (db.UpdateTrainmanTel(model.TrainmanGUID,model.TrainmanTel,model.TrainmanMobile,model.TrainmanAddress,model.TrainmanRemark))
                {
                    LCYAPlatTrainman.UpdateUserTel(model.TrainmanGUID,model.TrainmanTel);                    
                    json.result = "0";
                    json.resultStr = "返回成功，成功更新一条数据！";
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "返回失败，更新0条数据！";
                }
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 判断人员是否存在
        public class Get_InExistNumber
        {
            public string TrainmanGUID;
            public string TrainmanNumber;
        }
        public class Get_OutExistNumber
        {
            public string result = "";
            public string resultStr = "";
            public ResultExistNumber data;
        }

        public class ResultExistNumber
        {
            public bool result;
             
        }

        public Get_OutExistNumber ExistNumber(string data)
        {
            Get_OutExistNumber json = new Get_OutExistNumber();
            try
            {
                Get_InExistNumber input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InExistNumber>(data);
                DBTrainman db = new DBTrainman();
                ResultExistNumber r = new ResultExistNumber();
                r.result=db.ExistNumber(input.TrainmanGUID, input.TrainmanNumber);
                json.data = r;
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 通过GUID获取人员信息
        public class Get_InGetTrainman
        {
            public string TrainmanGUID;
            public int Option;
        }
        public class Get_OutGetTrainman
        {
            public string result = "";
            public string resultStr = "";
            public ResultGetTrainman data;
        }

        public class ResultGetTrainman
        {
            public List<VTrainman> trainmanArray;
        }
        public Get_OutGetTrainman GetTrainman(string data)
        {
            Get_OutGetTrainman json = new Get_OutGetTrainman();
            try
            {
                Get_InGetTrainman input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetTrainman>(data);
                DBTrainman db = new DBTrainman();
                ResultGetTrainman r = new ResultGetTrainman();
                r.trainmanArray = db.GetTrainman(input.TrainmanGUID, input.Option);
                json.data = r;
                json.result = "0";
                json.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 清除人员的指纹照片
        public class Get_InClearFinger
        {
            public string TrainmanGUID;

        }
        public class Get_OutClearFinger
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutClearFinger ClearFinger(string data)
        {
            Get_InClearFinger model = null;
            Get_OutClearFinger json = new Get_OutClearFinger();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InClearFinger>(data);
                DBTrainman db = new DBTrainman();
                if (db.ClearFinger(model.TrainmanGUID))
                {
                    json.result = "0";
                    json.resultStr = "返回成功，成功更新一条数据！";
                    AddLog(3, "", db.GetTrainmanNumberByGuid(model.TrainmanGUID), strIdentifier_zhiwen);
                }
                else
                {
                    json.result = "1";
                    json.resultStr = "返回失败，请查找原因！";
                }
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 修改人员的指纹照片
        public class Get_InUpdateFingerAndPic
        {
            public Trainman trainman;
        }
        public class Get_OutUpdateFingerAndPicr
        {
            public string result = "";
            public string resultStr = "";
        }

        public class Picture
        {
            public int ID { get; set; }
            public string Number { get; set; }
            public Object Pic { get; set; }
        }
        public class FingerPrint
        {
            public int ID { get; set; }
            public string Number { get; set; }
            public Object Finger1 { get; set; }
            public Object Finger2 { get; set; }
        }

        public Get_OutUpdateFingerAndPicr UpdateFingerAndPic(string data)
        {
            Get_InUpdateFingerAndPic model = null;
            Get_OutUpdateFingerAndPicr json = new Get_OutUpdateFingerAndPicr();
            try
            {
                //data = HttpUtility.UrlDecode(data);
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InUpdateFingerAndPic>(data);
                DBTrainman db = new DBTrainman();
                bool updateFingerPrint=false;
                bool updatePicture=false;
                int i=db.UpdateFingerAndPic(model.trainman,out updateFingerPrint, out updatePicture);
                if (i==1)
                {
                    json.result = "0";
                    json.resultStr = "返回成功，成功更新一条数据！";
                    if (updateFingerPrint)  //指纹
                    {
                        FingerPrint FingerPrint_m = new FingerPrint();
                        FingerPrint_m.ID = model.trainman.nID;
                        FingerPrint_m.Number = model.trainman.strTrainmanNumber;
                        FingerPrint_m.Finger1 = model.trainman.FingerPrint1;
                        FingerPrint_m.Finger2 = model.trainman.FingerPrint2;
                        AddLog(2, Newtonsoft.Json.JsonConvert.SerializeObject(FingerPrint_m), model.trainman.strTrainmanNumber, strIdentifier_zhiwen);
                    }
                    if (updatePicture)    //图片
                    {
                        Picture Picture_m = new Picture();
                        Picture_m.ID = model.trainman.nID;
                        Picture_m.Number = model.trainman.strTrainmanNumber;
                        Picture_m.Pic = model.trainman.Picture;
                        AddLog(2, Newtonsoft.Json.JsonConvert.SerializeObject(Picture_m), model.trainman.strTrainmanNumber, strIdentifier_zhaopian);
                    }
                }
                else if (i==2)
                {
                    json.result = "1";
                    json.resultStr = "所传对象为空！修改失败！";
                }
                else if (i == 3)
                {
                    json.result = "0";
                    json.resultStr = "未修改任何照片或指纹！";
                }
                else if (i == 0)
                {
                    json.result = "1";
                    json.resultStr = "找不到该记录！";
                }

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 设置指定人员状态
        public class Get_InSetTrainmanState
        {
            public string strTrainmanGUID;
            public int nTrainmanState;
        }
        public class Get_OutSetTrainmanState
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutSetTrainmanState SetTrainmanState(string data)
        {
            Get_InSetTrainmanState model = null;
            Get_OutSetTrainmanState json = new Get_OutSetTrainmanState();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InSetTrainmanState>(data);
                DBTrainman db = new DBTrainman();
                int i = db.SetTrainmanState(model.nTrainmanState, model.strTrainmanGUID);

                if (i >= 1)
                {
                    json.result = "0";
                    json.resultStr = "返回成功，成功更新" + i + "条数据！";
                }
                else if (i == 0)
                {
                    json.result = "1";
                    json.resultStr = "找不到该记录！";
                }

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 根据ID获取Trainman
        public class InGetTrainmanByID
        {
            //自增编号
            public int ID;
        }

        public class OutGetTrainmanByID
        {
            //人员
            public VTrainman TM = new VTrainman();
            //是否存在
            public int Exist;
        }

        /// <summary>
        /// 根据nid获取乘务员
        /// </summary>
        public InterfaceOutPut GetTrainmanByID(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTrainmanByID InParams = javaScriptSerializer.Deserialize<InGetTrainmanByID>(Data);
                string strSql = "select top 1 * from VIEW_Org_Trainman where nid = @nid";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("nid",InParams.ID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                OutGetTrainmanByID OutParams = new OutGetTrainmanByID();
                output.data = OutParams;
                if (dt.Rows.Count > 0)
                {
                    PSTrainman.TrainmanFromDB(OutParams.TM, dt.Rows[0], 0);
                }
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTrainmanByID:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion

        #region 获取各状态人员数量统计信息
        public class InGetTrainmanStateCount
        {
            //车间GUID
            public string WorkShopGUID;
        }

        public class OutGetTrainmanStateCount
        {
            //统计信息
            public TrainmanStateCount SumCount = new TrainmanStateCount();
        }

        /// <summary>
        /// 获取各状态人员数量统计信息
        /// </summary>
        public InterfaceOutPut GetTrainmanStateCount(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTrainmanStateCount InParams = javaScriptSerializer.Deserialize<InGetTrainmanStateCount>(Data);
                OutGetTrainmanStateCount OutParams = new OutGetTrainmanStateCount();
                string strSql = "select nTrainmanState,count(*)  as c from TAB_Org_Trainman where strWorkShopGUID = @strWorkShopGUID group by nTrainmanState order by nTrainmanState";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                };
                OutParams.SumCount.nInRoom = 0;
                OutParams.SumCount.nNil = 0;
                OutParams.SumCount.nNormal = 0;
                OutParams.SumCount.nOutRoom = 0;
                OutParams.SumCount.nPlaning = 0;
                OutParams.SumCount.nReady = 0;
                OutParams.SumCount.nRuning = 0;
                OutParams.SumCount.nUnRuning = 0;                

                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    switch (dr["nTrainmanState"].ToString())
                    {
                        case "0":
                            {
                                OutParams.SumCount.nUnRuning = ObjectConvertClass.static_ext_int(dr["c"]);
                                break;
                            }
                        case "1":
                            {
                                OutParams.SumCount.nReady = ObjectConvertClass.static_ext_int(dr["c"]);
                                break;
                            }
                        case "2":
                            {
                                OutParams.SumCount.nNormal = ObjectConvertClass.static_ext_int(dr["c"]);
                                break;
                            }
                        case "3":
                            {
                                OutParams.SumCount.nPlaning = ObjectConvertClass.static_ext_int(dr["c"]);
                                break;
                            }
                        case "4":
                            {
                                OutParams.SumCount.nInRoom = ObjectConvertClass.static_ext_int(dr["c"]);
                                break;
                            }
                        case "5":
                            {
                                OutParams.SumCount.nOutRoom = ObjectConvertClass.static_ext_int(dr["c"]);
                                break;
                            }
                        case "6":
                            {
                                OutParams.SumCount.nRuning = ObjectConvertClass.static_ext_int(dr["c"]);
                                break;
                            }
                        case "7":
                            {
                                OutParams.SumCount.nNil = ObjectConvertClass.static_ext_int(dr["c"]);
                                break;
                            }
                        default: break;
                    }
                }       
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTrainmanStateCount:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion

        #region 获取各交路下人员数量
        public class InGetTrainmanJiaoLuCount
        {
            //车间GUID
            public string WorkShopGUID;
        }

        public class OutGetTrainmanJiaoLuCount
        {
            //统计信息
            public TrainmanJiaoluCountList SumCount = new TrainmanJiaoluCountList();
        }

        /// <summary>
        /// 获取各交路下人员数量
        /// </summary>
        public InterfaceOutPut GetTrainmanJiaoLuCount(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTrainmanJiaoLuCount InParams = javaScriptSerializer.Deserialize<InGetTrainmanJiaoLuCount>(Data);
                OutGetTrainmanJiaoLuCount OutParams = new OutGetTrainmanJiaoLuCount();
                string strSql = @"select count(*) as nCount,strTrainmanJiaoluGUID,strTrainmanJiaoluName from VIEW_Nameplate_TrainmanInJiaolu_All 
                       where strTrainmanJiaoluGUID in (select strTrainmanJiaoluGUID from VIEW_Base_JiaoluRelation where strWorkShopGUID = @strWorkShopGUID)
                       group by strTrainmanJiaoluGUID,strTrainmanJiaoluName ";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString,CommandType.Text,strSql,sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TrainmanJiaoluCount c = new TrainmanJiaoluCount();
                    c.nCount = ObjectConvertClass.static_ext_int(dt.Rows[i]["nCount"]);
                    c.strJiaoLuName = dt.Rows[i]["strTrainmanJiaoluName"].ToString();
                    OutParams.SumCount.Add(c);
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTrainmanJiaoLuCount:" + ex.Message);
                throw ex;
            }
            return output;
        }

        #endregion

        #region 获取指定车间内各种请销假人员的数量
        public class InGetTrainmanLeaveCount
        {
            //车间GUID
            public string WorkShopGUID;
        }

        public class OutGetTrainmanLeaveCount
        {
            //统计信息
            public EnumSumList Sum = new EnumSumList();
        }

        /// <summary>
        /// 获取指定车间内各种请销假人员的数量
        /// </summary>
        public InterfaceOutPut GetTrainmanLeaveCount(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTrainmanLeaveCount InParams = javaScriptSerializer.Deserialize<InGetTrainmanLeaveCount>(Data);
                OutGetTrainmanLeaveCount OutParams = new OutGetTrainmanLeaveCount();
                string strSql = @"select * from TAB_LeaveMgr_LeaveType  order by strTypeGUID,strTypeName ";
                
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EnumSum sum = new EnumSum();
                    sum.EnumCount = 0;
                    sum.EnumID = dt.Rows[i]["strTypeGUID"].ToString();
                    sum.EnumName = dt.Rows[i]["strTypeName"].ToString();
                    OutParams.Sum.Add(sum);
                }

                strSql = @"select strTrainmanNumber, 
                (select top 1 strLeaveTypeGUID from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainmanID=strTrainmanNumber order by dBeginTime desc) as strLeaveTypeGUID, 
                 (select top 1 strTypeName from VIEW_LeaveMgr_AskLeaveWithTypeName where strTrainmanID=strTrainmanNumber order by dBeginTime desc) as strLeaveTypeName
                 from VIEW_Org_Trainman where strWorkShopGUID=@strWorkShopGUID  
                 and nTrainmanState = 0  order by strLeaveTypeGUID,strTrainmanNumber ";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                };
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool bFind =false;
                    for (int k = 0; k < OutParams.Sum.Count; k++)
                    {
                        if (OutParams.Sum[k].EnumID == dt.Rows[i]["strLeaveTypeGUID"].ToString())
                        {
                            OutParams.Sum[k].EnumCount++;
                            bFind = true;
                            break;
                        }
                        
                    }

                    if (!bFind)
                    {
                        EnumSum sum = new EnumSum();
                        sum.EnumCount = 1;
                        sum.EnumID = dt.Rows[i]["strLeaveTypeGUID"].ToString();
                        sum.EnumName = dt.Rows[i]["strLeaveTypeName"].ToString();
                        OutParams.Sum.Add(sum);
                    }
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTrainmanLeaveCount:" + ex.Message);
                throw ex;
            }
            return output;
        }       

        #endregion

        #region 获取车间下各交路的人员状态分布及机组信息

        public class InGetTrainmanRunStateCount
        {
            //车间GUID
            public string WorkShopGUID;
        }

        public class OutGetTrainmanRunStateCount
        {
            //统计信息
            public TrainmanRunStateCountList Sum = new TrainmanRunStateCountList();
        }

        /// <summary>
        /// 获取车间下各交路的人员状态分布及机组信息
        /// </summary>
        public InterfaceOutPut GetTrainmanRunStateCount(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTrainmanRunStateCount InParams = javaScriptSerializer.Deserialize<InGetTrainmanRunStateCount>(Data);
                OutGetTrainmanRunStateCount OutParams = new OutGetTrainmanRunStateCount();
                string strSql = @"select tm.*,p.nPlanState,  
                 (select top 1 nEventID from tab_plan_RunEvent where tm.strTrainPlanGUID = strTrainPlanGUID order by dtEventTime desc) as nEventID from 
                 (  
                 select strWorkShopGUID1,strTrainmanName1,strTrainmanNumber1, strTrainmanName2,strTrainmanNumber2,strTrainmanName3,strTrainmanNumber3, strTrainmanName4,strTrainmanNumber4,strTrainmanJiaoluName,strTrainPlanGUID from VIEW_Nameplate_Group 
                 )  
                 as tm left join tab_plan_train as p on  tm.strTrainPlanGUID = p.strTrainPlanGUID  
                 left join TAB_Org_WorkShop as w on  tm.strWorkShopGUID1 = w.strWorkShopGUID where strWorkShopGUID1 = @strWorkShopGUID order by strTrainmanjiaoluName";
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("strWorkShopGUID",InParams.WorkShopGUID)
                };
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool bFind = false;
                    
                    for (int k = 0; k < OutParams.Sum.Count; k++)
                    {
                        if (dt.Rows[i]["strTrainmanJiaoluName"].ToString() == OutParams.Sum[k].strJiaoLuName)
                        {
                            TrainmanGroup group = new TrainmanGroup();
                            group.TrainmanName1 = dt.Rows[i]["strTrainmanName1"].ToString();
                            group.TrainmanName2 = dt.Rows[i]["strTrainmanName2"].ToString();
                            group.TrainmanName3 = dt.Rows[i]["strTrainmanName3"].ToString();
                            group.TrainmanName4 = dt.Rows[i]["strTrainmanName4"].ToString();

                            group.TrainmanNumber1 = dt.Rows[i]["strTrainmanNumber1"].ToString();
                            group.TrainmanNumber2 = dt.Rows[i]["strTrainmanNumber2"].ToString();
                            group.TrainmanNumber3 = dt.Rows[i]["strTrainmanNumber3"].ToString();
                            group.TrainmanNumber4 = dt.Rows[i]["strTrainmanNumber4"].ToString(); 
                            string nEvent = dt.Rows[i]["nEventID"].ToString();
                            if (nEvent == "10001") 
                            {
                              group.GroupState = 5; //tsOutRoom
                              OutParams.Sum[k].nSiteCount ++; 
                              //Inc(TrainmanRunStateCountArray[n].nSiteCount)  ;
                            }
                            else
                            {
                              string nPlanState = dt.Rows[i]["nPlanState"].ToString(); 
                              if (nPlanState == "7")
                              {
                                  group.GroupState = 6;//tsRuning;
                                  OutParams.Sum[k].nRuningCount ++ ;
                                //Inc(TrainmanRunStateCountArray[n].nRuningCount)  ;
                              }
                              else
                              {
                                    group.GroupState = 4 ;//tsInRoom;
                                    OutParams.Sum[k].nLocalCount++;
                                    //Inc(TrainmanRunStateCountArray[n].nLocalCount)   ;
                              }
                            }
                            bFind = true;
                            OutParams.Sum[k].group.Add(group);
                            break;
                        }
                    }
                    if (!bFind)
                    {
                        TrainmanRunStateCount sum = new TrainmanRunStateCount();
                        sum.strJiaoLuName = dt.Rows[i]["strTrainmanJiaoluName"].ToString();                        
                        TrainmanGroup group = new TrainmanGroup();
                        group.TrainmanName1 = dt.Rows[i]["strTrainmanName1"].ToString();
                        group.TrainmanName2 = dt.Rows[i]["strTrainmanName2"].ToString();
                        group.TrainmanName3 = dt.Rows[i]["strTrainmanName3"].ToString();
                        group.TrainmanName4 = dt.Rows[i]["strTrainmanName4"].ToString();

                        group.TrainmanNumber1 = dt.Rows[i]["strTrainmanNumber1"].ToString();
                        group.TrainmanNumber2 = dt.Rows[i]["strTrainmanNumber2"].ToString();
                        group.TrainmanNumber3 = dt.Rows[i]["strTrainmanNumber3"].ToString();
                        group.TrainmanNumber4 = dt.Rows[i]["strTrainmanNumber4"].ToString(); 

                        string nEvent = dt.Rows[i]["nEventID"].ToString();
                        if (nEvent == "10001")
                        {
                            group.GroupState = 5; //tsOutRoom
                            sum.nSiteCount = 1;
                            //Inc(TrainmanRunStateCountArray[n].nSiteCount)  ;
                        }
                        else
                        {
                            string nPlanState = dt.Rows[i]["nPlanState"].ToString();
                            if (nPlanState == "7")
                            {
                                group.GroupState = 6;//tsRuning;
                                sum.nRuningCount = 1;
                                //Inc(TrainmanRunStateCountArray[n].nRuningCount)  ;
                            }
                            else
                            {
                                group.GroupState = 4;//tsInRoom;
                                sum.nLocalCount = 1;
                                //Inc(TrainmanRunStateCountArray[n].nLocalCount)   ;
                            }
                        }

                        sum.group.Add(group);
                        OutParams.Sum.Add(sum);
                    }
                }
                output.data = OutParams;
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetTrainmanRunStateCount:" + ex.Message);
                throw ex;
            }
            return output;
        }     
        #endregion

        #region 修改所属人员交路
        public class UpdateTrainmanJiaoluParam_Input
        {
            public string TrainmanGUID;
            public string TrainmanJiaoluGUID;
        }
        public Get_OutUpTrainmanMgr UpdateTrainmanJiaolu(string data)
        {
            UpdateTrainmanJiaoluParam_Input model;
            Get_OutUpTrainmanMgr result = new Get_OutUpTrainmanMgr();
            
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateTrainmanJiaoluParam_Input>(data);

                string sql = "update TAB_Org_Trainman set strTrainmanJiaoluGUID = @TrainmanJiaoluGUID where strTrainmanGUID = @TrainmanGUID";
                SqlParameter[] param = { 
                                       new SqlParameter("TrainmanJiaoluGUID",model.TrainmanJiaoluGUID),
                                       new SqlParameter("TrainmanGUID",model.TrainmanGUID)
                                   };

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, param);
                result.result = "0";
            }
            catch (Exception ex)
            {
                result.result = "1";
                result.resultStr = ex.Message.ToString();                
            }
            
            return result;
        }
        #endregion

        #region  修改人员的所属机构

        #region  传入参数

        public class TmOrg
        {
            public string DutyUserGUID = "";
            public string DutyUserNumber = "";
            public string DutyUserName = "";
            public string TrainmanNumber = "";
            public string TrainJiaoluGUID = "";
            public string AreaGUID = "";
            public string WorkShopGUID = "";
        }
        #endregion

        #region  修改机构主函数
        public InterfaceRet UpdateTMOrg(string data)
        {
            InterfaceRet _ret = new InterfaceRet();
            _ret.Clear();
            DBTrainman db = new DBTrainman();
            TmOrg input = Newtonsoft.Json.JsonConvert.DeserializeObject<TmOrg>(data);
            try
            {
                DBTrainman.Tm tm = db.getTm(input.TrainmanNumber);
                if (tm == null)
                    throw new Exception("找不到该司机,请从新输入工号");

                //判断所传机务段是否是空，如果是空直接停止修改
                //if (!string.IsNullOrEmpty(input.AreaGUID))
                //{
                    //if (tm.strJiWuDuanGUID != input.AreaGUID) 暂时先不检测
                UpdateArea(input, tm);
               // }

                //判断所传车间是否是空
                if (!string.IsNullOrEmpty(input.WorkShopGUID))
                {
                    if (tm.strWorkShopGUID != input.WorkShopGUID)
                    {

                        UpdateWorkShop(input, tm);

                        ////判断车间是否位于机务段下
                        //if (db.checkWorkShopInArea(input.AreaGUID, input.WorkShopGUID))

                        //else
                        //    throw new Exception("所选车间不属于所选机务段内！");
                    }
                }
                else
                {
                    UpdateWorkShop(input, tm);
                }

                //判断所传区段是否是空，如果是空直接停止修改
                if (!string.IsNullOrEmpty(input.TrainJiaoluGUID))
                {
                    if (tm.strTrainJiaoluGUID != input.TrainJiaoluGUID)
                    {
                        //判断车间是否位于机务段下
                        if (db.checkJiaoluInWorkShop(input.WorkShopGUID, input.TrainJiaoluGUID))
                            UpdateTrainJiaoLu(input, tm);
                        else
                            throw new Exception("所选区段不在所选车间内！");

                    }
                }
                else
                {
                    UpdateTrainJiaoLu(input, tm);
                }
                return _ret;
            }
            catch (Exception ex)
            {
                _ret.resultStr = ex.Message;
                _ret.result = 1;
            }
            return _ret;
        }
        #endregion


        #region 修改机务段
        public void UpdateArea(TmOrg input, DBTrainman.Tm tm)
        {

            if (string.IsNullOrEmpty(input.TrainmanNumber))
                throw new Exception("传入人员工号为空，无法修改！");

            DBTrainman db = new DBTrainman();
      
            //判断是否在牌  如果在牌 则需要移除
            if (!db.CheckIsCanUpdata(tm.strTrainmanGUID))
                throw new Exception("请将该人员移除名牌后，再进行修改操作！");
            //执行修改操作
            if (!db.UpdateArea(tm.strTrainmanGUID, input.AreaGUID))
                throw new Exception("未找到待修改的人员！");
            //添加名牌变动日志
            //db.addLog4UpdateArea(tm, tm.strJiWuDuanGUID, input.DutyUserGUID, input.DutyUserNumber, input.DutyUserName);

        }
        #endregion

        #region 修改车间
        public void UpdateWorkShop(TmOrg input, DBTrainman.Tm tm)
        {

            if (string.IsNullOrEmpty(input.TrainmanNumber))
                throw new Exception("传入人员工号为空，无法修改！");
            DBTrainman db = new DBTrainman();

            //判断是否在牌  如果在牌 则需要移除
            if (!db.CheckIsCanUpdata(tm.strTrainmanGUID))
                throw new Exception("请将该人员移除名牌后，再进行修改操作！");
            //执行修改操作
            if (!db.UpdateWorkShop(tm.strTrainmanGUID, input.WorkShopGUID))
                throw new Exception("未找到待修改的人员！");
            //添加名牌变动日志
            db.addLog4UpdateWs(tm, input.WorkShopGUID, input.DutyUserGUID, input.DutyUserNumber, input.DutyUserName);

        }
        #endregion

        #region  修改行车区段
        public void UpdateTrainJiaoLu(TmOrg input, DBTrainman.Tm tm)
        {

            if (string.IsNullOrEmpty(input.TrainmanNumber))
                throw new Exception("传入人员工号为空，无法修改");
            DBTrainman db = new DBTrainman();
          
            //判断是否在牌  如果在牌 则需要移除
            if (!db.CheckIsCanUpdata(tm.strTrainmanGUID))
                throw new Exception("请将该人员移除名牌后，再进行修改操作！");
            //执行修改操作
            if (!db.UpdateTrainJiaolu(tm.strTrainmanGUID, input.TrainJiaoluGUID))
                throw new Exception("未找到待修改的人员！");
            //添加名牌变动日志
            db.addLog4UpdateTrainmanJiaolu(tm, input.TrainJiaoluGUID, input.DutyUserGUID, input.DutyUserNumber, input.DutyUserName);

        }
        #endregion

        #endregion

    }
}
