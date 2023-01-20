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
    /// Summary description for MStoreInfortmativeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MStoreInfortmativeService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void RegisterUserDetails(long pLngTalukaId, string pStrIMEINumber, int pIntDeviceType, string pStrDeviceId, string pStrAccessKey)
        {
            RegisterationResult mObjRegisteration = new RegisterationResult();
            try
            {
                if (pStrAccessKey == "j3ehhg3=8943uu3U#l4-554&#$kg3(03j")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataTable dtRegisteration = SqlHelper.ReadTable("SP_ModifyMobileAppUsers", mStrConnection, true,
                                                          SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, pLngTalukaId),
                                                          SqlHelper.AddInParam("@vCharImeiNumber", SqlDbType.VarChar, pStrIMEINumber),
                                                          SqlHelper.AddInParam("@iNtDeviceType", SqlDbType.Int, pIntDeviceType),
                                                          SqlHelper.AddInParam("@vCharDeviceRegId", SqlDbType.VarChar, pStrDeviceId));
                    if (dtRegisteration.Rows.Count > 0)
                    {
                        mObjRegisteration.Error = null;
                        mObjRegisteration.UserRegistered = true;
                    }
                    else
                    {
                        mObjRegisteration.UserRegistered = false;
                        mObjRegisteration.Error = new JsonError("Something went wrong");
                    }
                }
                else
                {
                    mObjRegisteration.UserRegistered = false;
                    mObjRegisteration.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("RegisterUserDetails", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjRegisteration.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjRegisteration, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void RegisterUserDetails2(long pLngTalukaId, string pStrIMEINumber, int pIntDeviceType, string pStrDeviceId, string pBlnIsRegisterationProcess, string pStrAccessKey)
        {
            RegisterationResult mObjRegisteration = new RegisterationResult();
            try
            {
                if (pStrAccessKey == "j3ehhg3=8943uu3U#l4-554&#$kg3(03j")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataTable dtRegisteration = SqlHelper.ReadTable("SP_ModifyMobileAppUsers2", mStrConnection, true,
                                                          SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, pLngTalukaId),
                                                          SqlHelper.AddInParam("@vCharImeiNumber", SqlDbType.VarChar, pStrIMEINumber),
                                                          SqlHelper.AddInParam("@iNtDeviceType", SqlDbType.Int, pIntDeviceType),
                                                          SqlHelper.AddInParam("@vCharDeviceRegId", SqlDbType.VarChar, pStrDeviceId),
                                                          SqlHelper.AddInParam("@bItIsRegistertaionProcess", SqlDbType.Bit, pBlnIsRegisterationProcess));
                    if (dtRegisteration.Rows.Count > 0)
                    {
                        // Modified to save franchise details from server
                        // Dated 06/10/2015

                        DataRow drData = dtRegisteration.Rows[0];
                        mObjRegisteration.Error = null;
                        mObjRegisteration.UserRegistered = true;
                        mObjRegisteration.FranchiseDetails = new Franchise(Convert.ToString(drData["Comp_vCharName"]),
                                                                           Convert.ToString(drData["Comp_nVarName"]),
                                                                           Convert.ToString(drData["Comp_PhoneNumber"]));
                    }
                    else
                    {
                        mObjRegisteration.UserRegistered = false;
                        mObjRegisteration.Error = new JsonError("Something went wrong");
                    }
                }
                else
                {
                    mObjRegisteration.UserRegistered = false;
                    mObjRegisteration.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("RegisterUserDetails2", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjRegisteration.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjRegisteration, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetCategories(long pLngTalukaId, int pIntType, string pStrAccessKey)
        {
            InformationCategories mObjCategories = new InformationCategories();
            try
            {
                if (pStrAccessKey == "ng43euny576%$((%jh94jjh40906mMJH$(j)_)54he54")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                    DataTable dtCategories = SqlHelper.ReadTable("SP_GetCategoriesForApp", mStrConnection, true,
                                                       SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, pLngTalukaId),
                                                       SqlHelper.AddInParam("@iNtEntryType", SqlDbType.Int, pIntType));

                    if (dtCategories.Rows.Count > 0)
                    {

                        List<InfoCategory> mLstCategories = new List<InfoCategory>();
                        foreach (DataRow drCategory in dtCategories.Rows)
                        {
                            InfoCategory mObjCategory = new InfoCategory(Convert.ToInt64(drCategory["CM_bIntCatId"]),
                                                                         Convert.ToString(drCategory["CM_vCharName_En"]),
                                                                         Convert.ToString(drCategory["CM_nVarName_Reg"]));

                            if (Convert.ToString(drCategory["CM_vCharCatImgClass"]) != "")
                            {
                                mObjCategory.ImagePath = Convert.ToString(drCategory["CM_vCharCatImgClass"]);
                                mObjCategory.ImageIsCssClass = true;
                            }
                            else if (Convert.ToString(drCategory["CM_vCharCatImgPath"]) != "")
                            {
                                mObjCategory.ImagePath = Convert.ToString(drCategory["CM_vCharCatImgPath"]);
                                mObjCategory.ImageIsCssClass = false;
                            }
                            else
                            {
                                mObjCategory.ImagePath = "fa-question-circle category-image-not-avaliable";
                                mObjCategory.ImageIsCssClass = true;
                            }

                            mLstCategories.Add(mObjCategory);
                        }

                        //Assign categories
                        mObjCategories.Error = null;
                        mObjCategories.Categories = mLstCategories;
                    }
                    else
                    {
                        mObjCategories.Error = new JsonError(true, "No data found", -101);
                    }
                }
                else
                {
                    mObjCategories.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("GetCategories", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjCategories.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjCategories, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetSubCategories(long pLngTalukaId, long pLngCategoryId, int pIntType, string pStrAccessKey)
        {
            InformationSubCategories mObjSubCategories = new InformationSubCategories();
            try
            {
                if (pStrAccessKey == "gheu%&%4554HGJ0=4t644368Y#$*GH#(_")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                    DataTable dtSubCategories = SqlHelper.ReadTable("SP_GetSubCategoriesForApp", mStrConnection, true,
                                                          SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId),
                                                          SqlHelper.AddInParam("@bIntCategoryId", SqlDbType.BigInt, pLngCategoryId),
                                                          SqlHelper.AddInParam("@iNtEntryType", SqlDbType.Int, pIntType));

                    if (dtSubCategories.Rows.Count > 0)
                    {
                        List<InfoSubCategory> mLstSubCategories = new List<InfoSubCategory>();
                        foreach (DataRow drSubCategory in dtSubCategories.Rows)
                        {
                            InfoSubCategory mObjSubCategory = new InfoSubCategory(Convert.ToInt64(drSubCategory["SCM_bIntCatId"]),
                                                                         Convert.ToInt64(drSubCategory["SCM_bIntSubCatId"]),
                                                                         Convert.ToString(drSubCategory["SCM_vCharName_En"]),
                                                                         Convert.ToString(drSubCategory["SCM_nVarName_Reg"]));

                            if (Convert.ToString(drSubCategory["SCM_vCharCatImgClass"]) != "")
                            {
                                mObjSubCategory.ImagePath = Convert.ToString(drSubCategory["SCM_vCharCatImgClass"]);
                                mObjSubCategory.ImageIsCssClass = true;
                            }
                            else if (Convert.ToString(drSubCategory["SCM_vCharSubCatImgPath"]) != "")
                            {
                                mObjSubCategory.ImagePath = Convert.ToString(drSubCategory["SCM_vCharSubCatImgPath"]);
                                mObjSubCategory.ImageIsCssClass = false;
                            }
                            else
                            {
                                mObjSubCategory.ImagePath = "fa-question-circle image-not-avaliable";
                                mObjSubCategory.ImageIsCssClass = true;
                            }

                            mLstSubCategories.Add(mObjSubCategory);
                        }

                        //Assign categories
                        mObjSubCategories.Error = null;
                        mObjSubCategories.SubCategories = mLstSubCategories;
                    }
                    else
                    {
                        mObjSubCategories.Error = new JsonError(true, "No data found", -101);
                    }
                }
                else
                {
                    mObjSubCategories.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("GetSubCategories", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjSubCategories.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjSubCategories, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetBasicInformation2(long pLngTalukaId, long pLngCategoryId, long pLngSubCategoryId, int pIntInfoType, bool pBlnIsRegional, string pStrAccessKey)
        {
            InformationMin mObjInformationMin = new InformationMin();
            try
            {
                if (pStrAccessKey == "ageEk38913%^hjm8456vb85^$v000($(%05-05*(")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                    DataTable dtInformation = SqlHelper.ReadTable("SP_GetInfoListForApp2", mStrConnection, true,
                                                        SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId),
                                                        SqlHelper.AddInParam("@bIntCategoryId", SqlDbType.BigInt, pLngCategoryId),
                                                        SqlHelper.AddInParam("@bIntSubCategoryId", SqlDbType.BigInt, pLngSubCategoryId),
                                                        SqlHelper.AddInParam("@bItIsRegional", SqlDbType.Bit, (pBlnIsRegional ? 1 : 0)),
                                                        SqlHelper.AddInParam("@iNtEntryType", SqlDbType.Int, pIntInfoType));

                    if (dtInformation.Rows.Count > 0)
                    {
                        List<InformationDetailsMin> mLstInformationMin = new List<InformationDetailsMin>();
                        foreach (DataRow drInfo in dtInformation.Rows)
                        {

                            InformationDetailsMin mObjInfoMin = new InformationDetailsMin(
                                                                    Convert.ToInt64(drInfo["IM_bIntInfoId"]),
                                                                    Convert.ToString(drInfo["IM_vCharInfoName_En"]),
                                                                    Convert.ToString(drInfo["IM_nVarInfoName_Reg"]),
                                                                    Convert.ToString(drInfo["IM_vCharCity_En"]),
                                                                    Convert.ToString(drInfo["IM_nVarCity_Reg"]),
                                                                    Convert.ToString(drInfo["IIG_vCharImagePath"]));

                            mLstInformationMin.Add(mObjInfoMin);
                        }

                        //Assign information
                        mObjInformationMin.Error = null;
                        mObjInformationMin.Information = mLstInformationMin;
                    }
                    else
                    {
                        mObjInformationMin.Error = new JsonError(true, "No data found", -101);
                    }
                }
                else
                {
                    mObjInformationMin.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("GetBasicInformation", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjInformationMin.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjInformationMin, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetBasicInformation(long pLngTalukaId, long pLngCategoryId, long pLngSubCategoryId, int pIntInfoType, string pStrAccessKey)
        {
            InformationMin mObjInformationMin = new InformationMin();
            try
            {
                if (pStrAccessKey == "ageEk38913%^hjm8456vb85^$v000($(%05-05*(")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                    DataTable dtInformation = SqlHelper.ReadTable("SP_GetInfoListForApp", mStrConnection, true,
                                                        SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId),
                                                        SqlHelper.AddInParam("@bIntCategoryId", SqlDbType.BigInt, pLngCategoryId),
                                                        SqlHelper.AddInParam("@bIntSubCategoryId", SqlDbType.BigInt, pLngSubCategoryId),
                                                        SqlHelper.AddInParam("@iNtEntryType", SqlDbType.Int, pIntInfoType));

                    if (dtInformation.Rows.Count > 0)
                    {
                        List<InformationDetailsMin> mLstInformationMin = new List<InformationDetailsMin>();
                        foreach (DataRow drInfo in dtInformation.Rows)
                        {

                            InformationDetailsMin mObjInfoMin = new InformationDetailsMin(
                                                                    Convert.ToInt64(drInfo["IM_bIntInfoId"]),
                                                                    Convert.ToString(drInfo["IM_vCharInfoName_En"]),
                                                                    Convert.ToString(drInfo["IM_nVarInfoName_Reg"]),
                                                                    Convert.ToString(drInfo["IM_vCharCity_En"]),
                                                                    Convert.ToString(drInfo["IM_nVarCity_Reg"]),
                                                                    Convert.ToString(drInfo["IIG_vCharImagePath"]));

                            mLstInformationMin.Add(mObjInfoMin);
                        }

                        //Assign information
                        mObjInformationMin.Error = null;
                        mObjInformationMin.Information = mLstInformationMin;
                    }
                    else
                    {
                        mObjInformationMin.Error = new JsonError(true, "No data found", -101);
                    }
                }
                else
                {
                    mObjInformationMin.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("GetBasicInformation", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjInformationMin.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjInformationMin, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetInformation(long pLngTalukaId, long pLngInfoId, string pStrAccessKey)
        {
            InformationFull mObjInformationFull = new InformationFull();
            try
            {
                if (pStrAccessKey == "5483lubvh4843ltgxvbhlop58783$gg$%^ce52055")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                    //string mStrSql = "SELECT [IM_bIntInfoId],[IM_vCharInfoName_En],[IM_nVarInfoName_Reg]";
                    //mStrSql += ",[IM_vCharCity_En],[IM_nVarCity_Reg],REPLACE([IM_vCharInfoAdd_En],'\n','<br/>') As [IM_vCharInfoAdd_En],REPLACE([IM_nVarInfoAdd_Reg],'\n','<br/>') As [IM_nVarInfoAdd_Reg] ";
                    //mStrSql += ",[IM_vCharInfoEmail],[IM_vCharInfoPhone1],[IM_vCharInfoPhone2],[IM_vCharInfoPhone3] ";
                    //mStrSql += ",COALESCE([IM_decLongitude],0.0000) As [IM_decLongitude],COALESCE([IM_decLatitude],0.0000) As [IM_decLatitude] ";
                    //mStrSql += ",[IM_vCharPincode_En],[IM_nVarPincode_Reg] ";
                    //for (int iColCount = 1; iColCount <= 10; iColCount++)
                    //{
                    //    mStrSql += String.Format(",[IM_vCharInfoExtraLabel{0}_En]", iColCount);
                    //    mStrSql += String.Format(",[IM_nVarInforExtraLabel{0}_Reg]", iColCount);
                    //    mStrSql += String.Format(",[IM_vCharInfoExtraValue{0}_En]", iColCount);
                    //    mStrSql += String.Format(",[IM_nVarInforExtraValue{0}_Reg]", iColCount);
                    //}
                    //mStrSql += ",iig.IIG_vCharImagePath ,iig.IIG_vCharImageDescription_En,iig.IIG_nVarImageDescription_Reg  ";
                    //mStrSql += "FROM [Information_Master_{0}] im Left Join [Information_Image_Gallery_{0}] iig ";
                    //mStrSql += "On im.IM_bIntInfoId = iig.IIG_bIntInfoId  ";
                    //mStrSql += "Where im.IM_bIntInfoId = @infoId ";

                    //mStrSql = String.Format(mStrSql, pLngTalukaId);
                    DataTable dtInformation = SqlHelper.ReadTable("SP_GetInfoDetailsForApp", mStrConnection, true,
                                                        SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId),
                                                        SqlHelper.AddInParam("@bIntInformationId", SqlDbType.BigInt, pLngInfoId));

                    if (dtInformation.Rows.Count > 0)
                    {
                        DataRow drInfo1 = dtInformation.Rows[0];
                        InformationDetailsFull mObjFullDetail = new InformationDetailsFull(
                                                                        Convert.ToBoolean(drInfo1["IM_bItIsActive"]),
                                                                        Convert.ToInt32(drInfo1["IM_bItIsEmergency"]),
                                                                        Convert.ToInt64(drInfo1["IM_bIntInfoId"]),
                                                                        Convert.ToString(drInfo1["IM_vCharInfoName_En"]),
                                                                        Convert.ToString(drInfo1["IM_nVarInfoName_Reg"]),
                                                                        Convert.ToString(drInfo1["IM_vCharCity_En"]),
                                                                        Convert.ToString(drInfo1["IM_nVarCity_Reg"]),
                                                                        Convert.ToString(drInfo1["IM_vCharInfoAdd_En"]),
                                                                        Convert.ToString(drInfo1["IM_nVarInfoAdd_Reg"]),
                                                                        Convert.ToString(drInfo1["IM_vCharInfoEmail"]),
                                                                        Convert.ToString(drInfo1["IM_vCharInfoPhone1"]),
                                                                        Convert.ToString(drInfo1["IM_vCharInfoPhone2"]),
                                                                        Convert.ToString(drInfo1["IM_vCharInfoPhone3"]),
                                                                        Convert.ToDouble(drInfo1["IM_decLongitude"]),
                                                                        Convert.ToDouble(drInfo1["IM_decLatitude"]),
                                                                        Convert.ToString(drInfo1["IM_vCharPincode_En"]),
                                                                        Convert.ToString(drInfo1["IM_nVarPincode_Reg"]),
                                                                        Convert.ToString(drInfo1["IM_vCharUrl"]));

                        List<InformationExtraDetails> mLstExtras = new List<InformationExtraDetails>();
                        for (int mIntExtraDetails = 1; mIntExtraDetails <= 10; mIntExtraDetails++)
                        {
                            if (Convert.ToString(drInfo1[String.Format("IM_vCharInfoExtraLabel{0}_En", mIntExtraDetails)]) != "" || Convert.ToString(drInfo1[String.Format("IM_nVarInforExtraLabel{0}_Reg", mIntExtraDetails)]) != "")
                            {
                                mLstExtras.Add(new InformationExtraDetails(Convert.ToString(drInfo1[String.Format("IM_vCharInfoExtraLabel{0}_En", mIntExtraDetails)]),
                                                                           Convert.ToString(drInfo1[String.Format("IM_nVarInforExtraLabel{0}_Reg", mIntExtraDetails)]),
                                                                           Convert.ToString(drInfo1[String.Format("IM_vCharInfoExtraValue{0}_En", mIntExtraDetails)]),
                                                                           Convert.ToString(drInfo1[String.Format("IM_nVarInforExtraValue{0}_Reg", mIntExtraDetails)])));
                            }
                        }
                        mObjFullDetail.Extra = mLstExtras;

                        List<InformationImageGallery> Images = new List<InformationImageGallery>();
                        foreach (DataRow drRow in dtInformation.Rows)
                        {
                            if (Convert.ToString(drRow["IIG_vCharImagePath"]) != "")
                                Images.Add(new InformationImageGallery(Convert.ToString(drRow["IIG_vCharImagePath"]), Convert.ToString(drRow["IIG_vCharImageDescription_En"]), Convert.ToString(drRow["IIG_nVarImageDescription_Reg"])));
                        }
                        mObjFullDetail.Images = Images;

                        //Assign information
                        mObjInformationFull.Error = null;
                        mObjInformationFull.Information = mObjFullDetail;
                    }
                    else
                    {
                        mObjInformationFull.Error = new JsonError(true, "No data found", -101);
                    }
                }
                else
                {
                    mObjInformationFull.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("GetBasicInformation", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjInformationFull.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjInformationFull, this.Context.Response);
        }

        [WebMethod] // This method can be accessed over internet.
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetPageAdvertisments(string pStrActivityId, long pLngTalukaId, string pStrMethodAuthKey)
        {
            // TODO : Check Expiry.
            AdvertisementData mObjAdData = new AdvertisementData();
            if (pStrActivityId.Contains("."))
                pStrActivityId = pStrActivityId.Remove(pStrActivityId.LastIndexOf("."));

            if (pStrMethodAuthKey == "kheh[g'[85ty8j7*(%@T$Y&8544)KY&3994krh9%&_-0-0-_)_")
            {
                string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                DataTable dtAdsData = SqlHelper.ReadTable("SP_GetPageAdsForApp", mStrConnection, true,
                                                SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId),
                                                SqlHelper.AddInParam("@vCharPageId", SqlDbType.VarChar, pStrActivityId));

                if (dtAdsData.Rows.Count > 0)
                {
                    mObjAdData.LinkUrl = Convert.ToString(dtAdsData.Rows[0]["AM_vCharURL"]);
                    mObjAdData.HtmlText = Convert.ToString(dtAdsData.Rows[0]["AM_nVarHtmlText"]);
                    mObjAdData.ImageUrl = Convert.ToString(dtAdsData.Rows[0]["AM_vCharAdImageLink"]);
                }
            }
            else
            {
                // unauthorized access to method.
            }

            string mStrResponseJSon = JsonConvert.SerializeObject(mObjAdData);

            this.Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            this.Context.Response.ContentType = "application/json; charset=utf-8";
            this.Context.Response.Write(mStrResponseJSon);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetOtherStatic(long pLngTalukaId, string pStrAccessKey)
        {
            StaticOtherInformation mObjStaticOther = new StaticOtherInformation();
            try
            {
                if (pStrAccessKey == "=_=525kKYAabcd2.sun.sathiya.is.copied.from.abcd856jhi=_-")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                    DataTable dtOther = SqlHelper.ReadTable("SP_GetPeriodicalAdsForApp", mStrConnection, true,
                                                            SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId));

                    if (dtOther.Rows.Count > 0)
                    {
                        List<OtherInfo> mLstOthers = new List<OtherInfo>();
                        foreach (DataRow drInfo in dtOther.Rows)
                        {

                            OtherInfo mObjOther = new OtherInfo(
                                                                    Convert.ToInt64(drInfo["SOI_bIntId"]),
                                                                    Convert.ToString(drInfo["SOI_nVarTitle_Reg"]),
                                                                    Convert.ToString(drInfo["SOI_nVarText_Reg"]),
                                                                    Convert.ToString(drInfo["SOI_vCharImagePath"]),
                                                                    Convert.ToInt64(drInfo["SOI_bIntInfoId"]),
                                                                    Convert.ToDateTime(drInfo["SOI_dtCreateDate"])
                                                                );

                            mLstOthers.Add(mObjOther);
                        }

                        //Assign information
                        mObjStaticOther.Error = null;
                        mObjStaticOther.Others = mLstOthers;
                    }
                    else
                    {
                        mObjStaticOther.Error = new JsonError(true, "No data found", -101);
                    }
                }
                else
                {
                    mObjStaticOther.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("GetOtherStatic", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjStaticOther.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjStaticOther, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetJobEmployment(long pLngTalukaId, bool pBlnIsRegional, string pStrAccessKey)
        {
            StaticOtherInformation mObjJobEmployment = new StaticOtherInformation();
            try
            {
                if (pStrAccessKey == "HMRKN=P9A9E::COVETTHIH8::M4TIH=HSM3L")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                    DataTable dtJobEmployment = SqlHelper.ReadTable("SP_GetJobEmploymentForApp", mStrConnection, true,
                                                            SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId));

                    if (dtJobEmployment.Rows.Count > 0)
                    {
                        List<OtherInfo> mLstJobEmploy = new List<OtherInfo>();
                        foreach (DataRow drInfo in dtJobEmployment.Rows)
                        {
                            if (pBlnIsRegional)
                            {
                                OtherInfo mObjJob = new OtherInfo(
                                                                        Convert.ToInt64(drInfo["JEA_bIntId"]),
                                                                        Convert.ToString(drInfo["JEA_nVarTitle_Reg"]),
                                                                        Convert.ToString(drInfo["JEA_nVarText_Reg"]),
                                                                        Convert.ToString(drInfo["JEA_vCharImagePath"]),
                                                                        Convert.ToInt64(drInfo["JEA_bIntInfoId"]),
                                                                        Convert.ToDateTime(drInfo["JEA_dtCreateDate"])
                                                                    );

                                mLstJobEmploy.Add(mObjJob);
                            }
                            else
                            {
                                OtherInfo mObjJob = new OtherInfo(
                                                                        Convert.ToInt64(drInfo["JEA_bIntId"]),
                                                                        Convert.ToString(drInfo["JEA_vCharTitle_En"]),
                                                                        Convert.ToString(drInfo["JEA_vCharText_En"]),
                                                                        Convert.ToString(drInfo["JEA_vCharImagePath"]),
                                                                        Convert.ToInt64(drInfo["JEA_bIntInfoId"]),
                                                                        Convert.ToDateTime(drInfo["JEA_dtCreateDate"])
                                                                    );

                                mLstJobEmploy.Add(mObjJob);
                            }
                        }

                        //Assign information
                        mObjJobEmployment.Error = null;
                        mObjJobEmployment.Others = mLstJobEmploy;
                    }
                    else
                    {
                        mObjJobEmployment.Error = new JsonError(true, "No data found", -101);
                    }
                }
                else
                {
                    mObjJobEmployment.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("GetJobEmployment", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjJobEmployment.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjJobEmployment, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void FullDataSearchForWord(long pLngTalukaId, string pStrSearchWord, bool pBlnRegional, string pStrAccessKey)
        {
            InformationMin mObjInformationMin = new InformationMin();
            try
            {
                if (pStrAccessKey == "ek5862tha89tiger37bajrangi9164bhai7832jaan1597")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                    DataTable dtInformation = SqlHelper.ReadTable("SP_SearchFullDataMobApp", mStrConnection, true,
                                                        SqlHelper.AddInParam("@vCharSearchWord", SqlDbType.VarChar, (pBlnRegional ? "" : pStrSearchWord)),
                                                        SqlHelper.AddInParam("@nVarSearchWord", SqlDbType.NVarChar, (pBlnRegional ? pStrSearchWord : "")),
                                                        SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId));

                    if (dtInformation.Rows.Count > 0)
                    {
                        List<InformationDetailsMin> mLstInformationMin = new List<InformationDetailsMin>();
                        foreach (DataRow drInfo in dtInformation.Rows)
                        {

                            InformationDetailsMin mObjInfoMin = new InformationDetailsMin(
                                                                    Convert.ToInt64(drInfo["IM_bIntInfoId"]),
                                                                    Convert.ToString(drInfo["IM_vCharInfoName_En"]),
                                                                    Convert.ToString(drInfo["IM_nVarInfoName_Reg"]),
                                                                    Convert.ToString(drInfo["IM_vCharCity_En"]),
                                                                    Convert.ToString(drInfo["IM_nVarCity_Reg"]), "");

                            mLstInformationMin.Add(mObjInfoMin);
                        }

                        //Assign information
                        mObjInformationMin.Error = null;
                        mObjInformationMin.Information = mLstInformationMin;
                    }
                    else
                    {
                        mObjInformationMin.Error = new JsonError(true, "No data found", -101);
                    }
                }
                else
                {
                    mObjInformationMin.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("FullDataSearchForWord", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjInformationMin.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjInformationMin, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void CustomDataSearch(long pLngTalukaId, string pStrCategory, string pStrSubCategory,
                                     string pStrName, string pStrCity, string pStrAddress, string pStrPinCode,
                                     bool pBlnMatchAll, bool pBlnRegional, string pStrAccessKey)
        {
            InformationMin mObjInformationMin = new InformationMin();
            try
            {
                if (pStrAccessKey == "2807-asdf-2015-jilm-045-qsc-238-vhi")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                    DataTable dtInformation = SqlHelper.ReadTable("SP_SearchAdvacnedDataMobApp", mStrConnection, true,
                                                        SqlHelper.AddInParam("@nVarSearchCategory", SqlDbType.NVarChar, pStrCategory),
                                                        SqlHelper.AddInParam("@nVarSearchSubCategory", SqlDbType.NVarChar, pStrSubCategory),
                                                        SqlHelper.AddInParam("@nVarSearchName", SqlDbType.NVarChar, pStrName),
                                                        SqlHelper.AddInParam("@nVarSearchCity", SqlDbType.NVarChar, pStrCity),
                                                        SqlHelper.AddInParam("@nVarSearchAddress", SqlDbType.NVarChar, pStrAddress),
                                                        SqlHelper.AddInParam("@nVarSearchPinCode", SqlDbType.NVarChar, pStrPinCode),
                                                        SqlHelper.AddInParam("@bItSearchType", SqlDbType.Bit, pBlnRegional ? 1 : 0),
                                                        SqlHelper.AddInParam("@bItSearchFilterJoins", SqlDbType.Bit, pBlnMatchAll ? 1 : 0),
                                                        SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId));

                    if (dtInformation.Rows.Count > 0)
                    {
                        List<InformationDetailsMin> mLstInformationMin = new List<InformationDetailsMin>();
                        foreach (DataRow drInfo in dtInformation.Rows)
                        {

                            InformationDetailsMin mObjInfoMin = new InformationDetailsMin(
                                                                    Convert.ToInt64(drInfo["IM_bIntInfoId"]),
                                                                    Convert.ToString(drInfo["IM_vCharInfoName_En"]),
                                                                    Convert.ToString(drInfo["IM_nVarInfoName_Reg"]),
                                                                    Convert.ToString(drInfo["IM_vCharCity_En"]),
                                                                    Convert.ToString(drInfo["IM_nVarCity_Reg"]), "");

                            mLstInformationMin.Add(mObjInfoMin);
                        }

                        //Assign information
                        mObjInformationMin.Error = null;
                        mObjInformationMin.Information = mLstInformationMin;
                    }
                    else
                    {
                        mObjInformationMin.Error = new JsonError(true, "No data found", -101);
                    }
                }
                else
                {
                    mObjInformationMin.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("CustomDataSearch", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjInformationMin.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjInformationMin, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetTalukas(string pStrAccessKey, int pIntStateId)
        {
            TalukaDetails mObjTalukas = new TalukaDetails();
            try
            {
                if (pStrAccessKey == "2358-lzpf-r634-79is-rl5-449")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                    string mStrSql = "SELECT [TM_bIntId],[TM_vCharName_En],[TM_nVarName_Reg] "
                                   + "FROM Taluka_Master tm INNER JOIN State_Master sm  "
                                   + "On tm.TM_iNtStateMasterId = sm.SM_iNtId And sm.SM_bItIsActive = 1 "
                                   + "Where TM_bItIsActive = 1  "
                                   + "And sm.SM_iNtId = @stateId    ";
                    DataTable dtTalukas = SqlHelper.ReadTable(mStrSql, mStrConnection, false, SqlHelper.AddInParam("@stateId", SqlDbType.Int, pIntStateId));

                    if (dtTalukas.Rows.Count > 0)
                    {
                        List<Taluka> mLstTalukas = new List<Taluka>();
                        foreach (DataRow drTaluka in dtTalukas.Rows)
                        {
                            Taluka mObjTaluka = new Taluka(Convert.ToInt64(drTaluka["TM_bIntId"]),
                                                                         Convert.ToString(drTaluka["TM_vCharName_En"]),
                                                                         Convert.ToString(drTaluka["TM_nVarName_Reg"]));
                            mLstTalukas.Add(mObjTaluka);
                        }

                        //Assign categories
                        mObjTalukas.Error = null;
                        mObjTalukas.Talukas = mLstTalukas;
                    }
                    else
                    {
                        mObjTalukas.Error = new JsonError(true, "No data found", -101);
                    }
                }
                else
                {
                    mObjTalukas.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("GetTalukas", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjTalukas.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjTalukas, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetServices(string pStrAccessKey, long plngTalukaId)
        {
            //Added BY HVB on 10/08/2016
            //For list of service at taluka

            ServicesList mObjServiceList = new ServicesList();
            try
            {
                if (pStrAccessKey == "1008-lzpf-getS-79is-qDta-2016")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                    DataTable dtTalukas = SqlHelper.ReadTable("SP_GetServicesAtTaluka", mStrConnection, true, SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, plngTalukaId));

                    if (dtTalukas.Rows.Count > 0)
                    {
                        //Assign categories
                        mObjServiceList.Error = null;
                        foreach (DataRow drTaluka in dtTalukas.Rows)
                        {
                            mObjServiceList.Id.Add(Convert.ToInt64(drTaluka["TSM_bIntID"]));
                            mObjServiceList.Name_En.Add(Convert.ToString(drTaluka["TSM_vCharServiceName"]));
                            mObjServiceList.Name_Reg.Add(Convert.ToString(drTaluka["TSM_nVarServices"]));
                            mObjServiceList.CategoryId.Add(Convert.ToInt64(drTaluka["TSM_bIntCategoryID"]));
                            mObjServiceList.SubCateogryId.Add(Convert.ToInt64(drTaluka["TSM_bIntSubCatID"]));
                        }
                    }
                    else
                    {
                        mObjServiceList.Error = new JsonError(true, "No data found", -101);
                    }
                }
                else
                {
                    mObjServiceList.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("GetServices", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjServiceList.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjServiceList, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void BroadcastQuery(long pLngTalukaId, string pStrIMEI, string pStrName,
                                   string pStrPhone1, string pStrPhone2, string pStrPhone3,
                                   string pStrEmail1, string pStrEmail2,
                                   string pStrSubject, string pStrQueryBody,
                                   string pStrAccessKey)
         {
            QueryBroadCastResult mObjBroadCastRes = new QueryBroadCastResult();
            try
            {
                if (pStrAccessKey == "6WWH5-X3MC9-BFBMY-P9VKA-UAKJ89XXDU")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                    DataSet dsQueryData = SqlHelper.ReadDataSet("SP_AddAndModAppQueries", mStrConnection, true, false,
                                                              SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, pLngTalukaId),
                                                              SqlHelper.AddInParam("@bIntQueryId", SqlDbType.BigInt, -1),
                                                              SqlHelper.AddInParam("@vCharIMEI", SqlDbType.VarChar, pStrIMEI),
                                                              SqlHelper.AddInParam("@vCharList", SqlDbType.VarChar, null),
                                                              SqlHelper.AddInParam("@nVarName", SqlDbType.NVarChar, pStrName),
                                                              SqlHelper.AddInParam("@vCharPhone1", SqlDbType.VarChar, pStrPhone1),
                                                              SqlHelper.AddInParam("@vCharPhone2", SqlDbType.VarChar, pStrPhone2),
                                                              SqlHelper.AddInParam("@vCharPhone3", SqlDbType.VarChar, pStrPhone3),
                                                              SqlHelper.AddInParam("@vCharEmail1", SqlDbType.VarChar, pStrEmail1),
                                                              SqlHelper.AddInParam("@vCharEmail2", SqlDbType.VarChar, pStrEmail2),
                                                              SqlHelper.AddInParam("@nVarSubject", SqlDbType.NVarChar, pStrSubject),
                                                              SqlHelper.AddInParam("@nVarQryBody", SqlDbType.NVarChar, pStrQueryBody),
                                                              SqlHelper.AddInParam("@iNtFailCnt", SqlDbType.Int, -1),
                                                              SqlHelper.AddInParam("@iNtSentCnt", SqlDbType.Int, -1)
                                                              );

                    if (dsQueryData.Tables.Count > 0)
                    {
                        // *---------------------------------------*
                        // Sending clound message to devices
                        // *---------------------------------------*

                        // Create message object.
                        //
                        GcmMessageRequest GcmPushMessage = new GcmMessageRequest();

                        // Add Sender
                        //
                        GcmPushMessage.SetSender(pStrIMEI, pStrName);

                        // Set Reciever
                        //
                        Dictionary<string, string> mDicRecievers = dsQueryData.Tables[0].AsEnumerable().ToDictionary<DataRow, string, string>(row => row.Field<string>(1),
                                        row => row.Field<string>(3));
                        GcmPushMessage.SetRecieverList(mDicRecievers);

                       

                        // Set Message
                        //
                        GcmPushMessage.SetMessage(pStrSubject, Convert.ToInt32(-101), MessageTypes.MSTORE_INFORMATIVE_QUERY);
                        GcmPushMessage.SetQueryMessage(pStrName, pStrPhone1, pStrPhone2, pStrPhone3,
                                                       pStrEmail1, pStrEmail2, pStrSubject, pStrQueryBody);

                        // Set Bunch id
                        //
                        GcmPushMessage.MessageBunchId = String.Format("Qry_Msg_By_AppUser_{0}", DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss_tt"));

                        // Set custom date and time
                        //
                        GcmPushMessage.MsgDate = DateTime.Now.ToString("yyyy-MM-dd");
                        GcmPushMessage.MsgTime = DateTime.Now.ToString("hh:mm tt");

                        // Create Json Message
                        // 
                        string sJsonMsg = "";

                        try
                        {
                            sJsonMsg = GcmPushMessage.CreateJsonMsg();
                        }
                        catch (Exception _JsonError)
                        {
                            mObjBroadCastRes.Error = new JsonError(false, "Some error occured while Forwarding your query", ReportError("BroadcastQuery", -1, _JsonError.GetBaseException().GetType().ToString(), String.Format("Error Creating notification message : {0}", _JsonError.Message), _JsonError.StackTrace));
                        }

                        if (mObjBroadCastRes.Error == null)
                        {
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
                                        //UpdateNotificationData(Convert.ToInt64(mDicInformation["Id"]), txtNotificationTitle.Text, txtNotificationText.Text, mStrImagePath, dtUserData.Rows.Count, _MsgResponse.SuccessMessages, _MsgResponse.FailedMessages);
                                        UpdateQueryLog(Convert.ToInt64(dsQueryData.Tables[1].Rows[0]["AUQM_bIntId"]), _MsgResponse.SuccessMessages, _MsgResponse.FailedMessages, String.Join(",", mDicRecievers.Keys.ToArray<string>()));
                                        mObjBroadCastRes.NotifiedUsers = _MsgResponse.SuccessMessages;
                                        mObjBroadCastRes.UnNotifiedUsers = _MsgResponse.FailedMessages;
                                    }
                                }
                                else if (sHttpRes == "OK")
                                {
                                    // A valid json response.
                                    if (_MsgResponse.ReadResponse(sResponseFromServer))
                                    {
                                        //UpdateNotificationData(Convert.ToInt64(mDicInformation["Id"]), txtNotificationTitle.Text, txtNotificationText.Text, mStrImagePath, dtUserData.Rows.Count, _MsgResponse.SuccessMessages, _MsgResponse.FailedMessages);
                                        UpdateQueryLog(Convert.ToInt64(dsQueryData.Tables[1].Rows[0]["AUQM_bIntId"]), _MsgResponse.SuccessMessages, _MsgResponse.FailedMessages, String.Join(",", mDicRecievers.Keys.ToArray<string>()));
                                        mObjBroadCastRes.NotifiedUsers = _MsgResponse.SuccessMessages;
                                        mObjBroadCastRes.UnNotifiedUsers = _MsgResponse.FailedMessages;
                                    }
                                }
                                else
                                {
                                    // Invalid json response
                                    // wat to do here ??
                                    //MessageBox.Show("Response From Server : " + sResponseFromServer);
                                    mObjBroadCastRes.Error = new JsonError(true, "Something went wrong on serve side..", -55);
                                }

                                mObjBroadCastRes.TotalUsers = dsQueryData.Tables[0].Rows.Count;
                                mObjBroadCastRes.QueryDate = Convert.ToDateTime(dsQueryData.Tables[1].Rows[0][1]).ToString("dd-MMM-yyyy");
                                mObjBroadCastRes.QueryTime = Convert.ToDateTime(dsQueryData.Tables[1].Rows[0][1]).ToString("hh:mm:ss tt");
                                mObjBroadCastRes.QueryId = Convert.ToInt64(dsQueryData.Tables[1].Rows[0][0]);
                            }
                            catch (Exception exSender)
                            {
                                //_MdiMainLog.Add(new VTalkLog("Can't convert this resulted json to ur desired result : " + exSender.Message, "PushAckMessage", LogTypes.LogWarnings));
                                mObjBroadCastRes.Error = new JsonError(false, "Some error occured while Forwarding your query", ReportError("BroadcastQuery", -1, exSender.GetBaseException().GetType().ToString(), String.Format("Error Creating notification message : {0}", exSender.Message), exSender.StackTrace));
                            }
                        }
                    }
                    else
                    {
                        mObjBroadCastRes.Error = new JsonError(true, "No data found", -101);
                    }
                }
                else
                {
                    mObjBroadCastRes.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("GetTalukas", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjBroadCastRes.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjBroadCastRes, this.Context.Response);
        }

        //Added By HVB for emergency data retrieval dated 14-10-2015
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetEmergencyInfo(long pLngTalukaId, long pLngLastUpdateVersion, string pStrMethodAuthKey)
        {
            EmergencyInformationList mObjInformationFull = new EmergencyInformationList();
            try
            {
                if (pStrMethodAuthKey == "=-hgfg*)ng6373qdcio062a56bajrangi!-=")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

                    DataSet dsInformation = SqlHelper.ReadDataSet("SP_GetEmergencyInfo", mStrConnection, true, true,
                                                        SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId));

                    if (dsInformation.Tables.Count == 2)
                    {
                        if (dsInformation.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToInt64(dsInformation.Tables[0].Rows[0]["EIML_bIntEmergencyInfoVersion"]) == pLngLastUpdateVersion)
                                mObjInformationFull.Error = new JsonError(true, "Already upto date", -104);
                            else
                            {
                                if (dsInformation.Tables[1].Rows.Count > 0)
                                {
                                    mObjInformationFull.Error = null;
                                    mObjInformationFull.InformationVersion = Convert.ToInt64(dsInformation.Tables[0].Rows[0]["EIML_bIntEmergencyInfoVersion"]);

                                    DataTable dtInformations = dsInformation.Tables[1];

                                    Dictionary<long, EmergencyInformation> mLstInfos = new Dictionary<long, EmergencyInformation>();
                                    DataRow drCurrRow;
                                    long pLngInfoId;
                                    for (int mIntRowCnt = 0; mIntRowCnt < dtInformations.Rows.Count; mIntRowCnt++)
                                    {
                                        drCurrRow = dtInformations.Rows[mIntRowCnt];
                                        EmergencyInformation mObjEmergencyDtls = null;
                                        pLngInfoId = Convert.ToInt64(drCurrRow["IM_bIntInfoId"]);
                                        if (mLstInfos.ContainsKey(pLngInfoId))
                                            mObjEmergencyDtls = mLstInfos[pLngInfoId];
                                        else
                                        {
                                            mObjEmergencyDtls = new EmergencyInformation(pLngInfoId, Convert.ToString(drCurrRow["IM_vCharInfoName_En"]), Convert.ToString(drCurrRow["IM_nVarInfoName_Reg"]),
                                                                        Convert.ToString(drCurrRow["IM_vCharCity_En"]), Convert.ToString(drCurrRow["IM_nVarCity_Reg"]), Convert.ToString(drCurrRow["IM_vCharInfoAdd_En"]), Convert.ToString(drCurrRow["IM_nVarInfoAdd_Reg"]),
                                                                        Convert.ToString(drCurrRow["IM_vCharInfoEmail"]), Convert.ToString(drCurrRow["IM_vCharUrl"]), Convert.ToString(drCurrRow["IM_vCharInfoPhone1"]), Convert.ToString(drCurrRow["IM_vCharInfoPhone2"]), Convert.ToString(drCurrRow["IM_vCharInfoPhone3"]),
                                                                        Convert.ToDouble(drCurrRow["IM_decLongitude"]), Convert.ToDouble(drCurrRow["IM_decLatitude"]), Convert.ToString(drCurrRow["IM_vCharPincode_En"]), Convert.ToString(drCurrRow["IM_nVarPincode_Reg"]));
                                            mObjEmergencyDtls.Images = new List<ImageDetail>();
                                            mObjEmergencyDtls.Extra = new List<InformationExtraDetails>();
                                            for (int mCntr = 1; mCntr <= 10; mCntr++)
                                            {
                                                mObjEmergencyDtls.Extra.Add(new InformationExtraDetails(Convert.ToString(drCurrRow["IM_vCharInfoExtraLabel" + mCntr.ToString() + "_En"]),
                                                                                                Convert.ToString(drCurrRow["IM_nVarInforExtraLabel" + mCntr.ToString() + "_Reg"]),
                                                                                                Convert.ToString(drCurrRow["IM_vCharInfoExtraValue" + mCntr.ToString() + "_En"]),
                                                                                                Convert.ToString(drCurrRow["IM_nVarInforExtraValue" + mCntr.ToString() + "_Reg"])));
                                            }
                                            mLstInfos.Add(pLngInfoId, mObjEmergencyDtls);
                                        }

                                        if (Convert.ToString(drCurrRow["IIG_vCharImagePath"]) != "")
                                        {
                                            mObjEmergencyDtls.Images.Add(new ImageDetail(
                                                                                            Convert.ToInt64(drCurrRow["IIG_bIntId"]),
                                                                                            Convert.ToString(drCurrRow["IIG_vCharImagePath"]),
                                                                                            Convert.ToString(drCurrRow["IIG_vCharImageDescription_En"]),
                                                                                            Convert.ToString(drCurrRow["IIG_nVarImageDescription_Reg"]),
                                                                                            Convert.ToBoolean(drCurrRow["IIG_bItIsDefaImage"]))
                                                                          );
                                        }
                                    }

                                    mObjInformationFull.Informations = mLstInfos.Values.ToList();
                                }
                                else
                                    mObjInformationFull.Error = new JsonError(true, "No data found", -101);
                            }
                        }
                        else
                            mObjInformationFull.Error = new JsonError(true, "No data found", -101);
                    }
                    else
                        mObjInformationFull.Error = new JsonError(true, "No data found", -101);
                }
                else
                {
                    mObjInformationFull.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("GetEmergencyInfo", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjInformationFull.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjInformationFull, this.Context.Response);
        }


        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        //public void GetTalukas(string pStrAccessKey)
        //{
        //    TalukaDetails mObjTalukas = new TalukaDetails();
        //    try
        //    {
        //        if (pStrAccessKey == "2358-lzpf-r634-79is-rl5-449")
        //        {
        //            string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;

        //            string mStrSql = "SELECT [TM_bIntId],[TM_vCharName_En],[TM_nVarName_Reg] FROM Taluka_Master Where TM_bItIsActive = 1";
        //            DataTable dtTalukas = SqlHelper.ReadTable(mStrSql, mStrConnection, false);

        //            if (dtTalukas.Rows.Count > 0)
        //            {
        //                List<Taluka> mLstTalukas = new List<Taluka>();
        //                foreach (DataRow drTaluka in dtTalukas.Rows)
        //                {
        //                    Taluka mObjTaluka = new Taluka(Convert.ToInt64(drTaluka["TM_bIntId"]),
        //                                                                 Convert.ToString(drTaluka["TM_vCharName_En"]),
        //                                                                 Convert.ToString(drTaluka["TM_nVarName_Reg"]));
        //                    mLstTalukas.Add(mObjTaluka);
        //                }

        //                //Assign categories
        //                mObjTalukas.Error = null;
        //                mObjTalukas.Talukas = mLstTalukas;
        //            }
        //            else
        //            {
        //                mObjTalukas.Error = new JsonError(true, "No data found", -101);
        //            }
        //        }
        //        else
        //        {
        //            mObjTalukas.Error = new JsonError("Access denied to server");
        //        }
        //    }
        //    catch (Exception exError)
        //    {
        //        long pLngErr = -1;
        //        if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
        //            pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
        //        pLngErr = ReportError("GetTalukas", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

        //        mObjTalukas.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
        //    }

        //    SendJsonResponse(mObjTalukas, this.Context.Response);
        //}

        #region "Class Level Methods"

        private void SendJsonResponse(object mObjInformation, HttpResponse mObjCurrentResponse)
        {
            string mStrResponseJSon = JsonConvert.SerializeObject(mObjInformation);

            mObjCurrentResponse.AddHeader("Access-Control-Allow-Origin", "*");
            mObjCurrentResponse.ContentType = "application/json; charset=utf-8";
            mObjCurrentResponse.Write(mStrResponseJSon);
        }

        private void UpdateQueryLog(long pLngId, int pIntSentCnt, int pIntFailCnt, string pStrRecievers)
        {
            string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
            DataTable dtUpdate = SqlHelper.ReadTable("SP_AddAndModAppQueries", mStrConnection, true,
                                                    SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, -1),
                                                              SqlHelper.AddInParam("@bIntQueryId", SqlDbType.BigInt, pLngId),
                                                              SqlHelper.AddInParam("@vCharIMEI", SqlDbType.VarChar, null),
                                                              SqlHelper.AddInParam("@vCharList", SqlDbType.VarChar, pStrRecievers),
                                                              SqlHelper.AddInParam("@nVarName", SqlDbType.NVarChar, null),
                                                              SqlHelper.AddInParam("@vCharPhone1", SqlDbType.VarChar, null),
                                                              SqlHelper.AddInParam("@vCharPhone2", SqlDbType.VarChar, null),
                                                              SqlHelper.AddInParam("@vCharPhone3", SqlDbType.VarChar, null),
                                                              SqlHelper.AddInParam("@vCharEmail1", SqlDbType.VarChar, null),
                                                              SqlHelper.AddInParam("@vCharEmail2", SqlDbType.VarChar, null),
                                                              SqlHelper.AddInParam("@nVarSubject", SqlDbType.NVarChar, null),
                                                              SqlHelper.AddInParam("@nVarQryBody", SqlDbType.NVarChar, null),
                                                              SqlHelper.AddInParam("@iNtFailCnt", SqlDbType.Int, pIntFailCnt),
                                                              SqlHelper.AddInParam("@iNtSentCnt", SqlDbType.Int, pIntSentCnt));
            if (dtUpdate.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtUpdate.Rows[0][0]) > 0)
                {
                    // Updated
                }
            }
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

        #endregion "Class Level Methods"

        #region "JSON CLASSES"

        public class QueryBroadCastResult
        {
            public QueryBroadCastResult()
            {
                Error = null;
                NotifiedUsers = -1;
                UnNotifiedUsers = -1;
                TotalUsers = -1;
                QueryDate = "";
                QueryTime = "";
                QueryId = -1;
            }

            public JsonError Error { get; set; }
            public int NotifiedUsers { get; set; }
            public int UnNotifiedUsers { get; set; }
            public int TotalUsers { get; set; }
            public string QueryDate { get; set; }
            public string QueryTime { get; set; }
            public long QueryId { get; set; }
        }

        public class EmergencyInformationList
        {
            public JsonError Error { get; set; }
            public List<EmergencyInformation> Informations { get; set; }
            public long InformationVersion { get; set; }
        }

        public class EmergencyInformation
        {
            public EmergencyInformation(long pLngId, string pStrName_En, string pStrName_Reg,
                string pStrCity_En, string pStrCity_Reg, string pStrAdd_En, string pStrAdd_Reg,
                string pStrEmail, string pStrUrl, string pStrPhone1, string pStrPhone2, string pStrPhone3,
                double pDblLongitude, double pDblLatitude, string pStrPin_En, string pStrPin_Reg)
            {
                Id = pLngId;
                Name_En = pStrName_En;
                Name_Reg = pStrName_Reg;
                City_En = pStrCity_En;
                City_Reg = pStrCity_Reg;
                Address_En = pStrAdd_En;
                Address_Reg = pStrAdd_Reg;
                Email = pStrEmail;
                Website = pStrUrl;
                Phone1 = pStrPhone1;
                Phone2 = pStrPhone2;
                Phone3 = pStrPhone3;
                Longitude = pDblLongitude;
                Latitude = pDblLatitude;
                PinCode_En = pStrPin_En;
                PinCode_Reg = pStrPin_Reg;
            }

            public long Id { get; set; }
            public string Name_En { get; set; }
            public string Name_Reg { get; set; }
            public string City_En { get; set; }
            public string City_Reg { get; set; }
            public string Address_En { get; set; }
            public string Address_Reg { get; set; }
            public string Email { get; set; }
            public string Website { get; set; }
            public string Phone1 { get; set; }
            public string Phone2 { get; set; }
            public string Phone3 { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public string PinCode_En { get; set; }
            public string PinCode_Reg { get; set; }

            public List<InformationExtraDetails> Extra { get; set; }
            public List<ImageDetail> Images { get; set; }
        }

        public class ImageDetail
        {
            public ImageDetail(long pLngImgId, string pStrImagePath, string pStrImageDesc_En, string pStrImageDesc_Reg, bool pBlnIsDefa)
            {
                ImageId = pLngImgId;
                IsDefaImage = pBlnIsDefa;
                ImagePath = pStrImagePath;
                ImageDescription_En = pStrImageDesc_En;
                ImageDescription_Reg = pStrImageDesc_Reg;
            }

            public string ImagePath { get; set; }
            public string ImageDescription_En { get; set; }
            public string ImageDescription_Reg { get; set; }
            public bool IsDefaImage { get; set; }
            public long ImageId { get; set; }
        }

        public class ServicesList
        {
            /// <summary>
            /// User registeration failed to system.
            /// </summary>
            /// <param name="pObjError">Error description object.</param>
            public ServicesList(JsonError pObjError)
            {
                Error = pObjError;

                Id = null;
                Name_En = null;
                Name_Reg = null;
                CategoryId = null;
                SubCateogryId = null;
            }

            public ServicesList()
            {
                Error = null;

                Id = new List<long>();
                Name_En = new List<string>();
                Name_Reg = new List<string>();
                CategoryId = new List<long>();
                SubCateogryId = new List<long>();
            }

            public List<string> Name_En { get; set; }
            public List<string> Name_Reg { get; set; }
            public List<long> CategoryId { get; set; }
            public List<long> SubCateogryId { get; set; }
            public List<long> Id { get; set; }

            public JsonError Error { get; set; }
        }

        public class RegisterationResult
        {
            /// <summary>
            /// User registeration failed to system.
            /// </summary>
            /// <param name="pObjError">Error description object.</param>
            public RegisterationResult(JsonError pObjError)
            {
                UserRegistered = false;
                Error = pObjError;
                FranchiseDetails = null;
            }

            public RegisterationResult()
            {
                Error = null;
                UserRegistered = false;
                FranchiseDetails = null;
            }

            public bool UserRegistered { get; set; }
            public Franchise FranchiseDetails { get; set; }
            public JsonError Error { get; set; }
        }

        public class Franchise
        {
            public Franchise(string pStrNameEng, string pStrNameReg, string pStrPhone)
            {
                Name_En = pStrNameEng;
                Name_Reg = pStrNameReg;
                Phone = pStrPhone;
            }

            public string Name_En { get; set; }
            public string Name_Reg { get; set; }
            public string Phone { get; set; }
        }

        public class InformationCategories
        {
            public InformationCategories()
            {
                Error = null;
                Categories = null;
            }

            public JsonError Error { get; set; }
            public List<InfoCategory> Categories { get; set; }
        }

        public class InfoCategory
        {

            public InfoCategory()
            {
                Id = -1;
                Name_Reg = "";
                Name_Eng = "";
                ImagePath = "";
                ImageIsCssClass = false;
            }

            public InfoCategory(long pLngCatId, string pStrCatName_Eng, string pStrCatName_Reg)
            {
                Id = pLngCatId;
                Name_Eng = pStrCatName_Eng;
                Name_Reg = pStrCatName_Reg;
                ImagePath = "";
                ImageIsCssClass = false;
            }

            public InfoCategory(long pLngCatId, string pStrCatName_Eng, string pStrCatName_Reg, string pStrCat_Img, bool pBlnCat_ImgIsCss)
            {
                Id = pLngCatId;
                Name_Eng = pStrCatName_Eng;
                Name_Reg = pStrCatName_Reg;
                ImagePath = pStrCat_Img;
                ImageIsCssClass = pBlnCat_ImgIsCss;
            }

            public long Id { get; set; }
            public string Name_Eng { get; set; }
            public string Name_Reg { get; set; }
            public string ImagePath { get; set; }
            public bool ImageIsCssClass { get; set; }
        }

        public class InformationSubCategories
        {
            public InformationSubCategories()
            {
                Error = null;
                SubCategories = null;
            }

            public JsonError Error { get; set; }
            public List<InfoSubCategory> SubCategories { get; set; }
        }

        public class InfoSubCategory
        {

            public InfoSubCategory()
            {
                CategoryId = Id = -1;
                Name_Reg = "";
                Name_Eng = "";
                ImagePath = "";
                ImageIsCssClass = false;
            }

            public InfoSubCategory(long pLngCatId, long pLngSubCatId, string pStrSubCatName_Eng, string pStrSubCatName_Reg)
            {
                CategoryId = pLngCatId;
                Id = pLngSubCatId;
                Name_Eng = pStrSubCatName_Eng;
                Name_Reg = pStrSubCatName_Reg;
                ImagePath = "";
                ImageIsCssClass = false;
            }

            public InfoSubCategory(long pLngCatId, long pLngSubCatId, string pStrSubCatName_Eng, string pStrSubCatName_Reg, string pStrCat_Img, bool pBlnCat_ImgIsCss)
            {
                Id = pLngSubCatId;
                Name_Eng = pStrSubCatName_Eng;
                Name_Reg = pStrSubCatName_Reg;
                ImagePath = pStrCat_Img;
                ImageIsCssClass = pBlnCat_ImgIsCss;
            }

            public long CategoryId { get; set; }
            public long Id { get; set; }
            public string Name_Eng { get; set; }
            public string Name_Reg { get; set; }
            public string ImagePath { get; set; }
            public bool ImageIsCssClass { get; set; }
        }

        public class InformationMin
        {
            public JsonError Error { get; set; }
            public List<InformationDetailsMin> Information { get; set; }
        }

        public class InformationDetailsMin
        {

            public InformationDetailsMin(long pLngId, string pStrName_En, string pStrName_Reg, string pStrCity_En, string pStrCity_Reg, string pStrImage)
            {
                Id = pLngId;
                Name_En = pStrName_En;
                Name_Reg = pStrName_Reg;
                City_En = pStrCity_En;
                City_Reg = pStrCity_Reg;
                Default_Image = pStrImage;
            }

            public long Id { get; set; }
            public string Name_En { get; set; }
            public string Name_Reg { get; set; }
            public string City_En { get; set; }
            public string City_Reg { get; set; }
            public string Default_Image { get; set; }
        }

        public class InformationFull
        {
            public JsonError Error { get; set; }
            public InformationDetailsFull Information { get; set; }
        }

        public class InformationDetailsFull
        {

            public InformationDetailsFull(bool pBlnIsActive, int pIntInfoType, long pLngId, string pStrName_En, string pStrName_Reg,
                string pStrCity_En, string pStrCity_Reg, string pStrAdd_En, string pStrAdd_Reg,
                string pStrEmail, string pStrPhone1, string pStrPhone2, string pStrPhone3,
                double pDblLongitude, double pDblLatitude, string pStrPin_En, string pStrPin_Reg, string pWebsite)
            {
                Id = pLngId;
                Name_En = pStrName_En;
                Name_Reg = pStrName_Reg;
                City_En = pStrCity_En;
                City_Reg = pStrCity_Reg;
                Address_En = pStrAdd_En;
                Address_Reg = pStrAdd_Reg;
                Email = pStrEmail;
                Phone1 = pStrPhone1;
                Phone2 = pStrPhone2;
                Phone3 = pStrPhone3;
                Longitude = pDblLongitude;
                Latitude = pDblLatitude;
                PinCode_En = pStrPin_En;
                PinCode_Reg = pStrPin_Reg;
                Website = pWebsite;
                IsEmergency = pIntInfoType;
                IsActive = pBlnIsActive;
            }

            public long Id { get; set; }
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
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public string PinCode_En { get; set; }
            public string PinCode_Reg { get; set; }
            public string Website { get; set; }

            public int IsEmergency { get; set; } // 0 == General , 1 == Emergeny .. Added By HVB Dated 26/12/2015
            public bool IsActive { get; set; }

            public List<InformationExtraDetails> Extra { get; set; }
            public List<InformationImageGallery> Images { get; set; }
        }

        public class InformationImageGallery
        {
            public InformationImageGallery(string pStrImagePath, string pStrImageDesc_En, string pStrImageDesc_Reg)
            {
                ImagePath = pStrImagePath;
                ImageDescription_En = pStrImageDesc_En;
                ImageDescription_Reg = pStrImageDesc_Reg;
            }

            public string ImagePath { get; set; }
            public string ImageDescription_En { get; set; }
            public string ImageDescription_Reg { get; set; }
        }

        public class InformationExtraDetails
        {

            public InformationExtraDetails(string pStrLabel_En, string pStrLabel_Reg, string pStrInfo_En, string pStrInfo_Reg)
            {
                Label_En = pStrLabel_En;
                Label_Reg = pStrLabel_Reg;
                Information_En = pStrInfo_En;
                Information_Reg = pStrInfo_Reg;
            }

            public string Label_En { get; set; }
            public string Label_Reg { get; set; }
            public string Information_En { get; set; }
            public string Information_Reg { get; set; }
        }

        public class StaticOtherInformation
        {
            public JsonError Error { get; set; }
            public List<OtherInfo> Others { get; set; }
        }

        public class OtherInfo
        {

            public OtherInfo(long pLngOtherId, string pStrTitle, string pStrText,
                string pStrImgPath, long pLngInfoId, DateTime pDtModTime)
            {
                OtherInfoId = pLngOtherId;
                Title = pStrTitle;
                Text = pStrText;
                ImagePath = pStrImgPath;
                InfoId = pLngInfoId;
                ModDate = pDtModTime.ToString("yyyy-MM-dd");
                ModTime = pDtModTime.ToString("hh:mm tt");
            }

            public long OtherInfoId { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
            public string ImagePath { get; set; }
            public long InfoId { get; set; }
            public string ModDate { get; set; }
            public string ModTime { get; set; }
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

        public class AdvertisementData
        {
            public AdvertisementData()
            {
                HtmlText = "";
                LinkUrl = "";
                ImageUrl = "";
            }

            public string HtmlText { get; set; }
            public string LinkUrl { get; set; }
            public string ImageUrl { get; set; }
        }

        public class TalukaDetails
        {
            public JsonError Error { get; set; }
            public List<Taluka> Talukas { get; set; }
        }

        public class Taluka
        {

            public Taluka(long pLngId, string pStrNameEnglish, string pStrNameRegional)
            {
                Id = pLngId;
                Name_En = pStrNameEnglish;
                Name_Reg = pStrNameRegional;
            }

            public long Id { get; set; }
            public string Name_En { get; set; }
            public string Name_Reg { get; set; }

        }

        #endregion "JSON CLASSES"
    }
}
