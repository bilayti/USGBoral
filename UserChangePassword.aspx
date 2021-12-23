<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserChangePassword.aspx.cs" Inherits="NewApp.UserChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="~/css/style1.css" />
    <script src="~/js/jquery.min.js"></script>
    <script type="text/javascript" src="../js/dfs.gis.js"></script>
    <script type="text/javascript">
        function CheckPasswordStrength(password) {
            var password_strength = document.getElementById("password_strength");

            //TextBox left blank.
            if (password.length == 0) {
                password_strength.innerHTML = "";
                return;
            }

            //Regular Expressions.
            var regex = new Array();
            regex.push("[A-Z]"); //Uppercase Alphabet.
            regex.push("[a-z]"); //Lowercase Alphabet.
            regex.push("[0-9]"); //Digit.
            regex.push("[$@$!%*#?&]"); //Special Character.

            var passed = 0;

            //Validate for each Regular Expression.
            for (var i = 0; i < regex.length; i++) {
                if (new RegExp(regex[i]).test(password)) {
                    passed++;
                }
            }

            //Validate for length of Password.
            if (passed > 3 && password.length > 8) {
                passed++;
            }

            //Display status.
            var color = "";
            var strength = "";
            switch (passed) {
                case 0:
                case 1:
                    strength = "Poor";
                    color = "red";
                    break;
                case 2:
                    strength = "Weak";
                    color = "darkorange";
                    break;
                case 3:
                    strength = "Good";
                    color = "orange";
                    break;
                case 4:
                    strength = "Strong";
                    color = "green";
                    break;
                case 5:
                    strength = "Very Strong";
                    color = "darkgreen";
                    break;
            }
            password_strength.innerHTML = strength;
            password_strength.style.color = color;
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />

            <div class="InputForm" style="width: 60%;">
                <h2>Change Password</h2>
                <p>
                    <asp:Label ID="Label2" runat="server" Text="Old Password:" AssociatedControlID="txtOldPassword"
                        EnableViewState="False" Width="40%"></asp:Label>
                    <asp:TextBox ID="txtOldPassword" AutoCompleteType="None" CssClass="form-control" autocomplete="off" runat="server"
                        ToolTip="Password is required!" TextMode="Password" Width="40%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword"
                        Display="Dynamic" EnableViewState="False" ErrorMessage="Required!" SetFocusOnError="True"
                        ValidationGroup="ChangePasswordGrp"></asp:RequiredFieldValidator>
                    <asp:HiddenField ID="hfSalt" runat="server" />
                </p>
                <p>
                    <asp:Label ID="Label3" runat="server" Text="New Password:" AssociatedControlID="txtNewPassword"
                        EnableViewState="False" Width="40%"></asp:Label>
                    <asp:TextBox ID="txtNewPassword" AutoCompleteType="None" autocomplete="off" runat="server"
                        ToolTip="Password must contain at least one upper case letter, one special character, one numeric number, and length should be at least 8 characters!"
                        TextMode="Password" Width="40%" onkeyup="CheckPasswordStrength(this.value)"></asp:TextBox>
                    <span id="password_strength"></span>
                    <%--<ajaxToolkit:PasswordStrength ID="txtPassword_PasswordStrength" runat="server" BehaviorID="txtPassword_PasswordStrength" TargetControlID="txtPassword" MinimumLowerCaseCharacters="1" MinimumNumericCharacters="2"   
MinimumUpperCaseCharacters="1" PreferredPasswordLength="8" />--%>
                    <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                        Display="Dynamic" EnableViewState="False" ErrorMessage="Required!" SetFocusOnError="True"
                        ValidationGroup="ChangePasswordGrp"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:Label ID="Label1" runat="server" Text="Confirm New Password:" AssociatedControlID="txtNewPassword"
                        EnableViewState="False" Width="40%"></asp:Label>
                    <asp:TextBox ID="txtNewPasswordConfirm" runat="server" ToolTip="Password must contain at least one upper case letter, one special character, one numeric number, and length should be at least 8 characters!"
                        TextMode="Password" Width="40%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNewPasswordConfirm" runat="server" ControlToValidate="txtNewPasswordConfirm"
                        Display="Dynamic" EnableViewState="False" ErrorMessage="New  password confirmation is required!"
                        SetFocusOnError="True" ValidationGroup="ChangePasswordGrp">*</asp:RequiredFieldValidator>
                </p>
                <asp:Label ID="lblErrMsg" runat="server" EnableViewState="False" Width="40%"></asp:Label>
                <br />
                <p>
                    <asp:Button ID="btnSubmit" runat="server" EnableViewState="False" Text="Submit" CssClass="button_example"
                        ValidationGroup="ChangePasswordGrp" OnClientClick="return ComputeHash();" OnClick="btnSubmit_Click" />&nbsp;
                <asp:Button ID="btnReset" runat="server" EnableViewState="False" CssClass="button_reset"
                    ValidationGroup="ChangePasswordGrp" Text="Reset" OnClientClick="ClearPwd();" OnClick="btnReset_Click" />&nbsp;
                <asp:Button ID="btnBackToHome" runat="server" EnableViewState="False" CssClass="button_reset"
                    Text="Back to home" OnClick="btnBackToHome_Click" />
                </p>
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
            if (plainConfirmPassword.length > 7) {
                document.getElementById('<%= txtNewPasswordConfirm.ClientID %>').value = confirmPasswordSaltedHash;
            }
            else {
                document.getElementById('<%= lblErrMsg.ClientID %>').innerHTML = 'Password must contain at least one upper case letter, one special character, one numeric number, and length should be at least 8 characters!<br />';
                return false;
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
        </div>
    </form>
</body>
</html>
