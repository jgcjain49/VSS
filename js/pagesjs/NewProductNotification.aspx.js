function fnFormatDetails(oTable, nTr)
{
    var aData = oTable.fnGetData( nTr );
    var sOut = '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">';
    sOut += '<tr><td>Rendering engine:</td><td>'+aData[1]+' '+aData[4]+'</td></tr>';
    sOut += '<tr><td>Link to source:</td><td>Could provide a link here</td></tr>';
    sOut += '<tr><td>Extra info:</td><td>And any further details here (images etc)</td></tr>';
    sOut += '</table>';

    return sOut;
}

$(document).ready(function () {

    // Set active page as New product notification in side list menu.
    $('#mnunotification').addClass('nav-active');
    $('#mnunotificationproducts').addClass('active');

    /*$('#ctl00_CntAdminEx_Body_grdUsers')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdUsers tr:first")))
        .dataTable();
    $('#ctl00_CntAdminEx_Body_grdProducts')
        .prepend($("<thead></thead>")
        .append($(this).find("#ctl00_CntAdminEx_Body_grdProducts tr:first")))
        .dataTable();*/

    $('.dynamic-table-grid').each(function () {

        //Todo : Printing function not yet working.
        var gridid = '#' + this.id;
        var firstrow = $($(this).find("tr:first"));
        var printingDom = "";
        var createBasicTable = true;
        try{
            var sortings = [];
            var strSortingData = $(this).data('sorting');
            
            if (eval($(this).data('add-printing-function'))) {
                //printingDom = "<'row'<'col-lg-6'l><'col-lg-6'f>r>t<'row'<'col-lg-6'i><'col-lg-6'p>>";
                printingDom = 'T<"clear">lfrtip';
            }
            if (strSortingData != "") {
                if (strSortingData.indexOf(",") > 0) {
                    var sortArrBasic = strSortingData.split(",");
                    for (var iSortCnt = 0; iSortCnt < sortArrBasic.length; iSortCnt++) {
                        var tempSortDetail = sortArrBasic[iSortCnt];
                        if (tempSortDetail.indexOf(":") > 0) {
                            var sortsArr = tempSortDetail.split(":");
                            sortings.push([parseInt(sortsArr[0]), (sortsArr[1].toString() == "d" ? "desc" : "asc")]);
                        }
                        else {
                            sortings.push([parseInt(tempSortDetail), "asc"]);
                        }
                    }
                }
                else {
                    if (strSortingData.indexOf(":") > 0) {
                        var sortsArr = strSortingData.split(":");
                        sortings.push([parseInt(sortsArr[0]), (sortsArr[1].toString() == "d" ? "desc" : "asc")]);
                    }
                    else {
                        sortings.push([parseInt(strSortingData), "asc"]);
                    }
                }
            }
        
            if (sortings.length > 0)
            {
                createBasicTable = false;
                if (printingDom == "") {
                    $(gridid)
                        .prepend($("<thead></thead>")
                        .append(firstrow))
                        .dataTable({
                            "aaSorting": sortings
                        });
                }
                else {
                    $(gridid)
                        .prepend($("<thead></thead>")
                        .append(firstrow))
                        .dataTable({
                            "aaSorting": sortings,
                            "dom": printingDom,
                            "tableTools": {
                                "sSwfPath": "http://cdn.datatables.net/tabletools/2.2.2/swf/copy_csv_xls_pdf.swf"
                            }
                        });
                }
            }
        } catch (e) { console.log('There is error in javascript'); createBasicTable = true;}
        if (createBasicTable) {
            if (printingDom != "") {
                console.log('Create basic table with printing option');
                $(gridid)
                   .prepend($("<thead></thead>")
                   .append(firstrow))
                   .dataTable({
                       "sDom": printingDom
                   });
            }
            else {
                console.log('Create basic table');
                $(gridid)
                    .prepend($("<thead></thead>")
                    .append(firstrow))
                    .dataTable();
            }
        }
    });

    $('#chkAllUsers').on('ifToggled', function () {
        if ($('#chkAllUsers').is(":checked"))
            $('#ctl00_CntAdminEx_Body_grdUsers input[type=checkbox]').iCheck('check');
        else
            $('#ctl00_CntAdminEx_Body_grdUsers input[type=checkbox]').iCheck('uncheck');
    });


    /*
     * Insert a 'details' column to the table
     */
    var nCloneTh = document.createElement( 'th' );
    var nCloneTd = document.createElement( 'td' );
    nCloneTd.innerHTML = '<img src="AdminExContent/images/details_open.png">';
    nCloneTd.className = "center";

    $('#hidden-table-info thead tr').each( function () {
        this.insertBefore( nCloneTh, this.childNodes[0] );
    } );

    $('#hidden-table-info tbody tr').each( function () {
        this.insertBefore(  nCloneTd.cloneNode( true ), this.childNodes[0] );
    } );

    /*
     * Initialse DataTables, with no sorting on the 'details' column
     */
    var oTable = $('#hidden-table-info').dataTable( {
        "aoColumnDefs": [
            { "bSortable": false, "aTargets": [ 0 ] }
        ],
        "aaSorting": [[1, 'asc']]
    });

    /* Add event listener for opening and closing details
     * Note that the indicator for showing which row is open is not controlled by DataTables,
     * rather it is done here
     */
    $(document).on('click','#hidden-table-info tbody td img',function () {
        var nTr = $(this).parents('tr')[0];
        if ( oTable.fnIsOpen(nTr) )
        {
            /* This row is already open - close it */
            this.src = "AdminExContent/images/details_open.png";
            oTable.fnClose( nTr );
        }
        else
        {
            /* Open this row */
            this.src = "AdminExContent/images/details_close.png";
            oTable.fnOpen( nTr, fnFormatDetails(oTable, nTr), 'details' );
        }
    } );
} );