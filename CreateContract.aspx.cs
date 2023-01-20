using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_CommTrex
{
    public partial class CreateContract : System.Web.UI.Page
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
                    FillAdminGrid();
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
                    mstrGetUser = "SELECT * from Com_Contract where iContractId= '" + HidBnkId.Value + "'";
                    //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                    DataTable dtGetUserDetails = new DataTable();
                    dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);

                    dtDate.Value = Convert.ToDateTime(dtGetUserDetails.Rows[0]["dDate"]).ToString("yyyy-MM-dd");
                    txtSeller.Text = dtGetUserDetails.Rows[0]["sSeller"].ToString();
                    txtCommodity.Text = dtGetUserDetails.Rows[0]["sCommodity"].ToString();
                    txtContractMode.Text = dtGetUserDetails.Rows[0]["sContractMode"].ToString();
                    txtBroker.Text = dtGetUserDetails.Rows[0]["sBroker"].ToString();
                    drpSelect.SelectedItem.Text = dtGetUserDetails.Rows[0]["sSelect"].ToString();
                    ddlContCondition.SelectedItem.Text = dtGetUserDetails.Rows[0]["sContractCondition"].ToString();
                    txtQuantity.Text = dtGetUserDetails.Rows[0]["iQty"].ToString();
                    ddlContMode.SelectedItem.Text = dtGetUserDetails.Rows[0]["sContarctMode1"].ToString();
                    txtTotalRate.Text = dtGetUserDetails.Rows[0]["nTotalRate"].ToString();
                    ddlDeliveryCondition.SelectedItem.Text = dtGetUserDetails.Rows[0]["sDeliveryLocation"].ToString();
                    ddlDeliveryLocation.SelectedItem.Text = dtGetUserDetails.Rows[0]["sDeliveryLocation"].ToString();
                    txtPacking.Text = dtGetUserDetails.Rows[0]["sPacking"].ToString();
                    txtBrokerage.Text = dtGetUserDetails.Rows[0]["sBrokerage"].ToString();
                    txtPaymentDays.Text = dtGetUserDetails.Rows[0]["sPaymentDays"].ToString();
                    txtAgainCIS.Text = dtGetUserDetails.Rows[0]["sAgainCIS"].ToString();
                    ddlPayCondition.SelectedItem.Text = dtGetUserDetails.Rows[0]["sPaymentCondition"].ToString();
                    txtDiscKepat.Text = dtGetUserDetails.Rows[0]["nDiscount"].ToString();
                    txtContractTerms.Text = dtGetUserDetails.Rows[0]["sContractTerms"].ToString();
                    txtInternalComment.Text = dtGetUserDetails.Rows[0]["sInternalComment"].ToString();


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
            //dtDate.Enabled = pBoolState;
            txtCommodity.Enabled = pBoolState;
            txtContractMode.Enabled = pBoolState;
            txtBroker.Enabled = pBoolState;
            drpSelect.Enabled = pBoolState;
            ddlContCondition.Enabled = pBoolState;
            txtQuantity.Enabled = pBoolState;
            ddlContMode.Enabled = pBoolState;
            txtTotalRate.Enabled = pBoolState;
            //dtExpiry.Enabled = pBoolState;
            ddlDeliveryLocation.Enabled = pBoolState;
            ddlDeliveryCondition.Enabled = pBoolState;
            txtPacking.Enabled = pBoolState;
            txtBrokerage.Enabled = pBoolState;
            txtPaymentDays.Enabled = pBoolState;
            txtAgainCIS.Enabled = pBoolState;
            //dtExpiry.Enabled = pBoolState;
            ddlPayCondition.Enabled = pBoolState;
            txtDiscKepat.Enabled = pBoolState;
            txtContractTerms.Enabled = pBoolState;
            txtInternalComment.Enabled = pBoolState;


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
                    strValidate = ValidateUser(dtDate.Value, txtSeller.Text,
                        txtCommodity.Text, txtContractMode.Text, txtBroker.Text,
                        drpSelect.SelectedItem.Value, ddlContCondition.SelectedItem.Value, txtQuantity.Text, ddlContMode.Text,
                        txtTotalRate.Text, ddlDeliveryLocation.Text,
                        ddlDeliveryCondition.Text, txtPacking.Text, txtBrokerage.Text, 
                        txtPaymentDays.Text, txtAgainCIS.Text, ddlPayCondition.Text, txtDiscKepat.Text,
                        txtContractTerms.Text, txtInternalComment.Text);
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateContract", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                         SqlHelper.AddInParam("@dDate", SqlDbType.VarChar, dtDate.Value),
                         SqlHelper.AddInParam("@sSeller", SqlDbType.VarChar, txtSeller.Text),
                         SqlHelper.AddInParam("@sCommodity", SqlDbType.VarChar, txtCommodity.Text),
                         SqlHelper.AddInParam("@sContractMode", SqlDbType.VarChar, txtContractMode.Text),
                         SqlHelper.AddInParam("@sBroker", SqlDbType.VarChar, txtBroker.Text),
                         SqlHelper.AddInParam("@sSelect", SqlDbType.VarChar, drpSelect.SelectedItem.Text),
                         SqlHelper.AddInParam("@sContractCondition", SqlDbType.VarChar, ddlContCondition.SelectedItem.Text),
                         SqlHelper.AddInParam("@iQty", SqlDbType.Int, txtQuantity.Text),
                         SqlHelper.AddInParam("@sContarctMode1", SqlDbType.VarChar, ddlContMode.SelectedItem.Text),
                          SqlHelper.AddInParam("@nTotalRate", SqlDbType.Decimal, txtTotalRate.Text),
                          //SqlHelper.AddInParam("@nTotalRate", SqlDbType.VarChar, dtExpiry.Value),
                         SqlHelper.AddInParam("@sDeliveryLocation", SqlDbType.VarChar, ddlDeliveryLocation.SelectedItem.Text),
                         SqlHelper.AddInParam("@sDeliveryCondition", SqlDbType.VarChar, ddlDeliveryCondition.SelectedItem.Text),
                         SqlHelper.AddInParam("@sPacking", SqlDbType.VarChar, txtPacking.Text),
                         SqlHelper.AddInParam("@sBrokerage", SqlDbType.VarChar, txtBrokerage.Text),
                         SqlHelper.AddInParam("@sPaymentDays", SqlDbType.VarChar, txtPaymentDays.Text),
                         SqlHelper.AddInParam("@sAgainCIS", SqlDbType.VarChar, txtAgainCIS.Text),
                         SqlHelper.AddInParam("@sPaymentCondition", SqlDbType.VarChar, ddlPayCondition.SelectedItem.Text),
                         SqlHelper.AddInParam("@nDiscount", SqlDbType.Decimal, txtDiscKepat.Text),
                          SqlHelper.AddInParam("@sContractTerms", SqlDbType.VarChar, txtContractTerms.Text),
                         SqlHelper.AddInParam("@sInternalComment", SqlDbType.VarChar, txtInternalComment.Text));
                        FillAdminGrid();
                        SetMessage(false, "Admin Saved Successfully");
                        LockControls(false);
                        btnSave.Attributes["btn-action"] = "New";
                        //btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> New";
                        SetMessage(false, "Admin Added Succesfully!!");
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
                        strValidate = ValidateUser(dtDate.Value, txtSeller.Text,
                        txtCommodity.Text, txtContractMode.Text, txtBroker.Text,
                        drpSelect.SelectedItem.Value, ddlContCondition.SelectedItem.Value, txtQuantity.Text, ddlContMode.Text,
                        txtTotalRate.Text, ddlDeliveryLocation.Text,
                        ddlDeliveryCondition.Text, txtPacking.Text, txtBrokerage.Text,
                        txtPaymentDays.Text, txtAgainCIS.Text, ddlPayCondition.Text, txtDiscKepat.Text,
                        txtContractTerms.Text, txtInternalComment.Text);
                        if (strValidate == "")
                        {

                            DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateContract", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                   SqlHelper.AddInParam("@dDate", SqlDbType.Date, dtDate.Value),
                                   SqlHelper.AddInParam("@sSeller", SqlDbType.VarChar, txtSeller.Text),
                                   SqlHelper.AddInParam("@sCommodity", SqlDbType.VarChar, txtCommodity.Text),
                                   SqlHelper.AddInParam("@sContractMode", SqlDbType.VarChar, txtContractMode.Text),
                                   SqlHelper.AddInParam("@sBroker", SqlDbType.VarChar, txtBroker.Text),
                                   SqlHelper.AddInParam("@sSelect", SqlDbType.VarChar, drpSelect.SelectedItem.Text),
                                   SqlHelper.AddInParam("@sContractCondition", SqlDbType.VarChar, ddlContCondition.SelectedItem.Text),
                                   SqlHelper.AddInParam("@iQty", SqlDbType.Int, Convert.ToInt32(txtQuantity.Text)),
                                   SqlHelper.AddInParam("@sContarctMode1", SqlDbType.VarChar, ddlContMode.SelectedItem.Text),
                                  SqlHelper.AddInParam("@nTotalRate", SqlDbType.Decimal, txtTotalRate.Text),
                                   SqlHelper.AddInParam("@sDeliveryLocation", SqlDbType.VarChar, ddlDeliveryLocation.SelectedItem.Text),
                                   SqlHelper.AddInParam("@sDeliveryCondition", SqlDbType.VarChar, ddlDeliveryCondition.SelectedItem.Text),
                                   SqlHelper.AddInParam("@sPacking", SqlDbType.VarChar, txtPacking.Text),
                                   SqlHelper.AddInParam("@sBrokerage", SqlDbType.VarChar, txtBrokerage.Text),
                                   SqlHelper.AddInParam("@sPaymentDays", SqlDbType.VarChar, txtPaymentDays.Text),
                                   SqlHelper.AddInParam("@sAgainCIS", SqlDbType.VarChar, txtAgainCIS.Text),
                                   SqlHelper.AddInParam("@sPaymentCondition", SqlDbType.VarChar, ddlPayCondition.SelectedItem.Text),
                                   SqlHelper.AddInParam("@nDiscount", SqlDbType.Decimal, txtDiscKepat.Text),
                                   SqlHelper.AddInParam("@sContractTerms", SqlDbType.VarChar, txtContractTerms.Text),
                                  SqlHelper.AddInParam("@sInternalComment", SqlDbType.VarChar, txtInternalComment.Text),
                                    SqlHelper.AddInParam("@iContractId", SqlDbType.Int, HidBnkId.Value));



                            SetProductsUpdateMessage(false, "Contract Master Updated Successfully");

                            grdUser.EditIndex = -1;
                            FillAdminGrid();
                            SetMessage(false, "Contract Updated Succesfully!!");
                            btnSave.InnerHtml = "Save";
                            btnClear.InnerHtml = "Clear";
                            //drpSelect.Items.Clear();
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
                    //btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> Save";
                    SetMessage(false, "Press Save To Add Contract!!");
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

        private string ValidateUser(string dtDate,string Seller,
                       string txtCommodity,string txtContractMode,string txtBroker,
                       string drpSelect ,string ddlContCondition,string txtQuantity,string ddlContMode,
                       string txtTotalRate,string ddlDeliveryLocation,
                       string ddlDeliveryCondition,string txtPacking,string txtBrokerage, 
                        string txtPaymentDays,string txtAgainCIS,string ddlPayCondition,string txtDiscKepat,
                        string txtContractTerms,string txtInternalComment)
        {
            string mstrValidate = "";
            //if (strType == "0")
            //{
            //    mstrValidate = mstrValidate + " Type Cannot be Blank !!!";
            //} 
            if (dtDate == "")
            {

                mstrValidate = mstrValidate + "Contact  Cannot be Blank !!!";
                return mstrValidate;
            }

            if (Seller == "")
            {
                mstrValidate = mstrValidate + "Seller Name Cannot be Blank !!!";
                return mstrValidate;
            }
            if (txtCommodity == "")
            {
                mstrValidate = mstrValidate + "COmodity Name Cannot be Blank !!!";
                return mstrValidate;
            }
            if (txtContractMode == "")
            {
                mstrValidate = mstrValidate + "Contract Mode Cannot be Blank !!!";
                return mstrValidate;
            }
            if (txtBroker == "")
            {
                mstrValidate = mstrValidate + "Broker Cannot be Blank !!!";
                return mstrValidate;
            }
         
            if (drpSelect == "")
            {
                mstrValidate = mstrValidate + "DrpSelect Cannot be Blank !!!";
                return mstrValidate;
            }
            if (ddlContCondition == "")
            {
                mstrValidate = mstrValidate + "ContCondition Cannot be Blank !!!";
                return mstrValidate;
            }
            if (txtQuantity == "")
            {
                mstrValidate = mstrValidate + "Quantity areas Cannot be Blank !!!";
                return mstrValidate;
            }
            if (ddlContMode == "")
            {
                mstrValidate = mstrValidate + "ContMode Cannot be Blank !!!";
                return mstrValidate;
            }
            if (txtTotalRate == "")
            {
                mstrValidate = mstrValidate + "Total Rate Cannot be Blank !!!";
                return mstrValidate;
            }
            //if (dtExpiry == "")
            //{
            //    mstrValidate = mstrValidate + "Expiry area Cannot be Blank !!!";
            //}
            if (ddlDeliveryLocation == "")
            {
                mstrValidate = mstrValidate + "DeliveryLocation Cannot be Blank !!!";
                return mstrValidate;
            } 
            if (ddlDeliveryCondition == "")
            {
                mstrValidate = mstrValidate + "DeliveryCondition Cannot be Blank !!!";
                return mstrValidate;
            }

            if (txtPacking == "")
            {
                mstrValidate = mstrValidate + "Packing areas Cannot be Blank !!!";
                return mstrValidate;
            }
            if (txtBrokerage == "")
            {
                mstrValidate = mstrValidate + "Brokerage areas Cannot be Blank !!!";
                return mstrValidate;
            }
            if (txtPaymentDays == "")
            {
                mstrValidate = mstrValidate + "PaymentDays areas Cannot be Blank !!!";
                return mstrValidate;
            }
            if (txtAgainCIS == "")
            {
                mstrValidate = mstrValidate + "AgainCIS areas Cannot be Blank !!!";
                return mstrValidate;
            }
            if (ddlPayCondition == "")
            {
                mstrValidate = mstrValidate + "PayCondition Cannot be Blank !!!";
                return mstrValidate;
            }
            if (txtDiscKepat == "")
            {
                mstrValidate = mstrValidate + "DiscKepat Cannot be Blank !!!";
                return mstrValidate;
            }
            if (txtContractTerms == "")
            {
                mstrValidate = mstrValidate + "ContractTerms Cannot be Blank !!!";
                return mstrValidate;
            }
            if (txtInternalComment == "")
            {
                mstrValidate = mstrValidate + "InternalComment Cannot be Blank !!!";
                return mstrValidate;
            }



            return mstrValidate;
        }

        protected void btnDeleteClient_ServerClick(object sender, EventArgs e)
        {
            //string strquery = "update Com_Contract set @IsActive=@isavtive where iClientId=@id";
            DataTable dtCatData = SqlHelper.ReadTable("spDeleteContract", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                SqlHelper.AddInParam("@id", SqlDbType.Int, Convert.ToInt32(txtDelHidden.Value)));
            SetProductsUpdateMessage(false, "Contract  Deleted Successfully");
            txtDelHidden.Value = "";
            txtCommodity.Text = "";
            txtContractMode.Text = "";
            txtBroker.Text = "";
            txtQuantity.Text = "";
            txtTotalRate.Text = "";
            txtPaymentDays.Text = "";
            txtPacking.Text = "";
            txtBrokerage.Text = "";
            txtAgainCIS.Text = "";
            txtDiscKepat.Text = "";
            txtContractTerms.Text = "";
            txtInternalComment.Text = "";
            grdUser.EditIndex = -1;
            FillAdminGrid();
        }
        public void FillAdminGrid()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT * from  Com_Contract where bIsActive=1";
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
                pLngErr = GlobalFunctions.ReportError("FillClientGrid", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdUser.EditIndex = e.NewEditIndex;
            ViewState["RowVal"] = e.NewEditIndex;
            FillAdminGrid();
            //grdAdmin.Columns[5].Visible = true;
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox date = (TextBox)grdUser.Rows[e.RowIndex].FindControl("date");
                TextBox seller = (TextBox)grdUser.Rows[e.RowIndex].FindControl("Seller");
                TextBox comodity = (TextBox)grdUser.Rows[e.RowIndex].FindControl("Commodity");
                TextBox ContractMode = (TextBox)grdUser.Rows[e.RowIndex].FindControl("ContractMode");
                TextBox Broker = (TextBox)grdUser.Rows[e.RowIndex].FindControl("Broker");
                string MSelect = (grdUser.Rows[e.RowIndex].FindControl("drpSelect") as DropDownList).SelectedItem.Value;
                string ContractCondition = (grdUser.Rows[e.RowIndex].FindControl("ContCondition") as DropDownList).SelectedItem.Value;
                TextBox qntity = (TextBox)grdUser.Rows[e.RowIndex].FindControl("Qty");
                string ContarctMode1 = (grdUser.Rows[e.RowIndex].FindControl("ContMode") as DropDownList).SelectedItem.Value;
                TextBox TotalRate = (TextBox)grdUser.Rows[e.RowIndex].FindControl("TotalRate");
                string DeliveryLocation = (grdUser.Rows[e.RowIndex].FindControl("DeliveryLoc") as DropDownList).SelectedItem.Value;
                string DeliveryCondition = (grdUser.Rows[e.RowIndex].FindControl("DeliveryCondition") as DropDownList).SelectedItem.Value;
                TextBox Packing = (TextBox)grdUser.Rows[e.RowIndex].FindControl("Packing");
                TextBox Brokerage = (TextBox)grdUser.Rows[e.RowIndex].FindControl("Brokerage");
                TextBox PaymentDays = (TextBox)grdUser.Rows[e.RowIndex].FindControl("PaymentDays");
                TextBox AgainCIS = (TextBox)grdUser.Rows[e.RowIndex].FindControl("AgainCIS");
                string PaymentCondition = (grdUser.Rows[e.RowIndex].FindControl("PayCondition") as DropDownList).SelectedItem.Value;
                TextBox Discount = (TextBox)grdUser.Rows[e.RowIndex].FindControl("Discount");
                TextBox ContractTerms = (TextBox)grdUser.Rows[e.RowIndex].FindControl("ContractTerms");
                TextBox InternalComment = (TextBox)grdUser.Rows[e.RowIndex].FindControl("InternalComment");


                //string ddl_AdminRole = (grdcmdty.Rows[e.RowIndex].FindControl("gadminrole") as DropDownList).SelectedItem.Value;
                //string ddl_Action = (grdcmdty.Rows[e.RowIndex].FindControl("guseraction") as DropDownList).SelectedItem.Value;

                Label lblID = (Label)grdUser.Rows[e.RowIndex].FindControl("ContractID");
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
                strValidate = ValidateUser(date.Text, seller.Text,
                        comodity.Text, ContractMode.Text, Broker.Text,
                        MSelect, ContractCondition, qntity.Text, ContarctMode1,
                        TotalRate.Text, DeliveryLocation,
                        DeliveryCondition, Packing.Text, Brokerage.Text,
                        PaymentDays.Text, AgainCIS.Text, PaymentCondition, Discount.Text,
                        ContractTerms.Text, InternalComment.Text);
                   if (strValidate == "")
                   {

                       DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateContract", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                              SqlHelper.AddInParam("@dDate", SqlDbType.Date, date.Text),
                              SqlHelper.AddInParam("@sSeller", SqlDbType.VarChar, seller.Text),
                              SqlHelper.AddInParam("@sCommodity", SqlDbType.VarChar, comodity.Text),
                              SqlHelper.AddInParam("@sContractMode", SqlDbType.VarChar, ContractMode.Text),
                              SqlHelper.AddInParam("@sBroker", SqlDbType.VarChar, Broker.Text),
                              SqlHelper.AddInParam("@sSelect", SqlDbType.VarChar, MSelect),
                              SqlHelper.AddInParam("@sContractCondition", SqlDbType.VarChar, ContractCondition),
                              SqlHelper.AddInParam("@iQty", SqlDbType.Int, Convert.ToInt32(qntity.Text)),
                              SqlHelper.AddInParam("@sContarctMode1", SqlDbType.VarChar, ContarctMode1),
                             SqlHelper.AddInParam("@nTotalRate", SqlDbType.Decimal, TotalRate.Text),
                              SqlHelper.AddInParam("@sDeliveryLocation", SqlDbType.VarChar, DeliveryLocation),
                              SqlHelper.AddInParam("@sDeliveryCondition", SqlDbType.VarChar, DeliveryCondition),
                              SqlHelper.AddInParam("@sPacking", SqlDbType.VarChar, Packing.Text),
                              SqlHelper.AddInParam("@sBrokerage", SqlDbType.VarChar, Brokerage.Text),
                              SqlHelper.AddInParam("@sPaymentDays", SqlDbType.VarChar, PaymentDays.Text),
                              SqlHelper.AddInParam("@sAgainCIS", SqlDbType.VarChar, AgainCIS.Text),
                              SqlHelper.AddInParam("@sPaymentCondition", SqlDbType.VarChar, PaymentCondition),
                              SqlHelper.AddInParam("@nDiscount", SqlDbType.Decimal, Discount.Text),
                              SqlHelper.AddInParam("@sContractTerms", SqlDbType.VarChar, ContractTerms.Text),
                             SqlHelper.AddInParam("@sInternalComment", SqlDbType.VarChar, InternalComment.Text),
                               SqlHelper.AddInParam("@iContractId", SqlDbType.Int, lblID.Text));



                       SetProductsUpdateMessage(false, "Contract Master Updated Successfully");
                  
                       grdUser.EditIndex = -1;
                       FillAdminGrid();
                       ClearControls();
                   }
                   else
                   {
                       SetProductsUpdateMessage(false, strValidate);
                       //SetMessage(true, strValidate);
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
        //btnClear_ServerClick(object sender)

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

        public void ClearControls()
        {
            //ddl_Type.SelectedIndex = 0;
            dtDate.Value = "";
            ddlPayCondition.SelectedIndex = 0;
            ddlDeliveryLocation.SelectedIndex = 0;
            ddlDeliveryCondition.SelectedIndex = 0;
            ddlContMode.SelectedIndex = 0;
            ddlContCondition.SelectedIndex = 0;
            drpSelect.SelectedIndex = 0;
            txtSeller.Text="";
            txtCommodity.Text="";
            txtContractMode.Text="";
            txtBroker.Text="";
           
            ddlContCondition.SelectedItem.Value="";
            txtQuantity.Text="";
            ddlContMode.SelectedItem.Value = "";
            txtTotalRate.Text="";
            ddlDeliveryLocation.SelectedItem.Value = "";
            ddlDeliveryCondition.SelectedItem.Value = "";
            txtPacking.Text="";
            txtBrokerage.Text="";
            txtPaymentDays.Text="";
            txtAgainCIS.Text="";
            ddlPayCondition.SelectedItem.Value = ""; 
            txtDiscKepat.Text="";
            txtContractTerms.Text="";
            txtInternalComment.Text = "";
        }

    }
}