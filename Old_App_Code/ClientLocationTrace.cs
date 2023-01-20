using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Location trace of current logged in user
/// </summary>
public class ClientLocationTrace
{
    public ClientLocationTrace()
    {
        Ip = "";
        Host = "";
        HostDomain = "";
        City = "";
        State = "";
        Country = "";
        Latitude = "";
        Longitude = "";
        ErrorDetail = "";
        UserAgent = "";
    }

    public string Ip { get; set; }
    public string Host { get; set; }
    public string HostDomain { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string ErrorDetail { get; set; }
    public string UserAgent { get; set; }
    public string ASN { get; set; }

    public string FormattedAddress()
    {
        string strAddress = String.Format("Host Details : {0} , {1} , {7} \n Address : {2}, {3}, {4} \n Lat. : {5}, Long. : {6}",Host,HostDomain,City,State,Country,Latitude,Longitude,UserAgent);
        return strAddress;
    }
}

//Added by ARV on 08 Apr 19 for logging location data etc..
public class LogData {

    //public string LoginName { get; set; }
    //public string Password { get; set; }
    //public string CompanyKey { get; set; }
    //public string LoginType { get; set; }
    public string ASN { get; set; }
    public string Host { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Ip { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string ErrorDetail { get; set; }
    public string UserAgent { get; set; }


}
