using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.DBUtility;
using TF.RunSafty.Model.InterfaceModel;

namespace TF.RunSafty.BLL
{
   public partial class Tab_Plan_Rest
    {

       private readonly TF.RunSafty.DAL.Tab_Plan_Rest dal=new TF.RunSafty.DAL.Tab_Plan_Rest();

       public List<TF.RunSafty.Model.TAB_Plan_Rest> GetPlanListByTimeAndWorkShop(string strWorkShopGUID, string DtBeginDate, string DtEndDate)
       {
           DataSet set = dal.GetGetPlanRestByTimeAndWorkShop(strWorkShopGUID, DtBeginDate, DtEndDate);
           DataTable table = set.Tables[0];
           DataView view = table.DefaultView;
           table = view.ToTable();
           return DataTableToList(table);
       }


       public List<TF.RunSafty.Model.TAB_Plan_Rest> GetPlanTrain(string DtBeginDate, string DtEndDate, string strJiaoluGUID)
       {
           string dtNewDateBegin = "";
           string dtNewDateEnd = "";

          
           dtNewDateBegin = Convert.ToDateTime(DtBeginDate).ToString("yyyy-MM-dd") + " 00:00:00";
           dtNewDateEnd = Convert.ToDateTime(DtEndDate).ToString("yyyy-MM-dd") + " 00:00:00";     

      
           TimeSpan dtsp = Convert.ToDateTime(dtNewDateEnd) - Convert.ToDateTime(dtNewDateBegin) ;
           int spDays = dtsp.Days + 1;

           TF.RunSafty.Model.TAB_Plan_Rest TPRModel = new Model.TAB_Plan_Rest();
           DataTable dt = GetTuDingCheCi();
           for (int d = 0; d < spDays; d++)
           {

               string DtBeginDateTime = Convert.ToDateTime(dtNewDateBegin).AddDays(d).ToString("yyyy-MM-dd") + " 00:00:00";
               string DtEndDateTime = Convert.ToDateTime(dtNewDateBegin).AddDays(d + 1).ToString("yyyy-MM-dd") + " 00:00:00";

               string strDtBeginDateTime = Convert.ToDateTime(DtBeginDateTime).ToString("yyyy-MM-dd");
               int planCount = dal.CountPlan(DtBeginDateTime, DtEndDateTime, strJiaoluGUID);
               if (planCount == 0)
               {
                   for (int i = 0; i < dt.Rows.Count; i++)
                   {
                       //出勤时间
                       if (dt.Rows[i]["dtStartTime"].ToString() != "" && dt.Rows[i]["dtStartTime"] != null)
                       {
                           TPRModel.dtChuQinTime = Convert.ToDateTime(strDtBeginDateTime + " " + Convert.ToDateTime(dt.Rows[i]["dtStartTime"].ToString()).ToShortTimeString()).ToString();
                       }
                       else
                       {
                           TPRModel.dtChuQinTime = "";
                       }



                       string comDtStartTime = dt.Rows[i]["dtStartTime"].ToString();
                       string Ndate = Convert.ToDateTime(strDtBeginDateTime).AddDays(0).ToString("yyyy-MM-dd");
                       string Ydate = Convert.ToDateTime(strDtBeginDateTime).AddDays(-1).ToString("yyyy-MM-dd");
                       string Tdate = Convert.ToDateTime(strDtBeginDateTime).AddDays(1).ToString("yyyy-MM-dd");



                       //侯班时间
                       if (dt.Rows[i]["dtArriveTime"].ToString() != "")
                       {
                            string comDtArriveTime = dt.Rows[i]["dtArriveTime"].ToString();
                            if (Convert.ToDateTime(Convert.ToDateTime(comDtArriveTime).ToString("HH:mm")) > Convert.ToDateTime(Convert.ToDateTime(comDtStartTime).ToString("HH:mm")))
                            {
                                TPRModel.dtArriveTime = Convert.ToDateTime(Ydate + " " + Convert.ToDateTime(dt.Rows[i]["dtArriveTime"].ToString()).ToString("HH:mm")).ToString();
                            }
                            else
                            {
                                TPRModel.dtArriveTime = Convert.ToDateTime(Ndate + " " + Convert.ToDateTime(dt.Rows[i]["dtArriveTime"].ToString()).ToString("HH:mm")).ToString();
                            }
                       }

                       else
                       {
                           TPRModel.dtArriveTime = "";
                       }


                       //叫班时间
                       if (dt.Rows[i]["dtCallTime"].ToString() != "")
                       {
                           string comDtCallTime = dt.Rows[i]["dtCallTime"].ToString();

                           if (Convert.ToDateTime(Convert.ToDateTime(comDtCallTime).ToString("HH:mm")) > Convert.ToDateTime(Convert.ToDateTime(comDtStartTime).ToString("HH:mm")))
                           {

                               TPRModel.dtCallTime = Convert.ToDateTime(Ydate + " " + Convert.ToDateTime(dt.Rows[i]["dtCallTime"].ToString()).ToString("HH:mm")).ToString();
                           }
                           else
                           {
                               TPRModel.dtCallTime = Convert.ToDateTime(Ndate + " " + Convert.ToDateTime(dt.Rows[i]["dtCallTime"].ToString()).ToString("HH:mm")).ToString();
                           }
                       }

                       else
                       {
                           TPRModel.dtCallTime = "";
                       }


                       //开车时间
                       if (dt.Rows[i]["dtStartTrainTime"].ToString() != "")
                       {
                           string comDtStartTrainTime = dt.Rows[i]["dtStartTrainTime"].ToString();
                           if (Convert.ToDateTime(Convert.ToDateTime(comDtStartTrainTime).ToString("HH:mm")) >= Convert.ToDateTime(Convert.ToDateTime(comDtStartTime).ToString("HH:mm")))
                           {

                               TPRModel.dtStartTrainTime = Convert.ToDateTime(Ndate + " " + Convert.ToDateTime(dt.Rows[i]["dtStartTrainTime"].ToString()).ToString("HH:mm")).ToString();
                           }
                           else
                           {
                               TPRModel.dtStartTrainTime = Convert.ToDateTime(Tdate + " " + Convert.ToDateTime(dt.Rows[i]["dtStartTrainTime"].ToString()).ToString("HH:mm")).ToString();
                           
                           }
                       }
                       else
                       {
                           TPRModel.dtStartTrainTime = "";
                       }


                       TPRModel.strTrainNo = dt.Rows[i]["strTrainNo"].ToString();
                       TPRModel.strGUID = System.Guid.NewGuid().ToString();
                       TPRModel.strTrainJiaoLuGUID = dt.Rows[i]["strTrainJiaoLuGUID"].ToString();
                       TPRModel.nNeedRest = Convert.ToInt32(dt.Rows[i]["nNeedRest"].ToString());
                       dal.AddPlan(TPRModel);
                   }
               
               }

           }




           DataSet set = dal.GetPlanRest(DtBeginDate, DtEndDate, strJiaoluGUID);
           DataTable table = set.Tables[0];
           DataView view = table.DefaultView;
           table = view.ToTable();
           return DataTableToList(table);
       }
           

       public DataTable GetTuDingCheCi()
       {
           return dal.GetTuDingCheCi();
       }


       /// <summary>
       /// 获得数据列表
       /// </summary>
       public List<TF.RunSafty.Model.TAB_Plan_Rest> DataTableToList(DataTable dt)
       {
           List<TF.RunSafty.Model.TAB_Plan_Rest> modelList = new List<TF.RunSafty.Model.TAB_Plan_Rest>();
           int rowsCount = dt.Rows.Count;
           if (rowsCount > 0)
           {
               TF.RunSafty.Model.TAB_Plan_Rest model;
               for (int n = 0; n < rowsCount; n++)
               {
                   model = dal.DataRowToModel2(dt.Rows[n]);
                   if (model != null)
                   {
                       modelList.Add(model);
                   }
               }
           }
           return modelList;
       }





       public List<TF.RunSafty.Model.TAB_Plan_Rest> GetPlaceList(List<TF.RunSafty.Model.TAB_Plan_Rest> placeList)
       {
           if (placeList != null)
           {
               List<TF.RunSafty.Model.TAB_Plan_Rest> resultList = new List<TF.RunSafty.Model.TAB_Plan_Rest>();
               foreach (TF.RunSafty.Model.TAB_Plan_Rest place in placeList)
               {
                   TF.RunSafty.Model.TAB_Plan_Rest model = new TF.RunSafty.Model.TAB_Plan_Rest();
                   model.dtArriveTime = place.dtArriveTime;
                   model.dtCallTime = place.dtCallTime;
                   model.nID = place.nID;
                   model.strGUID = place.strGUID;
                   model.strTrainJiaoLuGUID = place.strTrainJiaoLuGUID;
                   model.strTrainmanGUID1 = place.strTrainmanGUID1;
                   model.strTrainmanGUID2 = place.strTrainmanGUID2;
                   model.strTrainmanGUID3 = place.strTrainmanGUID3;
                   model.strTrainmanGUID4 = place.strTrainmanGUID4;
                   model.strTrainNoGUID = place.strTrainNoGUID;
                   model.strWorkGrouGUID = place.strWorkGrouGUID;
                   model.strTrainNo = place.strTrainNo;
                   model.dtChuQinTime = place.dtChuQinTime;
                   model.dtStartTrainTime = place.dtStartTrainTime;
                   model.dtSignTime = place.dtSignTime;
                   model.ePlanState = place.ePlanState;
                   model.nNeedRest = place.nNeedRest;
                   model.nFinished = place.nFinished;
                   model.strTrainmanName1 = place.strTrainmanName1;
                   model.strTrainmanName2 = place.strTrainmanName2;
                   model.strTrainmanName3 = place.strTrainmanName3;
                   model.strTrainmanName4 = place.strTrainmanName4;

                   model.strTrainmanNumber1 = place.strTrainmanNumber1;
                   model.strTrainmanNumber2 = place.strTrainmanNumber2;
                   model.strTrainmanNumber3 = place.strTrainmanNumber3;
                   model.strTrainmanNumber4 = place.strTrainmanNumber4;
                   resultList.Add(model);
               }
               return resultList;
           }
           return null;
       }



       /// <summary>
       /// 更新一条数据
       /// </summary>
       public bool Update(TF.RunSafty.Model.TAB_Plan_Rest model)
       {
           return dal.Update(model);
       }


       /// <summary>
       /// 更新一条数据
       /// </summary>
       public bool UpdateAndFirst(TF.RunSafty.Model.TAB_Plan_Rest model,string JiaoLuGUID)
       {
           return dal.UpdateAndFirst(model, JiaoLuGUID);
       }




       public bool UpdateByParamModel(TF.RunSafty.Model.InterfaceModel.PlanRestModel paramModel,string IsOrNotIndexes)
       {
           TF.RunSafty.Model.TAB_Plan_Rest Plan_Rest = this.GetModel(paramModel.data.strguid);
           SetModelValue(paramModel, Plan_Rest);

           if (IsOrNotIndexes == "")
           {
               return this.Update(Plan_Rest);
           }
           else
           {
               return this.UpdateAndFirst(Plan_Rest,Plan_Rest.strTrainJiaoLuGUID);
           }
       }


       /// <summary>
       /// 得到一个对象实体
       /// </summary>
       public TF.RunSafty.Model.TAB_Plan_Rest GetModel(string strGUID)
       {

           return dal.GetModel(strGUID);
       }

       public void SetModelValue(TF.RunSafty.Model.InterfaceModel.PlanRestModel PlanRestModel, TF.RunSafty.Model.TAB_Plan_Rest Plan_Rest)
       {
           string dt = DateTime.Now.ToString();
           if (PlanRestModel.data.dtarrivetime.ToString() != "")
               Plan_Rest.dtArriveTime = PlanRestModel.data.dtarrivetime.ToString();
           else
               Plan_Rest.dtArriveTime = dt;
           if (PlanRestModel.data.dtcalltime.ToString() != "")
               Plan_Rest.dtCallTime = PlanRestModel.data.dtcalltime.ToString();
           else
               Plan_Rest.dtCallTime = dt;

           if (PlanRestModel.data.dtchuqintime.ToString() != "")
               Plan_Rest.dtChuQinTime = PlanRestModel.data.dtchuqintime.ToString();
           else
               Plan_Rest.dtChuQinTime = dt;

           if (PlanRestModel.data.dtStartTrainTime.ToString() != "")
           Plan_Rest.dtStartTrainTime = PlanRestModel.data.dtStartTrainTime.ToString();
           else
               Plan_Rest.dtStartTrainTime = dt;

           if (PlanRestModel.data.dtSignTime.ToString() != "")
               Plan_Rest.dtSignTime = PlanRestModel.data.dtSignTime.ToString();
           else
               Plan_Rest.dtSignTime = dt;




           Plan_Rest.strGUID = PlanRestModel.data.strguid;
           Plan_Rest.strTrainJiaoLuGUID = PlanRestModel.data.strtrainjiaoluguid;
           Plan_Rest.strTrainmanGUID1 = PlanRestModel.data.strtrainmanguid1;
           Plan_Rest.strTrainmanGUID2 = PlanRestModel.data.strtrainmanguid2;
           Plan_Rest.strTrainmanGUID3 = PlanRestModel.data.strtrainmanguid3;
           Plan_Rest.strTrainmanGUID4 = PlanRestModel.data.strtrainmanguid4;
           Plan_Rest.strTrainNo = PlanRestModel.data.strtrainno;
           Plan_Rest.strTrainNoGUID = PlanRestModel.data.strtrainnoguid;
           Plan_Rest.strWorkGrouGUID = PlanRestModel.data.strworkgrouguid;
           Plan_Rest.nNeedRest = PlanRestModel.data.nNeedRest;
           Plan_Rest.nFinished = PlanRestModel.data.nFinished; ;
           if (PlanRestModel.data.ePlanState != "")
               Plan_Rest.ePlanState = int.Parse(PlanRestModel.data.ePlanState);
           else
               Plan_Rest.ePlanState = 0;

       }
		
  


      
    }
}
