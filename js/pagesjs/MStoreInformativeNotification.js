$(document).ready(function () {

    // Set active page as New product notification in side list menu.
    $('#mnunotification').addClass('nav-active');
    $('#mnunotificationoffers').addClass('active');

    var oTable = $('#ctl00_CntAdminEx_Body_grdInformation')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdInformation tr:first")))
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

    $('.adv-table').css('visibility', 'visible');

    $('#ctl00_CntAdminEx_Body_grdViewNotifications')
    .prepend($("<thead></thead>")
    .append($(this).find("#ctl00_CntAdminEx_Body_grdViewNotifications tr:first")))
    .dataTable({
        'aoColumnDefs': [{
            'bSortable': false,
            'aTargets': ['nosort']
        }]
    });

    //$('#dynamic-table').dataTable();

    $('.adv-table').css('visibility', 'visible');

    // So that we can even listen to dynamic content
    var inProgress = false;

    $(document).on('ifToggled', '.iCheck-Radio-Grid-Button', function () {
        if (inProgress == false) {
            inProgress = true;
            $('.iCheck-Radio-Grid-Button').iCheck('uncheck');
            $(this).iCheck('check');
            inProgress = false;
        }
    });

    //$('.iCheck-Radio-Grid-Button')


    /*$('#chkAllUsers').on('ifToggled', function () {
        if ($('#chkAllUsers').is(":checked"))
            $('#ctl00_CntAdminEx_Body_grdUsers input[type=checkbox]').iCheck('check');
        else
            $('#ctl00_CntAdminEx_Body_grdUsers input[type=checkbox]').iCheck('uncheck');
    });*/
});