"use strict";
var dataTable;

const client = new signalR.HubConnectionBuilder()
    .withUrl("/orderHub")
    .build();
client.on("OrderReceived", newCall => {
    console.log("Order Placed");
    console.log(newCall);
});

$(document).ready(function () {
    loadData();
    client.start();
});


function toggleMe(me) {
    var id = $(me).attr("data-id");
    $.ajax({
        type: 'GET',
        url: "/customer/StatusChange",
        data: "id=" + id,
        success: function (data) {

            if (data) {
               
                $(me).removeClass("fa-toggle-off").removeClass("statusToggleOff").addClass('fa-toggle-on').addClass("statusToggleOn");
            } else {
                $(me).removeClass("fa-toggle-on").removeClass("statusToggleOn").addClass('fa-toggle-off').addClass("statusToggleOff");
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
                              <i class="fa-regular fa-pen-to-square"></i>      
                              </a>
                              <a href="/customer/Delete/${data}" class="btn btn-danger text-white" style="cursor:pointer">
                              <i class="fa-regular fa-trash-can"></i>       
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
<i class= "fa-solid fa-toggle-off statusToggle statusToggleOff" onclick = "toggleMe($(this));" data-id="${data['id']}"></i>
</div > `);

            }
            else {
                $('td:eq(5)', row).html(`<div class="text-center">
<i class= "fa-solid fa-toggle-on statusToggle statusToggleOn" onclick = "toggleMe($(this));" data-id="${data['id']}" >
</i></div > `);

            }

        }

    });
}
