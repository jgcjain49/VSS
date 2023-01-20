using FCMClassLibrary;
//using FCMClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_CommTrex
{
    public partial class MStoreInformativeNotification : System.Web.UI.Page
    {
        long CompanyId;
        string ImagePath = "";
        private const string SERVER_KEY = "AAAApfhRAgI:APA91bF2LweOGtgaugLgvHYU32qe_EtLA0cI8IznPs4wXjTm-9fNMFiOQpQT-_aGnYqsSw_SquHuWSmZjL15R3mcryKtrYaXu51NMGH6XDFwbv7TmKtWrxOMohJwcBPRgA1H2w9Daqsw";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshUsers();
                RefreshInformation();
            }
        }
       

        //protected void RefreshUsers()
        //{
        //    if (Session["TalukaDetails"] != null)
        //    {
        //        int mIntTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
        //        string mStrSql = "SELECT CAST([MAU_bIntUserID] as varchar(2500)) As [MAU_bIntUserID] "
        //                       + ",[MAU_vCharDeviceRegId] "
        //                       + ",[MAU_vCharImeiNumber]  "
        //                       + ",[MAU_iNtDeviceType]    "
        //                       + " FROM [Mobile_App_Users_{0}] "
        //                       + " WHERE (MAU_vCharDeviceRegId is not null Or MAU_vCharDeviceRegId <> '') AND MAU_bItIsActive = 1";
        //        mStrSql = String.Format(mStrSql, mIntTalukaId.ToString());
        //        ViewState["AppUsersDetails"] = SqlHelper.ReadTable(mStrSql, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false); ;
        //    }
        //    else
        //    {
        //        Response.Redirect("Home.aspx");
        //    }
        //}
        protected void RefreshUsers()
        {
            if (Session["TalukaDetails"] != null)
            {
                int mIntTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
                string mStrSql = "SELECT CAST([UD_bIntId] as varchar(2500)) As [UD_bIntId] "
                               + ",[UD_nCharDevicePlayerId] "
                               + ",[UD_bItIsActive]    "
                               + " FROM [User_Data_{0}] "
                               + " WHERE (UD_nCharDevicePlayerId is not null Or UD_nCharDevicePlayerId <> '') AND [UD_bItIsActive] = 1";
                mStrSql = String.Format(mStrSql, mIntTalukaId.ToString());
                ViewState["AppUsersDetails"] = SqlHelper.ReadTable(mStrSql, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false); ;
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
        }
        protected void RefreshInformation()
        {
            if (Session["TalukaDetails"] != null)
            {
                int mIntTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
                string mStrSql = "SP_GetInformationDataForNotifications";
                grdInformation.DataSource = SqlHelper.ReadTable(mStrSql, GlobalVariables.SqlConnectionStringMstoreInformativeDb, true,
                                            SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, mIntTalukaId));
                grdInformation.DataBind();

                mStrSql = "dbo.SP_readNotificationDetails";
                grdViewNotifications.DataSource = SqlHelper.ReadTable(mStrSql, GlobalVariables.SqlConnectionStringMstoreInformativeDb, true,
                                            SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, mIntTalukaId));
                grdViewNotifications.DataBind();
            }
            else
                Response.Redirect("Home.aspx"); // Session time out
        }
        public void clear()
        {
            txtNotificationTitle.Text = "";
            txtNotificationText.Text = "";
        }
        //Added by ARV on 11 Oct 18
        private NotificationResult AsyncEventcatch(NotificationResult m, EventArgs e)
        {
            int SuccessCount =0 ;
            int FailedCount = 0;
                for(int i = 0; i<m.SuccessDevideId.Count ; i ++){
                    if(m.SuccessDevideId[i].ToString()!= ""){
                        SuccessCount++; 
                }
                }
                for (int i = 0; i < m.FailureDeviceId.Count; i++)
                {
                    if (m.FailureDeviceId[i].ToString() != "")
                    {
                        FailedCount++;
                    }
                }
                UpdateNotificationData(CompanyId, txtNotificationTitle.Text, txtNotificationText.Text, ImagePath, m.SuccessDevideId.Count, SuccessCount, FailedCount);
            //MessageBox.Show(m.FailureDeviceId[0].ToString() + " with failure reason " + m.ErrorsDetail[0].ToString());
            return m;

            //System.Console.WriteLine("HEARD IT");
        }
        protected void btnSendNotification_ServerClick(object sender, EventArgs e)
        {
            btnSendNotification.Disabled = true;
            Dictionary<string, object> mDicInformation = GetInformationDetails();
            DataTable dtUserData = (DataTable)ViewState["AppUsersDetails"];
            if (dtUserData.Rows.Count == 0)
                UpdateProgress(true, "No user has installed app yet.");
            else if (Convert.ToInt64(mDicInformation["Id"]) == -1)
                UpdateProgress(true, "Select at least a information for which notification is being created!!");
            else if (txtNotificationTitle.Text == "")
                UpdateProgress(true, "Provide notification title!!");
            else if (txtNotificationText.Text == "")
                UpdateProgress(true, "Provide notification text!!");
            else
            {   
                //Added by ARV on 15 Nov 18 for getting IST Time for Sending Date Time in Notification
                DataTable sdatetime = SqlHelper.ReadTable("select dbo.IndianTime() AS [IST_TIME]", GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
                // Save notification to db. On notification sent
                // *---------------------------------------*
                //int mIntNotificationId = Convert.ToInt32(SqlHelper.ReadTable("SP_SaveNewProdLog", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                //                                   SqlHelper.AddInParam("@vCharProdList", SqlDbType.VarChar, mDicSelectionData["ProductIDs"]),
                //                                   SqlHelper.AddInParam("@vCharUserIdList", SqlDbType.VarChar, mDicSelectionData["UserIDs"])
                //                                            ).Rows[0]["PNL_bIntId"]);

                //UpdateProgress(false, "Notification data saved.");
                // *---------------------------------------*

                // Save uploaded file.
                // *---------------------------------------*
                string mStrImagePath = "";
                if (FileNotificationImage.HasFile)
                {
                    mStrImagePath = GetSafeFileNameOnLocation(FileNotificationImage.FileName, GlobalVariables.NotificationImagePath);
                    FileNotificationImage.SaveAs(mStrImagePath);
                    // Make this as respective path now.
                    mStrImagePath = String.Format("{0}/{1}", GlobalVariables.NotificationImagePath, Path.GetFileName(mStrImagePath));
                    ImagePath= String.Format("{0}/{1}", GlobalVariables.NotificationImagePath, Path.GetFileName(mStrImagePath));
                }
                else
                {
                    mStrImagePath = Convert.ToString(mDicInformation["DefaImagePath"]);
                    ImagePath = Convert.ToString(mDicInformation["DefaImagePath"]);
                }
                

                // *---------------------------------------*

                // Sending clound message to devices
                // *---------------------------------------*

                // Create message object.
                //
                //Commented by ARV as GCM is Replaced by FCM Notification
                //GcmMessageRequest GcmPushMessage = new GcmMessageRequest();


                
                // Add Sender
                //
                //GcmPushMessage.SetSender(((SysCompany)Session["SystemCompany"]).CompanyId.ToString(), "System Auto Sender");

                // Set Reciever
                //
                Dictionary<string, string> mDicRecievers = dtUserData.AsEnumerable().ToDictionary<DataRow, string, string>(row => row.Field<string>(0),
                                row => row.Field<string>(1));
                //GcmPushMessage.SetRecieverList(mDicRecievers);
                
                //mDicRecievers.Values.ToList();

                //Added by ARV on 11 Oct 18 
                FCMLib Fcm = new FCMLib(GlobalVariables.FCMServerKey);
                CompanyId = Convert.ToInt64(mDicInformation["Id"]);
                Fcm.OnSendHandler += new FCMLib.NotificationHandler(AsyncEventcatch);
                //Fcm.Multicast(new List<string>() { "dCjqK8DVg6Q:APA91bFnWjSSgm8B1qAlE5TF86m24kcgzY0Xfzhwkvwt6tBWiINot1LjJJu83z7nhk5OsIK8aReZMtL0x0gjOGG3esG73z6RU6vBZAL1CQxKJfu2JEZ1GTYq5elyVjBkt13rvVYWCjES" }, new NotificationMessage() { Title = "FCM Lib test", Body = "FCM BODY", Sender = "Server", ParentID = "Vasai", Extra_Text = new List<string>() { "Extra Text" }, Extra_Numbers = new List<int>() { 1, 2, 3 }, Extra_CustomObj = "cutstom obj", MsgType = enumNotificationType.notification, ExtrasUsedList = "Extra USed List", notificationdate = DateTime.Now });
                //Added Convert.ToDateTime(sdatetime.Rows[0]["IST_TIME"]) & ParentID = CompanyId.ToString() by ARV on 15 Nov 18 as per required by Neelam
                Fcm.Multicast(mDicRecievers.Values.ToList(), new NotificationMessage() { Title = txtNotificationTitle.Text, Body = txtNotificationText.Text, ImageUrl = ImagePath, Sender = "Server", ParentID = CompanyId.ToString(), Extra_Text = new List<string>() { "Extra Text" }, Extra_Numbers = new List<int>() { 1, 2, 3 }, Extra_CustomObj = "cutstom obj", MsgType = enumNotificationType.notification, ExtrasUsedList = "Extra USed List", notificationdate = Convert.ToDateTime(sdatetime.Rows[0]["IST_TIME"]) });

                
                //Added by ARV till here
                // Set Message
                //
                //GcmPushMessage.SetMessage(txtNotificationTitle.Text, Convert.ToInt32(mDicInformation["Id"]), MessageTypes.MSTORE_INFORMATIVE_OFFER);
                //GcmPushMessage.SetMessageOffer(txtNotificationTitle.Text, txtNotificationText.Text.Replace(System.Environment.NewLine, "<br/>"), mStrImagePath);

                // Set Bunch id
                //
                //GcmPushMessage.MessageBunchId = String.Format("Spcl_Prod_Offer_{0}", DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss_tt"));

                //Commented by ARV as GCM is Replaced by FCM Notification
                //>>
                // Set custom date and time
                //
                //GcmPushMessage.MsgDate = DateTime.Now.ToString("yyyy-MM-dd");
                //GcmPushMessage.MsgTime = DateTime.Now.ToString("hh:mm tt");
                
                // Create Json Message
                // 
                //string sJsonMsg = "";

                //try
                //{
                //    sJsonMsg = GcmPushMessage.CreateJsonMsg();
                //    UpdateProgress(false, "Notification message created.");
                //}
                //catch (Exception JsonError)
                //{
                //    UpdateProgress(true, String.Format("Error Creating notification message : {0}", JsonError.Message));
                //    return;
                //}

                //<< Commented by ARV as GCM is Replaced by FCM Notification till here


                //Commented by ARV as GCM is Replaced by FCM Notification
                //>>
                //WebRequest webRequest = WebRequest.Create(GlobalVariables.GCMServerUrl);
                //webRequest.Headers.Add(HttpRequestHeader.Authorization, string.Format("Key={0}", GlobalVariables.GCMApiKey));
                //webRequest.ContentType = "application/json";
                //webRequest.Method = "POST";

                //Byte[] byteArray = Encoding.UTF8.GetBytes(sJsonMsg);
                //webRequest.ContentLength = byteArray.Length;

                //Stream requestStream = webRequest.GetRequestStream();

                //try
                //{
                //    Stream dataStream = webRequest.GetRequestStream();
                //    dataStream.Write(byteArray, 0, byteArray.Length);
                //    dataStream.Close();

                //    UpdateProgress(false, "Notification message sent.");

                //    WebResponse tResponse = webRequest.GetResponse();

                //    dataStream = tResponse.GetResponseStream();

                //    StreamReader tReader = new StreamReader(dataStream);

                //    String sResponseFromServer = tReader.ReadToEnd();

                //    tReader.Close();
                //    dataStream.Close();
                //    tResponse.Close();

                //    string sHttpRes = GlobalFunctions.TryParseHeader(tResponse);

                //    GcmMessageResponse _MsgResponse = new GcmMessageResponse(GcmPushMessage);

                //    UpdateProgress(false, "Reading server response.");

                //    if (sHttpRes == null)
                //    {
                //        // Can't convert header.
                //        if (_MsgResponse.ReadResponse(sResponseFromServer))
                //        {
                //            string mStrResponseCount = "";
                //            if (_MsgResponse.FailedMessages == 0 && _MsgResponse.SuccessMessages > 0)
                //                mStrResponseCount = String.Format("{0} User were notified.", _MsgResponse.SuccessMessages);
                //            else if (_MsgResponse.FailedMessages == 0 && _MsgResponse.SuccessMessages == 0)
                //                mStrResponseCount = "No Users were notified.";
                //            else if (_MsgResponse.FailedMessages > 0 && _MsgResponse.SuccessMessages == 0)
                //                mStrResponseCount = String.Format("Failed to notify {0} users", _MsgResponse.FailedMessages);
                //            else
                //                mStrResponseCount = String.Format("{0} Users were notified and {1}<br/>User's weren't notified", _MsgResponse.SuccessMessages, _MsgResponse.FailedMessages);

                //            UpdateProgress(false, mStrResponseCount);

                //            UpdateNotificationData(Convert.ToInt64(mDicInformation["Id"]), txtNotificationTitle.Text, txtNotificationText.Text, mStrImagePath, dtUserData.Rows.Count, _MsgResponse.SuccessMessages, _MsgResponse.FailedMessages);
                //        }
                //    }
                //    else if (sHttpRes == "OK")
                //    {
                //        // A valid json response.
                //        if (_MsgResponse.ReadResponse(sResponseFromServer))
                //        {
                //            string mStrResponseCount = "";
                //            if (_MsgResponse.FailedMessages == 0 && _MsgResponse.SuccessMessages > 0)
                //                mStrResponseCount = String.Format("{0} User were notified.", _MsgResponse.SuccessMessages);
                //            else if (_MsgResponse.FailedMessages == 0 && _MsgResponse.SuccessMessages == 0)
                //                mStrResponseCount = "No Users were notified.";
                //            else if (_MsgResponse.FailedMessages > 0 && _MsgResponse.SuccessMessages == 0)
                //                mStrResponseCount = String.Format("Failed to notify {0} users", _MsgResponse.FailedMessages);
                //            else
                //                mStrResponseCount = String.Format("{0} Users were notified and {1}<br/>User's weren't notified", _MsgResponse.SuccessMessages, _MsgResponse.FailedMessages);

                //            UpdateProgress(false, mStrResponseCount);

                //            UpdateNotificationData(Convert.ToInt64(mDicInformation["Id"]), txtNotificationTitle.Text, txtNotificationText.Text, mStrImagePath, dtUserData.Rows.Count, _MsgResponse.SuccessMessages, _MsgResponse.FailedMessages);
                //        }
                //    }
                //    else
                //    {
                //        // Invalid json response
                //        // wat to do here ??
                //        //MessageBox.Show("Response From Server : " + sResponseFromServer);
                //    }
                //}
                //catch (Exception exSender)
                //{
                //    //_MdiMainLog.Add(new VTalkLog("Can't convert this resulted json to ur desired result : " + exSender.Message, "PushAckMessage", LogTypes.LogWarnings));
                //}
                //<< Commented by ARV as GCM is Replaced by FCM Notification till here

                // *---------------------------------------*
                // Sending cloud message done.
            }

            btnSendNotification.Disabled = false;
        }

        private void UpdateNotificationData(long pLngInfoId, string pStrTitle, string pStrText,
            string pStrImgPath, int pIntTotalUsers, int pIntSentCnt, int pIntFailedCnt)
        {
            try
            {
                Dictionary<string, object> mDicInformation = GetInformationDetails();
                if (Session["TalukaDetails"] != null)
                {
                    int mIntTalukaId = ((TalukaData)Session["TalukaDetails"]).TalukaID;
                    SqlHelper.ReadTable("SP_SaveNotification", GlobalVariables.SqlConnectionStringMstoreInformativeDb, true,
                              SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, mIntTalukaId),
                              SqlHelper.AddInParam("@bIntInfoId", SqlDbType.BigInt, pLngInfoId),
                              SqlHelper.AddInParam("@nVarTitle", SqlDbType.NVarChar, pStrTitle),
                              SqlHelper.AddInParam("@nVarText", SqlDbType.NVarChar, pStrText),
                              SqlHelper.AddInParam("@vCharImgPath", SqlDbType.VarChar, pStrImgPath),
                              SqlHelper.AddInParam("@dtSendTime", SqlDbType.DateTime, DateTime.Now),
                              SqlHelper.AddInParam("@iNtTotalUsers", SqlDbType.Int, pIntTotalUsers),
                              SqlHelper.AddInParam("@iNtSentCnt", SqlDbType.Int, pIntSentCnt),
                              SqlHelper.AddInParam("@iNtFailedCnt", SqlDbType.Int, pIntFailedCnt));

                    long lngCompanyId = Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId);
                    GlobalFunctions.saveInsertUserAction("Notification_Master", "[Advertisement Master Insert]:Insertion of Advertisements Notification with Title : " + pStrTitle + ", Description :" + pStrText + " For " + Convert.ToString(mDicInformation["Name"]) + " Information.,Image :" + pStrImgPath + ",Count of Total Users : " + pIntTotalUsers + ",Count of Sent Users : " + pIntSentCnt + ",Count of Failed Users : " + pIntFailedCnt, mIntTalukaId, lngCompanyId, Request); //Call to user Action Log
                    progInfo.Attributes["class"] = "alert alert-info";
                    progInfo.InnerHtml = "Notification Send Successfully!!!";
                    RefreshInformation();
                    clear();
                }
                else
                    Response.Redirect("Home.aspx");
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

        private void UpdateProgress(bool pBlnIsError, string pStrProgressText)
        {
            progInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            progInfo.InnerHtml = pStrProgressText;
        }

        private Dictionary<string, object> GetInformationDetails()
        {
            Dictionary<string, object> mDicSelectedInformation = new Dictionary<string, object>();
            mDicSelectedInformation["Id"] = -1;
            mDicSelectedInformation["DefaImagePath"] = "";
            mDicSelectedInformation["Name"] = "";//added by SSK Dated 29-07-2015
            foreach (GridViewRow mGvrInfoDetail in grdInformation.Rows)
            {
                if (((CheckBox)mGvrInfoDetail.FindControl("cbSelectInformation")).Checked)
                {
                    mDicSelectedInformation["Id"] = Convert.ToInt64(mGvrInfoDetail.Cells[5].Text);
                    mDicSelectedInformation["DefaImagePath"] = Convert.ToString(mGvrInfoDetail.Cells[6].Text);
                    mDicSelectedInformation["Name"] = Convert.ToString(mGvrInfoDetail.Cells[1].Text);//added by SSK Dated 29-07-2015
                    break;
                }
            }

            return mDicSelectedInformation;
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
    }
}