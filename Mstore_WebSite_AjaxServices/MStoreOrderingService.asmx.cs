using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace VTalk_WebApp.MStore_Informative_AppServices
{
    /// <summary>
    /// Summary description for MStoreOrderingService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MStoreOrderingService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void SearchProducts(long pLngTalukaId, string pStrPreviousProuductsList, bool pBlnIsRegionalSearch,
                                          string pStrSearchText,
                                          string pStrProdNm, string pStrProdCat, string pStrProdSubCat,
                                          double pDblPrice1, double pDblPrice2, int iNtPriceFilter,
                                          bool pBlnCheckAll, int pIntPriceSorting, string pStrAccessKey)
        {
            ProductsSearchResult mObjResult = new ProductsSearchResult();
            try
            {
                if (pStrAccessKey == "Innu204mera#*$s-de-pr656obe(2#I$*ntur")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataSet dsSearchResult = SqlHelper.ReadDataSet("SP_SearchProducts", mStrConnection, true, true,
                                                          SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId),
                                                          SqlHelper.AddInParam("@nVarSeenProdList", SqlDbType.NVarChar, pStrPreviousProuductsList),
                                                          SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pBlnIsRegionalSearch),
                                                          SqlHelper.AddInParam("@nVarBasicSearchTxt", SqlDbType.NVarChar, pStrSearchText),
                                                          SqlHelper.AddInParam("@nVarProductName", SqlDbType.NVarChar, pStrProdNm),
                                                          SqlHelper.AddInParam("@nVarProductCategory", SqlDbType.NVarChar, pStrProdCat),
                                                          SqlHelper.AddInParam("@nVarProductSubCategory", SqlDbType.NVarChar, pStrProdSubCat),
                                                          SqlHelper.AddInParam("@decPrice1", SqlDbType.Decimal, pDblPrice1),
                                                          SqlHelper.AddInParam("@decPrice2", SqlDbType.Decimal, pDblPrice2),
                                                          SqlHelper.AddInParam("@iNtPriceFilter", SqlDbType.Int, iNtPriceFilter),
                                                          SqlHelper.AddInParam("@bItCheckAllFilters", SqlDbType.Bit, pBlnCheckAll),
                                                          SqlHelper.AddInParam("@iNtSortByPrice", SqlDbType.Int, pIntPriceSorting)
                                                          );
                    if (dsSearchResult == null)
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    else if (dsSearchResult.Tables[dsSearchResult.Tables.Count - 1].Rows.Count > 0)
                    {
                        mObjResult.Error = null;
                        if (dsSearchResult.Tables.Count > 1)
                        {
                            mObjResult.FoundInformationsCount = Convert.ToInt64(dsSearchResult.Tables[0].Rows[0]["InfoCount"]);
                            mObjResult.FoundProductsCount = Convert.ToInt64(dsSearchResult.Tables[1].Rows[0]["ProdCount"]);
                        }

                        List<ProductMinDtls> mLstProducts = new List<ProductMinDtls>();
                        foreach (DataRow drProduct in dsSearchResult.Tables[dsSearchResult.Tables.Count - 1].Rows)
                        {
                            mLstProducts.Add(new ProductMinDtls(Convert.ToInt64(drProduct["ProdId"]),
                                                                Convert.ToInt64(drProduct["InfoId"]),
                                                                Convert.ToString(drProduct["InfoName"]),
                                                                Convert.ToString(drProduct["ProdName"]),
                                                                Convert.ToString(drProduct["ProdCat"]),
                                                                Convert.ToString(drProduct["ProdSubCat"]),
                                                                Convert.ToDouble(drProduct["ProdPrice"]),
                                                                Convert.ToString(drProduct["ImgPath"])));
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void GetFullProductDetails(long pLngTalukaId, long pLngProductId, string pStrAccessKey)
        {
            FullProductDetails mObjProductFullInfo = new FullProductDetails();
            try
            {
                if (pStrAccessKey == "Si-confir3585marHGi-persu#+()adeo856r$^v")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataSet dsProductDetail = SqlHelper.ReadDataSet("SP_ReadFullProductDetails", mStrConnection, true, true,
                                                          SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId),
                                                          SqlHelper.AddInParam("@bIntProductId", SqlDbType.BigInt, pLngProductId)
                                                          );
                    if (dsProductDetail == null)
                        mObjProductFullInfo.Error = new JsonError(true, "No data found", -101);
                    else if (dsProductDetail.Tables[dsProductDetail.Tables.Count - 1].Rows.Count > 0) // Last table is products so we check if products data is present
                    {
                        mObjProductFullInfo.Error = null;
                        Product mObjProduct = new Product();

                        DataTable dtProdData = dsProductDetail.Tables[0];

                        foreach (DataRow drSpecifications in dtProdData.Rows)
                        {
                            mObjProduct.AddSpecification(Convert.ToInt64(drSpecifications["PSMC_bIntCategoryId"]),
                                                         Convert.ToString(drSpecifications["PSMC_vCharCat_NameEn"]),
                                                         Convert.ToString(drSpecifications["PSMC_nVarCat_NameReg"]),
                                                         Convert.ToInt64(drSpecifications["PSSCM_bIntSubCatId"]),
                                                         Convert.ToString(drSpecifications["PSSCM_vCharSubCat_NameEn"]),
                                                         Convert.ToString(drSpecifications["PSSCM_nVarSubCat_NameReg"]),
                                                         Convert.ToInt64(drSpecifications["PSDM_bIntDetailId"]),
                                                         Convert.ToString(drSpecifications["PSDM_vCharValue_En"]),
                                                         Convert.ToString(drSpecifications["PSDM_nVarValue_Reg"]));
                        }

                        mObjProduct.SetSpecification();

                        dtProdData = dsProductDetail.Tables[1];

                        mObjProduct.Product_Id = Convert.ToInt64(dtProdData.Rows[0]["PM_bIntProdId"]);
                        mObjProduct.Name_En = Convert.ToString(dtProdData.Rows[0]["PM_vCharProdName"]);
                        mObjProduct.Name_Reg = Convert.ToString(dtProdData.Rows[0]["PM_nVarProdName"]);
                        mObjProduct.ActualPrice = Convert.ToDouble(dtProdData.Rows[0]["PM_decActualPrice"]);
                        mObjProduct.DiscountedPrice = Convert.ToDouble(dtProdData.Rows[0]["PM_decDiscountPrice"]);
                        mObjProduct.Discription_En = Convert.ToString(dtProdData.Rows[0]["PM_vCharProdDesc"]).Replace("\\n", "<br/>"); // Modified to replace \n
                        mObjProduct.Discription_Reg = Convert.ToString(dtProdData.Rows[0]["PM_nVarProdDesc"]).Replace("\\n", "<br/>"); // Modified to replace \n

                        List<ProductImages> mLstProdImgs = new List<ProductImages>();

                        for (int mIntImg = 0; mIntImg < dtProdData.Rows.Count; mIntImg++)
                        {
                            if (Convert.ToBoolean(dtProdData.Rows[mIntImg]["PI_bItIsDefaultImage"]))
                                mObjProduct.DefaultImageIndex = mIntImg;
                            mLstProdImgs.Add(new ProductImages(
                                                                Convert.ToString(dtProdData.Rows[mIntImg]["PI_vCharImgDesc"]),
                                                                Convert.ToString(dtProdData.Rows[mIntImg]["PI_nVarImgDesc"]),
                                                                Convert.ToString(dtProdData.Rows[mIntImg]["PI_vCharImgPath"]),
                                                                Convert.ToBoolean(dtProdData.Rows[mIntImg]["PI_bItIsDefaultImage"]),
                                                                Convert.ToInt32(dtProdData.Rows[mIntImg]["PI_iNtImgWidth"]),
                                                                Convert.ToInt32(dtProdData.Rows[mIntImg]["PI_iNtImgHeight"])
                                                              )
                                            );
                        }

                        mObjProduct.Images = mLstProdImgs;
                        mObjProductFullInfo.Product = mObjProduct;
                    }
                    else
                        mObjProductFullInfo.Error = new JsonError(true, "No data found", -101);
                }
                else
                {
                    mObjProductFullInfo.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("SearchProductsBasic", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjProductFullInfo.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjProductFullInfo, this.Context.Response);
        }

        [WebMethod] // This method can be accessed over internet.
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void ReadAndUpdateCart(long pLngTalukaId, string pStrProductIdList, string pStrAccessKey)
        {
            CartItemDetails mObjDetails = new CartItemDetails();
            try
            {
                string mStrConnectionStringToUse = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                if (pStrAccessKey == "$hhb-vir3_5dvj-=8364_4jj40_i83wj")
                {
                    string mStrQuery = "SP_GetProdDetailsForOrderCart";
                    DataTable dtDetails = SqlHelper.ReadTable(mStrQuery, mStrConnectionStringToUse, true,
                                                    SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId),
                                                    SqlHelper.AddInParam("@vCharProdList", SqlDbType.VarChar, pStrProductIdList));

                    if (dtDetails.Rows.Count > 0)
                    {
                        mObjDetails.Error = null;

                        List<CartItem> mLstData = new List<CartItem>();

                        foreach (DataRow drProductData in dtDetails.Rows)
                        {
                            mLstData.Add(new CartItem(Convert.ToInt64(drProductData["PM_bIntProdId"]),
                                                      Convert.ToBoolean(drProductData["PM_bItIsActive"]),
                                                      (Convert.ToDouble(drProductData["PM_decDiscountPrice"]) > 0 ? Convert.ToDouble(drProductData["PM_decDiscountPrice"]) : Convert.ToDouble(drProductData["PM_decActualPrice"])),
                                                      Convert.ToString(drProductData["PI_vCharImgPath"]),
                                                      Convert.ToString(drProductData["PM_vCharProdName"]),
                                                      Convert.ToString(drProductData["PM_nVarProdName"])
                                                     )
                                        );
                        }

                        mObjDetails.CartProds = mLstData;
                    }
                    else
                        mObjDetails.Error = new JsonError(true, "No data found", -101);
                }
                else
                {
                    // unauthorized access to method.
                    mObjDetails.Error = new JsonError("Access denied to server");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("ReadAndUpdateCart", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjDetails.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjDetails, this.Context.Response);

        }

        [WebMethod] // This method can be accessed over internet.
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void PlaceOrder(long pLngTalukaId, string pStrOrderParameterJson, string pStrMethodAuthKey)
        {
            // TODO : Check Expiry.
            OrderSubmitionDetails mObjSubmissionDetails = new OrderSubmitionDetails();

            try
            {
                OrderParameters mObjOrderDetailFull = JsonConvert.DeserializeObject<OrderParameters>(pStrOrderParameterJson);
                string mStrConnectionStringToUse = GlobalVariables.SqlConnectionStringMstoreInformativeDb;


                if (pStrMethodAuthKey == "lkenvg5=Uu=j8@*vb@lnmej^_8476-_dok<3" && mStrConnectionStringToUse != "")
                {
                    string mStrInsertQuery = String.Format("Insert Into Orders_{0}(Ord_vCharClntId,Ord_dblTotalAmount,Ord_dtOrderTime,", pLngTalukaId);
                    mStrInsertQuery += "Ord_nVcharContactName,Ord_nVcharContactAddress1,Ord_nVcharContactAddress2,";
                    mStrInsertQuery += "Ord_nVcharContactPhoneNumber,Ord_vCharContactEmailAddress,Ord_vCharContactPinCode,Ord_iNtOrderStatus)  ";
                    mStrInsertQuery += " Output inserted.Ord_bIntId,inserted.Ord_dtOrderTime ";
                    mStrInsertQuery += " Values(@clntid,@totamt,CAST(SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30') As DATETIME),@name,@addr1,@addr2,";
                    mStrInsertQuery += "@phn,@eml,@pin,0)     ";
                    DataTable dtInserted = SqlHelper.ReadTable(String.Format(mStrInsertQuery, pLngTalukaId), mStrConnectionStringToUse, false,
                                                    SqlHelper.AddInParam("@clntid", SqlDbType.VarChar, mObjOrderDetailFull.ClientId),
                                                    SqlHelper.AddInParam("@totamt", SqlDbType.Decimal, mObjOrderDetailFull.OrderTotalCost),
                                                    SqlHelper.AddInParam("@name", SqlDbType.NVarChar, mObjOrderDetailFull.OrderContactDetails.ContactName),
                                                    SqlHelper.AddInParam("@addr1", SqlDbType.NVarChar, mObjOrderDetailFull.OrderContactDetails.PrimaryAddress),
                                                    SqlHelper.AddInParam("@addr2", SqlDbType.NVarChar, mObjOrderDetailFull.OrderContactDetails.SecondaryAddress),
                                                    SqlHelper.AddInParam("@phn", SqlDbType.VarChar, mObjOrderDetailFull.OrderContactDetails.PhoneNumber),
                                                    SqlHelper.AddInParam("@eml", SqlDbType.VarChar, mObjOrderDetailFull.OrderContactDetails.EmailAddress),
                                                    SqlHelper.AddInParam("@pin", SqlDbType.VarChar, mObjOrderDetailFull.OrderContactDetails.PinCode)
                                                    );

                    if (dtInserted.Rows.Count > 0)
                    {
                        int mIntInsertCnt = 0;

                        mStrInsertQuery = "Insert into OrderSub_{0}(OrdSub_bIntOrdId,OrdSub_bIntProdId,";
                        mStrInsertQuery += "OrdSub_decQty,OrdSub_decBasicAmt,OrdSub_nVarRemark)";
                        mStrInsertQuery += " Values(@oid,@pid,@qty,@amt,@rmk)";
                        mStrInsertQuery = String.Format(mStrInsertQuery, pLngTalukaId);
                        SqlCommand cmd = SqlHelper.GetCommand(mStrInsertQuery, mStrConnectionStringToUse);

                        cmd.Parameters.Add(SqlHelper.AddInParam("@oid", SqlDbType.BigInt, dtInserted.Rows[0]["Ord_bIntId"]));
                        cmd.Parameters.Add("@pid", SqlDbType.BigInt);
                        cmd.Parameters.Add("@qty", SqlDbType.Decimal);
                        cmd.Parameters.Add("@amt", SqlDbType.Decimal);
                        cmd.Parameters.Add("@rmk", SqlDbType.NVarChar);

                        SqlHelper.OpenSafeConnection(cmd.Connection);

                        foreach (OrderDetail ordDetail in mObjOrderDetailFull.OrderDetails)
                        {
                            cmd.Parameters["@pid"].Value = ordDetail.ProudctId;
                            cmd.Parameters["@qty"].Value = ordDetail.ProductQty;
                            cmd.Parameters["@amt"].Value = ordDetail.ProudctBasicCost;
                            cmd.Parameters["@rmk"].Value = ordDetail.ExtraOrderRemark;
                            if (cmd.ExecuteNonQuery() > 0)
                                mIntInsertCnt++;
                        }

                        SqlHelper.CloseSafeConnection(cmd.Connection);

                        if (mIntInsertCnt == mObjOrderDetailFull.OrderDetails.Count)
                        {
                            mObjSubmissionDetails.Error = null;
                            mObjSubmissionDetails.OrderId = Convert.ToInt64(dtInserted.Rows[0]["Ord_bIntId"]);
                            mObjSubmissionDetails.OrderDate = Convert.ToDateTime(dtInserted.Rows[0]["Ord_dtOrderTime"]).ToString("dd-MMM-yyyy");
                            mObjSubmissionDetails.OrderTime = Convert.ToDateTime(dtInserted.Rows[0]["Ord_dtOrderTime"]).ToString("hh:mm:ss tt");

                            // Run send email async here
                            //Thread asyncBackEmailThread = new Thread(delegate()
                            //{
                            //    SendAsyncMail(pStrCompanyKey, Convert.ToInt64(dtInserted.Rows[0]["Ord_bIntId"]));
                            //});
                            //asyncBackEmailThread.IsBackground = true;
                            //asyncBackEmailThread.Start();
                        }
                        else
                        {
                            mObjSubmissionDetails.Error = new JsonError(mIntInsertCnt == 0 ? "Failed to add sub details." : "Failed to few items in ordering.");
                        }
                    }
                    else
                    {
                        //mObjError.IsRecieved = false;
                        mObjSubmissionDetails.Error = new JsonError("Failed to add order.");
                    }
                }
                else
                {
                    // unauthorized access to method.
                    mObjSubmissionDetails.Error = new JsonError("You are not authorized to access SERVER 1.0");
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = ReportError("PlaceOrder", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjSubmissionDetails.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjSubmissionDetails, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void SearchProductsBasic(long pLngTalukaId, string pStrPreviousProuductsList, bool pBlnIsRegionalSearch, string pStrSearchText, int pIntPriceSorting, string pStrAccessKey)
        {
            ProductsSearchResult mObjResult = new ProductsSearchResult();
            try
            {
                if (pStrAccessKey == "men$di-ai2ent-co3mpa=s-sio4^3n")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataSet dsSearchResult = SqlHelper.ReadDataSet("SP_SearchProducts", mStrConnection, true, true,
                                                          SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId),
                                                          SqlHelper.AddInParam("@nVarSeenProdList", SqlDbType.NVarChar, pStrPreviousProuductsList),
                                                          SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pBlnIsRegionalSearch),
                                                          SqlHelper.AddInParam("@nVarBasicSearchTxt", SqlDbType.NVarChar, pStrSearchText),
                                                          SqlHelper.AddInParam("@nVarProductName", SqlDbType.NVarChar, ""),
                                                          SqlHelper.AddInParam("@nVarProductCategory", SqlDbType.NVarChar, ""),
                                                          SqlHelper.AddInParam("@nVarProductSubCategory", SqlDbType.NVarChar, ""),
                                                          SqlHelper.AddInParam("@decPrice1", SqlDbType.Decimal, 0.00),
                                                          SqlHelper.AddInParam("@decPrice2", SqlDbType.Decimal, 0.00),
                                                          SqlHelper.AddInParam("@iNtPriceFilter", SqlDbType.Int, -1),
                                                          SqlHelper.AddInParam("@bItCheckAllFilters", SqlDbType.Bit, false),
                                                          SqlHelper.AddInParam("@iNtSortByPrice", SqlDbType.Int, pIntPriceSorting)
                                                          );
                    if (dsSearchResult == null)
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    else if (dsSearchResult.Tables[dsSearchResult.Tables.Count - 1].Rows.Count > 0)
                    {
                        mObjResult.Error = null;
                        if (dsSearchResult.Tables.Count > 1)
                        {
                            mObjResult.FoundInformationsCount = Convert.ToInt64(dsSearchResult.Tables[0].Rows[0]["InfoCount"]);
                            mObjResult.FoundProductsCount = Convert.ToInt64(dsSearchResult.Tables[1].Rows[0]["ProdCount"]);
                        }

                        List<ProductMinDtls> mLstProducts = new List<ProductMinDtls>();
                        foreach (DataRow drProduct in dsSearchResult.Tables[dsSearchResult.Tables.Count - 1].Rows)
                        {
                            mLstProducts.Add(new ProductMinDtls(Convert.ToInt64(drProduct["ProdId"]),
                                                                Convert.ToInt64(drProduct["InfoId"]),
                                                                Convert.ToString(drProduct["InfoName"]),
                                                                Convert.ToString(drProduct["ProdName"]),
                                                                Convert.ToString(drProduct["ProdCat"]),
                                                                Convert.ToString(drProduct["ProdSubCat"]),
                                                                Convert.ToDouble(drProduct["ProdPrice"]),
                                                                Convert.ToString(drProduct["ImgPath"])));
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void SearchProductsAdvaced(long pLngTalukaId, string pStrPreviousProuductsList, bool pBlnIsRegionalSearch,
                                          string pStrProdNm, string pStrProdCat, string pStrProdSubCat,
                                          double pDblPrice1, double pDblPrice2, int iNtPriceFilter,
                                          bool pBlnCheckAll, int pIntPriceSorting, string pStrAccessKey)
        {
            ProductsSearchResult mObjResult = new ProductsSearchResult();
            try
            {
                if (pStrAccessKey == "Innu204mera#*$s-de-pr656obe(2#I$*ntur")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataSet dsSearchResult = SqlHelper.ReadDataSet("SP_SearchProducts", mStrConnection, true, true,
                                                          SqlHelper.AddInParam("@nVarTalukaId", SqlDbType.NVarChar, pLngTalukaId),
                                                          SqlHelper.AddInParam("@nVarSeenProdList", SqlDbType.NVarChar, pStrPreviousProuductsList),
                                                          SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pBlnIsRegionalSearch),
                                                          SqlHelper.AddInParam("@nVarBasicSearchTxt", SqlDbType.NVarChar, ""),
                                                          SqlHelper.AddInParam("@nVarProductName", SqlDbType.NVarChar, pStrProdNm),
                                                          SqlHelper.AddInParam("@nVarProductCategory", SqlDbType.NVarChar, pStrProdCat),
                                                          SqlHelper.AddInParam("@nVarProductSubCategory", SqlDbType.NVarChar, pStrProdSubCat),
                                                          SqlHelper.AddInParam("@decPrice1", SqlDbType.Decimal, pDblPrice1),
                                                          SqlHelper.AddInParam("@decPrice2", SqlDbType.Decimal, pDblPrice2),
                                                          SqlHelper.AddInParam("@iNtPriceFilter", SqlDbType.Int, pIntPriceSorting),
                                                          SqlHelper.AddInParam("@bItCheckAllFilters", SqlDbType.Bit, pBlnCheckAll),
                                                          SqlHelper.AddInParam("@iNtSortByPrice", SqlDbType.Int, pIntPriceSorting)
                                                          );
                    if (dsSearchResult == null)
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    else if (dsSearchResult.Tables[dsSearchResult.Tables.Count - 1].Rows.Count > 0)
                    {
                        mObjResult.Error = null;
                        if (dsSearchResult.Tables.Count > 1)
                        {
                            mObjResult.FoundInformationsCount = Convert.ToInt64(dsSearchResult.Tables[0].Rows[0]["InfoCount"]);
                            mObjResult.FoundProductsCount = Convert.ToInt64(dsSearchResult.Tables[1].Rows[0]["ProdCount"]);
                        }

                        List<ProductMinDtls> mLstProducts = new List<ProductMinDtls>();
                        foreach (DataRow drProduct in dsSearchResult.Tables[dsSearchResult.Tables.Count - 1].Rows)
                        {
                            mLstProducts.Add(new ProductMinDtls(Convert.ToInt64(drProduct["ProdId"]),
                                                                Convert.ToInt64(drProduct["InfoId"]),
                                                                Convert.ToString(drProduct["InfoName"]),
                                                                Convert.ToString(drProduct["ProdName"]),
                                                                Convert.ToString(drProduct["ProdCat"]),
                                                                Convert.ToString(drProduct["ProdSubCat"]),
                                                                Convert.ToDouble(drProduct["ProdPrice"]),
                                                                Convert.ToString(drProduct["ImgPath"])));
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

        
        //Code added by SSK dated 22-09-2015
        #region SSKMobileCode 
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void SearchShops(long pLngTalukaId, string pStrSearchWord, bool pBlnRegional, string pStrAccessKey)
        {
            ShopSearchResult mObjResult = new ShopSearchResult();
            try
            {
                //int intIsReg = 0;
                //if (pBlnRegional == true) intIsReg = 1;

                if (pStrAccessKey == "iam204king#*$s-of-all656gir(2#I$*ls")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataTable dtshoplist = SqlHelper.ReadTable("SP_SearchSHOPLIST", mStrConnection, true,
                        SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, Convert.ToInt32(pLngTalukaId)),
                        SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pBlnRegional),
                        SqlHelper.AddInParam("@nVarShopName", SqlDbType.NVarChar, pStrSearchWord));

                    List<ShopMinDetails> mLstShops = new List<ShopMinDetails>();
                    if (dtshoplist.Rows.Count > 0)
                    {
                        foreach (DataRow drShops in dtshoplist.Rows)
                        {
                            mLstShops.Add(new ShopMinDetails(Convert.ToInt64(drShops["IM_bIntInfoId"]),
                                Convert.ToString(drShops["IM_vCharInfoName_En"]),
                                Convert.ToString(drShops["IM_vCharCity_En"]),
                                Convert.ToString(drShops["IIG_vCharImagePath"])));

                        }
                        mObjResult.FoundShop = mLstShops;
                        mObjResult.FoundShopCount = dtshoplist.Rows.Count;
                    }
                    else
                    {
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    }
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
                pLngErr = ReportError("SearchShops", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjResult.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjResult, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void DisplayShopList(long pLngTalukaId, bool pBlnRegional, string pStrAccessKey)
        {
            ShopSearchResult mObjResult = new ShopSearchResult();
            try
            {

                if (pStrAccessKey == "we87*2##0are@%fool83##ish")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataTable dtshoplist = SqlHelper.ReadTable("SP_displaySHOPLIST", mStrConnection, true,
                        SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, Convert.ToInt32(pLngTalukaId)),
                        SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pBlnRegional));

                    List<ShopMinDetails> mLstShops = new List<ShopMinDetails>();
                    if (dtshoplist.Rows.Count > 0)
                    {
                        foreach (DataRow drShops in dtshoplist.Rows)
                        {
                            mLstShops.Add(new ShopMinDetails(Convert.ToInt64(drShops["IM_bIntInfoId"]),
                                Convert.ToString(drShops["IM_vCharInfoName_En"]),
                                Convert.ToString(drShops["IM_vCharCity_En"]),
                                Convert.ToString(drShops["IIG_vCharImagePath"])));

                        }
                        mObjResult.FoundShop = mLstShops;
                        mObjResult.FoundShopCount = dtshoplist.Rows.Count;
                    }
                    else
                    {
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    }
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
                pLngErr = ReportError("DisplayShopList", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjResult.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjResult, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void DisplayShopListDescending(long pLngTalukaId,string pstrShopNameList, bool pBlnRegional,bool pBlnOrder, string pStrAccessKey)
        {
            ShopSearchResult mObjResult = new ShopSearchResult();
            try
            {
                //int intIsReg = 0;
                //if (pBlnRegional == true) intIsReg = 1;

                if (pStrAccessKey == "we87*2##0are@%fool83##ish")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataTable dtshoplist = SqlHelper.ReadTable("SP_displayMoreSHOPLISTDescending", mStrConnection, true,
                        SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, Convert.ToInt32(pLngTalukaId)),
                        SqlHelper.AddInParam("@bItOrder", SqlDbType.Bit, pBlnOrder),
                        SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pBlnRegional),
                        SqlHelper.AddInParam("@vcharShopNumberId", SqlDbType.NVarChar, pstrShopNameList));

                    List<ShopMinDetails> mLstShops = new List<ShopMinDetails>();
                    if (dtshoplist.Rows.Count > 0)
                    {
                        foreach (DataRow drShops in dtshoplist.Rows)
                        {
                            mLstShops.Add(new ShopMinDetails(Convert.ToInt64(drShops["IM_bIntInfoId"]),
                                Convert.ToString(drShops["IM_vCharInfoName_En"]),
                                Convert.ToString(drShops["IM_vCharCity_En"]),
                                Convert.ToString(drShops["IIG_vCharImagePath"])));

                        }
                        mObjResult.FoundShop = mLstShops;
                        mObjResult.FoundShopCount = dtshoplist.Rows.Count;
                    }
                    else
                    {
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    }
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
                pLngErr = ReportError("DisplayShopListDescending", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjResult.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjResult, this.Context.Response);
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void DisplayMoreShopList(long pLngTalukaId, string pstrShopNameList, bool pBlnRegional, string pStrAccessKey)
        {
            ShopSearchResult mObjResult = new ShopSearchResult();
            try
            {

                if (pStrAccessKey == "Mai#@34#@78#hu@D90On))see!23$%")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataTable dtshoplist = SqlHelper.ReadTable("SP_displayMoreSHOPLIST", mStrConnection, true,
                        SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, Convert.ToInt32(pLngTalukaId)),
                        SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pBlnRegional),
                        SqlHelper.AddInParam("@vcharShopNumberId", SqlDbType.VarChar, pstrShopNameList));

                    List<ShopMinDetails> mLstShops = new List<ShopMinDetails>();
                    if (dtshoplist.Rows.Count > 0)
                    {
                        foreach (DataRow drShops in dtshoplist.Rows)
                        {
                            mLstShops.Add(new ShopMinDetails(Convert.ToInt64(drShops["IM_bIntInfoId"]),
                                Convert.ToString(drShops["IM_vCharInfoName_En"]),
                                Convert.ToString(drShops["IM_vCharCity_En"]),
                                Convert.ToString(drShops["IIG_vCharImagePath"])));

                        }
                        mObjResult.FoundShop = mLstShops;
                        mObjResult.FoundShopCount = dtshoplist.Rows.Count;
                    }
                    else
                    {
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    }
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
                pLngErr = ReportError("DisplayMoreShopList", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjResult.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjResult, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void DisplayProductList(long pLngInformationId, long pLngTalukaId, bool pBlnRegional,bool pBlnOrder,string pStrSearchWord,string pStrProductIdList,string pStrAccessKey)
        {
            ProductCategorySearchResult mObjResult = new ProductCategorySearchResult();
            try
            {

                if (pStrAccessKey == "poke2@#27$#mon9$is(*&(gr21$$test")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataTable dtshoplist = SqlHelper.ReadTable("SP_getProductCategoryDetails", mStrConnection, true,
                        SqlHelper.AddInParam("@bintInformationid", SqlDbType.BigInt, Convert.ToInt32(pLngInformationId)),
                        SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, Convert.ToInt32(pLngTalukaId)),
                        SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pBlnRegional),
                        SqlHelper.AddInParam("@bItIsOrder", SqlDbType.Bit, pBlnOrder),
                        SqlHelper.AddInParam("@nVarShopName", SqlDbType.NVarChar, pStrSearchWord),
                        SqlHelper.AddInParam("@vcharProductCategoryNumberId", SqlDbType.NVarChar, pStrProductIdList));

                    
                    List<ProductCategoryDetails> mLstShops = new List<ProductCategoryDetails>();
                    if (dtshoplist.Rows.Count > 0)
                    {
                        foreach (DataRow drShops in dtshoplist.Rows)
                        {
                            mLstShops.Add(new ProductCategoryDetails(Convert.ToInt64(drShops["PC_bIntCategoryId"]),
                                Convert.ToString(drShops["PC_vCharCatName"]),
                                Convert.ToString(drShops["PC_vCharCatImgPath"])));

                        }
                        mObjResult.FoundProduct = mLstShops;
                        mObjResult.FoundProductCount = dtshoplist.Rows.Count;
                    }
                    else
                    {
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    }
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
                pLngErr = ReportError("DisplayProductList", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjResult.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjResult, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void DisplayMoreProductList(long pLngInformationId, long pLngTalukaId, bool pBlnRegional, bool pBlnOrder, string pStrSearchWord, string pstrProductNameList, string pStrAccessKey)
        {
            ProductCategorySearchResult mObjResult = new ProductCategorySearchResult();
            try
            {

                if (pStrAccessKey == "i3(&*2am@#sor#ry(6ws75aShakti484maan")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataTable dtshoplist = SqlHelper.ReadTable("SP_displayMoreProductCategoryLIST", mStrConnection, true,
                        SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, Convert.ToInt32(pLngTalukaId)),
                        SqlHelper.AddInParam("@bintInformationid", SqlDbType.BigInt, Convert.ToInt32(pLngInformationId)),
                        SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pBlnRegional),
                        SqlHelper.AddInParam("@vcharProductCategoryNumberId", SqlDbType.NVarChar, pstrProductNameList),
                        SqlHelper.AddInParam("@nVarShopName", SqlDbType.NVarChar, pStrSearchWord));

                    List<ProductCategoryDetails> mLstShops = new List<ProductCategoryDetails>();
                    if (dtshoplist.Rows.Count > 0)
                    {
                        foreach (DataRow drShops in dtshoplist.Rows)
                        {
                            mLstShops.Add(new ProductCategoryDetails(Convert.ToInt64(drShops["PC_bIntCategoryId"]),
                                Convert.ToString(drShops["PC_vCharCatName"]),
                                Convert.ToString(drShops["PC_vCharCatImgPath"])));

                        }
                        mObjResult.FoundProduct = mLstShops;
                        mObjResult.FoundProductCount = dtshoplist.Rows.Count;
                    }
                    else
                    {
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    }
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
                pLngErr = ReportError("DisplayMoreProductList", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjResult.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjResult, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void DisplayProductSubCategoryList(long pLngInformationId,long pLngProductCatId,long pLngTalukaId, bool pBlnRegional, bool pBlnOrder, string pStrSearchWord,string pStrProductIdList, string pStrAccessKey)
        {
            ProductCategorySearchResult mObjResult = new ProductCategorySearchResult();
            try
            {

                if (pStrAccessKey == "iam21##4@))Num23@#ber4Save&^#me")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataTable dtshoplist = SqlHelper.ReadTable("SP_getProductSubCategoryDetails", mStrConnection, true,
                        SqlHelper.AddInParam("@bintInformationid", SqlDbType.BigInt, Convert.ToInt32(pLngInformationId)),
                        SqlHelper.AddInParam("@bintCategoryProductid", SqlDbType.BigInt, Convert.ToInt32(pLngProductCatId)),
                        SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, Convert.ToInt32(pLngTalukaId)),
                        SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pBlnRegional),
                        SqlHelper.AddInParam("@bItIsOrder", SqlDbType.Bit, pBlnOrder),
                        SqlHelper.AddInParam("@nVarShopName", SqlDbType.NVarChar, pStrSearchWord),
                        SqlHelper.AddInParam("@vcharProductSubCategoryNumberId", SqlDbType.NVarChar, pStrProductIdList));

                    List<ProductCategoryDetails> mLstShops = new List<ProductCategoryDetails>();
                    if (dtshoplist.Rows.Count > 0)
                    {
                        foreach (DataRow drShops in dtshoplist.Rows)
                        {
                            mLstShops.Add(new ProductCategoryDetails(Convert.ToInt64(drShops["PSC_bIntSubCategoryId"]),
                                Convert.ToString(drShops["PSC_vCharSubCatName"]),
                                Convert.ToString(drShops["PSC_vCharSubCatImgPath"])));

                        }
                        mObjResult.FoundProduct = mLstShops;
                        mObjResult.FoundProductCount = dtshoplist.Rows.Count;
                    }
                    else
                    {
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    }
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
                pLngErr = ReportError("DisplayProductSubCategoryList", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjResult.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjResult, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void DisplayMoreProductSubCategoryList(long pLngInformationId,long pLngProductCatId, long pLngTalukaId, bool pBlnRegional, bool pBlnOrder, string pstrProductNameList, string pStrAccessKey)
        {
            ProductCategorySearchResult mObjResult = new ProductCategorySearchResult();
            try
            {

                if (pStrAccessKey == "ar34&row^67^%season4*((Trai32ler#22#$nice")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataTable dtshoplist = SqlHelper.ReadTable("SP_displayMoreProductSubCategoryLIST", mStrConnection, true,
                        SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, Convert.ToInt32(pLngTalukaId)),
                        SqlHelper.AddInParam("@bintInformationid", SqlDbType.BigInt, Convert.ToInt32(pLngInformationId)),
                        SqlHelper.AddInParam("@bintCategoryid", SqlDbType.BigInt, Convert.ToInt32(pLngProductCatId)),
                        SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pBlnRegional),
                        SqlHelper.AddInParam("@vcharProductSubCategoryNumberId", SqlDbType.VarChar, pstrProductNameList));

                    List<ProductCategoryDetails> mLstShops = new List<ProductCategoryDetails>();
                    if (dtshoplist.Rows.Count > 0)
                    {
                        foreach (DataRow drShops in dtshoplist.Rows)
                        {
                            mLstShops.Add(new ProductCategoryDetails(Convert.ToInt64(drShops["PSC_bIntSubCategoryId"]),
                                Convert.ToString(drShops["PSC_vCharSubCatName"]),
                                Convert.ToString(drShops["PSC_vCharSubCatImgPath"])));

                        }
                        mObjResult.FoundProduct = mLstShops;
                        mObjResult.FoundProductCount = dtshoplist.Rows.Count;
                    }
                    else
                    {
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    }
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
                pLngErr = ReportError("DisplayMoreProductSubCategoryList", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjResult.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjResult, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void DisplayProductNamesList(long pLngProductCatId, long pLngProductSubCatId, long pLngTalukaId, bool pBlnRegional, bool pBlnOrder, string pStrSearchWord,string pStrProductIdList, string pStrAccessKey)
        {
            ProductNamesSearchResult mObjResult = new ProductNamesSearchResult();
            try
            {

                if (pStrAccessKey == "Meg32@#7an)(&fox^%342is&%57^Lo09ve")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataTable dtshoplist = SqlHelper.ReadTable("SP_getProductDetails", mStrConnection, true,
                        SqlHelper.AddInParam("@bintSubCategoryProductid", SqlDbType.BigInt, Convert.ToInt32(pLngProductSubCatId)),
                        SqlHelper.AddInParam("@bintCategoryProductid", SqlDbType.BigInt, Convert.ToInt32(pLngProductCatId)),
                        SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, Convert.ToInt32(pLngTalukaId)),
                        SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pBlnRegional),
                        SqlHelper.AddInParam("@bItIsOrder", SqlDbType.Bit, pBlnOrder),
                        SqlHelper.AddInParam("@nVarShopName", SqlDbType.NVarChar, pStrSearchWord),
                        SqlHelper.AddInParam("@vcharProductNumberId", SqlDbType.NVarChar, pStrProductIdList));

                    List<ProductViewDetails> mLstShops = new List<ProductViewDetails>();
                    if (dtshoplist.Rows.Count > 0)
                    {
                        foreach (DataRow drShops in dtshoplist.Rows)
                        {
                            mLstShops.Add(new ProductViewDetails(Convert.ToInt64(drShops["PM_bIntProdId"]),
                                Convert.ToString(drShops["PM_vCharProdName"]),
                                Convert.ToDecimal(drShops["PM_decDiscountPrice"]),
                                Convert.ToDecimal(drShops["PM_decActualPrice"]),
                                Convert.ToString(drShops["PI_vCharImgPath"])));

                        }
                        mObjResult.FoundProducts = mLstShops;
                        mObjResult.FoundProductsCount = dtshoplist.Rows.Count;
                    }
                    else
                    {
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    }
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
                pLngErr = ReportError("DisplayProductNamesList", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjResult.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjResult, this.Context.Response);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Make return type as json.
        public void DisplayMoreProductNamesList(long pLngProductCatId, long pLngProductSubCatId, long pLngTalukaId, bool pBlnRegional, bool pBlnOrder, string pStrSearchWord,string pstrProductNameList, string pStrAccessKey)
        {
            ProductNamesSearchResult mObjResult = new ProductNamesSearchResult();
            try
            {

                if (pStrAccessKey == "Ni87ki$4(*ta(*(is**bes56Tv46Seri53$$^&37es")
                {
                    string mStrConnection = GlobalVariables.SqlConnectionStringMstoreInformativeDb;
                    DataTable dtshoplist = SqlHelper.ReadTable("SP_getMoreProductDetails", mStrConnection, true,
                        SqlHelper.AddInParam("@bintSubCategoryProductid", SqlDbType.BigInt, Convert.ToInt32(pLngProductSubCatId)),
                        SqlHelper.AddInParam("@bintCategoryProductid", SqlDbType.BigInt, Convert.ToInt32(pLngProductCatId)),
                        SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, Convert.ToInt32(pLngTalukaId)),
                        SqlHelper.AddInParam("@bItIsRegionalSearch", SqlDbType.Bit, pBlnRegional),
                        SqlHelper.AddInParam("@bItIsOrder", SqlDbType.Bit, pBlnOrder),
                        SqlHelper.AddInParam("@nVarShopName", SqlDbType.NVarChar, pStrSearchWord),
                        SqlHelper.AddInParam("@vcharProductNumberId", SqlDbType.VarChar, pstrProductNameList));

                    List<ProductViewDetails> mLstShops = new List<ProductViewDetails>();
                    if (dtshoplist.Rows.Count > 0)
                    {
                        foreach (DataRow drShops in dtshoplist.Rows)
                        {
                            mLstShops.Add(new ProductViewDetails(Convert.ToInt64(drShops["PM_bIntProdId"]),
                                Convert.ToString(drShops["PM_vCharProdName"]),
                                Convert.ToDecimal(drShops["PM_decDiscountPrice"]),
                                Convert.ToDecimal(drShops["PM_decActualPrice"]),
                                Convert.ToString(drShops["PI_vCharImgPath"])));

                        }
                        mObjResult.FoundProducts = mLstShops;
                        mObjResult.FoundProductsCount = dtshoplist.Rows.Count;
                    }
                    else
                    {
                        mObjResult.Error = new JsonError(true, "No data found", -101);
                    }
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
                pLngErr = ReportError("DisplayMoreProductNamesList", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

                mObjResult.Error = new JsonError(false, "Something went wrong on server side", pLngErr);
            }

            SendJsonResponse(mObjResult, this.Context.Response);
        }
        #endregion SSKMobileCode

        #region "Class Level Methods"

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

        #endregion "Class Level Methods"

        #region "JSON CLASSES"

        public class OrderSubmitionDetails
        {
            public JsonError Error { get; set; }
            public long OrderId { get; set; }
            public string OrderDate { get; set; }
            public string OrderTime { get; set; }
        }

        public class OrderContactDetails
        {
            public string ContactName { get; set; }
            public string PrimaryAddress { get; set; }
            public string SecondaryAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string EmailAddress { get; set; }
            public string PinCode { get; set; }
        }

        public class OrderDetail
        {
            public object ProudctId { get; set; }
            public double ProductQty { get; set; }
            public double ProudctBasicCost { get; set; }
            public string ExtraOrderRemark { get; set; }
        }

        public class OrderParameters
        {
            public string ClientId { get; set; }
            public double OrderTotalCost { get; set; }
            public OrderContactDetails OrderContactDetails { get; set; }
            public List<OrderDetail> OrderDetails { get; set; }
        }

        public class CartItemDetails
        {
            public CartItemDetails()
            {
                Error = null;
                CartProds = null;
            }

            public JsonError Error { get; set; }
            public List<CartItem> CartProds { get; set; }
        }

        public class CartItem
        {
            public CartItem()
            {
                ProductId = -1;
                ProductActive = false;
                ProductPrice = 0.0;
                ProdImg = "";
                ProductName_En = "";
                ProductName_Reg = "";
            }

            public CartItem(long pLngId,bool pBlnIsActive,double pDblPrice,string pStrImg,string pStrName_En,string pStrName_Reg)
            {
                ProductId = pLngId;
                ProductActive = pBlnIsActive;
                ProductPrice = pDblPrice;
                ProdImg = pStrImg;
                ProductName_En = pStrName_En;
                ProductName_Reg = pStrName_Reg;
            }

            public long ProductId { get; set; }
            public bool ProductActive { get; set; }
            public double ProductPrice { get; set; }
            public string ProdImg { get; set; }
            public string ProductName_En { get; set; }
            public string ProductName_Reg { get; set; }
        }

        public class ProductViewDetails
        {
            public ProductViewDetails(long plngid, string pstrShopname, decimal pDblPrice, decimal pDblActualPrice, string pstrImgPath)
            {
                Id = plngid;
                Name = pstrShopname;
                Price = pDblPrice;
                ActualPrice = pDblActualPrice;
                ImgPath = pstrImgPath;
            }

            public long Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public decimal ActualPrice { get; set; }
            public string ImgPath { get; set; }
        }

        public class ProductNamesSearchResult
        {
            public ProductNamesSearchResult()
            {
                Error = null;
                FoundProductsCount = 0;
                FoundProducts = null;
            }

            public JsonError Error { get; set; }
            public long FoundProductsCount { get; set; }
            public List<ProductViewDetails> FoundProducts { get; set; }
        }

        public class ShopSearchResult
        {
            public ShopSearchResult()
            {
                Error = null;
                FoundShopCount = 0;
                FoundShop = null;
            }

            public JsonError Error { get; set; }
            public long FoundShopCount { get; set; }
            public List<ShopMinDetails> FoundShop { get; set; }
        }

        public class ShopMinDetails
        {
            public ShopMinDetails(long plngid, string pstrShopname, string pstrCityName, string pstrImgPath)
            {
                Id = plngid;
                Name = pstrShopname;
                City = pstrCityName;
                ImgPath = pstrImgPath;
            }

            public long Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public string ImgPath { get; set; }
        }

        public class ProductCategorySearchResult
        {
            public ProductCategorySearchResult()
            {
                Error = null;
                FoundProductCount = 0;
                FoundProduct = null;
            }

            public JsonError Error { get; set; }
            public long FoundProductCount { get; set; }
            public List<ProductCategoryDetails> FoundProduct { get; set; }
        }

        public class ProductCategoryDetails
        {
            public ProductCategoryDetails(long plngid, string pstrShopname, string pstrImgPath)
            {
                Id = plngid;
                Name = pstrShopname;
                ImgPath = pstrImgPath;
            }

            public long Id { get; set; }
            public string Name { get; set; }
            public string ImgPath { get; set; }
        }

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

        public class FullProductDetails
        {
            public JsonError Error { get; set; }
            public Product Product { get; set; }
        }

        public class Product
        {
            public Product()
            {
                Product_Id = 0;
                Name_En = "";
                Name_Reg = "";
                Category = null;
                Sub_Category = null;
                ActualPrice = 0.00;
                DiscountedPrice = 0.00;
                DiscountPercentage = 0;
                Discription_En = "";
                Discription_Reg = "";
                Images = null;
                DefaultImageIndex = -1;
                Specifications = null;
                AddedSpecifications = new Dictionary<long, ProductSpecifications>();
            }


            public long Product_Id { get; set; }
            public string Name_En { get; set; }
            public string Name_Reg { get; set; }
            public ProductCategory Category { get; set; }
            public ProductSubCategory Sub_Category { get; set; }
            public double ActualPrice { get; set; }
            public double DiscountedPrice { get; set; }
            public int DiscountPercentage { get; set; }
            public string Discription_En { get; set; }
            public string Discription_Reg { get; set; }
            public List<ProductImages> Images { get; set; }
            public int DefaultImageIndex { get; set; }
            public List<ProductSpecifications> Specifications { get; set; }

            private Dictionary<long, ProductSpecifications> AddedSpecifications;

            public void AddSpecification(long CategoryId, string Category_En, string Category_Reg,
                                         long SubCategoryId, string SubCategory_En, string SubCategory_Reg,
                                         long SpecificationValId, string SpecificationValue_En, string SpecificationValue_Reg)
            {
                ProductSpecifications mObjSpecifications = null;

                if (AddedSpecifications.ContainsKey(CategoryId))
                    mObjSpecifications = AddedSpecifications[CategoryId];
                else
                {
                    mObjSpecifications = new ProductSpecifications(CategoryId, Category_En, Category_Reg);
                    AddedSpecifications.Add(CategoryId, mObjSpecifications);
                }

                mObjSpecifications.SubSpecifications.Add(new SubSpecification(SubCategoryId, SubCategory_En, SubCategory_Reg, SpecificationValId, SpecificationValue_En, SpecificationValue_Reg));

            }

            public void SetSpecification()
            {
                Specifications = AddedSpecifications.Values.ToList();
            }

        }

        public class ProductSpecifications
        {
            public ProductSpecifications(long pLngSpecificationId, string pStrSpeNm_En, string pStrSpeNm_Reg)
            {
                MainSpecification_Id = pLngSpecificationId;
                MainSpecification_En = pStrSpeNm_En;
                MainSpecification_Reg = pStrSpeNm_Reg;
                SubSpecifications = new List<SubSpecification>();
            }

            public long MainSpecification_Id { get; set; }
            public string MainSpecification_En { get; set; }
            public string MainSpecification_Reg { get; set; }
            public List<SubSpecification> SubSpecifications { get; set; }
        }

        public class SubSpecification
        {

            public SubSpecification(long pLngSubCategoryId, string pStrSubCategoryName_En, string pStrSubCategoryName_Reg,
                                    long pLngSpecificationValue_Id, string pStrSpecificationValue_En, string pStrSpecificationValue_Reg)
            {
                SubSpecificationId = pLngSubCategoryId;
                SubSpecificationLbl_En = pStrSubCategoryName_En;
                SubSpecificationLbl_Reg = pStrSubCategoryName_Reg;
                SubSpecificationValId = pLngSpecificationValue_Id;
                SubSpecificationVal_En = pStrSpecificationValue_En;
                SubSpecificationVal_Reg = pStrSpecificationValue_Reg;
            }

            public long SubSpecificationId { get; set; }
            public string SubSpecificationLbl_En { get; set; }
            public string SubSpecificationLbl_Reg { get; set; }
            public long SubSpecificationValId { get; set; }
            public string SubSpecificationVal_En { get; set; }
            public string SubSpecificationVal_Reg { get; set; }
        }

        public class ProductCategory
        {

            public ProductCategory()
            {
                Category_Id = -1;
                Name_En = "";
                Name_Reg = "";
                Image_Path = "";
            }

            public long Category_Id { get; set; }
            public string Name_En { get; set; }
            public string Name_Reg { get; set; }
            public string Image_Path { get; set; }
        }

        public class ProductSubCategory
        {
            public ProductSubCategory()
            {
                Sub_Category_Id = 0;
                Name_En = "";
                Name_Reg = "";
                Image_Path = "";
            }

            public long Sub_Category_Id { get; set; }
            public string Name_En { get; set; }
            public string Name_Reg { get; set; }
            public string Image_Path { get; set; }
        }

        public class ProductImages
        {
            public ProductImages()
            {
                Description_En = "";
                Description_Reg = "";
                Path = "";
                IsDefault = false;
                Width = 0;
                Height = 0;
            }

            public ProductImages(string Des_En, string Des_Reg, string Img_Path, bool IsDefa, int ImgWidth, int ImgHeight)
            {
                Description_En = Des_En;
                Description_Reg = Des_Reg;
                Path = Img_Path;
                IsDefault = IsDefa;
                Width = ImgWidth;
                Height = ImgHeight;
            }

            public string Description_En { get; set; }
            public string Description_Reg { get; set; }
            public string Path { get; set; }
            public bool IsDefault { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
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
