using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Security.Application;
using System.ComponentModel;
using System.Text;
using NewApp.Models;

/// <summary>
/// Summary description for AntiCSRF
/// </summary>
public class AntiCSRF
{
	public AntiCSRF()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static bool ValidateToken()
    {
        //obtain hidden field containing token from the client.
        if ((HttpContext.Current.CurrentHandler) is Page)
        {
            Page currentPage = (Page)HttpContext.Current.CurrentHandler;

            //first time?
            if (HttpContext.Current.Session["CSRFToken"] == null)
            {
                return true;
            }

            if (HttpContext.Current.Request.Form["CSRFToken"] != null)
            {
                //Dim CSRFTokenHiddenField As HiddenField = CType(currentPage.FindControl("CSRFToken"), HiddenField)

                string CSRFTokenInSession = HttpContext.Current.Session["CSRFToken"].ToString();
                string CSRFTokenInHiddenField = HttpContext.Current.Request.Form["CSRFToken"];

                if (CSRFTokenInSession != null & CSRFTokenInHiddenField != null)
                {
                    return CSRFTokenInSession.Equals(CSRFTokenInHiddenField);
                }

            }
        }

        //faliure i.e csrf token not matching/or missing.
        return false;

        //Return (userCSRFToken IsNot Nothing And userCSRFToken.Equals(HttpContext.Current.Session("CSRFToken")))
    }

    public static void GenerateCSRFToken()
    {
        string csrfToken = GetUniqueKey(32);
        //Guid.NewGuid().ToString("d") 

        HttpContext.Current.Session["CSRFToken"] = csrfToken;
        if ((HttpContext.Current.CurrentHandler) is Page)
        {
            Page currentPage = (Page)HttpContext.Current.CurrentHandler;

            currentPage.ClientScript.RegisterHiddenField("CSRFToken", csrfToken);
        }

    }

    public static string GetCSRFToken()
    {
        return HttpContext.Current.Session["CSRFToken"].ToString();
    }

    public static string GetUniqueKey(int length)
    {

        string[] charsMap = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

        Random rnd = new Random();
        StringBuilder sb = new StringBuilder("");
        int i = 0;
        for (i = 1; i <= length; i++)
        {
            sb.Append(charsMap[rnd.Next(charsMap.Length)]);
        }

        return sb.ToString();
    }
}