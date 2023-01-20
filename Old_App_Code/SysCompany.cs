using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Object level representation of company
/// </summary>
public class SysCompany
{
    /*
     * Private variable to hold user details.
     */
    string _DatabaseName , _DatabaseKey;
    string _CompanyName, _CompanyLogo, _UserName, _CompanyAddress, _UserID, _PerOrdComm, _PercntComm;
    long _CompId;



    public SysCompany(string DatabaseKey, string DatabaseName, string CompanyName, string CompanyLogo, long CompanyId)
	        {
                _DatabaseKey = DatabaseKey;
                _DatabaseName = DatabaseName;
                _CompId = CompanyId;
                _CompanyName = CompanyName;
                _CompanyLogo = CompanyLogo;
	        }


    public SysCompany(string DatabaseKey,string CompanyName, long CompanyId,string UserName,string Address)
            {
                _DatabaseKey = DatabaseKey;
                _CompId = CompanyId;
                _CompanyName = CompanyName;
                _UserName = UserName;
                _CompanyAddress = Address;
            }


    /// <summary>
    /// Gets name of database for the user.
    /// </summary>
    public string CompDatabaseName { get { return _DatabaseName; } }

    /// <summary>
    /// Gets key of database for the user.
    /// </summary>
    public string CompDatabaseKey { get { return _DatabaseKey; } }

    /// <summary>
    /// Gets primary key value of company in db.
    /// </summary>
    public long CompanyId { get { return _CompId; } }

    /// <summary>
    /// Gets name of company.
    /// </summary>
    public string CompanyName { get { return _CompanyName; } }

    /// <summary>
    /// Gets logo image path of company.
    /// </summary>
    public string CompanyLogo { get { return _CompanyLogo; } }

    public string UserName { get { return _UserName; } }

    public string UserID { get; set; }

    public string PerOrdComm { get; set; }

    public string PercntComm { get; set; }

    public string ComversationId { get; set; }

    public string CompanyAddress { get { return _CompanyAddress; } }

}