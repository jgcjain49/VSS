using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;
 
namespace Admin_CommTrex
{
    public partial class AddStock : System.Web.UI.Page
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
                    FillDrdId();
                    getWarehousename();
					  //FillWarehouse();
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
                    mstrGetUser = "SELECT * from Com_Stock where iStockId= '" + HidBnkId.Value + "'";
                    //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                    DataTable dtGetUserDetails = new DataTable();
                    //dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, Convert.ToString(Session["SystemUserSqlConnectionString"]), false,
                    //                SqlHelper.AddInParam("@iPayId", SqlDbType.Int, Convert.ToInt32(HidBnkId.Value)));
                    dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                    ddl_CommNm.SelectedItem.Text = dtGetUserDetails.Rows[0]["sCommodityName"].ToString();
                    ddlCommodityType.SelectedItem.Text = dtGetUserDetails.Rows[0]["sCommodityMarket"].ToString();
                    //dtDepDate.Value = dtGetUserDetails.Rows[0]["dDepositDate"].ToString();
                    dtDepDate.Value = Convert.ToDateTime(dtGetUserDetails.Rows[0]["dDepositDate"]).ToString("yyyy-MM-dd");
                    txtDepCompany.Text = dtGetUserDetails.Rows[0]["sSeller"].ToString();
                    txtQuantity.Text = dtGetUserDetails.Rows[0]["iQty"].ToString();
                    ddl_WareLocation.SelectedItem.Text = dtGetUserDetails.Rows[0]["sWarehouseLocation"].ToString();
                    ddl_WareName.SelectedItem.Text = dtGetUserDetails.Rows[0]["sWarehouseName"].ToString();
                    txtCDDNo.Text = dtGetUserDetails.Rows[0]["sCDDNo"].ToString();
                    txtLotId.Text = dtGetUserDetails.Rows[0]["sCMSELotId"].ToString();
                    txtDiscount.Text = dtGetUserDetails.Rows[0]["nDiscount"].ToString();
                    dtFEDDate.Value = Convert.ToDateTime(dtGetUserDetails.Rows[0]["dFED"]).ToString("yyyy-MM-dd");
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
            ddl_CommNm.Enabled = pBoolState;
            ddlCommodityType.Enabled = pBoolState;
            //dtDepDate.Enabled = pBoolState;
            txtDepCompany.Enabled = pBoolState;
            txtQuantity.Enabled = pBoolState;
            ddl_WareName.Enabled = pBoolState;
            ddl_WareLocation.Enabled = pBoolState;
            //dtExpiry.Enabled = pBoolState;
            txtCDDNo.Enabled = pBoolState;
            txtLotId.Enabled = pBoolState;
            txtDiscount.Enabled = pBoolState;
            //dtFEDDate.Enabled = pBoolState;
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
                    strValidate = ValidateUser(ddl_CommNm.SelectedItem.Value, ddlCommodityType.SelectedItem.Value,
                        dtDepDate.Value, txtDepCompany.Text, txtQuantity.Text,
                        ddl_WareLocation.SelectedItem.Value, ddl_WareName.SelectedItem.Value, txtCDDNo.Text, txtLotId.Text, txtDiscount.Text, dtFEDDate.Value);
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateStock", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                         SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, ddl_CommNm.SelectedItem.Text),
                         SqlHelper.AddInParam("@sCommodityMarket", SqlDbType.VarChar, ddlCommodityType.SelectedItem.Text),
                         SqlHelper.AddInParam("@dDepositDate", SqlDbType.Date, dtDepDate.Value),
                         SqlHelper.AddInParam("@sSeller", SqlDbType.VarChar, txtDepCompany.Text),
                         SqlHelper.AddInParam("@iQty", SqlDbType.Int, txtQuantity.Text),
                         SqlHelper.AddInParam("@sWarehouseLocation", SqlDbType.VarChar, ddl_WareLocation.SelectedItem.Text),
                         SqlHelper.AddInParam("@sWarehouseName", SqlDbType.VarChar, ddl_WareName.SelectedItem.Text),
                         SqlHelper.AddInParam("@sCDDNo", SqlDbType.VarChar, txtCDDNo.Text),
                         SqlHelper.AddInParam("@sCMSELotId", SqlDbType.VarChar, txtLotId.Text),
                         SqlHelper.AddInParam("@nDiscount", SqlDbType.Decimal, txtDiscount.Text),
                         SqlHelper.AddInParam("@dFED", SqlDbType.Date, dtFEDDate.Value));

                        FillAdminGrid();
                        SetMessage(false, "Admin Added Succesfully!!");
                        LockControls(false);
                        btnSave.Attributes["btn-action"] = "New";
                        //btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> New";
                       
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
                        strValidate = ValidateUser(ddl_CommNm.SelectedItem.Value, ddlCommodityType.SelectedItem.Value,
                       dtDepDate.Value, txtDepCompany.Text, txtQuantity.Text,
                       ddl_WareLocation.SelectedItem.Value, ddl_WareName.SelectedItem.Value, txtCDDNo.Text, txtLotId.Text, txtDiscount.Text, dtFEDDate.Value);
                        if (strValidate == "")
                        {
                            DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateStock", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                     SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, ddl_CommNm.SelectedItem.Text),
                                     SqlHelper.AddInParam("@sCommodityMarket", SqlDbType.VarChar, ddlCommodityType.SelectedItem.Text),
                                     SqlHelper.AddInParam("@dDepositDate", SqlDbType.Date, dtDepDate.Value),
                                     SqlHelper.AddInParam("@sSeller", SqlDbType.VarChar, txtDepCompany.Text),
                                     SqlHelper.AddInParam("@iQty", SqlDbType.Int, txtQuantity.Text),
                                     SqlHelper.AddInParam("@sWarehouseLocation", SqlDbType.VarChar, ddl_WareLocation.SelectedItem.Text),
                                     SqlHelper.AddInParam("@sWarehouseName", SqlDbType.VarChar, ddl_WareName.SelectedItem.Text),
                                     SqlHelper.AddInParam("@sCDDNo", SqlDbType.VarChar, txtCDDNo.Text),
                                     SqlHelper.AddInParam("@sCMSELotId", SqlDbType.VarChar, txtLotId.Text),
                                     SqlHelper.AddInParam("@nDiscount", SqlDbType.Decimal, txtDiscount.Text),
                                     SqlHelper.AddInParam("@dFED", SqlDbType.Date, dtFEDDate.Value),
                                     SqlHelper.AddInParam("@iStockId", SqlDbType.Int, HidBnkId.Value));

                            SetMessage(false, "Stock Updated Succesfully!!");
                            btnSave.InnerHtml = "Save";
                            btnClear.InnerHtml = "Clear";
                            //SetProductsUpdateMessage(false, " Stock Updated Successfully");
                            grdUser.EditIndex = -1;
                            FillAdminGrid();
                            ddl_CommNm.Items.Clear();
                            FillDrdId();
                 
                            ClearControls();
                        }
                        else
                        {
                            SetProductsUpdateMessage(false, strValidate);
                        }
                      
                        grdUser.EditIndex = -1;
                        FillAdminGrid();
                        
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

        //protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
        //            {
        //                string strConn = Convert.ToString(Session["SystemUserSqlConnectionString"]);
        //                string qGetCountries = "select Countryname,Countryid from Countries";
        //                DataTable dtActiveCountry = SqlHelper.ReadTable(qGetCountries, strConn, false);
        //                DropDownList edtdllwarehouseObj = (DropDownList)e.Row.FindControl("sWarehouseName");
        //                edtdllwarehouseObj.DataSource = dtActiveCountry;
        //                edtdllwarehouseObj.DataValueField = "Ware_Id";
        //                edtdllwarehouseObj.DataTextField = "Warehouse_Name";
        //                edtdllwarehouseObj.DataBind();
        //                edtdllwarehouseObj.Items.Insert(0, new ListItem("-- Select WareHouse --", "0"));
        //                Label lblOfware = (Label)e.Row.FindControl("lbWarehouseName");
        //                edtdllwarehouseObj.Items.FindByText(lblOfware.Text).Selected = true;
        //            }

        //        }
        //    }
        //    catch (Exception exError)
        //    {
        //        long pLngErr = -1;
        //        if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //            pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //        pLngErr = GlobalFunctions.ReportError("grdTeam_RowDataBound", "TeamMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
        //        updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
        //        updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
        //    }
        //}

        private string ValidateUser(/*string strType, */string dll_comName, string ddl_comType,
            string DepDate, string txtDepCompany, string Quantity,
            string WareLocation, string WareName, string CDDNo, string LotId, string Discount, string FEDDate)
        {
            string mstrValidate = "";
            if (dll_comName == "")
            {
                mstrValidate = mstrValidate + "comName Cannot be Blank !!!";
                return mstrValidate;
            }

            if (ddl_comType == "")
            {
                mstrValidate = mstrValidate + "ddl comType Cannot be Blank !!!";
                return mstrValidate;
            }
            if (DepDate == "")
            {
                mstrValidate = mstrValidate + "Deposit Date Cannot be Blank !!!";
                return mstrValidate;
            }
            if (txtDepCompany == "")
            {
                mstrValidate = mstrValidate + "DepCompany Cannot be Blank !!!";
                return mstrValidate;
            }
            if (Quantity == "")
            {
                mstrValidate = mstrValidate + "Quantity Cannot be Blank !!!";
                return mstrValidate;
            }
            //if (WareLocation == "")
            //{
            //    mstrValidate = mstrValidate + "Warehouse Location Cannot be Blank !!!";
            //    return mstrValidate;
            //}
            if (WareName == "")
            {
                mstrValidate = mstrValidate + "Warehouse Name Cannot be Blank !!!";
                return mstrValidate;
            }
            if (CDDNo == "")
            {
                mstrValidate = mstrValidate + "CDDNo areas Cannot be Blank !!!";
                return mstrValidate;
            }
            if (LotId == "")
            {
                mstrValidate = mstrValidate + "LotId Cannot be Blank !!!";
                return mstrValidate;
            }
            if (Discount == "")
            {
                mstrValidate = mstrValidate + "Discount Cannot be Blank !!!";
                return mstrValidate;
            }
            if (FEDDate == "")
            {
                mstrValidate = mstrValidate + "FED Date Cannot be Blank !!!";
                return mstrValidate;
            }
            return mstrValidate;
        }

        protected void btnDeleteClient_ServerClick(object sender, EventArgs e)
        {
            //string strquery = "update Com_Stock set @IsActive=@isavtive where iClientId=@id";
            DataTable dtCatData = SqlHelper.ReadTable("spDeleteStock", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                SqlHelper.AddInParam("@id", SqlDbType.Int, Convert.ToInt32(txtDelHidden.Value)));
            SetProductsUpdateMessage(false, "Admin Deleted Successfully");
            txtDelHidden.Value = "";
            txtQuantity.Text = "";
            ddl_WareLocation.SelectedIndex = 0;
            ddl_WareName.SelectedIndex = 0;
            txtCDDNo.Text = "";
            txtLotId.Text = "";
            txtDiscount.Text = "";

            grdUser.EditIndex = -1;
            FillAdminGrid();
        }
        public void FillAdminGrid()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT * from  Com_Stock where bIsActive = 1 ";
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

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {

            //Check whether files being selected or not
            if (fileExcel.HasFile)
            {
                try
                {
                    //Read an Excel file (1st sheet) for user transfer details
                    string ext = Path.GetExtension(fileExcel.FileName).ToLower();
                    string path = Server.MapPath(fileExcel.PostedFile.FileName);
                    DataSet ds;
                    fileExcel.SaveAs(path);
                    string ConStr = string.Empty;
                    if (ext.Trim() == ".xls")
                    {
                        ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=2\"";
                    }
                    else if (ext.Trim() == ".xlsx")
                    {
                        ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
                    }
                    else
                    {
                        actionInfo.Attributes["class"] = "alert alert-info ";
                        actionInfo.InnerHtml = "Only excel file (.xls or .xlsx) is valid !!!";
                        return;
                    }
                    OleDbConnection olecon = new OleDbConnection(ConStr);

                    //string readExcelSheet = "select * from [Sheet1$]";
                    OleDbCommand cmd = new OleDbCommand("select * from [" + "Sheet1" + "$A1:end]", olecon);
                    //OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$]", olecon);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    olecon.Open();
                    //SqlBulkCopyColumnMapping mapping = new SqlBulkCopyColumnMapping();
                    DbDataReader dr = cmd.ExecuteReader();
                    string coonnstr = @"Data Source=184.154.187.166;Initial Catalog=commtrex;User ID=vitco;Password=Vit@198376";
                    var options = SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction;
                    SqlBulkCopy bulkinsert = new SqlBulkCopy(coonnstr, options);


                    bulkinsert.DestinationTableName = "Warehouse_List";
                    bulkinsert.ColumnMappings.Add(0, "Warehouse_Name");
                



                    bulkinsert.BatchSize = 0;
                    bulkinsert.WriteToServer(dr);

                    olecon.Close();
                    Excell_Info.InnerHtml = "Excell Added Succesfully!!!";

                    FillAdminGrid();
                }
                catch (Exception exError)
                {
                    long pLngErr = -1;
                    if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                        pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                    pLngErr = GlobalFunctions.ReportError("btnUploadExcel_Click", "UserTransfer", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                    //bulkTransferMsg.Attributes["class"] = "alert alert-info";
                    //bulkTransferMsg.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
                }

            }
            else
            {
                Excell_Info.Attributes["class"] = "alert alert-info ";
                Excell_Info.InnerHtml = "Please select an excel file !!!";
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

                Label CommodityName = (Label)grdUser.Rows[e.RowIndex].FindControl("sCommodityName");
               string CommodityMarket = (grdUser.Rows[e.RowIndex].FindControl("CommodityMarket") as DropDownList).SelectedItem.Text;
                //DropDownList CommodityMarket = (DropDownList)grdUser.Rows[e.RowIndex].FindControl("CommodityMarket");
                TextBox DepositDate = (TextBox)grdUser.Rows[e.RowIndex].FindControl("DepositDate");
                TextBox Seller = (TextBox)grdUser.Rows[e.RowIndex].FindControl("Seller");
                TextBox Qty = (TextBox)grdUser.Rows[e.RowIndex].FindControl("qnt");
                TextBox WarehouseLocation = (TextBox)grdUser.Rows[e.RowIndex].FindControl("WarehouseLocation");
                TextBox sWarehouseName = (TextBox)grdUser.Rows[e.RowIndex].FindControl("sWarehouseName");
                TextBox sCDDNo = (TextBox)grdUser.Rows[e.RowIndex].FindControl("CDDNo");
                TextBox sCMSELotId = (TextBox)grdUser.Rows[e.RowIndex].FindControl("CMSELotId");
                TextBox nDiscount = (TextBox)grdUser.Rows[e.RowIndex].FindControl("Discount");
                TextBox dFED = (TextBox)grdUser.Rows[e.RowIndex].FindControl("FED");


                //string ddl_AdminRole = (grdcmdty.Rows[e.RowIndex].FindControl("gadminrole") as DropDownList).SelectedItem.Value;
                //string ddl_Action = (grdcmdty.Rows[e.RowIndex].FindControl("guseraction") as DropDownList).SelectedItem.Value;

                Label lblID = (Label)grdUser.Rows[e.RowIndex].FindControl("stockId");
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
                  strValidate = ValidateUser(CommodityName.Text, CommodityMarket,
                        DepositDate.Text, Seller.Text, Qty.Text,
                        WarehouseLocation.Text, sWarehouseName.Text, sCDDNo.Text, sCMSELotId.Text, nDiscount.Text, dFED.Text);
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateStock", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                 SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, CommodityName.Text),
                                 SqlHelper.AddInParam("@sCommodityMarket", SqlDbType.VarChar, CommodityMarket),
                                 SqlHelper.AddInParam("@dDepositDate", SqlDbType.Date, DepositDate.Text),
                                 SqlHelper.AddInParam("@sSeller", SqlDbType.VarChar, Seller.Text),
                                 SqlHelper.AddInParam("@iQty", SqlDbType.Int, Qty.Text),
                                 SqlHelper.AddInParam("@sWarehouseLocation", SqlDbType.VarChar, WarehouseLocation.Text),
                                 SqlHelper.AddInParam("@sWarehouseName", SqlDbType.VarChar, sWarehouseName.Text),
                                 SqlHelper.AddInParam("@sCDDNo", SqlDbType.VarChar, sCDDNo.Text),
                                 SqlHelper.AddInParam("@sCMSELotId", SqlDbType.VarChar, sCMSELotId.Text),
                                 SqlHelper.AddInParam("@nDiscount", SqlDbType.Decimal, nDiscount.Text),
                                 SqlHelper.AddInParam("@dFED", SqlDbType.Date, dFED.Text),
                                  SqlHelper.AddInParam("@iStockId", SqlDbType.Int, lblID.Text));


                        SetProductsUpdateMessage(false, " Stock updated Updated Successfully");
                        grdUser.EditIndex = -1;
                        FillAdminGrid();
                        ClearControls();
                    }
                    else {
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
                grdUser.EditIndex = -1;
                FillAdminGrid();
                //ClearControls();
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
        protected void btnClear_ServerClick(object sender, EventArgs e)
        {

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
        public void ClearControls()
        {
            //ddl_Type.SelectedIndex = 0;
            ddl_CommNm.SelectedValue = "0";
            ddlCommodityType.SelectedValue = "0";
            dtDepDate.Value = "";
            txtDepCompany.Text = "";
            txtQuantity.Text = "";
            ddl_WareName.SelectedValue = "0";
            ddl_WareLocation.SelectedValue = "0";
            txtCDDNo.Text = "";
            txtLotId.Text = "";
            txtDiscount.Text = "";
            dtFEDDate.Value="";
        }

        private void SetProductsUpdateMessage(bool pBlnIsError, string strMessage)
        {
            updateActionDiv.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            updateActionDiv.InnerHtml = strMessage;
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
                ddl_CommNm.DataSource = dtGetUserDetails;
                ddl_CommNm.DataTextField = "sCommodityName";
                ddl_CommNm.DataValueField = "iComID";
                ddl_CommNm.DataBind();
                ddl_CommNm.Items.Insert(0, new ListItem("-- Select Comodity  Name --", "0"));
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

        private void getWarehousename()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT Ware_Id,Warehouse_Name FROM Warehouse_List";
                // SqlHelper.AddInParam("@ID", SqlDbType.VarChar, strId));
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                ddl_WareName.DataSource = dtGetUserDetails;
                ddl_WareName.DataTextField = "Warehouse_Name";
                ddl_WareName.DataValueField = "Ware_Id";
                ddl_WareName.DataBind();
                ddl_WareName.Items.Insert(0, new ListItem("-- Select Warehouse Name --", "0"));
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

        protected void DownloadFile(object sender, EventArgs e)
        {
            string fileName = "warehouse.xlsx";
            string filePath = Server.MapPath(string.Format("~/Content/UserImages/warehouse.xlsx", fileName));
            Response.ContentType = "application/xlsx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.WriteFile(filePath);
            Response.Flush();
            Response.End();
        }

        protected void InserWarehouse(object sender, EventArgs e) 
        { 
         string buttonstate = btnSave.InnerText.Replace("\r\n", "");
            string button = buttonstate.Replace(" ", "");
       try
       {
           if (WareName.Text != "")
           {
               //string Query = "insert into Warehouse_List (Warehouse_Name) values ('" + WareName.Text + "')";

               SqlConnection conn = new SqlConnection(GlobalVariables.SqlConnectionStringMstoreInformativeDb);
               conn.Open();
               SqlCommand cmd = new SqlCommand("insert into Warehouse_List (Warehouse_Name) values ('" + WareName.Text + "')", conn);
               cmd.ExecuteNonQuery();
               Warehouse_alert.InnerHtml = "Warehouse Added Successfully !!";
               WareName.Text = "";
               conn.Close();

           }
           else {
               Warehouse_alert.InnerHtml = "Please Enter Warehouse Name !!";
           }
       }
       catch (Exception exError)
       {
           long pLngErr = -1;
           if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
               pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
           pLngErr = GlobalFunctions.ReportError("InserWarehouse", "Addstockpage", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

           Warehouse_alert.Attributes["class"] = "alert alert-info blink-border";
           Warehouse_alert.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
       }
        }
    }
}