<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Captcha.ascx.vb" Inherits="Controls_Captcha" %>
<div id="CaptchaDiv" runat="server" style="width: 200px; padding: 0px; margin: 0px; text-align: left">
    <%--<strong>Prevent SPAM &nbsp;&nbsp; <span style="float: right"></strong>--%>
    <div style="float: right;">
        <asp:ImageButton ID="btnReGenerate" OnClientClick="return ClearPwd();" runat="server" CausesValidation="false" ImageUrl="~/Captcha/refresh-icon16x16.png" ToolTip="Regenerate Captcha Code" />
    </div>
    <asp:Image ID="imgCaptcha" runat="server" AlternateText="Captcha Code" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Enter the code shown above:" AssociatedControlID="txtCaptchaCodeInput"></asp:Label>
    <br />
    <asp:TextBox ID="txtCaptchaCodeInput" runat="server" EnableViewState="False"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rvfCaptchaCode" runat="server" 
        ControlToValidate="txtCaptchaCodeInput" Display="Dynamic" 
        EnableViewState="False" ErrorMessage="Verification code is required!" 
        SetFocusOnError="True" ToolTip="Verification code is required!">*</asp:RequiredFieldValidator>
        
    
    <script type="text/javascript">
            function ClearPwd() {
            
                //compute hash
                document.getElementById('<%= PasswordFieldID %>').value = '';
              
                
                //submit form
                return true;
            }
        </script>
    
</div>
