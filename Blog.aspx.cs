using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_CommTrex
{
    public partial class Blog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["TalukaDetails"] != null)
                {
                    FillBlog();
                    LockControls(false);
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }

        }

        public void LockControls(bool blnFlag)
        {
            txtName.Enabled = blnFlag;
            dtDate.Disabled = false;
            //  drpFS.Enabled = blnFlag;
            // drpSize.Enabled = blnFlag;
            // txtBlogPara.Enabled = blnFlag;
            //  drpFS1.Enabled = blnFlag;
            //  drpSize1.Enabled = blnFlag;
            //  txtBlogPara2.Enabled = blnFlag;
            //  drpFS2.Enabled = blnFlag;
            //  drpSize2.Enabled = blnFlag;
            // txtBlogPara3.Enabled = blnFlag;
            drpImgAlignment.Enabled = blnFlag;
            btnSelectICon.Disabled = !blnFlag;
            // ProfileFileUpload.Enabled = blnFlag;
            //txtURL.Enabled = blnFlag;
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Attributes["btn-action"] == "Save")
                {
                    string strValidate = ValidateData();
                    if (strValidate == "")
                    {
                        string strPath = CopyFileSafely(txtImgPathMain.Value, txtImageText.Text);
                        strPath = txtImageText.Text.Replace("//", "/");

                        DataTable dtBlogData = SqlHelper.ReadTable("spInsertUpdateFontInfo", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                        SqlHelper.AddInParam("@vCharName", SqlDbType.VarChar, txtName.Text),
                        SqlHelper.AddInParam("@dtDate", SqlDbType.Date, dtDate.Value),
                        SqlHelper.AddInParam("@vCharFontStyle", SqlDbType.VarChar, "" /*drpFS.SelectedItem.Text*/),
                        SqlHelper.AddInParam("@intFontSize", SqlDbType.BigInt, 0 /*Convert.ToInt32(drpSize.SelectedItem.Value)*/),
                        SqlHelper.AddInParam("@vCharDescription", SqlDbType.VarChar, hidPara1.Value),
                        SqlHelper.AddInParam("@vCharFontStyle1", SqlDbType.VarChar, "" /*drpFS1.SelectedItem.Text*/),
                        SqlHelper.AddInParam("@intFontSize1", SqlDbType.BigInt, 0 /*Convert.ToInt32(drpSize1.SelectedItem.Value)*/),
                        SqlHelper.AddInParam("@vCharDescription1 ", SqlDbType.VarChar, hidPara2.Value),
                        SqlHelper.AddInParam("@vCharFontStyle2", SqlDbType.VarChar, "" /*drpFS2.SelectedItem.Text*/),
                        SqlHelper.AddInParam("@intFontSize2", SqlDbType.BigInt, 0 /* Convert.ToInt32(drpSize2.SelectedItem.Value)*/),
                        SqlHelper.AddInParam("@vCharDescription2 ", SqlDbType.VarChar, hidPara3.Value),
                        SqlHelper.AddInParam("@vCharImageAlign", SqlDbType.VarChar, drpImgAlignment.SelectedItem.Text),
                        SqlHelper.AddInParam("@vCharImage ", SqlDbType.VarChar, strPath),
                        SqlHelper.AddInParam("@nVarCharURL ", SqlDbType.NVarChar,"" /*txtURL.Text*/),

                        SqlHelper.AddInParam("@intID", SqlDbType.BigInt, 0));
                        long intBlogID;
                        if (dtBlogData.Rows.Count > 0)
                        {
                            DataRow dtRowBlog = dtBlogData.Rows[0];
                            intBlogID = Convert.ToInt64(dtRowBlog["ID"].ToString());
                            txtBlogID.Text = Convert.ToString(intBlogID);
                        }
                        btnSave.Attributes["btn-action"] = "New";
                        btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> New";
                        LockControls(false);
                        FillBlog();
                        SetMessage(false, "Blog Added Successfully!!!");
                        ClearControls();
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
                pLngErr = GlobalFunctions.ReportError("btnSave_ServerClick", "Blog", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }

        }

        private String ValidateData()
        {
            string mStrValidation = "";
            if (txtName.Text.Trim() == "")
            {
                mStrValidation += "- Please Enter Blog Title" + Environment.NewLine;
                return (mStrValidation);

            }
            if (dtDate.Value == "")
            {
                mStrValidation += "- Please select Date" + Environment.NewLine;
                return (mStrValidation);

            }
            //if (drpFS.SelectedIndex == 0)
            //{
            //    mStrValidation += "- Please select first font style" + Environment.NewLine;
            //    return (mStrValidation);
            //}
            //if (drpSize.SelectedIndex == 0)
            //{
            //    mStrValidation += "- Please select first font size" + Environment.NewLine;
            //    return (mStrValidation);
            //}
            if (hidPara1.Value == "")
            {
                mStrValidation += "- Please Enter first blog discription" + Environment.NewLine;
                return (mStrValidation);
            }
            //if (ProfileFileUpload.FileName == "")
            //{
            //    mStrValidation += "- Please select image file" + Environment.NewLine;
            //    return (mStrValidation);
            //}
            //if (txtURL.Text == "")
            //{
            //    mStrValidation += "- Please enter URL" + Environment.NewLine;
            //    return (mStrValidation);
            //}
            return (mStrValidation);

        }

        private String ValidateEditData()
        {
            string mStrValidation = "";
            if (txtEditName.Text.Trim() == "")
            {
                mStrValidation += "- Please Enter Blog Title" + Environment.NewLine;
                return (mStrValidation);

            }
            if (dtEditDate.Value == "")
            {
                mStrValidation += "- Please select Date" + Environment.NewLine;
                return (mStrValidation);

            }
            //if (drpEditFS.SelectedIndex == 0)
            //{
            //    mStrValidation += "- Please select first font style" + Environment.NewLine;
            //    return (mStrValidation);
            //}
            //if (drpEditSize.SelectedIndex == 0)
            //{
            //    mStrValidation += "- Please select first font size" + Environment.NewLine;
            //    return (mStrValidation);
            //}
            //if (edtTxtPara1.value == "")
            //{
            //    mStrValidation += "- Please Enter first blog discription" + Environment.NewLine;
            //    return (mStrValidation);
            //}
            //if (ProfileFileUpload.FileName == "")
            //{
            //    mStrValidation += "- Please select image file" + Environment.NewLine;
            //    return (mStrValidation);
            //}
            //if (txtURL.Text == "")
            //{
            //    mStrValidation += "- Please enter URL" + Environment.NewLine;
            //    return (mStrValidation);
            //}
            return (mStrValidation);

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

        public void ClearControls()
        {
            txtName.Text = "";
            dtDate.Value = ""; 
            drpImgAlignment.SelectedIndex = 0;
            // drpFS.SelectedIndex = 0;
            // drpSize.SelectedIndex = 0;
            hidPara1.Value = "";
            // drpFS1.SelectedIndex = 0;
            // drpSize1.SelectedIndex = 0;
            hidPara2.Value = "";
            // drpFS2.SelectedIndex = 0;
            //  drpSize2.SelectedIndex = 0;
            hidPara3.Value = "";
            txtImageText.Text = "";
            //ProfileFileUpload.Dispose();
            //imagePreview.Dispose();
            //txtURL.Text = "";

        }

        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSaveFilePath_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string pStrDestination = "";

                if (FileMainImage.HasFile)
                {
                    //if (!Directory.Exists(pStrDestination))
                    //    Directory.CreateDirectory(Server.MapPath(GlobalVariables.TempFileBlogPath));

                    //pStrDestination = Path.GetFileName(FileMainImage.FileName);
                    //int count = 1;
                    //if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TempFileBlogPath), pStrDestination)))
                    //{
                    //    while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TempFileBlogPath), pStrDestination)))
                    //    {
                    //        pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileMainImage.FileName), count++);
                    //        pStrDestination = pStrDestination + Path.GetExtension(FileMainImage.FileName);
                    //    }
                    //    FileMainImage.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.TempFileBlogPath), pStrDestination));
                    //}
                    //else
                    //{
                    //    FileMainImage.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.TempFileBlogPath), pStrDestination));
                    //}
                    //txtImgPathMain.Value = (GlobalVariables.TempFileBlogPath) + "//" + pStrDestination;
                    //strError = GlobalFunctions.ChkImageSize(Server.MapPath(txtImgPathMain.Value), 256, 256, 64, 64);

                    //if (strError == "")
                    //{
                    if (!Directory.Exists(pStrDestination))
                        Directory.CreateDirectory(Server.MapPath(GlobalVariables.FileBlogHostPath));

                    pStrDestination = Path.GetFileName(FileMainImage.FileName);
                    if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.FileBlogHostPath), pStrDestination)))
                    {
                        int count1 = 1;
                        while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.FileBlogHostPath), pStrDestination)))
                        {
                            pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileMainImage.FileName), count1++);
                            pStrDestination = pStrDestination + Path.GetExtension(FileMainImage.FileName);
                        }
                        FileMainImage.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.FileBlogHostPath), pStrDestination));

                        txtImageText.Text = (GlobalVariables.FileBlogHostPath) + "//" + pStrDestination;
                    }
                    else
                    {
                        FileMainImage.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.FileBlogHostPath), pStrDestination));

                        txtImageText.Text = (GlobalVariables.FileBlogHostPath) + "//" + pStrDestination;
                        //SetMessage(true, "Image Uploaded Successfully!!!");
                    }
                    if (hidBlogId.Value != "")
                    {
                        string query = "Update fontinfo set Image= '" + txtImageText.Text + "' where ID=" + hidBlogId.Value;
                        DataTable dtBlogData = SqlHelper.ReadTable(query, Convert.ToString(Session["SystemUserSqlConnectionString"]), false);
                        SetProductsUpdateMessage(false, "Blog Image Updated Successfully!!!");
                        FillBlog();
                    }
                    //}
                    //else
                    //{
                    //    File.Delete(Server.MapPath(txtImgPathMain.Value));
                    //    txtImgPathMain.Value = "";
                    //    //SetMessage(true, strError);

                    //    //Code added by SSK 13-07-2015 for message display of image
                    //    //Messagebox to display failure of subcategory image upload
                    //    string message = "Image size should be between 64X64 and 256X256 pixels";
                    //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //    sb.Append("<script type = 'text/javascript'>");
                    //    sb.Append("window.onload=function(){");
                    //    sb.Append(" bootbox.alert('");
                    //    sb.Append(message);
                    //    sb.Append("')};");
                    //    sb.Append("</script>");
                    //    ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());
                    //}
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSaveFilePath_ServerClick", "Blog", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
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
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
            return pStrDestination;
        }
        public void FillBlog()
        {
            try
            {
                DataTable dtBlog = SqlHelper.ReadTable("spGetBlogDetails", Convert.ToString(Session["SystemUserSqlConnectionString"]), true);

                grdBlog.DataSource = dtBlog;
                grdBlog.DataBind();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillProductInfo", "Blog", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        protected void btnDeleteBlog_ServerClick(object sender, EventArgs e)
        {
            try
            {

                int intBlogID = Convert.ToInt32(txtDelBlogIDHidden.Value);
                DataTable dtCatData = SqlHelper.ReadTable("[spProductDeleteBlog]", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                     SqlHelper.AddInParam("@ID", SqlDbType.BigInt, intBlogID));

                SetProductsUpdateMessage(false, "Blog Deleted Successfully");
                FillBlog();
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

        private void SetProductsDisMessage(bool pBlnIsError, string pStrMessage)
        {
            updateActionDivDis.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            updateActionDivDis.InnerHtml = pStrMessage;
        }

        protected void btnEditSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string strEditValidate = ValidateEditData();
                if (strEditValidate == "")
                {
                    DataTable dtBlogData = SqlHelper.ReadTable("spInsertUpdateFontInfo", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                    SqlHelper.AddInParam("@vCharName", SqlDbType.VarChar, txtEditName.Text),
                    SqlHelper.AddInParam("@dtDate", SqlDbType.Date, dtEditDate.Value),
                    SqlHelper.AddInParam("@vCharFontStyle", SqlDbType.VarChar,""/* drpEditFS.SelectedItem.Text*/),
                    SqlHelper.AddInParam("@intFontSize", SqlDbType.BigInt, 0 /*Convert.ToInt32(drpEditSize.SelectedItem.Value)*/),
                    SqlHelper.AddInParam("@vCharDescription", SqlDbType.VarChar, edtHidPara1.Value),
                    SqlHelper.AddInParam("@vCharFontStyle1", SqlDbType.VarChar, "" /*drpEditFS1.SelectedItem.Text*/),
                    SqlHelper.AddInParam("@intFontSize1", SqlDbType.BigInt, 0 /* Convert.ToInt32(drpEditSize1.SelectedItem.Value)*/),
                    SqlHelper.AddInParam("@vCharDescription1 ", SqlDbType.VarChar, edtHidPara2.Value),
                    SqlHelper.AddInParam("@vCharFontStyle2", SqlDbType.VarChar, "" /*drpEditFS2.SelectedItem.Text*/),
                    SqlHelper.AddInParam("@intFontSize2", SqlDbType.BigInt, 0 /* Convert.ToInt32(drpEditSize2.SelectedItem.Value)*/),
                    SqlHelper.AddInParam("@vCharDescription2 ", SqlDbType.VarChar, edtHidPara3.Value),
                    SqlHelper.AddInParam("@vCharImageAlign", SqlDbType.VarChar, drpEditImageAlign.SelectedItem.Text),
                    SqlHelper.AddInParam("@vCharImage ", SqlDbType.VarChar, edttxtImageText.Text),//edttxtImageText
                    SqlHelper.AddInParam("@nVarCharURL ", SqlDbType.NVarChar,"" /*txtURL.Text*/),

                    SqlHelper.AddInParam("@intID", SqlDbType.BigInt, Convert.ToInt64(HiddenFieldBlog.Value/*(txtEditBlogID.Text)*/)));
                    long intBlogID;
                    if (dtBlogData.Rows.Count > 0)
                    {
                        DataRow dtRowBlog = dtBlogData.Rows[0];
                        intBlogID = Convert.ToInt64(dtRowBlog["ID"].ToString());
                        //txtBlogID.Text = Convert.ToString(intBlogID);
                    }
                    FillBlog();
                    SetProductsUpdateMessage(false, "Blog Updated Successfully!!!");
                }
                else
                {
                    SetProductsDisMessage(true, strEditValidate);
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnEditSave_ServerClick", "Blog", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

    }
}