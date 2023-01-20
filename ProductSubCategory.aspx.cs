using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using LumenWorks.Framework.IO.Csv;

namespace Admin_CommTrex
{
    public partial class ProductSubCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["TalukaDetails"] != null)
                {
                    if (Convert.ToString(Session["UserType"]) == "D" || Convert.ToString(Session["UserType"]) == "M")
                    {
                        Response.Redirect("Home.aspx");
                    }
                    FillProductInfo();

                    ViewState["RowVal"] = "";
                    LockControls(false);
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }

        protected void grdProductSubCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grdProductSubCategory.EditIndex = -1;
                FillProductInfo();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("grdProductSubCategory_RowCancelingEdit", "Product_SubCategory_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }

        }

        protected void grdProductSubCategory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdProductSubCategory.EditIndex = e.NewEditIndex;
                ClearControls();
                FillProductInfo();
                ViewState["RowVal"] = e.NewEditIndex;
                DropDownList DrpInfo = (DropDownList)grdProductSubCategory.Rows[e.NewEditIndex].FindControl("drdlInformation");
                DropDownList DrpIsActive = (DropDownList)grdProductSubCategory.Rows[e.NewEditIndex].FindControl("drdlActive");
                DropDownList DrpCat = (DropDownList)grdProductSubCategory.Rows[e.NewEditIndex].FindControl("drdlCategory");
                Label lblInfo = (Label)grdProductSubCategory.Rows[e.NewEditIndex].FindControl("lblInformation");
                Label lblCategory = (Label)grdProductSubCategory.Rows[e.NewEditIndex].FindControl("lblCategory");
                Label lblIsActive = (Label)grdProductSubCategory.Rows[e.NewEditIndex].FindControl("lblIsActive");

                HiddenField lblCatID = (HiddenField)grdProductSubCategory.Rows[e.NewEditIndex].FindControl("HiddenCatID");
                HiddenField lblInfoID = (HiddenField)grdProductSubCategory.Rows[e.NewEditIndex].FindControl("lblInfoID");
                HtmlButton btnImage = (HtmlButton)grdProductSubCategory.Rows[e.NewEditIndex].FindControl("btnImage");
                btnImage.Disabled = false;
                lblCategory.Visible = false;
                FillInformation(DrpInfo);
                DrpCat.Visible = true;

                if (DrpInfo.Items.Count > 0)
                {
                    DrpInfo.Items.FindByValue(lblInfoID.Value).Selected = true;
                    DrpInfo.SelectedItem.Text = lblInfo.Text;

                    bool IsCat = FillCategory(Convert.ToInt64(DrpInfo.SelectedItem.Value), DrpCat);

                    if (IsCat)
                    {
                        DrpCat.Items.FindByValue(lblCatID.Value).Selected = true;
                        DrpCat.SelectedItem.Text = lblCategory.Text;
                    }
                }

                if (DrpIsActive.Items.Count > 0)
                {
                    if (lblIsActive.Text == "YES")
                    {
                        DrpIsActive.Items.FindByValue("1").Selected = true;
                        DrpIsActive.SelectedItem.Text = lblIsActive.Text;
                    }
                    else
                    {
                        DrpIsActive.Items.FindByValue("0").Selected = true;
                        DrpIsActive.SelectedItem.Text = lblIsActive.Text;
                    }
                }

                lblIsActive.Visible = false;
                DrpIsActive.Enabled = true;
                DrpIsActive.Visible = true;
                lblInfo.Visible = false;
                DrpInfo.Enabled = true;
                DrpInfo.Visible = true;

                // grdProductSubCategory.EditIndex = e.NewEditIndex;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("grdProductCategory_RowEditing", "Product_SubCategory_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";


            }
        }

        protected void grdProductSubCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox txtCatName = (TextBox)grdProductSubCategory.Rows[e.RowIndex].FindControl("txtSubCatName");
                TextBox txtRegCatName = (TextBox)grdProductSubCategory.Rows[e.RowIndex].FindControl("txtSubRegName");
                HiddenField ImgCat = (HiddenField)grdProductSubCategory.Rows[e.RowIndex].FindControl("imgPath");
                Label lblCatID = (Label)grdProductSubCategory.Rows[e.RowIndex].FindControl("txtSubCatID");
                HiddenField ImgOriginalCategory = (HiddenField)grdProductSubCategory.Rows[e.RowIndex].FindControl("imgOriginalPath");
                HiddenField CatID = (HiddenField)grdProductSubCategory.Rows[e.RowIndex].FindControl("HiddenCatID");

                DropDownList DrpInfo = (DropDownList)grdProductSubCategory.Rows[e.RowIndex].FindControl("drdlInformation");
                DropDownList DrpCategory = (DropDownList)grdProductSubCategory.Rows[e.RowIndex].FindControl("drdlCategory");
                DropDownList DrpIsActive = (DropDownList)grdProductSubCategory.Rows[e.RowIndex].FindControl("drdlActive");

                string strErrorImg;
                string strError;
                strError = Validate(Convert.ToInt64(DrpInfo.SelectedItem.Value), txtCatName.Text, txtRegCatName.Text, Convert.ToInt64(DrpIsActive.SelectedItem.Value), Convert.ToInt64(lblCatID.Text), Convert.ToInt64(CatID.Value));

                if (strError == "")
                {
                    if (txtImgPathMain.Value != "")
                    {
                        strErrorImg = CopyFileSafely(txtImgPathMain.Value, ImgCat.Value);
                    }
                    DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProductSubCatMaster", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                              SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                              SqlHelper.AddInParam("@vCharName_En ", SqlDbType.VarChar, txtCatName.Text),
                                               SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, Convert.ToInt64(DrpCategory.SelectedItem.Value)),
                                              SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, txtRegCatName.Text),
                                              SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, Convert.ToInt64(DrpIsActive.SelectedItem.Value)),
                                              SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, ImgCat.Value.Replace("//", "/")),
                                              SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, Convert.ToInt64(lblCatID.Text)));

                    int intTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
                    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                    GlobalFunctions.saveInsertUserAction("Product_SubCategory_Master", "[Product Sub Category Master Update]:Updation of Product Sub Category with Id : " + Convert.ToInt64(lblCatID.Text) + " with Image : " + ImgCat.Value, intTalukaId, lngCompanyId, Request); //Call to user Action Log
                    SetProductsUpdateMessage(false, "Product Sub Category Updated Successfully");

                    if (txtImgPathMain.Value != "")
                    {
                        bool blnFlagDelete = DeleteFile(ImgOriginalCategory.Value);
                    }


                    grdProductSubCategory.EditIndex = -1;
                    FillProductInfo();
                    ClearControls();
                    DrpInfo.Items.Clear();
                    DrpIsActive.Items.Clear();
                    DrpIsActive.Visible = false;
                    DrpInfo.Visible = false;
                    DrpCategory.Visible = false;
                    SetProductsUpdateMessage(true, "Product Sub Category Updated Successfully!!!");
                }
                else
                {
                    SetProductsUpdateMessage(true, strError);

                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("grdProductCategory_RowUpdating1", "Product_SubCategory_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }


        }

        public void FillProductInfo()
        {
            try
            {
                DataTable dtProdCat = new DataTable();
                DataTable dtCatData = SqlHelper.ReadTable("spGetSubProductCategoryDetails", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                             SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)));

                grdProductSubCategory.DataSource = dtCatData;
                grdProductSubCategory.DataBind();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillProductInfo", "Product_SubCategory_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        public void FillInformation()
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                DataTable dtCategoryList = SqlHelper.ReadTable("SELECT IM_vCharInfoName_En,IM_bIntInfoId FROM Information_Master_17  WHERE IM_IsOrder=1  And IM_intInfoType=1", conString, false);

                cmbInformationType.DataSource = dtCategoryList;
                cmbInformationType.DataTextField = "IM_vCharInfoName_En";
                cmbInformationType.DataValueField = "IM_bIntInfoId";
                cmbInformationType.DataBind();
                cmbInformationType.Items.Insert(0, new ListItem("--Select Information Type--", "0"));
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillInformation", "Product_Sub_Category", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        public bool FillCategory(long infoID, DropDownList drpCategoryType)
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                DataTable dtCategoryList = SqlHelper.ReadTable("SELECT PC_bIntCategoryId,PC_vCharCatName FROM Product_Categories_17  WHERE PC_bItIsActive = 1 And PC_bIntInformationId=" + infoID, conString, false);
                drpCategoryType.Items.Clear();

                if (dtCategoryList.Rows.Count > 0)
                {
                    drpCategoryType.DataSource = dtCategoryList;
                    drpCategoryType.DataTextField = "PC_vCharCatName";
                    drpCategoryType.DataValueField = "PC_bIntCategoryId";
                    drpCategoryType.DataBind();
                    drpCategoryType.Items.Insert(0, new ListItem("--Select Category Type--", "0"));
                    return true;
                }
                drpCategoryType.Items.Insert(0, new ListItem("--Select Category Type--", "0"));
                return false;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillCategory", "Product_Sub_Category", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
                return false;
            }
        }

        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            ClearControls();
            btnSave.Attributes["btn-action"] = "New";
            btnSave.InnerHtml = "<i class=\"fa fa-plus-square\"></i> New";
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Attributes["btn-action"] == "Save")
                {
                    string strValidate = "";
                    strValidate = Validate(Convert.ToInt64(cmbInformationType.SelectedItem.Value), txtProductSubCategoryName.Text, txtProductSubCategoryRegional.Text, Convert.ToInt64(drpIsActive.SelectedItem.Value), 0, Convert.ToInt64(drpCategoryType.SelectedItem.Value));
                    if (strValidate == "")
                    {
                        string strPath = CopyFileSafely(txtImgPathMain.Value, txtImageText.Text);
                        strPath = txtImageText.Text.Replace("//", "/");

                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProductSubCatMaster", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                        SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                        SqlHelper.AddInParam("@vCharName_En ", SqlDbType.VarChar, txtProductSubCategoryName.Text),
                         SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, Convert.ToInt64(drpCategoryType.SelectedItem.Value)),
                        SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, txtProductSubCategoryRegional.Text),
                        SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, Convert.ToInt64(drpIsActive.SelectedItem.Value)),
                        SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strPath),
                        SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, 0));
                        long intPCatID;
                        if (dtCatData.Rows.Count > 0)
                        {
                            DataRow dtRowCat = dtCatData.Rows[0];
                            intPCatID = Convert.ToInt64(dtRowCat["PSC_bIntSubCategoryId"].ToString());
                            txtProductCatID.Text = Convert.ToString(intPCatID);
                        }
                        btnSave.Attributes["btn-action"] = "New";
                        btnSave.InnerHtml = "<i class=\"fa fa-plus-square\"></i> New";
                        LockControls(false);

                        int intTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
                        long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                        GlobalFunctions.saveInsertUserAction("Product_Master", "[Product Sub Category Master Insert]:Insertion of Product Category with Id : " + Convert.ToInt64(txtProductCatID.Text) + " with Image : " + txtImageText.Text, intTalukaId, lngCompanyId, Request); //Call to user Action Log
                        SetMessage(false, "Product Sub Category Added Successfully!!!");
                        FillProductInfo();
                        LockControls(false);
                    }
                    else
                    {
                        SetMessage(true, strValidate);
                    }
                }
                else
                {
                    ClearControls();
                    LockControls(true);
                    btnSave.Attributes["btn-action"] = "Save";
                    btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> Save";
                    SetMessage(false, "Save to Add Product Sub Category!!!");
                }

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSave_ServerClick", "Product_Sub_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void btnDeleteProduct_ServerClick(object sender, EventArgs e)
        {
            try
            {

                int intCatID = Convert.ToInt32(txtDelSubCatIDHidden.Value);
                DataTable dtCatData = SqlHelper.ReadTable("spProductDeleteSubCategory", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                     SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                     SqlHelper.AddInParam("@intAmId", SqlDbType.BigInt, intCatID));

                //Delete image from location
                string strDelImg = HiddenDelSubCatPath.Value;
                if (File.Exists(Server.MapPath(strDelImg)))
                    File.Delete(Server.MapPath(strDelImg));

                int intTalukaId = Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID);
                long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                GlobalFunctions.saveInsertUserAction("Product_SubCategory_Master", "[Product Sub Category Master Delete]:Deletion of Product_SubCategory with Id : " + intCatID + " and Image : " + strDelImg, intTalukaId, lngCompanyId, Request); //Call to user Action Log

                SetProductsUpdateMessage(false, "Product Sub Category Deleted Successfully");
                FillProductInfo();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnDeleteProduct_ServerClick", "Product Sub Category", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void btnSaveFilePath_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string pStrDestination = "";
                string strError;

                if (Convert.ToString(ViewState["RowVal"]) != "" || btnSave.Attributes["btn-action"] == "Save")
                {
                    if (FileMainImage.HasFile)
                    {
                        if (!Directory.Exists(pStrDestination))
                            Directory.CreateDirectory(Server.MapPath(GlobalVariables.TempFileProdSubCatPath));

                        pStrDestination = Path.GetFileName(FileMainImage.FileName);
                        int count = 1;
                        if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TempFileProdSubCatPath), pStrDestination)))
                        {
                            while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TempFileProdSubCatPath), pStrDestination)))
                            {
                                pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileMainImage.FileName), count++);
                                pStrDestination = pStrDestination + Path.GetExtension(FileMainImage.FileName);
                            }
                            FileMainImage.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.TempFileProdSubCatPath), pStrDestination));
                        }
                        else
                        {
                            FileMainImage.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.TempFileProdSubCatPath), pStrDestination));
                        }
                        txtImgPathMain.Value = (GlobalVariables.TempFileProdSubCatPath) + "//" + pStrDestination;
                        strError = GlobalFunctions.ChkImageSize(Server.MapPath(txtImgPathMain.Value), 256, 256, 64, 64);

                        if (strError == "")
                        {
                            pStrDestination = Path.GetFileName(FileMainImage.FileName);
                            if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.FileProdSubCatHostPath), pStrDestination)))
                            {
                                int count1 = 1;
                                while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.FileProdSubCatHostPath), pStrDestination)))
                                {
                                    pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileMainImage.FileName), count1++);
                                    pStrDestination = pStrDestination + Path.GetExtension(FileMainImage.FileName);
                                }

                                if (Convert.ToString(ViewState["RowVal"]) == "")
                                {
                                    txtImageText.Text = (GlobalVariables.FileProdSubCatHostPath) + "//" + pStrDestination;
                                    //SetMessage(true, "Image Uploaded Successfully!!!");     
                                }
                                else
                                {
                                    HiddenField ImgCat = (HiddenField)grdProductSubCategory.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath");
                                    ImgCat.Value = txtImgPathMain.Value;
                                    Image Img = (Image)grdProductSubCategory.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat");
                                    Img.ImageUrl = "~/" + txtImgPathMain.Value;
                                    ImgCat.Value = (GlobalVariables.FileProdSubCatHostPath) + "//" + pStrDestination;
                                    SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                                }
                            }
                            else
                            {
                                if (Convert.ToString(ViewState["RowVal"]) == "")
                                {
                                    txtImageText.Text = (GlobalVariables.FileProdSubCatHostPath) + "//" + pStrDestination;
                                    //SetMessage(true, "Image Uploaded Successfully!!!");
                                }
                                else
                                {
                                    HiddenField ImgCat = (HiddenField)grdProductSubCategory.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath");
                                    ImgCat.Value = (GlobalVariables.FileProdSubCatHostPath) + "//" + pStrDestination;
                                    Image Img = (Image)grdProductSubCategory.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat");
                                    Img.ImageUrl = "~/" + txtImgPathMain.Value;
                                    SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                                }
                            }
                        }
                        else
                        {
                            File.Delete(Server.MapPath(txtImgPathMain.Value));
                            txtImgPathMain.Value = "";
                            //SetMessage(true, strError);

                            //Code added by SSK 13-07-2015 for message display of image
                            //Messagebox to display failure of subcategory image upload
                            string message = "Image size should be between 64x64 and 256X256 pixels";
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append(" bootbox.alert('");
                            sb.Append(message);
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());
                        }
                    }
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSaveFilePath_ServerClick", "Product_SubCategory_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        public void LockControls(bool blnFlag)
        {
            txtProductSubCategoryName.Enabled = blnFlag;
            txtProductSubCategoryName.Enabled = blnFlag;
            cmbInformationType.Enabled = blnFlag;
            drpCategoryType.Enabled = blnFlag;
            drpIsActive.Enabled = blnFlag;
            btnSelectICon.Disabled = !blnFlag;
            txtProductSubCategoryRegional.Enabled = blnFlag;

        }

        public void ClearControls()
        {
            txtProductSubCategoryName.Text = "";
            txtProductSubCategoryRegional.Text = "";
            txtImgPathMain.Value = "";
            txtImageText.Text = "";
            txtImgPathMain.Value = "";
            ViewState["RowVal"] = "";
            drpCategoryType.Items.Clear();
            FillInformation();
            // drpIsActive.Items.Insert(0, "<<Select Activation Type>>");
            // drpCategoryType.Items.Insert(0, "<<Select Category Type>>");
            cmbInformationType.SelectedIndex = 0;
            //drpCategoryType.Items.Insert(0, "<<Select Category Type>>");
            // drpCategoryType.SelectedIndex = 0;
            drpIsActive.SelectedIndex = 0;
            // drpCategoryType.SelectedIndex = 0;
            txtProductCatID.Text = "";
        }

        public string Validate(long InfoID, string CatName, string CatRegName, long ActiveID, long intProdID, long CatID)
        {
            string strValidate = "";
            string strDupChk = "";
            if (InfoID == 0)
            {
                strValidate = "Information Not Selected !!!";
            }
            if (CatName == "")
            {
                strValidate += "Category Name Cannot be blank!!";
            }

            if (CatID == 0)
            {
                strValidate += "Category Not Selected";
            }

            if (CatID != 0 && InfoID != 0)
            {
                strDupChk = ChkDuplicate(InfoID, CatID, CatName, CatRegName, intProdID);
                if (strDupChk != "")
                {
                    strValidate += strDupChk;
                }
            }

            if (ActiveID == -1)
            {
                strValidate += "Activation Type not Selected!!";
            }
            return strValidate;
        }
        public string ChkDuplicate(long InfoID, long CatID, string SubCatName, string SubCatRegName, long intProdID)
        {
            try
            {
                string strDuplicate = "";
                TalukaData objTaluka = (TalukaData)Session["TalukaDetails"];
                int TalukaID = objTaluka.TalukaID;
                DataTable dtChkDup = new DataTable();
                string strId = Convert.ToString(TalukaID);


                dtChkDup = SqlHelper.ReadTable("spGetSubProductChkDuplicate", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                             SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                             SqlHelper.AddInParam("@varSubCatName", SqlDbType.VarChar, SubCatName),
                                             SqlHelper.AddInParam("@bintInfoID", SqlDbType.BigInt, InfoID),
                                             SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, CatID),
                                             SqlHelper.AddInParam("@bintId", SqlDbType.BigInt, intProdID));

                if (dtChkDup.Rows.Count > 0)
                {
                    strDuplicate = "Product Category Name already Exsits";
                }

                dtChkDup = SqlHelper.ReadTable("spGetSubProductChkRegDuplicate", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                        SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                        SqlHelper.AddInParam("@varSubCatName", SqlDbType.NVarChar, SubCatRegName),
                                        SqlHelper.AddInParam("@bintInfoID", SqlDbType.BigInt, InfoID),
                                        SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, CatID),
                                        SqlHelper.AddInParam("@bintId", SqlDbType.BigInt, intProdID));
                if (dtChkDup.Rows.Count > 0)
                {
                    strDuplicate += "Product Regional Category Name already Exsits";
                }
                return strDuplicate;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSave_ServerClick", "Product_Sub_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
                return Convert.ToString(pLngErr);

            }
        }

        private void SetMessage(bool pBlnIsError, string pStrError)
        {
            actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            actionInfo.InnerHtml = pStrError;
        }

        private void SetProductsUpdateMessage(bool pBlnIsError, string pStrMessage)
        {
            updateActionDiv.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            updateActionDiv.InnerHtml = pStrMessage;
        }

        public string CopyFileSafely(string pStrSourceFile, String pStrDestination)
        {
            try
            {
                if (pStrSourceFile == "" || Server.MapPath(pStrDestination) == "")
                    return "";

                pStrDestination = Server.MapPath(pStrDestination);
                pStrSourceFile = Server.MapPath(pStrSourceFile);

                if (!Directory.Exists(pStrDestination))
                    Directory.CreateDirectory(Server.MapPath(GlobalVariables.FileProdCatHostPath));


                File.Copy(pStrSourceFile, pStrDestination);
                File.Delete(pStrSourceFile);
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("CopyFileSafely", "Product_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
            return pStrDestination;
        }
        public void FillInformation(DropDownList drpDownInfo)
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                DataTable dtCategoryList = SqlHelper.ReadTable("SELECT IM_vCharInfoName_En,IM_bIntInfoId FROM Information_Master_17  WHERE IM_IsOrder=1", conString, false);

                drpDownInfo.DataSource = dtCategoryList;
                drpDownInfo.DataTextField = "IM_vCharInfoName_En";
                drpDownInfo.DataValueField = "IM_bIntInfoId";
                drpDownInfo.DataBind();
                drpDownInfo.Items.Insert(0, new ListItem("--Select Information Type--", "0"));

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillInformation", "Product_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        protected void grdProductCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox txtSubCatName = (TextBox)grdProductSubCategory.Rows[e.RowIndex].FindControl("txtSubCatName");
                TextBox txtRegSubCatName = (TextBox)grdProductSubCategory.Rows[e.RowIndex].FindControl("txtSubRegName");
                HiddenField ImgCat = (HiddenField)grdProductSubCategory.Rows[e.RowIndex].FindControl("imgPath");
                Label lblSubCatID = (Label)grdProductSubCategory.Rows[e.RowIndex].FindControl("txtSubCatID");
                HiddenField ImgOriginalCategory = (HiddenField)grdProductSubCategory.Rows[e.RowIndex].FindControl("imgOriginalPath");

                HiddenField HiddenCatID = (HiddenField)grdProductSubCategory.Rows[e.RowIndex].FindControl("HiddenCatID");
                DropDownList DrpInfo = (DropDownList)grdProductSubCategory.Rows[e.RowIndex].FindControl("drdlInformation");

                DropDownList DrpIsActive = (DropDownList)grdProductSubCategory.Rows[e.RowIndex].FindControl("drdlActive");

                string strErrorImg;
                string strError;
                strError = Validate(Convert.ToInt64(DrpInfo.SelectedItem.Value), txtSubCatName.Text, txtRegSubCatName.Text, Convert.ToInt64(DrpIsActive.SelectedItem.Value), Convert.ToInt64(lblSubCatID.Text), Convert.ToInt64(HiddenCatID.Value));

                if (strError == "")
                {
                    if (txtImgPathMain.Value != "")
                    {
                        strErrorImg = CopyFileSafely(txtImgPathMain.Value, ImgCat.Value);
                    }
                    DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProductCatMaster", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                              SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                              SqlHelper.AddInParam("@vCharName_En ", SqlDbType.VarChar, txtSubCatName.Text),
                                               SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, Convert.ToInt64(DrpInfo.SelectedItem.Value)),
                                              SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, txtRegSubCatName.Text),
                                              SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, Convert.ToInt64(DrpIsActive.SelectedItem.Value)),
                                              SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, ImgCat.Value.Replace("//", "/")),
                                              SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, Convert.ToInt64(lblSubCatID.Text)));

                    int intTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
                    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                    GlobalFunctions.saveInsertUserAction("Product_SubCategory_Master", "[Product SubCategory Master Update]:Updation of Category with Id : " + Convert.ToInt64(lblSubCatID.Text) + " with Image : " + ImgCat.Value, intTalukaId, lngCompanyId, Request); //Call to user Action Log
                    SetProductsUpdateMessage(false, "Product Sub Category Updated Successfully");

                    if (txtImgPathMain.Value != "")
                    {
                        bool blnFlagDelete = DeleteFile(ImgOriginalCategory.Value);
                    }
                    grdProductSubCategory.EditIndex = -1;
                    FillProductInfo();
                    ClearControls();
                    DrpInfo.Items.Clear();
                    DrpIsActive.Items.Clear();
                    DrpInfo.Visible = false;
                    DrpIsActive.Visible = false;
                    SetProductsUpdateMessage(true, "Product Sub Updated Successfully!!!");
                }
                else
                {
                    SetProductsUpdateMessage(true, strError);

                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("grdProductCategory_RowUpdating1", "Product_Sub_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }
        protected bool DeleteFile(string pstrPath)
        {
            try
            {
                if (pstrPath != "")
                {
                    File.Delete(Server.MapPath(pstrPath));
                }
                return true;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("DeleteFile", "Product_Sub_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
                return false;
            }
        }



        protected void cmbInformationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbInformationType.SelectedIndex != 0)
                {
                    FillCategory(Convert.ToInt64(cmbInformationType.SelectedItem.Value), drpCategoryType);
                }

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("drpCategoryType_SelectedIndexChanged", "Product_SubCategory_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void drdlInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool blnCategory;
                DropDownList drpInfo = (DropDownList)grdProductSubCategory.Rows[Convert.ToInt16(ViewState["RowVal"])].FindControl("drdlInformation");
                DropDownList drpCategory = (DropDownList)grdProductSubCategory.Rows[Convert.ToInt16(ViewState["RowVal"])].FindControl("drdlCategory");
                drpCategory.Items.Clear();
                blnCategory = FillCategory(Convert.ToInt64(drpInfo.SelectedItem.Value), drpCategory);
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("drdlCategory_SelectedIndexChanged", "Product_SubCategory_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }


        }
        private string GetSafeFileNameOnLocation(string pStrCurrentFilePath, string pStrFileLocation)
        {

            if (!Directory.Exists(Server.MapPath(pStrFileLocation)))
                Directory.CreateDirectory(Server.MapPath(pStrFileLocation));

            string mStrUniqueFileLocation = "";
            string mStrFileName = Path.GetFileNameWithoutExtension(pStrCurrentFilePath);
            string mStrFileExtension = Path.GetExtension(pStrCurrentFilePath);
            string mStrUniqueId = Guid.NewGuid().ToString().Replace("-", "");

            mStrUniqueFileLocation = Server.MapPath(String.Format("{0}\\{1}{2}{3}", pStrFileLocation, mStrFileName, mStrUniqueId, mStrFileExtension));

            while (File.Exists(mStrUniqueFileLocation))
            {
                mStrUniqueId = Guid.NewGuid().ToString().Replace("-", "");
                mStrUniqueFileLocation = Server.MapPath(String.Format("{0}\\{1}{2}{3}", pStrFileLocation, mStrFileName, mStrUniqueId, mStrFileExtension));
            }

            return mStrUniqueFileLocation;
        }
        protected void btnImportonPassword_ServerClick(object sender, EventArgs e)
        {

            lblMsgError.Visible = false;
            //Code for importing from excedlsheet data and images
            // txtCode.Text = Convert.ToString(Session["myvalue"]);
            if (txtCode.Text.Trim() == "")
            {
                lblMsgError.Visible = true;
            }
            else
            {
                string strLoginPass = Convert.ToString(Request.Cookies["MStore_Cookie_Password"].Value);
                if (strLoginPass.Equals(txtCode.Text.Trim()) == true)
                {
                    int i;
                    HttpPostedFile f;
                    int introwCount = 0, intFailureCount = 0;
                    List<string> lstCategoryName = new List<string>();
                    List<string> lstCatFilePaths = new List<string>();
                    HttpFileCollection uploadedFiles = Request.Files;

                    string strStoredFilepath = "";
                    string strFileNm = "";
                    long lngAmId = 0;
                    bool blnExcelIsEmpty = false;

                    //Check whether files being selected or not
                    if (FileUploadControl1.HasFile)
                    {
                        try
                        {
                            bool blnHasImage = false;
                            //Read an Excel file sheet for category Details
                            string ext = Path.GetExtension(FileUploadControl1.FileName).ToLower();
                            string path = Server.MapPath(FileUploadControl1.PostedFile.FileName);
                            FileUploadControl1.SaveAs(path);
                            string ConStr = string.Empty;
                            //if (ext.Trim() == ".xls")
                            //{
                            //    ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            //}
                            //else if (ext.Trim() == ".xlsx")
                            //{
                            //    ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //}
                            //else
                            //{
                            //    Div2.Attributes["class"] = "alert alert-info ";
                            //    Div2.InnerHtml = "Please select Only .xls or .xlsx file for uploading !!!";
                            //    return;
                            //}
                            //OleDbConnection con = new OleDbConnection(ConStr);
                            //if (con.State == ConnectionState.Closed)
                            //{
                            //    con.Open();
                            //}

                            //string SelectCategoryquery = "select * from [Sheet2$]";
                            //OleDbCommand cmd = new OleDbCommand(SelectCategoryquery, con);
                            //OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                            //DataTable dtCategoryDetails = new DataTable();
                            //DataSet ds = new DataSet();
                            //da.Fill(dtCategoryDetails);

                            var dtCategoryDetails = new DataTable();

                            if (ext.Trim() == ".csv")
                            {
                                using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(path)), true))
                                {
                                    dtCategoryDetails.Load(csvReader);
                                }
                            }
                            else
                            {
                                Div2.Attributes["class"] = "alert alert-info ";
                                Div2.InnerHtml = "Please select Only .csv file for uploading !!!";
                                return;

                            }

                            for (i = 0; i < uploadedFiles.Count; i++)
                            {
                                f = uploadedFiles[i];
                                blnHasImage = false;


                                //Store Category Description and Images
                                if (f.ContentLength > 0 && (f.FileName == "ProductSubCategory.csv"))
                                {
                                    strFileNm = f.FileName;
                                    if (dtCategoryDetails.Rows.Count <= 0)
                                    {
                                        blnExcelIsEmpty = true;
                                    }

                                    foreach (DataRow drImgColVal in dtCategoryDetails.Rows)
                                    {

                                        //if (strFileNm.Equals(Convert.ToString(drImgColVal[4])) == true)
                                        //{

                                        int intTalId = Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID);

                                        if (!Directory.Exists(Server.MapPath(GlobalVariables.TempFileProdSubCatPath + "/" + intTalId)))
                                            Directory.CreateDirectory(Server.MapPath(GlobalVariables.TempFileProdSubCatPath + "/" + intTalId));


                                        //Code to check whether Category names already exist in db.
                                        lngAmId = 0;
                                        if (Convert.ToString(drImgColVal[0]).Trim() != "")
                                        {
                                            //string strClass = Convert.ToString(drImgColVal[5]);
                                            int IsActive = 0;
                                            if (Convert.ToString(drImgColVal[4]) == "Y") { IsActive = 1; }

                                            TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                                            int intTalukaId = objTal.TalukaID;
                                            string strId = Convert.ToString(intTalukaId);
                                            string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                                            DataTable dtCategoryid = SqlHelper.ReadTable("SELECT IM_bIntInfoId FROM Information_Master_17  WHERE [IM_bItIsActive] = 1 AND [IM_intInfoType]=1 AND [IM_vCharInfoName_En] = @Name ", conString, false,
                                                SqlHelper.AddInParam("@Name", SqlDbType.VarChar,drImgColVal[0].ToString()));

                                            int intCategoryid = -1;
                                            if (dtCategoryid.Rows.Count > 0)
                                            {
                                                DataRow rowCategoryId = dtCategoryid.Rows[0];
                                                intCategoryid = Convert.ToInt32(rowCategoryId["IM_bIntInfoId"]);
                                            }
                                            else
                                            {
                                                Div2.Attributes["class"] = "alert alert-info";
                                                Div2.InnerHtml = "Information Name not exist.";
                                                return;
                                            }
                                            //Code to check whether Category names already exist in db.

                                            string strQuery = "Select PC_bIntCategoryId from Product_Categories_17 where PC_vCharCatName = @Name  AND PC_bItIsActive=1";
                                            DataTable dtCatIdData = SqlHelper.ReadTable(strQuery, false,
                                                SqlHelper.AddInParam("@Name", SqlDbType.VarChar,drImgColVal[1].ToString()));

                                            if (dtCatIdData.Rows.Count > 0)
                                            {
                                                DataRow row = dtCatIdData.Rows[0];
                                                lngAmId = Convert.ToInt32(row["PC_bIntCategoryId"]);
                                                if (lngAmId == 0) lngAmId = -1;
                                            }
                                            else
                                            {
                                                Div2.Attributes["class"] = "alert alert-info";
                                                Div2.InnerHtml = "Product Category Name not exist.";
                                                return;
                                            }


                                            strQuery = "Select [PSC_bIntSubCategoryId] from Product_Sub_Categories_17 where PSC_vCharSubCatName = @Name AND  PSC_bItIsActive=1";
                                            //strQuery = strQuery + " AND IM_bIntCatId = " + intCategoryid;
                                            strQuery = strQuery + " AND PSC_bIntCategoryId = " + lngAmId;

                                            DataTable dtInfoTemp = SqlHelper.ReadTable(strQuery, false,
                                                SqlHelper.AddInParam("@Name", SqlDbType.VarChar,drImgColVal[2].ToString()));
                                            long lngInfoidexisting = 0;
                                            if (dtInfoTemp.Rows.Count > 0)
                                            {
                                                DataRow drInfoId = dtInfoTemp.Rows[0];
                                                lngInfoidexisting = Convert.ToInt64(drInfoId["PSC_bIntSubCategoryId"]);
                                            }
                                            
                                            string strError = "";
                                            if (strFileNm != null)
                                            {

                                                string mStrFileExtension = Path.GetExtension(strFileNm);
                                                if (mStrFileExtension != ".xls")
                                                {
                                                    if (strFileNm == drImgColVal[5].ToString())
                                                    {
                                                        strError = GlobalFunctions.ChkImageSize(Server.MapPath(strFileNm), 256, 256, 64, 64);
                                                        if (strError == "")
                                                        {
                                                            
                                                            string strFilepath = Path.GetFullPath(strFileNm);
                                                            strStoredFilepath = GetSafeFileNameOnLocation(strFilepath, GlobalVariables.TempFileProdSubCatPath + "/" + strId);
                                                            f.SaveAs(strStoredFilepath);
                                                            
                                                            strStoredFilepath = String.Format("{0}/{1}", GlobalVariables.TempFileProdSubCatPath + "/" + strId, Path.GetFileName(strStoredFilepath));
                                                            lstCatFilePaths.Add(strStoredFilepath); //Store file path for future mapping before storing in database

                                                            DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProductSubCatMaster", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                                   SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                                   SqlHelper.AddInParam("@vCharName_En ", SqlDbType.VarChar, Convert.ToString(drImgColVal[2])),
                                                                   SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, lngAmId),
                                                                   SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[3])),
                                                                   SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
                                                                   SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                                                                   SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngInfoidexisting));
                                                                                                                                                                               

                                                        }
                                                        else
                                                        {
                                                            string message = "Image size should be between 63X63 and 256X256 pixels";
                                                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                                            sb.Append("<script type = 'text/javascript'>");
                                                            sb.Append("window.onload=function(){");
                                                            sb.Append(" bootbox.alert('");
                                                            sb.Append(message);
                                                            sb.Append("')};");
                                                            sb.Append("</script>");
                                                            ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProductSubCatMaster", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                                   SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                                   SqlHelper.AddInParam("@vCharName_En ", SqlDbType.VarChar, Convert.ToString(drImgColVal[2])),
                                                                    SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, lngAmId),
                                                                   SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[3])),
                                                                   SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
                                                                   SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                                                                   SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngInfoidexisting));

                                                }
                                            }
                                        }
                                    }
                                }

                                //To insert Category when no images are selected
                                
                            }//Upload files rotate

                            //Code to insert an action log for Category Details insertion here.
                            long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);

                            introwCount = introwCount + lstCatFilePaths.Count - intFailureCount;
                            string strActionMsg = "[Business SubCategory Master] : " + introwCount + " number of rows inserted into database by " + Session["SystemUser"].ToString();
                            GlobalFunctions.saveInsertUserAction("SubCategory_Master", strActionMsg, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID), lngCompanyId, Request); //Call to user Action Log

                            if (intFailureCount > 0)
                            {
                                string strFailureMsgCount = "[Business SubCategory Master] : " + intFailureCount + " rows insertion failed!";

                                //Code for empty field validation
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append("<script type = 'text/javascript'>");
                                sb.Append("window.onload=function(){");
                                sb.Append(" bootbox.alert('");
                                sb.Append(strFailureMsgCount);
                                sb.Append("')};");
                                sb.Append("</script>");
                                ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());
                            }
                            FillProductInfo(); //Call to display SubCategories.

                            if (blnExcelIsEmpty == false)
                            {
                                Div2.Attributes["class"] = "alert alert-info";
                                Div2.InnerHtml = "SubCategory Added Successfully!!!";
                                //if (con.State == ConnectionState.Open)
                                //{
                                //    con.Close();
                                //}
                            }
                            else
                            {
                                Div2.Attributes["class"] = "alert alert-info";
                                Div2.InnerHtml = "Excel Sheet is found to be empty!";
                            }

                            //if (con.State == ConnectionState.Open)
                            //{
                            //    con.Close();
                            //}
                        }
                        catch (Exception exError)
                        {
                            long pLngErr = -1;
                            if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                                pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                            pLngErr = GlobalFunctions.ReportError("btnImportonPassword_ServerClick", "BusinessSubCategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                            Div2.Attributes["class"] = "alert alert-info blink-border";
                            Div2.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
                        }
                    }
                    else
                    {
                        Div2.Attributes["class"] = "alert alert-info ";
                        Div2.InnerHtml = "Please select file for uploading !!!";
                    }
                }
                else
                {
                    Div2.Attributes["class"] = "alert alert-info blink-border";
                    Div2.InnerHtml = "Invalid password";
                }
            }
        }
    }
}