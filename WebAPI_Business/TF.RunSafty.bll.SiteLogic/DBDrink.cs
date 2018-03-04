using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkFreely.DBUtility;
using System.Data;
using System.Data.SqlClient;
using ThinkFreely.RunSafty;
using TF.Runsafty.Plan.MD;
using TF.RunSafty.DrinkLogic;


namespace TF.RunSafty.SiteLogic
{
    class DBDrink
    {
        private static string CreateImgPath(SubmitDrinkRec Rec)
        {
            return "/DrinkImage/" +
                Rec.etime.Year.ToString() + "/" + Rec.etime.Month.ToString() + "/"+ 
                Rec.etime.Day.ToString() + "/" + Rec.testid + ".jpg";
                
            
        }
        public static void InsertDrinkRecord(SubmitDrinkRec Rec,SqlTransaction trans)
        {
            string placeName = DBDictionary.GetDutyPlaceID(Rec.stmis);

            Trainman trainman = new Trainman();

            DBDictionary.GetTrainman(Rec.tmid,trainman);


            #region 添加测酒记录
            MDDrink MDDr = new MDDrink();
            TF.Runsafty.Plan.DB.DBDrink   DBDr = new Runsafty.Plan.DB.DBDrink();
            //职位信息----- 开始----------
            DBDrinkLogic dbdl = new DBDrinkLogic();
            MDDrinkLogic mddl = new MDDrinkLogic();
            mddl = dbdl.GetDrinkCadreEntity(Rec.tmid);
            if (mddl != null)
            {
                MDDr.strDepartmentID = mddl.strDepartmentID;
                MDDr.strDepartmentName = mddl.strDepartmentName;
                MDDr.nCadreTypeID = mddl.nCadreTypeID;
                MDDr.strCadreTypeName = mddl.strCadreTypeName;
            }
            //职位信息----- 结束----------

            //是否是本段
            MDDr.nLocalAreaTrainman = 0;
            MDDr.trainmanID = trainman.tmGUID;
            MDDr.createTime = DateTime.Now.ToString();
            MDDr.verifyID = 0;
            MDDr.oPlaceId = "";
            MDDr.strGuid = Guid.NewGuid().ToString();
            MDDr.drinkResult = Rec.nresult.ToString();
            MDDr.strAreaGUID = "";
            MDDr.dutyUserID = "";
            MDDr.strTrainmanName = trainman.tmname;
            MDDr.strTrainmanNumber = Rec.tmid;
            MDDr.strTrainNo = "";
            MDDr.strTrainNumber = "";
            MDDr.strTrainTypeName = "";
            MDDr.strWorkShopGUID = trainman.workShopID;
            MDDr.strWorkShopName = trainman.workShopName;
            MDDr.strPlaceID = Rec.stmis;
            MDDr.strPlaceName = placeName;
            MDDr.strSiteGUID = "";
            MDDr.strSiteName = "";
            MDDr.dwAlcoholicity = "";
            MDDr.strWorkID = "";
            MDDr.nWorkTypeID = Rec.workType;//工作类型为退勤
            MDDr.imagePath = CreateImgPath(Rec);
            SqlTrans sqltrans = new SqlTrans();
            sqltrans.Begin();
            try
            {
                DBDr.SubmitDrink(MDDr, sqltrans.trans);
                sqltrans.Commit();
            }
            catch(Exception ex)
            {
                sqltrans.RollBack();
                throw ex;
            }
            #endregion

        }
    }
}
