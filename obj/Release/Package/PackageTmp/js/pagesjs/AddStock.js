$(document).ready(function () {
    $('#mnuAddStock').addClass('nav-active');
    //$('#mnumasterusermaster').addClass('active');

    var oTable = $('#ctl00_CntAdminEx_Body_grdUser')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdUser tr:first")))
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
        $('#tab_modifystocks').removeClass('active');
        $('#tab_addstocks').addClass('active');

        // $('#ctl00_CntAdminEx_Body_btnAdd').val('Update');

        __doPostBack('.edit', '');
    });

    $(document).on('click', '.delete', function () {
        $('#ctl00_CntAdminEx_Body_txtDelAdminID').val($(this).data('user-id'));
        $('#ctl00_CntAdminEx_Body_txtDelAdminName').val($(this).data('user-name'));
        $('#ctl00_CntAdminEx_Body_txtDelHidden').val($(this).data('user-id'));
        $('#modDeleteAdmin').modal('toggle');
    });

    $('#btnCancelDeleteAdmin').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelAdminID').val("");
        $('#ctl00_CntAdminEx_Body_txtDelAdminName').val("");
        $('#ctl00_CntAdminEx_Body_txtDelHidden').val("");
        $('#modDeleteAdmin').modal('toggle');
    });

});