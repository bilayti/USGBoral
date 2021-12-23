<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="NewApp.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="~/css/style1.css" />
    <script src="~/js/jquery.min.js"></script>
        <script type="text/javascript" src="../js/dfs.gis.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="InputForm">
    <h2>
        Change Password</h2>
    <p>
        <asp:Label ID="Label2" runat="server" Text="Old Password:" AssociatedControlID="txtOldPassword"
            EnableViewState="False"></asp:Label>
        <asp:TextBox ID="txtOldPassword" AutoCompleteType="None" autocomplete="off" runat="server"
            ToolTip="Password is required!" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword"
            Display="Dynamic" EnableViewState="False" ErrorMessage="Required!" SetFocusOnError="True"
            ValidationGroup="ChangePasswordGrp"></asp:RequiredFieldValidator>
        <asp:HiddenField ID="hfSalt" runat="server" />
    </p>
    <p>
        <asp:Label ID="Label3" runat="server" Text="New Password:" AssociatedControlID="txtNewPassword"
            EnableViewState="False"></asp:Label>
        <asp:TextBox ID="txtNewPassword" AutoCompleteType="None" autocomplete="off" runat="server"
            ToolTip="Password must contain at least one upper case letter, one special character, one numeric number, and length should be at least 8 characters!"
            TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
            Display="Dynamic" EnableViewState="False" ErrorMessage="Required!" SetFocusOnError="True"
            ValidationGroup="ChangePasswordGrp"></asp:RequiredFieldValidator>
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="Confirm New Password:" AssociatedControlID="txtNewPassword"
            EnableViewState="False"></asp:Label>
        <asp:TextBox ID="txtNewPasswordConfirm" runat="server" ToolTip="Password must contain at least one upper case letter, one special character, one numeric number, and length should be at least 8 characters!"
            TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvNewPasswordConfirm" runat="server" ControlToValidate="txtNewPasswordConfirm"
            Display="Dynamic" EnableViewState="False" ErrorMessage="New  password confirmation is required!"
            SetFocusOnError="True" ValidationGroup="ChangePasswordGrp">*</asp:RequiredFieldValidator>
    </p>
    <asp:Label ID="lblErrMsg" runat="server" EnableViewState="False"></asp:Label>
    <asp:Button ID="btnSubmit" runat="server" EnableViewState="False" Text="Submit" CssClass="button_example"
        ValidationGroup="ChangePasswordGrp" OnClientClick="return ComputeHash();" />
    <asp:Button ID="btnReset" runat="server" EnableViewState="False" CssClass="button_reset"
        ValidationGroup="ChangePasswordGrp" Text="Reset" OnClientClick="ClearPwd();" />
    <asp:Literal ID="litInfo" runat="server" EnableViewState="False"></asp:Literal>
    <%-- </asp:Panel>--%>

    <script type="text/javascript">
        var hiddenFieldStr = '';

        function ComputeHash() {

            var shaObj;

            var plainOldPassword = document.getElementById('<%= txtOldPassword.ClientID %>').value;
            var plainPassword = document.getElementById('<%= txtNewPassword.ClientID %>').value;
            var plainConfirmPassword = document.getElementById('<%= txtNewPasswordConfirm.ClientID %>').value;
            var hfSalt = document.getElementById('<%= hfSalt.ClientID %>');


            //compute old password salted-hash
            shaObj = new jsSHA(plainOldPassword, "ASCII");
            var oldPasswordHash = shaObj.getHash("SHA-256", "HEX");
            shaObj = new jsSHA(hfSalt.value + oldPasswordHash, "ASCII");
            var oldPasswordSaltedHash = shaObj.getHash("SHA-256", "HEX");
            if (plainOldPassword.length > 0) {
                document.getElementById('<%= txtOldPassword.ClientID %>').value = oldPasswordSaltedHash;
            }

            //validate password before hashing/salting.
            if (ValidatePassword(plainPassword) == false) {
                //display error msg.
                document.getElementById('<%= lblErrMsg.ClientID %>').innerHTML = 'Password must contain at least one upper case letter, one special character, one numeric number, and length should be at least 8 characters!<br />';
                return false;
            }
            if (plainPassword != plainConfirmPassword) {
                //display error msg.
                document.getElementById('<%= lblErrMsg.ClientID %>').innerHTML = 'Password and Confirm Password must match!<br />';
                return false;
            }

            //compute new password hash      
            shaObj = new jsSHA(plainPassword, "ASCII");
            var passwordHash = shaObj.getHash("SHA-256", "HEX");
            if (plainPassword.length > 0) {
                document.getElementById('<%= txtNewPassword.ClientID %>').value = passwordHash;
            }

            //compute confirm password salted hash
            shaObj = new jsSHA(plainConfirmPassword, "ASCII");
            var confirmPasswordHash = shaObj.getHash("SHA-256", "HEX");
            shaObj = new jsSHA(hfSalt.value + confirmPasswordHash, "ASCII");
            var confirmPasswordSaltedHash = shaObj.getHash("SHA-256", "HEX");
            if (plainConfirmPassword.length > 0) {
                document.getElementById('<%= txtNewPasswordConfirm.ClientID %>').value = confirmPasswordSaltedHash;
            }

            hfSalt.value = '';

            //submit form
            return true;
        }

        function ClearPwd() {

            document.getElementById('<%= txtOldPassword.ClientID %>').value = '';
            document.getElementById('<%= hfSalt.ClientID %>').value = '';
            document.getElementById('<%= txtNewPassword.ClientID %>').value = '';
            document.getElementById('<%= txtNewPasswordConfirm.ClientID %>').value = '';

            //submit form
            return true;
        }
    </script>

    
</div>
    </form>
</body>
</html>
