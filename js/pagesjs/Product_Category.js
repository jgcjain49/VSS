$(document).ready(function () {

    $('#mnuProductDetailmaster').addClass('nav-active');
    $('#mnumasterproductCatmaster').addClass('active');

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
        $('#ctl00_CntAdminEx_Body_txtDelCatID').val($(this).data('cat-id'));
        $('#ctl00_CntAdminEx_Body_txtDelCatName').val($(this).data('cat-name'));
        $('#ctl00_CntAdminEx_Body_txtDelCatIDHidden').val($(this).data('cat-id'));
        $('#ctl00_CntAdminEx_Body_txtDelInformation').val($(this).data('cat-info'));
        $('#modDeleteConfirm').modal('show');
    });

    $('#btnCancelDeleteProduct').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelCatID').val("");
        $('#ctl00_CntAdminEx_Body_txtDelCatName').val("");
        $('#ctl00_CntAdminEx_Body_txtDelCatIDHidden').val("");
        $('#ctl00_CntAdminEx_Body_txtDelInformation').val("");
        $('#modDeleteConfirm').modal('toggle');
    });

});