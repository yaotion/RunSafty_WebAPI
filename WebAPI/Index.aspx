
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="/_JScripts/jquery-easyui-1.3.2/jquery-1.8.0.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#AccessJieKou").html("<h2>&nbsp;&nbsp;1，接口连接成功</h2>");
            $("#NoAccess").html();
            $("#Access").html();
            window.setTimeout(function () {
                var ajaxTimeoutTest = $.ajax({
                    type: "get",
                    timeout: 2000,
                    cache: false,
                    url: "/TestPage/IsAccess.ashx",
                    dataType: "json",
                    success: function (d) {
                        if (d.nResult == 0) {
                            $("#Access").append("<h2>&nbsp;&nbsp;2，数据库连接成功</h2>")
                        } else {
                            $("#NoAccess").append("<h2>&nbsp;&nbsp;2，程序发生异常,请检查数据库连接设置</h2>")
                        }
                    },
                    error: function (d) {
                        $("#NoAccess").append("<h2>&nbsp;&nbsp;2，程序发生异常,请检查网络或者数据库连接设置</h2>")
                    },
                    complete: function (XMLHttpRequest, statusw) {
                        if (statusw == 'timeout') {
                            ajaxTimeoutTest.abort();
                            $("#NoAccess").append("<h2>&nbsp;&nbsp;2，连接超时，请检查网络或者数据库连接设置</h2>");
                        }
                    }
                });
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: left; width: 100%">
        <span style="color: Green; font-size: 18px;" id="AccessJieKou"></span><span style="color: Red;
            font-size: 18px;" id="NoAccess"></span><span style="color: Green; font-size: 18px;"
                id="Access"></span>
        <h2>
            <a style="color: Blue; text-decoration: none" href="TestPage/main.htm">&nbsp;&nbsp;3，点击进入api列表页面</a></h2>
    </div>
    </form>
</body>
</html>
  


          
