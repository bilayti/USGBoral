<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeSuccess.aspx.cs" Inherits="NewApp.ChangeSuccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="~/css/style1.css" />
    <script src="~/js/jquery.min.js"></script>
    <script type="text/javascript" src="../js/dfs.gis.js"></script>
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
