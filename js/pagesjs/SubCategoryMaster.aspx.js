
$(document).ready(function () {
     // Set active page as New product notification in side list menu.
    
    if (getParentHtmlName() == "SubCategoryMaster.aspx") {
        $('#mnuFreemaster').addClass('nav-active');
        $('#mnumastersubcatmaster').addClass('active');
    }
    else if (getParentHtmlName() == "SubCategorySalesARental.aspx") {
        $('#mnuSalesRentalmaster').addClass('nav-active');
        $('#mnumastersalessubcategorymaster').addClass('active');
    }
    else {
        $('#mnuBusinessmaster').addClass('nav-active');
        $('#mnumastersubcatbusinessmaster').addClass('active');
    }

    $('#dynamic-table').dataTable();



    function emptySubCategoryDetails() {
        bootbox.alert("Please input all fields", function () {//blank
        });
    }


    $(document).on('click', '.info_link', function () {
        var valueA = $(this).text();
        $('#ctl00_CntAdminEx_Body_txtIconvalue').val(valueA);
    });
 
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

    //$('.delete').click(function () {
    $(document).on('click','.delete',function () {
        $('#ctl00_CntAdminEx_Body_txtDelSubCatId').val($(this).data('subcat-id'));
        $('#ctl00_CntAdminEx_Body_txtDelSubCatName').val($(this).data('subcat-name'));
        $('#ctl00_CntAdminEx_Body_txtDeleteReason').val("");
        $('#ctl00_CntAdminEx_Body_txtDelSubCatIdHiden').val($(this).data('subcat-id'));
        $('#ctl00_CntAdminEx_Body_txtDeleteSubCatImage').val($(this).data('subcat-imagepath'));

        $('#modDeleteConfirm').modal('show');
    });


    $('#btnCancelDeleteProduct').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelSubCatId').val("");
        $('#ctl00_CntAdminEx_Body_txtDelSubCatName').val("");
        $('#ctl00_CntAdminEx_Body_txtDeleteReason').val("");
        $('#ctl00_CntAdminEx_Body_txtDelSubCatIdHiden').val("");
        $('#ctl00_CntAdminEx_Body_txtDeleteSubCatImage').val("");

        $('#modDeleteConfirm').modal('toggle');
    });


});