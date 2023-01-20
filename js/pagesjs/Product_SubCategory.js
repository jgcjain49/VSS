$(document).ready(function () {
    $('#mnuProductDetailmaster').addClass('nav-active');
    $('#mnumasterproductSubCatmaster').addClass('active');

    var oTable = $('#ctl00_CntAdminEx_Body_grdProductSubCategory')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdProductSubCategory tr:first")))
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


    $(document).on('click', '.delete', function () {
        $('#ctl00_CntAdminEx_Body_txtDelSubCatID').val($(this).data('subcat-id'));
        $('#ctl00_CntAdminEx_Body_txtDelCategory').val($(this).data('cat-name'));
        $('#ctl00_CntAdminEx_Body_txtDelSubCatName').val($(this).data('subcat-name'));
        $('#ctl00_CntAdminEx_Body_txtDelSubCatIDHidden').val($(this).data('subcat-id'));
        $('#ctl00_CntAdminEx_Body_txtDelSubInformation').val($(this).data('subcat-info'));
        $('#modDeleteConfirm').modal('show');

    });

    $('#btnCancelDeleteProduct').click(function () {
        $('#ctl00_CntAdminEx_Body_ txtDelSubCatID').val("");
        $('#ctl00_CntAdminEx_Body_ txtDelCategory').val("");
        $('#ctl00_CntAdminEx_Body_txtDelSubCatName').val("");
        $('#ctl00_CntAdminEx_Body_txtDelSubCatIDHidden').val("");
        $('#ctl00_CntAdminEx_Body_txtDelSubInformation').val("");
        $('#modDeleteConfirm').modal('toggle');
    });
});