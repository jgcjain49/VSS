using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_CommTrex
{
    public partial class ReSetPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TalukaDetails"] != null)
            {
                if (!IsPostBack)
                {

                }
                //actionInfo.Visible = true;
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        private string ValidateProductData()
        {
            string mStrValidation = "";
            //string strvalidate = "";

            if (txtCurrPass.Text.Trim() == "")
            {
                mStrValidation += "- Please Enter Your Current Password...!!!" + Environment.NewLine;
                return (mStrValidation);
            }
            if (txtEnterPass.Text.Trim() == "")
            {
                mStrValidation += "- Please Enter New Password...!!!" + Environment.NewLine;
                return (mStrValidation);
            }
            if (txtEnterCnfPass.Text.Trim() == "")
            {
                mStrValidation += "- Please enter Password to confirm new Password...!!!" + Environment.NewLine;
                return (mStrValidation);
            }
            if (txtEnterPass.Text.Trim() != txtEnterCnfPass.Text.Trim())
            {
                mStrValidation += "- New password and confirm password didn't match...!!!" + Environment.NewLine;
                return (mStrValidation);
            }
            return mStrValidation;
        }
        private void SetProductsUpdateMessage(bool pBlnIsError, string pStrMessage)
        {
            updateActionDiv.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            updateActionDiv.InnerHtml = pStrMessage;
        }
        public void clearall()
        {
            txtCurrPass.Text = "";
            txtEnterCnfPass.Text = "";
            txtEnterPass.Text = "";
        }
        protected void btnPassChangeClick(object sender, EventArgs e)
        {
            try
            {
                string strId; string qForPassMatch;
                string sQuery;
                string strError = ValidateProductData();
                if (strError == "")
                {
                    if (Convert.ToString(Session["UserType"]) == "A")
                    {
                        strId = "M1";
                        qForPassMatch = GlobalFunctions.CreateDecryptTextSyntax("Comp_vCharLoginPass", false);
                        string compKey = Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey);
                        string compLoginId = Convert.ToString(((SysCompany)Session["SystemCompany"]).UserName);
                        sQuery = "Select * from Company_Master ";
                        sQuery += "Where Comp_vCharLoginId  ='" + compLoginId + "' and ";
                        sQuery += "Comp_vCharKey  ='" + compKey + "' and ";
                        sQuery += qForPassMatch + "='" + txtCurrPass.Text + "'";

                        DataTable dtUser = SqlHelper.ReadTable(sQuery, false);

                        if (dtUser.Rows.Count > 0)
                        {
                            DataTable dtupdateUser = SqlHelper.ReadTable("sp_ResetCompanyPassword", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                   SqlHelper.AddInParam("@key", SqlDbType.VarChar, compKey),
                                                   SqlHelper.AddInParam("@loginId", SqlDbType.VarChar, compLoginId),
                                                   SqlHelper.AddInParam("@varPass", SqlDbType.VarChar, txtEnterCnfPass.Text.Trim()));

                            if (dtupdateUser.Rows.Count > 0)
                            {
                                SetProductsUpdateMessage(true, "Your Password Changes Successfully.!!!");
                            }
                            else
                            {
                                SetProductsUpdateMessage(false, "Error while changing Password.!!!");
                            }

                        }
                        else
                        {
                            SetProductsUpdateMessage(false, "Enter your Valid Current Password.!!!");
                        }
                    }
                    else
                    {
                        strId = Convert.ToString(Session["SystemUserID"]);
                        qForPassMatch = GlobalFunctions.CreateDecryptTextSyntaxWithNVarchar("DD_nVarPass", true);
                        sQuery = "Select * from [dbo].[Dove_Dist_t_17] ";
                        sQuery += " where  ";
                        sQuery += " DD_bIntId='" + strId + "'";
                        //sQuery += " And UD_bItIsActive = 1";
                        sQuery += " And " +
                             qForPassMatch + "='" + txtCurrPass.Text + "'";
                        DataTable dtUser = SqlHelper.ReadTable(sQuery, false);

                        if (dtUser.Rows.Count > 0)
                        {
                            DataTable dtupdateUser = SqlHelper.ReadTable("sp_DoveResetM_D_Pass", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                   SqlHelper.AddInParam("@id", SqlDbType.NVarChar, strId),
                                                   SqlHelper.AddInParam("@nVarPass", SqlDbType.NVarChar, txtEnterCnfPass.Text.Trim()));

                            if (dtupdateUser.Rows.Count > 0)
                            {
                                SetProductsUpdateMessage(true, "Your Password Changes Successfully.!!!");
                            }
                            else
                            {
                                SetProductsUpdateMessage(false, "Error while changing Password.!!!");
                            }

                        }
                        else
                        {
                            SetProductsUpdateMessage(false, "Enter your Valid Current Password.!!!");
                        }
                    }

                    clearall();

                }
                else
                {
                    SetProductsUpdateMessage(false, strError);
                }

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("PassChangeClick", "ReSetPassword", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";

            }
        }
    }
}