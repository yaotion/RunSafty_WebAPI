<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TextToSql_Add.aspx.cs" Inherits="TestPage_TextToSql_TextToSql_Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="/_JScripts/jquery-easyui-1.3.2/jquery-1.8.0.min.js" type="text/javascript"></script>
    <title></title>

    <script type="text/javascript">
        //获取非运转的数据信息
        function getcount() {
            $.ajax({
                type: "post",
                async: false,
                cache: false,
                url: "ashx/GetCount.ashx",
                success: function (data) {
                    if (data) {
                        $("#counts").html(data);
                    }
                    else {
                        alert();
                    }
                }
            });
            return sum;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click" OnClientClick="ReLoad()" />
        <input  type="button" onclick="getcount()" value="点击实时查看数据数量" />
        <span id="counts"></span>
    </div>
    </form>
</body>
</html>
