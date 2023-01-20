$(document).ready(function () {
    // Set active page as manangement page in side list menu.
    $('#mnuordermgt').addClass('active');

    var oTable = $('#ctl00_CntAdminEx_Body_grdOrderDetails')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdOrderDetails tr:first")))
        .dataTable({
            'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': ['nosort']
            },
            {
                'bVisible': false,
                'aTargets': ['col-hidden-to-clnt']
            }]
        });

    $('#chkSendEmail,#chkSendNotification').iCheck('check');
    $('.action-buttons-group button').find("[data-diable-me='true']").prop('disabled', true);
    $('#OrderDetailsContent').css('visibility', 'visible');

    ajaxDealers();

    $('.btn-view-order').click(function () {
        // We show dialog and create ajax request for data.

        /* Get the row as a parent of the link that was clicked on */
        var nRow = $(this).parents('tr')[0];
        var currRowData = oTable.fnGetData(nRow);

        var orderId = parseInt(currRowData[0]);

        $('#spnOrdFullNum').html("Order Number : " + orderId.toString());
        $('#txtClntNm').val(currRowData[1]);
        $('#txtOrderTotalAmt').val($($(currRowData[4])[2]).html());

        $('#OrderData').css('display', 'none');
        $('#loadingOrderData').css('display', 'block');

        $('#modViewFullOrder').data("orderid-to-check", orderId.toString());
        $('#modViewFullOrder').data("current-order-status", currRowData[7]);
        $('#modViewFullOrder').modal('show');

        ajaxSubOrders(orderId);
    });

    $(document).on('click', '.retry-read-prod-sub', function () {
        $('#OrderData').css('display', 'none');
        $('#loadingOrderData').css('display', 'block');
        ajaxSubOrders(parseInt($('#modViewFullOrder').data("orderid-to-check")));
    });

    $('#btnProcessViewOrder').click(function ()
    {
        $('#modViewFullOrder').modal('toggle');
        $('#spnOrdStatusNum').html($('#spnOrdFullNum').html());

        var currStatus = parseInt($('#modViewFullOrder').data("current-order-status"));
        if (currStatus < 3) {
            // if current new -> confirm , confirm -> dispatch ,dispatch -> deliever
            currStatus = currStatus + 1;
            $('#cmbOrdStatus').prop('disabled', false);
            $('#btnUpdateStatus').prop('disabled', false);
            $('#modViewOrderStatus').data("save-only", currStatus.toString());
        }
        else if (currStatus == 3 || currStatus == 4) {
            $('#cmbOrdStatus').prop('disabled', true);
            $('#btnUpdateStatus').prop('disabled', true);
            $('#modViewOrderStatus').data("save-only", "-1"); // so that next event handler doesn't enable button again
        }

        $('#cmbOrdStatus').val(currStatus.toString()).change();
        $('#modViewOrderStatus').modal('show');
    });

    $('#btnCloseViewOrder').click(function () {
        $('#modViewFullOrder').modal('toggle');
    });

    $('#btnCancelUpdate').click(function () {
        $('#modViewOrderStatus').modal('toggle');
    });

    $('#cmbOrdStatus').change(function () {
        var currValue = parseInt($(this).val());

        // disable save if value is not save-only value
        $('#btnUpdateStatus').prop('disabled', !(currValue == parseInt($('#modViewOrderStatus').data("save-only"))));

        switch (currValue)
        {
            case 0: // New
            case 3: // Delivered
                $('.dealer-details,.delivery-details,.cancel-details').css('display', 'none');
                break;
            case 1: // Confirmed
                $('.delivery-details,.cancel-details').css('display', 'none');
                $('.dealer-details').css('display', 'block');
                break;
            case 2: // Dispatched
                $('.dealer-details,.cancel-details').css('display', 'none');
                $('.delivery-details').css('display', 'block');
                break;
            case 4: // Cancelled
                $('.dealer-details,.delivery-details').css('display', 'none');
                $('.cancel-details').css('display', 'block');
                break;
        }
    });

    $('#cmbDealerRefresh').click(function () {
        $('#cmbDealerLoading').css('display', 'inline-block');
        $('#cmbDealerError,#cmbDealer').css('display', 'none');

        ajaxDealers();
    });

    $('#btnNotifyOnly').click(function () {
        if ($('#btnUpdateStatus').prop('disabled')) {
            // saved so allow sending notification
        }
        else {
            bootbox.confirm("Order must be updated first.<br/>Update and notify user?", function (result) {
                if (result) {

                }
                else {
                    // User cancelled to save and notify
                }
            });
        }
    });

    // ------------------------------------------------------------------
    // **** Ajax Methods ****
    function ajaxSubOrders(orderId) {
        var request = $.ajax({
            url: "Mstore_WebSite_AjaxServices/OrderMaster.asmx/GetFullOrderDetails",
            method: "POST",
            data: { mLngOrderId: orderId },
            dataType: "json"
        });

        request.done(function (jsonData) {
            if (jsonData.Error == "") {
                var productsData = jsonData.OrderData;
                if (productsData != null) {
                    $('#tblOrderSubsContent').html('<table id="tblOrderSubs" class="dynamic-table-grid display table table-bordered table-striped">'
                                        + '<thead>'
                                        + '<th class="nosort">Image</th>'
                                        + '<th>Name</th>'
                                        + '<th>Type</th>'
                                        + '<th>Quantity</th>'
                                        + '<th>Basic Amount</th>'
                                        + '<th>Total Amount</th>'
                                        + '</thead>'
                                        + '<tfoot>'
                                        + '<th>Image</th>'
                                        + '<th>Name</th>'
                                        + '<th>Type</th>'
                                        + '<th>Quantity</th>'
                                        + '<th>Basic Amount</th>'
                                        + '<th>Total Amount</th>'
                                        + '</tfoot>'
                                    + '</table>');

                    $('#tblOrderSubs').dataTable({
                        "aaData": productsData,
                        "aoColumns": [
                            { "mDataProp": "ProductImagePath" },
                            { "mDataProp": "ProductName" },
                            { "mDataProp": "ProductType" },
                            { "mDataProp": "Quantity" },
                            { "mDataProp": "BasicAmount" },
                            { "mDataProp": "TotalAmout" }],
                        "aoColumnDefs": [
                            {
                                // `data` refers to the data for the cell (defined by `mData`, which
                                // defaults to the column being worked with, in this case is the first
                                // Using `row[0]` is equivalent.
                                "mRender": function (data, type, row) {
                                    return '<img src="' + data + '" onError="this.onerror=null;this.src=\'http://www.placehold.it/64x64/EFEFEF/AAAAAA&amp;text=no+image;\'">';
                                    //return '<img src="' + data + '">';
                                }, "aTargets": [0]
                            },
                            { "sClass": "center", "aTargets": [0] },
                            { "bSortable": false, "aTargets": ['nosort'] }]
                    });
                    $('#loadingOrderData').css('display', 'none');
                    $('#OrderData').css('display', 'block');
                }
            }
            else {
                var errorInRequest = jsonData.Error + '<p style="text-align:center"><button class="btn btn-warning retry-read-prod-sub" type="button"><i class="fa fa-refresh"></i> Retry</button></p>';
                errorInRequest = '<div class="alert alert-danger" style="display:inline-block;">' + errorInRequest + '</div>';
                $('#loadingOrderData').html(errorInRequest);
            }
        });

        request.fail(function (jqXHR, textStatus) {
            var errorInRequest = 'Data request to server timed out' + '<p style="text-align:center"><button class="btn btn-warning retry-read-prod-sub" type="button"><i class="fa fa-refresh"></i> Retry</button></p>';
            errorInRequest = '<div class="alert alert-danger" style="display:inline-block;">' + errorInRequest + '</div>';
            $('#loadingOrderData').html(errorInRequest);
        });
    }

    function ajaxDealers() {
        var request = $.ajax({
            url: "Mstore_WebSite_AjaxServices/OrderMaster.asmx/GetDealersMin",
            method: "POST",
            dataType: "json"
        });

        request.done(function (jsonData) {
            if (jsonData.Error == "") {
                var dealersData = jsonData.Dealers;
                if (dealersData != null) {
                    var dealerHtml = '';
                    if (dealersData.length > 0) {
                        for (var iData = 0; iData < dealersData.length; iData++) {
                            dealerHtml += '<option value="' + dealersData[iData].DealerId + '">' + dealersData[iData].DealerName + '</option>'
                        }
                        $('#cmbDealer').empty().append(dealerHtml);
                        $('#cmbDealer').css('display', 'block');
                        $('#cmbDealerLoading,#cmbDealerError').css('display', 'none');
                    }
                    else {
                        $('#cmbDealerError').css('display', 'block');
                        $('#cmbDealerLoading,#cmbDealer').css('display', 'none');

                        $('#cmbDealerError').html('No dealer data found');
                    }
                }
            }
            else {
                $('#cmbDealerError').css('display', 'block');
                $('#cmbDealerLoading,#cmbDealer').css('display', 'none');

                $('#cmbDealerError').html(jsonData.Error);
            }
        });

        request.fail(function (jqXHR, textStatus) {
            $('#cmbDealerError').css('display', 'block');
            $('#cmbDealerLoading,#cmbDealer').css('display', 'none');

            $('#cmbDealerError').html('Data request to server timed out');
        });
    }

    // **** Ajax Methods ****
    // ------------------------------------------------------------------


});