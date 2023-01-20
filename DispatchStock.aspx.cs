using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Admin_CommTrex
{
    public partial class DispatchStock : System.Web.UI.Page
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
                    FillDispatchgrid();
                    FillDrdId();
                    getWarehousename();
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
                    mstrGetUser = "SELECT * from Com_DispatchStock where iDispatchId= '" + HidBnkId.Value + "'";
                    //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                    DataTable dtGetUserDetails = new DataTable();


                    //dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, Convert.ToString(Session["SystemUserSqlConnectionString"]), false,
                    //                SqlHelper.AddInParam("@iPayId", SqlDbType.Int, Convert.ToInt32(HidBnkId.Value)));

                    dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);

                    selectcommname.SelectedItem.Text = (dtGetUserDetails.Rows[0]["sCommodityName"]).ToString();
                    //drpdwncommoditytype.SelectedItem.Text  = dtGetUserDetails.Rows[0]["sCommodityType"].ToString();
                    ddlCommodityType.SelectedItem.Text = dtGetUserDetails.Rows[0]["sCommodityType"].ToString();
                    dtDispDate.Value = Convert.ToDateTime(dtGetUserDetails.Rows[0]["dDispatchDate"]).ToString("yyyy-MM-dd");
                    txtBuyCompany.Text = dtGetUserDetails.Rows[0]["sSeller"].ToString();
                    txtQuantity.Text = dtGetUserDetails.Rows[0]["iQty"].ToString();
                    ddlWareName.SelectedItem.Text = dtGetUserDetails.Rows[0]["sWarehouseName"].ToString();
                    txtWareLocation.Text = dtGetUserDetails.Rows[0]["sWarhouseLocation"].ToString();
                    txtCDDNo.Text = dtGetUserDetails.Rows[0]["sCDDNo"].ToString();
                    txtLotId.Text = dtGetUserDetails.Rows[0]["sCMSELotId"].ToString();
                    txtDiscount.Text = dtGetUserDetails.Rows[0]["nDiscount"].ToString();
                    //txtDiscount.Text = dtGetUserDetails.Rows[0]["nAmount"].ToString();
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
            selectcommname.Enabled = pBoolState;
            ddlCommodityType.Enabled = pBoolState;
            //dtDispDate.Enabled = pBoolState;
            txtBuyCompany.Enabled = pBoolState;
            txtQuantity.Enabled = pBoolState;
            ddlWareName.Enabled = pBoolState;
            txtWareLocation.Enabled = pBoolState;
            txtCDDNo.Enabled = pBoolState;
            txtLotId.Enabled = pBoolState;
            txtDiscount.Enabled = pBoolState;
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
                    //txtCommodityName.Text,
                    strValidate = ValidateUser(selectcommname.SelectedItem.Text, ddlCommodityType.SelectedItem.Text,
                        dtDispDate.Value, txtBuyCompany.Text, txtQuantity.Text,
                        ddlWareName.SelectedItem.Text, txtWareLocation.Text, txtCDDNo.Text, txtLotId.Text, txtDiscount.Text);
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateDispatchstock", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                        //string strquery = "Insert into User_Master(UM_CompId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation,UM_vCharPassword,UM_bItIsActive) values (@comp_id,@u_name,@u_ID,@u_desg," + GlobalFunctions.CreateEncryptTextSyntax("@u_pass", false, false) + ",1)";
                        //SqlHelper.UpdateDatabase(strquery, SqlHelper.AddInParam("@comp_id", SqlDbType.BigInt, Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId)),
                        //SqlHelper.AddInParam("@u_ID", SqlDbType.VarChar, txtUserName.Text),
                       SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, selectcommname.SelectedItem.Text),
                       //SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, selectcommname.SelectedItem.Text),
                        //SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, txtCommodityName.Text),
                        SqlHelper.AddInParam("@sCommodityType", SqlDbType.VarChar, (ddlCommodityType.SelectedItem.Text)),
                        SqlHelper.AddInParam("@dDispatchDate", SqlDbType.VarChar, dtDispDate.Value),
                        SqlHelper.AddInParam("@sSeller", SqlDbType.VarChar, txtBuyCompany.Text),
                        SqlHelper.AddInParam("@iQty", SqlDbType.Int, txtQuantity.Text),
                        SqlHelper.AddInParam("@sWarehouseName", SqlDbType.VarChar, ddlWareName.SelectedItem.Text),
                        SqlHelper.AddInParam("@sWarhouseLocation", SqlDbType.VarChar, txtWareLocation.Text),
                        SqlHelper.AddInParam("@sCDDNo", SqlDbType.VarChar, txtCDDNo.Text),
                        SqlHelper.AddInParam("@sCMSELotId", SqlDbType.VarChar, txtLotId.Text),
                        SqlHelper.AddInParam("@nDiscount", SqlDbType.Decimal, txtDiscount.Text));
                        FillDispatchgrid();
                        //SetMessage(false, "Dispatch Stock Saved Successfully");
                        LockControls(false);
                        btnSave.InnerHtml = "New";
                        //btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> New";
                        SetMessage(false, "Dispatch Stock Added Succesfully!!");
                        ClearControls();
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
                        strValidate = ValidateUser(selectcommname.SelectedItem.Text , ddlCommodityType.SelectedItem.Value,
                        dtDispDate.Value, txtBuyCompany.Text, txtQuantity.Text,
                        ddlWareName.SelectedItem.Value, txtWareLocation.Text, txtCDDNo.Text, txtLotId.Text, txtDiscount.Text);
                        if (strValidate == "")
                        {
                            DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateDispatchstock", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                //SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                //AddInParam("@iAdminId",SqlDbType.BigInt, Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId)) 
                                //SqlHelper.AddInParam("@iAdminId", SqlDbType.BigInt,Convert.ToInt64(((SysCompany)Session[""]) ),
                                    SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, selectcommname.SelectedItem.Text),
                                    SqlHelper.AddInParam("@sCommodityType", SqlDbType.VarChar, ddlCommodityType.SelectedItem.Text),
                                    SqlHelper.AddInParam("@dDispatchDate", SqlDbType.Date, dtDispDate.Value),
                                    SqlHelper.AddInParam("@sSeller", SqlDbType.VarChar, txtBuyCompany.Text),
                                    SqlHelper.AddInParam("@iQty", SqlDbType.Int, txtQuantity.Text),
                                    SqlHelper.AddInParam("@sWarehouseName", SqlDbType.VarChar, ddlWareName.SelectedItem.Text),
                                    SqlHelper.AddInParam("@sWarhouseLocation", SqlDbType.VarChar, txtWareLocation.Text),
                                    SqlHelper.AddInParam("@sCDDNo", SqlDbType.VarChar, txtCDDNo.Text),
                                    SqlHelper.AddInParam("@sCMSELotId", SqlDbType.VarChar, txtLotId.Text),
                                    SqlHelper.AddInParam("@nDiscount", SqlDbType.Decimal, txtDiscount.Text),
                                 SqlHelper.AddInParam("@iDispatchId", SqlDbType.Int, HidBnkId.Value));

                            SetProductsUpdateMessage(false, "Dispatch Stock Updated Successfully");
                            grddispatch.EditIndex = -1;
                            FillDispatchgrid();
                            SetMessage(false, "Dispatch Updated Succesfully!!");
                            btnSave.InnerHtml = "Save";
                            btnClear.InnerHtml = "Clear";
                            selectcommname.Items.Clear();
                            ddlWareName.Items.Clear();
                            FillDrdId();
                            getWarehousename();
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
                    //ClearControls();
                    LockControls(true);
                    btnSave.Attributes["btn-action"] = "Save";
                    btnSave.InnerHtml = "Save";
                    //btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> Save";
                    SetMessage(false, "Press Save To Add Dispatch Stock Details!!");
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

        private void SetMessage(bool pBlnIsError, string strMessage)
        {
            actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            actionInfo.InnerHtml = strMessage;
        }

        private void SetProductsUpdateMessage(bool pBlnIsError, string strMessage)
        {
            updateActionDiv.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            updateActionDiv.InnerHtml = strMessage;
        }

        private string ValidateUser(string selectcommname, string Commoditytype, string Dispatchdate,
           string buyercompany, string quantity, string warename, string warelocation, string cddno, string cmselotid, 
            string discount)
        {
            string mstrValidate = "";
            if (selectcommname == "0") 
            {
                mstrValidate += "- Please Select Commodity Name" + Environment.NewLine;
                return (mstrValidate);
            }
            if (Commoditytype == "")
            {
                mstrValidate = mstrValidate + "Commodity Type Cannot be Blank !!!";
                return mstrValidate;
            }
            if (Dispatchdate == "")
            {
                mstrValidate = mstrValidate + "Dispatch Date Cannot be Blank !!!";
                return mstrValidate;
            }
            if (buyercompany == "")
            {
                mstrValidate = mstrValidate + "Buyer Company Cannot be Blank !!!";
                return mstrValidate;
            }
            if (quantity == "")
            {
                mstrValidate = mstrValidate + "Quantity Cannot be Blank !!!";
                return mstrValidate;
            }
            if (warename == "")
            {
                mstrValidate = mstrValidate + "Warehouse Name Role Cannot be Blank !!!";
                return mstrValidate;
            }
            if (warelocation == "")
            {
                mstrValidate = mstrValidate + "Warehouse Location Cannot be Blank !!!";
                return mstrValidate;
            }
            if (cddno == "")
            {
                mstrValidate = mstrValidate + "CDD no. Cannot be Blank !!!";
                return mstrValidate;
            }
            if (cmselotid== "")
            {
                mstrValidate = mstrValidate + "CMSE LOT ID Cannot be Blank !!!";
                return mstrValidate;
            }
            if (discount == "")
            {
                mstrValidate = mstrValidate + "Discount Cannot be Blank !!!";
                return mstrValidate;
            }
           
            return mstrValidate;
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grddispatch.EditIndex = e.NewEditIndex;
            ViewState["RowVal"] = e.NewEditIndex;
       
            FillDispatchgrid();
           
           
            //grdAdmin.Columns[5].Visible = true;
        }

        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label commodityname = (Label)grddispatch.Rows[e.RowIndex].FindControl("edtcommodityname");

                //TextBox txtCommodityName = (TextBox)grddispatch.Rows[e.RowIndex].FindControl("edtcmdtyname");
                string ddlCommodityType = (grddispatch.Rows[e.RowIndex].FindControl("drpdwncommoditytype") as DropDownList).SelectedItem.Value;
                TextBox dtDispDate = (TextBox)grddispatch.Rows[e.RowIndex].FindControl("edtdispatchdate");
                TextBox txtBuyCompany = (TextBox)grddispatch.Rows[e.RowIndex].FindControl("edtbuyercompany");
                TextBox txtQuantity = (TextBox)grddispatch.Rows[e.RowIndex].FindControl("edtqty");
                DropDownList txtWareName = (DropDownList)grddispatch.Rows[e.RowIndex].FindControl("edtddlsWarehouse");
                TextBox txtWareLocation = (TextBox)grddispatch.Rows[e.RowIndex].FindControl("edtwarehouselocation");
                TextBox txtCDDNo = (TextBox)grddispatch.Rows[e.RowIndex].FindControl("edtcddno");
                TextBox txtLotId = (TextBox)grddispatch.Rows[e.RowIndex].FindControl("edtcmselotid");
                TextBox txtDiscount = (TextBox)grddispatch.Rows[e.RowIndex].FindControl("edtdiscount");
                Label lblID = (Label)grddispatch.Rows[e.RowIndex].FindControl("DSID");
                //string ddl_AdminRole = (grddispatch.Rows[e.RowIndex].FindControl("gadminrole") as DropDownList).SelectedItem.Value;
                //string ddl_Action = (grddispatch.Rows[e.RowIndex].FindControl("guseraction") as DropDownList).SelectedItem.Value;

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
                 strValidate = ValidateUser(commodityname.Text, ddlCommodityType,
                        dtDispDate.Text, txtBuyCompany.Text, txtQuantity.Text,
                        txtWareName.Text, txtWareLocation.Text, txtCDDNo.Text, txtLotId.Text, txtDiscount.Text);
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateDispatchstock", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                            //SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                            //AddInParam("@iAdminId",SqlDbType.BigInt, Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId)) 
                            //SqlHelper.AddInParam("@iAdminId", SqlDbType.BigInt,Convert.ToInt64(((SysCompany)Session[""]) ),
                                SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, commodityname.Text),
                                SqlHelper.AddInParam("@sCommodityType", SqlDbType.VarChar, ddlCommodityType),
                                SqlHelper.AddInParam("@dDispatchDate", SqlDbType.Date, dtDispDate.Text),
                                SqlHelper.AddInParam("@sSeller", SqlDbType.VarChar, txtBuyCompany.Text),
                                SqlHelper.AddInParam("@iQty", SqlDbType.Int, txtQuantity.Text),
                                SqlHelper.AddInParam("@sWarehouseName", SqlDbType.VarChar, txtWareName.SelectedItem.Text),
                                SqlHelper.AddInParam("@sWarhouseLocation", SqlDbType.VarChar, txtWareLocation.Text),
                                SqlHelper.AddInParam("@sCDDNo", SqlDbType.VarChar, txtCDDNo.Text),
                                SqlHelper.AddInParam("@sCMSELotId", SqlDbType.VarChar, txtLotId.Text),
                                SqlHelper.AddInParam("@nDiscount", SqlDbType.Decimal, txtDiscount.Text),
                             SqlHelper.AddInParam("@iDispatchId", SqlDbType.Int, lblID.Text));

                        SetProductsUpdateMessage(false, "Dispatch Stock Updated Successfully");
                        grddispatch.EditIndex = -1;
                        FillDispatchgrid();
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
            grddispatch.EditIndex = -1;
            FillDispatchgrid();
            selectcommname.Items.Clear();
            ClearAll();
          
            //if (txtImgPathMain.Value != "")
            //{
            //    bool blnFlagDelete = DeleteFile(txtImgPathMain.Value);
            //}
        }

        private void getWarehousename()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT Ware_Id,Warehouse_Name FROM Warehouse_List";
                // SqlHelper.AddInParam("@ID", SqlDbType.VarChar, strId));
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                ddlWareName.DataSource = dtGetUserDetails;
                ddlWareName.DataTextField = "Warehouse_Name";
                ddlWareName.DataValueField = "Ware_Id";
                ddlWareName.DataBind();
                ddlWareName.Items.Insert(0, new ListItem("-- Select Warehouse Name --", "0"));
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

        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                    {
                        string strConn = Convert.ToString(Session["SystemUserSqlConnectionString"]);
                        string qgetwarehousename = "select iStockId,sWarehouseName from Com_Stock";
                        //For Warehouse
                        DropDownList edtddlsWarehouseObj = (DropDownList)e.Row.FindControl("edtddlsWarehouse");
                        // string strConn = Convert.ToString(Session["SystemUserSqlConnectionString"]);
                        
                        DataTable dtActiveWarehouse = SqlHelper.ReadTable(qgetwarehousename, strConn, false);

                        edtddlsWarehouseObj.DataSource = dtActiveWarehouse;
                        edtddlsWarehouseObj.DataValueField = "iStockId";
                        edtddlsWarehouseObj.DataTextField = "sWarehouseName";
                        edtddlsWarehouseObj.DataBind();
                        edtddlsWarehouseObj.Items.Insert(0, new ListItem("-- Select Warehouse Name --", "0"));

                        Label lblsWarehouse = (Label)e.Row.FindControl("lblsWarehouse");
                        //drdlObject.Visible = true;
                        //lblObject.Visible = false;

                        edtddlsWarehouseObj.Items.FindByText(lblsWarehouse.Text).Selected = true;

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

        public void FillDispatchgrid()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "Select * from  Com_DispatchStock  where bIsActive=1";
                //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                grddispatch.DataSource = dtGetUserDetails;
                grddispatch.DataBind();
                
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillAdminGrid", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        private void ClearAll()
        {
            //txtCommodityName.Text = "";
            selectcommname.SelectedIndex = 0;
            ddlCommodityType.SelectedIndex = 0;
            dtDispDate.Value = "";
            txtBuyCompany.Text = "";
            //SetMessage(false, "Press Save to store Dispatch Stock Details");
            txtQuantity.Text = "";
            ddlWareName.SelectedIndex = 0;
            txtWareLocation.Text = "";
            txtCDDNo.Text = "";
            txtLotId.Text = "";
            txtDiscount.Text = "";

            //ViewState["ImgLogo"] = "";
            //ViewState["ImgPath"] = "";
            //ViewState["RowVal"] = "";
        }

        public void ClearControls()
        {
            //txtCommodityName.Text = "";
            selectcommname.SelectedIndex = 0;
            ddlCommodityType.SelectedIndex = 0;
            dtDispDate.Value = "";
            txtBuyCompany.Text = "";
            txtQuantity.Text = "";
            ddlWareName.SelectedIndex = 0;
            txtWareLocation.Text = "";
            txtCDDNo.Text = "";
            txtLotId.Text = "";
            txtDiscount.Text = "";
            //SetMessage(false, "Press Save To Add Dispatch Stock Details!!");
        } 

        protected void btnDeleteDispatchStock_ServerClick(object sender, EventArgs e)
        {
            // string strquery = "Update User_Master set UM_bItIsActive = 0 where UM_bIntId=@id";
            // SqlHelper.UpdateDatabase(strquery, SqlHelper.AddInParam("@id", SqlDbType.VarChar, txtDelHidden.Value.Trim()));
            DataTable dtCatData = SqlHelper.ReadTable("spDeleteDispatchStock", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
           SqlHelper.AddInParam("@id", SqlDbType.Int, Convert.ToInt32(txtDelHidden.Value)));
            SetMessage(false, "Dispatch Stock Deleted Successfully");
            txtDelHidden.Value = "";
            txtDelDispatchID.Text = "";
            txtDelCommodityName.Text = "";
            grddispatch.EditIndex = -1;
            FillDispatchgrid();
        }

        private void FillDrdId()
        {
            try
            {
               string mstrGetUser = "";
                mstrGetUser = "SELECT iComID,sCommodityName FROM Com_Commodity  WHERE bIsActive=1";
                   // SqlHelper.AddInParam("@ID", SqlDbType.VarChar, strId));
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                selectcommname.DataSource = dtGetUserDetails;
                selectcommname.DataTextField = "sCommodityName";
                selectcommname.DataValueField = "iComID";
                selectcommname.DataBind();
                selectcommname.Items.Insert(0, new ListItem("-- Select Commodity Name --", "0"));
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
        //private void editdropdown()
        //{
        //    try
        //    {
        //        string mstrGetUser = "";
        //        mstrGetUser = "SELECT iComID,sCommodityName FROM Com_Commodity  WHERE bIsActive=1";
        //        // SqlHelper.AddInParam("@ID", SqlDbType.VarChar, strId));
        //        DataTable dtGetUserDetails;
        //        dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
        //        edtselectcommname.DataSource = dtGetUserDetails;
        //        selectcommname.DataTextField = "sCommodityName";
        //        selectcommname.DataValueField = "iComID";
        //        selectcommname.DataBind();
        //        selectcommname.Items.Insert(0, new ListItem("-- Select Commodity Name --", "0"));
        //    }
        //    catch (Exception exError)
        //    {
        //        long pLngErr = -1;
        //        if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //            pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //        pLngErr = GlobalFunctions.ReportError("FillDrdId", "FillDrdId", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
        //        updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
        //        updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
        //    }
        //}
    }
}