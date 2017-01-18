<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="RemoteLightControl.Index" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" ID="lblStatus"></asp:Label>
        </div>
    <div>
        <asp:Button runat="server" ID="btnOn" Text="On" OnClick="btnOn_Click" />
        <asp:Button runat="server" ID="btnOff" Text="Off" OnClick="btnOff_Click" />
    </div>
    </form>
</body>
</html>
