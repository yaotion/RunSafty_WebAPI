<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TextToSql_List.aspx.cs" Inherits="TestPage_TextToSql_TextToSql_List" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/_JScripts/jquery-easyui-1.3.2/themes/metro/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/_JScripts/jquery-easyui-1.3.2/themes/icon.css" />

    <script src="/_JScripts/jquery-easyui-1.3.2/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/_JScripts/jquery-easyui-1.3.2/jquery.easyui.min.js"></script>
 
    <script src="/_JScripts/json2.js" charset="gbk" type="text/javascript"></script>

    <style type="text/css">
        .selector span
        {
            min-width: 48px;
        }
        .datagrid-header .datagrid-cell
        {
            height: 40px;
            line-height: 40px;
            vertical-align: middle;
            background-color: #f7f7f7;
        }
    </style>
</head>
<body style="margin: 0px; overflow: hidden; font-family: 微软雅黑;">
    <form runat="server" id="form2">
    <div id="SerachDiv" style="width: 98%;">
    </div>
    <div style="margin: 10px;">
        <div id="dgState">
        </div>
    </div>
    <script type="text/javascript">
        $(window).resize(function () {
            SetHeightWidth();
        });
        function SetHeightWidth() {
            var width = $("#SerachDiv").width();
            var height = $(window).height() - $("#SerachDiv").height() - 40;
            $("#dgState").datagrid('resize', {
                width: width,
                height: height
            });
        }
        var listwidth = $("#SerachDiv").width();
        var Listheight = $(window).height() - $("#SerachDiv").height() - 40;


        $('#dgState').datagrid({
            url: encodeURI('ashx/GetMaxAvgList.ashx?r=' + Math.random()),
            striped: true,
            rownumbers: true,
            border: true,
            toolbar: "#toolbar",
            method: 'post',
            width: listwidth,
            height: Listheight,
            singleSelect: true,

            loadMsg: '数据加载中,请稍候..',
            columns: [[
                       { field: 'strName', title: '接口名称', width: '700', align: 'left', formatter: function (val, row) {
                           return "<a href='TextToSql_ListInfo.aspx?strName=" + row.strName + " ' target='_blank'>" + row.strName + "</a>";
                       }
                       },
                         { field: 'AvgTimes', title: '平均调用时间', width: '140', align: 'left' },
                         { field: 'strTimes', title: '调用总次数', width: '140', align: 'left' }
            ]]
        });

       
    </script>
    </form>
</body>
</html>

