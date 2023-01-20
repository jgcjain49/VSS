using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Object Representation Of Web Site User.
/// </summary>
[Serializable]
public class SystemUser
{
    /*
     * Private variable to hold user details.
     */
    string _UserId, _UserName, _UserDeviceId, _UserRole, _OnlineOnDevice;

    public SystemUser(string UserId, string UserName, string UserDeviceId, string UserRole,string UserOnlineDevice)
    {
        _UserId = UserId;
        _UserName = UserName;
        _UserDeviceId = UserDeviceId;
        _UserRole = UserRole;
        _OnlineOnDevice = UserOnlineDevice;
    }

    /// <summary>
    /// Gets user id within the system.
    /// </summary>
    public string UserSysId { get { return _UserId; } }

    /// <summary>
    /// Gets user name within the system.
    /// </summary>
    public string UserSysName { get { return _UserName; } }

    /// <summary>
    /// Gets users GCM Device Id within the system.
    /// </summary>
    public string UserSysDeviceId { get { return _UserDeviceId; } }

    /// <summary>
    /// Gets user role within the system.
    /// </summary>
    public string UserSysRole { get { return _UserRole; } }

    /// <summary>
    /// Gets device on which user is currently online.
    /// </summary>
    public string OnlineDevice { get { return _OnlineOnDevice; } }
}