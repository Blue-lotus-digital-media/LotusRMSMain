﻿var dataTable;

$(document).ready(function () {
    loadData();
});
function toggleMe(me) {
    var id = $(me).attr("data-id");
    console.log(id);
    $.ajax({
        type: 'GET',
        url: "/admin/category/statuschange",
        data: "id=" + id,
        success: function (data) {

            console.log(data);
            if (data == true) {

                $(me).removeClass("bi-toggle-off").addClass('bi-toggle-on');
            } else {

                $(me).removeClass("bi-toggle-on").addClass('bi-toggle-off');
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });

    /* if ($(me).hasClass("bi-toggle-on")) {
 
 
         $(me).removeClass("bi-toggle-on").addClass('bi-toggle-off');
     } else {
         $(me).removeClass("bi-toggle-off").addClass('bi-toggle-on');
     }*/
}
function loadData() {
    console.log("i m here ");
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/Admin/Category/GetAll",
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
                data: "category_Name"

            },
            {
                data: "category_Description"
            },
            {
                data: "type_Name"
            },
            {
                data: "status"

            },
            {
                data: "id",

                render: function (data) {
                    return `<div class="text-center">
                              <a href="/Admin/Category/Update/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="bi bi-pencil-square"></i>    
                              </a>
                              <a href="/Admin/Category/Delete/${data}" class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="bi bi-trash"></i>    
                              </a>
                            </div>`;
                }
            }

        ],
        searching: true,
        rowCallback: function (row, data) {
            if (data["status"] == false) {
                $('td:eq(3)', row).html(`
<div class="text-center">
<i class= "bi bi-toggle-off statusToggle" onclick = "toggleMe($(this));" data-id="${data['id']}"></i>
</div > `);

            }
            else {
                $('td:eq(3)', row).html(`<div class="text-center">
<i class= "bi bi-toggle-on statusToggle" onclick = "toggleMe($(this));" data-id="${data['id']}" >
</i></div > `);

            }

        }

    });
}
