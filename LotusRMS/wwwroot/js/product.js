var dataTable;

$(document).ready(function () {
    loadData();
    });
function toggleMe(me) {
    var id = $(me).attr("data-id");
    console.log(id);
    $.ajax({
        type: 'GET',
        url: "/admin/product/statuschange",
        data: "id=" + id,
        success: function (data) {

            console.log(data);
            if (data == true) {

                $(me).removeClass("fa-toggle-off").removeClass("statusToggleOff").addClass('fa-toggle-on').addClass("statusToggleOn");
            } else {

                $(me).removeClass("fa-toggle-on").removeClass("statusToggleOn").addClass('fa-toggle-off').addClass("statusToggleOff");
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
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll",
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
                data: "product_Name"
            },
            {
                data: "product_Description"
            },
            {
                data: "product_Unit"
            },
            {
                data: "product_Type"
            },
            {
                data: "product_Category"
            },
            {
                data: "status"
            },
            {
                data: "id",
                render: function (data) {
                    return `<div class="text-center">
                              <a href="/Admin/Product/Update/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fa-regular fa-pen-to-square"></i>   
                              </a>
                              <a href="/Admin/Product/Delete/${data}" class="btn btn-danger text-white" style="cursor:pointer">
                                 <i class="fa-regular fa-trash-can"></i>  
                              </a>
                            </div>`;
                }
            }

        ],
        searching: true,
        rowCallback: function (row, data) {
            if (data["status"] == false) {
                $('td:eq(5)', row).html(`
<div class="text-center">
<i class= "fa-solid fa-toggle-off statusToggle statusToggleOff" onclick = "toggleMe($(this));" data-id="${data['id']}"></i>
</div > `);

            }
            else {
                $('td:eq(5)', row).html(`<div class="text-center">
<i class= "fa-solid fa-toggle-on statusToggle text-center statusToggleOn" onclick = "toggleMe($(this));" data-id="${data['id']}" >
</i></div > `);

            }

        }

    });
}
