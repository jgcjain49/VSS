using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using LumenWorks.Framework.IO.Csv;

namespace Admin_CommTrex
{
    public partial class ProductsMaster : System.Web.UI.Page
    {
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
                    FillProductInfo();
                    FillInformation(drpEditInfo);
                    ViewState["RowVal"] = "";
                    ViewState["ImgPath"] = new List<string>();
                    ViewState["ImgDes"] = new List<string>();
                    ViewState["ImgRegDes"] = new List<string>();
                    ViewState["ImgDefault"] = new List<string>();
                    ViewState["ImgOrginalPath"] = new List<string>();
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }

        protected void drpEditInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpEditInfo.SelectedIndex != 0)
                {
                    FillCategory(Convert.ToInt64(drpEditInfo.SelectedItem.Value), drpEditCategorySelect);
                    if (drpEditCategorySelect.Items.Count > 0 && HiddenCat.Value != "")
                    {
                        setIndexCatName(HiddenCat.Value);
                    }


                }

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("drpEditInfo_SelectedIndexChanged", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }

        }

        protected void btnEditSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string strValidate = "";
                double dblWeight = 0;
                double dblPurity = 0;
                if (txtEditWeight.Text != "")
                {
                    dblWeight = Convert.ToDouble(txtEditWeight.Text);
                }


                strValidate = Validate(Convert.ToInt64(drpEditInfo.SelectedItem.Value), Convert.ToInt64(drpEditCategorySelect.SelectedItem.Value), Convert.ToInt64(drpEditSubCategory.SelectedItem.Value), txtEditProdName.Text, txtEditRegProdName.Text, Convert.ToInt64(drpEditIsActive.SelectedItem.Value), Convert.ToInt64(drpEditArrival.SelectedItem.Value), Convert.ToInt64(txtEditProdID.Text), dblWeight, drpEditPurity.SelectedItem.Text, txtEditQuantity.Text, Convert.ToInt32(drpEditIsMakingChrgFixed.SelectedItem.Value));
                if (strValidate == "")
                {
                    DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProducts", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                    SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                    SqlHelper.AddInParam("@vcharProd_Name", SqlDbType.VarChar, txtEditProdName.Text),
                    SqlHelper.AddInParam("@nVarProd_Name", SqlDbType.NVarChar, txtEditRegProdName.Text),
                    SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, Convert.ToInt64(drpEditCategorySelect.SelectedItem.Value)),
                    SqlHelper.AddInParam("@bintSubCatID", SqlDbType.BigInt, Convert.ToInt64(drpEditSubCategory.SelectedItem.Value)),
                    SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, Convert.ToInt64(drpEditIsActive.SelectedItem.Value)),
                    SqlHelper.AddInParam("@bItIsNewArrival", SqlDbType.Bit, Convert.ToInt64(drpEditArrival.SelectedItem.Value)),
                    SqlHelper.AddInParam("@bitfixMakingcharge", SqlDbType.Bit, Convert.ToInt64(drpEditIsMakingChrgFixed.SelectedItem.Value)),
                    SqlHelper.AddInParam("@frmMakingcharge", SqlDbType.Decimal, editMakingCharge.Text != "" ? Convert.ToDecimal(editMakingCharge.Text) : 0),
                        //SqlHelper.AddInParam("@dblProdActualPrice", SqlDbType.Decimal, Convert.ToDecimal(txtEditPrice.Text)),
                        //SqlHelper.AddInParam("@dblProdDiscountPrice", SqlDbType.Decimal, Convert.ToDecimal(txtEditDisPrice.Text)),
                        //SqlHelper.AddInParam("@dblProdDiscountPer", SqlDbType.Decimal, Convert.ToDecimal(txtEditDisPer.Text)),
                     SqlHelper.AddInParam("@dblWeight", SqlDbType.Decimal, Convert.ToDecimal(txtEditWeight.Text)),
                    SqlHelper.AddInParam("@vcharPurity", SqlDbType.VarChar, drpEditPurity.SelectedItem.Text),
                    SqlHelper.AddInParam("@intQuantity", SqlDbType.Int, Convert.ToInt32(txtEditQuantity.Text)),
                    SqlHelper.AddInParam("@vCharProdDesc", SqlDbType.VarChar, txtEditProductDes.Text),
                    SqlHelper.AddInParam("@nVarProdDesc", SqlDbType.NVarChar, txtEditRegProdDescription.Text),
                    SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, Convert.ToInt64(txtEditProdID.Text)));

                    HiddenCat.Value = "";
                    HiddenSubCat.Value = "";
                    HiddenFieldInfo.Value = "";
                    HiddenFieldForDialogOpenClose.Value = "c";
                    drpEditInfo.SelectedIndex = 0;

                    int intTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
                    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);

                    //long lngImgID;
                    //for (int i = 0; i <= lstImagepath.Count; i++)
                    //    {
                    //        if (lstImagepath.ElementAt(i) != "")
                    //            {
                    //                CopyFileSafely(lstImagepath.ElementAt(i), lstOrginalPath.ElementAt(i));
                    //            }
                    //        lngImgID = InsertUpdateImages(intTalukaId, 0, i); 
                    //    }

                    GlobalFunctions.saveInsertUserAction("Product_Master", "[Product Master Update]:Updation of Product with Id : " + Convert.ToInt64(txtEditProdID.Text), intTalukaId, lngCompanyId, Request); //Call to user Action Log
                    SetProductsDisMessage(false, "Product Updated Successfully!!!");
                    FillProductInfo();
                    LockControls(false);
                }
                else
                {
                    SetProductsUpdateMessage(true, strValidate);
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnEditSave_ServerClick", "Product_Sub_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int intImgHeight;
                int intImgWidth;

                if (btnSave.Attributes["btn-action"] == "Save")
                {

                    double dblWeight = 0;
                    double dblPurity = 0;

                    if (txtProductWeight.Text != "")
                    {
                        dblWeight = Convert.ToDouble(txtProductWeight.Text);
                    }



                    string strValidate = "";
                    strValidate = Validate(Convert.ToInt64(drpInformation.SelectedItem.Value), Convert.ToInt64(cmbCategorySelection.SelectedItem.Value), Convert.ToInt64(drpSubCategorySelection.SelectedItem.Value), txtProductName.Text, txtProductRegName.Text, Convert.ToInt64(drpIsActive.SelectedItem.Value), Convert.ToInt64(drpIsArrival.SelectedItem.Value), 0, dblWeight, drpPurity.SelectedItem.Text, txtQuantity.Text, Convert.ToInt32(ddlMakingcharge.SelectedItem.Value));
                    if (strValidate == "")
                    {
                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProducts", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                        SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                        SqlHelper.AddInParam("@vcharProd_Name", SqlDbType.VarChar, txtProductName.Text),
                        SqlHelper.AddInParam("@nVarProd_Name", SqlDbType.NVarChar, txtProductRegName.Text),
                        SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, Convert.ToInt64(cmbCategorySelection.SelectedItem.Value)),
                        SqlHelper.AddInParam("@bintSubCatID", SqlDbType.BigInt, Convert.ToInt64(drpSubCategorySelection.SelectedItem.Value)),
                        SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, Convert.ToInt64(drpIsActive.SelectedItem.Value)),
                        SqlHelper.AddInParam("@bItIsNewArrival", SqlDbType.Bit, Convert.ToInt64(drpEditArrival.SelectedItem.Value)),
                        SqlHelper.AddInParam("@bitfixMakingcharge", SqlDbType.Bit, Convert.ToInt64(ddlMakingcharge.SelectedItem.Value)),
                        SqlHelper.AddInParam("@frmMakingcharge", SqlDbType.Decimal, txtMakingChrg.Text != "" ? Convert.ToDecimal(txtMakingChrg.Text) : 0),
                            //SqlHelper.AddInParam("@dblProdActualPrice", SqlDbType.Decimal, Convert.ToDecimal(txtProductActualPrice.Text)),
                            //SqlHelper.AddInParam("@dblProdDiscountPrice", SqlDbType.Decimal, Convert.ToDecimal(txtDiscountPrice.Text)),
                            //SqlHelper.AddInParam("@dblProdDiscountPer", SqlDbType.Decimal, Convert.ToDecimal(txtDiscountPercent.Text)),
                        SqlHelper.AddInParam("@dblWeight", SqlDbType.Decimal, Convert.ToDecimal(txtProductWeight.Text)),
                        SqlHelper.AddInParam("@vcharPurity", SqlDbType.VarChar, drpPurity.SelectedItem.Text),
                        SqlHelper.AddInParam("@intQuantity", SqlDbType.Int, Convert.ToInt32(txtQuantity.Text)),
                        SqlHelper.AddInParam("@vCharProdDesc", SqlDbType.VarChar, txtProdDesc.Text),
                        SqlHelper.AddInParam("@nVarProdDesc", SqlDbType.NVarChar, txtRegProdDesc.Text),
                        SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, 0));
                        long intPCatID = 0;

                        if (dtCatData.Rows.Count > 0)
                        {
                            DataRow dtRowCat = dtCatData.Rows[0];
                            intPCatID = Convert.ToInt64(dtRowCat["PM_bIntProdId"].ToString());
                            txtProductID.Text = Convert.ToString(intPCatID);
                        }

                        int intTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;

                        long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);

                        List<string> lstImgPath = (List<string>)ViewState["ImgPath"];
                        List<string> lstImgDes = (List<string>)ViewState["ImgDes"];
                        List<string> lstImgRegDes = (List<string>)ViewState["ImgRegDes"];
                        List<string> lstImgDefault = (List<string>)ViewState["ImgDefault"];
                        List<string> lstImgOrginalPath = (List<string>)ViewState["ImgOrginalPath"];

                        for (int i = 0; i <= lstImgPath.Count - 1; i++)
                        {
                            CopyFileSafely(lstImgPath.ElementAt(i), lstImgOrginalPath.ElementAt(i));
                            GetImageSize(out intImgHeight, out  intImgWidth, lstImgOrginalPath.ElementAt(i).ToString());
                            long ImageID = InsertUpdateImages(intTalukaId, 0, intPCatID, lstImgOrginalPath.ElementAt(i), lstImgDes.ElementAt(i), lstImgRegDes.ElementAt(i), Convert.ToInt32(lstImgDefault.ElementAt(i)), intImgWidth, intImgHeight);
                            GlobalFunctions.saveInsertUserAction("Product_Master", "[Product Master Insert]:Insertion of Product with ProductID " + Convert.ToInt64(txtProductID.Text) + "with Image ID: " + ImageID + "With ImagePath" + lstImgOrginalPath.ElementAt(i), intTalukaId, lngCompanyId, Request); //Call to user Action Log
                        }

                        btnSave.Attributes["btn-action"] = "New";
                        btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> New";
                        LockControls(false);
                        FillProductInfo();
                        ViewState["ImgPath"] = new List<string>();
                        ViewState["ImgDes"] = new List<string>();
                        ViewState["ImgRegDes"] = new List<string>();
                        ViewState["ImgDefault"] = new List<string>();
                        ViewState["ImgOrginalPath"] = new List<string>();

                        GlobalFunctions.saveInsertUserAction("Product_Master", "[Product Master Insert]:Insertion of Product  with Id : " + Convert.ToInt64(txtProductID.Text), intTalukaId, lngCompanyId, Request); //Call to user Action Log
                        SetMessage(false, "Product Added Successfully!!!");
                        FillProductInfo();
                        LockControls(false);
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
                    FillInformation(drpInformation);
                    btnSave.Attributes["btn-action"] = "Save";
                    btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> Save";
                }

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSave_ServerClick", "Product_Sub_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void cmbCategorySelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubCategory(Convert.ToInt64(drpInformation.SelectedItem.Value), Convert.ToInt64(cmbCategorySelection.SelectedItem.Value), drpSubCategorySelection);
            FillPurity(cmbCategorySelection.SelectedItem.Text, drpPurity);
        }

        protected void btnDeleteInfo_ServerClick(object sender, EventArgs e)
        {
            try
            {

                //DataTable dtGetImages = SqlHelper.ReadTable("spGetImageDetails", true, SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                //                    SqlHelper.AddInParam("@bintProdID", SqlDbType.BigInt, txtHProdID.Value));


                //if (dtGetImages.Rows.Count > 0)
                //    for (int intcount = 1; intcount <= dtGetImages.Rows.Count; intcount++)
                //    {
                //        DataRow dtRowCat = dtGetImages.Rows[intcount - 1];
                //       File.Delete(Server.MapPath( dtRowCat["PI_nVarImgDesc"].ToString()));
                //    }

                DataTable dtDeletedData = SqlHelper.ReadTable("spDeleteProduct", true,
                                         SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                         SqlHelper.AddInParam("@intAmId", SqlDbType.BigInt, Convert.ToInt64(txtHProdID.Value)),
                                          SqlHelper.AddInParam("@varcharCloseReason ", SqlDbType.VarChar, txtDelReason.Text),
                                         SqlHelper.AddInParam("@nvarcharCloseReason", SqlDbType.NVarChar, txtDelReasonReg.Text));

                SetProductsDisMessage(true, "Product Deleted SuccessFully!!!");
                FillProductInfo();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnDeleteInfo_ServerClick", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }

        }

        protected void drpEditCategorySelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubCategory(Convert.ToInt64(drpEditInfo.SelectedItem.Value), Convert.ToInt64(drpEditCategorySelect.SelectedItem.Value), drpEditSubCategory);
            FillPurity(drpEditCategorySelect.SelectedItem.Text, drpEditPurity);
            if (drpEditSubCategory.Items.Count > 0 && HiddenSubCat.Value != "")
            {
                setIndexSubCatName(HiddenSubCat.Value);
            }
            if (drpPurity.Items.Count > 0 && HiddenPurity.Value != "")
            {
                setIndexPurity(HiddenPurity.Value);
            }
        }

        public void FillProductInfo()
        {
            try
            {
                System.Data.DataTable dtProdCat = new DataTable();
                DataTable dtCatData = SqlHelper.ReadTable("spGetProductDetails", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                      SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)));
                grdProducts.DataSource = dtCatData;
                grdProducts.DataBind();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillProductInfo", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
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
                DataTable dtCategoryList = SqlHelper.ReadTable("SELECT IM_vCharInfoName_En,IM_bIntInfoId FROM Information_Master_17  WHERE IM_IsOrder=1  And IM_intInfoType=1", conString, false);

                drpInformation.DataSource = dtCategoryList;
                drpInformation.DataTextField = "IM_vCharInfoName_En";
                drpInformation.DataValueField = "IM_bIntInfoId";
                drpInformation.DataBind();
                drpInformation.Items.Insert(0, new ListItem("--Select Information Type--", "0"));
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
                DataTable dtCategoryList = SqlHelper.ReadTable("SELECT PC_bIntCategoryId,PC_vCharCatName FROM Product_Categories_17  WHERE PC_bItIsActive = 1 And PC_bIntInformationId=" + infoID, conString, false);
                drpCategoryType.Items.Clear();

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

        public bool FillPurity(string strPurity, DropDownList drpPurity)
        {
            try
            {
                //if (HiddenIsMakingCharge.Value !="" )
                //{
                //    if (HiddenIsMakingCharge.Value == "True")
                //    {
                //        drpEditIsMakingChrgFixed.Items.FindByValue("1").Selected = true;
                //    }
                //    else
                //    {
                //        drpEditIsMakingChrgFixed.Items.FindByValue("0").Selected = true;
                //    }
                //}


                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                DataTable dtPurityList;
                dtPurityList = SqlHelper.ReadTable("spGetProductDailyPriceDetails", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                             SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                             SqlHelper.AddInParam("@vCharCatName", SqlDbType.VarChar, strPurity));
                drpPurity.Items.Clear();

                if (dtPurityList.Rows.Count > 0)
                {
                    drpPurity.DataSource = dtPurityList;

                    drpPurity.DataTextField = "PDP_decPurity";
                    drpPurity.DataValueField = "PDP_bIntId";
                    drpPurity.DataBind();
                    drpPurity.Items.Insert(0, new ListItem("--Select Purity--", "0"));
                    return true;
                }
                drpPurity.Items.Insert(0, new ListItem("--Select Purity--", "0"));





                return false;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillPurity", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
                return false;
            }
        }

        public void LockControls(bool blnFlag)
        {
            drpInformation.Enabled = blnFlag;
            cmbCategorySelection.Enabled = blnFlag;
            drpSubCategorySelection.Enabled = blnFlag;

            txtProductName.Enabled = blnFlag;
            txtProductRegName.Enabled = blnFlag;

            //txtProductActualPrice.Enabled = blnFlag;
            //txtDiscountPrice.Enabled = blnFlag;
            //txtDiscountPercent.Enabled = blnFlag;

            txtProductWeight.Enabled = blnFlag;
            drpPurity.Enabled = blnFlag;

            txtProdDesc.Enabled = blnFlag;
            txtRegProdDesc.Enabled = blnFlag;
            drpIsActive.Enabled = blnFlag;
            drpIsArrival.Enabled = blnFlag;
            btnImgUpload.Disabled = !blnFlag;
        }

        public void ClearControls()
        {
            drpInformation.Items.Clear();
            cmbCategorySelection.Items.Clear();
            drpSubCategorySelection.Items.Clear();

            txtProductName.Text = "";
            txtProductRegName.Text = "";

            //txtProductActualPrice.Text = "";
            //txtDiscountPrice.Text = "";
            //txtDiscountPercent.Text = "";
            txtProductWeight.Text = "";
            drpPurity.Items.Clear();
            txtProdDesc.Text = "";
            txtRegProdDesc.Text = "";
            drpIsActive.SelectedIndex = 0;
            drpIsArrival.SelectedIndex = 0;
            drpDefaultImage.Items.Clear();
            DBDataPlaceHolder.Controls.Clear();

            FileUploadControl.Attributes.Clear();
            //  MainImage.ImageUrl = "~/";

        }

        public string Validate(long InfoID, long CatID, long SubCatID, string ProdName, string ProdRegName, long ActiveID, long ArrivalId, long intProdID, double dblWeight, string strPurity, string Quantity, int MakingCharge)
        {
            string strValidate = "";
            string strDupChk = "";

            if (InfoID == 0)
            {
                strValidate = "Information Not Selected !!!";
            }

            if (CatID == 0)
            {
                strValidate += "Category Not Selected !!!";
            }
            if (SubCatID == 0)
            {
                strValidate += "SubCategory Not Selected !!!";
            }


            //if (dblDiscountPrice > 0)
            //{
            //    if (dblActualPrice < dblDiscountPrice)
            //    {
            //        strValidate += "Actual Price Should be Greater then Discount Price!!!";
            //    }
            //}

            //if (dblActualPrice == 0)
            //{

            //    strValidate += "Actual Price Cannot be Blank";

            //}
            if (dblWeight == 0)
            {

                strValidate += "Weight Cannot be Blank";

            }
            if (strPurity == "")
            {

                strValidate += "Purity Cannot be Blank";

            }


            if (ProdName == "")
            {
                strValidate += "Product Name Cannot be blank!!";
            }

            if (ProdRegName == "")
            {
                strValidate += "Product Reg Name Cannot be Blank";
            }

            if (CatID != 0 && InfoID != 0 && SubCatID != 0)
            {
                strDupChk = ChkDuplicate(InfoID, CatID, SubCatID, ProdName, ProdRegName, intProdID);
                if (strDupChk != "")
                {
                    strValidate += strDupChk;
                }

            }

            if (ActiveID == -1)
            {
                strValidate += "Activation Type not Selected!!";
            }
            if (ArrivalId == -1)
            {
                strValidate += "Arrival Type not Selected!!";
            }

            if (Quantity == "")
            {
                strValidate += "Quantity cannot be blank !!";
            }
            if (MakingCharge == -1)
            {
                strValidate += "Making Charge not Selected!!";
            }

            return strValidate;
        }

        public string ChkDuplicate(long InfoID, long CatID, long SubCatID, string ProdName, string ProdRegName, long intProdID)
        {
            try
            {
                string strDuplicate = "";
                TalukaData objTaluka = (TalukaData)Session["TalukaDetails"];
                int TalukaID = objTaluka.TalukaID;
                DataTable dtChkDup = new DataTable();
                string strId = Convert.ToString(TalukaID);


                dtChkDup = SqlHelper.ReadTable("spProductChkDuplicate", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                             SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                             SqlHelper.AddInParam("@varcharProdName", SqlDbType.VarChar, ProdName),
                                             SqlHelper.AddInParam("@bintInfoID", SqlDbType.BigInt, InfoID),
                                             SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, CatID),
                                             SqlHelper.AddInParam("@bintSubCatID", SqlDbType.BigInt, SubCatID),
                                             SqlHelper.AddInParam("@bintId", SqlDbType.BigInt, intProdID));

                if (dtChkDup.Rows.Count > 0)
                {
                    strDuplicate = "Product Category Name already Exsits";
                }

                dtChkDup = SqlHelper.ReadTable("spProductRegChkDuplicate", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                        SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                        SqlHelper.AddInParam("@nvarcharProdName", SqlDbType.NVarChar, ProdRegName),
                                        SqlHelper.AddInParam("@bintInfoID", SqlDbType.BigInt, InfoID),
                                        SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, CatID),
                                        SqlHelper.AddInParam("@bintSubCatID", SqlDbType.BigInt, SubCatID),
                                        SqlHelper.AddInParam("@bintId", SqlDbType.BigInt, intProdID));
                if (dtChkDup.Rows.Count > 0)
                {
                    strDuplicate += " Product Regional Category Name already Exsits";
                }
                return strDuplicate;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("ChkDuplicate", "Product_Sub_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
                return Convert.ToString(pLngErr);

            }
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

        private void setIndexCatName(string strCatID)
        {
            if (HiddenCat.Value != "")
            {
                drpEditCategorySelect.Items.FindByValue(strCatID).Selected = true;
                FillSubCategory(Convert.ToInt64(drpEditInfo.SelectedItem.Value), Convert.ToInt64(drpEditCategorySelect.SelectedItem.Value), drpEditSubCategory);
                FillPurity(drpEditCategorySelect.SelectedItem.Text, drpEditPurity);
                if (drpEditSubCategory.Items.Count > 0 && HiddenSubCat.Value != "")
                {
                    setIndexSubCatName(HiddenSubCat.Value);
                }
                if (drpEditPurity.Items.Count > 0 && HiddenPurity.Value != "")
                {
                    setIndexPurity(HiddenPurity.Value);
                }
                HiddenCat.Value = "";

            }
        }

        private void setIndexSubCatName(string strSubCatID)
        {
            if (HiddenSubCat.Value != "")
            {
                drpEditSubCategory.Items.FindByValue(strSubCatID).Selected = true;
                HiddenSubCat.Value = "";
            }
        }
        private void setIndexPurity(string strPurity)
        {
            if (HiddenPurity.Value != "")
            {
                drpEditPurity.Items.FindByText(strPurity).Selected = true;
                HiddenPurity.Value = "";
            }
        }

        private void SetProductsDisMessage(bool pBlnIsError, string pStrMessage)
        {
            updateActionDivDis.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            updateActionDivDis.InnerHtml = pStrMessage;
        }

        protected void drpInformation_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                if (drpInformation.SelectedIndex != 0)
                {
                    FillCategory(Convert.ToInt64(drpInformation.SelectedItem.Value), cmbCategorySelection);
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

        protected void btnImgUpload_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int countImg = Convert.ToInt32(hfImgCount.Text);
                if (countImg <= 5)
                {
                    if (FileUploadControl.HasFile)
                    {
                        StringBuilder htmlTable = new StringBuilder();

                        htmlTable.Append("<center><table border='1'>");
                        htmlTable.Append("<tr style='background-color:#2042ae; color: White;'><th colspan='4'>Images</th></tr>");
                        htmlTable.Append("<tr style='color: White;'>");

                        string pstrFileName = "";
                        string pstrFilePath = "";
                        string strFinalFilePath = "";
                        string strFinalFileName = "";


                        pstrFileName = GetFileName(FileUploadControl.PostedFile.FileName.ToString(), GlobalVariables.FileTempProducts);
                        //pstrFileName = GetFileName(FileUploadControl.PostedFile.FileName.ToString(),"");
                        pstrFilePath = (GlobalVariables.FileTempProducts) + "//" + pstrFileName;
                        //pstrFilePath = "//" + pstrFileName;


                        strFinalFileName = GetFileName(FileUploadControl.PostedFile.FileName.ToString(), GlobalVariables.FileProducts);
                        strFinalFilePath = (GlobalVariables.FileProducts) + "//" + strFinalFileName;

                        List<string> lstImagepath = (List<string>)(this.ViewState["ImgPath"]);
                        List<string> lstImgDes = (List<string>)(this.ViewState["ImgDes"]);
                        List<string> lstImgRegDes = (List<string>)(this.ViewState["ImgRegDes"]);
                        List<string> lstDefault = (List<string>)(this.ViewState["ImgDefault"]);
                        List<string> lstOrginalPath = (List<string>)(this.ViewState["ImgOrginalPath"]);


                        lstImagepath.Add(pstrFilePath);
                        this.ViewState["ImgPath"] = lstImagepath;

                        lstImgRegDes.Add(txtRegImageDescription.Text);
                        this.ViewState["ImgRegDes"] = lstImgRegDes;


                        lstImgDes.Add(txtImgDescription.Text);
                        this.ViewState["ImgDes"] = lstImgDes;


                        lstOrginalPath.Add(strFinalFilePath);
                        this.ViewState["ImgOrginalPath"] = lstOrginalPath;

                        if (lstImagepath.Count == 1)
                        {
                            lstDefault.Add("1");
                            this.ViewState["ImgDefault"] = lstDefault;
                        }
                        else
                        {
                            lstDefault.Add("0");
                            this.ViewState["ImgDefault"] = lstDefault;

                        }

                        pstrFilePath = pstrFilePath.Replace("//", "/");
                        drpDefaultImage.Items.Add(new ListItem(pstrFileName, lstImagepath.Count.ToString()));

                        if (!Directory.Exists(GlobalVariables.FileTempProducts))
                        {
                            Directory.CreateDirectory(Server.MapPath(GlobalVariables.FileTempProducts));
                        }

                        FileUploadControl.SaveAs(Server.MapPath(GlobalVariables.FileTempProducts) + "//" + pstrFileName);

                        //FileUploadControl.SaveAs(Server.MapPath("") + "//" + pstrFileName);

                        if (drpDefaultImage.Items.Count > 0)
                        {
                            for (int i = 1; i <= drpDefaultImage.Items.Count; i++)
                            {
                                if (i == 5)
                                {
                                    htmlTable.Append("</tr>");
                                    htmlTable.Append("<tr style='background-color:#2042ae; color: White;'><th colspan='5'>Images</th></tr>");
                                    htmlTable.Append("<tr style='color: White;'>");
                                }
                                htmlTable.Append("<td><img src='" + lstImagepath.ElementAt(i - 1).Replace("//", "/") + "' width='150' height='150'/></td>");
                            }
                            htmlTable.Append("</tr>");
                            htmlTable.Append("</table></center>");

                            ViewState["TableData"] = htmlTable;
                            DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });
                            countImg = drpDefaultImage.Items.Count;
                            hfImgCount.Text = Convert.ToString(countImg);
                            txtImgDescription.Text = "";
                            txtRegImageDescription.Text = "";
                        }
                    }
                }
                else
                {
                    lblwarning.Visible = true;
                }
            }
            catch (Exception expImguploaderr)
            {
                long pLngErr = -1;
                if (expImguploaderr.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)expImguploaderr.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnImgUpload_ServerClick", "Category", pLngErr, expImguploaderr.GetBaseException().GetType().ToString(), expImguploaderr.Message, expImguploaderr.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
        }

        protected void drpDefaultImage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpDefaultImage_SelectedIndexChanged1(object sender, EventArgs e)
        {
            List<string> lstImgDefault = (List<string>)(ViewState["ImgDefault"]);
            if (drpDefaultImage.SelectedItem.Value != "0")
            {
                int intIndex = lstImgDefault.IndexOf("1");
                lstImgDefault[intIndex] = "0";

                for (int i = 0; i <= lstImgDefault.Count; i++)
                {
                    if (i == Convert.ToInt32(drpDefaultImage.SelectedItem.Value))
                    {
                        lstImgDefault[i - 1] = "1";
                    }
                    //else
                    //{
                    //    lstImgDefault.Insert(i, "0");
                    //}
                }
            }
            ViewState["ImgDefault"] = lstImgDefault;

        }

        protected long InsertUpdateImages(long lngtalukaID, long intImgID, long lngProdID, string strImgPath, string strImgDes, string strImgRegDes, int intdefaultImage, int intWidth, int intHeight)
        {
            try
            {
                DataTable dtImage = SqlHelper.ReadTable("spInsertUpdateProdImageMaster1", true,
                                  SqlHelper.AddInParam("@bintProdID", SqlDbType.BigInt, lngProdID),
                                  SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, lngtalukaID),
                                  SqlHelper.AddInParam("@varCharImgPath", SqlDbType.VarChar, strImgPath),
                                  SqlHelper.AddInParam("@varcharImgDes", SqlDbType.VarChar, strImgDes),
                                  SqlHelper.AddInParam("@nvarcharImgDes", SqlDbType.NVarChar, strImgRegDes),
                                  SqlHelper.AddInParam("@bitDefaultImage", SqlDbType.Bit, intdefaultImage),
                                  SqlHelper.AddInParam("@intWidth", SqlDbType.Int, intWidth),
                                  SqlHelper.AddInParam("@intHeight", SqlDbType.Int, intHeight),
                                 SqlHelper.AddInParam("@bintmgID", SqlDbType.BigInt, intImgID));
                DataRow dtrow = dtImage.Rows[0];
                long intImageID = Convert.ToInt64(dtrow["PI_bIntProdImgId"].ToString());
                return (intImageID);
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("InsertUpdateImages", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
                return Convert.ToInt64(pLngErr);
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
                    Directory.CreateDirectory(Server.MapPath(GlobalVariables.FileProducts));


                File.Copy(pStrSourceFile, pStrDestination);
                File.Delete(pStrSourceFile);
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("CopyFileSafely", "Product_Category_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
            return pStrDestination;
        }
        public string GetFileName(String pStrDestination, string DirPath)
        {
            int count = 1;
            if (File.Exists(Path.Combine(Server.MapPath(DirPath), pStrDestination)))
            {
                while (File.Exists(Path.Combine(Server.MapPath(DirPath), pStrDestination)))
                {
                    string strgetExtension = Path.GetExtension(pStrDestination);
                    pStrDestination = string.Format("{0}({1})", Path.GetFileNameWithoutExtension(pStrDestination), count++);
                    pStrDestination = pStrDestination + strgetExtension;
                    // return(pStrDestination);
                }
            }
            //else
            //    {
            //        return (pStrDestination);
            //    }
            return (pStrDestination);
        }

        protected void btnShowGallery_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int introw = Convert.ToInt32(btn.Attributes["RowIndex"]);

                Label lblProdID1 = (Label)grdProducts.Rows[introw].FindControl("lblProdID");

                long lngProdID = Convert.ToInt64(lblProdID1.Text);
                txtModifyImgProductid.Text = lngProdID.ToString();




                DataTable dtGetImages = SqlHelper.ReadTable("spGetImageDetails", true, SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                        SqlHelper.AddInParam("@bintProdID", SqlDbType.BigInt, lngProdID));



                ClearDialougControl();

                if (dtGetImages.Rows.Count > 0)
                {
                    for (int intcount = 1; intcount <= dtGetImages.Rows.Count; intcount++)
                    {
                        DataRow dtRowCat = dtGetImages.Rows[intcount - 1];
                        setImages(Convert.ToInt64(dtRowCat["PI_bIntProdImgId"].ToString()), dtRowCat["PI_vCharImgDesc"].ToString(), dtRowCat["PI_nVarImgDesc"].ToString(), dtRowCat["PI_vCharImgPath"].ToString(), intcount);
                    }
                }

                this.ClientScript.RegisterStartupScript(this.GetType(), "test", "ShowModal()", true);
            }

            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("InsertUpdateImages", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }

        }
        protected void btnImageModify_ServerClick(object sender, EventArgs e)
        {
            long ImgID;
            string strImagePath = "";
            string strOldImagePath = "";
            int intImgWidth;
            int intImgHeight;

            if (FileMainImage.HasFile || txtMainImageID.Value != "")
            {
                if (FileMainImage.HasFile)
                {
                    strOldImagePath = ImgCoverImage.ImageUrl;
                    strImagePath = UploadAndCopyFiles(FileMainImage.PostedFile.FileName.ToString(), ImgCoverImage, FileMainImage);
                }

                if (htxtMainDescription.Value != txtMainDescription.Text || htxtMainRegDescription.Value != txtMainRegDescription.Text || FileMainImage.HasFile)
                {
                    if (CheckForDescriptionWithoutImage(txtMainDescription, txtMainRegDescription, txtMainImageID, ImgCoverImage))
                    {
                        if (strImagePath == "")
                        {
                            strImagePath = ImgCoverImage.ImageUrl;
                            strImagePath = strImagePath.Replace("~/", "");
                        }

                        if (txtMainImageID.Value == "")
                        {
                            ImgID = 0;
                            GetImageSize(out intImgWidth, out  intImgHeight, strImagePath);
                            InsertUpdateImages(Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID), ImgID, Convert.ToInt64(txtModifyImgProductid.Text), strImagePath, txtMainDescription.Text, txtMainRegDescription.Text, 1, intImgWidth, intImgHeight);
                        }
                        else
                        {
                            ImgID = Convert.ToInt64(txtMainImageID.Value);
                            GetImageSize(out intImgWidth, out  intImgHeight, strImagePath);
                            InsertUpdateImages(Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID), ImgID, Convert.ToInt64(txtModifyImgProductid.Text), strImagePath, txtMainDescription.Text, txtMainRegDescription.Text, 1, intImgWidth, intImgHeight);

                        }

                        htxtMainDescription.Value = txtMainDescription.Text;
                        htxtMainRegDescription.Value = txtMainRegDescription.Text;

                        if (FileMainImage.HasFile)
                        {
                            FileMainImage.Attributes.Clear();
                        }

                        strOldImagePath = strOldImagePath.Replace("~/", "");
                        if (!strOldImagePath.Contains("http"))
                        {
                            if (strOldImagePath != "")
                            {
                                File.Delete(Server.MapPath(strOldImagePath));
                            }
                        }
                        ImgID = 0;
                        strImagePath = "";
                        strOldImagePath = "";
                        intImgHeight = 0;
                        intImgWidth = 0;
                    }
                }
            }

            if (FileMainImage1.HasFile || txtImagePath1ID.Value != "")
            {
                if (FileMainImage1.HasFile)
                {
                    strOldImagePath = Image1.ImageUrl;
                    strImagePath = UploadAndCopyFiles(FileMainImage1.PostedFile.FileName.ToString(), Image1, FileMainImage1);
                }

                if (htxtImage1Description.Value != txtImage1Description.Text || htxtImage1RegDescription.Value != txtImage1RegDescription.Text || FileMainImage1.HasFile)
                {

                    if (CheckForDescriptionWithoutImage(txtImage1Description, txtImage1RegDescription, txtImagePath1ID, Image1))
                    {

                        if (strImagePath == "")
                        {
                            strImagePath = Image1.ImageUrl;
                            //  strImagePath.Replace("~/", "");
                            strImagePath = strImagePath.Replace("~/", "");
                        }

                        if (txtImagePath1ID.Value == "")
                        {
                            ImgID = 0;
                        }
                        else
                        {
                            ImgID = Convert.ToInt64(txtImagePath1ID.Value);
                        }
                        GetImageSize(out intImgWidth, out  intImgHeight, strImagePath);
                        InsertUpdateImages(Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID), ImgID, Convert.ToInt64(txtModifyImgProductid.Text), strImagePath, txtImage1Description.Text, txtImage1RegDescription.Text, 0, intImgWidth, intImgHeight);
                        strOldImagePath = strOldImagePath.Replace("~/", "");

                        htxtImage1Description.Value = txtImage1Description.Text;
                        htxtImage1RegDescription.Value = txtImage1RegDescription.Text;

                        if (FileMainImage1.HasFile)
                        {
                            FileMainImage1.Attributes.Clear();
                        }

                        if (!strOldImagePath.Contains("http"))
                        {
                            if (strOldImagePath != "")
                            {
                                File.Delete(Server.MapPath(strOldImagePath));
                            }
                        }
                    }
                }
                ImgID = 0;
                strImagePath = "";
                strOldImagePath = "";
                intImgWidth = 0;
                intImgHeight = 0;
            }

            if (FileMainImage2.HasFile || txtImagePath2ID.Value != "")
            {
                if (FileMainImage2.HasFile)
                {
                    strOldImagePath = Image2.ImageUrl;
                    strImagePath = UploadAndCopyFiles(FileMainImage2.PostedFile.FileName.ToString(), Image2, FileMainImage2);

                }

                if (htxtImage2Description.Value != txtImage2Description.Text || htxtImage2RegDescription.Value != txtImage2RegDescription.Text || FileMainImage2.HasFile)
                {
                    if (CheckForDescriptionWithoutImage(txtImage2Description, txtImage2RegDescription, txtImagePath2ID, Image2))
                    {
                        if (strImagePath == "")
                        {
                            strImagePath = Image2.ImageUrl;
                            strImagePath = strImagePath.Replace("~/", "");
                        }

                        if (txtImagePath2ID.Value == "")
                        {
                            ImgID = 0;
                        }
                        else
                        {
                            ImgID = Convert.ToInt64(txtImagePath2ID.Value);
                        }
                        GetImageSize(out intImgWidth, out  intImgHeight, strImagePath);
                        InsertUpdateImages(Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID), ImgID, Convert.ToInt64(txtModifyImgProductid.Text), strImagePath, txtImage2Description.Text, txtImage2RegDescription.Text, 0, intImgWidth, intImgHeight);

                        htxtImage2Description.Value = txtImage2Description.Text;
                        htxtImage2RegDescription.Value = txtImage2RegDescription.Text;

                        if (FileMainImage2.HasFile)
                        {
                            FileMainImage2.Attributes.Clear();
                        }

                        strOldImagePath.Replace("~/", "");
                        if (!strOldImagePath.Contains("http"))
                        {
                            if (strOldImagePath != "")
                            {
                                File.Delete(Server.MapPath(strOldImagePath));
                            }
                        }
                    }
                }
                ImgID = 0;
                strImagePath = "";
                strOldImagePath = "";
                intImgWidth = 0;
                intImgHeight = 0;
            }

            if (FileMainImage3.HasFile || txtImagePath3ID.Value != "")
            {
                if (FileMainImage3.HasFile)
                {
                    strOldImagePath = Image3.ImageUrl;
                    strImagePath = UploadAndCopyFiles(FileMainImage3.PostedFile.FileName.ToString(), Image3, FileMainImage3);

                }

                if (htxtImage3Description.Value != txtImage3Description.Text || htxtImage3RegDescription.Value != txtImage3RegDescription.Text || FileMainImage3.HasFile)
                {
                    if (CheckForDescriptionWithoutImage(txtImage3Description, txtImage3RegDescription, txtImagePath3ID, Image3))
                    {
                        if (strImagePath == "" || strOldImagePath != "")
                        {
                            strImagePath = Image3.ImageUrl;
                            strImagePath = strImagePath.Replace("~/", "");
                        }

                        if (txtImagePath3ID.Value == "")
                        {
                            ImgID = 0;
                        }
                        else
                        {
                            ImgID = Convert.ToInt64(txtImagePath3ID.Value);
                        }
                        GetImageSize(out intImgWidth, out  intImgHeight, strImagePath);
                        InsertUpdateImages(Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID), ImgID, Convert.ToInt64(txtModifyImgProductid.Text), strImagePath, txtImage3Description.Text, txtImage3RegDescription.Text, 0, intImgWidth, intImgHeight);

                        htxtImage3Description.Value = txtImage3Description.Text;
                        htxtImage3RegDescription.Value = txtImage3RegDescription.Text;

                        if (FileMainImage3.HasFile)
                        {
                            FileMainImage3.Attributes.Clear();
                        }

                        strOldImagePath = strOldImagePath.Replace("~/", "");
                        if (!strOldImagePath.Contains("http"))
                        {
                            if (strOldImagePath != "")
                            {
                                File.Delete(Server.MapPath(strOldImagePath));
                            }
                        }
                    }
                }
                ImgID = 0;
                strImagePath = "";
                strOldImagePath = "";
                intImgWidth = 0;
                intImgHeight = 0;
            }

            if (FileMainImage4.HasFile || txtImagePath4ID.Value != "")
            {
                if (FileMainImage4.HasFile)
                {
                    strOldImagePath = Image4.ImageUrl;
                    strImagePath = UploadAndCopyFiles(FileMainImage4.PostedFile.FileName.ToString(), Image4, FileMainImage4);

                }

                if (htxtImage4Description.Value != txtImage4Description.Text || htxtImage4RegDescription.Value != txtImage4RegDescription.Text || FileMainImage4.HasFile)
                {
                    if (CheckForDescriptionWithoutImage(txtImage4Description, txtImage4RegDescription, txtImagePath4ID, Image4))
                    {
                        if (strImagePath == "")
                        {
                            strImagePath = Image4.ImageUrl;
                            strImagePath = strImagePath.Replace("~/", "");
                        }

                        if (txtImagePath4ID.Value == "")
                        {
                            ImgID = 0;
                        }
                        else
                        {
                            ImgID = Convert.ToInt64(txtImagePath4ID.Value);
                        }
                        GetImageSize(out intImgWidth, out  intImgHeight, strImagePath);
                        InsertUpdateImages(Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID), ImgID, Convert.ToInt64(txtModifyImgProductid.Text), strImagePath, txtImage4Description.Text, txtImage4RegDescription.Text, 0, intImgWidth, intImgHeight);

                        htxtImage4Description.Value = txtImage4Description.Text;
                        htxtImage4RegDescription.Value = txtImage4RegDescription.Text;

                        if (FileMainImage4.HasFile)
                        {
                            FileMainImage4.Attributes.Clear();
                        }

                        strOldImagePath = strOldImagePath.Replace("~/", "");
                        if (!strOldImagePath.Contains("http"))
                        {
                            if (strOldImagePath != "")
                            {
                                File.Delete(Server.MapPath(strOldImagePath));
                            }
                        }
                    }
                }
                ImgID = 0;
                strImagePath = "";
                strOldImagePath = "";
                intImgWidth = 0;
                intImgHeight = 0;
            }

            if (FileMainImage5.HasFile || txtImagePath5ID.Value != "")
            {
                if (FileMainImage5.HasFile)
                {
                    strOldImagePath = Image5.ImageUrl;
                    strImagePath = UploadAndCopyFiles(FileMainImage5.PostedFile.FileName.ToString(), Image5, FileMainImage5);

                }
                if (htxtImage5Description.Value != txtImage5Description.Text || htxtImage5RegDescription.Value != txtImage5RegDescription.Text || FileMainImage5.HasFile)
                {
                    if (CheckForDescriptionWithoutImage(txtImage5Description, txtImage5RegDescription, txtImagePath5ID, Image5))
                    {
                        if (strImagePath == "")
                        {
                            strImagePath = Image5.ImageUrl;
                            strImagePath = strImagePath.Replace("~/", "");
                        }

                        if (txtImagePath5ID.Value == "")
                        {
                            ImgID = 0;
                        }
                        else
                        {
                            ImgID = Convert.ToInt64(txtImagePath5ID.Value);
                        }
                        GetImageSize(out intImgHeight, out intImgWidth, strImagePath);

                        InsertUpdateImages(Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID), ImgID, Convert.ToInt64(txtModifyImgProductid.Text), strImagePath, txtImage5Description.Text, txtImage5RegDescription.Text, 0, intImgWidth, intImgHeight);

                        htxtImage5Description.Value = txtImage5Description.Text;
                        htxtImage5RegDescription.Value = txtImage5RegDescription.Text;

                        if (FileMainImage5.HasFile)
                        {
                            FileMainImage5.Attributes.Clear();
                        }

                        strOldImagePath = strOldImagePath.Replace("~/", "");
                        if (!strOldImagePath.Contains("http"))
                        {
                            if (strOldImagePath != "")
                            {
                                File.Delete(Server.MapPath(strOldImagePath));
                            }
                        }
                    }
                }
                ImgID = 0;
                strImagePath = "";
                strOldImagePath = "";
                intImgWidth = 0;
                intImgHeight = 0;
            }

        }

        protected void setImages(long lngImgID, string strDescription, string strRegDescription, string ImagePath, int count)
        {
            try
            {
                switch (count)
                {
                    case 1:
                        txtMainImageID.Value = lngImgID.ToString();
                        txtImgPathMain.Value = ImagePath;
                        htxtMainDescription.Value = strDescription;
                        htxtMainRegDescription.Value = strRegDescription;
                        txtMainDescription.Text = strDescription;
                        txtMainRegDescription.Text = strRegDescription;
                        ImgCoverImage.ImageUrl = "~/" + ImagePath;
                        break;
                    case 2:
                        txtImagePath1ID.Value = lngImgID.ToString();
                        txtImgPathMain1.Value = ImagePath;
                        htxtImage1Description.Value = strDescription;
                        htxtImage1RegDescription.Value = strRegDescription;
                        txtImage1Description.Text = strDescription;
                        txtImage1RegDescription.Text = strRegDescription;
                        Image1.ImageUrl = "~/" + ImagePath;
                        break;
                    case 3:
                        txtImagePath2ID.Value = lngImgID.ToString();
                        txtImgPathMain2.Value = ImagePath;
                        htxtImage2Description.Value = strDescription;
                        htxtImage2RegDescription.Value = strRegDescription;
                        txtImage2Description.Text = strDescription;
                        txtImage2RegDescription.Text = strRegDescription;
                        Image2.ImageUrl = "~/" + ImagePath;
                        break;
                    case 4:
                        txtImagePath3ID.Value = lngImgID.ToString();
                        txtImgPathMain3.Value = ImagePath;
                        htxtImage3Description.Value = strDescription;
                        htxtImage3RegDescription.Value = strRegDescription;
                        txtImage3Description.Text = strDescription;
                        txtImage3RegDescription.Text = strRegDescription;
                        Image3.ImageUrl = "~/" + ImagePath;
                        break;
                    case 5:
                        txtImagePath4ID.Value = lngImgID.ToString();
                        txtImgPathMain4.Value = ImagePath;
                        htxtImage4Description.Value = strDescription;
                        htxtImage4RegDescription.Value = strRegDescription;
                        txtImage4Description.Text = strDescription;
                        txtImage4RegDescription.Text = strRegDescription;
                        Image4.ImageUrl = "~/" + ImagePath;
                        break;
                    case 6:
                        txtImagePath5ID.Value = lngImgID.ToString();
                        txtImgPathMain5.Value = ImagePath;
                        htxtImage5Description.Value = strDescription;
                        htxtImage5RegDescription.Value = strRegDescription;
                        txtImage5Description.Text = strDescription;
                        txtImage5RegDescription.Text = strRegDescription;
                        Image5.ImageUrl = "~/" + ImagePath;
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
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";

            }
        }
        public string UploadAndCopyFiles(string pstrSourceFileName, Image Img, FileUpload FileUploadControl1)
        {
            try
            {
                string pstrFileName = "";
                string strFinalFilePath = "";

                pstrFileName = GetFileName(pstrSourceFileName, GlobalVariables.FileProducts);

                strFinalFilePath = (GlobalVariables.FileProducts) + "//" + pstrFileName;

                if (!Directory.Exists(GlobalVariables.FileProducts))
                {
                    Directory.CreateDirectory(Server.MapPath(GlobalVariables.FileProducts));
                }


                FileUploadControl1.SaveAs(Server.MapPath(GlobalVariables.FileProducts) + "//" + pstrFileName);
                strFinalFilePath = strFinalFilePath.Replace("//", "/");
                Img.ImageUrl = strFinalFilePath;
                return strFinalFilePath;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("UploadAndCopyFiles", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
                return pLngErr.ToString();
            }

        }

        public bool CheckForDescriptionWithoutImage(TextBox txtDescription, TextBox txtRegionalDescription, HiddenField txtImageID, Image ProdImage)
        {
            try
            {
                if ((txtDescription.Text != "" || txtRegionalDescription.Text != "") && (ProdImage.ImageUrl == "~/" || ProdImage.ImageUrl == ""))
                {
                    //divImageGallery.Attributes["class"] = "alert " + (true ? "alert-danger" : "alert-success");
                    //divImageGallery.InnerHtml = "Image Without Description Not Allowed";
                    return false;
                }
                else
                {
                    return true;
                }
            }

            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("CheckForDescriptionWithoutImage", "Product_Master", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
                return false;
            }

        }

        private void GetImageSize(out int pintWidth, out int pintHeight, string pstrImagePath)
        {
            int originalWidth = 0;
            int originalHeight = 0;
            try
            {
                System.Drawing.Bitmap image = new System.Drawing.Bitmap(Server.MapPath(pstrImagePath));
                originalWidth = image.Width;
                originalHeight = image.Height;
                //pintHeight = originalHeight;
                //pintWidth = originalWidth;
                image.Dispose();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("ChkImageSize", "Category", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
            pintHeight = originalHeight;
            pintWidth = originalWidth;
        }

        public void ClearDialougControl()
        {

            txtMainImageID.Value = "";
            txtImgPathMain.Value = "";
            htxtMainDescription.Value = "";
            htxtMainRegDescription.Value = "";
            txtMainDescription.Text = "";
            txtMainRegDescription.Text = "";
            ImgCoverImage.ImageUrl = "";


            txtImagePath1ID.Value = "";
            txtImgPathMain1.Value = "";
            htxtImage1Description.Value = "";
            htxtImage1RegDescription.Value = "";
            txtImage1Description.Text = "";
            txtImage1RegDescription.Text = "";
            Image1.ImageUrl = "";

            txtImagePath2ID.Value = "";
            txtImgPathMain2.Value = "";
            htxtImage2Description.Value = "";
            htxtImage2RegDescription.Value = "";
            txtImage2Description.Text = "";
            txtImage2RegDescription.Text = "";
            Image2.ImageUrl = "";

            txtImagePath3ID.Value = "";
            txtImgPathMain3.Value = "";
            htxtImage3Description.Value = "";
            htxtImage3RegDescription.Value = "";
            txtImage3Description.Text = "";
            txtImage3RegDescription.Text = "";
            Image3.ImageUrl = "";

            txtImagePath4ID.Value = "";
            txtImgPathMain4.Value = "";
            htxtImage4Description.Value = "";
            htxtImage4RegDescription.Value = "";
            txtImage4Description.Text = "";
            txtImage4RegDescription.Text = "";
            Image4.ImageUrl = "";

            txtImagePath5ID.Value = "";
            txtImgPathMain5.Value = "";
            htxtImage5Description.Value = "";
            htxtImage5RegDescription.Value = "";
            txtImage5Description.Text = "";
            txtImage5RegDescription.Text = "";
            Image5.ImageUrl = "";

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

            lblMsgError.Visible = false;
            //Code for importing from excedlsheet data and images
            // txtCode.Text = Convert.ToString(Session["myvalue"]);
            if (txtCode.Text.Trim() == "")
            {
                lblMsgError.Visible = true;
            }
            else
            {
                try
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

                        int intImgHeight;
                        int intImgWidth;
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
                                //if (ext.Trim() == ".csv")
                                //{
                                //    ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Text;HDR=Yes;FMT=CSVDelimited'";  
                                //}
                                //else
                                //{
                                //    Div2.Attributes["class"] = "alert alert-info ";
                                //    Div2.InnerHtml = "Please select Only .xls or .xlsx file for uploading !!!";
                                //    return;
                                //}
                                var dtCategoryDetails = new DataTable();
                                using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(path)), true))
                                {
                                    dtCategoryDetails.Load(csvReader);
                                }
                                //OleDbConnection con = new OleDbConnection(ConStr);
                                //if (con.State == ConnectionState.Closed)
                                //{
                                //    con.Open();
                                //}

                                //string SelectCategoryquery = "select * from [Sheet3$]";
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
                                    //if (f.ContentLength > 0 && (f.FileName != "MStoreInfo.xlsx" || f.FileName != "MStoreInfoFree.xlsx" || f.FileName != "MStoreInfoBusiness.xlsx"))
                                    if (f.ContentLength > 0 && (f.FileName != "MStoreInfo.csv" || f.FileName != "MStoreInfoFree.csv" || f.FileName != "MStoreInfoBusiness.csv"))
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

                                            if (!Directory.Exists(Server.MapPath(GlobalVariables.TempFileProdSubCatPath + "/" + intTalId)))
                                                Directory.CreateDirectory(Server.MapPath(GlobalVariables.TempFileProdSubCatPath + "/" + intTalId));


                                            //Code to check whether Category names already exist in db.
                                            lngAmId = 0;
                                            if (Convert.ToString(drImgColVal[0]).Trim() != "")
                                            {
                                                //string strClass = Convert.ToString(drImgColVal[5]);
                                                int IsActive = 0;
                                                if (Convert.ToString(drImgColVal[12]) == "Y") { IsActive = 1; }

                                                int IsArrival = 0;
                                                if (Convert.ToString(drImgColVal[13]) == "Y") { IsActive = 1; }


                                                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                                                int intTalukaId = objTal.TalukaID;
                                                string strId = Convert.ToString(intTalukaId);
                                                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                                                DataTable dtCategoryid = SqlHelper.ReadTable("SELECT IM_bIntInfoId FROM Information_Master_17  WHERE [IM_bItIsActive] = 1 AND [IM_intInfoType]=1 AND [IM_vCharInfoName_En] = @Name ", conString, false,
                                                    SqlHelper.AddInParam("@Name", SqlDbType.VarChar, drImgColVal[0].ToString()));

                                                int intCategoryid = -1;
                                                if (dtCategoryid.Rows.Count > 0)
                                                {
                                                    DataRow rowCategoryId = dtCategoryid.Rows[0];
                                                    intCategoryid = Convert.ToInt32(rowCategoryId["IM_bIntInfoId"]);
                                                }
                                                else
                                                {
                                                    Div2.Attributes["class"] = "alert alert-info";
                                                    Div2.InnerHtml = "Information Name not exist.";
                                                    return;
                                                }
                                                //Code to check whether Category names already exist in db.

                                                string strQuery = "Select PC_bIntCategoryId from Product_Categories_17 where PC_vCharCatName = @Name  AND PC_bItIsActive=1";
                                                DataTable dtCatIdData = SqlHelper.ReadTable(strQuery, false,
                                                    SqlHelper.AddInParam("@Name", SqlDbType.VarChar, drImgColVal[1].ToString()));

                                                if (dtCatIdData.Rows.Count > 0)
                                                {
                                                    DataRow row = dtCatIdData.Rows[0];
                                                    lngAmId = Convert.ToInt32(row["PC_bIntCategoryId"]);
                                                    if (lngAmId == 0) lngAmId = -1;
                                                }
                                                else
                                                {
                                                    Div2.Attributes["class"] = "alert alert-info";
                                                    Div2.InnerHtml = "Product Category Name not exist.";
                                                    return;
                                                }


                                                strQuery = "Select [PSC_bIntSubCategoryId] from Product_Sub_Categories_17 where PSC_vCharSubCatName = @Name AND  PSC_bItIsActive=1";
                                                //strQuery = strQuery + " AND IM_bIntCatId = " + intCategoryid;
                                                //strQuery = strQuery + " AND PSC_bIntCategoryId = " + lngAmId;
                                                DataTable dtInfoTemp = SqlHelper.ReadTable(strQuery, false,
                                                    SqlHelper.AddInParam("@Name", SqlDbType.VarChar, drImgColVal[2].ToString()));
                                                long lngInfoidexisting = 0;
                                                if (dtInfoTemp.Rows.Count > 0)
                                                {
                                                    DataRow drInfoId = dtInfoTemp.Rows[0];
                                                    lngInfoidexisting = Convert.ToInt64(drInfoId["PSC_bIntSubCategoryId"]);
                                                }
                                                else
                                                {
                                                    Div2.Attributes["class"] = "alert alert-info";
                                                    Div2.InnerHtml = "Product SubCategory Name not exist.";
                                                    return;
                                                }

                                                strQuery = "select PM_bIntProdId from Product_Master_17 where PM_vCharProdName =@Name AND PM_bItIsActive=1";
                                                strQuery = strQuery + " AND PM_bIntCatId = " + lngAmId;
                                                strQuery = strQuery + " AND PM_bIntSubCatId = " + lngInfoidexisting;

                                                DataTable dtProd = SqlHelper.ReadTable(strQuery, false,
                                                    SqlHelper.AddInParam("@Name", SqlDbType.VarChar, drImgColVal[3].ToString()));
                                                long lngProdexist = 0;
                                                if (dtProd.Rows.Count > 0)
                                                {
                                                    DataRow drprod = dtProd.Rows[0];
                                                    lngProdexist = Convert.ToInt64(drprod["PM_bIntProdId"]);
                                                }



                                                string strError = "";
                                                if (strFileNm != null)
                                                {

                                                    string mStrFileExtension = Path.GetExtension(strFileNm);
                                                    if (mStrFileExtension != ".csv")
                                                    {
                                                        if (strFileNm == drImgColVal[13].ToString())
                                                        {
                                                            //strError = GlobalFunctions.ChkImageSize(Server.MapPath(strFileNm), 256, 256, 64, 64);
                                                            if (strError == "")
                                                            {

                                                                //                              GetImageSize(out intImgHeight, out  intImgWidth, lstImgOrginalPath.ElementAt(i).ToString());
                                                                //                              long ImageID = InsertUpdateImages(intTalukaId, 0, intPCatID, lstImgOrginalPath.ElementAt(i), lstImgDes.ElementAt(i), lstImgRegDes.ElementAt(i), Convert.ToInt32(lstImgDefault.ElementAt(i)), intImgWidth, intImgHeight);


                                                                string strFilepath = Path.GetFullPath(strFileNm);
                                                                strStoredFilepath = GetSafeFileNameOnLocation(strFilepath, GlobalVariables.TempFileProdSubCatPath + "/" + strId);
                                                                f.SaveAs(strStoredFilepath);

                                                                strStoredFilepath = String.Format("{0}/{1}", GlobalVariables.TempFileProdSubCatPath + "/" + strId, Path.GetFileName(strStoredFilepath));
                                                                lstCatFilePaths.Add(strStoredFilepath); //Store file path for future mapping before storing in database


                                                                DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProducts", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                                         SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                                         SqlHelper.AddInParam("@vcharProd_Name", SqlDbType.VarChar, Convert.ToString(drImgColVal[3])),
                                                                         SqlHelper.AddInParam("@nVarProd_Name", SqlDbType.NVarChar, Convert.ToString(drImgColVal[4])),
                                                                         SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, Convert.ToInt64(lngAmId)),
                                                                         SqlHelper.AddInParam("@bintSubCatID", SqlDbType.BigInt, Convert.ToInt64(lngInfoidexisting)),
                                                                         SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
                                                                         SqlHelper.AddInParam("@bItIsNewArrival", SqlDbType.Bit, IsArrival),
                                                                         SqlHelper.AddInParam("@bitfixMakingcharge", SqlDbType.Bit, Convert.ToByte(drImgColVal[8])),
                                                                         SqlHelper.AddInParam("@frmMakingcharge", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[9])),
                                                                    //SqlHelper.AddInParam("@dblProdActualPrice", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[5])),
                                                                    //SqlHelper.AddInParam("@dblProdDiscountPrice", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[6])),
                                                                    //SqlHelper.AddInParam("@dblProdDiscountPer", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[7])),
                                                                          SqlHelper.AddInParam("@dblWeight", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[5])),
                                                                          SqlHelper.AddInParam("@vcharPurity", SqlDbType.VarChar, Convert.ToString(drImgColVal[6])),
                                                                          SqlHelper.AddInParam("@intQuantity", SqlDbType.Int, Convert.ToInt32(drImgColVal[7])),
                                                                         SqlHelper.AddInParam("@vCharProdDesc", SqlDbType.VarChar, Convert.ToString(drImgColVal[10])),
                                                                          SqlHelper.AddInParam("@nVarProdDesc", SqlDbType.NVarChar, Convert.ToString(drImgColVal[11])),
                                                                         SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngProdexist));


                                                                long intProductID = 0;

                                                                if (dtCatData.Rows.Count > 0)
                                                                {
                                                                    DataRow dtRowProd = dtCatData.Rows[0];
                                                                    intProductID = Convert.ToInt64(dtRowProd["PM_bIntProdId"].ToString());
                                                                    // txtProductID.Text = Convert.ToString(intPCatID);
                                                                }

                                                                int intTalukaI = ((TalukaData)Session["TalukaDetails"]).TalukaID;

                                                                strQuery = "select [PI_bIntProdImgId] from Product_Images_17 where [PI_bIntProdId] = @ID";
                                                                DataTable dtprodimg = SqlHelper.ReadTable(strQuery, false,
                                                                    SqlHelper.AddInParam("@ID", SqlDbType.BigInt, lngProdexist));

                                                                if (dtprodimg.Rows.Count <= 0)
                                                                {

                                                                    GetImageSize(out intImgHeight, out  intImgWidth, strStoredFilepath);
                                                                    long ImageID = InsertUpdateImages(intTalukaI, 0, Convert.ToInt64(intProductID), strStoredFilepath, Convert.ToString(drImgColVal[12]), Convert.ToString(drImgColVal[13]), 1, intImgWidth, intImgHeight);

                                                                }
                                                                //GlobalFunctions.saveInsertUserAction("Product_Master", "[Product Master Insert]:Insertion of Product with ProductID " + Convert.ToInt64(txtProductID.Text) + "with Image ID: " + ImageID + "With ImagePath" + lstImgOrginalPath.ElementAt(i), intTalukaId, lngCompanyId, Request); //Call to user Action Log


                                                                //DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProductSubCatMaster", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                                //       SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                                //       SqlHelper.AddInParam("@vCharName_En ", SqlDbType.VarChar, Convert.ToString(drImgColVal[2])),
                                                                //       SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, lngAmId),
                                                                //       SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[3])),
                                                                //       SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
                                                                //       SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                                                                //       SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngInfoidexisting));


                                                            }
                                                            else
                                                            {
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
                                                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateProducts", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                                          SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                                          SqlHelper.AddInParam("@vcharProd_Name", SqlDbType.VarChar, Convert.ToString(drImgColVal[3])),
                                                                          SqlHelper.AddInParam("@nVarProd_Name", SqlDbType.NVarChar, Convert.ToString(drImgColVal[4])),
                                                                          SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, Convert.ToInt64(lngAmId)),
                                                                          SqlHelper.AddInParam("@bintSubCatID", SqlDbType.BigInt, Convert.ToInt64(lngInfoidexisting)),
                                                                          SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, IsActive),
                                                                          SqlHelper.AddInParam("@bItIsNewArrival", SqlDbType.Bit, IsArrival),
                                                                         SqlHelper.AddInParam("@bitfixMakingcharge", SqlDbType.Bit, Convert.ToByte(drImgColVal[8])),
                                                                         SqlHelper.AddInParam("@frmMakingcharge", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[9])),
                                                            //SqlHelper.AddInParam("@dblProdActualPrice", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[5])),
                                                            //SqlHelper.AddInParam("@dblProdDiscountPrice", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[6])),
                                                            //SqlHelper.AddInParam("@dblProdDiscountPer", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[7])),
                                                                          SqlHelper.AddInParam("@dblWeight", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[5])),
                                                                          SqlHelper.AddInParam("@vcharPurity", SqlDbType.VarChar, Convert.ToString(drImgColVal[6])),
                                                                          SqlHelper.AddInParam("@intQuantity", SqlDbType.Int, Convert.ToInt32(drImgColVal[7])),
                                                                         SqlHelper.AddInParam("@vCharProdDesc", SqlDbType.VarChar, Convert.ToString(drImgColVal[10])),
                                                                          SqlHelper.AddInParam("@nVarProdDesc", SqlDbType.NVarChar, Convert.ToString(drImgColVal[11])),
                                                                         SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngProdexist));
                                                    }
                                                }



                                            }
                                        }
                                    }

                                    //To insert Category when no images are selected

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
                                FillProductInfo(); //Call to display SubCategories.

                                if (blnExcelIsEmpty == false)
                                {
                                    Div2.Attributes["class"] = "alert alert-info";
                                    Div2.InnerHtml = "Product Added Successfully!!!";
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
                                Div2.InnerText = "Import1:" + exError.Message;
                                //long pLngErr = -1;
                                //if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                                //    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                                //pLngErr = GlobalFunctions.ReportError("btnImportonPassword_ServerClick", "BusinessSubCategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                                //Div2.Attributes["class"] = "alert alert-info blink-border";
                                //Div2.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
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
                catch (Exception exError)
                {
                    Div2.InnerText = "Import2:" + exError.Message;

                    //long pLngErr = -1;
                    //if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    //    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                    //pLngErr = GlobalFunctions.ReportError("btnImportonPassword_ServerClick", "BusinessSubCategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                    //Div2.Attributes["class"] = "alert alert-info blink-border";
                    //Div2.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
                }
            }
        }

        protected void ddlMakingcharge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMakingcharge.SelectedItem.Text == "Yes")
            {
                DivMakingCharge.Attributes["style"] = "display:block;";
            }
            else
            {
                DivMakingCharge.Attributes["style"] = "display:none;";
                txtMakingChrg.Text = "";
            }
        }

        protected void drpEditIsMakingChrgFixed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpEditIsMakingChrgFixed.SelectedItem.Text == "Yes")
            {
                //editDivMakingCharge.Attributes["style"] = "display:block;";
                lblMakingCharge.Attributes["style"] = "width: auto;display:initial;";
                lblArrival.Attributes["style"] = "margin-left: 120px;";
                editMakingCharge.Visible = true;
            }
            else
            {
                //editDivMakingCharge.Attributes["style"] = "display:none;";
                lblMakingCharge.Attributes["style"] = "width: auto;display:none;";
                lblArrival.Attributes["style"] = "margin-left: auto;";
                editMakingCharge.Visible = false;
                editMakingCharge.Text = "";
            }
        }
    }
}

