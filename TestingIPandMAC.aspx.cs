using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Admin_CommTrex
{
    public partial class TestingIPandMAC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        //private LocationClass GetLocation(string ipaddress)
        //{
        //    WebRequest rssReq = WebRequest.Create(String.Format("http://api.ipinfodb.com/v3/ip-city/?key=adf0531f133f5110bc8212687ff6017139d190484fc286343b07f4778effaf0b&ip={0}&format=json", ipaddress));

        //    try
        //    {
        //        WebResponse tResponse = rssReq.GetResponse();

        //        Stream dataStream = tResponse.GetResponseStream();

        //        StreamReader tReader = new StreamReader(dataStream);

        //        String sResponseFromServer = tReader.ReadToEnd();

        //        tReader.Close();
        //        dataStream.Close();
        //        tResponse.Close();

        //        LocationClass objAddress = JsonConvert.DeserializeObject<LocationClass>(sResponseFromServer);
        //        return objAddress;
        //    }
        //    catch(Exception expErr)
        //    {
        //        return null;
        //    }
        //}

        //public class LocationClass
        //{
        //    public string statusCode { get; set; }
        //    public string statusMessage { get; set; }
        //    public string ipAddress { get; set; }
        //    public string countryCode { get; set; }
        //    public string countryName { get; set; }
        //    public string regionName { get; set; }
        //    public string cityName { get; set; }
        //    public string zipCode { get; set; }
        //    public string latitude { get; set; }
        //    public string longitude { get; set; }
        //    public string timeZone { get; set; }
        //}

        protected void btnTestAddress_ServerClick(object sender, EventArgs e)
        {
            try
            {
                //string strMac;
                //strMac = GetMAC();

                //string message = "GetMac() method O/P : " + strMac;
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("<script type = 'text/javascript'>");
                //sb.Append("window.onload=function(){");
                //sb.Append(" bootbox.alert('");
                //sb.Append(message);
                //sb.Append("')};");
                //sb.Append("</script>");
                //ClientScript.RegisterClientScriptBlock(this.GetType(), " bootbox.alert", sb.ToString());

                string ipaddress;
                //HttpRequest Request = this.Request;
                ipaddress = GlobalFunctions.getRemoteIP(Request);
                //strMac = GetMacAddress(ipaddress);

                //string message2 = "GetMacAddress() method using HTTP_X_Forwarded O/P : " + strMac;
                txtmacaddr.Text = " Ip : " + ipaddress;

                string strLocationDetails = GlobalFunctions.GetLocation(ipaddress);
                txtlocalmac.Text = strLocationDetails;

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showTestAddress", "TestIpandMAC", pLngErr, exError.GetBaseException().GetType().ToString(),exError.Message,exError.StackTrace);

            }

        }

        private string GetMAC()
        {
            string macAddresses = "";

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);
        public string GetMacAddress(string sName)
        {
            string s = string.Empty;
            try
            {
                System.Net.IPHostEntry Tempaddr = null;
                Tempaddr = (System.Net.IPHostEntry)Dns.GetHostEntry(sName);
                System.Net.IPAddress[] TempAd = Tempaddr.AddressList;
                string[] Ipaddr = new string[3];
                foreach (IPAddress TempA in TempAd)
                {
                    Ipaddr[1] = TempA.ToString();
                    byte[] ab = new byte[6];
                    int len = ab.Length;
                    int r = SendARP((int)TempA.Address, 0, ab, ref len);
                    string sMAC = BitConverter.ToString(ab, 0, 6);
                    Ipaddr[2] = sMAC;
                    s = sMAC;
                }
            }
            catch (Exception e)
            {
            txtmacaddr.Text=e.Message;
            }
            return s;
        }



    }
}