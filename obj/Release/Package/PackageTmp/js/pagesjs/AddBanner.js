$(document).ready(function () {
    // Set active page as New product notification in side list menu.


    $('#mnuaddBanner').addClass('active');




    $('#ctl00_CntAdminEx_Body_grdProducts')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdProducts tr:first")))
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

    var dialogOpen = $('#ctl00_CntAdminEx_Body_HiddenFieldForDialogOpenClose').val();
    if (dialogOpen == 'o') {
        $('#modEditProducts').modal('show');
    }
    $(document).on('click', '.delete', function () {

        $('#ctl00_CntAdminEx_Body_txtDelCatID').val($(this).data('cat-id'));
        $('#ctl00_CntAdminEx_Body_txtDelCatName').val($(this).data('cat-name'));
        $('#ctl00_CntAdminEx_Body_txtDelCatIDHidden').val($(this).data('cat-id'));
        $('#ctl00_CntAdminEx_Body_txtDelInformation').val($(this).data('cat-info'));

        $('#modDeleteConfirm').modal('show');
    });

    $('#btnCancelDeleteProduct').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelCatID').val($(this).data('cat-id'));
        $('#ctl00_CntAdminEx_Body_txtDelCatName').val($(this).data('cat-name'));
        $('#ctl00_CntAdminEx_Body_txtDelCatIDHidden').val($(this).data('cat-id'));
        $('#ctl00_CntAdminEx_Body_txtDelInformation').val($(this).data('cat-info'));
        $('#modDeleteConfirm').modal('toggle');
    });

    $('#btnCancel').click(function () {

        $('#ctl00_CntAdminEx_Body_txtEditProdID').val("");
        $('#ctl00_CntAdminEx_Body_drpEditInfo').val("");

        $('#ctl00_CntAdminEx_Body_txtEditProdName').val("");
        $('#ctl00_CntAdminEx_Body_txtEditRegProdName').val("");
        $('#ctl00_CntAdminEx_Body_txtEditPrice').val("");

        $('#ctl00_CntAdminEx_Body_txtEditDisPer').val("");
        $('#ctl00_CntAdminEx_Body_txtEditDisPrice').val("");
        $('#ctl00_CntAdminEx_Body_txtEditProductDes').val("");

        $('#ctl00_CntAdminEx_Body_txtEditRegProdDescription').val("");
        $('#ctl00_CntAdminEx_Body_drpEditIsActive').val("");

        $('#ctl00_CntAdminEx_Body_HiddenFieldForDialogOpenClose').attr('value', 'c')
        $('#modEditProducts').modal('toggle');
    });


    // $('.edit').click(function ()
    $(document).on('click', '.edit', function () {
        $('#ctl00_CntAdminEx_Body_txtEditProdID').val($(this).data('prod-id'));
        $('#ctl00_CntAdminEx_Body_drpEditInfo').attr('value', $(this).data('prod-infoid')).change();

        $('#ctl00_CntAdminEx_Body_txtEditProdName').val($(this).data('prod-name'));
        $('#ctl00_CntAdminEx_Body_txtEditRegProdName').val($(this).data('prod-regname'));
        $('#ctl00_CntAdminEx_Body_txtEditPrice').val($(this).data('prod-price'));

        $('#ctl00_CntAdminEx_Body_txtEditDisPer').val($(this).data('prod-disper'));
        $('#ctl00_CntAdminEx_Body_txtEditDisPrice').val($(this).data('prod-discountprice'));
        $('#ctl00_CntAdminEx_Body_txtEditProductDes').val($(this).data('prod-des'));

        $('#ctl00_CntAdminEx_Body_txtEditRegProdDescription').val($(this).data('prod-regdes'));


        $('#ctl00_CntAdminEx_Body_HiddenSubCat').val($(this).data('prod-subcat-id'));
        $('#ctl00_CntAdminEx_Body_HiddenCat').val($(this).data('prod-cat-id'));

        $('#ctl00_CntAdminEx_Body_HiddenFieldInfo').val($(this).data('prod-infoid'));

        $('#ctl00_CntAdminEx_Body_drpEditIsActive').attr('value', $(this).data('prod-isactiveval'));
        $('#ctl00_CntAdminEx_Body_HiddenFieldForDialogOpenClose').attr('value', 'o')

        $('#modEditProducts').modal('show');
    });
});

function ShowModal() {
    $('#modImageGallery').modal('show');
};
