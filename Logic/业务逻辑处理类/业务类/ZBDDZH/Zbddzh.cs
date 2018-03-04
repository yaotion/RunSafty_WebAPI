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
using TF.RunSafty.Logic;
using TF.RunSafty.Logic;

namespace TF.RunSafty.Logic
{
    /// <summary>
    ///Zbddzh 的摘要说明
    /// </summary>
    public class Zbddzh
    {
        public Zbddzh()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        //和谐电图片宽
        public const double hxd2bw = 42;
        //和谐电图片高
        public const double hxd2bh = 24;

        //箭头宽
        public const double arroww = 22;
        //箭头高
        public const double arrowh = 31;
        //提示框背景宽
        public const double tsbgw = 146;
        //提示框背景高
        public const double tsbgh = 101;

        //机车偏移x
        public const int pyX = 0;
        //机车偏移y
        public const int pyY = 10;
        /// <summary>
        /// 设置左侧入场的机车xy坐标
        /// </summary>
        /// <param name="jclist"></param>
        public static List<lsDatJT6JiChe> LeftFactory(List<lsDatJT6JiChe> jclist,int apanageid)
        {
            //待返回机车集合 此举为过滤数据库中无轨道数据的机车
            List<lsDatJT6JiChe> returnLeftlist = new List<lsDatJT6JiChe>();
            //获取股道xy坐标配置
            List<TrackRect> trackrectlist = TrackRect.GetAllTrackRect(apanageid);
            foreach (TrackRect trackrect in trackrectlist)
            {
                double mapX = trackrect.nRectLeft;
                double mapY = trackrect.nRectTop;
                foreach (lsDatJT6JiChe jc in jclist)
                {
                    if (jc.TrackId.ToString() == trackrect.nTrackID.ToString())
                    {
                        jc.nMapX = mapX;
                        jc.nMapY = mapY;
                        mapX = mapX + hxd2bw + 1;
                        returnLeftlist.Add(jc);
                    }
                }
            }
            return returnLeftlist;
        }

        /// <summary>
        /// 设置右侧入场的机车xy坐标
        /// </summary>
        /// <param name="jclist"></param>
        public static List<lsDatJT6JiChe> RightFactory(List<lsDatJT6JiChe> jclist, int apanageid)
        {
            //待返回机车集合 此举为过滤数据库中无轨道数据的机车
            List<lsDatJT6JiChe> returnRightlist = new List<lsDatJT6JiChe>();
            //获取股道xy坐标配置
            List<TrackRect> trackrectlist = TrackRect.GetAllTrackRect(apanageid);
            foreach (TrackRect trackrect in trackrectlist)
            {
                double mapX = trackrect.nRectRight;
                double mapY = trackrect.nRectTop;
                foreach (lsDatJT6JiChe jc in jclist)
                {
                    if (jc.TrackId.ToString() == trackrect.nTrackID.ToString())
                    {
                        mapX = mapX - hxd2bw - 1;
                        jc.nMapX = mapX;
                        jc.nMapY = mapY;
                        returnRightlist.Add(jc);
                    }
                }
            }
            return returnRightlist;
        }    
    }
}