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
    public partial class CoverImageViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            #region "General Display"
            txtProdId.Text = Convert.ToString(Session["FilePath"]);
            txtProdName.Text = Convert.ToString(Session["Temp_ProductName"]);
            txtProdType.Text = Convert.ToString(Session["Temp_ProductType"]);
            #endregion"General Display"

            string strPath = "~\\" + GlobalVariables.FileHostPath + "\\" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "\\" + String.Format("prod{0}", Session["FilePath"]);
            string strPath1;
            if (Directory.Exists(Server.MapPath(strPath)))
            {
                foreach (string thumbFile in Directory.GetFiles(Server.MapPath(strPath)))
                {
                    strPath = strPath.Replace("\\", "/");
                    strPath1 = "";
                    strPath1 = strPath + "/" + Path.GetFileName(thumbFile);

                    if (strPath1.Contains("16"))
                    {
                        //img = (Image)FindControl("Image16x16");
                        //if (img != null)
                        //{

                        //    intHeight = 16;
                        //    intWidth = 16;
                        //    img.ImageUrl = strPath1;
                        //    img.Height = intHeight;
                        //    img.Width = intWidth;
                        //    img.AlternateText = Path.GetFileName(thumbFile);
                        //}

                        Img16x16.ImageUrl = strPath1;
                        Img16x16.Width = Img16x16.Height = 16;
                        txtThumbPath16.Value = strPath1;
                    }
                    else if (strPath1.Contains("32"))
                    {
                        //img = (Image)FindControl("Image32x32");
                        //if (img != null)
                        //{
                        //    intHeight = 32;
                        //    intWidth = 32;
                        //    img.ImageUrl = strPath1;
                        //    img.Height = intHeight;
                        //    img.Width = intWidth;
                        //    img.AlternateText = Path.GetFileName(thumbFile);
                        //}
                        Img32x32.ImageUrl = strPath1;
                        Img32x32.Width = Img32x32.Height = 32;
                        txtThumbPath32.Value = strPath1;
                    }
                    else if (strPath1.Contains("64"))
                    {
                        //img = (Image)FindControl("Image64x64");
                        //if (img != null)
                        //{
                        //    intHeight = 64;
                        //    intWidth = 64;

                        //    img.ImageUrl = strPath1;
                        //    img.Height = intHeight;
                        //    img.Width = intWidth;
                        //    img.AlternateText = Path.GetFileName(thumbFile);
                        //}
                        Img64x64.ImageUrl = strPath1;
                        Img64x64.Width = Img64x64.Height = 64;
                        txtThumbPath64.Value = strPath1;
                    }

                    else if (strPath1.Contains("128"))
                    {
                        //img = (Image)FindControl("Image128x128");
                        //if (img != null)
                        //{

                        //    intHeight = 128;
                        //    intWidth = 128;

                        //    img.ImageUrl = strPath1;
                        //    img.Height = intHeight;
                        //    img.Width = intWidth;
                        //    img.AlternateText = Path.GetFileName(thumbFile);
                        //}
                        Img128x128.ImageUrl = strPath1;
                        Img128x128.Width = Img128x128.Height = 128;
                        txtThumbPath128.Value = strPath1;
                    }

                    else if (strPath1.Contains("256"))
                    {
                        //img = (Image)FindControl("Image256x256");
                        //if (img != null)
                        //{

                        //    intHeight = 256;
                        //    intWidth = 256;

                        //    img.ImageUrl = strPath1;
                        //    img.Height = intHeight;
                        //    img.Width = intWidth;
                        //    img.AlternateText = Path.GetFileName(thumbFile);

                        //}
                        Img256x256.ImageUrl = strPath1;
                        Img256x256.Width = Img256x256.Height = 256;
                        txtThumbPath256.Value = strPath1;
                    }
                    else
                    {
                        //intHeight = 256;
                        //intWidth = 256;
                        //img = (ImageButton)FindControl("ImageMainImage");
                        //ImageMainImage.ImageUrl = strPath1;
                        //ImageMainImage.Height = intHeight;
                        //ImageMainImage.Width = intWidth;
                        //ImageMainImage.AlternateText = Path.GetFileName(thumbFile);
                        MainImage.ImageUrl = strPath1;
                        txtImgPathMain.Value = strPath1;
                    }
                    // txtMainImage.Text = strPath1;
                }
            }
        }

        

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {

            string mStrProductImagePath = Server.MapPath(Path.Combine((GlobalVariables.FileHostPath + "\\" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey)), String.Format("prod{0}", Session["FilePath"].ToString())));

            if (FileMainImage.HasFile)
            { 
                // Upload main image and set it's value
                try
                {

                    string filename = Path.GetFileName(FileMainImage.FileName);

                    string strUploadPAth = Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId));

                    if (!Directory.Exists(strUploadPAth))
                        Directory.CreateDirectory(strUploadPAth);

                    FileMainImage.SaveAs(Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId)) + "//" + filename);
                    txtImgPathMain.Value = Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId) + "//" + filename);

                    MainImage.ImageUrl = "~//" + GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId) + "//" + filename;
                    txtThumbPath256.Value = txtThumbPath128.Value = txtThumbPath64.Value = txtThumbPath64.Value = txtThumbPath32.Value = txtThumbPath16.Value = "";

                    //Added Create Directory if it doesnt exist
                    //And then try deleting the previous file

                    if (!Directory.Exists(mStrProductImagePath))
                        Directory.CreateDirectory(mStrProductImagePath);

                    // Delete all previous files as we are going to create new one
                    foreach (string mStrPrevFile in Directory.GetFiles(mStrProductImagePath))
                        File.Delete(mStrPrevFile);
                }
                catch (Exception ex)
                {
                    SetMessage(true, "Main image Upload status: The file could not be uploaded.<br/>The following error occured: " + ex.Message);
                    return;
                }
            }
            else if (txtImgPathMain.Value == "")
            {
                SetMessage(true, "Failed to save details..Main Image Not Set<br/>");
                return;
            }
            
            //----------------------------------
            // Upload thumbs
            //----------------------------------
            string mStrResult = "";
            if (FileThumb256.HasFile)
            {
                try
                {
                    string filename = String.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(FileThumb256.FileName), "256", Path.GetExtension(FileThumb256.FileName));

                    string strUploadPAth = Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId));

                    if (!Directory.Exists(strUploadPAth))
                        Directory.CreateDirectory(strUploadPAth);

                    FileThumb256.SaveAs(Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId)) + "//" + filename);
                    txtThumbPath256.Value = Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId) + "//" + filename);

                    Img256x256.ImageUrl = "~//" + GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId) + "//" + filename;
                    mStrResult += "Thumbs of size 256x256 Updated<br/>";

                    // Delete previous thumb file of size 256
                    foreach (string mStrPrevFile in Directory.GetFiles(mStrProductImagePath, "*256x256*"))
                        File.Delete(mStrPrevFile);
                }
                catch (Exception ex)
                {
                    mStrResult += "Failed to upload thumb 256 : <br/>Following error occured: " + ex.Message;
                }
            }
            else if(txtThumbPath256.Value == "")
            {
                // Generate thumbs from main image and update it's value
                string mStrTempImgPath = (Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId)));
                txtThumbPath256.Value = ResizeImage(256, 256, txtImgPathMain.Value, mStrTempImgPath);
                mStrResult += "Thumbs of size 256x256 Generated & Updated<br/>";
            }

            if (FileThumb128.HasFile)
            {
                try
                {
                    string filename = String.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(FileThumb128.FileName), "128", Path.GetExtension(FileThumb128.FileName));

                    string strUploadPAth = Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId));

                    if (!Directory.Exists(strUploadPAth))
                        Directory.CreateDirectory(strUploadPAth);

                    FileThumb128.SaveAs(Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId)) + "//" + filename);
                    txtThumbPath128.Value = Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId) + "//" + filename);

                    Img128x128.ImageUrl = "~//" + GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId) + "//" + filename;
                    mStrResult += "Thumbs of size 128x128 Updated<br/>";

                    // Delete previous thumb file of size 128
                    foreach (string mStrPrevFile in Directory.GetFiles(mStrProductImagePath, "*128x128*"))
                        File.Delete(mStrPrevFile);
                }
                catch (Exception ex)
                {
                    mStrResult += "Failed to upload thumb 128 : <br/>Following error occured: " + ex.Message;
                }
            }
            else if (txtThumbPath128.Value == "")
            {
                // Generate thumbs from main image and update it's value
                string mStrTempImgPath = (Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId)));
                txtThumbPath128.Value = ResizeImage(128, 128, txtImgPathMain.Value, mStrTempImgPath);
                mStrResult += "Thumbs of size 128x128 Generated & Updated<br/>";
            }

            if (FileThumb64.HasFile)
            {
                try
                {
                    string filename = String.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(FileThumb64.FileName), "64", Path.GetExtension(FileThumb64.FileName));

                    string strUploadPAth = Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId));

                    if (!Directory.Exists(strUploadPAth))
                        Directory.CreateDirectory(strUploadPAth);

                    FileThumb64.SaveAs(Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId)) + "//" + filename);
                    txtThumbPath64.Value = Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId) + "//" + filename);

                    Img64x64.ImageUrl = "~//" + GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId) + "//" + filename;
                    mStrResult += "Thumbs of size 64x64 Updated<br/>";

                    // Delete previous thumb file of size 64
                    foreach (string mStrPrevFile in Directory.GetFiles(mStrProductImagePath, "*64x64*"))
                        File.Delete(mStrPrevFile);
                }
                catch (Exception ex)
                {
                    mStrResult += "Failed to upload thumb 64 : <br/>Following error occured: " + ex.Message;
                }
            }
            else if (txtThumbPath64.Value == "")
            {
                // Generate thumbs from main image and update it's value
                string mStrTempImgPath = (Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId)));
                txtThumbPath64.Value = ResizeImage(64, 64, txtImgPathMain.Value, mStrTempImgPath);
                mStrResult += "Thumbs of size 64x64 Generated & Updated<br/>";
            }

            if (FileThumb32.HasFile)
            {
                try
                {
                    string filename = String.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(FileThumb32.FileName), "32", Path.GetExtension(FileThumb32.FileName));

                    string strUploadPAth = Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId));

                    if (!Directory.Exists(strUploadPAth))
                        Directory.CreateDirectory(strUploadPAth);

                    FileThumb32.SaveAs(Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId)) + "//" + filename);
                    txtThumbPath32.Value = Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId) + "//" + filename);

                    Img32x32.ImageUrl = "~//" + GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId) + "//" + filename;
                    mStrResult += "Thumbs of size 32x32 Updated<br/>";

                    // Delete previous thumb file of size 32
                    foreach (string mStrPrevFile in Directory.GetFiles(mStrProductImagePath, "*32x32*"))
                        File.Delete(mStrPrevFile);
                }
                catch (Exception ex)
                {
                    mStrResult += "Failed to upload thumb 32 : <br/>Following error occured: " + ex.Message;
                }
            }
            else if (txtThumbPath32.Value == "")
            {
                // Generate thumbs from main image and update it's value
                string mStrTempImgPath = (Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId)));
                txtThumbPath32.Value = ResizeImage(32, 32, txtImgPathMain.Value, mStrTempImgPath);
                mStrResult += "Thumbs of size 32x32 Generated & Updated<br/>";
            }

            if (FileThumb16.HasFile)
            {
                try
                {
                    string filename = String.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(FileThumb16.FileName), "16", Path.GetExtension(FileThumb16.FileName));

                    string strUploadPAth = Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId));

                    if (!Directory.Exists(strUploadPAth))
                        Directory.CreateDirectory(strUploadPAth);

                    FileThumb16.SaveAs(Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId)) + "//" + filename);
                    txtThumbPath16.Value = Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId) + "//" + filename);

                    Img16x16.ImageUrl = "~//" + GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId) + "//" + filename;
                    mStrResult += "Thumbs of size 16x16 Updated<br/>";

                    // Delete previous thumb file of size 16
                    foreach (string mStrPrevFile in Directory.GetFiles(mStrProductImagePath, "*16x16*"))
                        File.Delete(mStrPrevFile);
                }
                catch (Exception ex)
                {
                    mStrResult += "Failed to upload thumb 16 : <br/>Following error occured: " + ex.Message;
                }
            }
            else if (txtThumbPath16.Value == "")
            {
                // Generate thumbs from main image and update it's value
                string mStrTempImgPath = (Server.MapPath(GlobalVariables.TemporaryPath + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompDatabaseKey) + "//" + Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyId) + "//" + Convert.ToString(((SystemUser)Session["SystemUser"]).UserSysId)));
                txtThumbPath16.Value = ResizeImage(16, 16, txtImgPathMain.Value, mStrTempImgPath);
                mStrResult += "Thumbs of size 16x16 Generated & Updated<br/>";
            }

            //-----------------------------------------------------------------
            // Move and update table
            //-----------------------------------------------------------------
            // Move
            if (txtImgPathMain.Value.Contains(GlobalVariables.TemporaryPath))
                txtImgPathMain.Value = GetRelativePath(CopyFileSafely(txtImgPathMain.Value, mStrProductImagePath, true, true));
            else
                txtImgPathMain.Value = txtImgPathMain.Value.Replace("/", "\\");
            
            if (txtThumbPath256.Value.Contains(GlobalVariables.TemporaryPath))
                txtThumbPath256.Value = GetRelativePath(CopyFileSafely(txtThumbPath256.Value, mStrProductImagePath, true, true));
            else
                txtThumbPath256.Value = txtThumbPath256.Value.Replace("/", "\\");
            
            if (txtThumbPath128.Value.Contains(GlobalVariables.TemporaryPath))
                txtThumbPath128.Value = GetRelativePath(CopyFileSafely(txtThumbPath128.Value, mStrProductImagePath, true, true));
            else
                txtThumbPath128.Value = txtThumbPath128.Value.Replace("/", "\\");

            if (txtThumbPath64.Value.Contains(GlobalVariables.TemporaryPath))
                txtThumbPath64.Value = GetRelativePath(CopyFileSafely(txtThumbPath64.Value, mStrProductImagePath, true, true));
            else
                txtThumbPath64.Value = txtThumbPath64.Value.Replace("/", "\\");

            if (txtThumbPath32.Value.Contains(GlobalVariables.TemporaryPath))
                txtThumbPath32.Value = GetRelativePath(CopyFileSafely(txtThumbPath32.Value, mStrProductImagePath, true, true));
            else
                txtThumbPath32.Value = txtThumbPath32.Value.Replace("/", "\\");

            if (txtThumbPath16.Value.Contains(GlobalVariables.TemporaryPath))
                txtThumbPath16.Value = GetRelativePath(CopyFileSafely(txtThumbPath16.Value, mStrProductImagePath, true, true));
            else
                txtThumbPath16.Value = txtThumbPath16.Value.Replace("/", "\\");

            // Update
            string mStrSql = "Update ProductMaster      "
                                   + "Set PM_vCharImgFull = @ImgFull,   "
                                   + "PM_vCharThumb256 = @Tn256,    "
                                   + "PM_vCharThumb128 = @Tn128,    "
                                   + "PM_vCharThumb64  = @Tn64,     "
                                   + "PM_vCharThumb32  = @Tn32,     "
                                   + "PM_vCharThumb16 = @Tn16       "
                                   + "Where PM_bIntProdId = @ProdId";


            //int mIntTemp = SqlHelper.UpdateDatabase(mStrSql, SqlHelper.AddInParam("@ImgFull", SqlDbType.VarChar, txtImgPathMain.Value.Replace("~\\","")),
            //                                  SqlHelper.AddInParam("@Tn256", SqlDbType.VarChar, txtThumbPath256.Value.Replace("~\\", "")),
            //                                  SqlHelper.AddInParam("@Tn128", SqlDbType.VarChar, txtThumbPath128.Value.Replace("~\\", "")),
            //                                  SqlHelper.AddInParam("@Tn64", SqlDbType.VarChar, txtThumbPath64.Value.Replace("~\\", "")),
            //                                  SqlHelper.AddInParam("@Tn32", SqlDbType.VarChar, txtThumbPath32.Value.Replace("~\\", "")),
            //                                  SqlHelper.AddInParam("@Tn16", SqlDbType.VarChar, txtThumbPath16.Value.Replace("~\\", "")),
            //                                  SqlHelper.AddInParam("@ProdId", SqlDbType.BigInt, Convert.ToInt32(Session["FilePath"])));


            //int mIntTemp = SqlHelper.UpdateDatabase(mStrSql, SqlHelper.AddInParam("@ImgFull", SqlDbType.VarChar, txtImgPathMain.Value.Replace("\\", "\")),
            //                               SqlHelper.AddInParam("@Tn256", SqlDbType.VarChar, txtThumbPath256.Value.Replace("\\", "\")),
            //                               SqlHelper.AddInParam("@Tn128", SqlDbType.VarChar, txtThumbPath128.Value.Replace("\\", "\")),
            //                               SqlHelper.AddInParam("@Tn64", SqlDbType.VarChar, txtThumbPath64.Value.Replace("\\", "\\")),
            //                               SqlHelper.AddInParam("@Tn32", SqlDbType.VarChar, txtThumbPath32.Value.Replace("\\", "\\")),
            //                               SqlHelper.AddInParam("@Tn16", SqlDbType.VarChar, txtThumbPath16.Value.Replace("\\", "/")),
            //                               SqlHelper.AddInParam("@ProdId", SqlDbType.BigInt, Convert.ToInt32(Session["FilePath"])));



            
            int mIntTemp = SqlHelper.UpdateDatabase(mStrSql, SqlHelper.AddInParam("@ImgFull", SqlDbType.VarChar, txtImgPathMain.Value),
                                           SqlHelper.AddInParam("@Tn256", SqlDbType.VarChar, txtThumbPath256.Value),
                                           SqlHelper.AddInParam("@Tn128", SqlDbType.VarChar, txtThumbPath128.Value),
                                           SqlHelper.AddInParam("@Tn64", SqlDbType.VarChar, txtThumbPath64.Value),
                                           SqlHelper.AddInParam("@Tn32", SqlDbType.VarChar, txtThumbPath32.Value),
                                           SqlHelper.AddInParam("@Tn16", SqlDbType.VarChar, txtThumbPath16.Value),
                                           SqlHelper.AddInParam("@ProdId", SqlDbType.BigInt, Convert.ToInt32(Session["FilePath"])));


            if (mIntTemp > 0)
                mStrResult += "Data commited to database<br/>";
            else
                mStrResult += "Data not commited to database<br/>";

            if (!txtImgPathMain.Value.StartsWith("~\\"))
                MainImage.ImageUrl = "~\\" + txtImgPathMain.Value;
            if (!txtThumbPath256.Value.StartsWith("~\\"))
                Img256x256.ImageUrl = "~\\" + txtThumbPath256.Value;
            if (!txtThumbPath128.Value.StartsWith("~\\"))
                Img128x128.ImageUrl = "~\\" + txtThumbPath128.Value;
            if (!txtThumbPath64.Value.StartsWith("~\\"))
                Img64x64.ImageUrl = "~\\" + txtThumbPath64.Value;
            if (!txtThumbPath32.Value.StartsWith("~\\"))
                Img32x32.ImageUrl = "~\\" + txtThumbPath32.Value;
            if (!txtThumbPath16.Value.StartsWith("~\\"))
                Img16x16.ImageUrl = "~\\" + txtThumbPath16.Value;

            SetMessage(false, mStrResult);
        }

        #region "Methods"

        private string GetRelativePath(string mStrRealPath)
        {
            return mStrRealPath.Substring(mStrRealPath.IndexOf(GlobalVariables.FileHostPath, 0));
        }

        private void SetMessage(bool pBlnIsError, string pStrError)
        {
            actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            actionInfo.InnerHtml = pStrError;
        }

        public string ResizeImage(int pIntWidth, int pIntHeight, string pStrSourcePath, string pStrOutPutPath)
        {
            string mStrDicKey = String.Format("{0}x{1}", pIntWidth, pIntHeight);
            string mStrOutPutImageName = Path.GetFileNameWithoutExtension(pStrSourcePath).Replace(" ", "_").ToLower() + "_{0}" + Path.GetExtension(pStrSourcePath); //Commented to make image name real //DateTime.Now.ToString("ddmmyyyyHHMMSS_{0}") + Path.GetExtension(pStrSourcePath);

            ImageHandler mObjHandler = new ImageHandler();

            string mStrResult = Path.Combine(pStrOutPutPath, String.Format(mStrOutPutImageName, mStrDicKey));

            mObjHandler.Resize(pStrSourcePath, pIntWidth, pIntHeight, mStrResult,
                               ImageHandlerImgQuality.Max, OutputImageExtension.Same);

            return mStrResult;
        }

        public string CopyFileSafely(string pStrSourceFile, string pStrDestination, bool pBlnForceOverWrite,bool pBlnDeleteSourceFile)
        {
            if (pStrSourceFile == "" || pStrDestination == "")
                return "";
            pStrDestination = Path.Combine(pStrDestination, Path.GetFileName(pStrSourceFile));
            if (File.Exists(pStrSourceFile))
            {
                if (File.Exists(pStrDestination) && pBlnForceOverWrite)
                {
                    File.Delete(pStrDestination);
                    File.Copy(pStrSourceFile, pStrDestination);
                    if (pBlnDeleteSourceFile)
                        File.Delete(pStrSourceFile);
                    return pStrDestination;
                }
                else
                {
                    if (!File.Exists(pStrDestination))
                    {
                        File.Copy(pStrSourceFile, pStrDestination);
                        if (pBlnDeleteSourceFile)
                            File.Delete(pStrSourceFile);
                        return pStrDestination;
                    }
                    else
                        return "";
                }
            }
            else
                return "";
        }
        #endregion "Methods"

    }
}