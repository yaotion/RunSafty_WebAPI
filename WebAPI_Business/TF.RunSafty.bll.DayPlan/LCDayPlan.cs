using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TF.RunSafty.DayPlan
{
    public class LCSystem
    {
        #region 1.6.1获取系统管理员密码
        public class Get_InIsAdmin
        {
            public string DayPlanPassWord;
        }
        public class Get_OutIsAdmin
        {
            public string result = "";
            public string resultStr = "";
            public strresult data;
        }
        public class strresult
        {
            public Boolean result = false;
        }
        public Get_OutIsAdmin IsAdmin(string data)
        {
            Get_OutIsAdmin json = new Get_OutIsAdmin();
            try
            {
                Get_InIsAdmin input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InIsAdmin>(data);
                DBsystem db = new DBsystem();
                strresult r = new strresult();

                r.result = db.IsAdmin(input.DayPlanPassWord);
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

        #region 1.6.19获取计划车型定义信息
        public class Get_InGetTrainTypes
        {
        }
        public class Get_OutGetTrainTypes
        {
            public string result = "";
            public string resultStr = "";
            public resultGetTrainTypes data;
        }
        public class resultGetTrainTypes
        {
            public List<ShortTrain> TrainTypes;
        }
        public Get_OutGetTrainTypes GetTrainTypes(string data)
        {
            Get_OutGetTrainTypes json = new Get_OutGetTrainTypes();
            try
            {
                Get_InGetTrainTypes input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetTrainTypes>(data);
                DBsystem db = new DBsystem();
                resultGetTrainTypes r = new resultGetTrainTypes();
                r.TrainTypes = db.GetTrainTypes();
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
    public class LCPlace
    {
        #region 1.6.2获取机车班计划区域
        public class Get_InQueryPlace
        {

        }
        public class Get_OutQueryPlace
        {
            public string result = "";
            public string resultStr = "";
            public resultQueryPlace data;
        }
        public class resultQueryPlace
        {
            public List<DayPlanPlace> placeList;
        }
        public Get_OutQueryPlace QueryPlace(string data)
        {
            Get_OutQueryPlace json = new Get_OutQueryPlace();
            try
            {
                Get_InQueryPlace input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InQueryPlace>(data);
                DBPlace db = new DBPlace();
                resultQueryPlace r = new resultQueryPlace();
                r.placeList = db.QueryPlace();
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
    public class LCGroup
    {
        #region 1.6.3获取指定计划区域内的机车组信息
        public class Get_InQueryGroups
        {
            public int ID;
            
        }
        public class Get_OutQueryGroups
        {
            public string result = "";
            public string resultStr = "";
            public resultQueryGroups data;
        }
        public class resultQueryGroups
        {
            public List<DayPlanItemGroup> Groups;
        }
        public Get_OutQueryGroups QueryGroups(string data)
        {
            Get_OutQueryGroups json = new Get_OutQueryGroups();
            try
            {
                Get_InQueryGroups input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InQueryGroups>(data);
                DBGroup db = new DBGroup();
                resultQueryGroups r = new resultQueryGroups();
                r.Groups = db.QueryGroups(input.ID);
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

        #region 1.6.4获取指定机车组信息
        public class Get_InGetGroup
        {

            public int DayPlanID;
            public int GroupID;
        }
        public class Get_OutGetGroup
        {
            public string result = "";
            public string resultStr = "";
            public resultGetGroup data;
        }
        public class resultGetGroup
        {
            public DayPlanItemGroup Group;
            public bool bExist;
        }
        public Get_OutGetGroup GetGroup(string data)
        {
            Get_OutGetGroup json = new Get_OutGetGroup();
            try
            {
                Get_InGetGroup input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetGroup>(data);
                DBGroup db = new DBGroup();
                resultGetGroup r = new resultGetGroup();
                bool bIsNull = true;
                r.Group = db.GetGroup(input.DayPlanID, input.GroupID,ref bIsNull);
                r.bExist = bIsNull;
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

        #region 1.6.3.3添加机组
        public class Get_InAdd
        {
            public DayPlanItemGroup group;
        }
        public class Get_OutAdd
        {
            public string result = "";
            public string resultStr = "";
        }

        public Get_OutAdd Add(string data)
        {
            Get_OutAdd json = new Get_OutAdd();
            try
            {
                Get_InAdd input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InAdd>(data);
                DBGroup db = new DBGroup();
                int d = db.Add(input.group);
                if (d == 1)
                {
                    json.result = "0";
                    json.resultStr = "成功插入一条数据";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "返回失败！成功插入" + d + "条数据！";
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

        #region 1.6.3.4 修改机组
        public class Get_InUpdate
        {
            public DayPlanItemGroup group;
        }
        public class Get_OutUpdate
        {
            public string result = "";
            public string resultStr = "";
        }

        public Get_OutUpdate Update(string data)
        {
            Get_OutUpdate json = new Get_OutUpdate();
            try
            {
                Get_InUpdate input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InUpdate>(data);
                DBGroup db = new DBGroup();
                int d = db.Update(input.group);
                if (d == 1)
                {
                    json.result = "0";
                    json.resultStr = "成功更新一条数据";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "返回失败！成功更新" + d + "条数据！";
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

        #region 1.6.3.5 删除机组
        public class Get_InDeleteGroup
        {
            public int DayPlanID;
            public int GroupID;

        }
        public class Get_OutDeleteGroup
        {
            public string result = "";
            public string resultStr = "";
        }

        public Get_OutDeleteGroup Delete(string data)
        {
            Get_OutDeleteGroup json = new Get_OutDeleteGroup();
            try
            {
                Get_InDeleteGroup input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InDeleteGroup>(data);
                DBGroup db = new DBGroup();
                int d = db.Delete(input.DayPlanID,input.GroupID);
                if (d == 1)
                {
                    json.result = "0";
                    json.resultStr = "成功删除一条数据";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "返回失败！成功删除" + d + "条数据！";
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
    public class LCModules
    {
        #region 1.6.5获取指定机车组内的计划信息
        public class Get_InQueryModules
        {

            public int GroupID;
            public int DayPlanType;
        }
        public class Get_OutQueryModules
        {
            public string result = "";
            public string resultStr = "";
            public resultQueryModules data;
        }
        public class resultQueryModules
        {
            public List<PlanModule> Modules;
        }
        public Get_OutQueryModules QueryModules(string data)
        {
            Get_OutQueryModules json = new Get_OutQueryModules();
            try
            {
                Get_InQueryModules input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InQueryModules>(data);
                DBModules db = new DBModules();
                resultQueryModules r = new resultQueryModules();
                r.Modules = db.QueryModules(input.GroupID,input.DayPlanType);
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

        #region 1.6.6添加机车计划模版信息

        public class Get_InAddModule
        {
            public PlanModule Module;
        }

        public class Get_OutAddModule
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutAddModule AddModule(string data)
        {

            Get_InAddModule model = null;
            Get_OutAddModule json = new Get_OutAddModule();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InAddModule>(data);
                DBModules db = new DBModules();
                int i=db.AddModule(model.Module);
                if (i==1)
                {
                    json.result = "0";
                    json.resultStr = "返回成功，成功插入一条数据！";
                }
                else if (i == 0)
                {
                    json.result = "1";
                    json.resultStr = "返回失败，插入0条数据！";
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

        #region 1.6.7修改机车计划模版信息

        public class Get_InUpdateModule
        {
            public PlanModule Module;
        }

        public class Get_OutUpdateModule
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutUpdateModule UpdateModule(string data)
        {

            Get_InUpdateModule model = null;
            Get_OutUpdateModule json = new Get_OutUpdateModule();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InUpdateModule>(data);
                DBModules db = new DBModules();
                int i = db.UpdateModule(model.Module);
                if (i == 1)
                {
                    json.result = "0";
                    json.resultStr = "返回成功，成功修改一条数据！";
                }
                else if (i == 0)
                {
                    json.result = "1";
                    json.resultStr = "返回失败，成功修改0条数据！";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "返回成功，成功修改" + i + "条数据！";
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

        #region 1.6.8 删除机车计划模版信息

        public class Get_InDeleteModule
        {
            public int ID;
        }

        public class Get_OutDeleteModule
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutDeleteModule DeleteModule(string data)
        {

            Get_InDeleteModule model = null;
            Get_OutDeleteModule json = new Get_OutDeleteModule();
            try
            {
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InDeleteModule>(data);
                DBModules db = new DBModules();
                int i = db.DeleteModule(model.ID);
                if (i == 1)
                {
                    json.result = "0";
                    json.resultStr = "返回成功，成功删除一条数据！";
                }                                   
                else if (i == 0)                    
                {                                   
                    json.result = "1";              
                    json.resultStr = "返回失败，成功删除0条数据！";
                }                                   
                else                                
                {                                   
                    json.result = "0";              
                    json.resultStr = "返回成功，成功删除" + i + "条数据！";
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

        #region 1.6.9获取机车信息模版信息
        public class Get_InGetModule
        {
            public int ID;
        }
        public class Get_OutGetModule
        {
            public string result = "";
            public string resultStr = "";
            public resultGetGroup data;
        }
        public class resultGetGroup
        {
            public PlanModule Module;
            public bool bExist;
        }
        public Get_OutGetModule GetModule(string data)
        {
            Get_OutGetModule json = new Get_OutGetModule();
            try
            {
                Get_InGetModule input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetModule>(data);
                DBModules db = new DBModules();
                resultGetGroup r = new resultGetGroup();
                bool bIsNull = true;
                r.Module = db.GetModule(input.ID, ref bIsNull);
                r.bExist = bIsNull;
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
    public class LCDayPlan
    {

        #region 1.6.10获取指定机车的所有机车计划信息
        public class Get_InQueryPlans
        {

            public DateTime BeginDate;
            public DateTime EndDate;
            public int GroupID;
        }
        public class Get_OutQueryPlans
        {
            public string result = "";
            public string resultStr = "";
            public resultQueryPlans data;
        }
        public class resultQueryPlans
        {
            public List<DayPlan> PlanList;
        }
        public Get_OutQueryPlans QueryPlans(string data)
        {
            Get_OutQueryPlans json = new Get_OutQueryPlans();
            try
            {
                Get_InQueryPlans input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InQueryPlans>(data);
                DBDayPlan db = new DBDayPlan();
                resultQueryPlans r = new resultQueryPlans();
                r.PlanList = db.QueryPlans(input.BeginDate, input.EndDate, input.GroupID);
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

        #region 1.6.11获取指定机车的已下发的计划信息
        public class Get_InQueryPublishPlans
        {
            public DateTime BeginDate;
            public DateTime EndDate;
            public int GroupID;
        }
        public class Get_OutQueryPublishPlans
        {
            public string result = "";
            public string resultStr = "";
            public resultQueryPublishPlans data;
        }
        public class resultQueryPublishPlans
        {
            public List<DayPlan> PlanList;
        }
        public Get_OutQueryPublishPlans QueryPublishPlans(string data)
        {
            Get_OutQueryPublishPlans json = new Get_OutQueryPublishPlans();
            try
            {
                Get_InQueryPublishPlans input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InQueryPublishPlans>(data);
                DBDayPlan db = new DBDayPlan();
                resultQueryPublishPlans r = new resultQueryPublishPlans();
                r.PlanList = db.QueryPublishPlans(input.BeginDate, input.EndDate, input.GroupID);
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

        #region 1.6.12 获取指定的机车计划信息
        public class Get_InGetPlan
        {

            public string PlanGUID;
        }
        public class Get_OutGetPlan
        {
            public string result = "";
            public string resultStr = "";
            public resultGetPlan data;
        }
        public class resultGetPlan
        {
            public DayPlan Plan;
            public bool bExist;
        }
        public Get_OutGetPlan GetPlan(string data)
        {
            Get_OutGetPlan json = new Get_OutGetPlan();
            try
            {
                Get_InGetPlan input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InGetPlan>(data);
                DBDayPlan db = new DBDayPlan();
                resultGetPlan r = new resultGetPlan();                
                r.bExist = false;
                DayPlan Plan = new DayPlan();
                if (db.GetPlan(input.PlanGUID, Plan))
                {
                    r.bExist = true;
                    r.Plan = Plan;
                }
                
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

        #region 1.6.13添加机车计划信息
        public class Get_InAddDayPlan
        {
            public DayPlan Plan;
            public ChangeLog Log;
         
        }
        public class Get_OutAddDayPlan
        {
            public string result = "";
            public string resultStr = "";
        }

        public Get_OutAddDayPlan AddDayPlan(string data)
        {
            Get_OutAddDayPlan json = new Get_OutAddDayPlan();
            try
            {
                Get_InAddDayPlan input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InAddDayPlan>(data);
                DBDayPlan db = new DBDayPlan();
                int d = db.AddDayPlan(input.Plan);
                int l = 0;
                if (d > 0)
                    l = db.AddModifyPlanLog(input.Log);

                if (d == 1 && l == 1)
                {
                    json.result = "0";
                    json.resultStr = "成功插入一条数据";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "返回失败！插入" + d + "条机车计划数据；插入" + l + "条日志信息！";
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

        #region 1.6.14修改机车计划

        public class Get_InUpdateDayPlan
        {
            public DayPlan Plan;
            public ChangeLog Log;
            public string strDutyGUID;
            public string strSiteGUID;
        }
        public Get_OutAddDayPlan UpdateDayPlan(string data)
        {
            Get_OutAddDayPlan json = new Get_OutAddDayPlan();
            try
            {
                Get_InUpdateDayPlan input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InUpdateDayPlan>(data);
                DBDayPlan db = new DBDayPlan();

                int d = db.ModifyDayPlanInfo(input.Plan);
                int l = 0;
                if (d > 0)
                {
                    l = db.AddModifyPlanLog(input.Log);
                }

              int upp=  db.UpdatePaiPlan(input.Plan.strTrainPlanGUID, input.Plan.strTrainTypeName, input.Plan.strTrainNumber, input.Plan.strRemark, input.strSiteGUID, input.strDutyGUID);




              if (d == 1 && l == 1 && upp==1)
                {
                    json.result = "0";
                    json.resultStr = "成功修改一条机车计划数据，并插入一条日志,更新一条TAB_Plan_Train信息";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "返回失败！修改" + d + "条机车计划数据；插入" + l + "条日志信息！";
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

        #region 1.6.15删除机车计划

        public class Get_InDeleteDayPlan
        {
            public string DayPlanGUID;
            public ChangeLog Log;
        }
        public class Get_OutDelete
        {
            public string result = "";
            public string resultStr = "";
            public resultDelete data;
        }
        public class resultDelete
        {
            public bool result;
        
        }

        public Get_OutDelete DeleteDayPlan(string data)
        {
            Get_OutDelete json = new Get_OutDelete();
            try
            {
                Get_InDeleteDayPlan input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InDeleteDayPlan>(data);
                DBDayPlan db = new DBDayPlan();
                resultDelete rd = new resultDelete();
                int d = db.DeleteDayPlan(input.DayPlanGUID);
                int l = 0;
                if (d > 0)
                    l = db.AddModifyPlanLog(input.Log);
                if (d >= 1 && l == 1)
                {
                    rd.result = true;
                    json.result = "0";
                    json.resultStr = "成功删除一条机车计划数据，并插入一条日志";

                }
                else
                {
                    rd.result = false;
                    json.result = "0";
                    json.resultStr = "返回失败！删除" + d + "条机车计划数据；插入" + l + "条日志信息！";
                }
                json.data = rd;

            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region 1.6.16下发机车计划

        public class Get_InSendDayPlan
        {
            public DateTime BeginTime;
            public DateTime EndTime;
            public int DayPlanID;
        }
        public class Get_OutSendDayPlan
        {
            public string result = "";
            public string resultStr = "";
        }

        public Get_OutSendDayPlan SendDayPlan(string data)
        {
            Get_OutSendDayPlan json = new Get_OutSendDayPlan();
            try
            {
                Get_InSendDayPlan input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InSendDayPlan>(data);
                DBDayPlan db = new DBDayPlan();
                int d = db.SendDayPlan(input.BeginTime, input.EndTime, input.DayPlanID);
                if (d == 1)
                {
                    json.result = "0";
                    json.resultStr = "成功修改一条数据！";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "";
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

        #region 1.6.17从模版中加载机车计划

        public class Get_InLoad
        {
            public DateTime BeginTime;
            public int DayPlanTypeID;
            public int DayPlanID;
            public DateTime EndTime;
        }
        public class Get_OutLoad
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutLoad Load(string data)
        {
            Get_OutLoad json = new Get_OutLoad();
            try
            {
                Get_InLoad input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InLoad>(data);
                DBDayPlan db = new DBDayPlan();
                DBGroup dg=new DBGroup();
                int d = 0;
                List<DayPlanItemGroup> GroupList = dg.QueryGroups(input.DayPlanID);
                foreach (DayPlanItemGroup g in GroupList)
                {
                    DataTable dt = db.QueryItemList(g.ID, input.DayPlanTypeID);
                    d += db.LoadDayPlanInfo(input.BeginTime, DateTime.Now, input.DayPlanTypeID, input.DayPlanID, g.ID, dt);
                }
                int l = db.AddDayPlan_Send(input.BeginTime, input.EndTime, input.DayPlanID, input.DayPlanTypeID);



                if (d >= 1 && l == 1)
                {
                    json.result = "0";
                    json.resultStr = "成功加载" + d + "条数据！向“Tab_DayPlan_Send”中插入" + l + "条数据";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "返回失败！成功加载0条数据！向“Tab_DayPlan_Send”中插入" + l + "条数据";
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

        #region 1.6.18导出机车计划
        public class Get_InExport
        {
            public DateTime BeginDate;
            public DateTime EndDate;
            public int DayPlanID;
            public  int DayOrNight;
        }


        public class Get_OutExport
        {
            public string result = "";
            public string resultStr = "";
            public datas data;
        }
        public class datas
        {
            public ExportDatas ExportData; 
        }
        public class ExportDatas
        {
            public List<PlanItemGroups> leftGroups;
            public List<PlanItemGroups> rightGroups;
        }
        public class PlanItemGroups
        {
            public PlanItemGroup PlanItemGroup;
            public List<DayPlan> PlanArray;
        }
        public class PlanItemGroup
        {
            public int nID;
            public string strName;
            public int nExcelSide;
            public int nExcelPos;
            public int nDayPlanID;
            public int bIsDawen;
        }


        public Get_OutExport Export(string data)
        {
            Get_OutExport json = new Get_OutExport();
            try
            {
                Get_InExport input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InExport>(data);
                DBDayPlan db = new DBDayPlan();

                List<PlanItemGroups> LLeftpgs = new List<PlanItemGroups>();
                List<PlanItemGroups> LRightpgs = new List<PlanItemGroups>();

                List<PlanItemGroup> LLeftPlanItemGroup = db.QueryLeftGroupListByID(input.BeginDate, input.EndDate, input.DayPlanID);
                foreach (PlanItemGroup pig in LLeftPlanItemGroup)
                {
                    PlanItemGroups Leftpgs = new PlanItemGroups();
                    Leftpgs.PlanArray = new List<DayPlan>();
                    Leftpgs.PlanItemGroup = pig;
                    List<DayPlan> dp = db.QueryDayPlanInfoListByGroupID(input.BeginDate, input.EndDate, pig.nID);
                    Leftpgs.PlanArray = dp;
                    LLeftpgs.Add(Leftpgs);
                }

                List<PlanItemGroup> LRightPlanItemGroup = db.QueryRightGroupListByID(input.BeginDate, input.EndDate, input.DayPlanID);
                foreach (PlanItemGroup pig in LRightPlanItemGroup)
                {
                    PlanItemGroups rightpgs = new PlanItemGroups();
                    rightpgs.PlanItemGroup = pig;
                    List<DayPlan> dp = db.QueryDayPlanInfoListByGroupID(input.BeginDate, input.EndDate, pig.nID);
                    rightpgs.PlanArray = dp;
                    LRightpgs.Add(rightpgs);
                }
                ExportDatas ed = new ExportDatas();
                ed.leftGroups = LLeftpgs;
                ed.rightGroups = LRightpgs;
                datas d = new datas();
                d.ExportData = ed;
                json.data = d;
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

        #region  修改计划为已发送
        public class Get_InSetSended
        {
            public DateTime dtBeginTime;
            public DateTime dtEndTime;
            public int nDayPlanID;
        }
        public class Get_OutSetSended
        {
            public string result = "";
            public string resultStr = "";
            public resultSetSended data;
        }
        public class resultSetSended
        {
            public List<string> plans;
        }
        public Get_OutSetSended SetSended(string data)
        {
            Get_OutSetSended json = new Get_OutSetSended();
            try
            {
                Get_InSetSended input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InSetSended>(data);
                DBDayPlan db = new DBDayPlan();
                resultSetSended rd = new resultSetSended();
                List<string> strIDs = db.GetPaiBanPlans(input.dtBeginTime, input.dtEndTime, input.nDayPlanID);
                int d = db.SetSended(input.dtBeginTime, input.dtEndTime, input.nDayPlanID);
                if (strIDs != null && d > 0)
                {
                    rd.plans = strIDs;
                    json.result = "0";
                    json.resultStr = "获取计划成功，并成功修改计划为已发送";
                }
                else
                {
                    rd.plans = new List<string>();
                    json.result = "0";
                    json.resultStr = "获取计划成功，影响修改计划条数" + d + "条";
                }
                json.data = rd;
            }
            catch (Exception ex)
            {
                json.result = "1";
                json.resultStr = "提交失败：" + ex.Message;
            }
            return json;
        }
        #endregion

        #region  1.6.5.11清除计划
        public class Get_InClearPlan
        {
            public DateTime dtBeginDate;
            public DateTime dtEndDate;
            public int nDayPlanID;
        }
        public class Get_OutClearPlan
        {
            public string result = "";
            public string resultStr = "";
        }
        public Get_OutClearPlan ClearPlan(string data)
        {
            Get_OutClearPlan json = new Get_OutClearPlan();
            try
            {
                Get_InClearPlan input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InClearPlan>(data);
                DBDayPlan db = new DBDayPlan();
                int d = db.ClearPlan(input.dtBeginDate, input.dtEndDate, input.nDayPlanID);
                if (d >=1)
                {
                    json.result = "0";
                    json.resultStr = "删除数据成功！成功删除数据" + d + "条";
                }
                else
                {
                    json.result = "0";
                    json.resultStr = "删除0条数据";
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

        #region  1.6.5.12是否已经加载过计划
        public class Get_InIsLoadedPlan
        {
            public DateTime BeginDate;
            public DateTime EndDate;
            public int nPlanType;
            public int nDayPlanID;
        }
        public class Get_OutIsLoadedPlan
        {
            public string result = "";
            public string resultStr = "";
            public resultIsLoadedPlan data;
        }
        public class resultIsLoadedPlan
        {
            public bool result;
        }
        public Get_OutIsLoadedPlan IsLoadedPlan(string data)
        {
            Get_OutIsLoadedPlan json = new Get_OutIsLoadedPlan();
            try
            {
                Get_InIsLoadedPlan input = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_InIsLoadedPlan>(data);
                DBDayPlan db = new DBDayPlan();
                resultIsLoadedPlan r = new resultIsLoadedPlan(); 
                int d = db.IsLoadedPlan(input.BeginDate, input.EndDate, input.nDayPlanID, input.nPlanType);
                if (d >= 1)
                {
                    r.result = true;
                    json.result = "0";
                    json.resultStr = "已经加载过计划";
                }
                else
                {
                    r.result = false;
                    json.result = "0";
                    json.resultStr = "尚未加载过计划";
                }
                json.data = r;
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
