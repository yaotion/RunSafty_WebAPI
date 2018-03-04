using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections;
using TF.RunSafty.Model.InterfaceModel;

namespace TF.RunSaftyAPI.App_Api.Public
{

    #region
    public class cQueryResult
    {
        private string action = "";
        private string _cid = "";

        public string cid
        {
            get { return _cid; }
            set { _cid = value; }
        }

        public string Action
        {
            get { return action; }
            set { action = value; }
        }
        private string data = "";

        public string Data
        {
            get { return data; }
            set { data = value; }
        }
        private ParamBase paramInput;
        private ResultJsonBase paramOutput;

 
        public virtual string Execute()
        {
            return "";
        }
    }
    #endregion
    public abstract class IQueryResult
    {
        private string action = "";
        private string _cid = "";

        public string cid
        {
            get { return _cid; }
            set { _cid = value; }
        }

        public string Action
        {
            get { return action; }
            set { action = value; }
        }
        private string data = "";

        public string Data
        {
            get { return data; }
            set { data = value; }
        }

        private HttpContext context;

        public HttpContext Context
        {
            get { return context; }
            set { context = value; }
        }

        public abstract string QueryResult();
    }
    public class ResultFactory : Hashtable
    {
        public ResultFactory()
        {
            //LED大屏
            this.Add("getledfile", new iLedFile());
            this.Add("getbanxu", new IBanXu());
            this.Add("getleave", new IKaoQin());
            this.Add("getversioninfo", new IUpdateInfo());
            this.Add("getbanxuconfig", new IGetledbanxu());

            //LED显示行车计划
            this.Add("getledtrainmanplan", new IGetTrainmanPlanForLed());

            //获取车间
            this.Add("getworkshop", new IGetOrgWorkShop());

            //获取交路
            this.Add("gettrainjiaolu", new IGetTrainJiaoLu());

            //通过GUID获取行车计划
            this.Add("gettrainplanbyid", new IGetTrainPlanById());


            //自助端操作
            this.Add("syncdoc", new IReadingSync());
            this.Add("getreadrecord", new IReadingRecord());
            this.Add("submitdeliverprinttime", new IJSPrint());
            this.Add("checkdeliverprinted", new JsPrintCheck());
            this.Add("submitattendance", new IReadingUpload());
            this.Add("getworktime", new IGetWorkTime());

            this.Add("getplacesofclient", new IPlaceOfClient());
            this.Add("getplacesoftrainjiaolu", new IPlaceOfJiaolu());


            //图定车次
            this.Add("gettrainnosoftrainjiaolu", new ITrainnosOfTrainJiaolu());
            this.Add("addtrainno", new IAddTrainno());
            this.Add("gettrainnosbyid", new ITrainnosByID());
            this.Add("edittrainno", new IEditTrainno());
            this.Add("deletetrainno", new IDeleteTrainno());
            this.Add("loadtrainnosbytime", new IGetTrainnosByTime());

            //***********签点计划
            //添加  获取图定车次
            this.Add("getsignplanlist", new IGetPlanRest());
            //添加补充签点计划
            this.Add("addsignplan", new IAddSignPlan());
            //修改补充签点计划
            this.Add("editsignplan", new IModiFySignPlan());
            //删除补充签点计划
            this.Add("delsignplan", new IDelSignPlan());
            //获取签点计划
            this.Add("getsignplanlistbyjiaoluary", new IGetPlanRestByWorkShopAndTime());
            //修改图定车次  行车计划
            this.Add("modifysignplan", new IEditPlanRest());
            ////修改图定车次  行车计划  添加签点计划
            //this.Add("addplanrest", new IAddPlanRestFirst());
            //修改人员
            this.Add("modifysignplantrainman", new IEditTrainman());
            //获取待乘计划
            this.Add("getwaitworksignplan", new IGetWaitWorkSignPlan());



            //行车计划
            this.Add("gettrainjiaolutrainmanplansofsite", new ITrainmanPlansOfSite());
            this.Add("gettrainjiaolusenttrainmanplansofsite", new ISentTrainmanPlansOfSite());
            this.Add("getchuqinplansofsite", new IChuqinPlansOfSite());
            this.Add("gettuiqinplansofsite", new ITuiqinPlansOfSite());
            this.Add("getchuqinplansoftrainmaninsite", new IChuqinPlansOfTrainmanInSite());
            this.Add("gettuiqinplansoftrainmaninsite", new ITuiqinPlansOfTrainmanInSite());
            this.Add("createeditabledtrainplan", new ICreateEditabledTrainPlan());
            this.Add("createacceptabledtrainplan", new ICreateAcceptabledTrainPlan());


            //电子名牌
            this.Add("getordergroupofplaceintrainmanjiaolu", new IOrderGroup());
            this.Add("getgroupofplaceintrainmanjiaolu", new INameGroup());
            this.Add("changegroupplace", new ChangeGroupPlace());
            this.Add("endwork", new IExecTuiQin());

            //短信叫班
            this.Add("submitneedcallworkmsg", new ISubmitNeedCallWorkMsg());
            this.Add("getcallworkmsg", new IGetCallWorkMsg());
            this.Add("submitcallworkmsg", new ISubmitCallWorkMsg());
            this.Add("cancelcallworkmsg", new ICancelNeedCallWorkMsg());
            //获取叫班信息
            this.Add("getcallwork", new IGetCallWork());
            //个性化出勤
            this.Add("gexinghuachuqin", new GetGeXingHuaChuQin());

            ////出乘计划Plan_ToBeTake
            //this.Add("addplantobetake", new IPlan_ToBeTake());
            //获取待乘计划
            this.Add("getwaitworkplan", new IPlan_BeginWork());
            //添加入寓记录
            this.Add("addplaninroom", new IPlan_InRoom());

            //添加入寓记录
            this.Add("addplanoutroom", new IPlan_OutRoom());

            //获取服务器时间
            this.Add("now", new GetServerNowTime());
        }

        public IQueryResult GetFactory(string name)
        {
            return (IQueryResult)this[name];
        }
    }
}