$(document).ready(function ()
{

    // Set active page as New product notification in side list menu.
    $('#mnumaster').addClass('nav-active');
    $('#mnumasterprodmaster').addClass('active');

    $('#ctl00_CntAdminEx_Body_grdProducts')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdProducts tr:first")))
        .dataTable({
            'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': ['nosort']
            }]
        });
});