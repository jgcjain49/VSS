$(document).ready(function () {
    // Set active page as New product notification in side list menu.
    $('#mnuservicemaster').addClass('active');

    $('#ctl00_CntAdminEx_Body_grdServices')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdServices tr:first")))
        .dataTable({
            'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': ['nosort']
            }]
        });

    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href").toString().substring(1, 50);
        $('#ctl00_CntAdminEx_Body_activeTab').val(target);
    });

    var activateTab = $('#ctl00_CntAdminEx_Body_activeTab').val();
    $('.nav-tabs a[href="#' + activateTab + '"]').tab('show');

    /*var dialogOpen = $('#ctl00_CntAdminEx_Body_HiddenFieldForDialogOpenClose').val();
    if (dialogOpen == 'o') {
        $('#').modal('show');
    }*/

    $(document).on('click', '.delete', function () {
        $('#ctl00_CntAdminEx_Body_txtDelServiceId').val($(this).data('service-id'));
        $('#ctl00_CntAdminEx_Body_txtDelServiceIdHiden').val($(this).data('service-id'));

        $('#ctl00_CntAdminEx_Body_txtDelCategory').val($(this).data('service-catname'));
        $('#ctl00_CntAdminEx_Body_txtDelSubCat').val($(this).data('service-subcatname'));
        $('#ctl00_CntAdminEx_Body_txtDelServiceName').val($(this).data('service-name'));
        $('#ctl00_CntAdminEx_Body_txtDeleteReason').val("");

        $('#modDeleteConfirm').modal('show');
    });

    $('#btnCancelDeleteService').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelServiceId').val("");
        $('#ctl00_CntAdminEx_Body_txtDelServiceIdHiden').val("");

        $('#ctl00_CntAdminEx_Body_txtDelCategory').val("");
        $('#ctl00_CntAdminEx_Body_txtDelSubCat').val("");
        $('#ctl00_CntAdminEx_Body_txtDelServiceName').val("");
        $('#ctl00_CntAdminEx_Body_txtDeleteReason').val("");

        $('#modDeleteConfirm').modal('toggle');
    });

    $(document).on('click', '.edit', function () {
        $('#ctl00_CntAdminEx_Body_txtEditServiceId').val($(this).data('service-id'));
        $('#ctl00_CntAdminEx_Body_HdntxtEditServiceId').val($(this).data('service-id'));

        $('#ctl00_CntAdminEx_Body_txtEditCategory').val($(this).data('service-catname'));
        $('#ctl00_CntAdminEx_Body_HdnEditCategory').val($(this).data('service-catid'));

        $('#ctl00_CntAdminEx_Body_txtEditSubCategory').val($(this).data('service-subcatname'));
        $('#ctl00_CntAdminEx_Body_HdnEditSubCategory').val($(this).data('service-subcatid'));

        $('#ctl00_CntAdminEx_Body_txtEditServiceNameEn').val($(this).data('service-name'));
        $('#ctl00_CntAdminEx_Body_txtEditServiceNameReg').val($(this).data('service-regname'));

        $('#modEditService').modal('show');
    });

    $('#btnCanelEdit').click(function () {
        $('#ctl00_CntAdminEx_Body_txtEditServiceId').val("");
        $('#ctl00_CntAdminEx_Body_HdntxtEditServiceId').val("");

        $('#ctl00_CntAdminEx_Body_txtEditCategory').val("");
        $('#ctl00_CntAdminEx_Body_HdnEditCategory').val("");

        $('#ctl00_CntAdminEx_Body_txtEditSubCategory').val("");
        $('#ctl00_CntAdminEx_Body_HdnEditSubCategory').val("");

        $('#ctl00_CntAdminEx_Body_txtEditServiceNameEn').val("");
        $('#ctl00_CntAdminEx_Body_txtEditServiceNameReg').val("");

        $('#modEditService').modal('toggle');
    });

});