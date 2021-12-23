<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmAgree.aspx.cs" Inherits="NewApp.FrmAgree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Terms and Conditions</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Custom CSS -->
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.css" rel="stylesheet" />
    <!-- jQuery -->
    <script src="js/jquery.min.js"></script>
    <!----webfonts--->
    <link href="//fonts.googleapis.com/css?family=Roboto:400,100,300,500,700,900" rel="stylesheet" type="text/css" />
    <!---//webfonts--->
    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="wrapper">
                <div class="graphs">
                    <div class="widget_5" style="pointer-events: none;">
                        <div class="col-md-12 widget_1_box2">
                            <div class="widget-body">
                                <div class="editor">
                                    <div class="editor-content ng-isolate-scope" mb-scrollbar="contentScrollbar">
                                        <div class="editor-input ng-scope" contenteditable="true" style="font-size: 89%;">
                                            <b><u>
                                                <p align="center" style="color: black;font-size:medium;font-family:Verdana;">WEB ORDER APPLICATION TERMS & CONDITIONS</p></b></u>
                                <b><u>
                                    <p align="center" style="color: black">BETWEEN</p></b></u>
                                <b><u>
                                    <p align="center" style="color: black;font-size:medium;font-family:Verdana;">USG Boral Building Products (India) Private Limited (“USG Boral”)</p></b></u>
                                <b><u>
                                    <p align="center" style="color: black">AND</p></b></u>
                                <b><u>
                                    <p align="center" style="color: black;font-size:medium;font-family:Verdana;">USER (“You” or Purchaser) of the Web-Order Application</p></b></u>
                                <b>
                                    <p style="color: black;font-size:medium;font-family:Verdana;">1. Confidentiality</p>
                                </b>

                                            <p style="color: black">
                                                The information in the web-order application is proprietory to USG Boral and must be treated as confidential. Confidential Information shall mean any information disclosed by 
                                    USG Boral (here in after the “Disclosing Party”) to the other (here in after the “receiving Party”), either directly or indirectly, 
                                    inwriting, by inspection of tangible objects (including, without limitation, documents, prototypes, samples, media, documentation, discsandcode).
                                     Confidential Information shall include, without limitation, any materials, trade secrets, know- how, formulae, processes, algorithms, ideas strategies,
                                     inventions, data, network configurations, system architecture, designs, flow charts, drawings, proprietary information, business and marketing plans, 
                                    financial and operational information, and all othernon-public information, material or data relating to the currentand/orfuture business and
                                    operations of the Company and analysis, compilations, studies, summaries, extract sorother documentation prepared by the Receiving Party base 
                                    don information disclosed by the Disclosing Party.
                                            </p>
                                            <p style="color: black">
                                                This information should not be disclosed to any other party/non-employee without prior written 
                                    consent of USG Boral and should only be given to those employees who need to know the information in order to use the web order Application. 
                                    The Receiving Party under takes to protect the Confidential Information with the same degree of care as it uses to protect its own Confidential Information. 
                                    In addition to such degree of care,the Receiving Party agree snottoin any way disclose, copy, reproduce, modify, use (except as permitted under this Agreement), 
                                    or otherwise transfer the Confidential Information to any  other person orentity without obtaining priorwritten consent from the Disclosing Party. 
                                    USG Boral will look to you for reimbursement if it suffers any loss resulting from unauthorized use of confidential information, reproduction, sales, 
                                    use, disclosure or other distribution of any confidential information.
                                            </p>

                                            <b>
                                                <p style="color: black; font-size:medium;font-family:Verdana;">2. Limit of Liability</p>
                                            </b>
                                            
                                            <p style="color: black">
                                                USG Boral shall not beliable for any loss of profits, Loss of Use , Direct or indirect, special, incidental or consequential damage of any kind, in 
                                    connection with or arising out of the failure to furnish, the performance of, or the use of , any part of the web-order application, or for any misconduct or 
                                    any infringements resulting therefrom.
                                            </p>
                                            
                                            <b>
                                                <p style="color: black;font-size:medium;font-family:Verdana;">3. Termination</p>
                                            </b>
                                            
                                            <p style="color: black">
                                                Either you or USG Boral may terminate your use of the web-order application for any reason with 7 (seven) days of advance notice, In order to protect all 
                                    of users of the web-order application if (1) it is used for any unlawful or illegal purpose. 2) If the purchaser breaches the Security of the web-order 
                                    application, 3) if in USG Boral’s sole opinion , the purchaser telecommunications or computer/online system unreasonably interferes with USG Boral’s 
                                    normal computer operations. Also, if for any reason your payments are delinquent, USG Boral may immediately terminate your permission to use the web-order 
                                    application.
                                            </p>
                                            <b>
                                                <p style="color: black;font-size:medium;font-family:Verdana;">3. Termination</p>
                                            </b>

                                            <p style="color: black">The laws of Union TerThe laws of Union TerThe laws of Union Territory of India shall govern this agreement and is subject to jurisdiction of courts of law in Delhi. </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="checkbox-inline1 col-md-12" align="center">
                        <label>
                            <asp:CheckBox ID="chkAgreeTermCondition" runat="server" Text="I Agree" Style="font-size: medium; font-family:Verdana;" />
                            <asp:Button ID="btnAgreeTermCondition" runat="server" class="btn btn-xs btn-primary" Style="width: 50px; margin-left: 10px;font-family:Verdana;font-weight:bold;" Text="OK" OnClick="btnAgreeTermCondition_Click"></asp:Button>
                            <asp:Button ID="btnCancel" runat="server" class="btn btn-xs btn-success warning_4" Style="width: 70px; margin-left: 10px;font-family:Verdana;font-weight:bold;" Text="CANCEL" OnClick="btnCancel_Click"></asp:Button>
                        </label>
                    </div>
                </div>
            </div>
            <!-- /#page-wrapper -->
            <link href="css/custom.css" rel="stylesheet" />
            <!-- Metis Menu Plugin JavaScript -->
            <script src="js/metisMenu.min.js"></script>
            <script src="js/custom.js"></script>
            <!--User Define Function in Jquery-->
            <script type="text/javascript">
                $('#btnAgreeTermCondition').click(function () {
                    if ($("#chkAgreeTermCondition").prop('checked') == true) {
                    }
                    else {
                        alert('First agree terms and conditions');
                    }
                });
            </script>
        </div>
    </form>
</body>
</html>
