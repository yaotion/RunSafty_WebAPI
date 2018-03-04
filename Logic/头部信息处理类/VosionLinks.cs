using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TF.RunSafty.Entry;
using System.Data;
using TF.RunSafty.DBUtils;

namespace TF.RunSafty.Logic
{
    public class VosionLinks
    {


        #region =========================================系统名称，版本号====================================
        public string VersionName = TF.RunSafty.DBUtils.SqlHelps.VersionName.ToString();
        public string VersionNumber = TF.RunSafty.DBUtils.SqlHelps.VersionNumber.ToString(); 
        #endregion

        TF.RunSafty.DBUtils.Nav DBNav = new TF.RunSafty.DBUtils.Nav(SqlHelps.SQLConnString);

        #region ======================================生成头部导航======================================
        /// <summary>
        /// 生成头部导航栏的html代码（根据权限判断）
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public string GetAllRootNodesForNav(string AllId, string OneTypeId)
        {
            DataTable dt = DBNav.GetListForNav("TypeParentID='01'");
            string str = "";
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {

                if (AllId == "")//如果是超级管理员，则不需要过滤，全部显示
                {
                    if (OneTypeId != "")//如果传递过来的参数（判断哪个选项卡被选中）是否是空
                    {
                        if (OneTypeId.Substring(0, 4) == dt.Rows[i]["TypeID"].ToString())//通过传递过来的参数，判断哪个选项卡被选中,添加背景色
                        {
                            str += "<div class='menus_h_div'><a href='" + dt.Rows[i]["NavUrl"] + "'>" + dt.Rows[i]["typeName"] + "</a></div>";

                        }
                        else//未被选中的不添加背景色
                        {
                            str += "<div class='menus_div'><a href='" + dt.Rows[i]["NavUrl"] + "'>" + dt.Rows[i]["typeName"] + "</a></div>";
                        }

                    }
                    else
                    {
                        str += "<div class='menus_div'><a href='" + dt.Rows[i]["NavUrl"] + "'>" + dt.Rows[i]["typeName"] + "</a></div>";
                    }

                }
                else
                {
                    if (AllId.Contains(dt.Rows[i]["TypeID"].ToString()))//在不是超级管理员的情况下,判断权限，进行过滤显示
                    {
                        if (OneTypeId != "")//如果传递过来的参数（判断哪个选项卡被选中）是否是空
                        {
                            if (OneTypeId.Substring(0, 4) == dt.Rows[i]["TypeID"].ToString())//通过传递过来的参数，判断哪个选项卡被选中
                            {
                                str += "<div class='menus_h_div'><a href='" + dt.Rows[i]["NavUrl"] + "'>" + dt.Rows[i]["typeName"] + "</a></div>";

                            }
                            else//未被选中的不添加背景色
                            {
                                str += "<div class='menus_div'><a href='" + dt.Rows[i]["NavUrl"] + "'>" + dt.Rows[i]["typeName"] + "</a></div>";
                            }

                        }
                        else//如果传递过来的参数（判断哪个选项卡被选中）是空的，则默认不做任何处理
                        {
                            str += "<div class='menus_div'><a href='" + dt.Rows[i]["NavUrl"] + "'>" + dt.Rows[i]["typeName"] + "</a></div>";
                        }


                    }

                }
            }

            return str;

        }
        #endregion


        #region ======================================生成页面列表======================================
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataForPageList()
        {
            return DBNav.GetListForNav("TypeParentID='01'");
        } 
        #endregion




    }

}
