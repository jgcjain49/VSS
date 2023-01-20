$(document).ready(function () {
    $('#mnumakememyself').addClass('nav-active');
    $('#ProgramMatser').addClass('active');

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

    $(document).on('click', '.delete', function () {
        $('#ctl00_CntAdminEx_Body_txtDelUserID').val($(this).data('user-id'));
        $('#ctl00_CntAdminEx_Body_txtDelUserName').val($(this).data('user-name'));
        $('#ctl00_CntAdminEx_Body_txtDelHidden').val($(this).data('user-id'));
        $('#modDeleteUser').modal('show');
    });
    //changes by vibha on 27-12-19
    function showPopup() {
        $('#ctl00_CntAdminEx_Body_txtUserID').val($(this).data('user-id'));
        $('#ctl00_CntAdminEx_Body_txtUserName').val($(this).data('user-name'));
        $('#ctl00_CntAdminEx_Body_txtPreReply').val($(this).data('Previous-reply'));
        $('#ctl00_CntAdminEx_Body_txtNewReply').val($(this).data('New-reply'));
        $('#ctl00_CntAdminEx_Body_txtDelHidden').val($(this).data('user-id'));
        $('#modDeleteUser').modal('show');
    };

    $('#btnCancelDeleteUser').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelUserID').val("");
        $('#ctl00_CntAdminEx_Body_txtDelUserName').val("");
        $('#ctl00_CntAdminEx_Body_txtDelHidden').val("");
        $('#modDeleteUser').modal('toggle');
    });
});