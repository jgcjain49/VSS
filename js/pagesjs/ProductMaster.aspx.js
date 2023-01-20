/// <reference path="../../CategorySalesARental.aspx" />
/// <reference path="../../CategoryBusiness.aspx" />
$(document).ready(function () {

    // Set active page as New product notification in side list menu.

    $('#dynamic-table').dataTable();

    if (getParentHtmlName() == "Category.aspx") {
        $('#mnuFreemaster').addClass('nav-active');
        $('#mnumasterprodmaster').addClass('active');
    }
    else if (getParentHtmlName() == "CategoryBusiness.aspx") {
        $('#mnuBusinessmaster').addClass('nav-active');
        $('#mnumasterbusinesscategorymaster').addClass('active');
    }
    else {
        $('#mnuSalesRentalmaster').addClass('nav-active');
        $('#mnumastersalescategorymaster').addClass('active');
    }

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
        var target = $(e.target).attr("href").toString().substring(1,50);
        $('#ctl00_CntAdminEx_Body_activeTab').val(target);
    });

    var activateTab = $('#ctl00_CntAdminEx_Body_activeTab').val();
    $('.nav-tabs a[href="#' + activateTab + '"]').tab('show');

    $(document).on('click', '.delete', function () {
        $('#ctl00_CntAdminEx_Body_txtDelCatID').val($(this).data('cat-id'));
        $('#ctl00_CntAdminEx_Body_txtDelCatName').val($(this).data('cat-name'));
        $('#ctl00_CntAdminEx_Body_txtDelCatPath').val($(this).data('cat-path'));
        $('#ctl00_CntAdminEx_Body_txtDelCatIDHidden').val($(this).data('cat-id'));

        $('#modDeleteConfirm').modal('show');
    });

    $('#btnCancelDeleteProduct').click(function ()
    {
         $('#ctl00_CntAdminEx_Body_txtDelCatID').val("");
        $('#ctl00_CntAdminEx_Body_txtDelCatName').val("");
        $('#ctl00_CntAdminEx_Body_txtDelCatPath').val("");
        $('#ctl00_CntAdminEx_Body_txtDelCatIDHidden').val("");
        $('#modDeleteConfirm').modal('toggle');
    });

    //Code added by SSK dated 23-07-2015 for icon selection
    $(document).on('click', '.info_link', function () {
        var valueA = $(this).text();
        $('#ctl00_CntAdminEx_Body_txtLogo').val(valueA);
    });

    /*Code by naina*/
    $("#ctl00_CntAdminEx_Body_txtProdName").keypress(function (event) {
        if (event.which == 13) {
            return false;
        }
    });


    $("#ctl00_CntAdminEx_Body_txtProdPrice").keypress(function (event) {
        if (event.which == 13) {
            return false;
        }
    });


    $("#ctl00_CntAdminEx_Body_txtNewPrice").keypress(function (event) {
        if (event.which == 13) {
            return false;
        }
    });
http://localhost:8080/js

    $("#ctl00_CntAdminEx_Body_txtProdQty").keypress(function (event) {
        if (event.which == 13) {
            return false;
        }
    });


    $("#ctl00_CntAdminEx_Body_txtDescription").keypress(function (event) {
        if (event.which == 13) {
            return false;
        }
    });
} );