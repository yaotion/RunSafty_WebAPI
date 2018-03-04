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
using System.Xml;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Net;
using System.Runtime.Serialization.Json;








public class KeyValue
{
    public object Key;
    public object Value;
    public KeyValue(object key, object value)
    {
        Key = key;
        Value = value;
    }
}


/// <summary>
///PageBase 的摘要说明
/// </summary>
public class PageBase : System.Web.UI.Page
{
    public PageBase()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    #region 公用基本方法类

    /// <summary>
    /// 页面输出错误提示
    /// </summary>
    /// <param name="p"></param>
    /// <param name="str"></param>
    //public void Message(Page p, string str)
    //{
    //    p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), "", "<script>alert('" + System.Web.HttpContext.Current.Server.HtmlEncode(str) + "')</script>");
    //}  

    public void Message(Page p, string str)
    {
        p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>art.dialog('" + System.Web.HttpContext.Current.Server.HtmlEncode(str.Replace("\"", " ").Replace("'", " ")) + "','确定')</script>");
    }

    public static void static_Message(Page p, string str)
    {
        p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>art.dialog('" + System.Web.HttpContext.Current.Server.HtmlEncode(str.Replace("\"", " ").Replace("'", " ")) + "','确定')</script>");
    }
    /// <summary>
    /// 弹出str提示 点击ok后关闭当前窗口 刷新父页面 reload方式
    /// </summary>
    /// <param name="p"></param>
    /// <param name="str"></param>
    public void MARP(Page p, string str)
    {
        p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>art.dialog({content:'" + System.Web.HttpContext.Current.Server.HtmlEncode(str.Replace("\"", " ").Replace("'", " ")) + "',ok: function () {var win = art.dialog.open.origin;win.location.reload();}})</script>");
    }
    /// <summary>
    /// 弹出str提示 点击ok后关闭当前窗口 刷新父页面 href方式
    /// </summary>
    /// <param name="p"></param>
    /// <param name="str"></param>
    public void MARP1(Page p, string str)
    {
        p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>art.dialog({content:'" + System.Web.HttpContext.Current.Server.HtmlEncode(str.Replace("\"", " ").Replace("'", " ")) + "',ok: function () {var win = art.dialog.open.origin;win.location.href=win.location.href;}})</script>");
    }
    /// <summary>
    /// 弹出str提示 点击ok后关闭当前窗口 刷新本页面 reload方式
    /// </summary>
    /// <param name="p"></param>
    /// <param name="str"></param>
    public void MARM(Page p, string str)
    {
        p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>art.dialog({content:'" + System.Web.HttpContext.Current.Server.HtmlEncode(str.Replace("\"", " ").Replace("'", " ")) + "',ok: function () {window.location.reload();}})</script>");
    }
    /// <summary>
    /// 弹出str提示 点击ok后关闭当前窗口 刷新本页面 href方式
    /// </summary>
    /// <param name="p"></param>
    /// <param name="str"></param>
    public void MARM1(Page p, string str)
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

    public void Message_ext(Page p, string js)
    {
        p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>" + js + "</script>");
    }

    public static void static_Message_ext(Page p, string js)
    {
        p.ClientScript.RegisterStartupScript(p.ClientScript.GetType(), System.Guid.NewGuid().ToString(), "<script>" + js + "</script>");
    }

    /// <summary>
    /// 例如200分钟 返回03：20格式 也可是秒 小时等
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string dateturn(object str, int type)
    {
        decimal i = Convert.ToDecimal(str);
        if (i <= 0)
        {
            i = 0;
        }
        decimal m = 0;
        m = i >= 60 ? Math.Floor(i / 60) : 0;
        decimal s = i - m * 60;
        string t = "";
        switch (type)
        {
            case 1:
                t += m < 10 ? "0" + m.ToString() : m.ToString();
                t += ":";
                t += s < 10 ? "0" + s.ToString() : s.ToString();
                break;
            case 2:
                t += m + "小时" + s + "分";
                break;
        }
        return t;
    }
    /// <summary>
    /// 获取配置文件键值
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetConfig(string id)
    {
        try
        {
            return ConfigurationManager.AppSettings[id].ToString();
        }
        catch (Exception)
        {
            return "";
        }
    }

    /// <summary>
    /// 获取接口主机地址
    /// </summary>
    public static string WebAPIHost
    {
        get
        {
            return PageBase.GetConfig("WebAPI");
        }
    }

    /// <summary>
    /// 字符串剪切
    /// </summary>
    /// <param name="str"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public string stringCut(string str, int len)
    {
        try
        {
            if (str.Length < len)
            {
                return str;
            }
            else
            {
                return str.Substring(0, len) + "...";
            }
        }
        catch
        {
            return "";
        }
    }

    /// <summary>
    /// 数据类型转换（对象转整型 失败返回0 ）
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public int ext_int(object o)
    {
        if (o != null && o != "undefined")
        {
            try
            {
                return int.Parse(ext_string(o));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }
    /// <summary>
    /// 数据类型转换（对象转整型 失败返回0 ）
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public static int static_ext_int(object o)
    {
        if (o != null && o != "undefined")
        {
            try
            {
                return int.Parse(static_ext_string(o));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }
    /// <summary>
    /// 数据类型转换（对象转整型 失败返回0 ）
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public static double static_ext_double(object o)
    {
        if (o != null && o != "undefined")
        {
            try
            {
                return double.Parse(static_ext_string(o));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }
    public static decimal? static_ext_decimal(object o)
    {
        if (o != null && o != "undefined")
        {
            try
            {
                return decimal.Parse(static_ext_string(o));
            }
            catch (Exception)
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
    public decimal? ext_decimal(object o)
    {
        if (o != null && o != "undefined")
        {
            try
            {
                return decimal.Parse(ext_string(o));
            }
            catch (Exception)
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// 精度
    /// </summary>
    /// <param name="o"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public decimal ext_decimalRound(object o, int len)
    {
        return decimal.Round(Convert.ToDecimal(ext_decimal(o, 0)), 2);
    }
    /// <summary>
    /// 初始值
    /// </summary>
    /// <param name="o"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    public decimal? ext_decimal(object o, decimal r)
    {
        if (o != null && o != "undefined")
        {
            try
            {
                return decimal.Parse(ext_string(o));
            }
            catch (Exception)
            {
                return r;
            }
        }
        else
        {
            return r;
        }
    }
    public string ext_string(object o)
    {
        if (o != null)
        {
            try
            {
                if (o.ToString().Trim() == "undefined")
                {
                    return "";
                }
                else
                {
                    return o.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }
    public static string static_ext_string(object o)
    {
        if (o != null)
        {
            try
            {
                if (o.ToString().Trim() == "undefined")
                {
                    return "";
                }
                else
                {
                    return o.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }
    public DateTime? ext_date(object o)
    {
        if (o != null)
        {
            try
            {
                return Convert.ToDateTime(o);
            }
            catch (Exception)
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public static DateTime? static_ext_date(object o)
    {
        if (o != null)
        {
            try
            {
                return Convert.ToDateTime(o);
            }
            catch (Exception)
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// 时间格式yyyy-MM-dd HH:mm
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public string ext_date1(object o)
    {
        if (o != null)
        {
            try
            {
                return Convert.ToDateTime(o) > DateTime.Parse("1989-12-30") ? Convert.ToDateTime(o).ToString("yyyy-MM-dd HH:mm") : "";
            }
            catch (Exception)
            {
                return null;
            }
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// 时间格式yyyy-MM-dd
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public string ext_date2(object o)
    {
        if (o != null)
        {
            try
            {
                return Convert.ToDateTime(o) > DateTime.Parse("1989-12-30") ? Convert.ToDateTime(o).ToString("yyyy-MM-dd") : "";
            }
            catch (Exception)
            {
                return null;
            }
        }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// 自定义格式
    /// </summary>
    /// <param name="o"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static string static_ext_date1(object o, string format)
    {
        if (o != null)
        {
            try
            {
                return Convert.ToDateTime(o) > DateTime.Parse("1899-12-30") ? Convert.ToDateTime(o).ToString(format) : "";
            }
            catch (Exception)
            {
                return null;
            }
        }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// 自定义格式
    /// </summary>
    /// <param name="o"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public string ext_date1(object o, string format)
    {
        if (o != null)
        {
            try
            {
                return Convert.ToDateTime(o) > DateTime.Parse("1899-12-30") ? Convert.ToDateTime(o).ToString(format) : "";
            }
            catch (Exception)
            {
                return null;
            }
        }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// 日志（字符串）
    /// </summary>
    /// <param name="_str"></param>
    public static void log(string _str)
    {
        string directory = "~/log/" + DateTime.Now.ToString("yyyy-MM") + "/";
        string filepath = directory + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        string logFile = System.Web.HttpContext.Current.Server.MapPath(filepath);
        StreamWriter sw = null;
        try
        {
            if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(directory)))
            {
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(directory));
            }
            if (File.Exists(logFile))
            {
                sw = File.AppendText(logFile);
            }
            else
            {
                sw = File.CreateText(logFile);
            }
            sw.WriteLine(System.DateTime.Now.ToString() + "   IP:" + System.Web.HttpContext.Current.Request.UserHostAddress);
            sw.WriteLine("出错信息：" + _str);
            sw.WriteLine("---------------------------------------------------------------------");
            sw.Flush();
            sw.Close();
            sw.Dispose();
            sw = null;
        }
        catch (Exception)
        {
        }
    }

    /// <summary>
    /// 日志（Exception）
    /// </summary>
    /// <param name="_str"></param>
    public void logex(Exception erroy, string url)
    {
        string directory = "~/log/" + DateTime.Now.ToString("yyyy-MM") + "/";
        string filepath = directory + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        string logFile = System.Web.HttpContext.Current.Server.MapPath(filepath);
        StreamWriter sw = null;
        try
        {
            if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(directory)))
            {
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(directory));
            }
            if (File.Exists(logFile))
            {
                sw = File.AppendText(logFile);
            }
            else
            {
                sw = File.CreateText(logFile);
            }
            sw.WriteLine(System.DateTime.Now.ToString() + "   IP:" + System.Web.HttpContext.Current.Request.UserHostAddress);
            sw.WriteLine("出错页面是：" + url);
            sw.WriteLine("出错信息：" + erroy.Message);
            sw.WriteLine("Source:" + erroy.Source);
            sw.WriteLine("StackTrace：" + erroy.StackTrace);
            sw.WriteLine("---------------------------------------------------------------------");
            sw.Flush();
            sw.Close();
            sw.Dispose();
            sw = null;
        }
        catch (Exception)
        {
        }
    }
    public static string DateTimeStr(string str)
    {
        try
        {
            return Convert.ToDateTime(str).ToString("yyyy-MM-dd");
        }
        catch
        {
            return str;
        }
    }

    #region DropDownList处理
    /// <summary>
    /// DropDownList数据绑定
    /// dd DropDownList id
    /// id 绑定的DataTextField
    /// text 绑定的DataValueField
    /// dt 用于绑定的数据源
    /// isall 显示全部还是请选择
    /// </summary>
    /// <param name="dd"></param>
    /// <param name="id"></param>
    /// <param name="isall"></param>
    public static void DdlistBind(DropDownList dd, string id, string text, DataTable dt, int type)
    {
        dd.DataSource = dt;
        dd.DataTextField = text;
        dd.DataValueField = id;
        dd.DataBind();
        switch (type)
        {
            case 0:
                dd.Items.Insert(0, new ListItem("全部", ""));
                break;
            case 1:
                dd.Items.Insert(0, new ListItem("--请选择--", ""));
                break;
            case 2:
                break;
        }
    }
    #endregion
    /// <summary>
    /// 分页每页显示数据条数
    /// </summary>
    public const int PerPageCount = 20;

    #region 处理
    /// <summary>
    /// 绑定CheckBoxList
    /// </summary>
    /// <param name="cbl"></param>
    /// <param name="id"></param>
    /// <param name="text"></param>
    /// <param name="dt"></param>
    public void CheckBoxListBind(CheckBoxList cbl, string id, string text, DataTable dt)
    {
        cbl.DataSource = dt;
        cbl.DataTextField = text;
        cbl.DataValueField = id;
        cbl.DataBind();
    }
    #endregion
    #region ListBox处理
    /// <summary>
    /// 绑定listbox
    /// </summary>
    /// <param name="dd"></param>
    /// <param name="id"></param>
    /// <param name="text"></param>
    /// <param name="dt"></param>
    public void ListBoxBind(ListBox dd, string id, string text, DataTable dt)
    {
        dd.DataSource = dt;
        dd.DataTextField = text;
        dd.DataValueField = id;
        dd.DataBind();
    }


    /// <summary>
    /// b1-b2 去掉listbox b1中含有的b2项
    /// </summary>
    public void cutb1withb2(ListBox b1, ListBox b2)
    {
        int count = b2.Items.Count;
        for (int i = 0; i < count; i++)
        {
            ListItem item = b2.Items[i];
            if (b1.Items.Contains(item))
            {
                b1.Items.Remove(item);
            }
        }
    }

    /// <summary>
    /// 转移一个或多个
    /// </summary>
    /// <param name="b1"></param>
    /// <param name="b2"></param>
    public void move(ListBox b1, ListBox b2)
    {
        int count = b1.Items.Count;
        int index = 0;
        for (int i = 0; i < count; i++)
        {
            ListItem item = b1.Items[index];
            if (b1.Items[index].Selected == true)
            {

                b1.Items.Remove(item);
                b2.Items.Add(item);
                index--;
            }
            index++;
        }
    }

    /// <summary>
    /// 转移全部b1tob2
    /// </summary>
    /// <param name="b1"></param>
    /// <param name="b2"></param>
    public void moveall(ListBox b1, ListBox b2)
    {
        int count = b1.Items.Count;
        int index = 0;
        for (int i = 0; i < count; i++)
        {
            ListItem item = b1.Items[index];
            b1.Items.Remove(item);
            b2.Items.Add(item);
            index--;
            index++;
        }
    }

    /// <summary>
    /// 将listbox中项值写入数组并返回
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public static string[] GetlistBoxItemsCount(ListBox b)
    {
        int count = b.Items.Count;
        string[] str = new string[count];
        for (int i = 0; i < count; i++)
        {
            str[i] = b.Items[i].Value;
        }
        return str;
    }

    /// <summary>
    /// 将listbox选中项值写入字符串并返回
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public static string GetlistBoxValues(ListBox b)
    {
        int count = b.Items.Count;
        int[] selectindex = b.GetSelectedIndices();
        string[] str = new string[count];
        string selectvalue = "";
        for (int i = 0; i < count; i++)
        {
            str[i] = b.Items[i].Value;
        }
        for (int j = 0; j < selectindex.Length; j++)
        {
            selectvalue += str[selectindex[j]].ToString() + ",";
        }
        selectvalue = selectvalue.Length > 0 ? selectvalue.Substring(0, selectvalue.Length - 1) : selectvalue;
        return selectvalue;
    }
    #endregion

    #region 页面传值编码 解码
    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string urlencode(object s)
    {
        return System.Web.HttpContext.Current.Server.UrlEncode(s.ToString());
    }
    /// <summary>
    /// 解码
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string urldecode(string s)
    {
        return System.Web.HttpContext.Current.Server.UrlDecode(s.ToString());
    }
    #endregion

    /// <summary>
    /// 通知所有外点sql
    /// </summary>
    /// <param name="changeType"></param>
    /// <param name="strTableName"></param>
    /// <param name="strKeyValue"></param>
    /// <returns></returns>
    private string informSiteSql(string changeType, string strTableName, string strKeyValue)
    {
        return string.Format(@"insert into tabDataChangeRecord (nChangeType,strTableName,strKeyValue,nSiteID) 
                (select {0},'{1}', '{2}', nID from tabSite where nSiteState = 1)", changeType, strTableName, strKeyValue);
    }

    /// <summary>
    /// 将xml字符串转变为dataset
    /// </summary>
    /// <param name="xmlStr"></param>
    /// <returns></returns>
    public DataSet CXmlToDataSet(string xmlStr)
    {
        if (!string.IsNullOrEmpty(xmlStr))
        {
            StringReader StrStream = null;
            XmlTextReader Xmlrdr = null;
            try
            {
                DataSet ds = new DataSet();
                //读取字符串中的信息
                StrStream = new StringReader(xmlStr);
                //获取StrStream中的数据
                Xmlrdr = new XmlTextReader(StrStream);
                //ds获取Xmlrdr中的数据                
                ds.ReadXml(Xmlrdr);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //释放资源
                if (Xmlrdr != null)
                {
                    Xmlrdr.Close();
                    StrStream.Close();
                    StrStream.Dispose();
                }
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 将 XMLDocument对象转变为字符串
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    static public string XMLDocumentToString(ref XmlDocument doc)
    {
        MemoryStream stream = new MemoryStream();
        XmlTextWriter writer = new XmlTextWriter(stream, null);
        writer.Formatting = Formatting.Indented;
        doc.Save(writer);

        StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
        stream.Position = 0;
        string xmlString = sr.ReadToEnd();
        sr.Close();
        stream.Close();

        return xmlString;
    }

    public DataTable StrToDt(string xmlString, string key)
    {
        XDocument document = XDocument.Parse(xmlString);
        DataTable dt = new DataTable();
        DataRow dataRow = null;
        //dt.Columns.Add(new DataColumn("ID"));
        bool isFirst = true;
        foreach (var item in document.Root.Element(key).Elements())
        {
            dataRow = dt.NewRow();
            //dataRow["ID"] = item.Name.LocalName;
            foreach (XAttribute attr in item.Attributes())
            {
                if (isFirst)
                {
                    dt.Columns.Add(new DataColumn(attr.Name.LocalName));
                }
                dataRow[attr.Name.LocalName] = attr.Value;
            }
            isFirst = false;
            dt.Rows.Add(dataRow);
        }
        return dt;
    }
    /// <summary>
    /// 两时间相减返回秒数
    /// </summary>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    /// <returns></returns>
    public static string datediff(string str1, string str2)
    {
        string str = "";
        if (str1 != "" && str2 != "")
        {
            DateTime dt1 = Convert.ToDateTime(str1);
            DateTime dt2 = Convert.ToDateTime(str2);
            if (dt1 < dt2 || dt1 < Convert.ToDateTime("1990-09-01") || dt2 < Convert.ToDateTime("1990-09-01"))
            {
                str = "0";
            }
            else
            {
                TimeSpan ts = dt1 - dt2;
                str = ts.TotalSeconds.ToString();
            }
        }
        return str;
    }
    /// <summary>
    /// 时间差 
    /// </summary>
    /// <param name="time1"></param>
    /// <param name="time2"></param>
    /// <param name="type">1 1小时1分钟 2 02:02</param>
    /// <returns></returns>
    public static string DateDiff(object time1, object time2, int type)
    {
        string date = "";
        if (static_ext_string(time1) != "" && static_ext_string(time2) != "")
        {
            DateTime t1 = Convert.ToDateTime(time1);
            DateTime t2 = Convert.ToDateTime(time2);
            TimeSpan ts1 = t2 - t1;
            int tsMin = ts1.Minutes;
            int tsHou = ts1.Hours;
            switch (type)
            {
                case 0:
                    date = tsHou.ToString() + "小时" + tsMin.ToString() + "分钟";
                    break;
                case 1:
                    string s1 = tsHou < 10 ? "0" + tsHou.ToString() : tsHou.ToString();
                    string s2 = tsMin < 10 ? "0" + tsMin.ToString() : tsMin.ToString();
                    date = s1 + ":" + s2;
                    break;
            }
        }
        return date;
    }
    /// <summary>
    /// 时间差 
    /// </summary>
    /// <param name="time1"></param>
    /// <param name="time2"></param>
    /// <param name="type">1 1小时1分钟 2 02:02</param>
    /// <returns></returns>
    public static string static_DateDiff(object time1, object time2, int type)
    {
        string date = "";
        if (PageBase.static_ext_string(time1) != "" && PageBase.static_ext_string(time2) != "")
        {
            DateTime t1 = Convert.ToDateTime(time1);
            DateTime t2 = Convert.ToDateTime(time2);
            TimeSpan ts1 = t2 - t1;
            int tsMin = ts1.Minutes;
            int tsHou = ts1.Hours;
            switch (type)
            {
                case 0:
                    date += tsHou != 0 ? tsHou.ToString() + "时" : "";
                    date += tsMin.ToString() + "分";
                    break;
                case 1:
                    string s1 = tsHou < 10 ? "0" + tsHou.ToString() : tsHou.ToString();
                    string s2 = tsMin < 10 ? "0" + tsMin.ToString() : tsMin.ToString();
                    date = s1 + ":" + s2;
                    break;
            }
        }
        return date;
    }

    /// <summary>
    /// 时间差返回分钟数
    /// </summary>
    /// <param name="time1"></param>
    /// <param name="time2"></param>
    /// <param name="type">0 相差天数 1 小时 2 分钟</param>
    /// <returns></returns>
    public static decimal static_DecimalDateDiff(object time1, object time2, int type)
    {
        DateTime t1 = Convert.ToDateTime(time1);
        DateTime t2 = Convert.ToDateTime(time2);
        TimeSpan ts1 = t2 - t1;
        decimal tsDay = decimal.Round(Convert.ToDecimal(ts1.TotalDays), 1);
        decimal tsMin = decimal.Round(Convert.ToDecimal(ts1.TotalMinutes), 1);
        decimal tsHou = decimal.Round(Convert.ToDecimal(ts1.TotalSeconds), 1);
        switch (type)
        {
            case 0:
                return tsDay;
                break;
            case 1:
                return tsHou;
                break;
            case 2:
                return tsMin;
                break;
        }
        return 0;
    }

    /// <summary>
    /// 格式化小时分钟 2014-01-15 15:50:30 -> 15点30  
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string static_FormatHourMinute(string time)
    {
        try
        {
            return Convert.ToDateTime(time).ToShortTimeString().ToString().Replace(":", "点");
        }
        catch (Exception ex)
        {
            PageBase.log(ex.Message);
        }
        return "";
    }
    #endregion

    #region 连接Excel  读取Excel数据   并返回DataSet数据集合
    /// <summary>
    /// 连接Excel  读取Excel数据   并返回DataSet数据集合
    /// </summary>
    /// <param name="filepath">Excel服务器路径</param>
    /// <param name="tableName">Excel表名称</param>
    /// <returns></returns>
    public static System.Data.DataSet ExcelSqlConnection(string filepath, string tableName)
    {
        string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
        OleDbConnection ExcelConn = new OleDbConnection(strCon);
        try
        {

            ExcelConn.Open();

            DataTable dtSheetName = ExcelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
            //包含excel中表名的字符串数组
            string[] strTableNames = new string[dtSheetName.Rows.Count];
            for (int k = 0; k < dtSheetName.Rows.Count; k++)
            {
                strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
            }
            string strCom = string.Format("SELECT * FROM [" + strTableNames[0] + "]");
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, ExcelConn);
            DataSet ds = new DataSet();
            myCommand.Fill(ds, strTableNames[0]);
            ExcelConn.Close();
            return ds;
        }
        catch
        {
            ExcelConn.Close();
            return null;
        }
    }

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="ds"></param>
    ///// <returns></returns>
    //public DataSet subcategory(DataSet ds,int id ,string name)
    //{
    //    DataSet ds1 = new DataSet();
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add(id, typeof(int));
    //    dt.Columns.Add(name, typeof(string));
    //    DataRow[] drTemp = ds.Tables[0].Select("parentid=0");
    //    for (int i = 0; i < drTemp.Length; i++)
    //    {
    //        string temp = drTemp[i][id].ToString();
    //        string name = drTemp[i][name].ToString();
    //        DataRow dr = dt.NewRow();
    //        dr[id] = temp;
    //        dr[name] = name;
    //        dt.Rows.Add(dr);
    //        drTemp[i].Delete();
    //        ForDelType(ds.Tables[0], "nparentid =" + temp, dt,id,name);
    //    }
    //    ds1.Tables.Add(dt);
    //    return ds1;
    //}
    //private static void ForDelType(DataTable AllData, string SelectWhere, DataTable dt, int id, string name)
    //{
    //    DataRow[] drTemp = AllData.Select(SelectWhere);
    //    for (int i = drTemp.Length - 1; i >= 0; i--)
    //    {
    //        string temp = drTemp[i][id].ToString();
    //        string name = drTemp[i][name].ToString();
    //        DataRow dr = dt.NewRow();
    //        dr[id] = temp;
    //        dr[name] = "->" + name;
    //        dt.Rows.Add(dr);
    //        drTemp[i].Delete();
    //        ForDelType(AllData, "parentId =" + temp, dt, id, name);
    //    }
    //}

    /// <summary>
    /// 根据测酒时间获取乘务员测酒照片
    /// </summary>
    /// <param name="dtime"></param>
    /// <returns></returns>
    public string[] getcatalog(object dtime, string catalogue)
    {
        DateTime dt = Convert.ToDateTime(dtime);
        string[] ca = new string[3];
        ca[0] = dt.ToString("yyyyMMdd");
        ca[1] = catalogue;
        return ca;
    }


    /// <summary>
    /// 根据url删除ftp文件
    /// </summary>
    public static bool DeleteFileOnServer(Uri serverUri, string ftpUser, string ftpPassWord)
    {
        try
        {
            if (serverUri.Scheme != Uri.UriSchemeFtp)
            {
                return false;
            }
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            //Console.WriteLine("Delete status: {0}", response.StatusDescription);
            response.Close();
        }
        catch
        {
            return false;
        }
        return true;
    }

    #endregion
    #region
    /// <summary>
    /// 序列化方法（带分页）
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public string SerializeP(DataTable dt, int count)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        foreach (DataRow dr in dt.Rows)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.DataType == System.Type.GetType("System.DateTime"))
                {
                    result.Add(dc.ColumnName, dr[dc].ToString() == "" ? "" : Convert.ToDateTime(dr[dc].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
            }
            list.Add(result);
        }
        string strReturn = "";
        if (count == 0)
        {
            strReturn = "{\"total\":0,\"rows\":[]}";
        }
        else
        {
            strReturn = ConventToJson(list, count);
        }
        return strReturn;
    }
    /// <summary>
    /// 序列化方法（带分页）
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string static_SerializeP(DataTable dt, int count)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        foreach (DataRow dr in dt.Rows)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.DataType == System.Type.GetType("System.DateTime"))
                {
                    result.Add(dc.ColumnName, dr[dc].ToString() == "" ? "" : Convert.ToDateTime(dr[dc].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
            }
            list.Add(result);
        }
        string strReturn = "";
        if (count == 0)
        {
            strReturn = "{\"total\":0,\"rows\":[]}";
        }
        else
        {
            strReturn = static_ConventToJson(list, count);
        }
        return strReturn;
    }
    /// <summary>
    /// 转换为JSON对象
    /// </summary>
    /// <returns></returns>
    public static string static_ConventToJson<T>(List<T> list, int count)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string strJson = serializer.Serialize(list);
        strJson = strJson.Substring(1);
        strJson = strJson.Insert(0, "{\"total\":" + count + ",\"rows\":[");
        strJson += "}";

        return strJson;
    }

    /// <summary>
    /// 转换为JSON对象
    /// </summary>
    /// <returns></returns>
    public string ConventToJson<T>(List<T> list, int count)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string strJson = serializer.Serialize(list);
        strJson = strJson.Substring(1);
        strJson = strJson.Insert(0, "{\"total\":" + count + ",\"rows\":[");
        strJson += "}";

        return strJson;
    }

    /// <summary>
    /// 序列化方法不需要分页
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string Serialize(DataTable dt)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        foreach (DataRow dr in dt.Rows)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.DataType == System.Type.GetType("System.DateTime"))
                {
                    result.Add(dc.ColumnName, dr[dc].ToString() == "" ? "" : Convert.ToDateTime(dr[dc].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }

            }
            list.Add(result);
        }
        return serializer.Serialize(list); ;
    }

    #endregion

    /// <summary>
    /// 去掉字符串首尾逗号
    /// </summary>
    /// <param name="StrIndex"></param>
    /// <returns></returns>
    public static string CutComma(string StrIndex)
    {
        if (StrIndex.Length > 0)
        {
            StrIndex = StrIndex.Substring(0, 1) == "," ? StrIndex.Substring(1) : StrIndex;
        }
        if (StrIndex.Length > 0)
        {
            StrIndex = StrIndex.Substring(StrIndex.Length - 1) == "," ? StrIndex.Substring(0, StrIndex.Length - 1) : StrIndex;
        }
        return StrIndex;
    }
    /// <summary>
    /// 去掉字符串首尾fh
    /// </summary>
    /// <param name="StrIndex"></param>
    /// <returns></returns>
    public static string CutComma(string StrIndex, string fh, int index)
    {
        if (StrIndex.Length > 0)
        {
            StrIndex = StrIndex.Substring(0, index) == fh ? StrIndex.Substring(1) : StrIndex;
            StrIndex = StrIndex.Substring(StrIndex.Length - index) == fh ? StrIndex.Substring(0, StrIndex.Length - index) : StrIndex;
        }
        return StrIndex;
    }
    /// <summary>
    /// 去掉上传文件中 文件名包含#
    /// </summary>
    /// <param name="Url"></param>
    /// <returns></returns>
    public static Uri ProcessSpecialCharacters(string Url)
    {
        Uri uriTarget = new Uri(Url);
        if (!Url.Contains("#"))
        {
            return uriTarget;
        }

        UriBuilder msPage = new UriBuilder();
        msPage.Host = uriTarget.Host;
        msPage.Scheme = uriTarget.Scheme;
        msPage.Port = uriTarget.Port;
        msPage.Path = uriTarget.LocalPath + uriTarget.Fragment;
        msPage.Fragment = uriTarget.Fragment;
        Uri uri = msPage.Uri;

        return uri;
    }
    //例如200分钟 返回03：20格式 也可是秒 小时等
    public static string TimeFormat(string str)
    {
        double i = Convert.ToDouble(PageBase.static_ext_int(str));
        if (i < 0 || str == "")
        {
            return "";
        }

        double h = 0;
        h = i >= 3600 ? Math.Floor(i / 3600) : 0;
        double m = (i - h * 3600) >= 60 ? Math.Floor((i - h * 3600) / 60) : 0;
        string t = "";
        t += h < 10 ? "0" + h.ToString() : h.ToString();
        t += ":";
        m = m >= 60 ? Math.Floor(m / 60) : m;
        t += m < 10 ? "0" + m.ToString() : m.ToString();
        double s = i % 60;
        t += ":";
        t += s < 10 ? "0" + s.ToString() : s.ToString();
        return t;
    }
    /// <summary>
    /// json反序列化为list对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public T JsonDeserialize<T>(string jsonString)
    {
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
        T obj = (T)ser.ReadObject(ms);
        return obj;
    }

    /// <summary>Excel导出</summary>
    /// <param name="dt">要导入的数据(存储于DataTable中的)</param>
    /// <param name="arrayList">需要导出的列名集合</param>
    public static bool ExportToExcel(Page page, DataTable dt, System.Collections.ArrayList arrayList)
    {
        StringWriter sw = new StringWriter();
        string columnName = "";

        for (int i = 0; i < arrayList.Count; i++)
        {
            if (columnName.Length == 0)
            {
                columnName = columnName + ((KeyValue)arrayList[i]).Value.ToString();
            }
            else
            {
                columnName = columnName + '\t' + ((KeyValue)arrayList[i]).Value.ToString();
            }
        }

        sw.WriteLine(columnName);

        foreach (DataRow dr in dt.Rows)
        {
            string rowString = "";
            for (int i = 0; i < arrayList.Count; i++)
            {
                if (rowString.Length == 0)
                {
                    string str = dr[((KeyValue)arrayList[i]).Key.ToString()].ToString().Trim().Length == 0 ? "  " : dr[((KeyValue)arrayList[i]).Key.ToString()].ToString();
                    rowString = rowString + str;
                }
                else
                {
                    string str = dr[((KeyValue)arrayList[i]).Key.ToString()].ToString().Trim().Length == 0 ? "  " : dr[((KeyValue)arrayList[i]).Key.ToString()].ToString();
                    rowString = rowString + '\t' + str;
                }
            }



            sw.WriteLine(rowString);
        }
        sw.Close();
        page.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(dt.TableName, System.Text.Encoding.UTF8) + ".xls\"");
        page.Response.ContentType = "application/ms-excel";
        page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        page.Response.Write(sw);
        page.Response.End();
        return true;
    }
    /// <summary>
    /// 如不为空字符串加括号返回
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string jkh(string str)
    {
        if (str != "")
        {
            return "(" + str + ")";
        }
        return str;
    }
    /// <summary>
    /// 字符串满len后自动换行
    /// </summary>
    /// <param name="str"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public static string TextAutoNewLine(string str, int len)
    {
        string s = "";
        if (str.Length > len)
        {
            for (int i = 1; i <= str.Length; i++)
            {
                int r = i % len;
                int last = (str.Length / len) * len;
                if (i != 0 && i <= last)
                {

                    if (r == 0)
                    {
                        s += str.Substring(i - len, len) + "\\n";
                    }

                }
                else if (i > last)
                {
                    s += str.Substring(i - 1);
                    break;
                }

            }
        }
        else
        {
            s = str;
        }
        s = PageBase.CutComma(s, "\\n", 2);
        return s;
    }

    /// <summar>o2-o1</summar>
    /// <param name="o1"></param>
    /// <param name="o2"></param>
    /// <returns></returns>
    public static TimeSpan diffTimeReturnTimeSpan(object o1, object o2)
    {
        TimeSpan ts = Convert.ToDateTime(o2) - Convert.ToDateTime(o1);
        return ts;
    }
    /// <summary>
    /// 字符前加逗号
    /// </summary>
    /// <param name="ccstr">储存str</param>
    /// <param name="clstr">待处理str</param>
    /// <returns></returns>
    public static string static_AddCumma(string ccstr, string clstr)
    {
        if (ccstr == "" && clstr != "")
        {
            ccstr += clstr;
        }
        else
        {
            if (ccstr != "" && clstr != "")
            {
                ccstr += ",";
                ccstr += clstr;
            }
        }
        return ccstr;
    }

    //看文件或文件夹是否存在
    public static bool FileExists(string strPath)
    {
        if (!File.Exists(strPath))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 过滤JS脚本，防止JS脚本注入
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ScriptFilter(string input)
    {
        input = HttpUtility.HtmlEncode(input);
        System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        input = regex1.Replace(input, "");
        
        return input;
    }


}



