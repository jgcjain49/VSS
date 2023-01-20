// JQUERY PLUGIN: I append each jQuery object (in an array of jQuery objects) to the currently selected collection.
//jQuery.fn.appendEach = function (arrayOfWrappers) {

//    // Map the array of jQuery objects to an array of
//    // raw DOM nodes.
//    var rawArray = jQuery.map(
//        arrayOfWrappers,
//        function (value, index) {
//            return (value.get());
//        }
//    );

//    // Add the raw DOM array to the current collection.
//    this.append(rawArray);

//    // Return this reference to maintain method chaining.
//    return (this);
//};




////Creating div structure
//function createDiv(val) {
//    return (
//        $("<td><div class='fa-hover col-md-3 col-sm-4'><a class='info_link' href='#' ><i class='fa " + val + "'></i>" + val + "</a></div></td>")
//        );
//}

$(document).ready(function () {

    //Json to create each of div for fontawesome icons.
    //js/pagesjs/font-awesome.json
    //extrahttp://localhost:8080/js/pagesjs/font-awesome.json
    $.getJSON("js/pagesjs/font-awesome.json", function (data) {

        //Create an font items array 
        //var items = [];

        var innerHTML = '<div class="panel-body">';
        innerHTML += '<div class="adv-table">';
        innerHTML += '<table  class="display table table-bordered table-striped" id="dynamic-table">';
        innerHTML += '<thead>';
        innerHTML += '<tr><th>Logo</th><th>Logo</th><th>Logo</th><th>Logo</th><th>Logo</th></tr>';
        innerHTML += '</thead>';
        innerHTML += '<tbody>';
        innerHTML += '<tr>';

        for (var i = 1; i <= data.length; i++) {
            //items.push(createDiv(data[i]));
            innerHTML += '<td><div class="fa-hover col-md-3 col-sm-4"><a class="info_link" href="#" ><i class="fa ' + data[i] + '"></i>' + data[i] + '</a></div></td>';
            if ((i % 5 == 0) || (i == data.length)) {
                innerHTML += '</tr><tr>';
            }
        }

        innerHTML += '<td colpan="5">Note : Press button to Upload Logo</td></tr>';
        innerHTML += '</tbody></table></div></div>';
        $('#bodyFont').append(innerHTML);
 


        //Append each items to body tag in jquery
        //$("#bodyFont").appendEach(items);

    });
});