using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.CommonUtility;
using System.Web.Script.Serialization;
using TF.RunSafty.Utils.Parse;

namespace TF.RunSafty.NamePlate
{
    public class GetPositionOfTM
    {
        InterfaceRet _Ret = new InterfaceRet();
        #region 实体对象
        public class InGetTrainman
        {
            //所查找的人员的工号
            public string Number;
        }
        public class NpSearchResult
        {
            public Boolean Find;
            public string JlID = "";
            public string JlName = "";
            public string PlaceID = "";
            public string PlaceName = "";
            public string GrpID = "";
            public int TmState = 0;
            public int TxState = 0;
        }
        public class TmInfo
        {
            public string Number;
            public string Name;
            public int State;
            public string TMID;
            public string TMJLID;
        }
        public class TmJl
        {
            public string ID;
            public string Name;
            public int JlType;
        }
        public class Place
        {
            public string strPlaceID;
            public string strPlaceName;
        }
        public class NamePlate
        {
            public string strGroupGUID;
            public int nTXState;
            public string strTMJL;
        }
        #endregion

        #region 主函数
        public InterfaceRet Find(string Data)
        {

            try
            {
                _Ret.Clear();
                _Ret.result = 0;
                _Ret.resultStr = "";
                NpSearchResult ret = new NpSearchResult();
                ret.Find = false;
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                InGetTrainman InParams = javaScriptSerializer.Deserialize<InGetTrainman>(Data);


                //获取人员基本信息
                TmInfo tm = GetTmInfo(InParams.Number);
                if (tm != null)
                {
                    Dictionary<string, TmJl> jlDict = GetJlDict();
                    ret.Find = true;
                    ret.TmState = tm.State;
                    ret.JlID = tm.TMJLID;
                    //通过人员id获取机组id，机组状态，交路id
                    NamePlate np = GetNamePlate(tm.TMID);
                    if (null != np)
                    {
                        ret.GrpID = np.strGroupGUID;
                        ret.TxState = np.nTXState;
                      
                    }
                    if (jlDict.ContainsKey(ret.JlID))
                    {
                        ret.JlName = jlDict[ret.JlID].Name;
                        //轮乘交路，获取出勤点
                        if (jlDict[ret.JlID].JlType == 3)
                        {
                            Place p = GetPlace(tm.TMID);
                            if (p != null)
                            {
                                ret.PlaceID = p.strPlaceID;
                                ret.PlaceName = p.strPlaceName;
                            }
                        }
                    }
                    else
                    {
                        if (tm.State == 1)
                            ret.TmState = 7;
                    }
                }
                _Ret.data = ret;


            }
            catch (Exception ex)
            {

                _Ret.resultStr = ex.Message;
                LogClass.log("Interface.Find:" + ex.Message);
                throw ex;
            }
            return _Ret;
        }
        #endregion

        #region 获取人员信息
        public TmInfo GetTmInfo(string Number)
        {

            string strSql = "select top 1 strTrainmanName,strTrainmanNumber,strTrainmanGUID,nTrainmanState,strTrainmanJiaoluGUID from TAB_Org_Trainman where strTrainmanNumber = '" + Number + "'";
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                TmInfo Ti = new TmInfo();
                Ti.Number = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanNumber"]);
                Ti.State = ObjectConvertClass.static_ext_int(dt.Rows[0]["nTrainmanState"]);
                Ti.Name = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanName"]);
                Ti.TMID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanGUID"]);
                Ti.TMJLID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strTrainmanJiaoluGUID"]);
                return Ti;
            }
            return null;
        }
        #endregion

        #region 获取出勤点
        public Place GetPlace(string TmId)
        {
            string strSql = @" SELECT dp.strPlaceName, dp.strPlaceID FROM TAB_Nameplate_Group ng LEFT OUTER JOIN
                       TAB_Base_DutyPlace dp ON ng.strPlaceID = dp.strPlaceID where  ng.strTrainmanGUID1=@strTrainmanGUID 
                    or ng.strTrainmanGUID2=@strTrainmanGUID  or ng.strTrainmanGUID3=@strTrainmanGUID or ng.strTrainmanGUID4=@strTrainmanGUID";

            SqlParameter[] sqlparams = new SqlParameter[]{
                        new SqlParameter("strTrainmanGUID",TmId)
                    };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlparams).Tables[0];
           
            if (dt.Rows.Count > 0)
            {
                Place p = new Place();
                p.strPlaceID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strPlaceID"]);
                p.strPlaceName = ObjectConvertClass.static_ext_string(dt.Rows[0]["strPlaceName"]);
                return p;
            }
            return null;
        }
        #endregion

        #region 获取人员区段
        public Dictionary<string, TmJl> GetJlDict()
        {
            string strSql = @"select  * from  TAB_Base_TrainmanJiaolu";
            DataTable dtTrainmanJiaolu = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql).Tables[0];
            Dictionary<string, TmJl> d = new Dictionary<string, TmJl>();
            for (int i = 0; i < dtTrainmanJiaolu.Rows.Count; i++)
            {
                TmJl tmjl = new TmJl();
                tmjl.ID = ObjectConvertClass.static_ext_string(dtTrainmanJiaolu.Rows[i]["strTrainmanJiaoluGUID"]);
                tmjl.JlType = ObjectConvertClass.static_ext_int(dtTrainmanJiaolu.Rows[i]["nJiaoluType"]);
                tmjl.Name = ObjectConvertClass.static_ext_string(dtTrainmanJiaolu.Rows[i]["strTrainmanJiaoluName"]);
                d.Add(tmjl.ID, tmjl);
            }
            return d;
        }
        #endregion

        #region 获取名牌
        public NamePlate GetNamePlate(string TmId)
        {
            string strSql = @" SELECT  * FROM TAB_Nameplate_Group where strTrainmanGUID1=@strTrainmanGUID  or strTrainmanGUID2=@strTrainmanGUID
 or  strTrainmanGUID3=@strTrainmanGUID or  strTrainmanGUID4=@strTrainmanGUID";
            SqlParameter[] sqlparams = new SqlParameter[]{
                        new SqlParameter("strTrainmanGUID",TmId)
                    };
            DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, strSql, sqlparams).Tables[0];
          
            if (dt.Rows.Count > 0)
            {
                NamePlate np = new NamePlate();
                np.strGroupGUID = ObjectConvertClass.static_ext_string(dt.Rows[0]["strGroupGUID"]);
                np.nTXState = ObjectConvertClass.static_ext_int(dt.Rows[0]["nTXState"]);
                return np;
            }
            return null;
           
        }
        #endregion

    }
}
