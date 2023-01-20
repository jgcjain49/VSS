using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PdfSharp;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Admin_CommTrex
{
    public partial class OrderApproval : System.Web.UI.Page
    {
        public int OrderID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TalukaDetails"] != null)
            {
                if (!IsPostBack)
                {
                    //_smsClient.DefaultRequestHeaders.ConnectionClose = true;
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        public void getPendingOrders() 
        {
            try
            {
                string strErrror = "";

                if (ordPlcDtFrm_Pend != null && ordPlcDtFrm_Pend.Value != "" && ordPlcDtTill_Pend != null && ordPlcDtTill_Pend.Value != "")
                {
                    DataTable dtData = SqlHelper.ReadTable("spGetOrderDetails", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                            SqlHelper.AddInParam("@dFromDate", SqlDbType.DateTime, ordPlcDtFrm_Pend.Value),
                            SqlHelper.AddInParam("@dToDate", SqlDbType.DateTime, ordPlcDtTill_Pend.Value),
                     SqlHelper.AddInParam("@sPhoneNo", SqlDbType.VarChar, txtUserPhn_Pend.Value ),
                            SqlHelper.AddInParam("@sClientName", SqlDbType.VarChar, txtUserName_Pend.Value),
                            SqlHelper.AddInParam("@sOrderNo", SqlDbType.VarChar, txtOrdNo_Pend.Value));

                    if (dtData.Rows.Count > 0)
                    {
                        grdPending.DataSource = dtData;
                        grdPending.DataBind();

                    }
                    else
                    {
                        //SetUpdateMessageForRecipt(false, "-No Record Found");
                        DataTable dt = new DataTable();
                        grdPending.DataSource = dt;
                        grdPending.DataBind();
                    }


                }
                else
                {
                    showgridRecipt();
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showgrid", "showDistrigrid", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
            }
        }

        protected void showgridRecipt()
        {
            try
            {
                string sql = "select * from Dove_User_wallet_Details_17";
                DataTable dtInfoData = SqlHelper.ReadTable(sql, Convert.ToString(Session["SystemUserSqlConnectionString"]), false);
                grdPending.DataSource = dtInfoData;
                grdPending.DataBind();
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showgrid", "showDistriReciptgrid", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                actionInfo.Attributes["class"] = "alert alert-info blink-border";
                actionInfo.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + " to System Administrator";
            }
        }

        public void getAcceptedorders()
        {
            try
            {
                string strErrror = "";

                if (ordPlcDtFrm_Accpt != null && ordPlcDtFrm_Accpt.Value != "" && ordPlcDtTill_Accpt != null && ordPlcDtTill_Accpt.Value != "")
                {
                    DataTable dtData = SqlHelper.ReadTable("spGetOrderDetails", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                            SqlHelper.AddInParam("@dFromDate", SqlDbType.DateTime, ordPlcDtFrm_Accpt.Value),
                            SqlHelper.AddInParam("@dToDate", SqlDbType.DateTime, ordPlcDtTill_Accpt.Value),
                     SqlHelper.AddInParam("@sPhoneNo", SqlDbType.VarChar, txtUserPhn_Accpt.Value),
                            SqlHelper.AddInParam("@sClientName", SqlDbType.VarChar, txtUserName_Accpt.Value),
                            SqlHelper.AddInParam("@sOrderNo", SqlDbType.VarChar, txtOrdNo_Accpt.Value));

                    if (dtData.Rows.Count > 0)
                    {
                        grdAccepted.DataSource = dtData;
                        grdAccepted.DataBind();

                    }
                    else
                    {
                        //SetUpdateMessageForRecipt(false, "-No Record Found");
                        DataTable dt = new DataTable();
                        grdAccepted.DataSource = dt;
                        grdAccepted.DataBind();
                    }


                }
                else
                {
                    showgridRecipt();
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showgrid", "ShowAccept", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                acceptedMsgDiv.InnerText = exError.ToString();
            }
        }

        public void getDispatchedOrders()
        {
            try
            {
                string strErrror = "";

                if (ordPlcDtFrm_Disp != null && ordPlcDtFrm_Disp.Value != "" && ordPlcDtTill_Disp != null && ordPlcDtTill_Disp.Value != "")
                {
                    DataTable dtData = SqlHelper.ReadTable("spGetOrderDetails", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                            SqlHelper.AddInParam("@dFromDate", SqlDbType.DateTime, ordPlcDtFrm_Disp.Value),
                            SqlHelper.AddInParam("@dToDate", SqlDbType.DateTime, ordPlcDtTill_Disp.Value),
                     SqlHelper.AddInParam("@sPhoneNo", SqlDbType.VarChar, txtUserPhn_Disp.Value),
                            SqlHelper.AddInParam("@sClientName", SqlDbType.VarChar, txtUserName_Disp.Value),
                            SqlHelper.AddInParam("@sOrderNo", SqlDbType.VarChar, txtOrdNo_Disp.Value));

                    if (dtData.Rows.Count > 0)
                    {
                        grdDispatched.DataSource = dtData;
                        grdDispatched.DataBind();

                    }
                    else
                    {
                        //SetUpdateMessageForRecipt(false, "-No Record Found");
                        DataTable dt = new DataTable();
                        grdDispatched.DataSource = dt;
                        grdDispatched.DataBind();
                    }


                }
                else
                {
                    showgridRecipt();
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showgrid", "Dispatchdata", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                dispatchedMsgDiv.Attributes["style"] = "padding-bottom: 10px; text-align: right; display: block;";
                dispatchedMsgDiv.InnerText = exError.ToString();
            }
        }

        public void getRejectedOrders()
        {
            
            try
            {
                string strErrror = "";

                if (ordPlcDtFrm_Rjct != null && ordPlcDtFrm_Rjct.Value != "" && ordPlcDtTill_Rjct != null && ordPlcDtTill_Rjct.Value != "")
                {
                    DataTable dtData = SqlHelper.ReadTable("spGetOrderDetails", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                            SqlHelper.AddInParam("@dFromDate", SqlDbType.DateTime, ordPlcDtFrm_Rjct.Value),
                            SqlHelper.AddInParam("@dToDate", SqlDbType.DateTime, ordPlcDtTill_Rjct.Value),
                     SqlHelper.AddInParam("@sPhoneNo", SqlDbType.VarChar, txtUserPhn_Rjct.Value),
                            SqlHelper.AddInParam("@sClientName", SqlDbType.VarChar, txtUserName_Rjct.Value),
                            SqlHelper.AddInParam("@sOrderNo", SqlDbType.VarChar, txtOrdNo_Rjct.Value));

                    if (dtData.Rows.Count > 0)
                    {
                        grdRejected.DataSource = dtData;
                        grdRejected.DataBind();

                    }
                    else
                    {
                        //SetUpdateMessageForRecipt(false, "-No Record Found");
                        DataTable dt = new DataTable();
                        grdRejected.DataSource = dt;
                        grdRejected.DataBind();
                    }


                }
                else
                {
                    showgridRecipt();
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showgrid", "showReject", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                rejectedMsgDiv.InnerText = exError.ToString();
            }
        }

        protected void btnSearchPending_Click(object sender, EventArgs e)
        {
            getPendingOrders();
        }

        protected void btnReset_ServerClick(object sender, EventArgs e)
        {
            ordPlcDtFrm_Pend.Value = "";
            ordPlcDtTill_Pend.Value = "";
            txtUserPhn_Pend.Value = "";
            txtUserName_Pend.Value = "";
            txtOrdNo_Pend.Value = "";
            //grdPending.DataSource = null;
            //grdPending.DataBind();
            //rowTotal.InnerHtml = "";
            divPendingOrder.Attributes["style"] = "padding-bottom: 10px; text-align: right; display: none;";
        }

        protected void grdPending_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //grdPending.EditIndex = -1;
            //this.Createdata();
        }

        protected void grdPending_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Accept")
            {
                string[] arg = new string[2];
                arg = e.CommandArgument.ToString().Split(',');
                hidOrderID.Value = arg[0];//e.CommandArgument.ToString();
                hidOrderNo.Value = arg[1];//e.CommandArgument.ToString();
                hidClientPhn.Value = arg[2];
                ClientScript.RegisterStartupScript(GetType(), "", "ShowConfirm()", true);
                //ClientScript.RegisterClientScriptBlock(GetType(), "", "ShowConfirm()", true);
            }
            if (e.CommandName == "Reject")
            {
                //hidOrderID.Value = e.CommandArgument.ToString();
                string[] arg = new string[2];
                arg = e.CommandArgument.ToString().Split(',');
                hidOrderID.Value = arg[0];//e.CommandArgument.ToString();
                hidOrderNo.Value = arg[1];//e.CommandArgument.ToString();
                hidClientPhn.Value = arg[2];
                ClientScript.RegisterStartupScript(GetType(), "", "ShowRejection()", true);
                //ClientScript.RegisterClientScriptBlock(GetType(), "", "ShowConfirm()", true);
            }
        }

        protected void btnCnfrmYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                SqlHelper.ReadTable("update Order_17 set EM_vOrderStatus='Accept' where EM_bIntId=@ID", false,
                    SqlHelper.AddInParam("@ID", SqlDbType.BigInt, Convert.ToInt32(hidOrderID.Value)));

                pendingMsgDiv.InnerHtml = "Order has been Accepted";
                getPendingOrders();
                getAcceptedorders();

                long mLngEnqId = Convert.ToInt64(hidOrderID.Value);
                //SendMailInSafeMachineThread(mLngEnqId, "Accept");

                //inventory code
                DataTable dtorders = SqlHelper.ReadTable("Select * from dbo.OrderSub_17 where EM_bIntId=@ID", false,
                    SqlHelper.AddInParam("@ID", SqlDbType.BigInt, Convert.ToInt32(hidOrderID.Value)));
                if (dtorders.Rows.Count > 0)
                {
                    foreach (DataRow rows in dtorders.Rows)
                    {
                        SqlHelper.ReadTable("update Product_Master_17 set PM_intQuantityAvailable=PM_intQuantityAvailable-@QTY where PM_bIntProdId=@ID", false,
                        SqlHelper.AddInParam("@ID", SqlDbType.BigInt, Convert.ToInt32(rows["OrdSub_bIntProdId"])),
                        SqlHelper.AddInParam("@QTY", SqlDbType.Decimal, Convert.ToDecimal(hidOrderQty.Value)));
                    }
                }
            }
            catch (Exception ex)
            {
                pendingMsgDiv.InnerText = ex.ToString();
            }
        }

        protected void btnRejectYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string validationResult = checkValidRejectInput();
                if (validationResult == "")
                {
                    string isOtpValidResult = validateOTP(Convert.ToInt32(txtRejOtp.Text.Trim()), ddlPhnRej.SelectedItem.Text, "5");
                    if (isOtpValidResult.Equals("Mobile number verified successfully."))
                    {
                        SqlHelper.ReadTable("update Order_17 set EM_vOrderStatus='Reject' where EM_bIntId=@ID", false,
                            SqlHelper.AddInParam("@ID", SqlDbType.BigInt, Convert.ToInt32(hidOrderID.Value)));
                        InsertOrderStatusLog(Convert.ToInt32(hidOrderID.Value), hidOrderNo.Value, "Reject");

                        //txtRejectReason
                        SqlHelper.ReadTable("Insert into OrderRejectQueue_17 ([ORQ_vcharorderId],[ORQ_vcharRejectReason],[ORQ_dtDatetime]) VALUES (@OrderID, @RejectReason, @dtDatetime)", false,
                               SqlHelper.AddInParam("@OrderID", SqlDbType.VarChar, hidOrderID.Value),
                               SqlHelper.AddInParam("@RejectReason", SqlDbType.VarChar, txtRejectReason.Text),
                               SqlHelper.AddInParam("@dtDatetime", SqlDbType.DateTime, DateTime.UtcNow.AddHours(5.5)));

                        DataTable dtorder = SqlHelper.ReadTable("select * from [dbo].[Order_17] where EM_bIntId=@ID", false, SqlHelper.AddInParam("@ID", SqlDbType.BigInt, Convert.ToInt64(hidOrderID.Value)));

                        if (dtorder.Rows.Count > 0)
                        {
                            DataTable dt3 = SqlHelper.ReadTable("Select DWTR_TransRefNo from [dbo].[Dove_Withdraw_Trans_RefNo_17]", false);
                            string TXN = dt3.Rows[0][0].ToString();
                            SqlHelper.ReadTable("SP_DoveUpdateTransRefNo", true);

                            if (Convert.ToDecimal(dtorder.Rows[0]["EM_decFinalAmt"].ToString()) > 0)
                            {
                                DataTable dt2 = SqlHelper.ReadTable("Select top 1 DUWD_decWallBal,DUWD_nCharTnxRefNo from [dbo].[Dove_User_wallet_Details_17] where DUWD_bIntUserId=@id order by DUWD_dtTransactionTime desc", false,
                                    SqlHelper.AddInParam("@id", SqlDbType.BigInt, Convert.ToInt64(dtorder.Rows[0]["EM_bIntUserId"].ToString())));

                                decimal Balance = Convert.ToDecimal(dt2.Rows[0][0].ToString());
                                decimal Amount = Convert.ToDecimal(dtorder.Rows[0]["EM_decFinalAmt"].ToString());
                                decimal NewBalance = Balance + Amount;

                                SqlHelper.ReadTable("Insert Into Dove_User_wallet_Details_17 " +
                                " ([DUWD_bIntUserId],[DUWD_decWallBal],[DUWD_vCharTransactionType],[DUWD_dtTransactionTime],[DUWD_decAmtDebCred],DUWD_nCharTnxRefNo,DUWD_charTransactedBY,DUWD_charReason) " +
                                " values (@bIntUserId,@decWallBal,@vCharTransactionType,@DUWD_dtTransactionTime,@decAmtDebCred,@nCharTnxRefNo,@Transactedby,@Reason)", false,
                            SqlHelper.AddInParam("@bIntUserId", SqlDbType.BigInt, Convert.ToInt64(dtorder.Rows[0]["EM_bIntUserId"].ToString())),
                            SqlHelper.AddInParam("@decWallBal", SqlDbType.Decimal, NewBalance),
                            SqlHelper.AddInParam("@vCharTransactionType", SqlDbType.VarChar, "recharge"),
                            SqlHelper.AddInParam("@DUWD_dtTransactionTime", SqlDbType.DateTime, DateTime.UtcNow.AddHours(5.5)),
                            SqlHelper.AddInParam("@decAmtDebCred", SqlDbType.Decimal, Amount),
                            SqlHelper.AddInParam("@nCharTnxRefNo", SqlDbType.VarChar, "HO" + TXN),
                            SqlHelper.AddInParam("@Transactedby", SqlDbType.VarChar, "Admin"),
                            SqlHelper.AddInParam("@Reason", SqlDbType.VarChar, "Order Rejected for Order No: " + dtorder.Rows[0]["EM_vCharOrdNum"].ToString()));
                            }

                            if (Convert.ToDecimal(dtorder.Rows[0]["EM_intGbeanUsed"].ToString()) > 0)
                            {
                                SqlHelper.ReadTable("INSERT INTO [dbo].[Dove_User_GBeans_17] ([DUG_bIntUSER_ID],[DUG_vCharREF_ORDERNO],[DUG_vCharTYPE] " +
                                    " ,[DUG_intGBEAN_VALUE],[DUG_dtGNRTD_ON],[DUG_dtVALID_UPTO],[DUG_bitIS_EXPIRED],[DUG_intUpdatedGBEAN_VALUE]) " +
                                    " VALUES (@USER_ID ,@ORDERNO ,'CREDIT',@GBEAN_VALUE ,@GNRTD_ON " +
                                    " ,@VALID_UPTO ,0 ,@UpdatedGBEAN_VALUE )", false,
                                    SqlHelper.AddInParam("@USER_ID", SqlDbType.BigInt, Convert.ToInt64(dtorder.Rows[0]["EM_bIntUserId"].ToString())),
                                    SqlHelper.AddInParam("@ORDERNO", SqlDbType.VarChar, "HO" + TXN),
                                    SqlHelper.AddInParam("@GBEAN_VALUE", SqlDbType.Int, Convert.ToInt32(dtorder.Rows[0]["EM_intGbeanUsed"].ToString())),
                                    SqlHelper.AddInParam("@GNRTD_ON", SqlDbType.DateTime, DateTime.UtcNow.AddHours(5.5)),
                                    SqlHelper.AddInParam("@VALID_UPTO", SqlDbType.DateTime, DateTime.UtcNow.AddHours(5.5).AddMonths(6)),
                                    SqlHelper.AddInParam("@UpdatedGBEAN_VALUE", SqlDbType.Int, Convert.ToInt32(dtorder.Rows[0]["EM_intGbeanUsed"].ToString())));

                            }
                        }

                        getPendingOrders();
                        getRejectedOrders();
                        getAcceptedorders();
                        long mLngEnqId = Convert.ToInt64(hidOrderID.Value);

                        //send email
                        SendMailInSafeMachineThread(mLngEnqId, "Reject");
                        //send sms
                        sendSms("reject", hidClientPhn.Value, hidOrderNo.Value);
                        if (Pending.Attributes["class"] == "active")
                        {

                            pendingMsgDiv.Attributes["Style"] = "display: block;";
                            pendingMsgDiv.InnerText = "Order has been Rejected";
                        }
                        else
                        {
                            acceptedMsgDiv.Attributes["Style"] = "display: block;";
                            acceptedMsgDiv.InnerText = "Order has been Rejected";
                        }
                        ddlPhnRej.SelectedIndex = 0;
                        txtRejectReason.Text = "";
                    }
                    else
                    {
                        pendingMsgDiv.Attributes["Style"] = "display: block;";
                        pendingMsgDiv.InnerText = isOtpValidResult;
                        acceptedMsgDiv.Attributes["Style"] = "display: block;";
                        acceptedMsgDiv.InnerText = isOtpValidResult;
                    }
                }
                else
                {
                    pendingMsgDiv.Attributes["Style"] = "display: block;";
                    pendingMsgDiv.InnerText = validationResult;
                    acceptedMsgDiv.Attributes["Style"] = "display: block;";
                    acceptedMsgDiv.InnerText = validationResult;
                }
            }
            catch (Exception ex)
            {
                pendingMsgDiv.InnerText = ex.ToString();
            }
        }

        //public void SendDelegatedMails(long pLngEnquiryId)
        //{
        //    AsyncMethodCaller mDeleObj = new AsyncMethodCaller(SendMailInSafeMachineThread);            
        //}
        //private delegate void AsyncMethodCaller(long pLngEnquiryId);

        private void SendMailInSafeMachineThread(long pLngEnquiryId, string Status)
        {
            try
            {
                DataSet dsEmailDetails = SqlHelper.ReadDataSet("SP_DoveModifyEnquiryLog",
                                                     GlobalVariables.SqlConnectionStringMstoreInformativeDb, true, true,
                                                     SqlHelper.AddInParam("@bintEnquiryId", SqlDbType.BigInt, pLngEnquiryId),
                                                     SqlHelper.AddInParam("@nVarExtraColsList", SqlDbType.NVarChar,
                                                                           "COALESCE(CAST(DECRYPTBYPASSPHRASE('*#_140351_HS_029081_75396_#*',EC_vCharSenderPassword) as varchar(Max)),'') As [vwSenerPass]"));

                string strEmailIdList = "No Senders ";

                if (dsEmailDetails != null)
                {
                    string mStrCommonTopTable = "";
                    //Mail body creation begin
                    //-------------------------------------------------------------
                    if (Status == "Accept")
                        mStrCommonTopTable = " Thank you for ordering the below mentioned product from Goldify!!";

                    if (Status == "Reject")
                        mStrCommonTopTable = " Sorry, we could not process your order.";



                    // Create common mailing objects
                    SmtpClient mObjSmtpClnt = new SmtpClient(Convert.ToString(dsEmailDetails.Tables[1].Rows[0]["EC_vCharEmailSmtpHost"]),
                                                             Convert.ToInt32(dsEmailDetails.Tables[1].Rows[0]["EC_iNtPort"]));

                    //mObjSmtpClnt.Credentials = new NetworkCredential(Convert.ToString(dsEmailDetails.Tables[1].Rows[0]["EC_vCharSenderEmailId"]),
                    //                                                 Convert.ToString(dsEmailDetails.Tables[1].Rows[0]["vwSenerPass"]));

                    mObjSmtpClnt.Credentials = new NetworkCredential(Convert.ToString(dsEmailDetails.Tables[1].Rows[0]["EC_vCharSenderEmailId"]), Convert.ToString(dsEmailDetails.Tables[1].Rows[0]["EC_vCharSenderPassword"]));
                    //mObjSmtpClnt.UseDefaultCredentials = false;
                    mObjSmtpClnt.EnableSsl = Convert.ToBoolean(dsEmailDetails.Tables[1].Rows[0]["EC_bItEnableSsl"]);

                    //Prepare comma seperated list of sender email id
                    strEmailIdList = Convert.ToString(dsEmailDetails.Tables[1].Rows[0]["EC_vCharSenderEmailId"]) + ",";

                    // Select Distinct informations to whom email is supposed to be sent.
                    DataTable dtInformations = dsEmailDetails.Tables[2].DefaultView.ToTable(true, "IM_bIntInfoId");

                    DataRow[] drArrSearchResult;

                    //Variables for database update
                    string mStrInnerOrderInfoHtml = "";

                    string mStrRecievers = "No Receiver";
                    string ownerPhnno = "";
                    int intTotCount = dtInformations.Rows.Count;
                    int intFailureCount = 0;
                    string errMsg = "No Error";
                    int intEmailStatus = 1;


                    //
                    foreach (DataRow drInfo in dtInformations.Rows)
                    {
                        try
                        {
                            drArrSearchResult = dsEmailDetails.Tables[2].Select("IM_bIntInfoId = " + Convert.ToString(drInfo["IM_bIntInfoId"]));
                            string mStrInfoName = "";
                            ownerPhnno = Convert.ToString(dsEmailDetails.Tables[2].Rows[0]["IM_vCharInfoPhone1"]);
                            //Code to create  enquiry summary details with product listings
                            if (drArrSearchResult.Length > 0)
                            {
                                mStrInfoName = Convert.ToString(drArrSearchResult[0]["IM_vCharInfoName_En"]);

                                if (Status == "Accept")
                                    mStrCommonTopTable = mStrCommonTopTable.Insert(0, ("Hello,<p><h2>Your order has Been Accepted. </i></h2></p>"));

                                if (Status == "Reject")
                                    mStrCommonTopTable = mStrCommonTopTable.Insert(0, ("Hello,<p><h2>Your order has Been Declined. </i></h2></p>"));

                                // Body to be added later once we get the format

                                mStrInnerOrderInfoHtml += "<table style=\"border: 2px solid rgba(204, 204, 204, 1);border-collapse: collapse;border-top-left-radius: 8px;border-top-right-radius: 8px;overflow: hidden; width: 50%; \">";
                                mStrInnerOrderInfoHtml += "    <tr>";
                                mStrInnerOrderInfoHtml += "        <td colspan=\"5\" style=\"background-color: rgba(204, 204, 204, 0.8);padding: 5px;font-weight: bold;font-family: Georgia, 'Times New Roman', Times, serif;\">Order Summary</td>";
                                mStrInnerOrderInfoHtml += "    </tr>";
                                mStrInnerOrderInfoHtml += "    <tr>";
                                mStrInnerOrderInfoHtml += "        <td colspan=\"2\" style=\"background-color: rgba(204, 204, 204, 0.8);padding: 5px !important;font-style: italic;border: 1px solid rgba(204, 204, 204, 1);font-size: small;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif; width: 55%;\">List of Products </td>";
                                mStrInnerOrderInfoHtml += "        <td style=\"background-color: rgba(204, 204, 204, 0.8);padding: 5px !important;font-style: italic;border: 1px solid rgba(204, 204, 204, 1);font-size: small;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif; width: 10%;\">Qty</td>";
                                mStrInnerOrderInfoHtml += "        <td style=\"background-color: rgba(204, 204, 204, 0.8);padding: 5px !important;font-style: italic;border: 1px solid rgba(204, 204, 204, 1);font-size: small;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif; width: 15%;\">Price</td>";
                                mStrInnerOrderInfoHtml += "        <td style=\"background-color: rgba(204, 204, 204, 0.8);padding: 5px !important;font-style: italic;border: 1px solid rgba(204, 204, 204, 1);font-size: small;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif; width: 20%;\">Amount</td>";
                                mStrInnerOrderInfoHtml += "    </tr>";

                            }

                            string mStrOrderList = "";
                            List<ProdEmailDet> EmailDet = new List<ProdEmailDet>();
                            for (int iRowCnt = 0; iRowCnt < drArrSearchResult.Length; iRowCnt++)
                            {
                                EmailDet.Add(new ProdEmailDet()
                                {
                                    ProdName = Convert.ToString(drArrSearchResult[iRowCnt]["PM_vCharProdName"]),
                                    ProdImg = Convert.ToString(drArrSearchResult[iRowCnt]["PI_vCharImgPath"]),
                                    ProdAmt = Convert.ToDecimal(dsEmailDetails.Tables[0].Rows[iRowCnt]["OrdSub_decBasicAmt"]).ToString("N2"),
                                    ProdQty = Convert.ToString(dsEmailDetails.Tables[0].Rows[iRowCnt]["OrdSub_decQty"]),
                                    TotalAmt = Convert.ToDecimal(dsEmailDetails.Tables[0].Rows[iRowCnt]["OrdSub_decBasicAmt"]) * Convert.ToInt16(dsEmailDetails.Tables[0].Rows[iRowCnt]["OrdSub_decQty"])
                                });
                            }
                            for (int iRowCnt = 0; iRowCnt < EmailDet.Count; iRowCnt++)
                            {
                                mStrOrderList += EmailDet[iRowCnt].ProdQty + ",";
                                if (iRowCnt % 2 == 0)
                                    mStrInnerOrderInfoHtml += "    <tr>";
                                else
                                    mStrInnerOrderInfoHtml += "    <tr style=\"background-color:rgba(204,204,204,0.5);\">";
                                if (EmailDet[iRowCnt].ProdImg == "")
                                    mStrInnerOrderInfoHtml += "        <td style=\"font-size: small;padding: 5px;border-bottom:1px solid rgba(204,204,204,1)\"><img style=\"width: 100%;max-width: 256px;height: auto\" src=\"" + "https://wap.goldifyapp.com/admin/" + "images/logo.png" + "\" /></td>";
                                else
                                {
                                    //Modified by ARV on 11 June 19 as it was giving broken image of product in mail because of space in url
                                    EmailDet[iRowCnt].ProdImg = EmailDet[iRowCnt].ProdImg.Replace(" ", "%20");
                                    mStrInnerOrderInfoHtml += "        <td style=\"font-size: small;padding: 5px;border-bottom:1px solid rgba(204,204,204,1)\"><img src=\"" + "https://www.goldifyapp.com/admin/" + EmailDet[iRowCnt].ProdImg + "\"  style=\"width: 100%;max-width: 256px;height: auto\" crossorigin=\"anonymous\" /></td>";
                                    //mStrInnerOrderInfoHtml += "        <td style=\"font-size: small;padding: 5px;border-bottom:1px solid rgba(204,204,204,1)\"><img src=\"" + "https://wap.goldifyapp.com/admin/" + EmailDet[iRowCnt].ProdImg+ "\"  style=\"width: 100%;max-width: 256px;height: auto\" crossorigin=\"anonymous\" /></td>";
                                }

                                // Body to be added later once we get the format

                                mStrInnerOrderInfoHtml += "        <td style=\"border-bottom:1px solid rgba(204,204,204,1);padding: 5px !important;font-size: small;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif;\">" + EmailDet[iRowCnt].ProdName + "</td>";
                                mStrInnerOrderInfoHtml += "        <td style=\"border: 1px solid rgba(204, 204, 204, 1);padding: 5px !important;font-style: italic;font-size: small;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif;\">" + EmailDet[iRowCnt].ProdQty + "</td>";
                                mStrInnerOrderInfoHtml += "        <td style=\"border: 1px solid rgba(204, 204, 204, 1);padding: 5px !important;font-style: italic;font-size: small;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif;\">Rs. " + EmailDet[iRowCnt].ProdAmt + "</td>";
                                mStrInnerOrderInfoHtml += "        <td style=\"border: 1px solid rgba(204, 204, 204, 1);padding: 5px !important;font-size: small;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif;\">Rs. " + (Convert.ToDecimal(EmailDet[iRowCnt].ProdQty) * Convert.ToDecimal(EmailDet[iRowCnt].ProdAmt)).ToString("N2") + "</td>";
                                mStrInnerOrderInfoHtml += "    </tr>";

                            }
                            var total = EmailDet.Sum(item => item.TotalAmt);

                            mStrInnerOrderInfoHtml += "    <tr>";
                            //mStrInnerOrderInfoHtml += "        <td colspan=\"3\" style=\"text-align:right;font-family: Georgia, 'Times New Roman', Times, serif;\">Total</td>";

                            mStrInnerOrderInfoHtml += "        <td colspan=\"5\" style=\"text-align:right;font-family: Georgia, 'Times New Roman', Times, serif;\">Total Rs. " + total + "</td>";

                            //mStrInnerOrderInfoHtml += "        <td style=\"font-family: Georgia, 'Times New Roman', Times, serif;\">&nbsp;-&nbsp;</td>";
                            mStrInnerOrderInfoHtml += "    </tr>";

                            if (Status == "Reject")
                            {
                                //mStrInnerOrderInfoHtml += "    <tr>";
                                //mStrInnerOrderInfoHtml += "        <td colspan=\"5\" style=\"text-align:right;font-family: Georgia, 'Times New Roman', Times, serif;\">You order has been declined because : " + txtRejectReason.Text + "</td>";
                                //mStrInnerOrderInfoHtml += "    </tr>";
                            }

                            if (mStrOrderList.EndsWith(","))
                                mStrOrderList = mStrOrderList.Remove(mStrOrderList.LastIndexOf(","), 1);
                            //-------------------------------------------------------------
                            //Mail body creation done

                            //Adding header to mail
                            //--------------------------------------------------------------
                            drArrSearchResult = dsEmailDetails.Tables[3].Select("EER_bIntInfoId = " + Convert.ToString(drInfo["IM_bIntInfoId"]));

                            string mStrEmailFullBody = "";
                            string mStrFooter = "";
                            if (drArrSearchResult.Length > 0)
                            {
                                mStrEmailFullBody = Convert.ToString(dsEmailDetails.Tables[1].Rows[0]["EC_nVarEmailHeader"]);
                                mStrFooter = Convert.ToString(dsEmailDetails.Tables[1].Rows[0]["EC_nVarEmailFooter"]);
                                mStrEmailFullBody = mStrEmailFullBody.Replace("[::{<AUTOSYS_CLIENT_NAME>}::]", Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_vCharClientName"]))
                                                       .Replace("[::{<AUTOSYS_ORDER_DATE>}::]", Convert.ToDateTime(dsEmailDetails.Tables[0].Rows[0]["EM_dtEnquiryTime"]).ToString("MMMM dd, yyyy"))
                                                       .Replace("[::{<AUTOSYS_ORDER_TIME>}::]", Convert.ToDateTime(dsEmailDetails.Tables[0].Rows[0]["EM_dtEnquiryTime"]).ToString("hh:mm:ss tt"))
                                                       .Replace("[::{<AUTOSYS_ORDER_NUMBER>}::]", pLngEnquiryId.ToString());
                                mStrFooter = mStrFooter.Replace("[::{<AUTOSYS_CLIENT_NAME>}::]", Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_vCharClientName"]))
                                                       .Replace("[::{<AUTOSYS_ORDER_DATE>}::]", Convert.ToDateTime(dsEmailDetails.Tables[0].Rows[0]["EM_dtEnquiryTime"]).ToString("MMMM dd, yyyy"))
                                                       .Replace("[::{<AUTOSYS_ORDER_TIME>}::]", Convert.ToDateTime(dsEmailDetails.Tables[0].Rows[0]["EM_dtEnquiryTime"]).ToString("hh:mm:ss tt"))
                                                       .Replace("[::{<AUTOSYS_ORDER_NUMBER>}::]", pLngEnquiryId.ToString());

                            }

                            //Email body created successfully with header and footer
                            mStrEmailFullBody += "<br/>" + mStrCommonTopTable + "<br/>" + mStrInnerOrderInfoHtml + "<br/>" + mStrFooter;

                            //DataTable dtCCEmail = SqlHelper.ReadTable("Select cm.Comp_emailID as Email ,cm.Comp_PhoneNumber as Phn From Company_Master cm INNER JOIN Taluka_Master tm on cm.Comp_bIntId = tm.TM_bIntCompId Where tm.TM_bItIsActive = 1 And tm.TM_bIntId = 17", false);

                            /* not required
                            //start: to create dynamic orderdetails html page on server added by Vibha_141019
                            string strFileName = "ord_no_" + pLngEnquiryId + ".html";
                            string serverPath = "";

                            //check if folder is present, if not create
                            //if (!Directory.Exists(Server.MapPath(@"OrderDetails/")))
                            //    Directory.CreateDirectory(Server.MapPath(@"OrderDetails/"));

                            //below code is to save dynamic html page on server  
                            serverPath = Path.Combine(HttpRuntime.AppDomainAppPath, "OrderDetails\\");

                            //below code is working on local path on computer c drivce
                            //StreamWriter file = new StreamWriter(Path.GetFullPath(@"OrderDetails/" + strFileName));

                            //StreamWriter file = new StreamWriter(serverPath + strFileName, true);
                            //file.WriteLine(mStrEmailFullBody.ToString());
                            //file.Close();

                            string orderDetailURL = "https://wap.goldifyapp.com/myapi/OrderDetails/" + strFileName;
                            //end: to create dynamic orderdetails html page on server added by Vibha_141019
                            */


                            ////send text sms
                            //SMSAPICall(pLngEnquiryId.ToString(), Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_bIntClientPhNo"]),
                            //    Convert.ToString(dtCCEmail.Rows[0]["Phn"]), ownerPhnno,
                            //    Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_vCharClientEmailId"]), orderDetailURL);


                            //Process of sending mails to respective receivers
                            //-----------------------------------------------------------------
                            mStrRecievers = Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_vCharClientEmailId"]);
                            if (mStrRecievers != null && mStrRecievers != "")
                            {
                                MailMessage mObjMailMsg = new MailMessage(Convert.ToString(dsEmailDetails.Tables[1].Rows[0]["EC_vCharSenderEmailId"]), mStrRecievers);
                                mObjMailMsg.IsBodyHtml = true;
                                mObjMailMsg.Body = mStrEmailFullBody;

                                if (Status == "Accept")
                                    mObjMailMsg.Subject = String.Format("GOLDIFY | Order Processed with Order No. {0}", dsEmailDetails.Tables[0].Rows[0]["EM_vCharOrdNum"].ToString());

                                if (Status == "Reject")
                                    mObjMailMsg.Subject = String.Format("GOLDIFY | Order Declined with Order No. {0}", dsEmailDetails.Tables[0].Rows[0]["EM_vCharOrdNum"].ToString());

                                mStrRecievers += ",";
                                for (int iRowCnt = 0; iRowCnt < drArrSearchResult.Length; iRowCnt++)
                                {
                                    mStrRecievers += Convert.ToString(drArrSearchResult[iRowCnt]["EER_vCharEmailId"]) + ",";
                                    mObjMailMsg.To.Add(new MailAddress(Convert.ToString(drArrSearchResult[iRowCnt]["EER_vCharEmailId"])));
                                }

                                //if (dtCCEmail.Rows.Count > 0)
                                //{

                                //    //mObjMailMsg.CC.Add(new MailAddress(Convert.ToString(dtCCEmail.Rows[0]["Email"])));
                                //    mObjMailMsg.Bcc.Add(new MailAddress(Convert.ToString(dtCCEmail.Rows[0]["Email"])));
                                //}
                                mStrRecievers += Convert.ToString(dsEmailDetails.Tables[2].Rows[0]["IM_vCharInfoEmail"]) + ",";
                                mObjMailMsg.Bcc.Add(new MailAddress(Convert.ToString(dsEmailDetails.Tables[2].Rows[0]["IM_vCharInfoEmail"])));

                                if (mStrRecievers.EndsWith(","))
                                    mStrRecievers = mStrRecievers.Remove(mStrRecievers.LastIndexOf(","), 1);

                                if (mObjMailMsg != null)
                                {
                                    try
                                    {
                                        mObjSmtpClnt.Send(mObjMailMsg);
                                    }
                                    catch (Exception ex)
                                    {
                                        pendingMsgDiv.InnerHtml = ex.InnerException.ToString();
                                    }
                                    finally
                                    {
                                        mObjMailMsg.Dispose();
                                    }

                                    /*start: commented by Pritesh_161019: not required as per current functionality
                                    drArrSearchResult = dsEmailDetails.Tables[2].Select("IM_bIntInfoId = " + Convert.ToString(drInfo["IM_bIntInfoId"]));
                                    List<string> lstProdname = new List<string>();
                                    List<string> lstProdPrice = new List<string>();
                                    List<string> lstContactNums = new List<string>();
                                    for (int iRowCnt = 0; iRowCnt < drArrSearchResult.Length; iRowCnt++)
                                    {
                                        lstProdname.Add(Convert.ToString(drArrSearchResult[iRowCnt]["PM_vCharProdName"]));
                                        lstProdPrice.Add(Convert.ToDecimal(drArrSearchResult[iRowCnt]["PM_decActualPrice"]).ToString("N2"));

                                    }
                                    lstContactNums.Add(Convert.ToString(dtCCEmail.Rows[0]["Phn"]));
                                    lstContactNums.Add(Convert.ToString(dsEmailDetails.Tables[2].Rows[0]["IM_vCharInfoEmail"]));
                                    lstContactNums.Add(Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_bIntClientPhNo"]));
                                    //string ClientName, string sDateTime, string EnqId, List<string> Products, List<string> ProdCost
                                    string Dt = Convert.ToDateTime(dsEmailDetails.Tables[0].Rows[0]["EM_dtEnquiryTime"]).ToString("MMMM dd, yyyy") + Convert.ToDateTime(dsEmailDetails.Tables[0].Rows[0]["EM_dtEnquiryTime"]).ToString("hh:mm:ss tt");
                                    end: commented by Pritesh_161019: not required as per current functionality*/

                                }
                            }
                            //-----------------------------------------------------------------
                        }
                        catch (Exception expErrorCount)
                        {
                            intFailureCount = intFailureCount + 1;
                            errMsg = "Error Message :- " + expErrorCount.Message + " Source :-" + expErrorCount.Source;
                            intEmailStatus = 2;
                        }
                    }
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("SendMailInSafeMachineThread", "sending Mail", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
                pendingMsgDiv.InnerText = exError.ToString();
            }
        }

        public object SMSAPICall(string EnqId, string ContactNo, string FContactNo, string VendorPhn, string Email, string OrderDetailURL)
        {
            String result = "";
            //string apiKey = "eTSDovX5KJE-VPgAM02vz8HajXruusj3gUWLlaRPph";
            /*
            string apiKey = "Q8TDRvLuz3k-trIWNHyxsv41GhOxZ6m0QukLVGzq7w";
            string numbers = ""; // in a comma seperated list
            if (ContactNo != null && ContactNo != "")
            {
                numbers = ContactNo + ",";
            }
            if (FContactNo != null && FContactNo != "")
            {
                numbers += FContactNo + ",";
            }
            if (VendorPhn != null && VendorPhn != "")
            {
                numbers += VendorPhn;
            }
            string OrdDetails = "Your Enquiry Id is " + EnqId;
            //for (int i = 0; i < Products.Count; i++)
            //{
            //    OrdDetails += Products[i] + " Of Price " + ProdCost[i];
            //}
            //string message = "Dear "+ClientName+", Your Enquiry with Id : "+ EnqId+", Total due amount will be Rs. "+ProdCost[0]+"/-,Order details is mailed on "+ Email +".";

            //Working but have to softcode it from db.
            //string message = "Dear " + ClientName + ",%nYour Enquiry with Id : " + EnqId + ", Total due amount will be Rs. " + "8,500" + "/-,Order details is mailed on " + Email + ".";

            DataTable dtSmsTemplate = SqlHelper.ReadTable("select TemplateMsg as Msg from dbo.SMSTemplate where TemplateID = 1", GlobalVariables.SqlConnectionStringMstoreInformativeDb, false
                  );


            string message = "";
            message = Convert.ToString(dtSmsTemplate.Rows[0]["Msg"]);
            message = message.Replace("[::{<AUTOSYS_OrdID>}::]", EnqId)
                                  .Replace("[::{<AUTOSYS_Email>}::]", Email)
                                  .Replace("[::{<AUTOSYS_Phn>}::]", ContactNo)
                                  .Replace("[::{<AUTOSYS_OrdDetails>}::]", OrderDetailURL);

            //string message = "	Dear "+ClientName+",%n %nYour Order Details are as Follows" +
            //    OrdDetails 
            //    +"%n %nThank You for Ordering with MstoreIndia.";
            string sender = "MSTORE";

            //String url = "https://api.textlocal.in/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;            //refer to parameters to complete correct url string
            String url = "https://api.textlocal.in/send/?apikey=" + apiKey + "&numbers=" + "9029347435" + "&message=" + message + "&sender=" + sender;            //refer to parameters to complete correct url string
            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(url);
            }
            catch (Exception e)
            {
                DataTable dtSmsLog = SqlHelper.ReadTable("SP_SMSLog", GlobalVariables.SqlConnectionStringMstoreInformativeDb, true,
                   SqlHelper.AddInParam("@APIKey", SqlDbType.VarChar, apiKey),
                   SqlHelper.AddInParam("@PhoneNumbers", SqlDbType.VarChar, numbers),
                   SqlHelper.AddInParam("@sMessage", SqlDbType.VarChar, message),
                   SqlHelper.AddInParam("@Sender", SqlDbType.VarChar, sender),
                   SqlHelper.AddInParam("@ErrorCode", SqlDbType.VarChar, null),
                   SqlHelper.AddInParam("@ErrorMsg", SqlDbType.VarChar, null),
                   SqlHelper.AddInParam("@MessageSentCount", SqlDbType.BigInt, null),
                   SqlHelper.AddInParam("@InDND", SqlDbType.VarChar, null),
                   SqlHelper.AddInParam("@BatchId", SqlDbType.VarChar, null),
                   SqlHelper.AddInParam("@sStatus ", SqlDbType.VarChar, null),
                   SqlHelper.AddInParam("@ExceptionMsg", SqlDbType.VarChar, e.Message),
                   SqlHelper.AddInParam("@BalanceSMSCount", SqlDbType.VarChar, null)
                   );
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader
                sr.Close();


            }
            var SMSCls = JsonConvert.DeserializeObject<Admin_CommTrex.Distributor.SMSLogCls>(result);
            DataTable dtSMSEx = SqlHelper.ReadTable("SP_SMSLog", GlobalVariables.SqlConnectionStringMstoreInformativeDb, true,
                    SqlHelper.AddInParam("@APIKey", SqlDbType.VarChar, apiKey),
                    SqlHelper.AddInParam("@PhoneNumbers", SqlDbType.VarChar, numbers),
                    SqlHelper.AddInParam("@sMessage", SqlDbType.VarChar, message),
                    SqlHelper.AddInParam("@Sender", SqlDbType.VarChar, sender),
                    SqlHelper.AddInParam("@ErrorCode", SqlDbType.VarChar, SMSCls.errors == null ? 0 : SMSCls.errors[0].code),
                    SqlHelper.AddInParam("@ErrorMsg", SqlDbType.VarChar, SMSCls.errors == null ? null : SMSCls.errors[0].message),
                    SqlHelper.AddInParam("@MessageSentCount", SqlDbType.BigInt, SMSCls.num_messages),
                    SqlHelper.AddInParam("@InDND", SqlDbType.VarChar, null),
                    SqlHelper.AddInParam("@BatchId", SqlDbType.VarChar, SMSCls.batch_id),
                    SqlHelper.AddInParam("@sStatus ", SqlDbType.VarChar, SMSCls.status),
                    SqlHelper.AddInParam("@ExceptionMsg", SqlDbType.VarChar, null),
                    SqlHelper.AddInParam("@BalanceSMSCount", SqlDbType.VarChar, SMSCls.balance)
                    );
             
            */
            //SMSCls.
            return result;


            //using (HttpClient client = new HttpClient())
            //using (HttpResponseMessage response = await client.GetAsync("https://www.fast2sms.com/dev/bulk?authorization=Vtylh3cqix7aL59WfeSoRuAJIEOzb1ZwKn0YBXFvrHCk8U6TQm9MA5Z6PHQXYyuWRTchINzogJ1xw0Cp&sender_id=FSTSMS&message=This%20is%20a%20test%20message&language=english&route=p&numbers=7977749655"))
            //using (HttpContent content = response.Content)
            //{
            //    // ... Read the string.
            //    string result = await content.ReadAsStringAsync().ConfigureAwait(false); 

            //    // ... Display the result.
            //    if (result != null &&
            //        result.Length >= 50)
            //    {

            //        Console.WriteLine(result.Substring(0, 50) + "...");
            //    }
            //}
        }

        protected void btnAcptSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string validationResult = checkValidInput();
                if (validationResult == "")
                {
                    string isOtpValidResult = validateOTP(Convert.ToInt32(txtOtp.Text.Trim()), ddlPhn.SelectedItem.Text, "5");
                    if (isOtpValidResult.Equals("Mobile number verified successfully."))
                    {
                        long mLngEnqId = Convert.ToInt64(hidOrderID.Value);

                        SqlHelper.ReadTable("update Order_17 set EM_vOrderStatus='Accept' where EM_bIntId=@ID", false,
                        SqlHelper.AddInParam("@ID", SqlDbType.BigInt, mLngEnqId));
                        InsertOrderStatusLog(Convert.ToInt32(hidOrderID.Value), hidOrderNo.Value, "Accept");


                        //inventory code
                        DataTable dtorders = SqlHelper.ReadTable("Select * from dbo.OrderSub_17 where EM_bIntId=@ID", false,
                            SqlHelper.AddInParam("@ID", SqlDbType.BigInt, mLngEnqId));

                        if (dtorders.Rows.Count > 0)
                        {
                            foreach (DataRow rows in dtorders.Rows)
                            {
                                SqlHelper.ReadTable("update Product_Master_17 set PM_intQuantityAvailable=PM_intQuantityAvailable-@QTY where PM_bIntProdId=@ID", false,
                                SqlHelper.AddInParam("@ID", SqlDbType.BigInt, Convert.ToInt32(rows["OrdSub_bIntProdId"])),
                                SqlHelper.AddInParam("@QTY", SqlDbType.Decimal, Convert.ToDecimal(rows["OrdSub_decQty"])));
                            }
                        }

                        getPendingOrders();
                        getAcceptedorders();

                        //send email
                        SendMailInSafeMachineThread(mLngEnqId, "Accept");

                        //send sms
                        sendSms("accept", hidClientPhn.Value, hidOrderNo.Value);

                        pendingMsgDiv.InnerText = "Order has been Accepted";
                        ddlPhn.SelectedIndex = 0;
                    }
                    else
                        pendingMsgDiv.InnerText = isOtpValidResult;
                }
                else
                {
                    pendingMsgDiv.InnerText = validationResult;
                    //ClientScript.RegisterStartupScript(this.GetType(), "retainConfirmModal", "$('#modAcceptConfirm').modal('show')", true);            
                }
            }
            catch (Exception ex)
            {
                pendingMsgDiv.InnerText = ex.ToString();
            }
        }

        protected void btnTestSms_Click(object sender, EventArgs e)
        {
            sendSms("accept", "9870680253", "20210919220013555555");
            sendSms("reject", "9870680253", "20210919220013555555");
        }
        protected string checkValidInput()
        {
            string isInputValid = "";

            if (ddlPhn.SelectedIndex == 0)
            {
                isInputValid = "Please select mobile no.";
                return isInputValid;
            }
            if (txtOtp.Text.Trim() == "")
            {
                isInputValid = "Please enter OTP";
                return isInputValid;
            }
            if (txtOtp.Text.Trim().Length != 5)
            {
                isInputValid = "Invalid OTP";
                return isInputValid;
            }

            return isInputValid;
        }

        protected string checkValidRejectInput()
        {
            string isInputValid = "";

            if (ddlPhnRej.SelectedIndex == 0)
            {
                isInputValid = "Please select mobile no.";
                return isInputValid;
            }
            if (txtRejOtp.Text.Trim() == "")
            {
                isInputValid = "Please enter OTP";
                return isInputValid;
            }
            if (txtRejOtp.Text.Trim().Length != 5)
            {
                isInputValid = "Invalid OTP";
                return isInputValid;
            }

            return isInputValid;
        }

        public string validateOTP(int userSubmittedOTP, string userMobNum, string pstrfrm)
        {
            string validationRes = "";
            string qFetchOtp = " SELECT TOP 1 [DOL_bIntId],[DOL_vCharOTP],[DOL_dtVALID_UPTO],[DOL_intNoOfAttmpt] " +
                            " FROM [dbo].[OTP_Log_17] " +
                            " WHERE DOL_vCharMOBILE_NO = '+91'+@userMobNum  and DOL_vCharPgReqFrm=@reqFrm" +
                            " ORDER BY DOL_dtGNRTD_ON DESC ";
            try
            {
                DataTable dtFetchOtp = SqlHelper.ReadTable(qFetchOtp, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false,
                    SqlHelper.AddInParam("@userMobNum", SqlDbType.VarChar, userMobNum),
                    SqlHelper.AddInParam("@reqFrm", SqlDbType.VarChar, pstrfrm));
                if (dtFetchOtp.Rows.Count > 0)
                {
                    int noOfAttempts = Convert.ToInt32(dtFetchOtp.Rows[0]["DOL_intNoOfAttmpt"]);
                    if (noOfAttempts != -1)
                    {
                        if (noOfAttempts < 3)
                        {
                            noOfAttempts = noOfAttempts + 1;
                            string sqlupdate = "update [dbo].[OTP_Log_17] set DOL_intNoOfAttmpt=@noOfAttempts where DOL_bIntId=@bintId";
                            DataTable dtUpdateAttemt = SqlHelper.ReadTable(sqlupdate, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false,
                                                SqlHelper.AddInParam("@noOfAttempts", SqlDbType.Int, noOfAttempts),
                                                SqlHelper.AddInParam("@bintId", SqlDbType.BigInt, Convert.ToInt64(dtFetchOtp.Rows[0]["DOL_bIntId"])));

                            int sentOTP = Convert.ToInt32(dtFetchOtp.Rows[0]["DOL_vCharOTP"]);
                            if (userSubmittedOTP == sentOTP)
                            {
                                DataTable dtUpdateApp = SqlHelper.ReadTable(sqlupdate, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false,
                                                    SqlHelper.AddInParam("@noOfAttempts", SqlDbType.Int, -1),
                                                    SqlHelper.AddInParam("@bintId", SqlDbType.BigInt, Convert.ToInt64(dtFetchOtp.Rows[0]["DOL_bIntId"])));
                                //validationRes = "Mobile number verified successfully.";
                                validationRes = "Mobile number verified successfully.";
                            }
                            else
                            {
                                validationRes = "Invalid OTP. Please try again.";
                            }
                        }
                        else
                        {
                            validationRes = "Too many invalid attempts. Please generate OTP again.";
                        }
                    }
                    else
                    {
                        validationRes = "Please generate OTP first.";
                    }
                }
                else
                {
                    validationRes = "Please generate OTP first.";
                }
            }
            catch (Exception ex)
            {
                validationRes = "Something went wrong. Please try again.";

                pendingMsgDiv.InnerText = ex.ToString();
            }
            return validationRes;
        }

        protected void btnAcceptSearch_ServerClick(object sender, EventArgs e)
        {
            getAcceptedorders();
        }

        protected void btnAcceptClear_ServerClick(object sender, EventArgs e)
        {
            txtUserName_Accpt.Value = "";
            txtUserPhn_Accpt.Value = "";
            txtOrdNo_Accpt.Value = "";
            ordPlcDtFrm_Accpt.Value = "";
            ordPlcDtTill_Accpt.Value = "";
            //grdAccepted.DataSource = null;
            //grdAccepted.DataBind();
            //rowTotalAccept.InnerHtml = "";
            divAcceptExport.Attributes["style"] = "padding-bottom: 10px; text-align: right; display: none;";

        }

        protected void btnRejectSearch_ServerClick(object sender, EventArgs e)
        {
            getRejectedOrders();
        }

        protected void btnRejectClear_ServerClick(object sender, EventArgs e)
        {
            txtUserName_Rjct.Value = "";
            txtUserPhn_Rjct.Value = "";
            txtOrdNo_Rjct.Value = "";
            ordPlcDtFrm_Rjct.Value = "";
            ordPlcDtTill_Rjct.Value = "";
            //grdRejected.DataSource = null;
            //grdRejected.DataBind();
            //rowTotalReject.InnerHtml = "";
            divRejectExport.Attributes["style"] = "padding-bottom: 10px; text-align: right; display: none;";
        }

        protected void btnDisSearch_ServerClick(object sender, EventArgs e)
        {
            getDispatchedOrders();
        }

        protected void btnDisClear_ServerClick(object sender, EventArgs e)
        {
            txtUserName_Disp.Value = "";
            txtUserPhn_Disp.Value = "";
            txtOrdNo_Disp.Value = "";
            ordPlcDtFrm_Disp.Value = "";
            ordPlcDtTill_Disp.Value = "";
            //grdAccepted.DataSource = null;
            //grdAccepted.DataBind();
            //rowTotalAccept.InnerHtml = "";
            divExportDisOrder.Attributes["style"] = "padding-bottom: 10px; text-align: right; display: none;";

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void grdAccepted_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Dispatch")
                {
                    string[] arg = new string[2];
                    arg = e.CommandArgument.ToString().Split(',');
                    hidOrderID.Value = arg[0];//e.CommandArgument.ToString();
                    hidOrderNo.Value = arg[1];//e.CommandArgument.ToString();
                    //ClientScript.RegisterStartupScript(GetType(), "", "ShowDispatch()", true);
                    SqlHelper.ReadTable("update Order_17 set EM_vOrderStatus='Dispatch' where EM_bIntId=@ID", false,
                                SqlHelper.AddInParam("@ID", SqlDbType.BigInt, Convert.ToInt32(hidOrderID.Value)));
                    InsertOrderStatusLog(Convert.ToInt32(hidOrderID.Value), hidOrderNo.Value, "Dispatch");

                    getDispatchedOrders();
                    getAcceptedorders();
                    acceptedMsgDiv.Attributes["Style"] = "display: block;";
                    acceptedMsgDiv.InnerText = "Order has been Dispatched";
                }
                if (e.CommandName == "Reject")
                {
                    //hidOrderID.Value = e.CommandArgument.ToString();
                    string[] arg = new string[2];
                    arg = e.CommandArgument.ToString().Split(',');
                    hidOrderID.Value = arg[0];//e.CommandArgument.ToString();
                    hidOrderNo.Value = arg[1];//e.CommandArgument.ToString();
                    hidClientPhn.Value = arg[2];
                    ClientScript.RegisterStartupScript(GetType(), "", "ShowRejection()", true);
                    //ClientScript.RegisterClientScriptBlock(GetType(), "", "ShowConfirm()", true);
                }
            }
            catch (Exception ex)
            {
                pendingMsgDiv.Attributes["style"] = "display:block;";
                pendingMsgDiv.InnerHtml = "grdAccepted_RowCommand error: " + ex.ToString();
            }
        }

        public void InsertOrderStatusLog(int iOrderId, string sOrderNo, string sOrderStatus)
        {
            try
            {
                DataTable dtCatData = SqlHelper.ReadTable("spInsertOrderStatusUpdateLog", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                      SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, ((TalukaData)Session["TalukaDetails"]).TalukaID),
                                      SqlHelper.AddInParam("@bIntOrderId", SqlDbType.BigInt, iOrderId),
                                      SqlHelper.AddInParam("@vCharOrdNum", SqlDbType.VarChar, sOrderNo),
                                      SqlHelper.AddInParam("@vCharOrdStatus", SqlDbType.VarChar, sOrderStatus));

            }
            catch (Exception ex)
            {
                pendingMsgDiv.Attributes["style"] = "display:block;";
                pendingMsgDiv.InnerHtml = "grdAccept_RowCommand error: " + ex.ToString();
            }

        }

        protected void grdPending_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //dynamically create img for products in an order
                    Label lblProdImgs = (e.Row.FindControl("lblProdImgs") as Label);
                    string[] prodImgArr = lblProdImgs.Text.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < prodImgArr.Length; i++)
                    {
                        System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                        img.ID = "img_Prod_" + (i + 1);
                        img.CssClass = "prod-img";
                        img.ImageUrl = "https://www.goldifyapp.com/admin/" + prodImgArr[i].Trim();
                        img.Width = Unit.Pixel(75);
                        img.Attributes.Add("onClick", "ShowEnlargedImg(this)");
                        img.Attributes.Add("onerror", "this.onerror=null;this.src='images/noimage.jpg';");
                        e.Row.Cells[9].Controls.Add(img);
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void grdAccepted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //dynamically create img for products in an order
                    Label lblProdImgs = (e.Row.FindControl("lblProdImgs") as Label);
                    string[] prodImgArr = lblProdImgs.Text.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < prodImgArr.Length; i++)
                    {
                        System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                        img.ID = "img_Prod_" + (i + 1);
                        img.CssClass = "prod-img";
                        img.ImageUrl = "https://www.goldifyapp.com/admin/" + prodImgArr[i].Trim();
                        img.Width = Unit.Pixel(75);
                        img.Attributes.Add("onClick", "ShowEnlargedImg(this)");
                        img.Attributes.Add("onerror", "this.onerror=null;this.src='images/noimage.jpg';");
                        e.Row.Cells[7].Controls.Add(img);
                    }


                    Literal lblOrdQty = (Literal)e.Row.FindControl("lblOrdQty");
                    Literal lblProdWeight = (Literal)e.Row.FindControl("lblProdWeight");
                    Label lblTotalWeight = (Label)e.Row.FindControl("lblTotalWeight");

                    string[] prodQtyArr = lblOrdQty.Text.Replace("<br /><br />", ",").Split(new[] { "," }, StringSplitOptions.None);
                    string[] prodWghtArr = lblProdWeight.Text.Replace("<br /><br />", ",").Split(new[] { "," }, StringSplitOptions.None);

                    decimal[] prodTotWeight = new decimal[prodWghtArr.Length];
                    for (int z = 0; z < prodWghtArr.Length; z++)
                    {
                        prodTotWeight[z] = Convert.ToDecimal(prodWghtArr[z]) * Convert.ToDecimal(prodQtyArr[z]);
                    }

                    decimal totWeight = prodTotWeight.Sum();
                    lblTotalWeight.Text = totWeight.ToString("0.00");
                }
            }
            catch (Exception ex) { }
        }

        protected void grdDispatched_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //dynamically create img for products in an order
                    Label lblProdImgs = (e.Row.FindControl("lblProdImgs_Disp") as Label);
                    string[] prodImgArr = lblProdImgs.Text.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < prodImgArr.Length; i++)
                    {
                        System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                        img.ID = "img_Prod_" + (i + 1);
                        img.CssClass = "prod-img";
                        img.ImageUrl = "https://www.goldifyapp.com/admin/" + prodImgArr[i].Trim();
                        img.Width = Unit.Pixel(75);
                        img.Attributes.Add("onClick", "ShowEnlargedImg(this)");
                        img.Attributes.Add("onerror", "this.onerror=null;this.src='images/noimage.jpg';");
                        e.Row.Cells[7].Controls.Add(img);
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void grdRejected_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //dynamically create img for products in an order
                    Label lblProdImgs = (e.Row.FindControl("lblProdImgs_Rjct") as Label);
                    string[] prodImgArr = lblProdImgs.Text.Split(new[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < prodImgArr.Length; i++)
                    {
                        System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                        img.ID = "img_Prod_" + (i + 1);
                        img.CssClass = "prod-img";
                        img.ImageUrl = "https://www.goldifyapp.com/admin/" + prodImgArr[i].Trim();
                        img.Width = Unit.Pixel(75);
                        img.Attributes.Add("onClick", "ShowEnlargedImg(this)");
                        img.Attributes.Add("onerror", "this.onerror=null;this.src='images/noimage.jpg';");
                        e.Row.Cells[7].Controls.Add(img);
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void grdPending_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPending.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["pendingOrders"];
            grdPending.DataSource = dt;
            grdPending.DataBind();
        }

        protected void grdAccepted_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAccepted.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["acceptedOrders"];
            grdAccepted.DataSource = dt;
            grdAccepted.DataBind();
        }

        protected void grdRejected_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRejected.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["rejectedOrders"];
            grdRejected.DataSource = dt;
            grdRejected.DataBind();
        }

        protected void grdDispatched_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDispatched.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["dispatchedOrders"];
            grdDispatched.DataSource = dt;
            grdDispatched.DataBind();

        }

        private string sendSms(string smsFor, string pLngClientPhno, string ordNo)
        {
            string response = "";
            string number = pLngClientPhno;
            string message = "";

            string apiUrl = GlobalVariables.smsApiUrl;
            string user = GlobalVariables.smsUser;
            string key = GlobalVariables.smsKey;
            string senderid = GlobalVariables.smsSenderid;
            string accusage = GlobalVariables.smsAccusage;
            string entityid = GlobalVariables.smsEntityid;
            string tmpltid = GlobalVariables.otpTmpltid;

            switch (smsFor)
            {
                case "accept":
                    message = GlobalVariables.ordAcptMsgTmplt;
                    break;
                case "reject":
                    message = GlobalVariables.ordRjctMsgTmplt;
                    break;
            }

            message = message.Replace("{var}", ordNo);// +". Please do not share this OTP with anyone.";

            string url = apiUrl +
                         "user=" + user +
                         "&key=" + key +
                         "&mobile=" + number +
                         "&message=" + message +
                         "&senderid=" + senderid +
                         "&accusage=" + accusage /*+
                         "&entityid=" + entityid +
                         "&tempid=" + tmpltid*/
                         ;

            //GoldifyManager.WriteLogToFile("SMS request sent:: " + url, "SMS_Log");
            WebClient client = new WebClient();
            try
            {
                client.Encoding = Encoding.UTF8;
                response = client.DownloadString(url);

                //response = await _smsClient.GetStringAsync(url);
                //GoldifyManager.WriteLogToFile("SMS response rcvd:: " + response, "SMS_Log");
                //if (response.ToLower().Contains("sent"))
                //    result.SetStatus(enResultStatus.Success, new PagedResult() { Result = response, HasMoreData = false, IsResultSuccess = true });
                //else
                //    result.SetStatus(enResultStatus.Error, response);

            }
            catch (Exception ex)
            {
                //GoldifyManager.WriteLogToFile("SMS Exception:: " + ex.ToString(), "SMS_Log");
                response = "Exception while sending SMS";
            }
            return response;
        }

        protected void btnExportPending_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string fileName = "Peinding_Orders_" + DateTime.UtcNow.AddMinutes(330).ToString("yyyy-MM-dd_HHmmss");

                //get gridview data inside datatable
                DataTable gridToTable = new DataTable();
                //define the positions of columns which are numeric
                int[] numbericCols = { 0, 4, 6 };
                int[] decimalCols = { 17, 18, 19, 20, 21 };

                grdPending.Columns[22].Visible = false;
                grdPending.Columns[23].Visible = false;
                //grdPending.Columns[14].Visible = false;

                //Add columns to DataTable.
                for (int z = 0; z < grdPending.HeaderRow.Cells.Count; z++)
                {
                    TableCell cell = grdPending.HeaderRow.Cells[z];
                    if (grdPending.Columns[z].Visible)
                    {
                        if (cell.Text != "Product Image")
                        {
                            if (numbericCols.Contains(z))
                                gridToTable.Columns.Add(cell.Text, typeof(long));
                            else if (decimalCols.Contains(z))
                                gridToTable.Columns.Add(cell.Text, typeof(decimal));
                            else
                                gridToTable.Columns.Add(cell.Text);
                        }
                    }
                }
                //foreach (TableCell cell in grdPaymentDashboard.HeaderRow.Cells)
                //{
                //    gridToTable.Columns.Add(cell.Text);
                //}

                //Get Current Page Index so You can get back here after process
                int a = grdPending.PageIndex;
                int counter = 1;
                //Loop through All Pages
                for (int i = 0; i < grdPending.PageCount; i++)
                {
                    //Set Page Index
                    grdPending.SetPageIndex(i);
                    //After Setting Page Index Loop through its Rows

                    foreach (GridViewRow eachRow in grdPending.Rows)
                    {
                        //gridToTable.Rows.Add();
                        //for (int i = 0; i < eachRow.Cells.Count; i++)
                        //{
                        //  gridToTable.Rows[eachRow.RowIndex][i] = eachCell.Text;
                        //}
                        var userName = ((Label)(eachRow.Cells[0].FindControl("lblUserName"))).Text;
                        var userMob = ((Label)(eachRow.Cells[0].FindControl("lblUserMobile"))).Text;
                        var clientName = ((Label)(eachRow.Cells[0].FindControl("lblClientName"))).Text;
                        var clientMob = ((Label)(eachRow.Cells[0].FindControl("lblClientPhn"))).Text;
                        var clientEmail = ((Label)(eachRow.Cells[0].FindControl("lblClientEmail"))).Text;
                        var address = ((Label)(eachRow.Cells[0].FindControl("lblAddress"))).Text;
                        var productValues = ((Literal)(eachRow.Cells[0].FindControl("lblProdName"))).Text;
                        productValues = productValues.Replace("<br /><br />", "\n");
                        var ordQty = ((Literal)(eachRow.Cells[0].FindControl("lblOrdQty"))).Text;
                        ordQty = ordQty.Replace("<br /><br />", "\n");
                        var availableQty = ((Literal)(eachRow.Cells[0].FindControl("lblAvailability"))).Text;
                        availableQty = availableQty.Replace("<br /><br />", "\n");
                        var productWeight = ((Literal)(eachRow.Cells[0].FindControl("lblProdWeight"))).Text;
                        productWeight = productWeight.Replace("<br /><br />", "\n");
                        var orderTime = ((Label)(eachRow.Cells[0].FindControl("lblOrderTime"))).Text;
                        var orderId = ((Label)(eachRow.Cells[0].FindControl("lblOrderNum"))).Text;
                        var orderStatus = ((Label)(eachRow.Cells[0].FindControl("lblOrderStatus"))).Text;
                        orderStatus = string.IsNullOrWhiteSpace(orderStatus) ? "Pending" : orderStatus;
                        var recharge = ((Label)(eachRow.Cells[0].FindControl("lblRechargelAmt"))).Text;
                        var withdrawl = ((Label)(eachRow.Cells[0].FindControl("lblWithdrawllAmt"))).Text;
                        var gbeanAmount = ((Label)(eachRow.Cells[0].FindControl("lblGbeanlAmt"))).Text;
                        var walletAmount = ((Label)(eachRow.Cells[0].FindControl("lblWalletAmt"))).Text;
                        var finalAmount = ((Label)(eachRow.Cells[0].FindControl("lblFinalAmt"))).Text;

                        gridToTable.Rows.Add(counter, userName, userMob, clientName, clientMob, clientEmail, address,
                            productValues, ordQty, availableQty, productWeight, orderTime, orderId, orderStatus, recharge,
                            withdrawl, gbeanAmount, walletAmount, finalAmount);
                        counter++;
                    }
                }
                //Getting Back to the First State
                grdPending.SetPageIndex(a);

                using (ExcelPackage myExcelPack = new ExcelPackage())
                {
                    ExcelWorksheet ws = myExcelPack.Workbook.Worksheets.Add(fileName);
                    ws.Cells["A1"].LoadFromDataTable(gridToTable, true);
                    ws.Cells.AutoFitColumns();

                    // Select only the header cells
                    var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
                    // Set their text to bold, italic and underline.
                    headerCells.Style.Font.Bold = true;
                    //headerCells.Style.Font.Italic = true;
                    //headerCells.Style.Font.UnderLine = true;
                    ws.Cells.Style.WrapText = true;
                    ws.View.FreezePanes(2, 1);

                    //center align all cells
                    var allCells = ws.Cells[1, 1, ws.Dimension.Rows, ws.Dimension.Columns];
                    allCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    allCells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    allCells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    allCells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    allCells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    allCells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    //allCells.AutoFilter = true;

                    //will change page to page
                    //adjust width column wise as needed
                    ws.Column(1).Width = 6.00;  //Sr no
                    ws.Column(7).Width = 50.00; //address
                    ws.Column(5).Width = 12.00;

                    //text align right for amounts
                    var amountCells = ws.Cells["O2:S" + ws.Dimension.Rows];
                    amountCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    amountCells.Style.Numberformat.Format = "0.00";

                    /*
                    var ms = new System.IO.MemoryStream();
                    myExcelPack.SaveAs(ms);
                    ms.WriteTo(Response.OutputStream);
                    */

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xlsx", fileName));
                    Response.BinaryWrite(myExcelPack.GetAsByteArray());
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                pendingMsgDiv.Attributes["style"] = "display:block;";
                pendingMsgDiv.InnerHtml = "Download error: " + ex.ToString();
            }

        }

        protected void btnExportAccept_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string fileName = "Accepted_Orders_" + DateTime.UtcNow.AddMinutes(330).ToString("yyyy-MM-dd_HHmmss");

                //get gridview data inside datatable
                DataTable gridToTable = new DataTable();

                //define the positions of columns which are numeric
                int[] numbericCols = { 0, 2, 4 };
                int[] decimalCols = { 11, 16, 17, 18, 19, 20 };
                grdAccepted.Columns[22].Visible = false;

                //Add columns to DataTable.
                for (int z = 0; z < grdAccepted.HeaderRow.Cells.Count; z++)
                {
                    if (grdAccepted.Columns[z].Visible)
                    {
                        TableCell cell = grdAccepted.HeaderRow.Cells[z];
                        if (cell.Text != "Product Image")
                        {
                            if (numbericCols.Contains(z))
                                gridToTable.Columns.Add(cell.Text, typeof(long));
                            else if (decimalCols.Contains(z))
                                gridToTable.Columns.Add(cell.Text, typeof(decimal));
                            else
                                gridToTable.Columns.Add(cell.Text);
                        }
                    }
                }
                //foreach (TableCell cell in grdPaymentDashboard.HeaderRow.Cells)
                //{
                //    gridToTable.Columns.Add(cell.Text);
                //}

                //Get Current Page Index so You can get back here after process
                int a = grdAccepted.PageIndex;
                int counter = 1;
                //Loop through All Pages
                for (int i = 0; i < grdAccepted.PageCount; i++)
                {
                    //Set Page Index
                    grdAccepted.SetPageIndex(i);
                    //After Setting Page Index Loop through its Rows

                    foreach (GridViewRow eachRow in grdAccepted.Rows)
                    {
                        //gridToTable.Rows.Add();
                        //for (int i = 0; i < eachRow.Cells.Count; i++)
                        //{
                        //  gridToTable.Rows[eachRow.RowIndex][i] = eachCell.Text;
                        //}
                        var userName = ((Label)(eachRow.Cells[0].FindControl("lblUserName"))).Text;
                        var userMob = ((Label)(eachRow.Cells[0].FindControl("lblUserMobile"))).Text;
                        var clientName = ((Label)(eachRow.Cells[0].FindControl("lblClientName"))).Text;
                        var clientMob = ((Label)(eachRow.Cells[0].FindControl("lblClientPhn"))).Text;
                        var clientEmail = ((Label)(eachRow.Cells[0].FindControl("lblClientEmail"))).Text;
                        var address = ((Label)(eachRow.Cells[0].FindControl("lblAddress"))).Text;
                        var productValues = ((Literal)(eachRow.Cells[0].FindControl("lblProdName"))).Text;
                        productValues = productValues.Replace("<br /><br />", "\n");
                        var ordQty = ((Literal)(eachRow.Cells[0].FindControl("lblOrdQty"))).Text;
                        ordQty = ordQty.Replace("<br /><br />", "\n");
                        var productWeight = ((Literal)(eachRow.Cells[0].FindControl("lblProdWeight"))).Text;
                        productWeight = productWeight.Replace("<br /><br />", "\n");
                        var lblTotalWeight = ((Label)(eachRow.Cells[0].FindControl("lblTotalWeight"))).Text;
                        var orderTime = ((Label)(eachRow.Cells[0].FindControl("lblOrderTime"))).Text;
                        var orderId = ((Label)(eachRow.Cells[0].FindControl("lblOrderNum"))).Text;
                        var orderStatus = ((Label)(eachRow.Cells[0].FindControl("lblOrderStatus"))).Text;
                        var recharge = ((Label)(eachRow.Cells[0].FindControl("lblRechargelAmt"))).Text;
                        var withdrawl = ((Label)(eachRow.Cells[0].FindControl("lblWithdrawllAmt"))).Text;
                        var gbeanAmount = ((Label)(eachRow.Cells[0].FindControl("lblGbeanlAmt"))).Text;
                        var walletAmount = ((Label)(eachRow.Cells[0].FindControl("lblWalletAmt"))).Text;
                        var finalAmount = ((Label)(eachRow.Cells[0].FindControl("lblFinalAmt"))).Text;

                        gridToTable.Rows.Add(counter, userName, userMob, clientName, clientMob, clientEmail, address,
                            productValues, ordQty, productWeight, lblTotalWeight, orderTime, orderId, orderStatus,
                            recharge, withdrawl, gbeanAmount, walletAmount, finalAmount);
                        counter++;
                    }
                }
                //Getting Back to the First State
                grdAccepted.SetPageIndex(a);

                using (ExcelPackage myExcelPack = new ExcelPackage())
                {
                    ExcelWorksheet ws = myExcelPack.Workbook.Worksheets.Add(fileName);
                    ws.Cells["A1"].LoadFromDataTable(gridToTable, true);
                    ws.Cells.AutoFitColumns();

                    // Select only the header cells
                    var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
                    // Set their text to bold, italic and underline.
                    headerCells.Style.Font.Bold = true;
                    //headerCells.Style.Font.Italic = true;
                    //headerCells.Style.Font.UnderLine = true;
                    ws.Cells.Style.WrapText = true;
                    ws.View.FreezePanes(2, 1);

                    //center align all cells
                    var allCells = ws.Cells[1, 1, ws.Dimension.Rows, ws.Dimension.Columns];
                    allCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    allCells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    allCells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    allCells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    allCells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    allCells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    //allCells.AutoFilter = true;

                    //will change page to page
                    //adjust width column wise as needed
                    ws.Column(1).Width = 6.00;  //Sr no
                    ws.Column(7).Width = 50.00; //address
                    ws.Column(5).Width = 12.00;

                    //text align right for amounts
                    var amountCells = ws.Cells["O2:S" + ws.Dimension.Rows];
                    amountCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    amountCells.Style.Numberformat.Format = "0.00";

                    /*
                    var ms = new System.IO.MemoryStream();
                    myExcelPack.SaveAs(ms);
                    ms.WriteTo(Response.OutputStream);
                    */

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xlsx", fileName));
                    Response.BinaryWrite(myExcelPack.GetAsByteArray());
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                acceptedMsgDiv.Attributes["style"] = "display:block;";
                acceptedMsgDiv.InnerHtml = "Download error: " + ex.ToString();
            }
        }

        protected void btnExportReject_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string fileName = "Rejected_Orders_" + DateTime.UtcNow.AddMinutes(330).ToString("yyyy-MM-dd_HHmmss");

                //get gridview data inside datatable
                DataTable gridToTable = new DataTable();
                //define the positions of columns which are numeric
                int[] numbericCols = { 0, 2, 4 };
                int[] decimalCols = { 14, 15, 16,17 };

                //Add columns to DataTable.
                for (int z = 0; z < grdRejected.HeaderRow.Cells.Count; z++)
                {
                    TableCell cell = grdRejected.HeaderRow.Cells[z];
                    if (grdRejected.Columns[z].Visible)
                    {
                        if (cell.Text != "Product Image")
                        {
                            if (numbericCols.Contains(z))
                                gridToTable.Columns.Add(cell.Text, typeof(long));
                            else if (decimalCols.Contains(z))
                                gridToTable.Columns.Add(cell.Text, typeof(decimal));
                            else
                                gridToTable.Columns.Add(cell.Text);
                        }
                    }
                }
                //foreach (TableCell cell in grdPaymentDashboard.HeaderRow.Cells)
                //{
                //    gridToTable.Columns.Add(cell.Text);
                //}

                //Get Current Page Index so You can get back here after process
                int a = grdRejected.PageIndex;
                int counter = 1;
                //Loop through All Pages
                for (int i = 0; i < grdRejected.PageCount; i++)
                {
                    //Set Page Index
                    grdRejected.SetPageIndex(i);
                    //After Setting Page Index Loop through its Rows

                    foreach (GridViewRow eachRow in grdRejected.Rows)
                    {
                        //gridToTable.Rows.Add();
                        //for (int i = 0; i < eachRow.Cells.Count; i++)
                        //{
                        //  gridToTable.Rows[eachRow.RowIndex][i] = eachCell.Text;
                        //}
                        var userName = ((Label)(eachRow.Cells[0].FindControl("lblUserName"))).Text;
                        var userMob = ((Label)(eachRow.Cells[0].FindControl("lblUserMobile"))).Text;
                        var clientName = ((Label)(eachRow.Cells[0].FindControl("lblClientName"))).Text;
                        var clientMob = ((Label)(eachRow.Cells[0].FindControl("lblClientPhn"))).Text;
                        var clientEmail = ((Label)(eachRow.Cells[0].FindControl("lblClientEmail"))).Text;
                        var address = ((Label)(eachRow.Cells[0].FindControl("lblAddress"))).Text;
                        var productValues = ((Literal)(eachRow.Cells[0].FindControl("lblProdName"))).Text;
                        productValues = productValues.Replace("<br /><br />", "\n");
                        var ordQty = ((Literal)(eachRow.Cells[0].FindControl("lblOrdQty"))).Text;
                        ordQty = ordQty.Replace("<br /><br />", "\n");
                        var productWeight = ((Literal)(eachRow.Cells[0].FindControl("lblProdWeight"))).Text;
                        productWeight = productWeight.Replace("<br /><br />", "\n");
                        var orderTime = ((Label)(eachRow.Cells[0].FindControl("lblOrderTime"))).Text;
                        var orderId = ((Label)(eachRow.Cells[0].FindControl("lblOrderNum"))).Text;
                        var orderStatus = ((Label)(eachRow.Cells[0].FindControl("lblOrderStatus"))).Text;
                        var recharge = ((Label)(eachRow.Cells[0].FindControl("lblRechargelAmt"))).Text;
                        var withdrawl = ((Label)(eachRow.Cells[0].FindControl("lblWithdrawllAmt"))).Text;
                        var gbeanAmount = ((Label)(eachRow.Cells[0].FindControl("lblGbeanlAmt"))).Text;
                        var finalAmount = ((Label)(eachRow.Cells[0].FindControl("lblFinalAmt"))).Text;

                        gridToTable.Rows.Add(counter, userName, userMob, clientName, clientMob, clientEmail, address,
                            productValues, ordQty, productWeight, orderTime, orderId, orderStatus, recharge, withdrawl,gbeanAmount, finalAmount);
                        counter++;
                    }
                }
                //Getting Back to the First State
                grdRejected.SetPageIndex(a);

                using (ExcelPackage myExcelPack = new ExcelPackage())
                {
                    ExcelWorksheet ws = myExcelPack.Workbook.Worksheets.Add(fileName);
                    ws.Cells["A1"].LoadFromDataTable(gridToTable, true);
                    ws.Cells.AutoFitColumns();
                    // Select only the header cells
                    var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
                    // Set their text to bold, italic and underline.
                    headerCells.Style.Font.Bold = true;
                    //headerCells.Style.Font.Italic = true;
                    //headerCells.Style.Font.UnderLine = true;
                    ws.Cells.Style.WrapText = true;
                    ws.View.FreezePanes(2, 1);

                    //center align all cells
                    var allCells = ws.Cells[1, 1, ws.Dimension.Rows, ws.Dimension.Columns];
                    allCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    allCells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    allCells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    allCells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    allCells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    allCells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    //allCells.AutoFilter = true;

                    //will change page to page
                    //adjust width column wise as needed
                    ws.Column(1).Width = 6.00;  //Sr no
                    ws.Column(7).Width = 50.00; //address
                    ws.Column(5).Width = 12.00;

                    //text align right for amounts
                    var amountCells = ws.Cells["N2:Q" + ws.Dimension.Rows];
                    amountCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    amountCells.Style.Numberformat.Format = "0.00";

                    /*
                    var ms = new System.IO.MemoryStream();
                    myExcelPack.SaveAs(ms);
                    ms.WriteTo(Response.OutputStream);
                    */

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xlsx", fileName));
                    Response.BinaryWrite(myExcelPack.GetAsByteArray());
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                rejectedMsgDiv.Attributes["style"] = "display:block;";
                rejectedMsgDiv.InnerHtml = "Download error: " + ex.ToString();
            }
        }

        protected void btnExportDisOrder_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string fileName = "Dispatched_Orders_" + DateTime.UtcNow.AddMinutes(330).ToString("yyyy-MM-dd_HHmmss");

                //get gridview data inside datatable
                DataTable gridToTable = new DataTable();
                //define the positions of columns which are numeric
                int[] numbericCols = { 0, 2, 4 };
                int[] decimalCols = { 14, 15, 16, 17, 18 };

                //Add columns to DataTable.
                for (int z = 0; z < grdDispatched.HeaderRow.Cells.Count; z++)
                {
                    TableCell cell = grdDispatched.HeaderRow.Cells[z];
                    if (grdDispatched.Columns[z].Visible)
                    {
                        if (cell.Text != "Product Image")
                        {
                            if (numbericCols.Contains(z))
                                gridToTable.Columns.Add(cell.Text, typeof(long));
                            else if (decimalCols.Contains(z))
                                gridToTable.Columns.Add(cell.Text, typeof(decimal));
                            else
                                gridToTable.Columns.Add(cell.Text);
                        }
                    }
                }
                //foreach (TableCell cell in grdPaymentDashboard.HeaderRow.Cells)
                //{
                //    gridToTable.Columns.Add(cell.Text);
                //}

                //Get Current Page Index so You can get back here after process
                int a = grdDispatched.PageIndex;
                int counter = 1;
                //Loop through All Pages
                for (int i = 0; i < grdDispatched.PageCount; i++)
                {
                    //Set Page Index
                    grdDispatched.SetPageIndex(i);
                    //After Setting Page Index Loop through its Rows

                    foreach (GridViewRow eachRow in grdDispatched.Rows)
                    {
                        //gridToTable.Rows.Add();
                        //for (int i = 0; i < eachRow.Cells.Count; i++)
                        //{
                        //  gridToTable.Rows[eachRow.RowIndex][i] = eachCell.Text;
                        //}
                        var userName = ((Label)(eachRow.Cells[0].FindControl("lblUserName"))).Text;
                        var userMob = ((Label)(eachRow.Cells[0].FindControl("lblUserMobile"))).Text;
                        var clientName = ((Label)(eachRow.Cells[0].FindControl("lblClientName"))).Text;
                        var clientMob = ((Label)(eachRow.Cells[0].FindControl("lblClientPhn"))).Text;
                        var clientEmail = ((Label)(eachRow.Cells[0].FindControl("lblClientEmail"))).Text;
                        var address = ((Label)(eachRow.Cells[0].FindControl("lblAddress"))).Text;
                        var productValues = ((Literal)(eachRow.Cells[0].FindControl("lblProdName"))).Text;
                        productValues = productValues.Replace("<br /><br />", "\n");
                        var ordQty = ((Literal)(eachRow.Cells[0].FindControl("lblOrdQty"))).Text;
                        ordQty = ordQty.Replace("<br /><br />", "\n");
                        var productWeight = ((Literal)(eachRow.Cells[0].FindControl("lblProdWeight"))).Text;
                        productWeight = productWeight.Replace("<br /><br />", "\n");
                        var orderTime = ((Label)(eachRow.Cells[0].FindControl("lblOrderTime"))).Text;
                        var orderId = ((Label)(eachRow.Cells[0].FindControl("lblOrderNum"))).Text;
                        var orderStatus = ((Label)(eachRow.Cells[0].FindControl("lblOrderStatus"))).Text;
                        var recharge = ((Label)(eachRow.Cells[0].FindControl("lblRechargelAmt"))).Text;
                        var withdrawl = ((Label)(eachRow.Cells[0].FindControl("lblWithdrawllAmt"))).Text;
                        var gbeanAmount = ((Label)(eachRow.Cells[0].FindControl("lblGbeanlAmt"))).Text;
                        var walletAmount = ((Label)(eachRow.Cells[0].FindControl("lblWalletAmt"))).Text;
                        var finalAmount = ((Label)(eachRow.Cells[0].FindControl("lblFinalAmt"))).Text;

                        gridToTable.Rows.Add(counter, userName, userMob, clientName, clientMob, clientEmail, address,
                            productValues, ordQty, productWeight, orderTime, orderId, orderStatus, recharge, withdrawl,
                            gbeanAmount, walletAmount, finalAmount);
                        counter++;
                    }
                }
                //Getting Back to the First State
                grdDispatched.SetPageIndex(a);

                using (ExcelPackage myExcelPack = new ExcelPackage())
                {
                    ExcelWorksheet ws = myExcelPack.Workbook.Worksheets.Add(fileName);
                    ws.Cells["A1"].LoadFromDataTable(gridToTable, true);
                    ws.Cells.AutoFitColumns();

                    // Select only the header cells
                    var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
                    // Set their text to bold, italic and underline.
                    headerCells.Style.Font.Bold = true;
                    //headerCells.Style.Font.Italic = true;
                    //headerCells.Style.Font.UnderLine = true;
                    ws.Cells.Style.WrapText = true;
                    ws.View.FreezePanes(2, 1);

                    //center align all cells
                    var allCells = ws.Cells[1, 1, ws.Dimension.Rows, ws.Dimension.Columns];
                    allCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    allCells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    allCells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    allCells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    allCells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    allCells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    //allCells.AutoFilter = true;

                    //will change page to page
                    //adjust width column wise as needed
                    ws.Column(1).Width = 6.00;  //Sr no
                    ws.Column(7).Width = 50.00; //address
                    ws.Column(5).Width = 12.00;

                    //text align right for amounts
                    var amountCells = ws.Cells["N2:R" + ws.Dimension.Rows];
                    amountCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    amountCells.Style.Numberformat.Format = "0.00";

                    /*
                    var ms = new System.IO.MemoryStream();
                    myExcelPack.SaveAs(ms);
                    ms.WriteTo(Response.OutputStream);
                    */

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xlsx", fileName));
                    Response.BinaryWrite(myExcelPack.GetAsByteArray());
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                dispatchedMsgDiv.Attributes["style"] = "display:block;";
                dispatchedMsgDiv.InnerHtml = "Download error: " + ex.ToString();
            }
        }

        protected void btnDwnldInvoiceClick(object sender, EventArgs e)
        {
            try
            {
                //grid inside btn not triggering download popup becoz of update panel
                //as an alternative, using hidden btn & hidden value to achieve the same
                //Button btn = (Button)sender;
                //int introw = Convert.ToInt32(btn.Attributes["RowIndex"]);

                if (!string.IsNullOrWhiteSpace(hidRowIdForInvoice.Value))
                {
                    int introw = Convert.ToInt32(hidRowIdForInvoice.Value);


                    Label lblOrderDate = (Label)grdAccepted.Rows[introw].FindControl("lblOrderDate");
                    Label lblOrderTime = (Label)grdAccepted.Rows[introw].FindControl("lblOrderTime");
                    Label lblOrderNum = (Label)grdAccepted.Rows[introw].FindControl("lblOrderNum");
                    Label lblClientName = (Label)grdAccepted.Rows[introw].FindControl("lblClientName");
                    Label lblClientPhn = (Label)grdAccepted.Rows[introw].FindControl("lblClientPhn");
                    Label lblClientEmail = (Label)grdAccepted.Rows[introw].FindControl("lblClientEmail");
                    Label lblClientGST = (Label)grdAccepted.Rows[introw].FindControl("lblClientGST");
                    Label lblClientPAN = (Label)grdAccepted.Rows[introw].FindControl("lblClientPAN");
                    Label lblClientAadhaar = (Label)grdAccepted.Rows[introw].FindControl("lblClientAadhaar");
                    Label lblAdd = (Label)grdAccepted.Rows[introw].FindControl("lblAddress");
                    Literal lblProdName = (Literal)grdAccepted.Rows[introw].FindControl("lblProdName");
                    Literal lblOrdQty = (Literal)grdAccepted.Rows[introw].FindControl("lblOrdQty");
                    Literal lblProdWeight = (Literal)grdAccepted.Rows[introw].FindControl("lblProdWeight");
                    Literal lblPordPrice = (Literal)grdAccepted.Rows[introw].FindControl("lblPordPrice");
                    Label lblFinalAmt = (Label)grdAccepted.Rows[introw].FindControl("lblFinalAmt");

                    string[] prodNameArr = lblProdName.Text.Replace("<br /><br />", ",").Split(new[] { "," }, StringSplitOptions.None);
                    string[] prodQtyArr = lblOrdQty.Text.Replace("<br /><br />", ",").Split(new[] { "," }, StringSplitOptions.None);
                    string[] prodWghtArr = lblProdWeight.Text.Replace("<br /><br />", ",").Split(new[] { "," }, StringSplitOptions.None);
                    string[] prodPriceArr = lblPordPrice.Text.Replace("<br /><br />", ",").Split(new[] { "," }, StringSplitOptions.None);

                    if (prodNameArr.Length != prodQtyArr.Length || prodNameArr.Length != prodWghtArr.Length || prodQtyArr.Length != prodWghtArr.Length)
                    {
                        //something is wrong
                        acceptedMsgDiv.Attributes["style"] = "display: block";
                        acceptedMsgDiv.InnerHtml += "\nSomething is wrong - generating the Invoice! ";
                    }
                    else
                    { }

                    decimal amountPaid = Convert.ToDecimal(lblFinalAmt.Text);
                    decimal amount = Math.Round(amountPaid / 1.03M, 2, MidpointRounding.AwayFromZero);
                    decimal gstOnAmount = Math.Round(amount * 0.03M, 2, MidpointRounding.AwayFromZero);
                    decimal discount = amountPaid - (amount + gstOnAmount);

                    string invoiceNum = string.Format("{0}_{1}", "INV", lblOrderNum.Text.Replace(":", "").Replace("-", "").Replace(" ", "_"));

                    //load template & read
                    string templatePath = HttpContext.Current.Server.MapPath(GlobalVariables.invoiceTemplatePath);
                    string invoiceTemplate = System.IO.File.ReadAllText(templatePath);


                    decimal[] prodTotWeight = new decimal[prodWghtArr.Length];
                    for (int z = 0; z < prodWghtArr.Length; z++)
                    {
                        prodTotWeight[z] = Convert.ToDecimal(prodWghtArr[z]) * Convert.ToDecimal(prodQtyArr[z]);
                    }

                    decimal[] prodPerGramRate = new decimal[prodPriceArr.Length];
                    for (int z = 0; z < prodPriceArr.Length; z++)
                    {
                        prodPerGramRate[z] = Math.Round(Math.Round(Convert.ToDecimal(prodPriceArr[z]) / 1.03M, 2, MidpointRounding.AwayFromZero)
                                            / Convert.ToDecimal(prodWghtArr[z]), 2, MidpointRounding.AwayFromZero);
                    }

                    /*
                    decimal totWeight = prodTotWeight.Sum();
                    decimal perGramRate = Math.Round(amount / totWeight, 3, MidpointRounding.AwayFromZero);
                    decimal calcAmout = prodTotWeight.Sum(x => Math.Round(x * perGramRate, 2, MidpointRounding.AwayFromZero));
                    discount = amountPaid - (calcAmout + gstOnAmount);
                    */

                    //for dynamically adding product rows
                    string productRows = "";
                    for (int i = 0; i < prodNameArr.Length; i++)
                    {
                        int counter = i + 1;

                        /*change in logic
                        string eachProdName = prodNameArr[i].Trim();
                        string eachProdQty = prodQtyArr[i].Trim();
                        string eachProdWght = prodWghtArr[i].Trim() + "gm";

                        invoiceTemplate = invoiceTemplate
                                            .Replace("{srNo" + counter + "}", counter.ToString())
                                            .Replace("{pName" + counter + "}", eachProdName)
                                            .Replace("{pQty" + counter + "}", eachProdQty)
                                            .Replace("{pRate" + counter + "}", eachProdWght)
                                            ;
                        */

                        productRows +=
                            "		<tr> " +
                            "			<td class=\"bold-text left-border\" colspan=6 align=\"left\" valign=middle>"
                                            + counter + ".&nbsp;&nbsp;" + prodNameArr[i].Trim() + "</td> " +
                            "			<td class=\"left-border\" align=\"center\" valign=middle>7113</td> " +
                            "			<td class=\"left-border\" align=\"right\" valign=middle>0 %</td> " +
                            "			<td class=\"left-border\" align=\"right\" valign=middle>" + prodQtyArr[i].Trim() + "</td> " +
                            "			<td class=\"left-border\" align=\"right\" valign=middle>" + prodWghtArr[i].Trim() + "gm</td> " +
                            "			<td class=\"left-border\" align=\"right\" valign=middle>" + prodPerGramRate[i].ToString("0.00") + "</td> " +
                            "			<td class=\"bold-text left-border right-border\" align=\"right\" valign=middle>"
                                            + (Math.Round(prodTotWeight[i] * prodPerGramRate[i], 2, MidpointRounding.AwayFromZero)).ToString("0.00") + "</td> " +

                            "		</tr> " +
                            "";
                    }

                    //for adding blank space
                    string emptyRows = "";
                    for (int j = 0; j < (10 - prodNameArr.Length); j++)
                    {
                        emptyRows +=
                            "		<tr> " +
                            "        	<td class=\"left-border\" colspan=6 align=\"center\" valign=middle>&nbsp;</td> " +
                            "        	<td class=\"left-border\" align=\"left\" valign=middle>&nbsp;</td> " +
                            "        	<td class=\"left-border\" align=\"right\" valign=middle>&nbsp;</td> " +
                            "        	<td class=\"left-border\" align=\"right\" valign=middle>&nbsp;</td> " +
                            "        	<td class=\"left-border\" align=\"right\" valign=middle>&nbsp;</td> " +
                            "        	<td class=\"left-border\" align=\"right\" valign=middle>&nbsp;</td> " +
                            "        	<td class=\"left-border right-border\" align=\"right\" valign=middle>&nbsp;</td> " +
                            "        </tr> " +
                            "";
                    }

                    //replace bill details in template
                    invoiceTemplate = invoiceTemplate
                                        .Replace("{invoiceNo}", invoiceNum)
                                        .Replace("{invoiceDt}", lblOrderDate.Text.Trim())
                                        .Replace("{userName}", lblClientName.Text.Trim().ToUpper())
                                        .Replace("{userAddress}", lblAdd.Text.Trim().ToUpper())
                                        .Replace("{userMobile}", lblClientPhn.Text.Trim())
                                        .Replace("{userEmail}", lblClientEmail.Text.Trim())
                                        .Replace("{userGSTNo}",
                                            lblClientGST == null ? "N/A" :
                                                string.IsNullOrWhiteSpace(lblClientGST.Text.Trim()) ? "N/A" : lblClientGST.Text.Trim())
                                        .Replace("{userPAN}",
                                            lblClientPAN == null ? "" :
                                                string.IsNullOrWhiteSpace(lblClientPAN.Text.Trim()) ? "" : lblClientPAN.Text.Trim())
                                        .Replace("{userAadhaar}",
                                            lblClientAadhaar == null ? "" :
                                                string.IsNullOrWhiteSpace(lblClientAadhaar.Text.Trim()) ? "" : lblClientAadhaar.Text.Trim())
                                        .Replace("{amount}", amount.ToString("0.00"))
                                        .Replace("{gstAmt}", gstOnAmount.ToString("0.00"))
                                        .Replace("{discount}", discount.ToString("0.00"))
                                        .Replace("{totalAmt}", amountPaid.ToString("N2"))
                                        .Replace("{amountWithComma}", amount.ToString("N2"))
                                        .Replace("{gstValOnAmtWithComma}", gstOnAmount.ToString("N2"))
                                        .Replace("{totalAmtInWords}", NumberToWord.ConvertNumberToWords(amountPaid))
                                        .Replace("{gstAmtInWords}", NumberToWord.ConvertNumberToWords(gstOnAmount))
                        //.Replace("{totalAmt}", amountPaid.ToString("N2"))
                        //                    ;

                        //invoiceTemplate = invoiceTemplate
                                        .Replace("{productRows}", productRows)
                                        .Replace("{emptyRows}", emptyRows)
                                      ;


                    string fileName;
                    byte[] pdfByteArray;
                    bool isInvoiceGenerated = pdfSharpConvert(invoiceTemplate, out fileName, out pdfByteArray);
                    if (isInvoiceGenerated)
                    {
                        acceptedMsgDiv.Attributes["style"] = "display: block";
                        acceptedMsgDiv.InnerHtml = "Invoice generated successfully! Check your Downloads folder";

                        //Prepare the HTTP response
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.pdf", fileName));

                        //Write the PDF file. Browsers will trigger a popup asking to save or open a PDF file.
                        Response.BinaryWrite(pdfByteArray);
                        //Close the HTTP Response Stream
                        Response.End();
                    }
                    else
                    {
                        acceptedMsgDiv.Attributes["style"] = "display: block";
                        acceptedMsgDiv.InnerHtml += "\n\nSomething went wrong while generating the Invoice!";
                    }
                }
                else
                {
                    acceptedMsgDiv.Attributes["style"] = "display: block";
                    acceptedMsgDiv.InnerHtml += "\n\nSomething went wrong while generating the Invoice!";
                }
            }
            catch (Exception ex)
            {
                acceptedMsgDiv.Attributes["style"] = "display: block";
                acceptedMsgDiv.InnerHtml += "\n\nSomething went wrong while generating the Invoice! " + ex.ToString();
            }
        }

        protected bool pdfSharpConvert(String html, out string nameForFile, out byte[] generatedPdfArray)
        {
            try
            {   //The actual PDF generation
                using (var pdf = PdfGenerator.GeneratePdf(html, PageSize.A4))
                {
                    //Get Date of PDF generation
                    DateTime dt = DateTime.UtcNow.AddMinutes(330);

                    //name for generated pdf
                    nameForFile = "Invoice_" + dt.ToString("yyyy-MM-dd_HHmmss");

                    //Setting Font for our footer
                    XFont font = new XFont("Segoe UI,Open Sans, sans-serif, serif", 7);
                    XBrush brush = XBrushes.Black;

                    //Loop through our generated PDF pages, one by one
                    for (int i = 0; i < pdf.PageCount; i++)
                    {
                        //Get each page
                        PdfSharp.Pdf.PdfPage page = pdf.Pages[i];

                        //Create rectangular area that will hold our footer – play with dimensions according to your page'scontent height and width
                        //XRect layoutRectangle = new XRect((double)0, (double)((int)page.Height – (font.Height + 9)), (double)page.Width, (double)(font.Height - 7));
                        int fontHeight = font.Height;
                        XRect layoutRectangle = new XRect(Convert.ToDouble(0), Convert.ToDouble(page.Height - (fontHeight + 9)), Convert.ToDouble(page.Width), Convert.ToDouble(fontHeight - 7));

                        //Draw the footer on each page
                        using (XGraphics gfx = XGraphics.FromPdfPage(page))
                        {
                            gfx.DrawString(
                                "Invoice generated on " + dt.ToString("yyyy-MM-dd")
                                + "      |      " +
                                "Page " + (i + 1) + " of " + pdf.PageCount,
                                font, brush, layoutRectangle, XStringFormats.Center);
                        }
                    }
                    //string SaveTo = string.Format(HttpContext.Current.Server.MapPath("Files/") + "{0}.pdf", nameForFile);
                    //pdf.Save(SaveTo);

                    //Prepare to download
                    using (MemoryStream outputstream = new MemoryStream())
                    {
                        pdf.Save(outputstream);
                        generatedPdfArray = outputstream.ToArray();
                    }
                }

                return true;
            }
            catch (Exception er)
            {
                //pendingMsgDiv.InnerHtml = "Report an error no : " + Convert.ToString(pLngErr) + "to System Administrator";
                acceptedMsgDiv.InnerHtml = er.ToString();
                nameForFile = "";
                generatedPdfArray = new byte[1];
                return false;
            }
            finally
            {
                GC.Collect();
            }
        }

        protected void btnExportClick(object sender, EventArgs e)
        {
            //grdPending.AllowPaging = false;
            //btnResult_Click(sender, e);
            //string fileName = "Withdrawal_" + DateTime.UtcNow.AddMinutes(330).ToString("yyyy-MM-dd_HHmmss");
            //
            //Response.Clear();
            //Response.Buffer = true;
            //Response.Charset = "";
            //Response.ContentType = "application/excel";
            //Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", fileName));
            //
            //StringWriter stringwriter = new StringWriter();
            //HtmlTextWriter htmlwriter = new HtmlTextWriter(stringwriter);
            //
            ////hide footer before writing to file
            ////grdPending.ShowFooter = false;
            //grdPending.FooterRow.Visible = false;
            //
            //grdPending.RenderControl(htmlwriter);
            //string style = @"<style> .AccNum { mso-number-format:\@; width: 170px; } </style>";
            //Response.Write(style);
            //Response.Write(stringwriter.ToString());
            //Response.End();
            //
            ////show footer again
            //grdPending.FooterRow.Visible = true;
        }

    }

}