using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TF.RunSafty.GoodsMgr
{
   public class LCGoodsMgr
   {
       #region 1.5.1获取物品类型）
       public class Get_InGetGoodType
        {
        }
       public class Get_OutGetGoodType
        {
            public string result = "";
            public string resultStr = "";
            public ResultsGetGoodType data;
        }

       public class ResultsGetGoodType
       {
           public List<LendingType> lendingTypeList;
       }

       public Get_OutGetGoodType GetGoodType(string data)
        {
            Get_OutGetGoodType json = new Get_OutGetGoodType();
            try
            {
                Get_InGetGoodType input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetGoodType>(data);
                DBGoodsMgr db = new DBGoodsMgr();

                ResultsGetGoodType RGT=new ResultsGetGoodType();
                RGT.lendingTypeList= db.GetGoodType();
                json.data = RGT;
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

       #region 1.5.2获取物品状态类型
       public class Get_InGetStateNames
       {
       }
       public class Get_OutGetStateNames
       {
           public string result = "";
           public string resultStr = "";
          public ResultGetStateNames data;
       }

       public class ResultGetStateNames
       {
           public List<ReturnStateType> returnStateList;
       }


       public Get_OutGetStateNames GetStateNames(string data)
       {
           Get_OutGetStateNames json = new Get_OutGetStateNames();
           try
           {
               Get_InGetStateNames input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetStateNames>(data);
               DBGoodsMgr db = new DBGoodsMgr();
               ResultGetStateNames Rgs=new ResultGetStateNames();
               Rgs.returnStateList=db.GetStateNames();
               json.data = Rgs;
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

       #region 1.5.3发放物品
       public class Get_InSendGoods
       {
           public string TrainmanGUID;
           public string remark;
           public string WorkShopGUID;
           public bool UsesGoodsRange;
           public List<LendingInfoDetail> lendingDetailList;
       }
       public class Get_OutSendGoods
       {
           public string result = "";
           public string resultStr = "";
       }
       public Get_OutSendGoods Send(string data)
       {
           Get_OutSendGoods json = new Get_OutSendGoods();
           try
           {
               Get_InSendGoods input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InSendGoods>(data);
               DBGoodsMgr db = new DBGoodsMgr();
               foreach (LendingInfoDetail l in input.lendingDetailList)
               {
                   if (db.CheckLendAble(l, input.WorkShopGUID))
                   {                     
                       throw new ArgumentOutOfRangeException(l.strLendingTypeAlias + " " + l.strLendingExInfo + "已经出借，无法再次借出!");
                   }
                   if (input.UsesGoodsRange)
                   {
                       if (!db.IsGoodInRange(l.nLendingType, l.strLendingExInfo, input.WorkShopGUID))
                       {
                           throw new ArgumentOutOfRangeException(l.strLendingTypeAlias + " " + l.strLendingExInfo + "物品编码不在指定的编码范围内,请检查!");
                       }
                   }
               }
               db.SendLendingInfo(input.TrainmanGUID, input.remark, input.lendingDetailList);
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

       #region 1.5.4归还物品
       public class Get_InGiveBackLendingInfo
       {
           public string TrainmanGUID;
           public string remark;
           public List<LendingInfoDetail> lendingDetailList;
       }
       public class Get_OutGiveBackLendingInfo
       {
           public string result = "";
           public string resultStr = "";
       }
       public Get_OutGiveBackLendingInfo Recieve(string data)
       {
           Get_OutGiveBackLendingInfo json = new Get_OutGiveBackLendingInfo();
           try
           {
               Get_InGiveBackLendingInfo input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGiveBackLendingInfo>(data);
               DBGoodsMgr db = new DBGoodsMgr();
               foreach (LendingInfoDetail l in input.lendingDetailList)
               {
                   if (!db.CheckReturnAble(input.TrainmanGUID, l))
                   {
                       throw new ArgumentOutOfRangeException("未找到" + l.strLendingTypeAlias + " " + l.strLendingExInfo + "的借用记录!");
                   }
               }

               db.GiveBackDetail(input.lendingDetailList);

               foreach (LendingInfoDetail l in input.lendingDetailList)
               {
                   db.UpdateLendingInfoRemark(l.strLendingInfoGUID, input.remark);

               }
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

       #region 1.5.5查询发放记录
       public class Get_InQueryRecord
       {
           public GoodsQueryParam queryParam;
       }
       public class Get_OutQueryRecord
       {
           public string result = "";
           public string resultStr = "";
           public RlendingInfoList data;
       }

       public class RlendingInfoList
       {
           public List<LendingInfo> lendingInfoList;
       }

       public Get_OutQueryRecord QueryRecord(string data)
       {
           Get_OutQueryRecord json = new Get_OutQueryRecord();
           try
           {
               Get_InQueryRecord input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InQueryRecord>(data);
               DBGoodsMgr db = new DBGoodsMgr();
               RlendingInfoList R = new RlendingInfoList();
               R.lendingInfoList = db.QueryRecord(input.queryParam);
               json.data = R;
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

       #region 1.5.6查询物品最新情况已借出则显示借出情况，已归还仅显示物品情况
       public class Get_InQueryGoodsNow
       {
           public string WorkShopGUID;
           public int GoodType;
           public int GoodID;
           public int orderType;

       }
       public class Get_OutQueryGoodsNow
       {
           public string result = "";
           public string resultStr = "";
           public LendingDetailList data;
       }

       public class LendingDetailList
       {
           public List<LendingInfoDetail> lendingDetailList;
       }

       public Get_OutQueryGoodsNow QueryGoodsNow(string data)
       {
           Get_OutQueryGoodsNow json = new Get_OutQueryGoodsNow();
           try
           {
               Get_InQueryGoodsNow input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InQueryGoodsNow>(data);
               DBGoodsMgr db = new DBGoodsMgr();
               LendingDetailList l = new LendingDetailList();
               l.lendingDetailList = db.QueryGoodsNow(input.WorkShopGUID, input.GoodType, input.GoodID, input.orderType);
               json.data = l;
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

       #region 1.5.7查询发放明细
       public class Get_InQueryDetails
       {
           public GoodsDetailQueryParam queryParam;

       }
       public class Get_OutQueryDetails
       {
           public string result = "";
           public string resultStr = "";
           public LendingDetailList data;
       }


       public Get_OutQueryDetails QueryDetails(string data)
       {
           Get_OutQueryDetails json = new Get_OutQueryDetails();
           try
           {
               Get_InQueryDetails input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InQueryDetails>(data);
               DBGoodsMgr db = new DBGoodsMgr();
               LendingDetailList l = new LendingDetailList();
               l.lendingDetailList = db.QueryDetails(input.queryParam);
               json.data = l;
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

       #region 1.5.8获取统计信息
       public class Get_InGetTongJiInfo
       {
           public string WorkShopGUID;

       }
       public class Get_OutGetTongJiInfo
       {
           public string result = "";
           public string resultStr = "";
           public LendingTjInfos data;
       }

       public class LendingTjInfos
       {
           public List<LendingTjInfo> lendingTjList;
       }
       public Get_OutGetTongJiInfo GetTongJiInfo(string data)
       {
           Get_OutGetTongJiInfo json = new Get_OutGetTongJiInfo();
           try
           {
               Get_InGetTongJiInfo input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetTongJiInfo>(data);
               DBGoodsMgr db = new DBGoodsMgr();
               LendingTjInfos l = new LendingTjInfos();
               l.lendingTjList = db.GetTongJiInfo(input.WorkShopGUID);
               json.data = l;
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

       #region 1.5.9判断指定人员是否有未归还的物品
       public class Get_InIsHaveNotReturnGoods
       {
           public string TrainmanGUID;

       }
       public class Get_OutIsHaveNotReturnGoods
       {
           public string result = "";
           public string resultStr = "";
           public Results data;
       }

       public class Results
       {
           public bool result;
       }
       public Get_OutIsHaveNotReturnGoods IsHaveNotReturnGoods(string data)
       {
           Get_OutIsHaveNotReturnGoods json = new Get_OutIsHaveNotReturnGoods();
           try
           {
               Get_InIsHaveNotReturnGoods input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InIsHaveNotReturnGoods>(data);
               DBGoodsMgr db = new DBGoodsMgr();
               Results r = new Results();
               r.result = db.IsHaveNotReturnGoods(input.TrainmanGUID);
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

       #region 1.5.10获得指定人员未归还物品列表
       public class Get_InGetTrainmanNotReturnLendingInfo
       {
           public string TrainmanGUID;

       }
       public class Get_OutGetTrainmanNotReturnLendingInfo
       {
           public string result = "";
           public string resultStr = "";
           public LendingDetailList data;
       }

       public Get_OutGetTrainmanNotReturnLendingInfo GetTrainmanNotReturnLendingInfo(string data)
       {
           Get_OutGetTrainmanNotReturnLendingInfo json = new Get_OutGetTrainmanNotReturnLendingInfo();
           try
           {
               Get_InGetTrainmanNotReturnLendingInfo input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetTrainmanNotReturnLendingInfo>(data);
               DBGoodsMgr db = new DBGoodsMgr();
               LendingDetailList l = new LendingDetailList();
               l.lendingDetailList = db.GetTrainmanNotReturnLendingInfo(input.TrainmanGUID);
               json.data = l;
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

       #region "删除物品及物品相关的发放归还记录"
       private class Get_InDeleteGoods
       {
           public int LendingType;
           public string LendingExInfo;
           public string WorkShopGUID;
       }

       public class Get_OutDeleteGoods
       {
           public string result = "";
           public string resultStr = "";
           public object data;
       }

       public Get_OutDeleteGoods DeleteGoods(string data)
       {
           DBGoodsMgr db = new DBGoodsMgr();
           Get_OutDeleteGoods result = new Get_OutDeleteGoods();

           try
           {
               Get_InDeleteGoods input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InDeleteGoods>(data);
               db.DeleteGoods(input.LendingType, input.LendingExInfo, input.WorkShopGUID);
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

   }
   public class LCCodeRange
   {
       #region 1.5.11获取编码范围
       public class Get_InGetGoodsCodeRange
       {
           public string WorkShopGUID;
           public int lendingType;
       }
       public class Get_OutGetGoodsCodeRange
       {
           public string result = "";
           public string resultStr = "";
           public codeRangeArrayResult data;
       }
       public class codeRangeArrayResult
       {
           public List<LendingManager> codeRangeArray;
       }
       public Get_OutGetGoodsCodeRange Get(string data)
       {
           Get_OutGetGoodsCodeRange json = new Get_OutGetGoodsCodeRange();
           try
           {
               Get_InGetGoodsCodeRange input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetGoodsCodeRange>(data);
               DBCodeRange db = new DBCodeRange();
               codeRangeArrayResult cr = new codeRangeArrayResult();
               cr.codeRangeArray = db.GetGoodsCodeRange(input.WorkShopGUID, input.lendingType);
               json.data = cr;
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

       #region 1.5.12增加编号范围

       public class Get_InInsertGoodsCodeRange
       {
           public LendingManager codeRangeEntity;
       }

       public class Get_OutInsertGoodsCodeRange
       {
           public string result = "";
           public string resultStr = "";
       }
       public Get_OutInsertGoodsCodeRange Add(string data)
       {

           Get_InInsertGoodsCodeRange model = null;
           Get_OutInsertGoodsCodeRange json = new Get_OutInsertGoodsCodeRange();
           try
           {
               model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InInsertGoodsCodeRange>(data);
               DBCodeRange db = new DBCodeRange();
               if (db.InsertGoodsCodeRange(model.codeRangeEntity))
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

       #region 1.5.13修改编号范围


       public Get_OutInsertGoodsCodeRange Update(string data)
       {

           Get_InInsertGoodsCodeRange model = null;
           Get_OutInsertGoodsCodeRange json = new Get_OutInsertGoodsCodeRange();
           try
           {
               model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InInsertGoodsCodeRange>(data);
               DBCodeRange db = new DBCodeRange();
               if (db.UpdateGoodsCodeRange(model.codeRangeEntity))
               {
                   json.result = "0";
                   json.resultStr = "返回成功，成功修改一条数据！";
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

       #region 1.5.14删除编号范围

       public class Get_InDeleteGoodsCodeRange
       {
           public string rangeGUID;
       }

       public class Get_OutDeleteGoodsCodeRange
       {
           public string result = "";
           public string resultStr = "";
       }
       public Get_OutDeleteGoodsCodeRange Delete(string data)
       {

           Get_InDeleteGoodsCodeRange model = null;
           Get_OutDeleteGoodsCodeRange json = new Get_OutDeleteGoodsCodeRange();
           try
           {
               model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InDeleteGoodsCodeRange>(data);
               DBCodeRange db = new DBCodeRange();
               if (db.DeleteGoodsCodeRange(model.rangeGUID))
               {
                   json.result = "0";
                   json.resultStr = "返回成功，成功删除一条数据！";
               }
               else
               {
                   json.result = "0";
                   json.resultStr = "执行成功，返回0条数据！";
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

}
