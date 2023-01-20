using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Admin_CommTrex.MStore_Informative_AppServices
{
    /// <summary>
    /// Summary description for MInfoMobileMasterService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MInfoMobileMasterService : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void LoginCompanyUser(string pStrCompanyKey, string pStrUserId,string pStrUserPass,string pStrUserType, string pStrAccessKey)
        {
            CompanyLoginDetails mObjLoginDetails = new CompanyLoginDetails();
            try
            {
                if (pStrAccessKey == "jhhg4877LKHG8926GBHGe[oheh092")
                {
                    object[] mObjArrAuth = new object[0];

                    pStrCompanyKey = pStrCompanyKey.Replace("-", "");

                    //Commented by SRV on 21/09/2020 for DoveGold Login
                    //if (pStrUserType.Substring(0, 1).ToUpper() == "U")
                    //    mObjArrAuth = SqlHelper.AuthenticateMainForUsers(pStrCompanyKey, pStrUserId, pStrUserPass);
                    //else 
                        
                        if (pStrUserType.Substring(0, 1).ToUpper() == "A")
                        mObjArrAuth = SqlHelper.AuthenticateForAdmin(pStrCompanyKey, pStrUserId, pStrUserPass);

                    if (mObjArrAuth.Length > 0)
                    {
                        if (mObjArrAuth[0] != null)
                        {
                            mObjLoginDetails.Error = null;
                            mObjLoginDetails.CompanyId = ((SysCompany)mObjArrAuth[0]).CompanyId.ToString();
                            mObjLoginDetails.CompanyKey = pStrCompanyKey;
                            mObjLoginDetails.UserId = pStrUserId;
                            mObjLoginDetails.UserRole = pStrUserType;
                        }
                        else
                            mObjLoginDetails.Error = new JsonError("Invalid login credentials");
                    }
                    else
                        mObjLoginDetails.Error = new JsonError("Invalid login credentials");
                }
                else
                    mObjLoginDetails.Error = new JsonError("Access denied to server");
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("LoginCompanyUser", "MInfoMobileMasterService", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjLoginDetails.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjLoginDetails, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetCompanyTalukas(string pStrCompanyId, string pStrAccessKey)
        {
            JsonListResponse mObjTalList = new JsonListResponse();
            try
            {
                if (pStrAccessKey == "#V..khjt8349g^GqK5704..;):-PtqL::")
                {

                    DataTable dtTaluks = SqlHelper.ReadTable("SP_GetTalukasForCompany", GlobalVariables.SqlConnectionStringMstoreInformativeDb, 
                                                                true, SqlHelper.AddInParam("@bIntCompanyId", SqlDbType.BigInt, pStrCompanyId));

                    if (dtTaluks.Rows.Count > 0)
                    {
                        mObjTalList.Error = null;
                        foreach (DataRow drRowData in dtTaluks.Rows)
                        {
                            mObjTalList.JsonList.Add(new NameIdPair(Convert.ToString(drRowData["TM_vCharName_En"]),
                                                               Convert.ToString(drRowData["TM_nVarName_Reg"]),
                                                               Convert.ToInt64(drRowData["Tm_bIntId"])));
                        }
                    }
                    else
                        mObjTalList.Error = new JsonError(true, "No data found", -101);
                }
                else
                    mObjTalList.Error = new JsonError("Access denied to server");
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("GetCompanyTalukas", "MInfoMobileMasterService", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjTalList.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjTalList, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void SaveInformationData(string pStrSaveJsonPara, string pStrAccessKey)
        {
            SaveToDbResult mObjSaveInfoDtls = new SaveToDbResult();
            try
            {
                if (pStrAccessKey == "I8XIJ=OUNQP::N0NQA43IZC::HL0RR=TTUJZ")
                {
                    InformationMasterInsertionPara mObjPara = JsonConvert.DeserializeObject<InformationMasterInsertionPara>(pStrSaveJsonPara);
                    //DataTable dtSaveLog = SqlHelper.ReadTable("SP_ModifyInfoMaster_MobApp", GlobalVariables.SqlConnectionStringMstoreInformativeDb,
                    DataSet dsSaveLogTables = SqlHelper.ReadDataSet("SP_ModifyInfoMaster_MobApp", GlobalVariables.SqlConnectionStringMstoreInformativeDb,
                                                                true,true,
                                                                SqlHelper.AddInParam("@bintTalukaID", SqlDbType.BigInt, mObjPara.TalukaId),
                                                                SqlHelper.AddInParam("@IM_bIntSubCatId", SqlDbType.BigInt, mObjPara.SubCatId),
                                                                SqlHelper.AddInParam("@IM_bIntCatId", SqlDbType.BigInt, mObjPara.CatId),
                                                                SqlHelper.AddInParam("@IM_iNtInfoType", SqlDbType.Int, mObjPara.InfoType),
                                                                
                                                                SqlHelper.AddInParam("@IM_vCharInfoName_En", SqlDbType.VarChar, mObjPara.Name_En),
                                                                SqlHelper.AddInParam("@IM_nVarInfoName_Reg", SqlDbType.VarChar, mObjPara.Name_Reg),
                                                                SqlHelper.AddInParam("@IM_vCharCity_En", SqlDbType.VarChar, mObjPara.City_En),
                                                                SqlHelper.AddInParam("@IM_nVarCity_Reg", SqlDbType.VarChar, mObjPara.City_Reg),
                                                                SqlHelper.AddInParam("@IM_vCharInfoAdd_En", SqlDbType.VarChar, mObjPara.Address_En),
                                                                SqlHelper.AddInParam("@IM_nVarInfoAdd_Reg", SqlDbType.VarChar, mObjPara.Address_Reg),
                                                                SqlHelper.AddInParam("@IM_vCharInfoEmail", SqlDbType.VarChar, mObjPara.Email),
                                                                SqlHelper.AddInParam("@IM_vCharInfoPhone1", SqlDbType.VarChar, mObjPara.Phone1),
                                                                SqlHelper.AddInParam("@IM_vCharInfoPhone2", SqlDbType.VarChar, mObjPara.Phone2),
                                                                SqlHelper.AddInParam("@IM_vCharInfoPhone3", SqlDbType.VarChar, mObjPara.Phone3),
                                                                SqlHelper.AddInParam("@IM_decLongitude", SqlDbType.Decimal, mObjPara.Longitude),
                                                                SqlHelper.AddInParam("@IM_decLatitude", SqlDbType.Decimal, mObjPara.Latitude),
                                                                SqlHelper.AddInParam("@IM_vCharPincode_En", SqlDbType.VarChar, mObjPara.Pincode_En),
                                                                SqlHelper.AddInParam("@IM_nVarPincode_Reg", SqlDbType.VarChar, mObjPara.Pincode_Reg),
                                                                SqlHelper.AddInParam("@IM_vCharUrl", SqlDbType.VarChar, mObjPara.WebSite_Url),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel1_En", SqlDbType.VarChar, mObjPara.ExtraLabel_En1),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel1_Reg", SqlDbType.VarChar, mObjPara.ExtraLabel_Reg1),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue1_En", SqlDbType.VarChar, mObjPara.ExtraValue_En1),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue1_Reg", SqlDbType.VarChar, mObjPara.ExtraValue_Reg1),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel2_En", SqlDbType.VarChar, mObjPara.ExtraLabel_En2),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel2_Reg", SqlDbType.VarChar, mObjPara.ExtraLabel_Reg2),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue2_En", SqlDbType.VarChar, mObjPara.ExtraValue_En2),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue2_Reg", SqlDbType.VarChar, mObjPara.ExtraValue_Reg2),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel3_En", SqlDbType.VarChar, mObjPara.ExtraLabel_En3),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel3_Reg", SqlDbType.VarChar, mObjPara.ExtraLabel_Reg3),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue3_En", SqlDbType.VarChar, mObjPara.ExtraValue_En3),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue3_Reg", SqlDbType.VarChar, mObjPara.ExtraValue_Reg3),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel4_En", SqlDbType.VarChar, mObjPara.ExtraLabel_En4),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel4_Reg", SqlDbType.VarChar, mObjPara.ExtraLabel_Reg4),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue4_En", SqlDbType.VarChar, mObjPara.ExtraValue_En4),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue4_Reg", SqlDbType.VarChar, mObjPara.ExtraValue_Reg4),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel5_En", SqlDbType.VarChar, mObjPara.ExtraLabel_En5),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel5_Reg", SqlDbType.VarChar, mObjPara.ExtraLabel_Reg5),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue5_En", SqlDbType.VarChar, mObjPara.ExtraValue_En5),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue5_Reg", SqlDbType.VarChar, mObjPara.ExtraValue_Reg5),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel6_En", SqlDbType.VarChar, mObjPara.ExtraLabel_En6),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel6_Reg", SqlDbType.VarChar, mObjPara.ExtraLabel_Reg6),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue6_En", SqlDbType.VarChar, mObjPara.ExtraValue_En6),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue6_Reg", SqlDbType.VarChar, mObjPara.ExtraValue_Reg6),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel7_En", SqlDbType.VarChar, mObjPara.ExtraLabel_En7),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel7_Reg", SqlDbType.VarChar, mObjPara.ExtraLabel_Reg7),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue7_En", SqlDbType.VarChar, mObjPara.ExtraValue_En7),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue7_Reg", SqlDbType.VarChar, mObjPara.ExtraValue_Reg7),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel8_En", SqlDbType.VarChar, mObjPara.ExtraLabel_En8),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel8_Reg", SqlDbType.VarChar, mObjPara.ExtraLabel_Reg8),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue8_En", SqlDbType.VarChar, mObjPara.ExtraValue_En8),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue8_Reg", SqlDbType.VarChar, mObjPara.ExtraValue_Reg8),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel9_En", SqlDbType.VarChar, mObjPara.ExtraLabel_En9),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel9_Reg", SqlDbType.VarChar, mObjPara.ExtraLabel_Reg9),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue9_En", SqlDbType.VarChar, mObjPara.ExtraValue_En9),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue9_Reg", SqlDbType.VarChar, mObjPara.ExtraValue_Reg9),

                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraLabel10_En", SqlDbType.VarChar, mObjPara.ExtraLabel_En10),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraLabel10_Reg", SqlDbType.VarChar, mObjPara.ExtraLabel_Reg10),
                                                                SqlHelper.AddInParam("@IM_vCharInfoExtraValue10_En", SqlDbType.VarChar, mObjPara.ExtraValue_En10),
                                                                SqlHelper.AddInParam("@IM_nVarInforExtraValue10_Reg", SqlDbType.VarChar, mObjPara.ExtraValue_Reg10),

                                                                SqlHelper.AddInParam("@IM_bitIsEmergency", SqlDbType.Bit, mObjPara.IsEmergency),
                                                                SqlHelper.AddInParam("@IM_bitIsActive", SqlDbType.Bit, mObjPara.IsActive),
                                                                SqlHelper.AddInParam("@intAmID", SqlDbType.BigInt, mObjPara.InformationId),

                                                                SqlHelper.AddInParam("@bIntCompId", SqlDbType.BigInt, mObjPara.CompanyId),
                                                                SqlHelper.AddInParam("@vCharLoginUserId", SqlDbType.VarChar, mObjPara.LoggedInUserId),
                                                                SqlHelper.AddInParam("@vCharImeiNumber", SqlDbType.VarChar, mObjPara.IMEINumber)

                                                                );
                    DataTable dtSaveLog = dsSaveLogTables.Tables[dsSaveLogTables.Tables.Count - 1];
                    if (dtSaveLog.Rows.Count > 0)
                    {
                        mObjSaveInfoDtls.Error = null;
                        if (dtSaveLog.Columns.Count == 2)
                        {
                            switch (Convert.ToInt32(dtSaveLog.Rows[0]["Err"]))
                            {
                                case 1:
                                    mObjSaveInfoDtls.Error = new JsonError(true, "Maximum limit for Emergency Information is 15 !!!", 1015);
                                    break;
                                case 2:
                                    mObjSaveInfoDtls.Error = new JsonError(true, "Maximum limit for Emergency Information is 10 !!!", 1010);
                                    break;
                                case 3:
                                    mObjSaveInfoDtls.Error = new JsonError(true, "Information with same name already exists !!!", 1035);
                                    break;
                            }
                        }
                        else 
                        {
                            mObjSaveInfoDtls.Error = null;
                            mObjSaveInfoDtls.ModId = Convert.ToInt64(dtSaveLog.Rows[0]["ModifiedInfoId"]);
                            mObjSaveInfoDtls.ModDate = Convert.ToDateTime(dtSaveLog.Rows[0]["SaveTime"]).ToString("dd-MMM-yyyy");
                            mObjSaveInfoDtls.ModTime = Convert.ToDateTime(dtSaveLog.Rows[0]["SaveTime"]).ToString("hh:mm:ss tt");
                            mObjSaveInfoDtls.ModText = Convert.ToString(dtSaveLog.Rows[0]["LogSaver"]);
                        }
                    }
                    else
                        mObjSaveInfoDtls.Error = new JsonError(true, "No data found", -101);
                }
                else
                    mObjSaveInfoDtls.Error = new JsonError("Access denied to server");
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("SaveInformationData", "MInfoMobileMasterService", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjSaveInfoDtls.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjSaveInfoDtls, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetCategories(long pLngTalukaId,long piNtEntryType, string pStrPreviousCategories,string pStrAccessKey)
        {
            JsonListResponse mObjCatList = new JsonListResponse();
            try
            {
                if (pStrAccessKey == "CP7YQ::3PZV7-ALA9A=72MP0-RGGAI::78TD0")
                {

                    DataTable dtCategories = SqlHelper.ReadTable("spGetCategoryDetailsOnType_Safely", GlobalVariables.SqlConnectionStringMstoreInformativeDb, true,
                                                            SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, pLngTalukaId),
                                                            SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, piNtEntryType),
                                                            SqlHelper.AddInParam("@vCharPreviousCategories", SqlDbType.VarChar, pStrPreviousCategories));

                    if (dtCategories.Rows.Count > 0)
                    {
                        mObjCatList.Error = null;
                        foreach (DataRow drRowData in dtCategories.Rows)
                        {
                            mObjCatList.JsonList.Add(new NameIdPair(Convert.ToString(drRowData["CM_vCharName_En"]),
                                                               Convert.ToString(drRowData["CM_nVarName_Reg"]),
                                                               Convert.ToInt64(drRowData["CM_bIntCatId"])));
                        }
                    }
                    else
                        mObjCatList.Error = new JsonError(true, "No data found", -101);
                }
                else
                    mObjCatList.Error = new JsonError("Access denied to server");
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("GetCategories", "MInfoMobileMasterService", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjCatList.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjCatList, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetSubCategories(long pLngTalukaId, long pLngCategoryId, long piNtEntryType, string pStrPreviousIds, string pStrAccessKey)
        {
            JsonListResponse mObjSubCatList = new JsonListResponse();
            try
            {
                if (pStrAccessKey == "OTI7G_BX0RW-70DM3#V2J8X-QM9RU=W9A1F")
                {

                    DataTable dtSubCategories = SqlHelper.ReadTable("spGetSubCategoryDetailsForInfo_Safely", GlobalVariables.SqlConnectionStringMstoreInformativeDb, true,
                                                            SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, pLngTalukaId),
                                                            SqlHelper.AddInParam("@bintCatID", SqlDbType.BigInt, pLngCategoryId),
                                                            SqlHelper.AddInParam("@intEntryType", SqlDbType.Int, piNtEntryType),
                                                            SqlHelper.AddInParam("@vCharPreviousIds", SqlDbType.VarChar, pStrPreviousIds));

                    if (dtSubCategories.Rows.Count > 0)
                    {
                        mObjSubCatList.Error = null;
                        foreach (DataRow drRowData in dtSubCategories.Rows)
                        {
                            mObjSubCatList.JsonList.Add(new NameIdPair(Convert.ToString(drRowData["SCM_vCharName_En"]),
                                                               Convert.ToString(drRowData["SCM_nVarName_Reg"]),
                                                               Convert.ToInt64(drRowData["SCM_bIntSubCatId"])));
                        }
                    }
                    else
                        mObjSubCatList.Error = new JsonError(true, "No data found", -101);
                }
                else
                    mObjSubCatList.Error = new JsonError("Access denied to server");
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("GetSubCategories", "MInfoMobileMasterService", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjSubCatList.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjSubCatList, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetInformationsList(long pLngTalukaId, long pLngCategoryId, long pLngSubCategoryId, long piNtEntryType, string pStrPreviousIds, string pStrAccessKey)
        {
            JsonListResponse mObjInfoList = new JsonListResponse();
            try
            {
                if (pStrAccessKey == "43148=81806-01187/37450-QM9RU*W9A1F")
                {

                    DataTable dtInfos = SqlHelper.ReadTable("SP_GetInfoListForApp_Safely", GlobalVariables.SqlConnectionStringMstoreInformativeDb, true,
                                                            SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId),
                                                            SqlHelper.AddInParam("@bIntCategoryId", SqlDbType.BigInt, pLngCategoryId),
                                                            SqlHelper.AddInParam("@bIntSubCategoryId", SqlDbType.BigInt, pLngSubCategoryId),
                                                            SqlHelper.AddInParam("@iNtEntryType", SqlDbType.Int, piNtEntryType),
                                                            SqlHelper.AddInParam("@vCharPreviousIds", SqlDbType.VarChar, pStrPreviousIds));

                    if (dtInfos.Rows.Count > 0)
                    {
                        mObjInfoList.Error = null;
                        foreach (DataRow drRowData in dtInfos.Rows)
                        {
                            mObjInfoList.JsonList.Add(new NameIdPair(Convert.ToString(drRowData["IM_vCharInfoName_En"]),
                                                               Convert.ToString(drRowData["IM_nVarInfoName_Reg"]),
                                                               Convert.ToInt64(drRowData["IM_bIntInfoId"])));
                        }
                    }
                    else
                        mObjInfoList.Error = new JsonError(true, "No data found", -101);
                }
                else
                    mObjInfoList.Error = new JsonError("Access denied to server");
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("GetInformationsList", "MInfoMobileMasterService", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjInfoList.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjInfoList, this.Context.Response);
        }

        private void SendJsonResponse(object mObjInformation, HttpResponse mObjCurrentResponse)
        {
            string mStrResponseJSon = JsonConvert.SerializeObject(mObjInformation);

            mObjCurrentResponse.AddHeader("Access-Control-Allow-Origin", "*");
            mObjCurrentResponse.ContentType = "application/json; charset=utf-8";
            mObjCurrentResponse.Write(mStrResponseJSon);
        }

        public class CompanyLoginDetails 
        {
            public string CompanyKey { get; set; }
            public string CompanyId { get; set; }
            public string UserRole { get; set; }
            public string UserId { get; set; }
            public JsonError Error { get; set; }

            public CompanyLoginDetails()
            {
                CompanyKey = "";
                CompanyId = "";
                UserId = "";
                UserRole = "";
                Error = null;
            }

            public CompanyLoginDetails(string pStrCompKey,string pStrCompId,string pStrUsrRole,string pStrUsrId)
            {
                CompanyKey = pStrCompId;
                CompanyId = pStrCompId;
                UserId = pStrUsrId;
                UserRole = pStrUsrRole;
                Error = null;
            }
        }

        public class InformationMasterInsertionPara
        {
            public long TalukaId { get; set; }
            public long CatId { get; set; }
            public long SubCatId { get; set; }
            public int InfoType { get; set; }
            public string Name_En { get; set; }
            public string Name_Reg { get; set; }
            public string City_En { get; set; }
            public string City_Reg { get; set; }
            public string Address_En { get; set; }
            public string Address_Reg { get; set; }
            public string Email { get; set; }
            public string Phone1 { get; set; }
            public string Phone2 { get; set; }
            public string Phone3 { get; set; }
            public string Longitude { get; set; }
            public string Latitude { get; set; }
            public string Pincode_En { get; set; }
            public string Pincode_Reg { get; set; }
            public string WebSite_Url { get; set; }

            public string ExtraLabel_En1 { get; set; }
            public string ExtraValue_En1 { get; set; }
            public string ExtraLabel_Reg1 { get; set; }
            public string ExtraValue_Reg1 { get; set; }

            public string ExtraLabel_En2 { get; set; }
            public string ExtraValue_En2 { get; set; }
            public string ExtraLabel_Reg2 { get; set; }
            public string ExtraValue_Reg2 { get; set; }

            public string ExtraLabel_En3 { get; set; }
            public string ExtraValue_En3 { get; set; }
            public string ExtraLabel_Reg3 { get; set; }
            public string ExtraValue_Reg3 { get; set; }

            public string ExtraLabel_En4 { get; set; }
            public string ExtraValue_En4 { get; set; }
            public string ExtraLabel_Reg4 { get; set; }
            public string ExtraValue_Reg4 { get; set; }

            public string ExtraLabel_En5 { get; set; }
            public string ExtraValue_En5 { get; set; }
            public string ExtraLabel_Reg5 { get; set; }
            public string ExtraValue_Reg5 { get; set; }

            public string ExtraLabel_En6 { get; set; }
            public string ExtraValue_En6 { get; set; }
            public string ExtraLabel_Reg6 { get; set; }
            public string ExtraValue_Reg6 { get; set; }

            public string ExtraLabel_En7 { get; set; }
            public string ExtraValue_En7 { get; set; }
            public string ExtraLabel_Reg7 { get; set; }
            public string ExtraValue_Reg7 { get; set; }

            public string ExtraLabel_En8 { get; set; }
            public string ExtraValue_En8 { get; set; }
            public string ExtraLabel_Reg8 { get; set; }
            public string ExtraValue_Reg8 { get; set; }

            public string ExtraLabel_En9 { get; set; }
            public string ExtraValue_En9 { get; set; }
            public string ExtraLabel_Reg9 { get; set; }
            public string ExtraValue_Reg9 { get; set; }

            public string ExtraLabel_En10 { get; set; }
            public string ExtraValue_En10 { get; set; }
            public string ExtraLabel_Reg10 { get; set; }
            public string ExtraValue_Reg10 { get; set; }

            public bool IsEmergency { get; set; }
            public bool IsActive { get; set; }

            public long InformationId { get; set; }
            public long CompanyId { get; set; }
            public string LoggedInUserId { get; set; }
            public string IMEINumber { get; set; }
        }

        public class SaveToDbResult
        {
            public SaveToDbResult()
            {
                ModId = -1;
                ModText = "";
                ModDate = "";
                ModTime = "";
                Error = null;
            }

            public long ModId { get; set; }
            public string ModText { get; set; }
            public string ModDate { get; set; }
            public string ModTime { get; set; }
            public JsonError Error { get; set; }
        }

        /*public class TalukaList
        {
            public TalukaList()
            {
                Talukas = new List<NameIdPair>();
                Error = null;
            }
            public JsonError Error { get; set; }
            public List<NameIdPair> Talukas { get; set; }
        }*/
        //public class Taluka
        //{
        //    public string Name_En { get; set; }
        //    public string Name_Reg { get; set; }
        //    public long Id { get; set; }

        //    public Taluka(string pStrTaluka_En,string pStrTaluka_Reg,long pLngTalukaId)
        //    {
        //        Name_En = pStrTaluka_En;
        //        Name_Reg = pStrTaluka_Reg;
        //        Id = pLngTalukaId;
        //    }
        //}

        public class JsonListResponse
        {
            public JsonListResponse()
            {
                JsonList = new List<NameIdPair>();
                Error = null;
            }
            public JsonError Error { get; set; }
            public List<NameIdPair> JsonList { get; set; }
        }

        

        public class NameIdPair
        {
            public string Name_En { get; set; }
            public string Name_Reg { get; set; }
            public long Id { get; set; }

            public NameIdPair(string pStrTaluka_En, string pStrTaluka_Reg, long pLngTalukaId)
            {
                Name_En = pStrTaluka_En;
                Name_Reg = pStrTaluka_Reg;
                Id = pLngTalukaId;
            }
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

    }
}
