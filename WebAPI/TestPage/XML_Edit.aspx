<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XML_Edit.aspx.cs" Inherits="TestPage_XML_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/_JScripts/jquery-easyui-1.3.2/jquery-1.8.0.min.js"></script>
    <script src="/_JScripts/artdialog/jquery.artDialog.js?skin=default" type="text/javascript"></script>
    <script src="/_JScripts/artdialog/plugins/iframeTools.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%=NodeText%>
        <div style="padding: 20px; width: 90%">
            <table style="line-height: 50px;">
                <tr>
                    <td align="right" style="width: 250px;">
                        APIName（识别参数）：
                    </td>
                    <td>
                        <asp:TextBox ID="TXT_APIName" Width="350px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 250px;">
                        APIBrief（描述）：
                    </td>
                    <td>
                        <asp:TextBox ID="TXT_APIBrief" Width="350px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 250px;">
                        TypeName（类名称）:
                    </td>
                    <td>
                        <asp:TextBox ID="TXT_TypeName" Width="350px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 250px;">
                        MethodName（方法名称）:
                    </td>
                    <td>
                        <asp:TextBox ID="TXT_MethodName" Width="350px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        <asp:Button ID="BtnSave" runat="server" Text="保存" OnClientClick="javascript:return beforeSave();"
                            OnClick="BtnSave_Click" Style="padding: 0; width: 60px; height: 25px;" onmouseover="this.className='rb1-13';"
                            onmouseout="this.className='rb2-13';" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="button" id="btn_fb" value="取消" title="取消" style="width: 60px; height: 25px;
                            margin-left: 5px;" onmouseover="this.className='rb1-13';" onmouseout="this.className='rb2-13';"
                            onclick="art.dialog.close();" />
                    </th>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
