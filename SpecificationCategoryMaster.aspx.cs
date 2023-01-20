using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace Admin_CommTrex
{
    public partial class SpecificationCategoryMaster : System.Web.UI.Page
    {

        public static int rowCount = 0;
        public static List<string> lstSubCategoryEnglish = new List<string>();
        public static List<string> lstSubCategoryRegional = new List<string>();

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
                    FillInformation(drpInfoselection);
                }
                else
                    Response.Redirect("Default.aspx"); // Session time out
            }
        }


        protected void showgrid()
        {
            System.Data.DataTable dtProdCat = new DataTable();
            DataTable dtCatData = SqlHelper.ReadTable("spGetSpecificationCatAndSubCat", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                  SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)));
            grdProductSpecification.DataSource = dtCatData;
            grdProductSpecification.DataBind();
        }

        public void FillInformation(DropDownList drpInformation)
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                DataTable dtCategoryList = SqlHelper.ReadTable("SELECT IM_vCharInfoName_En,IM_bIntInfoId FROM Information_Master_" + strId + "  WHERE IM_IsOrder=1  And IM_intInfoType=1", conString, false);

                drpInformation.DataSource = dtCategoryList;
                drpInformation.DataTextField = "IM_vCharInfoName_En";
                drpInformation.DataValueField = "IM_bIntInfoId";
                drpInformation.DataBind();
                drpInformation.Items.Insert(0, new ListItem("--Select Information Type--", "0"));
                drdCategoryType.Items.Insert(0, new ListItem("--Select Category Type--", "0"));
                drdSubCategoryType.Items.Insert(0, new ListItem("--Select Sub Category Type--", "0"));
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillInformation", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        public bool FillCategory(long infoID, DropDownList drpCategoryType)
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                DataTable dtCategoryList = SqlHelper.ReadTable("SELECT PC_bIntCategoryId,PC_vCharCatName FROM Product_Categories_" + strId + "  WHERE PC_bItIsActive = 1 And PC_bIntInformationId=" + infoID, conString, false);
                drpCategoryType.Items.Clear();
                drdSubCategoryType.Items.Clear();
                drdSubCategoryType.Items.Insert(0, new ListItem("--Select Sub Category Type--", "0"));

                if (dtCategoryList.Rows.Count > 0)
                {
                    drpCategoryType.DataSource = dtCategoryList;
                    drpCategoryType.DataTextField = "PC_vCharCatName";
                    drpCategoryType.DataValueField = "PC_bIntCategoryId";
                    drpCategoryType.DataBind();
                    drpCategoryType.Items.Insert(0, new ListItem("--Select Category Type--", "0"));
                    return true;
                }
                drpCategoryType.Items.Insert(0, new ListItem("--Select Category Type--", "0"));
                return false;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillCategory", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
                return false;
            }
        }

        public bool FillSubCategory(long infoID, long lngCatID, DropDownList drpSubCategoryType)
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                DataTable dtSubCategoryList;
                dtSubCategoryList = SqlHelper.ReadTable("spGetProductSubCategories", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                             SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                             SqlHelper.AddInParam("@bintInfoID", SqlDbType.BigInt, infoID),
                                             SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, lngCatID));
                drpSubCategoryType.Items.Clear();

                if (dtSubCategoryList.Rows.Count > 0)
                {
                    drpSubCategoryType.DataSource = dtSubCategoryList;

                    drpSubCategoryType.DataTextField = "PSC_vCharSubCatName";
                    drpSubCategoryType.DataValueField = "PSC_bIntSubCategoryId";
                    drpSubCategoryType.DataBind();
                    drpSubCategoryType.Items.Insert(0, new ListItem("--Select Sub Category Type--", "0"));
                    return true;
                }
                drpSubCategoryType.Items.Insert(0, new ListItem("--Select Sub Category Type--", "0"));
                return false;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillSubCategory", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
                return false;
            }
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            //Code for New button and Save button click on addition of new Category of Specification
            try
            {
                if (btnSave.Attributes["btn-action"] == "Save")
                {
                    //Code for saving specification details
                    string strValidate = Validate();
                    if (strValidate == "")
                    {
                        string strSpecificationEngCategoryName = txtCategoryName.Text;
                        string strSpecificationRegCategoryName = txtCategoryRegional.Text;
                        string intIsActiveindex = drdSpecificationCategoryIsActive.SelectedValue.ToString();
                        int intIsActive = Convert.ToInt32(intIsActiveindex);
                        long plngStatus = 0;
                        string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
                        long lngTalukaID = Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID);

                        if (lstSubCategoryEnglish.Count != 0)
                        {
                            string strQuery = "Select PSMC_bIntCategoryId from Product_Specifications_Category_Master_" + lngTalukaID + " Where PSMC_vCharCat_NameEn='" + txtCategoryName.Text + "' and PSMC_bIntProd_SubCatId = " + Convert.ToInt64(drdSubCategoryType.SelectedItem.Value);
                            DataTable dtSpecificationCategoryId;
                            dtSpecificationCategoryId = SqlHelper.ReadTable(strQuery, conString, false);
                            if (dtSpecificationCategoryId.Rows.Count > 0)
                            {
                                plngStatus = Convert.ToInt64(dtSpecificationCategoryId.Rows[0][0]);
                            }
                            else
                            {
                                plngStatus = 0;
                            }

                            //Store Specification Category Details
                            dtSpecificationCategoryId = SqlHelper.ReadTable("SP_insertSpecificationDetails", conString, true,
                                                       SqlHelper.AddInParam("@pintProductCatid", SqlDbType.BigInt, Convert.ToInt64(drdSubCategoryType.SelectedItem.Value)),
                                                       SqlHelper.AddInParam("@pstrCategoryNameEng", SqlDbType.VarChar, strSpecificationEngCategoryName),
                                                       SqlHelper.AddInParam("@pstrCategoryNameReg", SqlDbType.NVarChar, strSpecificationRegCategoryName),
                                                       SqlHelper.AddInParam("@pbintTalukaID", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                       SqlHelper.AddInParam("@pbitIsActive", SqlDbType.Bit, intIsActive),
                                                       SqlHelper.AddInParam("@pintAmtid", SqlDbType.BigInt, plngStatus));

                            long mLngSpecificationCategoryId;
                            long plngSpecSubCatID = 0;
                            bool blnSpecificationSubCatFlag = false;
                            string strMessage = "";
                            if (dtSpecificationCategoryId.Rows.Count > 0)
                            {
                                mLngSpecificationCategoryId = Convert.ToInt64(dtSpecificationCategoryId.Rows[0][0]);
                                //Store Specification SubCategory Details
                                for (int i = 0; i < lstSubCategoryEnglish.Count; i++)
                                {
                                    if (lstSubCategoryEnglish[i] != null) strSpecificationEngCategoryName = lstSubCategoryEnglish[i].ToString();
                                    if (lstSubCategoryRegional[i] != null) strSpecificationRegCategoryName = lstSubCategoryRegional[i].ToString();
                                    plngSpecSubCatID = 0;
                                    if (plngStatus != 0)
                                    {
                                        strQuery = "Select PSSCM_bIntSubCatId from Product_Specifications_SubCategory_Master_" + lngTalukaID + " Where PSSCM_bIntSpec_CatId=" + plngStatus + " and PSSCM_vCharSubCat_NameEn = '" + strSpecificationEngCategoryName + "'";
                                        dtSpecificationCategoryId = SqlHelper.ReadTable(strQuery, conString, false);
                                        if (dtSpecificationCategoryId.Rows.Count > 0)
                                        {
                                            plngSpecSubCatID = Convert.ToInt64(dtSpecificationCategoryId.Rows[0][0]);
                                        }
                                        else
                                        {
                                            plngSpecSubCatID = 0;
                                        }

                                    }

                                    if (plngSpecSubCatID == 0)
                                    {
                                        blnSpecificationSubCatFlag = true;
                                        dtSpecificationCategoryId = SqlHelper.ReadTable("SP_insertSpecificationSubcatDetails", conString, true,
                                                                SqlHelper.AddInParam("@pintCategoryMstid", SqlDbType.BigInt, Convert.ToInt32(mLngSpecificationCategoryId)),
                                                                SqlHelper.AddInParam("@pstrSubCategoryNameEng", SqlDbType.VarChar, strSpecificationEngCategoryName),
                                                                SqlHelper.AddInParam("@pstrSubCategoryNameReg", SqlDbType.NVarChar, strSpecificationRegCategoryName),
                                                                SqlHelper.AddInParam("@pbintTalukaID", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                                SqlHelper.AddInParam("@pbitIsActive", SqlDbType.Bit, intIsActive),
                                                                SqlHelper.AddInParam("@pintAmtid", SqlDbType.BigInt, plngStatus));
                                    }
                                }

                                if (blnSpecificationSubCatFlag)
                                {
                                    strMessage = "Specifications for Product created  Successfully!!!";
                                }
                                else
                                {
                                    strMessage = "Specifications Already Exists Successfully!!!";
                                }

                                mLngSpecificationCategoryId = Convert.ToInt64(dtSpecificationCategoryId.Rows[0][0]);
                                int intTalukaId = 0;
                                long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                                GlobalFunctions.saveInsertUserAction("Specification_Category_Master", "[Specification Category Master Insert]:Insertion of Specifications Categories on product with Id : " + Convert.ToInt32(mLngSpecificationCategoryId), intTalukaId, lngCompanyId, Request); //Call to user Action Log
                            }

                            grdProductSpecification.EditIndex = -1;
                            showgrid();

                            //Messagebox to display success in creation of subcategory
                            string message = strMessage;// "Specification for Product created successfully";
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append(" bootbox.alert('");
                            sb.Append(message);
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());
                            btnSave.Attributes["btn-action"] = "New";
                            btnSave.InnerHtml = "<i class=\"fa fa-plus-square\"></i> New";
                        }
                        else
                        {
                            string message = "Specificaiton Insertion Failed No SubCategory Specifications Selected!!!";
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
                        string message = strValidate;
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
                    drpInfoselection.Enabled = true;
                    SubcategoryAdd.Disabled = false;
                    drdCategoryType.Enabled = true;
                    drdSubCategoryType.Enabled = true;
                    txtCategoryName.Enabled = true;
                    txtCategoryRegional.Enabled = true;
                    drdSpecificationCategoryIsActive.Enabled = true;
                    rowCount = 0;
                    lstSubCategoryEnglish.Clear();
                    lstSubCategoryRegional.Clear();
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSave_ServerClick", "SpecificationCategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        private void SetMessage(bool pBlnIsError, string pStrError)
        {
            actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            actionInfo.InnerHtml = pStrError;
        }

        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            txtCategoryName.Text = "";
            txtCategoryRegional.Text = "";
            drdCategoryType.SelectedIndex = 0;
            drdSubCategoryType.SelectedIndex = 0;
            drpInfoselection.SelectedIndex = 0;
            drdSpecificationCategoryIsActive.SelectedIndex = 0;
            lstSubCategoryEnglish.Clear();
            lstSubCategoryRegional.Clear();
        }

        protected void btnSaveSpecificationSubCategory_ServerClick(object sender, EventArgs e)
        {
            try
            {
                //Code for saving Specification SubCategory Master    
                StringBuilder htmlTable = new StringBuilder();
                if (txtSpecificationSubcategoryEng.Text.Trim() != "" || txtSpecificationSubcategoryReg.Text.Trim() != "")
                {
                    string strSubCatSpecificationEnglish = txtSpecificationSubcategoryEng.Text;
                    string strSubCatSpecificationRegional = txtSpecificationSubcategoryReg.Text;
                    rowCount++;
                    lstSubCategoryEnglish.Add(strSubCatSpecificationEnglish);
                    lstSubCategoryRegional.Add(strSubCatSpecificationRegional);
                }

                htmlTable.Append("<table class='table' >");
                htmlTable.Append("<tbody>");

                for (int i = 0; i < rowCount; i++)
                {
                    if (i == 0)
                    {
                        htmlTable.Append("<tr><th>SubCategory Name (English)</th><th>SubCategory Name (Regional)</th></tr>");
                    }

                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td>" + lstSubCategoryEnglish[i].ToString() + "</td>");
                    htmlTable.Append("<td>" + lstSubCategoryRegional[i].ToString() + "</td>");
                    // htmlTable.Append("<td><a id='editSubCatRow' class='editSubCatRow' runat='server'  data-subcateng-name='<%" + lstSubCategoryEnglish[i].ToString() + " %>' data-subcatreg-name='<%" + lstSubCategoryRegional[i].ToString() + " %>'>Edit</a></td>");
                    htmlTable.Append("</tr>");
                }
                htmlTable.Append("</tbody></table>");
                DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });

                txtSpecificationSubcategoryEng.Text = "";
                txtSpecificationSubcategoryReg.Text = "";
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSaveSpecificationSubCategory", "SpecificationCategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
            #region codeforSubcategorysave
            //foreach (Control ctl in tpTableRows.Controls)
            //{
            //    if (ctl is TextBox)
            //    {
            //        TextBox txt = ctl as TextBox;
            //        if (ctl != null)
            //        {
            //            string str=(txt.Text);
            //        }
            //    }
            //}

            //for (int i = 1; i <= rowCount; i++)
            //    {
            //        TextBox tbEnglish1 = (TextBox)tpTableRows.FindControl("txtSpecificationSubcategoryEng" + i.ToString());
            //        TextBox tbRegional1 = (TextBox)tpTableRows.FindControl("txtSpecificationSubcategoryReg" + i.ToString());
            //        strSubCatSpecificationEnglish = tbEnglish1.Text;
            //        strSubCatSpecificationRegional = tbRegional1.Text;
            //    }
            //    rowCount = 0;
            #endregion codeforSubcategorysave

        }

        protected void btnClearSpecificationSubCategory_ServerClick(object sender, EventArgs e)
        {
            StringBuilder htmlTable = new StringBuilder();
            //rowCount = 0;
            htmlTable.Append("<table class='table' >");
            htmlTable.Append("<tbody>");

            for (int i = 0; i < rowCount; i++)
            {
                if (i == 0)
                {
                    htmlTable.Append("<tr><th>SubCategory Name (English)</th><th>SubCategory Name (Regional)</th></tr>");
                }

                htmlTable.Append("<tr>");
                htmlTable.Append("<td>" + lstSubCategoryEnglish[i].ToString() + "</td>");
                htmlTable.Append("<td>" + lstSubCategoryRegional[i].ToString() + "</td>");
                // htmlTable.Append("<td><a id='editSubCatRow' class='editSubCatRow' runat='server'  data-subcateng-name='<%" + lstSubCategoryEnglish[i].ToString() + " %>' data-subcatreg-name='<%" + lstSubCategoryRegional[i].ToString() + " %>'>Edit</a></td>");
                htmlTable.Append("</tr>");
            }
            htmlTable.Append("</tbody></table>");
            DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });

            txtSpecificationSubcategoryEng.Text = "";
            txtSpecificationSubcategoryReg.Text = "";
        }

        protected void drdCategoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drdCategoryType.SelectedIndex != 0)
                {
                    FillSubCategory(Convert.ToInt64(drpInfoselection.SelectedItem.Value), Convert.ToInt64(drdCategoryType.SelectedItem.Value), drdSubCategoryType);
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("drpInformation_SelectedIndexChanged", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void grdProductSpecification_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Code to modify details of table
            Label lblSubCategoryid = (Label)grdProductSpecification.Rows[e.RowIndex].FindControl("lblSubCatMasterId");
            HiddenField hfcatid = (HiddenField)grdProductSpecification.Rows[e.RowIndex].FindControl("hfGridview");
            TextBox txtCatEngName_gridview = (TextBox)grdProductSpecification.Rows[e.RowIndex].FindControl("txtCategoryEngName");
            TextBox txtCatRegName_gridview = (TextBox)grdProductSpecification.Rows[e.RowIndex].FindControl("txtCategoryRegName");
            TextBox txtSubCatEngName_gridview = (TextBox)grdProductSpecification.Rows[e.RowIndex].FindControl("txtSubCategoryEngName");
            TextBox txtSubCatRegName_gridview = (TextBox)grdProductSpecification.Rows[e.RowIndex].FindControl("txtSubCategoryRegName");


            Label lblProductSubCatID_gridview = (Label)grdProductSpecification.Rows[e.RowIndex].FindControl("lblProductSubCatID");// ;
            int intSubCategoryId = Convert.ToInt32(lblSubCategoryid.Text); //SpecificationSubCategoryId
            int intCategoryId = Convert.ToInt32(hfcatid.Value);  //SpecificationCategoryId
            string strSpecificationEngCategoryName = txtCatEngName_gridview.Text;
            string strSpecificationRegCategoryName = txtCatRegName_gridview.Text;
            string strSpecificationEngSubCategoryName = txtSubCatEngName_gridview.Text;
            string strSpecificationRegSubCategoryName = txtSubCatRegName_gridview.Text;

            try
            {
                int intIsActive = 1;
                long plngStatus = intCategoryId;

                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]);

                //Store Specification Category Details
                DataTable dtSpecificationCategoryId = SqlHelper.ReadTable("SP_insertSpecificationDetails", conString, true,
                                            SqlHelper.AddInParam("@pintProductCatid", SqlDbType.BigInt, Convert.ToInt32(lblProductSubCatID_gridview.Text)),
                                            SqlHelper.AddInParam("@pstrCategoryNameEng", SqlDbType.VarChar, strSpecificationEngCategoryName),
                                            SqlHelper.AddInParam("@pstrCategoryNameReg", SqlDbType.NVarChar, strSpecificationRegCategoryName),
                                            SqlHelper.AddInParam("@pbintTalukaID", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                            SqlHelper.AddInParam("@pbitIsActive", SqlDbType.Bit, intIsActive),
                                            SqlHelper.AddInParam("@pintAmtid", SqlDbType.BigInt, plngStatus));

                long mLngSpecificationCategoryId;
                if (dtSpecificationCategoryId.Rows.Count > 0)
                {
                    mLngSpecificationCategoryId = Convert.ToInt64(dtSpecificationCategoryId.Rows[0][0]);
                    plngStatus = intSubCategoryId;

                    //Store Specification SubCategory Details
                    //for (int i = 0; i < lstSubCategoryEnglish.Count; i++)
                    //{
                    //    if (lstSubCategoryEnglish[i] != null) strSpecificationEngCategoryName = lstSubCategoryEnglish[i].ToString();
                    //    if (lstSubCategoryRegional[i] != null) strSpecificationRegCategoryName = lstSubCategoryRegional[i].ToString();

                    dtSpecificationCategoryId = SqlHelper.ReadTable("SP_insertSpecificationSubcatDetails", conString, true,
                                            SqlHelper.AddInParam("@pintCategoryMstid", SqlDbType.BigInt, Convert.ToInt32(mLngSpecificationCategoryId)),
                                            SqlHelper.AddInParam("@pstrSubCategoryNameEng", SqlDbType.VarChar, strSpecificationEngSubCategoryName),
                                            SqlHelper.AddInParam("@pstrSubCategoryNameReg", SqlDbType.NVarChar, strSpecificationRegSubCategoryName),
                                             SqlHelper.AddInParam("@pbintTalukaID", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                            SqlHelper.AddInParam("@pbitIsActive", SqlDbType.Bit, intIsActive),
                                            SqlHelper.AddInParam("@pintAmtid", SqlDbType.BigInt, plngStatus));
                    //}
                    mLngSpecificationCategoryId = Convert.ToInt64(dtSpecificationCategoryId.Rows[0][0]);
                    int lngTalukaId = 0;
                    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                    GlobalFunctions.saveInsertUserAction("Specification_Category_Master", "[Specification Category Master Update]:Updation of Specifications Categories on product with Id : " + Convert.ToInt32(mLngSpecificationCategoryId), lngTalukaId, lngCompanyId, Request); //Call to user Action Log

                }

                grdProductSpecification.EditIndex = -1;
                showgrid();

                //Messagebox to display success in creation of subcategory
                string message = "Specification for Product modified successfully";
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
                pLngErr = GlobalFunctions.ReportError("grdProductSpecification_RowUpdating", "SpecificationCategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }

        }

        protected void grdProductSpecification_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdProductSpecification.EditIndex = -1;
            showgrid();
        }

        protected void grdProductSpecification_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Code on click of button on row
        }

        protected void grdProductSpecification_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdProductSpecification.EditIndex = e.NewEditIndex;
            showgrid();
        }

        protected void btnDeleteProductSpecification_ServerClick(object sender, EventArgs e)
        {
            try
            {
                //Code for deletion of  product specification details
                Dictionary<string, string> mDicInputs = new Dictionary<string, string>();

                mDicInputs.Add("ProductID", txtDelProdIdHiden.Value);
                mDicInputs.Add("ProductReason", txtDeleteReason.Text);
                string mStrConString = Convert.ToString(Session["SystemUserSqlConnectionString"]);

                DataTable dtRemoveData = SqlHelper.ReadTable("RemoveProductSubSpecificationsDetails", mStrConString, true,
                                                           SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                          SqlHelper.AddInParam("@vbIntProductId", SqlDbType.BigInt, mDicInputs["ProductID"]),
                                                          SqlHelper.AddInParam("@vCharCloseReason", SqlDbType.VarChar, mDicInputs["ProductReason"]));
                showgrid();

                int intTalukaId = 0;
                long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                GlobalFunctions.saveInsertUserAction("Specification_Category_Master", "[Specification Category Master Delete]:Deletion of Specifications Categories on product with Id : " + Convert.ToInt32(txtDelProdIdHiden.Value), intTalukaId, lngCompanyId, Request); //Call to user Action Log


            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnDeleteProductSpecification_ServerClick", "SpecificationCategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        protected void showSubCat_ServerClick(object sender, EventArgs e)
        {
            //Code to view subcategory for specifications

            string mStrConString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
            DataTable dtSelectSubcat = SqlHelper.ReadTable("Select Distinct sscm.* From SpecificationCategoryMaster scm,dbo.SpecificationSubCategoryMaster sscm where sscm.SMSC_CategoryMstId=scm.SMC_CategoryMstId", mStrConString, false);
            if (dtSelectSubcat.Rows.Count > 0)
            {
                StringBuilder htmlTable = new StringBuilder();

                htmlTable.Append("<table class='table'><thead>");
                htmlTable.Append("<tr><th>#</th><th>Specification SubCategory Name</th><th>Specification SubCategory Name</th></tr> </thead>");
                htmlTable.Append("<tbody>");

                foreach (DataRow drSubCat in dtSelectSubcat.Rows)
                {
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td>" + drSubCat[0].ToString() + "</td>");
                    htmlTable.Append("<td><input type='text' id='' value='" + drSubCat[3].ToString() + "' /></td>");
                    htmlTable.Append("<td><input type='text' id='' value='" + drSubCat[4].ToString() + "' /></td>");
                    htmlTable.Append("</tr>");
                }

                htmlTable.Append("</tbody></table>");
                SubCatViewPlaceholder.Controls.Add(new Literal { Text = htmlTable.ToString() });
            }
        }

        protected void drpInfoselection_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                if (drpInfoselection.SelectedIndex != 0)
                {
                    FillCategory(Convert.ToInt64(drpInfoselection.SelectedItem.Value), drdCategoryType);
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("drpInformation_SelectedIndexChanged", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }
        protected string Validate()
        {
            string mstrValidate = "";
            if (Convert.ToInt64(drpInfoselection.SelectedItem.Value) == 0)
            {
                mstrValidate = "Select the Information Type to Continue";
                return mstrValidate;
            }
            if (Convert.ToInt64(drdCategoryType.SelectedItem.Value) == 0)
            {
                mstrValidate += "Select the Category to Continue";
                return mstrValidate;
            }
            if (Convert.ToInt64(drdSubCategoryType.SelectedItem.Value) == 0)
            {
                mstrValidate += "Select the Sub Category to Continue";
                return mstrValidate;
            }
            return mstrValidate;
        }
      

    }
}