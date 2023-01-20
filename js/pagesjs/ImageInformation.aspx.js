$(document).ready(function () {

    // Set active page as New product notification in side list menu.
    //$('#mnuFreemaster').addClass('nav-active');
    //$('#mnumasterinformationimgmaster1').addClass('active');

    //$('#mnuBusinessmaster').addClass('nav-active');
    //$('#mnumasterinformationimgmaster2').addClass('active');

    
    $('#mnuImgmaster').addClass('nav-active');
    $('#mnumasterinformationimgmaster2').addClass('active');


    $('#ctl00_CntAdminEx_Body_grdInformationImages')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdInformationImages tr:first")))
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

    //$('.delete').click(function () {
    $(document).on('click','.delete',function () {
        $('#ctl00_CntAdminEx_Body_txtDelInformationImgId').val($(this).data('delimageinfo-id'));
        $('#ctl00_CntAdminEx_Body_txtDelInformationTitleName').val($(this).data('delinfo-name'));
        $('#ctl00_CntAdminEx_Body_txtDelInformationRegionalName').val($(this).data('delinfo-regname'));
        $('#ctl00_CntAdminEx_Body_txtDelInformationReason').val("");
        $('#ctl00_CntAdminEx_Body_txtDelInformationImgIdHiden').val($(this).data('delimageinfo-id'));
        $('#ctl00_CntAdminEx_Body_txtDelInformationImagePath').val($(this).data('delinfo-path'));

        $('#modDeleteConfirm').modal('show');
    });

    $('#btnCancelDeleteProduct').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelInformationImgId').val("");
        $('#ctl00_CntAdminEx_Body_txtDelInformationTitleName').val("");
        $('#ctl00_CntAdminEx_Body_txtDelInformationRegionalName').val("");
        $('#ctl00_CntAdminEx_Body_txtDelInformationReason').val("");
        $('#ctl00_CntAdminEx_Body_txtDelInformationImgIdHiden').val("");
        $('#ctl00_CntAdminEx_Body_txtDelInformationImagePath').val("");

        $('#modDeleteConfirm').modal('toggle');
    });

    $(document).on('click','.modify',function () {
    //$('.modify').click(function () {
        $('#ctl00_CntAdminEx_Body_MainImage').attr('src', $(this).data('modifyimageinfo-path'));
        $('#ctl00_CntAdminEx_Body_txtImgName').val($(this).data('modifyinfo-name'));
        $('#ctl00_CntAdminEx_Body_txtImgDescription').val($(this).data('modifyinfo-regname'));

        $('#myImageModal').modal('show');
    });

    $(document).on('click', '#btnCancelDeleteInformationImg', function () {
        $('#modDeleteConfirm').modal('toggle');
    });

});