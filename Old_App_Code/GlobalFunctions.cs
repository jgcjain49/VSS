using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

/// <summary>
/// Holds functions which are used by whole web site
/// </summary>
public static class GlobalFunctions
{
    /// <summary>
    /// Creates List of specified type from datacolumn.
    /// </summary>
    /// <typeparam name="T">Type of data column.</typeparam>
    /// <param name="dataTable">Table containing data column.</param>
    /// <param name="columnName">Name of column to be created as list.</param>
    /// <returns>List of data of specified datacolumn.</returns>
    public static List<T> ConvertColumnToList<T>(DataTable dataTable, string columnName)
    {
        return dataTable.Rows.OfType<DataRow>().Select(dr => dr.Field<T>(columnName)).ToList<T>();
    }

    /// <summary>
    /// Function used to check if text is number.
    /// </summary>
    /// <param name="inputvalue">String text</param>
    /// <param name="AllowDot">Validate number with dot</param>
    /// <param name="AllowMinus">Validate number with - sign</param>
    /// <returns>Is a number</returns>
    public static bool IsNumeric(string inputvalue, bool AllowDot, bool AllowMinus)
    {
        if (AllowDot)
        {
            if (inputvalue.Split('.').Length - 1 == 1)
                inputvalue = inputvalue.Replace(".", "");
            else
                return false;
        }
        if (AllowMinus)
        {
            if (inputvalue.Split('-').Length - 1 == 1)
                inputvalue = inputvalue.Replace("-", "");
            else
                return false;
        }
        return (new System.Text.RegularExpressions.Regex(@"^\d+$").IsMatch(inputvalue));
    }

    public static string CreateDecryptTextSyntax(string sParamName, bool ForMsg)
    {
        if (ForMsg)
            return String.Format("CAST(DECRYPTBYPASSPHRASE('VITCO_28655035_312005_VITCO',{0}) as varchar(Max))", sParamName);
        else
            return String.Format("CAST(DECRYPTBYPASSPHRASE('VITCO_28655035_312005_VITCO',{0}) as varchar(500))", sParamName);
    }
    public static string CreateDecryptTextSyntaxWithNVarchar(string sParamName, bool ForMsg)
    {
        if (ForMsg)
            return String.Format("CAST(DECRYPTBYPASSPHRASE('VITCO_28655035_312005_VITCO',{0}) as nvarchar(Max))", sParamName);
        else
            return String.Format("CAST(DECRYPTBYPASSPHRASE('VITCO_28655035_312005_VITCO',{0}) as nvarchar(500))", sParamName);
    }

    public static string CreateEncryptTextSyntax(string sParamName, bool ForMsg, bool AddSingleQuote)
    {
        if (ForMsg)
        {
            if (AddSingleQuote)
                return String.Format("CAST(ENCRYPTBYPASSPHRASE('VITCO_28655035_312005_VITCO','{0}') as varchar(max))", sParamName.Replace("'", "''"));
            else
                return String.Format("CAST(ENCRYPTBYPASSPHRASE('VITCO_28655035_312005_VITCO',{0}) as varchar(max))", sParamName);
        }
        else
        {
            if (AddSingleQuote)
                return String.Format("CAST(ENCRYPTBYPASSPHRASE('VITCO_28655035_312005_VITCO','{0}') as varchar(500))", sParamName.Replace("'", "''"));
            else
                return String.Format("CAST(ENCRYPTBYPASSPHRASE('VITCO_28655035_312005_VITCO',{0}) as varchar(500))", sParamName);
        }
    }

    public static string VerifyLogin()
    {
        if (HttpContext.Current.Session["SystemUser"] == null)
            return "";
        else
        {
            //SystemUser user = (SystemUser)
            //return user.UserSysName;

            String user = Convert.ToString(HttpContext.Current.Session["SystemUser"]);
            return user;
        }
    }

    public static string TryParseHeader(WebResponse wr)
    {
        try
        {
            HttpWebResponse _httpwr = (HttpWebResponse)wr;
            int iStatusCode = (int)_httpwr.StatusCode;
            switch (iStatusCode)
            {
                case (int)GcmHttpStatus.GcmOk:
                    return "OK";
                case (int)GcmHttpStatus.GcmInvalidJson:
                    return "Only applies for JSON requests. Indicates that the request could not be parsed as JSON, or it contained invalid fields (for instance, passing a string where a number was expected). The exact failure reason is described in the response and the problem should be addressed before the request can be retried.";
                case (int)GcmHttpStatus.GcmAuthenticationFailure:
                    return "There was an error authenticating the sender account.";
                default:
                    if (iStatusCode >= (int)GcmHttpStatus.GcmInternalErrorStartRange && iStatusCode <= (int)GcmHttpStatus.GcmInternalErrorEndRange)
                        return "Errors in the 500-599 range (such as 500 or 503) indicate that there wa an internal error in the GCM server while trying to process the request, or that the server is temporarily unavailable (for example, because of timeouts). Sender must retry later, honoring any Retry-After header included in the response. Application servers must implement exponential back-off.";
                    else
                        return "Error not definded";
            }
        }
        catch (Exception ex)
        {
            // Log this error.
            //GlobalInfo.WriteLog(String.Format("Error while parsing header to http {0}", ex.Message));
            return null;
        }
    }

    /// <summary>
    /// Reports error to server and returns unique report error number.
    /// </summary>
    /// <param name="pStrMethodName">Name of method where error occured</param>
    /// <param name="pStrClassName">Name of class where error occured</param>
    /// <param name="pLngErrorNumber">Error number if any</param>
    /// <param name="pStrErrorType">Type of error</param>
    /// <param name="pStrDescription">Error message</param>
    /// <returns>Id of error in ServerError Table.<para>You can check the error with this id in server.</para></returns>
    public static long ReportError(string pStrMethodName, string pStrClassName, long pLngErrorNumber, string pStrErrorType, string pStrDescription, string pStrStackTrace)
    {
        try
        {
            string mStrConnection = GlobalVariables.SqlConnectionString;
            DataTable dtRegisteration = SqlHelper.ReadTable("SP_GetErrorReportNumber", mStrConnection, true,
                                                          SqlHelper.AddInParam("@vCharMethodName", SqlDbType.VarChar, pStrMethodName),
                                                          SqlHelper.AddInParam("@vCharClassName", SqlDbType.VarChar, pStrClassName), // This is name of your class where method is....Change this accordingly.
                                                          SqlHelper.AddInParam("@bIntErrorNumber", SqlDbType.BigInt, pLngErrorNumber),
                                                          SqlHelper.AddInParam("@vCharErrorType", SqlDbType.VarChar, pStrErrorType),
                                                          SqlHelper.AddInParam("@vCharDescription", SqlDbType.VarChar, pStrDescription),
                                                          SqlHelper.AddInParam("@vCharStackTrace", SqlDbType.VarChar, pStrStackTrace));
            return Convert.ToInt64(dtRegisteration.Rows[0][0]);
        }
        catch (Exception exErr)
        {
            return -1;
        }
    }

    #region oldIPandMacAddressCode
    /// <summary>
    /// Get the MAC address from agent
    /// </summary>
    /// <returns></returns>
    public static string GetMACAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        }
        return sMacAddress;
    }


    /// <summary>
    /// Get an Ip address from agent
    /// </summary>
    /// <returns></returns>
    public static string getIPFromAgent()
    {
        string ip = null;
        WebClient client = new WebClient();
        // Add a user agent header in case the requested URI contains a query.
        client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR1.0.3705;)");
        string baseurl = "http://checkip.dyndns.org/";
        Stream data = client.OpenRead(baseurl);
        StreamReader reader = new StreamReader(data);
        string s = reader.ReadToEnd();
        data.Close();
        reader.Close();
        s = s.Replace("<html><head><title></title></head><body>", "").Replace("</body></html>", "").ToString();
        int i = s.IndexOf(':');
        // Remainder of string starting at ':' then one more space that's why i added +1
        string d = s.Substring(i + 1);

        // LblIPaddress.Text = s;
        //  return   ip = s;
        return ip = d.Trim();
    }

    #endregion oldAddress


    #region ipaddressthroughC#code
    /// <summary>
    /// Get IP address of remote machine.
    /// </summary>
    /// <returns></returns>
    public static string getRemoteIP(HttpRequest Request)
    {

        string ipaddress;
        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddress == "" || ipaddress == null)
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipaddress;
    }




    /// <summary>
    /// Location Tracking using an IP address for remote machine. 
    /// </summary>
    /// <param name="ipaddress"></param>
    /// <returns></returns>
    public static string GetLocation(string ipaddress)
    {
        WebRequest rssReq = WebRequest.Create(String.Format("http://api.ipinfodb.com/v3/ip-city/?key=adf0531f133f5110bc8212687ff6017139d190484fc286343b07f4778effaf0b&ip={0}&format=json", ipaddress));

        try
        {
            WebResponse tResponse = rssReq.GetResponse();

            Stream dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();

            LocationClass objAddress = JsonConvert.DeserializeObject<LocationClass>(sResponseFromServer);


            string Strmsg = "Address : " + TraceUserLocation(objAddress.latitude, objAddress.longitude) + " (" + objAddress.countryCode + ") ZipCode : " + objAddress.zipCode + " Latitude : " + objAddress.latitude + " Longitude : " + objAddress.longitude;
            return Strmsg;
        }
        catch (Exception expErr)
        {
            return null;
        }
    }


    public class LocationClass
    {
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public string ipAddress { get; set; }
        public string countryCode { get; set; }
        public string countryName { get; set; }
        public string regionName { get; set; }
        public string cityName { get; set; }
        public string zipCode { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string timeZone { get; set; }
    }

    /// <summary>
    /// Location Tracking using an IP address for remote machine. 
    /// </summary>
    /// <param name="ipaddress"></param>
    /// <returns></returns>
    public static string TraceUserLocation(string pstrLatitude, string pstrLongitude)
    {
        WebRequest rssReq = WebRequest.Create(String.Format("http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=tru", pstrLatitude, pstrLongitude));

        try
        {
            WebResponse tResponse = rssReq.GetResponse();

            Stream dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();

            string strLocAddress;
            DataSet ds = new DataSet();
            using (StringReader stringReader = new StringReader(sResponseFromServer))
            {
                ds = new DataSet();
                ds.ReadXml(stringReader);
            }
            strLocAddress = ds.Tables["result"].Rows[0][1].ToString();

            return strLocAddress;
        }
        catch (Exception expErr)
        {
            return null;
        }
    }
    #endregion ipaddressthroughC#code


    //Insert Operation on table
    public static void saveInsertUserAction(string pStrTableName, string pStrActionText, int intTalukaId, long lngCompanyId, HttpRequest webRequest)
    {
        try
        {
            //Using SQL, SELECT CONVERT(char(15), CONNECTIONPROPERTY('client_net_address'))

            string strIpaddress = "", strAddress = "";
            if (HttpContext.Current.Session["CurrentClientLocation"] is ClientLocationTrace)
            {
                strIpaddress = ((ClientLocationTrace)HttpContext.Current.Session["CurrentClientLocation"]).Ip;
                strAddress = ((ClientLocationTrace)HttpContext.Current.Session["CurrentClientLocation"]).FormattedAddress();
            }

            string strMacAddress = GlobalFunctions.GetMACAddress();
            //string strIPAddress = GlobalFunctions.getRemoteIP(webRequest);
            //string strLocDetails = GlobalFunctions.GetLocation(strIPAddress);

            string strUserAction = pStrActionText;
            string strTableNm = pStrTableName + "_" + intTalukaId;
            DataTable dtInsertMstoreInformativeLog = new DataTable();
            Dictionary<string, string> mDicInputs = new Dictionary<string, string>();
            mDicInputs.Add("CompanyId", lngCompanyId.ToString());
            mDicInputs.Add("TalukaId", intTalukaId.ToString());
            mDicInputs.Add("TableName", strTableNm);
            mDicInputs.Add("Action", strUserAction);
            mDicInputs.Add("IpAddress", strIpaddress);
            mDicInputs.Add("MacAddress", strMacAddress);
            mDicInputs.Add("LocationDetails", strAddress);

            dtInsertMstoreInformativeLog = SqlHelper.ReadTable("SP_insertMisLogDetail", true,
            SqlHelper.AddInParam("@bIntCompId", SqlDbType.BigInt, Convert.ToInt32(mDicInputs["CompanyId"])),
            SqlHelper.AddInParam("@bIntTalukaId", SqlDbType.BigInt, intTalukaId),
            SqlHelper.AddInParam("@vCharTableNm", SqlDbType.VarChar, mDicInputs["TableName"]),
            SqlHelper.AddInParam("@vCharActionText", SqlDbType.VarChar, mDicInputs["Action"]),
            SqlHelper.AddInParam("@vCharIpAddress", SqlDbType.VarChar, mDicInputs["IpAddress"]),
            SqlHelper.AddInParam("@vCharMacAddress", SqlDbType.VarChar, mDicInputs["MacAddress"]),
            SqlHelper.AddInParam("@vCharLocation", SqlDbType.VarChar, mDicInputs["LocationDetails"]));

        }
        catch (Exception exError)
        {
            long pLngErr = -1;
            if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
            pLngErr = GlobalFunctions.ReportError("Retrieve IP,Mac Address and Location Tracking in Page Load", "SubCategoryMaster", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.Message);
        }
    }

    public static string ChkImageSize(string pstrImagePath, long maxintheight, long maxintWidth, long minintheight, long minintWidth)
    {
        try
        {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(pstrImagePath);
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            if (originalWidth > maxintWidth || originalHeight > maxintheight)
            {
                image.Dispose();
                return ("Image Upload Failed Image Size is Greater than maximum size 256!!! Select Appropriate Image to Continue");
            }
            else if (originalWidth > maxintWidth && originalHeight > maxintheight)
            {
                image.Dispose();
                return ("Image Upload Failed Image Size is Greater than maximum size 256!!! Select Appropriate Image to Continue");
            }

            else if (originalHeight < minintheight || originalWidth < minintWidth)
            {
                image.Dispose();
                return (" Image Upload Failed Image Size is smaller than maximum size 64!!! Select Appropriate Image to Continue");
            }
            image.Dispose();

        }
        catch (Exception exError)
        {
            long pLngErr = -1;
            if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
            pLngErr = GlobalFunctions.ReportError("ChkImageSize", "Global Functions", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
        }
        return "";

    }


    public static string saveImage(string destDirectory, FileUpload file, System.Drawing.Image imageSize)
    {
        if (!Directory.Exists(HttpContext.Current.Server.MapPath(destDirectory)))
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(destDirectory));
        string finalFileName = string.Format("{0}_{1}{2}",
                                        Path.GetFileNameWithoutExtension(file.FileName),
                                        DateTime.Now.ToString("yyyy-MM-dd_HHmmss"),
                                        Path.GetExtension(file.FileName));
        imageSize.Save(Path.Combine(HttpContext.Current.Server.MapPath(destDirectory), finalFileName));
        //file.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath(destDirectory), finalFileName));

        return destDirectory + "//" + finalFileName;
    }
    public static string savepdf(string destDirectory, FileUpload file)
    {
        if (!Directory.Exists(HttpContext.Current.Server.MapPath(destDirectory)))
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(destDirectory));
        string finalFileName = string.Format("{0}_{1}{2}",
                                        Path.GetFileNameWithoutExtension(file.FileName),
                                        DateTime.Now.ToString("yyyy-MM-dd_HHmmss"),
                                        Path.GetExtension(file.FileName));
        //imageSize.Save(Path.Combine(HttpContext.Current.Server.MapPath(destDirectory), finalFileName));
        //file.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath(destDirectory), finalFileName));

        return destDirectory + "/" + finalFileName;
    }
    public static string sendOTPSms(string selectedPhn, int generatedOTP)
    {
        string result = "";

        string number = "+91" + selectedPhn;
        string message = GlobalVariables.otpMsgTmplt;

        message = message.Replace("{var}", generatedOTP.ToString());// +". Please do not share this OTP with anyone.";
        string apiUrl = GlobalVariables.otpApiUrl;
        string user = GlobalVariables.otpUser;
        string key = GlobalVariables.otpKey;
        string senderid = GlobalVariables.otpSenderid;
        string accusage = GlobalVariables.otpAccusage;
        string entityid = GlobalVariables.otpEntityid;
        string tmpltid = GlobalVariables.otpTmpltid;


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

        WriteLogToFile("OTP request sent:: " + url, "Withdraw_Approval_OTP_Log");

        WebClient client = new WebClient();
        try
        {
            client.Encoding = Encoding.UTF8;
            string response = client.DownloadString(url);
            WriteLogToFile("OTP response rcvd:: " + response, "Withdraw_Approval_OTP_Log");

            if (response.ToLower().Contains("sent"))
                result = "Success";
            else
                result = "Failed";
        }
        catch (Exception ex)
        {
            WriteLogToFile("Exception sendOTPSms:: " + ex.ToString(), "Withdraw_Approval_OTP_Log");
            result = ex.ToString();
        }
        //finally
        //{
        //    string qLogOtp = " INSERT INTO JournalOTP_Log_17 ([Jopt_vCharMOBILE_NO],[Jopt_vCharOTP],[Jopt_dtGNRTD_ON]) " +
        //                     " VALUES ('" + number + "','" + generatedOTP + "','" +
        //                     DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //    try
        //    {
        //        SqlHelper.ReadTable(qLogOtp, GlobalVariables.SqlConnectionStringMstoreInformativeDb, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        result = ex.ToString();
        //    }
        //}

        return result;
    }

    public static void WriteLogToFile(string msgToLog, string fileName = "")
    {
        string strFileName = string.Format("{0}_{1}.txt", fileName == "" ? "Log" : fileName, DateTime.Now.ToString("yyyy-MM-dd"));
        string serverPath = "";

        //check if folder is present, if not create
        //if (!Directory.Exists(Server.MapPath(@"OrderDetails/")))
        //    Directory.CreateDirectory(Server.MapPath(@"OrderDetails/"));

        //if (!Directory.Exists("C:/Goldify_Logs/"))
        //    Directory.CreateDirectory("C:/Goldify_Logs/");
        //serverPath = "C:/Goldify_Logs/";

        serverPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Logs/");
        if (!Directory.Exists(serverPath))
            Directory.CreateDirectory(serverPath);

        //below code is working on local path on computer c drivce
        //StreamWriter file = new StreamWriter(Path.GetFullPath(@"OrderDetails/" + strFileName));

        StreamWriter file = new StreamWriter(serverPath + strFileName, true);
        file.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("HH:mm:ss:ms"), msgToLog.ToString()));
        file.Close();
    }

    public static int GenerateOTP(int noOfDigits)
    {
        Random randomNumGenerator = new Random();
        int OTP = randomNumGenerator.Next((int)Math.Pow(10, noOfDigits - 1), (int)Math.Pow(10, noOfDigits));
        Console.WriteLine("Random number generated is " + OTP);
        return OTP;
    }
}