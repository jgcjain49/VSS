using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;


namespace Admin_CommTrex
{
    public partial class AddBanner : System.Web.UI.Page
    {
        string ImagePath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["TalukaDetails"] != null)
                {
                    TextBox1.Text = Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaName);
                    RefreshInformation();
                    LockControls(false);
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }
        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            ClearAll();
            LockControls(true);
            SetMessage(false, "Add New to Add Categories");

        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdProducts.EditIndex = -1;
            RefreshInformation();
            ClearAll();
            if (txtImgPathMain.Value != "")
            {
                bool blnFlagDelete = DeleteFile(txtImgPathMain.Value);
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
                pLngErr = GlobalFunctions.ReportError("DeleteFile", "Category", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
                return false;
            }
        }
        private String ValidateProductData(string strBannDesc)
        {
            string mStrValidation = "";
            if (strBannDesc == "")
            {
                mStrValidation += "- Please enter Description" + Environment.NewLine;
                return (mStrValidation);
            }

            return (mStrValidation);

        }
        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //  // Image imgOriginalPath = (Image)grdProducts.Rows[e.RowIndex].FindControl("ImgCat");
                //  TextBox txtCatName = (TextBox)grdProducts.Rows[e.RowIndex].FindControl("txtCatName");
                Label lblID = (Label)grdProducts.Rows[e.RowIndex].FindControl("lblTalID");
                TextBox txtDescription = (TextBox)grdProducts.Rows[e.RowIndex].FindControl("txtTalDes");
                HiddenField ImgCat = (HiddenField)grdProducts.Rows[e.RowIndex].FindControl("imgPath");
                HiddenField ImgOriginalCategory = (HiddenField)grdProducts.Rows[e.RowIndex].FindControl("imgOriginalPath");


                string strErrorImg = "";
                String strError = ValidateProductData(txtDescription.Text);

                if (Convert.ToString(ViewState["ImgPath"]) != "")
                {
                    strErrorImg = CopyFileSafely(txtImgPathMain.Value, Convert.ToString(ViewState["ImgPath"]));
                }
                else
                {
                    strErrorImg = "No Change";
                    ViewState["ImgPath"] = ImgOriginalCategory.Value;

                }

                if (strErrorImg.Trim() != "No Change")
                {
                    if (File.Exists(Server.MapPath(ImgOriginalCategory.Value)))
                        File.Delete(Server.MapPath(ImgOriginalCategory.Value));
                }


                if (strError == "" && strErrorImg != "")
                {
                    DataTable dtCatData = SqlHelper.ReadTable("SP_insertUpdatBanner", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                         SqlHelper.AddInParam("@intBId", SqlDbType.Int, lblID.Text),
                                         SqlHelper.AddInParam("@nVarDesc", SqlDbType.NVarChar, txtDescription.Text),
                                         SqlHelper.AddInParam("@nVarImage", SqlDbType.NVarChar, Convert.ToString(ViewState["ImgPath"]).Replace("//", "/")));

                    int intTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
                    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                    SetProductsUpdateMessage(false, "Banner Updated Successfully");

                    if (txtImgPathMain.Value != "")
                    {
                        bool blnFlagDelete = DeleteFile(txtImgPathMain.Value);
                    }
                    grdProducts.EditIndex = -1;
                    RefreshInformation();
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
                pLngErr = GlobalFunctions.ReportError("GridView1_RowUpdating1", "Category", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }
        protected void btnDeleteProduct_ServerClick(object sender, EventArgs e)
        {
            try
            {
                //Label lblID = (Label)grdProducts.Rows[e.RowIndex].FindControl("lblTalID");
                //Label lblID = (Label)grdProducts.Rows[e.RowIndex].FindControl("lblTalID");
                int intCatID = Convert.ToInt32(txtDelCatIDHidden.Value);
                // int intCatID = Convert.ToInt32(lblID.Text);
                DataTable dtCatData = SqlHelper.ReadTable("Delete from Banner_17 where B_bIntId=@intAmId", Convert.ToString(Session["SystemUserSqlConnectionString"]), false,
                                     SqlHelper.AddInParam("@intAmId", SqlDbType.BigInt, intCatID));

                //Delete image from location
                string strDelImg = txtDelCatPath.Value;
                if (File.Exists(Server.MapPath(strDelImg)))
                    File.Delete(Server.MapPath(strDelImg));

                int intTalukaId = Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID);
                long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                // GlobalFunctions.saveInsertUserAction("Category_Master", "[Free Category Master Delete]:Deletion of Category with Id : " + intCatID + " and Image : " + strDelImg, intTalukaId, lngCompanyId, Request); //Call to user Action Log

                SetProductsUpdateMessage(false, "Banner Deleted Successfully");
                RefreshInformation();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnDeleteProduct_ServerClick", "Category", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdProducts.EditIndex = e.NewEditIndex;
            ViewState["RowVal"] = e.NewEditIndex;
            RefreshInformation();
            grdProducts.Columns[3].Visible = true;
        }
        protected void RefreshInformation()
        {
            if (Session["TalukaDetails"] != null)
            {
                String mIntTalukaName = ((TalukaData)Session["TalukaDetails"]).TalukaName;
                string mStrSql = "SP_GetBannerInfo";

                grdProducts.DataSource = SqlHelper.ReadTable(mStrSql, GlobalVariables.SqlConnectionStringMstoreInformativeDb, true);
                grdProducts.DataBind();
                grdProducts.Columns[3].Visible = false;
            }
            else
                Response.Redirect("Home.aspx"); // Session time out
        }
        protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField ImgCat = (HiddenField)e.Row.FindControl("imgPath");
                Image Img = (Image)e.Row.FindControl("ImgCat");
                if (ImgCat.Value == "")
                {
                    Img.Visible = false;
                }
                //HiddenField ImgCat = (HiddenField)e.Row.FindControl("imgPath");
                //HiddenField LogoName = (HiddenField)e.Row.FindControl("LogoName");
                //HtmlButton btnLogo = (HtmlButton)e.Row.FindControl("btnLogoShow");
                //Image Img = (Image)e.Row.FindControl("ImgCat");
                //if (ImgCat.Value == "")
                //{
                //    Img.Visible = false;
                //}
                //if (LogoName.Value == "fa " || LogoName.Value == "")
                //{
                //    btnLogo.Visible = false;
                //}


            }
        }
        protected void btnSaveFilePath_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string pStrDestination = "";
                string strError;
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strTalukaId = Convert.ToString(intTalukaId);

                if (FileMainImage.HasFile)
                {
                    if (!Directory.Exists(Server.MapPath(GlobalVariables.TemporaryPath)))
                        Directory.CreateDirectory(Server.MapPath(GlobalVariables.TemporaryPath));

                    if (!Directory.Exists(Server.MapPath(GlobalVariables.BannerPath)))
                        Directory.CreateDirectory(Server.MapPath(GlobalVariables.BannerPath));

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

                        if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.BannerPath), pStrDestination)))
                        {
                            int count1 = 1;
                            while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.BannerPath), pStrDestination)))
                            {
                                pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileMainImage.FileName), count1++);
                                pStrDestination = pStrDestination + Path.GetExtension(FileMainImage.FileName);
                            }

                            if (Convert.ToString(ViewState["RowVal"]) == "")
                            {
                                txtImageText.Text = (GlobalVariables.BannerPath) + "//" + pStrDestination;
                                SetMessage(true, "Image Uploaded Successfully!!!");

                            }
                            else
                            {
                                HiddenField ImgCat = (HiddenField)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath");

                                ImgCat.Value = txtImgPathMain.Value;
                                Image Img = (Image)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat");
                                Img.ImageUrl = "~/" + txtImgPathMain.Value;
                                Img.Visible = true;
                                ViewState["ImgPath"] = (GlobalVariables.BannerPath) + "//" + pStrDestination;

                                SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                            }
                        }
                        else
                        {
                            if (Convert.ToString(ViewState["RowVal"]) == "")
                            {
                                txtImageText.Text = (GlobalVariables.BannerPath) + "//" + pStrDestination;
                                SetMessage(true, "Image Uploaded Successfully!!!");
                            }
                            else
                            {
                                HiddenField ImgCat = (HiddenField)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath");
                                ImgCat.Value = txtImgPathMain.Value;
                                Image Img = (Image)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat");
                                Img.ImageUrl = "~/" + txtImgPathMain.Value;
                                Img.Visible = true;
                                ViewState["ImgPath"] = (GlobalVariables.BannerPath) + "//" + pStrDestination;
                                SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                            }
                        }
                    }
                    else
                    {
                        File.Delete(Server.MapPath(txtImgPathMain.Value));
                        string message = "Invalid image resolution (dimension)! Image resolution should be 750 X 600 pixels.";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append(" bootbox.alert('");
                        sb.Append(message);
                        sb.Append("').find('.modal-footer button')"+
                            ".css({'background-color': '#2042ae',color: '#faf6f6 !important',"+
                            "'font-size': '1.4rem !important', 'border-radius': '12px',"+
                            "'letter-spacing': '1px', 'min-width': '110px'})};");
                        sb.Append("</script>");

                        //alternative way of doing above (applying custom css class to inner bootbox element)
                        //var box = bootbox.alert("Hello world!");
                        //box.find(".btn-primary").removeClass("btn-primary").addClass("gbtn");

                        ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());
                    }
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSaveFilePath_ServerClick", "Category", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
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

        private void SetProductsUpdateMessage(bool pBlnIsError, string pStrMessage)
        {
            updateActionDiv.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            updateActionDiv.InnerHtml = pStrMessage;
        }
        private string ChkImageSize(string pstrImagePath)
        {
            try
            {
                string strMsg = "";
                System.Drawing.Bitmap image = new System.Drawing.Bitmap(pstrImagePath);
                int originalWidth = image.Width;
                int originalHeight = image.Height;

                if (originalWidth > 751 || originalHeight > 601)
                {
                    image.Dispose();
                    return ("Image Upload Failed Image Size is Greater than maximum size 750px x 600px! Select Appropriate Image to Continue");
                }
                else if (originalWidth > 751 && originalWidth > 601)
                {
                    image.Dispose();
                    return ("Image Upload Failed Image Size is Greater than maximum size 750px x 600px! Select Appropriate Image to Continue");
                }
                else if (originalWidth < 749 && originalWidth < 599)
                {
                    image.Dispose();
                    return (" Image Upload Failed Image Size is smaller than minimum size 750px x 600px! Select Appropriate Image to Continue");
                }

                else if (originalWidth < 749 || originalWidth < 599)
                {
                    image.Dispose();
                    return (" Image Upload Failed Image Size is smaller than maximum size 750px x 600px! Select Appropriate Image to Continue");
                }
                image.Dispose();
                if (strMsg.Trim() != "")
                {
                    //ResizeImage(pstrImagePath, pstrImagePath, 750, 600, false);
                    strMsg = "";
                }
                return strMsg;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("ChkImageSize", "CategoryFree", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
            return "";

        }
        private void SetMessage(bool pBlnIsError, string pStrError)
        {
            actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            actionInfo.InnerHtml = pStrError;
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
        private String ValidateProductData()
        {
            string mStrValidation = "";
            if (txtDescriptiomText.Text == "")
            {
                mStrValidation += "- Please enter Description" + Environment.NewLine;
                return (mStrValidation);

            }
            if (txtImageText.Text == "")
            {
                mStrValidation += "- Please Select Banner Image" + Environment.NewLine;
                return (mStrValidation);
            }

            return (mStrValidation);

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
                            strPath = "";
                            // strPath = GlobalVariables.NoImagePath;
                        }
                        else
                        {
                            strPath = CopyFileSafely(txtImgPathMain.Value, txtImageText.Text);
                            strPath = txtImageText.Text.Replace("//", "/");
                            strLogoPath = "";
                            // strLogoPath = "fa-question-circle";
                        }

                        DataTable dtCatData = SqlHelper.ReadTable("[SP_insertUpdatBanner]", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                             SqlHelper.AddInParam("@intBId", SqlDbType.Int, 0),
                                            SqlHelper.AddInParam("@nVarDesc", SqlDbType.NVarChar, txtDescriptiomText.Text),
                                            SqlHelper.AddInParam("@nVarImage", SqlDbType.NVarChar, strPath));

                        ClearAll();
                        SetMessage(false, "Banner Added Successfully!!!");
                        btnSave.Attributes["btn-action"] = "New";
                        btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> New";
                        grdProducts.EditIndex = -1;
                        RefreshInformation();
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
                pLngErr = GlobalFunctions.ReportError("btnSave_Click", "Category", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
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
                pLngErr = GlobalFunctions.ReportError("CopyFileSafely", "Category", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
            return pStrDestination;
        }
        private void LockControls(bool pflag)
        {
            txtDescriptiomText.Enabled = pflag;
            //txtCategoryRegional.Enabled = pflag;
            ////txtImageText.Enabled = pflag;
            //btnSelectICon.Disabled = !pflag;
            //btnSelectImage.Disabled = !pflag;
        }
        private void ClearAll()
        {
            txtDescriptiomText.Text = "";
            txtImageText.Text = "";
            //txtImageText.Text = "";
            //txtCategoryRegional.Text = "";
            //txtCategoryID.Text = "";
            SetMessage(false, "Press Save to store Banner");
            //txtImgPathMain.Value = "";
            //txtLogo.Text = "";

            ViewState["ImgLogo"] = "";
            ViewState["ImgPath"] = "";
            ViewState["RowVal"] = "";

        }
    }
}