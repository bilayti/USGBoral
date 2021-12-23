<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NewApp.Views.Login.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home :: USG Boral</title>
    <link href="~/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="~/css/style.css" rel="stylesheet" />
    <!-- Nav CSS -->
    <link href="~/css/custom.css" rel="stylesheet" />
    <!-- Graph CSS -->
    <link href="~/css/lines.css" rel="stylesheet" />
    <link href="~/css/font-awesome.css" rel="stylesheet" />
    <!-- webfonts -->
    <!-- jQuery -->
    <script src="js/jquery.min.js"></script>
    <link href='//fonts.googleapis.com/css?family=Roboto:400,100,300,500,700,900' rel='stylesheet' type='text/css' />
    <link href="~/Content/DefaultImage/DefaultImage.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">

        <div id="wrapper" style="background-color: #009639;">
            <!-- Navigation -->
            <nav class="top1 navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: -20px; margin-top: -20px; height: 71px; background-color: white; padding: 10px 5px 20px 20px; top: 0px; left: 0px;">
                <div class="navbar-header" style="margin-top: -8px">
                    <a class="navbar-brand" href="https://www.usgboral.com/en_in/" target="_blank">
                        <img src="icons/rsz_usg_boral_logo.png" />
                    </a>

                </div>

                <!-- /.navbar-header -->
                <div class="nav navbar-nav navbar-right" style="font-family: Verdana; font-size: large; font-weight: bold;">
                    <ul class="nav navbar-nav navbar-right">
                        <li class="dropdown">
                            <asp:Button ID="btnLoginPage" runat="server" Style="margin-top: 20px; border-radius: 5px;" Text="LOGIN" CssClass="btn btn-xs btn-primary" OnClick="btnLoginPage_Click" /></li>
                    </ul>
                </div>
                <!-- /.navbar-static-side -->
            </nav>
            <br />
            <br />
            <div class="DefaultBackground">
            </div>
        </div>
    </form>
</body>
</html>
