using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace Admin_CommTrex
{
    public partial class InformationImageMaster : System.Web.UI.Page
    {
        Label lblImageid_GridView;
        public static Dictionary<int, ImageDetails> lstImg = new Dictionary<int, ImageDetails>();
        public static List<string> lstImgList = new List<string>();
        public static string strInformationid;
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
                    showCategory();//Call to load Information type in dropdown of Add new form
                    showInformationImagegrid(); //Call to Information Image grid display
                    clearFields(); //Call to clear all textboxes

                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }

        //Display all Information Details in combox box for add new
        public void showCategory()
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;//select IM_bIntInfoId,IM_vCharInfoName_En,isnull(IM_nVarInfoName_Reg,'Not Set') from Information_Master_28 
                DataTable dtInformationList = SqlHelper.ReadTable("Select IM_bIntInfoId,IM_vCharInfoName_En,isnull(IM_nVarInfoName_Reg,'Not Set') as 'IM_nVarInfoName_Reg' from Information_Master_" + strId, false);

                //Display Information Name
                cmbInformationType.DataSource = dtInformationList;
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
                pLngErr = GlobalFunctions.ReportError("showCategory", "InformationImageMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }

        }

        //To display all information image on grid for modification and deletion
        public void showInformationImagegrid()
        {
            try
            {
                txtRegName.Text = "";
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;

                string mStrSelectQuery = "SELECT ig.IIG_bIntId,ig.IIG_bIntInfoId,";
                mStrSelectQuery += " im.IM_vCharInfoName_En,isnull(im.IM_nVarInfoName_Reg,'Not Set') as 'IM_nVarInfoName_Reg',ig.IIG_vCharImageDescription_En,isnull(ig.IIG_nVarImageDescription_Reg,'Not Set') as 'IIG_nVarImageDescription_Reg',";
                mStrSelectQuery += "  Reverse(left(reverse(ig.IIG_vCharImagePath), charindex('/',reverse(ig.IIG_vCharImagePath))-1)) as 'ImageName',ig.IIG_vCharImagePath";
                mStrSelectQuery += "  FROM Information_Image_Gallery_" + strId + " ig,Information_Master_" + strId + " im";
                mStrSelectQuery += "  WHERE ig.IIG_bIntInfoId=im.IM_bIntInfoId "; //AND ig.IIG_IsActive = 1
                mStrSelectQuery += " Order By ig.IIG_bIntId";

                DataTable dtShowGallery = SqlHelper.ReadTable(mStrSelectQuery, false);
                grdInformationImages.DataSource = dtShowGallery;
                grdInformationImages.DataBind();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showInformationImagegrid", "InformationImageMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }


        //Call on click of save button in Add Information Image form
        protected void btnInformationSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (btnInformationSave.Attributes["btn-action"] == "Save")
                {
                    if (validateAddInformationImageInput() == false)
                    {
                        int intImageInformId = 0;
                        int intInformationId = 0;
                        if (cmbInformationType.SelectedIndex >= 0)
                            intInformationId = Convert.ToInt32(cmbInformationType.SelectedValue);
                        else
                            intInformationId = Convert.ToInt32(txtRegName.Text);

                        for (int i = 1; i <= lstImg.Count; i++)
                        {
                            ImageDetails objImgD = lstImg[i];
                            string ImgPath = objImgD.strImagePath;
                            int IsActive = objImgD.intIsDefault;
                            string ImgName = objImgD.strImageDescription;
                            string ImgDescription = objImgD.strImageRegDescription;
                            string ImgactualPath = objImgD.stractualImgPath;
                            insertInformationImgInDb(intImageInformId, intInformationId, ImgName, ImgDescription, ImgactualPath, ImgPath, IsActive); //call to save function
                        }

                        btnInformationSave.Attributes["btn-action"] = "New";
                        btnInformationSave.InnerHtml = "<i class=\"fa fa-plus-square\"></i> New";
                        clearFields();
                        CustImg.Disabled = true;
                        cmbInformationType.Enabled = false;



                        //Update the grid view for display of newly added record
                        showInformationImagegrid();

                        //Clear all images path detail before insertion of new ones.
                        lstImgList.Clear();

                        //Messagebox to display success in creation of subcategory
                        string message = "Image for Information created successfully";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append(" bootbox.alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());

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
                    //If user clicks New text button on add form
                    btnInformationSave.Attributes["btn-action"] = "Save";
                    btnInformationSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> Save";
                    btnInformationClear.Attributes["btn-action"] = "Cancel";
                    CustImg.Disabled = false;
                    cmbInformationType.Enabled = true;
                    txtInformationRegName.Enabled = true;
                    //cmbInformationRegType.Enabled = true;
                    clearFields();
                    lstImg.Clear();
                    SetMessage(false, "Press Save to store SubCategory");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnInformationSave_ServerClick", "InformationImageMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }


        private void SetMessage(bool pBlnIsError, string pStrError)
        {
            actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            actionInfo.InnerHtml = pStrError;
        }

        //To save Information Image content to Database
        public void insertInformationImgInDb(int pintImageInformId, int pIntInformationId, string pstrImageName, string pstrImageDescription, string pstrImgActualPath, string pstrInformationImgImage, int pintIsActive)
        {
            try
            {
                DataTable dtInsertImageInformation = new DataTable();
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string actualImgPath;

                //path to store image is Content/Information/map.png
                string mStrSubCategoryImagePath = pstrInformationImgImage.Replace("//", "/");
                actualImgPath = pstrImgActualPath;

                //Insert query code here
                Dictionary<string, string> mDicInputs = new Dictionary<string, string>();
                mDicInputs.Add("InformationId", Convert.ToString(pIntInformationId));
                mDicInputs.Add("ImageName", pstrImageName);
                mDicInputs.Add("ImageRegName", pstrImageDescription);
                mDicInputs.Add("ImagePath", mStrSubCategoryImagePath);
                mDicInputs.Add("ImageActive", Convert.ToString(pintIsActive));

                CopyFileSafely(actualImgPath, mStrSubCategoryImagePath);

                dtInsertImageInformation = SqlHelper.ReadTable("SP_insertImageInformationDetail", true,
                        SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, intTalukaId),
                        SqlHelper.AddInParam("@bintInformationID", SqlDbType.BigInt, Convert.ToInt32(mDicInputs["InformationId"])),
                        SqlHelper.AddInParam("@vCharInformationName", SqlDbType.VarChar, mDicInputs["ImageName"]),
                        SqlHelper.AddInParam("@nVarInformationName_Reg", SqlDbType.NVarChar, mDicInputs["ImageRegName"]),
                        SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, Convert.ToInt32(mDicInputs["ImageActive"])),
                        SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, mDicInputs["ImagePath"]),
                        SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, pintImageInformId));

                if (dtInsertImageInformation.Rows.Count > 0)
                {
                    long mLngImgInfoId = Convert.ToInt64(dtInsertImageInformation.Rows[0][0]);
                    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                    if (pIntInformationId == 0)
                        GlobalFunctions.saveInsertUserAction("Information_Image_Gallery", "[Information Image Master Insert]:Insertion of Information Image with Id : " + mLngImgInfoId + " and Image Uploaded : " + mDicInputs["ImagePath"], intTalukaId, lngCompanyId, Request); //Call to user Action Log
                    else
                        GlobalFunctions.saveInsertUserAction("Information_Image_Gallery", "[Information Image Master Update]:Updation of Information Image with Id : " + pIntInformationId + " and Image Uploaded : " + mDicInputs["ImagePath"], intTalukaId, lngCompanyId, Request); //Call to user Action Log
                }
                else
                {
                    //Code for error in insertion of sub category
                    string message = "Insertion  of ImageInformation Failed.";
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
                pLngErr = GlobalFunctions.ReportError("insertInformationImgInDb", "InformationImageMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }

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
                    Directory.CreateDirectory(Server.MapPath(GlobalVariables.InformationTemporaryPath));


                File.Copy(pStrSourceFile, pStrDestination);
                File.Delete(pStrSourceFile);

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("CopyFileSafely", "InformationImageMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
            return pStrDestination;
        }

        //To validate all inputs for Image Saving 
        public bool validateAddInformationImageInput()
        {
            bool chkEmpty = false;
            if (cmbInformationType.Text == "--Select Information Type--" || txtRegName.Text.Trim() == "") { chkEmpty = true; }
            //if (cmbImageIsActive.SelectedIndex == 0) { chkEmpty = true; }
            if (hfImgCount.Text == "0") { chkEmpty = true; }
            return chkEmpty;
        }

        //To validate all inputs for Image Saving 
        public bool validateModifyInformationImageInput(DropDownList drlObject)
        {
            bool chkEmpty = false;
            if (drlObject.Text == "--Select Information Type--") { chkEmpty = true; }
            if (cmbImageIsActive.SelectedIndex == -1) { chkEmpty = true; }
            return chkEmpty;
        }

        protected void btnInformationClear_ServerClick(object sender, EventArgs e)
        {
            clearFields();
            lstImgList.Clear();
            cmbInformationType.Enabled = false;
            btnInformationSave.Attributes["btn-action"] = "New";
        }

        //To clear all fields values on user input
        public void clearFields()
        {
            lblErrorMsg.Visible = false;
            txtInformImageId.Text = "";
            cmbInformationType.SelectedIndex = -1;
            txtInformationRegName.Text = "";
            txtInformationRegName.Enabled = false;
            txtRegName.Text = "";
            txtImgName.Text = "";
            txtImgDescription.Text = "";
            cmbImageIsActive.SelectedIndex = -1;
            txtImgName.BorderColor = System.Drawing.Color.Black;
            txtImgDescription.BorderColor = System.Drawing.Color.Black;
            hfImgCount.Text = "0";
            cmbInformationType.Focus();
        }

        public class ImageDetails
        {
            public ImageDetails(int pintisdefault, string pstrImageDescription, string pstrImageRegDescription, string pstrImagePath, string pstrOriginalPath)
            {
                intIsDefault = pintisdefault;
                strImageDescription = pstrImageDescription;
                strImageRegDescription = pstrImageRegDescription;
                strImagePath = pstrImagePath;
                stractualImgPath = pstrOriginalPath;
            }
            public int intIsDefault { get; set; }
            public string strImageDescription { get; set; }
            public string strImageRegDescription { get; set; }
            public string strImagePath { get; set; }
            public string stractualImgPath { get; set; }
        }

        //To upload an image on user selection
        protected void btnImgUpload_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int IsActive = 0;
                string ImgName = txtImgName.Text;
                string ImgDescription = txtImgDescription.Text;
                int countImg = Convert.ToInt32(hfImgCount.Text);
                if (countImg == 0) { IsActive = 1; } else { IsActive = cmbImageIsActive.SelectedIndex; }
                if (countImg <= 7)
                {

                    //Path to store an image : Content/Information/TalukaId/map.png
                    if (txtImgName.Text.Trim() != "" && txtImgDescription.Text.Trim() != "")
                    {
                        int intInformationId = 0;
                        string strTalukaid = Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID);
                        intInformationId = Convert.ToInt32(strTalukaid);

                        string strMainImgText;
                        txtAddImgPath.Text = Server.MapPath(FileUploadControl.FileName);
                        string strFileName = System.IO.Path.GetFileName(FileUploadControl.PostedFile.FileName);
                        txtAddMainImgPath.Text = GlobalVariables.InformationTemporaryPath + "/" + strFileName;
                        string pStrDestination = "";
                        string strError;
                        if (FileUploadControl.HasFile)
                        {
                            if (!Directory.Exists(pStrDestination))
                                Directory.CreateDirectory(Server.MapPath(GlobalVariables.InformationTemporaryPath));

                            pStrDestination = Path.GetFileName(FileUploadControl.FileName);
                            int count = 1;
                            if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.InformationTemporaryPath), pStrDestination)))
                            {
                                while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.InformationTemporaryPath), pStrDestination)))
                                {
                                    pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileUploadControl.FileName), count++);
                                    pStrDestination = pStrDestination + Path.GetExtension(FileUploadControl.FileName);
                                }
                                FileUploadControl.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.InformationTemporaryPath), pStrDestination));
                            }
                            else
                            {
                                FileUploadControl.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.InformationTemporaryPath), pStrDestination));
                            }

                            strMainImgText = (GlobalVariables.InformationTemporaryPath) + "/" + pStrDestination;
                            strError = GlobalFunctions.ChkImageSize(Server.MapPath(strMainImgText), 256, 256, 64, 64);
                            if (strError == "")
                            {

                                pStrDestination = Path.GetFileName(FileUploadControl.FileName);


                                if (!Directory.Exists((GlobalVariables.ContentFileHostPath) + "//" + intInformationId))
                                {
                                    Directory.CreateDirectory(Server.MapPath((GlobalVariables.ContentFileHostPath) + "//" + intInformationId));
                                }

                                if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.ContentFileHostPath + "//" + intInformationId), pStrDestination)))
                                {
                                    int count1 = 1;
                                    while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.ContentFileHostPath + "//" + intInformationId), pStrDestination)))
                                    {
                                        pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileUploadControl.FileName), count1++);
                                        pStrDestination = pStrDestination + Path.GetExtension(FileUploadControl.FileName);
                                    }
                                    txtAddImgPath.Text = (GlobalVariables.ContentFileHostPath) + "//" + intInformationId + "//" + pStrDestination;
                                }
                                else
                                {
                                    txtAddImgPath.Text = (GlobalVariables.ContentFileHostPath) + "//" + intInformationId + "//" + pStrDestination;
                                }

                                countImg = countImg + 1;
                                ImageDetails objImgD = new ImageDetails(IsActive, ImgName, ImgDescription, txtAddImgPath.Text, txtAddMainImgPath.Text);
                                lstImg.Add(countImg, objImgD);
                                hfImgCount.Text = Convert.ToString(countImg);

                                //code to display image
                                //---------------------------------------------------------------------------
                                StringBuilder htmlTable = new StringBuilder();

                                htmlTable.Append("<center><table border='1'>");
                                htmlTable.Append("<tr style='background-color:green; color: White;'><th colspan='4'>Images</th></tr>");
                                htmlTable.Append("<tr style='color: White;'>");

                                strFileName = (GlobalVariables.InformationTemporaryPath) + "//" + pStrDestination;
                                strFileName = strFileName.Replace("//", "/");
                                lstImgList.Add(strFileName);

                                for (int i = 1; i <= lstImgList.Count; i++)
                                {
                                    if (i == 5)
                                    {
                                        htmlTable.Append("</tr>");
                                        htmlTable.Append("<tr style='background-color:green; color: White;'><th colspan='4'>Images</th></tr>");
                                        htmlTable.Append("<tr style='color: White;'>");
                                    }
                                    htmlTable.Append("<td><img src='" + lstImgList[i - 1].ToString() + "' width='150' height='150'/></td>");
                                }
                                htmlTable.Append("</tr>");
                                htmlTable.Append("</table></center>");

                                DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });
                                //-----------------------------------------------------------------------------

                                //Messagebox to display success of subcategory image upload
                                string message = "Image Uploaded successfully";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append("<script type = 'text/javascript'>");
                                sb.Append("window.onload=function(){");
                                sb.Append(" bootbox.alert('");
                                sb.Append(message);
                                sb.Append("')};");
                                sb.Append("</script>");
                                ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());
                            }
                            else
                            {
                                //Messagebox to display success of subcategory image upload
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
                    else
                    {
                        if (txtImgName.Text.Trim() == "")
                        {
                            txtImgName.Focus();
                            txtImgName.BorderColor = System.Drawing.Color.DarkRed;
                        }
                        if (txtImgDescription.Text.Trim() == "")
                        {
                            txtImgDescription.Focus();
                            txtImgDescription.BorderColor = System.Drawing.Color.DarkRed;
                        }
                    }
                }
                else
                {
                    //Messagebox to display failure of subcategory image upload
                    lblErrorMsg.Visible = true;

                    if (lstImgList.Count > 0)
                    {
                        StringBuilder htmlTable = new StringBuilder();

                        htmlTable.Append("<center><table border='1'>");
                        htmlTable.Append("<tr style='background-color:green; color: White;'><th colspan='4'>Images</th></tr>");
                        htmlTable.Append("<tr style='color: White;'>");

                        for (int i = 1; i <= lstImgList.Count; i++)
                        {
                            if (i == 5)
                            {
                                htmlTable.Append("</tr>");
                                htmlTable.Append("<tr style='background-color:green; color: White;'><th colspan='4'>Images</th></tr>");
                                htmlTable.Append("<tr style='color: White;'>");
                            }
                            htmlTable.Append("<td><img src='" + lstImgList[i - 1].ToString() + "' width='150' height='150'/></td>");
                        }
                        htmlTable.Append("</tr>");
                        htmlTable.Append("</table></center>");

                        DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });
                    }
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnImgUpload_ServerClick", "InformationImageMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }



        protected void grdInformationImages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                    DataTable dt = SqlHelper.ReadTable("select IM_bIntInfoId,IM_vCharInfoName_En,IM_nVarInfoName_Reg from Information_Master_" + strId, false);

                    if (dt.Rows.Count > 0)
                    {
                        DropDownList drdlObject = (DropDownList)e.Row.FindControl("drdlInformationNm");
                        drdlObject.DataSource = dt;
                        drdlObject.DataTextField = "IM_vCharInfoName_En";
                        drdlObject.DataValueField = "IM_bIntInfoId";
                        drdlObject.DataBind();
                        Label lblObject = (Label)e.Row.FindControl("lblInformationName");
                        drdlObject.Items.FindByText(lblObject.Text).Selected = true;
                    }
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("grdInformationImages_RowDataBound", "InformationImageMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void grdInformationImages_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdInformationImages.EditIndex = e.NewEditIndex;


            TalukaData objTal = (TalukaData)Session["TalukaDetails"];
            int intTalukaId = objTal.TalukaID;
            string strId = Convert.ToString(intTalukaId);

            DropDownList drdlObject = (DropDownList)grdInformationImages.Rows[e.NewEditIndex].FindControl("drdlInformationNm");
            Label lblObject = (Label)grdInformationImages.Rows[e.NewEditIndex].FindControl("lblInformationName");
            Label lblInformationImageid = (Label)grdInformationImages.Rows[e.NewEditIndex].FindControl("lblInformationImgID");
            strInformationid = Convert.ToString(drdlObject.SelectedValue);
            lstImg.Clear();
            hfImgCount.Text = "0";
            drdlObject.Visible = true;
            lblObject.Visible = false;
            grdInformationImages.Columns[5].Visible = true;
            showInformationImagegrid(); //Call to Information Image grid display
        }

        protected void grdInformationImages_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdInformationImages.EditIndex = -1;
            showInformationImagegrid(); //Call to Information Image grid display
        }

        protected void grdInformationImages_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label lblimgPath_GridView = (Label)grdInformationImages.Rows[e.RowIndex].FindControl("lblimgInformationImagePath");
                DropDownList drlObject = (DropDownList)grdInformationImages.Rows[e.RowIndex].FindControl("drdlInformationNm");
                if (validateModifyInformationImageInput(drlObject) == false)
                {
                    lblImageid_GridView = (Label)grdInformationImages.Rows[e.RowIndex].FindControl("lblInformationImgID");

                    int intImageInformId = Convert.ToInt32(lblImageid_GridView.Text);
                    int intInformationId = Convert.ToInt32(drlObject.Text);

                    if (lstImg.Count > 0)
                    {
                        //Label lblimgPath_GridView = (Label)grdInformationImages.Rows[e.RowIndex].FindControl("lblimgInformationImagePath");
                        if (File.Exists(Server.MapPath(lblimgPath_GridView.Text)))
                        {
                            File.Delete(Server.MapPath(lblimgPath_GridView.Text));
                        }

                        for (int i = 1; i <= lstImg.Count; i++)
                        {
                            ImageDetails objImgD = lstImg[i];
                            string ImgPath = objImgD.strImagePath;
                            int IsActive = objImgD.intIsDefault;
                            string ImgName = objImgD.strImageDescription;
                            string ImgDescription = objImgD.strImageRegDescription;
                            string ImgactualPath = objImgD.stractualImgPath;
                            insertInformationImgInDb(intImageInformId, intInformationId, ImgName, ImgDescription, ImgactualPath, ImgPath, IsActive); //call to save function
                        }
                    }
                    else
                    {
                        string ImgPath = lblimgPath_GridView.Text;
                        string ImgactualPath = lblimgPath_GridView.Text;
                        int IsActive = cmbImageIsActive.SelectedIndex;
                        string ImgName = txtImgName.Text;
                        string ImgDescription = txtImgDescription.Text;
                        insertInformationImgInDb(intImageInformId, intInformationId, ImgName, ImgDescription, ImgactualPath, ImgPath, IsActive); //call to save function
                    }

                    btnInformationSave.Attributes["btn-action"] = "New";
                    btnInformationSave.InnerHtml = "<i class=\"fa fa-plus-square\"></i> New";
                    clearFields();
                    CustImg.Disabled = true;
                    cmbInformationType.Enabled = false;

                    //Messagebox to display success in creation of subcategory
                    string message = "Image for Information updated successfully";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append(" bootbox.alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());

                    grdInformationImages.EditIndex = -1;

                    //Update the grid view for display of newly added record
                    showInformationImagegrid();
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
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("grdInformationImages_RowUpdating", "InformationImageMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void btnDeleteInformationImg_ServerClick(object sender, EventArgs e)
        {
            try
            {
                // Delete sub category from server
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;

                //Delete image from location where it is stored
                string strImgPath = txtDelInformationImagePath.Value;
                if (File.Exists(Server.MapPath(strImgPath)))
                    File.Delete(Server.MapPath(strImgPath));

                Dictionary<string, string> mDicInputs = new Dictionary<string, string>();

                mDicInputs.Add("TalukatID", Convert.ToString(intTalukaId));
                mDicInputs.Add("ImageId", txtDelInformationImgIdHiden.Value);
                string mStrConString = Convert.ToString(Session["SystemUserSqlConnectionString"]);

                DataTable dtRemoveData = SqlHelper.ReadTable("SP_DeleteInformationImage", true,
                                                          SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(mDicInputs["TalukatID"])),
                                                          SqlHelper.AddInParam("@intAmId", SqlDbType.BigInt, Convert.ToInt32(mDicInputs["ImageId"])));
                if (dtRemoveData.Rows.Count > 0)
                {

                    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                    GlobalFunctions.saveInsertUserAction("Information_Image_Gallery", "[Information Image Master Delete]:Deletion of Information Image with Id : " + txtDelInformationImgIdHiden.Value + " and Image Uploaded : " + strImgPath, intTalukaId, lngCompanyId, Request); //Call to user Action Log

                    showInformationImagegrid();

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
                pLngErr = GlobalFunctions.ReportError("btnDeleteInformationImg_ServerClick", "InformationImageMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }


        //Set value of regional name on basis of selected information name
        protected void cmbInformationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);

                int intInformationId = 0;

                if (cmbInformationType.SelectedIndex >= 0)
                    strInformationid = Convert.ToString(cmbInformationType.SelectedValue);

                intInformationId = Convert.ToInt32(strInformationid);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;//select IM_bIntInfoId,IM_vCharInfoName_En,isnull(IM_nVarInfoName_Reg,'Not Set') from Information_Master_28 
                DataTable dtInformationList = SqlHelper.ReadTable("select IM_bIntInfoId,isnull(IM_nVarInfoName_Reg,'Not Set') as 'IM_nVarInfoName_Reg' from Information_Master_" + strId + "  where IM_bIntInfoId=" + intInformationId, false);
                txtInformationRegName.Text = Convert.ToString(dtInformationList.Rows[0]["IM_nVarInfoName_Reg"]);
                txtRegName.Text = Convert.ToString(dtInformationList.Rows[0]["IM_bIntInfoId"]);
                txtInformationRegName.Enabled = true;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("cmbInformationType_SelectedIndexChanged", "InformationImageMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void btnImportonPassword_ServerClick(object sender, EventArgs e)
        {

            //Code on import of Information image master
            //---------------------------------------------------
            lblMsgError.Visible = false;
            //Code for importing from excedlsheet data and images
            try
            {
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
                        string strImagePath = "";
                        string strFileNm = "";
                        long lngAmId = 0;
                        bool blnExcelIsEmpty = false;
                        int intHasImageDef = 0;


                        //Check whether files being selected or not
                        if (FileUpload1.HasFile)
                        {
                            //Read an Excel file sheet for Information Image Details
                            string ext = Path.GetExtension(FileUpload1.FileName).ToLower();
                            string path = Server.MapPath(FileUpload1.PostedFile.FileName);
                            FileUpload1.SaveAs(path);
                            string ConStr = string.Empty;
                            if (ext.Trim() == ".xls")
                            {
                                ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            }
                            else if (ext.Trim() == ".xlsx")
                            {
                                ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            }
                            else
                            {
                                Div2.Attributes["class"] = "alert alert-info ";
                                Div2.InnerHtml = "Please select Only .xls or .xlsx file for uploading !!!";
                                return;
                            }

                            OleDbConnection con = new OleDbConnection(ConStr);
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }

                            string SelectCategoryquery = "select * from [Sheet4$]";
                            OleDbCommand cmd = new OleDbCommand(SelectCategoryquery, con);
                            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                            DataTable dtImageDetails = new DataTable();
                            DataSet ds = new DataSet();
                            da.Fill(dtImageDetails);

                            //Looping through all images
                            for (i = 0; i < uploadedFiles.Count; i++)
                            {
                                f = uploadedFiles[i];
                                intHasImageDef = 0;

                                if (f.ContentLength > 0 && (f.FileName == "MStoreInfo.xlsx" || f.FileName == "MStoreInfoFree.xlsx" || f.FileName == "MStoreInfoBusiness.xlsx"))
                                {
                                    if (dtImageDetails.Rows.Count <= 0)
                                    {
                                        blnExcelIsEmpty = true;
                                    }
                                }


                                //Store Information Image along with  Description
                                //if (f.ContentLength > 0 && f.FileName != "MStoreInfo.xlsx")
                                if (f.ContentLength > 0 && (f.FileName != "MStoreInfo.xlsx" && f.FileName != "MStoreInfoFree.xlsx" && f.FileName != "MStoreInfoBusiness.xlsx"))
                                {

                                    strFileNm = f.FileName;
                                    bool blnFilenameFlag = false; //To set flag for image failure count

                                    //Code to loop through excel sheets datarow
                                    foreach (DataRow drImgColVal in dtImageDetails.Rows)
                                    {
                                        //Code to check whether Information exist in db for which image is being uploaded.
                                        lngAmId = 0;
                                        long lngInfoid = 0;
                                        string strQuery = "Select IM_bIntInfoId from Information_Master_" + Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID) + " where IM_vCharInfoName_En Like '" + Convert.ToString(drImgColVal[0]) + "' AND IM_bItIsActive=1";
                                        DataTable dtInfoIdData = SqlHelper.ReadTable(strQuery, false);

                                        if (dtInfoIdData.Rows.Count > 0)
                                        {
                                            DataRow row = dtInfoIdData.Rows[0];
                                            lngInfoid = Convert.ToInt32(row["IM_bIntInfoId"]);
                                        }

                                        //Check whether information id does  exist for storing images.
                                        if (lngInfoid != -1)
                                        {
                                            //Code to check whether image for given information already exist.
                                            string strQuery1 = "Select IIG_bIntId,IIG_bIntInfoId From Information_Image_Gallery_" + Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID) + ",Information_Master_" + Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID);
                                            strQuery1 = strQuery1 + " Where IIG_bIntInfoId = " + lngInfoid;
                                            strQuery1 = strQuery1 + " AND IIG_bIntInfoId = IM_bIntInfoId";

                                            DataTable dtInfoIdData1 = SqlHelper.ReadTable(strQuery1, false);

                                            if (dtInfoIdData1.Rows.Count > 0)
                                            {
                                                foreach (DataRow drImgInfoid in dtInfoIdData1.Rows)
                                                {
                                                    lngAmId = Convert.ToInt32(drImgInfoid[0]);
                                                    string strQuery2 = "Update Information_Image_Gallery_" + Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID);
                                                    strQuery2 = strQuery2 + " SET IIG_IsActive = 0";
                                                    strQuery2 = strQuery2 + " WHERE IIG_bIntId = " + lngAmId;
                                                    int intNoOfRows = SqlHelper.UpdateDatabase(strQuery2);
                                                }
                                                //DataRow row = dtInfoIdData1.Rows[0];
                                                //lngAmId = Convert.ToInt32(row["IIG_bIntId"]);
                                            }

                                            lngAmId = 0;


                                            if (strFileNm.Equals(Convert.ToString(drImgColVal[2])) == true)
                                            {
                                                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                                                int intTalukaId = objTal.TalukaID;

                                                string strFilepath = Path.GetFullPath(strFileNm);
                                                if (!Directory.Exists(Server.MapPath(GlobalVariables.InformationDetailsImagePath + "/" + intTalukaId)))
                                                { Directory.CreateDirectory(Server.MapPath(GlobalVariables.InformationDetailsImagePath + "/" + intTalukaId)); }

                                                
                                                strStoredFilepath = GetSafeFileNameOnLocation(strFilepath, GlobalVariables.InformationDetailsImagePath + "/" + intTalukaId);
                                                f.SaveAs(strStoredFilepath);

                                                if (Convert.ToString(drImgColVal[5]) == "Y") intHasImageDef = 1;

                                                //Insert Information details with an image in Taluka folder.
                                                //strStoredFilepath = String.Format("{0}/{1}", GlobalVariables.InformationDetailsImagePath + "/" + intTalukaId, Path.GetFileName(strStoredFilepath));
                                                //strStoredFilepath = (GlobalVariables.InformationDetailsImagePath + "/" + intTalukaId) + "/" + Convert.ToString(drImgColVal[2]);
                                                string pStrDestination = Path.GetFileName(Convert.ToString(drImgColVal[2]));

                                                if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.InformationDetailsImagePath + "/" + intTalukaId), pStrDestination)))
                                                {
                                                    int count1 = 1;
                                                    while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.InformationDetailsImagePath + "/" + intTalukaId), pStrDestination)))
                                                    {
                                                        pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(Convert.ToString(drImgColVal[2])), count1++);
                                                        pStrDestination = pStrDestination + Path.GetExtension(Convert.ToString(drImgColVal[2]));
                                                    }
                                                    if (Convert.ToString(ViewState["RowVal"]) == "")
                                                    {
                                                        strStoredFilepath = (GlobalVariables.InformationDetailsImagePath + "/" + intTalukaId) + "/" + pStrDestination;
                                                        //SetMessage(true, "Image Uploaded Successfully!!!");
                                                    }
                                                }
                                                else
                                                {
                                                    strStoredFilepath = (GlobalVariables.InformationDetailsImagePath + "/" + intTalukaId) + "/" + pStrDestination;
                                                }
                                                lstCatFilePaths.Add(strStoredFilepath); //Store file path for future mapping before storing in database



                                                DataTable dtInsertImageInformation = SqlHelper.ReadTable("SP_insertImageInformationDetail", true,
                                                SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, intTalukaId),
                                                SqlHelper.AddInParam("@bintInformationID", SqlDbType.BigInt, Convert.ToInt32(lngInfoid)),
                                                SqlHelper.AddInParam("@vCharInformationName", SqlDbType.VarChar, Convert.ToString(drImgColVal[3])),
                                                SqlHelper.AddInParam("@nVarInformationName_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[4])),
                                                SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, intHasImageDef),
                                                SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                                                SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngAmId));

                                                if (dtInsertImageInformation.Rows.Count > 0)
                                                {
                                                    long mLngImgInfoId = Convert.ToInt64(dtInsertImageInformation.Rows[0][0]);
                                                    introwCount++;
                                                }

                                                blnFilenameFlag = false;
                                                break;
                                            }
                                            //else if (drImgColVal[2].ToString().Contains(strFileNm) == true)
                                            //{
                                            //    //If file path exist into database already
                                            //    FileUpload1.SaveAs(drImgColVal[2].ToString());

                                            //    blnFilenameFlag = false;
                                            //    break;
                                            //}
                                            else
                                            {
                                                blnFilenameFlag = true;
                                                // intFailureCount++;
                                            }

                                        }//No information id exist
                                    }//End of foreach

                                    if (blnFilenameFlag == true) { intFailureCount++; }

                                }
                            }

                            //Code to insert an action log for Category Details insertion here.
                            long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);

                            introwCount = introwCount + lstCatFilePaths.Count - intFailureCount;
                            string strActionMsg = "[Information Image Master] : " + introwCount + " number of rows inserted into database by " + Session["SystemUser"].ToString();
                            strActionMsg = strActionMsg + " and " + intFailureCount + " no of rows insertion failed.";
                            GlobalFunctions.saveInsertUserAction("Information Image Master", strActionMsg, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID), lngCompanyId, Request); //Call to user Action Log

                            if (intFailureCount > 0)
                            {
                                string strFailureMsgCount = "[Information Image Master] : " + intFailureCount + " rows insertion failed!";

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

                            showInformationImagegrid(); //Call to display Information images.



                            if (blnExcelIsEmpty == false)
                            {
                                Div2.Attributes["class"] = "alert alert-info";
                                Div2.InnerHtml = "Image for Information Added Successfully!!!";
                            }
                            else
                            {
                                Div2.Attributes["class"] = "alert alert-info";
                                Div2.InnerHtml = "Excel Sheet is found to be empty!";
                            }

                            //Div2.Attributes["class"] = "alert alert-info";
                            //Div2.InnerHtml = "Image for information added successfully !!! ";

                        }
                        else
                        {
                            Div2.Attributes["class"] = "alert alert-info ";
                            Div2.InnerHtml = "Please select file for uploading !!!";
                        }
                    }
                }

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnImportonPassword_ServerClick", "InformationImageMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                Div2.Attributes["class"] = "alert alert-info blink-border";
                Div2.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }

            //---------------------------------------------------
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
    }
}