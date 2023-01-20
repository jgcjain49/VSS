using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.UI.WebControls;

namespace Admin_CommTrex
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string buttonstate = btnSave.InnerText.Replace("\r\n","");
            string button = buttonstate.Replace(" ", "");
            if (!IsPostBack)
            {
                //if (btnSave.Attributes["btn-action"] == "Save") { 
                if (Session["TalukaDetails"] != null)
                {
                    if (Convert.ToString(Session["UserType"]) == "U")
                    {
                        Response.Redirect("Home.aspx");
                    }
                    FillAdminGrid();
                    LockControls(false);
                    FillDrdId();
                    RadioSelectPanel.Visible = false;
                    Browser.Visible = false;
                    ChequePanel.Visible = false;
                    TransactionPanel.Visible = false;
                //}
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
                else {
                    
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
                    mstrGetUser = "SELECT * from Com_Pay where iPayId= '" + HidBnkId.Value + "'";
                    //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                    DataTable dtGetUserDetails = new DataTable();


                    //dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, Convert.ToString(Session["SystemUserSqlConnectionString"]), false,
                    //                SqlHelper.AddInParam("@iPayId", SqlDbType.Int, Convert.ToInt32(HidBnkId.Value)));

                    dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);

                    addClientName.SelectedItem.Text = dtGetUserDetails.Rows[0]["sClientName"].ToString();
                    ddlTypeOfPayment.SelectedItem.Text = dtGetUserDetails.Rows[0]["TypeOfPayment"].ToString();
                    ddlModeOfPayment.SelectedItem.Text = dtGetUserDetails.Rows[0]["sModeOfPayment"].ToString();
                    ChequeBox.Text = dtGetUserDetails.Rows[0]["ChequeNum"].ToString();
                    DocImgPath.Text = dtGetUserDetails.Rows[0]["DocumentImg"].ToString();
                    TransactionNum.Text = dtGetUserDetails.Rows[0]["TransactionNum"].ToString();
                    txtAgainstInvNo.Text = dtGetUserDetails.Rows[0]["sAgainstInvNo"].ToString();
                    txtAmt.Text = dtGetUserDetails.Rows[0]["nAmount"].ToString();
                    btnSave.InnerHtml = "Update";
                    btnClear.InnerHtml = "Cancel";
                    HiddenField3.Value = "o";
                    LockControls(true);
                    SetMessage(false, "Press Update to save changes!");
                    HidEdit.Value = "true";
                    if (ddlModeOfPayment.SelectedItem.Text == "Cheque")
                    {
                        if (ChequeBox.Text == "")
                        {
                            RadioSelectPanel.Visible = true;
                            Browser.Visible = true;
                            DocImgPath.ReadOnly = true;

                        
                        }
                        else if (DocImgPath.Text == "")
                        {
                            ChequePanel.Visible = true;
                        }
                        else { }
                    }



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
            addClientName.Enabled = pBoolState;
            ddlModeOfPayment.Enabled = pBoolState;
            txtAgainstInvNo.Enabled = pBoolState;
            txtAmt.Enabled = pBoolState;
            DocImgPath.Enabled = pBoolState;
            DropDownList1.Enabled = pBoolState; 
            ChequeBox.Enabled = pBoolState;
            TransactionNum.Enabled = pBoolState;
        }

        public void ChangeEventForAmount(object sender, EventArgs e) {

            try{
                if (txtAgainstInvNo.Text != "")
                {
                    string GetAmount = "select nTotalValue from Com_InvoiceCreditNote where sInvoiceNo='" +
                                                    txtAgainstInvNo.Text + "'";
                    string strConn = Convert.ToString(Session["SystemUserSqlConnectionString"]);
                    DataTable TotalFetchAmount = SqlHelper.ReadTable(GetAmount, strConn, false);
                    dllForAmount.DataSource = TotalFetchAmount;
                    //dllForAmount.DataValueField = "iInvoiceId";
                    dllForAmount.DataTextField = "nTotalValue";
                    dllForAmount.DataBind();
                    //DropDownList edtdllCityObj = (DropDownList)e.Row.FindControl("edtdllCity");

                    txtAmt.Text = dllForAmount.Text;
                    if (dllForAmount.Text == "")
                    {

                        SetMessage(false, "Invalid Invoice Number");
                    }
                    else {
                        SetMessage(false, "");
                    }


                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("grdTeam_RowDataBound", "TeamMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }
        public void btnSave_ServerClick(object sender, EventArgs e)
        {
            string buttonstate = btnSave.InnerText.Replace("\r\n", "");
            string button = buttonstate.Replace(" ", "");
            try
            {
                //Button btn = (Button)sender;
                //string strDate = (btn.Attributes["btnSave"]);
                if (button == "Save")
                {
                    string strValidate = ""; 
                    string docCheck = "";
                    strValidate = ValidateUser(addClientName.SelectedItem.Value, 
                        txtAmt.Text, ddlModeOfPayment.SelectedItem.Value,
                        txtAgainstInvNo.Text, DocImgPath.Text, ChequeBox.Text, TransactionNum.Text
                        //ddlTypeOfPayment.Text
                        );
                    docCheck = validateDocCheck(DocImgPath.Text, ChequeBox.Text, TransactionNum.Text);
                    if (strValidate == "")
                    {
                        if (docCheck == "")
                        {
                            string img1 = DocImgPath.Text.Replace("//", "/");
                            DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdatePayment", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                SqlHelper.AddInParam("@sClientName", SqlDbType.VarChar, addClientName.SelectedItem.Text),
                                SqlHelper.AddInParam("@nAmount", SqlDbType.Decimal, txtAmt.Text),
                                SqlHelper.AddInParam("@TypeOfPayment", SqlDbType.VarChar, Convert.ToString(ddlTypeOfPayment.SelectedItem.Text)),
                                SqlHelper.AddInParam("@sModeOfPayment", SqlDbType.VarChar, ddlModeOfPayment.SelectedItem.Text),
                                SqlHelper.AddInParam("@sAgainstInvNo", SqlDbType.VarChar, txtAgainstInvNo.Text),
                                 SqlHelper.AddInParam("@sDocumentImg", SqlDbType.VarChar, img1),
                                SqlHelper.AddInParam("@sChequeNum", SqlDbType.VarChar, ChequeBox.Text),
                                SqlHelper.AddInParam("@TransactionNum", SqlDbType.VarChar, TransactionNum.Text));
                            if (Convert.ToInt32(dtCatData.Rows[0][0].ToString()) > 0)
                            {
                                FillAdminGrid();
                                SetMessage(false, "Payment Saved Successfully");
                                LockControls(false);
                                btnSave.Attributes["btn-action"] = "New";
                                btnSave.InnerHtml = "New";
                                SetMessage(false, "Payment Detail Added Succesfully!!");
                                ClearControls();
                            }
                            else
                            {
                                SetMessage(false, "Error Occured");
                            }
                        }
                        else {
                            SetMessage(true, docCheck);
                        }
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
                        strValidate = ValidateUser(
                            addClientName.SelectedItem.Text,
                              txtAmt.Text,
                              ddlModeOfPayment.SelectedItem.Text,
                              txtAgainstInvNo.Text,
                              DocImgPath.Text,
                              ChequeBox.Text,
                              TransactionNum.Text
                            //typeofpay.SelectedItem.Value
                              );
                        if (strValidate == "")
                        {
                            string data1 = DocImgPath.Text.Replace("//", "/");
                            DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdatePayment", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                   SqlHelper.AddInParam("@sClientName", SqlDbType.VarChar, addClientName.SelectedItem.Text),
                                   SqlHelper.AddInParam("@nAmount", SqlDbType.Decimal, txtAmt.Text),
                                   SqlHelper.AddInParam("@TypeOfPayment", SqlDbType.VarChar, ddlTypeOfPayment.SelectedItem.Text),
                                   SqlHelper.AddInParam("@sModeOfPayment", SqlDbType.VarChar, ddlModeOfPayment.SelectedItem.Text),
                                   SqlHelper.AddInParam("@sAgainstInvNo", SqlDbType.VarChar, txtAgainstInvNo.Text),
                                     SqlHelper.AddInParam("@sDocumentImg", SqlDbType.VarChar, data1),
                                   SqlHelper.AddInParam("@sChequeNum", SqlDbType.VarChar, ChequeBox.Text),
                                   SqlHelper.AddInParam("@TransactionNum", SqlDbType.VarChar, TransactionNum.Text),
                                  SqlHelper.AddInParam("@iPayId", SqlDbType.Int, HidBnkId.Value));


                        

                            SetProductsUpdateMessage(false, "Payment Detail Updated Successfully");
                            grdUser.EditIndex = -1;
                            FillAdminGrid();
                            SetMessage(false, "Admin Updated Succesfully!!");
                            //grdUser.Columns[6].Visible = false;
                            btnSave.InnerHtml = "Save";
                            btnClear.InnerHtml = "Clear";
                            
                            addClientName.Items.Clear();
                            FillDrdId();
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
                    btnSave.InnerHtml = "Save";
                    SetMessage(false, "Press Save To Add payment Details!!");
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

        private void SetMessage(bool pBlnIsError, string strMessage)
        {
            actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            actionInfo.InnerHtml = strMessage;
        }


        private string validateDocCheck(string imgtext,string chequeNumberText, string transactionText)
                 {
           string fielsBlankError="";
           if (ddlModeOfPayment.SelectedValue == "1") 
           {
               if (TransactionNum.Text == "")
               {
                   fielsBlankError = fielsBlankError + "Transaction Number Cannot be blank!!";
                   return fielsBlankError;
               }          
           }
          

           else if (ddlModeOfPayment.SelectedValue == "3")
           {
               if(DropDownList1.SelectedValue=="0"){
               if (DocImgPath.Text == "")
               {
                   fielsBlankError = fielsBlankError + "Image Path  Cannot be blank!!";
                   return fielsBlankError;
               }
               }
               else if (DropDownList1.SelectedValue == "1")
               {
                   if (ChequeBox.Text == "")
                   {
                       fielsBlankError = fielsBlankError + "Cheque Number Cannot be blank!!";
                       return fielsBlankError;
                   }
               }
               else if (DropDownList1.SelectedValue == "") {
                   fielsBlankError = fielsBlankError + "Please select the payment method !!";
                   return fielsBlankError;
               }
           }
           return fielsBlankError;

                           }

        private string ValidateUser(
                      string ClientName, 
                      string Amount, string ddlModeofPayment, string Invoicetxt,string docImg, string chequeNumber,string TransactionNum
            //,string TypeofPay
            )
        {
            string mstrValidate = "";
            //if (strType == "0")
            //{
            //    mstrValidate = mstrValidate + " Type Cannot be Blank !!!";
            //} 
            if (ClientName == "0")
            {
                mstrValidate = mstrValidate + "Client Name Cannot be Blank !!!";
                return mstrValidate;
            }       
            if (Amount == "")
            {
                mstrValidate = mstrValidate + "Amount Cannot be Blank !!!";
                return mstrValidate;
            }
            if (ddlModeofPayment == "0")
            {
                mstrValidate = mstrValidate + "Mode of Payment Cannot be Blank !!!";
                return mstrValidate;
            }

            if (Invoicetxt == "")
            {
                mstrValidate = mstrValidate + "Invoice Field Cannot be Blank !!!";
                return mstrValidate;
            }
            //if (TypeofPay == "") {
            //    mstrValidate = mstrValidate + "Type Of Payment Cannot be Blank!!";
            //}
            {
            
            }
            //if (docImg == "")
            //{
            //    mstrValidate = mstrValidate + "Img Cannot Be blank!!";
            //    return mstrValidate;
            //}
            //if (chequeNumber == "")
            //{
            //    mstrValidate = mstrValidate + "chequeNumber Cannot Be blank!!";
            //    return mstrValidate;
            //}
            //if (TransactionNum == "")
            //{
            //    mstrValidate = mstrValidate + "TransactionNum Cannot Be blank!!";
            //    return mstrValidate;
            //}

            return mstrValidate;
        }

        public void FillAdminGrid()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT * from Com_Pay where bIsActive = 1 ";
                //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                grdUser.DataSource = dtGetUserDetails;
                grdUser.DataBind();
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

        protected void btnDeleteClient_ServerClick(object sender, EventArgs e)
        {
            //string strquery = "update Com_Payment set @IsActive=@isavtive where iPayId=@id";
            //string strquery="spDeletePayment";
            DataTable dtCatData = SqlHelper.ReadTable("spDeletePayment", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                SqlHelper.AddInParam("@id", SqlDbType.Int, Convert.ToInt32(txtDelHidden.Value)));
            SetProductsUpdateMessage(false, "Admin Deleted Successfully");
            FillAdminGrid();
            txtDelHidden.Value = "";
            addClientName.SelectedItem.Value = "0";
            //txtTxnTyp.Text = "";
            txtAmt.Text = "";
            txtAgainstInvNo.Text = "";
            grdUser.EditIndex = -1;
           
        }

        protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdUser.EditIndex = e.NewEditIndex;
            ViewState["RowVal"] = e.NewEditIndex;            
            FillAdminGrid();
            grdUser.Columns[8].Visible = true;
            grdUser.Columns[7].Visible = false;
            //grdAdmin.Columns[5].Visible = true;
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label Client_name = (Label)grdUser.Rows[e.RowIndex].FindControl("ClientName");
                //string dllTran_type = (grdUser.Rows[e.RowIndex].FindControl("TransctionType") as DropDownList).SelectedItem.Value;
                //TextBox trans_type = (TextBox)grdUser.Rows[e.RowIndex].FindControl("TransctionType1");
                TextBox Amount = (TextBox)grdUser.Rows[e.RowIndex].FindControl("Amount");
                string Payment_mode = (grdUser.Rows[e.RowIndex].FindControl("ModeOfPayment") as DropDownList).SelectedItem.Text;
                string TypeOf_Payment = (grdUser.Rows[e.RowIndex].FindControl("TypeOfPayment") as DropDownList).SelectedItem.Text;
              TextBox   Invoice = (TextBox)grdUser.Rows[e.RowIndex].FindControl("AgainstInvNo");
              TextBox DocImg = (TextBox)grdUser.Rows[e.RowIndex].FindControl("DocumentImg");
              TextBox Chequenumber = (TextBox)grdUser.Rows[e.RowIndex].FindControl("Chequegb");
              TextBox TransactionNumber = (TextBox)grdUser.Rows[e.RowIndex].FindControl("TransactionGb");
                Label lblID = (Label)grdUser.Rows[e.RowIndex].FindControl("paymentId");


                // HiddenField CatLogo = (HiddenField)grdAdmin.Rows[e.RowIndex].FindControl("LogoName");
                HiddenField ImgOriginalCategory = (HiddenField)grdUser.Rows[e.RowIndex].FindControl("txtImgPathMain"); //Added by SSK to delete old image

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
                  strValidate = ValidateUser(
                      Client_name.Text, 
                        Amount.Text,
                        Payment_mode,
                        Invoice.Text,
                        GlobalVariables.updateImage1,
                        ChequeBox.Text,
                        TransactionNum.Text
                        //typeofpay.SelectedItem.Value
                        );
                    if (strValidate == "")
                    {
                        string data1 = ImgOriginalCategory.Value.Replace("//", "/");
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdatePayment", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                               SqlHelper.AddInParam("@sClientName", SqlDbType.VarChar, Client_name.Text),
                               //SqlHelper.AddInParam("@sTransctionType", SqlDbType.VarChar, dllTran_type),
                               //SqlHelper.AddInParam("@sTransctionType1", SqlDbType.VarChar, trans_type.Text),
                               //SqlHelper.AddInParam("@TypeOfPayment"), SqlDbType.VarChar, edittypePayment.Text,
                               SqlHelper.AddInParam("@nAmount", SqlDbType.Decimal, Amount.Text),
                               SqlHelper.AddInParam("@sModeOfPayment", SqlDbType.VarChar, Payment_mode),
                               SqlHelper.AddInParam("@sAgainstInvNo", SqlDbType.VarChar, Invoice.Text),
                                 SqlHelper.AddInParam("@sDocumentImg", SqlDbType.VarChar, data1),
                               SqlHelper.AddInParam("@sChequeNum", SqlDbType.VarChar, ChequeBox.Text),
                               SqlHelper.AddInParam("@TransactionNum", SqlDbType.VarChar, TransactionNum.Text),
                              SqlHelper.AddInParam("@iPayId", SqlDbType.Int, lblID.Text));




                        SetProductsUpdateMessage(false, "Payment Updated Successfully");
                        grdUser.EditIndex = -1;
                        FillAdminGrid();
                        //grdUser.Columns[6].Visible = false;
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
            grdUser.EditIndex = -1;
            FillAdminGrid();
            grdUser.Columns[8].Visible = false;
            ClearControls();
            //if (txtImgPathMain.Value != "")
            //{
            //    bool blnFlagDelete = DeleteFile(txtImgPathMain.Value);
            //}
        }

        private void SetProductsUpdateMessage(bool pBlnIsError, string strMessage)
        {
            updateActionDiv.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            updateActionDiv.InnerHtml = strMessage;
        }

        public void ClearControls()
        {
            //addClientName.SelectedItem.Text, ddlTransactionType.SelectedItem.Text,
            //            txtTxnTyp.Text, txtAmt.Text, ddlModeOfPayment.SelectedItem.Text,
            //            txtAgainstInvNo.Text

            //addClientName.Text = "";
            addClientName.SelectedIndex = 0;
            ddlModeOfPayment.SelectedIndex = 0;
            DocImgPath.Text = "";
            ChequeBox.Text = "";
            ddlTypeOfPayment.SelectedIndex=0 ;
            TransactionNum.Text = "";
            txtAmt.Text = "";
            //txtTxnTyp.Text = "";
            txtAgainstInvNo.Text = "";
            RadioSelectPanel.Visible = false;
            Browser.Visible = false;
            ChequePanel.Visible = false;
            TransactionPanel.Visible = false;
           
           
        }

        private void FillDrdId()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT iClientId,sContactPerson FROM Com_Client  WHERE bIsActive=1";
                // SqlHelper.AddInParam("@ID", SqlDbType.VarChar, strId));
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                addClientName.DataSource = dtGetUserDetails;
                addClientName.DataTextField = "sContactPerson";
                addClientName.DataValueField = "iClientId";
                addClientName.DataBind();
                addClientName.Items.Insert(0, new ListItem("-- Select Payment Person  Name --", "0"));
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

        protected void btnSaveImgUpload(object sender, EventArgs e)
        {
        
          try
            {
              
                string strError;
                if (FileMainImage1.HasFile)
                {            
                    Stream strm = FileMainImage1.PostedFile.InputStream;
                    System.Drawing.Image img = compressImage(Convert.ToInt16(GlobalVariables.compressedImgQuality), strm);
                    string ChequeDocImg = GlobalFunctions.saveImage(GlobalVariables.UserImgPath, FileMainImage1, img );
                    DocImgPath.Text = ChequeDocImg;
                    txtImgPathMain.Value = ChequeDocImg;
                
                    if (Convert.ToString(ViewState["RowVal"]) == "")
                    {                      
                        DocImgPath.Text = ChequeDocImg;
                            SetMessage(true, "Image Uploaded Successfully!!!");                      
                    }
                    else
                    {
                        //GlobalVariables.updateImage1 = txtImgPathMain.Value;
                           
                              SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");                    
                    }
        }
          }
          catch (Exception exError)
          {
              long pLngErr = -1;
              if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                  pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
              pLngErr = GlobalFunctions.ReportError("btnSaveImgUpload", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
              updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
              updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Administrator";
          }
        }

        public  void paymentMethod_SelectedIndexChanged(object sender, EventArgs e) {
            if (ddlModeOfPayment.SelectedItem.Value == "3")
            {
                RadioSelectPanel.Visible = true;
                Browser.Visible = false;
                ChequePanel.Visible = false;
                TransactionPanel.Visible = false;
             
            }
            else if (ddlModeOfPayment.SelectedItem.Value == "1") {
                RadioSelectPanel.Visible = false;
                Browser.Visible = false;
                ChequePanel.Visible = false;
                TransactionPanel.Visible = true;
            }
            else if (ddlModeOfPayment.SelectedItem.Value == "2")
            {
                TransactionPanel.Visible = false;
                RadioSelectPanel.Visible = false;
                Browser.Visible = false;
                ChequePanel.Visible = false;
            }
            else if (ddlModeOfPayment.SelectedItem.Value == "4")
            { 
            
            }
            else
            {

            }
        }

        protected void drdCommType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedItem.Value == "1")
            {

                ChequePanel.Visible = true;
                Browser.Visible = false;
                
             }
                //Image select
            else if (DropDownList1.SelectedItem.Value == "0") {
                ChequePanel.Visible = false;
                Browser.Visible = true;
               
            }

            else
            {
             
            }
        }


        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in encoders)
                if (ici.MimeType == mimeType) return ici;
            return null;
        }

        private System.Drawing.Image compressImage(int newQuality, Stream strm)
        {
            using (System.Drawing.Image image = System.Drawing.Image.FromStream(strm))
            using (System.Drawing.Image memImage = new Bitmap(image, image.Width, image.Height))
            {
                ImageCodecInfo myImageCodecInfo;
                System.Drawing.Imaging.Encoder myEncoder;
                EncoderParameter myEncoderParameter;
                EncoderParameters myEncoderParameters;
                myImageCodecInfo = GetEncoderInfo("image/jpeg");
                myEncoder = System.Drawing.Imaging.Encoder.Quality;
                myEncoderParameters = new EncoderParameters(1);
                myEncoderParameter = new EncoderParameter(myEncoder, newQuality);
                myEncoderParameters.Param[0] = myEncoderParameter;

                MemoryStream memStream = new MemoryStream();
                memImage.Save(memStream, myImageCodecInfo, myEncoderParameters);
                System.Drawing.Image newImage = System.Drawing.Image.FromStream(memStream);
                ImageAttributes imageAttributes = new ImageAttributes();
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    g.InterpolationMode =
                      System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;  //**
                    g.DrawImage(newImage, new Rectangle(Point.Empty, newImage.Size), 0, 0,
                      newImage.Width, newImage.Height, GraphicsUnit.Pixel, imageAttributes);
                }
                return newImage;
            }
        }


        public void ClearDialougControl()
        {
            ImgCoverImage.ImageUrl = "";

        }

        protected void btnShowGallery_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int introw = Convert.ToInt32(btn.Attributes["RowIndex"]);

                Label lblOrgID = (Label)grdUser.Rows[introw].FindControl("paymentId");

                //long lngOrgID = Convert.ToInt64(lblOrgID.Text);
                //txtModifyImgOrgid.Text = lngOrgID.ToString();

                //DataTable dtGetImages = SqlHelper.ReadTable("spDoveInsertUpdateDistriAttch", true, SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                //                        SqlHelper.AddInParam("@bIntImgId", SqlDbType.BigInt, lngOrgID));

                string query = "SELECT [iPayId]  ,[DocumentImg]" +
                               "FROM [Com_Pay] where iPayId  ='" + lblOrgID.Text + "'";
                DataTable dtGetImages = SqlHelper.ReadTable(query, Convert.ToString(Session["SystemUserSqlConnectionString"]), false);
                //SqlHelper.AddInParam("@ID", SqlDbType.VarChar, lblOrgID.Text));
                ClearDialougControl();

                if (dtGetImages.Rows.Count > 0)
                {
                                    
                        for (int intcount = 1; intcount <= dtGetImages.Rows.Count; intcount++)
                        {
                            DataRow dtRowCat = dtGetImages.Rows[intcount - 1];
                            setImages(Convert.ToInt64(dtRowCat["iPayId"].ToString()), dtRowCat["DocumentImg"].ToString(), intcount);

                            //ImgCoverImage.ImageUrl = dtRowCat["DocumentImg"].ToString();
                        }
                        this.ClientScript.RegisterStartupScript(this.GetType(), "showGalleryModal", "ShowModal()", true);
                  
                }
                else
                {
                    //docArea.InnerHtml = "<b>No Documents found</b>";
                }
            }

            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("InsertUpdateImages", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Administrator";
            }

        }

        protected void setImages(long intImgID, string strAAtchdhrFrnt, int count)
        {
            try
            {
                switch (count)
                {
                    case 1:

                        //txtMainDescription.Text = "MD Pancard";
                        //ImgCoverImage.ImageUrl = "http://wap.goldifyapp.com/admin/" + (strAAtchdhrFrnt.ToLower().Contains(".pdf") ? "AdminExContent/images/pdf_logo.png" : strAAtchdhrFrnt);
                        //enlargeImgLnk1.HRef = "http://wap.goldifyapp.com/admin/" + strAAtchdhrFrnt;
                        //txtImage1Description.Text = "MD Aadhar Detail";     
                        ImgCoverImage.ImageUrl = (strAAtchdhrFrnt.ToLower().Contains(".pdf") ? "AdminExContent/images/pdf_logo.png" : strAAtchdhrFrnt);
                        enlargeImgLnk1.HRef = strAAtchdhrFrnt;
                        //ImgCoverImage.ImageUrl = strAAtchdhrFrnt;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("setImages", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Administrator";

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
            else{}
        }

    }
}