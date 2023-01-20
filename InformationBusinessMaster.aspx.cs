using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_CommTrex
{
    public partial class InformationBusinessMaster : System.Web.UI.Page
    {
        bool IsEmergencyUpdate = false;
        //PaidDetails objPd = new PaidDetails("", "", "");
        public static string strAmt = "";
        public static string strFrmDate = "";
        public static string strToDate = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["TalukaDetails"] != null)
                {
                    showInfoGrid();
                    FillCategoryComboForEdit();
                    LockControls(false);
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }



        //ADDED BY SSK dated 13-10-2015 to calculate no of business emergency entries  
        protected bool chkFreeandBusinessEntrycount(int pintMaxCount, long lngIsEmergency)
        {
            try
            {
                if (lngIsEmergency != 0)
                {
                    DataTable dtInfoData = SqlHelper.ReadTable("spChkEmergencyInfoCount", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                           SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                           SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                                           SqlHelper.AddInParam("@bitIsEmergency", SqlDbType.Bit, lngIsEmergency));
                    if (dtInfoData.Rows.Count > 0)
                    {
                        DataRow dtInfoRow = dtInfoData.Rows[0];
                        long lngFreeInfoCount = Convert.ToInt64(dtInfoRow["InfoCount"].ToString());

                        if (lngFreeInfoCount > pintMaxCount)
                        {
                            return (true);
                        }
                    }
                }
                return (false);
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("chkFreeandBusinessEntrycount", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
                return (false);
            }
        }

        protected void cmbCategorySelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubCategoryCombo(Convert.ToInt64(cmbCategorySelection.SelectedItem.Value));
        }

        protected void grdInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdInfo.EditIndex = e.NewEditIndex;
            showInfoGrid();
            DropDownList cmbCategory = (DropDownList)grdInfo.Rows[e.NewEditIndex].FindControl("cmbCategory");
            DropDownList cmbSubCategory = (DropDownList)grdInfo.Rows[e.NewEditIndex].FindControl("cmbSubCategory");
            Label lblCat = (Label)grdInfo.Rows[e.NewEditIndex].FindControl("lblCatName");
            Label lblSubCat = (Label)grdInfo.Rows[e.NewEditIndex].FindControl("lblSubCatName");

            cmbCategory.Visible = true;
            cmbSubCategory.Visible = true;
            lblCat.Visible = false;
            lblSubCat.Visible = false;
            ViewState["RowVal"] = e.NewEditIndex;
        }

        protected void grdInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                DropDownList drdInfoTypeid = (DropDownList)grdInfo.Rows[e.RowIndex].FindControl("drdModifyInfoType");
                Label lblInfoID = (Label)grdInfo.Rows[e.RowIndex].FindControl("lblInfoID");
                Label lblCat = (Label)grdInfo.Rows[e.RowIndex].FindControl("lblCatName");
                Label lblSubCat = (Label)grdInfo.Rows[e.RowIndex].FindControl("lblSubCatName");
                DropDownList cmbCategory = (DropDownList)grdInfo.Rows[e.RowIndex].FindControl("cmbCategory");
                DropDownList cmbSubCategory = (DropDownList)grdInfo.Rows[e.RowIndex].FindControl("cmbSubCategory");
                TextBox txtInfoName = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtInfoName");
                TextBox txtInfoRegName = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtInfoRegName");
                TextBox txtInfoAdd = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtInfoAdd");
                TextBox txtInfoRegAdd = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtInfoGrdRegAdd");
                TextBox txtEmail = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtEmail");
                TextBox txtPhone1 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtPhone1");
                TextBox txtPhone2 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtPhone2");
                TextBox txtPhone3 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtPhone3");
                TextBox txtLongitude = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtLongitude");
                TextBox txtLatitude = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtLatitude");
                TextBox txtPinCode = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtPinCode");
                TextBox txtRegPinCode = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtRegPinCode");

                TextBox txtCity = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtInfoCity");
                TextBox txtRegCity = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtInfoRegCity");

                TextBox txtExtraLabel1 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabel1");
                TextBox txtExtraLabelReg1 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelReg1");
                TextBox txtExtraLabelVal1 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelVal1");
                TextBox txtExtraLabelRegVal1 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelRegVal1");

                TextBox txtExtraLabel2 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabel2");
                TextBox txtExtraLabelReg2 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelReg2");
                TextBox txtExtraLabelVal2 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelVal2");
                TextBox txtExtraLabelRegVal2 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelRegVal2");

                TextBox txtExtraLabel3 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabel3");
                TextBox txtExtraLabelReg3 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelReg3");
                TextBox txtExtraLabelVal3 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelVal3");
                TextBox txtExtraLabelRegVal3 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelRegVal3");

                TextBox txtExtraLabel4 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabel4");
                TextBox txtExtraLabelReg4 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelReg4");
                TextBox txtExtraLabelVal4 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelVal4");
                TextBox txtExtraLabelRegVal4 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelRegVal4");

                TextBox txtExtraLabel5 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabel5");
                TextBox txtExtraLabelReg5 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelReg5");
                TextBox txtExtraLabelVal5 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelVal5");
                TextBox txtExtraLabelRegVal5 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelRegVal5");

                TextBox txtExtraLabel6 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabel6");
                TextBox txtExtraLabelReg6 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelReg6");
                TextBox txtExtraLabelVal6 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelVal6");
                TextBox txtExtraLabelRegVal6 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelRegVal6");

                TextBox txtExtraLabel7 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabel7");
                TextBox txtExtraLabelReg7 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelReg7");
                TextBox txtExtraLabelVal7 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelVal7");
                TextBox txtExtraLabelRegVal7 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelRegVal7");

                TextBox txtExtraLabel8 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabel8");
                TextBox txtExtraLabelReg8 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelReg8");
                TextBox txtExtraLabelVal8 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelVal8");
                TextBox txtExtraLabelRegVal8 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelRegVal8");


                TextBox txtExtraLabel9 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabel9");
                TextBox txtExtraLabelReg9 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelReg9");
                TextBox txtExtraLabelVal9 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelVal9");
                TextBox txtExtraLabelRegVal9 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelRegVal9");


                TextBox txtExtraLabel10 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabel10");
                TextBox txtExtraLabelReg10 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelReg10");
                TextBox txtExtraLabelVal10 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelVal10");
                TextBox txtExtraLabelRegVal10 = (TextBox)grdInfo.Rows[e.RowIndex].FindControl("txtExtraLabelRegVal10");

                string strValidate = Validate(txtInfoName.Text, txtInfoRegName.Text, txtPhone1.Text, txtPhone2.Text, txtPhone3.Text, txtInfoAdd.Text, txtEmail.Text, cmbCategory.SelectedIndex, txtLatitude.Text, txtLongitude.Text, Convert.ToInt64(lblInfoID.Text), Convert.ToInt64(cmbCategory.SelectedItem.Value), Convert.ToInt64(cmbSubCategory.SelectedItem.Value), Convert.ToInt64(drdInformationType.SelectedItem.Value));
                if (strValidate == "")
                {
                    //For Business information max limit is 10
                    if (chkFreeandBusinessEntrycount(10, Convert.ToInt64(drdInfoTypeid.SelectedItem.Value)) == false)
                    {
                        string strUrl = txtModifyUrl.Text.Trim();
                        DataTable dtInsertedData = SqlHelper.ReadTable("spInsertUpdateInfoMaster1", true,
                                                                 SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                                 SqlHelper.AddInParam("@IM_bIntSubCatId", SqlDbType.BigInt, Convert.ToInt64(cmbSubCategory.SelectedItem.Value)),
                                                                  SqlHelper.AddInParam("@IM_bIntCatId", SqlDbType.BigInt, Convert.ToInt64(cmbCategory.SelectedItem.Value)),
                                                                  SqlHelper.AddInParam("@IM_IntCharInfoType", SqlDbType.Int, 1),
                                                                   SqlHelper.AddInParam("@IM_vCharInfoName_En", SqlDbType.VarChar, (txtInfoName.Text)),
                                                                    SqlHelper.AddInParam("@IM_nVarInfoName_Reg", SqlDbType.NVarChar, (txtInfoRegName.Text)),
                                                                      SqlHelper.AddInParam("@IM_vCharInfoAdd_En", SqlDbType.VarChar, txtInfoAdd.Text),
                                                                    SqlHelper.AddInParam("@IM_nVarInfoAdd_Reg", SqlDbType.NVarChar, txtAddReg.Text),
                                                                     SqlHelper.AddInParam("@IM_vCharInfoEmail", SqlDbType.VarChar, txtEmail.Text),
                                                                      SqlHelper.AddInParam("@IM_vCharInfoPhone1", SqlDbType.VarChar, txtPhone1.Text),
                                                                       SqlHelper.AddInParam("@IM_vCharInfoPhone2", SqlDbType.VarChar, txtPhone2.Text),
                                                                        SqlHelper.AddInParam("@IM_vCharInfoPhone3", SqlDbType.VarChar, txtPhone3.Text),
                                                                         SqlHelper.AddInParam("@IM_decLatitude", SqlDbType.Decimal, txtLatitude.Text),
                                                                       SqlHelper.AddInParam("@IM_decLongitude", SqlDbType.Decimal, txtLongitude.Text),
                                                                       SqlHelper.AddInParam("@IM_vCharPincode_En", SqlDbType.VarChar, txtPinCode.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarPincode_Reg", SqlDbType.NVarChar, txtRegPinCode.Text),

                                                                       SqlHelper.AddInParam("@IM_vCharCity_En", SqlDbType.VarChar, txtCity.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarCity_Reg", SqlDbType.NVarChar, txtRegCity.Text),

                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraLabel1_En", SqlDbType.VarChar, txtExtraLabel1.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraLabel1_Reg", SqlDbType.NVarChar, txtExtraLabelReg1.Text),
                                                                       SqlHelper.AddInParam("@IM_vCharInfoExtraValue1_En", SqlDbType.VarChar, txtExtraLabelVal1.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraValue1_Reg", SqlDbType.NVarChar, txtExtraLabelRegVal1.Text),


                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraLabel2_En", SqlDbType.VarChar, txtExtraLabel2.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraLabel2_Reg", SqlDbType.NVarChar, txtExtraLabelReg2.Text),
                                                                       SqlHelper.AddInParam("@IM_vCharInfoExtraValue2_En", SqlDbType.VarChar, txtExtraLabelVal2.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraValue2_Reg", SqlDbType.NVarChar, txtExtraLabelRegVal2.Text),

                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraLabel3_En", SqlDbType.VarChar, txtExtraLabel3.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraLabel3_Reg", SqlDbType.NVarChar, txtExtraLabelReg3.Text),
                                                                       SqlHelper.AddInParam("@IM_vCharInfoExtraValue3_En", SqlDbType.VarChar, txtExtraLabelVal3.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraValue3_Reg", SqlDbType.NVarChar, txtExtraLabelRegVal3.Text),


                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraLabel4_En", SqlDbType.VarChar, txtExtraLabel4.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraLabel4_Reg", SqlDbType.NVarChar, txtExtraLabelReg4.Text),
                                                                       SqlHelper.AddInParam("@IM_vCharInfoExtraValue4_En", SqlDbType.VarChar, txtExtraLabelVal4.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraValue4_Reg", SqlDbType.NVarChar, txtExtraLabelRegVal4.Text),


                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraLabel5_En", SqlDbType.VarChar, txtExtraLabel5.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraLabel5_Reg", SqlDbType.NVarChar, txtExtraLabelReg5.Text),
                                                                       SqlHelper.AddInParam("@IM_vCharInfoExtraValue5_En", SqlDbType.VarChar, txtExtraLabelVal5.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraValue5_Reg", SqlDbType.NVarChar, txtExtraLabelRegVal5.Text),

                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraLabel6_En", SqlDbType.VarChar, txtExtraLabel6.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraLabel6_Reg", SqlDbType.NVarChar, txtExtraLabelReg6.Text),
                                                                       SqlHelper.AddInParam("@IM_vCharInfoExtraValue6_En", SqlDbType.VarChar, txtExtraLabelVal6.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraValue6_Reg", SqlDbType.NVarChar, txtExtraLabelRegVal6.Text),

                                                                          SqlHelper.AddInParam("@IM_vCharInfoExtraLabel7_En", SqlDbType.VarChar, txtExtraLabel7.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraLabel7_Reg", SqlDbType.NVarChar, txtExtraLabelReg7.Text),
                                                                       SqlHelper.AddInParam("@IM_vCharInfoExtraValue7_En", SqlDbType.VarChar, txtExtraLabelVal7.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraValue7_Reg", SqlDbType.NVarChar, txtExtraLabelRegVal7.Text),

                                                                       SqlHelper.AddInParam("@IM_vCharInfoExtraLabel8_En", SqlDbType.VarChar, txtExtraLabel8.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraLabel8_Reg", SqlDbType.NVarChar, txtExtraLabelReg8.Text),
                                                                       SqlHelper.AddInParam("@IM_vCharInfoExtraValue8_En", SqlDbType.VarChar, txtExtraLabelVal8.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraValue8_Reg", SqlDbType.NVarChar, txtExtraLabelRegVal8.Text),

                                                                       SqlHelper.AddInParam("@IM_vCharInfoExtraLabel9_En", SqlDbType.VarChar, txtExtraLabel9.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraLabel9_Reg", SqlDbType.NVarChar, txtExtraLabelReg9.Text),
                                                                       SqlHelper.AddInParam("@IM_vCharInfoExtraValue9_En", SqlDbType.VarChar, txtExtraLabelVal9.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraValue9_Reg", SqlDbType.NVarChar, txtExtraLabelRegVal9.Text),

                                                                       SqlHelper.AddInParam("@IM_vCharInfoExtraLabel10_En", SqlDbType.VarChar, txtExtraLabel10.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraLabel10_Reg", SqlDbType.NVarChar, txtExtraLabelReg10.Text),
                                                                       SqlHelper.AddInParam("@IM_vCharInfoExtraValue10_En", SqlDbType.VarChar, txtExtraLabelVal10.Text),
                                                                       SqlHelper.AddInParam("@IM_nVarInforExtraValue10_Reg", SqlDbType.NVarChar, txtExtraLabelRegVal10.Text),
                                                                       SqlHelper.AddInParam("@IM_bitIsActive", SqlDbType.Bit, 1),
                                                                       SqlHelper.AddInParam("@IM_vCharUrl", SqlDbType.VarChar, strUrl),
                                                                       SqlHelper.AddInParam("@IM_bitIsEmergency", SqlDbType.Bit, Convert.ToInt64(drdInfoTypeid.SelectedItem.Value)),
                                                                       SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, Convert.ToInt64(lblInfoID.Text)));

                        grdInfo.EditIndex = -1;
                        cmbCategory.Visible = false;
                        cmbSubCategory.Visible = false;
                        lblCat.Visible = true;
                        lblCat.Visible = true;

                        showInfoGrid();

                        //Code for maintaining log of Emergency 
                        //---------------------------------------------------------------------------------
                        if (dtInsertedData.Rows.Count > 0)
                        {
                            if (IsEmergencyUpdate == true || Convert.ToInt64(hfInfotype.Value) == 1)
                            {
                                string strQuery = " SELECT EIML_bIntId FROM EmergencyInformationModificationLog where EIML_bIntTalukaId = " + Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID);
                                DataTable dtselectEnergencyid = SqlHelper.ReadTable(strQuery, false);
                                long lngEmergencyid = 0;
                                if (dtselectEnergencyid.Rows.Count > 0)
                                {
                                    //Update emergency information version for taluka
                                    lngEmergencyid = Convert.ToInt64(dtselectEnergencyid.Rows[0][0]);
                                    dtselectEnergencyid = SqlHelper.ReadTable("SP_insertEmergencyInfoLog", true,
                                         SqlHelper.AddInParam("@pintTalukaid", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                         SqlHelper.AddInParam("@pintEmergencyInfo", SqlDbType.BigInt, Convert.ToInt64("0")),
                                         SqlHelper.AddInParam("@pintEmergencyId", SqlDbType.BigInt, lngEmergencyid));
                                }
                                else
                                {
                                    //Insert new emergency information version for taluka
                                    dtselectEnergencyid = SqlHelper.ReadTable("SP_insertEmergencyInfoLog", true,
                                                SqlHelper.AddInParam("@pintTalukaid", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                SqlHelper.AddInParam("@pintEmergencyInfo", SqlDbType.BigInt, Convert.ToInt64("1")),
                                                SqlHelper.AddInParam("@pintEmergencyId", SqlDbType.BigInt, lngEmergencyid));
                                }
                            }
                        }
                        //---------------------------------------------------------------------------------



                        #region oldmergencycode
                        //Code for maintaining log of Emergency 
                        //---------------------------------------------------------------------------------
                        //if (dtInsertedData.Rows.Count > 0)
                        //{
                        //    string strQuery = " SELECT EIML_bIntId FROM EmergencyInformationModificationLog where EIML_bIntTalukaId = " + Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID);
                        //    DataTable dtselectEnergencyid = SqlHelper.ReadTable(strQuery, false);
                        //    long lngEmergencyid = 0;
                        //    if (dtselectEnergencyid.Rows.Count > 0)
                        //    {
                        //        //Update emergency information version for taluka
                        //        lngEmergencyid = Convert.ToInt64(dtselectEnergencyid.Rows[0][0]);
                        //        dtselectEnergencyid = SqlHelper.ReadTable("SP_insertEmergencyInfoLog", true,
                        //             SqlHelper.AddInParam("@pintTalukaid", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                        //             SqlHelper.AddInParam("@pintEmergencyInfo", SqlDbType.BigInt, Convert.ToInt64("0")),
                        //             SqlHelper.AddInParam("@pintEmergencyId", SqlDbType.BigInt, lngEmergencyid));
                        //    }
                        //    else
                        //    {
                        //        //Insert new emergency information version for taluka
                        //        dtselectEnergencyid = SqlHelper.ReadTable("SP_insertEmergencyInfoLog", true,
                        //                    SqlHelper.AddInParam("@pintTalukaid", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                        //                    SqlHelper.AddInParam("@pintEmergencyInfo", SqlDbType.BigInt, Convert.ToInt64("1")),
                        //                    SqlHelper.AddInParam("@pintEmergencyId", SqlDbType.BigInt, lngEmergencyid));
                        //    }
                        //}
                        //---------------------------------------------------------------------------------
                        #endregion oldmergencycode

                        SetProductsUpdateMessage(true, "Information Updated Successfully !!!");
                    }
                    else
                    {
                        SetProductsUpdateMessage(true, "Maximum limit for Emergency Information is 10 !!!");
                    }
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
                pLngErr = GlobalFunctions.ReportError("grdInfo_RowUpdating", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        protected void grdInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdInfo.EditIndex = -1;
            DropDownList cmbCategory = (DropDownList)grdInfo.Rows[Convert.ToInt16(ViewState["RowVal"])].FindControl("cmbCategory");
            DropDownList cmbSubCategory = (DropDownList)grdInfo.Rows[Convert.ToInt16(ViewState["RowVal"])].FindControl("cmbSubCategory");
            Label lblCat = (Label)grdInfo.Rows[Convert.ToInt16(ViewState["RowVal"])].FindControl("lblCatName");
            Label lblSubCat = (Label)grdInfo.Rows[Convert.ToInt16(ViewState["RowVal"])].FindControl("lblSubCatName");

            cmbCategory.Visible = false;
            cmbSubCategory.Visible = false;
            lblCat.Visible = true;
            lblSubCat.Visible = true;

            showInfoGrid();
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Attributes["btn-action"] == "Save")
                {
                    //if (ChkEntryCount())
                    //{
                    if (drpSubCategorySelection.SelectedIndex == 0)
                    {
                        drpSubCategorySelection.SelectedIndex = -1;
                    }
                    string strValidate = Validate(txtInfoName.Text, txtInfoRegName.Text, txtPhone1.Text, txtPhone2.Text, txtPhone3.Text, txtInfoAdd.Text, txtEmail.Text, cmbCategorySelection.SelectedIndex, txtLatitude.Text, txtLongitude.Text, 0, Convert.ToInt64(cmbCategorySelection.SelectedItem.Value), Convert.ToInt64(drpSubCategorySelection.SelectedItem.Value), Convert.ToInt64(drdInformationType.SelectedItem.Value));

                    if (strValidate == "")
                    {

                        if (txtLatitude.Text == "")
                        {
                            txtLatitude.Text = "0";

                        }
                        if (txtLongitude.Text == "")
                        {
                            txtLongitude.Text = "0";
                        }

                        //For Business information max limit is 10
                        if (chkFreeandBusinessEntrycount(10, Convert.ToInt64(drdInformationType.SelectedItem.Value)) == false)
                        {
                            //Added for url addition for information 
                            string strUrl = txtUrl.Text.Trim();

                            DataTable dtInsertedData = SqlHelper.ReadTable("spInsertUpdateInfoMaster1", true,
                                                                  SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                                    SqlHelper.AddInParam("@IM_bIntSubCatId", SqlDbType.BigInt, Convert.ToInt64(drpSubCategorySelection.SelectedItem.Value)),
                                                                   SqlHelper.AddInParam("@IM_bIntCatId", SqlDbType.BigInt, Convert.ToInt64(cmbCategorySelection.SelectedItem.Value)),
                                                                   SqlHelper.AddInParam("@IM_IntCharInfoType", SqlDbType.Int, 1),
                                                                    SqlHelper.AddInParam("@IM_vCharInfoName_En", SqlDbType.VarChar, (txtInfoName.Text)),
                                                                     SqlHelper.AddInParam("@IM_nVarInfoName_Reg", SqlDbType.NVarChar, (txtInfoRegName.Text)),
                                                                       SqlHelper.AddInParam("@IM_vCharInfoAdd_En", SqlDbType.VarChar, txtInfoAdd.Text),
                                                                     SqlHelper.AddInParam("@IM_nVarInfoAdd_Reg", SqlDbType.NVarChar, txtAddReg.Text),
                                                                      SqlHelper.AddInParam("@IM_vCharInfoEmail", SqlDbType.VarChar, txtEmail.Text),
                                                                       SqlHelper.AddInParam("@IM_vCharInfoPhone1", SqlDbType.VarChar, txtPhone1.Text),
                                                                        SqlHelper.AddInParam("@IM_vCharInfoPhone2", SqlDbType.VarChar, txtPhone2.Text),
                                                                         SqlHelper.AddInParam("@IM_vCharInfoPhone3", SqlDbType.VarChar, txtPhone3.Text),
                                                                         SqlHelper.AddInParam("@IM_decLatitude", SqlDbType.Decimal, Convert.ToDecimal(txtLatitude.Text)),
                                                                        SqlHelper.AddInParam("@IM_decLongitude", SqlDbType.Decimal, Convert.ToDecimal(txtLongitude.Text)),
                                                                        SqlHelper.AddInParam("@IM_vCharPincode_En", SqlDbType.VarChar, txtPinCode.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarPincode_Reg", SqlDbType.NVarChar, txtRegionalPinCode.Text),

                                                                         SqlHelper.AddInParam("@IM_vCharCity_En", SqlDbType.VarChar, txtCity.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarCity_Reg", SqlDbType.NVarChar, txtRegCity.Text),
                                                                         SqlHelper.AddInParam("@IM_vCharInfoExtraLabel1_En", SqlDbType.VarChar, txtExtraField_1.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraLabel1_Reg", SqlDbType.NVarChar, txtExtraFieldReg_1.Text),
                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraValue1_En", SqlDbType.VarChar, txtExtraFieldValue_1.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraValue1_Reg", SqlDbType.NVarChar, txtExtraFieldValueReg_1.Text),


                                                                         SqlHelper.AddInParam("@IM_vCharInfoExtraLabel2_En", SqlDbType.VarChar, txtExtraField_2.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraLabel2_Reg", SqlDbType.NVarChar, txtExtraFieldReg_2.Text),
                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraValue2_En", SqlDbType.VarChar, txtExtraFieldValue_2.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraValue2_Reg", SqlDbType.NVarChar, txtExtraFieldValueReg_2.Text),

                                                                         SqlHelper.AddInParam("@IM_vCharInfoExtraLabel3_En", SqlDbType.VarChar, txtExtraField_3.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraLabel3_Reg", SqlDbType.NVarChar, txtExtraFieldReg_3.Text),
                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraValue3_En", SqlDbType.VarChar, txtExtraFieldValue_3.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraValue3_Reg", SqlDbType.NVarChar, txtExtraFieldValueReg_3.Text),


                                                                         SqlHelper.AddInParam("@IM_vCharInfoExtraLabel4_En", SqlDbType.VarChar, txtExtraField_4.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraLabel4_Reg", SqlDbType.NVarChar, txtExtraFieldReg_4.Text),
                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraValue4_En", SqlDbType.VarChar, txtExtraFieldValue_4.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraValue4_Reg", SqlDbType.NVarChar, txtExtraFieldValueReg_4.Text),

                                                                         SqlHelper.AddInParam("@IM_vCharInfoExtraLabel5_En", SqlDbType.VarChar, txtExtraField_5.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraLabel5_Reg", SqlDbType.NVarChar, txtExtraFieldReg_5.Text),
                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraValue5_En", SqlDbType.VarChar, txtExtraFieldValue_5.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraValue5_Reg", SqlDbType.NVarChar, txtExtraFieldValueReg_5.Text),

                                                                         SqlHelper.AddInParam("@IM_vCharInfoExtraLabel6_En", SqlDbType.VarChar, txtExtraField_6.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraLabel6_Reg", SqlDbType.NVarChar, txtExtraFieldReg_6.Text),
                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraValue6_En", SqlDbType.VarChar, txtExtraFieldValue_6.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraValue6_Reg", SqlDbType.NVarChar, txtExtraFieldValueReg_6.Text),

                                                                         SqlHelper.AddInParam("@IM_vCharInfoExtraLabel7_En", SqlDbType.VarChar, txtExtraField_7.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraLabel7_Reg", SqlDbType.NVarChar, txtExtraFieldReg_7.Text),
                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraValue7_En", SqlDbType.VarChar, txtExtraFieldValue_7.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraValue7_Reg", SqlDbType.NVarChar, txtExtraFieldValueReg_7.Text),

                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraLabel8_En", SqlDbType.VarChar, txtExtraField_8.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraLabel8_Reg", SqlDbType.NVarChar, txtExtraFieldReg_8.Text),
                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraValue8_En", SqlDbType.VarChar, txtExtraFieldValue_8.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraValue8_Reg", SqlDbType.NVarChar, txtExtraFieldValueReg_8.Text),

                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraLabel9_En", SqlDbType.VarChar, txtExtraField_9.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraLabel9_Reg", SqlDbType.NVarChar, txtExtraFieldReg_9.Text),
                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraValue9_En", SqlDbType.VarChar, txtExtraFieldValue_9.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraValue9_Reg", SqlDbType.NVarChar, txtExtraFieldValueReg_9.Text),

                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraLabel10_En", SqlDbType.VarChar, txtExtraField_10.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraLabel10_Reg", SqlDbType.NVarChar, txtExtraFieldReg_10.Text),
                                                                        SqlHelper.AddInParam("@IM_vCharInfoExtraValue10_En", SqlDbType.VarChar, txtExtraFieldValue_10.Text),
                                                                        SqlHelper.AddInParam("@IM_nVarInforExtraValue10_Reg", SqlDbType.NVarChar, txtExtraFieldValueReg_10.Text),
                                                                        SqlHelper.AddInParam("@IM_bitIsActive", SqlDbType.Bit, 1),
                                                                        SqlHelper.AddInParam("@IM_vCharUrl", SqlDbType.VarChar, strUrl),
                                                                        SqlHelper.AddInParam("@IM_bitIsEmergency", SqlDbType.Bit, Convert.ToInt64(drdInformationType.SelectedItem.Value)),
                                                                        SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, 0));
                            DataRow drInfo = dtInsertedData.Rows[0];
                            txtInformationID.Text = drInfo["IM_bIntInfoId"].ToString();


                            //Code added by SSK for payment details updation
                            //----------------------------------------------------------------
                            int intIsPaid = Convert.ToInt32(drIsPaid.SelectedValue.ToString());
                            double dblAmount = 0;
                            string dtFrmDate = "";
                            string dtToDate = "";

                            if (intIsPaid == 1)
                            {
                                dblAmount = Convert.ToDouble(txtAmount.Text);
                                dtFrmDate = txtFromdate.Text;
                                dtToDate = txtTodate.Text;
                            }
                            else
                            {
                                intIsPaid = 0;
                            }

                            string strUpdatePayment = " UPDATE Information_Master_" + Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID);
                            strUpdatePayment = strUpdatePayment + " SET IM_bitIsPaid = " + intIsPaid;
                            strUpdatePayment = strUpdatePayment + " ,IM_decAmt = " + dblAmount;
                            strUpdatePayment = strUpdatePayment + " ,IM_dtFromDate = CAST(SWITCHOFFSET('" + dtFrmDate + "', '+05:30') As DATETIME)";
                            strUpdatePayment = strUpdatePayment + " ,IM_dtToDate = CAST(SWITCHOFFSET('" + dtToDate + "', '+05:30') As DATETIME)";
                            strUpdatePayment = strUpdatePayment + " WHERE IM_bIntInfoId = " + Convert.ToInt32(drInfo["IM_bIntInfoId"]);

                            int intNoofRows = SqlHelper.UpdateDatabase(strUpdatePayment);


                            //----------------------------------------------------------------

                            showInfoGrid();

                            //Code for maintaining log of Emergency 
                            //---------------------------------------------------------------------------------
                            if (dtInsertedData.Rows.Count > 0 && Convert.ToInt64(drdInformationType.SelectedItem.Value) == 1)
                            {
                                string strQuery = " SELECT EIML_bIntId FROM EmergencyInformationModificationLog where EIML_bIntTalukaId = " + Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID);
                                DataTable dtselectEnergencyid = SqlHelper.ReadTable(strQuery, false);
                                long lngEmergencyid = 0;
                                if (dtselectEnergencyid.Rows.Count > 0)
                                {
                                    //Update emergency information version for taluka
                                    lngEmergencyid = Convert.ToInt64(dtselectEnergencyid.Rows[0][0]);
                                    dtselectEnergencyid = SqlHelper.ReadTable("SP_insertEmergencyInfoLog", true,
                                         SqlHelper.AddInParam("@pintTalukaid", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                         SqlHelper.AddInParam("@pintEmergencyInfo", SqlDbType.BigInt, Convert.ToInt64("0")),
                                         SqlHelper.AddInParam("@pintEmergencyId", SqlDbType.BigInt, lngEmergencyid));
                                }
                                else
                                {
                                    //Insert new emergency information version for taluka
                                    dtselectEnergencyid = SqlHelper.ReadTable("SP_insertEmergencyInfoLog", true,
                                                SqlHelper.AddInParam("@pintTalukaid", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                SqlHelper.AddInParam("@pintEmergencyInfo", SqlDbType.BigInt, Convert.ToInt64("1")),
                                                SqlHelper.AddInParam("@pintEmergencyId", SqlDbType.BigInt, lngEmergencyid));
                                }
                            }
                            //---------------------------------------------------------------------------------


                            #region oldemergencycode
                            //Code for maintaining log of Emergency 
                            //---------------------------------------------------------------------------------
                            //if (dtInsertedData.Rows.Count > 0)
                            //{
                            //    string strQuery = " SELECT EIML_bIntId FROM EmergencyInformationModificationLog where EIML_bIntTalukaId = " + Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID);
                            //    DataTable dtselectEnergencyid = SqlHelper.ReadTable(strQuery, false);
                            //    long lngEmergencyid = 0;
                            //    if (dtselectEnergencyid.Rows.Count > 0)
                            //    {
                            //        //Update emergency information version for taluka
                            //        lngEmergencyid = Convert.ToInt64(dtselectEnergencyid.Rows[0][0]);
                            //        dtselectEnergencyid = SqlHelper.ReadTable("SP_insertEmergencyInfoLog", true,
                            //             SqlHelper.AddInParam("@pintTalukaid", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                            //             SqlHelper.AddInParam("@pintEmergencyInfo", SqlDbType.BigInt, Convert.ToInt64("0")),
                            //             SqlHelper.AddInParam("@pintEmergencyId", SqlDbType.BigInt, lngEmergencyid));
                            //    }
                            //    else
                            //    {
                            //        //Insert new emergency information version for taluka
                            //        dtselectEnergencyid = SqlHelper.ReadTable("SP_insertEmergencyInfoLog", true,
                            //                    SqlHelper.AddInParam("@pintTalukaid", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                            //                    SqlHelper.AddInParam("@pintEmergencyInfo", SqlDbType.BigInt, Convert.ToInt64("1")),
                            //                    SqlHelper.AddInParam("@pintEmergencyId", SqlDbType.BigInt, lngEmergencyid));
                            //    }
                            //}
                            //---------------------------------------------------------------------------------
                            #endregion oldemergencycode

                            btnSave.Attributes["btn-action"] = "New";
                            btnSave.InnerHtml = "<i class=\"fa fa-plus-square\"></i> New";
                            SetMessage(true, "Information Saved Successfully!!!");
                        }
                        else
                        {
                            SetMessage(true, "Maximum limit for Emergency Information is 10 !!!");
                        }
                    }

                    else
                    {
                        SetMessage(true, strValidate);
                    }
                    // }//

                    //else
                    //  {
                    // SetMessage(true, "Entry Limit Exceeded !!!!");
                    // }//
                }
                else
                {
                    if (ChkEntryCount())
                    {
                        btnSave.Attributes["btn-action"] = "Save";
                        btnSave.InnerHtml = "<i class=\"fa fa-floppy-o\"></i> Save";
                        ClearControls();
                        FillCategoryCombo();
                        LockControls(true);
                        SetMessage(true, "Press Save to add Information!!!");
                    }
                    else
                    {
                        SetMessage(true, "Entry Limit Exceeded !!!!");
                    }
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnSave_ServerClick", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }
        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList cmbCategory = (DropDownList)grdInfo.Rows[Convert.ToInt16(ViewState["RowVal"])].FindControl("cmbCategory");
                DropDownList cmbSubCategory = (DropDownList)grdInfo.Rows[Convert.ToInt16(ViewState["RowVal"])].FindControl("cmbSubCategory");
                DataTable dtInfoData = SqlHelper.ReadTable("spGetSubCategoryDetailsForInfo", true,
                                  SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                   SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, cmbCategory.SelectedItem.Value),
                                   SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1));


                //DataTable dtInfoData = SqlHelper.ReadTable("spGetSubCategoryDetailsForInfo", true,
                //                                           SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                //                                    SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt,cmbCategory.SelectedItem.Value));
                cmbSubCategory.Items.Clear();
                if (dtInfoData.Rows.Count > 0)
                {
                    cmbSubCategory.AppendDataBoundItems = true;
                    cmbSubCategory.Items.Insert(0, new ListItem("<< Select Sub Category >>", "-1"));
                    cmbSubCategory.DataSource = dtInfoData;
                    cmbSubCategory.DataTextField = "SCM_vCharName_En";
                    cmbSubCategory.DataValueField = "SCM_bIntSubCatId";
                    cmbSubCategory.DataBind();
                    cmbSubCategory.SelectedIndex = 0;
                }
                else
                {
                    cmbSubCategory.AppendDataBoundItems = true;
                    cmbSubCategory.Items.Insert(0, new ListItem("<< No SubCategory found  >>", "-1"));
                    cmbSubCategory.SelectedIndex = 0;
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("cmbCategory_SelectedIndexChanged", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        protected void btnDeleteInfo_ServerClick(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDeletedData = SqlHelper.ReadTable("spDeleteInfo", true,
                                         SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                         SqlHelper.AddInParam("@IM_bIntInfoId", SqlDbType.BigInt, Convert.ToInt64(txtInfoHidden.Value)),
                                        SqlHelper.AddInParam("@IM_intInfoType", SqlDbType.Int, 1));

                SetProductsUpdateMessage(true, "Information Deleted SuccessFully!!!");
                showInfoGrid();
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

        protected void showInfoGrid()
        {
            try
            {
                DataTable dtInfoData = SqlHelper.ReadTable("spGetInfoDetails1", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                     SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                      SqlHelper.AddInParam("@intInfomationType", SqlDbType.Int, 1));
                grdInfo.DataSource = dtInfoData;
                grdInfo.DataBind();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showInfoGrid", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        protected void LockControls(bool pBoolean)
        {
            //txtInformationID.Enabled = pBoolean;
            txtInfoName.Enabled = pBoolean;
            txtInfoRegName.Enabled = pBoolean;

            cmbCategorySelection.Enabled = pBoolean;
            drpSubCategorySelection.Enabled = pBoolean;
            txtInfoAdd.Enabled = pBoolean;
            txtAddReg.Enabled = pBoolean;
            txtEmail.Enabled = pBoolean;
            txtPhone1.Enabled = pBoolean;
            txtPhone2.Enabled = pBoolean;
            txtPhone3.Enabled = pBoolean;
            txtLatitude.Enabled = pBoolean;
            txtLongitude.Enabled = pBoolean;
            txtPinCode.Enabled = pBoolean;
            txtRegionalPinCode.Enabled = pBoolean;
            txtRegCity.Enabled = pBoolean;
            txtCity.Enabled = pBoolean;

            txtExtraField_1.Enabled = pBoolean;
            txtExtraFieldReg_1.Enabled = pBoolean;
            txtExtraFieldValue_1.Enabled = pBoolean;

            txtExtraField_2.Enabled = pBoolean;
            txtExtraFieldReg_2.Enabled = pBoolean;
            txtExtraFieldValue_2.Enabled = pBoolean;

            txtExtraField_3.Enabled = pBoolean;
            txtExtraFieldReg_3.Enabled = pBoolean;
            txtExtraFieldValue_3.Enabled = pBoolean;

            txtExtraField_4.Enabled = pBoolean;
            txtExtraFieldReg_4.Enabled = pBoolean;
            txtExtraFieldValue_4.Enabled = pBoolean;

            txtExtraField_5.Enabled = pBoolean;
            txtExtraFieldReg_5.Enabled = pBoolean;
            txtExtraFieldValue_5.Enabled = pBoolean;

            txtExtraField_6.Enabled = pBoolean;
            txtExtraFieldReg_6.Enabled = pBoolean;
            txtExtraFieldValue_6.Enabled = pBoolean;

            txtExtraField_7.Enabled = pBoolean;
            txtExtraFieldReg_7.Enabled = pBoolean;
            txtExtraFieldValue_7.Enabled = pBoolean;

            txtExtraField_8.Enabled = pBoolean;
            txtExtraFieldReg_8.Enabled = pBoolean;
            txtExtraFieldValue_8.Enabled = pBoolean;

            txtExtraField_9.Enabled = pBoolean;
            txtExtraFieldReg_9.Enabled = pBoolean;
            txtExtraFieldValue_9.Enabled = pBoolean;

            txtExtraField_10.Enabled = pBoolean;
            txtExtraFieldReg_10.Enabled = pBoolean;
            txtExtraFieldValue_10.Enabled = pBoolean;

            txtExtraFieldValueReg_1.Enabled = pBoolean;
            txtExtraFieldValueReg_2.Enabled = pBoolean;
            txtExtraFieldValueReg_3.Enabled = pBoolean;
            txtExtraFieldValueReg_4.Enabled = pBoolean;
            txtExtraFieldValueReg_5.Enabled = pBoolean;
            txtExtraFieldValueReg_6.Enabled = pBoolean;
            txtExtraFieldValueReg_7.Enabled = pBoolean;
            txtExtraFieldValueReg_8.Enabled = pBoolean;
            txtExtraFieldValueReg_9.Enabled = pBoolean;
            txtExtraFieldValueReg_10.Enabled = pBoolean;

            drdInformationType.Enabled = pBoolean;

        }

        public void ClearControls()
        {
            txtInformationID.Text = "";
            txtInfoName.Text = "";
            txtInfoRegName.Text = "";

            cmbCategorySelection.Items.Clear();
            drpSubCategorySelection.Items.Clear();

            txtInfoAdd.Text = "";
            txtAddReg.Text = "";
            txtEmail.Text = "";
            txtPhone1.Text = "";
            txtPhone2.Text = "";
            txtPhone3.Text = "";
            txtLatitude.Text = "0.0000000";
            txtLongitude.Text = "0.0000000";
            txtPinCode.Text = "";
            txtRegionalPinCode.Text = "";

            txtCity.Text = "";
            txtRegCity.Text = "";
            txtExtraField_1.Text = "";
            txtExtraFieldReg_1.Text = "";
            txtExtraFieldValue_1.Text = "";
            txtExtraFieldValueReg_1.Text = "";

            txtExtraField_2.Text = "";
            txtExtraFieldReg_2.Text = "";
            txtExtraFieldValue_2.Text = "";
            txtExtraFieldValueReg_2.Text = "";

            txtExtraField_3.Text = "";
            txtExtraFieldReg_3.Text = "";
            txtExtraFieldValue_3.Text = "";
            txtExtraFieldValueReg_3.Text = "";

            txtExtraField_4.Text = "";
            txtExtraFieldReg_4.Text = "";
            txtExtraFieldValue_4.Text = "";
            txtExtraFieldValueReg_4.Text = "";

            txtExtraField_5.Text = "";
            txtExtraFieldReg_5.Text = "";
            txtExtraFieldValue_5.Text = "";
            txtExtraFieldValueReg_5.Text = "";

            txtExtraField_6.Text = "";
            txtExtraFieldReg_6.Text = "";
            txtExtraFieldValue_6.Text = "";
            txtExtraFieldValueReg_6.Text = "";

            txtExtraField_7.Text = "";
            txtExtraFieldReg_7.Text = "";
            txtExtraFieldValue_7.Text = "";
            txtExtraFieldValueReg_7.Text = "";

            txtExtraField_8.Text = "";
            txtExtraFieldReg_8.Text = "";
            txtExtraFieldValue_8.Text = "";
            txtExtraFieldValueReg_8.Text = "";

            txtExtraField_9.Text = "";
            txtExtraFieldReg_9.Text = "";
            txtExtraFieldValue_9.Text = "";
            txtExtraFieldValueReg_9.Text = "";

            txtExtraField_10.Text = "";
            txtExtraFieldReg_10.Text = "";
            txtExtraFieldValue_10.Text = "";
            txtExtraFieldValueReg_10.Text = "";
        }

        protected void FillCategoryCombo()
        {
            try
            {
                DataTable dtInfoData = SqlHelper.ReadTable("spGetCategoryDetailsOnType", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1));
                if (dtInfoData.Rows.Count > 0)
                {
                    cmbCategorySelection.AppendDataBoundItems = true;
                    cmbCategorySelection.Items.Insert(0, new ListItem("<< Select Category >>", "-1"));
                    cmbCategorySelection.DataSource = dtInfoData;
                    cmbCategorySelection.DataTextField = "CM_vCharName_En";
                    cmbCategorySelection.DataValueField = "CM_bIntCatId";
                    cmbCategorySelection.DataBind();
                    cmbCategorySelection.SelectedIndex = 0;
                    FillSubCategoryCombo(0);
                }
                else
                {
                    cmbCategorySelection.AppendDataBoundItems = true;
                    cmbCategorySelection.Items.Insert(0, new ListItem("<< No Category found  >>", "-1"));
                    cmbCategorySelection.SelectedIndex = 0;
                    FillSubCategoryCombo(0);
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillCategoryCombo", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        protected void FillSubCategoryCombo(long lngCatID)
        {
            //DataTable dtInfoData = SqlHelper.ReadTable("spGetSubCategoryDetailsForInfo", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
            //                                           SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
            //                                            SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt,lngCatID));
            try
            {
                DataTable dtInfoData = SqlHelper.ReadTable("spGetSubCategoryDetailsForInfo", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                            SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                             SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, lngCatID),
                                             SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1));

                drpSubCategorySelection.Items.Clear();
                if (dtInfoData.Rows.Count > 0)
                {
                    drpSubCategorySelection.AppendDataBoundItems = true;
                    drpSubCategorySelection.Items.Insert(0, new ListItem("<< Select Sub Category >>", "-1"));
                    drpSubCategorySelection.DataSource = dtInfoData;
                    drpSubCategorySelection.DataTextField = "SCM_vCharName_En";
                    drpSubCategorySelection.DataValueField = "SCM_bIntSubCatId";
                    drpSubCategorySelection.DataBind();
                    drpSubCategorySelection.SelectedIndex = 0;
                }
                else
                {
                    drpSubCategorySelection.AppendDataBoundItems = true;
                    drpSubCategorySelection.Items.Insert(0, new ListItem("<< No SubCategory found  >>", "-1"));
                    drpSubCategorySelection.SelectedIndex = 0;
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillSubCategoryCombo", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }

        protected void grdInfoRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        DataTable dtInfoData = SqlHelper.ReadTable("spGetCategoryDetailsOnType", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
            //                              SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
            //                              SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1));

            //        DropDownList cmbCategory = (DropDownList)e.Row.FindControl("cmbCategory");
            //        Label lblCatName = (Label)e.Row.FindControl("lblCatName");
            //        if (dtInfoData.Rows.Count > 0)
            //        {
            //            cmbCategory.AppendDataBoundItems = true;
            //            cmbCategory.Items.Insert(0, new ListItem("<< Select Category >>", "-1"));
            //            cmbCategory.DataSource = dtInfoData;
            //            cmbCategory.DataTextField = "CM_vCharName_En";
            //            cmbCategory.DataValueField = "CM_bIntCatId";
            //            cmbCategory.DataBind();
            //            cmbCategory.Items.FindByText(lblCatName.Text).Selected = true;
            //            cmbCategory.SelectedIndex = 0;
            //        }
            //        else
            //        {
            //            cmbCategory.AppendDataBoundItems = true;
            //            cmbCategory.Items.Insert(0, new ListItem("<< No Category found  >>", "-1"));
            //            cmbCategory.SelectedIndex = 0;
            //        }

            //        DropDownList cmbSubCategory = (DropDownList)e.Row.FindControl("cmbSubCategory");
            //        cmbSubCategory.AppendDataBoundItems = true;
            //        cmbSubCategory.Items.Insert(0, new ListItem("<< Select Sub Category >>", "-1"));
            //        cmbSubCategory.SelectedIndex = 0;
            //    }
            //}
            //catch (Exception exError)
            //{
            //    long pLngErr = -1;
            //    if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
            //        pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
            //    pLngErr = GlobalFunctions.ReportError("grdInfoRowDataBound", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message);
            //    updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
            //    updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            //}

        }

        private void SetMessage(bool pBlnIsError, string pStrError)
        {
            actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            actionInfo.InnerHtml = pStrError;
        }

        private void SetProductsUpdateMessage(bool pBlnIsError, string pStrMessage)
        {

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload=function(){");
            //sb.Append(" bootbox.alert('");
            //sb.Append(pStrMessage);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());

            updateActionDiv.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            updateActionDiv.InnerHtml = pStrMessage;
        }

        private string Validate(string strInfoName, string strregInfoName, string strPhone1, string strPhone2, string strPhone3, string strAddress, string strEmail, long CatSelectedIndex, string strLat, string strLong, long intInfoID, long lngCatId, long lngSubCatId, long lngInfotype)
        {
            string strValidate;
            if (strInfoName == "")
            {
                return ("Information Cannot be Blank!!!");
            }
            else
            {
                strValidate = chkDuplicateInfoName(strInfoName, intInfoID, lngCatId, lngSubCatId);
                if (strValidate != "")
                {
                    return (strValidate);
                }
            }

            if (lngInfotype == 2)
            {
                return ("Information type must be selected!!!");
            }

            if (strregInfoName == "")
            {
                return ("Regional Info Name Cannot be Blank!!!");

            }

            if (strPhone1 == "" && strPhone2 == "" && strPhone3 == "")
            {
                return ("Phone Cannot be Blank!!!");
            }
            if (strAddress == "")
            {
                return ("Address Cannot be Blank!!!");
            }

            //Modified By SSK since ChkEmailId is Not compulsory

            //if (strEmail == "")
            //{
            //    return ("Email Cannot be Blank!!!");
            //}
            //else
            //{
            //    strValidate = ChkEmail(strEmail);
            //    if (strValidate != "")
            //    {
            //        return ("Invalid Email!!!");
            //    }
            //}


            if (CatSelectedIndex == 0)
            {
                return ("Select Category to Continue!!!");
            }

            strValidate = ChkLonLatDecimal(strLat, strLong);
            if (strValidate != "")
            {
                return strValidate;
            }
            return "";
        }

        //Modified by NN dated 21-09-2015
        private string ChkLonLatDecimal(string dblLatitude, string dblLongititude)
        {
            string strLat;
            if (dblLatitude != "")
            {
                int index = 0;
                index = dblLatitude.LastIndexOf(".");
                //  Modified NYN 21/09/2015
                if (index != 0)
                {
                    strLat = dblLatitude.Substring(index + 1);
                    if (strLat.Length > 0 || strLat.Length <= 10)
                    {
                        return ("");
                    }
                    else
                    {
                        return ("Invalid Latitiude");
                    }
                }
                else
                {
                    return ("Invalid Latitiude");
                }
            }

            if (dblLongititude != "")
            {
                int index1 = 0;
                index1 = dblLongititude.LastIndexOf(".");
                strLat = dblLongititude.Substring(index1 + 1);
                if (index1 != 0)
                {
                    strLat = dblLongititude.Substring(index1 + 1);
                    if (strLat.Length > 0 || strLat.Length <= 10)
                    {
                        return ("");
                    }
                    else
                    {
                        return ("Invalid Longitude");
                    }
                }
                else
                {
                    return ("Invalid Longitude");
                }
            }
            return ("");
        }

        //private string ChkLonLatDecimal(string dblLatitude, string dblLongititude)
        //{
        //    string strLat;
        //    if (dblLatitude != "")
        //    {
        //        int index = 0;
        //        index = dblLatitude.LastIndexOf(".");
        //        if (index != 0)
        //        {
        //            strLat = dblLatitude.Substring(index + 1);
        //            if (strLat.Length > 7 || strLat.Length < 7)
        //            {
        //                return ("Invalid Latitiude");
        //            }
        //        }
        //        else
        //        {
        //            return ("Invalid Latitiude");
        //        }
        //    }

        //    if (dblLongititude != "")
        //    {
        //        int index1 = 0;
        //        index1 = dblLongititude.LastIndexOf(".");
        //        strLat = dblLongititude.Substring(index1 + 1);
        //        if (index1 != 0)
        //        {
        //            strLat = dblLongititude.Substring(index1 + 1);
        //            if (strLat.Length > 7 || strLat.Length < 7)
        //            {
        //                return ("Invalid Longitude");
        //            }
        //        }
        //        else
        //        {
        //            return ("Invalid Longitude");
        //        }
        //    }
        //    return ("");
        //}

        public string ChkEmail(string strEmail)
        {
            const string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                                   + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                      [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                                    + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                                                [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                                    + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

            bool blnVal = Regex.IsMatch(strEmail, MatchEmailPattern);
            if (blnVal == false)
            {
                return ("Invalid Email");
            }
            return ("");
        }

        public string chkDuplicateInfoName(string strInfoName, long InfoID, long catId, long subCatID)
        {
            try
            {
                DataTable dtInfoData = SqlHelper.ReadTable("spChkInfoDuplication", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                       SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                       SqlHelper.AddInParam("@IM_vCharInfoName_En", SqlDbType.VarChar, strInfoName),
                                       SqlHelper.AddInParam("@IM_bIntCatId", SqlDbType.BigInt, catId),
                                       SqlHelper.AddInParam("@IM_bIntSubCatId", SqlDbType.BigInt, subCatID),
                                       SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1));

                if (dtInfoData.Rows.Count != 0)
                {
                    if (InfoID == 0)
                    {
                        return ("Information Cannot be Duplicated");
                    }
                    else
                    {
                        DataRow dtInfoRow = dtInfoData.Rows[0];
                        long lngInfoID = Convert.ToInt64(dtInfoRow["IM_bIntInfoId"].ToString());
                        if (InfoID != lngInfoID)
                        {
                            return ("Information Cannot be Duplicated");
                        }
                    }
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("chkDuplicateInfoName", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Owner";
            }
            return ("");
        }

        public bool ChkEntryCount()
        {
            try
            {
                DataTable dtInfoData = SqlHelper.ReadTable("spChkEntryCount", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                       SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                       SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1));
                DataRow dtInfoRow = dtInfoData.Rows[0];
                long lngInfoCount = Convert.ToInt64(dtInfoRow["InfoCount"].ToString());

                if (lngInfoCount < Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaEntryCount))
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("ChkEntryCount", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
                return (false);
            }

        }

        protected void FillCategoryComboForEdit()
        {
            try
            {
                DataTable dtInfoData = SqlHelper.ReadTable("spGetCategoryDetailsOnType", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1));
                if (dtInfoData.Rows.Count > 0)
                {
                    drpCategorySelect.AppendDataBoundItems = true;
                    drpCategorySelect.Items.Insert(0, new ListItem("<< Select Category >>", "-1"));
                    drpCategorySelect.DataSource = dtInfoData;
                    drpCategorySelect.DataTextField = "CM_vCharName_En";
                    drpCategorySelect.DataValueField = "CM_bIntCatId";
                    drpCategorySelect.DataBind();
                    drpCategorySelect.SelectedIndex = 0;
                    FillSubCategoryComboForEdit(0);
                }
                else
                {
                    drpCategorySelect.AppendDataBoundItems = true;
                    drpCategorySelect.Items.Insert(0, new ListItem("<< No Category found  >>", "-1"));
                    drpCategorySelect.SelectedIndex = 0;
                    FillSubCategoryComboForEdit(0);
                }

                //Code added by SSK for items insertion in information type
                //---------------------------------------------------------
                drdInformationType.Items.Insert(0, new ListItem("Select Information type", "2"));
                drdInformationType.Items.Insert(1, new ListItem("General", "0"));
                drdInformationType.Items.Insert(2, new ListItem("Critical (Emergency)", "1"));
                drdInformationType.SelectedIndex = 0;

                drdModifyInfoType.Items.Insert(0, new ListItem("Select Information type", "2"));
                drdModifyInfoType.Items.Insert(1, new ListItem("General", "0"));
                drdModifyInfoType.Items.Insert(2, new ListItem("Critical (Emergency)", "1"));
                drdModifyInfoType.SelectedIndex = 0;
                //---------------------------------------------------------
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillCategoryComboForEdit", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }
        protected void FillSubCategoryComboForEdit(long lngCatID)
        {
            try
            {
                DataTable dtInfoData = SqlHelper.ReadTable("spGetSubCategoryDetailsForInfo", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                            SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                             SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, lngCatID),
                                             SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1));

                drpSubCategory.Items.Clear();
                if (dtInfoData.Rows.Count > 0)
                {
                    drpSubCategory.AppendDataBoundItems = true;
                    drpSubCategory.Items.Insert(0, new ListItem("<< Select Sub Category >>", "-1"));
                    drpSubCategory.DataSource = dtInfoData;
                    drpSubCategory.DataTextField = "SCM_vCharName_En";
                    drpSubCategory.DataValueField = "SCM_bIntSubCatId";
                    drpSubCategory.DataBind();
                    if (HiddenSubCat.Value != "")
                    {
                        drpSubCategory.SelectedValue = HiddenSubCat.Value;
                        HiddenSubCat.Value = "";
                    }
                    else
                    {
                        drpSubCategory.SelectedIndex = 0;
                    }
                }
                else
                {
                    drpSubCategory.AppendDataBoundItems = true;
                    drpSubCategory.Items.Insert(0, new ListItem("<< No SubCategory found  >>", "-1"));
                    drpSubCategory.SelectedIndex = 0;
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("FillSubCategoryComboForEdit", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
        }



        protected void btnEditSave_ServerClick(object sender, EventArgs e)
        {
            string strValidate = Validate(txtEditInfoName.Text, txtEditRegInfoName.Text, txtEditInfoPhone1.Text, txtEditInfoPhone2.Text, txtEditInfoPhone3.Text, txtEditInfoAdd.Text, txtEditInfoEmail.Text, drpCategorySelect.SelectedIndex, txtEditInfoLatitude.Text, txtEditInfoLongitude.Text, Convert.ToInt64(txtEditInfoID.Text), Convert.ToInt64(drpCategorySelect.SelectedItem.Value), Convert.ToInt64(drpSubCategory.SelectedItem.Value), Convert.ToInt64(drdModifyInfoType.SelectedItem.Value));
            if (strValidate == "")
            {
                //Modified NYN 21/09/2015
                if (txtEditInfoLongitude.Text == "")
                {
                    txtEditInfoLongitude.Text = "0";
                }
                if (txtEditInfoLatitude.Text == "")
                {
                    txtEditInfoLatitude.Text = "0";
                }

                //For Business information max limit is 10
                if (chkFreeandBusinessEntrycount(10, Convert.ToInt64(drdModifyInfoType.SelectedItem.Value)) == false)
                {
                    //Added for url addition for information 
                    string strUrl = txtModifyUrl.Text.Trim();

                    DataTable dtInsertedData = SqlHelper.ReadTable("spInsertUpdateInfoMaster1", true,
                                                             SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                             SqlHelper.AddInParam("@IM_bIntSubCatId", SqlDbType.BigInt, Convert.ToInt64(drpSubCategory.SelectedItem.Value)),
                                                              SqlHelper.AddInParam("@IM_bIntCatId", SqlDbType.BigInt, Convert.ToInt64(drpCategorySelect.SelectedItem.Value)),
                                                              SqlHelper.AddInParam("@IM_IntCharInfoType", SqlDbType.Int, 1),
                                                               SqlHelper.AddInParam("@IM_vCharInfoName_En", SqlDbType.VarChar, (txtEditInfoName.Text)),
                                                                SqlHelper.AddInParam("@IM_nVarInfoName_Reg", SqlDbType.NVarChar, (txtEditRegInfoName.Text)),
                                                                 SqlHelper.AddInParam("@IM_vCharInfoAdd_En", SqlDbType.VarChar, txtEditInfoAdd.Text),
                                                                SqlHelper.AddInParam("@IM_nVarInfoAdd_Reg", SqlDbType.NVarChar, txtEditInfoRegAdd.Text),
                                                                 SqlHelper.AddInParam("@IM_vCharInfoEmail", SqlDbType.VarChar, txtEditInfoEmail.Text),
                                                                  SqlHelper.AddInParam("@IM_vCharInfoPhone1", SqlDbType.VarChar, txtEditInfoPhone1.Text),
                                                                   SqlHelper.AddInParam("@IM_vCharInfoPhone2", SqlDbType.VarChar, txtEditInfoPhone2.Text),
                                                                    SqlHelper.AddInParam("@IM_vCharInfoPhone3", SqlDbType.VarChar, txtEditInfoPhone3.Text),
                                                                     SqlHelper.AddInParam("@IM_decLatitude", SqlDbType.Decimal, txtEditInfoLatitude.Text),
                                                                   SqlHelper.AddInParam("@IM_decLongitude", SqlDbType.Decimal, txtEditInfoLongitude.Text),
                                                                   SqlHelper.AddInParam("@IM_vCharPincode_En", SqlDbType.VarChar, txtEditInfoPinCode.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarPincode_Reg", SqlDbType.NVarChar, txtEditInfoRegPinCode.Text),

                                                                   SqlHelper.AddInParam("@IM_vCharCity_En", SqlDbType.VarChar, txtEditInfoCity.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarCity_Reg", SqlDbType.NVarChar, txtEditInfoRegCity.Text),

                                                                    SqlHelper.AddInParam("@IM_vCharInfoExtraLabel1_En", SqlDbType.VarChar, txtEditInfoExtraLabel1.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraLabel1_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraLabel1.Text),
                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraValue1_En", SqlDbType.VarChar, txtEditInfoExtraVal1.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraValue1_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraVal1.Text),


                                                                    SqlHelper.AddInParam("@IM_vCharInfoExtraLabel2_En", SqlDbType.VarChar, txtEditInfoExtraLabel2.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraLabel2_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraLabel2.Text),
                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraValue2_En", SqlDbType.VarChar, txtEditInfoExtraVal2.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraValue2_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraVal2.Text),

                                                                    SqlHelper.AddInParam("@IM_vCharInfoExtraLabel3_En", SqlDbType.VarChar, txtEditInfoExtraLabel3.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraLabel3_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraLabel3.Text),
                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraValue3_En", SqlDbType.VarChar, txtEditInfoExtraVal3.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraValue3_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraVal3.Text),


                                                                    SqlHelper.AddInParam("@IM_vCharInfoExtraLabel4_En", SqlDbType.VarChar, txtEditInfoExtraLabel4.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraLabel4_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraLabel4.Text),
                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraValue4_En", SqlDbType.VarChar, txtEditInfoExtraVal4.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraValue4_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraVal4.Text),


                                                                    SqlHelper.AddInParam("@IM_vCharInfoExtraLabel5_En", SqlDbType.VarChar, txtEditInfoExtraLabel5.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraLabel5_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraLabel5.Text),
                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraValue5_En", SqlDbType.VarChar, txtEditInfoExtraVal5.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraValue5_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraVal5.Text),

                                                                    SqlHelper.AddInParam("@IM_vCharInfoExtraLabel6_En", SqlDbType.VarChar, txtEditInfoExtraLabel6.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraLabel6_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraLabel6.Text),
                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraValue6_En", SqlDbType.VarChar, txtEditInfoExtraVal6.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraValue6_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraVal6.Text),

                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraLabel7_En", SqlDbType.VarChar, txtEditInfoExtraLabel7.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraLabel7_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraLabel7.Text),
                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraValue7_En", SqlDbType.VarChar, txtEditInfoExtraVal7.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraValue7_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraVal7.Text),

                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraLabel8_En", SqlDbType.VarChar, txtEditInfoExtraLabel8.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraLabel8_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraLabel8.Text),
                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraValue8_En", SqlDbType.VarChar, txtEditInfoExtraVal8.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraValue8_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraVal8.Text),

                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraLabel9_En", SqlDbType.VarChar, txtEditInfoExtraLabel9.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraLabel9_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraLabel9.Text),
                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraValue9_En", SqlDbType.VarChar, txtEditInfoExtraVal9.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraValue9_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraVal9.Text),

                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraLabel10_En", SqlDbType.VarChar, txtEditInfoExtraLabel10.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraLabel10_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraLabel10.Text),
                                                                   SqlHelper.AddInParam("@IM_vCharInfoExtraValue10_En", SqlDbType.VarChar, txtEditInfoExtraVal10.Text),
                                                                   SqlHelper.AddInParam("@IM_nVarInforExtraValue10_Reg", SqlDbType.NVarChar, txtEditInfoRegExtraVal10.Text),
                                                                   SqlHelper.AddInParam("@IM_bitIsActive", SqlDbType.Bit, 1),
                                                                   SqlHelper.AddInParam("@IM_vCharUrl", SqlDbType.VarChar, strUrl),
                                                                   SqlHelper.AddInParam("@IM_bitIsEmergency", SqlDbType.Bit, Convert.ToInt64(drdModifyInfoType.SelectedItem.Value)),
                                                                   SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, Convert.ToInt64(txtEditInfoID.Text)));

                    int mInformationId = Convert.ToInt32(txtEditInfoID.Text);

                    if (mInformationId > 0)
                    {
                        //Code added by SSK for payment details updation
                        //----------------------------------------------------------------
                        int intIsPaid = Convert.ToInt32(drModifyIsPaid.SelectedValue.ToString());
                        double dblAmount = 0;
                        string dtFrmDate = "";
                        string dtToDate = "";
                        if (intIsPaid == 1)
                        {
                            dblAmount = Convert.ToDouble(txtModifyAmount.Text);
                            dtFrmDate = txtModifyFromdate.Text;
                            dtToDate = txtModifyTodate.Text;
                        }
                        else { intIsPaid = 0; }

                        string strUpdatePayment = " UPDATE Information_Master_" + Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID);
                        strUpdatePayment = strUpdatePayment + " SET IM_bitIsPaid = " + intIsPaid;
                        strUpdatePayment = strUpdatePayment + " ,IM_decAmt = " + dblAmount;
                        strUpdatePayment = strUpdatePayment + " ,IM_dtFromDate = CAST(SWITCHOFFSET('" + dtFrmDate + "', '+05:30') As DATETIME)";
                        strUpdatePayment = strUpdatePayment + " ,IM_dtToDate = CAST(SWITCHOFFSET('" + dtToDate + "', '+05:30') As DATETIME)";
                        strUpdatePayment = strUpdatePayment + " WHERE IM_bIntInfoId = " + Convert.ToInt32(mInformationId);

                        int intNoofRows = SqlHelper.UpdateDatabase(strUpdatePayment);

                        //----------------------------------------------------------------
                    }
                    HiddenSubCat.Value = "";
                    HiddenFieldForDialogOpenClose.Value = "c";
                    drpCategorySelect.SelectedIndex = 0;
                    showInfoGrid();


                    //Code for maintaining log of Emergency 
                    //---------------------------------------------------------------------------------
                    if (dtInsertedData.Rows.Count > 0)
                    {
                        if (IsEmergencyUpdate == true || Convert.ToInt64(hfInfotype.Value) == 1)
                        {
                            string strQuery = " SELECT EIML_bIntId FROM EmergencyInformationModificationLog where EIML_bIntTalukaId = " + Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID);
                            DataTable dtselectEnergencyid = SqlHelper.ReadTable(strQuery, false);
                            long lngEmergencyid = 0;
                            if (dtselectEnergencyid.Rows.Count > 0)
                            {
                                //Update emergency information version for taluka
                                lngEmergencyid = Convert.ToInt64(dtselectEnergencyid.Rows[0][0]);
                                dtselectEnergencyid = SqlHelper.ReadTable("SP_insertEmergencyInfoLog", true,
                                     SqlHelper.AddInParam("@pintTalukaid", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                     SqlHelper.AddInParam("@pintEmergencyInfo", SqlDbType.BigInt, Convert.ToInt64("0")),
                                     SqlHelper.AddInParam("@pintEmergencyId", SqlDbType.BigInt, lngEmergencyid));
                            }
                            else
                            {
                                //Insert new emergency information version for taluka
                                dtselectEnergencyid = SqlHelper.ReadTable("SP_insertEmergencyInfoLog", true,
                                            SqlHelper.AddInParam("@pintTalukaid", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                            SqlHelper.AddInParam("@pintEmergencyInfo", SqlDbType.BigInt, Convert.ToInt64("1")),
                                            SqlHelper.AddInParam("@pintEmergencyId", SqlDbType.BigInt, lngEmergencyid));
                            }
                        }
                    }
                    //---------------------------------------------------------------------------------

                    #region oldemergencycode
                    //Code for maintaining log of Emergency 
                    //---------------------------------------------------------------------------------
                    //if (dtInsertedData.Rows.Count > 0)
                    //{
                    //    string strQuery = " SELECT EIML_bIntId FROM EmergencyInformationModificationLog where EIML_bIntTalukaId = " + Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID);
                    //    DataTable dtselectEnergencyid = SqlHelper.ReadTable(strQuery, false);
                    //    long lngEmergencyid = 0;
                    //    if (dtselectEnergencyid.Rows.Count > 0)
                    //    {
                    //        //Update emergency information version for taluka
                    //        lngEmergencyid = Convert.ToInt64(dtselectEnergencyid.Rows[0][0]);
                    //        dtselectEnergencyid = SqlHelper.ReadTable("SP_insertEmergencyInfoLog", true,
                    //             SqlHelper.AddInParam("@pintTalukaid", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                    //             SqlHelper.AddInParam("@pintEmergencyInfo", SqlDbType.BigInt, Convert.ToInt64("0")),
                    //             SqlHelper.AddInParam("@pintEmergencyId", SqlDbType.BigInt, lngEmergencyid));
                    //    }
                    //    else
                    //    {
                    //        //Insert new emergency information version for taluka
                    //        dtselectEnergencyid = SqlHelper.ReadTable("SP_insertEmergencyInfoLog", true,
                    //                    SqlHelper.AddInParam("@pintTalukaid", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                    //                    SqlHelper.AddInParam("@pintEmergencyInfo", SqlDbType.BigInt, Convert.ToInt64("1")),
                    //                    SqlHelper.AddInParam("@pintEmergencyId", SqlDbType.BigInt, lngEmergencyid));
                    //    }
                    //}
                    //---------------------------------------------------------------------------------
                    #endregion oldemergencycode

                    SetProductsUpdateMessage(false, "Modify Information!!!");
                    updateActionDivDis.Attributes["class"] = "alert " + (true ? "alert-danger" : "alert-success");
                    updateActionDivDis.InnerHtml = "Information Updated Successfully!!!";
                }
                else
                {
                    updateActionDiv.Attributes["class"] = "alert " + (true ? "alert-danger" : "alert-success");
                    updateActionDiv.InnerHtml = "Maximum limit for Emergency Information is 10 !!!";
                }
            }
            else
            {
                SetProductsUpdateMessage(true, strValidate);
            }


        }

        protected void drpCategorySelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubCategoryComboForEdit(Convert.ToInt64(drpCategorySelect.SelectedItem.Value));
        }

        protected void btnRead_ServerClick(object sender, EventArgs e)
        {
            try
            {

                //Code for ProgressBar view
                //string strImgLoaderPath = Server.MapPath("images\\loading-blue.gif");
                //StringBuilder htmlTable = new StringBuilder();
                //htmlTable.Append("<img src='" + strImgLoaderPath + "' width='75' height='75'/>");
                //DBProgressPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() }); 

                //Code for excel sheet to retrieve information details
                string ext = Path.GetExtension(FileUploadControl.FileName).ToLower();
                string path = Server.MapPath(FileUploadControl.PostedFile.FileName);
                FileUploadControl.SaveAs(path);
                string ConStr = string.Empty;
                if (ext.Trim() == ".xls")
                {
                    ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (ext.Trim() == ".xlsx")
                {

                    ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }
                string SelectInformationquery = "select * from [Sheet3$]";
                OleDbConnection con = new OleDbConnection(ConStr);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                OleDbCommand cmd = new OleDbCommand(SelectInformationquery, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable dtInformationDetails = new DataTable();
                DataSet ds = new DataSet();
                da.Fill(dtInformationDetails);

                string SelectCategoryquery = "select * from [Sheet1$]";
                cmd = new OleDbCommand(SelectCategoryquery, con);
                da = new OleDbDataAdapter(cmd);
                DataTable dtCategoryDetails = new DataTable();
                ds = new DataSet();
                da.Fill(dtCategoryDetails);

                string SelectSubCategoryquery = "select * from [Sheet2$]";
                cmd = new OleDbCommand(SelectSubCategoryquery, con);
                da = new OleDbDataAdapter(cmd);
                DataTable dtSubCategoryDetails = new DataTable();
                ds = new DataSet();
                da.Fill(dtSubCategoryDetails);

                con.Close();


                //  int i;
                // HttpPostedFile f;
                int introwCount = 0;
                List<int> lstCatid = new List<int>();
                List<string> lstCatFilePaths = new List<string>();
                List<string> lstSubCatFilePaths = new List<string>();
                HttpFileCollection uploadedFiles = Request.Files;

                #region catandsubcat
                //foreach (DataRow drCat in dtCategoryDetails.Rows)
                //{
                //    if (CheckDuplicateValues(drCat[0].ToString(), drCat[1].ToString(), -1) == " ")
                //    {
                //        //Code to insert all Category Details.
                //        for (i = 0; i < uploadedFiles.Count - 1; i++)
                //        {
                //            f = uploadedFiles[i];
                //            if (f.ContentLength > 0 && f.FileName != "MStoreInfo.xlsx")
                //            {
                //                string strFileNm = f.FileName;
                //                string strFilepath = Path.GetFullPath(strFileNm);
                //                string strStoredFilepath = "";

                //                //Store Category Description and Images
                //                foreach (DataRow drImgColVal in dtCategoryDetails.Rows)
                //                {
                //                    if (strFileNm.Equals(drImgColVal[3].ToString()) == true)
                //                    {
                //                        strStoredFilepath = GetSafeFileNameOnLocation(strFilepath, GlobalVariables.CategoryDetailImagePath);
                //                        f.SaveAs(strStoredFilepath);
                //                        strStoredFilepath = String.Format("{0}/{1}", GlobalVariables.CategoryDetailImagePath, Path.GetFileName(strStoredFilepath));
                //                        lstCatFilePaths.Add(strStoredFilepath); //Store file path for future mapping before storing in database

                //                        //Code to insert or modify Category Business master.
                //                        DataTable dtCatData = SqlHelper.ReadTable("spInsertUpdateCatMasterBusiness", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                //                        SqlHelper.AddInParam("@bintTalukaId", SqlDbType.VarChar, Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                //                        SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, drImgColVal[0].ToString()),
                //                        SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, drImgColVal[1].ToString()),
                //                        SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, 1),
                //                        SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, drImgColVal[2].ToString()),
                //                        SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                //                        SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                //                        SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, 0));

                //                        DataRow row = dtCatData.Rows[0];
                //                        int intCatid = Convert.ToInt32(row["CM_bIntCatId"]);
                //                        lstCatid.Add(intCatid);
                //                        break;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}

                ////Check whether SubCategory exist or not
                //foreach (DataRow drSubCat in dtSubCategoryDetails.Rows)
                //{
                //    if (chkSubCatExist(drSubCat[1].ToString()) == false)
                //    {
                //        //Code to insert all Subcategory Details.
                //        for (i = 0; i < uploadedFiles.Count - 1; i++)
                //        {
                //            f = uploadedFiles[i];
                //            if (f.ContentLength > 0 && f.FileName != "MStoreInfo.xlsx")
                //            {
                //                string strFileNm = f.FileName;
                //                string strFilepath = Path.GetFullPath(strFileNm);
                //                string strStoredFilepath = "";
                //                //Store Subcategory Description and Images
                //                foreach (DataRow drImgColVal in dtSubCategoryDetails.Rows)
                //                {
                //                    if (strFileNm.Equals(drImgColVal[5].ToString()) == true)
                //                    {
                //                        strStoredFilepath = GetSafeFileNameOnLocation(strFilepath, GlobalVariables.SubCategoryDetailImagePath);
                //                        f.SaveAs(strStoredFilepath);
                //                        strStoredFilepath = String.Format("{0}/{1}", GlobalVariables.SubCategoryDetailImagePath, Path.GetFileName(strStoredFilepath));
                //                        lstSubCatFilePaths.Add(strStoredFilepath); //Store file path for future mapping before storing in database
                //                        string strTalId = Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID);
                //                        DataTable dtCatid = SqlHelper.ReadTable("SELECT CM_bIntCatId FROM Category_Master_" + strTalId + " WHERE CM_vCharName_En LIKE '" + drImgColVal[0].ToString() + "'", false);

                //                        //Code to insert or modify SubCategory Business master.
                //                        DataTable dtInsertSubCategory = SqlHelper.ReadTable("SP_insertSubCategoryDetailBusiness", true,
                //                        SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                //                        SqlHelper.AddInParam("@vCharName_En", SqlDbType.VarChar, drImgColVal[1].ToString()),
                //                        SqlHelper.AddInParam("@nVarName_Reg", SqlDbType.NVarChar, drImgColVal[2].ToString()),
                //                        SqlHelper.AddInParam("@bItIsActive", SqlDbType.Bit, 1),
                //                        SqlHelper.AddInParam("@vCharCatImgClass", SqlDbType.VarChar, drImgColVal[4].ToString()),
                //                        SqlHelper.AddInParam("@vCharCatImgPath", SqlDbType.VarChar, strStoredFilepath),
                //                        SqlHelper.AddInParam("@intCategoryId", SqlDbType.BigInt, Convert.ToInt32(dtCatid.Rows[0][0])),
                //                        SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 1),
                //                        SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, 0));
                //                        break;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}


                //Code to insert all Information Details.
                //for (i = 0; i < uploadedFiles.Count - 1; i++)
                //{
                //    f = uploadedFiles[i];
                //    if (f.ContentLength > 0 && f.FileName != "MStoreInfo.xlsx")
                //    {
                //        string strFileNm = f.FileName;
                //        string strFilepath = Path.GetFullPath(strFileNm);
                #endregion catandsubcat

                //Store Information Description and Images
                int intFailureCount = 0;
                introwCount = 0;
                foreach (DataRow drImgColVal in dtInformationDetails.Rows)
                {

                    if (CheckDuplicateValues(drImgColVal[0].ToString(), "", -1) != " ")
                    {
                        if (chkSubCatExist(drImgColVal[1].ToString()) == true || drImgColVal[1].ToString().Trim() == "")
                        {
                            string strTalukaId = Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID);
                            DataTable dtCatid = SqlHelper.ReadTable("SELECT CM_bIntCatId FROM Category_Master_" + strTalukaId + " WHERE CM_vCharName_En LIKE '" + drImgColVal[0].ToString() + "'", false);
                            DataTable dtSubCatid = SqlHelper.ReadTable("SELECT SCM_bIntSubCatId FROM Sub_Category_Master_" + strTalukaId + " WHERE SCM_vCharName_En LIKE '" + drImgColVal[1].ToString() + "'", false);
                            int intSubCatId = -1;
                            if (dtSubCatid.Rows.Count > 0)
                            {
                                DataRow row = dtSubCatid.Rows[0];
                                intSubCatId = Convert.ToInt32(row[0]);
                            }

                            DataRow rowCat = dtCatid.Rows[0];
                            int intCatId = Convert.ToInt32(rowCat[0]);

                            //Code to insert or modify SubCategory Business master.
                            DataTable dtInsertInformation = SqlHelper.ReadTable("spInsertUpdateInfoMaster1", true,
                            SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                            SqlHelper.AddInParam("@IM_bIntCatId", SqlDbType.BigInt, intCatId),
                                                           SqlHelper.AddInParam("@IM_bIntSubCatId", SqlDbType.BigInt, intSubCatId),
                                                           SqlHelper.AddInParam("@IM_IntCharInfoType", SqlDbType.Int, 0),
                                                            SqlHelper.AddInParam("@IM_vCharInfoName_En", SqlDbType.VarChar, drImgColVal[2].ToString()),
                                                             SqlHelper.AddInParam("@IM_nVarInfoName_Reg", SqlDbType.NVarChar, drImgColVal[3].ToString()),
                                                               SqlHelper.AddInParam("@IM_vCharInfoAdd_En", SqlDbType.VarChar, drImgColVal[6].ToString()),
                                                             SqlHelper.AddInParam("@IM_nVarInfoAdd_Reg", SqlDbType.NVarChar, drImgColVal[7].ToString()),
                                                              SqlHelper.AddInParam("@IM_vCharInfoEmail", SqlDbType.VarChar, drImgColVal[8].ToString()),
                                                               SqlHelper.AddInParam("@IM_vCharInfoPhone1", SqlDbType.VarChar, drImgColVal[9].ToString()),
                                                                SqlHelper.AddInParam("@IM_vCharInfoPhone2", SqlDbType.VarChar, drImgColVal[10].ToString()),
                                                                 SqlHelper.AddInParam("@IM_vCharInfoPhone3", SqlDbType.VarChar, drImgColVal[11].ToString()),
                                                                 SqlHelper.AddInParam("@IM_decLatitude", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[12])),
                                                                SqlHelper.AddInParam("@IM_decLongitude", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[13])),
                                                                SqlHelper.AddInParam("@IM_vCharPincode_En", SqlDbType.VarChar, drImgColVal[14].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarPincode_Reg", SqlDbType.NVarChar, drImgColVal[15].ToString()),

                                                                 SqlHelper.AddInParam("@IM_vCharCity_En", SqlDbType.VarChar, drImgColVal[4].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarCity_Reg", SqlDbType.NVarChar, drImgColVal[5].ToString()),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel1_En", SqlDbType.VarChar, drImgColVal[16].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel1_Reg", SqlDbType.NVarChar, drImgColVal[17].ToString()),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue1_En", SqlDbType.VarChar, drImgColVal[18].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue1_Reg", SqlDbType.NVarChar, drImgColVal[19].ToString()),


                                                                 SqlHelper.AddInParam("@IM_vCharInfoExtraLabel2_En", SqlDbType.VarChar, drImgColVal[20].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel2_Reg", SqlDbType.NVarChar, drImgColVal[21].ToString()),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue2_En", SqlDbType.VarChar, drImgColVal[22].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue2_Reg", SqlDbType.NVarChar, drImgColVal[23].ToString()),

                                                                 SqlHelper.AddInParam("@IM_vCharInfoExtraLabel3_En", SqlDbType.VarChar, drImgColVal[24].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel3_Reg", SqlDbType.NVarChar, drImgColVal[25].ToString()),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue3_En", SqlDbType.VarChar, drImgColVal[26].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue3_Reg", SqlDbType.NVarChar, drImgColVal[27].ToString()),


                                                                 SqlHelper.AddInParam("@IM_vCharInfoExtraLabel4_En", SqlDbType.VarChar, drImgColVal[28].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel4_Reg", SqlDbType.NVarChar, drImgColVal[29].ToString()),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue4_En", SqlDbType.VarChar, drImgColVal[30].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue4_Reg", SqlDbType.NVarChar, drImgColVal[31].ToString()),

                                                                 SqlHelper.AddInParam("@IM_vCharInfoExtraLabel5_En", SqlDbType.VarChar, drImgColVal[32].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel5_Reg", SqlDbType.NVarChar, drImgColVal[33].ToString()),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue5_En", SqlDbType.VarChar, drImgColVal[34].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue5_Reg", SqlDbType.NVarChar, drImgColVal[35].ToString()),

                                                                 SqlHelper.AddInParam("@IM_vCharInfoExtraLabel6_En", SqlDbType.VarChar, drImgColVal[36].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel6_Reg", SqlDbType.NVarChar, drImgColVal[37].ToString()),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue6_En", SqlDbType.VarChar, drImgColVal[38].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue6_Reg", SqlDbType.NVarChar, drImgColVal[39].ToString()),

                                                                 SqlHelper.AddInParam("@IM_vCharInfoExtraLabel7_En", SqlDbType.VarChar, drImgColVal[40].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel7_Reg", SqlDbType.NVarChar, drImgColVal[41].ToString()),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue7_En", SqlDbType.VarChar, drImgColVal[42].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue7_Reg", SqlDbType.NVarChar, drImgColVal[43].ToString()),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel8_En", SqlDbType.VarChar, drImgColVal[44].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel8_Reg", SqlDbType.NVarChar, drImgColVal[45].ToString()),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue8_En", SqlDbType.VarChar, drImgColVal[46].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue8_Reg", SqlDbType.NVarChar, drImgColVal[47].ToString()),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel9_En", SqlDbType.VarChar, drImgColVal[48].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel9_Reg", SqlDbType.NVarChar, drImgColVal[49].ToString()),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue9_En", SqlDbType.VarChar, drImgColVal[50].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue9_Reg", SqlDbType.NVarChar, drImgColVal[51].ToString()),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel10_En", SqlDbType.VarChar, drImgColVal[52].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel10_Reg", SqlDbType.NVarChar, drImgColVal[53].ToString()),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue10_En", SqlDbType.VarChar, drImgColVal[54].ToString()),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue10_Reg", SqlDbType.NVarChar, drImgColVal[55].ToString()),
                                                                SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, 0));
                            introwCount++;
                            break;
                        }
                    }
                    else
                    {
                        intFailureCount++;
                    }

                }

                //    }//end of mstore file path values
                //}//end looping of selected file collections

                long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);

                # region catandsubcat
                //introwCount = introwCount + lstCatFilePaths.Count;
                //string strActionMsg = "[Business Category Master] : " + introwCount + " number of rows inserted into database by " + Session["SystemUser"].ToString();
                //GlobalFunctions.saveInsertUserAction("Category_Master", strActionMsg, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID), lngCompanyId, Request); //Call to user Action Log

                //introwCount = 0;
                //introwCount = introwCount + lstSubCatFilePaths.Count;
                //strActionMsg = strActionMsg + "\n[Business SubCategory Master] : " + introwCount + " number of rows inserted into database by " + Session["SystemUser"].ToString();
                //GlobalFunctions.saveInsertUserAction("Sub_Category_Master", strActionMsg, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID), lngCompanyId, Request); //Call to user Action Log
                # endregion catandsubcat

                if (intFailureCount > 0)
                {
                    string strFailureMsgCount = "\n[" + Session["SystemUser"].ToString() + " => Business Information Master] : " + intFailureCount + " number of rows insertion failed due to missing category and subcategory";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append(" bootbox.alert('");
                    sb.Append(strFailureMsgCount);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());
                }

                if (introwCount > 0)
                {
                    string strActionMsg = "\n[Business Information Master] : " + introwCount + " number of rows inserted into database by " + Session["SystemUser"].ToString();
                    GlobalFunctions.saveInsertUserAction("Information_Master", strActionMsg, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID), lngCompanyId, Request); //Call to user Action Log
                    Div2.InnerHtml = "Information stored successfully";
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnRead_ServerClick", "InformationBusinessMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
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

        //check whether subcategory with same name already exist in table
        public bool chkSubCatExist(string pstrSubCatNm)
        {
            try
            {
                TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                int intTalukaId = objTal.TalukaID;
                string strId = Convert.ToString(intTalukaId);
                string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;

                string mStrSelectQuery = "select scm.SCM_bIntSubCatId,";
                mStrSelectQuery += " scm.SCM_vCharName_En,";
                mStrSelectQuery += " scm.SCM_nVarName_Reg,";
                mStrSelectQuery += " CASE scm.SCM_bItIsActive WHEN 1 THEN 'Yes' ELSE 'No' END as 'SCM_bItIsActive',";
                mStrSelectQuery += " CASE WHEN scm.SCM_vCharCatImgClass is not null";
                mStrSelectQuery += " Then 'fa ' + scm.SCM_vCharCatImgClass";
                mStrSelectQuery += " WHEN scm.SCM_vCharSubCatImgPath is null";
                mStrSelectQuery += " Then 'fa-circle-question'";
                mStrSelectQuery += " else ''";
                mStrSelectQuery += " END AS 'SCM_vCharCatImgClass',";
                mStrSelectQuery += " CASE WHEN (scm.SCM_vCharSubCatImgPath is not null And scm.SCM_vCharCatImgClass is null)";
                mStrSelectQuery += " then scm.SCM_vCharSubCatImgPath else '' END AS 'SCM_vCharSubCatImgPath',";
                mStrSelectQuery += " cm.CM_vCharName_En";
                mStrSelectQuery += " from Sub_Category_Master_17 scm,Category_Master_17 cm";
                mStrSelectQuery += " Where scm.SCM_bIntCatId=cm.CM_bIntCatId AND scm.SCM_bItIsActive = 1 AND scm.SCM_vCharName_En Like '%" + pstrSubCatNm + "%' AND SCM_iNtEntryType=0";

                DataTable dtSubCatDetails = SqlHelper.ReadTable(mStrSelectQuery, conString, false);

                if (dtSubCatDetails.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("chkSubCatExist", "SubCategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
            return false;
        }

        //Check whether Category names exist or not
        private string CheckDuplicateValues(string strCatName, string strCatRegName, long CatID)
        {
            try
            {
                DataTable dtCatData = SqlHelper.ReadTable("spChkCatNameDuplicationBusiness", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                       SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                       SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 0),
                                       SqlHelper.AddInParam("@vCharCatName", SqlDbType.VarChar, strCatName));

                if (dtCatData.Rows.Count > 0)
                {
                    DataRow dtrow = dtCatData.Rows[0];
                    long intCatID = Convert.ToInt64(dtrow["CM_bIntCatId"].ToString());
                    if (intCatID != CatID)
                    {
                        return ("Category Regional  Name Already Exists!!");
                    }

                }
                if (strCatRegName != "")
                {
                    dtCatData = SqlHelper.ReadTable("spChkCatRegNameDuplicationBusiness", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, 0),
                                SqlHelper.AddInParam("@vCharRegionalCatName", SqlDbType.NVarChar, strCatRegName));

                    if (dtCatData.Rows.Count > 0)
                    {
                        DataRow dtrow = dtCatData.Rows[0];
                        long intCatID = Convert.ToInt64(dtrow["CM_bIntCatId"].ToString());
                        if (intCatID != CatID)
                        {
                            return ("Category Regional  Name Already Exists!!");
                        }

                    }
                }

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("CheckDuplicateValues", "Category", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }

            return "";
        }

        protected void btnImportonPassword_ServerClick(object sender, EventArgs e)
        {
            OleDbConnection con;
            //Code for import on password.
            //----------------------------------------------
            try
            {
                if (txtPwdCode.Text.Trim() == "")
                {
                    lblMsgError.Visible = true;
                }
                else
                {
                    string strLoginPass = Convert.ToString(Request.Cookies["MStore_Cookie_Password"].Value);
                    if (strLoginPass.Equals(txtPwdCode.Text.Trim()) == true)
                    {

                        //Password cleared for individual
                        int i;
                        HttpPostedFile f;
                        int introwCount = 0, intFailureCount = 0;
                        List<string> lstCategoryName = new List<string>();
                        List<string> lstCatFilePaths = new List<string>();
                        HttpFileCollection uploadedFiles = Request.Files;

                        //string strStoredFilepath = "";
                        string strFileNm = "";
                        long lngAmId = 0;
                        bool blnExcelIsEmpty = false;

                        //Check whether files being selected or not
                        if (FileUploadControl1.HasFile)
                        {
                            //Read an Excel file sheet for Subcategory Details
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
                            //else
                            //{
                            //    Div2.Attributes["class"] = "alert alert-info ";
                            //    Div2.InnerHtml = "Please select Only .xls or .xlsx file for uploading !!!";
                            //    return;
                            //}

                            var dtCategoryDetails = new DataTable();

                            if (ext.Trim() == ".csv")
                            {
                                using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(path)), true))
                                {
                                    dtCategoryDetails.Load(csvReader);
                                }
                            }
                            else
                            {
                                Div2.Attributes["class"] = "alert alert-info ";
                                Div2.InnerHtml = "Please select Only .csv file for uploading !!!";
                                return;

                            }
                            //con = new OleDbConnection(ConStr);
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


                                //Store Information Description
                                //if (f.ContentLength > 0 && f.FileName == "MStoreInfo.xlsx")
                                if (f.ContentLength > 0 && (f.FileName == "Information.csv"))
                                {
                                    if (dtCategoryDetails.Rows.Count <= 0)
                                    {
                                        blnExcelIsEmpty = true;
                                    }

                                    strFileNm = f.FileName;
                                    foreach (DataRow drImgColVal in dtCategoryDetails.Rows)
                                    {
                                        if (drImgColVal[2].ToString().Trim() != "")
                                        {
                                            //Code to check whether Category names already exist in db.
                                            lngAmId = 0;

                                            int IsActive = 1;

                                            TalukaData objTal = (TalukaData)Session["TalukaDetails"];
                                            int intTalukaId = objTal.TalukaID;
                                            string strId = Convert.ToString(intTalukaId);
                                            string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
                                            DataTable dtCategoryid = SqlHelper.ReadTable("SELECT CM_bIntCatId FROM Category_Master_17  WHERE CM_bItIsActive = 1 AND CM_iNtEntryType=1 AND CM_vCharName_En = @Name ", conString, false, SqlHelper.AddInParam("@Name", SqlDbType.VarChar,drImgColVal[0].ToString()));

                                            int intCategoryid = -1;
                                            if (dtCategoryid.Rows.Count > 0)
                                            {
                                                DataRow rowCategoryId = dtCategoryid.Rows[0];
                                                intCategoryid = Convert.ToInt32(rowCategoryId["CM_bIntCatId"]);
                                            }
                                            else
                                            {
                                                Div2.Attributes["class"] = "alert alert-info";
                                                Div2.InnerHtml = "Category Name not exist.";
                                                return;
                                            }

                                            //Code to check whether SubCategory names already exist in db.
                                            string strQuery = "Select SCM_bIntSubCatId from Sub_Category_Master_17 where SCM_vCharName_En Like @Name AND SCM_iNtEntryType=1";
                                            DataTable dtCatIdData = SqlHelper.ReadTable(strQuery, false,
                                                SqlHelper.AddInParam("@Name",SqlDbType.VarChar,drImgColVal[1].ToString()));

                                            if (dtCatIdData.Rows.Count > 0)
                                            {
                                                DataRow row = dtCatIdData.Rows[0];
                                                lngAmId = Convert.ToInt32(row["SCM_bIntSubCatId"]);
                                                if (lngAmId == 0) lngAmId = -1;
                                            }
                                            else
                                            {
                                                Div2.Attributes["class"] = "alert alert-info";
                                                Div2.InnerHtml = "SubCategory Name not exist.";
                                                return;
                                            }

                                            //Check whether information already exist in table 
                                            strQuery = "Select IM_bIntInfoId from Information_Master_" + Convert.ToString(((TalukaData)Session["TalukaDetails"]).TalukaID) + " where IM_vCharInfoName_En Like '" + drImgColVal[2].ToString() + "' AND IM_bItIsActive=1 AND IM_intInfoType=1 ";
                                            strQuery = strQuery + " AND IM_bIntCatId = " + intCategoryid;
                                            strQuery = strQuery + " AND IM_bIntSubCatId = " + lngAmId;

                                            DataTable dtInfoTemp = SqlHelper.ReadTable(strQuery, false);
                                            long lngInfoidexisting = 0;
                                            if (dtInfoTemp.Rows.Count > 0)
                                            {
                                                DataRow drInfoId = dtInfoTemp.Rows[0];
                                                lngInfoidexisting = Convert.ToInt64(drInfoId["IM_bIntInfoId"]);

                                            }
                                            string strInfotype = drImgColVal[57].ToString();
                                            //Check whether category is mandatory
                                            if (intCategoryid != -1 && Convert.ToInt32(strInfotype) == 1)
                                            {
                                                //Code to insert or modify Information free master.
                                                DataTable dtInsertedData = SqlHelper.ReadTable("spInsertUpdateInfoMaster1", true,
                                                                      SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                                                        SqlHelper.AddInParam("@IM_bIntSubCatId", SqlDbType.BigInt, Convert.ToInt64(lngAmId)),
                                                                       SqlHelper.AddInParam("@IM_bIntCatId", SqlDbType.BigInt, Convert.ToInt64(intCategoryid)),
                                                                       SqlHelper.AddInParam("@IM_IntCharInfoType", SqlDbType.Int, 1),
                                                                        SqlHelper.AddInParam("@IM_vCharInfoName_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[2])),
                                                                         SqlHelper.AddInParam("@IM_nVarInfoName_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[3])),
                                                                         SqlHelper.AddInParam("@IM_vCharCity_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[4])),
                                                                         SqlHelper.AddInParam("@IM_nVarCity_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[5])),
                                                                         SqlHelper.AddInParam("@IM_vCharInfoAdd_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[6])),
                                                                         SqlHelper.AddInParam("@IM_nVarInfoAdd_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[7])),
                                                                          SqlHelper.AddInParam("@IM_vCharInfoEmail", SqlDbType.VarChar, Convert.ToString(drImgColVal[8])),
                                                                           SqlHelper.AddInParam("@IM_vCharInfoPhone1", SqlDbType.VarChar, Convert.ToString(drImgColVal[9])),
                                                                            SqlHelper.AddInParam("@IM_vCharInfoPhone2", SqlDbType.VarChar, Convert.ToString(drImgColVal[10])),
                                                                             SqlHelper.AddInParam("@IM_vCharInfoPhone3", SqlDbType.VarChar, Convert.ToString(drImgColVal[11])),
                                                                             SqlHelper.AddInParam("@IM_decLatitude", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[12].ToString())),
                                                                            SqlHelper.AddInParam("@IM_decLongitude", SqlDbType.Decimal, Convert.ToDecimal(drImgColVal[13].ToString())),
                                                                            SqlHelper.AddInParam("@IM_vCharPincode_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[14])),
                                                                            SqlHelper.AddInParam("@IM_nVarPincode_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[15])),

                                                                            // SqlHelper.AddInParam("@IM_vCharCity_En", SqlDbType.VarChar, drImgColVal[16].ToString()),
                                                    //SqlHelper.AddInParam("@IM_nVarCity_Reg", SqlDbType.NVarChar, drImgColVal[17].ToString()),
                                                                             SqlHelper.AddInParam("@IM_vCharInfoExtraLabel1_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[16])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraLabel1_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[17])),
                                                                            SqlHelper.AddInParam("@IM_vCharInfoExtraValue1_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[18])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraValue1_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[19])),


                                                                             SqlHelper.AddInParam("@IM_vCharInfoExtraLabel2_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[20])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraLabel2_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[21])),
                                                                            SqlHelper.AddInParam("@IM_vCharInfoExtraValue2_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[22])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraValue2_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[23])),

                                                                             SqlHelper.AddInParam("@IM_vCharInfoExtraLabel3_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[24])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraLabel3_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[25])),
                                                                            SqlHelper.AddInParam("@IM_vCharInfoExtraValue3_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[26])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraValue3_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[27])),


                                                                             SqlHelper.AddInParam("@IM_vCharInfoExtraLabel4_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[28])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraLabel4_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[29])),
                                                                            SqlHelper.AddInParam("@IM_vCharInfoExtraValue4_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[30])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraValue4_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[31])),

                                                                             SqlHelper.AddInParam("@IM_vCharInfoExtraLabel5_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[32])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraLabel5_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[33])),
                                                                            SqlHelper.AddInParam("@IM_vCharInfoExtraValue5_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[34])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraValue5_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[35])),

                                                                             SqlHelper.AddInParam("@IM_vCharInfoExtraLabel6_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[36])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraLabel6_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[37])),
                                                                            SqlHelper.AddInParam("@IM_vCharInfoExtraValue6_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[38])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraValue6_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[39])),

                                                                             SqlHelper.AddInParam("@IM_vCharInfoExtraLabel7_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[40])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraLabel7_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[41])),
                                                                            SqlHelper.AddInParam("@IM_vCharInfoExtraValue7_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[42])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraValue7_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[43])),

                                                                            SqlHelper.AddInParam("@IM_vCharInfoExtraLabel8_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[44])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraLabel8_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[45])),
                                                                            SqlHelper.AddInParam("@IM_vCharInfoExtraValue8_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[46])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraValue8_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[47])),

                                                                            SqlHelper.AddInParam("@IM_vCharInfoExtraLabel9_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[48])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraLabel9_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[49])),
                                                                            SqlHelper.AddInParam("@IM_vCharInfoExtraValue9_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[50])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraValue9_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[51])),

                                                                            SqlHelper.AddInParam("@IM_vCharInfoExtraLabel10_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[52])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraLabel10_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[53])),
                                                                            SqlHelper.AddInParam("@IM_vCharInfoExtraValue10_En", SqlDbType.VarChar, Convert.ToString(drImgColVal[54])),
                                                                            SqlHelper.AddInParam("@IM_nVarInforExtraValue10_Reg", SqlDbType.NVarChar, Convert.ToString(drImgColVal[55])),
                                                                            SqlHelper.AddInParam("@IM_bitIsActive", SqlDbType.Bit, 1),
                                                                            SqlHelper.AddInParam("@IM_vCharUrl", SqlDbType.VarChar, Convert.ToString(drImgColVal[58])),
                                                                            SqlHelper.AddInParam("@IM_bitIsEmergency", SqlDbType.Bit, Convert.ToInt32(drImgColVal[56])),
                                                                            SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, lngInfoidexisting));
                                             
                                                DataRow drInfo = dtInsertedData.Rows[0];
                                                txtInformationID.Text = drInfo["IM_bIntInfoId"].ToString();

                                                int mInformationId = Convert.ToInt32(txtInformationID.Text);
                                                string strIsPaid = Convert.ToString(drImgColVal[59]);

                                                int intIsPaid = 0;
                                                double dblAmount = 0;
                                                string dtFrmDate = "";
                                                string dtToDate = "";

                                                if (strIsPaid == "Y")
                                                {
                                                    intIsPaid = 1;
                                                    if (intIsPaid == 1)
                                                    {
                                                        dblAmount = Convert.ToDouble(drImgColVal[60]);
                                                        dtFrmDate = Convert.ToString(drImgColVal[61]);
                                                        dtToDate = Convert.ToString(drImgColVal[62]);
                                                    }
                                                }
                                                else { intIsPaid = 0; }

                                                string strUpdatePayment = " UPDATE Information_Master_" + Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID);
                                                strUpdatePayment = strUpdatePayment + " SET IM_bitIsPaid = " + intIsPaid;
                                                strUpdatePayment = strUpdatePayment + " ,IM_decAmt = " + dblAmount;
                                                strUpdatePayment = strUpdatePayment + " ,IM_dtFromDate = CAST(SWITCHOFFSET('" + dtFrmDate + "', '+05:30') As DATETIME)";
                                                strUpdatePayment = strUpdatePayment + " ,IM_dtToDate = CAST(SWITCHOFFSET('" + dtToDate + "', '+05:30') As DATETIME)";
                                                strUpdatePayment = strUpdatePayment + " WHERE IM_bIntInfoId = " + Convert.ToInt32(mInformationId);

                                                int intNoofRows = SqlHelper.UpdateDatabase(strUpdatePayment);
                                            }
                                            else
                                            {
                                                //Code if no category is specified.
                                                intFailureCount = intFailureCount + 1;
                                            }
                                        }
                                    }//End of foreach
                                } //End of if
                                else
                                {
                                    Div2.Attributes["class"] = "alert alert-info";
                                    Div2.InnerHtml = "File name should be MStoreInfo.xlsx";
                                }
                            }//End of for statement with multiple excel sheets

                            //Code to insert an action log for Information Details insertion here.
                            long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);

                            introwCount = introwCount + lstCatFilePaths.Count - intFailureCount;
                            string strActionMsg = "[Business Information Master] : " + introwCount + " number of rows inserted into database by " + Session["SystemUser"].ToString();
                            strActionMsg = strActionMsg + " and " + intFailureCount + " no of rows insertion failed.";
                            GlobalFunctions.saveInsertUserAction("Information_Master", strActionMsg, Convert.ToInt32(((TalukaData)Session["TalukaDetails"]).TalukaID), lngCompanyId, Request); //Call to user Action Log

                            if (intFailureCount > 0)
                            {
                                string strFailureMsgCount = "[Business Information Master] : " + intFailureCount + " rows insertion failed!";

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

                            showInfoGrid();   //Call to display information.

                            if (blnExcelIsEmpty == false)
                            {
                                Div2.Attributes["class"] = "alert alert-info";
                                Div2.InnerHtml = "Information Added Successfully!!!";
                            }
                            else
                            {
                                Div2.Attributes["class"] = "alert alert-info";
                                Div2.InnerHtml = "Excel Sheet is found to be empty!";
                            }

                            //Div2.Attributes["class"] = "alert alert-info";
                            //Div2.InnerHtml = "Information Added Successfully!!!";
                            //if (con.State == ConnectionState.Open)
                            //{
                            //    con.Close();
                            //}
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
                        Div2.InnerHtml = "Invalid Password !!!";
                    }
                }//End of else part
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("btnImportonPassword_ServerClick", "Business InformationMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                Div2.Attributes["class"] = "alert alert-info blink-border";
                Div2.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }

        }

        protected void drdModifyInfoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Code to check whether value is updated or not.
            long lngModifyVal = Convert.ToInt64(drdModifyInfoType.SelectedItem.Value);

            if (lngModifyVal != Convert.ToInt64(hfInfotype.Value))
                IsEmergencyUpdate = true;
        }

        protected void drIsPaid_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Once paid is clicked visible the remaining controls.
            if (drIsPaid.SelectedValue.ToString().Trim() != "2")
            {
                txtAmount.Enabled = true;
                txtFromdate.Enabled = true;
                txtTodate.Enabled = true;
                txtTodate.Text = "";
            }
            else
            {
                txtTodate.Text = "";
                txtAmount.Text = "";
                txtFromdate.Text = "";

                txtAmount.Enabled = false;
                txtFromdate.Enabled = false;
                txtTodate.Enabled = false;
            }
        }

        protected void drModifyIsPaid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Once paid is clicked visible the remaining controls.
            if (drModifyIsPaid.SelectedValue.ToString().Trim() != "2")
            {
                txtModifyAmount.Enabled = true;
                txtModifyFromdate.Enabled = true;
                txtModifyTodate.Enabled = true;

                if (strToDate.Trim() != "") txtModifyTodate.Text = strToDate;
                if (strFrmDate.Trim() != "") txtModifyFromdate.Text = strFrmDate;
                if (strAmt.Trim() != "") txtModifyAmount.Text = strAmt;
            }
            else
            {
                strAmt = txtModifyAmount.Text;
                strFrmDate = txtModifyFromdate.Text;
                strToDate = txtModifyTodate.Text;

                txtModifyTodate.Text = "";
                txtModifyAmount.Text = "";
                txtModifyFromdate.Text = "";

                txtModifyAmount.Enabled = false;
                txtModifyFromdate.Enabled = false;
                txtModifyTodate.Enabled = false;
            }
        }
    }

    //public class PaidDetails
    //{
    //    public PaidDetails(string str1, string str2, string str3)
    //    {
    //        strAmt = str1;
    //        strFrmDate = str2;
    //        strToDate = str3;
    //    }

    //    public string strAmt { get; set; }
    //    public string strFrmDate { get; set; }
    //    public string strToDate { get; set; }
    //}
}