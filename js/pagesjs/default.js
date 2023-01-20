/*
 * File Pre-Req     :     jquery.js jquery.balloon.js utilityfunctions.js
 * File Desc        :     This file was created to include all javascript code for default page
 * File Create Date :     November,28 2014

 * File Modifi Date : (1) 06/08/2015
 * File Modifi Dtls : (1) Added Location trace of login person.
 * File Modifi Prsn : (1) Hitesh
 */

$(function ()
{
    // Don't allow user to come back here from home.
    //window.history.forward();

    // Document ready.
    $('#txtKey').blur(validateCompKey);
    $('#txtId').blur(validateLoginId);
    $('#txtPassword').blur(validateLoginPass);
    //initLocationTrace(); //Call to location Tracer
    LocationTrace();
});

function LocationTrace() {
   
    var jsonLocationTrace = {
        "Ip": "",
        "Host": "",
        "HostDomain": "",
        "City": "",
        "State": "",
        "Country": "",
        "Latitude": "",
        "Longitude": "",
        "UserAgent": "",
        "ErrorDetail": "",
        "ASN":""
    };

    $.getJSON('https://api.ipdata.co/?api-key=6228d35a0c3e38a5b788d979147a06967743b2c30bdcc2dfbc125605', function (data) {
        //console.log(JSON.stringify(data, null, 2));
        if (data != null && data != undefined) {
            jsonLocationTrace.Ip = data.ip;
            jsonLocationTrace.Host = data.organisation;
            jsonLocationTrace.HostDomain = data.organisation;
            jsonLocationTrace.City = data.city;
            jsonLocationTrace.State = data.region;
            jsonLocationTrace.Country = data.country_name;
            jsonLocationTrace.Latitude = data.latitude;
            jsonLocationTrace.Longitude = data.longitude;
            jsonLocationTrace.UserAgent = getUserAgent();
         //   jsonLocationTrace.ASN = data.asn;

           
            $('#ip-addr').html(jsonLocationTrace.Ip);
            $('#host').html(jsonLocationTrace.Host);
            $('#address').html(jsonLocationTrace.City + ', ' + jsonLocationTrace.State + ', ' + jsonLocationTrace.Country);

           
            //Commented for display error
            //$('.bottom-location-popup').animate({ right: "5px" }, "fast");

            //$('.bottom-location-popup').fadeIn("slow");

            $('#locationJson').val(JSON.stringify(jsonLocationTrace));
        }

        else {
            jsonLocationTrace.ErrorDetail = "Geolocation is not supported by this browser.";
            $('#locationJson').val(JSON.stringify(jsonLocationTrace));
        }
    });


}

function validateCompKey()
{
    var keyValue = $('#txtKey').val();
    keyValue = replaceAll(keyValue, '-', '');
    if (keyValue === '')
        showBalloonWithText('#txtKey', 'Key must be provided');
    else if (keyValue.length != 8)
        showBalloonWithText('#txtKey', 'Key length not proper!!');
    else if (isNumeric(keyValue) === false)
        showBalloonWithText('#txtKey', 'Not a proper key!!');
}

function validateLoginId() {
    var idValue = $('#txtId').val();
    if (idValue === '')
        showBalloonWithText('#txtId', 'Id must be provided');
}

function validateLoginPass() {
    var passValue = $('#txtPassword').val();
    if (passValue === '')
        showBalloonWithText('#txtPassword', 'Password must be provided');
}


function showBalloonWithText(elementId,text)
{
    var html = '<img src="images/excl.png" style="margin-right:3px;vertical-align:text-top;"/>' + text;
    $(elementId).showBalloon({ contents: html, position: 'bottom'});
    setTimeout(function () { $(elementId).hideBalloon(); }, 1000);
}


//Function for Location Tracing of user login.
    function initLocationTrace() {
        var jsonLocationTrace = {
            "Ip": "",
            "Host": "",
            "HostDomain": "",
            "City": "",
            "State": "",
            "Country": "",
            "Latitude": "",
            "Longitude": "",
            "UserAgent": "",
            "ErrorDetail": ""
        };

        if (navigator.geolocation) {
            try
            {
                var gotPosition = function (position) {
                    //alert("1 Done");
                    jsonLocationTrace.Latitude = position.coords.latitude;
                    jsonLocationTrace.Longitude = position.coords.longitude;
                    $.get("http://ipinfo.io", function (response) {
                        //alert("2 Done");
                        jsonLocationTrace.Ip = response.ip;
                        jsonLocationTrace.HostDomain = response.hostname;
                        jsonLocationTrace.Host = response.org;

                        var urlToGoogle = "http://maps.googleapis.com/maps/api/geocode/json?latlng=" + jsonLocationTrace.Latitude + "," + jsonLocationTrace.Longitude + "&sensor=true";

                        $.get(urlToGoogle, function (geoResult) {
                            //alert("3 Done");
                            for (var iRes = 0; iRes < geoResult.results[0].address_components.length; iRes++) {
                                var add_comp = geoResult.results[0].address_components[iRes];
                                if (add_comp.types.indexOf("political") != -1) {
                                    if (add_comp.types.indexOf("locality") != -1)
                                        jsonLocationTrace.City = add_comp.long_name;
                                    else if (add_comp.types.indexOf("administrative_area_level_1") != -1)
                                        jsonLocationTrace.State = add_comp.long_name;
                                    else if (add_comp.types.indexOf("country") != -1)
                                        jsonLocationTrace.Country = add_comp.long_name;
                                }
                            }

                            jsonLocationTrace.UserAgent = getUserAgent();

                            $('#ip-addr').html(jsonLocationTrace.Ip);
                            $('#host').html(jsonLocationTrace.Host);
                            $('#address').html(jsonLocationTrace.City + ', ' + jsonLocationTrace.State + ', ' + jsonLocationTrace.Country);
                    
                    
                            //Commented for display error
                            //$('.bottom-location-popup').animate({ right: "5px" }, "fast");

                            $('.bottom-location-popup').fadeIn("slow");

                            $('#locationJson').val(JSON.stringify(jsonLocationTrace));

                        }, "json");

                    }, "jsonp");
                };
                var failedPosition = function (err) {
                    alert('Failed to load location' + JSON.stringify(err));
                    jsonLocationTrace.ErrorDetail = "Error loading location data!!";
                }
                navigator.geolocation.getCurrentPosition(gotPosition, failedPosition, {maximumAge:60000, timeout:5000});
            }
            catch (exce)
            {
                alert(exce.message);
            }
        }
        else
        {
            jsonLocationTrace.ErrorDetail = "Geolocation is not supported by this browser.";
            $('#locationJson').val(JSON.stringify(jsonLocationTrace));
        }
    }

    $(document).on('click','.close-button',function () {
        $('.bottom-location-popup').fadeOut("fast");
    });

    function getUserAgent() {
        try {
            var isOpera = !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
            if (isOpera)
                return "Opera";
            else if (typeof InstallTrigger !== 'undefined')
                return "Firefox";
            else if (Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0)
                return "Safari";
            else if (!!window.chrome && !isOpera) 
                return "Chrome";
            else if (/*@cc_on!@*/false || !!document.documentMode)
                return "Internet Explorer";
            else
                return "Unknown";}
        catch (e) {
            //alert(e.message);
            return "";
        }
    }



/*
function initLocationTrace() {
    var jsonLocationTrace = {
        "Ip": "",
        "Host": "",
        "HostDomain": "",
        "City": "",
        "State": "",
        "Country": "",
        "Latitude": "",
        "Longitude": "",
        "UserAgent": "",
        "ErrorDetail": ""
    };

    if (navigator.geolocation) {
        var gotPosition = function (position) {
            jsonLocationTrace.Latitude = position.coords.latitude;
            jsonLocationTrace.Longitude = position.coords.longitude;
            $.get("http://ipinfo.io", function (response) {
                jsonLocationTrace.Ip = response.ip;
                jsonLocationTrace.HostDomain = response.hostname;
                jsonLocationTrace.Host = response.org;

                var urlToGoogle = "http://maps.googleapis.com/maps/api/geocode/json?latlng=" + jsonLocationTrace.Latitude + "," + jsonLocationTrace.Longitude + "&sensor=true";

                $.get(urlToGoogle, function (geoResult) {
                    for (var iRes = 0; iRes < geoResult.results[0].address_components.length; iRes++) {
                        var add_comp = geoResult.results[0].address_components[iRes];
                        if (add_comp.types.indexOf("political") != -1) {
                            if (add_comp.types.indexOf("locality") != -1)
                                jsonLocationTrace.City = add_comp.long_name;
                            else if (add_comp.types.indexOf("administrative_area_level_1") != -1)
                                jsonLocationTrace.State = add_comp.long_name;
                            else if (add_comp.types.indexOf("country") != -1)
                                jsonLocationTrace.Country = add_comp.long_name;
                        }
                    }

                    jsonLocationTrace.UserAgent = getUserAgent();

                    $('#locationJson').val(JSON.stringify(jsonLocationTrace));

                }, "json");

            }, "jsonp");
        };
        navigator.geolocation.getCurrentPosition(gotPosition);
    } else {
        jsonLocationTrace.ErrorDetail = "Geolocation is not supported by this browser.";
        $('#locationJson').val(JSON.stringify(jsonLocationTrace));
    }
}

function getUserAgent() {
    try {
        var isOpera = !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
        if (isOpera)
            return "Opera";
        else if (typeof InstallTrigger !== 'undefined')
            return "Firefox";
        else if (Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0)
            return "Safari";
        else if (!!window.chrome && !isOpera) 
            return "Chrome";
        else if (/*@cc_on!@ false || !!document.documentMode)
            return "Internet Explorer";
        else
            return "Unknown";}
    catch (e) {
        //alert(e.message);
        return "";
    }
    }*/

//Function for Location Tracing of user login.
//function initLocationTrace() {
//    var jsonLocationTrace = {
//        "Ip": "",
//        "Host": "",
//        "HostDomain": "",
//        "City": "",
//        "State": "",
//        "Country": "",
//        "Latitude": "",
//        "Longitude": "",
//        "ErrorDetail": ""
//    };

//    if (navigator.geolocation) {
//        var gotPosition = function (position) {
//            jsonLocationTrace.Latitude = position.coords.latitude;
//            jsonLocationTrace.Longitude = position.coords.longitude;
//            $.get("http://ipinfo.io", function (response) {
//                jsonLocationTrace.Ip = response.ip;
//                jsonLocationTrace.HostDomain = response.hostname;
//                jsonLocationTrace.Host = response.org;

//                var urlToGoogle = "http://maps.googleapis.com/maps/api/geocode/json?latlng=" + jsonLocationTrace.Latitude + "," + jsonLocationTrace.Longitude + "&sensor=true";

//                $.get(urlToGoogle, function (geoResult) {
//                    for (var iRes = 0; iRes < geoResult.results[0].address_components.length; iRes++) {
//                        var add_comp = geoResult.results[0].address_components[iRes];
//                        if (add_comp.types.indexOf("political") != -1) {
//                            if (add_comp.types.indexOf("locality") != -1)
//                                jsonLocationTrace.City = add_comp.long_name;
//                            else if (add_comp.types.indexOf("administrative_area_level_1") != -1)
//                                jsonLocationTrace.State = add_comp.long_name;
//                            else if (add_comp.types.indexOf("country") != -1)
//                                jsonLocationTrace.Country = add_comp.long_name;
//                        }
//                    }

//                    $('#locationJson').val(JSON.stringify(jsonLocationTrace));

//                }, "json");

//            }, "jsonp");
//        };
//        navigator.geolocation.getCurrentPosition(gotPosition);
//    } else {
//        jsonLocationTrace.ErrorDetail = "Geolocation is not supported by this browser.";
//        $('#locationJson').val(JSON.stringify(jsonLocationTrace));
//    }
//}