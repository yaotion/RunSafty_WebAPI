using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using ThinkFreely.DBUtility;

    /// <summary>
    ///PageModule made by 赵文龙 from 2013.5.28
    /// </summary>
    public class PageModule : PageBase
    {
        public PageModule()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 生成DataGrid
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public string MakeDataGrid(DataTable dt,string pageid,string where)
        {
            where = where != "" ? "&k=" + where.Replace("'", "@") : "";
            string config = "\"config\":\"{";
            string property = "";
            property += ext_string(dt.Rows[0]["width"]) != "" ? ",'width':'" + dt.Rows[0]["width"].ToString() + "'" : ",'width':'100%'";
            property += ext_string(dt.Rows[0]["height"]) != "" ? ",'height':'" + dt.Rows[0]["height"].ToString() + "'" : ",'height':'551'";
            property += ext_string(dt.Rows[0]["frozenColumns"]) != "" ? ",'frozenColumns':'" + dt.Rows[0]["frozenColumns"].ToString() + "'" : "";
            property += ext_string(dt.Rows[0]["fitColumns"]) != "" ? ",'fitColumns':" + dt.Rows[0]["fitColumns"].ToString() : "";
            property += ext_string(dt.Rows[0]["striped"]) != "" ? ",'striped':" + dt.Rows[0]["striped"].ToString() : ",'striped':true";
            property += ext_string(dt.Rows[0]["nowrap"]) != "" ? ",'nowrap':" + dt.Rows[0]["nowrap"].ToString() : "";
            property += ext_string(dt.Rows[0]["url"]) != "" ? ",'url':'" + dt.Rows[0]["url"].ToString() + where + "'" : ",'url': encodeURI('/Page/SerachMaster/ashx/SerachMaster.ashx?pageid=" + pageid + where + "')";
            property += ext_string(dt.Rows[0]["loadMsg"]) != "" ? ",'loadMsg':'" + dt.Rows[0]["loadMsg"].ToString() + "'" : ",'loadMsg':'正在为您准备数据,请稍候...'";
            property += ext_string(dt.Rows[0]["pagination"]) != "" ? ",'pagination':" + dt.Rows[0]["pagination"].ToString() : ",'pagination':true";
            property += ext_string(dt.Rows[0]["rownumbers"]) != "" ? ",'rownumbers':" + dt.Rows[0]["rownumbers"].ToString() : ",'rownumbers':true";
            property += ext_string(dt.Rows[0]["singleSelect"]) != "" ? ",'singleSelect':" + dt.Rows[0]["singleSelect"].ToString() : ",'singleSelect':true";
            property += ext_string(dt.Rows[0]["pageNumber"]) != "" ? ",'pageNumber':'" + dt.Rows[0]["pageNumber"].ToString() + "'" : ",'pageNumber':1";
            property += ext_string(dt.Rows[0]["pageSize"]) != "" ? ",'pageSize':'" + dt.Rows[0]["pageSize"].ToString()+"'" : ",'pageSize':'50'";
            property += ext_string(dt.Rows[0]["sortName"]) != "" ? ",'sortName':'" + dt.Rows[0]["sortName"].ToString() +"'": "";
            property += ext_string(dt.Rows[0]["sortOrder"]) != "" ? ",'sortOrder':'" + dt.Rows[0]["sortOrder"].ToString()+"'" : "";
            property += ext_string(dt.Rows[0]["remoteSort"]) != "" ? ",'remoteSort':" + dt.Rows[0]["remoteSort"].ToString() : "";
            property += ext_string(dt.Rows[0]["showFooter"]) != "" ? ",'showFooter':" + dt.Rows[0]["showFooter"].ToString() : "";
            property += ext_string(dt.Rows[0]["rowStyler"]) != "" ? ",'rowStyler':function(index,row){ " + dt.Rows[0]["rowStyler"].ToString()+"}" : "";
            property += ext_string(dt.Rows[0]["toolbar"]) != "" ? ",'toolbar':" + dt.Rows[0]["toolbar"].ToString() : "";
            property += ext_string(dt.Rows[0]["onClickRow"]) != "" ? ",'onClickRow':function(index,row){" + dt.Rows[0]["onClickRow"].ToString() + "}" : "";
            property += ext_string(dt.Rows[0]["onDblClickRow"]) != "" ? ",'onDblClickRow':function(index,row){" + dt.Rows[0]["onDblClickRow"].ToString() + "}" : "";
            config += CutComma(property);
            config += "}\"";
            return config;
        }

        /// <summary>
        /// 生成搜索控件
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string MakeControl(DataRow[] dr,string pageid)
        {
            string control = "";
            for (int i = 0; i < dr.Length; i++)
            {
                switch (dr[i]["type"].ToString())
                {
                    case "0"://label
                        control += MakeControlLabel(cutDataRowID(dr[i]));
                        break;
                    case "1"://sjTextBox
                        control += MakeTimeControl(cutDataRowID(dr[i]));
                        break;
                    case "2"://textBox
                        control += MakeTextBoxControl(cutDataRowID(dr[i]));
                        break;
                    case "3"://sqloption
                        control += MakeOptionControl(cutDataRowID(dr[i]),pageid);
                        break;
                    case "4"://btn
                        control += MakeButtonControl(cutDataRowID(dr[i]));
                        break;
                    case "5"://strOption
                        control += MakeStrOptionControl(cutDataRowID(dr[i]));
                        break;
                    case "6"://zwlpy
                        control += MakeStrZwlPyControl(cutDataRowID(dr[i]));
                        break;
                }
            }
            return control;
        }

        /// <summary>
        /// 生成列
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string MakeColumn(DataTable dt)
        {
            string column = "";
            for (int j = 1; j <= 3; j++)//循环所有pagetype列
            {
                DataRow[] dr = dt.Select("pagetype=" + j, "sortid asc");
                if (dr.Length > 0)
                {
                    column += "[";
                    for (int i = 0; i < dr.Length; i++)
                    {
                        switch (dr[i]["type"].ToString())
                        {
                            case "0"://普通列
                                column += MakeNormalColumn(dr[i]);
                                break;
                            case "1"://格式化列
                                column += MakeFColumn(dr[i]);
                                break;
                            case "2"://行样式普通列
                                column += MakeRowStyleNormalColumn(dr[i]);
                                break;
                            case "3"://行样式格式化列
                                column += MakeRowStyleFColumn(dr[i]);
                                break;
                            case "4"://列样式普通列
                                column += MakeColumnStyleNormalColumn(dr[i]);
                                break;
                            case "5"://列样式格式化列
                                column += MakeColumnStyleFColumn(dr[i]);
                                break;
                        }
                        column += ",";
                    }
                    column = CutComma(column); //去掉首尾逗号
                    column += "],";
                }
                else
                {
                    break;
                }
            }
            column = CutComma(column); //去掉首尾逗号
            return column;
        }

        /// <summary>
        /// 将含多id字段截取 只取第一个做为控件id
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private DataRow cutDataRowID(DataRow dr)
        {
            if (dr["id"].ToString() != "")
            {
                string[] ids = dr["id"].ToString().Split(',');
                dr["id"] = ids[0];
            }
            return dr;
        }

        /// <summary>
        /// 根据配置信息生成列样式格式化列
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MakeColumnStyleFColumn(DataRow dr)
        {
            string property = "";
            property += ext_string(dr["rowspan"]) != "" ? ",rowspan:" + dr["rowspan"].ToString() : "";
            property += ext_string(dr["colspan"]) != "" ? ",colspan:" + dr["colspan"].ToString() : "";
            property += ext_string(dr["editor"]) != "" ? ",editor:" + dr["editor"].ToString() : "";
            return "{field:'" + dr["optionvalue"].ToString() + "'" + property + ",title:'" + dr["optiontext"].ToString() + "',width:" + dr["width"].ToString() + ",align:'" + dr["align"].ToString() + "',sortable:" + dr["sortable"].ToString() + ",checkbox:" + dr["checkbox"].ToString() + ",styler:function(val,row,index){" + dr["Stylefunctionname"].ToString() + "},formatter:function(val,row,index){" + dr["functionname"].ToString() + "}}";
        }

        /// <summary>
        /// 根据配置信息生成列样式普通列
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MakeColumnStyleNormalColumn(DataRow dr)
        {
            string property = "";
            property += ext_string(dr["rowspan"]) != "" ? ",rowspan:" + dr["rowspan"].ToString() : "";
            property += ext_string(dr["colspan"]) != "" ? ",colspan:" + dr["colspan"].ToString() : "";
            property += ext_string(dr["editor"]) != "" ? ",editor:" + dr["editor"].ToString() : "";
            return "{field:'" + dr["optionvalue"].ToString() + "'" + property + ",title:'" + dr["optiontext"].ToString() + "',width:" + dr["width"].ToString() + ",align:'" + dr["align"].ToString() + "',sortable:" + dr["sortable"].ToString() + ",checkbox:" + dr["checkbox"].ToString() + ",styler:function(val,row,index){" + dr["Stylefunctionname"].ToString() + "}}";
        }

        /// <summary>
        /// 根据配置信息生成行样式格式化列
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MakeRowStyleFColumn(DataRow dr)
        {
            string property = "";
            property += ext_string(dr["rowspan"]) != "" ? ",rowspan:" + dr["rowspan"].ToString() : "";
            property += ext_string(dr["colspan"]) != "" ? ",colspan:" + dr["colspan"].ToString() : "";
            property += ext_string(dr["editor"]) != "" ? ",editor:" + dr["editor"].ToString() : "";
            return "{field:'" + dr["optionvalue"].ToString() + "'" + property + ",title:'" + dr["optiontext"].ToString() + "',width:" + dr["width"].ToString() + ",align:'" + dr["align"].ToString() + "',sortable:" + dr["sortable"].ToString() + ",checkbox:" + dr["checkbox"].ToString() + ",formatter:function(val,row,index){" + dr["functionname"].ToString() + "},rowStyler:function(val,row,index){" + dr["Stylefunctionname"].ToString() + "}}";
        }

        /// <summary>
        /// 根据配置信息生成行样式普通列
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MakeRowStyleNormalColumn(DataRow dr)
        {
            string property = "";
            property += ext_string(dr["rowspan"]) != "" ? ",rowspan:" + dr["rowspan"].ToString() : "";
            property += ext_string(dr["colspan"]) != "" ? ",colspan:" + dr["colspan"].ToString() : "";
            property += ext_string(dr["editor"]) != "" ? ",editor:" + dr["editor"].ToString() : "";
            return "{field:'" + dr["optionvalue"].ToString() + "'" + property + ",title:'" + dr["optiontext"].ToString() + "',width:" + dr["width"].ToString() + ",align:'" + dr["align"].ToString() + "',sortable:" + dr["sortable"].ToString() + ",checkbox:" + dr["checkbox"].ToString() + "},rowStyler:function(val,row,index){" + dr["Stylefunctionname"].ToString() + "}";
        }

        /// <summary>
        /// 根据配置信息生成格式化列
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MakeFColumn(DataRow dr)
        {
            string property = "";
            property += ext_string(dr["rowspan"]) != "" ? ",rowspan:" + dr["rowspan"].ToString() : "";
            property += ext_string(dr["colspan"]) != "" ? ",colspan:" + dr["colspan"].ToString() : "";
            property += ext_string(dr["editor"]) != "" ? ",editor:" + dr["editor"].ToString() : "";
            return "{field:'" + dr["optionvalue"].ToString() + "'" + property + ",title:'" + dr["optiontext"].ToString() + "',width:" + dr["width"].ToString() + ",align:'" + dr["align"].ToString() + "',sortable:" + dr["sortable"].ToString() + ",checkbox:" + dr["checkbox"].ToString() + ",formatter:function(val,row,index){" + dr["functionname"].ToString() + "}}";
        }

        /// <summary>
        /// 根据配置信息生成普通列
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MakeNormalColumn(DataRow dr)
        {
            string property = "";
            property += ext_string(dr["hidden"]) != "" ? ",hidden:" + dr["hidden"].ToString() : "";
            property += ext_string(dr["rowspan"]) != "" ? ",rowspan:" + dr["rowspan"].ToString(): "";
            property += ext_string(dr["colspan"]) != "" ? ",colspan:" + dr["colspan"].ToString(): "";
            property += ext_string(dr["width"]) != "" ? ",width:" + dr["width"].ToString() : "";
            property += ext_string(dr["checkbox"]) != "" ? ",checkbox:" + dr["checkbox"].ToString() : "";
            property += ext_string(dr["sortable"]) != "" ? ",sortable:" + dr["sortable"].ToString() : "";
            property += ext_string(dr["optionvalue"]) != "" ? ",field:'" + dr["optionvalue"].ToString() + "'" : "";
            property += ext_string(dr["align"]) != "" ? ",align:'" + dr["align"].ToString() + "'" : "";
            property += ext_string(dr["editor"]) != "" ? ",editor:" + dr["editor"].ToString() : "";
            return "{title:'" + dr["optiontext"].ToString() + "'" + property + "}";
        }

        /// <summary>
        /// 根据sql生成zwlpy下拉框控件
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MakeStrZwlPyControl(DataRow dr)
        {
            string[] ids=ext_string(dr["additionalids"])!=""?ext_string(dr["additionalids"]).Split(','):new string[]{"zwlpytb","zwlpydiv"};
            string required = dr["required"].ToString() != "" ? "required=\"" + dr["required"].ToString() + "\"" : "";
            return "<script type='text/javascript'> $(document).ready(function() {setReady('" + ids[0] + "','" + ids[1] + "'," + ext_string(dr["dataurl"]) + ",'" + dr["id"].ToString() + "'); })</script><div class='" + dr["class"].ToString() 
                + "'>&nbsp;<input type='text' id='" + ids[0] + "'" + required + " style='width:" + dr["width"].ToString() 
                + "px;height:" + dr["height"].ToString()
                        + "px;'/></div><div class='zwlpydiv' id='" + ids[1] + "'></div><input id='" + dr["id"].ToString() + "' type='hidden'>";
        }

        /// <summary>
        /// 根据sql生成下拉框控件
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MakeStrOptionControl(DataRow dr)
        {
            string stronchange = "";
            stronchange = ext_string(dr["btnclick"]) != "" ? " onchange='" + dr["btnclick"].ToString() + "'" : "";
            string required = dr["required"].ToString() != "" ? "required=\"" + dr["required"].ToString() + "\"" : "";
            return "<div class='" + dr["class"].ToString() + "'>&nbsp;<select id='" + dr["id"].ToString() + "'" + required +stronchange+ " style='width:" + dr["width"].ToString() + "px;height:" + dr["height"].ToString()
                        + "px;'>" + MakeOptionFromStr(dr["optionvalue"].ToString(), dr["optiontext"].ToString()) + "</select></div>";
        }
        /// <summary>
        /// 根据配置信息生成按钮控件
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MakeButtonControl(DataRow dr)
        {
            return "<div class='" + dr["class"].ToString()
                       + "'>&nbsp;<input type='button' ID='" + dr["id"].ToString() + "' style=\"width:" + dr["width"].ToString() + "px; height:" + dr["height"].ToString()
                       + "px; cursor:pointer;border-radius:5px;border:1px solid #FF7E00; background-color:#FF7E00;color:white;\" value='" + dr["inputvalue"].ToString() + "' onclick=\"" + dr["btnclick"].ToString() + "\" /></div>";
        }

        /// <summary>
        /// 根据sql生成下拉框控件
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MakeOptionControl(DataRow dr,string pageid)
        {
            string stronchange = "";
            stronchange =ext_string(dr["btnclick"])!=""?" onchange='"+dr["btnclick"].ToString()+"'":"";
            string required = dr["required"].ToString() != "" ? "required=\"" + dr["required"].ToString() + "\"" : "";
            return "<div class='" + dr["class"].ToString() + "'>&nbsp;<select id='" + dr["id"].ToString() + "'" + required +stronchange+" style='width:" + dr["width"].ToString() + "px;height:" + dr["height"].ToString()
                        + "px;'><option value=''>全部</option>" + MakeDtFromSql(dr["optionsql"].ToString(), dr["optionvalue"].ToString(), dr["optiontext"].ToString(), pageid) + "</select></div>";
        }

        /// <summary>
        /// 根据配置信息生成textbox控件
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MakeTextBoxControl(DataRow dr)
        {
            string required = dr["required"].ToString() != "" ? "required=\"" + dr["required"].ToString() + "\"" : "";
            return "<div class='" + dr["class"].ToString()
                        + "'>&nbsp;<input id='" + dr["id"].ToString() + "'   style='width:" + dr["width"].ToString() + "px;height:" + dr["height"].ToString()
                        + "px;'  type='text' value='" + dr["inputvalue"].ToString() + "'" + required + " /></div>";
        }

        /// <summary>
        /// 根据配置信息生成时间控件
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MakeTimeControl(DataRow dr)
        {
            string strformat = ext_string(dr["format"]) == "" ? "yyyy-MM-dd" : dr["format"].ToString();
            string required = dr["required"].ToString() != "" ? "required=\"" + dr["required"].ToString() + "\"" : "";
            return "<div   class='" + dr["class"].ToString()
                        + "'>&nbsp;<input id='" + dr["id"].ToString() + "'  style='width:" + dr["width"].ToString() + "px;height:" + dr["height"].ToString()
                        + "px;'  type='text' value='" + dr["inputvalue"].ToString() + "'" + required + " onclick=\"WdatePicker({skin:'whyGreen',dateFmt:'" + strformat + "'})\" /></div>";
        }

        /// <summary>
        /// 生成控件label名称 如：<div class='l w65 tar'>入寓时间：</div>
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public string MakeControlLabel(DataRow dr)
        {
            //string style =dr["width"].ToString()!= ""?" style='width:"+dr["width"].ToString()+"px;height:21px;'":"";
            return "<div style='white-space:nowrap;' class='" + dr["class"].ToString() + "'>" + dr["label"].ToString() + "</div>";
        }

        /// <summary>
        /// 根据sql语句生成下拉框
        /// </summary>
        /// <param name="id">绑定value</param>
        /// <param name="name">绑定text</param>
        /// <returns></returns>
        public string MakeDtFromSql(string sql, string value, string text, string pageid)
        {
            DataTable dt;
            switch (sql)
            {
                //case "site":
                //    dt = ThinkFreely.TrainmanWorkStation.Site.Site.GetSites();
                //    break;
                default:
                    dt = SqlHelper.ExecuteDataset(searchmaster.GetSqlConnConfig(pageid), CommandType.Text, sql).Tables[0];
                    break;
            }
            return MakeOption(dt, value, text);
        }

        /// <summary>
        /// 根据给定value，text生成option下拉框 数据格式value：0,1,2 text:全部,正常,不正常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public string MakeOptionFromStr(string value, string text)
        {
            string[] strvalue = value.Split(',');
            string[] strtext = text.Split(',');
            string str = "";
            for (int i = 0; i < strvalue.Length; i++)
            {
                str += "<option value='" + strvalue[i].ToString() + "'>" + strtext[i].ToString() + "</option>";
            }
            return str;
        }

        /// <summary>
        ///  生成下拉框 返回如：<option value="10">外点</option>
        /// </summary>
        /// <param name="dt">datatable 数据源</param>
        /// <param name="id">绑定value</param>
        /// <param name="name">绑定text</param>
        /// <returns></returns>
        public string MakeOption(DataTable dt, string value, string text)
        {
            string str = "";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += "<option value='" + dt.Rows[i][value].ToString() + "'>" + dt.Rows[i][text].ToString() + "</option>";
                }
            }
            return str;
        }
    }