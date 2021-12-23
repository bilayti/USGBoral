using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NewApp.Models
{
    public class CardCodeBind
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
                
        #region TAB REPORTS PARAMETERS
        public int DocEntry { get; set; }


        #region Local variable declaration 
        SqlConnection _Con;
        SqlCommand _Cmd;
        SqlDataAdapter _Ad;
        DataSet _Ds;
        private List<CardCodeBind> _CardCode = new List<CardCodeBind>();
        //private List<CardCodeBind> _CardName = new List<CardCodeBind>();
        #endregion

        #region Connection Method
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            _Con = new SqlConnection(constr);
        }
        #endregion

        
        #region CardCode and CardName Bind in Home Page
        public List<CardCodeBind> CardCodeBindMethod(int _UserTypeId)
        {
            try
            {
                string _UserTypeId1 = System.Web.HttpContext.Current.Session["USER_TYPEID"].ToString();
                string _sapid = System.Web.HttpContext.Current.Session["_SAP_ID"].ToString();
                connection();
                _Con.Open();
                _Cmd = new SqlCommand("SP_GetCustomer", _Con);
                _Cmd.CommandType = CommandType.StoredProcedure;
                _Cmd.Parameters.AddWithValue("@SAPId", _sapid);
                _Cmd.Parameters.AddWithValue("@UserType", _UserTypeId1);                
                _Ad = new SqlDataAdapter(_Cmd);
                _Ds = new DataSet();
                _Ad.Fill(_Ds);
                foreach (DataRow _Dr in _Ds.Tables[0].Rows)
                {
                    _CardCode.Add(new CardCodeBind() { CardCode = _Dr[0].ToString(), CardName = _Dr[1].ToString() });

                }
                _Con.Close();
                
            }
            catch
            {
            }
            return _CardCode;
        }
        
        #endregion

        #region Bind View Less Then Is Equal 15
        public string DocNum_Less { get; set; }
        public string DocDate_Less { get; set; }
        public string DocDueDate_Less { get; set; }
        public string Doctotal_Less { get; set; }
        public string BalancAmount_Less { get; set; }
        public string DAYS_Less1 { get; set; }
        #endregion

        #region Bind View Greater Then 15
        public string DocNum_Gre { get; set; }
        public string DocDate_Gre { get; set; }
        public string DocDueDate_Gre { get; set; }
        public string Doctotal_Gre { get; set; }
        public string BalancAmount_Gre { get; set; }
        public string DAYS_Less { get; set; }
        #endregion

        #region Bind View Exp Payment
        public string Document_No2 { get; set; }
        public string DocDate2 { get; set; }
        public string DocDueDate2 { get; set; }
        public string DocTotal2 { get; set; }
        public string BalancAmount2 { get; set; }
        public string DAYS2 { get; set; }
        #endregion

        #region Bind partially unpaid invoice
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string UnpaidDueDate { get; set; }
        public string InvoiceAmount { get; set; }
        public string PaidAmount { get; set; }
        public string BalancAmount { get; set; }
        public string UnpaidDueDays { get; set; }
        #endregion

        #region Bind View Order Value
        public string Document_No3 { get; set; }
        public string Document_Date3 { get; set; }
        public string Posting_Date3 { get; set; }
        public string DocTotal3 { get; set; }
        public string BalancAmount3 { get; set; }
        public string DAYS3 { get; set; }
        #endregion

        #region AGING REPORTS
        public string CustomerName { get; set; }
        public string CurrentBalance { get; set; }
        public string Parent_Child { get; set; }
        public string sAgingLocation { get; set; }
        public string Days_121 { get; set; }
        public string Days_030 { get; set; }
        public string Days_3160 { get; set; }
        public string Days_6190 { get; set; }
        public string Days_91120 { get; set; }
        public string Total { get; set; }
        // for details
        public string TransId { get; set; }
        public string Refdate { get; set; }
        public string Remarks { get; set; }
        public string DocumentValueLC { get; set; }
        public string LineID { get; set; }
        public string DocTotal_AD { get; set; }


        #endregion

        #region PENDING ORDER REPORTS
        public string Document_No_Pen { get; set; }
        public string CardName_Pen { get; set; }
        public string CardCode_Pen { get; set; }
        public string DocTotal { get; set; }
        public string Status { get; set; }
        public string TranspoterName { get; set; }
        public string LR_NO { get; set; }
        public string Veh_No { get; set; }
        public string dispatchedqty { get; set; }
        public string pendingqty { get; set; }

        #endregion

        #region BANK RECEIPT REPORTS
        public string Document_No1 { get; set; }
        public string Posting_Date1 { get; set; }
        public string Discription1 { get; set; }
        public string Original_Value { get; set; }
        public string PaymentType { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public string DriaweeBankName { get; set; }
        public string CheckAmount { get; set; }

        #endregion

        #region ACCOUNT STATEMENT REPORTS
        public string REFDATE { get; set; }
        public string Ref2 { get; set; }
        public string Ref1 { get; set; }
        public string DocDate { get; set; }
        public string TRANSID { get; set; }
        public string baseRef { get; set; }
        public string Series { get; set; }
        public string LINEMEMO { get; set; }
        public string InvoiceComment { get; set; }
        public string CreditNoteComment { get; set; }
        public string IncomingPaymentComment { get; set; }
        public string DEBIT { get; set; }
        public string CREDIT { get; set; }
        public string TransType { get; set; }
        public string RunningTotal { get; set; }
        //public string Location { get; set; }
        #endregion

        #region ITEM PURCHASE REPORTS
        public string DocEntry_ITEM { get; set; }
        public string CardCode_ITEM { get; set; }
        public string ItemGroup_ITEM { get; set; }
        public string ItemGroupCode_ITEM { get; set; }
        public string CardName_ITEM { get; set; }
        public string ItemCode_ITEM { get; set; }
        public string Discription_ITEM { get; set; }
        public string Quantity_ITEM { get; set; }
        public string DocTotal_ITEM { get; set; }
        
        public string QuantitySub_ITEM { get; set; }
        public string DocTotalSub_ITEM { get; set; }
        
        public string QuantityItem_ITEM { get; set; }
        public string DocTotalItem_ITEM { get; set; }
        
        #endregion

        #region ITEM INVOICE REPORTS
        public string Document_ENo_IN { get; set; }
        public string Document_No_IN { get; set; }
        public string CardCode_IN { get; set; }
        public string CardName_IN { get; set; }
        public string Document_Date_IN { get; set; }
        public string ItemCode_IN { get; set; }
        public string Description_IN { get; set; }
        public string Quantity_IN { get; set; }
        public string INVOICE_DOCNUM_IN { get; set; }
        public string INVOICE_DocDate_IN { get; set; }
        public string INVOICE_Quantity_IN { get; set; }
        public string INVOICE_OpenQua_IN { get; set; }
        public string Location { get; set; }

        #endregion

        #region CREDIT NOTE
        public string CardName_C1 { get; set; }
        public string CardCode_C1 { get; set; }
        public string CREDIT_NOTE_NO_C1 { get; set; }
        public string DocDate_C1 { get; set; }
        public string WAREHOUSE_C1 { get; set; }
        public string ItemCode_C1 { get; set; }
        public string Description_C1 { get; set; }
        public string Quantity_C1 { get; set; }
        public string ITEM_VALUE_C1 { get; set; }
        public string Open_Qty_C1 { get; set; }
        #endregion

        #endregion

    }
}