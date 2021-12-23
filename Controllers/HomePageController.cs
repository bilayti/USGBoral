using CrystalDecisions.CrystalReports.Engine;
using NewApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Mvc;

namespace NewApp.Controllers
{
    public class HomePageController : BaseController
    {
        #region Local variable declaration
        SqlConnection _Con;
        SqlCommand _Cmd;
        SqlDataAdapter _Ad;
        DataSet _DS = null;
        string username = string.Empty;
        string lastseen = string.Empty;
        string UserCode = string.Empty;
        int usertype = 0;
        int _PortalId = 0;
        public List<CardCodeBind> _CustomerList = new List<CardCodeBind>();
        public List<UserRegistrationDetails> _UserList = new List<UserRegistrationDetails>();
        #endregion

        #region Connection Method
        public void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            _Con = new SqlConnection(constr);
        }
        #endregion

        #region Constructor
        public HomePageController()
        {
            username = (string)System.Web.HttpContext.Current.Session["_SAP_ID"];
            lastseen = (string)System.Web.HttpContext.Current.Session["LastLoginDateTime"];
            UserCode = (string)System.Web.HttpContext.Current.Session["USER_CODE"];
            usertype = (int)System.Web.HttpContext.Current.Session["USER_TYPEID"];
        }

        #endregion

        #region Index
        public ActionResult Home()
        {
            try
            {
                if (UserCode == null || username == "")
                {
                    return Redirect("~/Default.aspx");
                }
                else
                {
                    int time = DateTime.Now.Hour;
                    if (time > 24)
                    {
                        time = 24;
                    }
                    if (time < 12)
                    {
                        ViewBag.UserName = "Good Morning : " + Session["F_NAME"].ToString();
                    }
                    else if (time < 17)
                    {
                        ViewBag.UserName = "Good Afternoon : " + Session["F_NAME"].ToString();
                    }
                    else
                    {
                        ViewBag.UserName = "Good Evening : " + Session["F_NAME"].ToString();
                    }
                    ViewBag.lastseen = "Last Login:" + Session["LastLoginDateTime"].ToString();

                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return View();

        }

        #endregion

        #region Bind Value Then Customer Card Code Selectes
        [HttpPost]
        public JsonResult CustomerOnChange(string CardCode)
        {
            List<CardCode> _Mod = new List<CardCode>();
            connection();
            try
            {
                _Con.Open();
                _Cmd = new SqlCommand("POR_SP_Bind_HOME", _Con);
                _Cmd.CommandType = CommandType.StoredProcedure;
                _Cmd.Parameters.AddWithValue("@CardCode", (CardCode));
                _Ad = new SqlDataAdapter(_Cmd);
                DataSet _Ds;
                _Ds = new DataSet();
                _Ad.Fill(_Ds);
                foreach (DataRow _Dr in _Ds.Tables[0].Rows)
                {
                    _Mod.Add(new CardCode()
                    {
                        Address = _Dr["Address"].ToString(),
                        City = _Dr["City"].ToString(),
                        ZipCode = _Dr["ZipCode"].ToString(),
                        State1 = _Dr["State1"].ToString(),
                        Country = _Dr["Country"].ToString(),
                        Area = _Dr["Area"].ToString(),
                        Territory = _Dr["Territory"].ToString(),
                        SlpCode = _Dr["SlpCode"].ToString(),
                        CntctPrsn = _Dr["CntctPrsn"].ToString(),
                        Phone1 = _Dr["Phone1"].ToString(),
                        Fax = _Dr["Fax"].ToString(),
                        E_Mail = _Dr["E_Mail"].ToString(),
                        BalanceSys = _Dr["BalanceSys"].ToString(),
                        LastPaymentOn = _Dr["LastPaymentOn"].ToString(),
                        PAN = _Dr["PAN"].ToString(),
                        TIN = _Dr["TIN"].ToString(),
                        GSTINNo = _Dr["GSTINNo"].ToString(),
                        OverDueLess15 = _Dr["OverDueLess15"].ToString(),
                        OverDueGre15 = _Dr["OverDueGre15"].ToString(),
                        ExpPayment = _Dr["ExpPayment"].ToString(),
                        ordervalue = _Dr["ordervalue"].ToString(),
                        Shortfall = _Dr["Shortfall"].ToString(),
                        OverDueDays = _Dr["OverDueDays"].ToString(),
                        Warehouse = _Dr["Warehouse"].ToString(),
                        CreditLimit = _Dr["CreditLimit"].ToString(),
                        CreditDate = _Dr["CreditDate"].ToString(),
                        Consumed = _Dr["Consumed"].ToString(),
                        Order_Value = _Dr["Order_Value"].ToString(),
                        Limit = _Dr["Limit"].ToString(),
                        Total_Outstanding = _Dr["Total_Outstanding"].ToString(),
                        Available_Limit = _Dr["Available_Limit"].ToString(),
                        MaxOverduedays = _Dr["MAXOverDueDays"].ToString(),
                        Totaloverdue = _Dr["TotalOverDue"].ToString(),
                        Cerditbalance = _Dr["CREDITBALANCE"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _Con.Close();
                _Con.Dispose();
            }
            return Json(_Mod, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get Customer Dropdown List
        [HttpPost]
        public JsonResult GetAllCustomerDdl(string _UserType)
        {
            //string _SAPId,
            DataSet _Ds = null;
            try
            {
                _Ds = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@UserType", _UserType.ToString()));
                _Ds = dataAccess.GetDataSet("SP_GetCustomer", lstparameters);
                foreach (DataRow _Dr in _Ds.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        CardCode = _Dr["CardCode"].ToString(),
                        CardName = _Dr["cardname"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GET USER TYPE DETAILS
        public ActionResult UserType()
        {
            int time = DateTime.Now.Hour;
            if (time > 24)
            {
                time = 24;
            }
            if (time < 12)
            {
                ViewBag.UserName = "Good Morning : " + Session["F_NAME"].ToString();
            }
            else if (time < 17)
            {
                ViewBag.UserName = "Good Afternoon : " + Session["F_NAME"].ToString();
            }
            else
            {
                ViewBag.UserName = "Good Evening : " + Session["F_NAME"].ToString();
            }
            ViewBag.lastseen = "Last Login:" + Session["LastLoginDateTime"].ToString();
            return View();
        }
        [HttpPost]
        public JsonResult GetUserTypeDetails()
        {
            try
            {
                
_UserList.Clear();
                _DS = new DataSet();
                _DS = dataAccess.GetDataSet("SP_USERTYPE_DETAILS");
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _UserList.Add(new UserRegistrationDetails()
                    {
                        USERID = Convert.ToInt32(_Dr["USER_ID"].ToString()),
                        USER_TYPE = _Dr["USER_TYPE"].ToString(),
                        User_Count = Convert.ToInt32(_Dr["User_Count"].ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(_UserList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Manage User
        [HttpPost]
        public JsonResult GetAutoStudentData(string username)
        {
            List<string> result = new List<string>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("select (Convert(varchar(100),F_NAME+' - '+CONVERT(varchar(10),USERID,0)+' - '+F_NAME+' - '+SAP_ID+' - '+USER_CODE))NAME,F_NAME,USERID,SAP_ID from PUSR where F_NAME LIKE '%'+@SearchText+'%' or USERID LIKE '%'+@SearchText+'%' or SAP_ID LIKE '%'+@SearchText+'%' or USER_CODE LIKE '%'+@SearchText+'%'", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchText", username);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        result.Add(string.Format("{0}/{1}/{2}/{3}", dr["NAME"], dr["F_NAME"], dr["USERID"], dr["SAP_ID"]));
                    }

                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region OverDue Days
        [HttpPost]
        public JsonResult BindViewLessThanEqual15(string sCardCode)
        {
            try
            {
                _CustomerList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                _DS = dataAccess.GetDataSet("SP_PORTAL_OVECRDUELESS15", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        DocNum_Less = _Dr["DocNum_Less"].ToString(),
                        DocDate_Less = _Dr["DocDate_Less"].ToString(),
                        DocDueDate_Less = _Dr["DocDueDate_Less"].ToString(),
                        Doctotal_Less = _Dr["Doctotal_Less"].ToString(),
                        BalancAmount_Less = _Dr["BalancAmount_Less"].ToString(),
                        DAYS_Less1 = _Dr["DAYS_Less1"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);

            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BindViewGreaterThan15(string sCardCode)
        {
            //string sCardCode = "C20000";
            try
            {
                _CustomerList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                _DS = dataAccess.GetDataSet("SP_PORTAL_OVECRDUE_GreThan15", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        DocNum_Gre = _Dr["DocNum_Gre"].ToString(),
                        DocDate_Gre = _Dr["DocDate_Gre"].ToString(),
                        DocDueDate_Gre = _Dr["DocDueDate_Gre"].ToString(),
                        Doctotal_Gre = _Dr["Doctotal_Gre"].ToString(),
                        BalancAmount_Gre = _Dr["BalancAmount_Gre"].ToString(),
                        DAYS_Less = _Dr["DAYS_Less"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BindViewExpPaymnet(string sCardCode)
        {
            try
            {
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                _DS = dataAccess.GetDataSet("SP_PORTAL_EXP_PAYMENT", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        Document_No2 = _Dr["Document_No2"].ToString(),
                        DocDate2 = _Dr["DocDate2"].ToString(),
                        DocDueDate2 = _Dr["DocDueDate2"].ToString(),
                        DocTotal2 = _Dr["DocTotal2"].ToString(),
                        BalancAmount2 = _Dr["BalancAmount2"].ToString(),
                        DAYS2 = _Dr["DAYS2"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PartiallyUnpaidInvoice(string sCardCode)
        {
            try
            {
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                _DS = dataAccess.GetDataSet("ShortFall", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        InvoiceNo = _Dr["InvoiceNo"].ToString(),
                        InvoiceDate = _Dr["InvoiceDate"].ToString(),
                        UnpaidDueDate = _Dr["Duedate"].ToString(),
                        InvoiceAmount = _Dr["InvoiceAmount"].ToString(),
                        PaidAmount = _Dr["PaidAmount"].ToString(),
                        BalancAmount = _Dr["BalanceAmount"].ToString(),
                        UnpaidDueDays = _Dr["Duedays"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BindViewOrderValue(string sCardCode)
        {
            try
            {
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                _DS = dataAccess.GetDataSet("SP_PORTAL_ORDERVALUE", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        Document_No3 = _Dr["Document_No3"].ToString(),
                        Document_Date3 = _Dr["Document_Date3"].ToString(),
                        Posting_Date3 = _Dr["Posting_Date3"].ToString(),
                        DocTotal3 = _Dr["DocTotal3"].ToString(),
                        BalancAmount3 = _Dr["BalancAmount3"].ToString(),
                        DAYS3 = _Dr["DAYS3"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region EMPORT EXCEL REPORTS

        public ActionResult AgingReportExcel(string sCardCode, string sRadio)
        {
            string _Result = string.Empty;
            _PortalId = Convert.ToInt32(username);
            try
            {
                _CustomerList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_CUSTOMER_AGING", lstparameters);

                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        CardCode = _Dr["Cardcode"].ToString(),
                        CustomerName = _Dr["Customer name"].ToString(),
                        Parent_Child = _Dr["Parent_Child"].ToString(),
                        sAgingLocation = _Dr["U_UNE_LOC"].ToString(),
                        CurrentBalance = _Dr["Current Balance"].ToString(),
                        Days_030 = _Dr["0-30 Days"].ToString(),
                        Days_3160 = _Dr["31-60 Days"].ToString(),
                        Days_6190 = _Dr["61-90 Days"].ToString(),
                        Days_91120 = _Dr["91-120 Days"].ToString(),
                        Days_121 = _Dr["121+ Days"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PendingOrderReportExcel(string sCardCode, string sFromDate, string sToDate, string sStatus, string sRadio)
        {
            _PortalId = Convert.ToInt32(username);
            string _Result = string.Empty;
            try
            {

                _CustomerList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@STATUS", sStatus.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_SALESORDER", lstparameters);

                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        Document_No_Pen = _Dr["Document_No_Pen"].ToString(),
                        CardCode_Pen = _Dr["CardCode_Pen"].ToString(),
                        CardName_Pen = _Dr["CardName_Pen"].ToString(),
                        Location = _Dr["Location"].ToString(),
                        dispatchedqty = _Dr["Quantity_Pen"].ToString(),
                        pendingqty = _Dr["Open_Quantity_Pen"].ToString(),
                        Status = _Dr["Status"].ToString(),
                        DocTotal = _Dr["DocTotal"].ToString(),
                        TranspoterName = _Dr["TranspoterName"].ToString(),
                        LR_NO = _Dr["LR_NO"].ToString(),
                        Veh_No = _Dr["Veh_No"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ItemPurchaseExcel(string sCardCode, string sFromDate, string sToDate, string sRadio)
        {
            string _Result = string.Empty;
            _PortalId = Convert.ToInt32(username);
            try
            {

                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_ItemPurchase_Group", lstparameters);

                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        ItemGroup_ITEM = _Dr["ItmsGrpNam"].ToString(),
                        Quantity_ITEM = _Dr["Quantity"].ToString(),
                        DocTotal_ITEM = _Dr["DocTotal"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InvoiceReportExcel(string sCardCode, string sFromDate, string sToDate, string sRadio)
        {
            string _Result = string.Empty;
            _PortalId = Convert.ToInt32(username);
            try
            {
                _CustomerList.Clear();

                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_InvoiceReport", lstparameters);

                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        Document_ENo_IN = _Dr["Document_No_IN"].ToString(),
                        CardCode_IN = _Dr["CardCode_IN"].ToString(),
                        CardName_IN = _Dr["CardName_IN"].ToString(),
                        DocDate_C1 = _Dr["Document_Date_IN"].ToString(),
                        Total = _Dr["INVOICE_TOTAL"].ToString(),
                        Location = _Dr["Location"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AccountStatementReportExcel(string sCardCode, string sFromDate, string sToDate, string sRadio)
        {
            string _Result = string.Empty;
            _PortalId = Convert.ToInt32(username);
            try
            {
                _CustomerList.Clear();

                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_AccountStatement", lstparameters);

                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        CardName = _Dr["Customer Name"].ToString(),
                        CardCode = _Dr["Customer Code"].ToString(),
                        // 
                        REFDATE = _Dr["Ref Date"].ToString(),
                        Location = _Dr["Location"].ToString(),
                        Ref1 = _Dr["Ref1"].ToString(),
                        Ref2 = _Dr["Ref2"].ToString(),
                        TRANSID = _Dr["Trans Id"].ToString(),
                        baseRef = _Dr["Base Ref"].ToString(),//invoice no
                        Series = _Dr["Series"].ToString(),
                        LINEMEMO = _Dr["Line Memo"].ToString(),//remarks
                        InvoiceComment = _Dr["Invoice Comment"].ToString(),
                        CreditNoteComment = _Dr["Credit Note Comment"].ToString(),
                        IncomingPaymentComment = _Dr["Incoming payment Comment"].ToString(),
                        DEBIT = _Dr["Debit"].ToString(),
                        CREDIT = _Dr["Credit"].ToString(),
                        TransType = _Dr["Trans Type"].ToString(),
                        RunningTotal = _Dr["RunningTotal"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BankReceiptReportExcel(string sCardCode, string sFromDate, string sToDate, string sRadio)
        {
            string _Result = string.Empty;
            _PortalId = Convert.ToInt32(username);
            try
            {
                _CustomerList.Clear();

                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PARTAL_BankReceipt", lstparameters);

                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        Document_No1 = _Dr["Document_No1"].ToString(),
                        Posting_Date1 = _Dr["Posting_Date1"].ToString(),
                        Discription1 = _Dr["Discription1"].ToString(),
                        Original_Value = _Dr["Original_Value"].ToString(),
                        PaymentType = _Dr["PaymentType"].ToString(),
                        ChequeNo = _Dr["ChequeNo"].ToString(),
                        ChequeDate = _Dr["ChequeDate"].ToString(),
                        DriaweeBankName = _Dr["DriaweeBankName"].ToString(),
                        CheckAmount = _Dr["CheckAmount"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreditNoteReportExcel(string sCardCode, string sFromDate, string sToDate, string sRadio)
        {
            string _Result = string.Empty;
            _PortalId = Convert.ToInt32(username);
            try
            {
                _CustomerList.Clear();

                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_CREDITNOTE", lstparameters);

                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        CardName_C1 = _Dr["CardName_C1"].ToString(),
                        CardCode_C1 = _Dr["CardCode_C1"].ToString(),
                        CREDIT_NOTE_NO_C1 = _Dr["CREDIT_NOTE_NO_C1"].ToString(),
                        DocDate_C1 = _Dr["DocDate_C1"].ToString(),
                        WAREHOUSE_C1 = _Dr["WAREHOUSE_C1"].ToString(),
                        ItemCode_C1 = _Dr["ItemCode_C1"].ToString(),
                        Description_C1 = _Dr["Description_C1"].ToString(),
                        Quantity_C1 = _Dr["Quantity_C1"].ToString(),
                        ITEM_VALUE_C1 = _Dr["ITEM_VALUE_C1"].ToString(),
                        Open_Qty_C1 = _Dr["Open_Qty_C1"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Excel For Release Object
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
                //MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion

        #region TAB REPORTS
        [HttpPost]
        public JsonResult AgingReport(string sCardCode, string sRadio)
        {
            try
            {
                _PortalId = Convert.ToInt32(username);
                _CustomerList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_CUSTOMER_AGING", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        CardCode = _Dr["Cardcode"].ToString(),
                        CustomerName = _Dr["Customer name"].ToString(),
                        Parent_Child = _Dr["Parent_Child"].ToString(),
                        sAgingLocation = _Dr["U_UNE_LOC"].ToString(),
                        CurrentBalance = _Dr["Current Balance"].ToString(),
                        Days_030 = _Dr["0-30 Days"].ToString(),
                        Days_3160 = _Dr["31-60 Days"].ToString(),
                        Days_6190 = _Dr["61-90 Days"].ToString(),
                        Days_91120 = _Dr["91-120 Days"].ToString(),
                        Days_121 = _Dr["121+ Days"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var jsonResult = Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
            //return Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PendingOrderReport(string sCardCode, string sFromDate, string sToDate, string sStatus, string sRadio)
        {
            try
            {
                _PortalId = Convert.ToInt32(username);
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@STATUS", sStatus.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_SALESORDER", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        Document_No_Pen = _Dr["Document_No_Pen"].ToString(),
                        CardCode_Pen = _Dr["CardCode_Pen"].ToString(),
                        CardName_Pen = _Dr["CardName_Pen"].ToString(),
                        Location = _Dr["Location"].ToString(),
                        dispatchedqty = _Dr["Quantity_Pen"].ToString(),
                        pendingqty = _Dr["Open_Quantity_Pen"].ToString(),
                        Status = _Dr["Status"].ToString(),
                        DocTotal = _Dr["DocTotal"].ToString(),
                        TranspoterName = _Dr["TranspoterName"].ToString(),
                        LR_NO = _Dr["LR_NO"].ToString(),
                        Veh_No = _Dr["Veh_No"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var jsonResult = Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
            //return Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);

            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ItemPurchase(string sCardCode, string sFromDate, string sToDate, string sRadio)
        {
            try
            {
                _PortalId = Convert.ToInt32(username);
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_ItemPurchase_Group", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        //ItemGroupCode_ITEM = _Dr["ItmsGrpCod"].ToString(),
                        ItemGroup_ITEM = _Dr["ItmsGrpNam"].ToString(),
                        Quantity_ITEM = _Dr["Quantity"].ToString(),
                        DocTotal_ITEM = _Dr["DocTotal"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var jsonResult = Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
            //return Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);

            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InvoiceReport(string sCardCode, string sFromDate, string sToDate, string sRadio)
        {
            try
            {
                _PortalId = Convert.ToInt32(username);
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_InvoiceReport", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        Document_ENo_IN = _Dr["Document_No_IN"].ToString(),
                        CardCode_IN = _Dr["CardCode_IN"].ToString(),
                        CardName_IN = _Dr["CardName_IN"].ToString(),
                        DocDate_C1 = _Dr["Document_Date_IN"].ToString(),
                        Total = _Dr["INVOICE_TOTAL"].ToString(),
                        Location = _Dr["Location"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var jsonResult = Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
            //return Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AccountStatement(string sCardCode, string sFromDate, string sToDate, string sRadio)
        {
            try
            {
                _PortalId = Convert.ToInt32(username);
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_AccountStatement", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        CardName = _Dr["Customer Name"].ToString(),
                        CardCode = _Dr["Customer Code"].ToString(),
                        // 
                        REFDATE = _Dr["Ref Date"].ToString(),
                        Location = _Dr["Location"].ToString(),
                        Ref1 = _Dr["Ref1"].ToString(),
                        Ref2 = _Dr["Ref2"].ToString(),
                        TRANSID = _Dr["Trans Id"].ToString(),
                        baseRef = _Dr["Base Ref"].ToString(),//invoice no
                        Series = _Dr["Series"].ToString(),
                        LINEMEMO = _Dr["Line Memo"].ToString(),//remarks
                        InvoiceComment = _Dr["Invoice Comment"].ToString(),
                        CreditNoteComment = _Dr["Credit Note Comment"].ToString(),
                        IncomingPaymentComment = _Dr["Incoming payment Comment"].ToString(),
                        //CardCode = _Dr["CardCode"].ToString(),
                        DEBIT = _Dr["Debit"].ToString(),
                        CREDIT = _Dr["Credit"].ToString(),
                        TransType = _Dr["Trans Type"].ToString(),
                        RunningTotal = _Dr["RunningTotal"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var jsonResult = Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
            //return Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FeedBack(string sCardCode, string sFromDate, string sToDate, string sStatus, string sRadio)
        {
            try
            {
                _PortalId = Convert.ToInt32(username);
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_FeedBack", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        //CustomerName = _Dr["CARDNAME"].ToString(),
                        //Area = _Dr["BILL T0 ADDRESS"].ToString(),
                        //City = _Dr["SHIP TO ADDRESS"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AccountStatementOld(string sCardCode, string sFromDate, string sToDate, string sStatus, string sRadio)
        {
            try
            {
                _PortalId = Convert.ToInt32(username);
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_AccountStatementOld", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        //CustomerName = _Dr["CARDNAME"].ToString(),
                        //Area = _Dr["BILL T0 ADDRESS"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BankReceiptReport(string sCardCode, string sFromDate, string sToDate, string sRadio)
        {
            try
            {
                _PortalId = Convert.ToInt32(username);
                _CustomerList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PARTAL_BankReceipt", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        Document_No1 = _Dr["Document_No1"].ToString(),
                        Posting_Date1 = _Dr["Posting_Date1"].ToString(),
                        Discription1 = _Dr["Discription1"].ToString(),
                        Original_Value = _Dr["Original_Value"].ToString(),
                        PaymentType = _Dr["PaymentType"].ToString(),
                        ChequeNo = _Dr["ChequeNo"].ToString(),
                        ChequeDate = _Dr["ChequeDate"].ToString(),
                        DriaweeBankName = _Dr["DriaweeBankName"].ToString(),
                        CheckAmount = _Dr["CheckAmount"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var jsonResult = Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
            //return Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreditNoteReport(string sCardCode, string sFromDate, string sToDate, string sRadio)
        {
            try
            {
                _PortalId = Convert.ToInt32(username);
                _CustomerList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_CREDITNOTE", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        CardName_C1 = _Dr["CardName_C1"].ToString(),
                        CardCode_C1 = _Dr["CardCode_C1"].ToString(),
                        CREDIT_NOTE_NO_C1 = _Dr["CREDIT_NOTE_NO_C1"].ToString(),
                        DocDate_C1 = _Dr["DocDate_C1"].ToString(),
                        WAREHOUSE_C1 = _Dr["WAREHOUSE_C1"].ToString(),
                        ItemCode_C1 = _Dr["ItemCode_C1"].ToString(),
                        Description_C1 = _Dr["Description_C1"].ToString(),
                        Quantity_C1 = _Dr["Quantity_C1"].ToString(),
                        ITEM_VALUE_C1 = _Dr["ITEM_VALUE_C1"].ToString(),
                        Open_Qty_C1 = _Dr["Open_Qty_C1"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var jsonResult = Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
            //return Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        #region VIEW DETAIL REPORTS
        [HttpPost]
        public JsonResult BindAgingReportRowWise(string sCardCode, string sRadio)
        {
            try
            {
                _PortalId = Convert.ToInt32(username);
                _CustomerList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                //lstparameters.Add(new Parameters("@MCUSTOMER", sRadio));
                //lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_CUSTOMER_AGING_DETAILS", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        Refdate = _Dr["Refdate"].ToString(),
                        CurrentBalance = _Dr["Balance Due"].ToString(),
                        Remarks = _Dr["Amount"].ToString(),
                        DocTotal_AD = _Dr["DocTotal"].ToString(),
                        DocumentValueLC = _Dr["Document Value LC"].ToString(),
                        LineID = _Dr["LineID"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var jsonResult = Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BindItemSubGroup(string sCardCode, string sFromDate, string sToDate, string sRadio, string sGroupName)
        {
            try
            {
                _PortalId = Convert.ToInt32(username);
                _CustomerList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate));
                lstparameters.Add(new Parameters("@TODATE", sToDate));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio));
                lstparameters.Add(new Parameters("@GROUP", sGroupName));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_ItemPurchase_SUB_Group", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        ItemGroup_ITEM = _Dr["ItmsGrpNam"].ToString(),
                        QuantitySub_ITEM = _Dr["Quantity"].ToString(),
                        DocTotalSub_ITEM = _Dr["DocTotal"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var jsonResult = Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BindItemGroupItem(string sCardCode, string sFromDate, string sToDate, string sRadio, string sGroupName)
        {
            try
            {
                _PortalId = Convert.ToInt32(username);
                _CustomerList.Clear();
                _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate));
                lstparameters.Add(new Parameters("@TODATE", sToDate));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio));
                lstparameters.Add(new Parameters("@GROUP", sGroupName));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_ItemPurchase", lstparameters);
                foreach (DataRow _Dr in _DS.Tables[0].Rows)
                {
                    _CustomerList.Add(new CardCodeBind()
                    {
                        DocEntry_ITEM = _Dr["DocEntry_ITEM"].ToString(),
                        CardCode_ITEM = _Dr["CardCode_ITEM"].ToString(),
                        CardName_ITEM = _Dr["CardName_ITEM"].ToString(),
                        ItemCode_ITEM = _Dr["ItemCode_ITEM"].ToString(),
                        Discription_ITEM = _Dr["Discription_ITEM"].ToString(),
                        Quantity_ITEM = _Dr["Quantity_ITEM"].ToString(),
                        DocTotal_ITEM = _Dr["DocTotal_ITEM"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var jsonResult = Json(new { draw = 1, recordsTotal = _CustomerList.Count, recordsFiltered = 10, data = _CustomerList }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
            //return Json(_CustomerList, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region GetUserReportAuthorization
        [HttpPost]
        public JsonResult GetUserReportAuthorization()//string sUserID)
        {
            //string _SAPId,
            List<UserRegistrationDetails> _UserList = new List<UserRegistrationDetails>();
            DataSet _Ds = null;
            try
            {
                //string _UserTypeId = System.Web.HttpContext.Current.Session["USER_TYPEID"].ToString();
                //string _UserCode = System.Web.HttpContext.Current.Session["USER_CODE"].ToString();
                _Ds = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@UserTypeId", usertype.ToString()));
                lstparameters.Add(new Parameters("@UserCode", UserCode.ToString()));
                _Ds = dataAccess.GetDataSet("SP_PORTAL_GetUserAuthorization", lstparameters);
                foreach (DataRow _Dr in _Ds.Tables[0].Rows)
                {
                    _UserList.Add(new UserRegistrationDetails()
                    {
                        USERID = Convert.ToInt32(_Dr["UserId"].ToString()),
                        Aging = _Dr["Aging"].ToString(),
                        PendingOrder = _Dr["PendingOrder"].ToString(),
                        ItemPurchase = _Dr["ItemPurchase"].ToString(),
                        Invoice = _Dr["Invoice"].ToString(),
                        AccountStatement = _Dr["AccountStatement"].ToString(),
                        CollectionSummary = _Dr["CollectionSummary"].ToString(),
                        CreditNote = _Dr["CreditNote"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(_UserList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Crystal Report
        //Invoice Report
        [HttpPost]
        public ActionResult ExportRPT(string sDocNum, string sDocDate)
        {
            try
            {
                DataSet _DS = new DataSet();
                DataSet _DS1 = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@DOCNUM", sDocNum.ToString()));
                lstparameters.Add(new Parameters("@Date", sDocDate.ToString()));
                _DS = dataAccess.GetDataSet("GET_WHS", lstparameters);

                if (_DS.Tables[0].Rows.Count > 0)
                {
                    if (_DS.Tables[0].Rows[0][0].ToString() == "H")
                    {
                        _DS1 = dataAccess.GetDataSet("UNE_SP_GST_INVOICE_PORTAL", lstparameters);

                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "GST Final Invoice layout Export PORTAL.rpt"));
                        rd.SetDataSource(_DS1.Tables[0]);

                        Session["report"] = rd;

                    }
                    if (_DS.Tables[0].Rows[0][0].ToString() == "G")
                    {
                        //_DS1 = dataAccess.GetDataSet("UNE_SP_GST_INVOICE_PORTAL", lstparameters);
                        _DS1 = dataAccess.GetDataSet("UNE_SP_GST_INVOICE_PORTAL_Final", lstparameters);

                        ReportDocument rd = new ReportDocument();
                        //rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "Final Invoice Layout-NEW 20082020.rpt"));
                        rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "TCS Invoice- Item 03-11-2020.rpt"));
                        rd.SetDataSource(_DS1.Tables[0]);

                        if (rd.Subreports.Count > 0)
                        {
                            List<Parameters> lstparameters1 = new List<Parameters>();
                            lstparameters1.Add(new Parameters("@DOCNUM", sDocNum.ToString()));
                            lstparameters1.Add(new Parameters("@Date", sDocDate.ToString()));;
                            _DS1 = dataAccess.GetDataSet("GST_Freight", lstparameters1);
                            rd.Subreports[0].SetDataSource(_DS1.Tables[0]);
                        }
                        Session["report"] = rd;

                    }
                    if (_DS.Tables[0].Rows[0][0].ToString() == "F")
                    {
                        _DS1 = dataAccess.GetDataSet("UNE_SP_CRSTALRPT_BEFORE_INCLUDED_EXTRENL", lstparameters);

                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "Tax Invoice1-16112016.rpt"));
                        rd.SetDataSource(_DS1.Tables[0]);

                        Session["report"] = rd;

                    }
                    else if (_DS.Tables[0].Rows[0][0].ToString().Equals("E"))
                    {
                        //_DS1 = dataAccess.GetDataSet("SFLLP_SALESInvoiceSERVICE", lstparameters);
                        _DS1 = dataAccess.GetDataSet("UNE_SP_GST_INVOICEFORSERVICE_PORTAL_FINAL", lstparameters);

                        ReportDocument rd = new ReportDocument();
                        //rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "Purchase Invoice_14102016.rpt"));
                        //rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "GST AR INVOICE  SERVICE TYPE REPORT 20082020.rpt"));
                        rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "GST AR INVOICE  SERVICE TYPE REPORT.rpt"));
                        rd.SetDataSource(_DS1.Tables[0]);

                        //if (rd.Subreports.Count > 0)
                        //{
                        //    rd.Subreports[0].SetDataSource(_DS1.Tables[0]);
                        //}

                        Session["report"] = rd;
                    }
                    else if (_DS.Tables[0].Rows[0][0].ToString().Equals("D"))
                    {
                        _DS1 = dataAccess.GetDataSet("PORTAL_BEFORE_INCLUDED_EXTRENL_Export", lstparameters);

                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "Export_New-2.rpt"));
                        rd.SetDataSource(_DS1.Tables[0]);

                        Session["report"] = rd;
                    }
                    else if (_DS.Tables[0].Rows[0][0].ToString().Equals("C"))
                    {
                        _DS1 = dataAccess.GetDataSet("UNE_SP_CRSTALRPT_BEFORE_INCLUDED_EXTRENL", lstparameters);

                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "TaxinvoiceForm8-16112016.rpt"));
                        rd.SetDataSource(_DS1.Tables[0]);

                        Session["report"] = rd;
                    }
                    else if (_DS.Tables[0].Rows[0][0].ToString().Equals("B"))
                    {
                        _DS1 = dataAccess.GetDataSet("PORTAL__BEFORE_INCLUDED_EXTRENL_Scarp", lstparameters);

                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "UNE_Scarp_Invoice.rpt"));
                        rd.SetDataSource(_DS1.Tables[0]);

                        Session["report"] = rd;
                    }
                    else if (_DS.Tables[0].Rows[0][0].ToString().Equals("A"))
                    {
                        _DS1 = dataAccess.GetDataSet("UNE_SP_CRSTALRPT_BEFORE_INCLUDED_EXTRENL", lstparameters);

                        ReportDocument rd = new ReportDocument();
                        rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "une_rinvoiceL22122015.rpt"));
                        rd.SetDataSource(_DS1.Tables[0]);

                        Session["report"] = rd;
                    }

                }
                var recordFound = _DS.Tables[0].Rows.Count;
                var jsonResult = Json(new { recordFound }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = Int32.MaxValue;
                return jsonResult;

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return null;
            }

        }
        // Credit Note Report
        [HttpPost]
        public ActionResult ExportRPTCreditNote(string sDocNum, string sDocDate)
        {
            try
            {
                DataSet _DS = new DataSet();
                DataSet _DS1 = new DataSet();
                DataSet _DS2 = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@DOCNUM", sDocNum.ToString()));
                lstparameters.Add(new Parameters("@Date", sDocDate.ToString()));
                _DS = dataAccess.GetDataSet("PORTAL_CREDIT_DOC", lstparameters);
                if (_DS.Tables[0].Rows.Count > 0)
                {
                    if (_DS.Tables[0].Rows[0][0].ToString() == "B")
                    {
                        //_DS1 = dataAccess.GetDataSet("UNE_SP_GST_CRNOTEFORSERVICE_PORTAL", lstparameters);
                        _DS1 = dataAccess.GetDataSet("UNE_SP_GST_ARCREDITFORSERVICE_PORTAL_FINAL", lstparameters);
                        ReportDocument rd = new ReportDocument();

                        //rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "GST AR CREDIT SERVICE TYPE REPORT_01092017.rpt"));
                        //rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "GST AR CREDIT SERVICE TYPE REPORT_20082020.rpt"));
                        rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "GST AR CREDIT SERVICE TYPE REPORT_01092017.rpt"));
                        rd.SetDataSource(_DS1.Tables[0]);

                        if (rd.Subreports.Count > 0)
                        {
                            List<Parameters> lstparameters1 = new List<Parameters>();
                            lstparameters1.Add(new Parameters("@DOCNUM", sDocNum.ToString()));
                            lstparameters1.Add(new Parameters("@Date", sDocDate.ToString()));
                            _DS2 = dataAccess.GetDataSet("UNE_SP_GST_CRNOTEFORSERVICE_PORTAL_FRGT", lstparameters1);
                            rd.Subreports[0].SetDataSource(_DS2.Tables[0]);
                        }
                        Session["report"] = rd;
                    }
                    if (_DS.Tables[0].Rows[0][0].ToString() == "A")
                    {
                        
                        //_DS1 = dataAccess.GetDataSet("UNE_SP_GST_CRNOTE_PORTAL", lstparameters);
                        _DS1 = dataAccess.GetDataSet("UNE_SP_GST_CREDIT_NOTE_PORTAL_FINAL", lstparameters);
                        ReportDocument rd = new ReportDocument();

                        //rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "GST AR CREDIT REPORT.rpt"));
                        //rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "GST AR CREDIT REPORT item 20082020.rpt"));
                        rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "GST AR CREDIT -Item REPORT.rpt"));
                        rd.SetDataSource(_DS1.Tables[0]);

                        if (rd.Subreports.Count > 0)
                        {
                            List<Parameters> lstparameters2 = new List<Parameters>();
                            lstparameters2.Add(new Parameters("@DOCNUM", sDocNum.ToString()));
                            lstparameters2.Add(new Parameters("@Date", sDocDate.ToString()));
                            _DS2 = dataAccess.GetDataSet("UNE_SP_GST_CRNOTE_PORTAL_freight", lstparameters2);
                            rd.Subreports[0].SetDataSource(_DS2.Tables[0]);
                        }
                        Session["report"] = rd;
                    }
                }

                var recordFound = _DS.Tables[0].Rows.Count;
                var jsonResult = Json(new { recordFound }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = Int32.MaxValue;
                return jsonResult;

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return null;
            }

        }
        // Payment Summary Report
        [HttpPost]
        public ActionResult ExportRPTPaymentSummary(string sDocNum, string sDocDate)
        {
            try
            {
                DataSet _DS = new DataSet();
                DataSet _DS1 = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@DOCNUM", sDocNum.ToString()));
                lstparameters.Add(new Parameters("@Date", sDocDate.ToString()));
                _DS = dataAccess.GetDataSet("PORTAL_INCOMING_PAYMENTS_CONDITION", lstparameters);
                if (_DS.Tables[0].Rows.Count > 0)
                {
                    if (_DS.Tables[0].Rows[0][0].ToString() == "A")
                    {

                        _DS1 = dataAccess.GetDataSet("PORTAL_INCOMING_PAYMENTS", lstparameters);

                        ReportDocument rd = new ReportDocument();

                        rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "IncomingPayments.rpt"));
                        rd.SetDataSource(_DS1.Tables[0]);

                        Session["report"] = rd;
                    }
                    if (_DS.Tables[0].Rows[0][0].ToString() == "B")
                    {

                        _DS1 = dataAccess.GetDataSet("PORTAL_INCOMING_PAYMENTS_CANCEL", lstparameters);

                        ReportDocument rd = new ReportDocument();

                        rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "IncomingPayments_CANCEL.rpt"));
                        rd.SetDataSource(_DS1.Tables[0]);

                        Session["report"] = rd;
                    }
                }

                var recordFound = _DS.Tables[0].Rows.Count;
                var jsonResult = Json(new { recordFound }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = Int32.MaxValue;
                return jsonResult;

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return null;
            }

        }
        // Account Statement Report
        [HttpPost]
        public ActionResult ExportRPTAccountStatementPDFData(string sCardCode, string sFromDate, string sToDate, string sRadio)
        {
            try
            {
                DataSet _DS = new DataSet();
                List<Parameters> lstparameters = new List<Parameters>();
                lstparameters.Add(new Parameters("@CARDCODE", sCardCode));
                lstparameters.Add(new Parameters("@FROMDATE", sFromDate.ToString()));
                lstparameters.Add(new Parameters("@TODATE", sToDate.ToString()));
                lstparameters.Add(new Parameters("@MCUSTOMER", sRadio.ToString()));
                lstparameters.Add(new Parameters("@PORTALID", _PortalId.ToString()));
                _DS = dataAccess.GetDataSet("SP_PORTAL_AccountStatement", lstparameters);

                if (_DS.Tables[0].Rows.Count > 0)
                {
                    ReportDocument rd = new ReportDocument();

                    rd.Load(Path.Combine(Server.MapPath("~/CrRPT"), "CustomerStatement.rpt"));
                    rd.SetDataSource(_DS.Tables[0]);

                    Session["report"] = rd;
                }

                var recordFound = _DS.Tables[0].Rows.Count;
                var jsonResult = Json(new { recordFound }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = Int32.MaxValue;
                return jsonResult;

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return null;
            }

        }

        #endregion
    }
}
