using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.Configuration;


namespace Admin_CommTrex
{
    public partial class UserMaster : System.Web.UI.Page
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {
       
            string buttonstate = btnSave.InnerText.Replace("\r\n", "");
            string button = buttonstate.Replace(" ", "");
            if (!IsPostBack)

            {txtmob.Enabled = false;
                if (Session["TalukaDetails"] != null)
                {
                    if (Convert.ToString(Session["UserType"]) == "U")
                    {
                        Response.Redirect("Home.aspx");
                    }
                    FillClientGrid();
                    getWarehousename();
                    getCommodityName();
                    
                    txtmob.Enabled = false;
                  
                    LockControls(false);
                    //FillPincode();
                    //FillCountry();
                    //grdUser.Columns[6].Visible = false;
                    //grdUser.Columns[10].Visible = false;
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
                    getWarehousename();
                    getCommodityName();
                    fetchEditDetails();
                   
                  
                }
                else if (button == "Update")
                {


                }
                else
                {

                }
            }
            //Session["Reset"] = true;
            //Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
            //SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
            //int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
        }

         public void GetPincodeData(object sender, EventArgs e) 
        {
            try
            {
                if (pincode.Text.Length == 6)
                {
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "", "alert('" + pincode.Text + "')", true);
                    string mstrGetUser = "";
                    mstrGetUser = "SELECT StateName,Cityname,Countryname,TalukaName from  Com_pincodeList where PincodeName= '" + pincode.Text + "'";
                    //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                    DataTable dtGetUserDetails;
                    dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                    Country.Text = dtGetUserDetails.Rows[0]["Countryname"].ToString();
                    State.Text = dtGetUserDetails.Rows[0]["StateName"].ToString();
                    City.Text = dtGetUserDetails.Rows[0]["Cityname"].ToString();
                    txtaddress2.Text = dtGetUserDetails.Rows[0]["TalukaName"].ToString();
                   
                }
                else { }
              
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillClientGrid", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }

        }

         protected void fetchEditDetails()  
         {
             //getWarehousename();
             //getCommodityName();
             if (HidBnkId.Value == "")
             {
             }
             else
             {
                 try
                 {
                     string selectedComDealt = listbox_comm_dealt.SelectedItem.Text;
                     string mstrGetUser = "";
                     mstrGetUser = "SELECT * from Com_Client where iClientId= '" + HidBnkId.Value + "'";
                     //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                     DataTable dtGetUserDetails = new DataTable();


                     //dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, Convert.ToString(Session["SystemUserSqlConnectionString"]), false,
                     //                SqlHelper.AddInParam("@iPayId", SqlDbType.Int, Convert.ToInt32(HidBnkId.Value)));

                     dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);

                     ////Selected Items which selected programmatically.  
                     //List<string> _selectedTechnology = new List<string>();
                     //_selectedTechnology.Add("ASP.NET Web Form");
                     //_selectedTechnology.Add("C#");
                     //_selectedTechnology.Add("JavaScript");
                     //_selectedTechnology.Add("VB.NET");
                     //if ((lbox_comm_dealt.Items.Count > 0) && (_selectedTechnology.Count > 0))
                     //{
                     //    for (int i2 = 0; i2 < lbox_comm_dealt.Items.Count; i2++)
                     //    {
                     //        for (int i = 0; i < _selectedTechnology.Count; i++)
                     //        {
                     //            if (lbox_comm_dealt.Items[i2].Text.ToString().Trim() == _selectedTechnology[i].ToString().Trim())
                     //            {
                     //                lbox_comm_dealt.Items[i2].Selected = true;
                     //            }
                     //        }
                     //    }
                     //}  

                     dllclientType.SelectedItem.Text = dtGetUserDetails.Rows[0]["SClientType"].ToString();
                     txtcon_pername.Text = dtGetUserDetails.Rows[0]["sContactPerson"].ToString();
                     txtphnno.Text = dtGetUserDetails.Rows[0]["sContactNumber"].ToString();
                     txtphnno2.Text = dtGetUserDetails.Rows[0]["sContactNumber2"].ToString();
                     txtmob.Text = dtGetUserDetails.Rows[0]["sMobileNumber"].ToString();
                     txtEmail.Text = dtGetUserDetails.Rows[0]["sEmailid"].ToString();
                     txtcompnm.Text = dtGetUserDetails.Rows[0]["sCompany"].ToString();
                     txtaddress.Text = dtGetUserDetails.Rows[0]["sCompanyAdd"].ToString();

                     txtaddress22.Text = dtGetUserDetails.Rows[0]["sAddressLine2"].ToString();
                     txtaddress3.Text = dtGetUserDetails.Rows[0]["sAddressLine3"].ToString();
                     
                     txtaddress2.Text = dtGetUserDetails.Rows[0]["sCompanyAdd2"].ToString();
                     pincode.Text = dtGetUserDetails.Rows[0]["sPostalCode"].ToString();
                     Country.Text = dtGetUserDetails.Rows[0]["sComapnyAddress"].ToString();
                     State.Text = dtGetUserDetails.Rows[0]["sComapnyAddress2"].ToString();
                     City.Text = dtGetUserDetails.Rows[0]["sComapnyAddress3"].ToString();
                    
                     txtGST.Text = dtGetUserDetails.Rows[0]["sGSTnumber"].ToString();
                     txtAGSTPath.Text = dtGetUserDetails.Rows[0]["sGstUplaodDoc"].ToString();
                     txtComRegNum.Text = dtGetUserDetails.Rows[0]["sComRegNum"].ToString();
                     txtAROCPath.Text = dtGetUserDetails.Rows[0]["sRocUploadDoc"].ToString();
                     selectedComDealt = dtGetUserDetails.Rows[0]["sCommodityDealt"].ToString();
                     ddl_warehouse.SelectedItem.Text = dtGetUserDetails.Rows[0]["sWarehouse"].ToString();
                     txtwarehouseLoc.Text = dtGetUserDetails.Rows[0]["sWarehouseLoc"].ToString();
                     //ddl_Role.SelectedItem.Text = dtGetUserDetails.Rows[0]["sRole"].ToString();
                  

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
                    string strPath1 = "";

                    strValidate = ValidateUser(dllclientType.SelectedItem.Text, txtcon_pername.Text, txtphnno.Text, txtmob.Text, txtEmail.Text,
                            txtcompnm.Text, txtaddress.Text, txtaddress22.Text, txtaddress2.Text, pincode.Text, Country.Text, State.Text, City.Text,
                            listbox_comm_dealt.SelectedItem.Text,
                            //txtGST.Text, txtAGSTPath.Text, txtComRegNum.Text, txtAROCPath.Text, 
                            ddl_warehouse.SelectedItem.Text, txtwarehouseLoc.Text
                            //, ddl_Role.SelectedItem.Text
                            );
                    if (strValidate == "")
                    {
                        strPath = txtAGSTPath.Text.Replace("//", "/");
                        strPath1 = txtAROCPath.Text.Replace("//", "/");

                        string selectedComDealt = string.Empty;
                        foreach (ListItem li in listbox_comm_dealt.Items)
                        {
                            if (li.Selected == true)
                            {
                                selectedComDealt += li.Text + ",";
                                //selectedComDealt += li.Text;
                            }
                        }


                        if ((txtGST.Text == "") || (strPath == "") || (txtComRegNum.Text == "") || (strPath1 == ""))
                        {
                            txtstatus.Text = "Incomplete";

                        }
                        else
                        {
                            txtstatus.Text = "Complete";
                        }
                        
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateClient", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                        SqlHelper.AddInParam("@SClientType", SqlDbType.VarChar, dllclientType.SelectedItem.Text),
                        SqlHelper.AddInParam("@sContactPerson", SqlDbType.VarChar, txtcon_pername.Text),
                        SqlHelper.AddInParam("@sContactNumber", SqlDbType.VarChar, txtphnno.Text),
                        SqlHelper.AddInParam("@sContactNumber2", SqlDbType.VarChar, txtphnno2.Text),
                        SqlHelper.AddInParam("@sMobileNumber", SqlDbType.VarChar,  txtmob.Text),
                        SqlHelper.AddInParam("@sEmailid", SqlDbType.VarChar, txtEmail.Text),
                        SqlHelper.AddInParam("@sCompany", SqlDbType.VarChar, txtcompnm.Text),
                        SqlHelper.AddInParam("@sCompanyAdd", SqlDbType.VarChar, txtaddress.Text),

                        SqlHelper.AddInParam("@sAddressLine2", SqlDbType.VarChar, txtaddress22.Text),
                        SqlHelper.AddInParam("@sAddressLine3", SqlDbType.VarChar, txtaddress3.Text),

                        SqlHelper.AddInParam("@sCompanyAdd2", SqlDbType.VarChar, txtaddress2.Text),
                        SqlHelper.AddInParam("@sPostalCode", SqlDbType.VarChar, Convert.ToString(pincode.Text)),
                        SqlHelper.AddInParam("@sComapnyAddress", SqlDbType.VarChar, Convert.ToString(Country.Text)),
                        SqlHelper.AddInParam("@sComapnyAddress2", SqlDbType.VarChar, Convert.ToString(State.Text)),
                        SqlHelper.AddInParam("@sComapnyAddress3", SqlDbType.VarChar, Convert.ToString(City.Text)),
                        SqlHelper.AddInParam("@sCommodityDealt", SqlDbType.VarChar, selectedComDealt),
                        SqlHelper.AddInParam("@sGSTnumber", SqlDbType.VarChar, txtGST.Text),
                        SqlHelper.AddInParam("@sGstUplaodDoc", SqlDbType.VarChar, strPath),
                        SqlHelper.AddInParam("@sComRegNum", SqlDbType.VarChar, txtComRegNum.Text),
                        SqlHelper.AddInParam("@sRocUploadDoc", SqlDbType.VarChar, strPath1),
                        SqlHelper.AddInParam("@sWarehouse", SqlDbType.VarChar, ddl_warehouse.SelectedItem.Text),
                        SqlHelper.AddInParam("@sWarehouseLoc", SqlDbType.VarChar, txtwarehouseLoc.Text),
                        //SqlHelper.AddInParam("@sRole", SqlDbType.VarChar, ddl_Role.SelectedItem.Text),
                        SqlHelper.AddInParam("@sStatus", SqlDbType.VarChar, txtstatus.Text));

                        FillClientGrid();
                        SetMessage(false, "Client Saved Successfully");
                        LockControls(false);
                        btnSave.Attributes["btn-action"] = "New";
                        btnSave.InnerHtml = "New";
                        SetMessage(false, "Client" +" "+ txtcon_pername.Text +" "+"Added Successfully, Kindly Proceed For Approval.");
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
                        strValidate = ValidateUser(dllclientType.SelectedItem.Text, txtcon_pername.Text, txtphnno.Text, txtmob.Text, txtEmail.Text,
                            txtcompnm.Text, txtaddress.Text, txtaddress22.Text, txtaddress2.Text, pincode.Text, Country.Text, State.Text, City.Text,
                            listbox_comm_dealt.SelectedItem.Text,
                            //txtGST.Text, txtAGSTPath.Text, txtComRegNum.Text, txtAROCPath.Text, 
                            ddl_warehouse.SelectedItem.Text, txtwarehouseLoc.Text
                            //, ddl_Role.SelectedItem.Text
                            );
                        if (strValidate == "")
                        {
                            string data1 = txtAGSTPath.Text.Replace("//", "/");
                            string data2 = txtAROCPath.Text.Replace("//", "/");

                            if ((txtGST.Text == "") || (data1 == "") || (txtComRegNum.Text == "") || (data2 == ""))
                            {
                                txtstatus.Text = "Incomplete";

                            }
                            else
                            {
                                txtstatus.Text = "Complete";
                            }
                            //string selectedComDealtupdate = string.Empty;
                            //foreach (ListItem li in lbox_comm_dealt.Items)
                            //{
                            //    if (li.Selected == true)
                            //    {
                            //        selectedComDealtupdate += li.Text + ",";
                            //    }
                            //}

                            DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateClient", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                 SqlHelper.AddInParam("@SClientType", SqlDbType.VarChar, dllclientType.SelectedItem.Text),
                                SqlHelper.AddInParam("@sContactPerson", SqlDbType.VarChar, txtcon_pername.Text),
                                   SqlHelper.AddInParam("@sContactNumber", SqlDbType.VarChar, txtphnno.Text),
                                    SqlHelper.AddInParam("@sContactNumber2", SqlDbType.VarChar, txtphnno2.Text),
                                     SqlHelper.AddInParam("@sMobileNumber", SqlDbType.VarChar,  txtmob.Text),
                                    SqlHelper.AddInParam("@sEmailid", SqlDbType.VarChar, txtEmail.Text),
                                   SqlHelper.AddInParam("@sCompany", SqlDbType.VarChar, txtcompnm.Text),
                                   SqlHelper.AddInParam("@sCompanyAdd", SqlDbType.VarChar, txtaddress.Text),

                                    SqlHelper.AddInParam("@sAddressLine2", SqlDbType.VarChar, txtaddress22.Text),
                                     SqlHelper.AddInParam("@sAddressLine3", SqlDbType.VarChar, txtaddress3.Text),

                                    SqlHelper.AddInParam("@sCompanyAdd2", SqlDbType.VarChar, txtaddress2.Text),
                                     SqlHelper.AddInParam("@sPostalCode", SqlDbType.VarChar, (pincode.Text)),
                                   SqlHelper.AddInParam("@sComapnyAddress", SqlDbType.VarChar, (Country.Text)),
                                    SqlHelper.AddInParam("@sComapnyAddress2", SqlDbType.VarChar, State.Text),
                                     SqlHelper.AddInParam("@sComapnyAddress3", SqlDbType.VarChar, City.Text),
                                   SqlHelper.AddInParam("@sCommodityDealt", SqlDbType.VarChar, listbox_comm_dealt.SelectedItem.Text),
                                     SqlHelper.AddInParam("@sGSTnumber", SqlDbType.VarChar, txtGST.Text),
                                      SqlHelper.AddInParam("@sGstUplaodDoc", SqlDbType.VarChar, data1),
                                     SqlHelper.AddInParam("@sComRegNum", SqlDbType.VarChar, txtComRegNum.Text),
                                   SqlHelper.AddInParam("@sRocUploadDoc", SqlDbType.VarChar, data2),
                                   SqlHelper.AddInParam("@sWarehouse", SqlDbType.VarChar, ddl_warehouse.SelectedItem.Text),
                                    SqlHelper.AddInParam("@sWarehouseLoc", SqlDbType.VarChar, txtwarehouseLoc.Text),
                                   //SqlHelper.AddInParam("@sRole", SqlDbType.VarChar, ddl_Role.SelectedItem.Text),
                                  SqlHelper.AddInParam("@iClientId", SqlDbType.Int, HidBnkId.Value),
                               SqlHelper.AddInParam("@sStatus", SqlDbType.VarChar, txtstatus.Text));


                            SetProductsUpdateMessage(false, "USER Master Updated Successfully");
                            grdUser.EditIndex = -1;
                            FillClientGrid();
                            SetMessage(false, "Client Master Updated Succesfully!!");
                            //grdUser.Columns[6].Visible = false;
                            btnSave.InnerHtml = "Save";
                            btnClear.InnerHtml = "Clear";
                            listbox_comm_dealt.Items.Clear();
                            ddl_warehouse.Items.Clear();
                            getWarehousename();
                            getCommodityName();
                            ClearControls();
                            grdUser.Columns[16].Visible = true;
                            grdUser.Columns[21].Visible = true;
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
                        pLngErr = GlobalFunctions.ReportError("GridView1_RowUpdating1", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
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
            else{}
        
        }

        protected string ValidateUser(string strclienttype, string strContPersonNm, string strContPersonNo, string strmobilenum,
            string stremailid, string strCompNm, string strCompAdd, string straddress22, string strCompAdd2, string strpincode, string strCompAddr1, string strCompAddr2,
            string strCompAddr3, string strCommod_dealt_in,
         //string strGSTnum, string strGST, string strComRegNum, string strROC, 
            string strWarehouse, string strWarehouseLoc)
        {

         
            string mstrValidate = "";
            if (strclienttype == "--Select Client Type--")
            {
                mstrValidate = mstrValidate + "Client Type Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strContPersonNm == "")
            {
                mstrValidate = mstrValidate + "Contact person Name Cannot be Blank !!!";
                return mstrValidate;
            }

            if (strContPersonNo == "")
            {
                mstrValidate = mstrValidate + "Contact person Number Cannot be Blank !!!";
                return mstrValidate;
            }
            //if (strContPersonNo2 == "")
            //{
            //    mstrValidate = mstrValidate + "Contact person 2 Number Cannot be Blank !!!";
            //    return mstrValidate;
            //}

            if (strmobilenum == "")
            {
                mstrValidate = mstrValidate + "Mobile Number Cannot be Blank !!!";
                return mstrValidate;
            }

            if (stremailid == "")
            {
                mstrValidate = mstrValidate + "Email ID Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strCompNm == "")
            {
                mstrValidate = mstrValidate + "Company Name Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strCompAdd == "")
            {
                mstrValidate = mstrValidate + "Address Line 1 Cannot be Blank !!!";
                return mstrValidate;
            }
            if (straddress22 == "")
            {
                mstrValidate = mstrValidate + "Address Line 2 Cannot be Blank !!!";
                return mstrValidate;
            }
            //if (straddress3 == "")
            //{
            //    mstrValidate = mstrValidate + "Address Line 3 Cannot be Blank !!!";
            //    return mstrValidate;
            //}

            if (strCompAdd2 == "")
            {
                mstrValidate = mstrValidate + "Company Address 2 Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strpincode == "--Pincode--")
            {
                mstrValidate = mstrValidate + "PinCode Cannot be Blank !!!";
                return mstrValidate;
            }

            if (strCompAddr1 == "")
            {
                mstrValidate = mstrValidate + "Company Address 1 Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strCompAddr2 == "")
            {
                mstrValidate = mstrValidate + "Company Address 2 Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strCompAddr3 == "")
            {
                mstrValidate = mstrValidate + "Company Address 3 Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strCommod_dealt_in == "-- Select Comodity  Name --")
            {
                mstrValidate = mstrValidate + "Commodities dealt in Cannot be Blank !!!";
                return mstrValidate;
            }
            //if (strGSTnum == "")
            //{
            //    mstrValidate = mstrValidate + "GST Number Cannot be Blank !!!";
            //    return mstrValidate;
            //}
 
            //if (strGST == "")
            //{
            //    mstrValidate = mstrValidate + "GST Document Cannot be Blank !!!";
            //    return mstrValidate;
            //}
            //if (strComRegNum == "")
            //{
            //    mstrValidate = mstrValidate + "Company Registration Number Cannot be Blank !!!";
            //    return mstrValidate;
            //}
          
            //if (strROC == "")
            //{
            //    mstrValidate = mstrValidate + "ROC Cannot be Blank !!!";
            //    return mstrValidate;
            //}
            if (strWarehouse == "-- Select Warehouse Name --")
            {
                mstrValidate = mstrValidate + "Warehouse Name Cannot be Blank !!!";
                return mstrValidate;
            }
            if (strWarehouseLoc == "")
            {
                mstrValidate = mstrValidate + "Warehouse Location Cannot be Blank !!!";
                return mstrValidate;
            }
            //if (strRolearea == "-- Select Role --")
            //{
            //    mstrValidate = mstrValidate + "Role areas Cannot be Blank !!!";
            //}
            return mstrValidate;
        }

        //protected void btnPermissionSet_ServerClick(object sender, EventArgs e)
        //{
        //    string conString = Convert.ToString(GlobalVariables.SqlConnectionStringMstoreInformativeDb); //GlobalVariables.ConnectionString;
        //    int intTalukaId = Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID);// Convert.ToInt32(drdlTalukaNm.DataValueField);
        //    string strTalukaNm = Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaName);

        //    Dictionary<string, object> mDicMenuList = GetPageLists();
        //    StringBuilder htmlTable = new StringBuilder(); 
        //    htmlTable.Append("<table border='1'>");
        //    htmlTable.Append("<tr style='background-color:green; color: White;'><th>Taluka ID</th><th>Taluka Name</th><th>Menu List</th></tr>");
        //    for(int i=0;i<mDicMenuList.Count;i++)
        //    {
        //             htmlTable.Append("<tr style='color: red;'>");
        //             htmlTable.Append("<td>" + intTalukaId + "</td>");
        //             htmlTable.Append("<td>" + strTalukaNm + "</td>");
        //             htmlTable.Append("<td>" + Convert.ToString(mDicMenuList["MenuName"]) + "</td>");
        //             htmlTable.Append("</tr>");
        //    }
        //    htmlTable.Append("</table>");
        //    DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });  
        //}
        //private Dictionary<string, object> GetPageLists()
        //{
        //    Dictionary<string, object> mDicSelectedMenus = new Dictionary<string, object>();
        //    mDicSelectedMenus["MenuId"] = -1;
        //    mDicSelectedMenus["MenuName"] = "";

        //    foreach (GridViewRow mGvrPermissionDetail in grdUserPermission.Rows)
        //    {
        //        //if (((CheckBox)mGvrPermissionDetail.FindControl("cbSelectPage")).Checked)
        //        //{
        //        //    mDicSelectedMenus.Add(Convert.ToInt32(mGvrPermissionDetail.Cells[0].Text));
        //        //    mDicSelectedMenus.Add(mGvrPermissionDetail.Cells[2].Text);
        //        //}
        //    }
        //    return mDicSelectedMenus;
        //}
        //private string ValidateUser()
        //    {
        //        string mstrValidate = "";

        //            if (txtuserid.Text=="")
        //                {
        //                    mstrValidate =mstrValidate + "UserId Cannot be Blank !!!";
        //                }
        //            if (txtpwd.Text=="")
        //                {
        //                    mstrValidate = mstrValidate + "Password Cannot be Blank !!!";
        //                }
        //            if (!CheckUserExists(txtuserid.Text))
        //                {
        //                    mstrValidate = mstrValidate + "User Id Already Exists !!!";
        //                }
        //            return mstrValidate;
        //    }
        //private string ValidateUser(string UserID,string Password,long lngID)
        //    {
        //        string mstrValidate = "";
        //        if (UserID == "")
        //                {
        //                    mstrValidate = mstrValidate + "UserId Cannot be Blank !!!";
        //                }
        //        if (Password == "")
        //                {
        //                    mstrValidate = mstrValidate + "Password Cannot be Blank !!!";
        //                }
        //        if (!CheckUserExists(UserID, lngID))
        //                {
        //                    mstrValidate = mstrValidate + "User Id Already Exists !!!";
        //                }
        //        return mstrValidate;
        //    }
        //public bool CheckUserExists(string strUserID)
        //    {
        //        try
        //            {
        //                string mstrGetUser = "";
        //               // mstrGetUser = "SELECT * from User_Master where   UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " And UM_vCharUserId ='" + txtuserid.Text + "'";
        //                mstrGetUser = "SELECT * from User_Master where   UM_CompId =@comp_id And UM_vCharUserId =@u_id";
        //                DataTable dtGetUserDetails;
        //                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false, SqlHelper.AddInParam("@comp_id", SqlDbType.BigInt, Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId)), SqlHelper.AddInParam("@u_id", SqlDbType.VarChar, strUserID));
        //                if (dtGetUserDetails.Rows.Count > 0)
        //                    {
        //                        return false;
        //                    }
        //                return true;
        //            }
        //        catch (Exception exError)
        //             {
        //                 long pLngErr = -1;
        //                 if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //                     pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //                 pLngErr = GlobalFunctions.ReportError("CheckUserExists", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

        //                 actionInfo.Attributes["class"] = "alert alert-info blink-border";
        //                 actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
        //                 return true;
        //            }
        //    }
        //public bool CheckUserExists(string strUserID,long lngID )
        //    {
        //        try
        //            {
        //                string mstrGetUser = "";
        //                // mstrGetUser = "SELECT * from User_Master where   UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " And UM_vCharUserId ='" + txtuserid.Text + "'";
        //                mstrGetUser = "SELECT * from User_Master where   UM_CompId =@comp_id And UM_vCharUserId = @u_id and  UM_bIntId <> @id";
        //                DataTable dtGetUserDetails;
        //                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false, SqlHelper.AddInParam("@comp_id", SqlDbType.BigInt, Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId)), SqlHelper.AddInParam("@u_id", SqlDbType.VarChar, strUserID), SqlHelper.AddInParam("@id", SqlDbType.BigInt, lngID));
        //                if (dtGetUserDetails.Rows.Count > 0)
        //                    {
        //                        return false;
        //                    }
        //                return true;
        //            }
        //        catch (Exception exError)
        //            {
        //                long pLngErr = -1;
        //                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //                    pLngErr = GlobalFunctions.ReportError("CheckUserExists", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
        //                    actionInfo.Attributes["class"] = "alert alert-info blink-border";
        //                    actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
        //                return true;
        //            }
        //    }

        public void FillClientGrid()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT * from  Com_Client where bIsActive = 1";
                //mstrGetUser = "SELECT UM_bIntId,UM_vCharName,UM_vCharUserId,UM_vCharDesignation," + GlobalFunctions.CreateDecryptTextSyntax("UM_vCharPassword", true) + " As UM_vCharPassword  from  User_Master where  UM_CompId =" + Convert.ToInt32(((SysCompany)Session["SystemCompany"]).CompanyId) + " and UM_bItIsActive = 1";
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                grdUser.DataSource = dtGetUserDetails;
                grdUser.DataBind();
                
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillClientGrid", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        //protected void grdUser_RowEditing(object sender, GridViewEditEventArgs e)
        //    {
        //        grdUser.EditIndex = e.NewEditIndex;
        //        FillClientGrid();
        //    }
        //protected void grdUser_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //    {
        //        grdUser.EditIndex = -1;
        //        FillClientGrid();
        //    }
        //protected void grdUser_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //    {
        //        try 
        //            {
        //                TextBox txtUserName = (TextBox)grdUser.Rows[e.RowIndex].FindControl("txtUserName");
        //                TextBox txtUserID = (TextBox)grdUser.Rows[e.RowIndex].FindControl("txtUserID");
        //                TextBox txtDesignation = (TextBox)grdUser.Rows[e.RowIndex].FindControl("txtDesignation");
        //                TextBox txtUserPassword = (TextBox)grdUser.Rows[e.RowIndex].FindControl("txtUserPassword");
        //                Label lblID = (Label)grdUser.Rows[e.RowIndex].FindControl("lblID");
        //                string strValidate = "";
        //                strValidate = ValidateUser(txtUserID.Text, txtUserPassword.Text,Convert.ToInt64(lblID.Text));
        //                if (strValidate == "")
        //                    {
        //                        string strquery = "Update User_Master set UM_vCharName=@u_name,UM_vCharUserId=@u_id,UM_vCharDesignation=@u_desg,UM_vCharPassword= " + GlobalFunctions.CreateEncryptTextSyntax("@u_pass", true, false) + " where UM_bIntId=@id";
        //                        SqlHelper.UpdateDatabase(strquery, SqlHelper.AddInParam("@u_id", SqlDbType.VarChar, txtUserID.Text.Trim()), SqlHelper.AddInParam("@u_name", SqlDbType.VarChar, txtUserName.Text.Trim()), SqlHelper.AddInParam("@u_desg", SqlDbType.VarChar, txtDesignation.Text.Trim()), SqlHelper.AddInParam("@u_pass", SqlDbType.VarChar, txtUserPassword.Text.Trim()), SqlHelper.AddInParam("@id", SqlDbType.BigInt,Convert.ToInt64(lblID.Text)));
        //                        SetProductsUpdateMessage(false, "User Updated Successfully");
        //                        grdUser.EditIndex = -1;
        //                        FillClientGrid();
        //                    }
        //                else
        //                    {
        //                        SetProductsUpdateMessage(false, strValidate);
        //                    }
        //            }
        //        catch(Exception exError)
        //            {
        //                long pLngErr = -1;
        //                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //                pLngErr = GlobalFunctions.ReportError("grdUser_RowUpdating", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

        //                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
        //                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";

        //            }
        //    }

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
            dllclientType.Enabled = pBoolState;
            txtcon_pername.Enabled = pBoolState;
            txtphnno.Enabled = pBoolState;
            txtphnno2.Enabled = pBoolState;
            txtmob.Enabled = pBoolState;
            txtEmail.Enabled = pBoolState;
            txtcompnm.Enabled = pBoolState;
            txtaddress.Enabled = pBoolState;
            txtaddress22.Enabled = pBoolState;
            txtaddress3.Enabled = pBoolState;
            txtaddress2.Enabled = pBoolState;
            pincode.Enabled = pBoolState;
            Country.Enabled = pBoolState;
            State.Enabled = pBoolState;
            City.Enabled = pBoolState;
            listbox_comm_dealt.Enabled = pBoolState;
            txtGST.Enabled = pBoolState;
            txtAGSTPath.Enabled = pBoolState;
            txtComRegNum.Enabled = pBoolState;
            txtAROCPath.Enabled = pBoolState;
            ddl_warehouse.Enabled = pBoolState;
            txtwarehouseLoc.Enabled = pBoolState;
            //ddl_Role.Enabled = pBoolState;
        }

        public void ClearControls()
        {
            //ddl_Type.SelectedIndex = 0;
            txtcon_pername.Text = "";
            txtphnno.Text = "";
            txtphnno2.Text = "";
            txtmob.Text = "";
            txtEmail.Text = "";
            txtcompnm.Text = "";
            txtaddress.Text = "";
            txtaddress22.Text = "";
            txtaddress3.Text = "";
            txtaddress2.Text = "";
            pincode.Text = "";
            Country.Text = "";
            State.Text = "";
            City.Text = "";
            listbox_comm_dealt.SelectedIndex = 0;
            dllclientType.SelectedIndex = 0;
            txtGST.Text = "";
            //txtqty_avlbl.Text = "";
            txtAGSTPath.Text = "";
            txtComRegNum.Text = "";
            txtAROCPath.Text = "";
            ddl_warehouse.SelectedIndex = 0;
            txtwarehouseLoc.Text = "";
            //ddl_Role.SelectedIndex = 0;
        }
        // created by hardik (2022_11_11)
        protected void btnDeleteClient_ServerClick(object sender, EventArgs e)
        {
            //string strquery = "delete from  Com_Client  where iClientId=@id";
            DataTable dtCatData = SqlHelper.ReadTable("spDeleteClient", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                SqlHelper.AddInParam("@id", SqlDbType.Int, Convert.ToInt32(txtDelHidden.Value)));
            SetProductsUpdateMessage(false, "Admin Deleted Successfully");
            txtDelHidden.Value = "";
            txtDelClientID.Text = "";
            txtDelClientName.Text = "";
            grdUser.Columns[16].Visible = false;
            grdUser.Columns[21].Visible = false;
            grdUser.EditIndex = -1;
            FillClientGrid();
              
        }

        // created by hardik (2022_11_13)
        protected void grdUser_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdUser.EditIndex = e.NewEditIndex;
            ViewState["RowVal"] = e.NewEditIndex;
            FillClientGrid();
            //FillCountry();
            //BindGrid();
            grdUser.Columns[16].Visible = true;
            grdUser.Columns[21].Visible = true;
          

        }

        // created by hardik (2022_11_15)
        //protected void grdUser_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    try
        //    {
        //        //DropDownList ClientTypes = (DropDownList)grdUser.Rows[e.RowIndex].FindControl("EditClientType");
        //        string ClientTypes = (grdUser.Rows[e.RowIndex].FindControl("EditClientType") as DropDownList).SelectedItem.Text;
        //        TextBox username = (TextBox)grdUser.Rows[e.RowIndex].FindControl("txtUserName");
        //        TextBox usernumber = (TextBox)grdUser.Rows[e.RowIndex].FindControl("txtUserNumber");
        //        TextBox usernumber2 = (TextBox)grdUser.Rows[e.RowIndex].FindControl("txtUserNumber2");
        //        TextBox mobilenum = (TextBox)grdUser.Rows[e.RowIndex].FindControl("txtUserMobNumber");
        //        TextBox Email = (TextBox)grdUser.Rows[e.RowIndex].FindControl("edtEmail");
        //        TextBox companyName = (TextBox)grdUser.Rows[e.RowIndex].FindControl("Companyname");
        //        TextBox complocadd = (TextBox)grdUser.Rows[e.RowIndex].FindControl("edtComLocAdd");
        //        TextBox complocadd2 = (TextBox)grdUser.Rows[e.RowIndex].FindControl("edtComLocAdd2");
        //        DropDownList postalcode = (DropDownList)grdUser.Rows[e.RowIndex].FindControl("ddlpostalcode");
        //        DropDownList Companyaddress = (DropDownList)grdUser.Rows[e.RowIndex].FindControl("edtdllCountry");
        //        DropDownList Companyaddress2 = (DropDownList)grdUser.Rows[e.RowIndex].FindControl("edtdllState");
        //        DropDownList Companyaddress3 = (DropDownList)grdUser.Rows[e.RowIndex].FindControl("edtdllCity");
        //        //string Comoditydetails = (grdUser.Rows[e.RowIndex].FindControl("sCommodityDealtail") as DropDownList).SelectedItem.Value;
        //        Label Comoditydetails = (Label)grdUser.Rows[e.RowIndex].FindControl("edtsCommodityDealt");
        //        TextBox gstnumb = (TextBox)grdUser.Rows[e.RowIndex].FindControl("edtgstNum");
        //        TextBox comregnumb = (TextBox)grdUser.Rows[e.RowIndex].FindControl("edtcomregnum");
        //        HiddenField ImgOriginalCategory = (HiddenField)grdUser.Rows[e.RowIndex].FindControl("HiddenDoc1");
        //        HiddenField ImgOriginalCategory1 = (HiddenField)grdUser.Rows[e.RowIndex].FindControl("HiddenDoc2");
        //        DropDownList Warehouse = (DropDownList)grdUser.Rows[e.RowIndex].FindControl("edtddlsWarehouse");
        //        TextBox WareLoc = (TextBox)grdUser.Rows[e.RowIndex].FindControl("edtWarehouseLoc");
        //        string Roletype = (grdUser.Rows[e.RowIndex].FindControl("sRole") as DropDownList).SelectedItem.Text;
        //        Label lblID = (Label)grdUser.Rows[e.RowIndex].FindControl("lblID");

        //        string strErrorImg = "";
        //        string strError = "";


        //        string strValidate = "";
        //        strValidate = ValidateUser(ClientTypes, username.Text, usernumber.Text, usernumber2.Text, mobilenum.Text, Email.Text, companyName.Text,
        //           complocadd.Text,complocadd2.Text, postalcode.Text,  Companyaddress.Text, Companyaddress2.Text, Companyaddress3.Text, Comoditydetails.Text, gstnumb.Text, GlobalVariables.updateImage1,
        //         comregnumb.Text, GlobalVariables.updateImage2, Warehouse.Text, WareLoc.Text, Roletype);
        //        if (strValidate == "")
        //        {
        //            string data1 = GlobalVariables.updateImage1.Replace("//", "/");
        //            //string data1 = GlobalVariables.updateImage1 =  "" ?  GlobalVariables.updateImage1.Replace("//", "/");
        //            string data2 = GlobalVariables.updateImage2.Replace("//", "/");

        //            DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateClient", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
        //                 SqlHelper.AddInParam("@SClientType", SqlDbType.VarChar, ClientTypes),
        //                SqlHelper.AddInParam("@sContactPerson", SqlDbType.VarChar, username.Text),
        //                   SqlHelper.AddInParam("@sContactNumber", SqlDbType.VarChar, usernumber.Text),
        //                    SqlHelper.AddInParam("@sContactNumber2", SqlDbType.VarChar, usernumber2.Text),
        //                     SqlHelper.AddInParam("@sMobileNumber", SqlDbType.VarChar, mobilenum.Text),
        //                    SqlHelper.AddInParam("@sEmailid", SqlDbType.VarChar, Email.Text),
        //                   SqlHelper.AddInParam("@sCompany", SqlDbType.VarChar, companyName.Text),
        //                   SqlHelper.AddInParam("@sCompanyAdd", SqlDbType.VarChar, complocadd.Text),
        //                    SqlHelper.AddInParam("@sCompanyAdd2", SqlDbType.VarChar, complocadd2.Text),
        //                     SqlHelper.AddInParam("@sPostalCode", SqlDbType.VarChar, (postalcode.SelectedItem.Text)),
        //                   SqlHelper.AddInParam("@sComapnyAddress", SqlDbType.VarChar, (Companyaddress.SelectedItem.Text)),
        //                    SqlHelper.AddInParam("@sComapnyAddress2", SqlDbType.VarChar, Companyaddress2.SelectedItem.Text),
        //                     SqlHelper.AddInParam("@sComapnyAddress3", SqlDbType.VarChar, Companyaddress3.SelectedItem.Text),
        //                   SqlHelper.AddInParam("@sCommodityDealt", SqlDbType.VarChar, Comoditydetails.Text),
        //                     SqlHelper.AddInParam("@sGSTnumber", SqlDbType.VarChar, gstnumb.Text),
        //                      SqlHelper.AddInParam("@sGstUplaodDoc", SqlDbType.VarChar, data1),
        //                     SqlHelper.AddInParam("@sComRegNum", SqlDbType.VarChar, comregnumb.Text),
        //                   SqlHelper.AddInParam("@sRocUploadDoc", SqlDbType.VarChar, data2),
        //                   SqlHelper.AddInParam("@sWarehouse", SqlDbType.VarChar, Warehouse.SelectedItem.Text),
        //                    SqlHelper.AddInParam("@sWarehouseLoc", SqlDbType.VarChar, WareLoc.Text),
        //                   SqlHelper.AddInParam("@sRole", SqlDbType.VarChar, Roletype),
        //                  SqlHelper.AddInParam("@iClientId", SqlDbType.Int, lblID.Text));


        //            SetProductsUpdateMessage(false, "USER Master Updated Successfully");
        //            grdUser.EditIndex = -1;
        //            FillClientGrid();
        //            ClearControls();
        //            grdUser.Columns[16].Visible = false;
        //            grdUser.Columns[21].Visible = false;
        //        }
        //        else
        //        {
        //            SetProductsUpdateMessage(false, strValidate);
        //        }

        //    }
        //    catch (Exception exError)
        //    {
        //        long pLngErr = -1;
        //        if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //            pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //        pLngErr = GlobalFunctions.ReportError("GridView1_RowUpdating1", "UserMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
        //        updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
        //        updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
        //    }
        //}

        // created by hardik (2022_11_15)
        protected void grdUser_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdUser.EditIndex = -1;
            //grdUser.Columns[13].Visible = false;
            //grdUser.Columns[17].Visible = false;
            grdUser.Columns[16].Visible = false;
            grdUser.Columns[21].Visible = false;
            FillClientGrid();
            ClearControls();

        }
        // created by hardik (2022_11_15)

        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                    {                        
                            string strConn = Convert.ToString(Session["SystemUserSqlConnectionString"]);
                            string qGetCountries = "select Countryname,Countryid from Countries";
                            DataTable dtActiveCountry = SqlHelper.ReadTable(qGetCountries, strConn, false);

                            DropDownList edtdllCountryObj = (DropDownList)e.Row.FindControl("edtdllCountry");
                            edtdllCountryObj.DataSource = dtActiveCountry;
                            edtdllCountryObj.DataValueField = "Countryid";
                            edtdllCountryObj.DataTextField = "Countryname";
                            edtdllCountryObj.DataBind();
                            edtdllCountryObj.Items.Insert(0, new ListItem("-- Select Country --", "0"));

                            Label lblCountryForEdit = (Label)e.Row.FindControl("CountryForEdit");
                            //drdlObject.Visible = true;
                            //lblObject.Visible = false;

                            edtdllCountryObj.Items.FindByText(lblCountryForEdit.Text).Selected = true;
                        
                        //For State

                           // string strConn = Convert.ToString(Session["SystemUserSqlConnectionString"]);
                            //string qGetState = "select Statename,Stateid from Country_State";
                            string qGetState = "select Statename,Stateid from Country_State where Countryid='" +
                                                            edtdllCountryObj.SelectedValue + "'"; 
           
                            DataTable dtActiveState = SqlHelper.ReadTable(qGetState, strConn, false);

                            DropDownList edtdllStateObj = (DropDownList)e.Row.FindControl("edtdllState");
                            edtdllStateObj.DataSource = dtActiveState;
                            edtdllStateObj.DataValueField = "Stateid";
                            edtdllStateObj.DataTextField = "Statename";
                            edtdllStateObj.DataBind();
                            edtdllStateObj.Items.Insert(0, new ListItem("-- Select State --", "0"));

                            Label lbledtlblState = (Label)e.Row.FindControl("edtlblState");
                            //drdlObject.Visible = true;
                            //lblObject.Visible = false;

                            edtdllStateObj.Items.FindByText(lbledtlblState.Text).Selected = true;

                        //For City

                            // string strConn = Convert.ToString(Session["SystemUserSqlConnectionString"]);
                            string qGetCity = "select Cityname,Cityid from State_City where Stateid='" +
                                                            edtdllStateObj.SelectedValue + "'"; 
                            DataTable dtActiveCity = SqlHelper.ReadTable(qGetCity, strConn, false);

                            DropDownList edtdllCityObj = (DropDownList)e.Row.FindControl("edtdllCity");
                            edtdllCityObj.DataSource = dtActiveCity;
                            edtdllCityObj.DataValueField = "Cityid";
                            edtdllCityObj.DataTextField = "Cityname";
                            edtdllCityObj.DataBind();
                            edtdllCityObj.Items.Insert(0, new ListItem("-- Select City --", "0"));

                            Label lbledtlblCity = (Label)e.Row.FindControl("edtlblCity");
                            //drdlObject.Visible = true;
                            //lblObject.Visible = false;

                            edtdllCityObj.Items.FindByText(lbledtlblCity.Text).Selected = true;

                       //For Warehouse
                            DropDownList edtddlsWarehouseObj = (DropDownList)e.Row.FindControl("edtddlsWarehouse");
                            // string strConn = Convert.ToString(Session["SystemUserSqlConnectionString"]);
                            string qgetwarehousename = "select iStockId,sWarehouseName from Com_Stock";
                            DataTable dtActiveWarehouse = SqlHelper.ReadTable(qgetwarehousename, strConn, false);

                            edtddlsWarehouseObj.DataSource = dtActiveWarehouse;
                            edtddlsWarehouseObj.DataValueField = "iStockId";
                            edtddlsWarehouseObj.DataTextField = "sWarehouseName";
                            edtddlsWarehouseObj.DataBind();
                            edtddlsWarehouseObj.Items.Insert(0, new ListItem("-- Select Warehouse Name --", "0"));

                            Label lblsWarehouse = (Label)e.Row.FindControl("lblsWarehouse");
                            //drdlObject.Visible = true;
                            //lblObject.Visible = false;

                            edtddlsWarehouseObj.Items.FindByText(lblsWarehouse.Text).Selected = true;
                        
                    }
                    
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("grdTeam_RowDataBound", "TeamMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        // created by Jitu (2022_12_10)
        protected void btnSaveImgUpload(object sender, EventArgs e)
        {
            try
            {

                string strError;
                if (FileMainImage1.HasFile)
                // (FileMainImage1.PostedFile.ContentType == ".jpeg" ))
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
                    txtAGSTPath.Text = gstPath;
                    txtImgPathMain.Value = gstPath;

                    if (Convert.ToString(ViewState["RowVal"]) == "")
                    {
                        //strError = GlobalFunctions.ChkImageSize(Server.MapPath(txtImgPathMain.Value), 256, 256, 64, 64);
                        //if (strError == "")
                        //{
                        //insert condition code 
                        txtAGSTPath.Text = gstPath;
                        SetMessage(true, "Image Uploaded Successfully!!!");
                        //txtLogo.Text = "";
                        //}
                        //else {
                        //    File.Delete(Server.MapPath(txtImgPathMain.Value));
                        //    txtImgPathMain.Value = "";
                        //    txtAGSTPath.Text = "";
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
                    else
                    {
                        //update condition code
                        //HiddenField ImgCat = (HiddenField)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("imgPath");
                        //HiddenField LogoName = (HiddenField)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("LogoName");
                        //Image Img = (Image)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("ImgCat");
                        //  //strError = GlobalFunctions.ChkImageSize(Server.MapPath(txtImgPathMain.Value), 256, 256, 64, 64);
                        //  //if (strError == "")
                        //  //{
                        GlobalVariables.updateImage1 = txtImgPathMain.Value;


                        //Img.ImageUrl = "~/" + txtImgPathMain.Value;
                        //Img.Visible = true;
                        //LogoName.Value = "";

                        //ImgCat.Value = txtImgPathMain.Value;
                        ViewState["ImgLogo"] = "";

                        HtmlButton btnLogo = (HtmlButton)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow");
                        btnLogo.Visible = false;

                        ViewState["ImgPath"] = (GlobalVariables.UserImgPath) + "//" + gstPath;
                        SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");

                        //}
                        //else {
                        //    File.Delete(Server.MapPath(txtImgPathMain.Value));
                        //    txtImgPathMain.Value = "";
                        //    txtAGSTPath.Text = "";
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




                    //if (FileMainImage1.PostedFile.ContentType == ".pdf")
                    //     {
                    //         string path = Server.MapPath(".") + "\\" + FileMainImage1.FileName;
                    //         FileMainImage1.PostedFile.SaveAs(path);
                    //         SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");
                    //         StreamReader reader = new StreamReader(FileMainImage1.FileContent);
                    //         string text = reader.ReadToEnd();
                    //     }
                    //     else
                    //     {

                    //     }

                }
                if (FileUpload4.HasFile)
                {

                    //string ext = Path.GetExtension(FileUpload3.FileName).ToLower();
                    //string path = Server.MapPath(FileUpload3.PostedFile.FileName);
                    ////DataSet ds;
                    //FileUpload3.SaveAs(path);
                    //string ConStr = string.Empty;
                    //if (ext.Trim() == ".pdf")
                    //{

                    //}
                    //else
                    //{

                    //}
                    Stream strm = FileUpload4.PostedFile.InputStream;
                    //System.Drawing.Image img = compressImage(Convert.ToInt16(GlobalVariables.compressedImgQuality), strm);
                    string gstpath = GlobalFunctions.savepdf(GlobalVariables.UserImgPath, FileUpload4);
                    txtAGSTPath.Text = gstpath;
                    txtpdfpath.Value = gstpath;

                    if (Convert.ToString(ViewState["RowVal"]) == "")
                    {


                        txtAGSTPath.Text = gstpath;
                        SetMessage(true, "PDF Uploaded Successfully!!!");

                    }
                    else
                    {

                        GlobalVariables.updateImage2 = txtpdfpath.Value;

                        ViewState["ImgLogo1"] = "";

                        HtmlButton btnLogo = (HtmlButton)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow1");
                        btnLogo.Visible = false;

                        ViewState["ImgPath1"] = (GlobalVariables.UserImgPath) + "//" + gstpath;
                        SetProductsUpdateMessage(false, "PDF Uploaded Successfully!!!");

                    }

                }


                if (FileUpload2.HasFile)
                {
                    Stream strm = FileUpload2.PostedFile.InputStream;
                    System.Drawing.Image img = compressImage(Convert.ToInt16(GlobalVariables.compressedImgQuality), strm);
                    string arocPath = GlobalFunctions.saveImage(GlobalVariables.UserImgPath, FileUpload2, img);
                    txtAROCPath.Text = arocPath;
                    txtImgPathMain1.Value = arocPath;

                    if (Convert.ToString(ViewState["RowVal"]) == "")
                    {


                        txtAROCPath.Text = arocPath;
                        SetMessage(true, "Image Uploaded Successfully!!!");

                    }
                    else
                    {

                        GlobalVariables.updateImage2 = txtImgPathMain1.Value;

                        ViewState["ImgLogo1"] = "";

                        HtmlButton btnLogo = (HtmlButton)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow1");
                        btnLogo.Visible = false;

                        ViewState["ImgPath1"] = (GlobalVariables.UserImgPath) + "//" + arocPath;
                        SetProductsUpdateMessage(false, "Image Uploaded Successfully!!!");

                    }
                }
                 if (FileUpload3.HasFile)
                {
                    
                        //string ext = Path.GetExtension(FileUpload3.FileName).ToLower();
                        //string path = Server.MapPath(FileUpload3.PostedFile.FileName);
                        ////DataSet ds;
                        //FileUpload3.SaveAs(path);
                        //string ConStr = string.Empty;
                        //if (ext.Trim() == ".pdf")
                        //{

                        //}
                        //else
                        //{

                        //}
                    Stream strm = FileUpload3.PostedFile.InputStream;
                    //System.Drawing.Image img = compressImage(Convert.ToInt16(GlobalVariables.compressedImgQuality), strm);
                    string arocPath = GlobalFunctions.savepdf(GlobalVariables.UserImgPath, FileUpload3);
                    txtAROCPath.Text = arocPath;
                    txtpdfpath.Value = arocPath;

                    if (Convert.ToString(ViewState["RowVal"]) == "")
                    {


                        txtAROCPath.Text = arocPath;
                        SetMessage(true, "PDF Uploaded Successfully!!!");

                    }
                    else
                    {

                        GlobalVariables.updateImage2 = txtpdfpath.Value;

                        ViewState["ImgLogo1"] = "";

                        HtmlButton btnLogo = (HtmlButton)grdUser.Rows[Convert.ToInt32(ViewState["RowVal"])].FindControl("btnLogoShow1");
                        btnLogo.Visible = false;

                        ViewState["ImgPath1"] = (GlobalVariables.UserImgPath) + "//" + arocPath;
                        SetProductsUpdateMessage(false, "PDF Uploaded Successfully!!!");

                    }
                   
                }
                
            }
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

        // created by Jitu (2022_12_01)
        //private void FillCountry()
        //{
        //    try
        //    {
        
        //        string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
        //        DataTable dtList = SqlHelper.ReadTable("select Countryname,Countryid from Countries", conString, false);
        //        dllCountry.DataSource = dtList;
        //        dllCountry.Items.Clear();
        //        //dllCountry.Items.Add("--Please Select country--");
        //        dllCountry.DataValueField = "Countryid";
        //        dllCountry.DataTextField = "Countryname";
        //        dllCountry.DataBind();
        //        dllCountry.Items.Insert(0, new ListItem("-- Select Country --", "0"));
        //    }
        //    catch (Exception exError)
        //    {
        //        long pLngErr = -1;
        //        if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //            pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //        pLngErr = GlobalFunctions.ReportError("FillState", "FillDrdSprtCnt_Lst", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
        //        updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
        //        updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
        //    }
        //}

        // created by Jitu    
        //private void FillState()
        //{
        //    try
        //    {
        //        if (dllCountry.SelectedValue != "0")
        //        {
        //            //TalukaData objTal = (TalukaData)Session["TalukaDetails"];
        //            //int intTalukaId = objTal.TalukaID;
        //            //string strId = Convert.ToString(intTalukaId);
        //            string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
        //            //[DSCL_bIntId], 
        //            DataTable dtList = SqlHelper.ReadTable("select Statename,Stateid from Country_State where Countryid='" +
        //                                                    dllCountry.SelectedValue + "'", conString, false);
        //            //SqlHelper.AddInParam("@selectedCountry", SqlDbType.VarChar, dllCountry.SelectedValue));

        //            dllState.DataSource = dtList;

        //            dllState.DataValueField = "Stateid";
        //            dllState.DataTextField = "Statename";
        //            dllState.DataBind();
        //        }
        //        else
        //        {
        //            dllState.Items.Clear();
        //        }
        //        dllState.Items.Insert(0, new ListItem("-- Select State --", "0"));
        //    }
        //    catch (Exception exError)
        //    {
        //        long pLngErr = -1;
        //        if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //            pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //        pLngErr = GlobalFunctions.ReportError("FillCity", "FillCity", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
        //        updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
        //        updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
        //    }
        //}

        // created by Jitu
        //private void FillCity()
        //{
        //    try
        //    {
        //        if (dllState.SelectedValue != "0")
        //        {
       
        //            string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
        //            DataTable dtList = SqlHelper.ReadTable("select * from State_City where Stateid ='" + dllState.SelectedValue + "'", conString, false);            
        //            dllCity.DataSource = dtList;
        //            dllCity.DataValueField = "Cityid";
        //            dllCity.DataTextField = "Cityname";
        //            dllCity.DataBind();
        //        }
        //        else
        //        {
        //            dllState.Items.Clear();
        //        }
        //        dllCity.Items.Insert(0, new ListItem("-- Select City --", "0"));
        //    }
        //    catch (Exception exError)
        //    {
        //        long pLngErr = -1;
        //        if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //            pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //        pLngErr = GlobalFunctions.ReportError("FillCity", "FillCity", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
        //        updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
        //        updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
        //    }
        //}

        // created by Jitu
        //public void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillState();
        //}

        // created by Jitu
        //public void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillCity();
        //}

        // created by Jitu (2022-12-12)
        public void ddlCountryForEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //GridView grid = (GridView)((((Control)sender).Page.Items[grdUser]));
                GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;

                DropDownList selectcountry = (DropDownList)row.FindControl("edtdllCountry");
                DropDownList selectstate = (DropDownList)row.FindControl("edtdllState");
                if (selectcountry.SelectedValue != "0")
                {
                   
                    string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                    //[DSCL_bIntId], 
                    DataTable dtList = SqlHelper.ReadTable("select Statename,Stateid from Country_State where Countryid='" +
                                                            selectcountry.SelectedValue + "'", conString, false);
                    selectstate.DataSource = dtList;
                    selectstate.DataValueField = "Stateid";
                    selectstate.DataTextField = "Statename";
                    selectstate.DataBind();
                }
                else
                {
                    selectstate.Items.Clear();
                }
                selectstate.Items.Insert(0, new ListItem("-- Select State --", "0"));
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillCity", "FillCity", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
            }
        }

        // created by Jitu (2022-12-12)
        public void ddlStateForEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;

                DropDownList selectState = (DropDownList)row.FindControl("edtdllState");
                DropDownList selectCity = (DropDownList)row.FindControl("edtdllCity");
                if (selectState.SelectedValue != "0")
                {
                    //TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                    //int intTalukaId = objTal.TalukaID;
                    //string strId = Convert.ToString(intTalukaId);
                    string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                    //[DSCL_bIntId], 
                    DataTable dtList = SqlHelper.ReadTable("select * from State_City where Stateid ='" + selectState.SelectedValue + "'", conString, false);
                    //SqlHelper.AddInParam("@selectedState", SqlDbType.VarChar, dllState.SelectedValue));

                    selectCity.DataSource = dtList;
                    selectCity.DataValueField = "Cityid";
                    selectCity.DataTextField = "Cityname";
                    selectCity.DataBind();
                }
                else
                {
                    selectState.Items.Clear();
                }
                selectCity.Items.Insert(0, new ListItem("-- Select City --", "0"));
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillCity", "FillCity", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
            }
        }
        
        private void getCommodityName()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT iComID,sCommodityName FROM Com_Commodity  WHERE bIsActive=1";
                // SqlHelper.AddInParam("@ID", SqlDbType.VarChar, strId));
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                listbox_comm_dealt.DataSource = dtGetUserDetails;
                listbox_comm_dealt.DataTextField = "sCommodityName";
                listbox_comm_dealt.DataValueField = "iComID";
                listbox_comm_dealt.DataBind();
                //listbox_comm_dealt.Items.Insert(0, new ListItem("-- Select Comodity  Name --", "0"));
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

        // created by Jitu (2022-12-15)
        private void getWarehousename()
        {
            try
            {
                string mstrGetUser = "";
                mstrGetUser = "SELECT Ware_Id,Warehouse_Name FROM Warehouse_List";
                // SqlHelper.AddInParam("@ID", SqlDbType.VarChar, strId));
                DataTable dtGetUserDetails;
                dtGetUserDetails = SqlHelper.ReadTable(mstrGetUser, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                ddl_warehouse.DataSource = dtGetUserDetails;
                ddl_warehouse.DataTextField = "Warehouse_Name";
                ddl_warehouse.DataValueField = "Ware_Id";
                ddl_warehouse.DataBind();
                ddl_warehouse.Items.Insert(0, new ListItem("-- Select Warehouse Name --", "0"));
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

        protected void btnShowGallery_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int introw = Convert.ToInt32(btn.Attributes["RowIndex"]);

                Label lblOrgID = (Label)grdUser.Rows[introw].FindControl("lblID");

                //long lngOrgID = Convert.ToInt64(lblOrgID.Text);
                //txtModifyImgOrgid.Text = lngOrgID.ToString();

                //DataTable dtGetImages = SqlHelper.ReadTable("spDoveInsertUpdateDistriAttch", true, SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                //                        SqlHelper.AddInParam("@bIntImgId", SqlDbType.BigInt, lngOrgID));

                string query = "SELECT [iClientId]  ,[sGstUplaodDoc] ,[sRocUploadDoc] " +
                               "FROM [Com_Client] where iClientId  ='" + lblOrgID.Text + "'";
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
                            setImages(Convert.ToInt64(dtRowCat["iClientId"].ToString()), dtRowCat["sGstUplaodDoc"].ToString(), intcount);
                            GlobalVariables.updateImage1 = dtRowCat["sGstUplaodDoc"].ToString();
                        }
                        this.ClientScript.RegisterStartupScript(this.GetType(), "showGalleryModal", "ShowModal()", true);
                    }
                    else if (clickedButton.ID == "btnShowGallery2")
                    {
                        for (int intcount = 1; intcount <= dtGetImages.Rows.Count; intcount++)
                        {
                            DataRow dtRowCat = dtGetImages.Rows[intcount - 1];
                            GlobalVariables.updateImage2 = dtRowCat["sRocUploadDoc"].ToString();
                            setImages(Convert.ToInt64(dtRowCat["iClientId"].ToString()), dtRowCat["sRocUploadDoc"].ToString(), intcount);
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

        public void ClearDialougControl()
        {
            ImgCoverImage.ImageUrl = "";

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

        //public void droptotextbox(object sender, EventArgs e)
        //{
        //    if (ddlccode.SelectedIndex == 0)
        //    {
        //        txtmob.Enabled = false;
        //    }
        //    else
        //    {
        //        txtmob.Enabled = true;
        //        txtmob.Text = ddlccode.SelectedItem.Text.ToString();
        //    }
        //}

        //public void SetStatusinColummn()
        //{
        //    try
        //    {
        //        for (int i = 0; i< grdUser.Rows.Count - 1; i++)
        //        {
        //            if (grdUser.Rows[i].Cells[3].Text.ToString() == "")
        //            {
        //                grdUser.Rows[i].Visible = false;
        //            }
        //        }

        //    }
        //    catch
        //    {
 
        //    }
        //}

        protected void btnSubmit_click(object sender, EventArgs e)
        {

            //if ((txtOtp.Text == "") || (NewPassword.Text == ""))
            //{

            //    Div2.InnerHtml = "Fields Cannot be blank";
            //}

            //else
            //{

            //}
        }

    }
}

