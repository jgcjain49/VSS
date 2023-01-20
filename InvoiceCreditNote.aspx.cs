using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Admin_CommTrex
{
    public partial class InvoiceCreditNote : System.Web.UI.Page
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
                   FillInvoiceCreditNoteGrid();
                   FillDrdId();
                   getcommodityname();
                   getcontractname();
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
                    mstrGetUser = "SELECT * from Com_InvoiceCreditNote where iInvoiceId= '" + HidBnkId.Value + "'";
                    //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                    DataTable dtGetUserDetails = new DataTable();


                    //dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, Convert.ToString(Session["SystemUserSqlConnectionString"]), false,
                    //                SqlHelper.AddInParam("@iPayId", SqlDbType.Int, Convert.ToInt32(HidBnkId.Value)));

                    dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);

                    txtInvoiceNumber.Text = dtGetUserDetails.Rows[0]["sInvoiceNo"].ToString();
                    dtDate.Value = Convert.ToDateTime(dtGetUserDetails.Rows[0]["dDate"]).ToString("yyyy-MM-dd");
                    selectclientname.SelectedItem.Text = dtGetUserDetails.Rows[0]["sClientName"].ToString();
                    selectcontractname.SelectedItem.Text = dtGetUserDetails.Rows[0]["sContractName"].ToString();
                    selectCommodityName.SelectedItem.Text = dtGetUserDetails.Rows[0]["sCommodityName"].ToString();
                    txtHSNCode.Text = dtGetUserDetails.Rows[0]["sHsnCode"].ToString();
                    txtValueForLotSize.Text = dtGetUserDetails.Rows[0]["iValueLotSize"].ToString();
                    txtQuantity.Text = dtGetUserDetails.Rows[0]["iQty"].ToString();
                    txtTotalValue.Text = dtGetUserDetails.Rows[0]["nTotalValue"].ToString();
                    txtGST.Text = dtGetUserDetails.Rows[0]["nGst"].ToString();
                    txtOtherTax.Text = dtGetUserDetails.Rows[0]["nOtherTax"].ToString();
                    txtDiscount.Text = dtGetUserDetails.Rows[0]["nDiscount"].ToString();
                    radlRounding.Text = Convert.ToBoolean(dtGetUserDetails.Rows[0]["bIsRoundOff"]).ToString();

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

        public void LockControls(bool pBoolState)
        {
           txtInvoiceNumber.Enabled = pBoolState;
            selectclientname.Enabled = pBoolState;
            selectcontractname.Enabled = pBoolState;
            selectCommodityName.Enabled = pBoolState;
            txtHSNCode.Enabled = pBoolState;
            txtValueForLotSize.Enabled = pBoolState;
            txtQuantity.Enabled = pBoolState;
            txtTotalValue.Enabled = pBoolState;
            txtGST.Enabled = pBoolState;
            txtOtherTax.Enabled = pBoolState;
            txtDiscount.Enabled = pBoolState;
        }

        public void ChangeEventOnQuality(object sender, EventArgs e) {
            if (txtValueForLotSize.Text!="") {
                if (txtQuantity.Text != "") {
                    int txtValue = Convert.ToInt32(txtValueForLotSize.Text);
                    int txtQuanity = Convert.ToInt32(txtQuantity.Text);
                    int Total = txtValue * txtQuanity;
                    txtTotalValue.Text = Convert.ToString(Total);
                }
            }
        }

        public void changeEventOnLotSIze(object sender, EventArgs e){
            if (txtValueForLotSize.Text != "")
            {
                if (txtQuantity.Text != "")
                {
                    int txtValue = Convert.ToInt32(txtValueForLotSize.Text);
                    int txtQuanity = Convert.ToInt32(txtQuantity.Text);
                    int Total = txtValue * txtQuanity;
                    txtTotalValue.Text = Convert.ToString(Total);
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
                    strValidate = Validateinvoicecreditnote(txtInvoiceNumber.Text, dtDate.Value, selectclientname.SelectedItem.Text,
                        //txtTypeOfInvoice.Text,
                        selectcontractname.SelectedItem.Text, selectCommodityName.SelectedItem.Text,
                        txtHSNCode.Text, txtValueForLotSize.Text, txtQuantity.Text, txtTotalValue.Text,
                        txtGST.Text, txtOtherTax.Text, txtDiscount.Text, radlRounding.SelectedItem.Value);
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateInvoiceCreditNote", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                        SqlHelper.AddInParam("@sInvoiceNo", SqlDbType.VarChar, txtInvoiceNumber.Text),
                        SqlHelper.AddInParam("@dDate", SqlDbType.VarChar, dtDate.Value),
                        SqlHelper.AddInParam("@sClientName", SqlDbType.VarChar, selectclientname.SelectedItem.Text),
                        SqlHelper.AddInParam("@sContractName", SqlDbType.VarChar, selectcontractname.SelectedItem.Text),
                        SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, selectCommodityName.SelectedItem.Text),
                        SqlHelper.AddInParam("@sHsnCode", SqlDbType.VarChar, txtHSNCode.Text),
                        SqlHelper.AddInParam("@iValueLotSize", SqlDbType.Int, txtValueForLotSize.Text),
                        SqlHelper.AddInParam("@iQty", SqlDbType.Int, txtQuantity.Text),
                        SqlHelper.AddInParam("@nTotalValue", SqlDbType.Decimal, txtTotalValue.Text),
                        SqlHelper.AddInParam("@nGst", SqlDbType.Decimal, txtGST.Text),
                        SqlHelper.AddInParam("@nOtherTax", SqlDbType.Decimal, txtOtherTax.Text),
                        SqlHelper.AddInParam("@nDiscount", SqlDbType.Decimal, txtDiscount.Text),
                        SqlHelper.AddInParam("@bIsRoundOff", SqlDbType.Bit, Convert.ToBoolean(radlRounding.SelectedItem.Value))
                        );
                        FillInvoiceCreditNoteGrid();
                        SetMessage(false, "Admin Saved Successfully");
                        LockControls(false);
                        btnSave.Attributes["btn-action"] = "New";
                        //btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> New";
                        SetMessage(false, "Invoice Credit Note Added Succesfully!!");
                        selectclientname.Items.Clear();
                        FillDrdId();
                        selectcontractname.Items.Clear();
                        getcontractname();
                        selectCommodityName.Items.Clear();                    
                        getcommodityname();
                       
                        ClearAll();
                    }
                    else
                    {
                        SetMessage(true, strValidate);
                    }
                }
                else if (button == "Update")
                {
                    try
                    {
                                
                        string strErrorImg = "";
                       
                        string strValidate = "";
                        strValidate =  Validateinvoicecreditnote(txtInvoiceNumber.Text, dtDate.Value, selectclientname.SelectedItem.Text,
                        //txtTypeOfInvoice.Text,
                        selectcontractname.SelectedItem.Text, selectCommodityName.SelectedItem.Text,
                        txtHSNCode.Text, txtValueForLotSize.Text, txtQuantity.Text, txtTotalValue.Text,
                        txtGST.Text, txtOtherTax.Text, txtDiscount.Text, radlRounding.SelectedItem.Value);
                        if (strValidate == "")
                        {
                            DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateInvoiceCreditNote", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                 SqlHelper.AddInParam("@sInvoiceNo", SqlDbType.Int, txtInvoiceNumber.Text),
                                    SqlHelper.AddInParam("@dDate", SqlDbType.Date, dtDate.Value),
                                    SqlHelper.AddInParam("@sClientName", SqlDbType.VarChar, selectclientname.SelectedItem.Text),
                                    //SqlHelper.AddInParam("@sTypeOfInvoice", SqlDbType.VarChar, invtype.Text),
                                    SqlHelper.AddInParam("@sContractName", SqlDbType.VarChar, selectcontractname.SelectedItem.Text),
                                    SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, selectCommodityName.SelectedItem.Text),
                                    SqlHelper.AddInParam("@sHsnCode", SqlDbType.VarChar, txtHSNCode.Text),
                                    SqlHelper.AddInParam("@iValueLotSize", SqlDbType.Int, txtValueForLotSize.Text),
                                    SqlHelper.AddInParam("@iQty", SqlDbType.Int, txtQuantity.Text),
                                    SqlHelper.AddInParam("@nTotalValue", SqlDbType.Decimal, txtTotalValue.Text),
                                    SqlHelper.AddInParam("@nGst", SqlDbType.Decimal, txtGST.Text),
                                    SqlHelper.AddInParam("@nOtherTax", SqlDbType.Decimal, txtOtherTax.Text),
                                    SqlHelper.AddInParam("@nDiscount", SqlDbType.Decimal, txtDiscount.Text),
                                    SqlHelper.AddInParam("@bIsRoundOff", SqlDbType.Bit, Convert.ToBoolean(radlRounding.SelectedItem.Value)),

                                     SqlHelper.AddInParam("@iInvoiceId", SqlDbType.Int, HidBnkId.Value));

                            SetProductsUpdateMessage(false, "Invoice Credit Note Updated Successfully");
                            grdinvcrnote.EditIndex = -1;
                            FillInvoiceCreditNoteGrid();
                            SetMessage(false, "Invoice Updated Succesfully!!");
                            btnSave.InnerHtml = "Save";
                            btnClear.InnerHtml = "Clear";
                            selectclientname.Items.Clear();
                            selectcontractname.Items.Clear();
                            selectCommodityName.Items.Clear();
                            FillDrdId();
                            getcommodityname();
                            getcontractname();
                            ClearAll();
                          
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
                    ClearAll();
                    LockControls(true);
                    btnSave.Attributes["btn-action"] = "Save";
                    btnSave.InnerHtml = "Save";
                    //btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> Save";
                    SetMessage(false, "Press Save To Add Invoice Credit Note !!!");
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
        private string Validateinvoicecreditnote(string InvoiceNumber, string dtDate, string selectclientname,
                       //string TypeOfInvoice,
            string selectcontractname, string selectCommodityName,
                       string HSNCode, string ValueForLotSize, string Quantity, string TotalValue,
                       string GST, string OtherTax, string Discount, string radlRounding)
        {
            string mstrValidate = "";
            if (InvoiceNumber == "")
            {
                mstrValidate = mstrValidate + " Invoice Number Cannot be Blank !!!";
                return mstrValidate;
            }
           
            if (dtDate == "")
            {
                mstrValidate = mstrValidate + "Select Date !!!";
                return mstrValidate;
            }
            if (selectclientname == "")
            {
                mstrValidate = mstrValidate + "Client Name Cannot be Blank !!!";
                return mstrValidate;
            }
            //if (TypeOfInvoice == "")
            //{
            //    mstrValidate = mstrValidate + "Invoice Type Cannot be Blank !!!";
            //    return mstrValidate;
            //}
            if (selectcontractname == "")
            {
                mstrValidate = mstrValidate + "Contract Name Cannot be Blank !!!";
                return mstrValidate;
            }
            if (selectCommodityName == "")
            {
                mstrValidate = mstrValidate + "Commodity Name Cannot be Blank !!!";
                return mstrValidate;
            }
            if (HSNCode == "")
            {
                mstrValidate = mstrValidate + "HSN Code Cannot be Blank !!!";
                return mstrValidate;
            }
            if (ValueForLotSize == "")
            {
                mstrValidate = mstrValidate + "Value For Lot Size Cannot be Blank !!!";
                return mstrValidate;
            }
            if (Quantity == "")
            {
                mstrValidate = mstrValidate + "Quantity Cannot be Blank !!!";
                return mstrValidate;
            }
            if (TotalValue == "")
            {
                mstrValidate = mstrValidate + "Total Value Cannot be Blank !!!";
                return mstrValidate;
            }
            if (GST == "")
            {
                mstrValidate = mstrValidate + "GST Cannot be Blank !!!";
                return mstrValidate;
            }
            if (OtherTax == "")
            {
                mstrValidate = mstrValidate + "Other Tax Cannot be Blank !!!";
                return mstrValidate;
            }
            if (Discount == "")
            {
                mstrValidate = mstrValidate + "Discount Cannot be Blank !!!";
                return mstrValidate;
            }
            if (radlRounding == "")
            {
                mstrValidate = mstrValidate + "Rounding Cannot be Blank !!!";
                return mstrValidate;
            }

            return mstrValidate;
        }
        private void SetMessage(bool pBlnIsError, string strMessage)
        {
            actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            actionInfo.InnerHtml = strMessage;
        }
        public void FillInvoiceCreditNoteGrid()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT * from  Com_InvoiceCreditNote where bIsActive=1";
                //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                grdinvcrnote.DataSource = dtGetUserDetails;
                grdinvcrnote.DataBind();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillInvoiceCreditNoteGrid", "Invoice Credit Note", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }
        private void SetProductsUpdateMessage(bool pBlnIsError, string strMessage)
        {
            updateActionDiv.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            updateActionDiv.InnerHtml = strMessage;
        }
        private void ClearAll()
        {
            txtInvoiceNumber.Text = "";
            //dtDate.Value = "0";
            selectclientname.SelectedIndex = 0;
            //txtTypeOfInvoice.Text = "";
            selectcontractname.SelectedIndex = 0;
            selectCommodityName.SelectedIndex = 0;
            txtHSNCode.Text = "";
            txtValueForLotSize.Text = "";
            txtQuantity.Text = "";
            txtTotalValue.Text = "";
            txtGST.Text = "";
            dtDate.Value = "";
            txtOtherTax.Text = "";
            txtDiscount.Text = "";
            radlRounding.ClearSelection();



            //SetMessage(false, "Press Save to store Admin Details");




            //ViewState["ImgLogo"] = "";
            //ViewState["ImgPath"] = "";
            //ViewState["RowVal"] = "";
        }
        private void ClearControls()
        {
            txtInvoiceNumber.Text = "";
            //dtDate.Value = "0";
            selectclientname.SelectedIndex = 0;
            //txtTypeOfInvoice.Text = "";
            selectcontractname.SelectedIndex = 0;
            selectCommodityName.SelectedIndex = 0;
            txtHSNCode.Text = "";
            txtValueForLotSize.Text = "";
            txtQuantity.Text = "";
            txtTotalValue.Text = "";
            txtGST.Text = "";
            txtOtherTax.Text = "";
            txtDiscount.Text = "";
            //radlRounding.SelectedItem.Value = "0";
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdinvcrnote.EditIndex = e.NewEditIndex;
            ViewState["RowVal"] = e.NewEditIndex;
            FillInvoiceCreditNoteGrid();
            //grdAdmin.Columns[5].Visible = true;
        }
        private void FillDrdId()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT iClientId,sCompany FROM Com_Client  WHERE bIsActive=1";
                // SqlHelper.AddInParam("@ID", SqlDbType.VarChar, strId));
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                selectclientname.DataSource = dtGetUserDetails;
                selectclientname.DataTextField = "sCompany";
                selectclientname.DataValueField = "iClientId";
                selectclientname.DataBind();
                selectclientname.Items.Insert(0, new ListItem("-- Select Client Name --", "0"));
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillDrdId", "FillDrdId", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
            }
        }
        private void getcommodityname()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT iComID,sCommodityName FROM Com_Commodity  WHERE bIsActive=1";
                // SqlHelper.AddInParam("@ID", SqlDbType.VarChar, strId));
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                selectCommodityName.DataSource = dtGetUserDetails;
                selectCommodityName.DataTextField = "sCommodityName";
                selectCommodityName.DataValueField = "iComID";
                selectCommodityName.DataBind();
                selectCommodityName.Items.Insert(0, new ListItem("-- Select Commodity Name --", "0"));
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillDrdId", "FillDrdId", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
            }
        }
        private void getcontractname()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT iContractId,sSeller FROM Com_Contract  WHERE bIsActive=1";
                // SqlHelper.AddInParam("@ID", SqlDbType.VarChar, strId));
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                selectcontractname.DataSource = dtGetUserDetails;
                selectcontractname.DataTextField = "sSeller";
                selectcontractname.DataValueField = "iContractId";
                selectcontractname.DataBind();
                selectcontractname.Items.Insert(0, new ListItem("-- Select Contract Name --", "0"));
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillDrdId", "FillDrdId", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
            }
        }
        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label invoiceno = (Label)grdinvcrnote.Rows[e.RowIndex].FindControl("edtinvoiceno");
                TextBox invdate = (TextBox)grdinvcrnote.Rows[e.RowIndex].FindControl("edtInvDate");
                Label clntname = (Label)grdinvcrnote.Rows[e.RowIndex].FindControl("edtclientname");
                TextBox invtype = (TextBox)grdinvcrnote.Rows[e.RowIndex].FindControl("edttypeofinvoice");
                Label cntrname = (Label)grdinvcrnote.Rows[e.RowIndex].FindControl("edtcontractname");
                Label cmdtyname = (Label)grdinvcrnote.Rows[e.RowIndex].FindControl("edtcommodityname");
                TextBox hsn = (TextBox)grdinvcrnote.Rows[e.RowIndex].FindControl("edthsncode");
                TextBox lotsize = (TextBox)grdinvcrnote.Rows[e.RowIndex].FindControl("edtvalueforlotsize");
                 TextBox quantity = (TextBox)grdinvcrnote.Rows[e.RowIndex].FindControl("edtqty");
                TextBox ttlvalue = (TextBox)grdinvcrnote.Rows[e.RowIndex].FindControl("edttotalvalue");
                TextBox gst = (TextBox)grdinvcrnote.Rows[e.RowIndex].FindControl("edtgst");
                TextBox othrtax = (TextBox)grdinvcrnote.Rows[e.RowIndex].FindControl("edtothertax");
                TextBox discount = (TextBox)grdinvcrnote.Rows[e.RowIndex].FindControl("edtdiscount");
                RadioButtonList rounding = (RadioButtonList)grdinvcrnote.Rows[e.RowIndex].FindControl("edtrounding");

                Label lblID = (Label)grdinvcrnote.Rows[e.RowIndex].FindControl("InvID");
                // HiddenField CatLogo = (HiddenField)grdAdmin.Rows[e.RowIndex].FindControl("LogoName");
                //HiddenField ImgOriginalCategory = (HiddenField)grdAdmin.Rows[e.RowIndex].FindControl("imgOriginalPath"); //Added by SSK to delete old image

                string strErrorImg = "";
                //String strError = ValidateProductData(txtCatName.Text, Convert.ToString(ViewState["ImgPath"]), Convert.ToString(ViewState["ImgLogo"]), txtRegCatName.Text, Convert.ToInt64(lblCatID.Text));

                //if (Convert.ToString(ViewState["ImgPath"]) != "")
                //{
                //    strErrorImg = CopyFileSafely(txtImgPathMain.Value, Convert.ToString(ViewState["ImgPath"]));
                //}
                //else if (Convert.ToString(ViewState["ImgLogo"]) != "")
                //{
                //    strErrorImg = (Convert.ToString(ViewState["ImgLogo"]));
                //}
                //else
                //{
                //    strErrorImg = "No Change";
                //    ViewState["ImgPath"] = ImgOriginalCategory.Value;
                //    ViewState["ImgLogo"] = CatLogo.Value;
                //}

                //if (Convert.ToString(ViewState["ImgPath"]) == "")
                //{
                //    // ViewState["ImgPath"] = GlobalVariables.NoImagePath;
                //}

                ////Code for deletion of existing image file.
                //if (strErrorImg.Trim() != "No Change")
                //{
                //    if (File.Exists(Server.MapPath(ImgOriginalCategory.Value)))
                //        File.Delete(Server.MapPath(ImgOriginalCategory.Value));
                //}

                //if (strError == "" && strErrorImg != "")
                //{
                 string strValidate = "";
                 strValidate = Validateinvoicecreditnote(invoiceno.Text, invdate.Text, clntname.Text,
                        //invtype.Text,
                        cntrname.Text, cmdtyname.Text,
                        hsn.Text, lotsize.Text, quantity.Text, ttlvalue.Text,
                        gst.Text, othrtax.Text, discount.Text, rounding.Text);
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateInvoiceCreditNote", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                            //SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                            //AddInParam("@iAdminId",SqlDbType.BigInt, Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId)) 
                            //SqlHelper.AddInParam("@iAdminId", SqlDbType.BigInt,Convert.ToInt64(((SysCompany)Session[""]) ), 
                             SqlHelper.AddInParam("@sInvoiceNo", SqlDbType.Int, invoiceno.Text),
                                SqlHelper.AddInParam("@dDate", SqlDbType.Date, invdate.Text),
                                SqlHelper.AddInParam("@sClientName", SqlDbType.VarChar, clntname.Text),
                                SqlHelper.AddInParam("@sTypeOfInvoice", SqlDbType.VarChar, invtype.Text),
                                SqlHelper.AddInParam("@sContractName", SqlDbType.VarChar, cntrname.Text),
                                SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, cmdtyname.Text),
                                SqlHelper.AddInParam("@sHsnCode", SqlDbType.VarChar, hsn.Text),
                                SqlHelper.AddInParam("@iValueLotSize", SqlDbType.Int, lotsize.Text),
                                SqlHelper.AddInParam("@iQty", SqlDbType.Int, quantity.Text),
                                SqlHelper.AddInParam("@nTotalValue", SqlDbType.Decimal, ttlvalue.Text),
                                SqlHelper.AddInParam("@nGst", SqlDbType.Decimal, gst.Text),
                                SqlHelper.AddInParam("@nOtherTax", SqlDbType.Decimal, othrtax.Text),
                                SqlHelper.AddInParam("@nDiscount", SqlDbType.Decimal, discount.Text),
                                SqlHelper.AddInParam("@bIsRoundOff", SqlDbType.Bit, Convert.ToBoolean(rounding.SelectedItem.Value)),

                                 SqlHelper.AddInParam("@iInvoiceId", SqlDbType.Int, lblID.Text));

                        SetProductsUpdateMessage(false, "Invoice Credit Note Updated Successfully");
                        grdinvcrnote.EditIndex = -1;
                        FillInvoiceCreditNoteGrid();
                        ClearAll();
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
            grdinvcrnote.EditIndex = -1;
            FillInvoiceCreditNoteGrid();
            ClearAll();
            //if (txtImgPathMain.Value != "")
            //{
            //    bool blnFlagDelete = DeleteFile(txtImgPathMain.Value);
            //}
        }
        protected void btnDeleteInvoiceCreditNt_ServerClick(object sender, EventArgs e)
        {
            // string strquery = "Update User_Master set UM_bItIsActive = 0 where UM_bIntId=@id";
            // SqlHelper.UpdateDatabase(strquery, SqlHelper.AddInParam("@id", SqlDbType.VarChar, txtDelHidden.Value.Trim()));
            DataTable dtCatData = SqlHelper.ReadTable("spDeleteInvoiceCreditNote", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
         SqlHelper.AddInParam("@id", SqlDbType.Int, Convert.ToInt32(txtDelHidden.Value)));
            SetMessage(false, "Invoice Credit Note Deleted Successfully");
            txtDelHidden.Value = "";
            txtDelinvcrnoteID.Text = "";
            txtDelinvcrnoteName.Text = "";
            grdinvcrnote.EditIndex = -1;
            FillInvoiceCreditNoteGrid();
        }
    }
}