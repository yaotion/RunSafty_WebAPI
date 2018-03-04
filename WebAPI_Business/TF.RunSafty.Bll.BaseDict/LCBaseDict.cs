using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.CommonUtility;

namespace TF.RunSafty.BaseDict
{

    #region 车站信息
    public class LCStation
    {

        #region 获取所有车站信息
        public class Get_In
        {
        }
        public class Get_Out
        {
            public string result = "";
            public string resultStr = "";
            public List<Station> data;
        }
        public Get_Out GetStations(string data)
        {
            Get_Out json = new Get_Out();
            try
            {
                Get_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_In>(data);
                DBStation db = new DBStation();
                json.data = db.GetStationList();
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

        #region 获取车站(根据交路ID)
        public class Get_InGetStationsOfJiaoJu
        {
            public string strTrainJiaoluGUID;
        }
        public class Get_OutGetStationsOfJiaoJu
        {
            public string result = "";
            public string resultStr = "";
            public List<StationInTrainJiaolu> data;
        }
        public Get_OutGetStationsOfJiaoJu GetStationsOfJiaoJu(string data)
        {
            Get_OutGetStationsOfJiaoJu json = new Get_OutGetStationsOfJiaoJu();
            try
            {
                Get_InGetStationsOfJiaoJu input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetStationsOfJiaoJu>(data);
                DBStation db = new DBStation();
                json.data = db.GetStationsOfJiaoJu(input.strTrainJiaoluGUID);
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

        public class TrainJiaoluGUIDData
        {
            public int nLendingTypeID;
            public string strLendingTypeName;
            public string strAlias;
        }

        public string getTest(string strTrainJiaoluGUID1, string strTrainJiaoluGUID2, TrainJiaoluGUIDData strTrainJiaoluGUID3)
        {

            return "";

        }
    }
    #endregion

    #region  获取车型
    public class CheXin
    {
        public class Get_InGetCheXinOfSite
        {
            public string strSiteGUID;

        }
        public class Get_OutGetCheXinOfSite
        {
            public string result = "";
            public string resultStr = "";
            public List<CheXinInSite> data;
        }

        public class CheXinInSite
        {
            public string strTrainType;
            public int nID;
        
        }


        public Get_OutGetCheXinOfSite GetCheXinBySite(string data)
        {
            Get_OutGetCheXinOfSite json = new Get_OutGetCheXinOfSite();
            try
            {
                Get_InGetCheXinOfSite input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetCheXinOfSite>(data);
                CheXing db = new CheXing();
                json.data = db.GetCheXinBySite(input.strSiteGUID);
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
    }
    #endregion

    #region 行车区段信息
    public class LCTrainJL
    {
        #region 获取指定客户端管辖的行车区段信息
        public class Get_InGetTrainJiaoluArrayOfSite
        {
            public string SiteGUID;
        }
        public class Get_OutGetTrainJiaoluArrayOfSite
        {
            public string result = "";
            public string resultStr = "";
            public List<TrainJiaoluInSite> data;
        }
        public Get_OutGetTrainJiaoluArrayOfSite GetTrainJiaoluArrayOfSite(string data)
        {
            Get_OutGetTrainJiaoluArrayOfSite json = new Get_OutGetTrainJiaoluArrayOfSite();
            try
            {
                Get_InGetTrainJiaoluArrayOfSite input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetTrainJiaoluArrayOfSite>(data);
                DBTrainJiaolu db = new DBTrainJiaolu();
                json.data = db.GetTrainJiaoluArrayOfSite(input.SiteGUID);
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

        #region 获取指定车间管辖的行车区段信息
        public class Get_InGetTrainJiaoluArrayOfWorkShop
        {
            public string strWorkShopGUID;
        }
        public class Get_OutGetTrainJiaoluArrayOfWorkShop
        {
            public string result = "";
            public string resultStr = "";
            public List<TrainJiaolu> data;
        }
        public Get_OutGetTrainJiaoluArrayOfWorkShop GetTrainJiaoluArrayOfWorkShop(string data)
        {
            Get_OutGetTrainJiaoluArrayOfWorkShop json = new Get_OutGetTrainJiaoluArrayOfWorkShop();
            try
            {
                Get_InGetTrainJiaoluArrayOfWorkShop input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetTrainJiaoluArrayOfWorkShop>(data);
                DBTrainJiaolu db = new DBTrainJiaolu();
                json.data = db.GetTrainJiaoluArrayOfWorkShop(input.strWorkShopGUID);
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

        #region  获取所有的区段
        public class Get_InGetAllTrainJiaolu
        {
        }
        public class Get_OutGetAllTrainJiaolu
        {
            public string result = "";
            public string resultStr = "";
            public List<TrainJiaolu> data;
        }
        public Get_OutGetAllTrainJiaolu GetAllTrainJiaolu(string data)
        {
            Get_OutGetAllTrainJiaolu json = new Get_OutGetAllTrainJiaolu();
            try
            {
                Get_InGetAllTrainJiaolu input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetAllTrainJiaolu>(data);
                DBTrainJiaolu db = new DBTrainJiaolu();
                json.data = db.AllTrainJiaolu();
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

        #region  根据行车区段名称获取对应的GUID
        public class Get_InGetTrainJiaoluGUIDByName
        {
            public string TrainJiaoluName;
        }
        public class Get_OutGetTrainJiaoluGUIDByName
        {
            public string result = "";
            public string resultStr = "";
            public strresult data;
        }

        public class strresult
        {
            public string result = "";
        }
        public Get_OutGetTrainJiaoluGUIDByName GetTrainJiaoluGUIDByName(string data)
        {
            Get_OutGetTrainJiaoluGUIDByName json = new Get_OutGetTrainJiaoluGUIDByName();
            try
            {
                Get_InGetTrainJiaoluGUIDByName input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetTrainJiaoluGUIDByName>(data);
                DBTrainJiaolu db = new DBTrainJiaolu();
                strresult r = new strresult();
                r.result = db.GetTrainJiaoluGUIDByName(input.TrainJiaoluName);
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

        #region 获取行车交路信息
        public class Get_InGetTrainJiaolu
        {
            public string strTrainJiaoluGUID;
        }
        public class Get_OutGetTrainJiaolu
        {
            public string result = "";
            public string resultStr = "";
            public List<TrainJiaolu> data;
        }
        public Get_OutGetTrainJiaolu GetTrainJiaolu(string data)
        {
            Get_OutGetTrainJiaolu json = new Get_OutGetTrainJiaolu();
            try
            {
                Get_InGetTrainJiaolu input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetTrainJiaolu>(data);
                DBTrainJiaolu db = new DBTrainJiaolu();
                json.data = db.GetTrainJiaolu(input.strTrainJiaoluGUID);
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

        #region  判断交路是否属于客户端管辖
        public class Get_InIsJiaoLuInSite
        {
            public string TrainJiaoluGUID;
            public string SiteGUID;
        }
        public class Get_OutIsJiaoLuInSite
        {
            public string result = "";
            public string resultStr = "";
            public boolresult data;
        }

        public class boolresult
        {
            public bool result;
        }
        public Get_OutIsJiaoLuInSite IsJiaoLuInSite(string data)
        {
            Get_OutIsJiaoLuInSite json = new Get_OutIsJiaoLuInSite();
            try
            {
                Get_InIsJiaoLuInSite input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InIsJiaoLuInSite>(data);
                DBTrainJiaolu db = new DBTrainJiaolu();
                boolresult r = new boolresult();
                r.result = db.IsJiaoLuInSite(input.TrainJiaoluGUID, input.SiteGUID);
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


        #region 获取客户端管辖人员交路及出勤点接口

        public class InGetTMJLList
        {
            public string SiteNumber;
        }
        public class OutTMJLList : PSBaseDict.InterfaceRet
        {
            public List<TMJL> data;
        }

        public class TMJL
        {
            public string JlName;
            public string JlGUID;
            public int JlType;
            public List<Place> PlaceList;
        }

        public class Place
        {
            public string ID;
            public string Name;
        }

        public OutTMJLList GetSiteTrainmanJiaolus(string data)
        {
            OutTMJLList _ret = new OutTMJLList();
            _ret.Clear();
            try
            {
                InGetTMJLList input = Newtonsoft.Json.JsonConvert.DeserializeObject<InGetTMJLList>(data);
                DBTrainJiaolu db = new DBTrainJiaolu();
                List<DBTrainJiaolu.TmJL> List_tmjl = db.getList_TMJL(input.SiteNumber);
                List<TMJL> List_TMJL = new List<TMJL>();
                foreach (DBTrainJiaolu.TmJL tmjl in List_tmjl)
                {
                    //通过人员区段获取 人员区段的行车区段下的所有出勤点
                    List<DBTrainJiaolu.Place> PlaceList = db.getList_Place(tmjl.strTrainmanJiaoluGUID);
                    List<Place> List_Place = new List<Place>();
                    foreach (DBTrainJiaolu.Place place in PlaceList)
                    {
                        Place p = new Place();
                        p.ID = place.strPlaceID;
                        p.Name = place.strPlaceName;
                        List_Place.Add(p);
                    }
                    TMJL TMJL = new TMJL();
                    TMJL.PlaceList = List_Place;
                    TMJL.JlGUID = tmjl.strTrainmanJiaoluGUID;
                    TMJL.JlName = tmjl.strTrainmanJiaoluName;
                    TMJL.JlType = tmjl.nJiaoluType;
                    List_TMJL.Add(TMJL);
                }
                _ret.data = List_TMJL;
                _ret.result = 0;
                _ret.resultStr = "返回成功";
            }
            catch (Exception ex)
            {
                _ret.result = 1;
                _ret.resultStr = "提交失败：" + ex.Message;
            }
            return _ret;
        }
        #endregion





    }
    #endregion

    #region 人员交路
    public class LCTrainmanJL
    {
        #region GetTrainmanJiaolusOfTrainJiaoluEx
        public class Get_In
        {
            public string SiteGUID;
            public string TrainJiaoluGUID;
        }
        public class Get_Out
        {
            public string result = "";
            public string resultStr = "";
            public List<TrainManJiaoLu> data;
        }
        public Get_Out GetTMJLByTrainJLWithSiteVirtual(string data)
        {
            Get_Out json = new Get_Out();
            try
            {
                Get_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_In>(data);
                DBTrainManJiaoLu db = new DBTrainManJiaoLu();
                json.data = db.GetTrainmanJiaolusOfTrainJiaoluEx(input.SiteGUID, input.TrainJiaoluGUID);
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

        #region 获取机车交路下的人员交路信息


        public class Get_InNoSite
        {
            public string TrainJiaoluGUID;
        }

        public class Get_OutOverride
        {
            public string result = "";
            public string resultStr = "";
            public List<TrainManJiaoluRelation> data;
        }


        public Get_OutOverride GetTMJLByTrainJLWithSite(string data)
        {
            Get_OutOverride json = new Get_OutOverride();
            try
            {
                Get_InNoSite input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InNoSite>(data);
                DBTrainManJiaoLu db = new DBTrainManJiaoLu();
                json.data = db.GetTrainmanJiaolusOfTrainJiaolu(input.TrainJiaoluGUID);
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

        #region 获取指定区段内的人员交路信息
        public Get_OutOverride GetTMJLByTrainJL(string data)
        {
            Get_OutOverride json = new Get_OutOverride();
            try
            {
                Get_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_In>(data);
                DBTrainManJiaoLu db = new DBTrainManJiaoLu();
                json.data = db.GetTrainmanJiaolusOfTrainJiaolu(input.SiteGUID, input.TrainJiaoluGUID);
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
    }
    #endregion

    #region 车间
    public class LCWorkShop
    {
        #region 获取机务段下所有车间

        public class JsonOutBase
        {
            public string result = "";
            public string resultStr = "";
        }

        public class RRsWorkShop
        {
            public string strWorkShopGUID = string.Empty;
            public string strAreaGUID = string.Empty;
            public string strWorkShopName = string.Empty;
        }

        public class Area_In
        {
            public string AreaGUID = string.Empty;
        }

        public class Area_Out : JsonOutBase
        {
            public List<RRsWorkShop> data;
        }

        public Area_Out GetWorkShopOfArea(string input)
        {
            Area_Out json = new Area_Out();
            RRsWorkShop workShop = null;
            Area_In model = null; 
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Area_In>(input);
                string strSql = "select * from TAB_Org_WorkShop  order by strWorkShopName";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strAreaGUID",model.AreaGUID), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                json.data = new List<RRsWorkShop>();
                foreach (DataRow row in table.Rows)
                {
                    workShop = new RRsWorkShop();
                    workShop.strAreaGUID = row["strAreaGUID"].ToString();
                    workShop.strWorkShopGUID = row["strWorkShopGUID"].ToString();
                    workShop.strWorkShopName = row["strWorkShopName"].ToString();
                    json.data.Add(workShop);
                }
                json.result = "0";
                json.resultStr = "获取车间成功";
            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }

        #endregion

        #region 根据车间名称获取对应的GUID

        public class Name_In
        {
            public string WorkShopName = string.Empty;
        }

        public class Name_Out : JsonOutBase
        {
            public RRsWorkShop data;
        }

        public Name_Out GetWorkShopGUIDByName(string input)
        {
            Name_Out json = new Name_Out();
            Name_In model = null;
            RRsWorkShop work = new RRsWorkShop();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Name_In>(input);
                string strSql = "select strWorkShopGUID from TAB_Org_WorkShop where  strWorkShopName =@strWorkShopName";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("strWorkShopName",model.WorkShopName), 
                };
                DataTable table =
                    SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParameters).Tables[0];
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    work.strWorkShopGUID = row["strWorkShopGUID"].ToString();
                    json.data = work;
                    json.result = "0";
                    json.resultStr = "获取车间GUID成功";
                }
                else
                {
                    work.strWorkShopGUID = "";
                    json.data = work;
                    json.result = "0";
                    json.resultStr = "未找到此名称的GUID";

                }

            }
            catch (Exception ex)
            {
                TF.CommonUtility.LogClass.logex(ex, "");
                throw ex;
            }
            return json;
        }
        #endregion
    }
    #endregion

    #region 获取所有的机务段信息
    public class LCJwd
    {
        #region 获取行车区段信息
        public class Get_In
        {
        }
        public class Get_Out
        {
            public string result = "";
            public string resultStr = "";
            public List<JWDCoding> data;
        }
        public Get_Out GetAllJwdList(string data)
        {
            Get_Out json = new Get_Out();
            try
            {
                Get_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_In>(data);
                DBJWDCoding db = new DBJWDCoding();
                json.data = db.GetBase_JWDCodingLuList();
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
    }
    #endregion

    #region 获取所有出勤规则字典
    public class LCWorkPlan
    {
        #region 获取行车区段信息
        public class Get_InGetPlanTimes
        {
            public int RemarkType;
            public string PlaceID;
        }
        public class Get_OutGetPlanTimes
        {
            public string result = "";
            public string resultStr = "";
            public nResult data;
        }
        public class nResult
        {
            public int result;

        }

        public Get_OutGetPlanTimes GetPlanTimes(string data)
        {
            Get_OutGetPlanTimes json = new Get_OutGetPlanTimes();
            try
            {
                Get_InGetPlanTimes input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetPlanTimes>(data);
                DBGetPlanTimes db = new DBGetPlanTimes();
                nResult nr = new nResult();
                nr.result = db.GetPlanTimes(input.RemarkType, input.PlaceID);
                json.data = nr;
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
    }
    #endregion

    #region 指导组
    public class LCGuideGroup
    {
        #region 获取指定车间的指导组信息
        public class Get_InGetGuideGroupOfWorkShop
        {
            public string strWorkShopGUID;
        }
        public class Get_OutGetGuideGroupOfWorkShop
        {
            public string result = "";
            public string resultStr = "";
            public List<GuideGroup> data;
        }
        public Get_OutGetGuideGroupOfWorkShop GetGuideGroupOfWorkShop(string data)
        {
            Get_OutGetGuideGroupOfWorkShop json = new Get_OutGetGuideGroupOfWorkShop();
            try
            {
                Get_InGetGuideGroupOfWorkShop input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetGuideGroupOfWorkShop>(data);
                DBOrg_GuideGroup db = new DBOrg_GuideGroup();
                json.data = db.GetGuideGroupOfWorkShop(input.strWorkShopGUID);
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

        #region 根据指导队名称获取对应的GUID
        public class Get_InGetGuideGroupGUIDByName
        {
            public string GuideGroupName;
        }
        public class Get_OutGetGuideGroupGUIDByName
        {
            public string result = "";
            public string resultStr = "";
            public Result data;
        }

        public class Result
        {
            public string result;
        }
        public Get_OutGetGuideGroupGUIDByName GetGuideGroupGUIDByName(string data)
        {
            Get_OutGetGuideGroupGUIDByName json = new Get_OutGetGuideGroupGUIDByName();
            try
            {
                Get_InGetGuideGroupGUIDByName input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetGuideGroupGUIDByName>(data);
                DBOrg_GuideGroup db = new DBOrg_GuideGroup();
                Result r = new Result();
                r.result = db.GetGuideGroupGUIDByName(input.GuideGroupName);
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

    }
    #endregion

    #region 常用意见
    public class LCSignType
    {

        #region 查询常用意见
        public class Get_InGetSignType
        {
        }
        public class Get_OutGetSignType
        {
            public string result = "";
            public string resultStr = "";
            public List<SignType> data;
        }
        public Get_OutGetSignType GetSignType(string data)
        {
            Get_OutGetSignType json = new Get_OutGetSignType();
            try
            {
                Get_InGetSignType input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetSignType>(data);
                DBSignType db = new DBSignType();
                json.data = db.GetSignType();
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


        #region 添加常用意见

        public class Get_OutAddSignType
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutAddSignType AddSignType(string data)
        {

            SignType model = null;
            Get_OutAddSignType json = new Get_OutAddSignType();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<SignType>(data);
                DBSignType db = new DBSignType();
                if (db.AddSignType(model))
                {
                    json.result = "0";
                    json.resultStr = "返回成功，成功插入一条数据！";
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


        #region 删除常用意见

        public class Get_OutDeleteSignType
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutDeleteSignType DeleteSignType(string data)
        {

            SignType model = null;
            Get_OutDeleteSignType json = new Get_OutDeleteSignType();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<SignType>(data);
                DBSignType db = new DBSignType();
                if (db.DeleteSignType(model))
                {
                    json.result = "0";
                    json.resultStr = "返回成功，成功删除一条数据！";
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


    }
    #endregion

    #region 测酒相关方法
    public class LCSystemDict
    {

        #region 1.1.9.1获取测酒类型数据
        public class Get_InGetDrinkTypeArray
        {
        }
        public class Get_OutGetDrinkTypeArray
        {
            public string result = "";
            public string resultStr = "";
            public List<DictTable> data;
        }
        public Get_OutGetDrinkTypeArray GetDrinkTypeArray(string data)
        {
            Get_OutGetDrinkTypeArray json = new Get_OutGetDrinkTypeArray();
            try
            {
                DBSystemDict db = new DBSystemDict();
                json.data = db.GetDrinkTypeArray();
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

        #region 1.1.9.2 获取验证方式数据

        public class Get_OutGetVerifyArray
        {
            public string result = "";
            public string resultStr = "";
            public List<DictTable> data;
        }
        public Get_OutGetVerifyArray GetVerifyArray(string data)
        {
            Get_OutGetVerifyArray json = new Get_OutGetVerifyArray();
            try
            {
                DBSystemDict db = new DBSystemDict();
                json.data = db.GetVerifyArray();
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

        #region 1.1.9.3 获取测酒结果数据

        public class Get_OutGetDrinkResult
        {
            public string result = "";
            public string resultStr = "";
            public List<DictTable> data;
        }
        public Get_OutGetDrinkResult GetDrinkResult(string data)
        {
            Get_OutGetDrinkResult json = new Get_OutGetDrinkResult();
            try
            {
                DBSystemDict db = new DBSystemDict();
                json.data = db.GetDrinkResult();
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


        #region '获取运安系统时间定义信息'
        public class OutGetKernelTimeConfig
        {
            //是否存在
            public int Exist = 0;
            //设置信息
            public KernelTimeConfig Config = new KernelTimeConfig();
        }

        /// <summary>
        /// 获取运安系统时间定义信息
        /// </summary>
        public InterfaceOutPut GetKernelTimeConfig(String Data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            output.result = 1;
            try
            {
                OutGetKernelTimeConfig OutParams = new OutGetKernelTimeConfig();
                output.data = OutParams;
                string strSql = "select top 1 * from tab_Base_KernelTime";
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    OutParams.Config.nMinCallBeforeChuQing = ObjectConvertClass.static_ext_int(dt.Rows[0]["nMinCallBeforeChuQing"]);
                    OutParams.Config.nMinChuQingBeforeStartTrain_K = ObjectConvertClass.static_ext_int(dt.Rows[0]["nMinChuQingBeforeStartTrain_K"]);
                    OutParams.Config.nMinChuQingBeforeStartTrain_Z = ObjectConvertClass.static_ext_int(dt.Rows[0]["nMinChuQingBeforeStartTrain_Z"]);
                    OutParams.Config.nMinDayWorkStart = ObjectConvertClass.static_ext_int(dt.Rows[0]["nMinDayWorkStart"]);
                    OutParams.Config.nMinNightWokrStart = ObjectConvertClass.static_ext_int(dt.Rows[0]["nMinNightWokrStart"]);
                    OutParams.Exist = 1;
                }
                output.result = 0;
            }
            catch (Exception ex)
            {
                output.resultStr = ex.Message;
                LogClass.log("Interface.GetKernelTimeConfig:" + ex.Message);
                throw ex;
            }
            return output;
        }
        #endregion
    }
    #endregion

    #region 客户端信息
    public class LCSite
    {
        #region 根据编号获取映射的客户端的岗位信息
        public class Get_InGetSiteByRelationIP
        {
            public string strSrcSiteIP;
            public int nToSiteJob;
        }

        public class SiteByRelIPOutParam
        {
            public string RealIP;
            public Site siteInfo;
        }

        public class Get_OutGetSiteByRelationIP
        {
            public string result = "";
            public string resultStr = "";
            public SiteByRelIPOutParam data;
        }


        public Get_OutGetSiteByRelationIP GetSiteByRelationIP(string data)
        {
            Get_OutGetSiteByRelationIP json = new Get_OutGetSiteByRelationIP();
            string realIP;
            Site site;
            try
            {
                Get_InGetSiteByRelationIP input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetSiteByRelationIP>(data);
                SiteByRelIPOutParam outParam = new SiteByRelIPOutParam();
                DBSite db = new DBSite();

                site = db.GetSiteByRelationIP(input.strSrcSiteIP, input.nToSiteJob, out realIP);

                json.data.siteInfo = site;
                json.data.RealIP = realIP;


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

        #region 根据编号获取岗位信息
        public class Get_InGetSiteByIP
        {
            public string strSiteIP;
        }
        public class Get_OutGetSiteByIP
        {
            public string result = "";
            public string resultStr = "";
            public Site data;
        }
        public Get_OutGetSiteByIP GetSiteByIP(string data)
        {
            Get_OutGetSiteByIP json = new Get_OutGetSiteByIP();
            try
            {
                Get_InGetSiteByIP input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetSiteByIP>(data);
                DBSite db = new DBSite();
                json.data = db.GetSiteByIP(input.strSiteIP);
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

        #region  获取全部客户端

        public class Get_OutGetSites
        {
            public string result = "";
            public string resultStr = "";
            public List<Site> data;
        }
        public Get_OutGetSites GetSites(string data)
        {
            Get_OutGetSites json = new Get_OutGetSites();
            try
            {
                DBSite db = new DBSite();
                json.data = db.GetSites();
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

        #region  获取客户端步骤信息

        public class Get_InGetSiteStepFlowByID
        {
            public string strSiteID;
        }
        public class Get_OutGetSiteStepFlowByID
        {
            public string result = "";
            public string resultStr = "";
            public string data;
        }
        public Get_OutGetSiteStepFlowByID GetSiteStepFlowByIP(string data)
        {
            Get_OutGetSiteStepFlowByID json = new Get_OutGetSiteStepFlowByID();

            try
            {
                Get_InGetSiteStepFlowByID input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetSiteStepFlowByID>(data);
                string strSql = "SELECT StepConfig, ConfigName FROM TAB_Base_Site_StepConfig WHERE (strSiteGUID = '" + input.strSiteID + "')";
                DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
                if (dt.Rows.Count < 1)
                    throw new Exception("未找到编号为" + input.strSiteID + "的cid信息");

                string strStepConfig = "";

                if (!DBNull.Value.Equals(dt.Rows[0]["StepConfig"]))
                    strStepConfig = Convert.ToBase64String((byte[])dt.Rows[0]["StepConfig"]);
                json.data = strStepConfig;
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


    }
    #endregion

    #region 获取嵌入的查询页
    public class LCEmbeddedPages
    {
        public class Get_GetPageItemsOut
        {
            public int result;
            public string resultStr;
            public object data;
        }

        public class Get_GetPageItemsIn
        {
            public int ClientJobType;
        }
        public Get_GetPageItemsOut GetPageItems(string data)
        {
            Get_GetPageItemsOut result = new Get_GetPageItemsOut();
            try
            {
                Get_GetPageItemsIn input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_GetPageItemsIn>(data);

                result.data = DBSystemDict.GetEmbeddedPages(input.ClientJobType);
                result.result = 0;
            }
            catch (Exception ex)
            {
                result.result = 1;
                result.resultStr = ex.Message.ToString();
            }


            return result;

        }
    }

    #endregion

    #region 干部管理
    public class LCGanBuType
    {
        public class GanBuType_Input
        {
            public int TypeID;
            public string WorkShopGUID = string.Empty;
        }

        public InterfaceOutPut Get(string data)
        {
            GanBuType_Input input = Newtonsoft.Json.JsonConvert.DeserializeObject<GanBuType_Input>(data);
            InterfaceOutPut result = new InterfaceOutPut();
            DBGanBuType dbGanBuType = new DBGanBuType();
            result.data = dbGanBuType.Get(input.TypeID);
            return result;
        }

        public InterfaceOutPut Add(string data)
        {
            GanBuType input = Newtonsoft.Json.JsonConvert.DeserializeObject<GanBuType>(data);

            InterfaceOutPut output = new InterfaceOutPut();
            new DBGanBuType().Add(input);
            output.result = 0;
            return output;
        }


        public InterfaceOutPut Delete(string data)
        {
            GanBuType_Input input = Newtonsoft.Json.JsonConvert.DeserializeObject<GanBuType_Input>(data);
            InterfaceOutPut output = new InterfaceOutPut();
            new DBGanBuType().Delete(input.TypeID);
            output.result = 0;
            return output;
        }

        public InterfaceOutPut Update(string data)
        {
            GanBuType input = Newtonsoft.Json.JsonConvert.DeserializeObject<GanBuType>(data);
            InterfaceOutPut output = new InterfaceOutPut();
            new DBGanBuType().Update(input);
            output.result = 0;
            return output;
        }

        public InterfaceOutPut Query(string data)
        {
            GanBuType_Input input = Newtonsoft.Json.JsonConvert.DeserializeObject<GanBuType_Input>(data);
            InterfaceOutPut result = new InterfaceOutPut();
            DBGanBuType dbGanBuType = new DBGanBuType();
            result.data = dbGanBuType.Query(input.WorkShopGUID);
            return result;
        }

        public class InputParam_ExchangeOrder
        {
            public string WorkShopGUID = string.Empty;
            public int TypeID1;
            public int TypeID2;
        }
        public InterfaceOutPut ExchangeOrder(string data)
        {
            InputParam_ExchangeOrder input = Newtonsoft.Json.JsonConvert.DeserializeObject<InputParam_ExchangeOrder>(data);

            InterfaceOutPut result = new InterfaceOutPut();
            DBGanBuType dbGanBuType = new DBGanBuType();
            dbGanBuType.ExchangeOrder(input.WorkShopGUID, input.TypeID1, input.TypeID2);
            result.result = 0;
            return result;
        }
    }
    public class LCGanBu
    {
        public class GanBu_Input
        {
            public int RecID;
            public string WorkShopGUID = string.Empty;
            public string Number;
        }
        public InterfaceOutPut Get(string data)
        {
            GanBu_Input input = Newtonsoft.Json.JsonConvert.DeserializeObject<GanBu_Input>(data);
            InterfaceOutPut output = new InterfaceOutPut();

            output.data = new DBGanBu().Get(input.RecID);
            return output;
        }

        public InterfaceOutPut Add(string data)
        {
            GanBu input = Newtonsoft.Json.JsonConvert.DeserializeObject<GanBu>(data);
            InterfaceOutPut output = new InterfaceOutPut();
            new DBGanBu().Add(input);
            output.result = 0;
            return output;
        }

        public InterfaceOutPut Delete(string data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            GanBu_Input input = Newtonsoft.Json.JsonConvert.DeserializeObject<GanBu_Input>(data);
            new DBGanBu().Delete(input.RecID);
            output.result = 0;
            return output;
        }

        public InterfaceOutPut Update(string data)
        {
            GanBu input = Newtonsoft.Json.JsonConvert.DeserializeObject<GanBu>(data);
            InterfaceOutPut output = new InterfaceOutPut();
            new DBGanBu().Update(input);
            output.result = 0;
            return output;
        }

        public class Query_Input
        {
            public string TypeID;
            public string TrainmanNumber;
            public string WorkShopGUID;
        }
        public InterfaceOutPut Query(string data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            Query_Input input = Newtonsoft.Json.JsonConvert.DeserializeObject<Query_Input>(data);
            output.data = new DBGanBu().Query(input.WorkShopGUID, input.TrainmanNumber, input.TypeID);
            output.result = 0;
            return output;
        }


     

    }
    #endregion

    #region 获取部门列表

    public class LCDepartment
    {
        //获取部门列表
        public InterfaceOutPut Query(string data)
        {
            InterfaceOutPut output = new InterfaceOutPut();
            DBDepartment d = new DBDepartment();
            output.resultStr = "获取数据成功！";
            output.data = d.GetDepartmentList();
            return output;
        }
    }
    #endregion

    #region  值班员
    public class DutyUser
    {
        public class Get_In
        {
            public string strWorkShopGUID;

        }
        public class Get_Out
        {
            public string result = "";
            public string resultStr = "";
            public List<trainMan> data;
        }
        public Get_Out GetDutyUserList(string data)
        {
            Get_Out json = new Get_Out();
            try
            {
                Get_In input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_In>(data);
                DataTable dt = this.GetDutyUserdt(input.strWorkShopGUID);
                List<trainMan> Ltm = new List<trainMan>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strTrainmanNumber = dt.Rows[i]["strNumber"].ToString();
                    string strTrainmanName = dt.Rows[i]["strName"].ToString();
                    string strTypeId = dt.Rows[i]["strTypeId"].ToString();
                    string strTypeName = "";
                    if (strTypeId == "1")
                        strTypeName = "甲";
                    else if (strTypeId == "2")
                        strTypeName = "乙";
                    else if (strTypeId == "3")
                        strTypeName = "丙";
                    else if (strTypeId == "4")
                        strTypeName = "丁";
                    else if (strTypeId == "5")
                        strTypeName = "备";
                    else
                        strTypeName = "";

                    string nPostID = "";
                    trainMan tm = new trainMan();
                    tm.tmName = strTrainmanName;
                    tm.tmNumber = strTrainmanNumber;
                    tm.typeName = strTypeName;
                    tm.nPostID = nPostID;
                    Ltm.Add(tm);
                }

                json.data = Ltm;
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

        public DataTable GetDutyUserdt(string strWorkShopGUID)
        {
            //获取该车间下的所有干部信息
            string strSql = @"   select * from TAB_Org_NeiWaiQin  where strWorkShopGUID = @strWorkShopGUID order by strTypeId";
            SqlParameter[] sqlParams = {
                                               new SqlParameter("strWorkShopGUID",strWorkShopGUID)
                                       };
            return SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlParams).Tables[0];

        }
        //人员列表
        public class trainMan
        {
            public string tmNumber;
            public string tmName;
            public string tmGUID;
            public string nPostID;
            public string tmGuideGroupName;
            public string typeName;
        }
    }
    #endregion 获取值班员
}
             