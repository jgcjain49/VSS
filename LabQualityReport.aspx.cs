
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;



namespace Admin_CommTrex
{
    public partial class LabQualityReport : System.Web.UI.Page
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
                    FillLabQualityGrid();
                    FillDrdId();
                    LockControls(false);
                    // grdLab.Columns[3].Visible = false;

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
                    mstrGetUser = "SELECT * from Com_LabQuality where iLabId= '" + HidBnkId.Value + "'";
                    //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                    DataTable dtGetUserDetails = new DataTable();


                    //dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, Convert.ToString(Session["SystemUserSqlConnectionString"]), false,
                    //                SqlHelper.AddInParam("@iPayId", SqlDbType.Int, Convert.ToInt32(HidBnkId.Value)));

                    dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);

                    selectcommodityname.SelectedItem.Text = dtGetUserDetails.Rows[0]["sCommodityName"].ToString();
                    txtAReportPath.Text = dtGetUserDetails.Rows[0]["sUploadReport"].ToString();


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
                    string strPath = "";
                    string strLogoPath = "";
                    strValidate = ValidateUser(selectcommodityname.SelectedValue, txtAReportPath.Text);
                    if (strValidate == "")
                    {
                        if (txtAReportPath.Text.Contains("fa"))
                        {
                            if (txtAReportPath.Text.Contains("fa"))
                            {
                                strLogoPath = txtAReportPath.Text;

                                //strPath = GlobalVariables.NoImagePath;
                            }
                        }
                        else
                        {
                            // strPath = CopyFileSafely(txtImgPathMain.Value, txtAReportPath.Text);

                            strPath = txtAReportPath.Text.Replace("//", "/");

                            // strLogoPath = "fa-question-circle";
                        }

                        DataTable dtData = SqlHelper.ReadTable("spInsertUpdateLabQuality", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                        SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, selectcommodityname.SelectedItem.Text),
                        SqlHelper.AddInParam("@sUploadReport", SqlDbType.VarChar, strPath));
                        // SqlHelper.AddInParam("@u_pass", SqlDbType.VarChar, dt.Value)
                        FillLabQualityGrid();
                        SetMessage(false, "Lab Quality Added Successfully");
                        LockControls(false);
                        btnSave.Attributes["btn-action"] = "New";
                        btnSave.InnerHtml = "New";
                        SetMessage(false, "Lab Quality Added Succesfully!!");
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
                        string strError = "";

                        string strValidate = "";
                        strValidate = ValidateUser(selectcommodityname.SelectedItem.Text, txtAReportPath.Text);
                        if (strValidate == "")
                        {
                            string img = txtAReportPath.Text.Replace("//", "/");

                            DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateLabQuality", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                    SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, selectcommodityname.SelectedItem.Text),
                                                     SqlHelper.AddInParam("@sUploadReport", SqlDbType.VarChar, img),

                                                     SqlHelper.AddInParam("@iLabId", SqlDbType.Int, (HidBnkId.Value)));

                            SetProductsUpdateMessage(false, "Lab Quality Details Updated Successfully");
                            grdLab.EditIndex = -1;
                            FillLabQualityGrid();
                            ClearControls();
                            SetMessage(false, "Lab Quality Updated Succesfully!!");
                            //grdUser.Columns[6].Visible = false;
                            btnSave.InnerHtml = "Save";
                            btnClear.InnerHtml = "Clear";
                            grdLab.Columns[4].Visible = false;
                            selectcommodityname.Items.Clear();
                            FillDrdId();
                            ClearAll();
                        }
                        else
                        {
                            SetProductsUpdateMessage(false, strValidate);
                        }

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
                else
                {
                    ClearControls();
                    LockControls(true);
                    btnSave.Attributes["btn-action"] = "Save";
                    btnSave.InnerHtml = "Save";
                    SetMessage(false, "Press Save To Add Lab Quality!!");
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

        protected void btnSaveImgUpload(object sender, EventArgs e)
        {
            try
            {

                string sizeerr;
                if (FileMainImage1.HasFile)
                {
                    #region
                    /*
                    if (!Directory.Exists(Server.MapPath(GlobalVariables.UserImgPath)))
                        Directory.CreateDirectory(Server.MapPath(GlobalVariables.UserImgPath));
                    //string finalFileNameCom = string.Format("{0}_{1}{2}",
                    //                                Path.GetFileNameWithoutExtension(FileMainImage1.FileName),
                    //                                DateTime.Now.ToString("yyyyMMdd_HHmm"),
                    //                                Path.GetExtension(FileMainImage1.FileName));
                    string finalFileName = string.Format("{0}_{1}{2}",
                                                    Path.GetFileNameWithoutExtension(FileMainImage1.FileName),
                                                    DateTime.Now.ToString("yyyy-MM-dd_HHmmss"),
                                                    Path.GetExtension(FileMainImage1.FileName));

                    FileMainImage1.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.UserImgPath), finalFileName));
                    //FileMainImage1.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.GoldifyKYCPath), finalFileName));

                    txtAGSTPath.Text = GlobalVariables.UserImgPath + "//" + finalFileName;
                    */
                    #endregion
                    Stream strm = FileMainImage1.PostedFile.InputStream;
                    System.Drawing.Image img = compressImage(Convert.ToInt16(GlobalVariables.compressedImgQuality), strm);
                    string gstPath = GlobalFunctions.saveImage(GlobalVariables.UserImgPath, FileMainImage1, img);
                    txtAReportPath.Text = gstPath;
                    txtImgPathMain.Value = gstPath;

                    if (Convert.ToString(ViewState["RowVal"]) == "")
                    {
                        if (Convert.ToString(ViewState["RowVal"]) == "")
                        {

                            txtAReportPath.Text = gstPath;
                            SetMessage(true, "Image Uploaded Successfully!!!");

                            //txtLogo.Text = "";
                        }
                        else
                        {
                            //GlobalVariables.updateImage1 = txtImgPathMain.Value;

                            SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                        }
                    }
                    else
                    {
                        HiddenField ImgCat = (HiddenField)grdLab.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath");
                        HiddenField LogoName = (HiddenField)grdLab.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("LogoName");
                        //Image Img = (Image)grdLab.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat");
                        //Img.ImageUrl = "~/" + txtImgPathMain.Value;
                        //Img.Visible = true;
                        //LogoName.Value = "";

                        ImgCat.Value = txtImgPathMain.Value;
                        ViewState["ImgLogo"] = "";

                        HtmlButton btnLogo = (HtmlButton)grdLab.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow");
                        btnLogo.Visible = false;

                        ViewState["ImgPath"] = (GlobalVariables.UserImgPath) + "//" + gstPath;
                        SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                    }



                }
            }



            #region
            /*start
                string pStrDestination = "";
               
                string strError;
                System.Web.UI.HtmlControls.HtmlButton button = sender as System.Web.UI.HtmlControls.HtmlButton;
                if (button != null)
                {
                    string buttonId = button.ID;
                    switch (buttonId)
                    {
                        case "b1":
                            {
                                //if (FileMainImage1.HasFile)
                                //{
                                //    FileMainImage1.SaveAs("path");
                                //    if (!Directory.Exists(Server.MapPath(GlobalVariables.TemporaryPath)))
                                //        Directory.CreateDirectory(Server.MapPath(GlobalVariables.TemporaryPath));

                                //    if (!Directory.Exists(Server.MapPath(GlobalVariables.FileHostPath)))
                                //        Directory.CreateDirectory(Server.MapPath(GlobalVariables.FileHostPath));

                                //    pStrDestination = Path.GetFileName(FileMainImage1.FileName);
                                //    int count = 1;
                                //    if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination)))
                                //    {
                                //        while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination)))
                                //        {
                                //            pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileMainImage1.FileName), count++);
                                //            pStrDestination = pStrDestination + Path.GetExtension(FileMainImage1.FileName);
                                //        }
                                //        FileMainImage1.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination));
                                //    }
                                //    else
                                //    {
                                //        FileMainImage1.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination));
                                //    }
                                //    txtImgPathMain.Value = (GlobalVariables.TemporaryPath) + "//" + pStrDestination;
                                //    strError = ChkImageSize(Server.MapPath(txtImgPathMain.Value));
                                //    if (strError == "")
                                //    {
                                //        pStrDestination = Path.GetFileName(FileMainImage1.FileName);

                                //        if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination)))
                                //        {
                                //            int count1 = 1;
                                //            while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination)))
                                //            {
                                //                pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileMainImage1.FileName), count1++);
                                //                pStrDestination = pStrDestination + Path.GetExtension(FileMainImage1.FileName);
                                //            }

                                //            if (Convert.ToString(ViewState["RowVal"]) == "")
                                //            {
                                //                txtAGSTPath.Text = (GlobalVariables.TemporaryPath) + "//" + pStrDestination;
                                //                SetMessage(true, "Image Uploaded Successfully!!!");
                                //                //txtLogo.Text = "";
                                //            }
                                //            else
                                //            {
                                //                HiddenField ImgCat = (HiddenField)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath");
                                //                HiddenField LogoName = (HiddenField)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("LogoName");
                                //                Image Img = (Image)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat");
                                //                Img.ImageUrl = "~/" + txtImgPathMain.Value;
                                //                Img.Visible = true;
                                //                LogoName.Value = "";
                                //                ImgCat.Value = txtImgPathMain.Value;
                                //                ViewState["ImgLogo"] = "";

                                //                HtmlButton btnLogo = (HtmlButton)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow");
                                //                btnLogo.Visible = false;

                                //                ViewState["ImgPath"] = (GlobalVariables.TemporaryPath) + "//" + pStrDestination;
                                //                SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                                //            }
                                //        }
                                //        else
                                //        {
                                //            if (Convert.ToString(ViewState["RowVal"]) == "")
                                //            {
                                //                txtAGSTPath.Text = (GlobalVariables.FileHostPath) + "//" + pStrDestination;
                                //                SetMessage(true, "Image Uploaded Successfully!!!");
                                //            }
                                //            else
                                //            {
                                //                HiddenField ImgCat = (HiddenField)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath");
                                //                HiddenField LogoName = (HiddenField)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("LogoName");
                                //                Image Img = (Image)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat");
                                //                Img.ImageUrl = "~/" + txtImgPathMain.Value;
                                //                Img.Visible = true;
                                //                LogoName.Value = "";
                                //                ImgCat.Value = txtImgPathMain.Value;

                                //                ViewState["ImgLogo"] = "";
                                //                HtmlButton btnLogo = (HtmlButton)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow");
                                //                btnLogo.Visible = false;
                                //                ViewState["ImgPath"] = (GlobalVariables.FileHostPath) + "//" + pStrDestination;
                                //                SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                                //            }
                                //        }
                                //    }
                                //    else
                                //    {
                                //        File.Delete(Server.MapPath(txtImgPathMain.Value));
                                //        //SetMessage(true, strError);

                                //        //Code added by SSK 13-07-2015 for message display of image
                                //        //Messagebox to display failure of subcategory image upload
                                //        string message = "Image size should be between 32X32 and 43X43 pixels";
                                //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                //        sb.Append("<script type = 'text/javascript'>");
                                //        sb.Append("window.onload=function(){");
                                //        sb.Append(" bootbox.alert('");
                                //        sb.Append(message);
                                //        sb.Append("')};");
                                //        sb.Append("</script>");
                                //        ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());
                                //    }
                                //}
                                
                        }
                            break;

                        case "b2": {
                            if (FileUpload2.HasFile)
                            {
                                if (!Directory.Exists(Server.MapPath(GlobalVariables.TemporaryPath)))
                                    Directory.CreateDirectory(Server.MapPath(GlobalVariables.TemporaryPath));

                                if (!Directory.Exists(Server.MapPath(GlobalVariables.FileHostPath)))
                                    Directory.CreateDirectory(Server.MapPath(GlobalVariables.FileHostPath));

                                pStrDestination = Path.GetFileName(FileUpload2.FileName);
                                int count = 1;
                                if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination)))
                                {
                                    while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination)))
                                    {
                                        pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileUpload2.FileName), count++);
                                        pStrDestination = pStrDestination + Path.GetExtension(FileUpload2.FileName);
                                    }
                                    FileUpload2.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination));
                                }
                                else
                                {
                                    FileUpload2.SaveAs(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination));
                                }
                                txtImgPathMain1.Value = (GlobalVariables.TemporaryPath) + "//" + pStrDestination;
                                strError = ChkImageSize(Server.MapPath(txtImgPathMain1.Value));
                                if (strError == "")
                                {
                                    pStrDestination = Path.GetFileName(FileUpload2.FileName);

                                    if (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination)))
                                    {
                                        int count1 = 1;
                                        while (File.Exists(Path.Combine(Server.MapPath(GlobalVariables.TemporaryPath), pStrDestination)))
                                        {
                                            pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(FileUpload2.FileName), count1++);
                                            pStrDestination = pStrDestination + Path.GetExtension(FileUpload2.FileName);
                                        }

                                        if (Convert.ToString(ViewState["RowVal"]) == "")
                                        {
                                            txtAROCPath.Text = (GlobalVariables.TemporaryPath) + "//" + pStrDestination;
                                            SetMessage(true, "Image Uploaded Successfully!!!");
                                            //txtLogo.Text = "";
                                        }
                                        else
                                        {
                                            HiddenField ImgCat = (HiddenField)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath1");
                                            HiddenField LogoName = (HiddenField)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("LogoName1");
                                            Image Img = (Image)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat1");
                                            Img.ImageUrl = "~/" + txtImgPathMain1.Value;
                                            Img.Visible = true;
                                            LogoName.Value = "";
                                            ImgCat.Value = txtImgPathMain1.Value;
                                            ViewState["ImgLogo"] = "";

                                            HtmlButton btnLogo = (HtmlButton)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow1");
                                            btnLogo.Visible = false;

                                            ViewState["ImgPath1"] = (GlobalVariables.FileHostPath) + "//" + pStrDestination;
                                            SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                                        }
                                    }
                                    else
                                    {
                                        if (Convert.ToString(ViewState["RowVal"]) == "")
                                        {
                                            txtAROCPath.Text = (GlobalVariables.FileHostPath) + "//" + pStrDestination;
                                            SetMessage(true, "Image Uploaded Successfully!!!");
                                        }
                                        else
                                        {
                                            HiddenField ImgCat = (HiddenField)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath1");
                                            HiddenField LogoName = (HiddenField)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("LogoName1");
                                            Image Img = (Image)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat1");
                                            Img.ImageUrl = "~/" + txtImgPathMain1.Value;
                                            Img.Visible = true;
                                            LogoName.Value = "";
                                            ImgCat.Value = txtImgPathMain1.Value;

                                            ViewState["ImgLogo"] = "";
                                            HtmlButton btnLogo = (HtmlButton)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow1");
                                            btnLogo.Visible = false;
                                            ViewState["ImgPath1"] = (GlobalVariables.FileHostPath) + "//" + pStrDestination;
                                            SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                                        }
                                    }
                                }
                                else
                                {
                                    File.Delete(Server.MapPath(txtImgPathMain1.Value));
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
                            break;
                       }
                               
                                                                                                 
                    
                }         
                 *
                 */
            #endregion


            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSaveImgUpload", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Administrator";
            }
        }
        private string ValidateUser(string selectcommodityname, string strAReportPath)
        {
            string mstrValidate = "";
            if (selectcommodityname == "0")
            {
                mstrValidate = mstrValidate + " Commodity Name Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strAReportPath == "")
            {
                mstrValidate = mstrValidate + "Report Path Cannot be Blank !!!";
                return mstrValidate;
            }
            return mstrValidate;
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
                selectcommodityname.DataSource = dtGetUserDetails;
                selectcommodityname.DataTextField = "sCommodityName";
                selectcommodityname.DataValueField = "iComID";
                selectcommodityname.DataBind();
                selectcommodityname.Items.Insert(0, new ListItem("-- Select Commodity Name --", "0"));
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
        public void FillLabQualityGrid()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "select * from Com_LabQuality where bIsActive=1";
                //where bIsActive=1";
                ////mstrGetUser = "SELECT * from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId);
                //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                grdLab.DataSource = dtGetUserDetails;
                grdLab.DataBind();
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
            selectcommodityname.Enabled = pBoolState;
            txtAReportPath.Enabled = pBoolState;
        }
        public void ClearControls()
        {
            selectcommodityname.SelectedIndex = 0;
            txtAReportPath.Text = "";
            //SetMessage(false, "Press Save To Add Lab Quality!!");
        }
        protected void btnDeleteLabQlty_ServerClick(object sender, EventArgs e)
        {
            // string strquery = "Update User_Master set UM_bItIsActive = 0 where UM_bIntId=@id";
            // SqlHelper.UpdateDatabase(strquery, SqlHelper.AddInParam("@id", SqlDbType.VarChar, txtDelHidden.Value.Trim()));
            DataTable dtCatData = SqlHelper.ReadTable("spDeleteLabQuality", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
            SqlHelper.AddInParam("@id", SqlDbType.Int, Convert.ToInt32(txtDelHidden.Value)));
            SetProductsUpdateMessage(false, "Lab Quality Deleted Successfully");
            txtDelHidden.Value = "";
            txtDelLabQltyID.Text = "";
            txtDelLabQltyName.Text = "";
            grdLab.EditIndex = -1;
            FillLabQualityGrid();
            grdLab.Columns[4].Visible = false;
        }
        //private string ChkImageSize(string pstrImagePath)
        //{
        //    try
        //    {
        //        string strMsg = "";
        //        System.Drawing.Bitmap image = new System.Drawing.Bitmap(pstrImagePath);
        //        int originalWidth = image.Width;
        //        int originalHeight = image.Height;

        //        if (originalWidth > 43 || originalHeight > 43)
        //        {
        //            image.Dispose();
        //            return ("Image Upload Failed Image Size is Greater than maximum size 43!!! Select Appropriate Image to Continue");
        //        }
        //        else if (originalWidth > 43 && originalWidth > 43)
        //        {
        //            image.Dispose();
        //            return ("Image Upload Failed Image Size is Greater than maximum size 43!!! Select Appropriate Image to Continue");
        //        }
        //        else if (originalWidth < 32 && originalWidth < 32)
        //        {
        //            image.Dispose();
        //            return (" Image Upload Failed Image Size is smaller than maximum size 32!!! Select Appropriate Image to Continue");
        //        }

        //        else if (originalWidth < 32 || originalWidth < 32)
        //        {
        //            image.Dispose();
        //            return (" Image Upload Failed Image Size is smaller than maximum size 32!!! Select Appropriate Image to Continue");
        //        }
        //        image.Dispose();
        //        if (strMsg.Trim() != "")
        //        {
        //            ResizeImage(pstrImagePath, pstrImagePath, 42, 42, false);
        //            strMsg = "";
        //        }
        //        return strMsg;
        //    }
        //    catch (Exception exError)
        //    {
        //        long pLngErr = -1;
        //        if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //            pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //        pLngErr = GlobalFunctions.ReportError("ChkImageSize", "CategoryBusiness", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
        //        updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
        //        updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
        //    }
        //    return "";

        //}
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdLab.EditIndex = e.NewEditIndex;
            ViewState["RowVal"] = e.NewEditIndex;
            FillLabQualityGrid();
            grdLab.Columns[4].Visible = true;
        }
        //public void ResizeImage(string OriginalFile, string NewFile, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        //{
        //    System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(OriginalFile);

        //    // Prevent using images internal thumbnail
        //    FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
        //    FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

        //    if (OnlyResizeIfWider)
        //    {
        //        if (FullsizeImage.Width <= NewWidth)
        //        {
        //            NewWidth = FullsizeImage.Width;
        //        }
        //    }

        //    int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
        //    if (NewHeight > MaxHeight)
        //    {
        //        // Resize with height instead
        //        NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
        //        NewHeight = MaxHeight;
        //    }

        //    System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

        //    // Clear handle to original file so that we can overwrite it if necessary
        //    FullsizeImage.Dispose();

        //    // Save resized picture
        //    NewImage.Save(NewFile);
        //}
        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label cmdtyname = (Label)grdLab.Rows[e.RowIndex].FindControl("edtcmdtyname");
                TextBox path = (TextBox)grdLab.Rows[e.RowIndex].FindControl("edtuploadpath");
                HiddenField ImgOriginalCategory = (HiddenField)grdLab.Rows[e.RowIndex].FindControl("imgPath");
                HiddenField CatLogo = (HiddenField)grdLab.Rows[e.RowIndex].FindControl("LogoName");
                Label lblID = (Label)grdLab.Rows[e.RowIndex].FindControl("Labid");

                string strErrorImg = "";
                string strError = "";

                string strValidate = "";
                strValidate = ValidateUser(cmdtyname.Text, txtImgPathMain.Value);
                if (strValidate == "")
                {
                    string img = ImgOriginalCategory.Value.Replace("//", "/");

                    DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateLabQuality", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                            SqlHelper.AddInParam("@sCommodityName", SqlDbType.VarChar, cmdtyname.Text),
                                             SqlHelper.AddInParam("@sUploadReport", SqlDbType.VarChar, img),

                                             SqlHelper.AddInParam("@iLabId", SqlDbType.Int, (lblID.Text)));

                    SetProductsUpdateMessage(false, "Lab Quality Details Updated Successfully");
                    grdLab.EditIndex = -1;
                    FillLabQualityGrid();
                    ClearControls();
                    grdLab.Columns[4].Visible = false;
                }
                else
                {
                    SetProductsUpdateMessage(false, strValidate);
                }

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
        //public string CopyFileSafely(string pStrSourceFile, String pStrDestination)
        //{
        //    try
        //    {
        //        if (pStrSourceFile == "" || Server.MapPath(pStrDestination) == "")
        //            return "";

        //        pStrDestination = Server.MapPath(pStrDestination);
        //        pStrSourceFile = Server.MapPath(pStrSourceFile);

        //        if (!Directory.Exists(pStrDestination))
        //            Directory.CreateDirectory(Server.MapPath(GlobalVariables.FileProdCatHostPath));


        //        File.Copy(pStrSourceFile, pStrDestination);
        //        File.Delete(pStrSourceFile);
        //    }
        //    catch (Exception exError)
        //    {
        //        long pLngErr = -1;
        //        if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //            pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //        pLngErr = GlobalFunctions.ReportError("CopyFileSafely", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
        //        updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
        //        updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
        //    }
        //    return pStrDestination;
        //}
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdLab.EditIndex = -1;
            FillLabQualityGrid();
            ClearControls();
            grdLab.Columns[4].Visible = false;
            //if (txtImgPathMain.Value != "")
            //{
            //    bool blnFlagDelete = DeleteFile(txtImgPathMain.Value);
            //}
        }
        private void ClearAll()
        {

            selectcommodityname.SelectedIndex = 0;
            //SetMessage(false, "Press Save to store Dispatch Stock Details");
            txtAReportPath.Text = "";
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in encoders)
                if (ici.MimeType == mimeType) return ici;
            return null;
        }

        private System.Drawing.Image compressImage(int newQuality, Stream strm)
        {
            using (System.Drawing.Image image = System.Drawing.Image.FromStream(strm))
            using (System.Drawing.Image memImage = new Bitmap(image, image.Width, image.Height))
            {
                ImageCodecInfo myImageCodecInfo;
                System.Drawing.Imaging.Encoder myEncoder;
                EncoderParameter myEncoderParameter;
                EncoderParameters myEncoderParameters;
                myImageCodecInfo = GetEncoderInfo("image/jpeg");
                myEncoder = System.Drawing.Imaging.Encoder.Quality;
                myEncoderParameters = new EncoderParameters(1);
                myEncoderParameter = new EncoderParameter(myEncoder, newQuality);
                myEncoderParameters.Param[0] = myEncoderParameter;

                MemoryStream memStream = new MemoryStream();
                memImage.Save(memStream, myImageCodecInfo, myEncoderParameters);
                System.Drawing.Image newImage = System.Drawing.Image.FromStream(memStream);
                ImageAttributes imageAttributes = new ImageAttributes();
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    g.InterpolationMode =
                      System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;  //**
                    g.DrawImage(newImage, new Rectangle(Point.Empty, newImage.Size), 0, 0,
                      newImage.Width, newImage.Height, GraphicsUnit.Pixel, imageAttributes);
                }
                return newImage;
            }
        }

        public void ClearDialougControl()
        {
            ImgCoverImage.ImageUrl = "";

        }

        protected void setImages(long intImgID, string strAAtchdhrFrnt, int count)
        {
            try
            {
                switch (count)
                {
                    case 1:

                        //txtMainDescription.Text = "MD Pancard";
                        //ImgCoverImage.ImageUrl = "http://wap.goldifyapp.com/admin/" + (strAAtchdhrFrnt.ToLower().Contains(".pdf") ? "AdminExContent/images/pdf_logo.png" : strAAtchdhrFrnt);
                        //enlargeImgLnk1.HRef = "http://wap.goldifyapp.com/admin/" + strAAtchdhrFrnt;
                        //txtImage1Description.Text = "MD Aadhar Detail";     
                        ImgCoverImage.ImageUrl = (strAAtchdhrFrnt.ToLower().Contains(".pdf") ? "AdminExContent/images/pdf_logo.png" : strAAtchdhrFrnt);
                        enlargeImgLnk1.HRef = strAAtchdhrFrnt;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("setImages", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Administrator";

            }
        }


        protected void btnShowGallery_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int introw = Convert.ToInt32(btn.Attributes["RowIndex"]);

                Label lblOrgID = (Label)grdLab.Rows[introw].FindControl("Labid");


                string query = "SELECT [iLabId]   ,[sUploadReport] " +
                               "FROM [Com_LabQuality] where iLabId  ='" + lblOrgID.Text + "'";
                DataTable dtGetImages = SqlHelper.ReadTable(query, Convert.ToString(Session["SystemUserSqlConnectionString"]), false);
                //SqlHelper.AddInParam("@ID", SqlDbType.VarChar, lblOrgID.Text));
                ClearDialougControl();

                if (dtGetImages.Rows.Count > 0)
                {
                    Button clickedButton = sender as Button;
                    if (clickedButton.ID == "btnShowGallery1")
                    {
                        for (int intcount = 1; intcount <= dtGetImages.Rows.Count; intcount++)
                        {
                            DataRow dtRowCat = dtGetImages.Rows[intcount - 1];
                            setImages(Convert.ToInt64(dtRowCat["iLabId"].ToString()), dtRowCat["sUploadReport"].ToString(), intcount);
                            //GlobalVariables.updateImage1 = dtRowCat["sGstUplaodDoc"].ToString();
                        }
                        this.ClientScript.RegisterStartupScript(this.GetType(), "showGalleryModal", "ShowModal()", true);
                    }

                    else { }
                }
                else
                {
                    //docArea.InnerHtml = "<b>No Documents found</b>";
                }
            }

            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("InsertUpdateImages", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Administrator";
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
            if (txtCode.Text.Trim() == "")
            {
                lblMsgError.Visible = true;
            }
            else
            {
                //string strLoginPass = Convert.ToString(Request.Cookies["MStore_Cookie_Password"].Value);
                string strLoginPass = "Welcome@7679";
                if (strLoginPass.Equals(txtCode.Text.Trim()) == true)
                {
                    if (FileUploadControl.HasFile)
                    {
                        try
                        {
                            //Read an Excel file (1st sheet) for user transfer details
                            string ext = Path.GetExtension(FileUploadControl.FileName).ToLower();
                            string path = Server.MapPath(FileUploadControl.PostedFile.FileName);
                            DataSet ds;
                            FileUploadControl.SaveAs(path);
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
                                Div2.Attributes["class"] = "alert alert-info ";
                                Div2.InnerHtml = "Only excel file (.xls or .xlsx) is valid !!!";
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


                            bulkinsert.DestinationTableName = "QC_report";
                            bulkinsert.ColumnMappings.Add(0, "list");




                            bulkinsert.BatchSize = 0;
                            bulkinsert.WriteToServer(dr);

                            olecon.Close();
                            Div2.InnerHtml = "Excel Added Succesfully!!!";

                            FillLabQualityGrid();
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

                        Div2.InnerHtml = "Empty Excel File!!!";

                    }

                }

                else {
                    Div2.InnerHtml = "Invalid Password!!!";
                }
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string fileName = "QC.xlsx";
            string filePath = Server.MapPath(string.Format("~/Content/UserImages/QC.xlsx", fileName));
            Response.ContentType = "application/xlsx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.WriteFile(filePath);
            Response.Flush();
            Response.End();
        }
    }
}