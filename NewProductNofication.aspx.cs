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
    public partial class NewProductNofication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshUsers();
                RefreshProducts();
            }
        }

        protected void RefreshUsers()
        {
            if (Session["SystemUser"] != null)
            {
                SystemUser mObjCurrUser = (SystemUser)Session["SystemUser"];
                string mStrQuery = "Select Cast(0 As Bit) As [isUserSelected],Usr_Name,vwUsr_RoleTxt,UCon_nVcharAddressPrimary,Usr_Id,Usr_DeviceRegId";
                mStrQuery += "  From vwUserHistory Where Usr_Id <> @id Order By vwUsr_Role,Usr_Name";
                grdUsers.DataSource = SqlHelper.ReadTable(mStrQuery,
                                        Convert.ToString(Session["SystemUserSqlConnectionString"]),
                                        false,
                                        SqlHelper.AddInParam("@id", System.Data.SqlDbType.VarChar, mObjCurrUser.UserSysId));
                grdUsers.DataBind();
                grdUsers.Columns[4].Visible = grdUsers.Columns[5].Visible = false;
            }
            else
                Response.Redirect("Default.aspx"); // Session time out
        }

        protected void RefreshProducts()
        {
            if (Session["SystemUser"] != null)
            {
                SystemUser mObjCurrUser = (SystemUser)Session["SystemUser"];
                string mStrQuery = "Select PM_vCharProdName,PM_vCharProdType,PM_decProdPrice,";
                mStrQuery += "PM_vCharProdDesc,vwvCharProdStatus,PM_bIntProdId";
                mStrQuery += "  From vwProductDetailsFull";
                grdProducts.DataSource = SqlHelper.ReadTable(mStrQuery,Convert.ToString(Session["SystemUserSqlConnectionString"]),false);
                grdProducts.DataBind();
                grdProducts.Columns[6].Visible = false;
            }
            else
                Response.Redirect("Default.aspx"); // Session time out
        }

        protected void btnSendNotification_ServerClick(object sender, EventArgs e)
        {
            btnSendNotification.Disabled = true;
            Dictionary<string, object> mDicSelectionData = GetSelectionData();
            if (Convert.ToString(mDicSelectionData["UserIDs"]) == "")
                UpdateProgress(true, "Select at least a user!!");
            else if (Convert.ToInt32(mDicSelectionData["ProductIdCnt"]) == 0)
                UpdateProgress(true, "Select at least a product!!");
            else
            {
                UpdateProgress(false, "Notification data to be sent collected.");

                // Save notification to db.
                // *---------------------------------------*
                int mIntNotificationId = Convert.ToInt32(SqlHelper.ReadTable("SP_SaveNewProdLog", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                   SqlHelper.AddInParam("@vCharProdList", SqlDbType.VarChar, mDicSelectionData["ProductIDs"]),
                                                   SqlHelper.AddInParam("@vCharUserIdList", SqlDbType.VarChar, mDicSelectionData["UserIDs"])
                                                            ).Rows[0]["PNL_bIntId"]);

                UpdateProgress(false, "Notification data saved.");
                // *---------------------------------------*

                // Sending clound message to devices
                // *---------------------------------------*
                
                // Create message object.
                //
                GcmMessageRequest GcmPushMessage = new GcmMessageRequest();
                
                // Add Sender
                //
                GcmPushMessage.SetSender(((SystemUser)Session["SystemUser"]).UserSysId, "System Auto Sender");

                // Set Reciever
                //
                GcmPushMessage.SetRecieverList((Dictionary<string, string>)mDicSelectionData["NotificationReciever"]);

                // Set Message
                //
                GcmPushMessage.SetMessage(String.Format("{0} new products are avaliable", mDicSelectionData["ProductIdCnt"]), mIntNotificationId, MessageTypes.PRODUCT_NOTIFICATIONS);

                // Set Bunch id
                //
                GcmPushMessage.MessageBunchId = String.Format("Spcl_Prod_Notific_{0}", DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss_tt"));

                // Create Json Message
                // 
                string sJsonMsg = "";

                try
                {
                    sJsonMsg = GcmPushMessage.CreateJsonMsg();
                    UpdateProgress(false, "Notification message created.");
                }
                catch (Exception JsonError)
                {
                    UpdateProgress(true, String.Format("Error Creating notification message : {0}", JsonError.Message));
                    return;
                }

                WebRequest webRequest = WebRequest.Create(GlobalVariables.GCMServerUrl);
                webRequest.Headers.Add(HttpRequestHeader.Authorization, string.Format("Key={0}", GlobalVariables.GCMApiKey));
                webRequest.ContentType = "application/json";
                webRequest.Method = "POST";

                Byte[] byteArray = Encoding.UTF8.GetBytes(sJsonMsg);
                webRequest.ContentLength = byteArray.Length;

                Stream requestStream = webRequest.GetRequestStream();

                try
                {
                    Stream dataStream = webRequest.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();

                    UpdateProgress(false, "Notification message sent.");

                    WebResponse tResponse = webRequest.GetResponse();

                    dataStream = tResponse.GetResponseStream();

                    StreamReader tReader = new StreamReader(dataStream);

                    String sResponseFromServer = tReader.ReadToEnd();

                    tReader.Close();
                    dataStream.Close();
                    tResponse.Close();

                    string sHttpRes = GlobalFunctions.TryParseHeader(tResponse);

                    GcmMessageResponse _MsgResponse = new GcmMessageResponse(GcmPushMessage);

                    UpdateProgress(false, "Reading server response.");

                    if (sHttpRes == null)
                    {
                        // Can't convert header.
                        if (_MsgResponse.ReadResponse(sResponseFromServer))
                        {
                            string mStrResponseCount = "";
                            if (_MsgResponse.FailedMessages == 0 && _MsgResponse.SuccessMessages > 0)
                                mStrResponseCount = String.Format("{0} User were notified.", _MsgResponse.SuccessMessages);
                            else if (_MsgResponse.FailedMessages == 0 && _MsgResponse.SuccessMessages == 0)
                                mStrResponseCount = "No Users were notified.";
                            else if (_MsgResponse.FailedMessages > 0 && _MsgResponse.SuccessMessages == 0)
                                mStrResponseCount = String.Format("Failed to notify {0} users", _MsgResponse.FailedMessages);
                            else
                                mStrResponseCount = String.Format("{0} Users were notified and {1}<br/>User's weren't notified", _MsgResponse.SuccessMessages, _MsgResponse.FailedMessages);
                            
                            UpdateProgress(false,mStrResponseCount);
                        }
                    }
                    else if (sHttpRes == "OK")
                    {
                        // A valid json response.
                        if (_MsgResponse.ReadResponse(sResponseFromServer))
                        {
                            string mStrResponseCount = "";
                            if (_MsgResponse.FailedMessages == 0 && _MsgResponse.SuccessMessages > 0)
                                mStrResponseCount = String.Format("{0} User were notified.", _MsgResponse.SuccessMessages);
                            else if (_MsgResponse.FailedMessages == 0 && _MsgResponse.SuccessMessages == 0)
                                mStrResponseCount = "No Users were notified.";
                            else if (_MsgResponse.FailedMessages > 0 && _MsgResponse.SuccessMessages == 0)
                                mStrResponseCount = String.Format("Failed to notify {0} users", _MsgResponse.FailedMessages);
                            else
                                mStrResponseCount = String.Format("{0} Users were notified and {1}<br/>User's weren't notified", _MsgResponse.SuccessMessages, _MsgResponse.FailedMessages);

                            UpdateProgress(false, mStrResponseCount);
                        }
                    }
                    else
                    {
                        // Invalid json response
                        // wat to do here ??
                        //MessageBox.Show("Response From Server : " + sResponseFromServer);
                    }
                }
                catch (Exception exSender)
                {
                    //_MdiMainLog.Add(new VTalkLog("Can't convert this resulted json to ur desired result : " + exSender.Message, "PushAckMessage", LogTypes.LogWarnings));
                }

                // *---------------------------------------*
                // Sending cloud message done.
            }

            btnSendNotification.Disabled = false;
        }

        private void UpdateProgress(bool pBlnIsError, string pStrProgressText)
        {
            progInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            progInfo.InnerHtml = pStrProgressText;
        }

        private Dictionary<string,object> GetSelectionData()
        {
            Dictionary<string, object> mDicData = new Dictionary<string, object>();

            string mStrTemp = "";
            Dictionary<string,string> mDicRecievers = new Dictionary<string,string>();

            foreach(GridViewRow mGvrUserDetail in grdUsers.Rows)
            {
                if (((CheckBox)mGvrUserDetail.FindControl("cbSelectUser")).Checked)
                {
                    mDicRecievers.Add(mGvrUserDetail.Cells[4].Text, mGvrUserDetail.Cells[5].Text);
                    mStrTemp += String.Format("'{0}'", mGvrUserDetail.Cells[4].Text) + ",";
                }
            }

            if (mStrTemp.EndsWith(","))
                mStrTemp = mStrTemp.Remove(mStrTemp.LastIndexOf(","), 1);

            mDicData.Add("UserIDs", mStrTemp);
            mDicData.Add("NotificationReciever", mDicRecievers);

            int mIntCnt = 0;
            mStrTemp = "";
            foreach (GridViewRow mGvrProductDetail in grdProducts.Rows)
            {
                if (((CheckBox)mGvrProductDetail.FindControl("cbSelectProduct")).Checked)
                {
                    mStrTemp += mGvrProductDetail.Cells[6].Text + ",";
                    mIntCnt++;
                }
            }

            if (mStrTemp.EndsWith(","))
                mStrTemp = mStrTemp.Remove(mStrTemp.LastIndexOf(","), 1);

            mDicData.Add("ProductIDs", mStrTemp);
            mDicData.Add("ProductIdCnt", mIntCnt);

            return mDicData;
        }
    }
}