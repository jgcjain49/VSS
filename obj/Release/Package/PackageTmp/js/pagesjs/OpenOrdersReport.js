$(document).ready(function () {
    //console.log("page loaded");
    //$('#ctl00_CntAdminEx_Body_grdReport')
    // .prepend($("<thead></thead>")
    // .append($(this).find("#ctl00_CntAdminEx_Body_grdReport tr:first")))
    // .dataTable({
    //     'aoColumnDefs': [{
    //         'bSortable': false,
    //         'aTargets': ['nosort'],
    //     }]
    // });
});
$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    var target = $(e.target).attr("href").toString().substring(1, 50);
    $('#ctl00_CntAdminEx_Body_activeTab').val(target);
});

var activateTab = $('#ctl00_CntAdminEx_Body_activeTab').val();
$('.nav-tabs a[href="#' + activateTab + '"]').tab('show');

function ordPlcDtSelected(e) {
    //set disabled = false
    document.getElementById("ctl00_CntAdminEx_Body_ordPlcDtTill").min = e.target.value;
    document.getElementById("ctl00_CntAdminEx_Body_ordPlcDtTill").disabled = false;
}

var rateSource;
var liveSilver = 0.00;
var liveGold = 0.00;
var z = 1;
var timer; var d;
var gramConversionFactor = 0.035274;

function getRateAndCalDIff() {
    console.log("tryIt method called ");
    /*
    $('#ctl00_CntAdminEx_Body_grdReport');

    //var reportGrid = $('#ctl00_CntAdminEx_Body_grdReport');
    //var gridOrdId = $('#lblOrderHisID.ClientID');
    //console.log("reportTable: " + JSON.stringify(reportGrid));
    //console.log("gridOrdId: " + JSON.stringify(gridOrdId));
    */

    //fetchLiveRates();
    //fetchLiveFromNewApi();
    fetchLiveRateFromMiddleware();
    setTimeout(function () {
        myTest();
    }, 2000);
}

function myTest() {
    if (liveGold != 0 || liveSilver != 0) {
        console.log("called after getting rates");
        var prodName, buyPrice, type, weight, qty, rate, diff, color;
        //console.log(d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds() + '\t' + z + '\t');
        //liveGold = liveGold + (myrandom * 0.0726);
        //liveSilver = liveSilver + (myrandom * 0.0137);
        $('#ctl00_CntAdminEx_Body_grdReport tr.gradeA').each(function () {
            d = new Date();
            prodName = $(this).children("td:nth-child(7)").text().trim();
            weight = $(this).children("td:nth-child(16)").text().trim().replace(" gm", "");
            qty = $(this).children("td:nth-child(18)").text().trim();
            buyPrice = $(this).children("td:nth-child(9)").text().trim();
            type = $(this).children("td:nth-child(19)").text().trim();
            //rate = $(this).children("td:nth-child(11)").text().trim();
            if (prodName.includes("Gold Bar")) {
                if (liveGold != 0) {
                    diff = getDiffValue(type, buyPrice, liveGold, weight, qty);
                    color = diff > 0 ? 'green' : 'red';
                    //$(this).children("td:nth-child(11)").text(liveGold.toFixed(3)).css({ "color": color, "font-weight": "600" });
                    console.log(d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds() + ' ' +
                        prodName + ' ' + weight + ' ' + qty + ' ' + type + ' ' + buyPrice + ' ' + liveGold + ' ' + diff);
                }
                else { diff = ""; }
            }
            else {
                if (liveSilver != 0) {
                    console.log("silver");
                    diff = getDiffValue(type, buyPrice, liveSilver, weight, qty);
                    color = diff > 0 ? 'green' : 'red';
                    // $(this).children("td:nth-child(11)").text(liveSilver.toFixed(3)).css({ "color": color, "font-weight": "600" });
                    console.log(d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds() + ' ' +
                        prodName + ' ' + weight + ' ' + qty + ' ' + type + ' ' + buyPrice + ' ' + liveSilver + ' ' + diff);
                }
                else { diff = ""; }
            }
            //console.log(prodName + ' ' + weight + ' ' + qty + ' ' + type + ' ' + buyPrice + ' ' + liveGold + ' ' + diff + ' ' + color);
            //$(this).children("td:nth-child(11)").text(liveGold.toFixed(3)).css("color", color);
            $(this).children("td:nth-child(4)").text(diff).css({ "color": color, "font-weight": "600" });
            //$(this).children("td:nth-child(13)").text(diff).css({ "color": color, "font-weight": "600" });
        });

        //continuous not reqd, will/should be called on btn click as per new requirement
        //timer = setTimeout(function () {
        //    myTest();
        //}, 500);
    }
    else {
        console.log("rates not available, will try again in 7 secs");
        timer = setTimeout(function () {
            myTest();
        }, 7000);
    }

}

function getDiffValue(type, price, rate, weight, qnty) {
    var diffVal;
    if (type.toLowerCase() == "booking") {
        diffVal = ((rate - price) * weight * qnty).toFixed(3);
    }
    else {
        diffVal = ((price - rate) * weight * qnty).toFixed(3);
    }
    return diffVal;
}

function getRndInteger() {
    //get random num between -50 to 50, for positive/up negative/down simulation of live rate
    return Math.floor(Math.random() * (50 - (-50))) + (-50);
}

//function fetchLiveFromNewApi() {
//    var d = new Date();
//    var gold = 0;
//    var silv = 0;
//    var usdinr = 0;
//    console.log("CONNECTING TO SOCKET AT " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds() + ":" + d.getMilliseconds())
//    var socket = io('https://multiapi.multiicon.in/live-feed'
//      , {
//          transports:
//            ['websocket']
//      }
//    );

//    socket.emit('login', { userid: 'goldify', password: 'exvF$yh456@DFrw&7' });
//    socket.on('receive-feed', function (symbols) {
//        // d = new Date();
//        // console.log(d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds() + ":" + d.getMilliseconds() +
//        //   " live-feed-rcvd.., connected: " + socket.connected);
//        console.log("symbols: " + JSON.stringify(symbols));
        
//        //for..of loop, showing error but working, do not modify
//        for (var symbol of symbols) {
//            if (symbol.symbol_name == "XAGUSD.p")
//            silv = symbol.bid;
//        if (symbol.symbol_name == "XAUUSD.p")
//            gold = symbol.bid;
//        if (symbol.symbol_name == "USDINR")
//            usdinr = symbol.bid;
//    }

//        // if ((symbols[1] != undefined || symbols[1] != null)) {
//        //   if (symbols[1].bid != null || symbols[1].bid != undefined)
//        //     gold = symbols[1].bid;
//        // }
//        // if ((symbols[0] != undefined || symbols[0] != null)) {
//        //   if (symbols[0].bid != null || symbols[0].bid != undefined)
//        //     silv = symbols[0].bid;
//        // }
//        // if ((symbols[2] != undefined || symbols[2] != null)) {
//        //   if (symbols[2].bid != null || symbols[2].bid != undefined)
//        //     usdinr = symbols[2].bid;
//        // }

//        var parsedGold = (gold * usdinr * gramConversionFactor);
//    var parsedSilv = (silv * usdinr * gramConversionFactor);

//    if (parsedGold < 10 && parsedSilv < 10) {
//        liveGold = parsedGold.toFixed(3);
//        liveSilver = parsedSilv.toFixed(4);
//    }
//    else {
//        liveGold = parsedGold.toFixed(3);
//        liveSilver = parsedSilv.toFixed(4);
//    }

//    //console.log("gold: " + liveGold + "\tsilv: " + liveSilver);
//    $("#ctl00_CntAdminEx_Body_liveGold").val(liveGold); $("#ctl00_CntAdminEx_Body_liveSilv").val(liveSilver);

//});

//setTimeout(function() {
//    console.log("disconnecting from socket");
//    socket.disconnect();
//},1500);
//}

//Pritesh start: changes to connect middleware api for rates
function fetchLiveRateFromMiddleware() {
    if (!!window.EventSource) {

        //rateSource = new EventSource('https://wap.goldifyapp.com/myapi/api/dovegold/GetSubscription');

        rateSource = new EventSource('https://wap.goldifyapp.com/myservice/api/rateservice/GetSubscription');


        rateSource.addEventListener('message', liveApiRateParser, false);

        rateSource.addEventListener('open', function (e) {
            console.log("open!");
        }, false);

        rateSource.addEventListener('error', function (e) {
            if (e.readyState == EventSource.CLOSED) {
                console.log("error!");
            }
        }, false);
    }
}

function liveApiRateParser(e) {
    //console.log(e.data);
    var dataGot = JSON.parse(e.data);
    //var parsedGold = parseFloat(json.liveGoldRate.replace(",", ""));
    //var parsedSilv = parseFloat(json.liveSilverRate.replace(",", ""));
    var parsedGold = dataGot.liveGoldRate;
    var parsedSilv = dataGot.liveSilverRate;
    //if (parsedRate > 1000) {
    //$("#liveGold").val(json.liveRate);
    {
        liveGold = parsedGold;
    }
    //else {
    //$("#liveSilv").val(json.liveRate);
    {
        liveSilver = parsedSilv;
    }

    $("#liveGold").val(liveGold.toFixed(3)); $("#liveSilv").val(liveSilver.toFixed(4));
}
//Pritesh end: changes to connect middleware api for rates

//function fetchLiveRates() {
//    if (!!window.EventSource) {

//        rateSource = new EventSource('https://wap.goldifyapp.com/myapi/api/dovegold/GetSubscription');
//        rateSource.addEventListener('message', liveRateParser, false);

//        rateSource.addEventListener('open', function (e) {
//            console.log("open!");
//        }, false);

//        rateSource.addEventListener('error', function (e) {
//            if (e.readyState == EventSource.CLOSED) {
//                console.log("error!");
//            }
//        }, false);
//    }
//}

//function liveRateParser(e) {
//    console.log(e.data);
//    var json = JSON.parse(e.data);
//    //viewModel.chatMessages.push(json);
//    //var parsedRate = parseFloat(json.liveRate.replace(",", ""));
//    var parsedGold = parseFloat(json.liveGoldRate.replace(",", ""));
//    var parsedSilv = parseFloat(json.liveSilverRate.replace(",", ""));
//    //if (parsedRate > 1000) {
//    //$("#liveGold").val(json.liveRate);
//    {
//        liveGold = parsedGold;
//    }
//    //else {
//    //$("#liveSilv").val(json.liveRate);
//    {
//        liveSilver = parsedSilv;
//    }

//    $("#liveGold").val(liveGold.toFixed(3)); $("#liveSilv").val(liveSilver.toFixed(3));
//}

function stopFetch() {
    rateSource.removeEventListener('message', liveRateParser, false);
}


$('.text').on('keypress', function (event) {
    var regex = new RegExp("^[a-zA-Z0-9 ]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

$('.email').on('keypress', function (event) {
    var regex = new RegExp("^[a-zA-Z0-9 @.]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

$('.numbers').on('keypress', function (event) {
    var regex = new RegExp("^[0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});


$('.address').on('keypress', function (event) {
    var regex = new RegExp("^[a-zA-Z0-9 ,.'-]");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});



$('input').bind("cut copy paste", function (e) {
    e.preventDefault();
});