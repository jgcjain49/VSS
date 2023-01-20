using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace Admin_CommTrex
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            int LogId = Convert.ToInt16(Session["LogId"]);
            DataTable sdatetime = SqlHelper.ReadTable("select dbo.IndianTime() AS [IST_TIME]", GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);

            DataTable dtTable = SqlHelper.ReadTable("dbo.SP_Session_Log", GlobalVariables.SqlConnectionStringMstoreInformativeDb, true,

                SqlHelper.AddInParam("@LogId", SqlDbType.Int, LogId),
                SqlHelper.AddInParam("@LoginName", SqlDbType.VarChar, null),
                SqlHelper.AddInParam("@Password", SqlDbType.VarChar, null),
                SqlHelper.AddInParam("@CompanyKey", SqlDbType.VarChar, null),
                SqlHelper.AddInParam("@LoginType", SqlDbType.VarChar, null),
                SqlHelper.AddInParam("@ASNNumber", SqlDbType.VarChar, null),
                SqlHelper.AddInParam("@OrganizationName", SqlDbType.VarChar, null),
                SqlHelper.AddInParam("@City", SqlDbType.VarChar, null),
                SqlHelper.AddInParam("@State", SqlDbType.VarChar, null),
                SqlHelper.AddInParam("@Country", SqlDbType.VarChar, null),
                SqlHelper.AddInParam("@IPAddress", SqlDbType.VarChar, null),
                SqlHelper.AddInParam("@Latitude", SqlDbType.VarChar, null),
                SqlHelper.AddInParam("@Longitude", SqlDbType.VarChar, null),
                SqlHelper.AddInParam("@UserAgent", SqlDbType.VarChar, null),
                SqlHelper.AddInParam("@LoginDateTime", SqlDbType.DateTime, null),
                SqlHelper.AddInParam("@LogoutDateTime", SqlDbType.DateTime, Convert.ToDateTime(sdatetime.Rows[0]["IST_TIME"]))
                );
            Session.Clear();
            //Response.Redirect("Default.aspx");
            //On session end it will redirect to home page
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}