using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Admin_CommTrex
{
    public partial class SpecificationValueMaster : System.Web.UI.Page
    {

        public static int intSubCatMstid, intProductId;
        public static Dictionary<string, string> dicSpecificationEnglishValue = new Dictionary<string, string>();
        public static Dictionary<string, string> dicSpecificationRegionalValue = new Dictionary<string, string>();


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
                    //showgrid();
                    //fillProductCategoryDetails(); //To fill Product Category
                    //fillProductNames(); //To fill Product names for View Specification combo
                    FillInformation(drpInfoselection);
                    fillProductNames();
                }
                else
                    Response.Redirect("Default.aspx"); // Session time out
            }
        }

        protected void fillProductCategoryDetails()
        {
            //Code to read product category details
            try
            {
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                DataTable dtCategoryList = SqlHelper.ReadTable("SELECT CM_bIntCatId,CM_vCharName_En FROM Category_Master  WHERE CM_bItIsActive = 1 AND CM_iNtEntryType=0", conString, false);

                drdCategoryType.DataSource = dtCategoryList;
                drdCategoryType.DataTextField = "CM_vCharName_En";
                drdCategoryType.DataValueField = "CM_bIntCatId";
                drdCategoryType.DataBind();
                drdCategoryType.Items.Insert(0, new ListItem("--Select Category--", "0"));
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("fillProductCategoryDetails", "SpecificationValueMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }

        }

        protected void fillProductSubCategoryDetails(string pstrCategoryName)
        {
            try
            {
                //Code to read product subcategory details
                string mStrConString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
                DataTable mDtTypes = SqlHelper.ReadTable("SELECT Distinct SCM_vCharName_En from Sub_Category_Master where SCM_bIntCatId=(Select CM_bIntCatId from Category_Master where CM_vCharName_En Like '" + pstrCategoryName + "')", mStrConString, false);

                if (mDtTypes.Rows.Count > 0)
                {
                    drdSubCategoryType.AppendDataBoundItems = true;
                    drdSubCategoryType.Items.Insert(0, new ListItem("Select Subcategory", "Select Subcategory"));
                    drdSubCategoryType.SelectedIndex = 0;

                    drdSubCategoryType.DataSource = mDtTypes;
                    drdSubCategoryType.DataTextField = "SCM_vCharName_En";
                    drdSubCategoryType.DataBind();
                }
                else
                {
                    drdSubCategoryType.Items.Insert(0, new ListItem("Select Subcategory", "Select Subcategory"));
                    drdSubCategoryType.SelectedIndex = 0;

                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("fillProductSubCategoryDetails", "SpecificationValueMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
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

        protected void drdSubCategoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            long lngSubCategoryID = Convert.ToInt64(drdSubCategoryType.SelectedItem.Value);
            drdProductName.Items.Clear();
            drdSpecification.Items.Clear();
            fillProductDetails(lngSubCategoryID);
            fillProductSpecificationCategoryDetails(lngSubCategoryID);
            grdProductSpecificationValue.Visible = false;
        }

        protected void fillProductSpecificationCategoryDetails(long lngSubCatID)
        {
            try
            {
                //Code to read Product Specification details
                string mStrConString = Convert.ToString(Session["SystemUserSqlConnectionString"]);

                DataTable mDtTypes = SqlHelper.ReadTable("spGetSpecification", mStrConString, true,
                                                        SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                        SqlHelper.AddInParam("@bintSubCatID", SqlDbType.BigInt, lngSubCatID));

                if (mDtTypes.Rows.Count > 0)
                {
                    drdSpecification.AppendDataBoundItems = true;
                    drdSpecification.Items.Insert(0, new ListItem("Select Product Specification", "0"));
                    drdSpecification.SelectedIndex = 0;

                    drdSpecification.DataSource = mDtTypes;
                    drdSpecification.DataTextField = "PSMC_vCharCat_NameEn";
                    drdSpecification.DataValueField = "PSMC_bIntCategoryId";
                    drdSpecification.DataBind();
                }
                else
                {
                    drdSpecification.Items.Insert(0, new ListItem("Select Product Specification", "0"));
                    drdSpecification.SelectedIndex = 0;

                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("fillProductSpecificationCategoryDetails", "SpecificationValueMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        protected void fillProductDetails(long lngSubCatID)
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                DataTable dtProdList;
                dtProdList = SqlHelper.ReadTable("spGetProducts", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                             SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                             SqlHelper.AddInParam("@bintSubCatID", SqlDbType.BigInt, lngSubCatID));


                if (dtProdList.Rows.Count > 0)
                {
                    drdProductName.DataSource = dtProdList;

                    drdProductName.DataTextField = "PM_vCharProdName";
                    drdProductName.DataValueField = "PM_bIntProdId";
                    drdProductName.DataBind();
                    drdProductName.Items.Insert(0, new ListItem("--Select Products--", "0"));

                }

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("fillProductDetails", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";

            }
        }

        protected void fillProductNames()
        {
            try
            {
                //Code to read Product details
                string mStrConString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
                drProductViewSpecification.Items.Clear();
                //string strSelectQuery = "SELECT DISTINCT pm.PM_bIntProdId,pm.PM_vCharProdName";
                //strSelectQuery = strSelectQuery + "  from dbo.SpecificationValueMaster svm ";
                //strSelectQuery = strSelectQuery + "  inner join dbo.ProductMaster pm ";
                //strSelectQuery = strSelectQuery + "  on pm.PM_bIntProdId=svm.SVM_Product_Id ";
                //strSelectQuery = strSelectQuery + "  and svm.SVM_IsActive=1 ";

                DataTable mDtTypes = SqlHelper.ReadTable("SP_ReadSpecifiedProducts", mStrConString, true, SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)));

                if (mDtTypes.Rows.Count > 0)
                {
                    drProductViewSpecification.AppendDataBoundItems = true;
                    drProductViewSpecification.Items.Insert(0, new ListItem("Select Product", "0"));
                    drProductViewSpecification.SelectedIndex = 0;

                    drProductViewSpecification.DataSource = mDtTypes;
                    drProductViewSpecification.DataTextField = "PM_vCharProdName";
                    drProductViewSpecification.DataValueField = "PM_bIntProdId";
                    drProductViewSpecification.DataBind();
                    //hfProductViewId.Value = mDtTypes.Rows[0]["PM_bIntProdId"].ToString();
                }
                else
                {
                    drProductViewSpecification.Items.Insert(0, new ListItem("Select Product", "0"));
                    drProductViewSpecification.SelectedIndex = 0;

                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("fillProductNames", "SpecificationValueMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            //Code for save of specification value
            //-----------------------------------------------
            int intupdatedrowCount = dicSpecificationEnglishValue.Count;
            int intIsActive = 1;
            string mStrConString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
            try
            {
                if (intupdatedrowCount > 0)
                {
                    DataTable dtproductSpecification = null;
                    for (int i = 0; i < intupdatedrowCount; i++)
                    {

                        string SubCatEngKey = Convert.ToString(dicSpecificationEnglishValue.ElementAt(i).Key);
                        string SubCatRegKey = Convert.ToString(dicSpecificationRegionalValue.ElementAt(i).Key);

                        string strSubCatEngName = dicSpecificationEnglishValue[SubCatEngKey].ToString();

                        int intIsModify = 0;
                        DataTable dtgetSpecificationValid = SqlHelper.ReadTable("Select SVM_ID From SpecificationValueMaster where SVM_SubCategoryEngValue Like '" + strSubCatEngName + "'", mStrConString, false);
                        if (dtgetSpecificationValid.Rows.Count > 0)
                            intIsModify = Convert.ToInt32(dtgetSpecificationValid.Rows[0][0]);

                        //code to insert or update specification values
                        dtproductSpecification = SqlHelper.ReadTable("SP_insertSpecificationValue", mStrConString, true,
                             SqlHelper.AddInParam("@pintProductid", SqlDbType.BigInt, intProductId),
                             SqlHelper.AddInParam("@pintSubCatid", SqlDbType.BigInt, intSubCatMstid),
                             SqlHelper.AddInParam("@pvcharSubCatEng", SqlDbType.VarChar, dicSpecificationEnglishValue[SubCatEngKey].ToString()),
                             SqlHelper.AddInParam("@pnvcharSubCatReg", SqlDbType.NVarChar, dicSpecificationEnglishValue[SubCatRegKey].ToString()),
                             SqlHelper.AddInParam("@pintisActive", SqlDbType.Bit, intIsActive),
                             SqlHelper.AddInParam("@pintIsModify", SqlDbType.BigInt, intIsModify));
                    }

                    if (dtproductSpecification.Rows.Count > 0)
                    {
                        int intTalukaId = 0;
                        long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                        GlobalFunctions.saveInsertUserAction("Specification_Value_Master", "[Specification Value Master Insert]:Insertion of Specifications details on product with Id : " + Convert.ToInt32(intProductId), intTalukaId, lngCompanyId, Request); //Call to user Action Log

                        //Code on updating tables
                        grdProductSpecificationValue.EditIndex = -1;
                        fillProductSpecificationDetails(Convert.ToInt64(drdProductName.SelectedItem.Value), Convert.ToInt64((drdSpecification.SelectedItem.Value)));  //Code for generating product specification details

                    }
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSave_ServerClick", "SpecificationValueMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }

            //-----------------------------------------------
        }

        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            //Code for clear the specification value
            drdCategoryType.SelectedIndex = -1;
            drdSubCategoryType.SelectedIndex = -1;
            drdProductName.SelectedIndex = -1;
            drdSpecification.SelectedIndex = -1;
            //drdSpecificationCategoryIsActive.Enabled = false;
        }

        protected void drdSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Code for specification types
            try
            {
                if (Convert.ToInt32(drdCategoryType.SelectedItem.Value) != 0 && Convert.ToInt32(drdSubCategoryType.SelectedItem.Value) != 0 && Convert.ToInt32(drdProductName.SelectedItem.Value) != 0 && Convert.ToInt32(drpInfoselection.SelectedItem.Value) != 0 && Convert.ToInt32(drdSpecification.SelectedItem.Value) != 0 && Convert.ToInt64(drdProductName.SelectedItem.Value) != 0)
                {
                    fillProductSpecificationDetails(Convert.ToInt64(drdProductName.SelectedItem.Value), Convert.ToInt64((drdSpecification.SelectedItem.Value)));  //Code for generating product specification details
                }
                //drdSpecificationCategoryIsActive.Enabled = true;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("drdSpecification_SelectedIndexChanged", "SpecificationValueMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        protected void fillProductSpecificationDetails(long ProdID, long SpecificationID)
        {
            try
            {
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
                string strq = "Select PSDM_bIntProductId from   Product_Specifications_Details_Master_" + Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID) + " Where PSDM_bIntProductId = " + ProdID;
                DataTable dtselectSpecificationDetails = SqlHelper.ReadTable(strq, conString, false);

                if (dtselectSpecificationDetails.Rows.Count > 0)
                {
                    ProdID = Convert.ToInt64(dtselectSpecificationDetails.Rows[0][0].ToString());
                }
                else
                {
                    ProdID = 0;
                }

                dtselectSpecificationDetails = SqlHelper.ReadTable("SP_GetSpecificationDetails", conString, true,
                   SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                   SqlHelper.AddInParam("@bIntProductId", SqlDbType.BigInt, ProdID),
                   SqlHelper.AddInParam("@bIntSpecificationId", SqlDbType.BigInt, SpecificationID),
                   SqlHelper.AddInParam("@bIntSpecificationExists", SqlDbType.BigInt, ProdID));

                if (dtselectSpecificationDetails.Rows.Count > 0)
                {
                    //Code for Specification details
                    //----------------------------------------------------------------------------
                    grdProductSpecificationValue.DataSource = dtselectSpecificationDetails;
                    grdProductSpecificationValue.DataBind();
                    grdProductSpecificationValue.Visible = true;
                    fillProductNames();
                    //----------------------------------------------------------------------------
                }
            }

            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("drdSpecification_SelectedIndexChanged", "SpecificationValueMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        protected void grdProductSpecificationValue_RowEditing(object sender, GridViewEditEventArgs e)
        {

            grdProductSpecificationValue.EditIndex = e.NewEditIndex;
            fillProductSpecificationDetails(Convert.ToInt64(drdProductName.SelectedItem.Value), Convert.ToInt64((drdSpecification.SelectedItem.Value)));  //Code for generating product specification details

        }

        protected void grdProductSpecificationValue_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            #region codeformultipleupdates
            //string mStrConString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
            //Label txtSubCatEngName_gridview = (Label)grdProductSpecificationValue.Rows[e.RowIndex].FindControl("lblSubCategoryEngName");
            //Label txtSubCatRegName_gridview = (Label)grdProductSpecificationValue.Rows[e.RowIndex].FindControl("lblSubCategoryRegName");

            //TextBox txtSubCatEngValue_gridview = (TextBox)grdProductSpecificationValue.Rows[e.RowIndex].FindControl("txtSubCategoryEngValue");
            //TextBox txtSubCatRegValue_gridview = (TextBox)grdProductSpecificationValue.Rows[e.RowIndex].FindControl("txtSubCategoryRegValue");
            //HiddenField hfGridview = (HiddenField)grdProductSpecificationValue.Rows[e.RowIndex].FindControl("hfSubcategoryId");
            //try
            //{
            //   // int intIsActive = 1;
            //    string strSubCatEnglishName = txtSubCatEngName_gridview.Text.Trim();
            //    string strSubCatRegionalName = txtSubCatRegName_gridview.Text.Trim();

            //    string strSubCatEnglishValue = txtSubCatEngValue_gridview.Text.Trim();
            //    string strSubCatRegionalValue = txtSubCatRegValue_gridview.Text.Trim();

            //    intSubCatMstid = Convert.ToInt32(hfGridview.Value);
            //    intProductId = Convert.ToInt32(hfProductId.Value);

            //    dicSpecificationEnglishValue.Add(strSubCatEnglishName, strSubCatEnglishValue);
            //    dicSpecificationRegionalValue.Add(strSubCatRegionalName, strSubCatRegionalValue);

            //    grdProductSpecificationValue.EditIndex = -1;
            //    fillProductSpecificationDetails();
            //}
            //catch (Exception exError)
            //{
            //    long pLngErr = -1;
            //    if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
            //        pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
            //    pLngErr = GlobalFunctions.ReportError("grdProductSpecificationValue_RowUpdating", "SpecificationValueMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
            //    actionInfo.Attributes["class"] = "alert alert-info blink-border";
            //    actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            //}
            #endregion codeformultipleupdates

            #region singlerowupdate
            string mStrConString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
            TextBox txtSubCatEngValue_gridview = (TextBox)grdProductSpecificationValue.Rows[e.RowIndex].FindControl("txtSubCategoryEngValue");
            TextBox txtSubCatRegValue_gridview = (TextBox)grdProductSpecificationValue.Rows[e.RowIndex].FindControl("txtSubCategoryRegValue");
            HiddenField hfGridview = (HiddenField)grdProductSpecificationValue.Rows[e.RowIndex].FindControl("hfSubcategoryId");
            try
            {
                int intIsActive = 1;
                string strSubCatEnglishValue = txtSubCatEngValue_gridview.Text;
                string strSubCatRegionalValue = txtSubCatRegValue_gridview.Text;

                long intSubCatMstid = Convert.ToInt64(hfGridview.Value);
                long intProductId = Convert.ToInt64(drdProductName.SelectedItem.Value);
                long longCategorySpecID = Convert.ToInt64(drdSpecification.SelectedItem.Value);

                long intIsModify = 0;
                string strUpdateSpecificationValues = "Select PSDM_bIntDetailId From Product_Specifications_Details_Master_" + Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID);
                strUpdateSpecificationValues = strUpdateSpecificationValues + " where PSDM_bItnSpec_SubCatId = " + intSubCatMstid;
                strUpdateSpecificationValues = strUpdateSpecificationValues + " And PSDM_bIntProductId = " + intProductId;

                DataTable dtgetSpecificationValid = SqlHelper.ReadTable(strUpdateSpecificationValues, mStrConString, false);
                if (dtgetSpecificationValid.Rows.Count > 0)
                    intIsModify = Convert.ToInt64(dtgetSpecificationValid.Rows[0][0]);

                DataTable dtproductSpecification = SqlHelper.ReadTable("SP_insertSpecificationValue", mStrConString, true,
                    SqlHelper.AddInParam("@pintTalukaid", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                    SqlHelper.AddInParam("@pintProductid", SqlDbType.BigInt, intProductId),
                    SqlHelper.AddInParam("@pintSubCatid", SqlDbType.BigInt, intSubCatMstid),
                    SqlHelper.AddInParam("@pintCatSpecid", SqlDbType.BigInt, longCategorySpecID),
                    SqlHelper.AddInParam("@pvcharSubCatEng", SqlDbType.VarChar, strSubCatEnglishValue),
                    SqlHelper.AddInParam("@pnvcharSubCatReg", SqlDbType.NVarChar, strSubCatRegionalValue),
                    SqlHelper.AddInParam("@pintisActive", SqlDbType.Bit, intIsActive),
                    SqlHelper.AddInParam("@pintIsModify", SqlDbType.BigInt, intIsModify));

                if (dtproductSpecification.Rows.Count > 0)
                {
                    //Code on updating tables
                    grdProductSpecificationValue.EditIndex = -1;
                    fillProductSpecificationDetails(Convert.ToInt64(drdProductName.SelectedItem.Value), Convert.ToInt64((drdSpecification.SelectedItem.Value)));  //Code for generating product specification details
                    //    fillProductNames();                 //Code for filling product names
                }

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("grdProductSpecificationValue_RowUpdating", "SpecificationValueMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
            #endregion singlerowupdate
        }

        protected void grdProductSpecificationValue_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdProductSpecificationValue.EditIndex = -1;
            fillProductSpecificationDetails(Convert.ToInt64(drdProductName.SelectedItem.Value), Convert.ToInt64((drdSpecification.SelectedItem.Value)));  //Code for generating product specification details

        }

        protected void grdProductSpecificationValue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Code on row binding 
        }

        //protected void drdProductName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string mStrConString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
        //        string strProductName = drdProductName.SelectedValue.ToString();
        //        string strQuery = "Select PM_bIntProdId from ProductMaster where (PM_vCharProdName LIKE '%" + strProductName + "%')";
        //        DataTable dtProductselect = SqlHelper.ReadTable(strQuery, mStrConString, false);
        //        if (dtProductselect.Rows.Count > 0)
        //            hfProductId.Value = Convert.ToString(dtProductselect.Rows[0][0]);

        //        grdProductSpecificationValue.Visible = false;
        //        drdSpecification.SelectedIndex = 0;
        //    }
        //    catch (Exception exError)
        //    {
        //        long pLngErr = -1;
        //        if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //            pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //        pLngErr = GlobalFunctions.ReportError("drdProductName_SelectedIndexChanged", "SpecificationValueMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
        //        actionInfo.Attributes["class"] = "alert alert-info blink-border";
        //        actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
        //    }
        //}

        protected void btnViewProductSpecification_ServerClick(object sender, EventArgs e)
        {
            //Code to display specification details for product selected
            try
            {
                string mStrConString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
                string strProductName = drProductViewSpecification.SelectedItem.ToString();
                DataTable dtSelectProductView;
                //lblProductName.Text = strProductName;

                long lngProductId = 0;
                lngProductId = Convert.ToInt64(drProductViewSpecification.SelectedItem.Value);

                dtSelectProductView = SqlHelper.ReadTable("SP_ReadProductSpecificationCatAndSubCat", mStrConString, true,
                                      SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                      SqlHelper.AddInParam("@bIntProductId", SqlDbType.BigInt, lngProductId));

                if (dtSelectProductView.Rows.Count > 0)
                {
                    //code to display image
                    //---------------------------------------------------------------------------
                    StringBuilder htmlTable = new StringBuilder();

                    htmlTable.Append("<center><table class='table'>");
                    htmlTable.Append("<thead><tr><th colspan='2'></th></tr></thead><tbody>");
                    for (int i = 0; i < dtSelectProductView.Rows.Count; i++)
                    {
                        htmlTable.Append("<tr>");
                        string mstrCategoryName = dtSelectProductView.Rows[i]["PSMC_vCharCat_NameEn"].ToString();
                        if (i != 0)
                        {
                            if (!dtSelectProductView.Rows[i - 1]["PSMC_vCharCat_NameEn"].ToString().Equals(mstrCategoryName))
                                htmlTable.Append("<td colspan='2' style='color:green'><label>" + mstrCategoryName + "</label></td></tr><tr>");
                        }
                        else
                        {
                            htmlTable.Append("<td colspan='2' style='color:green'><label>" + mstrCategoryName + "</label></td></tr><tr>");
                        }
                        string mstrProductSubcatName = dtSelectProductView.Rows[i]["PSSCM_vCharSubCat_NameEn"].ToString();
                        string mstrProductSubcatValue = dtSelectProductView.Rows[i]["PSDM_vCharValue_En"].ToString();
                        htmlTable.Append("<td><label>" + mstrProductSubcatName + "</label></td><td><label>" + mstrProductSubcatValue + "</label></td>");

                        htmlTable.Append("</tr>");
                    }

                    htmlTable.Append("</tbody></table></center>");

                    DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });

                    //-----------------------------------------------------------------------------
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnViewProductSpecification_ServerClick", "SpecificationValueMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
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
                drdSpecification.Items.Insert(0, new ListItem("--Select Specification--", "0"));
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

        protected void drdProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(drdCategoryType.SelectedItem.Value) != 0 && Convert.ToInt32(drdSubCategoryType.SelectedItem.Value) != 0 && Convert.ToInt32(drdProductName.SelectedItem.Value) != 0 && Convert.ToInt32(drpInfoselection.SelectedItem.Value) != 0 && Convert.ToInt32(drdSpecification.SelectedItem.Value) != 0)
            {
                fillProductSpecificationDetails(Convert.ToInt32(drdProductName.SelectedItem.Value), Convert.ToInt32(drdSpecification.SelectedItem.Value));

            }
        }

        protected void btnViewProductSpecify_ServerClick(object sender, EventArgs e)
        {
            //Code on view product specification in regional language
            try
            {
                string mStrConString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
                string strProductName = drProductViewSpecification.SelectedItem.ToString();
                DataTable dtSelectProductView;
                //lblProductName.Text = strProductName;

                long lngProductId = 0;
                lngProductId = Convert.ToInt64(drProductViewSpecification.SelectedItem.Value);

                dtSelectProductView = SqlHelper.ReadTable("SP_ReadProductSpecificationCatAndSubCat", mStrConString, true,
                                      SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                      SqlHelper.AddInParam("@bIntProductId", SqlDbType.BigInt, lngProductId));

                if (dtSelectProductView.Rows.Count > 0)
                {
                    //code to display image
                    //---------------------------------------------------------------------------
                    StringBuilder htmlTable = new StringBuilder();

                    htmlTable.Append("<center><table class='table'>");
                    htmlTable.Append("<thead><tr><th colspan='2'></th></tr></thead><tbody>");
                    for (int i = 0; i < dtSelectProductView.Rows.Count; i++)
                    {
                        htmlTable.Append("<tr>");
                        string mstrCategoryName = dtSelectProductView.Rows[i]["PSMC_nVarCat_NameReg"].ToString();
                        if (i != 0)
                        {
                            if (!dtSelectProductView.Rows[i - 1]["PSMC_nVarCat_NameReg"].ToString().Equals(mstrCategoryName))
                                htmlTable.Append("<td colspan='2' style='color:green'><label>" + mstrCategoryName + "</label></td></tr><tr>");
                        }
                        else
                        {
                            htmlTable.Append("<td colspan='2' style='color:green'><label>" + mstrCategoryName + "</label></td></tr><tr>");
                        }
                        string mstrProductSubcatName = dtSelectProductView.Rows[i]["PSSCM_nVarSubCat_NameReg"].ToString();
                        string mstrProductSubcatValue = dtSelectProductView.Rows[i]["PSDM_nVarValue_Reg"].ToString();
                        htmlTable.Append("<td><label>" + mstrProductSubcatName + "</label></td><td><label>" + mstrProductSubcatValue + "</label></td>");

                        htmlTable.Append("</tr>");
                    }

                    htmlTable.Append("</tbody></table></center>");

                    DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });

                    //-----------------------------------------------------------------------------
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnViewProductSpecify_ServerClick", "SpecificationValueMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }

        }

    }
}