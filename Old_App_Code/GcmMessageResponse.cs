using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

//namespace VTalk_WebApp.App_Code
//{
    public class GcmMessageResponse
    {
        #region "Data members"

        // Request message for this response.
        //
        GcmMessageRequest _GcmRequest;

        // Number of successfull messages
        //
        int _SuccessMessges;

        // Number of failed messages
        //
        int _FailedMessges;

        // Unique GCM multicast id over server for this message
        //
        long _MessageId;

        // Number of canonical id's (Deprecated id's over gcm that needed to be updated)
        //
        int _CanonicalIds;

        // Log messages. Writing to physical storage remaining.do it with global info class.
        // 
        // List<VTalkLog> _Log;

        // List of user id whom message need to be retried.
        // 
        List<string> _RetryUsrs;

        // List of user whose id need to be updated...And the new id for updationg
        // UserId,New GCM Id
        //
        Dictionary<string, string> _NewIds;

        // List of user to whom messaeg can not be sent.
        // User id , failure reason.
        //
        Dictionary<string, string> _NonavailUser;

        // List of user to whom message was sent.
        //
        List<string> _SuccessFullUserIds;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Initializes response object for a request.
        /// </summary>
        /// <param name="GcmRequestObject">Request object for which response is recieved.</param>
        public GcmMessageResponse(GcmMessageRequest GcmRequestObject)
        {
            _GcmRequest = GcmRequestObject;
            _SuccessMessges = 0;
            _FailedMessges = 0;
            _CanonicalIds = 0;
            //_Log = new List<VTalkLog>();

            _NewIds = new Dictionary<string, string>();
            _RetryUsrs = new List<string>();
            _NonavailUser = new Dictionary<string, string>();
            _SuccessFullUserIds = new List<string>();

            //_Log.Add(new VTalkLog("Null was pass as request" + Environment.NewLine + "This will generate improper response", "GcmMessageResponse", LogTypes.LogWarnings));
        }

        #endregion

        #region "Other properties"

        /// <summary>
        /// Gets number of messages that were sent.
        /// </summary>
        public int SuccessMessages { get { return _SuccessMessges; } }

        /// <summary>
        /// Gets number of messages that were failed to sent.
        /// </summary>
        public int FailedMessages { get { return _FailedMessges; } }

        /// <summary>
        /// Gets message id over server for this message.
        /// </summary>
        public long MessageId { get { return _MessageId; } }

        /// <summary>
        /// Gets number of device id's that need to be updated.
        /// </summary>
        public int DeviceIdToUpdate { get { return _CanonicalIds; } }

        /// <summary>
        /// Gets List of user whose device id need to be updated.
        /// <para>Keys in returned dictonary are user ids.</para>
        /// <para>Value in returned dictonary are new device ids.</para>
        /// </summary>
        public Dictionary<string, string> DeviceIdUpdateList { get { return _NewIds; } }

        /// <summary>
        /// Gets list of user id's to whom message need to be resent.
        /// </summary>
        public List<string> UserListToResendMessages { get { return _RetryUsrs; } }

        /// <summary>
        /// Gets list of user to whom message sending failed.
        /// <para>Keys in returned dictionary are user ids.</para>
        /// <para>Values in returned dictionary are error details.</para>
        /// </summary>
        public Dictionary<string, string> FailedSendingUserList { get { return _NonavailUser; } }

        /// <summary>
        /// Gets list of user id to whom message was sent successfully.
        /// </summary>
        public List<string> SucceedUserList { get { return _SuccessFullUserIds; } }

        #endregion


        /// <summary>
        /// Reads response in object from json.
        /// <para>Sets object ready to update server details.</para>
        /// </summary>
        /// <param name="sJonsResponse">Response string from gcm.</param>
        /// <returns>If response was readed or not.</returns>
        public bool ReadResponse(string sJonsResponse)
        {
            GCM_JSON_RESPONSE _GcmResponse = null;
            try
            {
                // try to desrialize json
                _GcmResponse = JsonConvert.DeserializeObject<GCM_JSON_RESPONSE>(sJonsResponse);
            }
            catch (Exception ex)
            {
                // To do
                // Add this to log
                //_Log.Add(new VTalkLog(String.Format("Deserialization failed with error : {0}", ex.Message), "ReadResponse", "GcmMessageResponse", LogTypes.LogError));
                return false;
            }
            if (_GcmResponse == null)
            {
                // To do
                // Add this to log
                //_Log.Add(new VTalkLog("Deserialization succeeded but object was still null", "ReadResponse", "GcmMessageResponse", LogTypes.LogWarnings));
                return false;
            }
            else
            {
                this._MessageId = _GcmResponse.multicast_id;
                this._SuccessMessges = _GcmResponse.success;
                this._FailedMessges = _GcmResponse.failure;
                this._CanonicalIds = _GcmResponse.canonical_ids;

                for (int iCntr = 0; iCntr < _GcmResponse.results.Count; iCntr++) //each (Result detail in _GcmResponse.results)
                {
                    Result detail = _GcmResponse.results[iCntr];
                    if (detail.error == null || detail.error == "")
                    {
                        if (detail.registration_id == "" || detail.registration_id == null)
                        {
                            // Success full sent.
                            //
                            _SuccessFullUserIds.Add(_GcmRequest.GetReciverId(iCntr));
                        }
                        else
                        {
                            // Success full sent.
                            // but device id need to be updated.
                            //
                            _NewIds.Add(_GcmRequest.GetReciverId(iCntr), detail.registration_id);
                        }
                    }
                    else
                    {
                        if (detail.error.ToLower() == "unavailable")
                        {
                            // Can't send now.
                            //
                            _RetryUsrs.Add(_GcmRequest.GetReciverId(iCntr));
                        }
                        else if (detail.error.ToLower() == "invalidregistration")
                        {
                            // Failed becoz of currupt gcm key
                            //
                            _NonavailUser.Add(_GcmRequest.GetReciverId(iCntr), "Currupt device key.");
                        }
                        else if (detail.error.ToLower() == "notregistered")
                        {
                            // Failed becoz of currupt gcm key
                            //
                            _NonavailUser.Add(_GcmRequest.GetReciverId(iCntr), "Invalid device key. Or user removed application");
                        }
                    }
                }
                return true;
            }
        }

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

        // Gcm response child class
        //
        class Result
        {
            public string message_id { get; set; }
            public string error { get; set; }
            public string registration_id { get; set; }
        }

        class GCM_JSON_RESPONSE
        {
            public long multicast_id { get; set; }
            public int success { get; set; }
            public int failure { get; set; }
            public int canonical_ids { get; set; }
            public List<Result> results { get; set; }
        }

        #endregion
    }
//}