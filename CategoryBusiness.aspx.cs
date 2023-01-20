using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.HtmlControls.HtmlControl;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using LumenWorks.Framework.IO.Csv;

namespace Admin_CommTrex
{
    public partial class CategoryBusiness : System.Web.UI.Page
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
                    showgrid();
                    LockControls(false);
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdProducts.EditIndex = e.NewEditIndex;
            ViewState["RowVal"] = e.NewEditIndex;
            showgrid();
            grdProducts.Columns[5].Visible = true;
        }

        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox txtCatName = (TextBox)grdProducts.Rows[e.RowIndex].FindControl("txtCatName");
                TextBox txtRegCatName = (TextBox)grdProducts.Rows[e.RowIndex].FindControl("txtRegName");
                HiddenField ImgCat = (HiddenField)grdProducts.Rows[e.RowIndex].FindControl("imgPath");
                Label lblCatID = (Label)grdProducts.Rows[e.RowIndex].FindControl("lblCatID");
                HiddenField CatLogo = (HiddenField)grdProducts.Rows[e.RowIndex].FindControl("LogoName");
                HiddenField ImgOriginalCategory = (HiddenField)grdProducts.Rows[e.RowIndex].FindControl("imgOriginalPath"); //Added by SSK to delete old image

                string strErrorImg = "";
                String strError = ValidateProductData(txtCatName.Text, Convert.ToString(ViewState["ImgPath"]), Convert.ToString(ViewState["ImgLogo"]), txtRegCatName.Text, Convert.ToInt64(lblCatID.Text));

                if (Convert.ToString(ViewState["ImgPath"]) != "")
                {
                    strErrorImg = CopyFileSafely(txtImgPathMain.Value, Convert.ToString(ViewState["ImgPath"]));
                }
                else if (Convert.ToString(ViewState["ImgLogo"]) != "")
                {
                    strErrorImg = (Convert.ToString(ViewState["ImgLogo"]));
                }
                else
                {
                    strErrorImg = "No Change";
                    ViewState["ImgPath"] = ImgOriginalCategory.Value;
                    ViewState["ImgLogo"] = CatLogo.Value;
                }

                if (Convert.ToString(ViewState["ImgPath"]) == "")
                {
                    // ViewState["ImgPath"] = GlobalVariables.NoImagePath;
                }

                //Code for deletion of existing image file.
                if (strErrorImg.Trim() != "No Change")
                {
                    if (File.Exists(Server.MapPath(ImgOriginalCategory.Value)))
                        File.Delete(Server.MapPath(ImgOriginalCategory.Value));
                }

                if (strError == "" && strErrorImg != "")
                {
                    DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateCatMasterBusiness", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                         SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                         SqlHelper.AddInParam("@vCharName_En ", SqlDbType.NVarChar, txtCatName.Text),
                                         SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, txtRegCatName.Text),
                                         SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, 1),
                                         SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, Convert.ToString(ViewState["ImgLogo"])),
                                         SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, Convert.ToString(ViewState["ImgPath"]).Replace("//", "/")),
                                         SqlHelper.AddInParam("@intEntryType ", SqlDbType.Int, 1),
                                         SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, Convert.ToInt64(lblCatID.Text)));

                    SetProductsUpdateMessage(false, "Category Updated Successfully");

                    int intTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
                    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                    if (Convert.ToString(ViewState["ImgPath"]) != "")
                        GlobalFunctions.saveInsertUserAction("Category_Master", "[Business Category Master Update]:Updation of Category with Id : " + Convert.ToInt64(lblCatID.Text) + " with Image : " + Convert.ToString(ViewState["ImgPath"]), intTalukaId, lngCompanyId, Request); //Call to user Action Log
                    else
                        GlobalFunctions.saveInsertUserAction("Category_Master", "[Business Category Master Update]:Updation of Category with Id : " + Convert.ToInt64(lblCatID.Text) + " with Image : " + Convert.ToString(ViewState["ImgLogo"]), intTalukaId, lngCompanyId, Request); //Call to user Action Log
                    SetProductsUpdateMessage(false, "Category Updated Successfully");

                    if (txtImgPathMain.Value != "")
                    {
                        bool blnFlagDelete = DeleteFile((txtImgPathMain.Value));
                    }
                    grdProducts.EditIndex = -1;
                    showgrid();
                    ClearAll();
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
                pLngErr = GlobalFunctions.ReportError("GridView1_RowUpdating1", "CategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdProducts.EditIndex = -1;
            showgrid();
            ClearAll();
            if (txtImgPathMain.Value != "")
            {
                bool blnFlagDelete = DeleteFile(txtImgPathMain.Value);
            }
        }

        private void SetMessage(bool pBlnIsError, string pStrError)
        {
            actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            actionInfo.InnerHtml = pStrError;
        }

        public void showgrid()
        {

            DataTable dtCatData = SqlHelper.ReadTable("spGetCategoryDetailsOnType", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                     SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                     SqlHelper.AddInParam("@intEntryType ", SqlDbType.Int, 1));

            grdProducts.DataSource = dtCatData;
            grdProducts.DataBind();
            grdProducts.Columns[5].Visible = false;

        }

        private void SetProductsUpdateMessage(bool pBlnIsError, string pStrMessage)
        {
            updateActionDiv.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            updateActionDiv.InnerHtml = pStrMessage;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Attributes["btn-action"] == "Save")
                {
                    string strErrror = ValidateProductData();
                    string strPath = "";
                    string strLogoPath = "";
                    if (strErrror == "")
                    {
                        if (txtImageText.Text.Contains("fa"))
                        {
                            strLogoPath = txtImageText.Text;
                            //strPath = GlobalVariables.NoImagePath;
                        }
                        else
                        {
                            strPath = CopyFileSafely(txtImgPathMain.Value, txtImageText.Text);
                            strPath = txtImageText.Text.Replace("//", "/");
                            // strLogoPath = "fa-question-circle";
                        }
                        // if (strPath != "")
                        // {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateCatMasterBusiness", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                              SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                              SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, txtCategoryName.Text),
                                              SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, txtCategoryRegional.Text),
                                              SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, 1),
                                              SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, strLogoPath),
                                              SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strPath),
                                              SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                              SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, 0));

                        DataRow row = dtCatData.Rows[0];
                        txtCategoryID.Text = row["CM_bIntCatId"].ToString();
                        btnSave.Attributes["btn-action"] = "New";
                        btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> New";

                        int intTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
                        long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                        if (strLogoPath.Trim() == "")
                            GlobalFunctions.saveInsertUserAction("Category_Master", "[Business Category Master Insert]:Insertion of Category with Id : " + Convert.ToInt64(txtCategoryID.Text) + " with Icon : " + strLogoPath, intTalukaId, lngCompanyId, Request); //Call to user Action Log
                        else
                            GlobalFunctions.saveInsertUserAction("Category_Master", "[Business Category Master Insert]:Insertion of Category with Id : " + Convert.ToInt64(txtCategoryID.Text) + " with Image : " + strPath, intTalukaId, lngCompanyId, Request); //Call to user Action Log

                        SetMessage(false, "Category Added Successfully!!!");
                        grdProducts.EditIndex = -1;
                        showgrid();
                        // }
                    }
                    else
                    {
                        SetMessage(false, strErrror);
                    }
                }
                else
                {
                    btnSave.Attributes["btn-action"] = "Save";
                    btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> Save";
                    ClearAll();

                    LockControls(true);
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSave_Click", "CategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }
        private String ValidateProductData()
        {
            string mStrValidation = "";
            if (txtCategoryName.Text == "")
            {
                mStrValidation += "- Please enter Category name" + Environment.NewLine;
                return (mStrValidation);
            }
            if (txtImageText.Text == "")
            {
                mStrValidation += "- Please Select Category Image" + Environment.NewLine;
                return (mStrValidation);
            }
            mStrValidation = CheckDuplicateValues(txtCategoryName.Text, txtCategoryRegional.Text, -1);

            return (mStrValidation);
        }
        private String ValidateProductData(string strCatName, string CatImage, string Catlogo, string strCatRegName, long intCatID)
        {
            string mStrValidation = "";
            if (strCatName == "")
            {
                mStrValidation += "- Please enter Category name" + Environment.NewLine;
            }
            if (CatImage == "" && Catlogo == "")
            {
                mStrValidation += "No Images/Icon Selected" + Environment.NewLine;
            }
            mStrValidation = CheckDuplicateValues(strCatName, strCatRegName, intCatID);
            return (mStrValidation);
        }
        private void ClearAll()
        {
            txtCategoryName.Text = "";
            txtImageText.Text = "";
            txtCategoryRegional.Text = "";
            txtCategoryID.Text = "";
            SetMessage(false, "Press Save to store Category");
            txtImgPathMain.Value = "";
            txtLogo.Text = "";

            ViewState["ImgLogo"] = "";
            ViewState["ImgPath"] = "";
            ViewState["RowVal"] = "";
        }
        private void LockControls(bool pflag)
        {
            txtCategoryName.Enabled = pflag;
            txtCategoryRegional.Enabled = pflag;
            txtImageText.Enabled = pflag;
            btnSelectICon.Disabled = !pflag;
            btnSelectImage.Disabled = !pflag;
        }
        protected void btnDeleteProduct_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int intCatID = Convert.ToInt32(txtDelCatIDHidden.Value);
                DataTable dtCatData = SqlHelper.ReadTable("spDeleteCategoryBusiness", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                     SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                     SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                     SqlHelper.AddInParam("@intAmId", SqlDbType.BigInt, intCatID));
                //Delete image from location
                string strDelImg = txtDelCatPath.Value;
                if (File.Exists(Server.MapPath(strDelImg)))
                    File.Delete(Server.MapPath(strDelImg));

                int intTalukaId = Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID);
                long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                GlobalFunctions.saveInsertUserAction("Category_Master", "[Business Category Master Delete]:Deletion of Category with Id : " + intCatID + " and Image : " + strDelImg, intTalukaId, lngCompanyId, Request); //Call to user Action Log

                SetProductsUpdateMessage(false, "Category Deleted Successfully");
                showgrid();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnDeleteProduct_ServerClick", "CategoryBusiness", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }
        protected void btnSaveFilePath_ServerClick(object sender, EventArgs e)
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strTalukaId = Convert.ToString(intTalukaId);

                string pStrDestination = "";
                string strError;
                if (FileMainImage.HasFile)
                {
                    if (!Directory.Exists(Server.MapPath(GlobalVariables.TemporaryPath)))
                        Directory.CreateDirectory(Server.MapPath(GlobalVariables.TemporaryPath));

                    if (!Directory.Exists(Server.MapPath(GlobalVariables.FileHostPath + "//" + strTalukaId)))
                        Directory.CreateDirectory(Server.MapPath(GlobalVariables.FileHostPath + "//" + strTalukaId));

                    pStrDestination = Path.GetFileName(FileMainImage.FileName);
                    int count = 1;
                    if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination)))
                    {
                        while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination)))
                        {
                            pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileMainImage.FileName), count++);
                            pStrDestination = pStrDestination + Path.GetExtension(FileMainImage.FileName);
                        }
                        FileMainImage.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination));
                    }
                    else
                    {
                        FileMainImage.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination));
                    }
                    txtImgPathMain.Value = (GlobalVariables.TemporaryPath) + "//" + pStrDestination;
                    strError = ChkImageSize(Server.MapPath(txtImgPathMain.Value));
                    if (strError == "")
                    {
                        pStrDestination = Path.GetFileName(FileMainImage.FileName);

                        if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.FileHostPath + "//" + strTalukaId), pStrDestination)))
                        {
                            int count1 = 1;
                            while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.FileHostPath + "//" + strTalukaId), pStrDestination)))
                            {
                                pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileMainImage.FileName), count1++);
                                pStrDestination = pStrDestination + Path.GetExtension(FileMainImage.FileName);
                            }

                            if (Convert.ToString(ViewState["RowVal"]) == "")
                            {
                                txtImageText.Text = (GlobalVariables.FileHostPath) + "//" + strTalukaId + "//" + pStrDestination;
                                SetMessage(true, "Image Uploaded Successfully!!!");
                                txtLogo.Text = "";
                            }
                            else
                            {
                                HiddenField ImgCat = (HiddenField)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath");
                                HiddenField LogoName = (HiddenField)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("LogoName");
                                Image Img = (Image)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat");
                                Img.ImageUrl = "~/" + txtImgPathMain.Value;
                                Img.Visible = true;
                                LogoName.Value = "";
                                ImgCat.Value = txtImgPathMain.Value;
                                ViewState["ImgLogo"] = "";

                                HtmlButton btnLogo = (HtmlButton)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow");
                                btnLogo.Visible = false;

                                ViewState["ImgPath"] = (GlobalVariables.FileHostPath) + "//" + strTalukaId + "//" + pStrDestination;
                                SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                            }
                        }
                        else
                        {
                            if (Convert.ToString(ViewState["RowVal"]) == "")
                            {
                                txtImageText.Text = (GlobalVariables.FileHostPath) + "//" + strTalukaId + "//" + pStrDestination;
                                SetMessage(true, "Image Uploaded Successfully!!!");
                            }
                            else
                            {
                                HiddenField ImgCat = (HiddenField)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath");
                                HiddenField LogoName = (HiddenField)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("LogoName");
                                Image Img = (Image)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat");
                                Img.ImageUrl = "~/" + txtImgPathMain.Value;
                                Img.Visible = true;
                                LogoName.Value = "";
                                ImgCat.Value = txtImgPathMain.Value;

                                ViewState["ImgLogo"] = "";
                                HtmlButton btnLogo = (HtmlButton)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow");
                                btnLogo.Visible = false;
                                ViewState["ImgPath"] = (GlobalVariables.FileHostPath) + "//" + strTalukaId + "//" + pStrDestination;
                                SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                            }
                        }
                    }
                    else
                    {
                        File.Delete(Server.MapPath(txtImgPathMain.Value));
                        //SetMessage(true, strError);

                        //Code added by SSK 13-07-2015 for message display of image
                        //Messagebox to display failure of subcategory image upload
                        string message = "Image size should be between 32X32 and 43X43 pixels";
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
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSaveFilePath_ServerClick", "CategoryBusiness", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
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
                    Directory.CreateDirectory(Server.MapPath(GlobalVariables.TemporaryPath));


                File.Copy(pStrSourceFile, pStrDestination);
                File.Delete(pStrSourceFile);
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("CopyFileSafely", "CategoryBusiness", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
            return pStrDestination;
        }

        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            ClearAll();
            LockControls(true);
            SetMessage(false, "Add New to Add Categories");

        }


        /*private void SetImageDetails(bool pBlnIsError, string pStrMessage)
        {
            divImageDetails.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            divImageDetails.InnerHtml = pStrMessage;
        }*/

        private string ChkImageSize(string pstrImagePath)
        {
            try
            {
                string strMsg = "";
                System.Drawing.Bitmap image = new System.Drawing.Bitmap(pstrImagePath);
                int originalWidth = image.Width;
                int originalHeight = image.Height;

                if (originalWidth > 43 || originalHeight > 43)
                {
                    image.Dispose();
                    return ("Image Upload Failed Image Size is Greater than maximum size 43!!! Select Appropriate Image to Continue");
                }
                else if (originalWidth > 43 && originalWidth > 43)
                {
                    image.Dispose();
                    return ("Image Upload Failed Image Size is Greater than maximum size 43!!! Select Appropriate Image to Continue");
                }
                else if (originalWidth < 32 && originalWidth < 32)
                {
                    image.Dispose();
                    return (" Image Upload Failed Image Size is smaller than maximum size 32!!! Select Appropriate Image to Continue");
                }

                else if (originalWidth < 32 || originalWidth < 32)
                {
                    image.Dispose();
                    return (" Image Upload Failed Image Size is smaller than maximum size 32!!! Select Appropriate Image to Continue");
                }
                image.Dispose();
                if (strMsg.Trim() != "")
                {
                    ResizeImage(pstrImagePath, pstrImagePath, 42, 42, false);
                    strMsg = "";
                }
                return strMsg;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("ChkImageSize", "CategoryBusiness", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
            return "";

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

        private string CheckDuplicateValues(string strCatName, string strCatRegName, long CatID)
        {
            try
            {
                DataTable dtCatData = SqlHelper.ReadTable("spChkCatNameDuplicationBusiness", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                       SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                       SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                       SqlHelper.AddInParam("@vCharCatName", SqlDbType.VarChar, strCatName));

                if (dtCatData.Rows.Count > 0)
                {
                    DataRow dtrow = dtCatData.Rows[0];
                    long intCatID = Convert.ToInt64(dtrow["CM_bIntCatId"].ToString());
                    if (intCatID != CatID)
                    {
                        return ("Category Regional  Name Already Exists!!");
                    }

                }
                if (strCatRegName != "")
                {
                    dtCatData = SqlHelper.ReadTable("spChkCatRegNameDuplicationBusiness", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                SqlHelper.AddInParam("@vCharRegionalCatName", SqlDbType.NVarChar, strCatRegName));

                    if (dtCatData.Rows.Count > 0)
                    {
                        DataRow dtrow = dtCatData.Rows[0];
                        long intCatID = Convert.ToInt64(dtrow["CM_bIntCatId"].ToString());
                        if (intCatID != CatID)
                        {
                            return ("Category Regional  Name Already Exists!!");
                        }

                    }
                }

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("CheckDuplicateValues", "CategoryBusiness", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }

            return "";
        }

        protected void btnLogoSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(ViewState["RowVal"]) == "")
                {
                    if (txtImageText.Text != "")
                    {
                        SetMessage(true, "Class Selected Successfully OverWriting Previous Selection");
                        txtImageText.Text = txtLogo.Text;
                        txtImgPathMain.Value = "";
                    }
                    else
                    {
                        SetMessage(true, "Class Selected Successfully !!!");
                        txtImageText.Text = txtLogo.Text;
                    }
                }
                else
                {
                    Image Img = (Image)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat");
                    Img.ImageUrl = "";
                    Img.Visible = false;

                    HiddenField ImgCat = (HiddenField)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath");
                    HiddenField LogoName = (HiddenField)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("LogoName");
                    LogoName.Value = txtLogo.Text;
                    ImgCat.Value = "";

                    //Img.ImageUrl = "~/" + GlobalVariables.NoImagePath;
                    SetMessage(true, "Class Selected Successfully OverWriting Previous Selection");
                    ViewState["ImgPath"] = "";
                    HtmlButton btnLogo = (HtmlButton)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow");
                    btnLogo.Visible = true;
                    if ((txtImgPathMain.Value) != "")
                    {
                        txtImgPathMain.Value = "";
                        DeleteFile(txtImgPathMain.Value);
                    }

                    ViewState["ImgLogo"] = txtLogo.Text;
                    string strLogo = "\"fa " + Convert.ToString(ViewState["ImgLogo"]) + "\"";
                    btnLogo.InnerHtml = "<i class=" + strLogo + "</i>";
                    txtImageText.Text = "";
                    SetProductsUpdateMessage(true, "Class Selected Successfully!!!");

                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnLogoSave_ServerClick", "CategoryBusiness", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
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
                pLngErr = GlobalFunctions.ReportError("DeleteFile", "CategoryBusiness", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
                return false;
            }
        }


        protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField ImgCat = (HiddenField)e.Row.FindControl("imgPath");
                HiddenField LogoName = (HiddenField)e.Row.FindControl("LogoName");
                HtmlButton btnLogo = (HtmlButton)e.Row.FindControl("btnLogoShow");
                Image Img = (Image)e.Row.FindControl("ImgCat");
                if (ImgCat.Value == "")
                {
                    Img.Visible = false;
                }
                if (LogoName.Value == "fa " || LogoName.Value == "")
                {
                    btnLogo.Visible = false;
                }


            }


        }

        protected void btnRead_ServerClick(object sender, EventArgs e)
        {
            //CategoryDetailImagePath
            int i;
            HttpPostedFile f;
            int introwCount = 0, intFailureCount = 0;
            List<string> lstCategoryName = new List<string>();
            List<string> lstCatFilePaths = new List<string>();
            HttpFileCollection uploadedFiles = Request.Files;

            string strStoredFilepath = "";
            string strFileNm = "";
            long lngAmId = 0;

            try
            {
                bool blnHasImage = false;
                //Read an Excel file sheet for category Details
                string ext = Path.GetExtension(FileUploadControl.FileName).ToLower();
                string path = Server.MapPath(FileUploadControl.PostedFile.FileName);
                FileUploadControl.SaveAs(path);
                string ConStr = string.Empty;
                if (ext.Trim() == ".xls")
                {
                    ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (ext.Trim() == ".xlsx")
                {
                    ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }

                OleDbConnection con = new OleDbConnection(ConStr);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string SelectCategoryquery = "select * from [Sheet1$]";
                OleDbCommand cmd = new OleDbCommand(SelectCategoryquery, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable dtCategoryDetails = new DataTable();
                DataSet ds = new DataSet();
                da.Fill(dtCategoryDetails);

                for (i = 0; i < uploadedFiles.Count - 1; i++)
                {
                    f = uploadedFiles[i];
                    blnHasImage = false;
                    //Store Category Description and Images
                    if (f.ContentLength > 0 && f.FileName != "MStoreInfo.xlsx")
                    {
                        strFileNm = f.FileName;
                        foreach (DataRow drImgColVal in dtCategoryDetails.Rows)
                        {
                            //Code to check whether Category names already exist in db.
                            lngAmId = 0;

                            string strQuery = "Select CM_bIntCatId from Category_Master_17 where CM_vCharName_En Like @Name AND CM_iNtEntryType=1";
                            DataTable dtCatIdData = SqlHelper.ReadTable(strQuery, false,
                                SqlHelper.AddInParam("@Name", SqlDbType.VarChar, drImgColVal[0].ToString()));

                            if (dtCatIdData.Rows.Count > 0)
                            {
                                DataRow row = dtCatIdData.Rows[0];
                                lngAmId = Convert.ToInt32(row["CM_bIntCatId"]);
                            }

                            if (strFileNm.Equals(drImgColVal[3].ToString()) == true)
                            {
                                //string strFilepath = Path.Combine(Path.GetPathRoot(strFileNm),Path.GetFileName(strFileNm));
                                string strFilepath = Path.GetFullPath(strFileNm);
                                strStoredFilepath = GetSafeFileNameOnLocation(strFilepath, GlobalVariables.CategoryDetailImagePath);
                                f.SaveAs(strStoredFilepath);

                                string strError = ChkImageSize(strStoredFilepath);

                                blnHasImage = true;
                                if (strError.Trim() == "")
                                {
                                    //Insert category details with an image of an appropriate size
                                    strStoredFilepath = String.Format("{0}/{1}", GlobalVariables.CategoryDetailImagePath, Path.GetFileName(strStoredFilepath));
                                    lstCatFilePaths.Add(strStoredFilepath); //Store file path for future mapping before storing in database

                                    //Code to insert or modify Category Business master.
                                    DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateCatMasterBusiness", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                    SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                    SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, drImgColVal[0].ToString()),
                                    SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, drImgColVal[1].ToString()),
                                    SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, 1),
                                    SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, drImgColVal[2].ToString()),
                                    SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                                    SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                    SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));

                                    DataRow row = dtCatData.Rows[0];
                                    int intCatid = Convert.ToInt32(row["CM_bIntCatId"]);
                                    //break;
                                }
                                else
                                {
                                    //Insert category details without an image due to invalid size
                                    intFailureCount = intFailureCount + 1;
                                    strStoredFilepath = null;
                                    //Code to insert or modify Category Business master.
                                    DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateCatMasterBusiness", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                    SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                    SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, drImgColVal[0].ToString()),
                                    SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, drImgColVal[1].ToString()),
                                    SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, 1),
                                    SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, drImgColVal[2].ToString()),
                                    SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                                    SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                    SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));
                                    DataRow row = dtCatData.Rows[0];
                                    int intCatid = Convert.ToInt32(row["CM_bIntCatId"]);
                                    //break;
                                }

                            }
                        }  //End of Store Category Description and Images
                    }

                    //To insert Category when no images are selected
                    if (blnHasImage == false && f.FileName == "MStoreInfo.xlsx")
                    {
                        foreach (DataRow drImgColVal in dtCategoryDetails.Rows)
                        {
                            //Code to check whether Category names already exist in db.
                            lngAmId = 0;

                            string strQuery = "Select CM_bIntCatId,CM_vCharCatImgPath from Category_Master_17 where CM_vCharName_En Like @Name AND CM_iNtEntryType=0";
                            DataTable dtCatIdData = SqlHelper.ReadTable(strQuery, false,
                                SqlHelper.AddInParam("@Name", SqlDbType.VarChar, drImgColVal[0].ToString()));
                            strStoredFilepath = null;
                            if (dtCatIdData.Rows.Count > 0)
                            {
                                DataRow row1 = dtCatIdData.Rows[0];
                                lngAmId = Convert.ToInt32(row1["CM_bIntCatId"]);
                                if (row1["CM_vCharCatImgPath"].ToString().Trim() != "")
                                { strStoredFilepath = row1["CM_vCharCatImgPath"].ToString(); }
                            }
                            //Code to insert or modify Category Business master with fontawesome class
                            DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateCatMasterBusiness", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                            SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                            SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, drImgColVal[0].ToString()),
                            SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, drImgColVal[1].ToString()),
                            SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, 1),
                            SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, drImgColVal[2].ToString()),
                            SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                            SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                            SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));

                            DataRow row = dtCatData.Rows[0];
                            int intCatid = Convert.ToInt32(row["CM_bIntCatId"]);
                        }
                    }

                    // } //End of Store Category Description and Images
                }//Upload files rotate

                //Code to insert an action log for Category Details insertion here.
                long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);

                introwCount = introwCount + lstCatFilePaths.Count - intFailureCount;
                string strActionMsg = "[Business Category Master] : " + introwCount + " number of rows inserted into database by " + Session["SystemUser"].ToString();
                GlobalFunctions.saveInsertUserAction("Category_Master", strActionMsg, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID), lngCompanyId, Request); //Call to user Action Log

                if (intFailureCount > 0)
                {
                    string strFailureMsgCount = "[Business Category Master] : " + intFailureCount + " rows insertion failed!";

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
                showgrid(); //Call to display Categories.

                Div2.InnerHtml = "Category Added Successfully!!!";
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnRead_ServerClick", "BusinessCategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                Div2.Attributes["class"] = "alert alert-info blink-border";
                Div2.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
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
            string strError;
            int intFailureCount = 0;
            int introwCount = 0;

            lblMsgError.Visible = false;
            //Code for importing from excedlsheet data and images
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
                    HttpFileCollection uploadedFiles = Request.Files;
                    List<string> lstCategoryName = new List<string>();
                    List<string> lstCatFilePaths = new List<string>();

                    string strStoredFilepath = "";
                    string strFileNm = "";
                    long lngAmId = 0;
                    bool blnExcelIsEmpty = false;

                    //Check whether files being selected or not
                    if (FileUploadControl.HasFile)
                    {
                        try
                        {
                            bool blnHasImage = false;
                            //Read an Excel file sheet for category Details
                            string ext = Path.GetExtension(FileUploadControl.FileName).ToLower();
                            string path = Server.MapPath(FileUploadControl.PostedFile.FileName);
                            FileUploadControl.SaveAs(path);
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

                            for (i = 0; i < uploadedFiles.Count - 1; i++)
                            {
                                f = uploadedFiles[i];
                                blnHasImage = false;
                                //Store Category Description and Images
                                //if (f.ContentLength > 0 && f.FileName != "MStoreInfo.xlsx")
                                if (f.ContentLength > 0 && (f.FileName == "Category.csv"))
                                {
                                    strFileNm = f.FileName;
                                    foreach (DataRow drImgColVal in dtCategoryDetails.Rows)
                                    {
                                        //Code to check whether Category names already exist in db.
                                        lngAmId = 0;
                                        if (drImgColVal[0].ToString().Trim() != "")
                                        {
                                            string strQuery = "Select CM_bIntCatId from Category_Master_17 where CM_vCharName_En = @Name AND CM_iNtEntryType=1";
                                            DataTable dtCatIdData = SqlHelper.ReadTable(strQuery, false,
                                                SqlHelper.AddInParam("@Name", SqlDbType.VarChar, drImgColVal[0].ToString()));

                                            if (dtCatIdData.Rows.Count > 0)
                                            {
                                                DataRow row = dtCatIdData.Rows[0];
                                                lngAmId = Convert.ToInt32(row["CM_bIntCatId"]);
                                            }

                                            //if (strFileNm.Equals(drImgColVal[3].ToString()) == true)
                                            //{

                                            int intTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;

                                            if (!Directory.Exists(Server.MapPath(GlobalVariables.CategoryDetailImagePath + "/" + intTalukaId)))
                                                Directory.CreateDirectory(Server.MapPath(GlobalVariables.CategoryDetailImagePath + "/" + intTalukaId));

                                            if (strFileNm != null)
                                            {
                                                string mStrFileExtension = Path.GetExtension(strFileNm);
                                                if (mStrFileExtension != ".xls")
                                                {
                                                    string strFilepath = Path.GetFullPath(strFileNm);
                                                    strStoredFilepath = GetSafeFileNameOnLocation(strFilepath, GlobalVariables.CategoryDetailImagePath + "/" + intTalukaId);
                                                    f.SaveAs(strStoredFilepath);
                                                    if (strFileNm == drImgColVal[3].ToString())
                                                    {
                                                        strError = ChkImageSize(strStoredFilepath);
                                                        if (strError == "")
                                                        {
                                                            //Insert category details with an image of an appropriate size
                                                            strStoredFilepath = String.Format("{0}/{1}", GlobalVariables.CategoryDetailImagePath + "/" + intTalukaId, Path.GetFileName(strStoredFilepath));
                                                            lstCatFilePaths.Add(strStoredFilepath); //Store file path for future mapping before storing in database

                                                            //Code to insert or modify Category Business master.
                                                            DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateCatMasterBusiness", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                            SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                            SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, drImgColVal[0].ToString()),
                                                            SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, drImgColVal[1].ToString()),
                                                            SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, 1),
                                                            SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, drImgColVal[2].ToString()),
                                                            SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                                                            SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                                            SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));

                                                            DataRow row = dtCatData.Rows[0];
                                                            int intCatid = Convert.ToInt32(row["CM_bIntCatId"]);

                                                        }
                                                        else
                                                        {
                                                            string message = "Image size should be between 32X32 and 43X43 pixels";
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
                                                    DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateCatMasterBusiness", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                               SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                               SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, drImgColVal[0].ToString()),
                                               SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, drImgColVal[1].ToString()),
                                               SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, 1),
                                               SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, drImgColVal[2].ToString()),
                                               SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, null),
                                               SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                               SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));

                                                    DataRow row = dtCatData.Rows[0];
                                                    int intCatid = Convert.ToInt32(row["CM_bIntCatId"]);

                                                }
                                            }

                                        }
                                    }
                                }
                            }

                            if (blnExcelIsEmpty == false)
                            {
                                Div2.Attributes["class"] = "alert alert-info";
                                Div2.InnerHtml = "Category Added Successfully!!!";
                                showgrid();
                            }
                            else
                            {
                                Div2.Attributes["class"] = "alert alert-info";
                                Div2.InnerHtml = "Excel Sheet is found to be empty!";
                                //Code to insert an action log for Category Details insertion here.
                                long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                                introwCount = introwCount - intFailureCount;
                                string strActionMsg = "[Business Category Master] : " + introwCount + " number of rows inserted into database by " + Session["SystemUser"].ToString();
                                strActionMsg = strActionMsg + " and " + intFailureCount + " no of rows insertion failed.";
                                GlobalFunctions.saveInsertUserAction("Category_Master", strActionMsg, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID), lngCompanyId, Request); //Call to user Action Log

                                if (intFailureCount > 0)
                                {
                                    string strFailureMsgCount = "[Business Category Master] : " + intFailureCount + " images insertion failed!";

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


                                //if (con.State == ConnectionState.Open)
                                //{
                                //    con.Close();
                                //}

                            }
                        }
                        catch (Exception exError)
                        {
                            long pLngErr = -1;
                            if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                                pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                            pLngErr = GlobalFunctions.ReportError("btnImportonPassword_Serverclick", "CategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
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
                    Div2.InnerHtml = "Invalid Password !!!";
                }

            }//end of method
        }
    }
}