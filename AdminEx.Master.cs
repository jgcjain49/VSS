using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_CommTrex
{
    public partial class AdminEx : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sUserName = GlobalFunctions.VerifyLogin();
                if (sUserName == "")
                    Response.Redirect("Default.aspx");
                else
                {
                    if (Convert.ToString(Session["UserType"]) == "M") 
                    {
                        //mnuDistributor_Admin.Attributes["style"] = "display: none";
                        //mnuUserMgnt.Attributes["Style"] = "display: block;";
                        //mnuusertransfer.Attributes["style"] = "display: none";
                        //mnuTransactionReport.Attributes["style"] = "display:block;";
                        //mnuBusinessmaster.Attributes["style"] = "display:none";
                        //mnuProductDetailmaster.Attributes["style"] = "display:none";
                        ////mnuUserMgnt.Attributes["Style"] = "display:none";
                        //sMnuAutoSquare.Attributes["style"] = "display: none;";
                        //sMnuWeekSquare.Attributes["style"] = "display: none;";
                        //mnuExchange.Attributes["style"] = "display: none;";
                        ////mnuTransactionReport.Attributes["style"] = "display: none;";
                        //mnuGBeanReport.Attributes["style"] = "display: none;";
                        //mnuWithdraw.Attributes["style"] = "display: none;";
                        //mnuPayment.Attributes["style"] = "display: none;";
                        //mnuInactvUser.Attributes["style"] = "display: none;";
                        ////mnuDistributor.Attributes["style"] = "display: block;"; 
                        //mnuLedgerReport.Attributes["style"] = "display: none;";
                    }
                    if (Convert.ToString(Session["UserType"]) == "D")
                    {
                        //mnuDistributor.Attributes["style"] = "display: none";
                        //mnuDistributor_Admin.Attributes["style"] = "display: none";
                        //mnuBusinessmaster.Attributes["style"] = "display:none";
                        //mnuProductDetailmaster.Attributes["style"] = "display:none";
                        //mnuUserMgnt.Attributes["Style"] = "display: block;";
                        //mnuusertransfer.Attributes["style"] = "display: none";
                        //mnuapprovalmgmt.Attributes["style"] = "display: none;";
                        //sMnuAutoSquare.Attributes["style"] = "display: none;";
                        //sMnuWeekSquare.Attributes["style"] = "display: none;";
                        //mnuExchange.Attributes["style"] = "display: none;";
                        //mnuTransactionReport.Attributes["style"] = "display:block;";
                        //mnuGBeanReport.Attributes["style"] = "display: none;";
                        //mnuWithdraw.Attributes["style"] = "display: none;";
                        //mnuPayment.Attributes["style"] = "display: none;";
                        ////mnuNotify.Attributes["style"] = "display: none;";
                        //mnuInactvUser.Attributes["style"] = "display: none;";
                        //mnuLedgerReport.Attributes["style"] = "display: none;";
                    }
                    if (Convert.ToString(Session["UserType"]) == "A")
                    {
                        //mnuDistributor.Attributes["style"] = "display: none;";
                        /*
                        mnuBusinessmaster.Attributes["style"] = "display:none";
                        mnuProductDetailmaster.Attributes["style"] = "display:none";
                        //mnuUserMgnt.Attributes["Style"] = "display:none";
                        sMnuAutoSquare.Attributes["style"] = "display: none;";
                        sMnuWeekSquare.Attributes["style"] = "display: none;";
                        mnuCustomSearch.Attributes["style"] = "display: none;";
                        mnuExchange.Attributes["style"] = "display: none;";
                        //mnuTransactionReport.Attributes["style"] = "display: none;";
                        mnuGBeanReport.Attributes["style"] = "display: none;";
                        mnuWithdraw.Attributes["style"] = "display: none;";
                        mnuPayment.Attributes["style"] = "display: none;";
                         * */
                    }

                    ltrlWebSiteLevelName.Text = lblUserName_Mobile.InnerHtml = sUserName;

                    //if (Session["TalukaDetails"] != null)
                    //{
                    //    Literal1.Text = Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyName);
                    //    //Literal1.Text = Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaName);
                    //    //showgrid();
                    //}
                    //else
                    //{
                    //    Literal1.Text = Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyName);
                    //    //showgrid();
                    //}
                }
            }
            //Session["Reset"] = true;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
            //SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
            //int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
        }
        protected void showgrid()
        {
            //string strId;
            //if (Convert.ToString(Session["UserType"]) == "A")
            //{
            //    lblProfit.Text = "You are Admin";
            //}
            //else
            //{
            //    strId = Convert.ToString(Session["SystemUserID"]);
            //    DataTable dtResult = SqlHelper.ReadTable("sp_GetDoveDistributorWeeklyComm", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
            //            SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.NVarChar, 17),
            //            SqlHelper.AddInParam("@bnVarDistID", SqlDbType.NVarChar, Convert.ToString(Session["SystemUserID"])));
            //    if (dtResult.Rows.Count>0)
            //    {
            //        //decimal decimalVar = Convert.ToDecimal(dtResult.Rows[0]["AmtComm"]);

            //        //decimalVar = decimal.Round(decimalVar, 2, MidpointRounding.AwayFromZero);

            //        //decimalVar = Math.Round(decimalVar, 2);
            //        //lblProfit.Text = decimalVar.ToString();

            //        lblProfit.Text = dtResult.Rows[0]["AmtComm"].ToString();
            //    }
            //}
        }

        protected void btnSignOut_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int LogId = Convert.ToInt32(Session["LogId"]);
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
                Response.Redirect("Default.aspx");
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("SignOut", "AdminEx.Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                Session.Clear();
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
        }
        

    }
}