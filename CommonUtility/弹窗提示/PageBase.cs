using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace TF.CommonUtility
{
    /// <summary>
    ///PageBase 的摘要说明
    /// </summary>
    public class PageBase
    {
        public PageBase()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 页面输出错误提示
        /// </summary>
        /// <param name="p"></param>
        /// <param name="str"></param>
        public static void static_Message(Page p, string str)
        {
            p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>art.dialog('" + System.Web.HttpContext.Current.Server.HtmlEncode(str.Replace("\"", " ").Replace("'", " ")) + "','确定')</script>");
        }
        /// <summary>
        /// 弹出str提示 点击ok后关闭当前窗口 刷新父页面 reload方式
        /// </summary>
        /// <param name="p"></param>
        /// <param name="str"></param>
        public static void MARP(Page p, string str)
        {
            p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>art.dialog({content:'" + System.Web.HttpContext.Current.Server.HtmlEncode(str.Replace("\"", " ").Replace("'", " ")) + "',ok: function () {var win = art.dialog.open.origin;win.location.reload();}})</script>");
        }
        /// <summary>
        /// 弹出str提示 点击ok后关闭当前窗口 刷新父页面 href方式
        /// </summary>
        /// <param name="p"></param>
        /// <param name="str"></param>
        public static void MARP1(Page p, string str)
        {
            p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>art.dialog({content:'" + System.Web.HttpContext.Current.Server.HtmlEncode(str.Replace("\"", " ").Replace("'", " ")) + "',ok: function () {var win = art.dialog.open.origin;win.location.href=win.location.href;}})</script>");
        }
        /// <summary>
        /// 弹出str提示 点击ok后关闭当前窗口 刷新本页面 reload方式
        /// </summary>
        /// <param name="p"></param>
        /// <param name="str"></param>
        public static void MARM(Page p, string str)
        {
            p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>art.dialog({content:'" + System.Web.HttpContext.Current.Server.HtmlEncode(str.Replace("\"", " ").Replace("'", " ")) + "',ok: function () {window.location.reload();}})</script>");
        }
        /// <summary>
        /// 弹出str提示 点击ok后关闭当前窗口 刷新本页面 href方式
        /// </summary>
        /// <param name="p"></param>
        /// <param name="str"></param>
        public static void MARM1(Page p, string str)
        {
            p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>art.dialog({content:'" + System.Web.HttpContext.Current.Server.HtmlEncode(str) + "',ok: function () {window.location.href=window.location.href;}})</script>");
        }

        /// <summary>
        /// 弹出str提示，点击ok后关闭页面
        /// </summary>
        /// <param name="p"></param>
        /// <param name="str"></param>
        public static void MC(Page p, string str)
        {
            p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>var win = art.dialog.open.origin;art.dialog.close();win.art.dialog('" + System.Web.HttpContext.Current.Server.HtmlEncode(str.Replace("\"", " ").Replace("'", " ")) + "','确定')</script>");
        }

        public static void static_Message_ext(Page p, string js)
        {
            p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>" + js + "</script>");
        }
    }
}


