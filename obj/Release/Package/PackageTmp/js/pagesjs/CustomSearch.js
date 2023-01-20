$(document).ready(function () {
    // Set active page as New product notification in side list menu.


    $('#mnuCustomSearch').addClass('nav-active');
    $('#mnuCustomSearch').addClass('active');



    var oTable = $('#ctl00_CntAdminEx_Body_GridView1')
      .prepend($("<thead></thead>")
      .append($(this).find("#ctl00_CntAdminEx_Body_GridView1 tr:first")))
      .dataTable({
          "scrollY": '63.5vh',
          "scrollX": true,
          'aoColumnDefs': [{
              'bSortable': false,
              'aTargets': ['nosort']
          },
          {
              'bVisible': false,
              'aTargets': ['col-hidden-to-clnt']
          }]
      });
    var oTable = $('#ctl00_CntAdminEx_Body_GridView2')
      .prepend($("<thead></thead>")
      .append($(this).find("#ctl00_CntAdminEx_Body_GridView2 tr:first")))
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
    var oTable = $('#ctl00_CntAdminEx_Body_GridView3')
      .prepend($("<thead></thead>")
      .append($(this).find("#ctl00_CntAdminEx_Body_GridView3 tr:first")))
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
    var oTable = $('#ctl00_CntAdminEx_Body_GridView4')
      .prepend($("<thead></thead>")
      .append($(this).find("#ctl00_CntAdminEx_Body_GridView4 tr:first")))
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

});