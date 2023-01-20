using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_CommTrex
{
    public partial class AdminCreate : System.Web.UI.Page
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
                    FillAdminGrid();
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
        public const int SaltByteSize = 24;
        public const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        public const int Pbkdf2Iterations = 1000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;
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
                    mstrGetUser = "SELECT * from Com_Admin where iAdminId= '" + HidBnkId.Value + "'";
                    //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                    DataTable dtGetUserDetails = new DataTable();


                    //dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, Convert.ToString(Session["SystemUserSqlConnectionString"]), false,
                    //                SqlHelper.AddInParam("@iPayId", SqlDbType.Int, Convert.ToInt32(HidBnkId.Value)));

                    dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);

                    txtUserName.Text = dtGetUserDetails.Rows[0]["sUserName"].ToString();
                    //txtPass.Text = dtGetUserDetails.Rows[0]["sPassword"].ToString();
                    txtEmail.Text = dtGetUserDetails.Rows[0]["sEmailId"].ToString();
                    txtphnno.Text = dtGetUserDetails.Rows[0]["sContactNumber"].ToString();
                    txtEmpCode.Text = dtGetUserDetails.Rows[0]["sEmployeeCode"].ToString();
                    ddl_AdminRole.SelectedItem.Text = dtGetUserDetails.Rows[0]["sAdminRole"].ToString();
                    ddl_Action.SelectedItem.Text = dtGetUserDetails.Rows[0]["sAction"].ToString();

                    //System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                    //System.Text.Decoder utf8Decode = encoder.GetDecoder();
                    //byte[] todecode_byte = Convert.FromBase64String(txtPass.Text);
                    //int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                    //char[] decoded_char = new char[charCount];
                    //utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                    //string PasswordREsult = new String(decoded_char);
                    //txtPass.Text = PasswordREsult;
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


        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            string buttonstate = btnSave.InnerText.Replace("\r\n", "");
            string button = buttonstate.Replace(" ", "");
            try
            {
                //if (btnSave.Attributes["btn-action"] == "Save")
                if (button == "Save")
                {
                    string strValidate = "";
                    strValidate = ValidateUser(txtUserName.Text, txtPass.Text,
                        txtEmail.Text, txtphnno.Text, txtEmpCode.Text,
                        ddl_AdminRole.SelectedItem.Value, ddl_Action.SelectedItem.Value);
                    if (strValidate == "")
                    {
                          
                        string password = txtPass.Text;
                        var cryptoProvider = new RNGCryptoServiceProvider();
                        byte[] salt = new byte[SaltByteSize];
                        cryptoProvider.GetBytes(salt);

                        var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
                        string pass= Pbkdf2Iterations + ":" +
                               Convert.ToBase64String(salt) + ":" +
                               Convert.ToBase64String(hash);
                       

                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateAdmin", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                       // string strquery = "Insert into User_Master(UM_CompId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation,UM_vCharPassword,UM_bItIsActive) values (@comp_id,@u_name,@u_ID,@u_desg," + GlobalFunctions.CreateEncryptTextSyntax("@u_pass", false, false) + ",1)";
                        //SqlHelper.UpdateDatabase(strquery, SqlHelper.AddInParam("@comp_id",SqlDbType.BigInt, Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId)),
                        SqlHelper.AddInParam("@sUserName", SqlDbType.VarChar, txtUserName.Text),
                        SqlHelper.AddInParam("@sPassword", SqlDbType.VarChar, pass),
                        SqlHelper.AddInParam("@sEmailId", SqlDbType.VarChar, txtEmail.Text),
                        SqlHelper.AddInParam("@sContactNumber", SqlDbType.VarChar, txtphnno.Text),
                        SqlHelper.AddInParam("@sEmployeeCode", SqlDbType.VarChar, txtEmpCode.Text),
                        SqlHelper.AddInParam("@sAdminRole", SqlDbType.VarChar, ddl_AdminRole.SelectedItem.Text),
                            SqlHelper.AddInParam("@sIsFlag", SqlDbType.VarChar, "B"),
                        SqlHelper.AddInParam("@sAction", SqlDbType.VarChar, ddl_Action.SelectedItem.Text));
                        FillAdminGrid();
                        SetMessage(false, "Admin Added Successfully");
                        LockControls(false);
                        
                        btnSave.InnerHtml = "New";
                        //btnSave.Attributes["btn-action"] = "New";
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
                        strValidate = ValidateUser(txtUserName.Text, txtPass.Text,
                         txtEmail.Text, txtphnno.Text, txtEmpCode.Text,
                         ddl_AdminRole.SelectedItem.Text, ddl_Action.SelectedItem.Text);
                        if (strValidate == "")
                        {
                            DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateAdmin", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,

                                                    SqlHelper.AddInParam("@sUserName", SqlDbType.VarChar, txtUserName.Text),
                                                     SqlHelper.AddInParam("@sPassword", SqlDbType.VarChar, txtPass.Text),
                                                     SqlHelper.AddInParam("@sEmailId", SqlDbType.VarChar, txtEmail.Text),
                                                     SqlHelper.AddInParam("@sContactNumber", SqlDbType.VarChar, txtphnno.Text),
                                                     SqlHelper.AddInParam("@sEmployeeCode", SqlDbType.VarChar, txtEmpCode.Text),
                                                     SqlHelper.AddInParam("@sAdminRole", SqlDbType.VarChar, ddl_AdminRole.SelectedItem.Text),
                                                     SqlHelper.AddInParam("@sAction", SqlDbType.VarChar, ddl_Action.SelectedItem.Text),
                                                     SqlHelper.AddInParam("@sIsFlag", SqlDbType.VarChar, "1"),
                                                     SqlHelper.AddInParam("@iAdminId", SqlDbType.Int, HidBnkId.Value)
                                                     
                                                     );

                         
                            grdAdmin.EditIndex = -1;
                            FillAdminGrid();
                            ClearAll();
                            SetMessage(false, "Admin Updated Successfully");
                            btnClear.InnerHtml = "Clear";
                            btnSave.InnerHtml = "Save";
                           
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
                    //btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> Save";
                    SetMessage(false, "Press Save To Add Client!!");
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
        
        private string ValidateUser(string strUserName, string strPass, string strEmail,
          string strphnno, string strEmpCode, string strAdminRole, string strAction)
        {
            string mstrValidate = "";
            if (strUserName == "")
            {
                mstrValidate = mstrValidate + " User Name Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strPass == "")
            {
                mstrValidate = mstrValidate + "Password Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strEmail == "")
            {
                mstrValidate = mstrValidate + "Email ID Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strphnno == "")
            {
                mstrValidate = mstrValidate + "Contact Number Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strEmpCode == "")
            {
                mstrValidate = mstrValidate + "Employee Code Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strAdminRole == "0")
            {
                mstrValidate = mstrValidate + "Admin Role Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strAction == "0")
            {
                mstrValidate = mstrValidate + "Action Cannot be Blank !!!";
                return mstrValidate;
            }
            return mstrValidate;
        }

        public void FillAdminGrid()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT * from  Com_Admin where bIsActive=1";
                //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                grdAdmin.DataSource = dtGetUserDetails;
                grdAdmin.DataBind();

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
            txtUserName.Enabled = pBoolState;
            txtPass.Enabled = pBoolState;
            txtEmail.Enabled = pBoolState;
            txtphnno.Enabled = pBoolState;
            txtEmpCode.Enabled = pBoolState;
            ddl_AdminRole.Enabled = pBoolState;
            ddl_Action.Enabled = pBoolState;
        }
        public void ClearControls()
        {
            txtUserName.Text = "";
            txtPass.Text = "";
            txtEmail.Text = "";
            txtphnno.Text = "";
            txtEmpCode.Text = "";
            ddl_AdminRole.SelectedIndex = 0;
            ddl_Action.SelectedIndex = 0;
            SetMessage(false, "Press Save to Add Admin Details");
        } 
              
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAdmin.EditIndex = e.NewEditIndex;
            ViewState["RowVal"] = e.NewEditIndex;
            FillAdminGrid();
            grdAdmin.Columns[8].Visible = true;
            
        }

        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            try
            { 
                TextBox txtname = (TextBox)grdAdmin.Rows[e.RowIndex].FindControl("gusername");
                TextBox txtpass = (TextBox)grdAdmin.Rows[e.RowIndex].FindControl("gpassword");
                 TextBox txtemailid = (TextBox)grdAdmin.Rows[e.RowIndex].FindControl("gemailid");
                TextBox txtcontactnum = (TextBox)grdAdmin.Rows[e.RowIndex].FindControl("gcontactnumber");
                TextBox txtempcode = (TextBox)grdAdmin.Rows[e.RowIndex].FindControl("gemployeecode");
                string ddl_AdminRole = (grdAdmin.Rows[e.RowIndex].FindControl("gadminrole") as DropDownList).SelectedItem.Text;
                string ddl_Action = (grdAdmin.Rows[e.RowIndex].FindControl("guseraction") as DropDownList).SelectedItem.Text;
               
                Label lblID = (Label)grdAdmin.Rows[e.RowIndex].FindControl("UID");
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
                    strValidate = ValidateUser(txtname.Text, txtpass.Text,
                        txtemailid.Text, txtcontactnum.Text, txtempcode.Text,
                        ddl_AdminRole, ddl_Action);
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateAdmin", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                            
                                                SqlHelper.AddInParam("@sUserName", SqlDbType.VarChar, txtname.Text),
                                                 SqlHelper.AddInParam("@sPassword", SqlDbType.VarChar, txtpass.Text),
                                                 SqlHelper.AddInParam("@sEmailId", SqlDbType.VarChar, txtemailid.Text),
                                                 SqlHelper.AddInParam("@sContactNumber", SqlDbType.VarChar, txtcontactnum.Text),
                                                 SqlHelper.AddInParam("@sEmployeeCode", SqlDbType.VarChar, txtempcode.Text),
                                                 SqlHelper.AddInParam("@sAdminRole", SqlDbType.VarChar, ddl_AdminRole),
                                                 SqlHelper.AddInParam("@sAction", SqlDbType.VarChar, ddl_Action),
                                                 SqlHelper.AddInParam("@iAdminId", SqlDbType.Int, (lblID.Text)));

                        SetProductsUpdateMessage(false, "Admin Updated Successfully");
                        grdAdmin.EditIndex = -1;
                        FillAdminGrid();
                        ClearAll();
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

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAdmin.EditIndex = -1;
            FillAdminGrid();
            grdAdmin.Columns[8].Visible = false;
            ClearAll();
            //if (txtImgPathMain.Value != "")
            //{
            //    bool blnFlagDelete = DeleteFile(txtImgPathMain.Value);
            //}
        }
        private void ClearAll()
        {
            txtUserName.Text = "";
            txtPass.Text = "";
            txtEmail.Text = "";
            txtphnno.Text = "";
            SetMessage(false, "Press Save to Add Admin Details");
            txtEmpCode.Text = "";
            ddl_AdminRole.SelectedItem.Value = "0";
            ddl_Action.SelectedItem.Value = "0";

            //ViewState["ImgLogo"] = "";
            //ViewState["ImgPath"] = "";
            //ViewState["RowVal"] = "";
        }
        protected void btnDeleteAdmin_ServerClick(object sender, EventArgs e)
        {
            try
            {
            //string strquery = "update Com_Admin set @IsActive=@isavtive where iAdminId=@id";
             DataTable dtCatData = SqlHelper.ReadTable("spDeleteAdmin", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
             SqlHelper.AddInParam("@id", SqlDbType.Int, Convert.ToInt32(txtDelHidden.Value)));
            SetProductsUpdateMessage(false, "Admin Deleted Successfully");
            txtDelHidden.Value = "";
            txtDelAdminID.Text = "";
            txtDelAdminName.Text = "";
            grdAdmin.EditIndex = -1;
            FillAdminGrid();
          
                // Delete sub category from server
               
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

        protected void btnSendOTP_Click(object sender, EventArgs e)
        {

            VerifyNumText.Enabled = false;
                int generatedOTP = GlobalFunctions.GenerateOTP(5);
                string qLogOtp = " INSERT INTO dbo.Voucher_OTP_Log_17 ([VOL_vCharMOBILE_NO],[VOL_vCharOTP],[VOL_dtGNRTD_ON],[VOL_intNoOfAttmpt]) " +
                                 " VALUES (@userMobile,@otp,@gnrtdDt,0) ";
                try
                {
                    SqlHelper.ReadTable(qLogOtp, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false,
                            SqlHelper.AddInParam("@userMobile", SqlDbType.VarChar, "+91" + VerifyNumText.Text),
                            SqlHelper.AddInParam("@otp", SqlDbType.VarChar, generatedOTP.ToString()),
                            SqlHelper.AddInParam("@gnrtdDt", SqlDbType.DateTime, DateTime.UtcNow.AddMinutes(330).ToString("yyyy-MM-dd HH:mm:ss"))
                        );
                }
                catch (Exception ex) { }
                string otpResponse = GlobalFunctions.sendOTPSms(VerifyNumText.Text, generatedOTP);

                if (otpResponse.Equals("Success"))
                {
                    actionInfo.InnerHtml = "OTP sent to selected mobile number successfully!";
                    //txtOTP.Enabled = true;
                    //OtpPanel.Visible = true;
                }
                else
                {
                    actionInfo.InnerHtml = "Something went wrong, please try after some time!";
                    VerifyNumText.Enabled = true;
                }          
         
        }

        public string validateOTP(int userSubmittedOTP, string userMobNum)
        {
            string validationRes = "";
            string qFetchOtp = " SELECT TOP 1 * " +//" SELECT TOP 1 [VOL_vCharOTP],isnull([VOL_intNoOfAttmpt],0) VOL_intNoOfAttmpt " +
                                " FROM dbo.Voucher_OTP_Log_17 " +
                                " WHERE VOL_vCharMOBILE_NO = '+91'+@userMobNum" +
                                " ORDER BY VOL_dtGNRTD_ON DESC ";
            try
            {
                DataTable dtFetchOtp = SqlHelper.ReadTable(qFetchOtp, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false,
                    SqlHelper.AddInParam("@userMobNum", SqlDbType.VarChar, userMobNum));
                if (dtFetchOtp.Rows.Count > 0)
                {

                    int noOfAttempts = Convert.ToInt32(dtFetchOtp.Rows[0]["VOL_intNoOfAttmpt"]);
                    if (noOfAttempts != -1)
                    {
                        if (noOfAttempts < 3)
                        {
                            noOfAttempts = noOfAttempts + 1;
                            string sqlupdate = "update [dbo].[Voucher_OTP_Log_17] set VOL_intNoOfAttmpt=@noOfAttempts where VOL_bIntId=@bintId";
                            DataTable dtUpdateAttemt = SqlHelper.ReadTable(sqlupdate, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false,
                                                SqlHelper.AddInParam("@noOfAttempts", SqlDbType.Int, noOfAttempts),
                                                SqlHelper.AddInParam("@bintId", SqlDbType.BigInt, Convert.ToInt64(dtFetchOtp.Rows[0]["VOL_bIntId"])));

                            int sentOTP = Convert.ToInt32(dtFetchOtp.Rows[0]["VOL_vCharOTP"]);
                            if (userSubmittedOTP == sentOTP)
                            {
                                string sqlupda = "update [dbo].[Voucher_OTP_Log_17] set VOL_intNoOfAttmpt=@noOfAttempts where VOL_bIntId=@bintId";
                                DataTable dtUpdateApp = SqlHelper.ReadTable(sqlupda, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false,
                                                    SqlHelper.AddInParam("@noOfAttempts", SqlDbType.Int, -1),
                                                    SqlHelper.AddInParam("@bintId", SqlDbType.BigInt, Convert.ToInt64(dtFetchOtp.Rows[0]["VOL_bIntId"])));
                                //validationRes = "Mobile number verified successfully.";

                            }
                            else
                            {
                                validationRes = "Invalid OTP. Please try again.";
                            }
                        }
                        else
                        {
                            validationRes = "Too many invalid attempts. Please try after sometime.";
                        }
                    }
                    else
                    {
                        validationRes = "Please generate OTP first.";
                    }
                }
                else
                    validationRes = "Something went wrong. Please try generating OTP again.";
            }
            catch (Exception ex)
            {
                validationRes = "Something went wrong. Please try again." + ex.Message.ToString();
            }
            return validationRes;
        }

        protected void btnSubmit_click(object sender, EventArgs e)
        {

            if ((txtOtp.Text == "") || (NewPassword.Text == ""))
            {

                Div2.InnerHtml = "Fields Cannot be blank";
            }

            else { 
         
            }
        }
    }
}