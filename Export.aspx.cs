using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using System.Globalization;
using CrystalDecisions.Shared;

namespace NewApp.CrRPT
{
    public partial class Export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["report"] != null)
                {


                    ReportDocument rpt = (ReportDocument)Session["report"];
                    rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");

                }
                else
                {
                    Response.Write("No Record Found");
                }

            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);

            }
        }
    }
}