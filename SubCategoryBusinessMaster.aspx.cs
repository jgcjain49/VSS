using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_CommTrex
{
    public partial class SubCategoryBusinessMaster : System.Web.UI.Page
    {
        FileUpload objFileup;
        Label mlblSubCatid_GridView;
        TextBox mtxtSubCatName_GridView;
        TextBox mtxtSubCatRegName_GridView;
        TextBox mtxticonValue_GridView;

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
                    showCategory();
                    showSubCategoryDetails();
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }

            }
        }

        private void SetMessage(bool pBlnIsError, string pStrError)
        {
            actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            actionInfo.InnerHtml = pStrError;
        }

        //View sub category details in grid view for modification and deletion
        public void showSubCategoryDetails()
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;

                string mStrSelectQuery = "select scm.SCM_bIntSubCatId,";
                mStrSelectQuery += " scm.SCM_vCharName_En,";
                mStrSelectQuery += " scm.SCM_nVarName_Reg,";
                mStrSelectQuery += " CASE scm.SCM_bItIsActive WHEN 1 THEN 'Yes' ELSE 'No' END as 'SCM_bItIsActive',";
                mStrSelectQuery += " CASE WHEN scm.SCM_vCharCatImgClass is not null";
                mStrSelectQuery += " Then 'fa ' + scm.SCM_vCharCatImgClass";
                mStrSelectQuery += " WHEN scm.SCM_vCharSubCatImgPath is null";
                mStrSelectQuery += " Then 'fa-question-circle'";
                mStrSelectQuery += " else ''";
                mStrSelectQuery += " END AS 'SCM_vCharCatImgClass',";
                mStrSelectQuery += " CASE WHEN (scm.SCM_vCharSubCatImgPath is not null And scm.SCM_vCharCatImgClass is null)";
                mStrSelectQuery += " then scm.SCM_vCharSubCatImgPath else '' END AS 'SCM_vCharSubCatImgPath',";
                mStrSelectQuery += " cm.CM_vCharName_En";
                mStrSelectQuery += " from Sub_Category_Master_17 scm,Category_Master_17 cm";
                mStrSelectQuery += " Where scm.SCM_bIntCatId=cm.CM_bIntCatId AND scm.SCM_bItIsActive = 1 AND scm.SCM_iNtEntryType = 1 ";
                mStrSelectQuery += " Order By scm.SCM_bIntSubCatId";

                DataTable dt = SqlHelper.ReadTable(mStrSelectQuery, conString, false);
                txtIconvalue.Text = "";
                grdProducts.DataSource = dt;
                grdProducts.DataBind();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showSubCategoryDetails", "SubCategoryBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }

        }


        //check whether subcategory with same name already exist in table
        public bool chkSubCatExist(string pstrSubCatNm)
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;

                string mStrSelectQuery = "select scm.SCM_bIntSubCatId,";
                mStrSelectQuery += " scm.SCM_vCharName_En,";
                mStrSelectQuery += " scm.SCM_nVarName_Reg,";
                mStrSelectQuery += " CASE scm.SCM_bItIsActive WHEN 1 THEN 'Yes' ELSE 'No' END as 'SCM_bItIsActive',";
                mStrSelectQuery += " CASE WHEN scm.SCM_vCharCatImgClass is not null";
                mStrSelectQuery += " Then 'fa ' + scm.SCM_vCharCatImgClass";
                mStrSelectQuery += " WHEN scm.SCM_vCharSubCatImgPath is null";
                mStrSelectQuery += " Then 'fa-circle-question'";
                mStrSelectQuery += " else ''";
                mStrSelectQuery += " END AS 'SCM_vCharCatImgClass',";
                mStrSelectQuery += " CASE WHEN (scm.SCM_vCharSubCatImgPath is not null And scm.SCM_vCharCatImgClass is null)";
                mStrSelectQuery += " then scm.SCM_vCharSubCatImgPath else '' END AS 'SCM_vCharSubCatImgPath',";
                mStrSelectQuery += " cm.CM_vCharName_En";
                mStrSelectQuery += " from Sub_Category_Master_17 scm,Category_Master_17 cm";
                mStrSelectQuery += " Where scm.SCM_bIntCatId=cm.CM_bIntCatId AND scm.SCM_bItIsActive = 1 AND scm.SCM_vCharName_En Like @SubCatNm AND SCM_iNtEntryType=1";

                DataTable dtSubCatDetails = SqlHelper.ReadTable(mStrSelectQuery, conString, false,
                    SqlHelper.AddInParam("@SubCatNm",SqlDbType.VarChar,pstrSubCatNm));

                if (dtSubCatDetails.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("chkSubCatExist", "SubCategoryBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
            return false;
        }

        //Display all categories in combox box for add new
        public void showCategory()
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                DataTable dtCategoryList = SqlHelper.ReadTable("SELECT CM_bIntCatId,CM_vCharName_En FROM Category_Master_17  WHERE CM_bItIsActive = 1 AND CM_iNtEntryType=1", conString, false);

                cmbCategoryType.DataSource = dtCategoryList;
                cmbCategoryType.DataTextField = "CM_vCharName_En";
                cmbCategoryType.DataValueField = "CM_bIntCatId";
                cmbCategoryType.DataBind();
                cmbCategoryType.Items.Insert(0, new ListItem("--Select Category--", "0"));
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showCategory", "SubCategoryBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        //Save newly created sub category details
        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Attributes["btn-action"] == "Save")
                {
                    if (validateAddInput() == false)
                    {
                        int intCatId = Convert.ToInt32(cmbCategoryType.SelectedValue);
                        string strSubCatNm = txtSubCategoryName.Text;
                        string strSubCatRegNm = txtSubCategoryRegName.Text;
                        string strImgPath = txtImgPath.Text;
                        int intIsActive = Convert.ToInt32(cmbSubCategoryIsActive.SelectedValue);

                        if (chkSubCatExist(strSubCatNm) == true)
                        {
                            lblSubCatValidate.Visible = true;
                            txtSubCategoryName.BackColor = System.Drawing.Color.Red;
                            txtSubCategoryName.ForeColor = System.Drawing.Color.Black;
                        }
                        else
                        {
                            insertSubCategoryInDb(intCatId, strSubCatNm, strSubCatRegNm, strImgPath, intIsActive);
                            btnSave.Attributes["btn-action"] = "New";
                            btnSave.InnerHtml = "<i class=\"fa fa-plus-square\"></i> New";
                            clearFields();
                            PreImg.Disabled = true;
                            CustImg.Disabled = true;
                            cmbCategoryType.Enabled = false;
                            txtSubCategoryName.Enabled = false;
                            txtSubCategoryRegName.Enabled = false;
                            cmbSubCategoryIsActive.Enabled = false;
                        }
                    }
                    else
                    {
                        //Code for empty field validation
                        string message = "Please input all fields";
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
                else
                {
                    btnSave.Attributes["btn-action"] = "Save";
                    btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> Save";
                    CustImg.Disabled = false;
                    PreImg.Disabled = false;
                    cmbCategoryType.Enabled = true;
                    txtSubCategoryName.Enabled = true;
                    txtSubCategoryRegName.Enabled = true;
                    cmbSubCategoryIsActive.Enabled = true;
                    clearFields();
                    SetMessage(false, "Press Save to store SubCategory");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSave_ServerClick", "SubCategoryBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        //Insert all subcategory details in database
        public void insertSubCategoryInDb(int pintCategoryId, string pstrSubCategoryName, string pstrSubCatRegName, string pstrSubCatImage, int intIsActive)
        {
            try
            {
                DataTable dtInsertSubCategory = new DataTable();
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string actualImgPath;

                //path to store image is Content/Categories/map.png
                string mStrSubCategoryImagePath = pstrSubCatImage.Replace("//", "/");
                actualImgPath = txtMainImgPath.Text;

                //Insert query code here
                Dictionary<string, string> mDicInputs = new Dictionary<string, string>();
                mDicInputs.Add("CategoryId", cmbCategoryType.SelectedValue.ToString());
                mDicInputs.Add("SubCategoryName", pstrSubCategoryName);
                mDicInputs.Add("SubCategoryRegName", pstrSubCatRegName);
                mDicInputs.Add("SubCategoryImagePath", mStrSubCategoryImagePath);
                mDicInputs.Add("IsActive", cmbSubCategoryIsActive.SelectedValue.ToString());

                long lngInsert = 0;
                string strImgType = txtIconvalue.Text.Trim();
                if (strImgType != "")
                {
                    dtInsertSubCategory = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
                        SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, intTalukaId),
                        SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, mDicInputs["SubCategoryName"]),
                        SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, mDicInputs["SubCategoryRegName"]),
                        SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, Convert.ToInt32(mDicInputs["IsActive"])),
                        SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, strImgType),
                        SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, null),
                        SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, Convert.ToInt32(mDicInputs["CategoryId"])),
                        SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                        SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngInsert));
                    mStrSubCategoryImagePath = "";
                }
                if (mStrSubCategoryImagePath.Trim() != "")
                {
                    CopyFileSafely(actualImgPath, mStrSubCategoryImagePath);
                    dtInsertSubCategory = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
                        SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, intTalukaId),
                        SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, mDicInputs["SubCategoryName"]),
                        SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, mDicInputs["SubCategoryRegName"]),
                        SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, Convert.ToInt32(mDicInputs["IsActive"])),
                        SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, null),
                        SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, mDicInputs["SubCategoryImagePath"]),
                        SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, Convert.ToInt32(mDicInputs["CategoryId"])),
                        SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                        SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngInsert));
                }
                if (dtInsertSubCategory.Rows.Count > 0)
                {
                    long mLngSubCatId = Convert.ToInt64(dtInsertSubCategory.Rows[0][0]);

                    //Update the grid view for display of newly added record
                    showSubCategoryDetails();

                    //Messagebox to display success in creation of subcategory
                    string message = "SubCategory created successfully";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append(" bootbox.alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());

                    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                    if (strImgType != "")
                    {
                        GlobalFunctions.saveInsertUserAction("Sub_Category_Master", "[Business SubCategory Master Insert]:Insertion of New Record with Id " + mLngSubCatId + " and Icon Uploaded : " + strImgType, intTalukaId, lngCompanyId, Request); //Call to user Action Log
                    }
                    else
                    {
                        GlobalFunctions.saveInsertUserAction("Sub_Category_Master", "[Business SubCategory Master Insert]:Insertion of New Record with Id " + mLngSubCatId + " and Image Uploaded : " + mStrSubCategoryImagePath, intTalukaId, lngCompanyId, Request); //Call to user Action Log
                    }
                }
                else
                {
                    //Code for error in insertion of sub category
                    string message = "Insertion  of SubCategory Failed.";
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
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("insertSubCategoryInDb", "SubCategoryBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        //Clear all textboxes for newly added subcategory
        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            clearFields(); //call to clear function
        }

        public void clearFields()
        {
            txtSubCatId.Text = "";
            txtSubCategoryName.Text = "";
            txtSubCategoryRegName.Text = "";
            txtImgPath.Text = "";
            txtMainImgPath.Text = "";
            ViewState["RowVal"] = "";
            cmbCategoryType.SelectedIndex = 0;
            cmbSubCategoryIsActive.SelectedIndex = -1;
        }

        //validate before addition of subcategory into table
        public bool validateAddInput()
        {
            bool chkEmpty = false;
            if (txtSubCategoryName.Text == "") { chkEmpty = true; }
            if (txtImgPath.Text.Trim() == "" && txtIconvalue.Text.Trim() == "") { chkEmpty = true; }
            if (cmbCategoryType.Text == "--Select Category--") { chkEmpty = true; }
            if (cmbSubCategoryIsActive.Text == "--Select Choice--") { chkEmpty = true; }

            return chkEmpty;
        }

        //Validate before modification of subcategory details in grid view
        public bool validateModifyInput()
        {
            bool chkEmpty = false;

            if (mtxtSubCatName_GridView.Text.Trim() == "") { chkEmpty = true; }
            if (txtImgPath.Text.Trim() == "") { chkEmpty = true; }
            return chkEmpty;
        }

        //Validate image size for 25*25 and 16*16 to save and update
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

        //Upload an image to temporary location
        protected void btnUpload_ServerClick(object sender, EventArgs e)
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strTalukaId = Convert.ToString(intTalukaId);

                //string strMainImgText; //replace txtMainImgPath.Value
                txtImgPath.Text = Server.MapPath(FileUploadControl.FileName);
                string strFileName = System.IO.Path.GetFileName(FileUploadControl.PostedFile.FileName);
                txtMainImgPath.Text = GlobalVariables.SubCategoryTemporaryPath + "/" + strFileName;
                string pStrDestination = "";
                if (FileUploadControl.HasFile)
                {
                    if (!Directory.Exists(Server.MapPath(GlobalVariables.SubCategoryTemporaryPath)))
                        Directory.CreateDirectory(Server.MapPath(GlobalVariables.SubCategoryTemporaryPath));

                    if (!Directory.Exists(Server.MapPath(GlobalVariables.SubcatFileHostPath + "//" + strTalukaId)))
                        Directory.CreateDirectory(Server.MapPath(GlobalVariables.SubcatFileHostPath + "//" + strTalukaId));

                    pStrDestination = Path.GetFileName(FileUploadControl.FileName);
                    int count = 1;
                    if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.SubCategoryTemporaryPath), pStrDestination)))
                    {
                        while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.SubCategoryTemporaryPath), pStrDestination)))
                        {
                            pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileUploadControl.FileName), count++);
                            pStrDestination = pStrDestination + Path.GetExtension(FileUploadControl.FileName);
                        }
                        FileUploadControl.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.SubCategoryTemporaryPath), pStrDestination));
                    }
                    else
                    {
                        FileUploadControl.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.SubCategoryTemporaryPath), pStrDestination));
                    }

                    txtMainImgPath.Text = (GlobalVariables.SubCategoryTemporaryPath) + "//" + pStrDestination;
                    string strError = ChkImageSize(Server.MapPath(txtMainImgPath.Text));
                    if (strError == "")
                    {
                        pStrDestination = Path.GetFileName(FileUploadControl.FileName);

                        if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.SubcatFileHostPath + "//" + strTalukaId), pStrDestination)))
                        {
                            int count1 = 1;
                            while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.SubcatFileHostPath + "//" + strTalukaId), pStrDestination)))
                            {
                                pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileUploadControl.FileName), count1++);
                                pStrDestination = pStrDestination + Path.GetExtension(FileUploadControl.FileName);
                            }
                            if (Convert.ToString(ViewState["RowVal"]) == "")
                            {
                                txtImgPath.Text = (GlobalVariables.SubcatFileHostPath + "//" + strTalukaId) + "//" + pStrDestination;
                                //SetMessage(true, "Image Uploaded Successfully!!!");
                            }
                            else
                            {
                                //HiddenField ImgCat = (HiddenField)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("MyImage");
                                //HiddenField LogoName = (HiddenField)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("LogoName");
                                //LogoName.Value = "";
                                //ImgCat.Value = strMainImgText;
                                Image Img = (Image)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("MyImage");
                                Img.ImageUrl = "~/" + txtMainImgPath.Text;
                                Img.Visible = true;
                                //HtmlButton btnLogo = (HtmlButton)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow");
                                //btnLogo.Visible = false;
                                ViewState["ImgLogo"] = "";
                                ViewState["ImgPath"] = (GlobalVariables.SubcatFileHostPath + "//" + strTalukaId) + "//" + pStrDestination;
                                txtImgPath.Text = (GlobalVariables.SubcatFileHostPath + "//" + strTalukaId) + "//" + pStrDestination;
                                //SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                            }
                        }
                        else
                        {
                            if (Convert.ToString(ViewState["RowVal"]) == "")
                            {
                                txtImgPath.Text = (GlobalVariables.SubcatFileHostPath + "//" + strTalukaId) + "//" + pStrDestination;
                                //SetMessage(true, "Image Uploaded Successfully!!!");
                            }
                            else
                            {
                                //HiddenField ImgCat = (HiddenField)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("MyImage");
                                //HiddenField LogoName = (HiddenField)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("LogoName");
                                //LogoName.Value = "";
                                //ImgCat.Value = strMainImgText;
                                Image Img = (Image)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("MyImage");
                                Img.ImageUrl = "~/" + txtMainImgPath.Text;
                                Img.Visible = true;
                                //HtmlButton btnLogo = (HtmlButton)grdProducts.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow");
                                //btnLogo.Visible = false;
                                ViewState["ImgLogo"] = "";
                                ViewState["ImgPath"] = (GlobalVariables.SubcatFileHostPath + "//" + strTalukaId) + "//" + pStrDestination;
                                txtImgPath.Text = (GlobalVariables.SubcatFileHostPath + "//" + strTalukaId) + "//" + pStrDestination;
                                //SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                            }
                        }
                        ////Messagebox to display success of subcategory image upload
                        string message = "Image Uploaded successfully";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append(" bootbox.alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());
                        //SetMessage(true, "Image Uploaded Successfully!!!");
                    }
                    else
                    {
                        File.Delete(Server.MapPath(txtMainImgPath.Text));
                        // SetMessage(true, strError);

                        txtMainImgPath.Text = "";

                        //Messagebox to display failure of subcategory image upload
                        string message = "Image size should be between 16X16 and 25X25 pixels";
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
                pLngErr = GlobalFunctions.ReportError("btnUpload_ServerClick", "SubCategoryBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        //On click of edit button display textboxes for editing
        protected void grdProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdProducts.EditIndex = e.NewEditIndex;
            DropDownList drdlObject = (DropDownList)grdProducts.Rows[e.NewEditIndex].FindControl("drdlCategoryNm");
            Label lblObject = (Label)grdProducts.Rows[e.NewEditIndex].FindControl("lblCategoryName");
            objFileup = (FileUpload)grdProducts.Rows[e.NewEditIndex].FindControl("FileUpload1");
            drdlObject.Visible = true;
            lblObject.Visible = false;
            grdProducts.Columns[5].Visible = true;
            showSubCategoryDetails();
            ViewState["RowVal"] = e.NewEditIndex;
        }

        //on updation of row save the edited column values.
        protected void grdProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            bool chkNewicon = false;
            bool chkNewimg = false;
            Image imgPic;
            DataTable dtInsertSubCategory;
            string strImgType = "";
            TalukaData objTal = (TalukaData)Session["TalukaDetails"];
            int intTalukaId = objTal.TalukaID;
            string mStrSubCategoryImagePath = "";

            string strCategoryNM = (grdProducts.Rows[e.RowIndex].FindControl("drdlCategoryNm") as DropDownList).SelectedItem.Value;
            string strIsActive = (grdProducts.Rows[e.RowIndex].FindControl("drdlIsActive") as DropDownList).SelectedItem.Value;
            mtxtSubCatName_GridView = (TextBox)grdProducts.Rows[e.RowIndex].FindControl("txtSubCategoryName");
            mtxtSubCatRegName_GridView = (TextBox)grdProducts.Rows[e.RowIndex].FindControl("txtSubCategoryRegName");
            mtxticonValue_GridView = (TextBox)grdProducts.Rows[e.RowIndex].FindControl("txtModifyIconValue");
            imgPic = (Image)grdProducts.Rows[e.RowIndex].FindControl("MyImage");
            HiddenField ImgOriginalSubCategory = (HiddenField)grdProducts.Rows[e.RowIndex].FindControl("hfSubCatImgPath"); //Added by SSK to delete old image

            try
            {


                if (txtIconvalue.Text.Trim() != "")
                {
                    chkNewicon = true;
                    strImgType = txtIconvalue.Text.Trim();
                }

                if (txtMainImgPath.Text.Trim() != "")
                {
                    chkNewimg = true;
                    //Code for deletion of existing image file.
                    if (File.Exists(Server.MapPath(ImgOriginalSubCategory.Value)))
                        File.Delete(Server.MapPath(ImgOriginalSubCategory.Value));
                }



                if (chkNewicon == false && chkNewimg == false)
                {
                    if (mtxticonValue_GridView.Text.Trim() != "")
                    {
                        strImgType = mtxticonValue_GridView.Text.Trim();
                    }
                    else
                    {
                        mStrSubCategoryImagePath = imgPic.ImageUrl;
                    }
                }

                if (chkNewimg == true && chkNewicon == false)
                {
                    //mStrSubCategoryImagePath = GlobalVariables.SubcatFileHostPath;
                    mStrSubCategoryImagePath = (txtImgPath.Text).Replace("//", "/");
                    CopyFileSafely(txtMainImgPath.Text, mStrSubCategoryImagePath);
                }
                else
                {
                    if (chkNewicon == true)
                        mStrSubCategoryImagePath = "";
                    else
                        mStrSubCategoryImagePath = imgPic.ImageUrl;
                }

                Dictionary<string, string> mDicInputs = new Dictionary<string, string>();
                mDicInputs.Add("CategoryId", strCategoryNM);
                mDicInputs.Add("SubCategoryName", mtxtSubCatName_GridView.Text);
                mDicInputs.Add("SubCategoryRegName", mtxtSubCatRegName_GridView.Text);
                mDicInputs.Add("SubCategoryImagePath", mStrSubCategoryImagePath);
                mDicInputs.Add("IsActive", strIsActive);

                mlblSubCatid_GridView = (Label)grdProducts.Rows[e.RowIndex].FindControl("lblSubCatID");
                long lngInsert = Convert.ToInt64(mlblSubCatid_GridView.Text);

                if (strImgType != "")
                {
                    dtInsertSubCategory = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
                        SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, intTalukaId),
                        SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, mDicInputs["SubCategoryName"]),
                        SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, mDicInputs["SubCategoryRegName"]),
                        SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, Convert.ToInt32(mDicInputs["IsActive"])),
                        SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, strImgType),
                        SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, null),
                        SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, Convert.ToInt32(mDicInputs["CategoryId"])),
                        SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                        SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngInsert));
                    mStrSubCategoryImagePath = "";
                    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                    GlobalFunctions.saveInsertUserAction("Sub_Category_Master", "[Business SubCategory Master Update]:Updation of SubCategory with Id : " + lngInsert + " and Icon Uploaded : " + strImgType, intTalukaId, lngCompanyId, Request); //Call to user Action Log
                }
                if (mStrSubCategoryImagePath.Trim() != "")
                {
                    dtInsertSubCategory = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
                        SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, intTalukaId),
                        SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, mDicInputs["SubCategoryName"]),
                        SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, mDicInputs["SubCategoryRegName"]),
                        SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, Convert.ToInt32(mDicInputs["IsActive"])),
                        SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, null),
                        SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, mDicInputs["SubCategoryImagePath"]),
                        SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, Convert.ToInt32(mDicInputs["CategoryId"])),
                        SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                        SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngInsert));
                    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                    GlobalFunctions.saveInsertUserAction("Sub_Category_Master", "[Business SubCategory Master Update]:Updation of SubCategory with Id : " + lngInsert + " and Image Uploaded : " + mDicInputs["SubCategoryImagePath"], intTalukaId, lngCompanyId, Request); //Call to user Action Log

                }

                grdProducts.EditIndex = -1;

                //Update the grid view for display of newly added record
                showSubCategoryDetails();

                //Clear the image storage textbox
                mtxticonValue_GridView.Text = "";
                txtIconvalue.Text = "";
                clearFields();

                //Messagebox to display success in creation of subcategory
                string message = "SubCategory updated successfully";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.onload=function(){");
                sb.Append(" bootbox.alert('");
                sb.Append(message);
                sb.Append("')};");
                sb.Append("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("grdProducts_RowUpdating", "SubCategoryBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
            //Response.Redirect(Request.Url.AbsoluteUri);
        }


        //Update the combobox value for category names
        protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                    DataTable dt = SqlHelper.ReadTable("SELECT CM_bIntCatId,CM_vCharName_En FROM Category_Master_17  WHERE CM_bItIsActive = 1  AND CM_iNtEntryType = 1 ", conString, false);

                    if (dt.Rows.Count > 0)
                    {
                        DropDownList drdlObject = (DropDownList)e.Row.FindControl("drdlCategoryNm");
                        drdlObject.DataSource = dt;
                        drdlObject.DataTextField = "CM_vCharName_En";
                        drdlObject.DataValueField = "CM_bIntCatId";
                        drdlObject.DataBind();
                        Label lblObject = (Label)e.Row.FindControl("lblCategoryName");
                        if (drdlObject.Items.Count > 0)
                            drdlObject.Items.FindByText(lblObject.Text).Selected = true;
                    }
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("grdProducts_RowDataBound", "SubCategoryBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        //On edit cancel reload existing subcategory details.
        protected void grdProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdProducts.EditIndex = -1;
            showSubCategoryDetails();
        }

        protected void grdProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //code for row command
        }

        //Copy file safely from temporary folder to content /subcategory folder
        public string CopyFileSafely(string pStrSourceFile, string pStrDestination)
        {
            try
            {
                if (pStrSourceFile == "" || Server.MapPath(pStrDestination) == "")
                    return "";

                pStrDestination = Server.MapPath(pStrDestination);
                pStrSourceFile = Server.MapPath(pStrSourceFile);

                if (!Directory.Exists(pStrDestination))
                    Directory.CreateDirectory(Server.MapPath(GlobalVariables.SubcatFileHostPath));


                File.Copy(pStrSourceFile, pStrDestination);
                File.Delete(pStrSourceFile);

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("CopyFileSafely", "SubCategoryBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
            return pStrDestination;
        }

        //Delete the subcategory on click of button.
        protected void btnDeleteProduct_ServerClick(object sender, EventArgs e)
        {
            try
            {
                // Delete sub category from server
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                //mlblSubCatid_GridView = (Label)grdProducts.Rows[e.RowIndex].FindControl("lblSubCatID");

                Dictionary<string, string> mDicInputs = new Dictionary<string, string>();

                mDicInputs.Add("TalukatID", Convert.ToString(intTalukaId));
                mDicInputs.Add("SubCatId", txtDelSubCatIdHiden.Value);
                string mStrConString = Convert.ToString(Session["SystemUserSqlConnectionString"]);

                DataTable dtRemoveData = SqlHelper.ReadTable("SP_DeleteSubCategoryBusiness", mStrConString, true,
                                                          SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(mDicInputs["TalukatID"])),
                                                          SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                                          SqlHelper.AddInParam("@intAmId", SqlDbType.BigInt, Convert.ToInt32(mDicInputs["SubCatId"])));

                long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                if (dtRemoveData.Rows.Count > 0)
                {
                    GlobalFunctions.saveInsertUserAction("Sub_Category_Master", "[Business SubCategory Master Delete]:Deletion of SubCategory with Id : " + Convert.ToInt32(mDicInputs["SubCatId"]), intTalukaId, lngCompanyId, Request); //Call to user Action Log

                    //Delete image from location
                    string strDelImg = txtDeleteSubCatImage.Value;
                    if (File.Exists(Server.MapPath(strDelImg)))
                        File.Delete(Server.MapPath(strDelImg));

                    showSubCategoryDetails();
                }
                else
                {
                    //Code for error in deletion of sub category
                    string message = "Deletion Failed.";
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
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnDeleteProduct_ServerClick", "SubCategoryBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        //Code to display image or icon in gridview
        public string GetImageToDisplay(object pstrPath)
        {
            string mStrSubCategoryImagePath = "";
            if (pstrPath != null)
            {
                mStrSubCategoryImagePath = Convert.ToString(pstrPath);

                return mStrSubCategoryImagePath;
            }
            else
            {
                return mStrSubCategoryImagePath;
            }
        }

        //Code to display image or icon in gridview
        public string GetIconToDisplay(object pstrPath)
        {
            string mStrSubCategoryIconPath = "";
            if (pstrPath != null)
            {
                mStrSubCategoryIconPath = "fa " + pstrPath;
                return mStrSubCategoryIconPath;
            }
            return mStrSubCategoryIconPath;
        }

        //Upload an icon in grid view column
        protected void btnUploadIcon_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string Imgclass = txtIconvalue.Text;
                if (Imgclass.Trim() != "")
                {
                    //Messagebox to display success in creation of subcategory
                    string message = "Icon Uploaded successfully";
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
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnUploadIcon_ServerClick", "SubCategoryBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }


        //Hide error message for subcategory name duplicacy
        protected void txtSubCategoryName_TextChanged(object sender, EventArgs e)
        {
            txtSubCategoryName.BackColor = System.Drawing.Color.White;
            lblSubCatValidate.Visible = false;
        }

        protected void btnImportToExcel_ServerClick(object sender, EventArgs e)
        {

            //SubCategoryDetailImagePath
            //int i;
            //HttpPostedFile f;
            //int introwCount = 0, intFailureCount = 0;
            //List<string> lstCategoryName = new List<string>();
            //List<string> lstCatFilePaths = new List<string>();
            //HttpFileCollection uploadedFiles = Request.Files;

            //string strStoredFilepath = "";
            //string strFileNm = "";
            //long lngAmId = 0;

            //try
            //{
            //    bool blnHasImage = false;
            //    //Read an Excel file sheet for category Details
            //    string ext = Path.GetExtension(FileUploadControl1.FileName).ToLower();
            //    string path = Server.MapPath(FileUploadControl1.PostedFile.FileName);
            //    FileUploadControl1.SaveAs(path);
            //    string ConStr = string.Empty;
            //    if (ext.Trim() == ".xls")
            //    {
            //        ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            //    }
            //    else if (ext.Trim() == ".xlsx")
            //    {
            //        ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            //    }

            //    OleDbConnection con = new OleDbConnection(ConStr);
            //    if (con.State == ConnectionState.Closed)
            //    {
            //        con.Open();
            //    }

            //    string SelectCategoryquery = "select * from [Sheet2$]";
            //    OleDbCommand cmd = new OleDbCommand(SelectCategoryquery, con);
            //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //    DataTable dtCategoryDetails = new DataTable();
            //    DataSet ds = new DataSet();
            //    da.Fill(dtCategoryDetails);



            //    for (i = 0; i < uploadedFiles.Count - 1; i++)
            //    {
            //        f = uploadedFiles[i];
            //        blnHasImage = false;


            //        //Store Category Description and Images
            //        if (f.ContentLength > 0 && f.FileName != "MStoreInfo.xlsx")
            //        {
            //            strFileNm = f.FileName;
            //            foreach (DataRow drImgColVal in dtCategoryDetails.Rows)
            //            {
            //                //Code to check whether Category names already exist in db.
            //                lngAmId = 0;

            //                int IsActive = 0;
            //                if (drImgColVal[3].ToString() == "Y") { IsActive = 1; }

            //                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
            //                int intTalukaId = objTal.TalukaID;
            //                string strId = Convert.ToString(intTalukaId);
            //                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
            //                DataTable dtCategoryid = SqlHelper.ReadTable("SELECT CM_bIntCatId FROM Category_Master_" + strId + "  WHERE CM_bItIsActive = 1 AND CM_iNtEntryType=1 AND CM_vCharName_En = '" + drImgColVal[0].ToString() + "'", conString, false);

            //                int intCategoryid = -1;
            //                if (dtCategoryid.Rows.Count > 0)
            //                {
            //                    DataRow rowCategoryId = dtCategoryid.Rows[0];
            //                    intCategoryid = Convert.ToInt32(rowCategoryId["CM_bIntCatId"]);
            //                }

            //                string strQuery = "Select SCM_bIntSubCatId from Sub_Category_Master_" + Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID) + " where SCM_vCharName_En Like '" + drImgColVal[1].ToString() + "' AND SCM_iNtEntryType=1";
            //                DataTable dtCatIdData = SqlHelper.ReadTable(strQuery, false);

            //                if (dtCatIdData.Rows.Count > 0)
            //                {
            //                    DataRow row = dtCatIdData.Rows[0];
            //                    lngAmId = Convert.ToInt32(row["SCM_bIntSubCatId"]);
            //                }

            //                if (strFileNm.Equals(drImgColVal[5].ToString()) == true)
            //                {
            //                    //string strFilepath = Path.Combine(Path.GetPathRoot(strFileNm),Path.GetFileName(strFileNm));
            //                    string strFilepath = Path.GetFullPath(strFileNm);
            //                    strStoredFilepath = GetSafeFileNameOnLocation(strFilepath, GlobalVariables.SubCategoryDetailImagePath);
            //                    f.SaveAs(strStoredFilepath);

            //                    string strError = ChkImageSize(strStoredFilepath);

            //                    blnHasImage = true;
            //                    if (strError.Trim() == "")
            //                    {
            //                        //Insert category details with an image of an appropriate size
            //                        strStoredFilepath = String.Format("{0}/{1}", GlobalVariables.SubCategoryDetailImagePath, Path.GetFileName(strStoredFilepath));
            //                        lstCatFilePaths.Add(strStoredFilepath); //Store file path for future mapping before storing in database

            //                        //Code to insert or modify Category Business master.
            //                        DataTable dtCatData = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
            //                            SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
            //                            SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, drImgColVal[1].ToString()),
            //                            SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, drImgColVal[2].ToString()),
            //                            SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
            //                            SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, drImgColVal[4].ToString()),
            //                            SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
            //                            SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, intCategoryid),
            //                            SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
            //                            SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));

            //                        //break;
            //                    }
            //                    else
            //                    {
            //                        //Insert category details without an image due to invalid size
            //                        intFailureCount = intFailureCount + 1;
            //                        strStoredFilepath = null;
            //                        //Code to insert or modify Category Business master.
            //                        DataTable dtCatData = SqlHelper.ReadTable("         ", true,
            //                            SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
            //                            SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, drImgColVal[1].ToString()),
            //                            SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, drImgColVal[2].ToString()),
            //                            SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
            //                            SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, drImgColVal[4].ToString()),
            //                            SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
            //                            SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, intCategoryid),
            //                            SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
            //                            SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));

            //                        //break;
            //                    }

            //                }
            //            }  //End of Store Category Description and Images
            //        }

            //        //To insert Category when no images are selected
            //        if (blnHasImage == false && f.FileName == "MStoreInfo.xlsx")
            //        {
            //            foreach (DataRow drImgColVal in dtCategoryDetails.Rows)
            //            {
            //                //Code to check whether Category names already exist in db.
            //                lngAmId = 0;

            //                int IsActive = 0;
            //                if (drImgColVal[3].ToString() == "Y") { IsActive = 1; }

            //                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
            //                int intTalukaId = objTal.TalukaID;
            //                string strId = Convert.ToString(intTalukaId);
            //                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
            //                DataTable dtCategoryid = SqlHelper.ReadTable("SELECT CM_bIntCatId FROM Category_Master_" + strId + "  WHERE CM_bItIsActive = 1 AND CM_iNtEntryType=1 AND CM_vCharName_En = '" + drImgColVal[0].ToString() + "'", conString, false);
            //                int intCategoryid = -1;
            //                if (dtCategoryid.Rows.Count > 0)
            //                {
            //                    DataRow rowCategoryId = dtCategoryid.Rows[0];
            //                    intCategoryid = Convert.ToInt32(rowCategoryId["CM_bIntCatId"]);
            //                 }

            //                string strQuery = "Select SCM_bIntSubCatId,SCM_vCharSubCatImgPath from Sub_Category_Master_" + Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID) + " where SCM_vCharName_En Like '" + drImgColVal[1].ToString() + "' AND SCM_iNtEntryType=1";
            //                DataTable dtSubCatIdData = SqlHelper.ReadTable(strQuery, false);
            //                strStoredFilepath = null;
            //                if (dtSubCatIdData.Rows.Count > 0)
            //                {
            //                    DataRow row1 = dtSubCatIdData.Rows[0];
            //                    lngAmId = Convert.ToInt32(row1["SCM_bIntSubCatId"]);
            //                    if (row1["SCM_vCharSubCatImgPath"].ToString().Trim() != "")
            //                    { strStoredFilepath = row1["SCM_vCharSubCatImgPath"].ToString(); }
            //                }


            //                //Code to insert or modify SubCategory Business master with fontawesome class
            //                DataTable dtCatData = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
            //                            SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
            //                            SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, drImgColVal[1].ToString()),
            //                            SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, drImgColVal[2].ToString()),
            //                            SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
            //                            SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, drImgColVal[4].ToString()),
            //                            SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
            //                            SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, intCategoryid),
            //                            SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
            //                            SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));

            //             }
            //        }

            //        // } //End of Store Category Description and Images
            //    }//Upload files rotate

            //    //Code to insert an action log for Category Details insertion here.
            //    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);

            //    introwCount = introwCount + lstCatFilePaths.Count - intFailureCount;
            //    string strActionMsg = "[Business SubCategory Master] : " + introwCount + " number of rows inserted into database by " + Session["SystemUser"].ToString();
            //    GlobalFunctions.saveInsertUserAction("SubCategory_Master", strActionMsg, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID), lngCompanyId, Request); //Call to user Action Log

            //    if (intFailureCount > 0)
            //    {
            //        string strFailureMsgCount = "[Business SubCategory Master] : " + intFailureCount + " images insertion failed!";

            //        //Code for empty field validation
            //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //        sb.Append("<script type = 'text/javascript'>");
            //        sb.Append("window.onload=function(){");
            //        sb.Append(" bootbox.alert('");
            //        sb.Append(strFailureMsgCount);
            //        sb.Append("')};");
            //        sb.Append("</script>");
            //        ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());
            //    }
            //    showSubCategoryDetails(); //Call to display SubCategories.

            //    Div2.InnerHtml = "SubCategory Added Successfully!!!";
            //}
            //catch (Exception exError)
            //{
            //    long pLngErr = -1;
            //    if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
            //        pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
            //    pLngErr = GlobalFunctions.ReportError("btnImportToExcel_ServerClick", "BusinessSubCategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
            //    Div2.Attributes["class"] = "alert alert-info blink-border";
            //    Div2.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            //}

            #region oldsubcatdetails
            ////CategoryDetailImagePath
            //int i;
            //HttpPostedFile f;
            //int introwCount = 0, intFailureCount = 0;
            //List<string> lstCategoryName = new List<string>();
            //List<string> lstCatFilePaths = new List<string>();
            //HttpFileCollection uploadedFiles = Request.Files;

            //try
            //{

            //    //Read an Excel file sheet for category Details
            //    string ext = Path.GetExtension(FileUploadControl1.FileName).ToLower();
            //    string path = Server.MapPath(FileUploadControl1.PostedFile.FileName);
            //    FileUploadControl1.SaveAs(path);
            //    string ConStr = string.Empty;
            //    if (ext.Trim() == ".xls")
            //    {
            //        ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            //    }
            //    else if (ext.Trim() == ".xlsx")
            //    {
            //        ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            //    }

            //    OleDbConnection con = new OleDbConnection(ConStr);
            //    if (con.State == ConnectionState.Closed)
            //    {
            //        con.Open();
            //    }

            //    string SelectCategoryquery = "select * from [Sheet2$]";
            //    OleDbCommand cmd = new OleDbCommand(SelectCategoryquery, con);
            //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //    DataTable dtCategoryDetails = new DataTable();
            //    DataSet ds = new DataSet();
            //    da.Fill(dtCategoryDetails);

            //    string strStoredFilepath = "";
            //    string strFileNm = "";
            //    long lngAmId = 0;
            //    //Store Category Description and Images
            //    foreach (DataRow drImgColVal in dtCategoryDetails.Rows)
            //    {
            //        //Code to check whether Category names already exist in db.
            //        lngAmId = 0;
            //        int IsActive = 0;
            //        if (drImgColVal[3].ToString() == "Y") { IsActive = 1; }

            //        if (chkSubCatExist(drImgColVal[0].ToString())==true)
            //            {
            //                lngAmId = 1;
            //                break;
            //            }

            //        TalukaData objTal = (TalukaData)Session["TalukaDetails"];
            //        int intTalukaId = objTal.TalukaID;
            //        string strId = Convert.ToString(intTalukaId);
            //        string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
            //        DataTable dtCategoryid = SqlHelper.ReadTable("SELECT CM_bIntCatId FROM Category_Master_" + strId + "  WHERE CM_bItIsActive = 1 AND CM_iNtEntryType=0 AND CM_vCharName_En = '" + drImgColVal[0].ToString() + "'", conString, false);



            //        for (i = 0; i < uploadedFiles.Count - 1; i++)
            //        {
            //            f = uploadedFiles[i];

            //            if (f.ContentLength > 0 && f.FileName != "MStoreInfo.xlsx")
            //            {
            //                strFileNm = f.FileName;
            //                if (strFileNm.Equals(drImgColVal[3].ToString()) == true)
            //                {

            //                    string strFilepath = Path.GetFullPath(strFileNm);
            //                    string strError = ChkImageSize(strFilepath);

            //                    if (strError == "")
            //                    {
            //                        //Code to upload file on server
            //                        strStoredFilepath = GetSafeFileNameOnLocation(strFilepath, GlobalVariables.CategoryDetailImagePath);
            //                        f.SaveAs(strStoredFilepath);
            //                        strStoredFilepath = String.Format("{0}/{1}", GlobalVariables.CategoryDetailImagePath, Path.GetFileName(strStoredFilepath));
            //                        lstCatFilePaths.Add(strStoredFilepath); //Store file path for future mapping before storing in database

            //                        //Code to insert or modify Category Business master.
            //                        DataTable dtCatData = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
            //                            SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
            //                            SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, drImgColVal[1].ToString()),
            //                            SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, drImgColVal[2].ToString()),
            //                            SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
            //                            SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, null),
            //                            SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
            //                            SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, dtCategoryid.Rows[0][0].ToString()),
            //                            SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
            //                            SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));

            //                        DataRow row = dtCatData.Rows[0];
            //                        int intCatid = Convert.ToInt32(row["SCM_bIntCatId"]);
            //                        break;
            //                    }
            //                    else
            //                    {
            //                        intFailureCount = intFailureCount + 1;
            //                        strStoredFilepath = null;
            //                        //Code to insert or modify Category Business master.
            //                        DataTable dtCatData = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
            //                            SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
            //                            SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, drImgColVal[1].ToString()),
            //                            SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, drImgColVal[2].ToString()),
            //                            SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
            //                            SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, null),
            //                            SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
            //                            SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, dtCategoryid.Rows[0][0].ToString()),
            //                            SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
            //                            SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));

            //                        DataRow row = dtCatData.Rows[0];
            //                        int intCatid = Convert.ToInt32(row["SCM_bIntCatId"]);
            //                        break;
            //                    }
            //                }
            //                else
            //                {
            //                    strStoredFilepath = null;
            //                    //Code to insert or modify Category Business master.
            //                    DataTable dtCatData = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
            //                            SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
            //                            SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, drImgColVal[1].ToString()),
            //                            SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, drImgColVal[2].ToString()),
            //                            SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
            //                            SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, drImgColVal[4].ToString()),
            //                            SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
            //                            SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, dtCategoryid.Rows[0][0].ToString()),
            //                            SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
            //                            SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));

            //                    DataRow row = dtCatData.Rows[0];
            //                    int intCatid = Convert.ToInt32(row["SCM_bIntCatId"]);
            //                    break;
            //                }
            //            }
            //        }

            //    }
            //    //Code to insert an action log for Category Details insertion here.
            //    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);

            //    introwCount = introwCount + lstCatFilePaths.Count - intFailureCount;
            //    string strActionMsg = "[Business SubCategory Master] : " + introwCount + " number of rows inserted into database by " + Session["SystemUser"].ToString();
            //    GlobalFunctions.saveInsertUserAction("SubCategory_Master", strActionMsg, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID), lngCompanyId, Request); //Call to user Action Log

            //    if (intFailureCount > 0)
            //    {
            //        string strFailureMsgCount = "[Business SubCategory Master] : " + intFailureCount + " images insertion failed!";

            //        //Code for empty field validation
            //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //        sb.Append("<script type = 'text/javascript'>");
            //        sb.Append("window.onload=function(){");
            //        sb.Append(" bootbox.alert('");
            //        sb.Append(strFailureMsgCount);
            //        sb.Append("')};");
            //        sb.Append("</script>");
            //        ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());
            //    }
            //    showSubCategoryDetails(); //Call to display SubCategories.

            //    Div2.InnerHtml = "SubCategory Added Successfully!!!";
            //}
            //catch (Exception exError)
            //{
            //    long pLngErr = -1;
            //    if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
            //        pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
            //    pLngErr = GlobalFunctions.ReportError("btnImportToExcel", "BusinessSubCategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
            //    Div2.Attributes["class"] = "alert alert-info blink-border";
            //    Div2.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            //}

            #endregion oldsubcatdetails

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

                            //string SelectCategoryquery = "select * from [Sheet2$]";
                            //OleDbCommand cmd = new OleDbCommand(SelectCategoryquery, con);
                            //OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                            //DataTable dtCategoryDetails = new DataTable();
                            //DataSet ds = new DataSet();
                            //da.Fill(dtCategoryDetails);



                            for (i = 0; i < uploadedFiles.Count; i++)
                            {
                                f = uploadedFiles[i];
                                blnHasImage = false;


                                //Store Category Description and Images
                                if (f.ContentLength > 0 && (f.FileName == "SubCategory.csv"))
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

                                        if (!Directory.Exists(Server.MapPath(GlobalVariables.SubCategoryDetailImagePath + "/" + intTalId)))
                                            Directory.CreateDirectory(Server.MapPath(GlobalVariables.SubCategoryDetailImagePath + "/" + intTalId));


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
                                            DataTable dtCategoryid = SqlHelper.ReadTable("SELECT CM_bIntCatId FROM Category_Master_17  WHERE CM_bItIsActive = 1 AND CM_iNtEntryType=1 AND CM_vCharName_En = @Name ", conString, false,
                                                SqlHelper.AddInParam("@Name",SqlDbType.VarChar,drImgColVal[0].ToString()));

                                            int intCategoryid = -1;
                                            if (dtCategoryid.Rows.Count > 0)
                                            {
                                                DataRow rowCategoryId = dtCategoryid.Rows[0];
                                                intCategoryid = Convert.ToInt32(rowCategoryId["CM_bIntCatId"]);
                                            }
                                            else
                                            {
                                                Div2.Attributes["class"] = "alert alert-info";
                                                Div2.InnerHtml = "Category Name not exist.";
                                                return;
                                            }
                                            if (intCategoryid != -1)
                                            {
                                                string strQuery = "Select SCM_bIntSubCatId from Sub_Category_Master_17 where SCM_vCharName_En =@Name AND SCM_iNtEntryType=1";
                                                DataTable dtCatIdData = SqlHelper.ReadTable(strQuery, false,                                                    
                                                    SqlHelper.AddInParam("@Name", SqlDbType.VarChar, drImgColVal[1].ToString()));

                                                if (dtCatIdData.Rows.Count > 0)
                                                {
                                                    DataRow row = dtCatIdData.Rows[0];
                                                    lngAmId = Convert.ToInt32(row["SCM_bIntSubCatId"]);
                                                }
                                                string strClass = Convert.ToString(drImgColVal[4]);
                                                //if (strFileNm.Equals(drImgColVal[5].ToString()) == true)
                                                //{
                                                //string strFilepath = Path.Combine(Path.GetPathRoot(strFileNm),Path.GetFileName(strFileNm));

                                                string strError = "";
                                                if (strFileNm != null)
                                                {

                                                    string mStrFileExtension = Path.GetExtension(strFileNm);
                                                    if (mStrFileExtension != ".xls")
                                                    {
                                                        string strFilepath = Path.GetFullPath(strFileNm);
                                                        strStoredFilepath = GetSafeFileNameOnLocation(strFilepath, GlobalVariables.SubCategoryDetailImagePath + "/" + strId);
                                                        f.SaveAs(strStoredFilepath);

                                                        if (strFileNm == drImgColVal[5].ToString())
                                                        {
                                                            strError = ChkImageSize(strStoredFilepath);
                                                            if (strError == "")
                                                            {
                                                                strStoredFilepath = String.Format("{0}/{1}", GlobalVariables.SubCategoryDetailImagePath + "/" + strId, Path.GetFileName(strStoredFilepath));
                                                                lstCatFilePaths.Add(strStoredFilepath); //Store file path for future mapping before storing in database


                                                                //Code to insert or modify Category Business master.
                                                                DataTable dtCatData = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
                                                                    SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                                    SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[1])),
                                                                    SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[2])),
                                                                    SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
                                                                    SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, (strClass.Trim() == "" ? null : strClass)),
                                                                    SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                                                                    SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, intCategoryid),
                                                                    SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                                                    SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));



                                                            }
                                                            else
                                                            {
                                                                string message = "Image size should be between 16X16 and 25X25 pixels";
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
                                                        DataTable dtCatData = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
                                                      SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                      SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[1])),
                                                      SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[2])),
                                                      SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
                                                      SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, (strClass.Trim() == "" ? null : strClass)),
                                                      SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, null),
                                                      SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, intCategoryid),
                                                      SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                                      SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));

                                                    }
                                                }
                                                //else
                                                //{
                                                //    if (strStoredFilepath == drImgColVal[5].ToString())
                                                //    {
                                                //        strStoredFilepath = String.Format("{0}/{1}", GlobalVariables.SubCategoryDetailImagePath + "/" + strId, Path.GetFileName(strStoredFilepath));
                                                //        lstCatFilePaths.Add(strStoredFilepath); //Store file path for future mapping before storing in database

                                                //        //Code to insert or modify Category Business master.
                                                //        DataTable dtCatData = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
                                                //            SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                //            SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[1])),
                                                //            SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[2])),
                                                //            SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
                                                //            SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, (strClass.Trim() == "" ? null : strClass)),
                                                //            SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, null),
                                                //            SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, intCategoryid),
                                                //            SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                                //            SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));
                                                //        strFileNm = "";
                                                //    }
                                                //}

                                            }
                                        }
                                    }
                                }

                                //To insert Category when no images are selected
                                //if (blnHasImage == false && (f.FileName == "MStoreInfo.xlsx" || f.FileName == "MStoreInfoBusiness.xlsx"))
                                //{
                                //    if (dtCategoryDetails.Rows.Count <= 0)
                                //    {
                                //        blnExcelIsEmpty = true;
                                //    }

                                //    foreach (DataRow drImgColVal in dtCategoryDetails.Rows)
                                //    {
                                //        //Code to check whether Category names already exist in db.
                                //        lngAmId = 0;
                                //        if (Convert.ToString(drImgColVal[0]).Trim() != "")
                                //        {
                                //            string strClass = Convert.ToString(drImgColVal[5]);
                                //            int IsActive = 0;
                                //            if (Convert.ToString(drImgColVal[3]) == "Y") { IsActive = 1; }

                                //            TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                                //            int intTalukaId = objTal.TalukaID;
                                //            string strId = Convert.ToString(intTalukaId);
                                //            string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                                //            DataTable dtCategoryid = SqlHelper.ReadTable("SELECT CM_bIntCatId FROM Category_Master_" + strId + "  WHERE CM_bItIsActive = 1 AND CM_iNtEntryType=1 AND CM_vCharName_En = '" + drImgColVal[0].ToString() + "'", conString, false);
                                //            int intCategoryid = -1;
                                //            if (dtCategoryid.Rows.Count > 0)
                                //            {
                                //                DataRow rowCategoryId = dtCategoryid.Rows[0];
                                //                intCategoryid = Convert.ToInt32(rowCategoryId["CM_bIntCatId"]);
                                //            }

                                //            if (intCategoryid != -1)
                                //            {
                                //                string strQuery = "Select SCM_bIntSubCatId,SCM_vCharSubCatImgPath from Sub_Category_Master_" + Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID) + " where SCM_vCharName_En Like '" + drImgColVal[1].ToString() + "' AND SCM_iNtEntryType=1";
                                //                DataTable dtSubCatIdData = SqlHelper.ReadTable(strQuery, false);
                                //                strStoredFilepath = null;
                                //                if (dtSubCatIdData.Rows.Count > 0)
                                //                {
                                //                    DataRow row1 = dtSubCatIdData.Rows[0];
                                //                    lngAmId = Convert.ToInt32(row1["SCM_bIntSubCatId"]);
                                //                    if (row1["SCM_vCharSubCatImgPath"].ToString().Trim() != "")
                                //                    { strStoredFilepath = Convert.ToString(row1["SCM_vCharSubCatImgPath"]); }
                                //                }


                                //                //Code to insert or modify SubCategory Business master with fontawesome class
                                //                DataTable dtCatData = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
                                //                            SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                //                            SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[1])),
                                //                            SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[2])),
                                //                            SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
                                //                            SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, (strClass.Trim() == "" ? null : strClass)),
                                //                            SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                                //                            SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, intCategoryid),
                                //                            SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                //                            SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));
                                //            }
                                //            else
                                //            {
                                //                intFailureCount++;
                                //            }
                                //        }
                                //    }//end of foreach

                                //}

                                // } //End of Store Category Description and Images
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
                            showSubCategoryDetails(); //Call to display SubCategories.

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