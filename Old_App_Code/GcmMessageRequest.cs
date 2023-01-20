using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Creates a Json Formated Message To sent over gcm.
/// </summary>
public class GcmMessageRequest
{

    #region "Data members"

    // list of messages
    //
    private string _Messages = null;
    private int _MessageId = -1;
    private string _MessageType = null;

    // file details null if message is not file type message.
    //
    private ExtraFileDetails _FileDetails = null;

    // list of senders.
    //
    private string _Sender = null;

    // name of sender over server.
    // 
    private string _SenderName = null;

    // list of recievers.
    //
    private Dictionary<string, string> _Recievers = null;

    // offer message object
    //
    private ExtraOfferMessage _ExtraOfferMessage = null;

    // query message object
    //
    private ExtraQueryMessage _ExtraQueryMessage = null;

    #endregion

    #region "Constructors"

    /// <summary>
    /// Initialize object of gcm message class.
    /// </summary>
    public GcmMessageRequest()
    {
        _Recievers = new Dictionary<string, string>();
        this.DelayMessageIfClientNotActive = false;
        this.MessageBunchId = "";
        this.HoldTimeOfMessageOverGcm = 2419200; // 4 Weeks
        this.MsgDate = "";
        this.MsgTime = "";
    }

    #endregion

    #region "Class properties for message handling"

    /// <summary>
    /// Get or set Server date when message was created.
    /// </summary>
    public string MsgDate { get; set; }

    /// <summary>
    /// Get or set server time when message was created.
    /// </summary>
    public string MsgTime { get; set; }

    /// <summary>
    /// Used to bunch messages if they are seprated.
    /// <para>If not specified then set to Current Date time.</para>
    /// </summary>
    public string MessageBunchId { get; set; }

    /// <summary>
    /// Time in seconds for which gcm should hold message,
    /// <para>till it is not forwared to client.</para>
    /// <para>default 4 Weeks.</para>
    /// </summary>
    public int HoldTimeOfMessageOverGcm { get; set; }

    /// <summary>
    /// Hold message over gcm if client device not active.
    /// <para>Default is false.</para>
    /// </summary>
    public bool DelayMessageIfClientNotActive { get; set; }

    #endregion

    /// <summary>
    /// Sets message to be sent.
    /// </summary>
    /// <param name="pStrMessage">Message to be sent.</param>
    /// <param name="pIntMessageId">Id of message over server.</param>
    /// <param name="pStrMessageType">Type of message.</param>
    public void SetMessage(string pStrMessage, int pIntMessageId, string pStrMessageType)
    {
        _Messages = pStrMessage;
        _MessageId = pIntMessageId;
        _MessageType = pStrMessageType;
    }

    #region "Reciever Handler Methods"

    /// <summary>
    /// Adds reciever into list of recievers with device id.
    /// <para>One id can be added only once.</para>
    /// </summary>
    /// <param name="strRecieverId">Id of reciever over server.</param>
    /// <param name="strDeviceId">Reciever's device id.</param>
    /// <returns>Index where reciver details was added.</returns>
    public int AddReciever(string strRecieverId, string strDeviceId)
    {
        _Recievers.Add(strRecieverId, strDeviceId);
        return _Recievers.Count - 1;
    }

    /// <summary>
    /// Clears all recievers.
    /// </summary>
    public void ClearRecievers()
    {
        _Recievers.Clear();
    }

    /// <summary>
    /// Removes reciver from reciever list.
    /// </summary>
    /// <param name="iIndex">Index from where reciever should be removed.</param>
    public void RemoveReciever(int iIndex)
    {
        string strKey = _Recievers.Keys.ToList<string>()[iIndex];
        _Recievers.Remove(strKey);
    }

    /// <summary>
    /// Remove reciver from message list.
    /// </summary>
    /// <param name="strRecieverId">Reciver to be removed.</param>
    public void RemoveReciever(string strRecieverId)
    {
        _Recievers.Remove(strRecieverId);
    }

    /// <summary>
    /// Set a predefined dictonary as reciever list.
    /// </summary>
    /// <param name="dicReciever">Predefined recivers list.
    /// <para>Dictonary's Key should be reciever id.</para>
    /// <para>Dictonary's Value should be reciever's device id.</para>
    /// </param>
    public void SetRecieverList(Dictionary<string, string> dicReciever)
    {
        _Recievers = dicReciever;
    }

    public string GetReciverId(int iIndex)
    {
        return _Recievers.Keys.ToList<string>()[iIndex];
    }

    public string GetRecieverDeviceId(int iIndex)
    {
        return _Recievers.Values.ToList<string>()[iIndex];
    }

    #endregion

    public void SetMessageOffer(string pStrOfferTitle, string pStrOfferText, string pStrOfferImageUrl)
    {
        if (_ExtraOfferMessage == null)
            _ExtraOfferMessage = new ExtraOfferMessage(pStrOfferTitle, pStrOfferText, pStrOfferImageUrl);
        else
        {
            _ExtraOfferMessage.OfferTitle = pStrOfferTitle;
            _ExtraOfferMessage.OfferImageUrl = pStrOfferImageUrl;
            _ExtraOfferMessage.OfferText = pStrOfferText;
        }
    }

    public void SetQueryMessage(string pStrName,
                                 string pStrPhone1, string pStrPhone2, string pStrPhone3,
                                 string pStrEmail1, string pStrEmail2,
                                 string pStrSubject, string pStrQryBody)
    {
        if (_ExtraQueryMessage == null)
            _ExtraQueryMessage = new ExtraQueryMessage(pStrName, pStrPhone1, pStrPhone2, pStrPhone3,
                                                       pStrEmail1, pStrEmail2, pStrSubject, pStrQryBody);
        else
        {
            _ExtraQueryMessage.ContactName = pStrName;
            _ExtraQueryMessage.Phone1 = pStrPhone1;
            _ExtraQueryMessage.Phone2 = pStrPhone2;
            _ExtraQueryMessage.Phone3 = pStrPhone3;
            _ExtraQueryMessage.Email1 = pStrEmail1;
            _ExtraQueryMessage.Email2 = pStrEmail2;
            _ExtraQueryMessage.Subject = pStrSubject;
            _ExtraQueryMessage.Query = pStrQryBody;
        }
    }

    /// <summary>
    /// Sets sender who's sending the message.
    /// </summary>
    /// <param name="pStrSenderId">Id of sender over server.</param>
    /// <param name="pStrSenderName">Name of sender over server.</param>
    public void SetSender(string pStrSenderId, string pStrSenderName)
    {
        _Sender = pStrSenderId;
        _SenderName = pStrSenderName;
    }

    /// <summary>
    /// Creates and adds extra details in message about files.
    /// </summary>
    /// <param name="pStrFileSize">Size of file.</param>
    /// <param name="pStrFileUrl">Url of file over server.</param>
    public void SetAttachmentDetails(string pStrFileSize, string pStrFileUrl)
    {
        _FileDetails = new ExtraFileDetails();
        _FileDetails.FileSize = pStrFileSize;
        _FileDetails.FileUri = pStrFileUrl;
    }


    #region "Main Serialize method"

    /// <summary>
    /// Returns json formated string for gcm push.
    /// </summary>
    /// <returns>Json Msg In format specified by GCM.</returns>
    public string CreateJsonMsg()
    {
        if (_Recievers.Count == 0)
            throw new Exception("No recievers were added to recivers list");
        if (_Sender == null)
            throw new Exception("No senders were added to senders list");
        if (_Messages == null)
            throw new Exception("No message was added to messages list");
        if (_MessageType == null)
            throw new Exception("Message type was not specified");
        if (_MessageId == -1)
            throw new Exception("Message type was not specified");

        GCM_PUSH_MSG jsonParent = new GCM_PUSH_MSG();

        jsonParent.collapse_key = this.MessageBunchId == "" ? DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss_tt") : this.MessageBunchId;
        jsonParent.delay_while_idle = this.DelayMessageIfClientNotActive;
        jsonParent.registration_ids = this._Recievers.Values.ToList<string>();
        jsonParent.time_to_live = this.HoldTimeOfMessageOverGcm;

        Data msgdata = new Data();

        if (_FileDetails == null)
        {
            // if no file details specified don't add extra load
            //
            msgdata.MessageHasExtras = false; // no extras attached.
            msgdata.MessageExtras = null; // Blank array.
            //msgdata.MessageExtras = new List<MessageExtra>(); // Blank array.
        }
        else
        {
            // We are adding file details as extra overload of message.

            List<MessageExtra> extraDetails = new List<MessageExtra>();

            MessageExtra extraLoad = new MessageExtra();
            extraLoad.DataLoadType = "ExtraDataLoadType_ExtraFileDetails";
            extraLoad.JsonDataLoad = JsonConvert.SerializeObject(_FileDetails);

            extraDetails.Add(extraLoad);

            msgdata.MessageHasExtras = true;
            msgdata.MessageExtras = extraDetails;
        }

        if (_ExtraOfferMessage != null)
        {
            if (msgdata.MessageHasExtras == false)
                msgdata.MessageExtras = new List<MessageExtra>();

            msgdata.MessageHasExtras = true; // no extras attached.

            MessageExtra extraOffers = new MessageExtra();
            extraOffers.DataLoadType = "ExtraDataLoadType_ExtraOfferDetails";
            extraOffers.JsonDataLoad = JsonConvert.SerializeObject(_ExtraOfferMessage);

            msgdata.MessageExtras.Add(extraOffers);
        }

        if (_ExtraQueryMessage != null)
        {
            if (msgdata.MessageHasExtras == false)
                msgdata.MessageExtras = new List<MessageExtra>();

            msgdata.MessageHasExtras = true; // no extras attached.

            MessageExtra extraQuery = new MessageExtra();
            extraQuery.DataLoadType = "ExtraDataLoadType_ExtraQueryDetails";
            extraQuery.JsonDataLoad = JsonConvert.SerializeObject(_ExtraQueryMessage);

            msgdata.MessageExtras.Add(extraQuery);
        }

        msgdata.MessageId = this._MessageId;
        msgdata.MessageText = this._Messages;
        msgdata.MessageType = this._MessageType;

        msgdata.SenderId = this._Sender;
        msgdata.SenderName = this._SenderName;

        msgdata.MessageDate = this.MsgDate != "" ? this.MsgDate : DateTime.Now.ToString("dd/MMMM/yyyy");
        msgdata.MessageTime = this.MsgTime != "" ? this.MsgTime : DateTime.Now.ToString("hh:mm:ss tt");

        jsonParent.data = msgdata;
        //jsonParent.data 

        return JsonConvert.SerializeObject(jsonParent);
    }

    #endregion

    #region "Inner classes for GCM JSon Structure"

    //
    // WARNING !! 
    // DO NOT MODIFY THIS CLASSES
    //
    //

    /* 
     * Classes Created With
     * http://json2csharp.com/
     * And Android Docs for GCM Push
     * 
     * U can also use Deserializer to create this classes.
     */

    // Child class of GCM_PUSH_MSG
    // Strucuture modified on 28/Apr/2015
    // To make structure more customizable
    // 

    #region "Json Structure"

    /*
         {
            "MessageType": "PSH_TEXT_MSG",              // Type of message
            "MessageId": 15,                            // Id of message on server
            "MessageText": "Hi..\r\nGood morning sir",  // Message body .. can be message or link or details or blank depending on message type
            "SenderId": "hitesh",                       // Senders id on server machine
            "SenderName":"Hitesh Bhanushali",           // Name of sender just for notification. Can be changed accroding to message type
            "MessageDate": "25-Apr-2015",               // Message Send Date Over db server
            "MessageTime": "06:05:00 AM",               // Message Sending Time Over db Server
            "MessageHasExtras": true,                   // If message contains extra json overloads.
            "MessageExtras": [                          // We are keeping extras as array for future use.
                {
                    "DataLoadType":"clsInformationNew", // Type of data or class that Json Data Load Contains
                    "JsonDataLoad": "ExtrasValue"       // Actual Json Data Load Converted in string
                },
                {
                    "DataLoadType":"clsInformationNew2",
                    "JsonDataLoad": "ExtrasValueaaaaa"
                }
            ]
        }        
         */

    #endregion "Json Structure"

    public class MessageExtra
    {
        public string DataLoadType { get; set; }
        public string JsonDataLoad { get; set; }
    }

    class Data
    {
        #region "Old message strucuture modified on date 28/04/2015"
        /*public List<string> Messages { get; set; }
            public List<int> MessageIdOnServerDb { get; set; }
            public List<string> From { get; set; }
            public List<string> To { get; set; }
            public string MessageDate { get; set; }
            public string MessageTime { get; set; }*/
        #endregion "Old message strucuture modified on date 28/04/2015"

        public string MessageType { get; set; }
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string MessageDate { get; set; }
        public string MessageTime { get; set; }
        public bool MessageHasExtras { get; set; }
        public List<MessageExtra> MessageExtras { get; set; }

    }

    // Main class for encapsulating details.
    class GCM_PUSH_MSG
    {
        public string collapse_key { get; set; }
        public int time_to_live { get; set; }
        public bool delay_while_idle { get; set; }
        public Data data { get; set; }
        public List<string> registration_ids { get; set; }
    }

    class ExtraFileDetails
    {
        public string FileUri { get; set; }
        public string FileSize { get; set; }
    }

    class ExtraOfferMessage
    {
        public ExtraOfferMessage(string pStrTitle, string pStrText, string pStrImgUrl)
        {
            OfferTitle = pStrTitle;
            OfferText = pStrText;
            OfferImageUrl = pStrImgUrl;
        }
        public string OfferTitle { get; set; }
        public string OfferText { get; set; }
        public string OfferImageUrl { get; set; }
    }

    class ExtraQueryMessage
    {
        public ExtraQueryMessage(string pStrName,
                                 string pStrPhone1, string pStrPhone2, string pStrPhone3,
                                 string pStrEmail1, string pStrEmail2,
                                 string pStrSubject, string pStrQryBody)
        {
            ContactName = pStrName;
            Phone1 = pStrPhone1;
            Phone2 = pStrPhone2;
            Phone3 = pStrPhone3;
            Email1 = pStrEmail1;
            Email2 = pStrEmail2;
            Subject = pStrSubject;
            Query = pStrQryBody;
        }

        public string ContactName { get; set;}
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Subject { get; set; }
        public string Query { get; set; }
    }

    #endregion

}

// Old class
//namespace VTalk_WebApp.App_Code
//{
/// <summary>
/// Creates a Json Formated Message To sent over gcm.
/// </summary>
//public class GcmMessageRequest
//{

//    #region "Data members"

//    // list of messages
//    //
//    private List<string> _Messages = null;
//    private List<int> _MessageId = null;

//    // list of senders.
//    //
//    private List<string> _Sender = null;

//    // list of recievers.
//    //
//    private Dictionary<string, string> _Recievers = null;

//    #endregion

//    #region "Constructors"

//    /// <summary>
//    /// Initialize object of gcm message class.
//    /// </summary>
//    public GcmMessageRequest()
//    {
//        _Messages = new List<string>();
//        _MessageId = new List<int>();
//        _Sender = new List<string>();
//        _Recievers = new Dictionary<string, string>();
//        this.DelayMessageIfClientNotActive = false;
//        this.MsgType = "";
//        this.HoldTimeOfMessageOverGcm = 2419200; // 4 Weeks
//        this.MsgDate = "";
//        this.MsgTime = "";
//    }

//    #endregion

//    #region "Class properties for message handling"

//    /// <summary>
//    /// Get or set Server date when message was created.
//    /// </summary>
//    public string MsgDate { get; set; }

//    /// <summary>
//    /// Get or set server time when message was created.
//    /// </summary>
//    public string MsgTime { get; set; }

//    /// <summary>
//    /// Type of message.
//    /// <para>If not specified then set to Current Date time.</para>
//    /// </summary>
//    public string MsgType { get; set; }

//    /// <summary>
//    /// Time in seconds for which gcm should hold message,
//    /// <para>till it is not forwared to client.</para>
//    /// <para>default 4 Weeks.</para>
//    /// </summary>
//    public int HoldTimeOfMessageOverGcm { get; set; }

//    /// <summary>
//    /// Hold message over gcm if client device not active.
//    /// <para>Default is false.</para>
//    /// </summary>
//    public bool DelayMessageIfClientNotActive { get; set; }

//    #endregion

//    #region "Message Handler Methods"

//    /// <summary>
//    /// Adds message into list of message.
//    /// </summary>
//    /// <param name="strMessage">Message to be added in message list.</param>
//    /// <returns>Index where message was added.</returns>
//    public int AddMessage(string strMessage, int iMessageId)
//    {
//        _Messages.Add(strMessage);
//        _MessageId.Add(iMessageId);
//        return _Messages.Count - 1;
//    }

//    /// <summary>
//    /// Clear all messages.
//    /// </summary>
//    public void ClearMessages()
//    {
//        _Messages.Clear();
//        _MessageId.Clear();
//    }

//    /// <summary>
//    /// Removes message from message list.
//    /// </summary>
//    /// <param name="iIndex">Index from where message should be removed.</param>
//    public void RemoveMessage(int iIndex)
//    {
//        _Messages.RemoveAt(iIndex);
//        _MessageId.RemoveAt(iIndex);
//    }

//    /// <summary>
//    /// Remove message from message list.
//    /// </summary>
//    /// <param name="strMessage">Message to be removed.</param>
//    /// <param name="bRemoveAll">Remove all matching messages.</param>
//    public void RemoveMessage(string strMessage, bool bRemoveAll)
//    {
//        int iIndex = -1;
//        if (bRemoveAll)
//        {
//            while (_Messages.Contains(strMessage))
//            {
//                iIndex = _Messages.IndexOf(strMessage);
//                _Messages.Remove(strMessage);
//                if (iIndex != -1)
//                    _MessageId.RemoveAt(iIndex);
//            }
//        }
//        else
//        {
//            iIndex = _Messages.IndexOf(strMessage);
//            _Messages.Remove(strMessage);
//            if (iIndex != -1)
//                _MessageId.RemoveAt(iIndex);
//        }
//    }

//    /// <summary>
//    /// Set a predefined list as message list.
//    /// </summary>
//    /// <param name="lstMessages">Predefined message list.</param>
//    /// <param name="lstMessageIds">Id's Of Messages On Server Db</param>
//    public void SetMessageList(List<string> lstMessages, List<int> lstMessageIds)
//    {
//        _Messages = lstMessages;
//        _MessageId = lstMessageIds;
//    }

//    #endregion

//    #region "Reciever Handler Methods"

//    /// <summary>
//    /// Adds reciever into list of recievers with device id.
//    /// <para>One id can be added only once.</para>
//    /// </summary>
//    /// <param name="strRecieverId">Id of reciever over server.</param>
//    /// <param name="strDeviceId">Reciever's device id.</param>
//    /// <returns>Index where reciver details was added.</returns>
//    public int AddReciever(string strRecieverId, string strDeviceId)
//    {
//        _Recievers.Add(strRecieverId, strDeviceId);
//        return _Recievers.Count - 1;
//    }

//    /// <summary>
//    /// Clears all recievers.
//    /// </summary>
//    public void ClearRecievers()
//    {
//        _Recievers.Clear();
//    }

//    /// <summary>
//    /// Removes reciver from reciever list.
//    /// </summary>
//    /// <param name="iIndex">Index from where reciever should be removed.</param>
//    public void RemoveReciever(int iIndex)
//    {
//        string strKey = _Recievers.Keys.ToList<string>()[iIndex];
//        _Recievers.Remove(strKey);
//    }

//    /// <summary>
//    /// Remove reciver from message list.
//    /// </summary>
//    /// <param name="strRecieverId">Reciver to be removed.</param>
//    public void RemoveReciever(string strRecieverId)
//    {
//        _Recievers.Remove(strRecieverId);
//    }

//    /// <summary>
//    /// Set a predefined dictonary as reciever list.
//    /// </summary>
//    /// <param name="dicReciever">Predefined recivers list.
//    /// <para>Dictonary's Key should be reciever id.</para>
//    /// <para>Dictonary's Value should be reciever's device id.</para>
//    /// </param>
//    public void SetRecieverList(Dictionary<string, string> dicReciever)
//    {
//        _Recievers = dicReciever;
//    }

//    public string GetReciverId(int iIndex)
//    {
//        return _Recievers.Keys.ToList<string>()[iIndex];
//    }

//    public string GetRecieverDeviceId(int iIndex)
//    {
//        return _Recievers.Values.ToList<string>()[iIndex];
//    }

//    #endregion

//    #region "Sender Handler Methods"

//    /// <summary>
//    /// Adds sender into list of sender.
//    /// </summary>
//    /// <param name="strSender">Id of sender over server.</param>
//    /// <returns>Index where Sender was added.</returns>
//    public int AddSender(string strSender)
//    {
//        _Sender.Add(strSender);
//        return _Sender.Count - 1;
//    }

//    /// <summary>
//    /// Clear all Senders.
//    /// </summary>
//    public void ClearSenders()
//    {
//        _Sender.Clear();
//    }

//    /// <summary>
//    /// Removes Sender from sender list.
//    /// </summary>
//    /// <param name="iIndex">Index from where Sender should be removed.</param>
//    public void RemoveSender(int iIndex)
//    {
//        _Sender.RemoveAt(iIndex);
//    }

//    /// <summary>
//    /// Remove Sender from sender list.
//    /// </summary>
//    /// <param name="strSender">Sender to be removed.</param>
//    /// <param name="bRemoveAll">Remove all matching senders.</param>
//    public void RemoveSender(string strSender, bool bRemoveAll)
//    {
//        if (bRemoveAll)
//        {
//            while (_Sender.Contains(strSender))
//                _Sender.Remove(strSender);
//        }
//        else
//            _Sender.Remove(strSender);
//    }

//    /// <summary>
//    /// Set a predefined list as sender list.
//    /// </summary>
//    /// <param name="lstSenders">Predefined sender list.</param>
//    public void SetSenderList(List<string> lstSenders)
//    {
//        _Sender = lstSenders;
//    }

//    #endregion

//    #region "Main Serialize method"

//    /// <summary>
//    /// Returns json formated string for gcm push.
//    /// </summary>
//    /// <returns>Json Msg In format specified by GCM.</returns>
//    public string CreateJsonMsg()
//    {
//        if (_Recievers.Count == 0)
//            throw new Exception("No recievers were added to recivers list");
//        if (_Sender.Count == 0)
//            throw new Exception("No senders were added to senders list");
//        if (_Messages.Count == 0)
//            throw new Exception("No messages were added to messages list");

//        GCM_PUSH_MSG jsonParent = new GCM_PUSH_MSG();

//        jsonParent.collapse_key = this.MsgType == "" ? DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss_tt") : this.MsgType;
//        jsonParent.delay_while_idle = this.DelayMessageIfClientNotActive;
//        jsonParent.registration_ids = this._Recievers.Values.ToList<string>();
//        jsonParent.time_to_live = this.HoldTimeOfMessageOverGcm;

//        Data msgdata = new Data();

//        msgdata.From = _Sender;
//        msgdata.To = _Recievers.Keys.ToList<string>();
//        msgdata.Messages = _Messages;
//        msgdata.MessageIdOnServerDb = _MessageId;
//        msgdata.MessageDate = this.MsgDate != "" ? this.MsgDate : DateTime.Now.ToString("dd/MMMM/yyyy");
//        msgdata.MessageTime = this.MsgTime != "" ? this.MsgTime : DateTime.Now.ToString("hh:mm:ss tt");

//        jsonParent.data = msgdata;
//        //jsonParent.data 

//        return JsonConvert.SerializeObject(jsonParent);
//    }

//    #endregion

//    #region "Inner classes for GCM JSon Structure"

//    //
//    // WARNING !! 
//    // DO NOT MODIFY THIS CLASSES
//    //
//    //

//    /* 
//     * Classes Created With
//     * http://json2csharp.com/
//     * And Android Docs for GCM Push
//     * 
//     * U can also use Deserializer to create this classes.
//     */

//    // Child class of GCM_PUSH_MSG
//    class Data
//    {
//        public List<string> Messages { get; set; }
//        public List<int> MessageIdOnServerDb { get; set; }
//        public List<string> From { get; set; }
//        public List<string> To { get; set; }
//        public string MessageDate { get; set; }
//        public string MessageTime { get; set; }
//    }

//    // Main class for encapsulating details.
//    class GCM_PUSH_MSG
//    {
//        public string collapse_key { get; set; }
//        public int time_to_live { get; set; }
//        public bool delay_while_idle { get; set; }
//        public Data data { get; set; }
//        public List<string> registration_ids { get; set; }
//    }

//    #endregion

//}
//}

