$(document).ready(function () {
    //var i = 1;
    // Set active page as New product notification in side list menu.
    if (getParentHtmlName() == "SpecificationCategoryMaster.aspx") {
        $('#mnuProductDetailmaster').addClass('nav-active');
        $('#mnumasterproductspecifycatmaster').addClass('active');
    }
    else {
        $('#mnuProductDetailmaster').addClass('nav-active');
        $('#mnumasterproductspecifyvaluemaster').addClass('active');
    }

    $('#dynamic-table').dataTable();

    $('#ctl00_CntAdminEx_Body_grdProductSpecification')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdProductSpecification tr:first")))
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

    //Code to dynamically delete Subcategory for each Specification Category 
    $(document).on('click', '.editSubCatRow', function () {
        //var par = $(this).parent().parent();
        // par.remove();
        $('#txtSpecificationSubcategoryEng').val($(this).data('subcateng-name'));
        $('#txtSpecificationSubcategoryReg').val($(this).data('subcatreg-name'));
        $('#modSubcategorySpecification').modal('show');
    });


    $('.showSubCat').click(function () {
        //Code for product specification  
        $('#ctl00_CntAdminEx_Body_txtSCategoryname').val($(this).data('categoryspecify-name'));
        //$('#ctl00_CntAdminEx_Body_txtImgPathMain').val($(this).data('subcategoryspecify-eng'));
        //$('#ctl00_CntAdminEx_Body_txtImgPathMain').val($(this).data('subcategoryspecify-reg'));

        $('#modViewSubcat').modal('show');
    });

    $(document).on('click', '.delete', function () {
        // $('').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelProdId').val($(this).data('prodspecifycat-id'));
        $('#ctl00_CntAdminEx_Body_txtDelProdName').val($(this).data('prodspecifycat-engname'));
        $('#ctl00_CntAdminEx_Body_txtDelProdRegname').val($(this).data('prodspecifycat-regname'));
        $('#ctl00_CntAdminEx_Body_txtDeleteReason').val("");
        $('#ctl00_CntAdminEx_Body_txtDelProdIdHiden').val($(this).data('prodspecifycat-id'));

        $('#modDeleteConfirm').modal('show');
    });

    $('#btnCancelDeleteProduct').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelProdId').val("");
        $('#ctl00_CntAdminEx_Body_txtDelProdName').val("");
        $('#ctl00_CntAdminEx_Body_txtDelProdRegname').val("");
        $('#ctl00_CntAdminEx_Body_txtDeleteReason').val("");
        $('#ctl00_CntAdminEx_Body_txtDelProdIdHiden').val("");

        $('#modDeleteConfirm').modal('toggle');
    });

});

