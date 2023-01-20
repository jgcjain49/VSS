using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Admin_CommTrex.MStore_Informative_AppServices
{
    /// <summary>
    /// Summary description for MStoreOrderingService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MStoreEnquiryService : System.Web.Services.WebService
    {
        [WebMethod] // This method can be accessed over internet.
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void PlaceEnquiryMail(long pLngTalukaId, string pStrClientId, string pStrProductIdList, string pStrClientName, string pStrClientAdd1, string pStrClientAdd2, long pLngClientPhno,string pStrClientEmailId,long pLngClientPinCode, string pStrEnquiry, string pStrRemark, string pStrMethodAuthKey)
        {
            EnquiryResult mObjEnqResult = new EnquiryResult();
            try
            {                
                string mStrConnectionStringToUse = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                //ProductIdList mobjEnquiryProducts = JsonConvert.DeserializeObject<ProductIdList>(pStrProductIdList);
     
                    if (pStrMethodAuthKey == "jkbajirao%^@$gvne2671mastaniIOsemohabat12#ki23#Hai" && mStrConnectionStringToUse != "")
                    {
                        #region "Old commented code"
                        //foreach (EnquiryProductIdList epl in mobjEnquiryProducts.EnquiryProductIdLists)
                        //{
                        //    string mStrSelectQuery = " SELECT IM.IM_vCharInfoName_En,IM.IM_vCharInfoAdd_En,IM.IM_vCharInfoEmail,IM.IM_vCharInfoPhone1";
                        //    mStrSelectQuery += String.Format("   FROM Information_Master_{0} IM INNER JOIN Product_Categories_{1}", pLngTalukaId, pLngTalukaId);
                        //    mStrSelectQuery += String.Format("   ON PC_bIntInformationId=IM.IM_bIntInfoId INNER JOIN Product_Sub_Categories_{0}", pLngTalukaId);
                        //    mStrSelectQuery += String.Format("   ON PSC_bIntCategoryId = PC_bIntCategoryId INNER JOIN Product_Master_{0}", pLngTalukaId);
                        //    mStrSelectQuery += " ON PSC_bIntSubCategoryId = PM_bIntSubCatId ";
                        //    mStrSelectQuery += String.Format(" WHERE PM_bIntProdId={0} ", epl.lngProductId);

                        //    DataTable dtInformationDetails = SqlHelper.ReadTable(mStrSelectQuery, false);

                        //    if (dtInformationDetails.Rows.Count > 0)
                        //    {
                        //        string ReceiverNm = Convert.ToString(dtInformationDetails.Rows[0]["IM_vCharInfoName_En"]);
                        //        string ReceiverAdd = Convert.ToString(dtInformationDetails.Rows[0]["IM_vCharInfoAdd_En"]);
                        //        string ReceiverEmailid = Convert.ToString(dtInformationDetails.Rows[0]["IM_vCharInfoEmail"]);
                        //        long lngPhoneNo = Convert.ToInt64(dtInformationDetails.Rows[0]["IM_vCharInfoPhone1"]);

                        //        string mStrInsertQuery = " INSERT INTO Enquiry_Log (EM_bIntTalukaID,EM_vcharClientId,EM_vcharProductId,EM_dtEnquiryTime,EM_vCharEnquiryText,EM_vCharEnquiryRemark";
                        //        mStrInsertQuery += ",EM_nvCharReceiverName,EM_nvCharReceiverAdd,EM_nvCharReceiverPhNo,EM_vCharReceiverEmailId,EM_intEmailStatus)";
                        //        mStrInsertQuery += " Output inserted.EM_bIntId ";
                        //        mStrInsertQuery += " Values(@TalukaId,@ClientId,@ProductId,CAST(SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30') As DATETIME),@EnquiryText,@EnquiryRemark,";
                        //        mStrInsertQuery += " @ReceiverName,@ReceiverAddress,@ReceiverPhNo,@ReceiverEmailId,1)";

                        //        DataTable dtInsertEmailLog = SqlHelper.ReadTable(mStrInsertQuery, false,
                        //            SqlHelper.AddInParam("@TalukaId", SqlDbType.BigInt, pLngTalukaId),
                        //            SqlHelper.AddInParam("@ClientId", SqlDbType.VarChar, pStrClientId),
                        //            SqlHelper.AddInParam("@ProductId", SqlDbType.BigInt, epl.lngProductId),
                        //            SqlHelper.AddInParam("@EnquiryText", SqlDbType.VarChar, pStrEnquiry),
                        //            SqlHelper.AddInParam("@EnquiryRemark", SqlDbType.VarChar, pStrRemark),
                        //            SqlHelper.AddInParam("@ReceiverName", SqlDbType.NVarChar, ReceiverNm),
                        //            SqlHelper.AddInParam("@ReceiverAddress", SqlDbType.NVarChar, ReceiverAdd),
                        //            SqlHelper.AddInParam("@ReceiverPhNo", SqlDbType.NVarChar, lngPhoneNo),
                        //            SqlHelper.AddInParam("@ReceiverEmailId", SqlDbType.VarChar, ReceiverEmailid));

                        //        if (dtInsertEmailLog.Rows.Count > 0)
                        //        {
                        //            mObjSubmissionDetails.EnquiryEmailId = Convert.ToInt64(dtInsertEmailLog.Rows[0]["EM_bIntId"]);
                        //            mObjSubmissionDetails.EnquiryText = pStrEnquiry;
                        //            mObjSubmissionDetails.EnquiryRem = pStrRemark;
                        //            mObjSubmissionDetails.RecName = ReceiverNm;
                        //            mObjSubmissionDetails.ProductId = epl.lngProductId;
                        //            //mObjSubmissionDetails.RecAdd = ReceiverAdd;
                        //            SendDelegatedMails(pLngTalukaId, mObjSubmissionDetails.EnquiryEmailId, mObjSubmissionDetails.ProductId, mObjSubmissionDetails.EnquiryText, mObjSubmissionDetails.EnquiryRem, mObjSubmissionDetails.RecName);
                        //        }

                        //        //string str
                        //    }
                        //}
                        #endregion "Old commented code"

                        if (pStrClientAdd2.ToLower().Trim() == "same as above")
                            pStrClientAdd2 = pStrClientAdd1;

                        // 1. Save log about enquiry.
                        DataTable dtEnqLog = SqlHelper.ReadTable("SP_SaveEnquiryLog", mStrConnectionStringToUse, true,
                                                       SqlHelper.AddInParam("@bIntEnqLogId", SqlDbType.BigInt, -1),
                                                       SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, pLngTalukaId),
                                                       SqlHelper.AddInParam("@vCharClientId", SqlDbType.VarChar, pStrClientId),
                                                       SqlHelper.AddInParam("@vCharProdList", SqlDbType.VarChar, pStrProductIdList),
                                                       SqlHelper.AddInParam("@nVarEnqTxt", SqlDbType.NVarChar, pStrEnquiry),
                                                       SqlHelper.AddInParam("@nVarEnqRmk", SqlDbType.NVarChar, pStrRemark),
                                                       SqlHelper.AddInParam("@vCharSendId", SqlDbType.VarChar, null),
                                                       SqlHelper.AddInParam("@vCharRecievers", SqlDbType.VarChar, null),
                                                       SqlHelper.AddInParam("@vCharClientName", SqlDbType.VarChar, pStrClientName),
                                                       SqlHelper.AddInParam("@vCharClientAdd1", SqlDbType.VarChar, pStrClientAdd1),
                                                       SqlHelper.AddInParam("@vCharClientAdd2", SqlDbType.VarChar, pStrClientAdd2),
                                                       SqlHelper.AddInParam("@bIntClientPhNo", SqlDbType.BigInt, pLngClientPhno),
                                                       SqlHelper.AddInParam("@bIntPinCode", SqlDbType.BigInt, pLngClientPinCode),
                                                       SqlHelper.AddInParam("@vCharClientEmail", SqlDbType.VarChar, pStrClientEmailId),
                                                       SqlHelper.AddInParam("@iNtEmailStatus", SqlDbType.Int, 0),
                                                       SqlHelper.AddInParam("@iNtSent", SqlDbType.Int, -1),
                                                       SqlHelper.AddInParam("@iNtFail", SqlDbType.Int, -1),
                                                       SqlHelper.AddInParam("@iNtTotal", SqlDbType.Int, -1),
                                                       SqlHelper.AddInParam("@vCharError", SqlDbType.VarChar, null),
                                                       SqlHelper.AddInParam("@bItIsError", SqlDbType.Bit, false));
                        if (dtEnqLog.Rows.Count > 0)
                        {
                            long mLngEnqId = Convert.ToInt64(dtEnqLog.Rows[0]["EM_bIntId"]);
                            
                            // 2. Send delegated mail.
                            SendDelegatedMails(mLngEnqId);

                            // 3. Respond with enquiry id.
                            mObjEnqResult.Error = null;
                            mObjEnqResult.EnquiryId = mLngEnqId;
                        }
                        else
                            mObjEnqResult.Error = new JsonError(true, "No data found", -101);
                    }  
                    else
                    {
                        // unauthorized access to method.
                        mObjEnqResult.Error = new JsonError("You are not authorized to access SERVER 1.0");
                    }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("PlaceEnquiryMail", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjEnqResult.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjEnqResult, this.Context.Response);
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void ReceiveEnquiryProducts(long pLngTalukaId, string pStrEnquiryProuductsList, string pStrMethodAuthKey, bool pblnIsRegional)
        {
             ProductsSearchResult mObjResult = new ProductsSearchResult();
            try
            {
                if (pStrMethodAuthKey == "23hum552%tum$@!#ek*&^Kamre^%mein82((321band323ho")
                {
                    if (pStrEnquiryProuductsList.Trim() == "")
                        pStrEnquiryProuductsList = "-1";

                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataSet dsSearchResult = SqlHelper.ReadDataSet("SP_SearchProductsById", mStrConnection, true, true,
                                                          SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId),
                                                          SqlHelper.AddInParam("@nVarSeenProdList", SqlDbType.NVarChar, pStrEnquiryProuductsList),
                                                          SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pblnIsRegional)
                                                          );
                    if (dsSearchResult == null)
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    else if (dsSearchResult.Tables[dsSearchResult.Tables.Count - 1].Rows.Count > 0)
                    {
                        mObjResult.Error = null;
                        if (dsSearchResult.Tables.Count > 0)
                        {
                            mObjResult.FoundInformationsCount = Convert.ToInt64(dsSearchResult.Tables[0].Rows.Count);
                            mObjResult.FoundProductsCount = Convert.ToInt64(dsSearchResult.Tables[0].Rows.Count);
                        }

                        List<ProductMinDtls> mLstProducts = new List<ProductMinDtls>();
                        foreach (DataRow drProduct in dsSearchResult.Tables[dsSearchResult.Tables.Count - 1].Rows)
                        {
                            mLstProducts.Add(new ProductMinDtls(Convert.ToInt64(drProduct["PM_bIntProdId"]),
                                                                Convert.ToInt64(drProduct["IM_bIntInfoId"]),
                                                                Convert.ToString(drProduct["IM_vCharInfoName_En"]),
                                                                Convert.ToString(drProduct["PM_vCharProdName"]),
                                                                Convert.ToString(drProduct["PC_vCharCatName"]),
                                                                Convert.ToString(drProduct["PSC_vCharSubCatName"]),
                                                                Convert.ToDouble(drProduct["vw_decProdPrice"]),
                                                                Convert.ToString(drProduct["PI_vCharImgPath"])));
                        }
                        mObjResult.FoundProducts = mLstProducts;
                    }
                    else
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                }
                else
                {
                    mObjResult.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("SearchProductsBasic", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjResult.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjResult, this.Context.Response);
    }


        #region "Email Methods"
        //To send delgate mails
        public void SendDelegatedMails(long pLngEnquiryId)
        {
            AsyncMethodCaller mDeleObj = new AsyncMethodCaller(SendMailInSafeMachineThread);
            AsyncCallback mObjMailMethodDone = new AsyncCallback(AsyncResourceRelease);
            mDeleObj.BeginInvoke(pLngEnquiryId, mObjMailMethodDone, null);
        }

        private delegate void AsyncMethodCaller(long pLngEnquiryId);

         //Method that works as different machine thread.
        //If gmail denies mail sending :  https://g.co/allowaccess 
        private void SendMailInSafeMachineThread(long pLngEnquiryId)
        {
            try
            {
                DataSet dsEmailDetails = SqlHelper.ReadDataSet("SP_ModifyEnquiryLog", GlobalVariables.SqlConnectionStringMstoreInformativeDb, true, true, SqlHelper.AddInParam("@bintEnquiryId", SqlDbType.BigInt, pLngEnquiryId), SqlHelper.AddInParam("@nVarExtraColsList", SqlDbType.NVarChar, "COALESCE(CAST(DECRYPTBYPASSPHRASE('*#_140351_HS_029081_75396_#*',EC_vCharSenderPassword) as varchar(Max)),'') As [vwSenerPass]"));
                string strEmailIdList = "No Senders ";

                if (dsEmailDetails != null)
                {
                    //Mail body creation begin
                    //-------------------------------------------------------------
                    string mStrCommonTopTable;
                    mStrCommonTopTable = "<table style=\"border: 2px solid rgba(204, 204, 204, 1);border-collapse: collapse;border-top-left-radius: 8px;border-top-right-radius: 8px;overflow: hidden;\">";
                    mStrCommonTopTable += "    <tr>";
                    mStrCommonTopTable += "        <td colspan=\"2\" style=\"background-color: rgba(204, 204, 204, 0.8);padding: 3px;font-weight: bold;font-family: Georgia, 'Times New Roman', Times, serif;\">Basic Details</td>";
                    mStrCommonTopTable += "    </tr>";
                    mStrCommonTopTable += "    <tr>";
                    mStrCommonTopTable += "        <td style=\"border: 1px solid rgba(204, 204, 204, 1);font-size: small;padding: 5px;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif;\">Enquiry Details</td>";
                    mStrCommonTopTable += "        <td style=\"font-family: Georgia, 'Times New Roman', Times, serif;border-bottom: 1px solid rgba(204, 204, 204, 1);\">Enquiry Number : <span style=\"color: rgba(0, 102, 255, 1)\">" + pLngEnquiryId + "</span>";
                    mStrCommonTopTable += "            <br/>";
                    mStrCommonTopTable += "            Enquiry Date : <span style=\"color: rgba(0, 102, 255, 1)\">" + Convert.ToDateTime(dsEmailDetails.Tables[0].Rows[0]["EM_dtEnquiryTime"]).ToString("dd-MMM-yyyy") + "</span>";
                    mStrCommonTopTable += "            <br/>";
                    mStrCommonTopTable += "            Enquiry Time : <span style=\"color: rgba(0, 102, 255, 1)\">" + Convert.ToDateTime(dsEmailDetails.Tables[0].Rows[0]["EM_dtEnquiryTime"]).ToString("hh:mm:ss tt") + "</span>";
                    mStrCommonTopTable += "        </td>";
                    mStrCommonTopTable += "    </tr>";
                    mStrCommonTopTable += "    <tr style=\"background-color:rgba(204,204,204,0.5);\">";
                    mStrCommonTopTable += "        <td style=\"border: 1px solid rgba(204, 204, 204, 1);font-size: small;padding: 5px;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif;\">Client Contact Details</td>";
                    mStrCommonTopTable += "        <td style=\"font-family: Georgia, 'Times New Roman', Times, serif;\">Contact Person : <span style=\"color: rgba(0, 102, 255, 1)\">" + Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_vCharClientName"]) + "</span>";
                    mStrCommonTopTable += "	        <br/>";
                    mStrCommonTopTable += "            Phone : <span cstyle=\"color: rgba(0, 102, 255, 1)\">" + Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_bIntClientPhNo"]) + "</span>";
                    mStrCommonTopTable += "            <br/>";
                    mStrCommonTopTable += "            Email : <span style=\"color: rgba(0, 102, 255, 1)\">" + Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_vCharClientEmailId"]) + "</span>";
                    mStrCommonTopTable += "        </td>";
                    mStrCommonTopTable += "    </tr>";
                    mStrCommonTopTable += "    <tr>";
                    mStrCommonTopTable += "        <td style=\"border: 1px solid rgba(204, 204, 204, 1);font-size: small;padding: 5px;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif;\">";
                    mStrCommonTopTable += "	        Primary Address : ";
                    mStrCommonTopTable += "            <br/>";
                    mStrCommonTopTable += Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_vCharClientAddress1"]).Replace(",", ",<br/>");
                    mStrCommonTopTable += "        </td>";
                    mStrCommonTopTable += "        <td style=\"border: 1px solid rgba(204, 204, 204, 1);font-size: small;padding: 5px;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif;\">";
                    mStrCommonTopTable += "	        Secondary Address : ";
                    mStrCommonTopTable += "            <br/>";
                    mStrCommonTopTable += Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_vCharClientAddress2"]).Replace(",", ",<br/>");
                    mStrCommonTopTable += "        </td>";
                    mStrCommonTopTable += "    </tr>";
                    mStrCommonTopTable += "</table>";


                    // Create common mailing objects
                    SmtpClient mObjSmtpClnt = new SmtpClient(Convert.ToString(dsEmailDetails.Tables[1].Rows[0]["EC_vCharEmailSmtpHost"]),
                                                             Convert.ToInt32(dsEmailDetails.Tables[1].Rows[0]["EC_iNtPort"]));

                    mObjSmtpClnt.Credentials = new NetworkCredential(Convert.ToString(dsEmailDetails.Tables[1].Rows[0]["EC_vCharSenderEmailId"]),
                                                                     Convert.ToString(dsEmailDetails.Tables[1].Rows[0]["vwSenerPass"]));
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
                    int intTotCount=dtInformations.Rows.Count; 
                    int intFailureCount = 0;
                    string errMsg = "No Error";
                    int intEmailStatus = 1;

                    foreach (DataRow drInfo in dtInformations.Rows)
                    {
                        try{
                         drArrSearchResult = dsEmailDetails.Tables[2].Select("IM_bIntInfoId = " + Convert.ToString(drInfo["IM_bIntInfoId"]));
                         string mStrInfoName = "";
                         
                        //Code to create  enquiry summary details with product listings
                        if (drArrSearchResult.Length > 0)
                         {
                             mStrInfoName = Convert.ToString(drArrSearchResult[0]["IM_vCharInfoName_En"]);
                             mStrInnerOrderInfoHtml = "<table style=\"border: 2px solid rgba(204, 204, 204, 1);border-collapse: collapse;border-top-left-radius: 8px;border-top-right-radius: 8px;overflow: hidden;\">";
                             mStrInnerOrderInfoHtml += "    <tr>";
                             mStrInnerOrderInfoHtml += "        <td colspan=\"5\" style=\"background-color: rgba(204, 204, 204, 0.8);padding: 3px;font-weight: bold;font-family: Georgia, 'Times New Roman', Times, serif;\">Enquiry Summary</td>";
                             mStrInnerOrderInfoHtml += "    </tr>";
                             mStrInnerOrderInfoHtml += "    <tr>";
                             mStrInnerOrderInfoHtml += "        <td colspan=\"2\" style=\"background-color: rgba(204, 204, 204, 0.8);padding: 2px !important;font-style: italic;border: 1px solid rgba(204, 204, 204, 1);font-size: small;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif;\">List of Products </td>";
                             mStrInnerOrderInfoHtml += "        <td style=\"background-color: rgba(204, 204, 204, 0.8);padding: 2px !important;font-style: italic;border: 1px solid rgba(204, 204, 204, 1);font-size: small;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif;\">Amount</td>";
                             mStrInnerOrderInfoHtml += "    </tr>";
                         }

                        string mStrOrderList = "";

                        for (int iRowCnt = 0; iRowCnt < drArrSearchResult.Length; iRowCnt++)
                        {
                            mStrOrderList += Convert.ToString(drArrSearchResult[iRowCnt]["PM_bIntProdId"]) + ",";
                            if (iRowCnt % 2 == 0)
                                mStrInnerOrderInfoHtml += "    <tr>";
                            else
                                mStrInnerOrderInfoHtml += "    <tr style=\"background-color:rgba(204,204,204,0.5);\">";
                            if (Convert.ToString(drArrSearchResult[iRowCnt]["PI_vCharImgPath"]) == "")
                                mStrInnerOrderInfoHtml += "        <td style=\"font-size: small;padding: 5px;border-bottom:1px solid rgba(204,204,204,1)\"><img style=\"width: 100%;max-width: 256px;height: auto\" src=\"" + "http://connect.mstoreindia.com/" + "images/logo.png" + "\" /></td>";
                            else
                                mStrInnerOrderInfoHtml += "        <td style=\"font-size: small;padding: 5px;border-bottom:1px solid rgba(204,204,204,1)\"><img style=\"width: 100%;max-width: 256px;height: auto\" src=\"" + "http://connect.mstoreindia.com/" + Convert.ToString(drArrSearchResult[iRowCnt]["PI_vCharImgPath"]) + "\" /></td>";
                            mStrInnerOrderInfoHtml += "        <td style=\"font-size: small;padding: 5px;border-bottom:1px solid rgba(204,204,204,1);font-family: Georgia, 'Times New Roman', Times, serif;\">" + Convert.ToString(drArrSearchResult[iRowCnt]["PM_vCharProdName"]) + "</td>";
                            mStrInnerOrderInfoHtml += "        <td style=\"border: 1px solid rgba(204, 204, 204, 1);font-size: small;padding: 5px;line-height: 1.5em;font-family: Georgia, 'Times New Roman', Times, serif;\">Rs. " + Convert.ToDecimal(drArrSearchResult[iRowCnt]["PM_decActualPrice"]).ToString("N2") + "</td>";
                            mStrInnerOrderInfoHtml += "    </tr>";
                        }

                        if (mStrOrderList.EndsWith(","))
                            mStrOrderList = mStrOrderList.Remove(mStrOrderList.LastIndexOf(","), 1);

                        mStrInnerOrderInfoHtml += "</table>";

                        mStrInnerOrderInfoHtml += "<table style=\"border: 2px solid rgba(204, 204, 204, 1);border-collapse: collapse;border-top-left-radius: 8px;border-top-right-radius: 8px;overflow: hidden;\">";
                        mStrInnerOrderInfoHtml += "    <tr>";
                        mStrInnerOrderInfoHtml += "        <td colspan=\"5\" style=\"background-color: rgba(204, 204, 204, 0.8);padding: 3px;font-weight: bold;font-family: Georgia, 'Times New Roman', Times, serif;\">Enquiry Details</td>";
                        mStrInnerOrderInfoHtml += "    </tr>";
                        mStrInnerOrderInfoHtml += "    <tr>";
                        mStrInnerOrderInfoHtml += "         <td colspan=\"5\" style=\"background-color: rgba(245,222,179, 0.8);padding: 3px;font-weight: bold;font-family: Georgia, 'Times New Roman', Times, serif;\">Enquiry  : <b>" + Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_nVarEnquiryText"]) + "</b></td>";
                        mStrInnerOrderInfoHtml += "    </tr>";
                        mStrInnerOrderInfoHtml += "    <tr>";
                        mStrInnerOrderInfoHtml += "         <td colspan=\"5\" style=\"background-color: rgba(245,222,179, 0.8);padding: 3px;font-weight: bold;font-family: Georgia, 'Times New Roman', Times, serif;\">Remark : <b>" + Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_nVarEnquiryRemark"]) + "</b></td>";
                        mStrInnerOrderInfoHtml += "    </tr>";
                        mStrInnerOrderInfoHtml += "</table>";
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

                        //Process of sending mails to respective receivers
                        //-----------------------------------------------------------------
                         mStrRecievers = Convert.ToString(dsEmailDetails.Tables[2].Rows[0]["IM_vCharInfoEmail"]);
                        MailMessage mObjMailMsg = new MailMessage(Convert.ToString(dsEmailDetails.Tables[1].Rows[0]["EC_vCharSenderEmailId"]), mStrRecievers);
                        mObjMailMsg.IsBodyHtml = true;
                        mObjMailMsg.Body = mStrEmailFullBody;

                        mObjMailMsg.Subject = String.Format("Enquiry reciept confirmation with Enquiry number {0} @ MStoreIndia-{1} : {2}", pLngEnquiryId, dsEmailDetails.Tables[0].Rows[0]["EM_bIntTalukaID"], mStrInfoName);

                        mStrRecievers += ",";
                        for (int iRowCnt = 0; iRowCnt < drArrSearchResult.Length; iRowCnt++)
                        {
                            mStrRecievers += Convert.ToString(drArrSearchResult[iRowCnt]["EER_vCharEmailId"]) + ",";
                            mObjMailMsg.Bcc.Add(new MailAddress(Convert.ToString(drArrSearchResult[iRowCnt]["EER_vCharEmailId"])));
                        }

                        mStrRecievers += Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_vCharClientEmailId"]) + ",";
                        mObjMailMsg.Bcc.Add(new MailAddress(Convert.ToString(dsEmailDetails.Tables[0].Rows[0]["EM_vCharClientEmailId"])));

                        if (mStrRecievers.EndsWith(","))
                            mStrRecievers = mStrRecievers.Remove(mStrRecievers.LastIndexOf(","), 1);

                        if (mObjMailMsg != null)
                        {
                            mObjSmtpClnt.Send(mObjMailMsg);
                            mObjMailMsg.Dispose();
                        }
                        //-----------------------------------------------------------------
                        }
                        catch(Exception expErrorCount)
                        {
                            intFailureCount=intFailureCount+1;
                            errMsg = "Error Message :- " + expErrorCount.Message + " Source :-" + expErrorCount.Source; 
                            intEmailStatus=2;
                        }
                    }

                    //Update status of receipt of enquiry email to client
                    DataTable dtEnqLog = SqlHelper.ReadTable("SP_SaveEnquiryLog", GlobalVariables.SqlConnectionStringMstoreInformativeDb, true,
                               SqlHelper.AddInParam("@bIntEnqLogId", SqlDbType.BigInt, pLngEnquiryId),
                               SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, -1),
                               SqlHelper.AddInParam("@vCharClientId", SqlDbType.VarChar, null),
                               SqlHelper.AddInParam("@vCharProdList", SqlDbType.VarChar, null),
                               SqlHelper.AddInParam("@nVarEnqTxt", SqlDbType.NVarChar, null),
                               SqlHelper.AddInParam("@nVarEnqRmk", SqlDbType.NVarChar, null),
                               SqlHelper.AddInParam("@vCharSendId", SqlDbType.VarChar, strEmailIdList),
                               SqlHelper.AddInParam("@vCharRecievers", SqlDbType.VarChar, mStrRecievers),
                               SqlHelper.AddInParam("@vCharClientName", SqlDbType.VarChar, null),
                               SqlHelper.AddInParam("@vCharClientAdd1", SqlDbType.VarChar, null),
                               SqlHelper.AddInParam("@vCharClientAdd2", SqlDbType.VarChar, null),
                               SqlHelper.AddInParam("@bIntClientPhNo", SqlDbType.BigInt, null),
                               SqlHelper.AddInParam("@bIntPinCode", SqlDbType.BigInt, null),
                               SqlHelper.AddInParam("@vCharClientEmail", SqlDbType.VarChar, null),
                               SqlHelper.AddInParam("@iNtEmailStatus", SqlDbType.Int, intEmailStatus),
                               SqlHelper.AddInParam("@iNtSent", SqlDbType.Int, Convert.ToInt32(intTotCount - intFailureCount)),
                               SqlHelper.AddInParam("@iNtFail", SqlDbType.Int, intFailureCount),
                               SqlHelper.AddInParam("@iNtTotal", SqlDbType.Int,intTotCount),
                               SqlHelper.AddInParam("@vCharError", SqlDbType.VarChar, errMsg),
                               SqlHelper.AddInParam("@bItIsError", SqlDbType.Bit, false));

                    if (dtEnqLog.Rows.Count > 0)
                    {
                        //success 
                    }

                }
                else
                {
                    //Code for no information pertaining to enquiry details
                    long pLngErr = -1;
                    pLngErr = ReportError("SendMailInSafeMachineThread", pLngErr,"Reading datatables", "Dataset has returned null tables", "No trace");
                }
     
            }
            catch (Exception exError)
            {
                ReportError("SendMailInSafeMachineThread", -1, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
            }
        }

        // This method is used to released system resource ie delegate.
        // Release invoker
        private void AsyncResourceRelease(IAsyncResult ar)
        {
            try
            {
                AsyncResult result = (AsyncResult)ar;
                AsyncMethodCaller caller = (AsyncMethodCaller)result.AsyncDelegate;
                caller.EndInvoke(ar);
            }
            catch (Exception exError)
            {
                ReportError("AsyncResourceRelease", -1, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
            }
        }


        #endregion "Email Methods"

        #region "JSON Classes Method"
        private void SendJsonResponse(object mObjInformation, HttpResponse mObjCurrentResponse)
        {
            string mStrResponseJSon = JsonConvert.SerializeObject(mObjInformation);

            mObjCurrentResponse.AddHeader("Access-Control-Allow-Origin", "*");
            mObjCurrentResponse.ContentType = "application/json; charset=utf-8";
            mObjCurrentResponse.Write(mStrResponseJSon);
        }

        /// <summary>
        /// Reports error to server and returns unique report error number.
        /// </summary>
        /// <param name="pStrMethodName">Name of method where error occured</param>
        /// <param name="pLngErrorNumber">Error number if any</param>
        /// <param name="pStrErrorType">Type of error</param>
        /// <param name="pStrDescription">Error message</param>
        /// <returns>Id of error in ServerError Table.<para>You can check the error with this id in server.</para></returns>
        private long ReportError(string pStrMethodName, long pLngErrorNumber, string pStrErrorType, string pStrDescription, string pStrStackTrace)
        {
            try
            {
                string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                DataTable dtRegisteration = SqlHelper.ReadTable("SP_GetErrorReportNumber", mStrConnection, true,
                                                              SqlHelper.AddInParam("@vCharMethodName", SqlDbType.VarChar, pStrMethodName),
                                                              SqlHelper.AddInParam("@vCharClassName", SqlDbType.VarChar, "MStoreInfortmativeService"), // This is name of your class where method is....Change this accordingly.
                                                              SqlHelper.AddInParam("@bIntErrorNumber", SqlDbType.BigInt, pLngErrorNumber),
                                                              SqlHelper.AddInParam("@vCharErrorType", SqlDbType.VarChar, pStrErrorType),
                                                              SqlHelper.AddInParam("@vCharDescription", SqlDbType.VarChar, pStrDescription),
                                                              SqlHelper.AddInParam("@vCharStackTrace", SqlDbType.VarChar, pStrStackTrace));
                return Convert.ToInt64(dtRegisteration.Rows[0][0]);
            }
            catch (Exception exErr)
            {
                return -404404;
            }
        }
        #endregion "JSON Classes Method"

        #region "JSON CLASSES"

        #region "Old commented logic"
        /*public class EnquirySubmissionDetails
        {
            public JsonError Error { get; set; }
            public long EnquiryEmailId { get; set; }
            public long TalukaId { get; set; }
            public long ClientId { get; set; }
            public long ProductId { get; set; }
            public string EnquiryText { get; set; }
            public string EnquiryRem { get; set; }
            public string RecName { get; set; }
            public string RecAdd { get; set; }
        }
        
        public class ProductIdList
        {
            public List<EnquiryProductIdList> EnquiryProductIdLists { get; set; }
        }

        public class EnquiryProductIdList
        {
            public long lngProductId { get; set; }
        }*/
        #endregion "Old commented logic"

        public class ProductsSearchResult
        {
            public ProductsSearchResult()
            {
                Error = null;
                FoundProductsCount = 0;
                FoundInformationsCount = 0;
                FoundProducts = null;
            }

            public JsonError Error { get; set; }
            public long FoundProductsCount { get; set; }
            public long FoundInformationsCount { get; set; }
            public List<ProductMinDtls> FoundProducts { get; set; }
        }


        public class ProductMinDtls
        {
            public ProductMinDtls(long pLngId, long pLngInfoId,
                                  string pStrInfoName, string pStrName, string pStrCategory,
                                  string pStrSubCategory, double pDblPrice, string pStrImgPath)
            {
                Id = pLngId;
                ParentInfoId = pLngInfoId;
                Name = pStrName;
                Category = pStrCategory;
                SubCategory = pStrSubCategory;
                Price = pDblPrice;
                ImagePath = pStrImgPath;
                InfoName = pStrInfoName;
            }

            public long Id { get; set; }
            public long ParentInfoId { get; set; }
            public string InfoName { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string ImagePath { get; set; }
            public double Price { get; set; }
        }

        public class EnquiryResult 
        {
            public JsonError Error { get; set; }
            public long EnquiryId { get; set; }
        }

        public class JsonError
        {

            public JsonError()
            {
                ErrorDescription = "";
                ErrorReportId = -1;
                IsSystemError = false;
            }

            /// <summary>
            /// Create error object for system error.
            /// </summary>
            /// <param name="pStrSystemErrorString">System error.</param>
            public JsonError(string pStrSystemErrorString)
            {
                IsSystemError = true;
                ErrorDescription = pStrSystemErrorString;
                ErrorReportId = -1;
            }

            /// <summary>
            /// Creats error object for custom error details.
            /// </summary>
            /// <param name="pBlnSystemError">Is error a system error.</param>
            /// <param name="pStrErrorString">Description of error.</param>
            /// <param name="pLngErrorReportId">Reporting id for error.</param>
            public JsonError(bool pBlnSystemError, string pStrErrorString, long pLngErrorReportId)
            {
                IsSystemError = pBlnSystemError;
                ErrorDescription = pStrErrorString;
                ErrorReportId = pLngErrorReportId;
            }

            /// <summary>
            /// If true then error is generated by us.And not .Net Error
            /// </summary>
            public bool IsSystemError { get; set; }
            public string ErrorDescription { get; set; }
            public long ErrorReportId { get; set; }
        }

        #endregion "JSON CLASSES"

    }

}
