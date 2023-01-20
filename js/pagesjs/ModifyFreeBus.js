$(document).ready(function () {
    // Set active page as New product notification in side list menu.
    if (getParentHtmlName() == "ModifyFreeBusinessMaster.aspx") {
        $('#mnuModifyFreeBusinessmaster').addClass('nav-active');
        $('#mnuModifyFreeBusinessmaster2').addClass('active');
    }
});