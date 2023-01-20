using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_CommTrex
{
    public partial class CreateFuture_options : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string buttonstate = btnSave.InnerText.Replace("\r\n", "");
            string button = buttonstate.Replace(" ", "");
            if (!IsPostBack)
            {
                if (Session["TalukaDetails"] != null)
                {
                    if (Convert.ToString(Session["UserType"]) == "U")
                    {
                        Response.Redirect("Home.aspx");
                    }
                    FillFuture_OptionGrid();
                    LockControls(false);
                }
                else
                {
                    Response.Redirect("Home.aspx"); // Session time out
                }
            }
            else
            {
                if (button == "Save")
                {
                    fetchEditDetails();
                }
                else if (button == "New")
                {
                    fetchEditDetails();
                }
                else if (button == "Update")
                {


                }
                else
                {

                }
            }
        }


        protected void fetchEditDetails()
        {
            if (HidBnkId.Value == "")
            {
            }
            else
            {
                try
                {

                    string mstrGetUser = "";
                    mstrGetUser = "SELECT * from Com_Future where iFutureId= '" + HidBnkId.Value + "'";
                    //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                    DataTable dtGetUserDetails = new DataTable();


                    //dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, Convert.ToString(Session["SystemUserSqlConnectionString"]), false,
                    //                SqlHelper.AddInParam("@iPayId", SqlDbType.Int, Convert.ToInt32(HidBnkId.Value)));

                    dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);

                    txtSymbol.Text = dtGetUserDetails.Rows[0]["sSymbol"].ToString();
                    txtQuantity.Text = dtGetUserDetails.Rows[0]["iLotSize"].ToString();
                    expdate.Value = Convert.ToDateTime(dtGetUserDetails.Rows[0]["ExpiryDate"]).ToString("yyyy-MM-dd");
                    btnSave.InnerHtml = "Update";
                    btnClear.InnerHtml = "Cancel";
                    LockControls(true);
                    SetMessage(false, "Press Update to save changes!");
                    HidEdit.Value = "true";
                    HiddenField3.Value = "o";

                }
                catch (Exception exError)
                {
                    long pLngErr = -1;
                    if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                        pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                    pLngErr = GlobalFunctions.ReportError("FillClientGrid", "paymentpage", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                    actionInfo.Attributes["class"] = "alert alert-info blink-border";
                    actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
                }
            }
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
             string buttonstate = btnSave.InnerText.Replace("\r\n", "");
            string button = buttonstate.Replace(" ", "");
            try
            {
                if (button == "Save")
                {
                    string strValidate = "";
                    strValidate = ValidateUser(txtSymbol.Text, txtQuantity.Text, expdate.Value);//, ddl_Type.SelectedItem.Value
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateFuture", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,

                        SqlHelper.AddInParam("@sSymbol", SqlDbType.VarChar, txtSymbol.Text),
                        SqlHelper.AddInParam("@iLotSize", SqlDbType.Int, txtQuantity.Text),
                        SqlHelper.AddInParam("@ExpiryDate", SqlDbType.Date, expdate.Value));
                        FillFuture_OptionGrid();
                        SetMessage(false, "Future Option Saved Successfully");
                        LockControls(false);
                        btnSave.Attributes["btn-action"] = "New";
                        //btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> New";
                        ClearControls();
                        SetMessage(false, "Future Option Added Succesfully!!");
                    }
                    else
                    {
                        SetMessage(true, strValidate);
                    }
                }
                else if (button=="Update")
                {
                      try
            {
           
                string strErrorImg = "";
             
                   string strValidate = "";
                   strValidate = ValidateUser(txtSymbol.Text, txtQuantity.Text, expdate.Value);//, ddl_Type.SelectedItem.Value
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateFuture", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                               SqlHelper.AddInParam("@sSymbol", SqlDbType.VarChar, txtSymbol.Text),
                               SqlHelper.AddInParam("@iLotSize", SqlDbType.VarChar, txtQuantity.Text),
                               SqlHelper.AddInParam("@ExpiryDate", SqlDbType.Date, expdate.Value),
                               SqlHelper.AddInParam("@iFutureId", SqlDbType.Int, HidBnkId.Value));


                        SetProductsUpdateMessage(false, "Create Future Updated Successfully");
                        grdFuture.EditIndex = -1;
                        FillFuture_OptionGrid();
                        SetMessage(false, "Create Future Updated Succesfully!!");
                        //grdUser.Columns[6].Visible = false;
                        btnSave.InnerHtml = "Save";
                        btnClear.InnerHtml = "Clear";
                        ClearControls();
                    }
                    else
                    {
                        SetProductsUpdateMessage(false, strValidate);
                    }
              
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("GridView1_RowUpdating1", "CategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
                    }
                else
                {
                    ClearControls();
                    LockControls(true);
                    btnSave.Attributes["btn-action"] = "Save";
                    //btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> Save";
                    btnSave.InnerHtml = "Save";
                    SetMessage(false, "Press Save To Add Future Option!!");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSave_ServerClick", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }
        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            string buttonstate = btnClear.InnerText.Replace("\r\n", "");
            string button = buttonstate.Replace(" ", "");
            if (button == "Cancel")
            {
                ClearControls();
                btnSave.InnerHtml = "Save";
                btnClear.InnerHtml = "Clear";
                HidBnkId.Value = "";

            }
            else if (button == "Clear")
            {
                ClearControls();

            }
            else { }
        }
        private string ValidateUser(string strSymbol, string strQuantity, string expirydate)//, string strType
        {
            string mstrValidate = "";
            if (strSymbol == "")
            {
                mstrValidate = mstrValidate + " Symbol Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strQuantity == "")
            {
                mstrValidate = mstrValidate + "Quantity Cannot be Blank !!!";
                return mstrValidate;
            }
            if (expirydate == "")
            {
                mstrValidate = mstrValidate + "Expiry Date Cannot be Blank !!!";
            }
            return mstrValidate;
        }

        public void FillFuture_OptionGrid()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT * from  Com_Future WHERE bIsActive=1";
                //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                grdFuture.DataSource = dtGetUserDetails;
                grdFuture.DataBind();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillFuture_OptionGrid", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }
        private void SetProductsUpdateMessage(bool pBlnIsError, string strMessage)
        {
            updateActionDiv.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            updateActionDiv.InnerHtml = strMessage;
        }
        private void SetMessage(bool pBlnIsError, string strMessage)
        {
            actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            actionInfo.InnerHtml = strMessage;
        }
        public void LockControls(bool pBoolState)
        {
            txtSymbol.Enabled = pBoolState;
            txtQuantity.Enabled = pBoolState;
            //ddl_Type.Enabled = pBoolState;
        }
        public void ClearControls()
        {
            txtSymbol.Text = "";
            txtQuantity.Text = "";
            expdate.Value = "";
            //ddl_Type.SelectedIndex = 0;
        }
        protected void btnDeleteFuture_opt_ServerClick(object sender, EventArgs e)
        {
            //string strquery = "update Com_Future set @IsActive=@isavtive where iFutureId=@id";
            DataTable dtCatData = SqlHelper.ReadTable("spDeleteCreateFuture", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                SqlHelper.AddInParam("@id", SqlDbType.Int, txtDelHidden.Value.Trim()));
            SetProductsUpdateMessage(false, "Admin Deleted Successfully");
            txtDelHidden.Value = "";
            txtSymbol.Text = "";
            txtQuantity.Text = "";
            grdFuture.EditIndex = -1;
            FillFuture_OptionGrid();
           
        }

            protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdFuture.EditIndex = e.NewEditIndex;
            ViewState["RowVal"] = e.NewEditIndex;
            FillFuture_OptionGrid();
            //grdAdmin.Columns[5].Visible = true;
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox username = (TextBox)grdFuture.Rows[e.RowIndex].FindControl("txtUserName");
                TextBox UserSize = (TextBox)grdFuture.Rows[e.RowIndex].FindControl("TxtSize");

                Label lblID = (Label)grdFuture.Rows[e.RowIndex].FindControl("futureId");            
                string strErrorImg = "";
             
                   string strValidate = "";
                   strValidate = ValidateUser(username.Text, UserSize.Text, expdate.Value);//, ddl_Type.SelectedItem.Value
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateFuture", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                               SqlHelper.AddInParam("@sSymbol", SqlDbType.VarChar, username.Text),
                               SqlHelper.AddInParam("@iLotSize", SqlDbType.VarChar, UserSize.Text),
                               SqlHelper.AddInParam("@iFutureId", SqlDbType.Int, lblID.Text));


                        SetProductsUpdateMessage(false, "Create Future Updated Successfully");
                        grdFuture.EditIndex = -1;
                        FillFuture_OptionGrid();
                        ClearControls();
                    }
                    else
                    {
                        SetProductsUpdateMessage(false, strValidate);
                    }
                //int intTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
                //long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                //if (Convert.ToString(ViewState["ImgPath"]) != "")
                //    GlobalFunctions.saveInsertUserAction("Category_Master", "[Business Category Master Update]:Updation of Category with Id : " + Convert.ToInt64(lblCatID.Text) + " with Image : " + Convert.ToString(ViewState["ImgPath"]), intTalukaId, lngCompanyId, Request); //Call to user Action Log
                //else
                //    GlobalFunctions.saveInsertUserAction("Category_Master", "[Business Category Master Update]:Updation of Category with Id : " + Convert.ToInt64(lblCatID.Text) + " with Image : " + Convert.ToString(ViewState["ImgLogo"]), intTalukaId, lngCompanyId, Request); //Call to user Action Log
                //SetProductsUpdateMessage(false, "Category Updated Successfully");

                //if (txtImgPathMain.Value != "")
                //{
                //    bool blnFlagDelete = DeleteFile((txtImgPathMain.Value));
                //}
              
                //}
                //else
                //{
                //    SetProductsUpdateMessage(false, strError);

                //}
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("GridView1_RowUpdating1", "CategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdFuture.EditIndex = -1;
             FillFuture_OptionGrid();
            ClearControls();
            //if (txtImgPathMain.Value != "")
            //{
            //    bool blnFlagDelete = DeleteFile(txtImgPathMain.Value);
            //}
        }
    }
    }
