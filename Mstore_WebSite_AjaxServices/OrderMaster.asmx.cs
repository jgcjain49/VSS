using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace Admin_CommTrex.Mstore_WebSite_AjaxServices
{
    /// <summary>
    /// Summary description for OrderMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class OrderMaster : System.Web.Services.WebService
    {

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession=true)]
        public void GetFullOrderDetails(long mLngOrderId)
        {
            OrderDetailsFull mObjDetails = new OrderDetailsFull();
            string pStrConnectionString = "";
            try
            {
                pStrConnectionString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
            }
            catch (Exception ex)
            {
                mObjDetails.Error = "User login timed out";
            }
            if (pStrConnectionString != "")
            {
                string mStrQuery = "Select REPLACE(pm.PM_vCharThumb64,'\\','/') As [ImgPath],"
                                 + "pm.PM_vCharProdName,"
                                 + "pm.PM_vCharProdType,"
                                 + "Coalesce(os.OrdSub_decQty,0.00) As [OrdSub_decQty],"
                                 + "Coalesce(os.OrdSub_decBasicAmt,0.00) As [OrdSub_decBasicAmt],"
                                 + "Coalesce(os.OrdSub_decQty * os.OrdSub_decBasicAmt,0.00) As [TotAmt]    "
                                 + "From dbo.OrderSub os Left Join "
                                 + "ProductMaster pm "
                                 + "On	 os.OrdSub_bIntProdId = pm.PM_bIntProdId    "
                                 + "Where os.OrdSub_bIntOrdId = @lngId   ";
                try
                {
                    DataTable dtOrderSubsData = SqlHelper.ReadTable(mStrQuery, pStrConnectionString, false,
                                                          SqlHelper.AddInParam("@lngId", SqlDbType.BigInt, mLngOrderId));

                    if (dtOrderSubsData.Rows.Count > 0)
                    {
                        List<OrderSub> mLstOrderSubs = new List<OrderSub>();
                        foreach (DataRow mDrData in dtOrderSubsData.Rows)
                        {
                            mLstOrderSubs.Add(new OrderSub(Convert.ToString(mDrData["ImgPath"]),
                                                           Convert.ToString(mDrData["PM_vCharProdName"]),
                                                           Convert.ToString(mDrData["PM_vCharProdType"]),
                                                           Convert.ToDouble(mDrData["OrdSub_decQty"]),
                                                           Convert.ToDouble(mDrData["OrdSub_decBasicAmt"]),
                                                           Convert.ToDouble(mDrData["TotAmt"]))
                                              );
                        }
                        mObjDetails.Error = "";
                        mObjDetails.OrderData = mLstOrderSubs;
                    }
                    else
                    {
                        mObjDetails.Error = "No data found for selection";
                    }
                }
                catch (Exception ex)
                {
                    mObjDetails.Error = "Something went wrong at server side";
                }
            }
            else
            {
                mObjDetails.Error = "User login timed out";
            }

            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(JsonConvert.SerializeObject(mObjDetails));
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]

        public void GetDealersMin() // Dealers with minimum data
        {
            DealerData mObjDetails = new DealerData();
            string pStrConnectionString = "";
            try
            {
                pStrConnectionString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
            }
            catch (Exception ex)
            {
                mObjDetails.Error = "User login timed out";
            }
            if (pStrConnectionString != "")
            {
                string mStrQuery = "SELECT Dlr_bIntId,Dlr_nVarName FROM DealerMaster";
                try
                {
                    DataTable dtDealerDataMin = SqlHelper.ReadTable(mStrQuery, pStrConnectionString, false);

                    if (dtDealerDataMin.Rows.Count > 0)
                    {
                        List<Dealer> mLstDealers = new List<Dealer>();
                        foreach (DataRow mDrData in dtDealerDataMin.Rows)
                        {
                            mLstDealers.Add(new Dealer(Convert.ToInt64(mDrData["Dlr_bIntId"]),
                                                       Convert.ToString(mDrData["Dlr_nVarName"]))
                                           );
                        }
                        mObjDetails.Error = "";
                        mObjDetails.Dealers = mLstDealers;
                    }
                    else
                    {
                        //mObjDetails.Error = "<i class=\"fa fa-ban fa-stack-1x fa-database text-danger\"></i> No dealer data found";
                        string mStrHtml = "<span class=\"fa-stack fa-lg\">"
                                        + "     <i class=\"fa fa-database fa-stack-1x\"></i>"
                                        + "     <i class=\"fa fa-ban fa-stack-2x\"></i>"
                                        + "</span>";
                        mObjDetails.Error = mStrHtml + " No dealer data found";
                    }
                }
                catch (Exception ex)
                {
                    mObjDetails.Error = "Something went wrong at server side";
                }
            }
            else
            {
                mObjDetails.Error = "User login timed out";
            }

            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(JsonConvert.SerializeObject(mObjDetails));
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void UpdateOrderStatus(long pLngOrderId,int pIntStatus,bool pBlnSendEmail,bool pBlnSendNotification,string pJsonStrOrderStatusParams) // Updates Order Status And Sends ... Email , Notification
        {
            OrderStausUpdateResults mObjStatusDetails = new OrderStausUpdateResults();
            string mStrConnectionString = "";
            try
            {
                mStrConnectionString = Convert.ToString(Session["SystemUserSqlConnectionString"]);
            }
            catch (Exception ex)
            {
                mObjStatusDetails.UpdateStatusGeneralError = "User login timed out";
            }
            if (mStrConnectionString != "")
            {
                // Update
                if (pJsonStrOrderStatusParams == "")
                    mObjStatusDetails.UpdateStatusDbError = "No details provided for updation";
                else
                {
                    try
                    {
                        OrderStausParams mObjParams = JsonConvert.DeserializeObject<OrderStausParams>(pJsonStrOrderStatusParams);

                        try
                        {

                            if (mObjParams.DeliveryAgent == null)
                                mObjParams.DeliveryAgent = new DeliveryAgent();

                            DataTable dtUpdateStatus = SqlHelper.ReadTable("SP_UpdateOrderStatus", mStrConnectionString,true,
                                                                SqlHelper.AddInParam("@bIntOrderId", SqlDbType.BigInt, pLngOrderId),
                                                                SqlHelper.AddInParam("@iNtOrderStatus", SqlDbType.Int, pIntStatus),
                                                                SqlHelper.AddInParam("@bIntDealerId", SqlDbType.BigInt, mObjParams.DealerId),
                                                                SqlHelper.AddInParam("@nVarDeliveryAgentName", SqlDbType.NVarChar, mObjParams.DeliveryAgent.DeliveryAgentName),
                                                                SqlHelper.AddInParam("@dtDeliveryTime", SqlDbType.DateTime, mObjParams.DeliveryAgent.DeliveryTime),
                                                                SqlHelper.AddInParam("@vCharTrackId1", SqlDbType.VarChar, mObjParams.DeliveryAgent.DeliveryTrackingId1),
                                                                SqlHelper.AddInParam("@vCharTrackId2", SqlDbType.VarChar, mObjParams.DeliveryAgent.DeliveryTrackingId2),
                                                                SqlHelper.AddInParam("@vCharTrackId3", SqlDbType.VarChar, mObjParams.DeliveryAgent.DeliveryTrackingId3),
                                                                SqlHelper.AddInParam("@vCharTrackId4", SqlDbType.VarChar, mObjParams.DeliveryAgent.DeliveryTrackingId4),
                                                                SqlHelper.AddInParam("@vCharTrackId5", SqlDbType.VarChar, mObjParams.DeliveryAgent.DeliveryTrackingId5),
                                                                SqlHelper.AddInParam("@nVarCancellationReason", SqlDbType.NVarChar, mObjParams.CancellationReason));

                            if (dtUpdateStatus.Rows.Count > 0)
                            {
                                if (Convert.ToString(dtUpdateStatus.Rows[0][0]) == "")
                                {
                                    string mStrQry = "Select * From vwOrder_Manangement_Basic_Status Where Ord_bIntId = @id";
                                    DataTable dtOrderDetails = SqlHelper.ReadTable(mStrQry, mStrConnectionString, false,
                                                    SqlHelper.AddInParam("@id", SqlDbType.BigInt, pLngOrderId));

                                    if (pBlnSendEmail)
                                    {
                                        // Send email
                                        if (Session["SystemCompany"] is SysCompany)
                                            mObjStatusDetails.UpdateStatusEmailError = SendEmail(dtOrderDetails, (SysCompany)Session["SystemCompany"]);
                                        else
                                            mObjStatusDetails.UpdateStatusEmailError = "User login timed out";
                                    }

                                    if (pBlnSendNotification)
                                    {
                                        // Send notification
                                        if (Session["SystemUser"] is SystemUser)
                                            mObjStatusDetails.UpdateStatusNotificationError = SendNotification(dtOrderDetails, (SystemUser)Session["SystemUser"]);
                                        else
                                            mObjStatusDetails.UpdateStatusNotificationError = "User login timed out";
                                    }
                                }
                                else
                                    mObjStatusDetails.UpdateStatusDbError = Convert.ToString(dtUpdateStatus.Rows[0][0]);
                            }
                            else
                            {
                                mObjStatusDetails.UpdateStatusDbError = "Something went wrong on server side";
                            }
                        }
                        catch (Exception exUpdateFailed)
                        {
                            // Failed to update sql database
                            mObjStatusDetails.UpdateStatusDbError = String.Format("Server Updates Error : <br/>{0}", exUpdateFailed.Message);
                        }
                    }
                    catch 
                    {
                        // Failed to cast json string to .net object
                        mObjStatusDetails.UpdateStatusDbError = "Failed to read update details";
                    }
                }
            }
            else
            {
                mObjStatusDetails.UpdateStatusGeneralError = "User login timed out";
            }

            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(JsonConvert.SerializeObject(mObjStatusDetails));
        }



        #region "Class Methods"

        private string SendEmail(long pLngOrderId,string pStrConnectionString,SysCompany pObjCurrCompany)
        {
            string mStrQry = "Select * From vwOrder_Manangement_Basic_Status Where Ord_bIntId = @id";
            try
            {
                DataTable dtOrderDetails = SqlHelper.ReadTable(mStrQry, pStrConnectionString, false, 
                                                    SqlHelper.AddInParam("@id", SqlDbType.BigInt, pLngOrderId));
                return SendEmail(dtOrderDetails, pObjCurrCompany);
            }
            catch (Exception exReadTable)
            {
                return exReadTable.Message;
            }
        }

        private string SendEmail(DataTable dtBasicDetails, SysCompany pObjCurrCompany)
        {
            string mStrError = "";
            try
            {
                string mStrQry = "Select * From EmailSettings Where ES_bIntCompanyId = @id";
                DataTable dtEmailSettings = SqlHelper.ReadTable(mStrQry, false, SqlHelper.AddInParam("@id", SqlDbType.BigInt, pObjCurrCompany.CompanyId));
                if (dtEmailSettings.Rows.Count > 0 && dtBasicDetails.Rows.Count > 0)
                {
                    DataRow drEmail = dtEmailSettings.Rows[0];
                    if (Convert.ToString(drEmail["ES_CompanyGmailId"]) != "" && Convert.ToString(drEmail["ES_CompanyGmailPassword"]) != "")
                    {
                        DataRow drOrder = dtBasicDetails.Rows[0];
                        if (Convert.ToString(drOrder["UCon_vCharEmail"]) != "")
                        {

                            string mStrHtml = EmailCss + "<h4>Respected Client, <b>" + Convert.ToString(drOrder["Usr_Name"]) + "</b></h4>Status of your order has been updated as follows : <br/>"
                                        + "<div class=\"coolTable\"><table><tr><td>Description</td><td>Detail</td></tr>{0}</table></div>";
                            string mStrRows = "";
                            mStrRows += GetTableNode("Order Number", drOrder["Ord_bIntId"]);
                            mStrRows += GetTableNode("Order Time", String.Format("{0} {1}", drOrder["vw_vCharOrderDate"], drOrder["vw_vCharOrderTime"]));
                            mStrRows += GetTableNode("Order Status", drOrder["vw_vCharOrderStatus"]);

                            switch (Convert.ToInt32(drOrder["Ord_iNtOrderStatus"]))
                            {
                                case 1: //CONFIRMED
                                    mStrRows += GetTableNode("Confirmation Time", drOrder["OrdStat_dtConfirmTime"]);
                                    mStrRows += GetTableNode("Dealer Name", drOrder["Dlr_nVarName"]);
                                    mStrRows += GetTableNode("Dealer Email", drOrder["Dlr_vCharEmail"]);
                                    mStrRows += GetTableNode("Dealer Phone", drOrder["Dlr_nVarPhone"]);
                                    mStrRows += GetTableNode("Dealer Address", drOrder["Dlr_nVarAddress"]);
                                    break;
                                case 2: //DISPATCHED
                                    mStrRows += GetTableNode("Delivery Service", drOrder["Dlvry_nVarServiceName"]);
                                    mStrRows += GetTableNode("Delivery Estimated Time", drOrder["Dlvry_dtEstimatedDeliveryTime"]);
                                    mStrRows += GetTableNode("Delivery Tracking Id", drOrder["Dlvry_vCharTrackingId1"]);
                                    break;
                                case 3: //PROCESSED
                                    mStrRows += GetTableNode("Delivery Date", drOrder["OrdStat_dtDeliveryDate"]);
                                    break;
                                case 4: //CANCELLED
                                    mStrRows += GetTableNode("Cancellation Time", drOrder["OrdStat_dtCancelTime"]);
                                    mStrRows += GetTableNode("Cancellation Reason", drOrder["OrdStat_nVarCancelReason"]);
                                    break;
                            }

                            mStrHtml = String.Format(mStrHtml, mStrRows);

                            MailMessage mMailMsg = new MailMessage(new MailAddress(Convert.ToString(drEmail["ES_CompanyGmailId"])), new MailAddress(Convert.ToString(drOrder["UCon_vCharEmail"])));
                            if (Convert.ToString(drEmail["ES_CCEmail"]) != "")
                                mMailMsg.Bcc.Add(Convert.ToString(drEmail["ES_CCEmail"]));
                            mMailMsg.IsBodyHtml = true;
                            mMailMsg.Body = mStrHtml;
                            mMailMsg.Subject = String.Format("MStore - Order Status Update ({0})", drOrder["Ord_bIntId"]);

                            SmtpClient mClient = new SmtpClient(Convert.ToString(drEmail["ES_CompanyGmailSmtpServver"]), Convert.ToInt32(drEmail["ES_CompanyGmailSmtpPort"]));
                            mClient.Credentials = new NetworkCredential(Convert.ToString(drEmail["ES_CompanyGmailId"]), Convert.ToString(drEmail["ES_CompanyGmailPassword"]));
                            mClient.EnableSsl = true;

                            mClient.Send(mMailMsg);

                            mStrError = "";
                        }
                        else
                            mStrError = "Client hasn't provided email id";
                    }
                    else
                        mStrError = "Please set gmail id and password of your company for sending emails";
                }
                else
                    mStrError = "No email setting found your company";
            }
            catch (Exception exSendEmail)
            {
                mStrError = exSendEmail.Message;
            }

            return mStrError;
        }

        private string SendNotification(DataTable dtBasicDetails, SystemUser pObjCurrUser)
        {
            string mStrError = "";

            if (dtBasicDetails.Rows.Count > 0)
            {
                DataRow drOrder = dtBasicDetails.Rows[0];

                if (Convert.ToString(drOrder["Usr_DeviceRegId"]) != "")
                {

                    // Sending clound message to devices
                    // *---------------------------------------*

                    // Create message object.
                    //
                    GcmMessageRequest GcmPushMessage = new GcmMessageRequest();

                    // Add Sender
                    //
                    GcmPushMessage.SetSender(pObjCurrUser.UserSysId, "System Auto Sender");

                    // Set Reciever
                    //
                    GcmPushMessage.AddReciever(Convert.ToString(drOrder["Ord_vCharClntId"]), Convert.ToString(drOrder["Usr_DeviceRegId"]));

                    // Set Message
                    //
                    GcmPushMessage.SetMessage(String.Format("Your order has been {0}.Order Number {1}", drOrder["vw_vCharOrderStatus"], drOrder["Ord_bIntId"]), Convert.ToInt32(drOrder["Ord_bIntId"]), MessageTypes.ORDER_STATUS_UPDATES);

                    // Set Bunch id
                    //
                    GcmPushMessage.MessageBunchId = String.Format("Spcl_Order_Status_Update_{0}", DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss_tt"));

                    // Create Json Message
                    // 
                    string sJsonMsg = "";

                    try
                    {
                        sJsonMsg = GcmPushMessage.CreateJsonMsg();
                    }
                    catch (Exception JsonError)
                    {
                        mStrError = String.Format("Error Creating notification message : {0}", JsonError.Message);
                    }

                    if (mStrError == "")
                    {
                        WebRequest webRequest = WebRequest.Create(GlobalVariables.GCMServerUrl);
                        webRequest.Headers.Add(HttpRequestHeader.Authorization, string.Format("Key={0}", GlobalVariables.GCMApiKey));
                        webRequest.ContentType = "application/json";
                        webRequest.Method = "POST";

                        Byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(sJsonMsg);
                        webRequest.ContentLength = byteArray.Length;

                        Stream requestStream = webRequest.GetRequestStream();

                        try
                        {
                            Stream dataStream = webRequest.GetRequestStream();
                            dataStream.Write(byteArray, 0, byteArray.Length);
                            dataStream.Close();

                            WebResponse tResponse = webRequest.GetResponse();

                            dataStream = tResponse.GetResponseStream();

                            StreamReader tReader = new StreamReader(dataStream);

                            String sResponseFromServer = tReader.ReadToEnd();

                            tReader.Close();
                            dataStream.Close();
                            tResponse.Close();

                            string sHttpRes = GlobalFunctions.TryParseHeader(tResponse);

                            GcmMessageResponse _MsgResponse = new GcmMessageResponse(GcmPushMessage);

                            if (sHttpRes == null)
                            {
                                // Can't convert header.
                                if (_MsgResponse.ReadResponse(sResponseFromServer))
                                {
                                    if (_MsgResponse.FailedMessages == 1)
                                        mStrError = _MsgResponse.FailedSendingUserList[Convert.ToString(drOrder["Ord_vCharClntId"])];
                                }
                            }
                            else if (sHttpRes == "OK")
                            {
                                // A valid json response.
                                if (_MsgResponse.ReadResponse(sResponseFromServer))
                                {
                                    if (_MsgResponse.FailedMessages == 1)
                                        mStrError = _MsgResponse.FailedSendingUserList[Convert.ToString(drOrder["Ord_vCharClntId"])];
                                }
                            }
                            else
                            {
                                mStrError = "Failed to read notification response";
                            }
                        }
                        catch (Exception exSender)
                        {
                            mStrError = String.Format("Error occured while sending notification<br/>{0}", exSender.Message);
                        }

                        // *---------------------------------------*
                        // Sending cloud message done.
                    }
                }
                else
                    mStrError = "User device not registered with system";
            }
            else
                mStrError = "No data found for sending notification";

            return mStrError;
        }

        private string GetTableNode(string pStrDescription,object pObjDetail)
        {
            if (Convert.ToString(pObjDetail) == "")
                return "";

            if (pStrDescription != "Order Status")
                return "<tr><td>" + pStrDescription + "</td><td style=\"color:#FFF\">" + Convert.ToString(pObjDetail) + "</td></tr>";
            else
                return "<tr><td>" + pStrDescription + "</td><td><span class=\"status-lbl\">" + Convert.ToString(pObjDetail) + "</span></td></tr>";
        }

        private string EmailCss
        {
            get
            {
                return "<style>"
                     + " .coolTable {"
                     + " 	margin:0px;"
                     + " 	padding:0px;"
                     + " 	"
                     + " 	border:1px solid #ffffff;"
                     + " 	"
                     + " 	-moz-border-radius-bottomleft:0px;"
                     + " 	-webkit-border-bottom-left-radius:0px;"
                     + " 	border-bottom-left-radius:0px;"
                     + " 	"
                     + " 	-moz-border-radius-bottomright:0px;"
                     + " 	-webkit-border-bottom-right-radius:0px;"
                     + "	border-bottom-right-radius:0px;"
                     + "	"
                     + "	-moz-border-radius-topright:0px;"
                     + "	-webkit-border-top-right-radius:0px;"
                     + "	border-top-right-radius:0px;"
                     + "	"
                     + "	-moz-border-radius-topleft:0px;"
                     + "	-webkit-border-top-left-radius:0px;"
                     + "	border-top-left-radius:0px;"
                     + "} .coolTable table{"
                     + "border-collapse: collapse;"
                     + "        border-spacing: 0;"
                     + "	margin:0px;padding:0px;"
                     + "	box-shadow: 10px 10px 5px #888888;"
                     + "} .coolTable tr:last-child td:last-child {"
                     + "	-moz-border-radius-bottomright:0px;"
                     + "	-webkit-border-bottom-right-radius:0px;"
                     + "	border-bottom-right-radius:0px;"
                     + "}"
                     + " .coolTable table tr:first-child td:first-child {"
                     + " 	-moz-border-radius-topleft:0px;"
                     + " 	-webkit-border-top-left-radius:0px;"
                     + " 	border-top-left-radius:0px;"
                     + " }"
                     + " .coolTable table tr:first-child td:last-child {"
                     + " -moz-border-radius-topright:0px;"
                     + " 	-webkit-border-top-right-radius:0px;"
                     + " 	border-top-right-radius:0px;"
                     + " } .coolTable tr:last-child td:first-child{"
                     + " 	-moz-border-radius-bottomleft:0px;"
                     + " 	-webkit-border-bottom-left-radius:0px;"
                     + " 	border-bottom-left-radius:0px;"
                     + " } .coolTable tr:hover td{"
                     + " 	background-color:#cccccc;"
                     + " }"
                     + " .coolTable td{"
                     + " 	vertical-align:middle;"
                     + " 	background-color:#6699cc;"
                     + " 	border:1px solid #ffffff;"
                     + " 	border-width:0px 1px 1px 0px;"
                     + " 	text-align:left;"
                     + " 	padding:7px;"
                     + " 	font-size:14px;"
                     + " 	font-family:Verdana;"
                     + " 	font-weight:normal;"
                     + " 	color:#000000;"
                     + " } .coolTable tr:last-child td{"
                     + " 	border-width:0px 1px 0px 0px;"
                     + " } .coolTable tr td:last-child{"
                     + " 	border-width:0px 0px 1px 0px;"
                     + " } .coolTable tr:last-child td:last-child{"
                     + " 	border-width:0px 0px 0px 0px;"
                     + " }"
                     + "  .coolTable tr:first-child td{"
                     + " 		background:-o-linear-gradient(bottom, #003366 5%, #003f7f 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #003366), color-stop(1, #003f7f) );"
                     + " 	background:-moz-linear-gradient( center top, #003366 5%, #003f7f 100% );"
                     + " 	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr=\"#003366\", endColorstr=\"#003f7f\");	background: -o-linear-gradient(top,#003366,003f7f);"
                     + " "
                     + " 	background-color:#003366;"
                     + " 	border:0px solid #ffffff;"
                     + " 	text-align:center;"
                     + " 	border-width:0px 0px 1px 1px;"
                     + " 	font-size:19px;"
                     + " 	font-family:Times New Roman;"
                     + " 	font-weight:bold;"
                     + " 	color:#ffffff;"
                     + " }"
                     + "  .coolTable tr:first-child:hover td{"
                     + " 	background:-o-linear-gradient(bottom, #003366 5%, #003f7f 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #003366), color-stop(1, #003f7f) );"
                     + " 	background:-moz-linear-gradient( center top, #003366 5%, #003f7f 100% );"
                     + " 	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr=\"#003366\", endColorstr=\"#003f7f\");	background: -o-linear-gradient(top,#003366,003f7f);"
                     + " "
                     + " 	background-color:#003366;"
                     + " }"
                     + "  .coolTable tr:first-child td:first-child{"
                     + " 	border-width:0px 0px 1px 0px;"
                     + " }"
                     + "  .coolTable tr:first-child td:last-child{"
                     + " 	border-width:0px 0px 1px 1px;"
                     + " }"
                     + " "
                     + " .status-lbl{"
                     + " 	display:inline-block;"
                     + " 	color:white;"
                     + " 	font-variant:small-caps;"
                     + " 	text-shadow:1px 1px black;"
                     + " 	font-weight:bold;"
                     + " }"
                     + " "
                     + " </style>";
            }
        }

        #endregion "Class Methods"

        #region "Json Object Classes"

        public class OrderStausUpdateResults 
        {
            public string UpdateStatusGeneralError { get; set; }
            public string UpdateStatusDbError { get; set; }
            public string UpdateStatusEmailError { get; set; }
            public string UpdateStatusNotificationError { get; set; }
        }

        #region "Json Structure"
        /*
           {
                "DealerId":111111111111,
                "DeliveryAgent":{
                                     "DeliveryAgentName":"aa",
                                     "DeliveryTime":"aa",
                                     "DeliveryTrackingId1":"aa",
                                     "DeliveryTrackingId2":"aa",
                                     "DeliveryTrackingId3":"aa",
                                     "DeliveryTrackingId4":"aa",
                                     "DeliveryTrackingId5":"aa",
                                },
                "CancellationReason":"Out of stock orders"
            }
        */
        #endregion "Json Structure"

        public class OrderStausParams
        {
            public long DealerId { get; set; }
            public DeliveryAgent DeliveryAgent { get; set; }
            public string CancellationReason { get; set; }
        }

        public class OrderSub
        {
            public OrderSub(string pStrImgPath,string pStrName,string pStrType,double pDblQty,double pDblAmt,double pDblTotAmt)
            {
                ProductImagePath = pStrImgPath;
                ProductName = pStrName;
                ProductType = pStrType;
                Quantity = pDblQty;
                BasicAmount = pDblAmt;
                TotalAmout = pDblTotAmt;
            }

            public string ProductImagePath { get; set; }
            public string ProductName { get; set; }
            public string ProductType { get; set; }
            public double Quantity { get; set; }
            public double BasicAmount { get; set; }
            public double TotalAmout { get; set; }
        }

        public class OrderDetailsFull
        {
            public OrderDetailsFull()
            {
                Error = "";
                OrderData = null;
            }

            public string Error { get; set; }
            public List<OrderSub> OrderData { get; set; }
        }

        public class Dealer 
        {
            public Dealer(long pLngDealerId,string pStrDealerName)
            {
                DealerId = pLngDealerId;
                DealerName = pStrDealerName;

                // Defa others
                DealerEmail = "";
                DealerPhone = "";
                DealerAddress = "";
            }

            public Dealer(long pLngDealerId, string pStrDealerName, string pStrDealerEmail, string pStrDealerPhone, string pStrDealerAddress)
            {
                DealerId = pLngDealerId;
                DealerName = pStrDealerName;
                DealerEmail = pStrDealerEmail;
                DealerPhone = pStrDealerPhone;
                DealerAddress = pStrDealerAddress;
            }

            public long DealerId { get; set; }
            public string DealerName { get; set; }
            public string DealerEmail { get; set; }
            public string DealerPhone { get; set; }
            public string DealerAddress { get; set; }
        }

        public class DealerData 
        {
            public DealerData()
            {
                Error = "";
                Dealers = null;
            }

            public string Error { get; set; }
            public List<Dealer> Dealers { get; set; }
        }

        public class DeliveryAgent 
        {

            public DeliveryAgent()
            {
                DeliveryAgentName = "";
                DeliveryTime = "";
                DeliveryTrackingId1 = "";
                DeliveryTrackingId2 = "";
                DeliveryTrackingId3 = "";
                DeliveryTrackingId4 = "";
                DeliveryTrackingId5 = "";
            }

            public DeliveryAgent(string pStrName, string pStrTime, string pStrId1)
            {
                DeliveryAgentName = pStrName;
                DeliveryTime = pStrTime;
                DeliveryTrackingId1 = pStrId1;

                DeliveryTrackingId2 = "";
                DeliveryTrackingId3 = "";
                DeliveryTrackingId4 = "";
                DeliveryTrackingId5 = "";
            }

            public DeliveryAgent(string pStrName, string pStrTime, string pStrId1, string pStrId2, string pStrId3, string pStrId4, string pStrId5)
            {
                DeliveryAgentName = pStrName;
                DeliveryTime = pStrTime;
                DeliveryTrackingId1 = pStrId1;
                DeliveryTrackingId2 = pStrId2;
                DeliveryTrackingId3 = pStrId3;
                DeliveryTrackingId4 = pStrId4;
                DeliveryTrackingId5 = pStrId5;
            }

            public string DeliveryAgentName { get; set; }
            public string DeliveryTime { get; set; }
            public string DeliveryTrackingId1 { get; set; }
            public string DeliveryTrackingId2 { get; set; }
            public string DeliveryTrackingId3 { get; set; }
            public string DeliveryTrackingId4 { get; set; }
            public string DeliveryTrackingId5 { get; set; }
        }

        #endregion
    }
}
