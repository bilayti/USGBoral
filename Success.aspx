<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="NewApp.Success" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>
                <asp:Literal ID="litMsg" EnableViewState="false" runat="server"></asp:Literal>
                &nbsp;Click <asp:Button ID="btnbacktoChange" runat="server" Text="here" Font-Bold="true" Font-Size="Large" OnClick="btnbacktoChange_Click" /></a> to 
        continue (it will redirect you to home page)...</h3>
        </div>
    </form>
</body>
</html>
