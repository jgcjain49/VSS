using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
/// <summary>
/// This class contains GlobalVariables that are used on web site.
/// </summary>
public static class GlobalVariables
{

    // A session variable is meant to be per user. 
    // A static variable is a variable that will be shared between all users.
    // So while using static variable be careful

    #region "SqlConnectionString"

    static string _SqlConnInstance;
    static string _SqlConnId;
    static string _SqlConnPass;
    static string _SqlConnectionString;



    public static string SqlConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["Mstore_2705ConnectionString"].ConnectionString;
        }

    }

    public static string SqlConnectionUserName
    {
        get
        {
            return _SqlConnId;
        }
        set
        {
            _SqlConnId = value;
        }
    }

    public static string SqlConnectionUserPass
    {
        get
        {
            return _SqlConnPass;
        }
        set
        {
            _SqlConnPass = value;
        }
    }

    public static string SqlConnectionInstance
    {
        get
        {
            return _SqlConnInstance;
        }
        set
        {
            _SqlConnInstance = value;
        }
    }

    #endregion "SqlConnectionString"
    public static string FCMServerKey { get { return ConfigurationManager.AppSettings["FCMKey"]; } }
    static public string GCMServerUrl { get { return @"https://android.googleapis.com/gcm/send"; } }
    static public string GCMApiKey { get { return "AIzaSyBm_GnNovNxAQESzVQsCUVR1hb-nR5ivUw"; } }

    static public string SqlConnectionStringMstoreInformativeDb { get { return ConfigurationManager.ConnectionStrings["Mstore_2705ConnectionString"].ConnectionString; } }
    //static public string SqlConnectionStringMstoreInformativeDb { get { return ConfigurationManager.ConnectionStrings["MStoreInformative"].ConnectionString; } }
    static public string NotificationImagePath { get { return ConfigurationManager.AppSettings["NotificationImagePath"]; } }
    static public string SAdvertisementsImagePath { get { return ConfigurationManager.AppSettings["AdvertiseImagePath"]; } }

    static public string InformationDetailsImagePath { get { return ConfigurationManager.AppSettings["InformationImagePath"]; } }
    static public string CategoryDetailImagePath { get { return ConfigurationManager.AppSettings["CategoryImagePath"]; } }
    static public string SubCategoryDetailImagePath { get { return ConfigurationManager.AppSettings["SubCategoryImagePath"]; } }
    static public string InternalChatAttachmentPath { get { return ConfigurationManager.AppSettings["InternalChatAttachmentPath"]; } }
    static public string DoveDistriAttachment { get { return ConfigurationManager.AppSettings["DoveDistriAttachment"]; } }
    //added for ICIC API by Bejoy_10032021
    public static string icici_bal_fetch_test { get { return "bbCDEWSE4XZrCmqGmTiOdIKg3GBJ4l2x"; } }
    public static string icici_bal_fetch_live { get { return "nJXiGN39jp2hDaop0rkIvKbeCewuCkOv"; } }
    public static string icici_balchk_certificate_test { get { return "~/Security/ICICI_PUBLIC_CERT_BALCHK_UAT.txt"; } }
    public static string icici_balchk_certificate_live { get { return "~/Security/ICICI_PUBLIC_CERT_BALCHK_PROD.txt"; } }

    public static string icici_imps_api_test { get { return "AGFi15ZAA3r6AZZApF1zbVUgVGn2IIEw"; } }
    public static string icici_imps_api_live { get { return "geHtzBb8N92FJA2VoODxc3l2oAEy2XgS"; } }
    public static string icici_imps_bcid_live { get { return "IBCGOL00637"; } }
    public static string icici_imps_pass_code_live { get { return "82f88b05ce06447a8b607dd7586308b0"; } }
    public static string icici_imps_imps_url_live { get { return "https://apibankingone.icicibank.com/api/v1/imps/p2a"; } }
    public static string icici_imps_certificate_test { get { return "~/Security/ICICI_publiccert_UAT.txt"; } }
    public static string icici_imps_certificate_live { get { return "~/Security/ICICI_publiccert_prod.txt"; } }
    //added by Bejoy_10032021 for encyption-decryption private key (user/client)
    public static string private_key_secret { get { return "goldify"; } }

    // hardik  Otp generate code 2023-01-06
    public static string otpApiUrl { get { return Convert.ToString(ConfigurationManager.AppSettings["otpApiUrl"]); } }
    public static string otpUser { get { return Convert.ToString(ConfigurationManager.AppSettings["otpUser"]); } }
    public static string otpKey { get { return Convert.ToString(ConfigurationManager.AppSettings["otpKey"]); } }
    public static string otpSenderid { get { return Convert.ToString(ConfigurationManager.AppSettings["otpSenderid"]); } }
    public static string otpAccusage { get { return Convert.ToString(ConfigurationManager.AppSettings["otpAccusage"]); } }
  
    public static string otpEntityid { get { return Convert.ToString(ConfigurationManager.AppSettings["otpEntityid"]); } }
    //public static string otpTmpltid { get { return Convert.ToString(ConfigurationManager.AppSettings["otpTmpltid"]); } }


    //added for invoice template by Pritesh_20210913
    public static string invoiceTemplatePath { get { return ConfigurationManager.AppSettings["invoiceTmpltPath"]; } }

    //added for SMS by Pritesh_20210919
    public static string smsApiUrl { get { return Convert.ToString(ConfigurationManager.AppSettings["otpApiUrl"]); } }
    public static string smsUser { get { return Convert.ToString(ConfigurationManager.AppSettings["otpUser"]); } }
    public static string smsKey { get { return Convert.ToString(ConfigurationManager.AppSettings["otpKey"]); } }
    public static string smsSenderid { get { return Convert.ToString(ConfigurationManager.AppSettings["otpSenderid"]); } }
    public static string smsAccusage { get { return Convert.ToString(ConfigurationManager.AppSettings["otpAccusage"]); } }
    public static string smsEntityid { get { return Convert.ToString(ConfigurationManager.AppSettings["otpEntityid"]); } }
    public static string otpTmpltid { get { return Convert.ToString(ConfigurationManager.AppSettings["otpTmpltid"]); } }
    public static string otpMsgTmplt { get { return Convert.ToString(ConfigurationManager.AppSettings["otpMsgTmplt"]); } }
    public static string ordAcptMsgTmplt { get { return Convert.ToString(ConfigurationManager.AppSettings["ordAcptMsgTmplt"]); } }
    public static string ordRjctMsgTmplt { get { return Convert.ToString(ConfigurationManager.AppSettings["ordRjctMsgTmplt"]); } }

    public static string UserImgPath { get { return ConfigurationManager.AppSettings["UserImgPath"]; } }

    public static string compressedImgQuality { get { return ConfigurationManager.AppSettings["compressedImgQuality"]; } }
    public static Dictionary<string, string> gstStateCode = new Dictionary<string, string>()
    {
        { "Andhra Pradesh", "37" },
        { "Arunachal Pradesh", "12" },
        { "Assam", "18" },
        { "Bihar", "10" },
        { "Chattisgarh", "22" },
        { "Delhi", "7" },
        { "Goa", "30" },
        { "Gujarat", "24" },
        { "Haryana", "6" },
        { "Himachal Pradesh", "2" },
        { "Jammu and Kashmir", "1" },
        { "Jharkhand", "20" },
        { "Karnataka", "29" },
        { "Kerala", "32" },
        { "Lakshadweep Islands", "31" },
        { "Madhya Pradesh", "23" },
        { "Maharashtra", "27" },
        { "Manipur", "14" },
        { "Meghalaya", "17" },
        { "Mizoram", "15" },
        { "Nagaland", "13" },
        { "Odisha", "21" },
        { "Pondicherry", "34" },
        { "Punjab", "3" },
        { "Rajasthan", "8" },
        { "Sikkim", "11" },
        { "Tamil Nadu", "33" },
        { "Telangana", "36" },
        { "Tripura", "16" },
        { "Uttar Pradesh", "9" },
        { "Uttarakhand", "5" },
        { "West Bengal", "19" },
        { "Andaman and Nicobar Islands", "35" },
        { "Chandigarh", "4" },
        { "Dadra & Nagar Haveli and Daman & Diu", "26" },
        { "Ladakh", "38" },
        { "Other Territory", "97" }        
    };

    static public string FileProdCatHostPath
    {
        get
        {

            return System.Configuration.ConfigurationManager.AppSettings["FullProductcatHostPath"];
        }

    }

    static public string TempFileProdCatPath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["TempProdCatPath"];
        }
    }
    static public string FileBlogHostPath
    {
        get
        {

            return System.Configuration.ConfigurationManager.AppSettings["FullBlogHostPath"];
        }

    }

    static public string TempFileBlogPath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["TempBlogPath"];
        }
    }


    static public string FileProdSubCatHostPath
    {
        get
        {

            return System.Configuration.ConfigurationManager.AppSettings["FullProductSubCatHostPath"];
        }
    }
    static public string BannerPath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["BannerPath"];
        }

    }

    static public string TempFileProdSubCatPath
    {
        get
        {

            return System.Configuration.ConfigurationManager.AppSettings["TempProdSubCatPath"];
        }
    }


    static public string FileProducts
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["Product"];
        }
    }


    static public string FileTempProducts
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["TempProduct"];
        }
    }

    static public string FileHostPath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["FullFileHostPath"];
        }

    }


    static public string SubcatFileHostPath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["FullSubcatHostPath"];
        }

    }

    static public string ContentFileHostPath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["ContentFileHostPath"];
        }

    }

    static public string ServerLocalMachineDomain { get; set; }

    static public string updateImage1 { get; set; }
    static public string updateImage2 { get; set; }

    static public string ServerSharedUploadPath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["ServerSharedUploadPath"];
        }
    }

    static public string TemporaryPath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["TempPath"];
        }
    }

    static public string SubCategoryTemporaryPath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["SubCatTempPath"];
        }
    }

    static public string InformationTemporaryPath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["InformationTempPath"];
        }
    }

    static public string NoImagePath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["DefaultImage"];
        }
    }

    static public string ServerMachineUserId
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["ServerUserId"];
        }
    }

    static public string ServerMachinePassword
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["ServerPassword"];
        }
    }

    static public string GoldifyKYCPath { get { return ConfigurationManager.AppSettings["GoldifyKYCPath"]; } }
}

public enum UserIdListType
{
    OnlyId,
    ExcludeId,
    OnlyIdList,
    ExcludeIdList
}

public enum MessageType
{
    SingleChatMsg,
    MultiCastMsg,
    BroadCastMsg,
    SingleFileMsg,
    MultiCastFileMsg
}

public enum GcmHttpStatus
{
    GcmOk = 200,
    GcmInvalidJson = 400,
    GcmAuthenticationFailure = 401,
    GcmInternalErrorStartRange = 500,
    GcmInternalErrorEndRange = 599
}

public class ProdEmailDet
{
    public string ProdImg;
    public string ProdName;
    public string ProdQty;
    public string ProdAmt;
    public decimal TotalAmt;
}