<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="NewApp.ManageUser" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage User</title>
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="~/css/style1.css" />
    <script src="../js/jquery.min.js"></script>
    <script type="text/javascript" src="../js/dfs.gis.js"></script>
    <script src="File/jquery-1.10.2.js" type="text/javascript"></script>
    <link href="~/File/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="File/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            SearchText();
        });
        function SearchText() {
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "HomePage/GetAutoStudentData",
                        data: "{'username':'" + document.getElementById('txtAdmissionId').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            console.log(data);
                            if (data.length > 0) {

                                response($.map(data, function (item) {
                                    return {
                                        label: item.split('/')[0],
                                        val1: item.split('/')[1],
                                        val2: item.split('/')[2],
                                        val3: item.split('/')[3]
                                    }
                                }));
                            }
                            else {
                                response([{ label: 'No Records Found', val: -1 }]);
                            }
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                select: function (event, ui) {
                    if (ui.item.val == -1) {
                        return false;
                    }
                    document.getElementById('<%=lblStudentName.ClientID %>').innerHTML = ui.item.val1;
                    document.getElementById('<%=hdnStudentName.ClientID %>').value = ui.item.val1;
                    document.getElementById('<%=hfCustomerId.ClientID %>').value = ui.item.val2;
                    document.getElementById('<%=lblSAPID.ClientID %>').innerHTML = ui.item.val3;
                }
            });
        }
    </script>
    <script type="text/javascript">
        var hiddenFieldStr = '';

        function ComputeHash() {

            //validate password for valid chars.
            var NewPassword = document.getElementById('<%= txtNewPassword.ClientID %>').value;
            var NewPasswordConfirm = document.getElementById('<%= txtNewPasswordConfirm.ClientID %>').value;
            var lblErrMsg2 = document.getElementById('<%= lblErrMsg.ClientID %>');
            var hfSalt = document.getElementById('<%= hfSalt.ClientID %>');

            if (ValidatePassword(NewPassword) == false) {
                //display error msg.
                lblErrMsg2.innerHTML = 'Password must contain at least one upper case letter, one special character, one numeric number, and length should be at least 8 characters!<br />';
                return false;
            }

            if (NewPassword != NewPasswordConfirm) {
                //display error msg.
                lblErrMsg2.innerHTML = 'Password and Confirm Password must match!<br />';
                return false;
            }

            shaObj = new jsSHA(NewPassword, "ASCII");
            var passwordHash = shaObj.getHash("SHA-256", "HEX");
            if (NewPassword.length > 0) {
                document.getElementById('<%= txtNewPassword.ClientID %>').value = passwordHash;
            }

            //compute confirm password salted hash
            shaObj = new jsSHA(NewPasswordConfirm, "ASCII");
            var confirmPasswordHash = shaObj.getHash("SHA-256", "HEX");
            shaObj = new jsSHA(hfSalt.value + confirmPasswordHash, "ASCII");
            var confirmPasswordSaltedHash = shaObj.getHash("SHA-256", "HEX");
            if (NewPasswordConfirm.length > 0) {
                document.getElementById('<%= txtNewPasswordConfirm.ClientID %>').value = confirmPasswordSaltedHash;
            }

            hfSalt.value = '';
            //submit form
            return true;
        }
        function ClearPwd() {


            document.getElementById('<%= txtNewPassword.ClientID %>').value = '';
            document.getElementById('<%= txtNewPasswordConfirm.ClientID %>').value = '';

            //submit form
            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <div class="InputForm" style="width: 90%">
                <h2>Manage users</h2>
                <div style="width: 70%;">
                    <p>
                        <asp:Button ID="btnBack" runat="server" Text="Back to home" CssClass="button_example" OnClick="btnBack_Click" />
                    </p>
                    <div class="row-fluid">
                        <div class="span6 ">
                            <div class="control-group">
                                <asp:Label ID="routename" runat="server" CssClass="control-label" Style="font-size: 14px;">First Name/SAP ID/Login Id<span style="color:Red;"> *</span></asp:Label>
                                <div class="controls">
                                    <input type="text" id="txtAdmissionId" style="width: 70%;" class="autosuggest" placeholder="Search with First Name/SAP ID/Login Id" />
                                    <asp:Button ID="btnGO" runat="server" CssClass="button_example" Text="Search" OnClick="btnGO_Click" />
                                    <asp:HiddenField ID="hfCustomerId" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="span3">
                            <div class="control-group">
                                <asp:Label ID="lblSession" runat="server" CssClass="control-label" Style="font-size: 14px;">User Details </asp:Label>
                                <div class="controls">
                                    <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                                    <asp:Label ID="lblSAPID" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnStudentName" runat="server" />

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <br />
                <br />

                <asp:Literal ID="litInfo" runat="server" EnableViewState="False"></asp:Literal>

                <div runat="server" id="spanResetPasssword" visible="false">
                    <h4>Reset selected User's Password</h4>
                    <p>
                        <asp:Label ID="Label3" runat="server" Text="New Password:" AssociatedControlID="txtNewPassword"
                            EnableViewState="False"></asp:Label>
                        <asp:TextBox ID="txtNewPassword" autocomplete="off" AutoCompleteType="None" runat="server" ToolTip="Password must contain at least one upper case letter, one special character, one numeric number, and length should be at least 8 characters!"
                            TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                            Display="Dynamic" EnableViewState="False" ErrorMessage="Required!" SetFocusOnError="True"
                            ValidationGroup="ResetPasswordGrp"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="hfSalt" runat="server" />

                    </p>
                    <p>
                        <asp:Label ID="Label1" runat="server" Text="Confirm New Password:" AssociatedControlID="txtNewPasswordConfirm"
                            EnableViewState="False"></asp:Label>
                        <asp:TextBox ID="txtNewPasswordConfirm" autocomplete="off" AutoCompleteType="None" runat="server" ToolTip="Password must contain at least one upper case letter, one special character, one numeric number, and length should be at least 8 characters!"
                            TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewPasswordConfirm" runat="server" ControlToValidate="txtNewPasswordConfirm"
                            Display="Dynamic" EnableViewState="False" ErrorMessage="Required!" SetFocusOnError="True"
                            ValidationGroup="ResetPasswordGrp"></asp:RequiredFieldValidator>
                    </p>
                    <asp:Label ID="lblErrMsg" runat="server" EnableViewState="False"></asp:Label>
                    <asp:Button ID="btnSubmit" runat="server" EnableViewState="False" Text="Submit" ValidationGroup="ResetPasswordGrp"
                        OnClientClick="return ComputeHash();" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnReset" runat="server" EnableViewState="False" OnClientClick="ClearPwd();"
                        ValidationGroup="ResetPasswordGrp" Text="Reset" OnClick="btnReset_Click" />
                </div>

                <br />
                &nbsp;<asp:Literal ID="litInfo2" runat="server" EnableViewState="False"></asp:Literal>
                <asp:GridView ID="gridUsers" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="50"
                    EmptyDataText="No users present in the system!"
                    EnableViewState="true" Width="100%" Style="margin-bottom: 0px" OnRowCommand="gridUsers_RowCommand"
                    OnRowDeleted="gridUsers_RowDeleted" OnRowUpdated="gridUsers_RowUpdated"
                    OnRowDeleting="gridUsers_RowDeleting"
                    OnRowEditing="gridUsers_RowEditing"
                    OnPageIndexChanging="gridUsers_PageIndexChanging"
                    OnRowDataBound="gridUsers_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="SrNo." ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Login Id" ItemStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:HiddenField ID="hfDistrictCodeE" Value='<%# Bind("USER_CODE") %>' runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HiddenField ID="hfDistrictCodeR" Value='<%# Bind("USER_CODE") %>' runat="server" />
                                <asp:HiddenField ID="hdnstatecode" Value='<%# Bind("USER_CODE") %>' runat="server" />
                                <asp:Label ID="lblDistrictCodeR" runat="server" Text='<%# Eval("USER_CODE") %>'></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Id" ItemStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("USERID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLoginIdUser" runat="server" Text='<%# Bind("USERID")%>'></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="First Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("F_Name") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLoginIdUserName" runat="server" Text='<%# Bind("F_Name")%>'></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SAP Id" ItemStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SAP_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLoginIdUserSapId" runat="server" Text='<%# Bind("SAP_ID")%>'></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Status" ItemStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">

                            <ItemTemplate>
                                <asp:Label ID="lblLoginIdUserStatus" runat="server" Text='<%# Bind("UserStatus")%>'></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="true" HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnUnlock" runat="server" CssClass="LinkButtons" CommandName="UnlockUser"
                                    CommandArgument='<%# Eval("USERID") %>' Text="[Unlock User]" />
                                &nbsp;
                    <asp:LinkButton ID="btnChangePassword" runat="server" CssClass="LinkButtons" CommandName="Select"
                        CommandArgument='<%#  Eval("USERID") %>' Text="[Reset Password]" ValidationGroup="LockUnlockGrp" />
                                &nbsp;
                                <asp:LinkButton ID="lnkReleaseUser" runat="server" CssClass="LinkButtons" CommandName="ReleaseUser"
                                    CommandArgument='<%# Eval("USERID") %>' Text="[Release User]"  />
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle BackColor="Orange" ForeColor="Black" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
