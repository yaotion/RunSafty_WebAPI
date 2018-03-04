using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.NamePlate
{
    
    /// <summary>
    /// 预备人员排序字段
    /// </summary>
    public class PrepareTMOrder
    {
        public int nid = 0;
        /// <summary>
        /// 人员交路GUID
        /// </summary>
        public string TrainmanJiaoluGUID = "";
        /// <summary>
        /// 人员交路交路名称
        /// </summary>
        public string TrainmanJiaoluName = "";
        //担当职务
        public int PostID = 0;
        /// <summary>
        /// 工号
        /// </summary>
        public string TrainmanNumber = "";
        /// <summary>
        /// 工号
        /// </summary>
        public string TrainmanName = "";
        /// <summary>
        /// 序号
        /// </summary>
        public int TrainmanOrder = 0;
        //实际职位
        public int nTrainmanState = 1;
        //上次退勤时间
        public DateTime dtLastEndWorkTime = DateTime.MinValue;
        //联系电话
        public string strTelNumber = "";
        //实际职位
        public int nPostID = 0;
    }


    public class PrepareTMOrderLog
    {
        public DateTime LogTime = DateTime.MinValue;
        public string UserNumber  = "";
        public string UserName  = "";
        public string TMJiaoluGUID  = "";
        public string TMJiaoluName  = "";
        //1添加。2替换，3删除
        public int ChangeType  = 0;
        public string LogText  = "";

    }
}
