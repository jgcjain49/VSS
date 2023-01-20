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
    public partial class OpenOrdersReport : System.Web.UI.Page
    {
      // public static DropDownList ddUserLevelSearch;
        static DataTable dtReport;
        public static DataTable dtSmartOrders;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TalukaDetails"] != null)
            {
                if (!IsPostBack)
                {
                    // fillDistriList();
                }
                actionInfo.Visible = true;
                LoadUserLevel();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }


        public void LoadUserLevel()
        {
            string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;                    
            DataTable dt = SqlHelper.ReadTable("select DRC_bIntID,DRC_vCharTypeName from Dove_Rating_Center_17 ", conString, false);

            if (dt.Rows.Count > 0)
            {
                // DropDownList drdlObjectsearch = (DropDownList)e.Row.FindControl("ddlUserLevelSearch");

                ddUserLevelSearch.DataSource = dt;
                ddUserLevelSearch.DataTextField = "DRC_vCharTypeName";
                ddUserLevelSearch.DataValueField = "DRC_bIntID";
                ddUserLevelSearch.DataBind();
                ddUserLevelSearch.Items.Insert(0, new ListItem("--Select User Level--", "0"));
            }
        }

        //protected void fillDistriList()
        //{
        //    string sql;
        //    if (Session["UserType"] == "A")
        //    {
        //        sql = "Select DD_bIntId,DD_nVarFullname from Dove_Dist_t_" + Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
        //    }
        //    else
        //    {
        //        sql = "Select DD_bIntId,DD_nVarFullname from Dove_Dist_t_17 where DD_bIntId='" + Session["SystemUserID"] + "'";
        //    }
        //    DataTable dtTable = SqlHelper.ReadTable(sql, false);
        //    cmbDistributer.Items.Clear();
        //    if (dtTable.Rows.Count > 0)
        //    {
        //        cmbDistributer.AppendDataBoundItems = true;
        //        cmbDistributer.Items.Insert(0, new ListItem("<< Select Distributor >>", "-1"));
        //        cmbDistributer.DataSource = dtTable;
        //        cmbDistributer.DataTextField = "DD_nVarFullname";
        //        cmbDistributer.DataValueField = "DD_bIntId";
        //        cmbDistributer.DataBind();
        //        cmbDistributer.SelectedIndex = 0;

        //    }
        //    else
        //    {
        //        cmbDistributer.AppendDataBoundItems = true;
        //        cmbDistributer.Items.Insert(0, new ListItem("<< Distributor not found  >>", "-1"));
        //        cmbDistributer.SelectedIndex = 0;
        //    }

        //}

        protected void btnresetclick(object sender, EventArgs e)
        {
            ordPlcDt.Value = "";
            ordPlcDtTill.Value = "";
            txtorgid.Value = "";
            txtusermob.Value = "";
        }

        //public void LoadUserLevel()
        //{
        //    string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;                    
        //    DataTable dt = SqlHelper.ReadTable("select DRC_bIntID,DRC_vCharTypeName from Dove_Rating_Center_17 ", conString, false);

        //    if (dt.Rows.Count > 0)
        //    {
        //        // DropDownList drdlObjectsearch = (DropDownList)e.Row.FindControl("ddlUserLevelSearch");

        //        ddUserLevelSearch.DataSource = dt;
        //        ddUserLevelSearch.DataTextField = "DRC_vCharTypeName";
        //        ddUserLevelSearch.DataValueField = "DRC_bIntID";
        //        ddUserLevelSearch.DataBind();
        //        ddUserLevelSearch.Items.Insert(0, new ListItem("--Select User Level--", "0"));
        //    }
        //}


        protected void grdReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdReport.PageIndex = e.NewPageIndex;
            btnFetchReportClick(sender, e);
        }

        protected void btnFetchReportClick(object sender, EventArgs e)
        {
            try
            {
                string strId;
                if (Convert.ToString(Session["UserType"]) == "A")
                {
                    strId = "M1";
                }
                else
                {
                    strId = Convert.ToString(Session["SystemUserID"]);
                }
                string qryParamOne = "";
                string qryParamTwo = "";
                string qryParamThree = "";
                string qryParamFour = "";
                List<string> paramList = new List<string>();

                if (ordPlcDt != null && ordPlcDt.Value != "")
                {
                    if (ordPlcDtTill != null && ordPlcDtTill.Value != "")
                    {
                        qryParamOne = " CONVERT(VARCHAR(10), DUOH_nVarProdOrderTime , 111) between '" + (ordPlcDt.Value) + "' and '" + ordPlcDtTill.Value + "'";
                    }
                    else
                    {
                        qryParamOne = " CONVERT(VARCHAR(10), DUOH_nVarProdOrderTime , 111) = '" + (ordPlcDt.Value) + "' ";
                    }
                    paramList.Add(qryParamOne);
                }
                if (txtOrdNum != null && txtOrdNum.Value != "")
                {
                    qryParamTwo = " DUOH_nVarProdOrderNumber = '" + (txtOrdNum.Value) + "' ";
                    paramList.Add(qryParamTwo);
                }
                if (txtusermob != null && txtusermob.Value != "")
                {
                    qryParamThree = " UD_vCharPhoneNumber = '" + (txtusermob.Value) + "' ";
                    paramList.Add(qryParamThree);
                }
                if (txtorgid != null && txtorgid.Value != "")
                {
                    qryParamFour = " UD_SubUserId = '" + (txtorgid.Value) + "' ";
                    paramList.Add(qryParamFour);
                }
                //base query
               string qSmartOrders = " SELECT [DUOH_bIntOrderHistoryId], [DUOH_bIntUserId], [dbo].[User_Data_17].[UD_vCharName],[Dove_User_Membership_17].DUM_bIntMemberType, " +
                                     " [dbo].[User_Data_17].[UD_vCharPhoneNumber], [dbo].[User_Data_17].UD_SubUserId, DUOH_intGbeanRcvd, " +
                                     " [dbo].[User_Data_17].[UD_vCharCity], [DUOH_nVarProductName], [DUOH_decProdWeight], [DUOH_decProdQty]," +
                                     " [DUOH_decProdBuyPrice], [DUOH_nVarProdOrderNumber], [DUOH_nVarProdOrderTime], [DUOH_vCharProdOrderType], " +
                                     " [DUOH_nVarProdCancelTime], [DUOH_decProdCancelPrice], [DUOH_decProdDiffPrice], [DUOH_nVarOrderStatus], " +
                                     " [DUOH_decUpperlmtPrice], [DUOH_vCharUppLmtRatio], [DUOH_vCharLowLmtRatio], [DUOH_decLowerlmtPrice], " +
                                     " [DUOH_decPrePayAmt],[DUOH_intGbeanRcvd], round((300-DUOH_decPrePayAmt-3)*100/118,2) Net_ServFee, "+
                                     " round(round((300-DUOH_decPrePayAmt-3)*100/118,2)*0.18,2) Net_Gst "+
                                     " FROM Dove_User_Week_Order_History_17 inner join  [dbo].[User_Data_17] " +
                                     " ON Dove_User_Week_Order_History_17.[DUOH_bIntUserId]=[dbo].[User_Data_17].[UD_bIntId] " +
                                     " INNER JOIN [Dove_User_Membership_17] ON DUM_bIntUserId = DUOH_bIntUserId  " +
                                     " where COALESCE(DUOH_nVarProdCancelTime,'') = ''  " +
                                     " And UD_SubUserId LIKE '%" + strId + "%' ";


                //Filter(where) clause
                /* commented to modify logic for including more than 2 parameters
                if (qryParamOne != "" || qryParamTwo != "")
                {
                    qSmartOrders += " where " + qryParamOne + ((qryParamOne != "" && qryParamTwo != "") ? " AND " : " ") + qryParamTwo;
                }*/
                if (paramList.Count > 0)
                {
                    //qSmartOrders += " WHERE " + paramList[0];

                    // if (paramList.Count > 1)
                    {
                        for (int i = 0; i < paramList.Count; i++)
                        {
                            qSmartOrders += " AND " + paramList[i];
                        }
                    }
                }

                //Sorting(order by) clause
                qSmartOrders += " ORDER by DUOH_bIntOrderHistoryId DESC ";

                //" where  ";

                //string sql = "SELECT [DUOH_bIntOrderHistoryId], [DUOH_bIntUserId], [DUOH_nVarProductName], [DUOH_decProdWeight], [DUOH_decProdQty], " +
                //            "[DUOH_decProdBuyPrice], [DUOH_nVarProdOrderNumber], [DUOH_nVarProdOrderTime], [DUOH_vCharProdOrderType], " +
                //            "[DUOH_nVarProdCancelTime], [DUOH_decProdCancelPrice], [DUOH_decProdDiffPrice], [DUOH_nVarOrderStatus], " +
                //            "[DUOH_decUpperlmtPrice], [DUOH_vCharUppLmtRatio], [DUOH_vCharLowLmtRatio], [DUOH_decLowerlmtPrice], [DUOH_decPrePayAmt] " +
                //            " FROM Dove_User_Week_Order_History_" + Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId) + " +"
                //+"where CONVERT(VARCHAR(10), DUOH_nVarProdOrderTime , 111) between CONVERT(VARCHAR(10), @dtSdate , 111) and CONVERT(VARCHAR(10), @dtEdate , 111 ";

                dtSmartOrders = SqlHelper.ReadTable(qSmartOrders, Convert.ToString(Session["SystemUserSqlConnectionString"]), false);
                // SqlHelper.AddInParam("@dtEdate", SqlDbType.DateTime, Convert.ToDateTime()));
                if (dtSmartOrders.Rows.Count > 0)
                {
                    DataColumn totWeight = new DataColumn();
                    totWeight.ColumnName = "Total_Weight";
                    totWeight.DataType = System.Type.GetType("System.Decimal");
                    totWeight.Expression = "DUOH_decProdWeight * DUOH_decProdQty";

                    //DataColumn gbean = new DataColumn();
                    //gbean.ColumnName = "GBean";
                    //gbean.DataType = System.Type.GetType("System.Decimal");
                    //gbean.Expression = "3 * DUOH_decProdQty *" +
                    //                   "IIF(DUOH_nVarProductName like '*Gold Bar*'," +
                    //                   "(DUOH_decProdWeight/10), " +
                    //                   "(DUOH_decProdWeight/500) ) ";

                    DataColumn prepayment = new DataColumn();
                    prepayment.ColumnName = "Prepayment";
                    prepayment.DataType = System.Type.GetType("System.Decimal");
                    prepayment.Expression = "DUOH_decPrePayAmt * DUOH_decProdQty * " +
                                            " IIF(DUOH_nVarProductName LIKE '*Gold Bar*', " +
                                            " (DUOH_decProdWeight/10), " +
                                            " (DUOH_decProdWeight/500) ) ";

                    DataColumn totPrepay = new DataColumn();
                    totPrepay.ColumnName = "Total_Prepay";
                    totPrepay.DataType = System.Type.GetType("System.Decimal");
                    totPrepay.Expression = "300 * DUOH_decProdQty * " +
                                           " IIF(DUOH_nVarProductName LIKE '*Gold Bar*', " +
                                           " (DUOH_decProdWeight/10), " +
                                           " (DUOH_decProdWeight/500) ) ";

                    DataColumn srvcChrg = new DataColumn();
                    srvcChrg.ColumnName = "Srvc_Fee";
                    srvcChrg.DataType = System.Type.GetType("System.Decimal");
                    srvcChrg.Expression = " IIF(DUOH_nVarProductName LIKE '*Gold Bar*', " +
                                           " (DUOH_decProdWeight/10*DUOH_decProdQty * Net_ServFee), " +
                                           " (DUOH_decProdWeight/500*DUOH_decProdQty * Net_ServFee) ) ";

                    DataColumn gst = new DataColumn();
                    gst.ColumnName = "GST";
                    gst.DataType = System.Type.GetType("System.Decimal");
                    //gst.Expression = "Srvc_Fee*0.18";
                    gst.Expression = " IIF(DUOH_nVarProductName LIKE '*Gold Bar*', " +
                                           " (DUOH_decProdWeight/10*DUOH_decProdQty*Net_Gst), " +
                                           " (DUOH_decProdWeight/500*DUOH_decProdQty*Net_Gst) ) ";

                    dtSmartOrders.Columns.Add(totWeight);
                    dtSmartOrders.Columns.Add(prepayment);
                    dtSmartOrders.Columns.Add(totPrepay);
                    dtSmartOrders.Columns.Add(srvcChrg);
                    //dtSmartOrders.Columns.Add(gbean);
                    dtSmartOrders.Columns.Add(gst);

                    downloadBtnDiv.Attributes["style"] = "text-align: right; display: block;";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "getRateAndCalDIff()", true);
                }
                grdReport.DataSource = dtSmartOrders;
                grdReport.DataBind();


            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showgrid", "showDistrigrid", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                //  updateActionDiv.Attributes["class"] = "alert alert-info blink-border";
                //  updateActionDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Owner";
            }
            // showgrid();
        }
        protected void grdReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            decimal totValDiff = 0.0m, totgst = 0.0m, totservicefee = 0.0m, totprepay = 0.0m, totgbean = 0.0m, totsumprepay = 0.0m;

            object objtotValDiff = dtSmartOrders.Compute("Sum(DUOH_decProdDiffPrice)", string.Empty);
            if (objtotValDiff != null && objtotValDiff != System.DBNull.Value)
            { totValDiff = Convert.ToDecimal(objtotValDiff); }

            object objtotgst = dtSmartOrders.Compute("Sum(GST)", string.Empty);
            if (objtotgst != null && objtotgst != System.DBNull.Value)
            { totgst = Convert.ToDecimal(objtotgst); }

            object objtotservicefee = dtSmartOrders.Compute("Sum(Srvc_Fee)", string.Empty);
            if (objtotservicefee != null && objtotservicefee != System.DBNull.Value)
            { totservicefee = Convert.ToDecimal(objtotservicefee); }

            object objtotprepay = dtSmartOrders.Compute("Sum(Prepayment)", string.Empty);
            if (objtotprepay != null && objtotprepay != System.DBNull.Value)
            { totprepay = Convert.ToDecimal(objtotprepay); }

            object objtotgbean = dtSmartOrders.Compute("Sum(DUOH_intGbeanRcvd)", string.Empty);
            if (objtotgbean != null && objtotgbean != System.DBNull.Value)
            { totgbean = Convert.ToDecimal(objtotgbean); }

            object objtotsumprepay = dtSmartOrders.Compute("Sum(Total_Prepay)", string.Empty);
            if (objtotsumprepay != null && objtotsumprepay != System.DBNull.Value)
            { totsumprepay = Convert.ToDecimal(objtotsumprepay); }


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblSrvcChrgTot = (Label)e.Row.FindControl("lblSrvcChrgTot");
                lblSrvcChrgTot.Text = totservicefee.ToString("N3");

                Label lblgstTot = (Label)e.Row.FindControl("lblgstTot");
                lblgstTot.Text = totgst.ToString("N3");

                Label lblPrePayAmtTot = (Label)e.Row.FindControl("lblPrePayAmtTot");
                lblPrePayAmtTot.Text = totprepay.ToString("N3");

                Label lblgbeanTot = (Label)e.Row.FindControl("lblgbeanTot");
                lblgbeanTot.Text = totgbean.ToString("N3");


                Label lblTotPrePayTot = (Label)e.Row.FindControl("lblTotPrePayTot");
                lblTotPrePayTot.Text = totsumprepay.ToString("N3");

                Label lblProdDiffPriceTot = (Label)e.Row.FindControl("lblProdDiffPriceTot");
                lblProdDiffPriceTot.Text = totValDiff.ToString("N3");
            }
        }

        protected void btnExpotClick(object sender, EventArgs e)
        {
            grdReport.AllowPaging = false;
            btnFetchReportClick(sender, e);
            string fileName = "Smartbuy_OpenOrders_" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", fileName));
           // Response.Charset = "";
            StringWriter stringwriter = new StringWriter();
            HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
            //hide footer before writing to file
            //grdReport.ShowFooter = false;
            grdReport.FooterRow.Visible = false;
            grdReport.RenderControl(htmlwriter);
            string style = @"<style> .ordNum { mso-number-format:\@; width: 170px; } </style>";
            Response.Write(style);
            Response.Write(stringwriter.ToString());
            Response.End();
            //show footer again
            grdReport.FooterRow.Visible = true;

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }


        //protected void btnFetchUserReportClick(object sender, EventArgs e)
        //{
        //    long lngentryCount;
        //   // if (Convert.ToInt64(cmbUserList.SelectedItem.Value) != 0)
        //   // {
        //        //changed query to fetch only pending orders - Pritesh_29-08-2020
        //        //string sql = " SELECT [DUOH_bIntOrderHistoryId],[DUOH_bIntUserId],[DUOH_nVarProductName],[DUOH_decProdWeight], " +
        //        //            " [DUOH_decProdQty],[DUOH_decProdBuyPrice],[DUOH_nVarProdOrderNumber],[DUOH_nVarProdOrderTime], " +
        //        //            " [DUOH_vCharProdOrderType],[DUOH_nVarProdCancelTime],[DUOH_decProdCancelPrice],[DUOH_decProdDiffPrice] " +
        //        //            " FROM Dove_User_Week_Order_History_" + Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId) +
        //        //            " where DUOH_bIntUserId=" + Convert.ToInt64(cmbUserList.SelectedItem.Value) +
        //        //               " AND COALESCE(DUOH_nVarProdCancelTime,'') = '' ";
        //       // dtReport = SqlHelper.ReadTable(sql, false);
        //        if (dtReport.Rows.Count > 0)
        //        {
        //             //- Pritesh_29-08-2020
        //            //foreach (DataRow drReportRow in dtReport.Rows)
        //            //{

        //            //}
        //            //dtReport.Columns["DUOH_decProdCancelPrice"].Expression =
        //            //            "IIF(DUOH_nVarProductName LIKE '*Gold*', " +
        //            //            Convert.ToString(liveGoldASP.Text) + ", " +
        //            //            Convert.ToString(70.342) + " )";
        //            //dtReport.Columns["DUOH_decProdDiffPrice"].Expression =
        //            //            "IIF(DUOH_vCharProdOrderType = 'Booking', " +
        //            //            "(DUOH_decProdCancelPrice - DUOH_decProdBuyPrice) * DUOH_decProdWeight * DUOH_decProdQty , " +
        //            //            "(DUOH_decProdBuyPrice - DUOH_decProdCancelPrice) * DUOH_decProdWeight * DUOH_decProdQty )";
        //            grdReport.DataSource = dtReport;
        //            grdReport.DataBind();
        //            SetMessage(true, "");
        //            actionInfo.Visible = false;
        //            grdReport.Visible = true;
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "tryIt()", true);
        //        }
        //        else
        //        {
        //            SetMessage(true, "User's data not found !!!");
        //        }
        //   // }
        //    //else
        //    //{
        //    //    Session["TalukaDetails"] = null;
        //    //    SetMessage(false, "Select user to Continue... !!!!");
        //    //}
        //}

        protected void calculateDiff(object sender, EventArgs e)
        {
            //dtReport.Columns["DUOH_decProdCancelPrice"].Expression =
            //            "IIF(DUOH_nVarProductName LIKE '*Gold*', " +
            //            Convert.ToString(liveGold.Text) + ", " +
            //            Convert.ToString(70.342) + " )";
            dtReport.Columns["DUOH_decProdDiffPrice"].Expression =
                        "IIF(DUOH_vCharProdOrderType = 'Booking', " +
                        "(DUOH_decProdCancelPrice - DUOH_decProdBuyPrice) * DUOH_decProdWeight * DUOH_decProdQty , " +
                        "(DUOH_decProdBuyPrice - DUOH_decProdCancelPrice) * DUOH_decProdWeight * DUOH_decProdQty )";
            grdReport.DataSource = dtReport;
            grdReport.DataBind();
        }

        private void SetMessage(bool pBlnIsError, string pStrError)
        {
            actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            actionInfo.InnerHtml = pStrError;
        }

        //protected void cmbDistributer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillUserList(cmbDistributer.SelectedItem.Value);
        //    //added to hide gridview & display msg when distributor change - Pritesh_29-08-2020
        //    if (cmbDistributer.SelectedItem.Value == "-1")
        //        SetMessage(false, "Select user to Continue... !!!!");
        //    grdReport.Visible = false;
        //}
        //protected void cmbUserList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //added to hide gridview on user change - Pritesh_29-08-2020
        //    grdReport.Visible = false;
        //}

        //public void FillUserList(string SubUserId)
        //{
        //    string sql = "Select UD_bIntId,UD_vCharName from User_Data_" + Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId) + " where [UD_SubUserId]=@nCharDistId";
        //    string conString = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
        //    DataTable dtTable = SqlHelper.ReadTable(sql, conString, false,
        //          SqlHelper.AddInParam("@nCharDistId", SqlDbType.NVarChar, Convert.ToString(SubUserId)));

        //    cmbUserList.Items.Clear();
        //    if (dtTable.Rows.Count > 0)
        //    {
        //        cmbUserList.AppendDataBoundItems = true;
        //        cmbUserList.Items.Insert(0, new ListItem("<< Select User >>", "-1"));
        //        cmbUserList.DataSource = dtTable;
        //        cmbUserList.DataTextField = "UD_vCharName";
        //        cmbUserList.DataValueField = "UD_bIntId";
        //        cmbUserList.DataBind();
        //        cmbUserList.SelectedIndex = 0;
        //    }
        //    else
        //    {
        //        cmbUserList.AppendDataBoundItems = true;
        //        cmbUserList.Items.Insert(0, new ListItem("<< Users not found  >>", "-1"));
        //        cmbUserList.SelectedIndex = 0;
        //    }
        //}

    }
}