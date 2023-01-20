$(document).ready(function () {

    var oTable = $('#ctl00_CntAdminEx_Body_grdBlog')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdBlog tr:first")))
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

    var dialogOpen = $('#ctl00_CntAdminEx_Body_HiddenFieldForDialogOpenClose').val();
    if (dialogOpen == 'o') {
        $('#modEditBlog').modal('show');
    }

    $(document).on('click', '.delete', function () {
        $('#ctl00_CntAdminEx_Body_txtDelBlogID').val($(this).data('blog-id'));
        $('#ctl00_CntAdminEx_Body_txtDelBlogName').val($(this).data('blog-name'));
        $('#ctl00_CntAdminEx_Body_txtDelBlogIDHidden').val($(this).data('blog-id'));
        $('#ctl00_CntAdminEx_Body_txtDelBlogPath').val($(this).data('blog-image'));
        $('#modDeleteConfirm').modal('show');
    });

    $('#btnCancelDeleteProduct').click(function () {
        $('#ctl00_CntAdminEx_Body_txtDelBlogID').val("");
        $('#ctl00_CntAdminEx_Body_txtDelBlogName').val("");
        $('#ctl00_CntAdminEx_Body_txtDelBlogIDHidden').val("");
        $('#ctl00_CntAdminEx_Body_txtDelBlogPath').val("");
        $('#modDeleteConfirm').modal('toggle');
    });
    $(document).on('click', '.edit', function () {
        $('#ctl00_CntAdminEx_Body_txtEditBlogID').val($(this).data('blog-id'));
        $('#ctl00_CntAdminEx_Body_txtEditName').val($(this).data('blog-name'));
        //var a = $(this).data('blog-date');
        //alert(a.toString('mm/dd/yyyy'));
        $('#ctl00_CntAdminEx_Body_dtEditDate').val($(this).data('blog-date'));
        //$('#ctl00_CntAdminEx_Body_drpEditFS').val($(this).data('blog-fontstyle'));
        //$('#ctl00_CntAdminEx_Body_drpEditSize').val($(this).data('blog-fontsize'));
        $('#edtTxtPara1').val($(this).data('blog-descr'));
        $('#ctl00_CntAdminEx_Body_edtHidPara1').val($(this).data('blog-descr'));
        //$('#ctl00_CntAdminEx_Body_drpEditFS1').val($(this).data('blog-fontstyle1'));
        //$('#ctl00_CntAdminEx_Body_drpEditSize1').val($(this).data('blog-fontsize1'));
        $('#edtTxtPara2').val($(this).data('blog-descr1'));
        $('#ctl00_CntAdminEx_Body_edtHidPara2').val($(this).data('blog-descr1'));
        //$('#ctl00_CntAdminEx_Body_drpEditFS2').val($(this).data('blog-fontstyle2'));
        //$('#ctl00_CntAdminEx_Body_drpEditSize2').val($(this).data('blog-fontsize2'));
        $('#edtTxtPara3').val($(this).data('blog-descr2'));
        $('#ctl00_CntAdminEx_Body_edtHidPara3').val($(this).data('blog-descr2'));

        $('#ctl00_CntAdminEx_Body_drpEditImageAlign').val($(this).data('blog-imagealign'));
        $('#ctl00_CntAdminEx_Body_edttxtImageText').val($(this).data('blog-image'));
        $('#ctl00_CntAdminEx_Body_HiddenFieldBlog').val($(this).data('blog-id'));
      //  $('#ctl00_CntAdminEx_Body_HiddenFieldForDialogOpenClose').attr('value', 'o')

        $('#modEditBlog').modal('show');
        // for edit 

        $("#edtTxtPara1").jqte({
            blur: function () {
                document.getElementById('ctl00_CntAdminEx_Body_edtHidPara1').value = document.getElementById('edtTxtPara1').value;
            }
        });
        $("#edtTxtPara2").jqte({
            blur: function () {
                document.getElementById('ctl00_CntAdminEx_Body_edtHidPara2').value = document.getElementById('edtTxtPara2').value;
            }
        });
        $("#edtTxtPara3").jqte({
            blur: function () {
                document.getElementById('ctl00_CntAdminEx_Body_edtHidPara3').value = document.getElementById('edtTxtPara3').value;
            }
        });
    });
    $('#btnCancel').click(function () {

        $('#ctl00_CntAdminEx_Body_txtEditBlogID').val("");
        $('#ctl00_CntAdminEx_Body_txtEditName').val("");
        $('#ctl00_CntAdminEx_Body_dtEditDate').val("");
        $('#ctl00_CntAdminEx_Body_drpEditFS').val("");
        $('#ctl00_CntAdminEx_Body_drpEditSize').val("");
        $('#ctl00_CntAdminEx_Body_txtEditBlogPara1').val("");
        $('#ctl00_CntAdminEx_Body_drpEditFS1').val("");
        $('#ctl00_CntAdminEx_Body_drpEditSize1').val("");
        $('#ctl00_CntAdminEx_Body_txtEditBlogPara2').val("");
        $('#ctl00_CntAdminEx_Body_drpEditFS2').val("");
        $('#ctl00_CntAdminEx_Body_drpEditSize2').val("");
        $('#ctl00_CntAdminEx_Body_txtEditBlogPara3').val("");

        $('#ctl00_CntAdminEx_Body_drpEditImageAlign').val("");
        $('#ctl00_CntAdminEx_Body_HiddenImage').val("");
        $('#ctl00_CntAdminEx_Body_HiddenFieldForDialogOpenClose').attr('value', 'c')
        $('#modEditBlog').modal('toggle');
    });
    $(document).on('click', '.image', function () {
        //alert();
        $('#ctl00_CntAdminEx_Body_hidBlogId').val($(this).data('blog-id'));
        $('#ctl00_CntAdminEx_Body_MainImage').attr("src", $(this).data('blog-image'));
        
        $('#modBlogImages').modal('show');
    });

    document.getElementById("ctl00_CntAdminEx_Body_dtDate").max = new Date().toISOString().substring(0, 10);
    document.getElementById("ctl00_CntAdminEx_Body_dtEditDate").max = new Date().toISOString().substring(0, 10);
});

$("#txtPara1").jqte({
    blur: function () {
        document.getElementById('ctl00_CntAdminEx_Body_hidPara1').value = document.getElementById('txtPara1').value;
    }
});
$("#txtPara2").jqte({
    blur: function () {
        document.getElementById('ctl00_CntAdminEx_Body_hidPara2').value = document.getElementById('txtPara2').value;
    }
});
$("#txtPara3").jqte({
    blur: function () {
        document.getElementById('ctl00_CntAdminEx_Body_hidPara3').value = document.getElementById('txtPara3').value;
    }
});

function clear() {
    document.getElementById('txtPara1').value = "";
    document.getElementById('txtPara2').value = "";
    document.getElementById('txtPara3').value = "";
}

function clear() {
    document.getElementById('edtTxtPara1').value = "";
    document.getElementById('edtTxtPara2').value = "";
    document.getElementById('edtTxtPara3').value = "";
}