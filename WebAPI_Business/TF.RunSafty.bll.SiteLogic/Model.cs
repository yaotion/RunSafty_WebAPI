using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.SiteLogic
{
    public class SubmitInOutRoom
    {
        public DateTime etime;//入寓时间
        public string tmid;//乘务员工号
        public string tmname;	//乘务员姓名
        public string stmis;//出入寓地点TMIS
        public int nresult;//暂时无用
        public string nVerify;//登录方式
        public JsonTestResult strResult;//
        public string workid;//入寓记录ID

        public string strTrainType;// 车型
        public string strTrainNum;// 车号
        public string strCheCi;// 车次
        public int nLoginType;// 登记类型
        public string PhotoID;// 登记照片
        public int IsLackOfRest;// 寓休情况

        public string LackReason;//     不足原因
        public string ShenHeNumber;//  审核工号 
        public string ShenHeName;//     审核名字 
        public string ShenHeLoginType;//审核人登记方式 
        public string ShenHePhotoID;//审核照片ID 
    }

    public class JsonTestResult
    {
        public string VerfiyID;
        public string TestAlcoholResult;
        public string TestAlcoholPictureName;
        public string Path;
        public DateTime InRoomTime;

        public string ResultToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }

    public class SubmitDrinkRec
    {
        public DateTime etime;//测酒时间
        public string tmid;//乘务员工号
        public string tmname;//乘务员姓名
        public string stmis;//测酒地点TMIS
        public int nresult;//测酒结果
        public int workType;//工作类型
        public string workid;//主记录ID
        public string testid;//测酒记录ID  

        public string ToResultString()
        {
            JsonTestResult jsResult = new JsonTestResult();
            jsResult.VerfiyID = "0";
            jsResult.TestAlcoholResult = nresult.ToString();
            jsResult.TestAlcoholPictureName = testid;
            jsResult.Path = etime.ToString("yyyy-MM-dd HH:mm:ss");

            return Newtonsoft.Json.JsonConvert.SerializeObject(jsResult);

        }
    }


    public class DrinkPic
    {
        public DateTime etime;          	//测酒时间
        public string workid; 		  	//主记录ID
        public string testid;			//测酒记录ID
        public string pic;			//照片BASE64
    }


}
