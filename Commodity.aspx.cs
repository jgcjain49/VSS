using System;
using System.Collections.Generic;
using System.Data;

using System.IO;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;

namespace Admin_CommTrex
{
    public partial class Commodity : System.Web.UI.Page
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
                    FillCommodityGrid();
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
                    mstrGetUser = "SELECT * from Com_Commodity where iComID= '" + HidBnkId.Value + "'";
                    //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                    DataTable dtGetUserDetails = new DataTable();

                   
                    //dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, Convert.ToString(Session["SystemUserSqlConnectionString"]), false,
                    //                SqlHelper.AddInParam("@iPayId", SqlDbType.Int, Convert.ToInt32(HidBnkId.Value)));

                    dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);


                    txtCommName.Text = dtGetUserDetails.Rows[0]["sCommodityName"].ToString();
                    txtGST.Text = dtGetUserDetails.Rows[0]["sGst"].ToString();
                    txtPacking.Text = dtGetUserDetails.Rows[0]["sPacking"].ToString();
                    txtDefaultQty.Text = dtGetUserDetails.Rows[0]["iLotSize"].ToString();
                    //ddlCommodityType.Text = dtGetUserDetails.Rows[0]["iLotSize"].ToString();

                    //string selectedCommodity = ddlCommodityType.Items[ddlCommodityType.SelectedIndex].Text;
                    ddlCommodityType.SelectedItem.Text = dtGetUserDetails.Rows[0]["sTypeOfCommodity"].ToString();

                    //txtAmt.Text = dtGetUserDetails.Rows[0]["nAmount"].ToString();
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
                    strValidate = ValidateUser(txtCommName.Text , txtGST.Text,txtPacking.Text ,
                         txtDefaultQty.Text,
                         //txtQtyType.Text, 
                         ddlCommodityType.SelectedItem.Value); 
                    //, txtDefFuturesQty.Text , txtDefOptionsQty.Text
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateCommodity", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                        //string strquery = "Insert into User_Master(UM_CompId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation,UM_vCharPassword,UM_bItIsActive) values (@comp_id,@u_name,@u_ID,@u_desg," + GlobalFunctions.CreateEncryptTextSyntax("@u_pass", false, false) + ",1)";
                      //  DataTable dtData = SqlHelper.ReadTable(strquery, SqlHelper.AddInParam("@comp_id",SqlDbType.BigInt, Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId)),
                        //DataTable dtData = SqlHelper.ReadTable(strquery, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false,
                        //SqlHelper.AddInParam("@u_ID", SqlDbType.VarChar, txtCommName.Text),
                        SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, txtCommName.Text),
                        SqlHelper.AddInParam("@sGst", SqlDbType.VarChar, txtGST.Text),
                        SqlHelper.AddInParam("@sPacking", SqlDbType.VarChar, txtPacking.Text),
                        SqlHelper.AddInParam("@iLotSize", SqlDbType.VarChar, txtDefaultQty.Text),
                        //SqlHelper.AddInParam("@sQtyType", SqlDbType.VarChar, ""),
                        SqlHelper.AddInParam("@sTypeOfCommodity", SqlDbType.VarChar, ddlCommodityType.SelectedItem.Text));
                        //SqlHelper.AddInParam("@u_desg", SqlDbType.VarChar, txtDefFuturesQty.Text),
                        //SqlHelper.AddInParam("@u_name", SqlDbType.VarChar, txtDefOptionsQty.Text));
                        FillCommodityGrid();
                        //ddlCommodityType.Items.Clear();
                        SetMessage(false, "Commodity Added Successfully");
                        LockControls(false);
                        btnSave.Attributes["btn-action"] = "New";
                        btnSave.InnerHtml = "New";
                        SetMessage(false, "Commodity Added Succesfully!!");
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
                    strValidate =  ValidateUser(txtCommName.Text , txtGST.Text,txtPacking.Text ,
                         txtDefaultQty.Text, ddlCommodityType.SelectedItem.Value); 
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateCommodity", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                     
                                                SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, txtCommName.Text),
                                                 SqlHelper.AddInParam("@sGst", SqlDbType.VarChar, txtGST.Text),
                                                 SqlHelper.AddInParam("@sPacking", SqlDbType.VarChar, txtPacking.Text),
                                                 SqlHelper.AddInParam("@iLotSize", SqlDbType.VarChar, txtDefaultQty.Text),
                                                 //SqlHelper.AddInParam("@sQtyType", SqlDbType.VarChar, ""),
                                                 SqlHelper.AddInParam("@sTypeOfCommodity", SqlDbType.VarChar,ddlCommodityType.SelectedItem.Text),
                        
                                                 SqlHelper.AddInParam("@iComID", SqlDbType.Int, (HidBnkId.Value)));

                        SetProductsUpdateMessage(false, "Commodity Updated Successfully");
                         grdcmdty.EditIndex = -1;
                FillCommodityGrid();
                          SetMessage(false, "Comodity Updated Succesfully!!");
                            //grdUser.Columns[6].Visible = false;
                            btnSave.InnerHtml = "New";
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
                    btnSave.InnerHtml = "Save";
                    SetMessage(false, "Press Save To Add Commodity!!");
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
            else{}
        
        }
        private string ValidateUser(string strCommName, string strGST, string strPacking,
            string strDefaultQty,string strTypofcomm)
            // ,string strDefFuturesQty, string strDefOptionsQty
        {
            string mstrValidate = "";
            if (strCommName == "")
            {
                mstrValidate = mstrValidate + " Commodity Name Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strGST == "")
            {
                mstrValidate = mstrValidate + "GST Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strPacking == "")
            {
                mstrValidate = mstrValidate + "Packing Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strDefaultQty == "")
            {
                mstrValidate = mstrValidate + "Default Qty Cannot be Blank !!!";
                return mstrValidate;
            }
            //if (strQtyType == "")
            //{
            //    mstrValidate = mstrValidate + "Quantity Type Cannot be Blank !!!";
            //    return mstrValidate;
            //}
            if (strTypofcomm == "0")
            {
                mstrValidate = mstrValidate + "Type of commodity Cannot be Blank !!!";
                return mstrValidate;
            }
            //if (strDefFuturesQty == "")
            //{
            //    mstrValidate = mstrValidate + "Default Futures Qty Cannot be Blank !!!";
            //}
            //if (strDefOptionsQty == "0")
            //{
            //    mstrValidate = mstrValidate + "Default Options Qty Cannot be Blank !!!";
            //}
            return mstrValidate;
        }

        public void FillCommodityGrid()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT * from  Com_Commodity where bIsActive=1";
                ////mstrGetUser = "SELECT * from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId);
                //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                DataTable dtGetCommodityDetails;
                dtGetCommodityDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                grdcmdty.DataSource = dtGetCommodityDetails;
                grdcmdty.DataBind();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillCommodityGrid", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

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
            txtCommName.Enabled = pBoolState;
            txtGST.Enabled = pBoolState;
            txtPacking.Enabled = pBoolState;
            txtDefaultQty.Enabled = pBoolState;
            //txtQtyType.Enabled = pBoolState;
            ddlCommodityType.Enabled = pBoolState;
            //txtDefFuturesQty.Enabled = pBoolState;
            //txtDefOptionsQty.Enabled = pBoolState;
        }
        public void ClearControls()
        {
            txtCommName.Text = "";
            txtGST.Text = "";
            txtPacking.Text = "";
            txtDefaultQty.Text = "";
            //txtQtyType.Text = "";
            ddlCommodityType.SelectedIndex = 0;
            //SetMessage(false, "Press Save To Add Commodity!!");
            //txtDefFuturesQty.Text = "";
            //txtDefOptionsQty.Text = "";
        }
        protected void btnDeleteCommodity_ServerClick(object sender, EventArgs e)
        {
            // string strquery = "Update User_Master set UM_bItIsActive = 0 where UM_bIntId=@id";
             DataTable dtCatData = SqlHelper.ReadTable("spDeleteCommodity", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
            SqlHelper.AddInParam("@id", SqlDbType.Int, Convert.ToInt32(txtDelHidden.Value)));
            SetMessage(false, "Commodity Deleted Successfully");
            txtDelHidden.Value = "";
            txtDelCommID.Text = "";
            txtDelCommName.Text = "";
            grdcmdty.EditIndex = -1;
            FillCommodityGrid();
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdcmdty.EditIndex = e.NewEditIndex;
            ViewState["RowVal"] = e.NewEditIndex;
            FillCommodityGrid();
            //grdAdmin.Columns[5].Visible = true;
        }

        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox cmdtyname = (TextBox)grdcmdty.Rows[e.RowIndex].FindControl("edtcmdtyname");
                TextBox gst = (TextBox)grdcmdty.Rows[e.RowIndex].FindControl("edtgst");
                TextBox packing = (TextBox)grdcmdty.Rows[e.RowIndex].FindControl("edtpacking");
                TextBox lotsize = (TextBox)grdcmdty.Rows[e.RowIndex].FindControl("edtlotsize");
                TextBox qtytype = (TextBox)grdcmdty.Rows[e.RowIndex].FindControl("edtquantitytype");
                TextBox cmdtytype = (TextBox)grdcmdty.Rows[e.RowIndex].FindControl("edtcommoditytype");
                
                //string ddl_AdminRole = (grdcmdty.Rows[e.RowIndex].FindControl("gadminrole") as DropDownList).SelectedItem.Value;
                //string ddl_Action = (grdcmdty.Rows[e.RowIndex].FindControl("guseraction") as DropDownList).SelectedItem.Value;

                Label lblID = (Label)grdcmdty.Rows[e.RowIndex].FindControl("CID");
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
                    strValidate = ValidateUser(cmdtyname.Text , gst.Text, packing.Text ,
                         lotsize.Text 
                  //qtytype.Text
                , cmdtytype.Text
                         ); //, txtDefFuturesQty.Text , txtDefOptionsQty.Text
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateCommodity", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                            //SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                            //AddInParam("@iAdminId",SqlDbType.BigInt, Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId)) 
                            //SqlHelper.AddInParam("@iAdminId", SqlDbType.BigInt,Convert.ToInt64(((SysCompany)Session[""]) ), 
                                                SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, cmdtyname.Text),
                                                 SqlHelper.AddInParam("@sGst", SqlDbType.VarChar, gst.Text),
                                                 SqlHelper.AddInParam("@sPacking", SqlDbType.VarChar, packing.Text),
                                                 SqlHelper.AddInParam("@iLotSize", SqlDbType.VarChar, lotsize.Text),
                                                 SqlHelper.AddInParam("@sQtyType", SqlDbType.VarChar, qtytype.Text),
                                                 SqlHelper.AddInParam("@sTypeOfCommodity", SqlDbType.VarChar, cmdtytype.Text),
                            //SqlHelper.AddInParam("@sAdminRole", SqlDbType.VarChar, ddl_AdminRole),
                            //SqlHelper.AddInParam("@sAction", SqlDbType.VarChar, ddl_Action),
                                                 SqlHelper.AddInParam("@iComID", SqlDbType.Int, (lblID.Text)));

                        SetProductsUpdateMessage(false, "Commodity Updated Successfully");
                         grdcmdty.EditIndex = -1;
                FillCommodityGrid();
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
            grdcmdty.EditIndex = -1;
            FillCommodityGrid();
            ClearControls();
            //if (txtImgPathMain.Value != "")
            //{
            //    bool blnFlagDelete = DeleteFile(txtImgPathMain.Value);
            //}
        }

        //protected void btnUploadExcel_Click(object sender, EventArgs e)
        //{
           
        //        //Check whether files being selected or not
        //    if (fileExcel.HasFile)
        //    {
        //        try
        //        {
        //            //Read an Excel file (1st sheet) for user transfer details
        //            string ext = Path.GetExtension(fileExcel.FileName).ToLower();
        //            string path = Server.MapPath(fileExcel.PostedFile.FileName);
        //            DataSet ds;
        //            fileExcel.SaveAs(path);
        //            string ConStr = string.Empty;
        //            if (ext.Trim() == ".xls")
        //            {
        //                ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        //            }
        //            else if (ext.Trim() == ".xlsx")
        //            {
        //                ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
        //            }
        //            else
        //            {
        //                actionInfo.Attributes["class"] = "alert alert-info ";
        //                actionInfo.InnerHtml = "Only excel file (.xls or .xlsx) is valid !!!";
        //                return;
        //            }
        //            OleDbConnection olecon = new OleDbConnection(ConStr);

        //            //string readExcelSheet = "select * from [Sheet1$]";
        //            OleDbCommand cmd = new OleDbCommand("select * from [" + "Sheet1" + "$A1:end]", olecon);
        //            //OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$]", olecon);
        //            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //            ds = new DataSet();
        //            da.Fill(ds);
        //            olecon.Open();
        //            //SqlBulkCopyColumnMapping mapping = new SqlBulkCopyColumnMapping();
        //            DbDataReader dr = cmd.ExecuteReader();
        //            string coonnstr = @"Data Source=108.178.51.226,1533;Initial Catalog=commtrex;User ID=vitco;Password=Vit@198376";
        //            var options = SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction;
        //            SqlBulkCopy bulkinsert = new SqlBulkCopy(coonnstr, options);


        //            //bulkinsert.DestinationTableName = "Com_Commodity";
        //            bulkinsert.ColumnMappings.Add(0, "sCommodityName");
        //            bulkinsert.ColumnMappings.Add(1, "sGst");
        //            bulkinsert.ColumnMappings.Add(2, "sPacking");
        //            bulkinsert.ColumnMappings.Add(3, "iLotSize");
        //            bulkinsert.ColumnMappings.Add(4, "sQtyType");
        //            bulkinsert.ColumnMappings.Add(5, "sTypeOfCommodity");                  
        //            bulkinsert.DestinationTableName = "Com_Commodity";
                    
                
                    
        //            bulkinsert.BatchSize = 0;
        //            bulkinsert.WriteToServer(dr);
                   
        //            olecon.Close();
        //            SetMessage(false, "Excell Added Succesfully!!!");

        //            FillCommodityGrid();
        //        }
        //        catch (Exception exError)
        //        {
        //            long pLngErr = -1;
        //            if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //                pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //            pLngErr = GlobalFunctions.ReportError("btnUploadExcel_Click", "UserTransfer", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
        //            //bulkTransferMsg.Attributes["class"] = "alert alert-info";
        //            //bulkTransferMsg.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
        //        }

        //    }
        //    else
        //    {
        //        //bulkTransferMsg.Attributes["class"] = "alert alert-info ";
        //        //bulkTransferMsg.InnerHtml = "Please select an excel file for User Transfer !!!";
        //    }  
        //}

        protected string validateRow(string p1, string p2, string p3, string p4,string p5,string p6,string p7)
        {
            if (string.IsNullOrWhiteSpace(p1))
                return "User ID blank";
            if (string.IsNullOrWhiteSpace(p2))
                return "Name cannot blank";
            if (string.IsNullOrWhiteSpace(p3))
                return "GSt blank";
            if (string.IsNullOrWhiteSpace(p4))
                return "Packaging blank";
            if (string.IsNullOrWhiteSpace(p5))
                return "lotsize blank";
            if (string.IsNullOrWhiteSpace(p6))
                return "Quanity blank";
            if (string.IsNullOrWhiteSpace(p7))
                return "size blank";
            return "";
        }
    }
}