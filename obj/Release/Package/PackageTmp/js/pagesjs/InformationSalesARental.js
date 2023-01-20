$(document).ready(function () {
    // Set active page as New product notification in side list menu.
    if (getParentHtmlName() == "InformationMaster.aspx") {
        $('#mnuFreemaster').addClass('nav-active');
        $('#mnumasterinformationmaster').addClass('active');
    }
    else if (getParentHtmlName() == "InformationBusinessMaster.aspx") {
        $('#mnuBusinessmaster').addClass('nav-active');
        $('#mnumasterinformationbusinessmaster').addClass('active');
    }
    else {
        $('#mnuSalesRentalmaster').addClass('nav-active');
        $('#mnumastersalesinformationmaster').addClass('active');
    }

    $('#ctl00_CntAdminEx_Body_grdInfo')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdInfo tr:first")))
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
        $('#modEditInformation').modal('show');
    }

    //$('.delete').click(function ()
    $(document).on('click', '.delete', function () {
        $('#ctl00_CntAdminEx_Body_txtDelInfoID').val($(this).data('info-id'));
        $('#ctl00_CntAdminEx_Body_txtDelInfoName').val($(this).data('info-name'));
        $('#ctl00_CntAdminEx_Body_txtInfoHidden').val($(this).data('info-id'));
        $('#modDeleteInformation').modal('show');
    });

    $('#modDeleteInformation').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelInfoID').val("");
        $('#ctl00_CntAdminEx_Body_txtDelInfoName').val("");
        $('#ctl00_CntAdminEx_Body_txtInfoHidden').val("");
        $('#modDeleteInformation').modal('toggle');
    });

    $('#btnCancel').click(function () {
        $('#ctl00_CntAdminEx_Body_txtEditInfoID').val("");
        $('#ctl00_CntAdminEx_Body_HiddenFieldInfo').val("");

        $('#ctl00_CntAdminEx_Body_txtEditInfoName').val("");
        $('#ctl00_CntAdminEx_Body_txtEditRegInfoName').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoCity').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegCity').val("");

        $('#ctl00_CntAdminEx_Body_txtEditInfoAdd').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegAdd').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoEmail').val("");

        $('#ctl00_CntAdminEx_Body_txtEditInfoPhone1').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoPhone2').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoPhone3').val("");

        $('#ctl00_CntAdminEx_Body_txtEditInfoLongitude').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoLatitude').val("");

        $('#ctl00_CntAdminEx_Body_txtEditInfoPinCode').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegPinCode').val("");

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel1').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal1').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel1').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal1').val("");

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel2').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal2').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel2').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal2').val("");


        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel3').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal3').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel3').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal3').val("");

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel4').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal4').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel4').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal4').val("");

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel5').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal5').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel5').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal5').val("");

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel6').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal6').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel6').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal6').val("");

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel7').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal7').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel7').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal7').val("");


        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel8').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal8').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel8').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal8').val("");

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel9').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal9').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel9').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal9').val("");


        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel10').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal10').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel10').val("");
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal10').val("");



        $('#ctl00_CntAdminEx_Body_HiddenFieldForDialogOpenClose').attr('value', 'c')
        $('#modEditInformation').modal('toggle');
    });


    // $('.edit').click(function ()
    $(document).on('click', '.edit', function () {
        $('#ctl00_CntAdminEx_Body_txtEditInfoID').val($(this).data('info-id'));
        $('#ctl00_CntAdminEx_Body_HiddenFieldInfo').val($(this).data('info-id'));

        $('#ctl00_CntAdminEx_Body_HiddenSubCat').val($(this).data('info-subcat'));
        $('#ctl00_CntAdminEx_Body_drpCategorySelect').attr('value', $(this).data('info-cat')).change();

        $('#ctl00_CntAdminEx_Body_txtEditInfoName').val($(this).data('info-name'));
        $('#ctl00_CntAdminEx_Body_txtEditRegInfoName').val($(this).data('info-regname'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoCity').val($(this).data('info-city'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegCity').val($(this).data('info-regcity'));

        $('#ctl00_CntAdminEx_Body_txtEditInfoAdd').val($(this).data('info-add'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegAdd').val($(this).data('info-regadd'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoEmail').val($(this).data('info-email'));

        $('#ctl00_CntAdminEx_Body_txtEditInfoPhone1').val($(this).data('info-phone1'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoPhone2').val($(this).data('info-phone2'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoPhone3').val($(this).data('info-phone3'));

        $('#ctl00_CntAdminEx_Body_txtEditInfoLongitude').val($(this).data('info-long'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoLatitude').val($(this).data('info-lat'));

        $('#ctl00_CntAdminEx_Body_txtEditInfoPinCode').val($(this).data('info-pincode'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegPinCode').val($(this).data('info-regpincode'));

        $('#ctl00_CntAdminEx_Body_txtModifyUrl').val($(this).data('info-infourl'));

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel1').val($(this).data('info-extralabel1'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal1').val($(this).data('info-extraval1'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel1').val($(this).data('info-regextralabel1'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal1').val($(this).data('info-regextraval1'));

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel2').val($(this).data('info-extralabel2'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal2').val($(this).data('info-extraval2'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel2').val($(this).data('info-regextralabel2'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal2').val($(this).data('info-regextraval2'));


        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel3').val($(this).data('info-extralabel3'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal3').val($(this).data('info-extraval3'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel3').val($(this).data('info-regextralabel3'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal3').val($(this).data('info-regextraval3'));

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel4').val($(this).data('info-extralabel4'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal4').val($(this).data('info-extraval4'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel4').val($(this).data('info-regextralabel4'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal4').val($(this).data('info-regextraval4'));

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel5').val($(this).data('info-extralabel5'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal5').val($(this).data('info-extraval5'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel5').val($(this).data('info-regextralabel5'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal5').val($(this).data('info-regextraval5'));

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel6').val($(this).data('info-extralabel6'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal6').val($(this).data('info-extraval6'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel6').val($(this).data('info-regextralabel6'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal6').val($(this).data('info-regextraval6'));

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel7').val($(this).data('info-extralabel7'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal7').val($(this).data('info-extraval7'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel7').val($(this).data('info-regextralabel7'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal7').val($(this).data('info-regextraval7'));


        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel8').val($(this).data('info-extralabel8'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal8').val($(this).data('info-extraval8'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel8').val($(this).data('info-regextralabel8'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal8').val($(this).data('info-regextraval8'));

        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel9').val($(this).data('info-extralabel9'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal9').val($(this).data('info-extraval9'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel9').val($(this).data('info-regextralabel9'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal9').val($(this).data('info-regextraval9'));


        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraLabel10').val($(this).data('info-extralabel10'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoExtraVal10').val($(this).data('info-extraval10'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraLabel10').val($(this).data('info-regextralabel10'));
        $('#ctl00_CntAdminEx_Body_txtEditInfoRegExtraVal10').val($(this).data('info-regextraval10'));
        $('#ctl00_CntAdminEx_Body_HiddenFieldForDialogOpenClose').attr('value', 'o')

        //$('#ctl00_CntAdminEx_Body_drpSubCategory').attr('value', $(this).data('info-subcat'));

        if ($(this).data('info-type') == 'True') {
            $('#ctl00_CntAdminEx_Body_drdModifyInfoType').val(1);
            $('#ctl00_CntAdminEx_Body_hfInfotype').val(1);
        }
        else {
            $('#ctl00_CntAdminEx_Body_drdModifyInfoType').val(0);
            $('#ctl00_CntAdminEx_Body_hfInfotype').val(0);
        }

        $('#ctl00_CntAdminEx_Body_txtModifyAmount').val($(this).data('info-modifyamt'));
        $('#ctl00_CntAdminEx_Body_txtModifyFromdate').val($(this).data('info-modifyfrmdate'));
        $('#ctl00_CntAdminEx_Body_txtModifyTodate').val($(this).data('info-modifytodate'));

        if ($(this).data('info-ispaidtype') == 'True') {
            $('#ctl00_CntAdminEx_Body_drModifyIsPaid').val(1);
        }
        else {
            $('#ctl00_CntAdminEx_Body_drModifyIsPaid').val(2);
        }

        $('#modEditInformation').modal('show');
    });


});