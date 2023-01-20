
$(document).ready(function () {
    // Set active page as New product notification in side list menu.
    $('#mnuDispatchStock').addClass('nav-active');
    //$('#mnumasterprodmaster').addClass('active');

    //var nEditing = null;
    var oTable = $('#ctl00_CntAdminEx_Body_grddispatch')
         .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grddispatch tr:first")))
        .dataTable({
            "aLengthMenu": [
                [5, 15, 20, -1],
                [5, 15, 20, "All"] // change per page values here
            ],
            // set the initial value
            "iDisplayLength": 5,
            "sDom": "<'row'<'col-lg-6'l><'col-lg-6'f>r>t<'row'<'col-lg-6'i><'col-lg-6'p>>",
            "sPaginationType": "bootstrap",
            "oLanguage": {
                "sLengthMenu": "_MENU_ records per page",
                "oPaginate": {
                    "sPrevious": "Prev",
                    "sNext": "Next"
                }
            },
            "aoColumnDefs": [{
                'bSortable': false,
                'aTargets': [0]
            }
            ]
        });
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href").toString().substring(1, 50);
        $('#ctl00_CntAdminEx_Body_activeTab').val(target);
    });

    var activateTab = $('#ctl00_CntAdminEx_Body_activeTab').val();
    $('.nav-tabs a[href="#' + activateTab + '"]').tab('show');

    $(document).on('click', '.edit', function () {
        $('#ctl00_CntAdminEx_Body_HidIsEditMode').val(true);
        $('#ctl00_CntAdminEx_Body_HidBnkId').val($(this).data('dispatch-id'));
        //  $('#ctl00_CntAdminEx_Body_txtBanknm').val($(this).data('bnk-name'));
        // $('#modDeleteConfirm').modal('show');
        $('#tab_modifyPayment').removeClass('active');
        $('#tab_addpayment').addClass('active');

        // $('#ctl00_CntAdminEx_Body_btnAdd').val('Update');

        __doPostBack('.edit', '');
    });

    $(document).on('click', '.delete', function () {
        $('#ctl00_CntAdminEx_Body_txtDelDispatchID').val($(this).data('dispatch-id'));
        $('#ctl00_CntAdminEx_Body_txtDelCommodityName').val($(this).data('commodity-name'));
        $('#ctl00_CntAdminEx_Body_txtDelHidden').val($(this).data('dispatch-id'));
        $('#modDeleteDispatchStock').modal('show');
    });

    $('#btnCancelDeleteDispatchStock').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelDispatchID').val("");
        $('#ctl00_CntAdminEx_Body_txtDelCommodityName').val("");
        $('#ctl00_CntAdminEx_Body_txtDelHidden').val("");
        $('#modDeleteDispatchStock').modal('toggle');
    });

});