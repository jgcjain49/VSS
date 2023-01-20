$(document).ready(function () {
    // Set active page as New product notification in side list menu.
    $('#mnuAdmaster').addClass('nav-active');
    if (getParentHtmlName() == "StaticAd.aspx") { $('#mnumasterstaticadsmaster').addClass('active'); }

    $('#ctl00_CntAdminEx_Body_grdAdInfo')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdAdInfo tr:first")))
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

    $(document).on('click','.delete',function () {
    //$('.delete').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelAdID').val($(this).data('ad-id'));
        $('#ctl00_CntAdminEx_Body_txtAdIdHidden').val($(this).data('ad-id'));
        $('#ctl00_CntAdminEx_Body_txtDelAdTitle').val($(this).data('adtitle-name'));
        $('#ctl00_CntAdminEx_Body_txtDelAdText').val($(this).data('adtext-name'));
        $('#ctl00_CntAdminEx_Body_txtDelPath').val($(this).data('ad-imagepath'));

        $('#modDeleteInformation').modal('show');
    });

    $('#modDeleteInformation').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelAdID').val("");
        $('#ctl00_CntAdminEx_Body_txtAdIdHidden').val("");
        $('#ctl00_CntAdminEx_Body_txtDelAdTitle').val("");
        $('#ctl00_CntAdminEx_Body_txtDelAdText').val("");
        $('#ctl00_CntAdminEx_Body_txtDelPath').val("");

        $('#modDeleteInformation').modal('toggle');
    });


});