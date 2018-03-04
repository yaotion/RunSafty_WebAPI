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
    ///FlowChartDraw 的摘要说明
    /// </summary>
    public class FlowChartDraw
    {
        public FlowChartDraw()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        //字体size
        public const int FONT_SIZE = 12;
        //字体类别
        public const string FONT_FAMILY = "宋体";
        //每个字体宽度,单位像素
        public  const int FONT_WIDTH = 12;
		//每个字体的高度,单位像素
        public  const int FONT_HEIGHT = 12;
		//字体距离背景余量,单位像素
        public  const int FONT_MARGIN = 4;
		//时间轴X坐标,单位像素
        public const int TIMELINE_X = 40;
		
		//时间轴高度,单位像素
        //public  const int TIMELINE_HEIGHT = 43;
		//一个时刻，在时间轴的宽度,（半个小时为一个基本单位）,单位像素
        public  const int MOMENT_WIDTH = 59;
		//项目上下的间隙高度,单位像素
        public  const int ITEM_GAP = 12;
		//未开工的颜色
        public  const string WEIKAIGONG_COLOR = "Red";
		//已开工的颜色
        public  const string YIKAIGONG_COLOR = "Blue";
		//已完工的颜色
        public  const string YIWANGONG_COLOR = "Green";
		//未验收的颜色
        public const string WEIYANSHOU_COLOR = "Red";
        //默认字体的颜色
        public const string DEFAULTFONTCOLOR = "White";
        //验收字体的颜色
        public  const string YANSHOUFONTCOLOR = "Black";

        //线条颜色
        public const string LINECOLOR = "#004EFF";
        //框颜色
        public const string RECTCOLOR = "White";

        //矩形角弧度
        public const int RECTRADIUS = 1;
        //质检及验收状态高度
        public const int ZHIJIANYANSHOU_HEIGHT = 34;

        /// <summary>
        /// 时间相减返回30分钟区间数量
        /// </summary>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public static int DateSubtract(string begintime, string endtime)
        {
            DateTime bt = DateTime.Parse(begintime);
            DateTime et = DateTime.Parse(endtime);
            TimeSpan ts = et.Subtract(bt);
            double nc = ts.TotalMinutes;
            return Convert.ToInt32(nc / 30);
        }

        /// <summary>
        /// 比较时间大小
        /// </summary>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public static int DateComparison(string begintime, string endtime)
        {
            DateTime bt = DateTime.Parse(begintime);
            DateTime et = DateTime.Parse(endtime);

            if (bt < et)
            {
                return 1;
            }
            else if (bt > et)
            {
                return -1;
            }
            else
            {
                return 0;

            }
        }

    }
}