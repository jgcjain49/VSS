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
    public partial class Product_Category : System.Web.UI.Page
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
                    FillInformation();
                    ViewState["RowVal"] = "";
                    LockControls(false);
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }

        public void FillProductInfo()
        {
            try
            {
                DataTable dtProdCat = new DataTable();
                DataTable dtCatData = SqlHelper.ReadTable("spGetProductCategoryDetails", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                             SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)));

                grdProductCategory.DataSource = dtCatData;
                grdProductCategory.DataBind();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillProductInfo", "Product_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
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
                DataTable dtCategoryList = SqlHelper.ReadTable("SELECT IM_vCharInfoName_En,IM_bIntInfoId FROM Information_Master_17  WHERE IM_IsOrder=1  And IM_bItIsActive=1 AND IM_intInfoType=1", conString, false);

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
                pLngErr = GlobalFunctions.ReportError("FillInformation", "Product_Category", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Attributes["btn-action"] == "Save")
                {
                    string strValidate = "";
                    strValidate = Validate(Convert.ToInt64(cmbInformationType.SelectedItem.Value), txtProductCategoryName.Text, txtProductCategoryRegional.Text, Convert.ToInt64(drpIsActive.SelectedItem.Value), 0);
                    if (strValidate == "")
                    {
                        string strPath = CopyFileSafely(txtImgPathMain.Value, txtImageText.Text);
                        strPath = txtImageText.Text.Replace("//", "/");

                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProductCatMaster", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                        SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                        SqlHelper.AddInParam("@vCharName_En ", SqlDbType.VarChar, txtProductCategoryName.Text),
                        SqlHelper.AddInParam("@bintInformaitonID", SqlDbType.BigInt, Convert.ToInt64(cmbInformationType.SelectedItem.Value)),
                        SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, txtProductCategoryRegional.Text),
                        SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, Convert.ToInt64(drpIsActive.SelectedItem.Value)),
                        SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strPath),
                        SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, 0));
                        long intPCatID;
                        if (dtCatData.Rows.Count > 0)
                        {
                            DataRow dtRowCat = dtCatData.Rows[0];
                            intPCatID = Convert.ToInt64(dtRowCat["PC_bIntCategoryId"].ToString());
                            txtProductCatID.Text = Convert.ToString(intPCatID);
                        }
                        btnSave.Attributes["btn-action"] = "New";
                        btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> New";
                        LockControls(false);

                        int intTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
                        long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                        GlobalFunctions.saveInsertUserAction("Product_Master", "[Product Category Master Insert]:Insertion of Product Category with Id : " + Convert.ToInt64(txtProductCatID.Text) + " with Image : " + txtImageText.Text, intTalukaId, lngCompanyId, Request); //Call to user Action Log
                        SetMessage(false, "Product Category Added Successfully!!!");
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
                }

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSave_ServerClick", "Product_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void btnDeleteProduct_ServerClick(object sender, EventArgs e)
        {
            try
            {

                int intCatID = Convert.ToInt32(txtDelCatIDHidden.Value);
                DataTable dtCatData = SqlHelper.ReadTable("[spProductDeleteCategory]", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                     SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                     SqlHelper.AddInParam("@intAmId", SqlDbType.BigInt, intCatID));

                //Delete image from location
                string strDelImg = txtDelCatPath.Value;
                if (File.Exists(Server.MapPath(strDelImg)))
                    File.Delete(Server.MapPath(strDelImg));

                int intTalukaId = Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID);
                long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                GlobalFunctions.saveInsertUserAction("Product_Category_Master", "[Product Category Master Delete]:Deletion of Product_Category with Id : " + intCatID + " and Image : " + strDelImg, intTalukaId, lngCompanyId, Request); //Call to user Action Log
                SetProductsUpdateMessage(false, "Product Category Deleted Successfully");
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
                            Directory.CreateDirectory(Server.MapPath(GlobalVariables.TempFileProdCatPath));

                        pStrDestination = Path.GetFileName(FileMainImage.FileName);
                        int count = 1;
                        if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TempFileProdCatPath), pStrDestination)))
                        {
                            while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TempFileProdCatPath), pStrDestination)))
                            {
                                pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileMainImage.FileName), count++);
                                pStrDestination = pStrDestination + Path.GetExtension(FileMainImage.FileName);
                            }
                            FileMainImage.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.TempFileProdCatPath), pStrDestination));
                        }
                        else
                        {
                            FileMainImage.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.TempFileProdCatPath), pStrDestination));
                        }
                        txtImgPathMain.Value = (GlobalVariables.TempFileProdCatPath) + "//" + pStrDestination;
                        strError = GlobalFunctions.ChkImageSize(Server.MapPath(txtImgPathMain.Value), 256, 256, 64, 64);

                        if (strError == "")
                        {
                            pStrDestination = Path.GetFileName(FileMainImage.FileName);
                            if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.FileProdCatHostPath), pStrDestination)))
                            {
                                int count1 = 1;
                                while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.FileProdCatHostPath), pStrDestination)))
                                {
                                    pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileMainImage.FileName), count1++);
                                    pStrDestination = pStrDestination + Path.GetExtension(FileMainImage.FileName);
                                }

                                if (Convert.ToString(ViewState["RowVal"]) == "")
                                {
                                    txtImageText.Text = (GlobalVariables.FileProdCatHostPath) + "//" + pStrDestination;
                                    //SetMessage(true, "Image Uploaded Successfully!!!");     
                                }
                                else
                                {
                                    HiddenField ImgCat = (HiddenField)grdProductCategory.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath");
                                    ImgCat.Value = txtImgPathMain.Value;
                                    Image Img = (Image)grdProductCategory.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat");
                                    Img.ImageUrl = "~/" + txtImgPathMain.Value;
                                    ImgCat.Value = (GlobalVariables.FileProdCatHostPath) + "//" + pStrDestination;
                                    SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                                }
                            }
                            else
                            {
                                if (Convert.ToString(ViewState["RowVal"]) == "")
                                {
                                    txtImageText.Text = (GlobalVariables.FileProdCatHostPath) + "//" + pStrDestination;
                                    //SetMessage(true, "Image Uploaded Successfully!!!");
                                }
                                else
                                {
                                    HiddenField ImgCat = (HiddenField)grdProductCategory.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath");
                                    ImgCat.Value = (GlobalVariables.FileProdCatHostPath) + "//" + pStrDestination;
                                    Image Img = (Image)grdProductCategory.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat");
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
                            string message = "Image size should be between 64X64 and 256X256 pixels";
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
                pLngErr = GlobalFunctions.ReportError("btnSaveFilePath_ServerClick", "Product_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        public void LockControls(bool blnFlag)
        {
            txtProductCategoryName.Enabled = blnFlag;
            txtProductCategoryRegional.Enabled = blnFlag;
            cmbInformationType.Enabled = blnFlag;
            drpIsActive.Enabled = blnFlag;
            btnSelectICon.Disabled = !blnFlag;
        }

        public void ClearControls()
        {
            txtProductCategoryName.Text = "";
            txtProductCategoryRegional.Text = "";
            txtImgPathMain.Value = "";
            txtImageText.Text = "";
            txtImgPathMain.Value = "";
            ViewState["RowVal"] = "";
            cmbInformationType.SelectedIndex = 0;
            drpIsActive.SelectedIndex = 0;
            txtProductCatID.Text = "";
        }

        public string Validate(long InfoID, string CatName, string CatRegName, long ActiveID, long intProdID)
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
            strDupChk = ChkDuplicate(InfoID, CatName, CatRegName, intProdID);
            if (strDupChk != "")
            {
                strValidate += strDupChk;
            }

            if (ActiveID == -1)
            {
                strValidate += "Activation Type not Selected!!";
            }
            return strValidate;
        }

        public string ChkDuplicate(long InfoID, string CatName, string CatRegName, long intProdID)
        {
            try
            {
                string strDuplicate = "";
                string strq;
                TalukaData objTaluka = (TalukaData)Session["TalukaDetails"];
                int TalukaID = objTaluka.TalukaID;
                DataTable dtChkDup = new DataTable();
                string strId = Convert.ToString(TalukaID);
                if (intProdID == 0)
                {
                    strq = "Select * from  Product_Categories_17 where PC_bIntInformationId =  @InfoID  And PC_vCharCatName =  @catName and PC_bItIsActive=1";
                    dtChkDup = SqlHelper.ReadTable(strq, false, SqlHelper.AddInParam("@InfoID", SqlDbType.BigInt, InfoID), SqlHelper.AddInParam("@catName", SqlDbType.VarChar, CatName));
                }
                else
                {
                    strq = "Select * from  Product_Categories_17 where PC_bIntInformationId =  @InfoID  And PC_vCharCatName =  @catName And PC_bIntCategoryId<> @PCatID And PC_bItIsActive=1";
                    dtChkDup = SqlHelper.ReadTable(strq, false, SqlHelper.AddInParam("@InfoID", SqlDbType.BigInt, InfoID), SqlHelper.AddInParam("@catName", SqlDbType.VarChar, CatName), SqlHelper.AddInParam("@PCatID", SqlDbType.BigInt, intProdID));

                }

                if (dtChkDup.Rows.Count > 0)
                {
                    strDuplicate = "Product Category Name already Exsits";
                    //return false;
                }
                if (intProdID == 0)
                {
                    strq = "Select * from  Product_Categories_17 where PC_bIntInformationId =  @InfoID  And PC_nVarCatName =  @catName And PC_bItIsActive=1";
                    dtChkDup = SqlHelper.ReadTable(strq, false, SqlHelper.AddInParam("@InfoID", SqlDbType.BigInt, InfoID), SqlHelper.AddInParam("@catName", SqlDbType.NVarChar, CatRegName));
                }
                else
                {
                    strq = "Select * from  Product_Categories_17 where PC_bIntInformationId =  @InfoID  And PC_nVarCatName =  @catName And PC_bIntCategoryId<> @PCatID And PC_bItIsActive=1 ";
                    dtChkDup = SqlHelper.ReadTable(strq, false, SqlHelper.AddInParam("@InfoID", SqlDbType.BigInt, InfoID), SqlHelper.AddInParam("@catName", SqlDbType.NVarChar, CatRegName), SqlHelper.AddInParam("@PCatID", SqlDbType.BigInt, intProdID));
                }
                if (dtChkDup.Rows.Count > 0)
                {
                    strDuplicate = "Product Regional Category Name already Exsits";
                }
                return strDuplicate;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSave_ServerClick", "Product_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
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

        protected void grdProductCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdProductCategory.EditIndex = -1;
            ViewState["RowVal"] = "";
            FillProductInfo();
        }

        protected void grdProductCategory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdProductCategory.EditIndex = e.NewEditIndex;
                ClearControls();
                FillProductInfo();
                ViewState["RowVal"] = e.NewEditIndex;
                DropDownList DrpInfo = (DropDownList)grdProductCategory.Rows[e.NewEditIndex].FindControl("drdlInformation");
                DropDownList DrpIsActive = (DropDownList)grdProductCategory.Rows[e.NewEditIndex].FindControl("drdlActive");
                Label lblInfo = (Label)grdProductCategory.Rows[e.NewEditIndex].FindControl("lblInformation");
                Label lblIsActive = (Label)grdProductCategory.Rows[e.NewEditIndex].FindControl("lblIsActive");
                HiddenField lblInfoID = (HiddenField)grdProductCategory.Rows[e.NewEditIndex].FindControl("lblInfoID");
                HtmlButton btnImage = (HtmlButton)grdProductCategory.Rows[e.NewEditIndex].FindControl("btnImage");
                btnImage.Disabled = false;
                FillInformation(DrpInfo);

                if (DrpInfo.Items.Count > 0)
                {
                    DrpInfo.Items.FindByValue(lblInfoID.Value).Selected = true;
                    DrpInfo.SelectedItem.Text = lblInfo.Text;
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
                grdProductCategory.EditIndex = e.NewEditIndex;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("grdProductCategory_RowEditing", "Product_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";


            }
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
                TextBox txtCatName = (TextBox)grdProductCategory.Rows[e.RowIndex].FindControl("txtCatName");
                TextBox txtRegCatName = (TextBox)grdProductCategory.Rows[e.RowIndex].FindControl("txtRegName");
                HiddenField ImgCat = (HiddenField)grdProductCategory.Rows[e.RowIndex].FindControl("imgPath");
                Label lblCatID = (Label)grdProductCategory.Rows[e.RowIndex].FindControl("PC_bIntCategoryId");
                HiddenField ImgOriginalCategory = (HiddenField)grdProductCategory.Rows[e.RowIndex].FindControl("imgOriginalPath");

                DropDownList DrpInfo = (DropDownList)grdProductCategory.Rows[e.RowIndex].FindControl("drdlInformation");
                DropDownList DrpIsActive = (DropDownList)grdProductCategory.Rows[e.RowIndex].FindControl("drdlActive");

                string strErrorImg;
                string strError;
                strError = Validate(Convert.ToInt64(DrpInfo.SelectedItem.Value), txtCatName.Text, txtRegCatName.Text, Convert.ToInt64(DrpIsActive.SelectedItem.Value), Convert.ToInt64(lblCatID.Text));

                if (strError == "")
                {
                    if (txtImgPathMain.Value != "")
                    {
                        strErrorImg = CopyFileSafely(txtImgPathMain.Value, ImgCat.Value);
                    }
                    DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProductCatMaster", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                              SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                              SqlHelper.AddInParam("@vCharName_En ", SqlDbType.VarChar, txtCatName.Text),
                                               SqlHelper.AddInParam("@bintInformaitonID", SqlDbType.BigInt, Convert.ToInt64(DrpInfo.SelectedItem.Value)),
                                              SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, txtRegCatName.Text),
                                              SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, Convert.ToInt64(DrpIsActive.SelectedItem.Value)),
                                              SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, ImgCat.Value.Replace("//", "/")),
                                              SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, Convert.ToInt64(lblCatID.Text)));

                    int intTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
                    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                    GlobalFunctions.saveInsertUserAction("Product_Category_Master", "[Product Category Master Update]:Updation of Category with Id : " + Convert.ToInt64(lblCatID.Text) + " with Image : " + ImgCat.Value, intTalukaId, lngCompanyId, Request); //Call to user Action Log
                    SetProductsUpdateMessage(false, "Category Updated Successfully");

                    if (txtImgPathMain.Value != "")
                    {
                        bool blnFlagDelete = DeleteFile(ImgOriginalCategory.Value);
                    }


                    grdProductCategory.EditIndex = -1;
                    FillProductInfo();
                    ClearControls();
                    DrpInfo.Items.Clear();
                    DrpIsActive.Items.Clear();
                    DrpInfo.Visible = false;
                    DrpIsActive.Visible = false;
                    SetProductsUpdateMessage(true, "Product Updated Successfully!!!");
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
                pLngErr = GlobalFunctions.ReportError("grdProductCategory_RowUpdating1", "Product_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
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
                pLngErr = GlobalFunctions.ReportError("DeleteFile", "Product_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
                return false;
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

        public void ResizeImage(string OriginalFile, string NewFile, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(OriginalFile);

            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (OnlyResizeIfWider)
            {
                if (FullsizeImage.Width <= NewWidth)
                {
                    NewWidth = FullsizeImage.Width;
                }
            }

            int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
            if (NewHeight > MaxHeight)
            {
                // Resize with height instead
                NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                NewHeight = MaxHeight;
            }

            System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            FullsizeImage.Dispose();

            // Save resized picture
            NewImage.Save(NewFile);
        }

        private string ChkImageSize(string pstrImagePath)
        {
            try
            {
                string strMsg = "";
                System.Drawing.Bitmap image = new System.Drawing.Bitmap(pstrImagePath);
                int originalWidth = image.Width;
                int originalHeight = image.Height;

                if (originalWidth > 25 || originalHeight > 25)
                {
                    image.Dispose();
                    return ("Image Upload Failed Image Size is Greater than maximum size 25!!! Select Appropriate Image to Continue");
                }
                else if (originalWidth > 25 && originalWidth > 25)
                {
                    image.Dispose();
                    return ("Image Upload Failed Image Size is Greater than maximum size 25!!! Select Appropriate Image to Continue");
                }
                else if (originalWidth < 16 && originalWidth < 16)
                {
                    image.Dispose();
                    return (" Image Upload Failed Image Size is smaller than maximum size 16!!! Select Appropriate Image to Continue");
                }

                else if (originalWidth < 16 || originalWidth < 16)
                {
                    image.Dispose();
                    return (" Image Upload Failed Image Size is smaller than maximum size 16!!! Select Appropriate Image to Continue");
                }
                image.Dispose();
                if (strMsg.Trim() != "")
                {
                    ResizeImage(pstrImagePath, pstrImagePath, 25, 25, false);
                    strMsg = "";
                }
                return strMsg;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("ChkImageSize", "SubCategoryBusiness", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
            return "";
        }

        protected void btnImportonPassword_ServerClick(object sender, EventArgs e)
        {
            //Code on click of upload in password box
            lblMsgError.Visible = false;
            //Code for importing from excedlsheet data and images
            // txtCode.Text = Convert.ToString(Session["myvalue"]);
            if (txtCode.Text.Trim() == "")
            {
                lblMsgError.Visible = true;
            }
            else
            {
                string strLoginPass = Request.Cookies["MStore_Cookie_Password"].Value;
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

                            //string SelectCategoryquery = "select * from [Sheet1$]";
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
                                if (f.ContentLength > 0 && (f.FileName == "ProductCategory.csv"))
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

                                        if (!Directory.Exists(Server.MapPath(GlobalVariables.FileProdCatHostPath + "/" + intTalId)))
                                            Directory.CreateDirectory(Server.MapPath(GlobalVariables.FileProdCatHostPath + "/" + intTalId));


                                        //Code to check whether Category names already exist in db.
                                        lngAmId = 0;
                                        if (Convert.ToString(drImgColVal[0]).Trim() != "")
                                        {
                                            //string strClass = Convert.ToString(drImgColVal[5]);
                                            int IsActive = 0;
                                            if (Convert.ToString(drImgColVal[3]) == "Y") { IsActive = 1; }

                                            TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                                            int intTalukaId = objTal.TalukaID;
                                            string strId = Convert.ToString(intTalukaId);
                                            string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                                            DataTable dtCategoryid = SqlHelper.ReadTable("SELECT IM_bIntInfoId FROM Information_Master_17  WHERE [IM_bItIsActive] = 1 AND [IM_intInfoType]=1 AND [IM_vCharInfoName_En] =@Name ", conString, false, SqlHelper.AddInParam("@Name", SqlDbType.VarChar, drImgColVal[0].ToString()));

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
                                            if (intCategoryid != -1)
                                            {
                                                string strQuery = "Select [PC_bIntCategoryId] from Product_Categories_17 where PC_vCharCatName =@Name AND PC_bItIsActive=1";
                                                DataTable dtCatIdData = SqlHelper.ReadTable(strQuery, false,SqlHelper.AddInParam("@Name",SqlDbType.VarChar,drImgColVal[1].ToString()));

                                                if (dtCatIdData.Rows.Count > 0)
                                                {
                                                    DataRow row = dtCatIdData.Rows[0];
                                                    lngAmId = Convert.ToInt32(row["PC_bIntCategoryId"]);
                                                }
                                                //string strClass = Convert.ToString(drImgColVal[4]);
                                                //if (strFileNm.Equals(drImgColVal[5].ToString()) == true)
                                                //{
                                                //string strFilepath = Path.Combine(Path.GetPathRoot(strFileNm),Path.GetFileName(strFileNm));

                                                string strError = "";
                                                if (strFileNm != null)
                                                {

                                                    string mStrFileExtension = Path.GetExtension(strFileNm);
                                                    if (mStrFileExtension != ".xls")
                                                    {

                                                        if (strFileNm == drImgColVal[4].ToString())
                                                        {
                                                            strError = GlobalFunctions.ChkImageSize(Server.MapPath(strFileNm), 256, 256, 64, 64);
                                                           // strError = ChkImageSize(strStoredFilepath);
                                                            if (strError == "")
                                                            {

                                                                string strFilepath = Path.GetFullPath(strFileNm);
                                                                strStoredFilepath = GetSafeFileNameOnLocation(strFilepath, GlobalVariables.FileProdCatHostPath + "/" + strId);
                                                                f.SaveAs(strStoredFilepath);

                                                                strStoredFilepath = String.Format("{0}/{1}", GlobalVariables.FileProdCatHostPath + "/" + strId, Path.GetFileName(strStoredFilepath));
                                                                lstCatFilePaths.Add(strStoredFilepath); //Store file path for future mapping before storing in database


                                                                //Code to insert or modify Category Business master.

                                                                DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProductCatMaster", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                                    SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                                      SqlHelper.AddInParam("@vCharName_En ", SqlDbType.VarChar, Convert.ToString(drImgColVal[1])),
                                                                      SqlHelper.AddInParam("@bintInformaitonID", SqlDbType.BigInt, intCategoryid),
                                                                      SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[2])),
                                                                      SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
                                                                      SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                                                                      SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));



                                                                //DataTable dtCatData = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
                                                                //    SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                                //    SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[1])),
                                                                //    SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[2])),
                                                                //    SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
                                                                //    SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, (strClass.Trim() == "" ? null : strClass)),
                                                                //    SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                                                                //    SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, intCategoryid),
                                                                //    SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                                                //    SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));



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
                                                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProductCatMaster", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                                   SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                                     SqlHelper.AddInParam("@vCharName_En ", SqlDbType.VarChar, Convert.ToString(drImgColVal[1])),
                                                                     SqlHelper.AddInParam("@bintInformaitonID", SqlDbType.BigInt, intCategoryid),
                                                                     SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[2])),
                                                                     SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
                                                                     SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                                                                     SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));



                                                    //    DataTable dtCatData = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
                                                    //  SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                    //  SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[1])),
                                                    //  SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[2])),
                                                    //  SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
                                                    //  SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, (strClass.Trim() == "" ? null : strClass)),
                                                    //  SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, null),
                                                    //  SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, intCategoryid),
                                                    //  SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                                    //  SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));

                                                    }
                                                }

                                            }
                                        }
                                    }
                                }

                               
                            }

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
                                Div2.InnerHtml = "Product Category Added Successfully!!!";
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