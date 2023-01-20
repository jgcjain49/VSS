using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
   static string ipAdd;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.Cookies["MStore_Cookie_UserKey"] != null && Request.Cookies["MStore_Cookie_UserName"] != null && Request.Cookies["MStore_Cookie_Password"] != null)
            {
                txtKey.Text = Request.Cookies["MStore_Cookie_UserKey"].Value;
                txtId.Text = Request.Cookies["MStore_Cookie_UserName"].Value;
                txtPassword.Attributes["value"] = Request.Cookies["MStore_Cookie_Password"].Value;
                remember.Checked = true;
            }
        }
        fetchIP();
    }
    
    protected async void fetchIP()
    {
        string url = "https://api.ipify.org/?format=json";

        using (var httpClient = new System.Net.Http.HttpClient())
        {
            try
            {
                string response = await httpClient.GetStringAsync(url);
                var JsonResponse = JObject.Parse(response);
                ipAdd = Convert.ToString(JsonResponse["ip"]);
            }
            catch (Exception ex)
            {

            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (IsValid) // if page is valid
        {
            string CompKey = txtKey.Text.Replace("-", "");
            if (CompKey.Length == 8)
            {
                if (GlobalFunctions.IsNumeric(CompKey, false, false))
                {
                    // For remember me option.
                    SaveCookie();
                    object[] objAut;
                    if (ddlRoleList.SelectedItem.Value == "ADMIN")
                    {
                        Session["UserType"] = "A";
                        //objAut = SqlHelper.AuthenticateForAdmin(CompKey, txtId.Text, txtPassword.Text);
                        objAut = SqlHelper.AuthenticateForSubAdmin(CompKey, txtId.Text, txtPassword.Text);
                    }
                    else
                    {
                        if (ddlRoleList.SelectedItem.Value == "MD")
                        {
                            //Session["UserType"] = "M";
                            //objAut = SqlHelper.AuthenticateMainForDove(CompKey, txtId.Text, txtPassword.Text, 1);
                            objAut = new object[1] { null };
                            ShowError("Invalid login values.");
                        }
                        else
                        {
                            //Session["UserType"] = "D";
                            //objAut = SqlHelper.AuthenticateMainForDove(CompKey, txtId.Text, txtPassword.Text, 2);
                            objAut = new object[1] { null };
                            ShowError("Invalid login values.");
                        }

                    }
                    if (objAut[0] == null)
                    {
                        ShowError("Invalid login values.");
                    }
                    else
                    {
                        try
                        {
                            // Added to keep location trace
                            //if (locationJson.Value == "")
                            //{
                            //    ShowError("Please share your location to login.");


                            //}
                            //else
                            {

                                //ClientLocationTrace mObjTrace = JsonConvert.DeserializeObject<ClientLocationTrace>(locationJson.Value);
                                ClientLocationTrace mObjTrace = JsonConvert.DeserializeObject<ClientLocationTrace>("{\"Ip\": \"103.232.237.17\"}");
                                LogData objLogData = JsonConvert.DeserializeObject<LogData>(locationJson.Value);
                                if (mObjTrace.ErrorDetail == "")
                                {
                                    Session["CurrentClientLocation"] = mObjTrace;
                                    DataTable sdatetime = SqlHelper.ReadTable("select dbo.IndianTime() AS [IST_TIME]", GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);


                                    DataTable dtTable = SqlHelper.ReadTable("dbo.[SP_Session_Log]", GlobalVariables.SqlConnectionStringMstoreInformativeDb, true,


                                        SqlHelper.AddInParam("@LogId", SqlDbType.Int, 0),
                                        SqlHelper.AddInParam("@LoginName", SqlDbType.VarChar, txtId.Text),
                                        SqlHelper.AddInParam("@Password", SqlDbType.VarChar, txtPassword.Text),
                                        SqlHelper.AddInParam("@CompanyKey", SqlDbType.VarChar, CompKey),
                                        SqlHelper.AddInParam("@LoginType", SqlDbType.VarChar, Session["UserType"].ToString()),
                                        SqlHelper.AddInParam("@ASNNumber", SqlDbType.VarChar, mObjTrace.ASN),
                                        SqlHelper.AddInParam("@OrganizationName", SqlDbType.VarChar, mObjTrace.Host),
                                        SqlHelper.AddInParam("@City", SqlDbType.VarChar, mObjTrace.City),
                                        SqlHelper.AddInParam("@State", SqlDbType.VarChar, mObjTrace.State),
                                        SqlHelper.AddInParam("@Country", SqlDbType.VarChar, mObjTrace.Country),
                                        //SqlHelper.AddInParam("@IPAddress", SqlDbType.VarChar, mObjTrace.Ip),
                                        SqlHelper.AddInParam("@IPAddress", SqlDbType.VarChar, ipAdd),
                                        SqlHelper.AddInParam("@Latitude", SqlDbType.VarChar, mObjTrace.Latitude),
                                        SqlHelper.AddInParam("@Longitude", SqlDbType.VarChar, mObjTrace.Longitude),
                                        SqlHelper.AddInParam("@UserAgent", SqlDbType.VarChar, mObjTrace.UserAgent),
                                        SqlHelper.AddInParam("@LoginDateTime", SqlDbType.DateTime, Convert.ToDateTime(sdatetime.Rows[0]["IST_TIME"])),
                                        SqlHelper.AddInParam("@LogoutDateTime", SqlDbType.VarChar, null)
                                        );

                                    Session["LogId"] = dtTable.Rows[0][0];
                                    Session["SystemCompany"] = (SysCompany)objAut[0];
                                    //  Session["SystemUser"] = (SystemUser)objAut[1];/?Commented Since Now we dont Save the User Details
                                    //string sClntConnection = String.Format("Server={0};Database={1};User Id={2};Password={3};",
                                    //                           GlobalVariables.SqlConnectionInstance,
                                    //                           ((SysCompany)objAut[0]).CompDatabaseName,
                                    //                           GlobalVariables.SqlConnectionUserName,
                                    //                           GlobalVariables.SqlConnectionUserPass);


                                    Session["SystemUserSqlConnectionString"] = GlobalVariables.SqlConnectionString;
                                    Session["SystemUser"] = Convert.ToString(((SysCompany)Session["SystemCompany"]).UserName);
                                    Session["SystemUserID"] = Convert.ToString(((SysCompany)Session["SystemCompany"]).UserID);
                                    Session["PerOrdComm"] = Convert.ToString(((SysCompany)Session["SystemCompany"]).PerOrdComm);
                                    Session["PercntComm"] = Convert.ToString(((SysCompany)Session["SystemCompany"]).PercntComm);

                                    string strUserId = (Convert.ToString(Session["UserType"]) == "A") ? "M1" : Convert.ToString(((SysCompany)Session["SystemCompany"]).UserID);
                                    string query = "Insert into   Dove_Admin_Session_Log_17" +
                                               " ([DASL_bIntDistId],[DASL_varName],[DASL_nVarIpAdd],[DASL_dtLoginTime])" +
                                                "  Values (@bIntDistId,@varName,@nVarIpAdd,@dtLoginTime)";
                                    DataTable dtTable2 = SqlHelper.ReadTable(query, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false,
                                         SqlHelper.AddInParam("@bIntDistId", SqlDbType.NVarChar, strUserId),
                                         SqlHelper.AddInParam("@varName", SqlDbType.VarChar, Convert.ToString(((SysCompany)Session["SystemCompany"]).UserName)),
                                         SqlHelper.AddInParam("@nVarIpAdd", SqlDbType.NVarChar, ipAdd),
                                         SqlHelper.AddInParam("@dtLoginTime", SqlDbType.DateTime, Convert.ToDateTime(sdatetime.Rows[0]["IST_TIME"])));
                                    Response.Redirect("Home.aspx");
                                    // Go to next page according to role.
                                }
                                else
                                {
                                    ShowError(mObjTrace.ErrorDetail);
                                }
                            }
                        }
                        catch (Exception exError)
                        {
                            long pLngErr = -1;
                            if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                                pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                            pLngErr = GlobalFunctions.ReportError("Login", "Default", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                            ShowError("Login error : " + exError.Message + "at line No " + exError.StackTrace);
                        }

                        #region beforeaddinglocation
                        //  Session["SystemCompany"] = (SysCompany)objAut[0];
                        ////  Session["SystemUser"] = (SystemUser)objAut[1];/?Commented Since Now we dont Save the User Details
                        //  //string sClntConnection = String.Format("Server={0};Database={1};User Id={2};Password={3};",
                        //  //                           GlobalVariables.SqlConnectionInstance,
                        //  //                           ((SysCompany)objAut[0]).CompDatabaseName,
                        //  //                           GlobalVariables.SqlConnectionUserName,
                        //  //                           GlobalVariables.SqlConnectionUserPass);


                        //Session["SystemUserSqlConnectionString"] = GlobalVariables.SqlConnectionString;
                        //Session["SystemUser"] = Convert.ToString(((SysCompany)Session["SystemCompany"]).UserName);

                        //  Response.Redirect("Home.aspx");
                        //  // Go to next page according to role.
                        #endregion beforeaddinglocation
                    }
                }
                else
                    ShowError("Not a proper key.");
            }
            else
                ShowError("Key length not proper.");
        }
    }

    private void ShowError(string sError)
    {
        lblError.Text = sError;
        error.Style.Value = "display:block;";
    }

    private void SaveCookie()
    {
        if (remember.Checked)
        {
            Response.Cookies["MStore_Cookie_UserKey"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["MStore_Cookie_UserName"].Expires = DateTime.Now.AddDays(30);
            Response.Cookies["MStore_Cookie_Password"].Expires = DateTime.Now.AddDays(30);
        }
        else
        {
            Response.Cookies["MStore_Cookie_UserKey"].Expires = DateTime.Now.AddHours(6);
            Response.Cookies["MStore_Cookie_UserName"].Expires = DateTime.Now.AddHours(6);
            Response.Cookies["MStore_Cookie_Password"].Expires = DateTime.Now.AddHours(6);
        }
        Response.Cookies["MStore_Cookie_UserKey"].Value = txtKey.Text;
        Response.Cookies["MStore_Cookie_UserName"].Value = txtId.Text;
        Response.Cookies["MStore_Cookie_Password"].Value = txtPassword.Text;
        //MachineKey.Protect(Encoding.UTF8.GetBytes(Response.Cookies["MStore_Cookie_Password"].Value), "VITCO_28655035_312005_VITCO");

    }
}