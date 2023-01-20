$(document).ready(function () {
    $('#mnuCreateFuture').addClass('nav-active');
    //$('#mnumasterusermaster').addClass('active');

    var oTable = $('#ctl00_CntAdminEx_Body_grdFuture')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdFuture tr:first")))
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


    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href").toString().substring(1, 50);
        $('#ctl00_CntAdminEx_Body_activeTab').val(target);
    });

    var activateTab = $('#ctl00_CntAdminEx_Body_activeTab').val();
    $('.nav-tabs a[href="#' + activateTab + '"]').tab('show');

    $(document).on('click', '.edit', function () {
        $('#ctl00_CntAdminEx_Body_HidIsEditMode').val(true);
        $('#ctl00_CntAdminEx_Body_HidBnkId').val($(this).data('user-id'));
        //  $('#ctl00_CntAdminEx_Body_txtBanknm').val($(this).data('bnk-name'));
        // $('#modDeleteConfirm').modal('show');
        $('#tab_modifyPayment').removeClass('active');
        $('#tab_addpayment').addClass('active');

        // $('#ctl00_CntAdminEx_Body_btnAdd').val('Update');

        __doPostBack('.edit', '');
    });

    $(document).on('click', '.delete', function () {
        $('#ctl00_CntAdminEx_Body_txtDelFut_OptID').val($(this).data('user-id'));
        $('#ctl00_CntAdminEx_Body_txtDelFut_OptName').val($(this).data('user-name'));
        $('#ctl00_CntAdminEx_Body_txtDelHidden').val($(this).data('user-id'));
        $('#modDeleteFuture_opt').modal('toggle');
    });

    $('#btnCancelDeleteFuture_opt').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelFut_OptID').val("");
        $('#ctl00_CntAdminEx_Body_txtDelFut_OptName').val("");
        $('#ctl00_CntAdminEx_Body_txtDelHidden').val("");
        $('#modDeleteFuture_opt').modal('toggle');
    });

});