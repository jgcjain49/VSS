$(function () {

    // Set active page as New product notification in side list menu.
    $('#mnumaster').addClass('nav-active');
    $('#mnumasterprodmaster').addClass('active');
    
    $('.prerview-img-thumb,.fileupload-preview').click(function () {
        var imgUrl = this.children[0].src;
        var caption = $(this.parentNode).data('caption') + ' Preview';

        $('#dlgImgPreview').attr('src', imgUrl);
        $('#ImageName').html(caption);

        $('#modImageFull').modal('show');
    });

    $('#btnClear').click(function () {
        $('.fileupload').fileupload('clear');
    });

    $('#ctl00_CntAdminEx_Body_FileMainImage').on('change', function () {
        $('#ctl00_CntAdminEx_Body_txtImgPathMain').val($(this).val());
    });

    $('#ctl00_CntAdminEx_Body_FileThumb256').on('change', function () {
        $('#ctl00_CntAdminEx_Body_txtThumbPath256').val($(this).val());
    });

    $('#ctl00_CntAdminEx_Body_FileThumb128').on('change', function () {
        $('#ctl00_CntAdminEx_Body_txtThumbPath128').val($(this).val());
    });

    $('#ctl00_CntAdminEx_Body_FileThumb64').on('change', function () {
        $('#ctl00_CntAdminEx_Body_txtThumbPath64').val($(this).val());
    });

    $('#ctl00_CntAdminEx_Body_FileThumb32').on('change', function () {
        $('#ctl00_CntAdminEx_Body_txtThumbPath32').val($(this).val());
    });

    $('#ctl00_CntAdminEx_Body_FileThumb16').on('change', function () {
        $('#ctl00_CntAdminEx_Body_txtThumbPath16').val($(this).val());
    });

});