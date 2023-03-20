var dataTable;
$(document).ready(function () {
    loadData();
});


function toggleMe(me) {
    var id = $(me).attr("data-id");
    $.ajax({
        type: 'GET',
        url: "/customer/StatusChange",
        data: "id=" + id,
        success: function (data) {

            if (data) {
               
                $(me).removeClass("bi-toggle-off").addClass('bi-toggle-on');
            } else {
                $(me).removeClass("bi-toggle-on").addClass('bi-toggle-off');
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}
function loadData() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/customer/GetAll",
            /* "success": function (data) {
                 console.log(data);
             }*/
        },
        /* columnDefs: [{
             "defaultContent": "-",
             "targets": "_all"
         }],*/
        "columns": [

            {
                data: "name"

            },
            {
                data: "address",

            },
            {
                data: "contact"
            },
            {
                data: "panOrVat"

            },
            {
                data: "dueAmount"

            },
            {
                data: "status"

            },
            {
                data: "id",

                render: function (data) {
                    return `<div class="text-center">
                              <a href="/customer/Update/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>    
                              </a>
                              <a href="/customer/Delete/${data}" class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="bi bi-trash"></i>    
                              </a>
                            </div>`;
                }
            }

        ],
        searching: false,
        rowCallback: function (row, data) {
           

            if (data["status"] == false) {
                $('td:eq(5)', row).html(`
<div class="text-center">
<i class= "bi bi-toggle-off statusToggle" onclick = "toggleMe($(this));" data-id="${data['id']}"></i>
</div > `);

            }
            else {
                $('td:eq(5)', row).html(`<div class="text-center">
<i class= "bi bi-toggle-on statusToggle" onclick = "toggleMe($(this));" data-id="${data['id']}" >
</i></div > `);

            }

        }

    });
}
